using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;

namespace Sc.Editor.Tests.Data
{
    /// <summary>
    /// EventCurrencyData 단위 테스트.
    /// 이벤트 재화 관리 검증.
    /// </summary>
    [TestFixture]
    public class EventCurrencyDataTests
    {
        private EventCurrencyData _currencyData;

        [SetUp]
        public void SetUp()
        {
            _currencyData = EventCurrencyData.CreateDefault();
        }

        #region CreateDefault Tests

        [Test]
        public void CreateDefault_InitializesEmptyList()
        {
            var data = EventCurrencyData.CreateDefault();

            Assert.That(data.Currencies, Is.Not.Null);
            Assert.That(data.Currencies.Count, Is.EqualTo(0));
        }

        #endregion

        #region GetAmount Tests

        [Test]
        public void GetAmount_ReturnsZero_WhenCurrenciesIsNull()
        {
            _currencyData.Currencies = null;

            var amount = _currencyData.GetAmount("event_001", "token_001");

            Assert.That(amount, Is.EqualTo(0));
        }

        [Test]
        public void GetAmount_ReturnsZero_WhenCurrencyNotFound()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var amount = _currencyData.GetAmount("event_001", "token_002");

            Assert.That(amount, Is.EqualTo(0));
        }

        [Test]
        public void GetAmount_ReturnsZero_WhenEventNotFound()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var amount = _currencyData.GetAmount("event_002", "token_001");

            Assert.That(amount, Is.EqualTo(0));
        }

        [Test]
        public void GetAmount_ReturnsCorrectAmount()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 },
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_002", Amount = 200 },
                new EventCurrencyItem { EventId = "event_002", CurrencyId = "token_001", Amount = 300 }
            };

            Assert.That(_currencyData.GetAmount("event_001", "token_001"), Is.EqualTo(100));
            Assert.That(_currencyData.GetAmount("event_001", "token_002"), Is.EqualTo(200));
            Assert.That(_currencyData.GetAmount("event_002", "token_001"), Is.EqualTo(300));
        }

        #endregion

        #region CanAfford Tests

        [Test]
        public void CanAfford_ReturnsTrue_WhenHasEnough()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var result = _currencyData.CanAfford("event_001", "token_001", 50);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanAfford_ReturnsTrue_WhenExactAmount()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var result = _currencyData.CanAfford("event_001", "token_001", 100);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanAfford_ReturnsFalse_WhenNotEnough()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var result = _currencyData.CanAfford("event_001", "token_001", 150);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanAfford_ReturnsFalse_WhenCurrencyNotFound()
        {
            var result = _currencyData.CanAfford("event_001", "token_001", 1);

            Assert.That(result, Is.False);
        }

        #endregion

        #region GetEventCurrencies Tests

        [Test]
        public void GetEventCurrencies_ReturnsEmpty_WhenCurrenciesIsNull()
        {
            _currencyData.Currencies = null;

            var result = _currencyData.GetEventCurrencies("event_001");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetEventCurrencies_ReturnsEmpty_WhenEventNotFound()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            var result = _currencyData.GetEventCurrencies("event_002");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetEventCurrencies_ReturnsAllCurrenciesForEvent()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 },
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_002", Amount = 200 },
                new EventCurrencyItem { EventId = "event_002", CurrencyId = "token_001", Amount = 300 }
            };

            var result = _currencyData.GetEventCurrencies("event_001");

            Assert.That(result.Count, Is.EqualTo(2));
        }

        #endregion

        #region RemoveCurrency Tests

        [Test]
        public void RemoveCurrency_DoesNothing_WhenCurrenciesIsNull()
        {
            _currencyData.Currencies = null;

            _currencyData.RemoveCurrency("event_001", "token_001");

            Assert.That(_currencyData.Currencies, Is.Null);
        }

        [Test]
        public void RemoveCurrency_DoesNothing_WhenCurrencyNotFound()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 }
            };

            _currencyData.RemoveCurrency("event_001", "token_002");

            Assert.That(_currencyData.Currencies.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveCurrency_RemovesCurrency()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_001", Amount = 100 },
                new EventCurrencyItem { EventId = "event_001", CurrencyId = "token_002", Amount = 200 }
            };

            _currencyData.RemoveCurrency("event_001", "token_001");

            Assert.That(_currencyData.Currencies.Count, Is.EqualTo(1));
            Assert.That(_currencyData.GetAmount("event_001", "token_001"), Is.EqualTo(0));
            Assert.That(_currencyData.GetAmount("event_001", "token_002"), Is.EqualTo(200));
        }

        #endregion

        #region CleanupExpired Tests

        [Test]
        public void CleanupExpired_DoesNothing_WhenCurrenciesIsNull()
        {
            _currencyData.Currencies = null;

            _currencyData.CleanupExpired(1000);

            Assert.That(_currencyData.Currencies, Is.Null);
        }

        [Test]
        public void CleanupExpired_RemovesExpiredItems()
        {
            _currencyData.Currencies = new List<EventCurrencyItem>
            {
                new EventCurrencyItem { EventId = "e1", CurrencyId = "t1", Amount = 100, ExpiresAt = 500 },  // expired
                new EventCurrencyItem { EventId = "e2", CurrencyId = "t1", Amount = 200, ExpiresAt = 1500 }, // not expired
                new EventCurrencyItem { EventId = "e3", CurrencyId = "t1", Amount = 300, ExpiresAt = 0 }     // no expiry
            };

            _currencyData.CleanupExpired(1000);

            Assert.That(_currencyData.Currencies.Count, Is.EqualTo(2));
            Assert.That(_currencyData.GetAmount("e1", "t1"), Is.EqualTo(0));
            Assert.That(_currencyData.GetAmount("e2", "t1"), Is.EqualTo(200));
            Assert.That(_currencyData.GetAmount("e3", "t1"), Is.EqualTo(300));
        }

        #endregion

        #region EventCurrencyItem.IsExpired Tests

        [Test]
        public void IsExpired_ReturnsFalse_WhenExpiresAtZero()
        {
            var item = new EventCurrencyItem { ExpiresAt = 0 };

            Assert.That(item.IsExpired(1000), Is.False);
        }

        [Test]
        public void IsExpired_ReturnsFalse_WhenNotExpired()
        {
            var item = new EventCurrencyItem { ExpiresAt = 2000 };

            Assert.That(item.IsExpired(1000), Is.False);
        }

        [Test]
        public void IsExpired_ReturnsTrue_WhenExpired()
        {
            var item = new EventCurrencyItem { ExpiresAt = 500 };

            Assert.That(item.IsExpired(1000), Is.True);
        }

        #endregion
    }
}
