using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Tests
{
    /// <summary>
    /// 테스트용 Popup State.
    /// </summary>
    public class TestPopupState : IPopupState
    {
        public string Message;
    }

    /// <summary>
    /// 테스트용 Popup.
    /// </summary>
    public class TestPopup : PopupWidget<TestPopup, TestPopupState>
    {
        [Header("UI References")]
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _closeButton;

        private TestPopupState _currentState;

        protected override void OnInitialize()
        {
            Debug.Log($"[TestPopup] OnInitialize");

            _confirmButton?.onClick.AddListener(OnConfirmClicked);
            _closeButton?.onClick.AddListener(OnCloseClicked);
        }

        protected override void OnBind(TestPopupState state)
        {
            _currentState = state ?? new TestPopupState { Message = "Default Message" };

            Debug.Log($"[TestPopup] OnBind: {_currentState.Message}");

            UpdateUI();
        }

        protected override void OnShow()
        {
            Debug.Log($"[TestPopup] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log($"[TestPopup] OnHide");
        }

        public override TestPopupState GetState()
        {
            return _currentState;
        }

        public override bool OnEscape()
        {
            Debug.Log("[TestPopup] OnEscape - allowing close");
            return true;
        }

        private void UpdateUI()
        {
            if (_messageText != null)
                _messageText.text = _currentState?.Message ?? "No Message";
        }

        private void OnConfirmClicked()
        {
            Debug.Log("[TestPopup] Confirm clicked");
            UITestSetup.Instance?.PopPopup(this);
        }

        private void OnCloseClicked()
        {
            Debug.Log("[TestPopup] Close clicked");
            UITestSetup.Instance?.PopPopup(this);
        }
    }
}
