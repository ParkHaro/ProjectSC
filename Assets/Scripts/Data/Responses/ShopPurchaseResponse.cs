using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 구매 보상 아이템
    /// </summary>
    [Serializable]
    public class PurchaseRewardItem
    {
        /// <summary>
        /// 보상 타입 (Currency, Item, Character)
        /// </summary>
        public string RewardType;

        /// <summary>
        /// 보상 ID (재화 타입 또는 아이템/캐릭터 ID)
        /// </summary>
        public string RewardId;

        /// <summary>
        /// 보상 수량
        /// </summary>
        public int Amount;
    }

    /// <summary>
    /// 상점 구매 응답
    /// </summary>
    [Serializable]
    public class ShopPurchaseResponse : IGameActionResponse
    {
        /// <summary>
        /// 요청 성공 여부
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 서버 타임스탬프
        /// </summary>
        public long ServerTime { get; set; }

        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        public UserDataDelta Delta { get; set; }

        /// <summary>
        /// 구매한 상품 ID
        /// </summary>
        public string ProductId;

        /// <summary>
        /// 획득한 보상 목록
        /// </summary>
        public List<PurchaseRewardItem> Rewards;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static ShopPurchaseResponse Success(string productId, List<PurchaseRewardItem> rewards, UserDataDelta delta)
        {
            return new ShopPurchaseResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Delta = delta,
                ProductId = productId,
                Rewards = rewards
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static ShopPurchaseResponse Fail(int errorCode, string errorMessage)
        {
            return new ShopPurchaseResponse
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Delta = UserDataDelta.Empty()
            };
        }
    }
}
