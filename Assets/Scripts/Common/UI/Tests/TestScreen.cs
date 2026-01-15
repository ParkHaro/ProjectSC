using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Tests
{
    /// <summary>
    /// 테스트용 Screen State.
    /// </summary>
    public class TestScreenState : IScreenState
    {
        public string Title;
        public int Counter;
    }

    /// <summary>
    /// 테스트용 Screen.
    /// </summary>
    public class TestScreen : ScreenWidget<TestScreen, TestScreenState>
    {
        [Header("UI References")]
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _counterText;
        [SerializeField] private Button _popupButton;
        [SerializeField] private Button _nextScreenButton;
        [SerializeField] private Button _backButton;

        private TestScreenState _currentState;

        protected override void OnInitialize()
        {
            Debug.Log($"[TestScreen] OnInitialize");

            _popupButton?.onClick.AddListener(OnPopupButtonClicked);
            _nextScreenButton?.onClick.AddListener(OnNextScreenClicked);
            _backButton?.onClick.AddListener(OnBackButtonClicked);
        }

        protected override void OnBind(TestScreenState state)
        {
            _currentState = state ?? new TestScreenState { Title = "Default Screen", Counter = 0 };

            Debug.Log($"[TestScreen] OnBind: {_currentState.Title}");

            UpdateUI();
        }

        protected override void OnShow()
        {
            Debug.Log($"[TestScreen] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log($"[TestScreen] OnHide");
        }

        public override TestScreenState GetState()
        {
            return _currentState;
        }

        private void UpdateUI()
        {
            if (_titleText != null)
                _titleText.text = _currentState?.Title ?? "No Title";

            if (_counterText != null)
                _counterText.text = $"Counter: {_currentState?.Counter ?? 0}";
        }

        private void OnPopupButtonClicked()
        {
            Debug.Log("[TestScreen] Popup button clicked");
            UITestSetup.Instance?.PushPopup(new TestPopupState
            {
                Message = $"Popup from {_currentState?.Title ?? "Screen"}"
            });
        }

        private void OnNextScreenClicked()
        {
            Debug.Log("[TestScreen] Next screen clicked");
            var nextCounter = (_currentState?.Counter ?? 0) + 1;
            UITestSetup.Instance?.PushScreen(new TestScreenState
            {
                Title = $"Screen #{nextCounter}",
                Counter = nextCounter
            });
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("[TestScreen] Back button clicked");
            UITestSetup.Instance?.PopScreen();
        }
    }
}
