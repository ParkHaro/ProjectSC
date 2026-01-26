using System;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 현재 진행 중인 스테이지/컨텐츠를 표시하는 Widget.
    /// 메인 스토리 진행 현황, 진행률 바, 다음 보상 미리보기 등을 담당.
    /// </summary>
    public class ContentProgressWidget : MonoBehaviour
    {
        [Header("Progress Info")]
        [SerializeField] private TMP_Text _progressLabelText;
        [SerializeField] private TMP_Text _stageNameText;
        [SerializeField] private TMP_Text _timeRemainingText;

        [Header("Progress Bar")]
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressValueText;
        [SerializeField] private Image _progressFillImage;

        [Header("Next Reward Preview")]
        [SerializeField] private GameObject _rewardPreviewContainer;
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TMP_Text _rewardCountText;
        [SerializeField] private TMP_Text _rewardConditionText;

        [Header("Navigation")]
        [SerializeField] private Button _navigateButton;

        [Header("Colors")]
        [SerializeField] private Color _progressActiveColor = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _progressInactiveColor = new Color32(80, 80, 80, 200);

        private InGameContentType _currentContentType;
        private string _currentStageId;
        private int _currentProgress;
        private int _maxProgress;

        /// <summary>
        /// 네비게이트 버튼 클릭 이벤트.
        /// </summary>
        public event Action<InGameContentType, string> OnNavigateClicked;

        private void Awake()
        {
            if (_navigateButton != null)
            {
                _navigateButton.onClick.AddListener(HandleNavigateClicked);
            }
        }

        private void OnDestroy()
        {
            if (_navigateButton != null)
            {
                _navigateButton.onClick.RemoveListener(HandleNavigateClicked);
            }
        }

        /// <summary>
        /// 진행 정보 설정.
        /// </summary>
        /// <param name="contentType">컨텐츠 타입</param>
        /// <param name="progressLabel">진행 라벨 (예: "제 1 엘리베이터 B7 도전중")</param>
        /// <param name="stageName">스테이지 이름 (예: "세계수 급착기지")</param>
        /// <param name="stageId">스테이지 ID (네비게이션용)</param>
        public void Configure(InGameContentType contentType, string progressLabel, string stageName, string stageId)
        {
            _currentContentType = contentType;
            _currentStageId = stageId;

            if (_progressLabelText != null)
            {
                _progressLabelText.text = progressLabel ?? string.Empty;
            }

            if (_stageNameText != null)
            {
                _stageNameText.text = stageName ?? string.Empty;
            }

            // 초기에는 타이머 숨김
            if (_timeRemainingText != null)
            {
                _timeRemainingText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 진행률 바 설정.
        /// </summary>
        /// <param name="current">현재 진행도</param>
        /// <param name="max">최대 진행도</param>
        public void SetProgress(int current, int max)
        {
            _currentProgress = current;
            _maxProgress = max;

            if (_progressSlider != null)
            {
                _progressSlider.minValue = 0;
                _progressSlider.maxValue = max;
                _progressSlider.value = current;
            }

            if (_progressValueText != null)
            {
                _progressValueText.text = $"{current}/{max}";
            }

            // 진행률에 따른 색상 변경
            if (_progressFillImage != null)
            {
                float progress = max > 0 ? (float)current / max : 0f;
                _progressFillImage.color = progress > 0 ? _progressActiveColor : _progressInactiveColor;
            }
        }

        /// <summary>
        /// 남은 시간 표시 설정.
        /// </summary>
        /// <param name="remainingTime">남은 시간 (TimeSpan)</param>
        public void SetTimeRemaining(TimeSpan remainingTime)
        {
            if (_timeRemainingText == null) return;

            if (remainingTime.TotalSeconds > 0)
            {
                _timeRemainingText.gameObject.SetActive(true);

                if (remainingTime.TotalDays >= 1)
                {
                    _timeRemainingText.text = $"{(int)remainingTime.TotalDays}일 {remainingTime.Hours:D2}시간 {remainingTime.Minutes:D2}분";
                }
                else if (remainingTime.TotalHours >= 1)
                {
                    _timeRemainingText.text = $"{(int)remainingTime.TotalHours}시간 {remainingTime.Minutes:D2}분";
                }
                else
                {
                    _timeRemainingText.text = $"{remainingTime.Minutes:D2}분 {remainingTime.Seconds:D2}초";
                }
            }
            else
            {
                _timeRemainingText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 다음 보상 미리보기 설정.
        /// </summary>
        /// <param name="rewardSprite">보상 아이콘</param>
        /// <param name="rewardCount">보상 수량</param>
        /// <param name="conditionText">획득 조건 텍스트 (예: "★ 15개 달성 시")</param>
        public void SetNextReward(Sprite rewardSprite, int rewardCount, string conditionText)
        {
            if (_rewardPreviewContainer == null) return;

            bool hasReward = rewardSprite != null && rewardCount > 0;
            _rewardPreviewContainer.SetActive(hasReward);

            if (!hasReward) return;

            if (_rewardIcon != null)
            {
                _rewardIcon.sprite = rewardSprite;
                _rewardIcon.enabled = rewardSprite != null;
            }

            if (_rewardCountText != null)
            {
                _rewardCountText.text = rewardCount.ToString();
            }

            if (_rewardConditionText != null)
            {
                _rewardConditionText.text = conditionText ?? string.Empty;
            }
        }

        /// <summary>
        /// 보상 미리보기 숨기기.
        /// </summary>
        public void HideRewardPreview()
        {
            if (_rewardPreviewContainer != null)
            {
                _rewardPreviewContainer.SetActive(false);
            }
        }

        /// <summary>
        /// 네비게이트 버튼 표시 여부 설정.
        /// </summary>
        public void SetNavigateButtonVisible(bool visible)
        {
            if (_navigateButton != null)
            {
                _navigateButton.gameObject.SetActive(visible);
            }
        }

        /// <summary>
        /// 위젯 전체 초기화.
        /// </summary>
        public void Clear()
        {
            if (_progressLabelText != null) _progressLabelText.text = string.Empty;
            if (_stageNameText != null) _stageNameText.text = string.Empty;
            if (_timeRemainingText != null)
            {
                _timeRemainingText.text = string.Empty;
                _timeRemainingText.gameObject.SetActive(false);
            }

            SetProgress(0, 0);
            HideRewardPreview();
        }

        private void HandleNavigateClicked()
        {
            OnNavigateClicked?.Invoke(_currentContentType, _currentStageId);
        }
    }
}
