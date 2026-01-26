using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Shop.Widgets
{
    /// <summary>
    /// 상점 탭 버튼.
    /// 세로 탭 리스트에서 사용되는 버튼 위젯.
    /// </summary>
    public class ShopTabButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private GameObject _badge;
        [SerializeField] private TMP_Text _badgeCount;

        [Header("Colors")] [SerializeField] private Color _normalColor = new Color32(60, 60, 80, 255);
        [SerializeField] private Color _selectedColor = new Color32(255, 150, 100, 255);
        [SerializeField] private Color _normalTextColor = Color.white;
        [SerializeField] private Color _selectedTextColor = Color.white;

        private int _tabIndex;
        private bool _isSelected;

        /// <summary>
        /// 탭 인덱스
        /// </summary>
        public int TabIndex => _tabIndex;

        /// <summary>
        /// 선택 여부
        /// </summary>
        public bool IsSelected => _isSelected;

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action<int> OnClicked;

        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            OnClicked?.Invoke(_tabIndex);
        }

        /// <summary>
        /// 탭 설정
        /// </summary>
        public void Setup(int index, Sprite icon, string label)
        {
            _tabIndex = index;

            if (_icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(icon != null);
            }

            if (_label != null)
            {
                _label.text = label;
            }

            SetSelected(false);
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;

            if (_background != null)
            {
                _background.color = selected ? _selectedColor : _normalColor;
            }

            if (_label != null)
            {
                _label.color = selected ? _selectedTextColor : _normalTextColor;
            }

            if (_selectedIndicator != null)
            {
                _selectedIndicator.SetActive(selected);
            }
        }

        /// <summary>
        /// 배지 설정
        /// </summary>
        public void SetBadge(int count)
        {
            if (_badge != null)
            {
                _badge.SetActive(count > 0);
            }

            if (_badgeCount != null)
            {
                _badgeCount.text = count > 99 ? "99+" : count.ToString();
            }
        }

        /// <summary>
        /// 라벨 변경
        /// </summary>
        public void SetLabel(string label)
        {
            if (_label != null)
            {
                _label.text = label;
            }
        }
    }
}