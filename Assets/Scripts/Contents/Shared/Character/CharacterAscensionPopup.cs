using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character
{
    /// <summary>
    /// 캐릭터 돌파 팝업 상태
    /// </summary>
    public class CharacterAscensionState : IPopupState
    {
        public OwnedCharacter Character;
        public CharacterData CharacterData;
        public CharacterLevelDatabase LevelDb;
        public CharacterAscensionDatabase AscensionDb;
        public ItemDatabase ItemDb;
        public List<OwnedItem> OwnedItems = new();
        public UserCurrency Currency;

        public Action OnConfirm;
        public Action OnCancel;

        public bool AllowBackgroundDismiss => true;
    }

    /// <summary>
    /// 캐릭터 돌파 팝업
    /// </summary>
    [PopupTemplate(PopupTemplateType.Confirm, 500f, 400f)]
    public class CharacterAscensionPopup : PopupWidget<CharacterAscensionPopup, CharacterAscensionState>
    {
        [Header("Header")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundButton;

        [Header("Ascension Display")]
        [SerializeField] private TMP_Text _currentAscensionText;
        [SerializeField] private TMP_Text _arrowText;
        [SerializeField] private TMP_Text _nextAscensionText;

        [Header("Requirements")]
        [SerializeField] private TMP_Text _levelRequirementText;
        [SerializeField] private Transform _materialContainer;

        [Header("Level Cap")]
        [SerializeField] private TMP_Text _levelCapChangeText;

        [Header("Stats Preview")]
        [SerializeField] private TMP_Text _hpBonusText;
        [SerializeField] private TMP_Text _atkBonusText;
        [SerializeField] private TMP_Text _defBonusText;
        [SerializeField] private TMP_Text _powerChangeText;

        [Header("Cost")]
        [SerializeField] private TMP_Text _goldCostText;
        [SerializeField] private TMP_Text _currentGoldText;

        [Header("Buttons")]
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_Text _confirmButtonText;

        [Header("Colors")]
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _insufficientColor = Color.red;
        [SerializeField] private Color _positiveColor = Color.green;
        [SerializeField] private Color _satisfiedColor = Color.green;

        private CharacterAscensionState _state;
        private AscensionRequirement? _requirement;
        private bool _meetsLevelRequirement;
        private bool _hasMaterials;
        private bool _hasGold;

        protected override void OnInitialize()
        {
            _closeButton?.onClick.AddListener(OnCloseClicked);
            _backgroundButton?.onClick.AddListener(OnCloseClicked);
            _confirmButton?.onClick.AddListener(OnConfirmClicked);
        }

        protected override void OnBind(CharacterAscensionState state)
        {
            _state = state;

            // 현재 돌파 단계의 요구사항 조회
            _requirement = _state.AscensionDb.GetRequirement(
                _state.CharacterData.Rarity,
                _state.Character.Ascension
            );

            RefreshUI();
        }

        public override CharacterAscensionState GetState() => _state;

        private void RefreshUI()
        {
            if (_state == null) return;

            // 제목
            if (_titleText != null)
                _titleText.text = $"{_state.CharacterData.Name} 돌파";

            // 현재 돌파 단계
            if (_currentAscensionText != null)
                _currentAscensionText.text = GetAscensionDisplayText(_state.Character.Ascension);

            // 최대 돌파 확인
            int maxAscension = _state.AscensionDb.GetMaxAscension(_state.CharacterData.Rarity);
            bool isMaxAscension = _state.Character.Ascension >= maxAscension;

            if (isMaxAscension)
            {
                ShowMaxAscensionState();
                return;
            }

            // 다음 돌파 단계
            if (_nextAscensionText != null)
                _nextAscensionText.text = GetAscensionDisplayText(_state.Character.Ascension + 1);

            if (_arrowText != null)
                _arrowText.gameObject.SetActive(true);

            // 요구사항 확인
            UpdateRequirements();
            UpdateStatsPreview();
            UpdateGoldDisplay();
            UpdateConfirmButton();
        }

        private void ShowMaxAscensionState()
        {
            if (_nextAscensionText != null)
                _nextAscensionText.text = "MAX";

            if (_arrowText != null)
                _arrowText.gameObject.SetActive(true);

            if (_levelRequirementText != null)
                _levelRequirementText.text = "최대 돌파 단계 도달";

            if (_confirmButton != null)
                _confirmButton.interactable = false;

            if (_confirmButtonText != null)
                _confirmButtonText.text = "최대 돌파";
        }

        private void UpdateRequirements()
        {
            if (!_requirement.HasValue) return;

            var req = _requirement.Value;

            // 레벨 요구사항
            _meetsLevelRequirement = _state.Character.Level >= req.RequiredCharacterLevel;
            if (_levelRequirementText != null)
            {
                _levelRequirementText.text = $"필요 레벨: Lv.{req.RequiredCharacterLevel} " +
                    (_meetsLevelRequirement ? "✓" : $"(현재: Lv.{_state.Character.Level})");
                _levelRequirementText.color = _meetsLevelRequirement ? _satisfiedColor : _insufficientColor;
            }

            // 재료 요구사항
            _hasMaterials = CheckMaterials(req.Materials);

            // 레벨 상한 변화
            int currentCap = _state.AscensionDb.GetLevelCap(
                _state.CharacterData.Rarity,
                _state.Character.Ascension,
                _state.LevelDb.BaseLevelCap
            );
            int newCap = currentCap + req.LevelCapIncrease;

            if (_levelCapChangeText != null)
            {
                _levelCapChangeText.text = $"레벨 상한: {currentCap} → {newCap} (+{req.LevelCapIncrease})";
                _levelCapChangeText.color = _positiveColor;
            }
        }

        private bool CheckMaterials(List<RewardInfo> materials)
        {
            if (materials == null || materials.Count == 0) return true;

            foreach (var mat in materials)
            {
                if (mat.Type != RewardType.Item) continue;

                var owned = _state.OwnedItems.Find(i => i.ItemId == mat.ItemId);
                if (owned.ItemId == null || owned.Count < mat.Amount)
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateStatsPreview()
        {
            if (!_requirement.HasValue) return;

            var bonus = _requirement.Value.StatBonus;

            // 현재 스탯
            var currentStats = PowerCalculator.CalculateStats(
                _state.Character,
                _state.CharacterData,
                _state.AscensionDb
            );
            int currentPower = PowerCalculator.Calculate(currentStats);

            // 돌파 후 스탯
            var previewChar = _state.Character;
            previewChar.Ascension += 1;
            var previewStats = PowerCalculator.CalculateStats(
                previewChar,
                _state.CharacterData,
                _state.AscensionDb
            );
            int previewPower = PowerCalculator.Calculate(previewStats);

            if (_hpBonusText != null && bonus.HP > 0)
            {
                _hpBonusText.text = $"HP +{bonus.HP:N0}";
                _hpBonusText.color = _positiveColor;
                _hpBonusText.gameObject.SetActive(true);
            }
            else if (_hpBonusText != null)
            {
                _hpBonusText.gameObject.SetActive(false);
            }

            if (_atkBonusText != null && bonus.ATK > 0)
            {
                _atkBonusText.text = $"ATK +{bonus.ATK:N0}";
                _atkBonusText.color = _positiveColor;
                _atkBonusText.gameObject.SetActive(true);
            }
            else if (_atkBonusText != null)
            {
                _atkBonusText.gameObject.SetActive(false);
            }

            if (_defBonusText != null && bonus.DEF > 0)
            {
                _defBonusText.text = $"DEF +{bonus.DEF:N0}";
                _defBonusText.color = _positiveColor;
                _defBonusText.gameObject.SetActive(true);
            }
            else if (_defBonusText != null)
            {
                _defBonusText.gameObject.SetActive(false);
            }

            if (_powerChangeText != null)
            {
                int diff = previewPower - currentPower;
                _powerChangeText.text = $"전투력: {currentPower:N0} → {previewPower:N0} (+{diff:N0})";
                _powerChangeText.color = _positiveColor;
            }
        }

        private void UpdateGoldDisplay()
        {
            if (!_requirement.HasValue) return;

            int goldCost = _requirement.Value.GoldCost;
            _hasGold = _state.Currency.Gold >= goldCost;

            if (_goldCostText != null)
            {
                _goldCostText.text = $"-{goldCost:N0}";
                _goldCostText.color = _hasGold ? _normalColor : _insufficientColor;
            }

            if (_currentGoldText != null)
            {
                _currentGoldText.text = $"(보유: {_state.Currency.Gold:N0})";
            }
        }

        private void UpdateConfirmButton()
        {
            bool canAscend = _meetsLevelRequirement && _hasMaterials && _hasGold;

            if (_confirmButton != null)
                _confirmButton.interactable = canAscend;

            if (_confirmButtonText != null)
            {
                if (!_meetsLevelRequirement)
                    _confirmButtonText.text = "레벨 부족";
                else if (!_hasMaterials)
                    _confirmButtonText.text = "재료 부족";
                else if (!_hasGold)
                    _confirmButtonText.text = "골드 부족";
                else
                    _confirmButtonText.text = "돌파";
            }
        }

        private string GetAscensionDisplayText(int ascension)
        {
            // ●○○○○○ 형식으로 표시
            int maxDisplay = 6;
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < maxDisplay; i++)
            {
                sb.Append(i < ascension ? "●" : "○");
            }
            return sb.ToString();
        }

        private void OnConfirmClicked()
        {
            if (!_meetsLevelRequirement || !_hasMaterials || !_hasGold) return;

            _state.OnConfirm?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnCloseClicked()
        {
            _state.OnCancel?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        public override bool OnEscape()
        {
            OnCloseClicked();
            return false;
        }

        protected override void OnRelease()
        {
            _state = null;
            _requirement = null;
        }
    }
}
