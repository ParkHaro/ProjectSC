using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 가챠 결과 아이템
    /// </summary>
    [Serializable]
    public class GachaResultItem
    {
        /// <summary>
        /// 캐릭터 ID
        /// </summary>
        public string CharacterId;

        /// <summary>
        /// 희귀도
        /// </summary>
        public Rarity Rarity;

        /// <summary>
        /// 신규 획득 여부
        /// </summary>
        public bool IsNew;

        /// <summary>
        /// 천장 발동 여부
        /// </summary>
        public bool IsPity;
    }

    /// <summary>
    /// 가챠 응답
    /// </summary>
    [Serializable]
    public class GachaResponse : IGameActionResponse
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
        /// 가챠 결과 목록
        /// </summary>
        public List<GachaResultItem> Results;

        /// <summary>
        /// 현재 천장 카운트
        /// </summary>
        public int CurrentPityCount;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static GachaResponse Success(List<GachaResultItem> results, UserDataDelta delta, int pityCount)
        {
            return new GachaResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Delta = delta,
                Results = results,
                CurrentPityCount = pityCount
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static GachaResponse Fail(int errorCode, string errorMessage)
        {
            return new GachaResponse
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
