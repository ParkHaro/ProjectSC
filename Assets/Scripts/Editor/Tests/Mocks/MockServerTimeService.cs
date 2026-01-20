using System;
using Sc.Data;

namespace Sc.Editor.Tests.Mocks
{
    /// <summary>
    /// 테스트용 ServerTimeService Mock.
    /// 고정된 시간을 반환하거나 시간 조작이 가능.
    /// LocalServer 테스트에서 사용.
    /// </summary>
    public class MockServerTimeService
    {
        private long _fixedTimeUtc;
        private bool _useFixedTime;

        public MockServerTimeService()
        {
            _useFixedTime = false;
        }

        public MockServerTimeService(long fixedTimeUtc)
        {
            _fixedTimeUtc = fixedTimeUtc;
            _useFixedTime = true;
        }

        public MockServerTimeService(DateTime dateTime)
        {
            _fixedTimeUtc = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
            _useFixedTime = true;
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
        /// 일 단위 시간 경과
        /// </summary>
        public void AdvanceDays(int days)
        {
            AdvanceTime(TimeSpan.FromDays(days));
        }

        public long ServerTimeUtc => _useFixedTime
            ? _fixedTimeUtc
            : DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public DateTime ServerDateTime => DateTimeOffset
            .FromUnixTimeSeconds(ServerTimeUtc)
            .UtcDateTime;

        public long GetNextResetTime(LimitType limitType)
        {
            var now = ServerDateTime;

            return limitType switch
            {
                LimitType.None or LimitType.Permanent or LimitType.EventPeriod => 0,
                LimitType.Daily => GetNextDayReset(now),
                LimitType.Weekly => GetNextWeekReset(now),
                LimitType.Monthly => GetNextMonthReset(now),
                _ => 0
            };
        }

        public bool HasResetOccurred(long lastTimestamp, LimitType limitType)
        {
            if (lastTimestamp <= 0) return false;

            return limitType switch
            {
                LimitType.None or LimitType.Permanent => false,
                LimitType.EventPeriod => false,
                LimitType.Daily or LimitType.Weekly or LimitType.Monthly =>
                    ServerTimeUtc >= GetResetTimeAfter(lastTimestamp, limitType),
                _ => false
            };
        }

        public bool IsWithinPeriod(long startTime, long endTime)
        {
            var now = ServerTimeUtc;
            return now >= startTime && now < endTime;
        }

        private long GetNextDayReset(DateTime now)
        {
            var nextDay = now.Date.AddDays(1);
            return new DateTimeOffset(nextDay, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        private long GetNextWeekReset(DateTime now)
        {
            var daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
            if (daysUntilMonday == 0) daysUntilMonday = 7;
            var nextMonday = now.Date.AddDays(daysUntilMonday);
            return new DateTimeOffset(nextMonday, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        private long GetNextMonthReset(DateTime now)
        {
            var nextMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
            return new DateTimeOffset(nextMonth, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        private long GetResetTimeAfter(long timestamp, LimitType limitType)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;

            return limitType switch
            {
                LimitType.Daily => GetNextDayReset(dateTime),
                LimitType.Weekly => GetNextWeekReset(dateTime),
                LimitType.Monthly => GetNextMonthReset(dateTime),
                _ => 0
            };
        }
    }
}
