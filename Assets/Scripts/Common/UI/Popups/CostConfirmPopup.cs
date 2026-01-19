using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// 재화 소모 확인 팝업. 재화 아이콘과 수량을 시각적으로 표시.
    /// </summary>
    public class CostConfirmPopup : PopupWidget<CostConfirmPopup, CostConfirmState>
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Text _confirmButtonText;
        [SerializeField] private TMP_Text _cancelButtonText;
        [SerializeField] private Button _backgroundButton;

        [Header("Cost Display")]
        [SerializeField] private Image _costIcon;
        [SerializeField] private TMP_Text _costAmountText;
        [SerializeField] private TMP_Text _currentAmountText;

        [Header("Cost Icon Sprites")]
        [SerializeField] private Sprite _goldIcon;
        [SerializeField] private Sprite _gemIcon;
        [SerializeField] private Sprite _staminaIcon;
        [SerializeField] private Sprite _defaultIcon;

        [Header("Colors")]
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _insufficientColor = Color.red;

        private CostConfirmState _currentState;

        protected override void OnInitialize()
        {
            if (_confirmButton != null)
            {
                _confirmButton.onClick.AddListener(OnConfirmClicked);
            }

            if (_cancelButton != null)
            {
                _cancelButton.onClick.AddListener(OnCancelClicked);
            }

            if (_backgroundButton != null)
            {
                _backgroundButton.onClick.AddListener(OnBackgroundClicked);
            }
        }

        protected override void OnBind(CostConfirmState state)
        {
            _currentState = state ?? new CostConfirmState();

            if (!_currentState.Validate())
            {
                Debug.LogWarning("[CostConfirmPopup] State validation failed, closing popup");
                NavigationManager.Instance?.Pop();
                return;
            }

            RefreshUI();
        }

        public override CostConfirmState GetState() => _currentState;

        private void RefreshUI()
        {
            // 제목, 메시지
            if (_titleText != null)
            {
                _titleText.text = _currentState.Title;
            }

            if (_messageText != null)
            {
                _messageText.text = _currentState.Message;
            }

            // 버튼 텍스트
            if (_confirmButtonText != null)
            {
                _confirmButtonText.text = _currentState.ConfirmText;
            }

            if (_cancelButtonText != null)
            {
                _cancelButtonText.text = _currentState.CancelText;
            }

            // 재화 아이콘
            if (_costIcon != null)
            {
                _costIcon.sprite = GetCostIcon(_currentState.CostType);
            }

            // 소모량 표시
            if (_costAmountText != null)
            {
                _costAmountText.text = $"-{_currentState.CostAmount:N0}";
                _costAmountText.color = _currentState.IsInsufficient ? _insufficientColor : _normalColor;
            }

            // 보유량 표시 (있을 때만)
            if (_currentAmountText != null)
            {
                if (_currentState.CurrentAmount.HasValue)
                {
                    _currentAmountText.gameObject.SetActive(true);
                    _currentAmountText.text = $"(보유: {_currentState.CurrentAmount.Value:N0})";
                    _currentAmountText.color = _currentState.IsInsufficient ? _insufficientColor : _normalColor;
                }
                else
                {
                    _currentAmountText.gameObject.SetActive(false);
                }
            }
        }

        private Sprite GetCostIcon(CostType costType)
        {
            return costType switch
            {
                CostType.Gold => _goldIcon != null ? _goldIcon : _defaultIcon,
                CostType.Gem => _gemIcon != null ? _gemIcon : _defaultIcon,
                CostType.Stamina => _staminaIcon != null ? _staminaIcon : _defaultIcon,
                _ => _defaultIcon
            };
        }

        private void OnConfirmClicked()
        {
            _currentState?.OnConfirm?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnCancelClicked()
        {
            _currentState?.OnCancel?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnBackgroundClicked()
        {
            // 배경 터치 = 취소
            _currentState?.OnCancel?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        /// <summary>
        /// ESC 키 처리: 취소와 동일하게 처리
        /// </summary>
        public override bool OnEscape()
        {
            OnCancelClicked();
            return false;
        }

        protected override void OnRelease()
        {
            _currentState = null;
        }
    }
}
