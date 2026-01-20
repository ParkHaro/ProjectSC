using System;
using Cysharp.Threading.Tasks;
using Sc.Data;
using Sc.LocalServer;
using UnityEngine;

namespace Sc.Packet
{
    /// <summary>
    /// 로컬 API 클라이언트 (서버 응답 시뮬레이션)
    /// 개발/테스트용 더미 서버 구현
    /// </summary>
    /// <remarks>
    /// 서버 로직은 LocalGameServer로 위임
    /// 이 클래스는 IApiClient 인터페이스 구현 및 저장/로드만 담당
    /// </remarks>
    public class LocalApiClient : IApiClient
    {
        private const string SaveKey = "user_save";

        private readonly int _simulatedLatencyMs;
        private readonly Sc.Foundation.ISaveStorage _storage;
        private readonly LocalGameServer _server;
        private UserSaveData _userData;
        private string _sessionToken;
        private bool _isInitialized;

        public bool IsInitialized => _isInitialized;
        public string SessionToken => _sessionToken;

        /// <summary>
        /// 기본 생성자 (FileSaveStorage 사용)
        /// </summary>
        public LocalApiClient(int simulatedLatencyMs = 100)
            : this(new Sc.Foundation.FileSaveStorage(), simulatedLatencyMs)
        {
        }

        /// <summary>
        /// 의존성 주입 생성자 (테스트용)
        /// </summary>
        public LocalApiClient(Sc.Foundation.ISaveStorage storage, int simulatedLatencyMs = 100)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _simulatedLatencyMs = simulatedLatencyMs;
            _server = new LocalGameServer();
        }

        #region IApiClient Implementation

        public async UniTask<bool> InitializeAsync(string baseUrl)
        {
            if (_isInitialized) return true;

            await UniTask.Delay(_simulatedLatencyMs);

            // 기존 저장 데이터 로드 시도
            if (_storage.Exists(SaveKey))
            {
                var loadResult = _storage.Load(SaveKey);
                if (loadResult.IsSuccess)
                {
                    try
                    {
                        _userData = JsonUtility.FromJson<UserSaveData>(loadResult.Value);

                        // 마이그레이션 적용 (버전 업그레이드)
                        if (_userData.Version < UserSaveData.CurrentVersion)
                        {
                            Debug.Log(
                                $"[LocalApiClient] 데이터 마이그레이션: v{_userData.Version} → v{UserSaveData.CurrentVersion}");
                            _userData = UserSaveData.Migrate(_userData);
                            SaveUserData();
                        }

                        // EventCurrency null 체크 (JSON 역직렬화 특성)
                        if (_userData.EventCurrency.Currencies == null)
                        {
                            _userData.EventCurrency = EventCurrencyData.CreateDefault();
                        }

                        Debug.Log($"[LocalApiClient] 저장 데이터 로드 완료: {_userData.Profile.Nickname}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"[LocalApiClient] 저장 데이터 파싱 실패: {e.Message}");
                        _userData = default;
                    }
                }
                else
                {
                    Debug.LogWarning($"[LocalApiClient] 저장 데이터 로드 실패: {loadResult.Message}");
                }
            }

            _isInitialized = true;
            return true;
        }

        public async UniTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            var response = await SendAsync(request);
            return (TResponse)response;
        }

        public async UniTask<IResponse> SendAsync(IRequest request)
        {
            await UniTask.Delay(_simulatedLatencyMs);

            try
            {
                // 서버로 요청 위임
                var response = _server.HandleRequest(request, ref _userData);

                // 로그인 시 세션 토큰 저장
                if (response is LoginResponse loginResponse && loginResponse.IsSuccess)
                {
                    _sessionToken = loginResponse.SessionToken;
                }

                // 데이터 저장
                SaveUserData();

                return response;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[LocalApiClient] Request failed: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Database Injection

        /// <summary>
        /// ShopProductDatabase 설정 (외부에서 Database 주입)
        /// </summary>
        public void SetShopProductDatabase(ShopProductDatabase database)
        {
            _server?.SetShopProductDatabase(database);
        }

        /// <summary>
        /// LiveEventDatabase 설정 (외부에서 Database 주입)
        /// </summary>
        public void SetEventDatabase(LiveEventDatabase database)
        {
            _server?.SetEventDatabase(database);
        }

        #endregion

        #region Private Methods

        private void SaveUserData()
        {
            _userData.LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var json = JsonUtility.ToJson(_userData, true);
            var result = _storage.Save(SaveKey, json);

            if (result.IsSuccess)
            {
                Debug.Log("[LocalApiClient] 데이터 저장 완료");
            }
            else
            {
                Debug.LogError($"[LocalApiClient] 데이터 저장 실패: {result.Message}");
            }
        }

        #endregion
    }
}