using System.Collections.Generic;
using System.Linq;
using Sc.Data;

namespace Sc.LocalServer
{
    /// <summary>
    /// 상점 구매 요청 핸들러 (서버측)
    /// 상품 구매, 재화 차감, 보상 지급 처리
    /// </summary>
    public class ShopHandler : IRequestHandler<ShopPurchaseRequest, ShopPurchaseResponse>
    {
        private readonly ServerValidator _validator;
        private readonly RewardService _rewardService;
        private readonly ServerTimeService _timeService;
        private readonly PurchaseLimitValidator _limitValidator;
        private ShopProductDatabase _productDatabase;

        // 에러 코드
        private const int ERROR_PRODUCT_NOT_FOUND = 1001;
        private const int ERROR_LIMIT_EXCEEDED = 1002;
        private const int ERROR_INSUFFICIENT_CURRENCY = 1003;
        private const int ERROR_PRODUCT_DISABLED = 1004;
        private const int ERROR_SERVER = 9999;

        public ShopHandler(
            ServerValidator validator,
            RewardService rewardService,
            ServerTimeService timeService)
        {
            _validator = validator;
            _rewardService = rewardService;
            _timeService = timeService;
            _limitValidator = new PurchaseLimitValidator(timeService);
        }

        /// <summary>
        /// ShopProductDatabase 설정 (외부에서 주입)
        /// </summary>
        public void SetProductDatabase(ShopProductDatabase database)
        {
            _productDatabase = database;
        }

        public ShopPurchaseResponse Handle(ShopPurchaseRequest request, ref UserSaveData userData)
        {
            // 0. 데이터베이스 확인
            if (_productDatabase == null)
            {
                return ShopPurchaseResponse.Fail(ERROR_SERVER, "상품 데이터베이스가 초기화되지 않았습니다.");
            }

            // 1. 상품 조회
            var product = _productDatabase.GetById(request.ProductId);
            if (product == null)
            {
                return ShopPurchaseResponse.Fail(ERROR_PRODUCT_NOT_FOUND, "존재하지 않는 상품입니다.");
            }

            // 2. 상품 활성화 여부 확인
            if (!product.IsEnabled)
            {
                return ShopPurchaseResponse.Fail(ERROR_PRODUCT_DISABLED, "판매 중지된 상품입니다.");
            }

            // 3. 구매 제한 검증
            var existingRecord = userData.FindShopPurchaseRecord(product.Id);
            if (!_limitValidator.CanPurchase(product, existingRecord, out var remainingCount))
            {
                return ShopPurchaseResponse.Fail(ERROR_LIMIT_EXCEEDED, $"구매 제한에 도달했습니다. (남은 횟수: {remainingCount})");
            }

            // 4. 재화 검증
            if (!HasEnoughCurrency(ref userData, product))
            {
                return ShopPurchaseResponse.Fail(ERROR_INSUFFICIENT_CURRENCY, "재화가 부족합니다.");
            }

            // 5. 재화 차감
            DeductCurrency(ref userData, product);

            // 6. 보상 지급
            var rewardList = product.Rewards.ToArray();
            var delta = _rewardService.CreateRewardDelta(rewardList, ref userData);

            // 7. 구매 기록 업데이트
            var updatedRecord = _limitValidator.UpdatePurchaseRecord(product, existingRecord);
            userData.UpdateShopPurchaseRecord(product.Id, updatedRecord);

            // 8. 응답 생성
            return ShopPurchaseResponse.Success(
                product.Id,
                rewardList.ToList(),
                delta,
                updatedRecord
            );
        }

        /// <summary>
        /// 재화 충분 여부 확인
        /// </summary>
        private bool HasEnoughCurrency(ref UserSaveData userData, ShopProductData product)
        {
            var costType = product.CostType;
            var amount = product.Price;

            return costType switch
            {
                CostType.None => true,
                CostType.Gold => _validator.HasEnoughGold(userData.Currency, amount),
                CostType.Gem => _validator.HasEnoughGem(userData.Currency, amount),
                CostType.Stamina => _validator.HasEnoughStamina(userData.Currency, amount),
                CostType.EventCurrency => userData.EventCurrency.CanAffordCurrency(product.EventId, amount),
                _ => false
            };
        }

        /// <summary>
        /// 재화 차감
        /// </summary>
        private void DeductCurrency(ref UserSaveData userData, ShopProductData product)
        {
            var costType = product.CostType;
            var amount = product.Price;

            switch (costType)
            {
                case CostType.None:
                    break;
                case CostType.Gold:
                    _rewardService.DeductGold(ref userData.Currency, amount);
                    break;
                case CostType.Gem:
                    _rewardService.DeductGem(ref userData.Currency, amount);
                    break;
                case CostType.Stamina:
                    userData.Currency.Stamina -= amount;
                    break;
                case CostType.EventCurrency:
                    userData.EventCurrency.DeductCurrency(product.EventId, amount);
                    break;
            }
        }
    }
}