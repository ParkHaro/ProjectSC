namespace Sc.Data
{
    /// <summary>
    /// 미션 조건 타입
    /// </summary>
    public enum MissionConditionType
    {
        #region 스테이지 관련

        /// <summary>
        /// 특정 스테이지 클리어
        /// </summary>
        ClearStage,

        /// <summary>
        /// 스테이지 N회 클리어
        /// </summary>
        ClearStageCount,

        /// <summary>
        /// 특정 타입 스테이지 N회
        /// </summary>
        ClearStageType,

        /// <summary>
        /// 이벤트 스테이지 N회 클리어
        /// </summary>
        ClearEventStage,

        #endregion

        #region 가챠/상점 관련

        /// <summary>
        /// 가챠 N회
        /// </summary>
        GachaCount,

        /// <summary>
        /// 구매 N회
        /// </summary>
        PurchaseCount,

        /// <summary>
        /// 이벤트 상점 구매 N회
        /// </summary>
        PurchaseEventShop,

        #endregion

        #region 출석 관련

        /// <summary>
        /// 출석 N일
        /// </summary>
        LoginCount,

        /// <summary>
        /// 연속 출석 N일
        /// </summary>
        LoginConsecutive,

        #endregion

        #region 수집/소비 관련

        /// <summary>
        /// 아이템 N개 수집
        /// </summary>
        CollectItem,

        /// <summary>
        /// 이벤트 아이템 N개 수집
        /// </summary>
        CollectEventItem,

        /// <summary>
        /// 재화 N 소비
        /// </summary>
        SpendCurrency,

        /// <summary>
        /// 이벤트 재화 N 소비
        /// </summary>
        SpendEventCurrency,

        #endregion

        #region 성장 관련

        /// <summary>
        /// 플레이어 레벨 N 달성
        /// </summary>
        ReachLevel,

        /// <summary>
        /// 캐릭터 N명 보유
        /// </summary>
        OwnCharacter,

        /// <summary>
        /// 캐릭터 강화 N회
        /// </summary>
        UpgradeCharacter

        #endregion
    }
}
