namespace Sc.Foundation
{
    /// <summary>
    /// 에러 코드 열거형
    /// </summary>
    /// <remarks>
    /// 번호 대역:
    /// - 0: 에러 없음
    /// - 1000~1999: System (초기화, 설정)
    /// - 2000~2999: Network (연결, 타임아웃)
    /// - 3000~3999: Data (저장/로드, 파싱)
    /// - 4000~4999: Auth (로그인, 인증)
    /// - 5000~5999: Game (재화, 조건)
    /// - 6000~6999: UI (화면 로드)
    /// - 1100~1199: Asset (에셋 로딩)
    /// </remarks>
    public enum ErrorCode
    {
        None = 0,

        // System (1000~)
        SystemInitFailed = 1001,
        ConfigLoadFailed = 1002,
        InitializationFailed = 1003,
        InitStepFailed = 1004,

        // Asset (1100~)
        AssetNotFound = 1100,
        AssetLoadTimeout = 1101,
        AssetLoadPartialFail = 1102,
        AddressablesInitFailed = 1103,

        // Network (2000~)
        NetworkDisconnected = 2001,
        NetworkTimeout = 2002,
        ServerError = 2003,
        InvalidResponse = 2004,

        // Data (3000~)
        SaveFailed = 3001,
        LoadFailed = 3002,
        ParseFailed = 3003,
        MigrationFailed = 3004,

        // Auth (4000~)
        LoginFailed = 4001,
        SessionExpired = 4002,
        InvalidToken = 4003,

        // Game (5000~)
        InsufficientGold = 5001,
        InsufficientGem = 5002,
        InsufficientStamina = 5003,
        InventoryFull = 5004,
        LevelNotMet = 5005,
        AlreadyOwned = 5006,
        PurchaseLimitReached = 5007,
        StageNotCleared = 5008,

        // UI (6000~)
        ScreenLoadFailed = 6001,
        PopupLoadFailed = 6002
    }
}