# LiveEvent 구현 계획

> **작성일**: 2026-01-20
> **상태**: 계획 확정, 구현 대기
> **스펙 문서**: [LiveEvent.md](../LiveEvent.md)

---

## 구현 범위 (확정)

### 포함

| 분류 | 범위 | 비고 |
|------|------|------|
| **Mission** | 데이터 구조만 | Enum, Struct, SO 정의. 로직/UI 제외 |
| **Stage Tab** | 기본 UI 구조 | 기존 Stage 시스템 미구현, 플레이스홀더 |
| **Shop Tab** | 기본 UI 구조 | 기존 Shop 시스템 미구현, 플레이스홀더 |
| **이벤트 재화** | 전체 구현 | 유예기간 체크 + 범용재화 자동 전환 |

### 제외

- Mission 진행도 자동 업데이트 로직 (게임 액션 연동)
- Mission 보상 수령 로직
- EventMissionTab UI
- 실제 Stage/Shop 기능 (별도 시스템 구현 후 연동)

---

## Phase 별 작업 단위

### Phase A: Enums + 구조체 (5개 파일)

**의존성**: 없음

| 파일 | 경로 | 설명 |
|------|------|------|
| `EventType.cs` | `Data/Enums/` | Story, Collection, Raid, Login, Celebration, Collaboration |
| `EventSubContentType.cs` | `Data/Enums/` | Mission, Stage, Shop, Minigame, Story |
| `MissionConditionType.cs` | `Data/Enums/` | ClearStage, GachaCount, PurchaseCount 등 |
| `EventSubContent.cs` | `Data/Structs/MasterData/` | Type, ContentId, TabOrder, TabNameKey, IsUnlocked |
| `EventCurrencyPolicy.cs` | `Data/Structs/MasterData/` | CurrencyId, GracePeriodDays, ConversionRate |

**테스트**: 불필요 (단순 정의)

---

### Phase B: SO + 유저 데이터 (7개 파일)

**의존성**: Phase A

| 파일 | 경로 | 설명 |
|------|------|------|
| `LiveEventData.cs` | `Data/ScriptableObjects/` | 이벤트 정보 SO |
| `LiveEventDatabase.cs` | `Data/ScriptableObjects/` | 이벤트 목록 관리 SO |
| `EventMissionData.cs` | `Data/ScriptableObjects/` | 미션 정보 SO (구조만) |
| `EventMissionGroup.cs` | `Data/ScriptableObjects/` | 미션 그룹 SO (구조만) |
| `LiveEventProgress.cs` | `Data/Structs/UserData/` | EventId, HasVisited, MissionProgresses |
| `EventMissionProgress.cs` | `Data/Structs/UserData/` | MissionId, CurrentCount, IsCompleted, IsClaimed |
| `UserSaveData.cs` | (수정) | EventProgresses 필드 추가, Version 3 마이그레이션 |

**테스트**: 마이그레이션 테스트, SO 헬퍼 메서드 테스트

---

### Phase C: Request/Response (6개 파일)

**의존성**: Phase A, B

| 파일 | 경로 | 설명 |
|------|------|------|
| `GetActiveEventsRequest.cs` | `Data/Requests/` | IncludeGracePeriod 파라미터 |
| `GetActiveEventsResponse.cs` | `Data/Responses/` | List<LiveEventInfo> |
| `VisitEventRequest.cs` | `Data/Requests/` | EventId |
| `VisitEventResponse.cs` | `Data/Responses/` | LiveEventProgress |
| `ClaimEventMissionRequest.cs` | `Data/Requests/` | EventId, MissionId (구조만) |
| `ClaimEventMissionResponse.cs` | `Data/Responses/` | ClaimedRewards, Delta (구조만) |

**참조 패턴**: `GachaRequest.cs`, `GachaResponse.cs`

**테스트**: 직렬화/역직렬화 테스트

---

### Phase D: 이벤트 + Handler (3개 파일)

**의존성**: Phase C

| 파일 | 경로 | 설명 |
|------|------|------|
| `LiveEventEvents.cs` | `Event/OutGame/` | EventStarted, EventEnded, CurrencyConverted 등 |
| `EventHandler.cs` | `LocalServer/Handlers/` | GetActiveEvents, VisitEvent, ConvertCurrency |
| `LocalGameServer.cs` | (수정) | EventHandler 라우팅 추가 |

**참조 패턴**: `GachaHandler.cs`, `ApiResultEvents.cs`

**테스트**: Handler 단위 테스트

---

### Phase E: UI Assembly + Screen (4개 파일)

**의존성**: Phase D

| 파일 | 경로 | 설명 |
|------|------|------|
| `Sc.Contents.Event.asmdef` | `Contents/OutGame/Event/` | Assembly 정의 |
| `IsExternalInit.cs` | `Contents/OutGame/Event/` | C# 9 record 지원 |
| `LiveEventScreen.cs` | `Contents/OutGame/Event/` | 활성 이벤트 목록 화면 |
| `EventBannerItem.cs` | `Contents/OutGame/Event/` | 배너 아이템 위젯 |

**참조 패턴**: `GachaScreen.cs`, `Sc.Contents.Gacha.asmdef`

**테스트**: PlayMode 테스트 (선택)

---

### Phase F: EventDetailScreen + Tabs (4개 파일)

**의존성**: Phase E

| 파일 | 경로 | 설명 |
|------|------|------|
| `EventDetailScreen.cs` | `Contents/OutGame/Event/` | 이벤트 상세, 탭 관리 |
| `EventMissionTab.cs` | `Contents/OutGame/Event/` | 미션 탭 (구조만, "준비 중" 표시) |
| `EventStageTab.cs` | `Contents/OutGame/Event/` | 스테이지 탭 (플레이스홀더 UI) |
| `EventShopTab.cs` | `Contents/OutGame/Event/` | 상점 탭 (플레이스홀더 UI) |

**참조 패턴**: 기존 TabWidget 패턴 (있다면)

**테스트**: PlayMode 테스트 (선택)

---

### Phase G: 재화 전환 + 통합 (3개 파일)

**의존성**: Phase D, F

| 파일 | 경로 | 설명 |
|------|------|------|
| `EventCurrencyConverter.cs` | `LocalServer/Services/` | 유예기간 체크 + 범용재화 전환 |
| `LobbyScreen.cs` | (수정) | [이벤트] 버튼 추가, LiveEventScreen 진입 |
| `EventResponseHandler.cs` | `Core/Handlers/` | GetActiveEvents, VisitEvent 응답 처리 |

**테스트**: 재화 전환 단위 테스트

---

## 의존성 그래프

```
Phase A (Enums + 구조체)
    │
    └──► Phase B (SO + 유저 데이터)
            │
            └──► Phase C (Request/Response)
                    │
                    └──► Phase D (이벤트 + Handler)
                            │
                            ├──► Phase E (UI Assembly + Screen)
                            │       │
                            │       └──► Phase F (EventDetailScreen + Tabs)
                            │
                            └──► Phase G (재화 전환 + 통합)
```

---

## 참조 패턴 (기존 코드)

다른 세션에서 작업 시 아래 파일들을 먼저 읽고 패턴을 파악할 것.

### Request/Response

```
Assets/Scripts/Data/Requests/GachaRequest.cs
Assets/Scripts/Data/Responses/GachaResponse.cs
```

- `IRequest<TResponse>` 상속
- Static factory methods (Create, Success, Fail)
- `IGameActionResponse` 로 Delta 포함

### LocalServer Handler

```
Assets/Scripts/LocalServer/Handlers/GachaHandler.cs
Assets/Scripts/LocalServer/LocalGameServer.cs
```

- `IRequestHandler<TRequest, TResponse>` 구현
- `ref UserSaveData` 로 유저 데이터 직접 수정
- switch expression 으로 라우팅

### Response Handler (Client)

```
Assets/Scripts/Core/Handlers/GachaResponseHandler.cs
```

- `PacketHandlerBase<TResponse>` 상속
- DataManager.ApplyDelta() 호출
- EventManager.Publish() 로 UI 알림

### Screen/Popup

```
Assets/Scripts/Contents/OutGame/Gacha/GachaScreen.cs
Assets/Scripts/Common/UI/ScreenWidget.cs
```

- `ScreenWidget<TScreen, TState>` 상속
- State 기반 UI 관리
- OnShow/OnHide 에서 이벤트 구독/해제

### 이벤트

```
Assets/Scripts/Event/OutGame/ApiResultEvents.cs
```

- `readonly struct` + `init` 프로퍼티
- Completed/Failed 이벤트 쌍

---

## 작업 지시 예시

각 Phase 작업 시 아래와 같이 지시:

```
"LiveEvent Phase A 구현하자"
"LiveEvent Phase B 구현하자"
...
```

또는 전체:

```
"LiveEvent 구현 시작하자 (Phase A부터)"
```

---

## 예상 파일 수

| Phase | 파일 수 |
|-------|---------|
| A | 5 |
| B | 7 (수정 1 포함) |
| C | 6 |
| D | 3 (수정 1 포함) |
| E | 4 |
| F | 4 |
| G | 3 (수정 1 포함) |
| **총계** | **~32개** |

---

## 체크리스트

```
Phase A: Enums + 구조체
- [ ] EventType.cs
- [ ] EventSubContentType.cs
- [ ] MissionConditionType.cs
- [ ] EventSubContent.cs
- [ ] EventCurrencyPolicy.cs

Phase B: SO + 유저 데이터
- [ ] LiveEventData.cs
- [ ] LiveEventDatabase.cs
- [ ] EventMissionData.cs
- [ ] EventMissionGroup.cs
- [ ] LiveEventProgress.cs
- [ ] EventMissionProgress.cs
- [ ] UserSaveData.cs (Version 3 마이그레이션)

Phase C: Request/Response
- [ ] GetActiveEventsRequest.cs
- [ ] GetActiveEventsResponse.cs
- [ ] VisitEventRequest.cs
- [ ] VisitEventResponse.cs
- [ ] ClaimEventMissionRequest.cs
- [ ] ClaimEventMissionResponse.cs

Phase D: 이벤트 + Handler
- [ ] LiveEventEvents.cs
- [ ] EventHandler.cs
- [ ] LocalGameServer.cs (라우팅 추가)

Phase E: UI Assembly + Screen
- [ ] Sc.Contents.Event.asmdef
- [ ] IsExternalInit.cs
- [ ] LiveEventScreen.cs
- [ ] EventBannerItem.cs

Phase F: EventDetailScreen + Tabs
- [ ] EventDetailScreen.cs
- [ ] EventMissionTab.cs (플레이스홀더)
- [ ] EventStageTab.cs (플레이스홀더)
- [ ] EventShopTab.cs (플레이스홀더)

Phase G: 재화 전환 + 통합
- [ ] EventCurrencyConverter.cs
- [ ] LobbyScreen.cs (버튼 추가)
- [ ] EventResponseHandler.cs
```
