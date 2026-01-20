using System.Collections.Generic;
using Sc.Data;

namespace Sc.Event.OutGame
{
    #region ProductPurchase

    /// <summary>
    /// 상품 구매 완료 이벤트
    /// </summary>
    public readonly struct ProductPurchasedEvent
    {
        /// <summary>
        /// 구매한 상품 ID
        /// </summary>
        public string ProductId { get; init; }

        /// <summary>
        /// 획득한 보상 목록
        /// </summary>
        public List<RewardInfo> Rewards { get; init; }

        /// <summary>
        /// 갱신된 구매 기록
        /// </summary>
        public ShopPurchaseRecord UpdatedRecord { get; init; }

        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        public UserDataDelta Delta { get; init; }
    }

    /// <summary>
    /// 상품 구매 실패 이벤트
    /// </summary>
    public readonly struct ProductPurchaseFailedEvent
    {
        /// <summary>
        /// 구매 시도한 상품 ID
        /// </summary>
        public string ProductId { get; init; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion
}