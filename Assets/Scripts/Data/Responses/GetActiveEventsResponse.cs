using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 라이브 이벤트 정보 (Response용)
    /// </summary>
    [Serializable]
    public class LiveEventInfo
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId;

        /// <summary>
        /// 이벤트 타입
        /// </summary>
        public LiveEventType EventType;

        /// <summary>
        /// 이벤트 이름 키
        /// </summary>
        public string NameKey;

        /// <summary>
        /// 배너 이미지 경로
        /// </summary>
        public string BannerImage;

        /// <summary>
        /// 시작 시간 (Unix Timestamp)
        /// </summary>
        public long StartTime;

        /// <summary>
        /// 종료 시간 (Unix Timestamp)
        /// </summary>
        public long EndTime;

        /// <summary>
        /// 남은 일수
        /// </summary>
        public int RemainingDays;

        /// <summary>
        /// 유예 기간 중 여부
        /// </summary>
        public bool IsInGracePeriod;

        /// <summary>
        /// 유예 기간 남은 일수
        /// </summary>
        public int GracePeriodRemainingDays;

        /// <summary>
        /// 서브 컨텐츠 목록
        /// </summary>
        public List<EventSubContent> SubContents;

        /// <summary>
        /// 수령 가능 보상 여부
        /// </summary>
        public bool HasClaimableReward;

        /// <summary>
        /// 방문 여부
        /// </summary>
        public bool HasVisited;

        /// <summary>
        /// 이벤트 재화 사용 여부
        /// </summary>
        public bool HasEventCurrency;

        /// <summary>
        /// 이벤트 재화 정책
        /// </summary>
        public EventCurrencyPolicy CurrencyPolicy;
    }

    /// <summary>
    /// 활성 이벤트 목록 조회 응답
    /// </summary>
    [Serializable]
    public class GetActiveEventsResponse : IResponse
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
        /// 활성 이벤트 목록
        /// </summary>
        public List<LiveEventInfo> ActiveEvents;

        /// <summary>
        /// 유예 기간 이벤트 목록
        /// </summary>
        public List<LiveEventInfo> GracePeriodEvents;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static GetActiveEventsResponse Success(
            List<LiveEventInfo> activeEvents,
            List<LiveEventInfo> gracePeriodEvents = null)
        {
            return new GetActiveEventsResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                ActiveEvents = activeEvents ?? new List<LiveEventInfo>(),
                GracePeriodEvents = gracePeriodEvents ?? new List<LiveEventInfo>()
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static GetActiveEventsResponse Fail(int errorCode, string errorMessage)
        {
            return new GetActiveEventsResponse
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                ActiveEvents = new List<LiveEventInfo>(),
                GracePeriodEvents = new List<LiveEventInfo>()
            };
        }
    }
}
