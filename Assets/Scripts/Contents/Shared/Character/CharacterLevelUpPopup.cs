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
    /// 캐릭터 레벨업 팝업 상태
    /// </summary>
    public class CharacterLevelUpState : IPopupState
    {
        public OwnedCharacter Character;
        public CharacterData CharacterData;
        public CharacterLevelDatabase LevelDb;
        public CharacterAscensionDatabase AscensionDb;
        public ItemDatabase ItemDb;
        public List<OwnedItem> AvailableMaterials = new();
        public UserCurrency Currency;

        public Action<Dictionary<string, int>> OnConfirm;
        public Action OnCancel;

        public bool AllowBackgroundDismiss => true;
    }

    /// <summary>
    /// 캐릭터 레벨업 팝업
    /// </summary>
    [PopupTemplate(PopupTemplateType.Confirm, 500f, 400f)]
    public class CharacterLevelUpPopup : PopupWidget<CharacterLevelUpPopup, CharacterLevelUpState>
    {
        [Header("Header")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundButton;

        [Header("Level Display")]
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _arrowText;
        [SerializeField] private TMP_Text _targetLevelText;
        [SerializeField] private Slider _expProgressBar;
        [SerializeField] private TMP_Text _expText;

        [Header("Stats Preview")]
        [SerializeField] private TMP_Text _hpChangeText;
        [SerializeField] private TMP_Text _atkChangeText;
        [SerializeField] private TMP_Text _defChangeText;
        [SerializeField] private TMP_Text _powerChangeText;

        [Header("Materials")]
        [SerializeField] private Transform _materialContainer;
        [SerializeField] private Button _autoSelectButton;

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

        private CharacterLevelUpState _state;
        private Dictionary<string, int> _selectedMaterials = new();
        private long _previewTotalExp;
        private int _previewLevel;
        private int _totalGoldCost;

        protected override void OnInitialize()
        {
            _closeButton?.onClick.AddListener(OnCloseClicked);
            _backgroundButton?.onClick.AddListener(OnCloseClicked);
            _confirmButton?.onClick.AddListener(OnConfirmClicked);
            _autoSelectButton?.onClick.AddListener(OnAutoSelectClicked);
        }

        protected override void OnBind(CharacterLevelUpState state)
        {
            _state = state;
            _selectedMaterials.Clear();
            _previewTotalExp = _state.Character.Exp;
            _previewLevel = _state.Character.Level;
            _totalGoldCost = 0;

            RefreshUI();
        }

        public override CharacterLevelUpState GetState() => _state;

        private void RefreshUI()
        {
            if (_state == null) return;

            // 제목
            if (_titleText != null)
                _titleText.text = $"{_state.CharacterData.Name} 레벨업";

            // 현재 레벨
            if (_currentLevelText != null)
                _currentLevelText.text = $"Lv.{_state.Character.Level}";

            // 레벨 상한 계산
            int levelCap = _state.AscensionDb.GetLevelCap(
                _state.CharacterData.Rarity,
                _state.Character.Ascension,
                _state.LevelDb.BaseLevelCap
            );

            // 미리보기 레벨 계산
            _previewLevel = _state.LevelDb.CalculateLevelFromExp(
                _state.CharacterData.Rarity,
                _previewTotalExp,
                levelCap
            );

            // 목표 레벨 표시
            bool hasLevelUp = _previewLevel > _state.Character.Level;
            if (_targetLevelText != null)
            {
                _targetLevelText.text = hasLevelUp ? $"Lv.{_previewLevel}" : "-";
                _targetLevelText.color = hasLevelUp ? _positiveColor : _normalColor;
            }

            if (_arrowText != null)
                _arrowText.gameObject.SetActive(hasLevelUp);

            // 경험치 진행률
            UpdateExpProgress();

            // 스탯 미리보기
            UpdateStatsPreview();

            // 골드 비용
            UpdateGoldDisplay();

            // 확인 버튼 상태
            UpdateConfirmButton();
        }

        private void UpdateExpProgress()
        {
            if (_state == null) return;

            var currentReq = _state.LevelDb.GetRequirement(_state.CharacterData.Rarity, _previewLevel);
            var nextReq = _state.LevelDb.GetRequirement(_state.CharacterData.Rarity, _previewLevel + 1);

            if (nextReq.HasValue && currentReq.HasValue)
            {
                long currentLevelExp = currentReq.Value.RequiredExp;
                long nextLevelExp = nextReq.Value.RequiredExp;
                long expInLevel = _previewTotalExp - currentLevelExp;
                long expNeeded = nextLevelExp - currentLevelExp;

                if (_expProgressBar != null)
                    _expProgressBar.value = expNeeded > 0 ? (float)expInLevel / expNeeded : 1f;

                if (_expText != null)
                    _expText.text = $"{expInLevel:N0} / {expNeeded:N0}";
            }
            else
            {
                if (_expProgressBar != null)
                    _expProgressBar.value = 1f;

                if (_expText != null)
                    _expText.text = "MAX";
            }
        }

        private void UpdateStatsPreview()
        {
            if (_state == null) return;

            var currentStats = PowerCalculator.CalculateStats(
                _state.Character,
                _state.CharacterData,
                _state.AscensionDb
            );
            int currentPower = PowerCalculator.Calculate(currentStats);

            var previewChar = _state.Character;
            previewChar.Level = _previewLevel;
            var previewStats = PowerCalculator.CalculateStats(
                previewChar,
                _state.CharacterData,
                _state.AscensionDb
            );
            int previewPower = PowerCalculator.Calculate(previewStats);

            bool hasChange = _previewLevel > _state.Character.Level;

            if (_hpChangeText != null)
            {
                int diff = previewStats.HP - currentStats.HP;
                _hpChangeText.text = hasChange ? $"HP: {currentStats.HP:N0} → {previewStats.HP:N0} (+{diff:N0})" : $"HP: {currentStats.HP:N0}";
                _hpChangeText.color = hasChange ? _positiveColor : _normalColor;
            }

            if (_atkChangeText != null)
            {
                int diff = previewStats.ATK - currentStats.ATK;
                _atkChangeText.text = hasChange ? $"ATK: {currentStats.ATK:N0} → {previewStats.ATK:N0} (+{diff:N0})" : $"ATK: {currentStats.ATK:N0}";
                _atkChangeText.color = hasChange ? _positiveColor : _normalColor;
            }

            if (_defChangeText != null)
            {
                int diff = previewStats.DEF - currentStats.DEF;
                _defChangeText.text = hasChange ? $"DEF: {currentStats.DEF:N0} → {previewStats.DEF:N0} (+{diff:N0})" : $"DEF: {currentStats.DEF:N0}";
                _defChangeText.color = hasChange ? _positiveColor : _normalColor;
            }

            if (_powerChangeText != null)
            {
                int diff = previewPower - currentPower;
                _powerChangeText.text = hasChange ? $"전투력: {currentPower:N0} → {previewPower:N0} (+{diff:N0})" : $"전투력: {currentPower:N0}";
                _powerChangeText.color = hasChange ? _positiveColor : _normalColor;
            }
        }

        private void UpdateGoldDisplay()
        {
            if (_goldCostText != null)
            {
                _goldCostText.text = $"-{_totalGoldCost:N0}";
                _goldCostText.color = _state.Currency.Gold >= _totalGoldCost ? _normalColor : _insufficientColor;
            }

            if (_currentGoldText != null)
            {
                _currentGoldText.text = $"(보유: {_state.Currency.Gold:N0})";
            }
        }

        private void UpdateConfirmButton()
        {
            bool hasSelection = _selectedMaterials.Count > 0;
            bool hasEnoughGold = _state.Currency.Gold >= _totalGoldCost;
            bool canLevelUp = hasSelection && hasEnoughGold;

            if (_confirmButton != null)
                _confirmButton.interactable = canLevelUp;

            if (_confirmButtonText != null)
                _confirmButtonText.text = canLevelUp ? "레벨업" : (hasSelection ? "골드 부족" : "재료 선택");
        }

        /// <summary>
        /// 재료 사용량 변경 시 호출 (외부 MaterialSlot에서)
        /// </summary>
        public void OnMaterialAmountChanged(string itemId, int amount, int expValue, int goldCost)
        {
            if (amount <= 0)
            {
                _selectedMaterials.Remove(itemId);
            }
            else
            {
                _selectedMaterials[itemId] = amount;
            }

            // 총 경험치/골드 재계산
            RecalculateTotals();
            RefreshUI();
        }

        private void RecalculateTotals()
        {
            _previewTotalExp = _state.Character.Exp;
            _totalGoldCost = 0;

            foreach (var (itemId, count) in _selectedMaterials)
            {
                var itemData = _state.ItemDb?.GetById(itemId);
                if (itemData != null)
                {
                    _previewTotalExp += itemData.ExpValue * count;
                    _totalGoldCost += itemData.GoldCostPerUse * count;
                }
            }
        }

        private void OnAutoSelectClicked()
        {
            // 레벨 상한까지 자동 선택
            int levelCap = _state.AscensionDb.GetLevelCap(
                _state.CharacterData.Rarity,
                _state.Character.Ascension,
                _state.LevelDb.BaseLevelCap
            );

            var levelCapReq = _state.LevelDb.GetRequirement(_state.CharacterData.Rarity, levelCap);
            long targetExp = levelCapReq?.RequiredExp ?? _state.Character.Exp;

            _selectedMaterials.Clear();
            long currentExp = _state.Character.Exp;
            long currentGold = _state.Currency.Gold;
            int totalGoldCost = 0;

            // 경험치가 높은 재료부터 선택
            var sortedMaterials = new List<OwnedItem>(_state.AvailableMaterials);
            sortedMaterials.Sort((a, b) =>
            {
                var dataA = _state.ItemDb?.GetById(a.ItemId);
                var dataB = _state.ItemDb?.GetById(b.ItemId);
                return (dataB?.ExpValue ?? 0).CompareTo(dataA?.ExpValue ?? 0);
            });

            foreach (var material in sortedMaterials)
            {
                if (currentExp >= targetExp) break;

                var itemData = _state.ItemDb?.GetById(material.ItemId);
                if (itemData == null || itemData.ExpValue <= 0) continue;

                int maxUse = material.Count;
                int neededForLevel = (int)Math.Ceiling((targetExp - currentExp) / (float)itemData.ExpValue);
                int affordableByGold = itemData.GoldCostPerUse > 0
                    ? (int)((currentGold - totalGoldCost) / itemData.GoldCostPerUse)
                    : int.MaxValue;

                int useCount = Math.Min(maxUse, Math.Min(neededForLevel, affordableByGold));

                if (useCount > 0)
                {
                    _selectedMaterials[material.ItemId] = useCount;
                    currentExp += itemData.ExpValue * useCount;
                    totalGoldCost += itemData.GoldCostPerUse * useCount;
                }
            }

            RecalculateTotals();
            RefreshUI();
        }

        private void OnConfirmClicked()
        {
            if (_selectedMaterials.Count == 0) return;

            _state.OnConfirm?.Invoke(new Dictionary<string, int>(_selectedMaterials));
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
            _selectedMaterials.Clear();
        }
    }
}
