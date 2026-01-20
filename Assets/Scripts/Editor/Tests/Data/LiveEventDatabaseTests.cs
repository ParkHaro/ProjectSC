using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sc.Data;
using UnityEngine;

namespace Sc.Editor.Tests.Data
{
    /// <summary>
    /// LiveEventDatabase 단위 테스트.
    /// 이벤트 조회 로직 검증.
    /// </summary>
    [TestFixture]
    public class LiveEventDatabaseTests
    {
        private LiveEventDatabase _database;
        private List<LiveEventData> _testEvents;
        private DateTime _baseTime;

        [SetUp]
        public void SetUp()
        {
            _database = ScriptableObject.CreateInstance<LiveEventDatabase>();
            _testEvents = new List<LiveEventData>();
            _baseTime = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var evt in _testEvents)
            {
                if (evt != null)
                {
                    UnityEngine.Object.DestroyImmediate(evt);
                }
            }
            _testEvents.Clear();

            if (_database != null)
            {
                UnityEngine.Object.DestroyImmediate(_database);
            }
        }

        private LiveEventData CreateEventData(
            string id,
            DateTime startTime,
            DateTime endTime,
            bool hasEventCurrency = false,
            int gracePeriodDays = 7,
            int displayOrder = 0,
            LiveEventType eventType = LiveEventType.Limited)
        {
            var eventData = ScriptableObject.CreateInstance<LiveEventData>();
            eventData.Initialize(
                id: id,
                eventType: eventType,
                nameKey: $"event_{id}",
                descriptionKey: $"event_{id}_desc",
                bannerImage: $"banner_{id}",
                startTime: startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                endTime: endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                subContents: new List<EventSubContent>(),
                hasEventCurrency: hasEventCurrency,
                currencyPolicy: hasEventCurrency ? new EventCurrencyPolicy
                {
                    CurrencyId = $"token_{id}",
                    GracePeriodDays = gracePeriodDays,
                    ConvertToCurrencyId = "gold",
                    ConversionRate = 10f
                } : default,
                displayOrder: displayOrder,
                showCountdown: true
            );
            _testEvents.Add(eventData);
            _database.Add(eventData);
            return eventData;
        }

        #region GetById Tests

        [Test]
        public void GetById_ReturnsNull_WhenNotFound()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            var result = _database.GetById("nonexistent");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetById_ReturnsEvent_WhenFound()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("event_002", _baseTime.AddDays(-3), _baseTime.AddDays(7));

            var result = _database.GetById("event_002");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("event_002"));
        }

        #endregion

        #region GetActiveEvents Tests

        [Test]
        public void GetActiveEvents_ReturnsEmpty_WhenNoActiveEvents()
        {
            CreateEventData("event_001", _baseTime.AddDays(-14), _baseTime.AddDays(-7));
            CreateEventData("event_002", _baseTime.AddDays(7), _baseTime.AddDays(14));

            var result = _database.GetActiveEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetActiveEvents_ReturnsActiveEventsOnly()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("expired_001", _baseTime.AddDays(-14), _baseTime.AddDays(-7));
            CreateEventData("active_002", _baseTime.AddDays(-3), _baseTime.AddDays(3));
            CreateEventData("future_001", _baseTime.AddDays(7), _baseTime.AddDays(14));

            var result = _database.GetActiveEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(e => e.Id == "active_001"), Is.True);
            Assert.That(result.Any(e => e.Id == "active_002"), Is.True);
        }

        [Test]
        public void GetActiveEvents_OrdersByDisplayOrder()
        {
            CreateEventData("event_a", _baseTime.AddDays(-1), _baseTime.AddDays(7), displayOrder: 2);
            CreateEventData("event_b", _baseTime.AddDays(-1), _baseTime.AddDays(7), displayOrder: 0);
            CreateEventData("event_c", _baseTime.AddDays(-1), _baseTime.AddDays(7), displayOrder: 1);

            var result = _database.GetActiveEvents(_baseTime).ToList();

            Assert.That(result[0].Id, Is.EqualTo("event_b")); // displayOrder 0
            Assert.That(result[1].Id, Is.EqualTo("event_c")); // displayOrder 1
            Assert.That(result[2].Id, Is.EqualTo("event_a")); // displayOrder 2
        }

        #endregion

        #region GetGracePeriodEvents Tests

        [Test]
        public void GetGracePeriodEvents_ReturnsEmpty_WhenNoGracePeriodEvents()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: true);
            CreateEventData("expired_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14), hasEventCurrency: true);

            var result = _database.GetGracePeriodEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetGracePeriodEvents_ReturnsGracePeriodEventsOnly()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: true);
            CreateEventData("grace_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);
            CreateEventData("expired_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14), hasEventCurrency: true, gracePeriodDays: 7);
            CreateEventData("no_currency", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: false);

            var result = _database.GetGracePeriodEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo("grace_001"));
        }

        #endregion

        #region GetAvailableEvents Tests

        [Test]
        public void GetAvailableEvents_CombinesActiveAndGracePeriod()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), displayOrder: 0);
            CreateEventData("grace_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7, displayOrder: 1);

            var result = _database.GetAvailableEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAvailableEvents_ExcludesGracePeriod_WhenFlagFalse()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), displayOrder: 0);
            CreateEventData("grace_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7, displayOrder: 1);

            var result = _database.GetAvailableEvents(_baseTime, includeGracePeriod: false).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo("active_001"));
        }

        #endregion

        #region GetExpiredGracePeriodEvents Tests

        [Test]
        public void GetExpiredGracePeriodEvents_ReturnsEmpty_WhenNoExpiredEvents()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: true);
            CreateEventData("grace_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);

            var result = _database.GetExpiredGracePeriodEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetExpiredGracePeriodEvents_ReturnsExpiredEventsOnly()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: true);
            CreateEventData("grace_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);
            CreateEventData("expired_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14), hasEventCurrency: true, gracePeriodDays: 7);
            CreateEventData("expired_no_currency", _baseTime.AddDays(-30), _baseTime.AddDays(-14), hasEventCurrency: false);

            var result = _database.GetExpiredGracePeriodEvents(_baseTime).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo("expired_001"));
        }

        #endregion

        #region GetByType Tests

        [Test]
        public void GetByType_ReturnsEmpty_WhenNoMatchingType()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), eventType: LiveEventType.Limited);
            CreateEventData("event_002", _baseTime.AddDays(-1), _baseTime.AddDays(7), eventType: LiveEventType.Limited);

            var result = _database.GetByType(LiveEventType.Collab).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetByType_ReturnsMatchingEvents()
        {
            CreateEventData("limited_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), eventType: LiveEventType.Limited);
            CreateEventData("collab_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), eventType: LiveEventType.Collab);
            CreateEventData("limited_002", _baseTime.AddDays(-1), _baseTime.AddDays(7), eventType: LiveEventType.Limited);

            var result = _database.GetByType(LiveEventType.Limited).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(e => e.EventType == LiveEventType.Limited), Is.True);
        }

        #endregion

        #region Count Tests

        [Test]
        public void Count_ReturnsCorrectCount()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("event_002", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("event_003", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            Assert.That(_database.Count, Is.EqualTo(3));
        }

        #endregion
    }
}
