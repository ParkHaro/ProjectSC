using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 개별 가챠 풀 천장 데이터
    /// </summary>
    [Serializable]
    public struct GachaPityInfo
    {
        /// <summary>
        /// 가챠 풀 ID
        /// </summary>
        public string GachaPoolId;

        /// <summary>
        /// 현재 천장 카운트
        /// </summary>
        public int PityCount;

        /// <summary>
        /// 총 뽑기 횟수
        /// </summary>
        public int TotalPullCount;

        /// <summary>
        /// 마지막 뽑기 시간 (Unix Timestamp)
        /// </summary>
        public long LastPullAt;
    }

    /// <summary>
    /// 가챠 천장 데이터
    /// </summary>
    [Serializable]
    public struct GachaPityData
    {
        /// <summary>
        /// 가챠 풀별 천장 정보
        /// </summary>
        public List<GachaPityInfo> PityInfos;

        /// <summary>
        /// 특정 가챠 풀의 천장 카운트 조회
        /// </summary>
        public int GetPityCount(string gachaPoolId)
        {
            if (PityInfos == null) return 0;
            foreach (var info in PityInfos)
            {
                if (info.GachaPoolId == gachaPoolId)
                    return info.PityCount;
            }
            return 0;
        }

        /// <summary>
        /// 특정 가챠 풀 정보 조회 또는 생성
        /// </summary>
        public GachaPityInfo GetOrCreatePityInfo(string gachaPoolId)
        {
            PityInfos ??= new List<GachaPityInfo>();

            for (int i = 0; i < PityInfos.Count; i++)
            {
                if (PityInfos[i].GachaPoolId == gachaPoolId)
                    return PityInfos[i];
            }

            var newInfo = new GachaPityInfo
            {
                GachaPoolId = gachaPoolId,
                PityCount = 0,
                TotalPullCount = 0,
                LastPullAt = 0
            };
            PityInfos.Add(newInfo);
            return newInfo;
        }

        /// <summary>
        /// 기본값으로 초기화
        /// </summary>
        public static GachaPityData CreateDefault()
        {
            return new GachaPityData
            {
                PityInfos = new List<GachaPityInfo>()
            };
        }
    }
}
