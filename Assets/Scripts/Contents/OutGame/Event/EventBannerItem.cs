using System;
using Sc.Common.UI;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Event
{
    /// <summary>
    /// 이벤트 배너 아이템 위젯
    /// </summary>
    public class EventBannerItem : Widget
    {
        [Header("UI References")]
        [SerializeField] private Image _bannerImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _remainingDaysText;
        [SerializeField] private GameObject _newBadge;
        [SerializeField] private GameObject _rewardBadge;
        [SerializeField] private GameObject _gracePeriodIndicator;
        [SerializeField] private TMP_Text _gracePeriodText;
        [SerializeField] private Button _button;

        private LiveEventInfo _eventInfo;
        private Action<LiveEventInfo> _onClickCallback;

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(OnClicked);
            }
        }

        /// <summary>
        /// 배너 설정
        /// </summary>
        public void Setup(LiveEventInfo eventInfo, Action<LiveEventInfo> onClickCallback)
        {
            _eventInfo = eventInfo;
            _onClickCallback = onClickCallback;

            RefreshUI();
        }

        private void RefreshUI()
        {
            if (_eventInfo == null) return;

            // 이름
            if (_nameText != null)
            {
                // TODO: StringData에서 실제 이름 가져오기
                _nameText.text = _eventInfo.NameKey ?? _eventInfo.EventId;
            }

            // 남은 기간
            if (_remainingDaysText != null)
            {
                if (_eventInfo.IsInGracePeriod)
                {
                    _remainingDaysText.text = $"재화 전환 D-{_eventInfo.GracePeriodRemainingDays}";
                }
                else
                {
                    _remainingDaysText.text = $"D-{_eventInfo.RemainingDays}";
                }
            }

            // NEW 뱃지
            if (_newBadge != null)
            {
                _newBadge.SetActive(!_eventInfo.HasVisited);
            }

            // 보상 뱃지
            if (_rewardBadge != null)
            {
                _rewardBadge.SetActive(_eventInfo.HasClaimableReward);
            }

            // 유예 기간 표시
            if (_gracePeriodIndicator != null)
            {
                _gracePeriodIndicator.SetActive(_eventInfo.IsInGracePeriod);
            }

            if (_gracePeriodText != null && _eventInfo.IsInGracePeriod)
            {
                _gracePeriodText.text = "이벤트 종료";
            }

            // TODO: 배너 이미지 로드 (Addressables)
            // LoadBannerImage(_eventInfo.BannerImage);
        }

        private void OnClicked()
        {
            Debug.Log($"[EventBannerItem] Clicked: {_eventInfo?.EventId}");
            _onClickCallback?.Invoke(_eventInfo);
        }

        protected override void OnRelease()
        {
            _eventInfo = null;
            _onClickCallback = null;
        }
    }
}
