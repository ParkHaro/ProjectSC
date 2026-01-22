using System.Collections.Generic;
using System.Text;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 확률 상세 팝업
    /// </summary>
    [PopupTemplate(PopupTemplateType.Info)]
    public class RateDetailPopup : PopupWidget<RateDetailPopup, RateDetailState>
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _ratesText;
        [SerializeField] private TMP_Text _pityInfoText;
        [SerializeField] private TMP_Text _characterListText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundButton;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Character List")]
        [SerializeField] private Transform _characterListContainer;
        [SerializeField] private GameObject _characterItemPrefab;

        private RateDetailState _currentState;
        private readonly List<GameObject> _spawnedItems = new();

        protected override void OnInitialize()
        {
            Debug.Log("[RateDetailPopup] OnInitialize");

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(OnCloseClicked);
            }

            if (_backgroundButton != null)
            {
                _backgroundButton.onClick.AddListener(OnCloseClicked);
            }
        }

        protected override void OnBind(RateDetailState state)
        {
            _currentState = state ?? new RateDetailState();
            Debug.Log($"[RateDetailPopup] OnBind - Pool: {_currentState.PoolData?.Name ?? "null"}");

            RefreshUI();
        }

        protected override void OnShow()
        {
            Debug.Log("[RateDetailPopup] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[RateDetailPopup] OnHide");
        }

        protected override void OnRelease()
        {
            ClearCharacterItems();
        }

        public override RateDetailState GetState() => _currentState;

        private void RefreshUI()
        {
            var pool = _currentState?.PoolData;
            if (pool == null) return;

            // 타이틀
            if (_titleText != null)
            {
                _titleText.text = $"{pool.Name} 확률 정보";
            }

            // 기본 확률 정보
            RefreshRatesInfo(pool);

            // 천장 정보
            RefreshPityInfo(pool);

            // 캐릭터 목록
            RefreshCharacterList(pool);
        }

        private void RefreshRatesInfo(GachaPoolData pool)
        {
            if (_ratesText == null) return;

            var sb = new StringBuilder();
            sb.AppendLine("<b>기본 확률</b>");
            sb.AppendLine();

            var rates = pool.Rates;
            sb.AppendLine($"<color=#FFD700>SSR:</color> {rates.SSR * 100f:F2}%");
            sb.AppendLine($"<color=#9932CC>SR:</color> {rates.SR * 100f:F2}%");
            sb.AppendLine($"<color=#4169E1>R:</color> {rates.R * 100f:F2}%");

            // 픽업 정보
            if (pool.Type == GachaType.Pickup && !string.IsNullOrEmpty(pool.RateUpCharacterId))
            {
                sb.AppendLine();
                sb.AppendLine("<b>픽업 정보</b>");

                var rateUpChar = DataManager.Instance?.Characters?.GetById(pool.RateUpCharacterId);
                var charName = rateUpChar?.Name ?? pool.RateUpCharacterId;

                sb.AppendLine($"대상: {charName}");
                sb.AppendLine($"SSR 중 확률: {pool.RateUpBonus * 100f:F1}%");
            }

            _ratesText.text = sb.ToString();
        }

        private void RefreshPityInfo(GachaPoolData pool)
        {
            if (_pityInfoText == null) return;

            if (pool.PityCount <= 0)
            {
                _pityInfoText.text = "이 소환에는 천장 시스템이 없습니다.";
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<b>천장 시스템</b>");
            sb.AppendLine();
            sb.AppendLine($"<color=#FF6B6B>확정 천장:</color> {pool.PityCount}회");

            if (pool.PitySoftStart > 0 && pool.PitySoftRateBonus > 0)
            {
                sb.AppendLine();
                sb.AppendLine("<b>소프트 천장</b>");
                sb.AppendLine($"시작 구간: {pool.PitySoftStart}회부터");
                sb.AppendLine($"확률 증가: 매 회차 +{pool.PitySoftRateBonus * 100f:F1}%");

                // 소프트 천장 설명
                sb.AppendLine();
                sb.AppendLine("<size=80%><color=#888888>");
                sb.AppendLine($"* {pool.PitySoftStart}회부터 SSR 확률이 점진적으로 증가합니다.");
                sb.AppendLine($"* {pool.PityCount}회 도달 시 SSR이 확정됩니다.");
                sb.AppendLine("</color></size>");
            }

            // 현재 천장 진행도
            var pityData = DataManager.Instance?.GachaPity;
            if (pityData.HasValue)
            {
                var pityInfo = pityData.Value.GetOrCreatePityInfo(pool.Id);
                sb.AppendLine();
                sb.AppendLine($"<color=#00CED1>현재 진행도: {pityInfo.PityCount}/{pool.PityCount}</color>");

                // 현재 적용 확률 계산
                if (pool.PitySoftStart > 0 && pityInfo.PityCount >= pool.PitySoftStart)
                {
                    var bonusCount = pityInfo.PityCount - pool.PitySoftStart;
                    var currentBonus = bonusCount * pool.PitySoftRateBonus;
                    var currentRate = (pool.Rates.SSR + currentBonus) * 100f;

                    sb.AppendLine($"<color=#FF6B6B>현재 SSR 확률: {currentRate:F2}%</color>");
                }
            }

            _pityInfoText.text = sb.ToString();
        }

        private void RefreshCharacterList(GachaPoolData pool)
        {
            ClearCharacterItems();

            if (_characterListText == null && _characterListContainer == null) return;

            var characterIds = pool.CharacterIds;
            if (characterIds == null || characterIds.Length == 0)
            {
                if (_characterListText != null)
                {
                    _characterListText.text = "캐릭터 정보가 없습니다.";
                }
                return;
            }

            // 텍스트 기반 리스트
            if (_characterListText != null)
            {
                var sb = new StringBuilder();
                sb.AppendLine("<b>출현 캐릭터</b>");
                sb.AppendLine();

                // 희귀도별 분류
                var ssrChars = new List<string>();
                var srChars = new List<string>();
                var rChars = new List<string>();

                foreach (var charId in characterIds)
                {
                    var charData = DataManager.Instance?.Characters?.GetById(charId);
                    if (charData == null) continue;

                    var displayName = charData.Name;
                    var isRateUp = charId == pool.RateUpCharacterId;

                    if (isRateUp)
                    {
                        displayName = $"<color=#FF6B6B>{displayName} [UP]</color>";
                    }

                    switch (charData.Rarity)
                    {
                        case Rarity.SSR:
                            ssrChars.Add(displayName);
                            break;
                        case Rarity.SR:
                            srChars.Add(displayName);
                            break;
                        case Rarity.R:
                            rChars.Add(displayName);
                            break;
                    }
                }

                if (ssrChars.Count > 0)
                {
                    sb.AppendLine("<color=#FFD700>[SSR]</color>");
                    foreach (var name in ssrChars)
                    {
                        sb.AppendLine($"  {name}");
                    }
                    sb.AppendLine();
                }

                if (srChars.Count > 0)
                {
                    sb.AppendLine("<color=#9932CC>[SR]</color>");
                    foreach (var name in srChars)
                    {
                        sb.AppendLine($"  {name}");
                    }
                    sb.AppendLine();
                }

                if (rChars.Count > 0)
                {
                    sb.AppendLine("<color=#4169E1>[R]</color>");
                    foreach (var name in rChars)
                    {
                        sb.AppendLine($"  {name}");
                    }
                }

                _characterListText.text = sb.ToString();
            }

            // 프리팹 기반 리스트 (선택적)
            if (_characterListContainer != null && _characterItemPrefab != null)
            {
                foreach (var charId in characterIds)
                {
                    var charData = DataManager.Instance?.Characters?.GetById(charId);
                    if (charData == null) continue;

                    var itemGo = Instantiate(_characterItemPrefab, _characterListContainer);
                    SetupCharacterItem(itemGo, charData, charId == pool.RateUpCharacterId);
                    _spawnedItems.Add(itemGo);
                }
            }
        }

        private void SetupCharacterItem(GameObject itemGo, CharacterData charData, bool isRateUp)
        {
            var nameText = itemGo.GetComponentInChildren<TMP_Text>();
            if (nameText != null)
            {
                var displayName = charData.Name;
                if (isRateUp)
                {
                    displayName += " <color=#FF6B6B>[UP]</color>";
                }
                nameText.text = $"[{charData.Rarity}] {displayName}";
            }

            var image = itemGo.GetComponent<Image>();
            if (image != null)
            {
                image.color = GetRarityColor(charData.Rarity);
            }
        }

        private Color GetRarityColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.SSR => new Color(1f, 0.84f, 0f, 0.3f),
                Rarity.SR => new Color(0.5f, 0f, 0.5f, 0.3f),
                Rarity.R => new Color(0f, 0.5f, 1f, 0.3f),
                _ => new Color(0.5f, 0.5f, 0.5f, 0.3f)
            };
        }

        private void ClearCharacterItems()
        {
            foreach (var item in _spawnedItems)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
            _spawnedItems.Clear();
        }

        private void OnCloseClicked()
        {
            Debug.Log("[RateDetailPopup] Close clicked");
            NavigationManager.Instance?.Pop();
        }

        /// <summary>
        /// ESC로 닫기 허용
        /// </summary>
        public override bool OnEscape() => true;
    }
}
