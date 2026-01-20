---
type: spec
assembly: Sc.Contents.Stage
category: System
status: draft
version: "3.1"
dependencies: [Sc.Common, Sc.Packet, Sc.Data, Sc.Event, Sc.Contents.Character]
created: 2026-01-17
updated: 2026-01-20
changelog:
  - "3.1: Dungeon → StageCategory 용어 변경, Content Module 구현 완료"
  - "3.0: 컨텐츠 모듈 패턴 설계"
---

# Sc.Contents.Stage

## 목적

인게임 전투(Stage) 선택, 파티 편성, 전투 시작까지의 아웃게임 → 인게임 브릿지 시스템

## 핵심 개념

| 용어 | 정의 | 예시 |
|------|------|------|
| **Stage** | 인게임 전투 **한 판** | 1-1, 1-2, 보스전, 일일던전 1층 |
| **InGameContent** | 전투 컨텐츠 **대분류** | 메인스토리, 골드던전, 경험치던전, 보스레이드 |
| **StageCategory** | 컨텐츠 내 **세부 분류** | 불속성, 물속성, 1장, 2장 |

---

## 의존성

### 참조
- `Sc.Common` - UI 시스템, Navigation, Widget
- `Sc.Packet` - NetworkManager, Request/Response
- `Sc.Data` - 마스터/유저 데이터
- `Sc.Event` - 이벤트 발행
- `Sc.Contents.Character` - 캐릭터 정보, 파티 편성

### 참조됨
- `Sc.Contents.Lobby` - InGameContentDashboard 진입
- `Sc.Contents.Battle` - 전투 시스템 (BattleReadyEvent 수신)
- `Sc.Contents.Event` - 이벤트 스테이지 (EventStageContentModule 사용)

---

## 화면 계층 구조

```
Lobby
  │
  └─> InGameContentDashboard (컨텐츠 종류 선택)
        │
        │  ┌─────────────────────────────────────────────────────┐
        │  │ 컨텐츠에 따라 StageDashboard 유무 결정               │
        │  │ - 메인스토리: StageDashboard 스킵                   │
        │  │ - 골드/경험치던전: StageDashboard 필요 (카테고리 선택)│
        │  └─────────────────────────────────────────────────────┘
        │
        ├─[메인스토리]────────────────────> StageSelectScreen
        │                                   + MainStoryContentModule
        │
        ├─[골드던전]──> StageDashboard ──> StageSelectScreen
        │               (카테고리 선택)      + ElementDungeonContentModule
        │
        ├─[경험치던전]─> StageDashboard ─> StageSelectScreen
        │               (카테고리 선택)      + ExpDungeonContentModule
        │
        ├─[보스레이드]─> StageDashboard ─> StageSelectScreen
        │               (카테고리 선택)      + BossRaidContentModule
        │
        └─[무한의탑]────────────────────> StageSelectScreen
                                          + TowerContentModule
```

### 이벤트 스테이지 연동

```
LiveEventScreen
  │
  └─> EventDetailScreen
        │
        └─[스테이지 탭]─────────────────> StageSelectScreen
                                          + EventStageContentModule
```

---

## UI 아키텍처 (컴포지션 패턴)

### StageSelectScreen 구조

```
┌─────────────────────────────────────────────────────────────────┐
│                     StageSelectScreen                            │
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                    Header (공통)                             ││
│  │  [←] 스테이지 선택                       남은 입장: 3/5      ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │              Custom Content Area (확장 영역)                 ││
│  │     ← IStageContentModule이 UI를 생성하는 영역 →            ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                 StageListPanel (공통)                        ││
│  │  ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐                           ││
│  │  │ 1-1 │ │ 1-2 │ │ 1-3 │ │ 1-4 │  ...                      ││
│  │  │ ★★★ │ │ ★★☆ │ │ ☆☆☆ │ │ 🔒  │                           ││
│  │  └─────┘ └─────┘ └─────┘ └─────┘                           ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                    Footer (공통)                             ││
│  │  총 보상: 💰1000  💎10              [소탕] [입장]            ││
│  └─────────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────────┘
```

### 컨텐츠별 모듈 (IStageContentModule)

| 모듈 | Custom Content Area 내용 |
|------|--------------------------|
| **MainStoryContentModule** | 챕터 탭 `[1장][2장][3장🔒]`, 스토리 진행도 |
| **ElementDungeonContentModule** | 속성 아이콘 🔥, 권장 속성 안내 💧 |
| **ExpDungeonContentModule** | 난이도 표시, 획득 경험치 미리보기 |
| **BossRaidContentModule** | 보스 HP 게이지, 내 기여도, 랭킹 버튼 |
| **TowerContentModule** | 현재 층, 최고 층, 보상 미리보기 |
| **EventStageContentModule** | 이벤트 이름, 남은 기간, 이벤트 재화 |

---

## 클래스 역할 정의

### 화면 (Screen)

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `InGameContentDashboard` | 컨텐츠 종류 선택 화면 | 컨텐츠 목록 표시, 진입 처리 | 스테이지 표시 |
| `StageDashboard` | 세부 분류 선택 화면 | 속성/난이도/보스 선택 | 스테이지 표시 |
| `StageSelectScreen` | 스테이지 선택 화면 | 공통 UI + 모듈 조합, 스테이지 목록 | 컨텐츠별 특수 로직 |
| `PartySelectScreen` | 파티 편성 화면 | 캐릭터 선택, 프리셋 관리, 전투 진입 | 전투 로직 |

### 패널/위젯 (Panel/Widget)

| 클래스 | 역할 | 책임 |
|--------|------|------|
| `StageListPanel` | 스테이지 목록 패널 | 스테이지 아이템 생성/관리, 스크롤 |
| `StageItemWidget` | 개별 스테이지 위젯 | 스테이지 정보 표시, 클릭 이벤트 |
| `ContentCategoryItem` | 컨텐츠 카테고리 아이템 | 컨텐츠 정보 표시 (Dashboard용) |

### 모듈 (Module)

| 인터페이스 | 역할 |
|------------|------|
| `IStageContentModule` | 컨텐츠별 확장 UI 인터페이스 |
| `BaseStageContentModule` | 모듈 공통 로직 (Template Method Pattern) |
| `StageContentModuleFactory` | 컨텐츠 타입별 모듈 생성 팩토리 |

```csharp
public interface IStageContentModule
{
    event Action<string> OnCategoryChanged;  // 카테고리 변경 이벤트
    void Initialize(Transform container, InGameContentType contentType);
    void SetCategoryId(string categoryId);   // 외부에서 카테고리 설정
    void Refresh(string selectedStageId);
    void OnStageSelected(StageData stageData);
    void Release();
}
```

### 팝업 (Popup)

| 클래스 | 역할 |
|--------|------|
| `StageInfoPopup` | 스테이지 상세 정보, Star 조건, 보상 표시 |

---

## 마스터 데이터

### InGameContentType

**위치**: `Assets/Scripts/Data/Enums/InGameContentType.cs`

```csharp
public enum InGameContentType
{
    MainStory,      // 메인 스토리
    HardMode,       // 하드 모드
    GoldDungeon,    // 골드 던전
    ExpDungeon,     // 경험치 던전
    SkillDungeon,   // 스킬 재화 던전
    BossRaid,       // 보스 레이드
    Tower,          // 무한의 탑
    Event,          // 이벤트 스테이지
}
```

### StageType (기존 확장)

**위치**: `Assets/Scripts/Data/Enums/StageType.cs`

```csharp
public enum StageType
{
    Normal,         // 일반 스테이지
    Boss,           // 보스 스테이지
    Challenge,      // 챌린지 스테이지
    Hidden,         // 히든 스테이지
}
```

### StarConditionType

**위치**: `Assets/Scripts/Data/Enums/StarConditionType.cs`

```csharp
public enum StarConditionType
{
    Clear,              // 클리어
    TurnLimit,          // N턴 이내 클리어
    NoCharacterDeath,   // 사망자 없이 클리어
    FullHP,             // 아군 전원 HP 100%
    ElementAdvantage,   // 유리 속성으로 클리어
}
```

### StageData

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageData.cs`

```csharp
[CreateAssetMenu(fileName = "StageData", menuName = "SC/Data/Stage")]
public class StageData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public InGameContentType ContentType;
    public string CategoryId;           // 속하는 카테고리 ID (속성/챕터 등)
    public StageType StageType;
    public int Chapter;
    public int StageNumber;
    public Difficulty Difficulty;

    [Header("입장 조건")]
    public CostType EntryCostType;      // 입장 재화 타입
    public int EntryCost;               // 입장 비용
    public LimitType LimitType;         // 입장 제한 타입
    public int LimitCount;              // 제한 횟수
    public DayOfWeek[] AvailableDays;   // 요일 제한 (일일 던전용)

    [Header("해금 조건")]
    public string UnlockConditionStageId;  // 선행 스테이지
    public int UnlockConditionLevel;       // 필요 레벨

    [Header("전투 정보")]
    public int RecommendedPower;        // 추천 전투력
    public string[] EnemyIds;           // 적 캐릭터 ID 목록

    [Header("보상 (레거시)")]
    public int RewardGold;
    public int RewardExp;

    [Header("보상 (신규)")]
    public List<RewardInfo> FirstClearRewards;
    public List<RewardInfo> RepeatClearRewards;

    [Header("별점 조건")]
    public StarCondition Star1Condition;
    public StarCondition Star2Condition;
    public StarCondition Star3Condition;

    [Header("표시")]
    public int DisplayOrder;
    public bool IsEnabled;
}
```

### StageDatabase

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageDatabase.cs`

```csharp
[CreateAssetMenu(fileName = "StageDatabase", menuName = "SC/Database/Stage")]
public class StageDatabase : ScriptableObject
{
    [SerializeField] private List<StageData> _stages;

    public StageData GetById(string id);
    public IEnumerable<StageData> GetByContentType(InGameContentType contentType);
    public IEnumerable<StageData> GetByContentTypeAndCategory(InGameContentType contentType, string categoryId);
    public IEnumerable<StageData> GetByCategory(string categoryId);
    public IEnumerable<StageData> GetByEvent(string eventId);
}
```

### StageCategoryData

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageCategoryData.cs`

```csharp
[CreateAssetMenu(fileName = "StageCategoryData", menuName = "SC/Data/StageCategory")]
public class StageCategoryData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public InGameContentType ContentType;
    public string NameKey;
    public string DescriptionKey;
    public Sprite IconSprite;

    [Header("컨텐츠별 특화 필드")]
    public Element Element;         // 속성 던전용
    public Difficulty Difficulty;   // 난이도 던전용
    public int ChapterNumber;       // 메인스토리 챕터용

    [Header("표시")]
    public int DisplayOrder;
    public bool IsEnabled;
}
```

### StageCategoryDatabase

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageCategoryDatabase.cs`

```csharp
[CreateAssetMenu(fileName = "StageCategoryDatabase", menuName = "SC/Database/StageCategory")]
public class StageCategoryDatabase : ScriptableObject
{
    [SerializeField] private List<StageCategoryData> _categories;

    public StageCategoryData GetById(string id);
    public IEnumerable<StageCategoryData> GetByContentType(InGameContentType contentType);
    public List<StageCategoryData> GetSortedByContentType(InGameContentType contentType);
    public StageCategoryData GetByElement(InGameContentType contentType, Element element);
    public StageCategoryData GetByChapter(int chapterNumber);
}
```

---

## 유저 데이터

### StageClearInfo 확장

**위치**: `Assets/Scripts/Data/Structs/UserData/StageProgress.cs`

```csharp
[Serializable]
public struct StageClearInfo
{
    public string StageId;
    public bool IsCleared;
    public int Stars;               // 0~3
    public bool[] StarAchieved;     // [star1, star2, star3] 개별 달성 여부
    public int BestTurnCount;
    public int ClearCount;
    public long FirstClearedAt;
    public long LastClearedAt;
}
```

### StageEntryRecord (NEW)

**위치**: `Assets/Scripts/Data/Structs/UserData/StageEntryRecord.cs`

```csharp
[Serializable]
public struct StageEntryRecord
{
    public string StageId;
    public int EntryCount;          // 입장 횟수
    public long LastEntryTime;
    public long ResetTime;          // 다음 리셋 시각

    public bool NeedsReset(long currentTime) => currentTime >= ResetTime;
}
```

### PartyPreset

**위치**: `Assets/Scripts/Data/Structs/UserData/PartyPreset.cs`

```csharp
[Serializable]
public struct PartyPreset
{
    public string PresetId;
    public string PresetGroupId;        // "main_story", "gold_dungeon_fire" 등
    public string Name;                 // 유저 지정 이름
    public List<string> CharacterInstanceIds;  // 최대 4~5명
    public long LastModifiedTime;
}
```

### UserSaveData 확장

```csharp
// UserSaveData v5
public Dictionary<string, StageEntryRecord> StageEntryRecords;  // Key: StageId
public List<PartyPreset> PartyPresets;

// Helper 메서드
public StageEntryRecord? FindStageEntryRecord(string stageId);
public void UpdateStageEntryRecord(string stageId, StageEntryRecord record);
public List<PartyPreset> GetPresetsForGroup(string presetGroupId);
public void UpdatePartyPreset(PartyPreset preset);
```

---

## Request/Response

### EnterStageRequest

```csharp
[Serializable]
public struct EnterStageRequest : IRequest
{
    public long Timestamp { get; set; }
    public string StageId;
    public List<string> PartyCharacterIds;
}
```

### EnterStageResponse

```csharp
[Serializable]
public struct EnterStageResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }  // 입장료 차감

    public string BattleSessionId;            // 전투 세션 ID
    public StageEntryRecord EntryRecord;      // 갱신된 입장 기록
}
```

### ClearStageRequest

```csharp
[Serializable]
public struct ClearStageRequest : IRequest
{
    public long Timestamp { get; set; }
    public string BattleSessionId;
    public bool IsVictory;
    public int TurnCount;
    public bool NoCharacterDeath;
    public bool AllFullHP;
}
```

### ClearStageResponse

```csharp
[Serializable]
public struct ClearStageResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }  // 보상 지급

    public StageClearInfo ClearInfo;
    public bool[] NewStarsAchieved;           // 새로 달성한 별
    public List<RewardInfo> TotalRewards;
}
```

---

## Events

### StageEvents.cs

```csharp
// 입장 성공
public readonly struct StageEnteredEvent
{
    public string StageId { get; init; }
    public string BattleSessionId { get; init; }
}

// 입장 실패
public readonly struct StageEntryFailedEvent
{
    public string StageId { get; init; }
    public ErrorCode ErrorCode { get; init; }
    public string ErrorMessage { get; init; }
}

// 클리어 성공
public readonly struct StageClearedEvent
{
    public string StageId { get; init; }
    public bool IsVictory { get; init; }
    public bool IsFirstClear { get; init; }
    public bool[] NewStarsAchieved { get; init; }
    public List<RewardInfo> Rewards { get; init; }
}

// 전투 준비 완료 (Battle 시스템으로 전달)
public readonly struct BattleReadyEvent
{
    public string BattleSessionId { get; init; }
    public StageData StageData { get; init; }
    public List<string> PartyCharacterIds { get; init; }
}
```

---

## LocalServer

### StageEntryValidator

```csharp
public class StageEntryValidator
{
    public bool CanEnter(StageData stage, StageEntryRecord? record, out int remainingCount);
    public StageEntryRecord UpdateEntryRecord(StageData stage, StageEntryRecord? existing);
    public long CalculateNextResetTime(LimitType limitType, long currentTime);
    public bool IsAvailableToday(StageData stage, DayOfWeek today);
}
```

### StageHandler

```csharp
public class StageHandler :
    IRequestHandler<EnterStageRequest, EnterStageResponse>,
    IRequestHandler<ClearStageRequest, ClearStageResponse>
{
    public EnterStageResponse Handle(EnterStageRequest request, ref UserSaveData userData);
    public ClearStageResponse Handle(ClearStageRequest request, ref UserSaveData userData);

    private bool[] EvaluateStarConditions(StageData stage, ClearStageRequest request);
}
```

---

## 에러 코드

| ErrorCode | 값 | 설명 |
|-----------|-----|------|
| `StageNotFound` | 5101 | 스테이지 없음 |
| `StageLocked` | 5102 | 스테이지 잠김 (해금 조건 미충족) |
| `StageInsufficientCost` | 5103 | 입장 재화 부족 |
| `StageEntryLimitExceeded` | 5104 | 입장 제한 초과 |
| `StageInvalidParty` | 5105 | 잘못된 파티 구성 |
| `StageNotAvailableToday` | 5106 | 오늘 이용 불가 (요일 제한) |
| `StageInvalidBattleSession` | 5107 | 잘못된 전투 세션 |

---

## 파일 구조

```
Assets/Scripts/Contents/OutGame/Stage/
├── Sc.Contents.Stage.asmdef
│
├── Screens/
│   ├── InGameContentDashboard.cs
│   ├── StageDashboard.cs        (StageCategoryDatabase 사용)
│   ├── StageSelectScreen.cs     (StageContentModuleFactory 사용)
│   └── PartySelectScreen.cs
│
├── Panels/
│   ├── StageListPanel.cs
│   └── StageItemWidget.cs
│
├── Modules/
│   ├── IStageContentModule.cs
│   ├── BaseStageContentModule.cs     (추상 베이스, Template Method)
│   ├── StageContentModuleFactory.cs  (팩토리, 모듈 생성)
│   ├── MainStoryContentModule.cs     (챕터 탭, 진행도)
│   ├── ElementDungeonContentModule.cs (속성 아이콘, 권장 속성)
│   ├── ExpDungeonContentModule.cs    (TODO)
│   ├── BossRaidContentModule.cs      (TODO)
│   ├── TowerContentModule.cs         (TODO)
│   └── EventStageContentModule.cs    (TODO)
│
├── Popups/
│   ├── StageInfoPopup.cs
│   └── StageInfoState.cs
│
└── (States - Screen 내부 클래스)

Assets/Scripts/Data/ScriptableObjects/
├── StageData.cs                      (ContentType, CategoryId 확장)
├── StageDatabase.cs                  (GetByContentType, GetByCategory 확장)
├── StageCategoryData.cs              (카테고리 마스터 데이터)
└── StageCategoryDatabase.cs          (카테고리 데이터베이스)

Assets/Scripts/Editor/Tests/Stage/
├── StageContentModuleFactoryTests.cs
├── StageDatabaseTests.cs
└── StageCategoryDatabaseTests.cs
```

---

## 구현 체크리스트

```
Phase A: Data Foundation
- [x] InGameContentType.cs
- [x] StageType.cs
- [x] StarConditionType.cs
- [x] StarCondition.cs
- [x] StageData.cs (ContentType, CategoryId, StarConditions 확장)
- [x] StageDatabase.cs (GetByContentType, GetByCategory 등 확장)
- [x] StageCategoryData.cs
- [x] StageCategoryDatabase.cs
- [x] StageClearInfo 확장 (StarAchieved[])
- [x] StageEntryRecord.cs
- [x] PartyPreset.cs
- [x] UserSaveData v6 마이그레이션 (PartyPresets 추가)
- [x] Stage.json 샘플 데이터 (v2.0 - 신규 필드 포함)

Phase B: Request/Response
- [x] EnterStageRequest.cs
- [x] EnterStageResponse.cs
- [x] ClearStageRequest.cs
- [x] ClearStageResponse.cs

Phase C: Events
- [x] StageEvents.cs

Phase D: LocalServer
- [x] StageEntryValidator.cs
- [x] StageHandler.cs
- [x] LocalGameServer.cs 연동

Phase E: UI Screens
- [x] InGameContentDashboard.cs
- [x] StageDashboard.cs (StageCategoryDatabase 연동)
- [x] StageSelectScreen.cs (StageContentModuleFactory 연동)
- [x] PartySelectScreen.cs (플레이스홀더)

Phase F: UI Panels/Widgets
- [x] StageListPanel.cs
- [x] StageItemWidget.cs
- [x] ContentCategoryItem.cs

Phase G: Content Modules
- [x] IStageContentModule.cs (OnCategoryChanged, SetCategoryId 추가)
- [x] BaseStageContentModule.cs (Template Method Pattern)
- [x] StageContentModuleFactory.cs (Factory Pattern)
- [x] MainStoryContentModule.cs (챕터 탭, 진행도)
- [x] ElementDungeonContentModule.cs (속성 아이콘, 권장 속성)
- [x] ExpDungeonContentModule.cs (난이도 표시, 경험치 미리보기)
- [x] BossRaidContentModule.cs (보스 HP, 기여도, 랭킹)
- [x] TowerContentModule.cs (현재/최고 층, 보상 미리보기)
- [x] EventStageContentModule.cs (이벤트 정보, 남은 기간, 이벤트 재화)

Phase H: Popups/States
- [x] StageInfoPopup.cs
- [x] StageInfoState.cs
- [x] StageSelectState.cs (CategoryId 포함)
- [x] StageDashboardState.cs (InitialCategoryId 포함)
- [x] PartySelectState.cs

Phase I: Integration
- [x] LobbyScreen에 [던전] 버튼 추가
- [x] EventDetailScreen Stage 탭 연동 (EventStageTab → StageSelectScreen 네비게이션)
- [x] DataManager StageCategoryDatabase 추가

Phase J: Testing
- [x] StageEntryValidatorTests.cs
- [x] StageHandlerTests.cs
- [x] StageContentModuleFactoryTests.cs
- [x] StageDatabaseTests.cs
- [x] StageCategoryDatabaseTests.cs
```

---

## 관련 문서

- [Data.md](Data.md) - 데이터 구조 개요
- [Packet.md](Packet.md) - 네트워크 패턴
- [Character.md](Character.md) - 캐릭터 시스템
- [LiveEvent.md](LiveEvent.md) - 이벤트 스테이지 연동
- [Common/Reward.md](Common/Reward.md) - 보상 시스템
