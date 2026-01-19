using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// 범용 확인/취소 팝업. ShowCancelButton=false로 Alert 모드 지원.
    /// </summary>
    public class ConfirmPopup : PopupWidget<ConfirmPopup, ConfirmState>
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Text _confirmButtonText;
        [SerializeField] private TMP_Text _cancelButtonText;
        [SerializeField] private Button _backgroundButton;

        private ConfirmState _currentState;

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

        protected override void OnBind(ConfirmState state)
        {
            _currentState = state ?? new ConfirmState();

            if (!_currentState.Validate())
            {
                return;
            }

            RefreshUI();
        }

        public override ConfirmState GetState() => _currentState;

        private void RefreshUI()
        {
            if (_titleText != null)
            {
                _titleText.text = _currentState.Title;
            }

            if (_messageText != null)
            {
                _messageText.text = _currentState.Message;
            }

            if (_confirmButtonText != null)
            {
                _confirmButtonText.text = _currentState.ConfirmText;
            }

            if (_cancelButtonText != null)
            {
                _cancelButtonText.text = _currentState.CancelText;
            }

            // Alert 모드: 취소 버튼 숨김
            if (_cancelButton != null)
            {
                _cancelButton.gameObject.SetActive(_currentState.ShowCancelButton);
            }
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
            return false; // 이미 처리했으므로 추가 처리 불필요
        }

        protected override void OnRelease()
        {
            _currentState = null;
        }
    }
}
