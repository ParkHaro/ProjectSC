using System;

namespace Sc.Data
{
    /// <summary>
    /// 유저 재화 데이터
    /// </summary>
    [Serializable]
    public struct UserCurrency
    {
        /// <summary>
        /// 골드 (일반 재화)
        /// </summary>
        public long Gold;

        /// <summary>
        /// 보석 (유료 재화)
        /// </summary>
        public int Gem;

        /// <summary>
        /// 무료 보석
        /// </summary>
        public int FreeGem;

        /// <summary>
        /// 현재 스태미나
        /// </summary>
        public int Stamina;

        /// <summary>
        /// 최대 스태미나
        /// </summary>
        public int MaxStamina;

        /// <summary>
        /// 스태미나 마지막 갱신 시간 (Unix Timestamp)
        /// </summary>
        public long StaminaUpdatedAt;

        /// <summary>
        /// 총 보석 (유료 + 무료)
        /// </summary>
        public int TotalGem => Gem + FreeGem;

        /// <summary>
        /// 기본값으로 초기화된 재화 생성
        /// </summary>
        public static UserCurrency CreateDefault()
        {
            return new UserCurrency
            {
                Gold = 10000,
                Gem = 0,
                FreeGem = 1000,
                Stamina = 100,
                MaxStamina = 100,
                StaminaUpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 재화 차감 가능 여부 확인
        /// </summary>
        public bool CanAfford(CostType costType, int amount)
        {
            return costType switch
            {
                CostType.Gold => Gold >= amount,
                CostType.Gem => TotalGem >= amount,
                CostType.None => true,
                _ => false
            };
        }
    }
}
