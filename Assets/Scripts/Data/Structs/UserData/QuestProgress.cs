using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 퀘스트 상태
    /// </summary>
    public enum QuestStatus
    {
        NotStarted,   // 시작 안 함
        InProgress,   // 진행 중
        Completed,    // 완료 (보상 미수령)
        Claimed       // 보상 수령 완료
    }

    /// <summary>
    /// 개별 퀘스트 진행 정보
    /// </summary>
    [Serializable]
    public struct QuestInfo
    {
        /// <summary>
        /// 퀘스트 ID
        /// </summary>
        public string QuestId;

        /// <summary>
        /// 퀘스트 상태
        /// </summary>
        public QuestStatus Status;

        /// <summary>
        /// 현재 진행도
        /// </summary>
        public int Progress;

        /// <summary>
        /// 목표 진행도
        /// </summary>
        public int TargetProgress;

        /// <summary>
        /// 시작 시간 (Unix Timestamp)
        /// </summary>
        public long StartedAt;

        /// <summary>
        /// 완료 시간 (Unix Timestamp)
        /// </summary>
        public long CompletedAt;

        /// <summary>
        /// 완료 여부
        /// </summary>
        public bool IsCompleted => Status == QuestStatus.Completed || Status == QuestStatus.Claimed;

        /// <summary>
        /// 진행률 (0.0 ~ 1.0)
        /// </summary>
        public float ProgressRate => TargetProgress > 0 ? (float)Progress / TargetProgress : 0f;
    }

    /// <summary>
    /// 퀘스트 진행 데이터
    /// </summary>
    [Serializable]
    public struct QuestProgress
    {
        /// <summary>
        /// 일일 퀘스트 목록
        /// </summary>
        public List<QuestInfo> DailyQuests;

        /// <summary>
        /// 주간 퀘스트 목록
        /// </summary>
        public List<QuestInfo> WeeklyQuests;

        /// <summary>
        /// 업적 퀘스트 목록
        /// </summary>
        public List<QuestInfo> Achievements;

        /// <summary>
        /// 일일 퀘스트 리셋 시간 (Unix Timestamp)
        /// </summary>
        public long DailyResetAt;

        /// <summary>
        /// 주간 퀘스트 리셋 시간 (Unix Timestamp)
        /// </summary>
        public long WeeklyResetAt;

        /// <summary>
        /// 퀘스트 정보 조회
        /// </summary>
        public QuestInfo? GetQuestInfo(string questId)
        {
            if (DailyQuests != null)
            {
                foreach (var quest in DailyQuests)
                    if (quest.QuestId == questId) return quest;
            }
            if (WeeklyQuests != null)
            {
                foreach (var quest in WeeklyQuests)
                    if (quest.QuestId == questId) return quest;
            }
            if (Achievements != null)
            {
                foreach (var quest in Achievements)
                    if (quest.QuestId == questId) return quest;
            }
            return null;
        }

        /// <summary>
        /// 기본값으로 초기화
        /// </summary>
        public static QuestProgress CreateDefault()
        {
            return new QuestProgress
            {
                DailyQuests = new List<QuestInfo>(),
                WeeklyQuests = new List<QuestInfo>(),
                Achievements = new List<QuestInfo>(),
                DailyResetAt = 0,
                WeeklyResetAt = 0
            };
        }
    }
}
