using System;

namespace Sc.Data
{
    /// <summary>
    /// 활성 이벤트 목록 조회 요청
    /// </summary>
    [Serializable]
    public class GetActiveEventsRequest : IRequest<GetActiveEventsResponse>
    {
        /// <summary>
        /// 유예 기간 이벤트 포함 여부
        /// </summary>
        public bool IncludeGracePeriod;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public GetActiveEventsRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 요청 생성
        /// </summary>
        public static GetActiveEventsRequest Create(bool includeGracePeriod = true)
        {
            return new GetActiveEventsRequest
            {
                IncludeGracePeriod = includeGracePeriod
            };
        }
    }
}
