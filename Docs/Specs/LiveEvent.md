---
type: spec
assembly: Sc.Contents.Event
category: System
status: draft
version: "2.0"
dependencies: [Sc.Common, Sc.Packet, Sc.Data, Sc.Event, Sc.Contents.Stage, Sc.Contents.Shop]
created: 2025-01-14
updated: 2026-01-18
---

# Sc.Contents.Event (라이브 이벤트)

## 목적

라이브 서비스 운영을 위한 기간제 이벤트 시스템. 미션, 상점, 스테이지 등 다양한 컨텐츠를 포함하는 컨테이너.

## 의존성

### 참조
- `Sc.Common` - UI 시스템, Navigation
- `Sc.Packet` - NetworkManager, Request/Response
- `Sc.Data` - 마스터/유저 데이터
- `Sc.Event` - 이벤트 발행
- `Sc.Contents.Shop` - 이벤트 상점 (재사용)
- `Sc.Contents.Stage` - 이벤트 스테이지 (재사용)

### 참조됨
- `Sc.Contents.Lobby` - 이벤트 진입

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **LiveEvent** | 기간제 컨텐츠 컨테이너 (미션, 상점, 스테이지, 미니게임 포함 가능) |
| **EventSubContent** | 이벤트 내 모듈형 서브 컨텐츠 (Mission/Stage/Shop/Minigame) |
| **EventDashboard** | 활성 이벤트 목록 UI (로비에서 진입) |
| **EventMission** | 이벤트 전용 미션 (진행도 + 보상) |
| **EventCurrency** | 이벤트 전용 재화 (유예 기간 후 범용 재화로 전환) |

### 서브컨텐츠 모듈 구조

```
LiveEvent (컨테이너)
    │
    ├─ SubContents[] (모듈 배열)
    │   ├─ [0] Mission (미션 모듈)
    │   │       └─ MissionGroupId 참조
    │   ├─ [1] Stage (스테이지 모듈)
    │   │       └─ StageGroupId + PresetGroupId 참조
    │   ├─ [2] Shop (상점 모듈)
    │   │       └─ ShopCategoryId 참조
    │   └─ [3] Minigame (미니게임 모듈) - 추후
    │
    └─ EventCurrency (이벤트 재화)
            └─ 유예 기간 후 범용 재화 전환
```

---

## 클래스 역할 정의

### 마스터 데이터

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `EventType` | 이벤트 타입 열거형 | 이벤트 분류 | - |
| `EventSubContentType` | 서브컨텐츠 타입 열거형 | 모듈 분류 | - |
| `MissionConditionType` | 미션 조건 타입 | 조건 분류 | 조건 검증 |
| `LiveEventData` | 이벤트 SO | 이벤트 정보 저장, 서브컨텐츠 관리 | 진행 로직 |
| `LiveEventDatabase` | 이벤트 DB SO | 이벤트 목록 관리 | 진행 로직 |
| `EventSubContent` | 서브컨텐츠 구조체 | 모듈 참조 정보 | 실제 컨텐츠 |
| `EventMissionData` | 미션 SO | 미션 정보 저장 | 진행 검증 |
| `EventMissionDatabase` | 미션 DB SO | 미션 목록 관리 | 진행 검증 |
| `EventCurrencyPolicy` | 재화 정책 구조체 | 유예기간, 전환 비율 | 실제 전환 |

### 유저 데이터

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `LiveEventProgress` | 이벤트 진행 상태 | 참여 이벤트 진행 저장 | 미션 처리 |
| `EventMissionProgress` | 미션 진행 상태 | 개별 미션 진행 저장 | 보상 지급 |
| `EventCurrencyData` | 이벤트 재화 (기존) | 이벤트 재화 저장 | 재화 사용 |

### Request/Response

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `GetActiveEventsRequest` | 활성 이벤트 조회 요청 | - | - |
| `GetActiveEventsResponse` | 활성 이벤트 목록 응답 | 이벤트 목록 전달 | - |
| `ClaimEventMissionRequest` | 미션 보상 수령 요청 | 미션 ID 전달 | 보상 계산 |
| `ClaimEventMissionResponse` | 미션 보상 응답 | 보상, Delta 전달 | 데이터 적용 |

### 이벤트

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `EventStartedEvent` | 이벤트 시작 알림 | 새 이벤트 알림 | - |
| `EventEndedEvent` | 이벤트 종료 알림 | 종료 처리 유도 | - |
| `EventMissionProgressedEvent` | 미션 진행 알림 | 진행도 변경 알림 | - |
| `EventMissionCompletedEvent` | 미션 완료 알림 | 완료 상태 알림 | - |
| `EventRewardClaimedEvent` | 보상 수령 알림 | 보상 수령 결과 | - |

### UI

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `LiveEventScreen` | 이벤트 메인 화면 | 활성 이벤트 목록, 배너 표시 | 개별 이벤트 진행 |
| `EventBannerItem` | 이벤트 배너 아이템 | 배너 표시, 남은 시간 | 진입 처리 |
| `EventDetailScreen` | 이벤트 상세 화면 | 서브컨텐츠 탭 관리 | 개별 기능 처리 |
| `EventMissionTab` | 미션 탭 컨텐츠 | 미션 목록 표시 | 미션 처리 |
| `EventMissionItem` | 미션 아이템 위젯 | 미션 표시, 보상 수령 | 미션 처리 |
| `EventStageTab` | 스테이지 탭 컨텐츠 | StageListScreen 재사용 | 전투 로직 |
| `EventShopTab` | 상점 탭 컨텐츠 | ShopScreen 필터링 재사용 | 구매 로직 |

---

## 상세 정의

### EventSubContent

**위치**: `Assets/Scripts/Data/Structs/MasterData/EventSubContent.cs`

```csharp
[Serializable]
public struct EventSubContent
{
    public EventSubContentType Type;
    public string ContentId;        // MissionGroupId, StageGroupId, ShopCategoryId 등
    public int TabOrder;            // UI 탭 순서 (0부터)
    public string TabNameKey;       // StringData 키 (탭 이름)
    public bool IsUnlocked;         // 초기 잠금 여부
    public string UnlockCondition;  // 해금 조건 설명 (선택)
}
```

**예시**:
| Type | ContentId | TabOrder | 설명 |
|------|-----------|----------|------|
| Mission | `"event_newyear_mission"` | 0 | 미션 탭 |
| Stage | `"event_newyear_stage"` | 1 | 스테이지 탭 |
| Shop | `"event_newyear_shop"` | 2 | 상점 탭 |

### EventCurrencyPolicy

**위치**: `Assets/Scripts/Data/Structs/MasterData/EventCurrencyPolicy.cs`

```csharp
[Serializable]
public struct EventCurrencyPolicy
{
    public string CurrencyId;           // 이벤트 재화 ID
    public string CurrencyNameKey;      // StringData 키
    public string CurrencyIcon;         // 아이콘 경로

    public int GracePeriodDays;         // 유예 기간 (일)
    public string ConvertToCurrencyId;  // 전환 대상 재화 (예: "gold")
    public float ConversionRate;        // 전환 비율 (예: 0.5 = 50%)
}
```

**예시**:
```json
{
    "CurrencyId": "event_newyear_token",
    "CurrencyNameKey": "currency.event_newyear",
    "CurrencyIcon": "Icons/Currency/event_newyear",
    "GracePeriodDays": 7,
    "ConvertToCurrencyId": "gold",
    "ConversionRate": 10.0
}
```
→ 이벤트 종료 후 7일간 상점 이용 가능, 이후 1토큰당 10골드로 자동 전환

### LiveEventData

**위치**: `Assets/Scripts/Data/ScriptableObjects/LiveEventData.cs`

```csharp
[CreateAssetMenu(fileName = "LiveEventData", menuName = "SC/Data/LiveEventData")]
public class LiveEventData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public EventType EventType;
    public string NameKey;              // StringData 키
    public string DescriptionKey;
    public string BannerImage;          // 배너 이미지 경로

    [Header("기간")]
    public DateTime StartTime;          // 시작 시간 (UTC)
    public DateTime EndTime;            // 종료 시간 (UTC)

    [Header("서브 컨텐츠")]
    public List<EventSubContent> SubContents;

    [Header("이벤트 재화")]
    public EventCurrencyPolicy CurrencyPolicy;
    public bool HasEventCurrency;       // 이벤트 재화 사용 여부

    [Header("표시")]
    public int DisplayOrder;            // 대시보드 표시 순서
    public bool ShowCountdown;          // 카운트다운 표시 여부
    public string PreviewStartTime;     // 사전 공개 시작 (선택)

    // 헬퍼 메서드
    public bool IsActive(DateTime serverTime)
        => serverTime >= StartTime && serverTime < EndTime;

    public bool IsInGracePeriod(DateTime serverTime)
        => serverTime >= EndTime && serverTime < EndTime.AddDays(CurrencyPolicy.GracePeriodDays);

    public int GetRemainingDays(DateTime serverTime)
        => Math.Max(0, (EndTime - serverTime).Days);

    public EventSubContent? GetSubContent(EventSubContentType type)
        => SubContents.FirstOrDefault(s => s.Type == type);
}
```

### EventMissionData

**위치**: `Assets/Scripts/Data/ScriptableObjects/EventMissionData.cs`

```csharp
[CreateAssetMenu(fileName = "EventMissionData", menuName = "SC/Data/EventMissionData")]
public class EventMissionData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public string MissionGroupId;       // 소속 미션 그룹
    public string NameKey;              // StringData 키
    public string DescriptionKey;

    [Header("조건")]
    public MissionConditionType ConditionType;
    public string ConditionTargetId;    // 특정 스테이지ID, 아이템ID 등 (선택)
    public int RequiredCount;           // 목표 수치

    [Header("보상")]
    public List<RewardInfo> Rewards;

    [Header("표시")]
    public int DisplayOrder;            // 목록 내 순서
    public bool IsHidden;               // 히든 미션 여부
}
```

### EventMissionGroup

**위치**: `Assets/Scripts/Data/ScriptableObjects/EventMissionGroup.cs`

```csharp
[CreateAssetMenu(fileName = "EventMissionGroup", menuName = "SC/Data/EventMissionGroup")]
public class EventMissionGroup : ScriptableObject
{
    public string Id;
    public string EventId;              // 소속 이벤트
    public List<EventMissionData> Missions;

    // 헬퍼
    public EventMissionData GetMission(string missionId)
        => Missions.FirstOrDefault(m => m.Id == missionId);
}
```

---

## 유저 데이터

### LiveEventProgress

**위치**: `Assets/Scripts/Data/Structs/UserData/LiveEventProgress.cs`

```csharp
[Serializable]
public struct EventMissionProgress
{
    public string MissionId;
    public int CurrentCount;        // 현재 진행 수치
    public bool IsCompleted;        // 완료 여부
    public bool IsClaimed;          // 보상 수령 여부
}

[Serializable]
public struct LiveEventProgress
{
    public string EventId;
    public List<EventMissionProgress> MissionProgresses;
    public bool HasVisited;         // 방문 여부 (NEW 뱃지용)
    public DateTime FirstVisitTime;

    // 헬퍼 메서드
    public EventMissionProgress GetMissionProgress(string missionId);
    public int GetCompletedMissionCount();
    public int GetClaimableMissionCount();
}
```

### UserSaveData 확장

```csharp
// UserSaveData에 추가
public Dictionary<string, LiveEventProgress> EventProgresses;  // Key: EventId
```

---

## 화면 흐름

```
로비
  │
  ├─────────────────────────────────────────────────┐
  │                                                 │
  ▼                                                 ▼
┌─────────────────────────┐         ┌─────────────────────────┐
│  StageDashboardScreen   │         │   LiveEventScreen       │
│  (상시 컨텐츠)           │         │   (기간 한정 컨텐츠)     │
│                         │         │                         │
│  ├─ 메인 스토리          │         │  활성 이벤트 배너 목록   │
│  ├─ 하드 모드            │         │  ├─ 신년 이벤트 (D-5)   │
│  ├─ 일일 던전            │         │  ├─ 콜라보 이벤트 (D-14)│
│  └─ ...                 │         │  └─ 출석 이벤트 (D-30)  │
└─────────────────────────┘         └───────────┬─────────────┘
                                                │ 배너 클릭
                                                ▼
                                    ┌─────────────────────────┐
                                    │   EventDetailScreen     │
                                    │   (이벤트 상세)          │
                                    │                         │
                                    │  [미션][스테이지][상점]  │
                                    │  ┌─────────────────────┐│
                                    │  │ 탭 컨텐츠 영역      ││
                                    │  │ (모듈별 화면)       ││
                                    │  └─────────────────────┘│
                                    │  이벤트 재화: 🎫 150    │
                                    └─────────────────────────┘
```

---

## UI 상세

### LiveEventScreen

**역할**: 활성 이벤트 목록 표시 (로비에서 진입)

```
┌─────────────────────────────────────────┐
│  ←  이벤트                              │
├─────────────────────────────────────────┤
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ 🎉 신년 맞이 대축제               ││
│  │ [배너 이미지]                       ││
│  │                          D-5 남음  ││
│  │                          [NEW]     ││
│  └─────────────────────────────────────┘│
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ ⚔️ 콜라보: XXX                     ││
│  │ [배너 이미지]                       ││
│  │                          D-14 남음 ││
│  └─────────────────────────────────────┘│
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ 📅 1월 출석 이벤트                 ││
│  │ 출석 12/30일                       ││
│  │                          D-19 남음 ││
│  │                          [보상!]   ││
│  └─────────────────────────────────────┘│
│                                         │
└─────────────────────────────────────────┘
```

**표시 정보**:
- 이벤트 배너 이미지
- 이벤트 이름
- 남은 기간 (D-N)
- NEW 뱃지 (미방문 시)
- 보상 뱃지 (수령 가능 미션 있을 때)
- 유예 기간 표시 (종료된 이벤트)

### EventDetailScreen

**역할**: 이벤트 상세 (서브컨텐츠 탭으로 구성)

```
┌─────────────────────────────────────────┐
│  ← 신년 맞이 대축제           D-5 남음  │
├─────────────────────────────────────────┤
│  [미션]  [스테이지]  [상점]             │
├═════════════════════════════════════════┤
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ 이벤트 스테이지 5회 클리어          ││
│  │ ████████░░░░░░░░ 3/5               ││
│  │ 보상: 🎫x50                [진행중] ││
│  └─────────────────────────────────────┘│
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ 가챠 3회 실행                       ││
│  │ ████████████████ 3/3               ││
│  │ 보상: 💎x100             [보상받기] ││
│  └─────────────────────────────────────┘│
│                                         │
│  ┌─────────────────────────────────────┐│
│  │ 이벤트 상점 구매 1회                ││
│  │ ░░░░░░░░░░░░░░░░ 0/1               ││
│  │ 보상: ⭐ 캐릭터           [진행중]  ││
│  └─────────────────────────────────────┘│
│                                         │
├─────────────────────────────────────────┤
│  이벤트 재화: 🎫 150                    │
└─────────────────────────────────────────┘
```

**탭 구성** (EventSubContent 기반):
- 미션 탭: EventMissionTab (미션 목록)
- 스테이지 탭: EventStageTab (StageListScreen 재사용)
- 상점 탭: EventShopTab (ShopScreen 필터링)

### EventMissionItem

**역할**: 개별 미션 표시

```csharp
public class EventMissionItem : Widget
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Button _claimButton;
    [SerializeField] private GameObject _completedMark;

    public void Setup(EventMissionData data, EventMissionProgress progress);
    public void OnClaimClicked();
}
```

---

## 관계도 (상세)

```
┌──────────────────────────────────────────────────────────────┐
│                     LiveEventScreen                          │
│  ┌────────────────────────────────────────────────────────┐  │
│  │              EventBannerItem[] (세로 스크롤)            │  │
│  │  ┌──────────────────────────────────────────────────┐  │  │
│  │  │ 신년 이벤트                              D-5     │  │  │
│  │  │ [배너 이미지]                            [NEW]   │  │  │
│  │  └──────────────────────────────────────────────────┘  │  │
│  │  ┌──────────────────────────────────────────────────┐  │  │
│  │  │ 콜라보 이벤트                            D-14    │  │  │
│  │  └──────────────────────────────────────────────────┘  │  │
│  └─────────────────────────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────┘
                          │ Click
                          ▼
┌──────────────────────────────────────────────────────────────┐
│                   EventDetailScreen                          │
│  ┌────────────────────────────────────────────────────────┐  │
│  │ Tab: [미션] [스테이지] [상점]  (SubContents 기반)       │  │
│  └────────────────────────────────────────────────────────┘  │
│  ┌────────────────────────────────────────────────────────┐  │
│  │                     탭 컨텐츠 영역                      │  │
│  │  ┌───────────────────────────────────────────────────┐ │  │
│  │  │ [미션 탭] EventMissionTab                         │ │  │
│  │  │  └─ EventMissionItem[] (미션 목록)                │ │  │
│  │  │                                                   │ │  │
│  │  │ [스테이지 탭] EventStageTab                       │ │  │
│  │  │  └─ StageListScreen (재사용, Event 타입 필터)     │ │  │
│  │  │                                                   │ │  │
│  │  │ [상점 탭] EventShopTab                            │ │  │
│  │  │  └─ ShopScreen (재사용, EventShop 카테고리)       │ │  │
│  │  └───────────────────────────────────────────────────┘ │  │
│  └────────────────────────────────────────────────────────┘  │
│  ┌────────────────────────────────────────────────────────┐  │
│  │ 이벤트 재화 표시: 🎫 150                               │  │
│  └────────────────────────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────┘
                          │ [보상받기] Click
                          ▼
┌────────────────────┐     ┌─────────────────────┐
│  NetworkManager    │────►│  LocalApiClient     │
│ ClaimMissionRequest│     │ ClaimMissionAsync   │
└────────────────────┘     └──────────┬──────────┘
                                      │
                                      ▼
                           ┌─────────────────────┐
                           │  RewardPopup        │
                           │  획득 보상 표시      │
                           └─────────────────────┘
```

---

## 설계 원칙

1. **모듈형 서브컨텐츠 패턴**
   - 이벤트는 Mission, Stage, Shop, Minigame의 모듈 컨테이너
   - EventSubContent 배열로 유연한 구성
   - 기존 시스템 재사용 (ShopScreen, StageListScreen)

2. **기간 관리**
   - 서버 시간 기준 활성/비활성 판단
   - 클라이언트는 남은 시간 표시만
   - 유예 기간 중에도 상점 이용 가능

3. **이벤트 재화 정책**
   - EventCurrencyPolicy로 재화별 정책 정의
   - **유예 기간**: 이벤트 종료 후 N일간 상점 이용 가능
   - **자동 전환**: 유예 기간 종료 시 범용 재화로 전환
   - 전환 비율은 이벤트별로 설정 가능

4. **미션 진행 추적**
   - 게임 내 액션 → 미션 진행도 업데이트
   - MissionConditionType으로 다양한 조건 지원
   - 조건 충족 시 보상 수령 가능 상태로 변경

5. **Stage와의 연동**
   - 이벤트 스테이지는 StageListScreen 재사용
   - PresetGroupId: `event_{eventId}` 형식으로 프리셋 분리

---

## Request/Response

### GetActiveEventsRequest

**위치**: `Assets/Scripts/Packet/Requests/GetActiveEventsRequest.cs`

```csharp
[Serializable]
public struct GetActiveEventsRequest : IRequest
{
    public long Timestamp { get; set; }
    public bool IncludeGracePeriod;  // 유예 기간 이벤트 포함 여부
}
```

### GetActiveEventsResponse

**위치**: `Assets/Scripts/Packet/Responses/GetActiveEventsResponse.cs`

```csharp
[Serializable]
public struct LiveEventInfo
{
    public string EventId;
    public EventType EventType;
    public string NameKey;
    public string BannerImage;
    public DateTime StartTime;
    public DateTime EndTime;
    public int RemainingDays;
    public bool IsInGracePeriod;
    public int GracePeriodRemainingDays;
    public List<EventSubContent> SubContents;
    public bool HasClaimableReward;     // 수령 가능 보상 여부
    public bool HasVisited;             // 방문 여부
}

[Serializable]
public struct GetActiveEventsResponse : IResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }

    public List<LiveEventInfo> ActiveEvents;
    public List<LiveEventInfo> GracePeriodEvents;  // 유예 기간 이벤트
}
```

### ClaimEventMissionRequest

**위치**: `Assets/Scripts/Packet/Requests/ClaimEventMissionRequest.cs`

```csharp
[Serializable]
public struct ClaimEventMissionRequest : IRequest
{
    public long Timestamp { get; set; }
    public string EventId;
    public string MissionId;
}
```

### ClaimEventMissionResponse

**위치**: `Assets/Scripts/Packet/Responses/ClaimEventMissionResponse.cs`

```csharp
[Serializable]
public struct ClaimEventMissionResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }

    public List<RewardInfo> ClaimedRewards;
}
```

### VisitEventRequest/Response

**위치**: `Assets/Scripts/Packet/Requests/VisitEventRequest.cs`

```csharp
[Serializable]
public struct VisitEventRequest : IRequest
{
    public long Timestamp { get; set; }
    public string EventId;
}

[Serializable]
public struct VisitEventResponse : IResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }

    public LiveEventProgress EventProgress;  // 해당 이벤트 진행 상태
}
```

---

## 에러 코드

| ErrorCode | 값 | 설명 |
|-----------|-----|------|
| `EventNotFound` | 6001 | 이벤트 없음 |
| `EventNotActive` | 6002 | 이벤트 비활성 |
| `EventExpired` | 6003 | 이벤트 종료 |
| `EventMissionNotFound` | 6004 | 미션 없음 |
| `EventMissionNotCompleted` | 6005 | 미션 미완료 |
| `EventMissionAlreadyClaimed` | 6006 | 이미 보상 수령함 |
| `EventCurrencyInsufficient` | 6007 | 이벤트 재화 부족 |

---

## 이벤트 재화 전환 흐름

```
이벤트 종료
    │
    ▼
유예 기간 시작 (N일)
    │
    ├─ 상점 이용 가능
    ├─ 스테이지 진입 불가
    └─ 미션 진행 불가
    │
    ▼
유예 기간 종료
    │
    ▼
┌─────────────────────────────────────┐
│ LocalApiClient.ConvertEventCurrency │
│   ├─ 이벤트 재화 잔량 확인          │
│   ├─ 전환 비율 적용                 │
│   ├─ 범용 재화로 전환               │
│   └─ 이벤트 재화 삭제               │
└─────────────────────────────────────┘
    │
    ▼
전환 완료 알림 (EventCurrencyConvertedEvent)
```

---

## 상세 문서

### 마스터 데이터
- [LiveEventData.md](LiveEvent/LiveEventData.md)
- [EventMissionData.md](LiveEvent/EventMissionData.md)

### UI
- [EventDashboardScreen.md](LiveEvent/EventDashboardScreen.md)
- [EventDetailScreen.md](LiveEvent/EventDetailScreen.md)

---

## 상태

| 분류 | 상태 |
|------|------|
| 마스터 데이터 | ✅ 설계 완료 |
| 유저 데이터 | ✅ 설계 완료 |
| Request/Response | ✅ 설계 완료 |
| 이벤트 | ✅ 설계 완료 |
| LocalApiClient | ⬜ 구현 대기 |
| UI | ✅ 설계 완료 |
| 테스트 | ⬜ 대기 |

---

## 구현 체크리스트

```
Phase 4: 라이브 이벤트 구현

설계 완료:
- [x] EventType, EventSubContentType, MissionConditionType
- [x] EventSubContent, EventCurrencyPolicy 구조체
- [x] LiveEventData, EventMissionData, EventMissionGroup
- [x] LiveEventProgress, EventMissionProgress
- [x] Request/Response (GetActiveEvents, ClaimEventMission, VisitEvent)
- [x] UI 설계 (LiveEventScreen, EventDetailScreen, 탭 구조)
- [x] 재화 정책 (유예 기간 + 범용 재화 전환)
- [x] 에러 코드 (6001~6007)

Enums:
- [ ] EventType.cs (Data/Enums/)
- [ ] EventSubContentType.cs (Data/Enums/)
- [ ] MissionConditionType.cs (Data/Enums/)

마스터 데이터:
- [ ] EventSubContent.cs (Data/Structs/MasterData/)
- [ ] EventCurrencyPolicy.cs (Data/Structs/MasterData/)
- [ ] LiveEventData.cs (Data/ScriptableObjects/)
- [ ] LiveEventDatabase.cs (Data/ScriptableObjects/)
- [ ] EventMissionData.cs (Data/ScriptableObjects/)
- [ ] EventMissionGroup.cs (Data/ScriptableObjects/)
- [ ] LiveEvent.json (Data/MasterData/)
- [ ] EventMission.json (Data/MasterData/)
- [ ] MasterDataImporter에 LiveEvent, EventMission 추가

유저 데이터:
- [ ] LiveEventProgress.cs (Data/Structs/UserData/)
- [ ] EventMissionProgress.cs (Data/Structs/UserData/)
- [ ] UserSaveData.EventProgresses 필드 추가

Request/Response:
- [ ] GetActiveEventsRequest.cs
- [ ] GetActiveEventsResponse.cs (LiveEventInfo 포함)
- [ ] ClaimEventMissionRequest.cs
- [ ] ClaimEventMissionResponse.cs
- [ ] VisitEventRequest.cs
- [ ] VisitEventResponse.cs

이벤트:
- [ ] LiveEventEvents.cs (Event/OutGame/)
  - [ ] EventStartedEvent
  - [ ] EventEndedEvent
  - [ ] EventMissionProgressedEvent
  - [ ] EventMissionCompletedEvent
  - [ ] EventRewardClaimedEvent
  - [ ] EventCurrencyConvertedEvent

API:
- [ ] LocalApiClient.GetActiveEventsAsync 구현
- [ ] LocalApiClient.VisitEventAsync 구현
- [ ] LocalApiClient.ClaimEventMissionAsync 구현
- [ ] LocalApiClient.ConvertEventCurrencyAsync 구현
- [ ] 미션 진행도 업데이트 로직

UI:
- [ ] Sc.Contents.Event Assembly 생성
- [ ] LiveEventScreen.cs
- [ ] EventBannerItem.cs
- [ ] EventDetailScreen.cs
- [ ] EventMissionTab.cs
- [ ] EventMissionItem.cs
- [ ] EventStageTab.cs (StageListScreen 재사용)
- [ ] EventShopTab.cs (ShopScreen 재사용)
- [ ] EventDashboardScreen.cs
- [ ] EventBannerItem.cs
- [ ] EventDetailScreen.cs
- [ ] EventMissionItem.cs
- [ ] MVPSceneSetup에 Event 프리팹 추가

연동:
- [ ] LobbyScreen에 [이벤트] 버튼 추가
- [ ] 이벤트 상점 연동 (ShopScreen 재사용, EventShop 필터)
- [ ] 이벤트 스테이지 연동 (StageListScreen 재사용, Event 필터)
- [ ] 미션 조건 연동
  - [ ] 스테이지 클리어 → ClearStageCount 조건
  - [ ] 가챠 실행 → GachaCount 조건
  - [ ] 상점 구매 → PurchaseCount 조건

검증 시나리오:
- [ ] 활성 이벤트 목록 표시
- [ ] 이벤트 상세 진입
- [ ] 미션 진행도 표시
- [ ] 미션 보상 수령
- [ ] 이벤트 상점 이용
- [ ] 이벤트 스테이지 진입
```

---

## Enum 정의

### EventType

**위치**: `Assets/Scripts/Data/Enums/EventType.cs`

```csharp
public enum EventType
{
    Story,          // 스토리 이벤트 (기간 한정 스토리)
    Collection,     // 수집 이벤트 (아이템 모으기)
    Raid,           // 레이드 이벤트 (협동 보스)
    Login,          // 출석 이벤트
    Celebration,    // 기념 이벤트 (기념일, 업데이트)
    Collaboration,  // 콜라보 이벤트
}
```

### EventSubContentType

**위치**: `Assets/Scripts/Data/Enums/EventSubContentType.cs`

```csharp
public enum EventSubContentType
{
    Mission,        // 미션 모듈
    Stage,          // 스테이지 모듈
    Shop,           // 상점 모듈
    Minigame,       // 미니게임 모듈 (추후)
    Story,          // 스토리 모듈 (추후)
}
```

### MissionConditionType

**위치**: `Assets/Scripts/Data/Enums/MissionConditionType.cs`

```csharp
public enum MissionConditionType
{
    // 스테이지 관련
    ClearStage,         // 특정 스테이지 클리어
    ClearStageCount,    // 스테이지 N회 클리어
    ClearStageType,     // 특정 타입 스테이지 N회
    ClearEventStage,    // 이벤트 스테이지 N회 클리어

    // 가챠/상점 관련
    GachaCount,         // 가챠 N회
    PurchaseCount,      // 구매 N회
    PurchaseEventShop,  // 이벤트 상점 구매 N회

    // 출석 관련
    LoginCount,         // 출석 N일
    LoginConsecutive,   // 연속 출석 N일

    // 수집/소비 관련
    CollectItem,        // 아이템 N개 수집
    CollectEventItem,   // 이벤트 아이템 N개 수집
    SpendCurrency,      // 재화 N 소비
    SpendEventCurrency, // 이벤트 재화 N 소비

    // 성장 관련
    ReachLevel,         // 플레이어 레벨 N 달성
    OwnCharacter,       // 캐릭터 N명 보유
    UpgradeCharacter,   // 캐릭터 강화 N회
}
```

---

## 에러 코드

| 코드 | 설명 |
|------|------|
| `EVENT_NOT_FOUND` | 이벤트 없음 |
| `EVENT_NOT_ACTIVE` | 이벤트 비활성 |
| `EVENT_EXPIRED` | 이벤트 종료 |
| `MISSION_NOT_FOUND` | 미션 없음 |
| `MISSION_NOT_COMPLETED` | 미션 미완료 |
| `MISSION_ALREADY_CLAIMED` | 이미 보상 수령함 |

---

## 미션 조건 연동

### 게임 액션 → 미션 진행

| 액션 | 이벤트 | 미션 조건 |
|------|--------|-----------|
| 스테이지 클리어 | `BattleEndEvent` | ClearStage, ClearStageCount |
| 가챠 실행 | `GachaCompletedEvent` | GachaCount |
| 상점 구매 | `ProductPurchasedEvent` | PurchaseCount |
| 로그인 | `LoginCompletedEvent` | LoginCount |

### 처리 흐름
```
[게임 액션]
     │
     ▼
[이벤트 발행]
     │
     ▼
[LocalApiClient.UpdateMissionProgress()]
     │ 조건 체크
     ▼
[UserSaveData.EventProgress 업데이트]
     │
     ▼
[EventMissionProgressedEvent 발행]
     │ 완료 시
     ▼
[EventMissionCompletedEvent 발행]
```

---

## 관련 문서

- [Shop.md](Shop.md) - 상점 시스템 (이벤트 상점 재사용)
- [Stage.md](Stage.md) - 스테이지 시스템 (이벤트 스테이지 재사용)
- [Common/Reward.md](Common/Reward.md) - 보상 시스템
- [Data.md](Data.md) - 데이터 구조 개요
