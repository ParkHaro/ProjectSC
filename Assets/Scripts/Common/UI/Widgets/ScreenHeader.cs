using Sc.Data;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Widgets
{
    /// <summary>
    /// Screen 상단 공용 헤더 UI.
    /// 데이터 기반 설정으로 버튼 표시/숨김 제어.
    /// </summary>
    public class ScreenHeader : Widget
    {
        public static ScreenHeader Instance { get; private set; }

        [Header("Database")]
        [SerializeField] private ScreenHeaderConfigDatabase _configDatabase;

        [Header("타이틀")]
        [SerializeField] private TMP_Text _titleText;

        [Header("버튼")]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _mailButton;
        [SerializeField] private Button _noticeButton;

        [Header("재화")]
        [SerializeField] private CurrencyHUD _currencyHUD;

        private ScreenHeaderConfigData _currentConfig;

        protected override void Awake()
        {
            base.Awake();

            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("[ScreenHeader] Duplicate instance detected. Destroying this.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Unity Start에서 Widget 초기화 시작.
        /// ScreenHeader는 Navigation 시스템 밖에 있으므로 자체적으로 Initialize() 호출.
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        protected override void OnInitialize()
        {
            // 버튼 이벤트 연결
            if (_backButton != null)
                _backButton.onClick.AddListener(OnBackButtonClicked);

            if (_profileButton != null)
                _profileButton.onClick.AddListener(OnProfileButtonClicked);

            if (_menuButton != null)
                _menuButton.onClick.AddListener(OnMenuButtonClicked);

            if (_mailButton != null)
                _mailButton.onClick.AddListener(OnMailButtonClicked);

            if (_noticeButton != null)
                _noticeButton.onClick.AddListener(OnNoticeButtonClicked);

            Debug.Log("[ScreenHeader] Initialized");
        }

        protected override void OnRelease()
        {
            // 버튼 이벤트 해제
            if (_backButton != null)
                _backButton.onClick.RemoveListener(OnBackButtonClicked);

            if (_profileButton != null)
                _profileButton.onClick.RemoveListener(OnProfileButtonClicked);

            if (_menuButton != null)
                _menuButton.onClick.RemoveListener(OnMenuButtonClicked);

            if (_mailButton != null)
                _mailButton.onClick.RemoveListener(OnMailButtonClicked);

            if (_noticeButton != null)
                _noticeButton.onClick.RemoveListener(OnNoticeButtonClicked);

            if (Instance == this)
                Instance = null;
        }

        /// <summary>
        /// Config ID로 헤더 설정 적용
        /// </summary>
        /// <param name="configId">설정 ID (예: "lobby_default", "gacha_main")</param>
        public void Configure(string configId)
        {
            // 방어적 초기화: Start() 이전에 Configure() 호출되는 경우 대비
            if (!IsInitialized)
            {
                Initialize();
            }

            if (_configDatabase == null)
            {
                Debug.LogWarning("[ScreenHeader] ConfigDatabase not assigned");
                return;
            }

            var config = _configDatabase.GetById(configId);
            if (config == null)
            {
                Debug.LogWarning($"[ScreenHeader] Config not found: {configId}");
                return;
            }

            ApplyConfig(config);
            Show();
        }

        /// <summary>
        /// Config 데이터로 직접 헤더 설정 적용
        /// </summary>
        public void Configure(ScreenHeaderConfigData config)
        {
            // 방어적 초기화: Start() 이전에 Configure() 호출되는 경우 대비
            if (!IsInitialized)
            {
                Initialize();
            }

            if (config == null)
            {
                Debug.LogWarning("[ScreenHeader] Config is null");
                return;
            }

            ApplyConfig(config);
            Show();
        }

        private void ApplyConfig(ScreenHeaderConfigData config)
        {
            _currentConfig = config;

            // 타이틀 설정
            if (_titleText != null)
            {
                _titleText.text = config.Title ?? string.Empty;
                _titleText.gameObject.SetActive(!string.IsNullOrEmpty(config.Title));
            }

            // 버튼 표시/숨김
            SetButtonActive(_backButton, config.ShowBackButton);
            SetButtonActive(_profileButton, config.ShowProfileButton);
            SetButtonActive(_menuButton, config.ShowMenuButton);
            SetButtonActive(_mailButton, config.ShowMailButton);
            SetButtonActive(_noticeButton, config.ShowNoticeButton);

            // 재화 표시/숨김
            if (_currencyHUD != null)
            {
                if (config.ShowCurrency)
                    _currencyHUD.Show();
                else
                    _currencyHUD.Hide();
            }

            Debug.Log($"[ScreenHeader] Config applied: {config.Id}");
        }

        private void SetButtonActive(Button button, bool active)
        {
            if (button != null)
            {
                button.gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// 현재 적용된 설정
        /// </summary>
        public ScreenHeaderConfigData CurrentConfig => _currentConfig;

        #region Button Event Handlers

        private void OnBackButtonClicked()
        {
            Debug.Log("[ScreenHeader] Back button clicked");
            EventManager.Instance?.Publish(new HeaderBackClickedEvent());
        }

        private void OnProfileButtonClicked()
        {
            Debug.Log("[ScreenHeader] Profile button clicked");
            EventManager.Instance?.Publish(new HeaderProfileClickedEvent());
        }

        private void OnMenuButtonClicked()
        {
            Debug.Log("[ScreenHeader] Menu button clicked");
            EventManager.Instance?.Publish(new HeaderMenuClickedEvent());
        }

        private void OnMailButtonClicked()
        {
            Debug.Log("[ScreenHeader] Mail button clicked");
            EventManager.Instance?.Publish(new HeaderMailClickedEvent());
        }

        private void OnNoticeButtonClicked()
        {
            Debug.Log("[ScreenHeader] Notice button clicked");
            EventManager.Instance?.Publish(new HeaderNoticeClickedEvent());
        }

        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: Database 설정
        /// </summary>
        public void SetDatabase(ScreenHeaderConfigDatabase database)
        {
            _configDatabase = database;
        }
#endif
    }
}
