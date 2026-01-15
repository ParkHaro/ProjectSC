using System;

namespace Sc.Data
{
    /// <summary>
    /// 보유 아이템 데이터
    /// </summary>
    [Serializable]
    public struct OwnedItem
    {
        /// <summary>
        /// 인스턴스 고유 ID (장비류용, 소모품은 null)
        /// </summary>
        public string InstanceId;

        /// <summary>
        /// 아이템 마스터 데이터 ID
        /// </summary>
        public string ItemId;

        /// <summary>
        /// 보유 수량 (소모품용, 장비류는 1)
        /// </summary>
        public int Count;

        /// <summary>
        /// 강화 레벨 (장비류용)
        /// </summary>
        public int EnhanceLevel;

        /// <summary>
        /// 획득 시간 (Unix Timestamp)
        /// </summary>
        public long AcquiredAt;

        /// <summary>
        /// 잠금 여부 (장비류용)
        /// </summary>
        public bool IsLocked;

        /// <summary>
        /// 장착 캐릭터 인스턴스 ID (장비류용)
        /// </summary>
        public string EquippedByCharacterId;

        /// <summary>
        /// 장비류 아이템인지 여부
        /// </summary>
        public bool IsEquipment => !string.IsNullOrEmpty(InstanceId);

        /// <summary>
        /// 장착 중인지 여부
        /// </summary>
        public bool IsEquipped => !string.IsNullOrEmpty(EquippedByCharacterId);

        /// <summary>
        /// 장비 아이템 생성
        /// </summary>
        public static OwnedItem CreateEquipment(string itemId)
        {
            return new OwnedItem
            {
                InstanceId = Guid.NewGuid().ToString(),
                ItemId = itemId,
                Count = 1,
                EnhanceLevel = 0,
                AcquiredAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsLocked = false,
                EquippedByCharacterId = null
            };
        }

        /// <summary>
        /// 소모품 아이템 생성
        /// </summary>
        public static OwnedItem CreateConsumable(string itemId, int count = 1)
        {
            return new OwnedItem
            {
                InstanceId = null,
                ItemId = itemId,
                Count = count,
                EnhanceLevel = 0,
                AcquiredAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsLocked = false,
                EquippedByCharacterId = null
            };
        }
    }
}
