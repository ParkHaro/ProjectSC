using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sc.Data;
using Sc.Packet;

namespace Sc.Tests
{
    /// <summary>
    /// 테스트용 API 클라이언트 Mock.
    /// 요청에 대한 응답을 미리 설정하거나 기본 응답을 반환.
    /// </summary>
    public class MockApiClient : IApiClient
    {
        private readonly Dictionary<Type, Func<IRequest, IResponse>> _handlers = new();
        private readonly List<IRequest> _sentRequests = new();

        private bool _isInitialized;
        private string _sessionToken;
        private int _delayMs;
        private bool _shouldFail;
        private int _failErrorCode;
        private string _failErrorMessage;

        /// <summary>
        /// 전송된 모든 요청 목록
        /// </summary>
        public IReadOnlyList<IRequest> SentRequests => _sentRequests;

        /// <summary>
        /// 마지막 전송된 요청
        /// </summary>
        public IRequest LastRequest => _sentRequests.Count > 0
            ? _sentRequests[^1]
            : null;

        /// <summary>
        /// 응답 지연 시간 설정 (ms)
        /// </summary>
        public void SetDelay(int delayMs)
        {
            _delayMs = delayMs;
        }

        /// <summary>
        /// 실패 모드 설정
        /// </summary>
        public void SetFailure(bool shouldFail, int errorCode = -1, string errorMessage = "MOCK_ERROR")
        {
            _shouldFail = shouldFail;
            _failErrorCode = errorCode;
            _failErrorMessage = errorMessage;
        }

        /// <summary>
        /// 특정 요청 타입에 대한 응답 핸들러 등록
        /// </summary>
        public void SetHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            _handlers[typeof(TRequest)] = request => handler((TRequest)request);
        }

        /// <summary>
        /// 요청 기록 초기화
        /// </summary>
        public void ClearRequests()
        {
            _sentRequests.Clear();
        }

        /// <summary>
        /// 모든 설정 초기화
        /// </summary>
        public void Reset()
        {
            _handlers.Clear();
            _sentRequests.Clear();
            _delayMs = 0;
            _shouldFail = false;
            _failErrorCode = 0;
            _failErrorMessage = null;
        }

        public bool IsInitialized => _isInitialized;
        public string SessionToken => _sessionToken;

        public async UniTask<bool> InitializeAsync(string baseUrl)
        {
            if (_delayMs > 0)
            {
                await UniTask.Delay(_delayMs);
            }

            _isInitialized = !_shouldFail;
            _sessionToken = _isInitialized ? "mock_session_token" : null;
            return _isInitialized;
        }

        public async UniTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            _sentRequests.Add(request);

            if (_delayMs > 0)
            {
                await UniTask.Delay(_delayMs);
            }

            var response = await SendAsync(request);
            return (TResponse)response;
        }

        public async UniTask<IResponse> SendAsync(IRequest request)
        {
            _sentRequests.Add(request);

            if (_delayMs > 0)
            {
                await UniTask.Delay(_delayMs);
            }

            // 등록된 핸들러가 있으면 사용
            var requestType = request.GetType();
            if (_handlers.TryGetValue(requestType, out var handler))
            {
                return handler(request);
            }

            // 기본 Mock 응답 생성
            return CreateDefaultResponse(request);
        }

        private IResponse CreateDefaultResponse(IRequest request)
        {
            // 실패 모드이면 실패 응답
            if (_shouldFail)
            {
                return new MockResponse
                {
                    IsSuccess = false,
                    ErrorCode = _failErrorCode,
                    ErrorMessage = _failErrorMessage,
                    ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };
            }

            // 기본 성공 응답
            return new MockResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }
    }

    /// <summary>
    /// Mock 기본 응답
    /// </summary>
    public struct MockResponse : IResponse
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public long ServerTime { get; set; }
    }
}
