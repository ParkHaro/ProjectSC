#if UNITY_EDITOR || DEVELOPMENT_BUILD
using System;
using System.Collections.Generic;
using System.Linq;
using Sc.Common.UI;
using Sc.Contents.Character;
using Sc.Contents.Event;
using Sc.Contents.Gacha;
using Sc.Contents.Lobby;
using Sc.Contents.Shop;
using Sc.Contents.Stage;
using Sc.Contents.Title;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.DebugTools
{
    /// <summary>
    /// 디버그용 네비게이션 패널.
    /// F12 키로 토글하여 모든 Screen/Popup에 바로 접근 가능.
    /// </summary>
    public class DebugNavigationPanel : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject _panel;
        [SerializeField] private KeyCode _toggleKey = KeyCode.F12;

        [Header("Buttons Container")]
        [SerializeField] private Transform _screenButtonContainer;
        [SerializeField] private Transform _popupButtonContainer;

        [Header("Button Prefab")]
        [SerializeField] private Button _buttonPrefab;

        [Header("Settings")]
        [SerializeField] private bool _autoCreateButtons = true;

        private bool _isVisible;
        private readonly List<Button> _createdButtons = new();

        private void Awake()
        {
            if (_panel != null)
            {
                _panel.SetActive(false);
            }

            if (_autoCreateButtons)
            {
                CreateAllButtons();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_toggleKey))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            _isVisible = !_isVisible;
            if (_panel != null)
            {
                _panel.SetActive(_isVisible);
            }
            UnityEngine.Debug.Log($"[DebugNavigationPanel] Visible: {_isVisible}");
        }

        public void Show()
        {
            _isVisible = true;
            if (_panel != null)
            {
                _panel.SetActive(true);
            }
        }

        public void Hide()
        {
            _isVisible = false;
            if (_panel != null)
            {
                _panel.SetActive(false);
            }
        }

        private void CreateAllButtons()
        {
            CreateScreenButtons();
            CreatePopupButtons();
        }

        private void CreateScreenButtons()
        {
            if (_screenButtonContainer == null || _buttonPrefab == null) return;

            var screens = new List<(string label, Action action)>
            {
                ("Title", () => TitleScreen.Open(new TitleState())),
                ("Lobby", () => LobbyScreen.Open(new ())),
                ("Gacha", () => GachaScreen.Open(new GachaState())),
                ("GachaHistory", () => GachaHistoryScreen.Open(new GachaHistoryState())),
                ("CharacterList", () => CharacterListScreen.Open(new CharacterListState())),
                ("CharacterDetail", OpenCharacterDetail),
                ("Shop", () => ShopScreen.Open(new ShopScreen.ShopState())),
                ("LiveEvent", () => LiveEventScreen.Open(new LiveEventState())),
                ("EventDetail", OpenEventDetail),
                ("ContentDashboard", () => InGameContentDashboard.Open(new InGameContentDashboard.DashboardState())),
                ("StageDashboard", OpenStageDashboard),
                ("StageSelect", OpenStageSelect),
                ("PartySelect", OpenPartySelect),
            };

            foreach (var (label, action) in screens)
            {
                CreateButton(_screenButtonContainer, $"[S] {label}", action);
            }
        }

        private void CreatePopupButtons()
        {
            if (_popupButtonContainer == null || _buttonPrefab == null) return;

            var popups = new List<(string label, Action action)>
            {
                ("Confirm", OpenConfirmPopup),
                ("CostConfirm", OpenCostConfirmPopup),
                ("Reward", OpenRewardPopup),
                ("GachaResult", OpenGachaResultPopup),
                ("RateDetail", OpenRateDetailPopup),
                ("LevelUp", OpenLevelUpPopup),
                ("Ascension", OpenAscensionPopup),
                ("StageInfo", OpenStageInfoPopup),
            };

            foreach (var (label, action) in popups)
            {
                CreateButton(_popupButtonContainer, $"[P] {label}", action);
            }
        }

        private void CreateButton(Transform container, string label, Action onClick)
        {
            var buttonObj = Instantiate(_buttonPrefab, container);
            var button = buttonObj.GetComponent<Button>();

            var tmpText = buttonObj.GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                tmpText.text = label;
            }
            else
            {
                var legacyText = buttonObj.GetComponentInChildren<Text>();
                if (legacyText != null)
                {
                    legacyText.text = label;
                }
            }

            button.onClick.AddListener(() =>
            {
                UnityEngine.Debug.Log($"[DebugNavigationPanel] Button clicked: {label}");
                onClick?.Invoke();
                Hide();
            });

            _createdButtons.Add(button);
        }

        #region Screen Openers

        private void OpenCharacterDetail()
        {
            var characters = DataManager.Instance?.OwnedCharacters;
            if (characters != null && characters.Count > 0)
            {
                CharacterDetailScreen.Open(new CharacterDetailState { CharacterId = characters[0].CharacterId });
            }
            else
            {
                CharacterDetailScreen.Open(new CharacterDetailState { CharacterId = "char_001" });
            }
        }

        private void OpenEventDetail()
        {
            var database = DataManager.Instance?.GetDatabase<LiveEventDatabase>();
            if (database != null)
            {
                var events = database.GetActiveEvents(DateTime.UtcNow).ToList();
                if (events.Count > 0)
                {
                    EventDetailScreen.Open(new EventDetailState { EventId = events[0].Id });
                    return;
                }
            }
            EventDetailScreen.Open(new EventDetailState { EventId = "event_001" });
        }

        private void OpenStageDashboard()
        {
            StageDashboard.Open(new StageDashboard.StageDashboardState
            {
                ContentType = InGameContentType.MainStory
            });
        }

        private void OpenStageSelect()
        {
            StageSelectScreen.Open(new StageSelectScreen.StageSelectState
            {
                ContentType = InGameContentType.MainStory,
                CategoryId = "chapter_1"
            });
        }

        private void OpenPartySelect()
        {
            PartySelectScreen.Open(new PartySelectScreen.PartySelectState
            {
                StageId = "stage_1_1"
            });
        }

        #endregion

        #region Popup Openers

        private void OpenConfirmPopup()
        {
            ConfirmPopup.Open(new ConfirmState
            {
                Title = "테스트 확인",
                Message = "이것은 ConfirmPopup 테스트입니다.",
                ConfirmText = "확인",
                CancelText = "취소",
                OnConfirm = () => UnityEngine.Debug.Log("[Debug] Confirm clicked"),
                OnCancel = () => UnityEngine.Debug.Log("[Debug] Cancel clicked")
            });
        }

        private void OpenCostConfirmPopup()
        {
            CostConfirmPopup.Open(new CostConfirmState
            {
                Title = "테스트 비용 확인",
                Message = "이것은 CostConfirmPopup 테스트입니다.",
                CostType = CostType.Gold,
                CostAmount = 1000,
                CurrentAmount = (int)(DataManager.Instance?.Currency.Gold ?? 5000),
                ConfirmText = "구매",
                CancelText = "취소",
                OnConfirm = () => UnityEngine.Debug.Log("[Debug] CostConfirm clicked"),
                OnCancel = () => UnityEngine.Debug.Log("[Debug] CostCancel clicked")
            });
        }

        private void OpenRewardPopup()
        {
            var rewards = new[]
            {
                new RewardInfo(RewardType.Currency, "Gold", 1000),
                new RewardInfo(RewardType.Currency, "Gem", 100),
                new RewardInfo(RewardType.Currency, "Stamina", 50)
            };

            RewardPopup.Open(new RewardPopup.State
            {
                Title = "테스트 보상",
                Rewards = rewards
            });
        }

        private void OpenGachaResultPopup()
        {
            var results = new List<GachaResultItem>
            {
                new() { CharacterId = "char_001", Rarity = Rarity.SSR, IsNew = true },
                new() { CharacterId = "char_002", Rarity = Rarity.SR, IsNew = false },
                new() { CharacterId = "char_003", Rarity = Rarity.R, IsNew = false }
            };

            GachaResultPopup.Open(new GachaResultState
            {
                Results = results
            });
        }

        private void OpenRateDetailPopup()
        {
            var database = DataManager.Instance?.GetDatabase<GachaPoolDatabase>();
            var poolData = database?.GachaPools.FirstOrDefault();

            if (poolData != null)
            {
                RateDetailPopup.Open(new RateDetailState { PoolData = poolData });
            }
            else
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] No GachaPoolData found");
            }
        }

        private void OpenLevelUpPopup()
        {
            var characters = DataManager.Instance?.OwnedCharacters;
            if (characters == null || characters.Count == 0)
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] No owned characters found");
                return;
            }

            var character = characters[0];
            var charDb = DataManager.Instance?.GetDatabase<CharacterDatabase>();
            var levelDb = DataManager.Instance?.GetDatabase<CharacterLevelDatabase>();
            var ascDb = DataManager.Instance?.GetDatabase<CharacterAscensionDatabase>();
            var itemDb = DataManager.Instance?.GetDatabase<ItemDatabase>();

            if (charDb == null || levelDb == null || ascDb == null || itemDb == null)
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] Required databases not found");
                return;
            }

            var charData = charDb.GetById(character.CharacterId);
            if (charData == null)
            {
                UnityEngine.Debug.LogWarning($"[DebugNavigationPanel] Character data not found: {character.CharacterId}");
                return;
            }

            var materials = DataManager.Instance?.OwnedItems?.ToList() ?? new List<OwnedItem>();

            CharacterLevelUpPopup.Open(new CharacterLevelUpState
            {
                Character = character,
                CharacterData = charData,
                LevelDb = levelDb,
                AscensionDb = ascDb,
                ItemDb = itemDb,
                AvailableMaterials = materials,
                Currency = DataManager.Instance?.Currency ?? new UserCurrency()
            });
        }

        private void OpenAscensionPopup()
        {
            var characters = DataManager.Instance?.OwnedCharacters;
            if (characters == null || characters.Count == 0)
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] No owned characters found");
                return;
            }

            var character = characters[0];
            var charDb = DataManager.Instance?.GetDatabase<CharacterDatabase>();
            var levelDb = DataManager.Instance?.GetDatabase<CharacterLevelDatabase>();
            var ascDb = DataManager.Instance?.GetDatabase<CharacterAscensionDatabase>();
            var itemDb = DataManager.Instance?.GetDatabase<ItemDatabase>();

            if (charDb == null || levelDb == null || ascDb == null || itemDb == null)
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] Required databases not found");
                return;
            }

            var charData = charDb.GetById(character.CharacterId);
            if (charData == null)
            {
                UnityEngine.Debug.LogWarning($"[DebugNavigationPanel] Character data not found: {character.CharacterId}");
                return;
            }

            CharacterAscensionPopup.Open(new CharacterAscensionState
            {
                Character = character,
                CharacterData = charData,
                LevelDb = levelDb,
                AscensionDb = ascDb,
                ItemDb = itemDb,
                OwnedItems = DataManager.Instance?.OwnedItems?.ToList() ?? new List<OwnedItem>(),
                Currency = DataManager.Instance?.Currency ?? new UserCurrency()
            });
        }

        private void OpenStageInfoPopup()
        {
            var database = DataManager.Instance?.GetDatabase<StageDatabase>();
            var stage = database?.Stages.FirstOrDefault();

            if (stage != null)
            {
                StageInfoPopup.Open(new StageInfoState { StageData = stage });
            }
            else
            {
                UnityEngine.Debug.LogWarning("[DebugNavigationPanel] No stage data found");
            }
        }

        #endregion

        private void OnDestroy()
        {
            foreach (var button in _createdButtons)
            {
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();
                }
            }
            _createdButtons.Clear();
        }
    }
}
#endif