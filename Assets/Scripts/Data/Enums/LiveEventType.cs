namespace Sc.Data
{
    /// <summary>
    /// 라이브 이벤트 타입
    /// </summary>
    public enum LiveEventType
    {
        /// <summary>
        /// 기간 한정 이벤트 (일반)
        /// </summary>
        Limited,

        /// <summary>
        /// 스토리 이벤트 (기간 한정 스토리)
        /// </summary>
        Story,

        /// <summary>
        /// 수집 이벤트 (아이템 모으기)
        /// </summary>
        Collection,

        /// <summary>
        /// 레이드 이벤트 (협동 보스)
        /// </summary>
        Raid,

        /// <summary>
        /// 출석 이벤트
        /// </summary>
        Login,

        /// <summary>
        /// 기념 이벤트 (기념일, 업데이트)
        /// </summary>
        Celebration,

        /// <summary>
        /// 콜라보 이벤트
        /// </summary>
        Collab
    }
}
