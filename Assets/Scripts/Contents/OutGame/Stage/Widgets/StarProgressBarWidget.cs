using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 별 진행도 바 위젯.
    /// 현재 챕터에서 획득한 별 수와 마일스톤 보상을 표시합니다.
    /// </summary>
    public class StarProgressBarWidget : MonoBehaviour
    {
        [Header("Progress Display")] [SerializeField]
        private Image _starIcon;

        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;

        [Header("Milestones")] [SerializeField]
        private Transform _milestoneContainer;

        [SerializeField] private MilestoneItem _milestonePrefab;

        [Header("Colors")] [SerializeField] private Color _starColor = new Color(1f, 0.85f, 0.2f, 1f);
        [SerializeField] private Color _progressFillColor = new Color(0.4f, 0.7f, 0.4f, 1f);
        [SerializeField] private Color _milestoneReachedColor = new Color(0.4f, 0.8f, 0.4f, 1f);
        [SerializeField] private Color _milestoneUnreachedColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

        private int _currentStars;
        private int _maxStars;
        private List<MilestoneItem> _milestoneItems = new();

        /// <summary>
        /// 마일스톤 클릭 이벤트 (마일스톤 인덱스, 필요 별 수)
        /// </summary>
        public event Action<int, int> OnMilestoneClicked;

        /// <summary>
        /// 진행도 초기화
        /// </summary>
        /// <param name="currentStars">현재 획득한 별 수</param>
        /// <param name="maxStars">최대 별 수</param>
        /// <param name="milestones">마일스톤 목록 (필요 별 수, 보상 수량)</param>
        public void Initialize(int currentStars, int maxStars, List<(int requiredStars, int reward)> milestones)
        {
            _currentStars = currentStars;
            _maxStars = maxStars;

            UpdateProgressDisplay();
            CreateMilestones(milestones);
        }

        /// <summary>
        /// 진행도 업데이트
        /// </summary>
        public void SetProgress(int currentStars, int maxStars)
        {
            _currentStars = currentStars;
            _maxStars = maxStars;

            UpdateProgressDisplay();
            UpdateMilestoneStates();
        }

        /// <summary>
        /// 현재 별 수만 업데이트
        /// </summary>
        public void SetCurrentStars(int stars)
        {
            _currentStars = stars;
            UpdateProgressDisplay();
            UpdateMilestoneStates();
        }

        private void UpdateProgressDisplay()
        {
            // 텍스트 업데이트
            if (_progressText != null)
            {
                _progressText.text = $"{_currentStars}/{_maxStars}";
            }

            // 슬라이더 업데이트
            if (_progressSlider != null)
            {
                _progressSlider.minValue = 0;
                _progressSlider.maxValue = _maxStars;
                _progressSlider.value = _currentStars;
            }
        }

        private void CreateMilestones(List<(int requiredStars, int reward)> milestones)
        {
            ClearMilestones();

            if (_milestonePrefab == null || _milestoneContainer == null || milestones == null)
                return;

            for (int i = 0; i < milestones.Count; i++)
            {
                var (requiredStars, reward) = milestones[i];
                var milestoneGo = Instantiate(_milestonePrefab.gameObject, _milestoneContainer);
                var milestone = milestoneGo.GetComponent<MilestoneItem>();

                if (milestone != null)
                {
                    int index = i;
                    milestone.Initialize(requiredStars, reward, _currentStars >= requiredStars);
                    milestone.OnClicked += () => OnMilestoneClicked?.Invoke(index, requiredStars);
                    _milestoneItems.Add(milestone);

                    // 슬라이더 위에 위치 설정
                    PositionMilestone(milestone, requiredStars);
                }
            }
        }

        private void PositionMilestone(MilestoneItem milestone, int requiredStars)
        {
            if (_progressSlider == null || _maxStars <= 0) return;

            var rect = milestone.GetComponent<RectTransform>();
            if (rect == null) return;

            // 슬라이더 상의 비율 위치 계산
            float normalizedPosition = (float)requiredStars / _maxStars;

            // 슬라이더 너비 기준으로 위치 설정
            var sliderRect = _progressSlider.GetComponent<RectTransform>();
            if (sliderRect != null)
            {
                float sliderWidth = sliderRect.rect.width;
                rect.anchoredPosition = new Vector2(sliderWidth * normalizedPosition, rect.anchoredPosition.y);
            }
        }

        private void UpdateMilestoneStates()
        {
            foreach (var milestone in _milestoneItems)
            {
                if (milestone != null)
                {
                    milestone.SetReached(_currentStars >= milestone.RequiredStars);
                }
            }
        }

        private void ClearMilestones()
        {
            foreach (var milestone in _milestoneItems)
            {
                if (milestone != null)
                {
                    Destroy(milestone.gameObject);
                }
            }

            _milestoneItems.Clear();
        }

        private void OnDestroy()
        {
            ClearMilestones();
        }
    }
}