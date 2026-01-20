using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 미션 진행 상태
    /// </summary>
    [Serializable]
    public struct EventMissionProgress
    {
        /// <summary>
        /// 미션 ID
        /// </summary>
        public string MissionId;

        /// <summary>
        /// 현재 진행 수치
        /// </summary>
        public int CurrentCount;

        /// <summary>
        /// 완료 여부
        /// </summary>
        public bool IsCompleted;

        /// <summary>
        /// 보상 수령 여부
        /// </summary>
        public bool IsClaimed;

        /// <summary>
        /// 진행률 (0.0 ~ 1.0)
        /// </summary>
        public float GetProgressRatio(int requiredCount)
        {
            if (requiredCount <= 0) return 0f;
            return Math.Min(1f, (float)CurrentCount / requiredCount);
        }

        /// <summary>
        /// 기본 진행 상태 생성
        /// </summary>
        public static EventMissionProgress CreateDefault(string missionId)
        {
            return new EventMissionProgress
            {
                MissionId = missionId,
                CurrentCount = 0,
                IsCompleted = false,
                IsClaimed = false
            };
        }
    }
}
