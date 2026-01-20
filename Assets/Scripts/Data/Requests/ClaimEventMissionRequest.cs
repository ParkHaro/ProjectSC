using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 미션 보상 수령 요청 (구조만)
    /// </summary>
    [Serializable]
    public class ClaimEventMissionRequest : IRequest<ClaimEventMissionResponse>
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId;

        /// <summary>
        /// 미션 ID
        /// </summary>
        public string MissionId;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public ClaimEventMissionRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 요청 생성
        /// </summary>
        public static ClaimEventMissionRequest Create(string eventId, string missionId)
        {
            return new ClaimEventMissionRequest
            {
                EventId = eventId,
                MissionId = missionId
            };
        }
    }
}
