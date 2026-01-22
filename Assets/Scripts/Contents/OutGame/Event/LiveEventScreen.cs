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
    /// 라이브 이벤트 화면 State
    /// </summary>
    public class LiveEventState : IScreenState
    {
        /// <summary>
        /// 유예 기간 이벤트 포함 여부
        /// </summary>
        public bool IncludeGracePeriod = true;
    }

    /// <summary>
    /// 라이브 이벤트 화면 - 활성 이벤트 목록
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.Standard)]
    public class LiveEventScreen : ScreenWidget<LiveEventScreen, LiveEventState>
    {
        [Header("UI References")] [SerializeField]
        private Transform _eventListContainer;

        [SerializeField] private EventBannerItem _bannerItemPrefab;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _emptyText;
        [SerializeField] private GameObject _loadingIndicator;

        private LiveEventState _currentState;
        private readonly List<EventBannerItem> _spawnedBanners = new();
        private bool _isLoading;

        protected override void OnInitialize()
        {
            Debug.Log("[LiveEventScreen] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }
        }

        protected override void OnBind(LiveEventState state)
        {
            _currentState = state ?? new LiveEventState();
            Debug.Log($"[LiveEventScreen] OnBind - IncludeGracePeriod: {_currentState.IncludeGracePeriod}");

            // Header 설정
            ScreenHeader.Instance?.Configure("event_main");
        }

        protected override void OnShow()
        {
            Debug.Log("[LiveEventScreen] OnShow");

            // 이벤트 구독
            EventManager.Instance?.Subscribe<GetActiveEventsCompletedEvent>(OnGetActiveEventsCompleted);
            EventManager.Instance?.Subscribe<GetActiveEventsFailedEvent>(OnGetActiveEventsFailed);
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // 활성 이벤트 목록 요청
            RequestActiveEvents();
        }

        protected override void OnHide()
        {
            Debug.Log("[LiveEventScreen] OnHide");

            // 이벤트 해제
            EventManager.Instance?.Unsubscribe<GetActiveEventsCompletedEvent>(OnGetActiveEventsCompleted);
            EventManager.Instance?.Unsubscribe<GetActiveEventsFailedEvent>(OnGetActiveEventsFailed);
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            // 배너 정리
            ClearBanners();
        }

        public override LiveEventState GetState() => _currentState;

        private void RequestActiveEvents()
        {
            if (NetworkManager.Instance == null || !NetworkManager.Instance.IsInitialized)
            {
                Debug.LogError("[LiveEventScreen] NetworkManager not initialized");
                ShowEmpty("네트워크 오류");
                return;
            }

            _isLoading = true;
            SetLoadingState(true);

            var request = GetActiveEventsRequest.Create(_currentState.IncludeGracePeriod);
            Debug.Log("[LiveEventScreen] Sending GetActiveEventsRequest");
            NetworkManager.Instance.Send(request);
        }

        private void OnGetActiveEventsCompleted(GetActiveEventsCompletedEvent evt)
        {
            Debug.Log(
                $"[LiveEventScreen] GetActiveEvents completed: {evt.ActiveEvents?.Count ?? 0} active, {evt.GracePeriodEvents?.Count ?? 0} grace period");

            _isLoading = false;
            SetLoadingState(false);

            // 배너 목록 구성
            var allEvents = new List<LiveEventInfo>();
            if (evt.ActiveEvents != null)
            {
                allEvents.AddRange(evt.ActiveEvents);
            }

            if (evt.GracePeriodEvents != null)
            {
                allEvents.AddRange(evt.GracePeriodEvents);
            }

            if (allEvents.Count == 0)
            {
                ShowEmpty("진행 중인 이벤트가 없습니다.");
                return;
            }

            RefreshEventList(allEvents);
        }

        private void OnGetActiveEventsFailed(GetActiveEventsFailedEvent evt)
        {
            Debug.LogWarning($"[LiveEventScreen] GetActiveEvents failed: {evt.ErrorCode} - {evt.ErrorMessage}");

            _isLoading = false;
            SetLoadingState(false);
            ShowEmpty($"이벤트 목록을 불러올 수 없습니다.\n({evt.ErrorMessage})");
        }

        private void RefreshEventList(List<LiveEventInfo> events)
        {
            ClearBanners();
            HideEmpty();

            if (_bannerItemPrefab == null || _eventListContainer == null)
            {
                Debug.LogError("[LiveEventScreen] Banner prefab or container is null");
                return;
            }

            foreach (var eventInfo in events)
            {
                var bannerGo = Instantiate(_bannerItemPrefab, _eventListContainer);
                var bannerItem = bannerGo.GetComponent<EventBannerItem>();
                if (bannerItem != null)
                {
                    bannerItem.Setup(eventInfo, OnBannerClicked);
                    _spawnedBanners.Add(bannerItem);
                }
            }

            Debug.Log($"[LiveEventScreen] Created {_spawnedBanners.Count} banners");
        }

        private void ClearBanners()
        {
            foreach (var banner in _spawnedBanners)
            {
                if (banner != null)
                {
                    Destroy(banner.gameObject);
                }
            }

            _spawnedBanners.Clear();
        }

        private void OnBannerClicked(LiveEventInfo eventInfo)
        {
            Debug.Log($"[LiveEventScreen] Banner clicked: {eventInfo.EventId}");

            // EventDetailScreen으로 이동
            EventDetailScreen.Open(new EventDetailState
            {
                EventId = eventInfo.EventId,
                EventInfo = eventInfo
            });
        }

        private void SetLoadingState(bool isLoading)
        {
            if (_loadingIndicator != null)
            {
                _loadingIndicator.SetActive(isLoading);
            }

            if (_eventListContainer != null)
            {
                _eventListContainer.gameObject.SetActive(!isLoading);
            }
        }

        private void ShowEmpty(string message)
        {
            if (_emptyText != null)
            {
                _emptyText.text = message;
                _emptyText.gameObject.SetActive(true);
            }

            if (_eventListContainer != null)
            {
                _eventListContainer.gameObject.SetActive(false);
            }
        }

        private void HideEmpty()
        {
            if (_emptyText != null)
            {
                _emptyText.gameObject.SetActive(false);
            }

            if (_eventListContainer != null)
            {
                _eventListContainer.gameObject.SetActive(true);
            }
        }

        private void OnBackClicked()
        {
            Debug.Log("[LiveEventScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }
    }
}