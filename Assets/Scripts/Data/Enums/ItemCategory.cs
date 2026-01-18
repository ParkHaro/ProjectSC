namespace Sc.Data
{
    /// <summary>
    /// 아이템 세부 분류
    /// </summary>
    public enum ItemCategory
    {
        /// <summary>장비 (장착 전, 수량 기반)</summary>
        Equipment = 0,

        /// <summary>소모품 (경험치 아이템, 버프 등)</summary>
        Consumable = 1,

        /// <summary>재료 (강화, 진화, 돌파)</summary>
        Material = 2,

        /// <summary>캐릭터 조각</summary>
        CharacterShard = 3,

        /// <summary>가구</summary>
        Furniture = 4,

        /// <summary>소환 티켓</summary>
        Ticket = 5,
    }
}
