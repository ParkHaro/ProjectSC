namespace Sc.Common.UI
{
    /// <summary>
    /// 로딩 UI 타입
    /// </summary>
    public enum LoadingType
    {
        /// <summary>
        /// 전체 화면 로딩 (씬 전환, 대용량 로드)
        /// 입력 차단, 어두운 오버레이
        /// </summary>
        FullScreen,

        /// <summary>
        /// 작은 인디케이터 (API 호출, 짧은 대기)
        /// 입력 차단 없음, 우측 상단 표시
        /// </summary>
        Indicator,

        /// <summary>
        /// 진행률 표시 (다운로드, 리소스 로드)
        /// 프로그레스 바 포함
        /// </summary>
        Progress
    }
}
