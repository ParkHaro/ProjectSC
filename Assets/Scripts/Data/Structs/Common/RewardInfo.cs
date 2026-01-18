using System;

namespace Sc.Data
{
    /// <summary>
    /// 보상 정보 구조체
    /// </summary>
    [Serializable]
    public struct RewardInfo
    {
        /// <summary>보상 타입</summary>
        public RewardType Type;

        /// <summary>
        /// 아이템/캐릭터 ID 또는 CostType 문자열
        /// - Currency: "Gold", "Gem", "Stamina" 등 (CostType.ToString())
        /// - Character: "char_001"
        /// - Item: "item_001"
        /// - PlayerExp: "" (빈 문자열, Amount만 사용)
        /// </summary>
        public string ItemId;

        /// <summary>수량</summary>
        public int Amount;

        /// <summary>
        /// 생성자
        /// </summary>
        public RewardInfo(RewardType type, string itemId, int amount)
        {
            Type = type;
            ItemId = itemId;
            Amount = amount;
        }

        /// <summary>
        /// 재화 보상 생성
        /// </summary>
        public static RewardInfo Currency(CostType costType, int amount)
        {
            return new RewardInfo(RewardType.Currency, costType.ToString(), amount);
        }

        /// <summary>
        /// 아이템 보상 생성
        /// </summary>
        public static RewardInfo Item(string itemId, int amount)
        {
            return new RewardInfo(RewardType.Item, itemId, amount);
        }

        /// <summary>
        /// 캐릭터 보상 생성
        /// </summary>
        public static RewardInfo Character(string characterId)
        {
            return new RewardInfo(RewardType.Character, characterId, 1);
        }

        /// <summary>
        /// 플레이어 경험치 보상 생성
        /// </summary>
        public static RewardInfo PlayerExp(int amount)
        {
            return new RewardInfo(RewardType.PlayerExp, string.Empty, amount);
        }

        public override string ToString()
        {
            return $"[{Type}] {ItemId} x{Amount}";
        }
    }
}
