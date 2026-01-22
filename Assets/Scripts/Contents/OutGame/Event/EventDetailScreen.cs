using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Event.UI;
using Sc.Foundation;
using Sc.Packet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Event
{
    /// <summary>
    /// 이벤트 상세 화면 State
    /// </summary>
    public class EventDetailState : IScreenState
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId;

        /// <summary>
        /// 이벤트 정보 (이미 로드된 경우)
        /// </summary>
        public LiveEventInfo EventInfo;

        /// <summary>
        /// 초기 선택 탭 인덱스
        /// </summary>
        public int InitialTabIndex = 0;
    }

    /// <summary>
    /// 이벤트 상세 화면 - 탭 기반 서브 컨텐츠
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.Detail)]
    public class EventDetailScreen : ScreenWidget<EventDetailScreen, EventDetailState>
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _remainingDaysText;
        [SerializeField] private TabGroupWidget _tabGroup;
        [SerializeField] private Button _backButton;

        [Header("Tab Contents")]
        [SerializeField] private EventMissionTab _missionTab;
        [SerializeField] private EventStageTab _stageTab;
        [SerializeField] private EventShopTab _shopTab;

        [Header("Currency Display")]
        [SerializeField] private GameObject _currencyDisplay;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _currencyAmountText;

        private EventDetailState _currentState;
        private LiveEventInfo _eventInfo;
        private readonly List<EventSubContent> _sortedSubContents = new();

        protected override void OnInitialize()
        {
            Debug.Log("[EventDetailScreen] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_tabGroup != null)
            {
                _tabGroup.Initialize();
                _tabGroup.OnTabChanged += OnTabChanged;
            }
        }

        protected override void OnBind(EventDetailState state)
        {
            _currentState = state ?? new EventDetailState();
            _eventInfo = _currentState.EventInfo;

            Debug.Log($"[EventDetailScreen] OnBind - EventId: {_currentState.EventId}");

            // Header 설정
            ScreenHeader.Instance?.Configure("event_detail");

            SetupUI();
        }

        protected override void OnShow()
        {
            Debug.Log("[EventDetailScreen] OnShow");

            // 이벤트 구독
            EventManager.Instance?.Subscribe<VisitEventCompletedEvent>(OnVisitEventCompleted);
            EventManager.Instance?.Subscribe<VisitEventFailedEvent>(OnVisitEventFailed);
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // 이벤트 방문 요청
            SendVisitRequest();
        }

        protected override void OnHide()
        {
            Debug.Log("[EventDetailScreen] OnHide");

            // 이벤트 해제
            EventManager.Instance?.Unsubscribe<VisitEventCompletedEvent>(OnVisitEventCompleted);
            EventManager.Instance?.Unsubscribe<VisitEventFailedEvent>(OnVisitEventFailed);
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override EventDetailState GetState() => _currentState;

        private void SetupUI()
        {
            if (_eventInfo == null)
            {
                Debug.LogWarning("[EventDetailScreen] EventInfo is null");
                return;
            }

            // 제목
            if (_titleText != null)
            {
                // TODO: StringData에서 실제 이름 가져오기
                _titleText.text = _eventInfo.NameKey ?? _eventInfo.EventId;
            }

            // 남은 기간
            if (_remainingDaysText != null)
            {
                if (_eventInfo.IsInGracePeriod)
                {
                    _remainingDaysText.text = $"재화 전환까지 D-{_eventInfo.GracePeriodRemainingDays}";
                }
                else
                {
                    _remainingDaysText.text = $"D-{_eventInfo.RemainingDays}";
                }
            }

            // 이벤트 재화
            SetupCurrencyDisplay();

            // 탭 설정
            SetupTabs();
        }

        private void SetupCurrencyDisplay()
        {
            if (_currencyDisplay == null) return;

            if (!_eventInfo.HasEventCurrency)
            {
                _currencyDisplay.SetActive(false);
                return;
            }

            _currencyDisplay.SetActive(true);

            // TODO: 실제 재화 수량 표시 (DataManager에서 가져오기)
            if (_currencyAmountText != null)
            {
                _currencyAmountText.text = "0";
            }

            // TODO: 아이콘 로드
        }

        private void SetupTabs()
        {
            if (_tabGroup == null || _eventInfo == null) return;

            // 서브 컨텐츠 정렬
            _sortedSubContents.Clear();
            if (_eventInfo.SubContents != null)
            {
                _sortedSubContents.AddRange(_eventInfo.SubContents);
                _sortedSubContents.Sort((a, b) => a.TabOrder.CompareTo(b.TabOrder));
            }

            if (_sortedSubContents.Count == 0)
            {
                Debug.LogWarning("[EventDetailScreen] No sub contents");
                return;
            }

            // 탭 데이터 생성
            var tabDataList = new List<TabData>();
            var tabContents = new List<GameObject>();

            foreach (var subContent in _sortedSubContents)
            {
                // TODO: StringData에서 실제 탭 이름 가져오기
                var label = GetTabLabel(subContent.Type);

                tabDataList.Add(new TabData
                {
                    Index = subContent.TabOrder,
                    Label = label,
                    UserData = subContent
                });

                // 탭 컨텐츠 매핑
                var content = GetTabContent(subContent.Type);
                tabContents.Add(content?.gameObject);
            }

            _tabGroup.SetupTabs(tabDataList, tabContents);

            // 초기 탭 선택
            var initialIndex = Mathf.Clamp(_currentState.InitialTabIndex, 0, _sortedSubContents.Count - 1);
            _tabGroup.SelectTab(initialIndex);
        }

        private string GetTabLabel(EventSubContentType type)
        {
            return type switch
            {
                EventSubContentType.Mission => "미션",
                EventSubContentType.Stage => "스테이지",
                EventSubContentType.Shop => "상점",
                EventSubContentType.Minigame => "미니게임",
                EventSubContentType.Story => "스토리",
                _ => "탭"
            };
        }

        private Widget GetTabContent(EventSubContentType type)
        {
            return type switch
            {
                EventSubContentType.Mission => _missionTab,
                EventSubContentType.Stage => _stageTab,
                EventSubContentType.Shop => _shopTab,
                _ => null
            };
        }

        private void OnTabChanged(int tabIndex)
        {
            Debug.Log($"[EventDetailScreen] Tab changed to: {tabIndex}");

            if (tabIndex < 0 || tabIndex >= _sortedSubContents.Count) return;

            var subContent = _sortedSubContents[tabIndex];

            // 탭별 초기화
            switch (subContent.Type)
            {
                case EventSubContentType.Mission:
                    _missionTab?.Setup(_eventInfo.EventId, subContent.ContentId);
                    break;
                case EventSubContentType.Stage:
                    _stageTab?.Setup(_eventInfo.EventId, subContent.ContentId);
                    break;
                case EventSubContentType.Shop:
                    _shopTab?.Setup(_eventInfo.EventId, subContent.ContentId);
                    break;
            }
        }

        private void SendVisitRequest()
        {
            if (string.IsNullOrEmpty(_currentState.EventId)) return;

            if (NetworkManager.Instance == null || !NetworkManager.Instance.IsInitialized)
            {
                Debug.LogError("[EventDetailScreen] NetworkManager not initialized");
                return;
            }

            var request = VisitEventRequest.Create(_currentState.EventId);
            Debug.Log($"[EventDetailScreen] Sending VisitEventRequest: {_currentState.EventId}");
            NetworkManager.Instance.Send(request);
        }

        private void OnVisitEventCompleted(VisitEventCompletedEvent evt)
        {
            if (evt.EventId != _currentState.EventId) return;

            Debug.Log($"[EventDetailScreen] Visit completed: {evt.EventId}");

            // 미션 진행 상태 등 업데이트 가능
        }

        private void OnVisitEventFailed(VisitEventFailedEvent evt)
        {
            if (evt.EventId != _currentState.EventId) return;

            Debug.LogWarning($"[EventDetailScreen] Visit failed: {evt.ErrorCode} - {evt.ErrorMessage}");
        }

        private void OnBackClicked()
        {
            Debug.Log("[EventDetailScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        protected override void OnRelease()
        {
            if (_tabGroup != null)
            {
                _tabGroup.OnTabChanged -= OnTabChanged;
            }
            _sortedSubContents.Clear();
        }
    }
}
