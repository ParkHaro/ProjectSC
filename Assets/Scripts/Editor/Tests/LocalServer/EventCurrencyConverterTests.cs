using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;
using Sc.Editor.Tests.Mocks;
using Sc.LocalServer;
using UnityEngine;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// EventCurrencyConverter 단위 테스트.
    /// 이벤트 재화 전환 로직 검증.
    /// </summary>
    [TestFixture]
    public class EventCurrencyConverterTests
    {
        private EventCurrencyConverter _converter;
        private TestServerTimeService _timeService;
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

            _eventDatabase = ScriptableObject.CreateInstance<LiveEventDatabase>();
            _testEvents = new List<LiveEventData>();

            _converter = new EventCurrencyConverter(_eventDatabase, _timeService);
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
            int gracePeriodDays = 7,
            float conversionRate = 10f,
            string targetCurrency = "gold")
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
                hasEventCurrency: true,
                currencyPolicy: new EventCurrencyPolicy
                {
                    CurrencyId = $"token_{id}",
                    GracePeriodDays = gracePeriodDays,
                    ConvertToCurrencyId = targetCurrency,
                    ConversionRate = conversionRate
                },
                displayOrder: 0,
                showCountdown: true
            );
            _testEvents.Add(eventData);
            _eventDatabase.Add(eventData);
            return eventData;
        }

        private void AddEventCurrency(string eventId, string currencyId, int amount)
        {
            _userData.EventCurrency.Currencies ??= new List<EventCurrencyItem>();
            _userData.EventCurrency.Currencies.Add(new EventCurrencyItem
            {
                EventId = eventId,
                CurrencyId = currencyId,
                Amount = amount
            });
        }

        #region ConvertExpiredCurrencies Tests

        [Test]
        public void ConvertExpiredCurrencies_ReturnsEmpty_WhenNoExpiredEvents()
        {
            CreateEventData("active", _baseTime.AddDays(-1), _baseTime.AddDays(7));
            AddEventCurrency("active", "token_active", 100);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConvertExpiredCurrencies_ReturnsEmpty_WhenNoCurrency()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConvertExpiredCurrencies_ConvertsExpiredEventCurrency()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 10f);
            AddEventCurrency("expired", "token_expired", 100);

            var initialGold = _userData.Currency.Gold;
            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].EventId, Is.EqualTo("expired"));
            Assert.That(results[0].SourceAmount, Is.EqualTo(100));
            Assert.That(results[0].TargetAmount, Is.EqualTo(1000)); // 100 * 10
            Assert.That(_userData.Currency.Gold, Is.EqualTo(initialGold + 1000));
        }

        [Test]
        public void ConvertExpiredCurrencies_RemovesEventCurrency()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7);
            AddEventCurrency("expired", "token_expired", 100);

            _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(_userData.EventCurrency.GetAmount("expired", "token_expired"), Is.EqualTo(0));
        }

        [Test]
        public void ConvertExpiredCurrencies_ConvertsMultipleEvents()
        {
            CreateEventData("expired_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 10f);
            CreateEventData("expired_002", _baseTime.AddDays(-45), _baseTime.AddDays(-28), gracePeriodDays: 7, conversionRate: 5f);
            AddEventCurrency("expired_001", "token_expired_001", 100);
            AddEventCurrency("expired_002", "token_expired_002", 200);

            var initialGold = _userData.Currency.Gold;
            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(_userData.Currency.Gold, Is.EqualTo(initialGold + 1000 + 1000)); // 100*10 + 200*5
        }

        [Test]
        public void ConvertExpiredCurrencies_IgnoresGracePeriodEvents()
        {
            CreateEventData("grace", _baseTime.AddDays(-14), _baseTime.AddDays(-1), gracePeriodDays: 7);
            AddEventCurrency("grace", "token_grace", 100);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(_userData.EventCurrency.GetAmount("grace", "token_grace"), Is.EqualTo(100));
        }

        [Test]
        public void ConvertExpiredCurrencies_AppliesConversionRate()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 2.5f);
            AddEventCurrency("expired", "token_expired", 100);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results[0].TargetAmount, Is.EqualTo(250)); // Floor(100 * 2.5)
        }

        [Test]
        public void ConvertExpiredCurrencies_FloorsFractionalConversion()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 3.3f);
            AddEventCurrency("expired", "token_expired", 10);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results[0].TargetAmount, Is.EqualTo(33)); // Floor(10 * 3.3)
        }

        #endregion

        #region ConvertEventCurrency Tests

        [Test]
        public void ConvertEventCurrency_ReturnsNull_WhenEventNotFound()
        {
            var result = _converter.ConvertEventCurrency(ref _userData, "nonexistent");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ConvertEventCurrency_ReturnsNull_WhenNoCurrency()
        {
            CreateEventData("event_001", _baseTime.AddDays(-30), _baseTime.AddDays(-14));

            var result = _converter.ConvertEventCurrency(ref _userData, "event_001");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ConvertEventCurrency_ConvertsManually()
        {
            CreateEventData("event_001", _baseTime.AddDays(-1), _baseTime.AddDays(7), conversionRate: 5f);
            AddEventCurrency("event_001", "token_event_001", 50);

            var initialGold = _userData.Currency.Gold;
            var result = _converter.ConvertEventCurrency(ref _userData, "event_001");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.SourceAmount, Is.EqualTo(50));
            Assert.That(result.Value.TargetAmount, Is.EqualTo(250));
            Assert.That(_userData.Currency.Gold, Is.EqualTo(initialGold + 250));
        }

        #endregion

        #region Target Currency Tests

        [Test]
        public void ConvertExpiredCurrencies_AddsToGem_WhenTargetIsGem()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 1f, targetCurrency: "gem");
            AddEventCurrency("expired", "token_expired", 100);

            var initialGem = _userData.Currency.Gem;
            _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(_userData.Currency.Gem, Is.EqualTo(initialGem + 100));
        }

        [Test]
        public void ConvertExpiredCurrencies_AddsToFreeGem_WhenTargetIsFreeGem()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 1f, targetCurrency: "freegem");
            AddEventCurrency("expired", "token_expired", 100);

            var initialFreeGem = _userData.Currency.FreeGem;
            _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(_userData.Currency.FreeGem, Is.EqualTo(initialFreeGem + 100));
        }

        [Test]
        public void ConvertExpiredCurrencies_DefaultsToGold_WhenUnknownCurrency()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7, conversionRate: 1f, targetCurrency: "unknown");
            AddEventCurrency("expired", "token_expired", 100);

            var initialGold = _userData.Currency.Gold;
            _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(_userData.Currency.Gold, Is.EqualTo(initialGold + 100));
        }

        #endregion

        #region Edge Cases

        [Test]
        public void ConvertExpiredCurrencies_HandlesZeroAmount()
        {
            CreateEventData("expired", _baseTime.AddDays(-30), _baseTime.AddDays(-14), gracePeriodDays: 7);
            AddEventCurrency("expired", "token_expired", 0);

            var results = _converter.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConvertExpiredCurrencies_ReturnsEmpty_WhenDatabaseNull()
        {
            var converterWithNull = new EventCurrencyConverter(null, _timeService);

            var results = converterWithNull.ConvertExpiredCurrencies(ref _userData);

            Assert.That(results.Count, Is.EqualTo(0));
        }

        #endregion
    }
}
