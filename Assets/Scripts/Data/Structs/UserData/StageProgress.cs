using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 개별 스테이지 클리어 데이터
    /// </summary>
    [Serializable]
    public struct StageClearInfo
    {
        /// <summary>
        /// 스테이지 ID
        /// </summary>
        public string StageId;

        /// <summary>
        /// 클리어 여부
        /// </summary>
        public bool IsCleared;

        /// <summary>
        /// 획득 별 수 (0~3)
        /// </summary>
        public int Stars;

        /// <summary>
        /// 최고 기록 (데미지, 턴 수 등)
        /// </summary>
        public int BestScore;

        /// <summary>
        /// 클리어 횟수
        /// </summary>
        public int ClearCount;

        /// <summary>
        /// 첫 클리어 시간 (Unix Timestamp)
        /// </summary>
        public long FirstClearedAt;
    }

    /// <summary>
    /// 스테이지 진행 데이터
    /// </summary>
    [Serializable]
    public struct StageProgress
    {
        /// <summary>
        /// 현재 진행 중인 챕터
        /// </summary>
        public int CurrentChapter;

        /// <summary>
        /// 현재 진행 중인 스테이지 번호
        /// </summary>
        public int CurrentStageNumber;

        /// <summary>
        /// 클리어한 스테이지 목록
        /// </summary>
        public List<StageClearInfo> ClearedStages;

        /// <summary>
        /// 스테이지 클리어 여부 확인
        /// </summary>
        public bool IsStageCleared(string stageId)
        {
            if (ClearedStages == null) return false;
            foreach (var info in ClearedStages)
            {
                if (info.StageId == stageId && info.IsCleared)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 스테이지 별 수 조회
        /// </summary>
        public int GetStageStars(string stageId)
        {
            if (ClearedStages == null) return 0;
            foreach (var info in ClearedStages)
            {
                if (info.StageId == stageId)
                    return info.Stars;
            }
            return 0;
        }

        /// <summary>
        /// 기본값으로 초기화된 진행 데이터 생성
        /// </summary>
        public static StageProgress CreateDefault()
        {
            return new StageProgress
            {
                CurrentChapter = 1,
                CurrentStageNumber = 1,
                ClearedStages = new List<StageClearInfo>()
            };
        }
    }
}
