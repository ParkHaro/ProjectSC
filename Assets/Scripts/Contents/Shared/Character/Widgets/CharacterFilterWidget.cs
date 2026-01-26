using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 캐릭터 필터 위젯.
    /// 필터, 정렬 옵션 제공.
    /// </summary>
    public class CharacterFilterWidget : MonoBehaviour
    {
        [Header("Filter Buttons")]
        [SerializeField] private Button _expressionFilterButton;
        [SerializeField] private TMP_Text _expressionFilterText;

        [SerializeField] private Button _filterToggleButton;
        [SerializeField] private TMP_Text _filterToggleText;

        [SerializeField] private Button _sortButton;
        [SerializeField] private TMP_Text _sortText;

        [SerializeField] private Button _sortOrderButton;
        [SerializeField] private Image _sortOrderIcon;

        private bool _isFilterOn;
        private SortType _currentSortType = SortType.Level;
        private bool _isAscending;

        /// <summary>
        /// 감정표현 필터 클릭 이벤트
        /// </summary>
        public event Action OnExpressionFilterClicked;

        /// <summary>
        /// 필터 토글 이벤트
        /// </summary>
        public event Action<bool> OnFilterToggled;

        /// <summary>
        /// 정렬 타입 변경 이벤트
        /// </summary>
        public event Action<SortType> OnSortTypeChanged;

        /// <summary>
        /// 정렬 순서 변경 이벤트
        /// </summary>
        public event Action<bool> OnSortOrderChanged;

        private void Awake()
        {
            if (_expressionFilterButton != null)
                _expressionFilterButton.onClick.AddListener(HandleExpressionFilterClick);

            if (_filterToggleButton != null)
                _filterToggleButton.onClick.AddListener(HandleFilterToggle);

            if (_sortButton != null)
                _sortButton.onClick.AddListener(HandleSortClick);

            if (_sortOrderButton != null)
                _sortOrderButton.onClick.AddListener(HandleSortOrderClick);

            UpdateUI();
        }

        private void OnDestroy()
        {
            if (_expressionFilterButton != null)
                _expressionFilterButton.onClick.RemoveListener(HandleExpressionFilterClick);

            if (_filterToggleButton != null)
                _filterToggleButton.onClick.RemoveListener(HandleFilterToggle);

            if (_sortButton != null)
                _sortButton.onClick.RemoveListener(HandleSortClick);

            if (_sortOrderButton != null)
                _sortOrderButton.onClick.RemoveListener(HandleSortOrderClick);
        }

        private void HandleExpressionFilterClick()
        {
            OnExpressionFilterClicked?.Invoke();
        }

        private void HandleFilterToggle()
        {
            _isFilterOn = !_isFilterOn;
            UpdateUI();
            OnFilterToggled?.Invoke(_isFilterOn);
        }

        private void HandleSortClick()
        {
            // 정렬 타입 순환 (간단한 구현)
            _currentSortType = _currentSortType switch
            {
                SortType.Level => SortType.Rarity,
                SortType.Rarity => SortType.Element,
                SortType.Element => SortType.Name,
                SortType.Name => SortType.Level,
                _ => SortType.Level
            };

            UpdateUI();
            OnSortTypeChanged?.Invoke(_currentSortType);
        }

        private void HandleSortOrderClick()
        {
            _isAscending = !_isAscending;
            UpdateUI();
            OnSortOrderChanged?.Invoke(_isAscending);
        }

        private void UpdateUI()
        {
            // 필터 토글 텍스트
            if (_filterToggleText != null)
            {
                _filterToggleText.text = _isFilterOn ? "필터 ON" : "필터 OFF";
            }

            // 정렬 타입 텍스트
            if (_sortText != null)
            {
                _sortText.text = GetSortTypeText(_currentSortType);
            }

            // 정렬 순서 아이콘 (회전)
            if (_sortOrderIcon != null)
            {
                _sortOrderIcon.transform.localRotation = Quaternion.Euler(0, 0, _isAscending ? 180 : 0);
            }
        }

        private string GetSortTypeText(SortType sortType)
        {
            return sortType switch
            {
                SortType.Level => "레벨",
                SortType.Rarity => "희귀도",
                SortType.Element => "속성",
                SortType.Name => "이름",
                _ => "정렬"
            };
        }

        /// <summary>
        /// 현재 필터 상태
        /// </summary>
        public bool IsFilterOn => _isFilterOn;

        /// <summary>
        /// 현재 정렬 타입
        /// </summary>
        public SortType CurrentSortType => _currentSortType;

        /// <summary>
        /// 오름차순 여부
        /// </summary>
        public bool IsAscending => _isAscending;
    }

    /// <summary>
    /// 정렬 타입
    /// </summary>
    public enum SortType
    {
        Level,
        Rarity,
        Element,
        Name
    }
}
