namespace Sc.Data
{
    /// <summary>
    /// 보상 타입
    /// </summary>
    public enum RewardType
    {
        /// <summary>재화 (골드, 젬, 스태미나, 이벤트 코인 등)</summary>
        Currency = 0,

        /// <summary>인벤토리 아이템 (ItemCategory로 세분화)</summary>
        Item = 1,

        /// <summary>캐릭터 획득</summary>
        Character = 2,

        /// <summary>플레이어 경험치</summary>
        PlayerExp = 3,
    }
}
