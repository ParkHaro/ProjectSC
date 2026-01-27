using System;
using Sc.Common.UI;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Shop
{
    /// <summary>
    /// 상점 상품 아이템 위젯
    /// </summary>
    public class ShopProductItem : Widget
    {
        [Header("Product Info")] [SerializeField]
        private Image _iconImage;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;

        [Header("Price")] [SerializeField] private Image _costTypeIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _originalPriceObject;
        [SerializeField] private TMP_Text _originalPriceText;

        [Header("Limit")] [SerializeField] private GameObject _limitObject;
        [SerializeField] private TMP_Text _limitText;
        [SerializeField] private TMP_Text _resetTimeText;

        [Header("State")] [SerializeField] private GameObject _soldOutObject;
        [SerializeField] private GameObject _discountBadge;
        [SerializeField] private TMP_Text _discountText;

        [Header("Interaction")] [SerializeField]
        private Button _button;

        private ShopProductData _productData;
        private ShopPurchaseRecord? _purchaseRecord;
        private Action<ShopProductData> _onClickCallback;

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(OnClicked);
            }
        }

        /// <summary>
        /// 상품 설정
        /// </summary>
        public void Setup(ShopProductData productData, ShopPurchaseRecord? record,
            Action<ShopProductData> onClickCallback)
        {
            _productData = productData;
            _purchaseRecord = record;
            _onClickCallback = onClickCallback;

            RefreshUI();
        }

        /// <summary>
        /// 상호작용 가능 여부 설정
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            base.SetInteractable(interactable);

            if (_button != null)
            {
                _button.interactable = interactable;
            }

            GetOrAddCanvasGroup().alpha = interactable ? 1f : 0.5f;
        }

        private void RefreshUI()
        {
            if (_productData == null) return;

            // 이름
            if (_nameText != null)
            {
                // TODO: StringData에서 실제 이름 가져오기
                _nameText.text = _productData.NameKey ?? _productData.Id;
            }

            // 설명
            if (_descriptionText != null)
            {
                _descriptionText.text = GetRewardDescription();
            }

            // 가격
            if (_priceText != null)
            {
                _priceText.text = $"{_productData.Price:N0}";
            }

            // 재화 타입 아이콘
            UpdateCostTypeIcon();

            // 구매 제한
            UpdateLimitDisplay();

            // 매진 여부
            UpdateSoldOutState();

            // 할인 배지 (TODO: 할인 시스템 연동)
            if (_discountBadge != null)
            {
                _discountBadge.SetActive(false);
            }

            // TODO: 상품 아이콘 로드 (Addressables)
            // LoadProductIcon(_productData.IconPath);
        }

        private string GetRewardDescription()
        {
            if (_productData.Rewards == null || _productData.Rewards.Count == 0)
                return "";

            // 첫 번째 보상 기준으로 설명 생성
            var firstReward = _productData.Rewards[0];
            var rewardText = firstReward.Type switch
            {
                RewardType.Currency => GetCurrencyText(firstReward),
                RewardType.Item => $"아이템 x{firstReward.Amount}",
                RewardType.Character => $"캐릭터 x{firstReward.Amount}",
                RewardType.PlayerExp => $"경험치 {firstReward.Amount:N0}",
                _ => $"x{firstReward.Amount}"
            };

            if (_productData.Rewards.Count > 1)
            {
                rewardText += $" 외 {_productData.Rewards.Count - 1}종";
            }

            return rewardText;
        }

        private string GetCurrencyText(RewardInfo reward)
        {
            // ItemId에 CostType 문자열이 저장됨 (예: "Gold", "Gem")
            return reward.ItemId switch
            {
                "Gold" => $"골드 {reward.Amount:N0}",
                "Gem" => $"다이아 {reward.Amount:N0}",
                "Stamina" => $"스태미나 {reward.Amount:N0}",
                _ => $"{reward.ItemId} {reward.Amount:N0}"
            };
        }

        private void UpdateCostTypeIcon()
        {
            if (_costTypeIcon == null) return;

            // TODO: CostType별 아이콘 설정
            // var iconPath = _productData.CostType switch
            // {
            //     CostType.Gold => "Icons/Currency/Gold",
            //     CostType.Gem => "Icons/Currency/Gem",
            //     CostType.EventCurrency => "Icons/Currency/Event",
            //     _ => "Icons/Currency/Default"
            // };
            // LoadCostIcon(iconPath);
        }

        private void UpdateLimitDisplay()
        {
            bool hasLimit = _productData.LimitType != LimitType.None && _productData.LimitCount > 0;

            if (_limitObject != null)
            {
                _limitObject.SetActive(hasLimit);
            }

            if (!hasLimit) return;

            int purchaseCount = _purchaseRecord?.PurchaseCount ?? 0;
            int remaining = _productData.LimitCount - purchaseCount;

            if (_limitText != null)
            {
                _limitText.text = $"{remaining}/{_productData.LimitCount}";
            }

            if (_resetTimeText != null)
            {
                _resetTimeText.text = GetLimitTypeText();
            }
        }

        private string GetLimitTypeText()
        {
            return _productData.LimitType switch
            {
                LimitType.Daily => "일일",
                LimitType.Weekly => "주간",
                LimitType.Monthly => "월간",
                LimitType.Permanent => "한정",
                _ => ""
            };
        }

        private void UpdateSoldOutState()
        {
            bool isSoldOut = IsSoldOut();

            if (_soldOutObject != null)
            {
                _soldOutObject.SetActive(isSoldOut);
            }

            if (_button != null)
            {
                _button.interactable = !isSoldOut;
            }

            GetOrAddCanvasGroup().alpha = isSoldOut ? 0.5f : 1f;
        }

        private bool IsSoldOut()
        {
            if (_productData.LimitType == LimitType.None || _productData.LimitCount <= 0)
                return false;

            // 리셋 필요 여부 확인
            var currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (_purchaseRecord.HasValue && _purchaseRecord.Value.NeedsReset(currentTime))
                return false;

            int purchaseCount = _purchaseRecord?.PurchaseCount ?? 0;
            return purchaseCount >= _productData.LimitCount;
        }

        private void OnClicked()
        {
            if (IsSoldOut())
            {
                Debug.Log($"[ShopProductItem] Product sold out: {_productData?.Id}");
                return;
            }

            Debug.Log($"[ShopProductItem] Clicked: {_productData?.Id}");
            _onClickCallback?.Invoke(_productData);
        }

        protected override void OnRelease()
        {
            _productData = null;
            _purchaseRecord = null;
            _onClickCallback = null;
        }
    }
}