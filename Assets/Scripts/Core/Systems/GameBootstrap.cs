using Cysharp.Threading.Tasks;
using Sc.Common.UI;
using Sc.Core.Initialization;
using Sc.Core.Initialization.Steps;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 게임 초기화 부트스트랩.
    /// InitializationSequence를 통해 단계별 초기화를 실행하고
    /// 진행률을 LoadingService에 표시.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("설정")]
        [SerializeField] private bool _autoInitialize = true;
        [SerializeField] private int _maxRetryCount = 3;

        [Header("상태 (읽기 전용)")]
        [SerializeField] private bool _isInitialized;
        [SerializeField] private string _currentUserId;
        [SerializeField] private int _ownedCharacterCount;
        [SerializeField] private long _currentGold;
        [SerializeField] private int _currentGem;

        private InitializationSequence _initSequence;
        private int _retryCount;

        // 레거시 호환용 이벤트 완료 대기
        private UniTaskCompletionSource<GachaResponse> _gachaCompletionSource;

        private async void Start()
        {
            if (_autoInitialize)
            {
                await InitializeGameAsync();
            }
        }

        private void OnEnable()
        {
            // 가챠 테스트용 이벤트 구독 (레거시 호환)
            if (EventManager.HasInstance)
            {
                EventManager.Instance.Subscribe<GachaCompletedEvent>(OnGachaCompleted);
                EventManager.Instance.Subscribe<GachaFailedEvent>(OnGachaFailed);
                EventManager.Instance.Subscribe<UserDataSyncedEvent>(OnUserDataSynced);
            }
        }

        private void OnDisable()
        {
            if (EventManager.HasInstance)
            {
                EventManager.Instance.Unsubscribe<GachaCompletedEvent>(OnGachaCompleted);
                EventManager.Instance.Unsubscribe<GachaFailedEvent>(OnGachaFailed);
                EventManager.Instance.Unsubscribe<UserDataSyncedEvent>(OnUserDataSynced);
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
        /// 게임 초기화 흐름.
        /// InitializationSequence를 통해 단계별로 실행.
        /// </summary>
        public async UniTask<bool> InitializeGameAsync()
        {
            if (_isInitialized)
            {
                Log.Warning("[GameBootstrap] 이미 초기화됨", LogCategory.System);
                return true;
            }

            Log.Info("========== 게임 초기화 시작 ==========", LogCategory.System);

            // InitializationSequence 생성
            _initSequence = new InitializationSequence();

            // 단계 등록 (순서대로)
            _initSequence.RegisterStep(new AssetManagerInitStep());
            _initSequence.RegisterStep(new NetworkManagerInitStep());
            _initSequence.RegisterStep(new DataManagerInitStep());
            _initSequence.RegisterStep(new LoginStep());

            // 실행
            var result = await _initSequence.RunAsync();

            if (result.IsFailure)
            {
                Log.Error($"[GameBootstrap] 초기화 실패: {result.Message}", LogCategory.System);
                await ShowInitFailurePopup(result.Error, result.Message);
                return false;
            }

            // 상태 업데이트
            UpdateStatus();

            Log.Info("========== 게임 초기화 완료 ==========", LogCategory.System);
            LogCurrentState();

            _isInitialized = true;

            // 게임 초기화 완료 이벤트 발행
            if (EventManager.HasInstance)
            {
                EventManager.Instance.Publish(new GameInitializedEvent { IsSuccess = true });
            }

            return true;
        }

        /// <summary>
        /// 초기화 실패 시 재시도 팝업 표시
        /// </summary>
        private async UniTask ShowInitFailurePopup(ErrorCode errorCode, string message)
        {
            _retryCount++;

            var canRetry = _retryCount <= _maxRetryCount;
            var retryMessage = canRetry
                ? $"{message}\n\n다시 시도하시겠습니까? (재시도 {_retryCount}/{_maxRetryCount})"
                : $"{message}\n\n재시도 횟수를 초과했습니다.";

            var state = new ConfirmState
            {
                Title = "초기화 실패",
                Message = retryMessage,
                ConfirmText = canRetry ? "재시도" : "종료",
                CancelText = "종료",
                ShowCancelButton = canRetry,
                OnConfirm = () =>
                {
                    if (canRetry)
                    {
                        RetryInitialize().Forget();
                    }
                    else
                    {
                        QuitApplication();
                    }
                },
                OnCancel = QuitApplication
            };

            ConfirmPopup.Open(state);

            // 팝업 닫힐 때까지 대기 (간단히 딜레이로 구현)
            await UniTask.Yield();
        }

        private async UniTaskVoid RetryInitialize()
        {
            // 잠시 대기 후 재시도
            await UniTask.Delay(500);
            await InitializeGameAsync();
        }

        private void QuitApplication()
        {
            Log.Info("[GameBootstrap] 애플리케이션 종료", LogCategory.System);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #region Legacy Test Methods

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
                Log.Warning("먼저 초기화를 진행하세요!", LogCategory.System);
                return;
            }

            Log.Info($"========== 가챠 테스트 ({pullType}) ==========", LogCategory.System);

            _gachaCompletionSource = new UniTaskCompletionSource<GachaResponse>();

            var request = pullType == GachaPullType.Single
                ? GachaRequest.CreateSingle("gacha_standard")
                : GachaRequest.CreateMulti("gacha_standard");

            NetworkManager.Instance.Send(request);

            var response = await _gachaCompletionSource.Task;

            if (response != null && response.IsSuccess)
            {
                Log.Info($"가챠 성공! 결과 {response.Results.Count}개:", LogCategory.System);
                foreach (var result in response.Results)
                {
                    var marker = result.IsNew ? "[NEW]" : "";
                    var pity = result.IsPity ? "[PITY]" : "";
                    Log.Info($"  - {result.CharacterId} ({result.Rarity}) {marker}{pity}", LogCategory.System);
                }
                Log.Info($"현재 천장: {response.CurrentPityCount}", LogCategory.System);

                UpdateStatus();
            }
            else
            {
                Log.Error($"가챠 실패: {response?.ErrorMessage ?? "Unknown error"}", LogCategory.System);
            }
        }

        #endregion

        #region Event Handlers

        private void OnGachaCompleted(GachaCompletedEvent evt)
        {
            var response = new GachaResponse
            {
                Results = evt.Results,
                CurrentPityCount = evt.PityCount
            };
            _gachaCompletionSource?.TrySetResult(response);
        }

        private void OnGachaFailed(GachaFailedEvent evt)
        {
            Log.Error($"가챠 실패: {evt.ErrorCode} - {evt.ErrorMessage}", LogCategory.System);
            _gachaCompletionSource?.TrySetResult(null);
        }

        private void OnUserDataSynced(UserDataSyncedEvent evt)
        {
            Log.Info("[GameBootstrap] 유저 데이터 동기화됨", LogCategory.System);
            UpdateStatus();
        }

        #endregion

        /// <summary>
        /// 현재 상태 로그 출력
        /// </summary>
        [ContextMenu("Log Current State")]
        public void LogCurrentState()
        {
            if (!DataManager.HasInstance || !DataManager.Instance.IsInitialized)
            {
                Log.Warning("DataManager가 초기화되지 않았습니다.", LogCategory.System);
                return;
            }

            var dm = DataManager.Instance;

            Log.Info("---------- 현재 상태 ----------", LogCategory.System);
            Log.Info($"[프로필] {dm.Profile.Nickname} (Lv.{dm.Profile.Level})", LogCategory.System);
            Log.Info($"[재화] Gold: {dm.Currency.Gold:N0} | Gem: {dm.Currency.TotalGem}", LogCategory.System);
            Log.Info($"[캐릭터] 보유: {dm.OwnedCharacters.Count}개", LogCategory.System);

            foreach (var owned in dm.OwnedCharacters)
            {
                var master = dm.GetCharacterMasterData(owned);
                var name = master != null ? master.Name : owned.CharacterId;
                Log.Info($"  - {name} (Lv.{owned.Level})", LogCategory.System);
            }

            Log.Info($"[아이템] 보유: {dm.OwnedItems.Count}개", LogCategory.System);
            Log.Info($"[마스터 데이터]", LogCategory.System);
            Log.Info($"  - Characters: {dm.Characters?.Count ?? 0}", LogCategory.System);
            Log.Info($"  - Skills: {dm.Skills?.Count ?? 0}", LogCategory.System);
            Log.Info($"  - Items: {dm.Items?.Count ?? 0}", LogCategory.System);
            Log.Info($"  - Stages: {dm.Stages?.Count ?? 0}", LogCategory.System);
            Log.Info($"  - GachaPools: {dm.GachaPools?.Count ?? 0}", LogCategory.System);
            Log.Info("--------------------------------", LogCategory.System);
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
                Log.Info($"저장 데이터 삭제됨: {savePath}", LogCategory.System);
            }
            else
            {
                Log.Info("저장 데이터가 없습니다.", LogCategory.System);
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
