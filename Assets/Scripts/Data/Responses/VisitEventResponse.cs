using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 방문 응답
    /// </summary>
    [Serializable]
    public class VisitEventResponse : IResponse
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
        /// 이벤트 진행 상태
        /// </summary>
        public LiveEventProgress EventProgress;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static VisitEventResponse Success(LiveEventProgress eventProgress)
        {
            return new VisitEventResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                EventProgress = eventProgress
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static VisitEventResponse Fail(int errorCode, string errorMessage)
        {
            return new VisitEventResponse
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }
    }
}
