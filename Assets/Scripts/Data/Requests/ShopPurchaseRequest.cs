using System;

namespace Sc.Data
{
    /// <summary>
    /// 상점 구매 요청
    /// </summary>
    [Serializable]
    public class ShopPurchaseRequest : IRequest<ShopPurchaseResponse>
    {
        /// <summary>
        /// 상품 ID
        /// </summary>
        public string ProductId;

        /// <summary>
        /// 구매 수량
        /// </summary>
        public int Count;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public ShopPurchaseRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 구매 요청 생성
        /// </summary>
        public static ShopPurchaseRequest Create(string productId, int count = 1)
        {
            return new ShopPurchaseRequest
            {
                ProductId = productId,
                Count = count
            };
        }
    }
}
