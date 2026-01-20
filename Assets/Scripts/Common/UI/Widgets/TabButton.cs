using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Widgets
{
    /// <summary>
    /// 탭 버튼 위젯
    /// </summary>
    public class TabButton : Widget
    {
        [Header("UI References")]
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private GameObject _badge;
        [SerializeField] private TMP_Text _badgeCountText;

        [Header("Colors")]
        [SerializeField] private Color _selectedColor = Color.white;
        [SerializeField] private Color _normalColor = new Color(0.7f, 0.7f, 0.7f);
        [SerializeField] private Color _selectedTextColor = Color.black;
        [SerializeField] private Color _normalTextColor = Color.white;

        private int _tabIndex;
        private bool _isSelected;

        /// <summary>
        /// 탭 인덱스
        /// </summary>
        public int TabIndex => _tabIndex;

        /// <summary>
        /// 선택 상태
        /// </summary>
        public bool IsSelected => _isSelected;

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action<int> OnClicked;

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        /// <summary>
        /// 탭 버튼 설정
        /// </summary>
        public void Setup(int index, string label)
        {
            _tabIndex = index;

            if (_labelText != null)
            {
                _labelText.text = label;
            }

            SetSelected(false);
            SetBadge(false);
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;

            if (_selectedIndicator != null)
            {
                _selectedIndicator.SetActive(selected);
            }

            if (_backgroundImage != null)
            {
                _backgroundImage.color = selected ? _selectedColor : _normalColor;
            }

            if (_labelText != null)
            {
                _labelText.color = selected ? _selectedTextColor : _normalTextColor;
            }
        }

        /// <summary>
        /// 뱃지 표시/숨김
        /// </summary>
        public void SetBadge(bool show)
        {
            if (_badge != null)
            {
                _badge.SetActive(show);
            }

            if (_badgeCountText != null)
            {
                _badgeCountText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 뱃지 카운트 표시
        /// </summary>
        public void SetBadgeCount(int count)
        {
            if (count <= 0)
            {
                SetBadge(false);
                return;
            }

            if (_badge != null)
            {
                _badge.SetActive(true);
            }

            if (_badgeCountText != null)
            {
                _badgeCountText.gameObject.SetActive(true);
                _badgeCountText.text = count > 99 ? "99+" : count.ToString();
            }
        }

        /// <summary>
        /// 레이블 변경
        /// </summary>
        public void SetLabel(string label)
        {
            if (_labelText != null)
            {
                _labelText.text = label;
            }
        }

        private void HandleClick()
        {
            OnClicked?.Invoke(_tabIndex);
        }

        protected override void OnRelease()
        {
            OnClicked = null;
        }
    }
}
