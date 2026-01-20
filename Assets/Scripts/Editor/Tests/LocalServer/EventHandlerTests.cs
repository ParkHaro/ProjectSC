using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;
using Sc.Editor.Tests.Mocks;
using Sc.LocalServer;
using UnityEngine;
using EventHandler = Sc.LocalServer.EventHandler;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// EventHandler 단위 테스트.
    /// 이벤트 요청 처리 로직 검증.
    /// </summary>
    [TestFixture]
    public class EventHandlerTests
    {
        private EventHandler _handler;
        private TestServerTimeService _timeService;
        private ServerValidator _validator;
        private LiveEventDatabase _eventDatabase;
        private List<LiveEventData> _testEvents;
        private UserSaveData _userData;
        private DateTime _baseTime;

        [SetUp]
        public void SetUp()
        {
            _baseTime = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);
            _timeService = new TestServerTimeService();
            _timeService.SetFixedTime(_baseTime);

            _validator = new ServerValidator(_timeService);
            _eventDatabase = ScriptableObject.CreateInstance<LiveEventDatabase>();
            _testEvents = new List<LiveEventData>();

            _handler = new EventHandler(_validator, _timeService, _eventDatabase);
            _userData = UserSaveData.CreateNew("test_uid", "TestUser");
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

            if (_eventDatabase != null)
            {
                UnityEngine.Object.DestroyImmediate(_eventDatabase);
            }
        }

        private LiveEventData CreateEventData(
            string id,
            DateTime startTime,
            DateTime endTime,
            bool hasEventCurrency = false,
            int gracePeriodDays = 7,
            int displayOrder = 0)
        {
            var eventData = ScriptableObject.CreateInstance<LiveEventData>();
            eventData.Initialize(
                id: id,
                eventType: LiveEventType.Limited,
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
            _eventDatabase.Add(eventData);
            return eventData;
        }

        #region HandleGetActiveEvents Tests

        [Test]
        public void HandleGetActiveEvents_ReturnsEmpty_WhenNoActiveEvents()
        {
            CreateEventData("expired", _baseTime.AddDays(-14), _baseTime.AddDays(-7));

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.ActiveEvents.Count, Is.EqualTo(0));
        }

        [Test]
        public void HandleGetActiveEvents_ReturnsActiveEvents()
        {
            CreateEventData("active_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("active_002", _baseTime.AddDays(-3), _baseTime.AddDays(14));
            CreateEventData("expired", _baseTime.AddDays(-14), _baseTime.AddDays(-7));

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.ActiveEvents.Count, Is.EqualTo(2));
        }

        [Test]
        public void HandleGetActiveEvents_IncludesGracePeriodEvents_WhenRequested()
        {
            CreateEventData("active", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("grace", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);

            var request = new GetActiveEventsRequest { IncludeGracePeriod = true };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.ActiveEvents.Count, Is.EqualTo(1));
            Assert.That(response.GracePeriodEvents, Is.Not.Null);
            Assert.That(response.GracePeriodEvents.Count, Is.EqualTo(1));
        }

        [Test]
        public void HandleGetActiveEvents_ExcludesGracePeriodEvents_WhenNotRequested()
        {
            CreateEventData("active", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            CreateEventData("grace", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.GracePeriodEvents, Is.Null);
        }

        [Test]
        public void HandleGetActiveEvents_SetsHasVisited_Correctly()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            // 먼저 방문
            _userData.VisitEvent("event_001", _timeService.ServerTimeUtc);

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.ActiveEvents[0].HasVisited, Is.True);
        }

        [Test]
        public void HandleGetActiveEvents_SetsHasVisitedFalse_WhenNotVisited()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.ActiveEvents[0].HasVisited, Is.False);
        }

        #endregion

        #region HandleVisitEvent Tests

        [Test]
        public void HandleVisitEvent_Succeeds_WhenEventActive()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            var request = new VisitEventRequest { EventId = "event_001" };
            var response = _handler.HandleVisitEvent(request, ref _userData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.EventProgress.HasVisited, Is.True);
        }

        [Test]
        public void HandleVisitEvent_Succeeds_WhenEventInGracePeriod()
        {
            CreateEventData("event_001", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);

            var request = new VisitEventRequest { EventId = "event_001" };
            var response = _handler.HandleVisitEvent(request, ref _userData);

            Assert.That(response.IsSuccess, Is.True);
        }

        [Test]
        public void HandleVisitEvent_Fails_WhenEventNotFound()
        {
            var request = new VisitEventRequest { EventId = "nonexistent" };
            var response = _handler.HandleVisitEvent(request, ref _userData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(EventErrorCode.EventNotFound));
        }

        [Test]
        public void HandleVisitEvent_Fails_WhenEventExpired()
        {
            CreateEventData("event_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14), hasEventCurrency: true, gracePeriodDays: 7);

            var request = new VisitEventRequest { EventId = "event_001" };
            var response = _handler.HandleVisitEvent(request, ref _userData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(EventErrorCode.EventExpired));
        }

        [Test]
        public void HandleVisitEvent_UpdatesUserData()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7));

            var request = new VisitEventRequest { EventId = "event_001" };
            _handler.HandleVisitEvent(request, ref _userData);

            Assert.That(_userData.HasVisitedEvent("event_001"), Is.True);
        }

        #endregion

        #region HandleClaimMission Tests

        [Test]
        public void HandleClaimMission_ReturnsNotImplemented()
        {
            var request = new ClaimEventMissionRequest
            {
                EventId = "event_001",
                MissionId = "mission_001"
            };
            var response = _handler.HandleClaimMission(request, ref _userData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(EventErrorCode.MissionSystemNotImplemented));
        }

        #endregion

        #region LiveEventInfo Tests

        [Test]
        public void HandleGetActiveEvents_SetsRemainingDays_Correctly()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(5));

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.ActiveEvents[0].RemainingDays, Is.EqualTo(5));
        }

        [Test]
        public void HandleGetActiveEvents_SetsIsInGracePeriod_ForGracePeriodEvents()
        {
            CreateEventData("grace", _baseTime.AddDays(-14), _baseTime.AddDays(-1), hasEventCurrency: true, gracePeriodDays: 7);

            var request = new GetActiveEventsRequest { IncludeGracePeriod = true };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            Assert.That(response.GracePeriodEvents[0].IsInGracePeriod, Is.True);
            Assert.That(response.GracePeriodEvents[0].RemainingDays, Is.EqualTo(0));
        }

        [Test]
        public void HandleGetActiveEvents_SetsHasEventCurrency_Correctly()
        {
            CreateEventData("with_currency", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: true);
            CreateEventData("no_currency", _baseTime.AddDays(-1), _baseTime.AddDays(7), hasEventCurrency: false);

            var request = new GetActiveEventsRequest { IncludeGracePeriod = false };
            var response = _handler.HandleGetActiveEvents(request, ref _userData);

            var withCurrency = response.ActiveEvents.Find(e => e.EventId == "with_currency");
            var noCurrency = response.ActiveEvents.Find(e => e.EventId == "no_currency");

            Assert.That(withCurrency.HasEventCurrency, Is.True);
            Assert.That(noCurrency.HasEventCurrency, Is.False);
        }

        #endregion
    }
}
