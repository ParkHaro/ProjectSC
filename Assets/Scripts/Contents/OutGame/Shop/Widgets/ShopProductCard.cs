using System;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Shop.Widgets
{
    /// <summary>
    /// 상점 상품 카드 위젯.
    /// 상품 정보를 카드 형태로 표시.
    /// </summary>
    public class ShopProductCard : MonoBehaviour
    {
        [Header("Card Background")] [SerializeField]
        private Image _cardBackground;

        [SerializeField] private Button _cardButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Product Info")] [SerializeField]
        private Image _productIcon;

        [SerializeField] private TMP_Text _productName;
        [SerializeField] private TMP_Text _productAmount;

        [Header("Tags")] [SerializeField] private GameObject _tagContainer;
        [SerializeField] private Image _tagBackground;
        [SerializeField] private TMP_Text _tagText;
        [SerializeField] private GameObject _dailyResetTag;

        [Header("Price")] [SerializeField] private Image _priceIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _originalPriceContainer;
        [SerializeField] private TMP_Text _originalPriceText;

        [Header("Purchase Limit")] [SerializeField]
        private GameObject _limitContainer;

        [SerializeField] private TMP_Text _limitText;

        [Header("Discount")] [SerializeField] private GameObject _discountBadge;
        [SerializeField] private TMP_Text _discountText;

        [Header("States")] [SerializeField] private GameObject _soldOutOverlay;
        [SerializeField] private TMP_Text _soldOutText;
        [SerializeField] private GameObject _selectedIndicator;

        [Header("Selection")] [SerializeField] private Toggle _selectToggle;
        [SerializeField] private GameObject _checkMark;

        private ShopProductData _productData;
        private ShopPurchaseRecord? _purchaseRecord;
        private Action<ShopProductCard> _onClickCallback;
        private Action<ShopProductCard, bool> _onSelectCallback;
        private bool _isSelected;

        /// <summary>
        /// 상품 데이터
        /// </summary>
        public ShopProductData ProductData => _productData;

        /// <summary>
        /// 선택 여부
        /// </summary>
        public bool IsSelected => _isSelected;

        /// <summary>
        /// 매진 여부
        /// </summary>
        public bool IsSoldOut => CheckSoldOut();

        private void Awake()
        {
            if (_cardButton != null)
            {
                _cardButton.onClick.AddListener(HandleClick);
            }

            if (_selectToggle != null)
            {
                _selectToggle.onValueChanged.AddListener(HandleSelectToggle);
            }
        }

        private void OnDestroy()
        {
            if (_cardButton != null)
            {
                _cardButton.onClick.RemoveListener(HandleClick);
            }

            if (_selectToggle != null)
            {
                _selectToggle.onValueChanged.RemoveListener(HandleSelectToggle);
            }
        }

        private void HandleClick()
        {
            if (IsSoldOut) return;
            _onClickCallback?.Invoke(this);
        }

        private void HandleSelectToggle(bool isOn)
        {
            _isSelected = isOn;
            UpdateCheckMark();
            _onSelectCallback?.Invoke(this, isOn);
        }

        /// <summary>
        /// 상품 설정
        /// </summary>
        public void Setup(
            ShopProductData productData,
            ShopPurchaseRecord? record,
            Action<ShopProductCard> onClick,
            Action<ShopProductCard, bool> onSelect = null)
        {
            _productData = productData;
            _purchaseRecord = record;
            _onClickCallback = onClick;
            _onSelectCallback = onSelect;
            _isSelected = false;

            RefreshUI();
        }

        /// <summary>
        /// 구매 기록 업데이트
        /// </summary>
        public void UpdatePurchaseRecord(ShopPurchaseRecord? record)
        {
            _purchaseRecord = record;
            UpdateLimitDisplay();
            UpdateSoldOutState();
        }

        /// <summary>
        /// 상호작용 가능 여부 설정
        /// </summary>
        public void SetInteractable(bool interactable)
        {
            if (_cardButton != null)
            {
                _cardButton.interactable = interactable && !IsSoldOut;
            }

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = interactable ? 1f : 0.5f;
            }

            if (_selectToggle != null)
            {
                _selectToggle.interactable = interactable && !IsSoldOut;
            }
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;

            if (_selectToggle != null)
            {
                _selectToggle.SetIsOnWithoutNotify(selected);
            }

            UpdateCheckMark();
        }

        /// <summary>
        /// 선택 모드 활성화/비활성화
        /// </summary>
        public void SetSelectMode(bool enabled)
        {
            if (_selectToggle != null)
            {
                _selectToggle.gameObject.SetActive(enabled);
            }

            if (!enabled)
            {
                SetSelected(false);
            }
        }

        private void RefreshUI()
        {
            if (_productData == null) return;

            UpdateProductInfo();
            UpdateTagDisplay();
            UpdatePriceDisplay();
            UpdateLimitDisplay();
            UpdateDiscountDisplay();
            UpdateSoldOutState();
            UpdateCheckMark();
        }

        private void UpdateProductInfo()
        {
            // 상품명
            if (_productName != null)
            {
                _productName.text = _productData.NameKey ?? _productData.Id;
            }

            // 상품 수량 (보상 첫번째 항목 기준)
            if (_productAmount != null && _productData.Rewards != null && _productData.Rewards.Count > 0)
            {
                var firstReward = _productData.Rewards[0];
                _productAmount.text = firstReward.Amount > 1 ? $"x{firstReward.Amount}" : "";
            }

            // TODO: Addressables로 아이콘 로드
            // LoadProductIcon(_productData.IconPath);
        }

        private void UpdateTagDisplay()
        {
            if (_tagContainer == null) return;

            // 일일 갱신 태그
            bool isDailyReset = _productData.LimitType == LimitType.Daily;
            if (_dailyResetTag != null)
            {
                _dailyResetTag.SetActive(isDailyReset);
            }

            // 기타 태그 (예: 인기, 추천 등)
            if (_tagText != null)
            {
                _tagText.text = GetTagText();
            }

            _tagContainer.SetActive(isDailyReset || !string.IsNullOrEmpty(_tagText?.text));
        }

        private string GetTagText()
        {
            // 리셋 타입에 따른 태그
            return _productData.LimitType switch
            {
                LimitType.Daily => "일일갱신",
                LimitType.Weekly => "주간갱신",
                LimitType.Monthly => "월간갱신",
                LimitType.Permanent => "한정",
                _ => ""
            };
        }

        private void UpdatePriceDisplay()
        {
            if (_priceText != null)
            {
                _priceText.text = $"{_productData.Price:N0}";
            }

            // TODO: CostType에 따른 아이콘 설정
            // UpdatePriceIcon(_productData.CostType);

            // 할인 시 원가 표시
            if (_originalPriceContainer != null)
            {
                // TODO: 할인 시스템 연동
                _originalPriceContainer.SetActive(false);
            }
        }

        private void UpdateLimitDisplay()
        {
            bool hasLimit = _productData.LimitType != LimitType.None && _productData.LimitCount > 0;

            if (_limitContainer != null)
            {
                _limitContainer.SetActive(hasLimit);
            }

            if (!hasLimit || _limitText == null) return;

            int purchaseCount = _purchaseRecord?.PurchaseCount ?? 0;
            int remaining = _productData.LimitCount - purchaseCount;
            _limitText.text = $"구매 가능 {remaining}/{_productData.LimitCount}";
        }

        private void UpdateDiscountDisplay()
        {
            if (_discountBadge == null) return;

            // TODO: 할인 시스템 연동
            _discountBadge.SetActive(false);
        }

        private void UpdateSoldOutState()
        {
            bool soldOut = CheckSoldOut();

            if (_soldOutOverlay != null)
            {
                _soldOutOverlay.SetActive(soldOut);
            }

            if (_cardButton != null)
            {
                _cardButton.interactable = !soldOut;
            }

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = soldOut ? 0.5f : 1f;
            }

            if (_selectToggle != null)
            {
                _selectToggle.interactable = !soldOut;
            }
        }

        private bool CheckSoldOut()
        {
            if (_productData == null) return false;
            if (_productData.LimitType == LimitType.None || _productData.LimitCount <= 0)
                return false;

            // 리셋 필요 여부 확인
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (_purchaseRecord.HasValue && _purchaseRecord.Value.NeedsReset(currentTime))
                return false;

            int purchaseCount = _purchaseRecord?.PurchaseCount ?? 0;
            return purchaseCount >= _productData.LimitCount;
        }

        private void UpdateCheckMark()
        {
            if (_checkMark != null)
            {
                _checkMark.SetActive(_isSelected);
            }

            if (_selectedIndicator != null)
            {
                _selectedIndicator.SetActive(_isSelected);
            }
        }
    }
}