using Cysharp.Threading.Tasks;
using Sc.Data;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 게임 초기화 부트스트랩
    /// 테스트 및 실제 게임 시작점
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("설정")]
        [SerializeField] private bool _autoInitialize = true;
        [SerializeField] private string _testNickname = "TestPlayer";

        [Header("상태 (읽기 전용)")]
        [SerializeField] private bool _isInitialized;
        [SerializeField] private string _currentUserId;
        [SerializeField] private int _ownedCharacterCount;
        [SerializeField] private long _currentGold;
        [SerializeField] private int _currentGem;

        private IApiService _apiService;

        private async void Start()
        {
            if (_autoInitialize)
            {
                await InitializeGameAsync();
            }
        }

        /// <summary>
        /// 게임 초기화 (Inspector 버튼용)
        /// </summary>
        [ContextMenu("Initialize Game")]
        public async void InitializeGame()
        {
            await InitializeGameAsync();
        }

        /// <summary>
        /// 게임 초기화 흐름
        /// </summary>
        public async UniTask<bool> InitializeGameAsync()
        {
            Debug.Log("========== 게임 초기화 시작 ==========");

            // 1. API 서비스 생성
            Debug.Log("[1/3] API 서비스 생성...");
            _apiService = new LocalApiService();

            // 2. DataManager 초기화
            Debug.Log("[2/3] DataManager 초기화...");
            var initResult = await DataManager.Instance.InitializeAsync(_apiService);
            if (!initResult)
            {
                Debug.LogError("DataManager 초기화 실패!");
                return false;
            }
            Debug.Log("DataManager 초기화 성공");

            // 3. 로그인
            Debug.Log("[3/3] 로그인 시도...");
            var loginRequest = LoginRequest.CreateGuest(
                SystemInfo.deviceUniqueIdentifier,
                Application.version,
                Application.platform.ToString()
            );

            var loginResponse = await DataManager.Instance.LoginAsync(loginRequest);
            if (!loginResponse.IsSuccess)
            {
                Debug.LogError($"로그인 실패: {loginResponse.ErrorMessage}");
                return false;
            }

            Debug.Log($"로그인 성공! 신규유저: {loginResponse.IsNewUser}");

            // 상태 업데이트
            UpdateStatus();

            Debug.Log("========== 게임 초기화 완료 ==========");
            LogCurrentState();

            _isInitialized = true;
            return true;
        }

        /// <summary>
        /// 가챠 테스트 (Inspector 버튼용)
        /// </summary>
        [ContextMenu("Test Gacha (Single)")]
        public async void TestGachaSingle()
        {
            await TestGachaAsync(GachaPullType.Single);
        }

        [ContextMenu("Test Gacha (Multi)")]
        public async void TestGachaMulti()
        {
            await TestGachaAsync(GachaPullType.Multi);
        }

        private async UniTask TestGachaAsync(GachaPullType pullType)
        {
            if (!_isInitialized)
            {
                Debug.LogWarning("먼저 초기화를 진행하세요!");
                return;
            }

            Debug.Log($"========== 가챠 테스트 ({pullType}) ==========");

            var request = pullType == GachaPullType.Single
                ? GachaRequest.CreateSingle("gacha_standard")
                : GachaRequest.CreateMulti("gacha_standard");

            var response = await _apiService.GachaAsync(request);

            if (response.IsSuccess)
            {
                Debug.Log($"가챠 성공! 결과 {response.Results.Count}개:");
                foreach (var result in response.Results)
                {
                    var marker = result.IsNew ? "[NEW]" : "";
                    var pity = result.IsPity ? "[PITY]" : "";
                    Debug.Log($"  - {result.CharacterId} ({result.Rarity}) {marker}{pity}");
                }

                // Delta 적용
                DataManager.Instance.ApplyDelta(response.Delta);
                Debug.Log($"현재 천장: {response.CurrentPityCount}");

                UpdateStatus();
            }
            else
            {
                Debug.LogError($"가챠 실패: {response.ErrorMessage}");
            }
        }

        /// <summary>
        /// 현재 상태 로그 출력
        /// </summary>
        [ContextMenu("Log Current State")]
        public void LogCurrentState()
        {
            if (!DataManager.HasInstance || !DataManager.Instance.IsInitialized)
            {
                Debug.LogWarning("DataManager가 초기화되지 않았습니다.");
                return;
            }

            var dm = DataManager.Instance;

            Debug.Log("---------- 현재 상태 ----------");
            Debug.Log($"[프로필] {dm.Profile.Nickname} (Lv.{dm.Profile.Level})");
            Debug.Log($"[재화] Gold: {dm.Currency.Gold:N0} | Gem: {dm.Currency.TotalGem}");
            Debug.Log($"[캐릭터] 보유: {dm.OwnedCharacters.Count}개");

            foreach (var owned in dm.OwnedCharacters)
            {
                var master = dm.GetCharacterMasterData(owned);
                var name = master != null ? master.Name : owned.CharacterId;
                Debug.Log($"  - {name} (Lv.{owned.Level})");
            }

            Debug.Log($"[아이템] 보유: {dm.OwnedItems.Count}개");
            Debug.Log($"[마스터 데이터]");
            Debug.Log($"  - Characters: {dm.Characters?.Count ?? 0}");
            Debug.Log($"  - Skills: {dm.Skills?.Count ?? 0}");
            Debug.Log($"  - Items: {dm.Items?.Count ?? 0}");
            Debug.Log($"  - Stages: {dm.Stages?.Count ?? 0}");
            Debug.Log($"  - GachaPools: {dm.GachaPools?.Count ?? 0}");
            Debug.Log("--------------------------------");
        }

        /// <summary>
        /// 저장 데이터 삭제 (테스트용)
        /// </summary>
        [ContextMenu("Delete Save Data")]
        public void DeleteSaveData()
        {
            var savePath = System.IO.Path.Combine(Application.persistentDataPath, "user_save_data.json");
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
                Debug.Log($"저장 데이터 삭제됨: {savePath}");
            }
            else
            {
                Debug.Log("저장 데이터가 없습니다.");
            }
        }

        private void UpdateStatus()
        {
            if (!DataManager.HasInstance || !DataManager.Instance.IsInitialized) return;

            var dm = DataManager.Instance;
            _currentUserId = dm.Profile.Uid;
            _ownedCharacterCount = dm.OwnedCharacters.Count;
            _currentGold = dm.Currency.Gold;
            _currentGem = dm.Currency.TotalGem;
        }
    }
}
