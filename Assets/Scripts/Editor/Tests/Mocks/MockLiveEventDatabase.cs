using System;
using System.Collections.Generic;
using System.Linq;
using Sc.Data;

namespace Sc.Editor.Tests.Mocks
{
    /// <summary>
    /// 테스트용 LiveEventDatabase Mock.
    /// Dictionary 기반으로 이벤트 데이터를 관리.
    ///
    /// Note: 현재는 EventHandler/EventCurrencyConverter 테스트에서 직접
    /// ScriptableObject.CreateInstance&lt;LiveEventDatabase&gt;를 사용하지만,
    /// Unity 환경 없이 순수 단위 테스트가 필요한 경우 이 Mock을 사용할 수 있음.
    /// </summary>
    public class MockLiveEventDatabase
    {
        private readonly Dictionary<string, MockEventData> _events = new();

        public int Count => _events.Count;

        /// <summary>
        /// 테스트용 이벤트 데이터
        /// </summary>
        public class MockEventData
        {
            public string Id { get; set; }
            public LiveEventType EventType { get; set; }
            public string NameKey { get; set; }
            public string BannerImage { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public List<EventSubContent> SubContents { get; set; } = new();
            public bool HasEventCurrency { get; set; }
            public EventCurrencyPolicy CurrencyPolicy { get; set; }
            public int DisplayOrder { get; set; }

            public bool IsActive(DateTime serverTime)
            {
                return serverTime >= StartTime && serverTime < EndTime;
            }

            public bool IsInGracePeriod(DateTime serverTime)
            {
                if (!HasEventCurrency) return false;
                return serverTime >= EndTime && serverTime < EndTime.AddDays(CurrencyPolicy.GracePeriodDays);
            }

            public bool IsGracePeriodExpired(DateTime serverTime)
            {
                if (!HasEventCurrency) return false;
                return serverTime >= EndTime.AddDays(CurrencyPolicy.GracePeriodDays);
            }

            public int GetRemainingDays(DateTime serverTime)
            {
                var timeSpan = EndTime - serverTime;
                return Math.Max(0, (int)Math.Ceiling(timeSpan.TotalDays));
            }

            public int GetGracePeriodRemainingDays(DateTime serverTime)
            {
                if (!IsInGracePeriod(serverTime)) return 0;
                var gracePeriodEnd = EndTime.AddDays(CurrencyPolicy.GracePeriodDays);
                var timeSpan = gracePeriodEnd - serverTime;
                return Math.Max(0, (int)Math.Ceiling(timeSpan.TotalDays));
            }
        }

        /// <summary>
        /// 이벤트 추가
        /// </summary>
        public void Add(MockEventData eventData)
        {
            if (eventData != null && !string.IsNullOrEmpty(eventData.Id))
            {
                _events[eventData.Id] = eventData;
            }
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void Clear()
        {
            _events.Clear();
        }

        /// <summary>
        /// ID로 이벤트 조회
        /// </summary>
        public MockEventData GetById(string id)
        {
            return _events.TryGetValue(id, out var data) ? data : null;
        }

        /// <summary>
        /// 활성 이벤트 목록 조회
        /// </summary>
        public IEnumerable<MockEventData> GetActiveEvents(DateTime serverTime)
        {
            return _events.Values
                .Where(e => e.IsActive(serverTime))
                .OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 유예 기간 이벤트 목록 조회
        /// </summary>
        public IEnumerable<MockEventData> GetGracePeriodEvents(DateTime serverTime)
        {
            return _events.Values
                .Where(e => e.IsInGracePeriod(serverTime))
                .OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 활성 + 유예 기간 이벤트 목록 조회
        /// </summary>
        public IEnumerable<MockEventData> GetAvailableEvents(DateTime serverTime, bool includeGracePeriod = true)
        {
            var result = GetActiveEvents(serverTime);
            if (includeGracePeriod)
            {
                result = result.Concat(GetGracePeriodEvents(serverTime));
            }
            return result.OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 유예 기간 만료된 이벤트 목록 (재화 전환 대상)
        /// </summary>
        public IEnumerable<MockEventData> GetExpiredGracePeriodEvents(DateTime serverTime)
        {
            return _events.Values.Where(e => e.HasEventCurrency && e.IsGracePeriodExpired(serverTime));
        }

        /// <summary>
        /// 타입별 이벤트 목록 조회
        /// </summary>
        public IEnumerable<MockEventData> GetByType(LiveEventType eventType)
        {
            return _events.Values.Where(e => e.EventType == eventType);
        }

        #region Test Helpers

        /// <summary>
        /// 기본 활성 이벤트 생성 (테스트 헬퍼)
        /// </summary>
        public MockEventData CreateActiveEvent(
            string id,
            DateTime serverTime,
            int durationDays = 7,
            bool hasEventCurrency = false,
            int gracePeriodDays = 7,
            float conversionRate = 10f)
        {
            var eventData = new MockEventData
            {
                Id = id,
                EventType = LiveEventType.Limited,
                NameKey = $"event_{id}",
                BannerImage = $"banner_{id}",
                StartTime = serverTime.AddDays(-1),
                EndTime = serverTime.AddDays(durationDays),
                HasEventCurrency = hasEventCurrency,
                CurrencyPolicy = hasEventCurrency ? new EventCurrencyPolicy
                {
                    CurrencyId = $"token_{id}",
                    CurrencyNameKey = $"currency_{id}",
                    GracePeriodDays = gracePeriodDays,
                    ConvertToCurrencyId = "gold",
                    ConversionRate = conversionRate
                } : default,
                DisplayOrder = _events.Count
            };

            Add(eventData);
            return eventData;
        }

        /// <summary>
        /// 유예 기간 이벤트 생성 (테스트 헬퍼)
        /// </summary>
        public MockEventData CreateGracePeriodEvent(
            string id,
            DateTime serverTime,
            int gracePeriodRemainingDays = 3,
            float conversionRate = 10f)
        {
            var eventData = new MockEventData
            {
                Id = id,
                EventType = LiveEventType.Limited,
                NameKey = $"event_{id}",
                BannerImage = $"banner_{id}",
                StartTime = serverTime.AddDays(-14),
                EndTime = serverTime.AddDays(-1),
                HasEventCurrency = true,
                CurrencyPolicy = new EventCurrencyPolicy
                {
                    CurrencyId = $"token_{id}",
                    CurrencyNameKey = $"currency_{id}",
                    GracePeriodDays = gracePeriodRemainingDays + 1,
                    ConvertToCurrencyId = "gold",
                    ConversionRate = conversionRate
                },
                DisplayOrder = _events.Count
            };

            Add(eventData);
            return eventData;
        }

        /// <summary>
        /// 만료된 이벤트 생성 (테스트 헬퍼)
        /// </summary>
        public MockEventData CreateExpiredEvent(
            string id,
            DateTime serverTime,
            float conversionRate = 10f)
        {
            var eventData = new MockEventData
            {
                Id = id,
                EventType = LiveEventType.Limited,
                NameKey = $"event_{id}",
                BannerImage = $"banner_{id}",
                StartTime = serverTime.AddDays(-30),
                EndTime = serverTime.AddDays(-14),
                HasEventCurrency = true,
                CurrencyPolicy = new EventCurrencyPolicy
                {
                    CurrencyId = $"token_{id}",
                    CurrencyNameKey = $"currency_{id}",
                    GracePeriodDays = 7,
                    ConvertToCurrencyId = "gold",
                    ConversionRate = conversionRate
                },
                DisplayOrder = _events.Count
            };

            Add(eventData);
            return eventData;
        }

        #endregion
    }
}
