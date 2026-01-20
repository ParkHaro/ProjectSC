using System;
using Sc.LocalServer;

namespace Sc.Editor.Tests.Mocks
{
    /// <summary>
    /// 테스트용 ServerTimeService.
    /// ServerTimeService를 상속하여 LocalServer 테스트에서 사용.
    /// 고정된 시간을 반환하거나 시간 조작이 가능.
    /// </summary>
    public class TestServerTimeService : ServerTimeService
    {
        private long _fixedTimeUtc;
        private bool _useFixedTime;

        public TestServerTimeService()
        {
            _useFixedTime = false;
        }

        public TestServerTimeService(DateTime dateTime)
        {
            _fixedTimeUtc = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
            _useFixedTime = true;
        }

        /// <summary>
        /// 현재 서버 시간 (고정 시간 또는 실시간)
        /// </summary>
        public new long ServerTimeUtc => _useFixedTime
            ? _fixedTimeUtc
            : DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        /// <summary>
        /// 고정 시간 설정
        /// </summary>
        public void SetFixedTime(DateTime dateTime)
        {
            _fixedTimeUtc = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
            _useFixedTime = true;
        }

        /// <summary>
        /// 고정 시간 설정 (Unix Timestamp)
        /// </summary>
        public void SetFixedTime(long utcTimestamp)
        {
            _fixedTimeUtc = utcTimestamp;
            _useFixedTime = true;
        }

        /// <summary>
        /// 일 단위 시간 경과 시뮬레이션
        /// </summary>
        public void AdvanceDays(int days)
        {
            if (!_useFixedTime)
            {
                _fixedTimeUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _useFixedTime = true;
            }
            _fixedTimeUtc += days * 24 * 60 * 60;
        }

        /// <summary>
        /// 시간 경과 시뮬레이션
        /// </summary>
        public void AdvanceTime(TimeSpan duration)
        {
            if (!_useFixedTime)
            {
                _fixedTimeUtc = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _useFixedTime = true;
            }
            _fixedTimeUtc += (long)duration.TotalSeconds;
        }

        /// <summary>
        /// 실시간 모드로 전환
        /// </summary>
        public void UseRealTime()
        {
            _useFixedTime = false;
        }
    }
}
