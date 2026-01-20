using Sc.Foundation;

namespace Sc.Core.Initialization
{
    /// <summary>
    /// 초기화 진행률 변경 이벤트
    /// </summary>
    public readonly struct InitProgressChangedEvent
    {
        /// <summary>진행률 (0~1)</summary>
        public float Progress { get; init; }

        /// <summary>현재 단계 이름</summary>
        public string StepName { get; init; }
    }

    /// <summary>
    /// 초기화 완료 이벤트
    /// </summary>
    public readonly struct InitializationCompletedEvent
    {
        /// <summary>성공 여부</summary>
        public bool IsSuccess { get; init; }

        /// <summary>실패 시 에러 코드</summary>
        public ErrorCode ErrorCode { get; init; }

        /// <summary>실패 시 에러 메시지</summary>
        public string ErrorMessage { get; init; }
    }
}
