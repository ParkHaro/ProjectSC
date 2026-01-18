using NUnit.Framework;
using Sc.Core;
using Sc.Data;
using Sc.Tests;

namespace Sc.Editor.Tests.Core
{
    /// <summary>
    /// TimeHelper 단위 테스트
    /// </summary>
    [TestFixture]
    public class TimeHelperTests
    {
        private MockTimeService _mockTimeService;

        [SetUp]
        public void SetUp()
        {
            _mockTimeService = new MockTimeService();
        }

        #region FormatRemainingTime Tests

        [Test]
        public void FormatRemainingTime_ZeroSeconds_ReturnsZero()
        {
            var result = TimeHelper.FormatRemainingTime(0);
            Assert.AreEqual("0초", result);
        }

        [Test]
        public void FormatRemainingTime_NegativeSeconds_ReturnsZero()
        {
            var result = TimeHelper.FormatRemainingTime(-100);
            Assert.AreEqual("0초", result);
        }

        [Test]
        public void FormatRemainingTime_SecondsOnly()
        {
            var result = TimeHelper.FormatRemainingTime(45);
            Assert.AreEqual("45초", result);
        }

        [Test]
        public void FormatRemainingTime_MinutesAndSeconds()
        {
            var result = TimeHelper.FormatRemainingTime(150); // 2분 30초
            Assert.AreEqual("2분 30초", result);
        }

        [Test]
        public void FormatRemainingTime_MinutesOnly()
        {
            var result = TimeHelper.FormatRemainingTime(120); // 2분
            Assert.AreEqual("2분", result);
        }

        [Test]
        public void FormatRemainingTime_HoursAndMinutes()
        {
            var result = TimeHelper.FormatRemainingTime(9000); // 2시간 30분
            Assert.AreEqual("2시간 30분", result);
        }

        [Test]
        public void FormatRemainingTime_HoursOnly()
        {
            var result = TimeHelper.FormatRemainingTime(7200); // 2시간
            Assert.AreEqual("2시간", result);
        }

        [Test]
        public void FormatRemainingTime_DaysAndHours()
        {
            var result = TimeHelper.FormatRemainingTime(104400); // 1일 5시간
            Assert.AreEqual("1일 5시간", result);
        }

        [Test]
        public void FormatRemainingTime_DaysOnly()
        {
            var result = TimeHelper.FormatRemainingTime(172800); // 2일
            Assert.AreEqual("2일", result);
        }

        #endregion

        #region FormatRemainingTimeShort Tests

        [Test]
        public void FormatRemainingTimeShort_ZeroSeconds()
        {
            var result = TimeHelper.FormatRemainingTimeShort(0);
            Assert.AreEqual("00:00:00", result);
        }

        [Test]
        public void FormatRemainingTimeShort_WithSeconds()
        {
            var result = TimeHelper.FormatRemainingTimeShort(45);
            Assert.AreEqual("00:00:45", result);
        }

        [Test]
        public void FormatRemainingTimeShort_WithMinutes()
        {
            var result = TimeHelper.FormatRemainingTimeShort(150);
            Assert.AreEqual("00:02:30", result);
        }

        [Test]
        public void FormatRemainingTimeShort_WithHours()
        {
            var result = TimeHelper.FormatRemainingTimeShort(9000);
            Assert.AreEqual("02:30:00", result);
        }

        [Test]
        public void FormatRemainingTimeShort_Over99Hours_ShowsDays()
        {
            var result = TimeHelper.FormatRemainingTimeShort(400000); // 약 4.6일
            Assert.AreEqual("4일", result);
        }

        #endregion

        #region ToLocalTimeString Tests

        [Test]
        public void ToLocalTimeString_ZeroTimestamp_ReturnsEmpty()
        {
            var result = TimeHelper.ToLocalTimeString(0);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ToLocalTimeString_ValidTimestamp_ReturnsFormatted()
        {
            // 2026-01-19 12:00:00 UTC
            var timestamp = 1768996800L;
            var result = TimeHelper.ToLocalTimeString(timestamp, "yyyy-MM-dd");

            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.StartsWith("2026-01"));
        }

        [Test]
        public void ToLocalTimeString_CustomFormat()
        {
            var timestamp = 1768996800L;
            var result = TimeHelper.ToLocalTimeString(timestamp, "HH:mm");

            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Contains(":"));
        }

        #endregion

        #region FormatRelativeTime Tests

        [Test]
        public void FormatRelativeTime_JustNow()
        {
            var now = _mockTimeService.ServerTimeUtc;

            var result = TimeHelper.FormatRelativeTime(now, _mockTimeService);

            Assert.AreEqual("방금 전", result);
        }

        [Test]
        public void FormatRelativeTime_MinutesAgo()
        {
            var fiveMinutesAgo = _mockTimeService.ServerTimeUtc - 300;

            var result = TimeHelper.FormatRelativeTime(fiveMinutesAgo, _mockTimeService);

            Assert.AreEqual("5분 전", result);
        }

        [Test]
        public void FormatRelativeTime_HoursAgo()
        {
            var threeHoursAgo = _mockTimeService.ServerTimeUtc - 10800;

            var result = TimeHelper.FormatRelativeTime(threeHoursAgo, _mockTimeService);

            Assert.AreEqual("3시간 전", result);
        }

        [Test]
        public void FormatRelativeTime_DaysAgo()
        {
            var twoDaysAgo = _mockTimeService.ServerTimeUtc - 172800;

            var result = TimeHelper.FormatRelativeTime(twoDaysAgo, _mockTimeService);

            Assert.AreEqual("2일 전", result);
        }

        [Test]
        public void FormatRelativeTime_OverWeek_ShowsDate()
        {
            var twoWeeksAgo = _mockTimeService.ServerTimeUtc - (14 * 86400);

            var result = TimeHelper.FormatRelativeTime(twoWeeksAgo, _mockTimeService);

            Assert.IsTrue(result.Contains("-")); // 날짜 형식
        }

        #endregion

        #region FormatTimeUntilReset Tests

        [Test]
        public void FormatTimeUntilReset_None_ReturnsEmpty()
        {
            var result = TimeHelper.FormatTimeUntilReset(LimitType.None, _mockTimeService);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void FormatTimeUntilReset_Daily_ReturnsFormattedTime()
        {
            var result = TimeHelper.FormatTimeUntilReset(LimitType.Daily, _mockTimeService);

            Assert.IsNotEmpty(result);
            // 최대 24시간 미만
        }

        [Test]
        public void FormatTimeUntilReset_Weekly_ReturnsFormattedTime()
        {
            var result = TimeHelper.FormatTimeUntilReset(LimitType.Weekly, _mockTimeService);

            Assert.IsNotEmpty(result);
            // 최대 7일 미만
        }

        #endregion

        #region GetLimitTypeDisplayName Tests

        [Test]
        public void GetLimitTypeDisplayName_None()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.None);
            Assert.AreEqual("무제한", result);
        }

        [Test]
        public void GetLimitTypeDisplayName_Daily()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.Daily);
            Assert.AreEqual("일일", result);
        }

        [Test]
        public void GetLimitTypeDisplayName_Weekly()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.Weekly);
            Assert.AreEqual("주간", result);
        }

        [Test]
        public void GetLimitTypeDisplayName_Monthly()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.Monthly);
            Assert.AreEqual("월간", result);
        }

        [Test]
        public void GetLimitTypeDisplayName_Permanent()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.Permanent);
            Assert.AreEqual("1회", result);
        }

        [Test]
        public void GetLimitTypeDisplayName_EventPeriod()
        {
            var result = TimeHelper.GetLimitTypeDisplayName(LimitType.EventPeriod);
            Assert.AreEqual("이벤트", result);
        }

        #endregion
    }
}
