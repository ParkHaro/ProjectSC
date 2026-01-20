using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 재화 정책
    /// 유예기간, 범용 재화 전환 비율 등
    /// </summary>
    [Serializable]
    public struct EventCurrencyPolicy
    {
        /// <summary>
        /// 이벤트 재화 ID
        /// </summary>
        public string CurrencyId;

        /// <summary>
        /// StringData 키 (재화 이름)
        /// </summary>
        public string CurrencyNameKey;

        /// <summary>
        /// 아이콘 경로
        /// </summary>
        public string CurrencyIcon;

        /// <summary>
        /// 유예 기간 (일). 이벤트 종료 후 N일간 상점 이용 가능.
        /// </summary>
        public int GracePeriodDays;

        /// <summary>
        /// 전환 대상 재화 ID (예: "gold")
        /// </summary>
        public string ConvertToCurrencyId;

        /// <summary>
        /// 전환 비율 (예: 10.0 = 1토큰당 10골드)
        /// </summary>
        public float ConversionRate;
    }
}
