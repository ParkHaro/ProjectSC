using System;
using Sc.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 메뉴 버튼 타입
    /// </summary>
    public enum MenuButtonType
    {
        Info,       // 정보
        LevelUp,    // 레벨업
        Equipment,  // 장비
        Skill,      // 스킬
        Promotion,  // 승급
        Board,      // 보드
        Aside       // 어사이드
    }

    /// <summary>
    /// 메뉴 버튼 위젯.
    /// 캐릭터 상세 화면의 좌측 메뉴 영역에 사용.
    /// - 아이콘 + 텍스트 조합
    /// - 선택 상태 표시
    /// - 알림 뱃지 (선택적)
    /// </summary>
    public class MenuButtonWidget : Widget
    {
        [Header("Button")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;

        [Header("Content")]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _labelText;

        [Header("Badge")]
        [SerializeField] private GameObject _badgeContainer;
        [SerializeField] private TMP_Text _badgeText;

        [Header("Settings")]
        [SerializeField] private MenuButtonType _buttonType;

        private bool _isSelected;
        private bool _hasBadge;
        private int _badgeCount;

        // 색상 정의
        private static readonly Color SelectedBgColor = new Color32(150, 230, 150, 255);  // 연두색
        private static readonly Color NormalBgColor = new Color32(80, 80, 80, 200);       // 회색
        private static readonly Color SelectedTextColor = new Color32(30, 30, 30, 255);   // 어두운 색
        private static readonly Color NormalTextColor = Color.white;
        private static readonly Color BadgeColor = new Color32(255, 80, 80, 255);         // 빨간색

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action<MenuButtonType> OnClicked;

        /// <summary>
        /// 버튼 타입
        /// </summary>
        public MenuButtonType ButtonType => _buttonType;

        /// <summary>
        /// 선택 상태
        /// </summary>
        public bool IsSelected => _isSelected;

        protected override void OnInitialize()
        {
            Debug.Log($"[MenuButtonWidget] OnInitialize - Type: {_buttonType}");

            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }

            // 초기 상태 설정
            SetSelected(false);
            SetBadge(false);
        }

        /// <summary>
        /// 버튼 설정
        /// </summary>
        public void Configure(MenuButtonType type, string label, Sprite icon = null)
        {
            _buttonType = type;

            if (_labelText != null)
            {
                _labelText.text = label;
            }

            if (_icon != null && icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 라벨 텍스트 설정
        /// </summary>
        public void SetLabel(string label)
        {
            if (_labelText != null)
            {
                _labelText.text = label;
            }
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;
            UpdateSelectionUI();
        }

        /// <summary>
        /// 뱃지 표시 설정
        /// </summary>
        public void SetBadge(bool show, int count = 0)
        {
            _hasBadge = show;
            _badgeCount = count;
            UpdateBadgeUI();
        }

        private void UpdateSelectionUI()
        {
            if (_background != null)
            {
                _background.color = _isSelected ? SelectedBgColor : NormalBgColor;
            }

            if (_labelText != null)
            {
                _labelText.color = _isSelected ? SelectedTextColor : NormalTextColor;
            }

            if (_icon != null)
            {
                _icon.color = _isSelected ? SelectedTextColor : NormalTextColor;
            }
        }

        private void UpdateBadgeUI()
        {
            if (_badgeContainer != null)
            {
                _badgeContainer.SetActive(_hasBadge);
            }

            if (_badgeText != null && _hasBadge)
            {
                _badgeText.text = _badgeCount > 0 ? _badgeCount.ToString() : "";
            }
        }

        private void HandleClick()
        {
            OnClicked?.Invoke(_buttonType);
        }

        /// <summary>
        /// 메뉴 타입에 따른 기본 라벨 반환
        /// </summary>
        public static string GetDefaultLabel(MenuButtonType type)
        {
            return type switch
            {
                MenuButtonType.Info => "정보",
                MenuButtonType.LevelUp => "레벨업",
                MenuButtonType.Equipment => "장비",
                MenuButtonType.Skill => "스킬",
                MenuButtonType.Promotion => "승급",
                MenuButtonType.Board => "보드",
                MenuButtonType.Aside => "어사이드",
                _ => ""
            };
        }

        protected override void OnRelease()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }

            OnClicked = null;
        }
    }
}
