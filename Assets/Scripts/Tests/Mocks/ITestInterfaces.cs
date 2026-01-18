using System;

namespace Sc.Tests
{
    /// <summary>
    /// 테스트용 시간 서비스 인터페이스.
    /// Phase 0 구현 시 Core로 이동 예정.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// 현재 서버 시간 (UTC Unix timestamp)
        /// </summary>
        long ServerTimeUtc { get; }

        /// <summary>
        /// 현재 서버 시간 (DateTime)
        /// </summary>
        DateTime ServerDateTime { get; }
    }

    // ISaveStorage는 Sc.Core.ISaveStorage로 이동됨
}
