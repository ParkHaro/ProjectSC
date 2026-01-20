using System.Collections.Generic;
using System.Linq;
using Sc.Common.UI;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 스테이지 선택 화면.
    /// 스테이지 목록, 상세 정보, 진입/소탕 기능을 제공합니다.
    /// </summary>
    public class StageSelectScreen : ScreenWidget<StageSelectScreen, StageSelectScreen.StageSelectState>
    {
        /// <summary>
        /// 스테이지 선택 화면 상태
        /// </summary>
        public class StageSelectState : IScreenState
        {
            /// <summary>
            /// 컨텐츠 타입
            /// </summary>
            public InGameContentType ContentType { get; set; }

            /// <summary>
            /// 던전 ID (StageDashboard에서 선택한 경우)
            /// </summary>
            public string DungeonId { get; set; }

            /// <summary>
            /// 초기 선택 스테이지 ID
            /// </summary>
            public string SelectedStageId { get; set; }
        }

        [Header("Header")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _entryLimitText;

        [Header("Content Module Area")]
        [SerializeField] private Transform _moduleContainer;

        [Header("Stage List")]
        [SerializeField] private StageListPanel _stageListPanel;

        [Header("Stage Detail")]
        [SerializeField] private GameObject _detailPanel;
        [SerializeField] private TMP_Text _stageNameText;
        [SerializeField] private TMP_Text _stageDifficultyText;
        [SerializeField] private TMP_Text _stageDescriptionText;
        [SerializeField] private TMP_Text _recommendedPowerText;
        [SerializeField] private TMP_Text _rewardPreviewText;

        [Header("Footer")]
        [SerializeField] private TMP_Text _staminaCostText;
        [SerializeField] private Button _enterButton;
        [SerializeField] private TMP_Text _enterButtonText;
        [SerializeField] private Button _sweepButton;
        [SerializeField] private TMP_Text _sweepButtonText;

        [Header("Navigation")]
        [SerializeField] private Button _backButton;

        private StageSelectState _currentState;
        private StageData _selectedStage;
        private IStageContentModule _contentModule;
        private bool _isEntering;

        protected override void OnInitialize()
        {
            Debug.Log("[StageSelectScreen] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_enterButton != null)
            {
                _enterButton.onClick.AddListener(OnEnterClicked);
            }

            if (_sweepButton != null)
            {
                _sweepButton.onClick.AddListener(OnSweepClicked);
            }

            if (_stageListPanel != null)
            {
                _stageListPanel.Initialize();
                _stageListPanel.OnStageSelected += OnStageSelected;
            }
        }

        protected override void OnBind(StageSelectState state)
        {
            _currentState = state ?? new StageSelectState();

            Debug.Log($"[StageSelectScreen] OnBind - ContentType: {_currentState.ContentType}, DungeonId: {_currentState.DungeonId}");

            // Header 설정
            ScreenHeader.Instance?.Configure("stage_select");

            // 타이틀 설정
            if (_titleText != null)
            {
                _titleText.text = GetContentTitle(_currentState.ContentType);
            }

            // 컨텐츠 모듈 초기화 (플레이스홀더)
            InitializeContentModule();

            // 스테이지 목록 로드
            LoadStageList();

            // 입장 제한 표시
            RefreshEntryLimit();
        }

        protected override void OnShow()
        {
            Debug.Log("[StageSelectScreen] OnShow");

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // Stage 이벤트 구독
            EventManager.Instance?.Subscribe<StageEnteredEvent>(OnStageEntered);
            EventManager.Instance?.Subscribe<StageEntryFailedEvent>(OnStageEntryFailed);

            // 스태미나 갱신
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged += OnUserDataChanged;
            }

            RefreshFooter();
        }

        protected override void OnHide()
        {
            Debug.Log("[StageSelectScreen] OnHide");

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // Stage 이벤트 해제
            EventManager.Instance?.Unsubscribe<StageEnteredEvent>(OnStageEntered);
            EventManager.Instance?.Unsubscribe<StageEntryFailedEvent>(OnStageEntryFailed);

            // DataManager 이벤트 해제
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged -= OnUserDataChanged;
            }
        }

        public override StageSelectState GetState()
        {
            _currentState.SelectedStageId = _selectedStage?.Id;
            return _currentState;
        }

        #region Content Module

        private void InitializeContentModule()
        {
            // TODO: ContentType에 따라 적절한 모듈 생성
            // 현재는 플레이스홀더로 null
            _contentModule = null;

            if (_contentModule != null && _moduleContainer != null)
            {
                _contentModule.Initialize(_moduleContainer, _currentState.ContentType);
            }
        }

        #endregion

        #region Stage List

        private void LoadStageList()
        {
            var stages = GetStagesForContent(_currentState.ContentType, _currentState.DungeonId);

            if (_stageListPanel != null)
            {
                _stageListPanel.SetStages(stages, _currentState.SelectedStageId);
            }

            // 선택된 스테이지가 없으면 첫 번째 선택
            if (_selectedStage == null && stages.Count > 0)
            {
                OnStageSelected(stages[0]);
            }
        }

        private List<StageData> GetStagesForContent(InGameContentType contentType, string dungeonId)
        {
            // StageDatabase에서 조회
            var database = DataManager.Instance?.GetDatabase<StageDatabase>();
            if (database == null)
            {
                Debug.LogWarning("[StageSelectScreen] StageDatabase not found");
                return new List<StageData>();
            }

            // TODO: ContentType, DungeonId로 필터링
            // 현재 StageData에 ContentType 필드가 없으므로 전체 반환
            return database.Stages.ToList();
        }

        private void OnStageSelected(StageData stage)
        {
            if (stage == null) return;

            _selectedStage = stage;
            Debug.Log($"[StageSelectScreen] Stage selected: {stage.Id}");

            // 상세 정보 갱신
            RefreshStageDetail();

            // 푸터 갱신
            RefreshFooter();

            // 모듈에 알림
            _contentModule?.OnStageSelected(stage);
        }

        #endregion

        #region Stage Detail

        private void RefreshStageDetail()
        {
            if (_selectedStage == null)
            {
                if (_detailPanel != null)
                {
                    _detailPanel.SetActive(false);
                }
                return;
            }

            if (_detailPanel != null)
            {
                _detailPanel.SetActive(true);
            }

            if (_stageNameText != null)
            {
                _stageNameText.text = _selectedStage.Name;
            }

            if (_stageDifficultyText != null)
            {
                _stageDifficultyText.text = GetDifficultyText(_selectedStage.Difficulty);
            }

            if (_stageDescriptionText != null)
            {
                _stageDescriptionText.text = _selectedStage.Description;
            }

            if (_recommendedPowerText != null)
            {
                _recommendedPowerText.text = $"권장 전투력: {_selectedStage.RecommendedPower:N0}";
            }

            if (_rewardPreviewText != null)
            {
                _rewardPreviewText.text = $"골드: {_selectedStage.RewardGold:N0} / 경험치: {_selectedStage.RewardExp:N0}";
            }
        }

        private void RefreshEntryLimit()
        {
            // TODO: 입장 제한 횟수 표시
            if (_entryLimitText != null)
            {
                _entryLimitText.text = ""; // 제한 없음 표시 안함
            }
        }

        #endregion

        #region Footer

        private void RefreshFooter()
        {
            if (_selectedStage == null)
            {
                SetFooterInteractable(false);
                return;
            }

            // 스태미나 비용
            if (_staminaCostText != null)
            {
                int currentStamina = DataManager.Instance?.Currency.Stamina ?? 0;
                _staminaCostText.text = $"{currentStamina} / {_selectedStage.StaminaCost}";
            }

            // 진입 버튼
            if (_enterButtonText != null)
            {
                _enterButtonText.text = "입장";
            }

            // 소탕 버튼 (클리어한 스테이지만)
            bool isCleared = IsStageCleared(_selectedStage.Id);
            if (_sweepButton != null)
            {
                _sweepButton.gameObject.SetActive(isCleared);
            }

            if (_sweepButtonText != null)
            {
                _sweepButtonText.text = "소탕";
            }

            // 스태미나 충분 여부
            int stamina = DataManager.Instance?.Currency.Stamina ?? 0;
            bool hasEnoughStamina = stamina >= _selectedStage.StaminaCost;

            SetFooterInteractable(!_isEntering && hasEnoughStamina);
        }

        private void SetFooterInteractable(bool interactable)
        {
            if (_enterButton != null)
            {
                _enterButton.interactable = interactable;
            }

            if (_sweepButton != null)
            {
                _sweepButton.interactable = interactable;
            }
        }

        private bool IsStageCleared(string stageId)
        {
            var progress = DataManager.Instance?.StageProgress;
            return progress?.IsStageCleared(stageId) ?? false;
        }

        #endregion

        #region Enter/Sweep

        private void OnEnterClicked()
        {
            if (_selectedStage == null || _isEntering) return;

            Debug.Log($"[StageSelectScreen] Enter clicked: {_selectedStage.Id}");

            // 스태미나 확인
            int currentStamina = DataManager.Instance?.Currency.Stamina ?? 0;
            if (currentStamina < _selectedStage.StaminaCost)
            {
                ShowInsufficientStaminaPopup();
                return;
            }

            // 파티 선택 화면으로 이동
            PartySelectScreen.Open(new PartySelectScreen.PartySelectState
            {
                StageId = _selectedStage.Id,
                StageData = _selectedStage
            });
        }

        private void OnSweepClicked()
        {
            if (_selectedStage == null || _isEntering) return;

            Debug.Log($"[StageSelectScreen] Sweep clicked: {_selectedStage.Id}");

            // TODO: 소탕 확인 팝업 표시
            ShowSweepConfirmPopup();
        }

        private void ShowInsufficientStaminaPopup()
        {
            ConfirmPopup.Open(new ConfirmState
            {
                Title = "스태미나 부족",
                Message = "스태미나가 부족합니다.\n스태미나를 충전하시겠습니까?",
                ConfirmText = "충전",
                CancelText = "취소",
                OnConfirm = () => Debug.Log("[StageSelectScreen] Navigate to stamina shop"),
                OnCancel = () => { }
            });
        }

        private void ShowSweepConfirmPopup()
        {
            int staminaCost = _selectedStage?.StaminaCost ?? 0;
            int currentStamina = DataManager.Instance?.Currency.Stamina ?? 0;

            var state = new CostConfirmState
            {
                Title = "소탕 확인",
                Message = $"{_selectedStage?.Name}\n스테이지를 소탕하시겠습니까?",
                CostType = CostType.Stamina,
                CostAmount = staminaCost,
                CurrentAmount = currentStamina,
                ConfirmText = "소탕",
                CancelText = "취소",
                OnConfirm = ExecuteSweep,
                OnCancel = () => { }
            };

            CostConfirmPopup.Open(state);
        }

        private void ExecuteSweep()
        {
            // TODO: 소탕 요청
            Debug.Log($"[StageSelectScreen] Executing sweep for: {_selectedStage?.Id}");
        }

        #endregion

        #region Event Handlers

        private void OnStageEntered(StageEnteredEvent evt)
        {
            Debug.Log($"[StageSelectScreen] Stage entered: {evt.StageId}");
            _isEntering = false;
            RefreshFooter();
        }

        private void OnStageEntryFailed(StageEntryFailedEvent evt)
        {
            Debug.LogWarning($"[StageSelectScreen] Stage entry failed: {evt.ErrorCode} - {evt.ErrorMessage}");
            _isEntering = false;
            RefreshFooter();

            // 에러 팝업 표시
            ConfirmPopup.Open(new ConfirmState
            {
                Title = "입장 실패",
                Message = evt.ErrorMessage,
                ShowCancelButton = false
            });
        }

        private void OnUserDataChanged()
        {
            RefreshFooter();
        }

        #endregion

        #region Navigation

        private void OnBackClicked()
        {
            Debug.Log("[StageSelectScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion

        #region Helpers

        private string GetContentTitle(InGameContentType contentType)
        {
            return contentType switch
            {
                InGameContentType.MainStory => "메인 스토리",
                InGameContentType.HardMode => "하드 모드",
                InGameContentType.GoldDungeon => "골드 던전",
                InGameContentType.ExpDungeon => "경험치 던전",
                InGameContentType.SkillDungeon => "스킬 던전",
                InGameContentType.BossRaid => "보스 레이드",
                InGameContentType.Tower => "무한의 탑",
                InGameContentType.Event => "이벤트",
                _ => "스테이지 선택"
            };
        }

        private string GetDifficultyText(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => "Easy",
                Difficulty.Normal => "Normal",
                Difficulty.Hard => "Hard",
                _ => difficulty.ToString()
            };
        }

        #endregion

        protected override void OnRelease()
        {
            if (_backButton != null)
            {
                _backButton.onClick.RemoveListener(OnBackClicked);
            }

            if (_enterButton != null)
            {
                _enterButton.onClick.RemoveListener(OnEnterClicked);
            }

            if (_sweepButton != null)
            {
                _sweepButton.onClick.RemoveListener(OnSweepClicked);
            }

            if (_stageListPanel != null)
            {
                _stageListPanel.OnStageSelected -= OnStageSelected;
            }

            _contentModule?.Release();
            _contentModule = null;
        }
    }
}
