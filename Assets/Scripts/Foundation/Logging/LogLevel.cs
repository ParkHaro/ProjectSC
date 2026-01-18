namespace Sc.Foundation
{
    /// <summary>
    /// 로그 심각도 레벨
    /// </summary>
    public enum LogLevel
    {
        /// <summary>개발 중 상세 정보 (릴리즈 strip)</summary>
        Debug = 0,

        /// <summary>일반 정보 (릴리즈 strip)</summary>
        Info = 1,

        /// <summary>주의 필요</summary>
        Warning = 2,

        /// <summary>오류 발생</summary>
        Error = 3,

        /// <summary>로깅 비활성화</summary>
        None = 99
    }
}
