using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 재화 데이터 (동적)
    /// </summary>
    [Serializable]
    public struct EventCurrencyData
    {
        /// <summary>
        /// 이벤트 재화 목록
        /// </summary>
        public List<EventCurrencyItem> Currencies;

        /// <summary>
        /// 기본값 생성
        /// </summary>
        public static EventCurrencyData CreateDefault()
        {
            return new EventCurrencyData
            {
                Currencies = new List<EventCurrencyItem>()
            };
        }

        /// <summary>
        /// 이벤트 재화 조회
        /// </summary>
        /// <param name="eventId">이벤트 ID (예: "event_2024_summer")</param>
        /// <param name="currencyId">재화 ID (예: "summer_coin")</param>
        /// <returns>보유 수량 (없으면 0)</returns>
        public int GetAmount(string eventId, string currencyId)
        {
            if (Currencies == null) return 0;

            foreach (var item in Currencies)
            {
                if (item.EventId == eventId && item.CurrencyId == currencyId)
                    return item.Amount;
            }

            return 0;
        }

        /// <summary>
        /// 이벤트 재화 차감 가능 여부
        /// </summary>
        public bool CanAfford(string eventId, string currencyId, int amount)
        {
            return GetAmount(eventId, currencyId) >= amount;
        }

        /// <summary>
        /// 특정 이벤트의 모든 재화 조회
        /// </summary>
        public List<EventCurrencyItem> GetEventCurrencies(string eventId)
        {
            var result = new List<EventCurrencyItem>();
            if (Currencies == null) return result;

            foreach (var item in Currencies)
            {
                if (item.EventId == eventId)
                    result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 만료된 재화 정리
        /// </summary>
        public void CleanupExpired(long currentTimestamp)
        {
            if (Currencies == null) return;

            Currencies.RemoveAll(item =>
                item.ExpiresAt > 0 && item.ExpiresAt < currentTimestamp);
        }

        /// <summary>
        /// 이벤트 재화 제거
        /// </summary>
        public void RemoveCurrency(string eventId, string currencyId)
        {
            if (Currencies == null) return;

            for (int i = Currencies.Count - 1; i >= 0; i--)
            {
                if (Currencies[i].EventId == eventId && Currencies[i].CurrencyId == currencyId)
                {
                    Currencies.RemoveAt(i);
                    break;
                }
            }
        }

        #region Simple Event Currency API (eventId = currencyId)

        /// <summary>
        /// 이벤트 재화 조회 (단일 재화 이벤트용)
        /// </summary>
        /// <param name="eventId">이벤트 ID (재화 ID로도 사용)</param>
        public int GetCurrency(string eventId)
        {
            return GetAmount(eventId, eventId);
        }

        /// <summary>
        /// 이벤트 재화 설정 (단일 재화 이벤트용)
        /// </summary>
        public void SetCurrency(string eventId, int amount)
        {
            Currencies ??= new List<EventCurrencyItem>();

            for (int i = 0; i < Currencies.Count; i++)
            {
                if (Currencies[i].EventId == eventId && Currencies[i].CurrencyId == eventId)
                {
                    var item = Currencies[i];
                    item.Amount = amount;
                    Currencies[i] = item;
                    return;
                }
            }

            // 없으면 새로 추가
            Currencies.Add(new EventCurrencyItem
            {
                EventId = eventId,
                CurrencyId = eventId,
                Amount = amount,
                ExpiresAt = 0
            });
        }

        /// <summary>
        /// 이벤트 재화 추가 (단일 재화 이벤트용)
        /// </summary>
        public void AddCurrency(string eventId, int amount)
        {
            var current = GetCurrency(eventId);
            SetCurrency(eventId, current + amount);
        }

        /// <summary>
        /// 이벤트 재화 차감 가능 여부 (단일 재화 이벤트용)
        /// </summary>
        public bool CanAffordCurrency(string eventId, int amount)
        {
            return GetCurrency(eventId) >= amount;
        }

        /// <summary>
        /// 이벤트 재화 차감 (단일 재화 이벤트용)
        /// </summary>
        public bool DeductCurrency(string eventId, int amount)
        {
            var current = GetCurrency(eventId);
            if (current < amount) return false;

            SetCurrency(eventId, current - amount);
            return true;
        }

        #endregion
    }


    /// <summary>
    /// 개별 이벤트 재화
    /// </summary>
    [Serializable]
    public struct EventCurrencyItem
    {
        /// <summary>
        /// 이벤트 ID (예: "event_2024_summer")
        /// </summary>
        public string EventId;

        /// <summary>
        /// 재화 ID (예: "summer_coin", "summer_token")
        /// </summary>
        public string CurrencyId;

        /// <summary>
        /// 보유 수량
        /// </summary>
        public int Amount;

        /// <summary>
        /// 만료 시간 (Unix Timestamp, 0이면 무제한)
        /// </summary>
        public long ExpiresAt;

        /// <summary>
        /// 만료 여부 확인
        /// </summary>
        public bool IsExpired(long currentTimestamp)
        {
            return ExpiresAt > 0 && ExpiresAt < currentTimestamp;
        }
    }
}