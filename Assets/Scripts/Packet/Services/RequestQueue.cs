using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sc.Data;
using UnityEngine;

namespace Sc.Packet
{
    /// <summary>
    /// 요청 순차 처리 큐
    /// FIFO 방식으로 한 번에 하나의 요청만 처리
    /// </summary>
    public class RequestQueue
    {
        private readonly Queue<QueuedRequest> _pendingRequests = new();
        private readonly IApiClient _apiClient;
        private readonly PacketDispatcher _dispatcher;

        private QueuedRequest _currentRequest;
        private bool _isProcessing;
        private CancellationTokenSource _cts;

        /// <summary>
        /// 대기 중인 요청 수
        /// </summary>
        public int PendingCount => _pendingRequests.Count;

        /// <summary>
        /// 현재 처리 중 여부
        /// </summary>
        public bool IsProcessing => _isProcessing;

        /// <summary>
        /// 현재 처리 중인 요청
        /// </summary>
        public QueuedRequest CurrentRequest => _currentRequest;

        /// <summary>
        /// 요청 시작 이벤트
        /// </summary>
        public event Action<QueuedRequest> OnRequestStarted;

        /// <summary>
        /// 요청 완료 이벤트
        /// </summary>
        public event Action<QueuedRequest, IResponse> OnRequestCompleted;

        /// <summary>
        /// 요청 실패 이벤트
        /// </summary>
        public event Action<QueuedRequest, Exception> OnRequestFailed;

        public RequestQueue(IApiClient apiClient, PacketDispatcher dispatcher)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        /// 요청을 큐에 추가 (TResponse 자동 추론)
        /// Request가 IRequest&lt;TResponse&gt;를 구현하면 TResponse를 자동으로 추론
        /// </summary>
        /// <typeparam name="TRequest">요청 타입 (IRequest&lt;TResponse&gt; 구현 필수)</typeparam>
        /// <param name="request">요청 데이터</param>
        public void Enqueue<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            var requestType = typeof(TRequest);
            var responseType = GetResponseType(requestType);

            if (responseType == null)
            {
                throw new InvalidOperationException(
                    $"Request type {requestType.Name} does not implement IRequest<TResponse>");
            }

            var metadata = RequestMetadata.Create<TRequest>();
            var queuedRequest = new QueuedRequest(request, metadata, requestType, responseType);

            _pendingRequests.Enqueue(queuedRequest);
            Debug.Log($"[RequestQueue] Enqueued: {metadata.RequestType} (Pending: {PendingCount})");

            // 처리 시작
            ProcessNextAsync().Forget();
        }

        /// <summary>
        /// 요청을 큐에 추가 (명시적 타입 지정)
        /// </summary>
        /// <typeparam name="TRequest">요청 타입</typeparam>
        /// <typeparam name="TResponse">응답 타입</typeparam>
        /// <param name="request">요청 데이터</param>
        public void Enqueue<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            var metadata = RequestMetadata.Create<TRequest>();
            var queuedRequest = new QueuedRequest(request, metadata, typeof(TRequest), typeof(TResponse));

            _pendingRequests.Enqueue(queuedRequest);
            Debug.Log($"[RequestQueue] Enqueued: {metadata.RequestType} (Pending: {PendingCount})");

            // 처리 시작
            ProcessNextAsync().Forget();
        }

        /// <summary>
        /// IRequest&lt;TResponse&gt; 인터페이스에서 TResponse 타입 추출
        /// </summary>
        private static Type GetResponseType(Type requestType)
        {
            var genericInterface = requestType
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));

            return genericInterface?.GetGenericArguments().FirstOrDefault();
        }

        /// <summary>
        /// 다음 요청 처리
        /// </summary>
        private async UniTaskVoid ProcessNextAsync()
        {
            if (_isProcessing || _pendingRequests.Count == 0)
                return;

            _isProcessing = true;
            _currentRequest = _pendingRequests.Dequeue();

            Debug.Log($"[RequestQueue] Processing: {_currentRequest.Metadata.RequestType}");
            OnRequestStarted?.Invoke(_currentRequest);

            try
            {
                // 리플렉션으로 제네릭 메서드 호출
                var response = await SendRequestAsync(_currentRequest);

                Debug.Log($"[RequestQueue] Completed: {_currentRequest.Metadata.RequestType} (Success: {response?.IsSuccess})");
                OnRequestCompleted?.Invoke(_currentRequest, response);

                // PacketDispatcher로 응답 처리 → 이벤트 발행
                _dispatcher.Dispatch(_currentRequest, response);
            }
            catch (OperationCanceledException)
            {
                Debug.Log($"[RequestQueue] Cancelled: {_currentRequest.Metadata.RequestType}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[RequestQueue] Failed: {_currentRequest.Metadata.RequestType} - {ex.Message}");
                OnRequestFailed?.Invoke(_currentRequest, ex);

                // 에러 이벤트 발행
                _dispatcher.DispatchError(_currentRequest, ex);
            }
            finally
            {
                _currentRequest = null;
                _isProcessing = false;

                // 다음 요청 처리
                ProcessNextAsync().Forget();
            }
        }

        /// <summary>
        /// 실제 요청 전송
        /// </summary>
        private UniTask<IResponse> SendRequestAsync(QueuedRequest queuedRequest)
        {
            // non-generic SendAsync 사용 (리플렉션 없음)
            return _apiClient.SendAsync(queuedRequest.Request);
        }

        /// <summary>
        /// 모든 대기 요청 취소
        /// </summary>
        public void CancelAll()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            _pendingRequests.Clear();
            _currentRequest = null;
            _isProcessing = false;

            Debug.Log("[RequestQueue] All requests cancelled");
        }

        /// <summary>
        /// 특정 요청 취소
        /// </summary>
        /// <param name="requestId">요청 ID</param>
        public bool Cancel(string requestId)
        {
            // 현재 처리 중인 요청은 취소 불가
            if (_currentRequest?.RequestId == requestId)
            {
                Debug.LogWarning($"[RequestQueue] Cannot cancel in-progress request: {requestId}");
                return false;
            }

            // 대기 큐에서 제거
            var tempQueue = new Queue<QueuedRequest>();
            bool found = false;

            while (_pendingRequests.Count > 0)
            {
                var request = _pendingRequests.Dequeue();
                if (request.RequestId == requestId)
                {
                    found = true;
                    Debug.Log($"[RequestQueue] Cancelled: {request.Metadata.RequestType}");
                }
                else
                {
                    tempQueue.Enqueue(request);
                }
            }

            while (tempQueue.Count > 0)
            {
                _pendingRequests.Enqueue(tempQueue.Dequeue());
            }

            return found;
        }

        /// <summary>
        /// 리소스 정리
        /// </summary>
        public void Dispose()
        {
            CancelAll();
            _cts?.Dispose();
        }
    }
}
