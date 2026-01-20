using System.Collections.Generic;
using System.Data.Common;
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
    /// 파티 선택 화면.
    /// 스테이지 진입 전 파티를 편성합니다.
    /// 현재는 플레이스홀더 구현입니다.
    /// </summary>
    public class PartySelectScreen : ScreenWidget<PartySelectScreen, PartySelectScreen.PartySelectState>
    {
        /// <summary>
        /// 파티 선택 화면 상태
        /// </summary>
        public class PartySelectState : IScreenState
        {
            /// <summary>
            /// 진입할 스테이지 ID
            /// </summary>
            public string StageId { get; set; }

            /// <summary>
            /// 스테이지 데이터
            /// </summary>
            public StageData StageData { get; set; }

            /// <summary>
            /// 프리셋 그룹 ID (선택적)
            /// </summary>
            public string PresetGroupId { get; set; }
        }

        [Header("Stage Info")] [SerializeField]
        private TMP_Text _stageNameText;

        [SerializeField] private TMP_Text _stageDifficultyText;
        [SerializeField] private TMP_Text _recommendedPowerText;

        [Header("Party Slots")] [SerializeField]
        private Transform _partySlotContainer;

        [SerializeField] private TMP_Text _partyPowerText;

        [Header("Character List")] [SerializeField]
        private Transform _characterListContainer;

        [SerializeField] private TMP_Text _characterCountText;

        [Header("Footer")] [SerializeField] private TMP_Text _staminaCostText;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _startButtonText;

        [Header("Navigation")] [SerializeField]
        private Button _backButton;

        private PartySelectState _currentState;
        private readonly List<string> _selectedCharacterIds = new();
        private bool _isStarting;

        protected override void OnInitialize()
        {
            Debug.Log("[PartySelectScreen] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_startButton != null)
            {
                _startButton.onClick.AddListener(OnStartClicked);
            }
        }

        protected override void OnBind(PartySelectState state)
        {
            _currentState = state ?? new PartySelectState();

            Debug.Log($"[PartySelectScreen] OnBind - StageId: {_currentState.StageId}");

            // Header 설정
            ScreenHeader.Instance?.Configure("party_select");

            // 스테이지 정보 표시
            RefreshStageInfo();

            // 파티 슬롯 초기화
            InitializePartySlots();

            // 캐릭터 목록 로드
            LoadCharacterList();

            // 푸터 갱신
            RefreshFooter();
        }

        protected override void OnShow()
        {
            Debug.Log("[PartySelectScreen] OnShow");

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // Stage 이벤트 구독
            EventManager.Instance?.Subscribe<StageEnteredEvent>(OnStageEntered);
            EventManager.Instance?.Subscribe<StageEntryFailedEvent>(OnStageEntryFailed);
        }

        protected override void OnHide()
        {
            Debug.Log("[PartySelectScreen] OnHide");

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // Stage 이벤트 해제
            EventManager.Instance?.Unsubscribe<StageEnteredEvent>(OnStageEntered);
            EventManager.Instance?.Unsubscribe<StageEntryFailedEvent>(OnStageEntryFailed);
        }

        public override PartySelectState GetState() => _currentState;

        #region Stage Info

        private void RefreshStageInfo()
        {
            var stage = _currentState.StageData;
            if (stage == null) return;

            if (_stageNameText != null)
            {
                _stageNameText.text = stage.Name;
            }

            if (_stageDifficultyText != null)
            {
                _stageDifficultyText.text = GetDifficultyText(stage.Difficulty);
            }

            if (_recommendedPowerText != null)
            {
                _recommendedPowerText.text = $"권장 전투력: {stage.RecommendedPower:N0}";
            }
        }

        #endregion

        #region Party Slots

        private void InitializePartySlots()
        {
            // TODO: 파티 슬롯 UI 초기화
            // 현재는 플레이스홀더
            _selectedCharacterIds.Clear();

            RefreshPartyPower();
        }

        private void RefreshPartyPower()
        {
            // TODO: 선택된 캐릭터들의 전투력 합산
            int totalPower = 0;

            if (_partyPowerText != null)
            {
                _partyPowerText.text = $"전투력: {totalPower:N0}";
            }
        }

        #endregion

        #region Character List

        private void LoadCharacterList()
        {
            // TODO: 보유 캐릭터 목록 로드
            // 현재는 플레이스홀더

            int characterCount = DataManager.Instance?.OwnedCharacters?.Count ?? 0;

            if (_characterCountText != null)
            {
                _characterCountText.text = $"캐릭터: {characterCount}";
            }
        }

        #endregion

        #region Footer

        private void RefreshFooter()
        {
            var stage = _currentState.StageData;
            if (stage == null) return;

            if (_staminaCostText != null)
            {
                int currentStamina = DataManager.Instance?.Currency.Stamina ?? 0;
                _staminaCostText.text = $"스태미나: {currentStamina} / {stage.StaminaCost}";
            }

            if (_startButtonText != null)
            {
                _startButtonText.text = "전투 시작";
            }

            // 시작 버튼 활성화 조건
            // - 최소 1명 선택
            // - 스태미나 충분
            // - 진행 중 아님
            bool canStart = !_isStarting;
            // TODO: && _selectedCharacterIds.Count > 0
            // TODO: && HasEnoughStamina()

            if (_startButton != null)
            {
                _startButton.interactable = canStart;
            }
        }

        #endregion

        #region Battle Start

        private void OnStartClicked()
        {
            if (_isStarting) return;

            Debug.Log($"[PartySelectScreen] Start clicked - StageId: {_currentState.StageId}");

            // TODO: 파티 선택 검증
            if (_selectedCharacterIds.Count == 0)
            {
                // 임시: 기본 파티로 시작 (첫 번째 캐릭터)
                var characters = DataManager.Instance.OwnedCharacters;
                if (characters != null && characters.Count > 0)
                {
                    _selectedCharacterIds.Add(characters[0].InstanceId);
                }
            }

            if (_selectedCharacterIds.Count == 0)
            {
                ShowNoCharacterPopup();
                return;
            }

            // 스테이지 입장 요청
            ExecuteEnterStage();
        }

        private void ExecuteEnterStage()
        {
            _isStarting = true;
            RefreshFooter();

            var request = EnterStageRequest.Create(
                _currentState.StageId,
                _selectedCharacterIds
            );

            Debug.Log($"[PartySelectScreen] Sending EnterStageRequest: {_currentState.StageId}");
            NetworkManager.Instance?.Send(request);
        }

        private void ShowNoCharacterPopup()
        {
            ConfirmPopup.Open(new ConfirmState
            {
                Title = "파티 편성",
                Message = "파티에 최소 1명의 캐릭터가 필요합니다.",
                ShowCancelButton = false
            });
        }

        #endregion

        #region Event Handlers

        private void OnStageEntered(StageEnteredEvent evt)
        {
            if (evt.StageId != _currentState.StageId) return;

            Debug.Log($"[PartySelectScreen] Stage entered successfully: {evt.BattleSessionId}");
            _isStarting = false;

            // TODO: BattleScreen으로 전환
            // BattleReadyEvent 발행하여 Battle 시스템에 알림
            EventManager.Instance?.Publish(new BattleReadyEvent
            {
                BattleSessionId = evt.BattleSessionId,
                StageId = evt.StageId,
                PartyCharacterIds = _selectedCharacterIds
            });

            // 임시: 전투 완료 시뮬레이션 (실제로는 Battle 시스템에서 처리)
            Debug.Log("[PartySelectScreen] Battle simulation - navigating back");
            NavigationManager.Instance?.Back();
        }

        private void OnStageEntryFailed(StageEntryFailedEvent evt)
        {
            if (evt.StageId != _currentState.StageId) return;

            Debug.LogWarning($"[PartySelectScreen] Stage entry failed: {evt.ErrorCode} - {evt.ErrorMessage}");
            _isStarting = false;
            RefreshFooter();

            ConfirmPopup.Open(new ConfirmState
            {
                Title = "입장 실패",
                Message = evt.ErrorMessage,
                ShowCancelButton = false
            });
        }

        #endregion

        #region Navigation

        private void OnBackClicked()
        {
            Debug.Log("[PartySelectScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion

        #region Helpers

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

            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(OnStartClicked);
            }

            _selectedCharacterIds.Clear();
        }
    }
}