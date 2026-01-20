using System;

namespace Sc.Data
{
    /// <summary>
    /// 상점 구매 기록
    /// </summary>
    [Serializable]
    public struct ShopPurchaseRecord
    {
        /// <summary>
        /// 상품 ID
        /// </summary>
        public string ProductId;

        /// <summary>
        /// 구매 횟수
        /// </summary>
        public int PurchaseCount;

        /// <summary>
        /// 마지막 구매 시간 (Unix Timestamp)
        /// </summary>
        public long LastPurchaseTime;

        /// <summary>
        /// 다음 리셋 시간 (Unix Timestamp, 0 = 리셋 없음)
        /// </summary>
        public long ResetTime;

        /// <summary>
        /// 생성자
        /// </summary>
        public ShopPurchaseRecord(string productId, int purchaseCount, long lastPurchaseTime, long resetTime)
        {
            ProductId = productId;
            PurchaseCount = purchaseCount;
            LastPurchaseTime = lastPurchaseTime;
            ResetTime = resetTime;
        }

        /// <summary>
        /// 새 구매 기록 생성
        /// </summary>
        public static ShopPurchaseRecord Create(string productId, long purchaseTime, long resetTime)
        {
            return new ShopPurchaseRecord(productId, 1, purchaseTime, resetTime);
        }

        /// <summary>
        /// 리셋이 필요한지 확인
        /// </summary>
        public bool NeedsReset(long currentTime)
        {
            return ResetTime > 0 && currentTime >= ResetTime;
        }

        /// <summary>
        /// 구매 횟수 증가
        /// </summary>
        public ShopPurchaseRecord IncrementPurchase(long purchaseTime, long newResetTime)
        {
            return new ShopPurchaseRecord(
                ProductId,
                PurchaseCount + 1,
                purchaseTime,
                newResetTime > 0 ? newResetTime : ResetTime
            );
        }

        /// <summary>
        /// 리셋 후 새 구매
        /// </summary>
        public ShopPurchaseRecord ResetAndPurchase(long purchaseTime, long newResetTime)
        {
            return new ShopPurchaseRecord(ProductId, 1, purchaseTime, newResetTime);
        }

        public override string ToString()
        {
            return $"[{ProductId}] Count: {PurchaseCount}, LastPurchase: {LastPurchaseTime}, Reset: {ResetTime}";
        }
    }
}