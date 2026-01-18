namespace Sc.Foundation
{
    /// <summary>
    /// 로그 출력 대상 인터페이스
    /// </summary>
    public interface ILogOutput
    {
        /// <summary>
        /// 로그 출력
        /// </summary>
        /// <param name="level">로그 레벨</param>
        /// <param name="category">로그 카테고리</param>
        /// <param name="message">로그 메시지</param>
        void Write(LogLevel level, LogCategory category, string message);
    }
}
