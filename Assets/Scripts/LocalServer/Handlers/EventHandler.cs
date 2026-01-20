using System;
using System.Collections.Generic;
using System.Linq;
using Sc.Data;

namespace Sc.LocalServer
{
    /// <summary>
    /// 이벤트 에러 코드
    /// </summary>
    public static class EventErrorCode
    {
        public const int EventNotFound = 6001;
        public const int EventNotActive = 6002;
        public const int EventExpired = 6003;
        public const int MissionNotFound = 6004;
        public const int MissionNotCompleted = 6005;
        public const int MissionAlreadyClaimed = 6006;
        public const int CurrencyInsufficient = 6007;
        public const int MissionSystemNotImplemented = 6099;
    }

    /// <summary>
    /// 이벤트 요청 핸들러 (서버측)
    /// </summary>
    public class EventHandler
    {
        private readonly ServerValidator _validator;
        private readonly ServerTimeService _timeService;
        private readonly LiveEventDatabase _eventDatabase;

        public EventHandler(
            ServerValidator validator,
            ServerTimeService timeService,
            LiveEventDatabase eventDatabase)
        {
            _validator = validator;
            _timeService = timeService;
            _eventDatabase = eventDatabase;
        }

        /// <summary>
        /// 활성 이벤트 목록 조회
        /// </summary>
        public GetActiveEventsResponse HandleGetActiveEvents(
            GetActiveEventsRequest request,
            ref UserSaveData userData)
        {
            var serverTime = DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime;

            // 활성 이벤트 목록 조회
            var activeEvents = new List<LiveEventInfo>();
            foreach (var eventData in _eventDatabase.GetActiveEvents(serverTime))
            {
                activeEvents.Add(CreateLiveEventInfo(eventData, ref userData, serverTime, false));
            }

            // 유예 기간 이벤트 목록 조회
            List<LiveEventInfo> gracePeriodEvents = null;
            if (request.IncludeGracePeriod)
            {
                gracePeriodEvents = new List<LiveEventInfo>();
                foreach (var eventData in _eventDatabase.GetGracePeriodEvents(serverTime))
                {
                    gracePeriodEvents.Add(CreateLiveEventInfo(eventData, ref userData, serverTime, true));
                }
            }

            return GetActiveEventsResponse.Success(activeEvents, gracePeriodEvents);
        }

        /// <summary>
        /// 이벤트 방문 처리
        /// </summary>
        public VisitEventResponse HandleVisitEvent(
            VisitEventRequest request,
            ref UserSaveData userData)
        {
            var serverTime = DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime;

            // 이벤트 존재 확인
            var eventData = _eventDatabase.GetById(request.EventId);
            if (eventData == null)
            {
                return VisitEventResponse.Fail(EventErrorCode.EventNotFound, "이벤트를 찾을 수 없습니다.");
            }

            // 이벤트 활성 또는 유예 기간 확인
            if (!eventData.IsActive(serverTime) && !eventData.IsInGracePeriod(serverTime))
            {
                return VisitEventResponse.Fail(EventErrorCode.EventExpired, "종료된 이벤트입니다.");
            }

            // 방문 처리
            userData.VisitEvent(request.EventId, _timeService.ServerTimeUtc);

            var progress = userData.FindEventProgress(request.EventId);
            return VisitEventResponse.Success(progress ?? LiveEventProgress.CreateDefault(request.EventId));
        }

        /// <summary>
        /// 이벤트 미션 보상 수령 (구조만 - 미구현)
        /// </summary>
        public ClaimEventMissionResponse HandleClaimMission(
            ClaimEventMissionRequest request,
            ref UserSaveData userData)
        {
            // 현재는 플레이스홀더 - 미션 시스템 미구현
            return ClaimEventMissionResponse.Fail(
                EventErrorCode.MissionSystemNotImplemented,
                "미션 시스템이 아직 구현되지 않았습니다.");
        }

        /// <summary>
        /// LiveEventInfo 생성
        /// </summary>
        private LiveEventInfo CreateLiveEventInfo(
            LiveEventData eventData,
            ref UserSaveData userData,
            DateTime serverTime,
            bool isInGracePeriod)
        {
            var progress = userData.FindEventProgress(eventData.Id);

            return new LiveEventInfo
            {
                EventId = eventData.Id,
                EventType = eventData.EventType,
                NameKey = eventData.NameKey,
                BannerImage = eventData.BannerImage,
                StartTime = new DateTimeOffset(eventData.StartTime).ToUnixTimeSeconds(),
                EndTime = new DateTimeOffset(eventData.EndTime).ToUnixTimeSeconds(),
                RemainingDays = isInGracePeriod ? 0 : eventData.GetRemainingDays(serverTime),
                IsInGracePeriod = isInGracePeriod,
                GracePeriodRemainingDays = eventData.GetGracePeriodRemainingDays(serverTime),
                SubContents = eventData.SubContents?.ToList() ?? new List<EventSubContent>(),
                HasClaimableReward = progress?.GetClaimableMissionCount() > 0,
                HasVisited = progress?.HasVisited ?? false,
                HasEventCurrency = eventData.HasEventCurrency,
                CurrencyPolicy = eventData.CurrencyPolicy
            };
        }
    }
}
