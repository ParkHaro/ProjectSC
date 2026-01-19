using Sc.Data;
using Sc.Event.OutGame;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 상점 구매 응답 핸들러
    /// </summary>
    public class PurchaseResponseHandler : PacketHandlerBase<ShopPurchaseResponse>
    {
        public override void Handle(ShopPurchaseResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[PurchaseHandler] Purchase success: {response.ProductId}, Rewards: {response.Rewards.Count}");

                // DataManager에 Delta 적용
                if (response.Delta != null && response.Delta.HasChanges)
                {
                    DataManager.Instance.ApplyDelta(response.Delta);
                }

                // 성공 이벤트 발행
                EventManager.Instance.Publish(new PurchaseCompletedEvent
                {
                    ProductId = response.ProductId,
                    Rewards = response.Rewards,
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
                Debug.LogWarning($"[PurchaseHandler] Purchase failed: {response.ErrorCode} - {response.ErrorMessage}");

                // 실패 이벤트 발행
                EventManager.Instance.Publish(new PurchaseFailedEvent
                {
                    ProductId = "", // 요청에서 가져와야 함
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }
}
