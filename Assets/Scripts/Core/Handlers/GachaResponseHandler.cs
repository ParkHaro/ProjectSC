using Sc.Event.OutGame;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 가챠 응답 핸들러
    /// </summary>
    public class GachaResponseHandler : PacketHandlerBase<GachaResponse>
    {
        public override void Handle(GachaResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[GachaHandler] Gacha success: {response.Results.Count} results, Pity: {response.CurrentPityCount}");

                // DataManager에 Delta 적용
                if (response.Delta != null && response.Delta.HasChanges)
                {
                    DataManager.Instance.ApplyDelta(response.Delta);
                }

                // 성공 이벤트 발행
                EventManager.Instance.Publish(new GachaCompletedEvent
                {
                    GachaPoolId = "", // 요청에서 가져와야 하지만 현재 구조상 어려움
                    Results = response.Results,
                    PityCount = response.CurrentPityCount,
                    Delta = response.Delta
                });

                // 유저 데이터 동기화 이벤트
                if (response.Delta != null && response.Delta.HasChanges)
                {
                    EventManager.Instance.Publish(new UserDataSyncedEvent
                    {
                        Delta = response.Delta
                    });
                }
            }
            else
            {
                Debug.LogWarning($"[GachaHandler] Gacha failed: {response.ErrorCode} - {response.ErrorMessage}");

                // 실패 이벤트 발행
                EventManager.Instance.Publish(new GachaFailedEvent
                {
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }
}
