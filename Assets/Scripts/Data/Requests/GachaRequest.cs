using System;

namespace Sc.Data
{
    /// <summary>
    /// 가챠 타입
    /// </summary>
    public enum GachaPullType
    {
        Single,   // 1회
        Multi     // 10연차
    }

    /// <summary>
    /// 가챠 요청
    /// </summary>
    [Serializable]
    public class GachaRequest : IRequest<GachaResponse>
    {
        /// <summary>
        /// 가챠 풀 ID
        /// </summary>
        public string GachaPoolId;

        /// <summary>
        /// 뽑기 타입 (1회/10연차)
        /// </summary>
        public GachaPullType PullType;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public GachaRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 1회 뽑기 요청 생성
        /// </summary>
        public static GachaRequest CreateSingle(string gachaPoolId)
        {
            return new GachaRequest
            {
                GachaPoolId = gachaPoolId,
                PullType = GachaPullType.Single
            };
        }

        /// <summary>
        /// 10연차 요청 생성
        /// </summary>
        public static GachaRequest CreateMulti(string gachaPoolId)
        {
            return new GachaRequest
            {
                GachaPoolId = gachaPoolId,
                PullType = GachaPullType.Multi
            };
        }
    }
}
