namespace Sc.Data
{
    /// <summary>
    /// API 응답 인터페이스
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// 요청 성공 여부
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// 에러 코드 (성공 시 0)
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// 서버 타임스탬프 (Unix Timestamp)
        /// </summary>
        long ServerTime { get; }
    }

    /// <summary>
    /// 게임 액션 응답 인터페이스 (유저 데이터 변경 포함)
    /// </summary>
    public interface IGameActionResponse : IResponse
    {
        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        UserDataDelta Delta { get; }
    }
}
