using System;
using NUnit.Framework;
using Sc.Core;
using Sc.Data;

namespace Sc.Editor.Tests.Core
{
    /// <summary>
    /// TimeService 단위 테스트
    /// </summary>
    [TestFixture]
    public class TimeServiceTests
    {
        private TimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = new TimeService();
        }

        #region ServerTimeUtc Tests

        [Test]
        public void ServerTimeUtc_ReturnsCurrentUtcTime()
        {
            var before = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var serverTime = _timeService.ServerTimeUtc;
            var after = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            Assert.GreaterOrEqual(serverTime, before);
            Assert.LessOrEqual(serverTime, after);
        }

        [Test]
        public void ServerDateTime_ReturnsCurrentUtcDateTime()
        {
            var before = DateTime.UtcNow;
            var serverDateTime = _timeService.ServerDateTime;
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(serverDateTime, before.AddSeconds(-1));
            Assert.LessOrEqual(serverDateTime, after.AddSeconds(1));
        }

        #endregion

        #region SyncServerTime Tests

        [Test]
        public void SyncServerTime_UpdatesOffset()
        {
            var fakeServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600; // 1시간 후

            _timeService.SyncServerTime(fakeServerTime);

            Assert.AreEqual(3600, _timeService.TimeOffset, 1); // 1초 오차 허용
        }

        [Test]
        public void SyncServerTime_AffectsServerTimeUtc()
        {
            var fakeServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600;

            _timeService.SyncServerTime(fakeServerTime);

            var diff = Math.Abs(_timeService.ServerTimeUtc - fakeServerTime);
            Assert.Less(diff, 2); // 2초 이내
        }

        #endregion

        #region GetNextResetTime Tests

        [Test]
        public void GetNextResetTime_None_ReturnsZero()
        {
            var result = _timeService.GetNextResetTime(LimitType.None);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetNextResetTime_Permanent_ReturnsZero()
        {
            var result = _timeService.GetNextResetTime(LimitType.Permanent);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetNextResetTime_EventPeriod_ReturnsZero()
        {
            var result = _timeService.GetNextResetTime(LimitType.EventPeriod);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetNextResetTime_Daily_ReturnsTomorrowMidnight()
        {
            var result = _timeService.GetNextResetTime(LimitType.Daily);
            var nextReset = DateTimeOffset.FromUnixTimeSeconds(result).UtcDateTime;

            Assert.AreEqual(0, nextReset.Hour);
            Assert.AreEqual(0, nextReset.Minute);
            Assert.AreEqual(0, nextReset.Second);
            Assert.Greater(result, _timeService.ServerTimeUtc);
        }

        [Test]
        public void GetNextResetTime_Weekly_ReturnsNextMonday()
        {
            var result = _timeService.GetNextResetTime(LimitType.Weekly);
            var nextReset = DateTimeOffset.FromUnixTimeSeconds(result).UtcDateTime;

            Assert.AreEqual(DayOfWeek.Monday, nextReset.DayOfWeek);
            Assert.AreEqual(0, nextReset.Hour);
            Assert.Greater(result, _timeService.ServerTimeUtc);
        }

        [Test]
        public void GetNextResetTime_Monthly_ReturnsFirstDayOfNextMonth()
        {
            var result = _timeService.GetNextResetTime(LimitType.Monthly);
            var nextReset = DateTimeOffset.FromUnixTimeSeconds(result).UtcDateTime;

            Assert.AreEqual(1, nextReset.Day);
            Assert.AreEqual(0, nextReset.Hour);
            Assert.Greater(result, _timeService.ServerTimeUtc);
        }

        #endregion

        #region HasResetOccurred Tests

        [Test]
        public void HasResetOccurred_ZeroTimestamp_ReturnsFalse()
        {
            var result = _timeService.HasResetOccurred(0, LimitType.Daily);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasResetOccurred_None_ReturnsFalse()
        {
            var yesterday = _timeService.ServerTimeUtc - 86400;
            var result = _timeService.HasResetOccurred(yesterday, LimitType.None);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasResetOccurred_Permanent_ReturnsFalse()
        {
            var yesterday = _timeService.ServerTimeUtc - 86400;
            var result = _timeService.HasResetOccurred(yesterday, LimitType.Permanent);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasResetOccurred_Daily_YesterdayTimestamp_ReturnsTrue()
        {
            // 어제 오전 10시 (리셋 발생했어야 함)
            var yesterday = _timeService.ServerTimeUtc - 86400;
            var result = _timeService.HasResetOccurred(yesterday, LimitType.Daily);
            Assert.IsTrue(result);
        }

        [Test]
        public void HasResetOccurred_Daily_RecentTimestamp_ReturnsFalse()
        {
            // 1분 전 (리셋 발생 안 함)
            var recentTime = _timeService.ServerTimeUtc - 60;
            var result = _timeService.HasResetOccurred(recentTime, LimitType.Daily);
            Assert.IsFalse(result);
        }

        [Test]
        public void HasResetOccurred_Weekly_LastWeekTimestamp_ReturnsTrue()
        {
            // 8일 전 (주간 리셋 발생했어야 함)
            var lastWeek = _timeService.ServerTimeUtc - (8 * 86400);
            var result = _timeService.HasResetOccurred(lastWeek, LimitType.Weekly);
            Assert.IsTrue(result);
        }

        [Test]
        public void HasResetOccurred_Monthly_LastMonthTimestamp_ReturnsTrue()
        {
            // 35일 전 (월간 리셋 발생했어야 함)
            var lastMonth = _timeService.ServerTimeUtc - (35 * 86400);
            var result = _timeService.HasResetOccurred(lastMonth, LimitType.Monthly);
            Assert.IsTrue(result);
        }

        #endregion

        #region IsWithinPeriod Tests

        [Test]
        public void IsWithinPeriod_CurrentTimeInRange_ReturnsTrue()
        {
            var start = _timeService.ServerTimeUtc - 3600;
            var end = _timeService.ServerTimeUtc + 3600;

            var result = _timeService.IsWithinPeriod(start, end);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsWithinPeriod_CurrentTimeBeforeStart_ReturnsFalse()
        {
            var start = _timeService.ServerTimeUtc + 3600;
            var end = _timeService.ServerTimeUtc + 7200;

            var result = _timeService.IsWithinPeriod(start, end);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsWithinPeriod_CurrentTimeAfterEnd_ReturnsFalse()
        {
            var start = _timeService.ServerTimeUtc - 7200;
            var end = _timeService.ServerTimeUtc - 3600;

            var result = _timeService.IsWithinPeriod(start, end);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsWithinPeriod_AtStartTime_ReturnsTrue()
        {
            var start = _timeService.ServerTimeUtc;
            var end = _timeService.ServerTimeUtc + 3600;

            var result = _timeService.IsWithinPeriod(start, end);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsWithinPeriod_AtEndTime_ReturnsFalse()
        {
            var start = _timeService.ServerTimeUtc - 3600;
            var end = _timeService.ServerTimeUtc;

            var result = _timeService.IsWithinPeriod(start, end);

            Assert.IsFalse(result);
        }

        #endregion

        #region GetRemainingSeconds Tests

        [Test]
        public void GetRemainingSeconds_FutureTime_ReturnsPositive()
        {
            var future = _timeService.ServerTimeUtc + 3600;

            var result = _timeService.GetRemainingSeconds(future);

            Assert.AreEqual(3600, result, 1);
        }

        [Test]
        public void GetRemainingSeconds_PastTime_ReturnsZero()
        {
            var past = _timeService.ServerTimeUtc - 3600;

            var result = _timeService.GetRemainingSeconds(past);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetRemainingSeconds_CurrentTime_ReturnsZero()
        {
            var current = _timeService.ServerTimeUtc;

            var result = _timeService.GetRemainingSeconds(current);

            Assert.AreEqual(0, result);
        }

        #endregion
    }
}
