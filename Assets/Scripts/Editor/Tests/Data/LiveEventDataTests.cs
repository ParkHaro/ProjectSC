using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;
using UnityEngine;

namespace Sc.Editor.Tests.Data
{
    /// <summary>
    /// LiveEventData 단위 테스트.
    /// 시간 헬퍼 및 서브 컨텐츠 조회 검증.
    /// </summary>
    [TestFixture]
    public class LiveEventDataTests
    {
        private LiveEventData _eventData;
        private DateTime _baseTime;

        [SetUp]
        public void SetUp()
        {
            _eventData = ScriptableObject.CreateInstance<LiveEventData>();
            _baseTime = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);
        }

        [TearDown]
        public void TearDown()
        {
            if (_eventData != null)
            {
                UnityEngine.Object.DestroyImmediate(_eventData);
            }
        }

        private void InitializeEventData(
            DateTime startTime,
            DateTime endTime,
            bool hasEventCurrency = false,
            int gracePeriodDays = 7,
            List<EventSubContent> subContents = null)
        {
            _eventData.Initialize(
                id: "test_event",
                eventType: LiveEventType.Limited,
                nameKey: "event_test",
                descriptionKey: "event_test_desc",
                bannerImage: "banner_test",
                startTime: startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                endTime: endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                subContents: subContents ?? new List<EventSubContent>(),
                hasEventCurrency: hasEventCurrency,
                currencyPolicy: hasEventCurrency ? new EventCurrencyPolicy
                {
                    CurrencyId = "token_test",
                    GracePeriodDays = gracePeriodDays,
                    ConvertToCurrencyId = "gold",
                    ConversionRate = 10f
                } : default,
                displayOrder: 0,
                showCountdown: true
            );
        }

        #region IsActive Tests

        [Test]
        public void IsActive_ReturnsTrue_WhenWithinPeriod()
        {
            var startTime = _baseTime.AddDays(-3);
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.IsActive(_baseTime);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsActive_ReturnsTrue_WhenExactlyAtStart()
        {
            var startTime = _baseTime;
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.IsActive(_baseTime);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsActive_ReturnsFalse_WhenExactlyAtEnd()
        {
            var startTime = _baseTime.AddDays(-7);
            var endTime = _baseTime;
            InitializeEventData(startTime, endTime);

            var result = _eventData.IsActive(_baseTime);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsActive_ReturnsFalse_WhenBeforeStart()
        {
            var startTime = _baseTime.AddDays(1);
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.IsActive(_baseTime);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsActive_ReturnsFalse_WhenAfterEnd()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.IsActive(_baseTime);

            Assert.That(result, Is.False);
        }

        #endregion

        #region IsInGracePeriod Tests

        [Test]
        public void IsInGracePeriod_ReturnsFalse_WhenNoEventCurrency()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-1);
            InitializeEventData(startTime, endTime, hasEventCurrency: false);

            var result = _eventData.IsInGracePeriod(_baseTime);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsInGracePeriod_ReturnsTrue_WhenInGracePeriod()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-1);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.IsInGracePeriod(_baseTime);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsInGracePeriod_ReturnsFalse_WhenStillActive()
        {
            var startTime = _baseTime.AddDays(-3);
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.IsInGracePeriod(_baseTime);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsInGracePeriod_ReturnsFalse_WhenGracePeriodExpired()
        {
            var startTime = _baseTime.AddDays(-30);
            var endTime = _baseTime.AddDays(-14);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.IsInGracePeriod(_baseTime);

            Assert.That(result, Is.False);
        }

        #endregion

        #region IsGracePeriodExpired Tests

        [Test]
        public void IsGracePeriodExpired_ReturnsFalse_WhenNoEventCurrency()
        {
            var startTime = _baseTime.AddDays(-30);
            var endTime = _baseTime.AddDays(-14);
            InitializeEventData(startTime, endTime, hasEventCurrency: false);

            var result = _eventData.IsGracePeriodExpired(_baseTime);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsGracePeriodExpired_ReturnsTrue_WhenExpired()
        {
            var startTime = _baseTime.AddDays(-30);
            var endTime = _baseTime.AddDays(-14);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.IsGracePeriodExpired(_baseTime);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsGracePeriodExpired_ReturnsFalse_WhenStillInGracePeriod()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-1);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.IsGracePeriodExpired(_baseTime);

            Assert.That(result, Is.False);
        }

        #endregion

        #region GetRemainingDays Tests

        [Test]
        public void GetRemainingDays_ReturnsZero_WhenExpired()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.GetRemainingDays(_baseTime);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetRemainingDays_ReturnsCeiling()
        {
            var startTime = _baseTime.AddDays(-3);
            var endTime = _baseTime.AddHours(12);
            InitializeEventData(startTime, endTime);

            var result = _eventData.GetRemainingDays(_baseTime);

            Assert.That(result, Is.EqualTo(1)); // Ceiling(0.5) = 1
        }

        [Test]
        public void GetRemainingDays_ReturnsExactDays()
        {
            var startTime = _baseTime.AddDays(-3);
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime);

            var result = _eventData.GetRemainingDays(_baseTime);

            Assert.That(result, Is.EqualTo(7));
        }

        #endregion

        #region GetGracePeriodRemainingDays Tests

        [Test]
        public void GetGracePeriodRemainingDays_ReturnsZero_WhenNotInGracePeriod()
        {
            var startTime = _baseTime.AddDays(-3);
            var endTime = _baseTime.AddDays(7);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.GetGracePeriodRemainingDays(_baseTime);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetGracePeriodRemainingDays_ReturnsCorrectDays()
        {
            var startTime = _baseTime.AddDays(-14);
            var endTime = _baseTime.AddDays(-1);
            InitializeEventData(startTime, endTime, hasEventCurrency: true, gracePeriodDays: 7);

            var result = _eventData.GetGracePeriodRemainingDays(_baseTime);

            Assert.That(result, Is.EqualTo(6)); // Ceiling(endTime + 7 - baseTime) = Ceiling(6) = 6
        }

        #endregion

        #region SubContent Tests

        [Test]
        public void GetSubContent_ReturnsNull_WhenTypeNotFound()
        {
            var subContents = new List<EventSubContent>
            {
                new EventSubContent { Type = EventSubContentType.Mission, ContentId = "mission_group_1" }
            };
            InitializeEventData(_baseTime.AddDays(-1), _baseTime.AddDays(7), subContents: subContents);

            var result = _eventData.GetSubContent(EventSubContentType.Stage);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetSubContent_ReturnsSubContent_WhenTypeFound()
        {
            var subContents = new List<EventSubContent>
            {
                new EventSubContent { Type = EventSubContentType.Mission, ContentId = "mission_group_1" },
                new EventSubContent { Type = EventSubContentType.Stage, ContentId = "stage_group_1" }
            };
            InitializeEventData(_baseTime.AddDays(-1), _baseTime.AddDays(7), subContents: subContents);

            var result = _eventData.GetSubContent(EventSubContentType.Stage);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.ContentId, Is.EqualTo("stage_group_1"));
        }

        [Test]
        public void HasSubContent_ReturnsTrue_WhenTypeExists()
        {
            var subContents = new List<EventSubContent>
            {
                new EventSubContent { Type = EventSubContentType.Mission, ContentId = "mission_group_1" }
            };
            InitializeEventData(_baseTime.AddDays(-1), _baseTime.AddDays(7), subContents: subContents);

            var result = _eventData.HasSubContent(EventSubContentType.Mission);

            Assert.That(result, Is.True);
        }

        [Test]
        public void HasSubContent_ReturnsFalse_WhenTypeNotExists()
        {
            var subContents = new List<EventSubContent>
            {
                new EventSubContent { Type = EventSubContentType.Mission, ContentId = "mission_group_1" }
            };
            InitializeEventData(_baseTime.AddDays(-1), _baseTime.AddDays(7), subContents: subContents);

            var result = _eventData.HasSubContent(EventSubContentType.Shop);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetUnlockedSubContents_ReturnsOnlyUnlocked()
        {
            var subContents = new List<EventSubContent>
            {
                new EventSubContent { Type = EventSubContentType.Mission, ContentId = "m1", IsUnlocked = true, TabOrder = 2 },
                new EventSubContent { Type = EventSubContentType.Stage, ContentId = "s1", IsUnlocked = false, TabOrder = 1 },
                new EventSubContent { Type = EventSubContentType.Shop, ContentId = "sh1", IsUnlocked = true, TabOrder = 0 }
            };
            InitializeEventData(_baseTime.AddDays(-1), _baseTime.AddDays(7), subContents: subContents);

            var result = new List<EventSubContent>(_eventData.GetUnlockedSubContents());

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Type, Is.EqualTo(EventSubContentType.Shop)); // TabOrder 0
            Assert.That(result[1].Type, Is.EqualTo(EventSubContentType.Mission)); // TabOrder 2
        }

        #endregion
    }
}
