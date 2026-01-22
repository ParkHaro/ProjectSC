# Sc.Contents.Lobby

## 개요
메인 로비 및 메뉴 시스템 (OutGame)

로비는 게임의 중앙 허브로서:
- 각 컨텐츠(가챠, 캐릭터, 이벤트, 상점 등)로의 진입점 제공
- 로그인 후 자동 실행 작업(출석체크, 재화 전환, 알림) 처리
- 유저 상태 정보 표시

## 레퍼런스
- `Docs/Design/Reference/Lobby.jpg`

---

## UI 레이아웃 구조

### 전체 구조

```
LobbyScreen (FullScreen)
├─ ScreenHeader ─────────────────────────────────────────────────────────
│   ├─ [Left] Profile (레벨, 닉네임, EXP)
│   └─ [Right] CurrencyHUD (스태미나, 골드, 프리미엄) + Settings + Mail
│
├─ LeftTopArea ──────────────────────┬─ RightTopArea ────────────────────
│   ├─ EventBannerCarousel           │   ├─ StageProgressWidget
│   │   └─ Slides (좌우 스와이프)    │   │   └─ "11-10 최후의 방어선!"
│   └─ PassButtonGroup               │   └─ QuickMenuGrid (2x4)
│       ├─ LevelPass                 │       ├─ EventPopup, Farm, Friend, Quest
│       ├─ StoryPass                 │       └─ PowerUp, Monthly, Return, ...
│       ├─ TrialPass                 │
│       └─ StepUpPackage             │
│                                    │
├─ CenterArea ───────────────────────┼─ RightBottomArea ─────────────────
│   ├─ CharacterDisplay              │   └─ InGameContentDashboard
│   │   ├─ CharacterImage (터치)     │       ├─ StageShortcut (11-1 바로가자!)
│   │   └─ DialogueText              │       ├─ CharacterMini
│   └─ CharacterSwitch (< >)         │       └─ AdventureButton (모험)
│                                    │
└─ BottomNav ────────────────────────┴───────────────────────────────────
    └─ ContentButtonGroup (스크롤 가능)
        ├─ GachaButton (모집)
        ├─ CashShopButton (캐시상점)
        ├─ ShopButton (상점)
        ├─ CharacterButton (사도)
        ├─ CardButton (카드)
        ├─ TheaterButton (극장)
        └─ GuildButton (고단)
```

### 영역별 상세

#### 1. ScreenHeader (별도 컴포넌트)
| 위치 | 요소 | 설명 |
|------|------|------|
| Left | ProfileWidget | 레벨, 닉네임, EXP 버튼 |
| Right | CurrencyHUD | 스태미나(102/102), 골드(549,061), 프리미엄(1,809) |
| Right | SettingsButton | 설정 |
| Right | MailButton | 메일 (배지 표시) |

#### 2. LeftTopArea (좌상단)
| 요소 | 설명 |
|------|------|
| **EventBannerCarousel** | 이벤트 배너 슬라이드 (좌우 스와이프) |
| **PassButtonGroup** | 패스/상품 버튼 (4개) |
| - LevelPass | 레벨패스 |
| - StoryPass | 사록패스 |
| - TrialPass | 트라이얼패스 |
| - StepUpPackage | 스텝업패키지 |

#### 3. RightTopArea (우상단)
| 요소 | 설명 |
|------|------|
| **StageProgressWidget** | 현재 스테이지 진행 표시 (11-10 최후의 방어선! 알프트반선!) |
| **QuickMenuGrid** | 퀵메뉴 아이콘 그리드 (2행 x 4열) |
| - Row 1 | 이벤트팝업, 평일농장, 친구, 퀘스트 |
| - Row 2 | 빠르게강해지기, 많이하는달, 복귀환영, ... |

#### 4. CenterArea (중앙)
| 요소 | 설명 |
|------|------|
| **CharacterDisplay** | 메인 캐릭터 일러스트 (터치 시 상호작용) |
| **DialogueText** | 캐릭터 대사 ("내가 놀고 싶을 때 놀러 오고...") |
| **CharacterSwitch** | 좌우 화살표 (< >) 캐릭터 전환 |

#### 5. RightBottomArea (우하단)
| 요소 | 설명 |
|------|------|
| **InGameContentDashboard** | 인게임 컨텐츠 대시보드 |
| - StageShortcut | 스테이지 바로가기 (11-1 바로 가자!) |
| - CharacterMini | 파티 캐릭터 미니 이미지 |
| - AdventureButton | "모험" 버튼 → StageSelectScreen |

#### 6. BottomNav (하단 네비게이션)
| 버튼 | 라벨 | 이동 화면 |
|------|------|----------|
| GachaButton | 모집 | GachaScreen |
| CashShopButton | 캐시상점 | (TBD) |
| ShopButton | 상점 | ShopScreen |
| CharacterButton | 사도 | CharacterListScreen |
| CardButton | 카드 | (TBD) |
| TheaterButton | 극장 | (TBD) |
| GuildButton | 고단 | (TBD) |

---

### Prefab 계층 구조

```
LobbyScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   └─ ScreenHeader [Prefab]
│   │
│   ├─ Content (Stretch, Top=80)
│   │   ├─ LeftTopArea (Anchor: TopLeft, 400x300)
│   │   │   ├─ EventBannerCarousel
│   │   │   │   ├─ BannerContainer (HorizontalScroll)
│   │   │   │   └─ Indicators
│   │   │   └─ PassButtonGroup (HorizontalLayoutGroup)
│   │   │       └─ PassButton x4
│   │   │
│   │   ├─ RightTopArea (Anchor: TopRight, 350x250)
│   │   │   ├─ StageProgressWidget
│   │   │   │   ├─ StageLabel
│   │   │   │   └─ ProgressBar (선택)
│   │   │   └─ QuickMenuGrid (GridLayoutGroup 4x2)
│   │   │       └─ QuickMenuButton x8
│   │   │
│   │   ├─ CenterArea (Anchor: Center, Stretch)
│   │   │   ├─ CharacterDisplay
│   │   │   │   ├─ CharacterImage
│   │   │   │   └─ DialogueBox
│   │   │   │       └─ DialogueText
│   │   │   ├─ LeftArrow (Button)
│   │   │   └─ RightArrow (Button)
│   │   │
│   │   └─ RightBottomArea (Anchor: BottomRight, 200x180)
│   │       └─ InGameContentDashboard
│   │           ├─ StageShortcutButton
│   │           ├─ CharacterMiniGroup
│   │           └─ AdventureButton
│   │
│   └─ BottomNav (Bottom, 100px)
│       └─ ContentButtonGroup (HorizontalLayoutGroup, Scroll)
│           └─ ContentButton x7
│
└─ OverlayLayer
```

---

### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| LeftTop | EventBannerCarousel | `_eventBannerCarousel` |
| LeftTop | PassButtonGroup | `_passButtons` |
| RightTop | StageProgressWidget | `_stageProgressWidget` |
| RightTop | QuickMenuGrid | `_quickMenuButtons` |
| Center | CharacterDisplay | `_characterDisplay` |
| Center | DialogueText | `_dialogueText` |
| Center | LeftArrow/RightArrow | `_leftArrow`, `_rightArrow` |
| RightBottom | InGameContentDashboard | `_inGameDashboard` |
| BottomNav | ContentButtonGroup | `_contentButtons` |

---

### 네비게이션 흐름

```
LobbyScreen
├─ GachaButton → GachaScreen
├─ ShopButton → ShopScreen
├─ CharacterButton → CharacterListScreen
├─ AdventureButton → StageSelectScreen
├─ StageShortcut → StageSelectScreen (특정 스테이지)
├─ EventBanner → LiveEventScreen / EventDetailScreen
├─ QuickMenu → 각 기능별 Screen/Popup
└─ ProfileWidget → (TBD) ProfileScreen
```

---

## 참조
- Sc.Common (UI, Popup, Services)
- Sc.Core (DataManager, NetworkManager)
- Sc.Event (EventManager, Events)
- Sc.Data (UserSaveData, RewardInfo)

---

## 구조

```
Contents/OutGame/Lobby/
├── LobbyScreen.cs           # 로비 메인 화면 (ScreenWidget)
├── LobbyState.cs            # 화면 상태
├── ILobbyEntryTask.cs       # 진입 후처리 인터페이스
├── LobbyEntryTaskRunner.cs  # Task 실행기
└── Tasks/
    ├── AttendanceCheckTask.cs         # 출석체크 (Stub)
    ├── EventCurrencyConversionTask.cs # 이벤트 재화 전환
    └── NewEventNotificationTask.cs    # 신규 이벤트 알림 (Stub)

Common/UI/Services/
└── PopupQueueService.cs     # 팝업 큐잉 서비스 (재사용 가능)

Event/OutGame/
└── LobbyEvents.cs           # 로비 관련 이벤트
```

---

## 클래스 목록

### UI

| 클래스 | 설명 | 상태 |
|--------|------|------|
| LobbyScreen | 로비 메인 화면 (ScreenWidget<LobbyScreen, LobbyState>) | ✅ |
| LobbyState | 화면 상태 (ActiveTabIndex 등) | ✅ |

### 로비 진입 후처리 시스템

| 클래스 | 설명 | 상태 |
|--------|------|------|
| ILobbyEntryTask | Task 인터페이스 | ✅ |
| IPopupQueueService | 팝업 큐 인터페이스 | ✅ |
| LobbyTaskResult | Task 결과 DTO | ✅ |
| LobbyEntryTaskRunner | Task 순차 실행기 | ✅ |
| AttendanceCheckTask | 출석체크 (Priority: 10) | ✅ Stub |
| EventCurrencyConversionTask | 재화 전환 (Priority: 20) | ✅ |
| NewEventNotificationTask | 신규 이벤트 알림 (Priority: 30) | ✅ Stub |

### 공통 서비스

| 클래스 | 설명 | 상태 |
|--------|------|------|
| PopupQueueService | 팝업 순차 표시 서비스 | ✅ |

---

## 로비 진입 후처리 시스템

### 개요

로그인 완료 후 또는 로비 화면 진입 시 자동으로 실행되는 작업들을 관리합니다.

**실행 시점:**
- 로그인 직후 1회 (세션당)
- LobbyScreen.OnShow() 시 Background Refresh (CheckRequired로 중복 방지)

**설계 원칙:**
- Continue on Failure: 하나의 Task가 실패해도 다음 Task 계속 실행
- 팝업 큐잉: 여러 Task 결과를 순차적으로 팝업 표시
- 확장성: 새 Task 추가 시 인터페이스 구현만으로 가능

---

### ILobbyEntryTask 인터페이스

```csharp
public interface ILobbyEntryTask
{
    /// <summary>Task 이름 (로깅용)</summary>
    string TaskName { get; }

    /// <summary>실행 우선순위 (낮을수록 먼저 실행)</summary>
    int Priority { get; }

    /// <summary>
    /// 이 Task가 실행 필요한지 체크.
    /// false 반환 시 스킵 (예: 오늘 이미 출석체크 완료)
    /// </summary>
    UniTask<bool> CheckRequiredAsync();

    /// <summary>
    /// Task 실행.
    /// 실패해도 다음 Task는 계속 실행됨.
    /// </summary>
    UniTask<Result<LobbyTaskResult>> ExecuteAsync();
}
```

---

### LobbyTaskResult

```csharp
public class LobbyTaskResult
{
    /// <summary>팝업 표시 여부</summary>
    public bool ShouldShowPopup { get; init; }

    /// <summary>보상 정보 (RewardPopup용)</summary>
    public RewardInfo[] Rewards { get; init; }

    /// <summary>팝업 제목</summary>
    public string PopupTitle { get; init; }

    /// <summary>팝업 메시지 (알림용)</summary>
    public string PopupMessage { get; init; }

    // Factory methods
    public static LobbyTaskResult WithRewards(string title, RewardInfo[] rewards);
    public static LobbyTaskResult WithNotification(string title, string message);
    public static LobbyTaskResult Empty();
}
```

---

### LobbyEntryTaskRunner

```csharp
public class LobbyEntryTaskRunner
{
    private readonly List<ILobbyEntryTask> _tasks = new();
    private readonly PopupQueueService _popupQueue;
    private bool _isRunning;

    public LobbyEntryTaskRunner(PopupQueueService popupQueue);

    /// <summary>Task 등록 (Priority 순 자동 정렬)</summary>
    public void RegisterTask(ILobbyEntryTask task);

    /// <summary>
    /// 모든 Task 순차 실행.
    /// - CheckRequiredAsync() → 실행 여부 판단
    /// - ExecuteAsync() → 실행 (실패해도 계속)
    /// - 결과를 PopupQueue에 추가
    /// </summary>
    public async UniTask<TaskRunnerSummary> ExecuteAllAsync();
}

public class TaskRunnerSummary
{
    public int TotalTasks { get; init; }
    public int ExecutedTasks { get; init; }
    public int SkippedTasks { get; init; }
    public int FailedTasks { get; init; }
}
```

---

### PopupQueueService

```csharp
/// <summary>
/// 팝업 순차 표시 서비스.
/// 여러 시스템에서 재사용 가능 (로비, 상점, 가챠 등).
/// </summary>
public class PopupQueueService
{
    private readonly Queue<IPopupRequest> _queue = new();
    private bool _isProcessing;

    /// <summary>보상 팝업 큐에 추가</summary>
    public void EnqueueReward(string title, RewardInfo[] rewards);

    /// <summary>알림 팝업 큐에 추가</summary>
    public void EnqueueNotification(string title, string message);

    /// <summary>큐의 모든 팝업 순차 표시</summary>
    public async UniTask ProcessQueueAsync();

    /// <summary>대기 중인 팝업 모두 제거</summary>
    public void Clear();
}
```

---

### Task 구현

#### AttendanceCheckTask (Priority: 10) - Stub

```csharp
public class AttendanceCheckTask : ILobbyEntryTask
{
    public string TaskName => "출석 체크";
    public int Priority => 10;

    public async UniTask<bool> CheckRequiredAsync()
    {
        // TODO: 오늘 이미 출석체크했는지 확인
        // UserSaveData.LastAttendanceDate 비교
        return false; // Stub: 항상 스킵
    }

    public async UniTask<Result<LobbyTaskResult>> ExecuteAsync()
    {
        // TODO: 실제 출석 로직 구현
        // 1. 서버에 출석 요청
        // 2. 보상 수령
        // 3. UserSaveData 업데이트
        return Result<LobbyTaskResult>.Success(LobbyTaskResult.Empty());
    }
}
```

#### EventCurrencyConversionTask (Priority: 20)

```csharp
public class EventCurrencyConversionTask : ILobbyEntryTask
{
    private readonly EventCurrencyConverter _converter;
    private readonly DataManager _dataManager;

    public string TaskName => "이벤트 재화 전환";
    public int Priority => 20;

    public async UniTask<bool> CheckRequiredAsync()
    {
        // 만료된 이벤트 재화가 있는지 확인
        return true; // 항상 체크 (converter 내부에서 판단)
    }

    public async UniTask<Result<LobbyTaskResult>> ExecuteAsync()
    {
        var userData = _dataManager.UserData;
        var results = _converter.ConvertExpiredCurrencies(ref userData);

        if (results.Count == 0)
            return Result<LobbyTaskResult>.Success(LobbyTaskResult.Empty());

        // 저장 및 서버 동기화
        await SaveManager.Instance.SaveAsync(userData);

        // 보상 정보로 변환
        var rewards = results.Select(r => new RewardInfo
        {
            Type = RewardType.Currency,
            Id = r.TargetCurrencyId,
            Amount = r.TargetAmount
        }).ToArray();

        return Result<LobbyTaskResult>.Success(
            LobbyTaskResult.WithRewards("이벤트 재화 전환", rewards)
        );
    }
}
```

#### NewEventNotificationTask (Priority: 30) - Stub

```csharp
public class NewEventNotificationTask : ILobbyEntryTask
{
    public string TaskName => "신규 이벤트 알림";
    public int Priority => 30;

    public async UniTask<bool> CheckRequiredAsync()
    {
        // TODO: 마지막 로그인 이후 시작된 새 이벤트 있는지 확인
        return false; // Stub: 항상 스킵
    }

    public async UniTask<Result<LobbyTaskResult>> ExecuteAsync()
    {
        // TODO: 새 이벤트 목록 조회 및 알림
        return Result<LobbyTaskResult>.Success(LobbyTaskResult.Empty());
    }
}
```

---

### LobbyScreen 통합

```csharp
public class LobbyScreen : ScreenWidget<LobbyScreen, LobbyState>
{
    private LobbyEntryTaskRunner _taskRunner;
    private bool _initialTasksCompleted;

    protected override void OnInitialize()
    {
        // 기존 버튼 설정...

        // Task Runner 초기화
        var popupQueue = new PopupQueueService();
        _taskRunner = new LobbyEntryTaskRunner(popupQueue);

        // Task 등록
        _taskRunner.RegisterTask(new AttendanceCheckTask());
        _taskRunner.RegisterTask(new EventCurrencyConversionTask(
            new EventCurrencyConverter(...),
            DataManager.Instance
        ));
        _taskRunner.RegisterTask(new NewEventNotificationTask());
    }

    protected override async void OnShow()
    {
        base.OnShow();

        // DataManager 구독
        DataManager.Instance.OnUserDataChanged += OnUserDataChanged;

        // 로비 진입 후처리 Task 실행
        await RunLobbyEntryTasksAsync();

        RefreshUI();
    }

    private async UniTask RunLobbyEntryTasksAsync()
    {
        var summary = await _taskRunner.ExecuteAllAsync();

        Log.Info($"[LobbyScreen] Tasks completed: " +
            $"Executed={summary.ExecutedTasks}, " +
            $"Skipped={summary.SkippedTasks}, " +
            $"Failed={summary.FailedTasks}",
            LogCategory.Lobby);

        // 팝업 큐 처리
        await _taskRunner.PopupQueue.ProcessQueueAsync();
    }
}
```

---

### 이벤트 정의

```csharp
// Event/OutGame/LobbyEvents.cs

/// <summary>로비 진입 후처리 Task 완료 이벤트</summary>
public readonly struct LobbyEntryTasksCompletedEvent
{
    public int TotalTasks { get; init; }
    public int ExecutedTasks { get; init; }
    public int FailedTasks { get; init; }
}

/// <summary>개별 Task 완료 이벤트 (로깅/분석용)</summary>
public readonly struct LobbyEntryTaskCompletedEvent
{
    public string TaskName { get; init; }
    public bool IsSuccess { get; init; }
    public float ExecutionTimeMs { get; init; }
}
```

---

## 데이터 흐름

```
로그인 완료
    ↓
LobbyScreen.OnShow()
    ↓
LobbyEntryTaskRunner.ExecuteAllAsync()
    ↓
┌─────────────────────────────────────────────┐
│ For each Task (sorted by Priority):        │
│   1. CheckRequiredAsync() → Skip if false  │
│   2. ExecuteAsync() → Log error, continue  │
│   3. If result.ShouldShowPopup:            │
│      → EnqueueReward/Notification          │
└─────────────────────────────────────────────┘
    ↓
PopupQueueService.ProcessQueueAsync()
    ↓
순차적 팝업 표시 (사용자가 닫을 때까지 대기)
```

---

## 구현 Phase

### Phase A: 인터페이스 및 Runner (Core) ✅
- [x] ILobbyEntryTask.cs
- [x] IPopupQueueService.cs
- [x] LobbyTaskResult.cs
- [x] LobbyEntryTaskRunner.cs
- [x] LobbyEvents.cs

### Phase B: PopupQueueService (Common) ✅
- [x] PopupQueueService.cs (IPopupRequest 내부 포함)

### Phase C: Task 구현 ✅
- [x] AttendanceCheckTask.cs (Stub)
- [x] EventCurrencyConversionTask.cs (Full)
- [x] NewEventNotificationTask.cs (Stub)

### Phase D: 통합 ✅
- [x] LobbyScreen.OnShow() 수정
- [x] Task 등록 및 실행 연결
- [x] DataManager 확장 (GetUserDataCopy, UpdateUserData)

---

## 확장 포인트

### 새 Task 추가

1. `ILobbyEntryTask` 인터페이스 구현
2. `LobbyEntryTaskRunner.RegisterTask()` 호출
3. Priority 값으로 실행 순서 제어

### PopupQueueService 다른 곳에서 사용

```csharp
// Shop에서 구매 완료 후 보상 표시
var queue = new PopupQueueService();
queue.EnqueueReward("구매 완료", purchasedRewards);
await queue.ProcessQueueAsync();
```

---

## 참조 문서

| 문서 | 내용 |
|------|------|
| [InitializationSequence](../ARCHITECTURE.md#initialization) | 유사한 순차 실행 패턴 |
| [EventCurrencyConverter](LiveEvent.md#재화-전환) | 재화 전환 로직 |
| [RewardPopup](Common/Popups/RewardPopup.md) | 보상 표시 팝업 |
