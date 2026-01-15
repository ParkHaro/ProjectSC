using System;

namespace Sc.Data
{
    /// <summary>
    /// 보유 캐릭터 데이터
    /// </summary>
    [Serializable]
    public struct OwnedCharacter
    {
        /// <summary>
        /// 인스턴스 고유 ID
        /// </summary>
        public string InstanceId;

        /// <summary>
        /// 캐릭터 마스터 데이터 ID
        /// </summary>
        public string CharacterId;

        /// <summary>
        /// 캐릭터 레벨
        /// </summary>
        public int Level;

        /// <summary>
        /// 현재 경험치
        /// </summary>
        public long Exp;

        /// <summary>
        /// 돌파 단계 (0~6)
        /// </summary>
        public int Ascension;

        /// <summary>
        /// 장착 무기 아이템 인스턴스 ID
        /// </summary>
        public string EquippedWeaponId;

        /// <summary>
        /// 장착 방어구 아이템 인스턴스 ID
        /// </summary>
        public string EquippedArmorId;

        /// <summary>
        /// 장착 악세서리 아이템 인스턴스 ID
        /// </summary>
        public string EquippedAccessoryId;

        /// <summary>
        /// 획득 시간 (Unix Timestamp)
        /// </summary>
        public long AcquiredAt;

        /// <summary>
        /// 잠금 여부 (판매/분해 방지)
        /// </summary>
        public bool IsLocked;

        /// <summary>
        /// 즐겨찾기 여부
        /// </summary>
        public bool IsFavorite;

        /// <summary>
        /// 새로 획득한 캐릭터 생성
        /// </summary>
        public static OwnedCharacter Create(string characterId)
        {
            return new OwnedCharacter
            {
                InstanceId = Guid.NewGuid().ToString(),
                CharacterId = characterId,
                Level = 1,
                Exp = 0,
                Ascension = 0,
                EquippedWeaponId = null,
                EquippedArmorId = null,
                EquippedAccessoryId = null,
                AcquiredAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsLocked = false,
                IsFavorite = false
            };
        }
    }
}
