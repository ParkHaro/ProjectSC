namespace Sc.Foundation
{
    /// <summary>
    /// 로그 카테고리 (도메인별 분류)
    /// </summary>
    public enum LogCategory
    {
        /// <summary>초기화, 종료, 일반 시스템</summary>
        System,

        /// <summary>API 요청/응답, 연결</summary>
        Network,

        /// <summary>데이터 로드/저장, Delta 적용</summary>
        Data,

        /// <summary>화면 전환, 팝업</summary>
        UI,

        /// <summary>전투 로직</summary>
        Battle,

        /// <summary>가챠 결과</summary>
        Gacha
    }
}
