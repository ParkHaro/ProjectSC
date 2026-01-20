using Sc.Core;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core.Handlers
{
    /// <summary>
    /// GetActiveEvents 응답 핸들러
    /// </summary>
    public class GetActiveEventsResponseHandler : PacketHandlerBase<GetActiveEventsResponse>
    {
        public override void Handle(GetActiveEventsResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[GetActiveEventsResponseHandler] Success - Active: {response.ActiveEvents?.Count ?? 0}, Grace: {response.GracePeriodEvents?.Count ?? 0}");

                // 이벤트 발행
                EventManager.Instance?.Publish(new GetActiveEventsCompletedEvent
                {
                    ActiveEvents = response.ActiveEvents,
                    GracePeriodEvents = response.GracePeriodEvents,
                    ServerTime = response.ServerTime
                });
            }
            else
            {
                Debug.LogWarning($"[GetActiveEventsResponseHandler] Failed - {response.ErrorCode}: {response.ErrorMessage}");

                EventManager.Instance?.Publish(new GetActiveEventsFailedEvent
                {
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }

    /// <summary>
    /// VisitEvent 응답 핸들러
    /// </summary>
    public class VisitEventResponseHandler : PacketHandlerBase<VisitEventResponse>
    {
        private string _currentEventId;

        public void SetEventId(string eventId)
        {
            _currentEventId = eventId;
        }

        public override void Handle(VisitEventResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[VisitEventResponseHandler] Success - EventId: {response.EventProgress.EventId}");

                // 이벤트 발행
                EventManager.Instance?.Publish(new VisitEventCompletedEvent
                {
                    EventId = response.EventProgress.EventId,
                    EventProgress = response.EventProgress
                });
            }
            else
            {
                Debug.LogWarning($"[VisitEventResponseHandler] Failed - {response.ErrorCode}: {response.ErrorMessage}");

                EventManager.Instance?.Publish(new VisitEventFailedEvent
                {
                    EventId = _currentEventId,
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }

    /// <summary>
    /// ClaimEventMission 응답 핸들러 (구조만)
    /// </summary>
    public class ClaimEventMissionResponseHandler : PacketHandlerBase<ClaimEventMissionResponse>
    {
        private string _currentEventId;
        private string _currentMissionId;

        public void SetIds(string eventId, string missionId)
        {
            _currentEventId = eventId;
            _currentMissionId = missionId;
        }

        public override void Handle(ClaimEventMissionResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[ClaimEventMissionResponseHandler] Success - Rewards: {response.ClaimedRewards?.Count ?? 0}");

                // Delta 적용
                if (response.Delta != null && response.Delta.HasChanges)
                {
                    DataManager.Instance?.ApplyDelta(response.Delta);
                }

                // 이벤트 발행
                EventManager.Instance?.Publish(new ClaimEventMissionCompletedEvent
                {
                    EventId = _currentEventId,
                    MissionId = _currentMissionId,
                    ClaimedRewards = response.ClaimedRewards,
                    Delta = response.Delta
                });

                // UserDataSynced 이벤트
                if (response.Delta != null && response.Delta.HasChanges)
                {
                    EventManager.Instance?.Publish(new UserDataSyncedEvent
                    {
                        Delta = response.Delta
                    });
                }
            }
            else
            {
                Debug.LogWarning($"[ClaimEventMissionResponseHandler] Failed - {response.ErrorCode}: {response.ErrorMessage}");

                EventManager.Instance?.Publish(new ClaimEventMissionFailedEvent
                {
                    EventId = _currentEventId,
                    MissionId = _currentMissionId,
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }
}
