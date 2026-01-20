using Sc.Common.UI;
using Sc.Common.UI.Widgets;
using Sc.Contents.Character;
using Sc.Contents.Event;
using Sc.Contents.Gacha;
using Sc.Contents.Shop;
using Sc.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby
{
    /// <summary>
    /// 로비 화면 State
    /// </summary>
    public class LobbyState : IScreenState
    {
        /// <summary>
        /// 현재 선택된 탭 인덱스
        /// </summary>
        public int ActiveTabIndex;
    }

    /// <summary>
    /// 로비 화면 - 메인 허브 화면
    /// </summary>
    public class LobbyScreen : ScreenWidget<LobbyScreen, LobbyState>
    {
        [Header("UI References")] [SerializeField]
        private Button _gachaButton;

        [SerializeField] private Button _characterButton;
        [SerializeField] private Button _eventButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private TMP_Text _welcomeText;

        private LobbyState _currentState;

        protected override void OnInitialize()
        {
            Debug.Log("[LobbyScreen] OnInitialize");

            if (_gachaButton != null)
            {
                _gachaButton.onClick.AddListener(OnGachaButtonClicked);
            }

            if (_characterButton != null)
            {
                _characterButton.onClick.AddListener(OnCharacterButtonClicked);
            }

            if (_eventButton != null)
            {
                _eventButton.onClick.AddListener(OnEventButtonClicked);
            }

            if (_shopButton != null)
            {
                _shopButton.onClick.AddListener(OnShopButtonClicked);
            }
        }

        protected override void OnBind(LobbyState state)
        {
            _currentState = state ?? new LobbyState();
            Debug.Log($"[LobbyScreen] OnBind - Tab: {_currentState.ActiveTabIndex}");

            // Header 설정
            ScreenHeader.Instance?.Configure("lobby_default");

            RefreshUI();
        }

        protected override void OnShow()
        {
            Debug.Log("[LobbyScreen] OnShow");

            // DataManager 이벤트 구독
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged += OnUserDataChanged;
            }

            RefreshUI();
        }

        protected override void OnHide()
        {
            Debug.Log("[LobbyScreen] OnHide");

            // DataManager 이벤트 해제
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged -= OnUserDataChanged;
            }
        }

        public override LobbyState GetState() => _currentState;

        private void RefreshUI()
        {
            if (_welcomeText != null && DataManager.Instance?.IsInitialized == true)
            {
                var nickname = DataManager.Instance.Profile.Nickname;
                _welcomeText.text = $"환영합니다, {nickname}님!";
            }
        }

        private void OnUserDataChanged()
        {
            RefreshUI();
        }

        private void OnGachaButtonClicked()
        {
            Debug.Log("[LobbyScreen] Gacha button clicked");
            GachaScreen.Open(new GachaState());
        }

        private void OnCharacterButtonClicked()
        {
            Debug.Log("[LobbyScreen] Character button clicked");
            CharacterListScreen.Open(new CharacterListState());
        }

        private void OnEventButtonClicked()
        {
            Debug.Log("[LobbyScreen] Event button clicked");
            LiveEventScreen.Open(new LiveEventState());
        }

        private void OnShopButtonClicked()
        {
            Debug.Log("[LobbyScreen] Shop button clicked");
            ShopScreen.Open(new ShopScreen.ShopState());
        }
    }
}