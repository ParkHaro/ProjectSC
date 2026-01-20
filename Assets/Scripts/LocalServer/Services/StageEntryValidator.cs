using System;
using Sc.Data;

namespace Sc.LocalServer
{
    /// <summary>
    /// 스테이지 입장 검증 서비스
    /// </summary>
    public class StageEntryValidator
    {
        private readonly ServerTimeService _timeService;

        public StageEntryValidator(ServerTimeService timeService)
        {
            _timeService = timeService;
        }

        /// <summary>
        /// 입장 가능 여부 확인
        /// </summary>
        public bool CanEnter(
            LimitType limitType,
            int limitCount,
            StageEntryRecord? record,
            out int remainingCount)
        {
            remainingCount = 0;

            // 제한 없음
            if (limitType == LimitType.None)
            {
                remainingCount = int.MaxValue;
                return true;
            }

            var currentTime = _timeService.ServerTimeUtc;

            // 입장 기록 없음 = 첫 입장
            if (!record.HasValue)
            {
                remainingCount = limitCount;
                return true;
            }

            var rec = record.Value;

            // 리셋 필요 여부 확인
            if (rec.NeedsReset(currentTime))
            {
                remainingCount = limitCount;
                return true;
            }

            // 남은 횟수 계산
            remainingCount = Math.Max(0, limitCount - rec.EntryCount);
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
        /// 입장 기록 업데이트 (신규 또는 갱신)
        /// </summary>
        public StageEntryRecord UpdateEntryRecord(
            string stageId,
            LimitType limitType,
            StageEntryRecord? existingRecord)
        {
            var currentTime = _timeService.ServerTimeUtc;
            var resetTime = CalculateResetTime(limitType);

            // 기존 기록 없음 = 신규 생성
            if (!existingRecord.HasValue)
            {
                return StageEntryRecord.Create(stageId, currentTime, resetTime);
            }

            var record = existingRecord.Value;

            // 리셋 필요하면 리셋 후 입장
            if (record.NeedsReset(currentTime))
            {
                return record.ResetAndEnter(currentTime, resetTime);
            }

            // 기존 기록에 입장 횟수 증가
            return record.IncrementEntry(currentTime, resetTime);
        }

        /// <summary>
        /// 요일 제한 확인
        /// </summary>
        public bool IsAvailableToday(DayOfWeek[] availableDays)
        {
            if (availableDays == null || availableDays.Length == 0)
                return true;

            var currentTime = _timeService.ServerTimeUtc;
            var today = DateTimeOffset.FromUnixTimeSeconds(currentTime).UtcDateTime.DayOfWeek;

            foreach (var day in availableDays)
            {
                if (day == today)
                    return true;
            }

            return false;
        }
    }
}