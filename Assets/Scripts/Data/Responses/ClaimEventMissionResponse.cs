using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 미션 보상 수령 응답 (구조만)
    /// </summary>
    [Serializable]
    public class ClaimEventMissionResponse : IGameActionResponse
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
        /// 수령한 보상 목록
        /// </summary>
        public List<RewardInfo> ClaimedRewards;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static ClaimEventMissionResponse Success(List<RewardInfo> rewards, UserDataDelta delta)
        {
            return new ClaimEventMissionResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Delta = delta,
                ClaimedRewards = rewards ?? new List<RewardInfo>()
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static ClaimEventMissionResponse Fail(int errorCode, string errorMessage)
        {
            return new ClaimEventMissionResponse
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Delta = UserDataDelta.Empty(),
                ClaimedRewards = new List<RewardInfo>()
            };
        }
    }
}
