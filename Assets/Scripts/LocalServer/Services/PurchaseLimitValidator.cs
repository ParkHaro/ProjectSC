using System;
using Sc.Data;

namespace Sc.LocalServer
{
    /// <summary>
    /// 구매 제한 검증 서비스
    /// LimitType별 리셋 시간 계산 및 구매 가능 여부 검증
    /// </summary>
    public class PurchaseLimitValidator
    {
        private readonly ServerTimeService _timeService;

        public PurchaseLimitValidator(ServerTimeService timeService)
        {
            _timeService = timeService;
        }

        /// <summary>
        /// 구매 가능 여부 확인
        /// </summary>
        public bool CanPurchase(
            ShopProductData product,
            ShopPurchaseRecord? record,
            out int remainingCount)
        {
            remainingCount = 0;

            // 제한 없음
            if (!product.HasLimit)
            {
                remainingCount = int.MaxValue;
                return true;
            }

            var currentTime = _timeService.ServerTimeUtc;

            // 구매 기록 없음 = 첫 구매
            if (!record.HasValue)
            {
                remainingCount = product.LimitCount;
                return true;
            }

            var rec = record.Value;

            // 리셋 필요 여부 확인
            if (rec.NeedsReset(currentTime))
            {
                remainingCount = product.LimitCount;
                return true;
            }

            // 남은 횟수 계산
            remainingCount = Math.Max(0, product.LimitCount - rec.PurchaseCount);
            return remainingCount > 0;
        }

        /// <summary>
        /// LimitType에 따른 다음 리셋 시간 계산
        /// </summary>
        public long CalculateResetTime(LimitType limitType)
        {
            var currentTime = _timeService.ServerTimeUtc;
            var dt = DateTimeOffset.FromUnixTimeSeconds(currentTime).UtcDateTime;

            return limitType switch
            {
                LimitType.None => 0,
                LimitType.Daily => GetNextDailyReset(dt),
                LimitType.Weekly => GetNextWeeklyReset(dt),
                LimitType.Monthly => GetNextMonthlyReset(dt),
                LimitType.Permanent => 0, // 영구 제한은 리셋 없음
                _ => 0
            };
        }

        /// <summary>
        /// 다음 일일 리셋 시간 (00:00 UTC)
        /// </summary>
        private long GetNextDailyReset(DateTime currentUtc)
        {
            var nextDay = currentUtc.Date.AddDays(1);
            return new DateTimeOffset(nextDay, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 다음 주간 리셋 시간 (월요일 00:00 UTC)
        /// </summary>
        private long GetNextWeeklyReset(DateTime currentUtc)
        {
            var daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentUtc.DayOfWeek + 7) % 7;
            if (daysUntilMonday == 0) daysUntilMonday = 7; // 오늘이 월요일이면 다음 주 월요일

            var nextMonday = currentUtc.Date.AddDays(daysUntilMonday);
            return new DateTimeOffset(nextMonday, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 다음 월간 리셋 시간 (1일 00:00 UTC)
        /// </summary>
        private long GetNextMonthlyReset(DateTime currentUtc)
        {
            var nextMonth = new DateTime(currentUtc.Year, currentUtc.Month, 1).AddMonths(1);
            return new DateTimeOffset(nextMonth, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 구매 기록 업데이트 (신규 또는 갱신)
        /// </summary>
        public ShopPurchaseRecord UpdatePurchaseRecord(
            ShopProductData product,
            ShopPurchaseRecord? existingRecord)
        {
            var currentTime = _timeService.ServerTimeUtc;
            var resetTime = CalculateResetTime(product.LimitType);

            // 기존 기록 없음 = 신규 생성
            if (!existingRecord.HasValue)
            {
                return ShopPurchaseRecord.Create(product.Id, currentTime, resetTime);
            }

            var record = existingRecord.Value;

            // 리셋 필요하면 리셋 후 구매
            if (record.NeedsReset(currentTime))
            {
                return record.ResetAndPurchase(currentTime, resetTime);
            }

            // 기존 기록에 구매 횟수 증가
            return record.IncrementPurchase(currentTime, resetTime);
        }
    }
}