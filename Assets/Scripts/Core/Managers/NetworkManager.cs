using System;
using Cysharp.Threading.Tasks;
using Sc.Data;
using Sc.Event.Network;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 네트워크 매니저
    /// HTTP REST + WebSocket 통합 관리
    /// </summary>
    public class NetworkManager : Singleton<NetworkManager>
    {
        [SerializeField]
        private NetworkConfig _config;

        private IApiClient _apiClient;
        private IWebSocketClient _wsClient;
        private RequestQueue _requestQueue;
        private PacketDispatcher _dispatcher;

        private bool _isInitialized;
        private ConnectionState _state = ConnectionState.Disconnected;

        /// <summary>
        /// 초기화 여부
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 연결 상태
        /// </summary>
        public ConnectionState State => _state;

        /// <summary>
        /// 현재 처리 중 여부
        /// </summary>
        public bool IsProcessing => _requestQueue?.IsProcessing ?? false;

        /// <summary>
        /// 대기 중인 요청 수
        /// </summary>
        public int PendingRequestCount => _requestQueue?.PendingCount ?? 0;

        /// <summary>
        /// 네트워크 설정
        /// </summary>
        public NetworkConfig Config => _config;

        /// <summary>
        /// API 클라이언트 (테스트용)
        /// </summary>
        public IApiClient ApiClient => _apiClient;

        protected override void OnSingletonAwake()
        {
            // 설정이 없으면 기본값 생성
            if (_config == null)
            {
                _config = ScriptableObject.CreateInstance<NetworkConfig>();
                Debug.LogWarning("[NetworkManager] NetworkConfig not assigned, using defaults");
            }
        }

        /// <summary>
        /// 네트워크 매니저 초기화
        /// </summary>
        /// <param name="config">네트워크 설정 (null이면 인스펙터 설정 사용)</param>
        public async UniTask<bool> InitializeAsync(NetworkConfig config = null)
        {
            if (_isInitialized)
            {
                Debug.LogWarning("[NetworkManager] Already initialized");
                return true;
            }

            if (config != null)
            {
                _config = config;
            }

            Debug.Log($"[NetworkManager] Initializing (Mode: {_config.Mode})");

            try
            {
                // Dispatcher 생성 및 콜백 등록
                _dispatcher = new PacketDispatcher();
                _dispatcher.OnDispatchCompleted += OnDispatchCompleted;
                _dispatcher.OnDispatchError += OnDispatchError;
                RegisterDefaultHandlers();

                // 모드에 따라 클라이언트 생성
                if (_config.Mode == NetworkMode.Local)
                {
                    _apiClient = new LocalApiClient(_config.SimulatedLatencyMs);
                    _wsClient = new LocalWebSocketClient();
                }
                else
                {
                    // TODO: 실제 서버 클라이언트 구현
                    // _apiClient = new HttpApiClient();
                    // _wsClient = new WebSocketClient();
                    throw new NotImplementedException("Server mode not implemented yet");
                }

                // API 클라이언트 초기화
                var baseUrl = _config.Mode == NetworkMode.Local ? "local" : _config.BaseUrl;
                var success = await _apiClient.InitializeAsync(baseUrl);

                if (!success)
                {
                    Debug.LogError("[NetworkManager] Failed to initialize API client");
                    return false;
                }

                // 요청 큐 생성
                _requestQueue = new RequestQueue(_apiClient, _dispatcher);
                _requestQueue.OnRequestStarted += OnRequestStarted;
                _requestQueue.OnRequestCompleted += OnRequestCompleted;
                _requestQueue.OnRequestFailed += OnRequestFailed;

                _isInitialized = true;
                _state = ConnectionState.Connected;

                Debug.Log("[NetworkManager] Initialized successfully");
                EventManager.Instance.Publish(new NetworkConnectedEvent());

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[NetworkManager] Initialization failed: {ex.Message}");
                _state = ConnectionState.Error;
                return false;
            }
        }

        /// <summary>
        /// 요청 전송 (큐에 추가, 이벤트로 결과 알림)
        /// Request가 IRequest&lt;TResponse&gt;를 구현하면 TResponse를 자동으로 추론
        /// </summary>
        /// <typeparam name="TRequest">요청 타입 (IRequest&lt;TResponse&gt; 구현 필수)</typeparam>
        /// <param name="request">요청 데이터</param>
        /// <example>
        /// NetworkManager.Instance.Send(LoginRequest.CreateGuest("device", "1.0", "Editor"));
        /// NetworkManager.Instance.Send(GachaRequest.CreateMulti("standard"));
        /// </example>
        public void Send<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            if (!_isInitialized)
            {
                Debug.LogError("[NetworkManager] Not initialized");
                EventManager.Instance.Publish(new NetworkErrorEvent
                {
                    ErrorCode = -1,
                    ErrorMessage = "NetworkManager not initialized",
                    IsRecoverable = false
                });
                return;
            }

            _requestQueue.Enqueue(request);
        }

        /// <summary>
        /// WebSocket 연결
        /// </summary>
        public async UniTask ConnectWebSocketAsync()
        {
            if (_wsClient == null)
            {
                Debug.LogError("[NetworkManager] WebSocket client not initialized");
                return;
            }

            if (_wsClient.IsConnected)
            {
                Debug.LogWarning("[NetworkManager] WebSocket already connected");
                return;
            }

            try
            {
                _wsClient.OnMessageReceived += OnWebSocketMessage;
                _wsClient.OnDisconnected += OnWebSocketDisconnected;

                await _wsClient.ConnectAsync(_config.WebSocketUrl);
                Debug.Log("[NetworkManager] WebSocket connected");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[NetworkManager] WebSocket connection failed: {ex.Message}");
            }
        }

        /// <summary>
        /// WebSocket 연결 해제
        /// </summary>
        public async UniTask DisconnectWebSocketAsync()
        {
            if (_wsClient == null || !_wsClient.IsConnected)
            {
                return;
            }

            await _wsClient.DisconnectAsync();
            Debug.Log("[NetworkManager] WebSocket disconnected");
        }

        /// <summary>
        /// 모든 대기 요청 취소
        /// </summary>
        public void CancelAllRequests()
        {
            _requestQueue?.CancelAll();
        }

        /// <summary>
        /// 응답 핸들러 등록
        /// </summary>
        public void RegisterHandler<TResponse>(IPacketHandler<TResponse> handler)
            where TResponse : IResponse
        {
            _dispatcher?.RegisterHandler(handler);
        }

        #region Private Methods

        private void RegisterDefaultHandlers()
        {
            // 기본 핸들러 등록
            _dispatcher.RegisterHandler(new LoginResponseHandler());
            _dispatcher.RegisterHandler(new GachaResponseHandler());
            _dispatcher.RegisterHandler(new PurchaseResponseHandler());
        }

        private void OnRequestStarted(QueuedRequest request)
        {
            EventManager.Instance.Publish(new RequestStartedEvent
            {
                RequestId = request.RequestId,
                RequestType = request.Metadata.RequestType
            });
        }

        private void OnRequestCompleted(QueuedRequest request, IResponse response)
        {
            // Dispatcher에서 처리하므로 여기서는 추가 작업 없음
        }

        private void OnRequestFailed(QueuedRequest request, Exception ex)
        {
            // Dispatcher에서 처리하므로 여기서는 추가 작업 없음
        }

        private void OnDispatchCompleted(QueuedRequest request, IResponse response)
        {
            EventManager.Instance.Publish(new RequestCompletedEvent
            {
                RequestId = request.RequestId,
                RequestType = request.Metadata.RequestType,
                IsSuccess = response.IsSuccess,
                ErrorCode = response.ErrorCode,
                ErrorMessage = response.ErrorMessage
            });
        }

        private void OnDispatchError(QueuedRequest request, Exception ex)
        {
            EventManager.Instance.Publish(new NetworkErrorEvent
            {
                RequestId = request.RequestId,
                RequestType = request.Metadata.RequestType,
                ErrorCode = -1,
                ErrorMessage = ex.Message,
                IsRecoverable = ex is TimeoutException
            });
        }

        private void OnWebSocketMessage(string message)
        {
            try
            {
                var pushMessage = JsonUtility.FromJson<PushMessage>(message);
                EventManager.Instance.Publish(new ServerPushReceivedEvent
                {
                    Type = pushMessage.Type,
                    Payload = pushMessage.Payload
                });
            }
            catch (Exception ex)
            {
                Debug.LogError($"[NetworkManager] Failed to parse push message: {ex.Message}");
            }
        }

        private void OnWebSocketDisconnected(string reason)
        {
            EventManager.Instance.Publish(new NetworkDisconnectedEvent
            {
                Reason = reason,
                IsIntentional = false
            });
        }

        #endregion

        protected override void OnSingletonDestroy()
        {
            _requestQueue?.Dispose();

            if (_dispatcher != null)
            {
                _dispatcher.OnDispatchCompleted -= OnDispatchCompleted;
                _dispatcher.OnDispatchError -= OnDispatchError;
            }

            if (_wsClient != null)
            {
                _wsClient.OnMessageReceived -= OnWebSocketMessage;
                _wsClient.OnDisconnected -= OnWebSocketDisconnected;
            }
        }
    }
}
