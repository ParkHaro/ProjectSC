using System;
using System.Collections.Generic;
using System.Linq;

namespace Sc.Data
{
    /// <summary>
    /// 라이브 이벤트 진행 상태
    /// </summary>
    [Serializable]
    public struct LiveEventProgress
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId;

        /// <summary>
        /// 미션 진행 상태 목록
        /// </summary>
        public List<EventMissionProgress> MissionProgresses;

        /// <summary>
        /// 방문 여부 (NEW 뱃지용)
        /// </summary>
        public bool HasVisited;

        /// <summary>
        /// 최초 방문 시간
        /// </summary>
        public long FirstVisitTime;

        /// <summary>
        /// 미션 진행 상태 조회
        /// </summary>
        public EventMissionProgress? GetMissionProgress(string missionId)
        {
            if (MissionProgresses == null) return null;
            foreach (var progress in MissionProgresses)
            {
                if (progress.MissionId == missionId)
                    return progress;
            }
            return null;
        }

        /// <summary>
        /// 미션 진행 상태 업데이트 또는 생성
        /// </summary>
        public void UpdateMissionProgress(EventMissionProgress progress)
        {
            MissionProgresses ??= new List<EventMissionProgress>();

            for (int i = 0; i < MissionProgresses.Count; i++)
            {
                if (MissionProgresses[i].MissionId == progress.MissionId)
                {
                    MissionProgresses[i] = progress;
                    return;
                }
            }

            MissionProgresses.Add(progress);
        }

        /// <summary>
        /// 완료된 미션 수
        /// </summary>
        public int GetCompletedMissionCount()
        {
            if (MissionProgresses == null) return 0;
            return MissionProgresses.Count(m => m.IsCompleted);
        }

        /// <summary>
        /// 보상 수령 가능한 미션 수
        /// </summary>
        public int GetClaimableMissionCount()
        {
            if (MissionProgresses == null) return 0;
            return MissionProgresses.Count(m => m.IsCompleted && !m.IsClaimed);
        }

        /// <summary>
        /// 모든 미션 완료 여부
        /// </summary>
        public bool IsAllMissionsCompleted(int totalMissionCount)
        {
            return GetCompletedMissionCount() >= totalMissionCount;
        }

        /// <summary>
        /// 기본 진행 상태 생성
        /// </summary>
        public static LiveEventProgress CreateDefault(string eventId)
        {
            return new LiveEventProgress
            {
                EventId = eventId,
                MissionProgresses = new List<EventMissionProgress>(),
                HasVisited = false,
                FirstVisitTime = 0
            };
        }
    }
}
