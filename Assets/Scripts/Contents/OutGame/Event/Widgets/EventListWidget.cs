using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Event.Widgets
{
    /// <summary>
    /// 이벤트 목록 위젯 (좌측 패널).
    /// 활성 이벤트 배너 목록을 세로 스크롤로 표시합니다.
    /// </summary>
    public class EventListWidget : Widget
    {
        [Header("References")] [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField] private Transform _bannerContainer;
        [SerializeField] private EventBannerItem _bannerItemPrefab;

        [Header("Empty State")] [SerializeField]
        private GameObject _emptyState;

        [SerializeField] private TMPro.TMP_Text _emptyText;

        [Header("Loading")] [SerializeField] private GameObject _loadingIndicator;

        private readonly List<EventBannerItem> _spawnedBanners = new();
        private LiveEventInfo _selectedEvent;
        private Action<LiveEventInfo> _onBannerSelected;

        public event Action<LiveEventInfo> OnBannerSelected;

        /// <summary>
        /// 현재 선택된 이벤트
        /// </summary>
        public LiveEventInfo SelectedEvent => _selectedEvent;

        protected override void OnInitialize()
        {
            // 초기화
        }

        /// <summary>
        /// 이벤트 목록을 설정합니다.
        /// </summary>
        public void SetEvents(List<LiveEventInfo> events)
        {
            ClearBanners();
            SetLoadingState(false);

            if (events == null || events.Count == 0)
            {
                ShowEmptyState("진행 중인 이벤트가 없습니다.");
                return;
            }

            HideEmptyState();

            foreach (var eventInfo in events)
            {
                var bannerGo = Instantiate(_bannerItemPrefab, _bannerContainer);
                var bannerItem = bannerGo.GetComponent<EventBannerItem>();
                if (bannerItem != null)
                {
                    bannerItem.Setup(eventInfo, OnBannerClicked);
                    _spawnedBanners.Add(bannerItem);
                }
            }

            // 첫 번째 이벤트 자동 선택
            if (events.Count > 0)
            {
                SelectEvent(events[0]);
            }

            Debug.Log($"[EventListWidget] Created {_spawnedBanners.Count} banners");
        }

        /// <summary>
        /// 특정 이벤트를 선택합니다.
        /// </summary>
        public void SelectEvent(LiveEventInfo eventInfo)
        {
            _selectedEvent = eventInfo;
            UpdateSelectionUI();
            OnBannerSelected?.Invoke(eventInfo);
        }

        /// <summary>
        /// 로딩 상태를 설정합니다.
        /// </summary>
        public void SetLoadingState(bool isLoading)
        {
            if (_loadingIndicator != null)
            {
                _loadingIndicator.SetActive(isLoading);
            }

            if (_bannerContainer != null)
            {
                _bannerContainer.gameObject.SetActive(!isLoading);
            }
        }

        /// <summary>
        /// 빈 상태를 표시합니다.
        /// </summary>
        public void ShowEmptyState(string message)
        {
            if (_emptyState != null)
            {
                _emptyState.SetActive(true);
            }

            if (_emptyText != null)
            {
                _emptyText.text = message;
            }

            if (_bannerContainer != null)
            {
                _bannerContainer.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 빈 상태를 숨깁니다.
        /// </summary>
        public void HideEmptyState()
        {
            if (_emptyState != null)
            {
                _emptyState.SetActive(false);
            }

            if (_bannerContainer != null)
            {
                _bannerContainer.gameObject.SetActive(true);
            }
        }

        private void OnBannerClicked(LiveEventInfo eventInfo)
        {
            SelectEvent(eventInfo);
        }

        private void UpdateSelectionUI()
        {
            // TODO: 선택된 배너 하이라이트 표시
            foreach (var banner in _spawnedBanners)
            {
                // banner.SetSelected(banner.EventInfo == _selectedEvent);
            }
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
            _selectedEvent = null;
        }

        protected override void OnRelease()
        {
            ClearBanners();
        }
    }
}