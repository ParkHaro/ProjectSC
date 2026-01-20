using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 방문 요청
    /// </summary>
    [Serializable]
    public class VisitEventRequest : IRequest<VisitEventResponse>
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public VisitEventRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 요청 생성
        /// </summary>
        public static VisitEventRequest Create(string eventId)
        {
            return new VisitEventRequest
            {
                EventId = eventId
            };
        }
    }
}
