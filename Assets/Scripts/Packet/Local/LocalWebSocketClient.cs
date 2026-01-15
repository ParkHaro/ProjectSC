using System;
using Cysharp.Threading.Tasks;

namespace Sc.Packet
{
    /// <summary>
    /// 로컬 WebSocket 클라이언트 (더미 구현)
    /// 서버 없이 테스트할 때 사용
    /// </summary>
    public class LocalWebSocketClient : IWebSocketClient
    {
        private bool _isConnected;
        private WebSocketState _state = WebSocketState.Disconnected;

        public bool IsConnected => _isConnected;
        public WebSocketState State => _state;

        public event Action<string> OnMessageReceived;
        public event Action OnConnected;
        public event Action<string> OnDisconnected;
        public event Action<Exception> OnError;

        public async UniTask ConnectAsync(string url)
        {
            _state = WebSocketState.Connecting;
            await UniTask.Delay(100);

            _isConnected = true;
            _state = WebSocketState.Connected;
            OnConnected?.Invoke();
        }

        public async UniTask DisconnectAsync()
        {
            if (!_isConnected) return;

            await UniTask.Delay(50);

            _isConnected = false;
            _state = WebSocketState.Disconnected;
            OnDisconnected?.Invoke("Manual disconnect");
        }

        public void Send(string message)
        {
            if (!_isConnected)
            {
                OnError?.Invoke(new InvalidOperationException("Not connected"));
                return;
            }

            // 로컬에서는 전송만 하고 응답 없음
            UnityEngine.Debug.Log($"[LocalWebSocket] Sent: {message}");
        }

        /// <summary>
        /// 테스트용: 수동으로 메시지 수신 시뮬레이션
        /// </summary>
        public void SimulateMessage(string message)
        {
            if (_isConnected)
            {
                OnMessageReceived?.Invoke(message);
            }
        }

        /// <summary>
        /// 테스트용: 수동으로 연결 해제 시뮬레이션
        /// </summary>
        public void SimulateDisconnect(string reason)
        {
            if (_isConnected)
            {
                _isConnected = false;
                _state = WebSocketState.Disconnected;
                OnDisconnected?.Invoke(reason);
            }
        }
    }
}
