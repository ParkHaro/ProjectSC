using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 스테이지 상세 정보 팝업.
    /// 스테이지명, 난이도, 권장전투력, 별 조건, 보상, 입장 제한 표시.
    /// </summary>
    public class StageInfoPopup : PopupWidget<StageInfoPopup, StageInfoState>
    {
        [Header("Header")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _closeButton;

        [Header("Basic Info")]
        [SerializeField] private TMP_Text _difficultyText;
        [SerializeField] private TMP_Text _recommendedPowerText;
        [SerializeField] private TMP_Text _descriptionText;

        [Header("Star Conditions")]
        [SerializeField] private GameObject _starConditionContainer;
        [SerializeField] private GameObject[] _starConditionItems;
        [SerializeField] private TMP_Text[] _starConditionTexts;
        [SerializeField] private GameObject[] _starAchievedIcons;

        [Header("Rewards")]
        [SerializeField] private TMP_Text _firstClearRewardText;
        [SerializeField] private TMP_Text _repeatRewardText;

        [Header("Entry Info")]
        [SerializeField] private TMP_Text _entryCostText;
        [SerializeField] private TMP_Text _entryLimitText;

        [Header("Footer")]
        [SerializeField] private Button _enterButton;
        [SerializeField] private TMP_Text _enterButtonText;
        [SerializeField] private Button _backgroundButton;

        private StageInfoState _currentState;

        protected override void OnInitialize()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(OnCloseClicked);
            }

            if (_enterButton != null)
            {
                _enterButton.onClick.AddListener(OnEnterClicked);
            }

            if (_backgroundButton != null)
            {
                _backgroundButton.onClick.AddListener(OnBackgroundClicked);
            }
        }

        protected override void OnBind(StageInfoState state)
        {
            _currentState = state;

            if (_currentState?.StageData == null)
            {
                Debug.LogWarning("[StageInfoPopup] StageData is null");
                return;
            }

            RefreshUI();
        }

        public override StageInfoState GetState() => _currentState;

        private void RefreshUI()
        {
            var stage = _currentState.StageData;
            var clearInfo = _currentState.ClearInfo;

            // Header
            if (_titleText != null)
            {
                _titleText.text = stage.Name;
            }

            // Basic Info
            if (_difficultyText != null)
            {
                _difficultyText.text = $"난이도: {GetDifficultyText(stage.Difficulty)}";
            }

            if (_recommendedPowerText != null)
            {
                _recommendedPowerText.text = $"권장 전투력: {stage.RecommendedPower:N0}";
            }

            if (_descriptionText != null)
            {
                _descriptionText.text = stage.Description;
            }

            // Star Conditions
            RefreshStarConditions(stage, clearInfo);

            // Rewards
            RefreshRewards(stage);

            // Entry Info
            RefreshEntryInfo(stage);

            // Footer
            RefreshFooter();
        }

        private void RefreshStarConditions(StageData stage, StageClearInfo? clearInfo)
        {
            var conditions = new[]
            {
                stage.Star1Condition,
                stage.Star2Condition,
                stage.Star3Condition
            };

            bool[] achieved = clearInfo?.StarAchieved ?? new bool[3];

            for (int i = 0; i < 3; i++)
            {
                if (_starConditionItems != null && i < _starConditionItems.Length && _starConditionItems[i] != null)
                {
                    _starConditionItems[i].SetActive(true);
                }

                if (_starConditionTexts != null && i < _starConditionTexts.Length && _starConditionTexts[i] != null)
                {
                    _starConditionTexts[i].text = GetStarConditionText(conditions[i]);
                }

                if (_starAchievedIcons != null && i < _starAchievedIcons.Length && _starAchievedIcons[i] != null)
                {
                    bool isAchieved = i < achieved.Length && achieved[i];
                    _starAchievedIcons[i].SetActive(isAchieved);
                }
            }
        }

        private void RefreshRewards(StageData stage)
        {
            // First Clear Rewards
            if (_firstClearRewardText != null)
            {
                var firstRewards = stage.FirstClearRewards;
                if (firstRewards != null && firstRewards.Count > 0)
                {
                    _firstClearRewardText.text = FormatRewards(firstRewards);
                }
                else
                {
                    // Legacy fallback
                    _firstClearRewardText.text = $"골드 {stage.RewardGold:N0}, 경험치 {stage.RewardExp:N0}";
                }
            }

            // Repeat Clear Rewards
            if (_repeatRewardText != null)
            {
                var repeatRewards = stage.RepeatClearRewards;
                if (repeatRewards != null && repeatRewards.Count > 0)
                {
                    _repeatRewardText.text = FormatRewards(repeatRewards);
                }
                else
                {
                    // Legacy fallback
                    _repeatRewardText.text = $"골드 {stage.RewardGold:N0}, 경험치 {stage.RewardExp:N0}";
                }
            }
        }

        private void RefreshEntryInfo(StageData stage)
        {
            // Entry Cost
            if (_entryCostText != null)
            {
                string costTypeName = GetCostTypeName(stage.EntryCostType);
                _entryCostText.text = $"{costTypeName} {stage.EntryCost}";
            }

            // Entry Limit
            if (_entryLimitText != null)
            {
                if (_currentState.MaxEntryCount > 0)
                {
                    _entryLimitText.text = $"남은 횟수: {_currentState.RemainingEntryCount}/{_currentState.MaxEntryCount}";
                    _entryLimitText.gameObject.SetActive(true);
                }
                else
                {
                    _entryLimitText.gameObject.SetActive(false);
                }
            }
        }

        private void RefreshFooter()
        {
            if (_enterButtonText != null)
            {
                _enterButtonText.text = "입장";
            }

            if (_enterButton != null)
            {
                _enterButton.interactable = _currentState.CanEnter;
            }
        }

        #region Event Handlers

        private void OnEnterClicked()
        {
            _currentState?.OnEnter?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnCloseClicked()
        {
            _currentState?.OnClose?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnBackgroundClicked()
        {
            if (_currentState?.AllowBackgroundDismiss ?? true)
            {
                OnCloseClicked();
            }
        }

        public override bool OnEscape()
        {
            OnCloseClicked();
            return false;
        }

        #endregion

        #region Helpers

        private string GetDifficultyText(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => "Easy",
                Difficulty.Normal => "Normal",
                Difficulty.Hard => "Hard",
                _ => difficulty.ToString()
            };
        }

        private string GetStarConditionText(StarCondition condition)
        {
            return condition.ConditionType switch
            {
                StarConditionType.Clear => "클리어",
                StarConditionType.TurnLimit => $"{condition.Value}턴 이내 클리어",
                StarConditionType.NoCharacterDeath => "사망자 없이 클리어",
                StarConditionType.FullHP => "아군 전원 HP 100%",
                StarConditionType.ElementAdvantage => "유리 속성으로 클리어",
                _ => "클리어"
            };
        }

        private string GetCostTypeName(CostType costType)
        {
            return costType switch
            {
                CostType.Stamina => "스태미나",
                CostType.Gold => "골드",
                CostType.Gem => "보석",
                CostType.EventCurrency => "이벤트 재화",
                _ => costType.ToString()
            };
        }

        private string FormatRewards(List<RewardInfo> rewards)
        {
            if (rewards == null || rewards.Count == 0) return "-";

            var parts = new List<string>();
            foreach (var reward in rewards)
            {
                string rewardName = GetRewardTypeName(reward.Type, reward.ItemId);
                parts.Add($"{rewardName} {reward.Amount:N0}");
            }
            return string.Join(", ", parts);
        }

        private string GetRewardTypeName(RewardType type, string itemId)
        {
            return type switch
            {
                RewardType.Currency when itemId == "Gold" => "골드",
                RewardType.Currency when itemId == "Gem" => "보석",
                RewardType.Currency => itemId,
                RewardType.PlayerExp => "경험치",
                RewardType.Item => itemId,
                RewardType.Character => "캐릭터",
                _ => type.ToString()
            };
        }

        #endregion

        protected override void OnRelease()
        {
            _currentState = null;
        }
    }
}
