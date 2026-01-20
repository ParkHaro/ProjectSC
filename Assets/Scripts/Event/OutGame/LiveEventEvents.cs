using System.Collections.Generic;
using Sc.Data;

namespace Sc.Event.OutGame
{
    #region GetActiveEvents

    /// <summary>
    /// 활성 이벤트 목록 조회 완료 이벤트
    /// </summary>
    public readonly struct GetActiveEventsCompletedEvent
    {
        /// <summary>
        /// 활성 이벤트 목록
        /// </summary>
        public List<LiveEventInfo> ActiveEvents { get; init; }

        /// <summary>
        /// 유예 기간 이벤트 목록
        /// </summary>
        public List<LiveEventInfo> GracePeriodEvents { get; init; }

        /// <summary>
        /// 서버 시간
        /// </summary>
        public long ServerTime { get; init; }
    }

    /// <summary>
    /// 활성 이벤트 목록 조회 실패 이벤트
    /// </summary>
    public readonly struct GetActiveEventsFailedEvent
    {
        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region VisitEvent

    /// <summary>
    /// 이벤트 방문 완료 이벤트
    /// </summary>
    public readonly struct VisitEventCompletedEvent
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId { get; init; }

        /// <summary>
        /// 이벤트 진행 상태
        /// </summary>
        public LiveEventProgress EventProgress { get; init; }
    }

    /// <summary>
    /// 이벤트 방문 실패 이벤트
    /// </summary>
    public readonly struct VisitEventFailedEvent
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId { get; init; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region ClaimEventMission (구조만)

    /// <summary>
    /// 이벤트 미션 보상 수령 완료 이벤트 (구조만)
    /// </summary>
    public readonly struct ClaimEventMissionCompletedEvent
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId { get; init; }

        /// <summary>
        /// 미션 ID
        /// </summary>
        public string MissionId { get; init; }

        /// <summary>
        /// 수령한 보상 목록
        /// </summary>
        public List<RewardInfo> ClaimedRewards { get; init; }

        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        public UserDataDelta Delta { get; init; }
    }

    /// <summary>
    /// 이벤트 미션 보상 수령 실패 이벤트 (구조만)
    /// </summary>
    public readonly struct ClaimEventMissionFailedEvent
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId { get; init; }

        /// <summary>
        /// 미션 ID
        /// </summary>
        public string MissionId { get; init; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region EventCurrency

    /// <summary>
    /// 이벤트 재화 전환 완료 이벤트
    /// </summary>
    public readonly struct EventCurrencyConvertedEvent
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public string EventId { get; init; }

        /// <summary>
        /// 원본 재화 ID
        /// </summary>
        public string SourceCurrencyId { get; init; }

        /// <summary>
        /// 원본 재화 수량
        /// </summary>
        public int SourceAmount { get; init; }

        /// <summary>
        /// 전환된 재화 ID
        /// </summary>
        public string TargetCurrencyId { get; init; }

        /// <summary>
        /// 전환된 재화 수량
        /// </summary>
        public int TargetAmount { get; init; }
    }

    #endregion
}
