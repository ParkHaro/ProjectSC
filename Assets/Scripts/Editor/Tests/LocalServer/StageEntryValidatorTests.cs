using System;
using NUnit.Framework;
using Sc.Data;
using Sc.LocalServer;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// StageEntryValidator 단위 테스트.
    /// 스테이지 입장 제한 검증 로직 테스트.
    /// </summary>
    [TestFixture]
    public class StageEntryValidatorTests
    {
        private StageEntryValidator _validator;
        private ServerTimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = new ServerTimeService();
            _validator = new StageEntryValidator(_timeService);
        }

        #region CanEnter Tests

        [Test]
        public void CanEnter_ReturnsTrue_WhenNoLimit()
        {
            var result = _validator.CanEnter(LimitType.None, 0, null, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void CanEnter_ReturnsTrue_WhenFirstEntry()
        {
            var result = _validator.CanEnter(LimitType.Daily, 3, null, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(3));
        }

        [Test]
        public void CanEnter_ReturnsTrue_WhenUnderLimit()
        {
            var record = CreateRecord("stage_1", 1, 0, long.MaxValue); // 1회 입장함

            var result = _validator.CanEnter(LimitType.Daily, 3, record, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(2));
        }

        [Test]
        public void CanEnter_ReturnsFalse_WhenAtLimit()
        {
            var record = CreateRecord("stage_1", 3, 0, long.MaxValue); // 3회 입장함

            var result = _validator.CanEnter(LimitType.Daily, 3, record, out int remaining);

            Assert.That(result, Is.False);
            Assert.That(remaining, Is.EqualTo(0));
        }

        [Test]
        public void CanEnter_ReturnsFalse_WhenOverLimit()
        {
            var record = CreateRecord("stage_1", 5, 0, long.MaxValue); // 5회 입장 (3회 제한 초과)

            var result = _validator.CanEnter(LimitType.Daily, 3, record, out int remaining);

            Assert.That(result, Is.False);
            Assert.That(remaining, Is.EqualTo(0));
        }

        [Test]
        public void CanEnter_ReturnsTrue_WhenNeedsReset()
        {
            // ResetTime이 과거 시간 (리셋 필요)
            var record = CreateRecord("stage_1", 3, 0, _timeService.ServerTimeUtc - 1000);

            var result = _validator.CanEnter(LimitType.Daily, 3, record, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(3));
        }

        [Test]
        public void CanEnter_ReturnsFalse_WhenPermanentLimit()
        {
            var record = CreateRecord("stage_1", 1, 0, 0); // 영구 제한, ResetTime = 0

            var result = _validator.CanEnter(LimitType.Permanent, 1, record, out int remaining);

            Assert.That(result, Is.False);
            Assert.That(remaining, Is.EqualTo(0));
        }

        #endregion

        #region CalculateResetTime Tests

        [Test]
        public void CalculateResetTime_ReturnsZero_ForNoLimit()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.None);

            Assert.That(resetTime, Is.EqualTo(0));
        }

        [Test]
        public void CalculateResetTime_ReturnsZero_ForPermanent()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Permanent);

            Assert.That(resetTime, Is.EqualTo(0));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextMidnight_ForDaily()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Daily);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 24시간 이내여야 함
            Assert.That(resetTime, Is.LessThanOrEqualTo(_timeService.ServerTimeUtc + 86400));

            // 리셋 시간은 자정(00:00 UTC)이어야 함
            var resetDateTime = DateTimeOffset.FromUnixTimeSeconds(resetTime).UtcDateTime;
            Assert.That(resetDateTime.Hour, Is.EqualTo(0));
            Assert.That(resetDateTime.Minute, Is.EqualTo(0));
            Assert.That(resetDateTime.Second, Is.EqualTo(0));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextMonday_ForWeekly()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Weekly);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 7일 이내여야 함
            Assert.That(resetTime, Is.LessThanOrEqualTo(_timeService.ServerTimeUtc + 7 * 86400));

            // 리셋 시간은 월요일이어야 함
            var resetDateTime = DateTimeOffset.FromUnixTimeSeconds(resetTime).UtcDateTime;
            Assert.That(resetDateTime.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextFirstOfMonth_ForMonthly()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Monthly);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 다음 달 1일이어야 함
            var resetDateTime = DateTimeOffset.FromUnixTimeSeconds(resetTime).UtcDateTime;
            Assert.That(resetDateTime.Day, Is.EqualTo(1));
        }

        #endregion

        #region UpdateEntryRecord Tests

        [Test]
        public void UpdateEntryRecord_CreatesNewRecord_WhenNoExisting()
        {
            var record = _validator.UpdateEntryRecord("stage_1", LimitType.Daily, null);

            Assert.That(record.StageId, Is.EqualTo("stage_1"));
            Assert.That(record.EntryCount, Is.EqualTo(1));
            Assert.That(record.LastEntryTime, Is.GreaterThan(0));
            Assert.That(record.ResetTime, Is.GreaterThan(_timeService.ServerTimeUtc));
        }

        [Test]
        public void UpdateEntryRecord_IncrementsEntryCount()
        {
            var existingRecord = CreateRecord(
                "stage_1",
                2,
                _timeService.ServerTimeUtc - 1000,
                _timeService.ServerTimeUtc + 100000);

            var record = _validator.UpdateEntryRecord("stage_1", LimitType.Weekly, existingRecord);

            Assert.That(record.EntryCount, Is.EqualTo(3));
        }

        [Test]
        public void UpdateEntryRecord_ResetsCount_WhenNeedsReset()
        {
            // 리셋 필요 (ResetTime이 과거)
            var existingRecord = CreateRecord(
                "stage_1",
                5,
                _timeService.ServerTimeUtc - 2000,
                _timeService.ServerTimeUtc - 1000);

            var record = _validator.UpdateEntryRecord("stage_1", LimitType.Daily, existingRecord);

            // 리셋 후 1회 입장으로 시작
            Assert.That(record.EntryCount, Is.EqualTo(1));
            Assert.That(record.ResetTime, Is.GreaterThan(_timeService.ServerTimeUtc));
        }

        [Test]
        public void UpdateEntryRecord_KeepsZeroReset_ForPermanent()
        {
            var record = _validator.UpdateEntryRecord("stage_1", LimitType.Permanent, null);

            Assert.That(record.ResetTime, Is.EqualTo(0));
        }

        [Test]
        public void UpdateEntryRecord_KeepsZeroReset_ForNoLimit()
        {
            var record = _validator.UpdateEntryRecord("stage_1", LimitType.None, null);

            Assert.That(record.ResetTime, Is.EqualTo(0));
        }

        [Test]
        public void UpdateEntryRecord_UpdatesLastEntryTime()
        {
            var existingRecord = CreateRecord(
                "stage_1",
                1,
                _timeService.ServerTimeUtc - 10000,
                _timeService.ServerTimeUtc + 100000);

            var record = _validator.UpdateEntryRecord("stage_1", LimitType.Daily, existingRecord);

            Assert.That(record.LastEntryTime, Is.GreaterThan(existingRecord.LastEntryTime));
        }

        #endregion

        #region IsAvailableToday Tests

        [Test]
        public void IsAvailableToday_ReturnsTrue_WhenNoDayRestriction()
        {
            var result = _validator.IsAvailableToday(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAvailableToday_ReturnsTrue_WhenEmptyDayRestriction()
        {
            var result = _validator.IsAvailableToday(Array.Empty<DayOfWeek>());

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAvailableToday_ReturnsTrue_WhenTodayIsIncluded()
        {
            var today = DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime.DayOfWeek;
            var availableDays = new[] { today };

            var result = _validator.IsAvailableToday(availableDays);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAvailableToday_ReturnsFalse_WhenTodayIsNotIncluded()
        {
            var today = DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime.DayOfWeek;
            // 오늘이 아닌 다른 요일들만 포함
            var availableDays = GetOtherDays(today);

            var result = _validator.IsAvailableToday(availableDays);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsAvailableToday_ReturnsTrue_WhenAllDaysAvailable()
        {
            var availableDays = new[]
            {
                DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday
            };

            var result = _validator.IsAvailableToday(availableDays);

            Assert.That(result, Is.True);
        }

        #endregion

        #region Helper Methods

        private StageEntryRecord CreateRecord(string stageId, int entryCount, long lastEntryTime, long resetTime)
        {
            return new StageEntryRecord(stageId, entryCount, lastEntryTime, resetTime);
        }

        private DayOfWeek[] GetOtherDays(DayOfWeek excludeDay)
        {
            var days = new System.Collections.Generic.List<DayOfWeek>();
            for (int i = 0; i < 7; i++)
            {
                var day = (DayOfWeek)i;
                if (day != excludeDay)
                    days.Add(day);
            }

            return days.ToArray();
        }

        #endregion
    }
}