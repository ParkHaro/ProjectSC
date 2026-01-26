using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Event.Widgets
{
    /// <summary>
    /// 이벤트 상세 위젯 (우측 패널).
    /// 선택된 이벤트의 상세 정보를 표시합니다.
    /// - 타이틀 배너 이미지
    /// - 캐릭터 일러스트
    /// - 이벤트 기간 정보
    /// - 보상 미리보기
    /// - 바로가기 버튼
    /// </summary>
    public class EventDetailWidget : Widget
    {
        [Header("Banner")]
        [SerializeField] private Image _titleBannerImage;
        [SerializeField] private Image _characterIllustImage;

        [Header("Period Info")]
        [SerializeField] private TMP_Text _startTimeLabel;
        [SerializeField] private TMP_Text _endTimeLabel;

        [Header("Reward Preview")]
        [SerializeField] private TMP_Text _rewardPreviewTitle;
        [SerializeField] private Transform _rewardIconContainer;
        [SerializeField] private GameObject _rewardIconPrefab;

        [Header("Actions")]
        [SerializeField] private Button _enterButton;
        [SerializeField] private TMP_Text _enterButtonLabel;

        private LiveEventInfo _currentEvent;
        private readonly List<GameObject> _spawnedRewardIcons = new();

        public event Action<LiveEventInfo> OnEnterClicked;

        protected override void OnInitialize()
        {
            if (_enterButton != null)
            {
                _enterButton.onClick.AddListener(OnEnterButtonClicked);
            }
        }

        /// <summary>
        /// 이벤트 상세 정보를 표시합니다.
        /// </summary>
        public void SetEvent(LiveEventInfo eventInfo)
        {
            _currentEvent = eventInfo;
            RefreshUI();
        }

        /// <summary>
        /// 상세 정보를 초기화합니다.
        /// </summary>
        public void Clear()
        {
            _currentEvent = null;
            gameObject.SetActive(false);
        }

        private void RefreshUI()
        {
            if (_currentEvent == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);

            // 기간 정보
            if (_startTimeLabel != null)
            {
                _startTimeLabel.text = $"이벤트 시작: {FormatDateTime(_currentEvent.StartTime)}";
            }

            if (_endTimeLabel != null)
            {
                _endTimeLabel.text = $"이벤트 종료: {FormatDateTime(_currentEvent.EndTime)}";
            }

            // 보상 미리보기 타이틀
            if (_rewardPreviewTitle != null)
            {
                _rewardPreviewTitle.text = "참여 시 획득 가능한 보상";
            }

            // 버튼 라벨
            if (_enterButtonLabel != null)
            {
                _enterButtonLabel.text = "바로 가기";
            }

            // 보상 아이콘 (TODO: 실제 보상 데이터 연동)
            RefreshRewardIcons();

            // 이미지 로드 (TODO: Addressables 연동)
            // LoadBannerImage(_currentEvent.BannerImage);
            // LoadCharacterIllust(_currentEvent.CharacterIllustImage);
        }

        private void RefreshRewardIcons()
        {
            // 기존 아이콘 정리
            foreach (var icon in _spawnedRewardIcons)
            {
                if (icon != null)
                    Destroy(icon);
            }
            _spawnedRewardIcons.Clear();

            // TODO: 실제 보상 데이터 기반으로 아이콘 생성
            // 현재는 플레이스홀더로 1개 생성
            if (_rewardIconContainer != null && _rewardIconPrefab != null)
            {
                var icon = Instantiate(_rewardIconPrefab, _rewardIconContainer);
                _spawnedRewardIcons.Add(icon);
            }
        }

        private string FormatDateTime(long unixTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
            return dateTime.ToString("yyyy-MM-dd HH:mm") + "(UTC +9)";
        }

        private void OnEnterButtonClicked()
        {
            if (_currentEvent == null) return;

            Debug.Log($"[EventDetailWidget] Enter clicked: {_currentEvent.EventId}");
            OnEnterClicked?.Invoke(_currentEvent);
        }

        protected override void OnRelease()
        {
            foreach (var icon in _spawnedRewardIcons)
            {
                if (icon != null)
                    Destroy(icon);
            }
            _spawnedRewardIcons.Clear();
            _currentEvent = null;
        }
    }
}
