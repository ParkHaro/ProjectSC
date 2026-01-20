using System;
using System.Collections.Generic;
using Sc.Data;
using UnityEngine;

namespace Sc.LocalServer
{
    /// <summary>
    /// 이벤트 재화 전환 서비스
    /// 유예 기간 만료 시 이벤트 재화를 범용 재화로 자동 전환
    /// </summary>
    public class EventCurrencyConverter
    {
        private readonly LiveEventDatabase _eventDatabase;
        private readonly ServerTimeService _timeService;

        /// <summary>
        /// 전환 결과 정보
        /// </summary>
        public struct ConversionResult
        {
            public string EventId;
            public string SourceCurrencyId;
            public int SourceAmount;
            public string TargetCurrencyId;
            public int TargetAmount;
        }

        public EventCurrencyConverter(LiveEventDatabase eventDatabase, ServerTimeService timeService)
        {
            _eventDatabase = eventDatabase;
            _timeService = timeService;
        }

        /// <summary>
        /// 만료된 이벤트 재화 전환
        /// </summary>
        public List<ConversionResult> ConvertExpiredCurrencies(ref UserSaveData userData)
        {
            var results = new List<ConversionResult>();

            if (_eventDatabase == null)
            {
                Debug.LogWarning("[EventCurrencyConverter] EventDatabase is null");
                return results;
            }

            var serverTime = DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime;

            // 유예 기간 만료된 이벤트 조회
            var expiredEvents = _eventDatabase.GetExpiredGracePeriodEvents(serverTime);

            foreach (var eventData in expiredEvents)
            {
                if (!eventData.HasEventCurrency) continue;

                var policy = eventData.CurrencyPolicy;
                var currencyId = policy.CurrencyId;

                // 이벤트 재화 잔량 확인
                var amount = userData.EventCurrency.GetAmount(eventData.Id, currencyId);
                if (amount <= 0) continue;

                // 전환 수량 계산
                var convertedAmount = Mathf.FloorToInt(amount * policy.ConversionRate);

                Debug.Log($"[EventCurrencyConverter] Converting {amount} {currencyId} -> {convertedAmount} {policy.ConvertToCurrencyId}");

                // 범용 재화 추가
                AddCurrency(ref userData.Currency, policy.ConvertToCurrencyId, convertedAmount);

                // 이벤트 재화 제거
                userData.EventCurrency.RemoveCurrency(eventData.Id, currencyId);

                // 이벤트 진행 데이터 제거 (선택적)
                // userData.EventProgresses?.Remove(eventData.Id);

                results.Add(new ConversionResult
                {
                    EventId = eventData.Id,
                    SourceCurrencyId = currencyId,
                    SourceAmount = amount,
                    TargetCurrencyId = policy.ConvertToCurrencyId,
                    TargetAmount = convertedAmount
                });
            }

            if (results.Count > 0)
            {
                Debug.Log($"[EventCurrencyConverter] Converted {results.Count} event currencies");
            }

            return results;
        }

        /// <summary>
        /// 특정 이벤트 재화 전환 (수동)
        /// </summary>
        public ConversionResult? ConvertEventCurrency(
            ref UserSaveData userData,
            string eventId)
        {
            if (_eventDatabase == null) return null;

            var eventData = _eventDatabase.GetById(eventId);
            if (eventData == null || !eventData.HasEventCurrency)
            {
                return null;
            }

            var policy = eventData.CurrencyPolicy;
            var currencyId = policy.CurrencyId;

            var amount = userData.EventCurrency.GetAmount(eventId, currencyId);
            if (amount <= 0) return null;

            var convertedAmount = Mathf.FloorToInt(amount * policy.ConversionRate);

            AddCurrency(ref userData.Currency, policy.ConvertToCurrencyId, convertedAmount);
            userData.EventCurrency.RemoveCurrency(eventId, currencyId);

            return new ConversionResult
            {
                EventId = eventId,
                SourceCurrencyId = currencyId,
                SourceAmount = amount,
                TargetCurrencyId = policy.ConvertToCurrencyId,
                TargetAmount = convertedAmount
            };
        }

        private void AddCurrency(ref UserCurrency currency, string currencyId, int amount)
        {
            // CurrencyId에 따라 적절한 필드에 추가
            switch (currencyId.ToLower())
            {
                case "gold":
                    currency.Gold += amount;
                    break;
                case "gem":
                    currency.Gem += amount;
                    break;
                case "freegem":
                    currency.FreeGem += amount;
                    break;
                default:
                    Debug.LogWarning($"[EventCurrencyConverter] Unknown currency: {currencyId}, adding to Gold");
                    currency.Gold += amount;
                    break;
            }
        }
    }
}
