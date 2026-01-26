using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Inventory.Widgets
{
    /// <summary>
    /// 인벤토리 카테고리 탭 위젯.
    /// 사용, 성장, 장비, 교단, 연성카드 탭 관리.
    /// </summary>
    public class InventoryTabWidget : MonoBehaviour
    {
        [Header("Tab Buttons")] [SerializeField]
        private List<InventoryTabButton> _tabButtons;

        [Header("Tab Colors")] [SerializeField]
        private Color _activeColor = new Color32(100, 200, 100, 255);

        [SerializeField] private Color _inactiveColor = new Color32(220, 220, 220, 255);
        [SerializeField] private Color _activeTextColor = Color.white;
        [SerializeField] private Color _inactiveTextColor = new Color32(80, 80, 80, 255);

        private int _currentTabIndex;
        private Action<InventoryCategory> _onTabChanged;

        /// <summary>
        /// 현재 선택된 탭 인덱스
        /// </summary>
        public int CurrentTabIndex => _currentTabIndex;

        /// <summary>
        /// 현재 선택된 카테고리
        /// </summary>
        public InventoryCategory CurrentCategory => (InventoryCategory)_currentTabIndex;

        private void Awake()
        {
            InitializeTabButtons();
        }

        private void OnDestroy()
        {
            CleanupTabButtons();
        }

        private void InitializeTabButtons()
        {
            if (_tabButtons == null) return;

            for (int i = 0; i < _tabButtons.Count; i++)
            {
                var tab = _tabButtons[i];
                if (tab != null)
                {
                    int index = i; // 클로저 캡처용
                    tab.Initialize(index, OnTabButtonClicked);
                }
            }
        }

        private void CleanupTabButtons()
        {
            if (_tabButtons == null) return;

            foreach (var tab in _tabButtons)
            {
                tab?.Cleanup();
            }
        }

        /// <summary>
        /// 탭 변경 콜백 설정
        /// </summary>
        public void SetOnTabChanged(Action<InventoryCategory> callback)
        {
            _onTabChanged = callback;
        }

        /// <summary>
        /// 특정 탭 선택
        /// </summary>
        public void SelectTab(int index)
        {
            if (index < 0 || _tabButtons == null || index >= _tabButtons.Count)
                return;

            _currentTabIndex = index;
            UpdateTabStates();

            var category = (InventoryCategory)index;
            _onTabChanged?.Invoke(category);
        }

        /// <summary>
        /// 카테고리로 탭 선택
        /// </summary>
        public void SelectCategory(InventoryCategory category)
        {
            SelectTab((int)category);
        }

        private void OnTabButtonClicked(int index)
        {
            SelectTab(index);
        }

        private void UpdateTabStates()
        {
            if (_tabButtons == null) return;

            for (int i = 0; i < _tabButtons.Count; i++)
            {
                bool isActive = i == _currentTabIndex;
                var tab = _tabButtons[i];

                tab?.SetSelected(
                    isActive,
                    isActive ? _activeColor : _inactiveColor,
                    isActive ? _activeTextColor : _inactiveTextColor
                );
            }
        }

        /// <summary>
        /// 탭 레이블 설정
        /// </summary>
        public void SetTabLabels(List<string> labels)
        {
            if (_tabButtons == null || labels == null) return;

            int count = Mathf.Min(_tabButtons.Count, labels.Count);
            for (int i = 0; i < count; i++)
            {
                _tabButtons[i]?.SetLabel(labels[i]);
            }
        }

        /// <summary>
        /// 알림 뱃지 표시
        /// </summary>
        public void SetNotificationBadge(int tabIndex, bool show, int count = 0)
        {
            if (_tabButtons == null || tabIndex < 0 || tabIndex >= _tabButtons.Count)
                return;

            _tabButtons[tabIndex]?.SetNotificationBadge(show, count);
        }
    }

    /// <summary>
    /// 인벤토리 카테고리 열거형
    /// </summary>
    public enum InventoryCategory
    {
        Usage = 0, // 사용
        Growth = 1, // 성장
        Equipment = 2, // 장비
        Guild = 3, // 교단
        Card = 4 // 연성카드
    }

    /// <summary>
    /// 개별 탭 버튼 컴포넌트
    /// </summary>
    [Serializable]
    public class InventoryTabButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private GameObject _notificationBadge;
        [SerializeField] private TMP_Text _notificationCount;

        private int _index;
        private Action<int> _onClick;

        public void Initialize(int index, Action<int> onClick)
        {
            _index = index;
            _onClick = onClick;

            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        public void Cleanup()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            _onClick?.Invoke(_index);
        }

        public void SetSelected(bool selected, Color bgColor, Color textColor)
        {
            if (_background != null)
            {
                _background.color = bgColor;
            }

            if (_label != null)
            {
                _label.color = textColor;
            }

            if (_selectedIndicator != null)
            {
                _selectedIndicator.SetActive(selected);
            }
        }

        public void SetLabel(string text)
        {
            if (_label != null)
            {
                _label.text = text;
            }
        }

        public void SetIcon(Sprite sprite)
        {
            if (_icon != null && sprite != null)
            {
                _icon.sprite = sprite;
            }
        }

        public void SetNotificationBadge(bool show, int count = 0)
        {
            if (_notificationBadge != null)
            {
                _notificationBadge.SetActive(show);
            }

            if (_notificationCount != null && show)
            {
                _notificationCount.text = count > 99 ? "99+" : count.ToString();
            }
        }
    }
}