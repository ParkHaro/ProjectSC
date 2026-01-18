using System;
using Sc.Core;
using Sc.Data;

namespace Sc.Tests
{
    /// <summary>
    /// 테스트용 시간 서비스 Mock.
    /// 고정된 시간을 반환하거나 시간 조작이 가능.
    /// </summary>
    public class MockTimeService : ITimeService
    {
        private long _fixedTimeUtc;
        private bool _useFixedTime;
        private long _offset;

        /// <summary>
        /// 기본 생성자 (현재 시간 사용)
        /// </summary>
        public MockTimeService()
        {
            _useFixedTime = false;
            _offset = 0;
        }

        /// <summary>
        /// 고정 시간 생성자
        /// </summary>
        public MockTimeService(long fixedTimeUtc)
        {
            _fixedTimeUtc = fixedTimeUtc;
            _useFixedTime = true;
            _offset = 0;
        }

        /// <summary>
        /// 고정 시간 설정
        /// </summary>
        public void SetFixedTime(long utcTimestamp)
        {
            _fixedTimeUtc = utcTimestamp;
            _useFixedTime = true;
        }

        /// <summary>
        /// 고정 시간 설정 (DateTime)
        /// </summary>
        public void SetFixedTime(DateTime dateTime)
        {
            _fixedTimeUtc = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
            _useFixedTime = true;
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

        public long ServerTimeUtc => _useFixedTime
            ? _fixedTimeUtc
            : DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _offset;

        public DateTime ServerDateTime => DateTimeOffset
            .FromUnixTimeSeconds(ServerTimeUtc)
            .UtcDateTime;

        public long TimeOffset => _offset;

        public void SyncServerTime(long serverTimestamp)
        {
            if (_useFixedTime)
            {
                // 고정 시간 모드에서는 무시
                return;
            }
            _offset = serverTimestamp - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public long GetNextResetTime(LimitType limitType)
        {
            var now = ServerDateTime;

            switch (limitType)
            {
                case LimitType.None:
                case LimitType.Permanent:
                case LimitType.EventPeriod:
                    return 0;

                case LimitType.Daily:
                    var nextDay = now.Date.AddDays(1);
                    return new DateTimeOffset(nextDay, TimeSpan.Zero).ToUnixTimeSeconds();

                case LimitType.Weekly:
                    var daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
                    if (daysUntilMonday == 0) daysUntilMonday = 7;
                    var nextMonday = now.Date.AddDays(daysUntilMonday);
                    return new DateTimeOffset(nextMonday, TimeSpan.Zero).ToUnixTimeSeconds();

                case LimitType.Monthly:
                    var nextMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
                    return new DateTimeOffset(nextMonth, TimeSpan.Zero).ToUnixTimeSeconds();

                default:
                    return 0;
            }
        }

        public bool HasResetOccurred(long lastTimestamp, LimitType limitType)
        {
            if (lastTimestamp <= 0) return false;

            switch (limitType)
            {
                case LimitType.None:
                case LimitType.Permanent:
                case LimitType.EventPeriod:
                    return false;

                case LimitType.Daily:
                case LimitType.Weekly:
                case LimitType.Monthly:
                    var lastResetTime = GetResetTimeAfter(lastTimestamp, limitType);
                    return ServerTimeUtc >= lastResetTime;

                default:
                    return false;
            }
        }

        public bool IsWithinPeriod(long startTime, long endTime)
        {
            var now = ServerTimeUtc;
            return now >= startTime && now < endTime;
        }

        public long GetRemainingSeconds(long targetTime)
        {
            var remaining = targetTime - ServerTimeUtc;
            return remaining > 0 ? remaining : 0;
        }

        private long GetResetTimeAfter(long timestamp, LimitType limitType)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;

            switch (limitType)
            {
                case LimitType.Daily:
                    var nextDay = dateTime.Date.AddDays(1);
                    return new DateTimeOffset(nextDay, TimeSpan.Zero).ToUnixTimeSeconds();

                case LimitType.Weekly:
                    var daysUntilMonday = ((int)DayOfWeek.Monday - (int)dateTime.DayOfWeek + 7) % 7;
                    if (daysUntilMonday == 0) daysUntilMonday = 7;
                    var nextMonday = dateTime.Date.AddDays(daysUntilMonday);
                    return new DateTimeOffset(nextMonday, TimeSpan.Zero).ToUnixTimeSeconds();

                case LimitType.Monthly:
                    var nextMonth = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1);
                    return new DateTimeOffset(nextMonth, TimeSpan.Zero).ToUnixTimeSeconds();

                default:
                    return 0;
            }
        }
    }
}
