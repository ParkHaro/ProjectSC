namespace Sc.Data
{
    /// <summary>
    /// API 요청 기본 인터페이스
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// 요청 타임스탬프 (Unix Timestamp)
        /// </summary>
        long Timestamp { get; }
    }

    /// <summary>
    /// API 요청 인터페이스 (응답 타입 지정)
    /// Request가 자신의 Response 타입을 명시
    /// </summary>
    /// <typeparam name="TResponse">응답 타입</typeparam>
    public interface IRequest<TResponse> : IRequest
        where TResponse : IResponse
    {
    }
}
