# LiveEvent 구현 현황

## 현재 상태 (2026-01-20)

- **스펙 문서**: ✅ 완료 (`Docs/Specs/LiveEvent.md`)
- **구현 계획**: ✅ 완료 (`Docs/Specs/LiveEvent/IMPLEMENTATION_PLAN.md`)
- **코드 구현**: ✅ 완료 (100%)

## 구현 완료 내역

### 생성된 파일 (30개)

**Phase A: Enums + 구조체 (5개)**
- `Assets/Scripts/Data/Enums/EventType.cs`
- `Assets/Scripts/Data/Enums/EventSubContentType.cs`
- `Assets/Scripts/Data/Enums/MissionConditionType.cs`
- `Assets/Scripts/Data/Structs/MasterData/EventSubContent.cs`
- `Assets/Scripts/Data/Structs/MasterData/EventCurrencyPolicy.cs`

**Phase B: SO + UserData (7개)**
- `Assets/Scripts/Data/ScriptableObjects/LiveEventData.cs`
- `Assets/Scripts/Data/ScriptableObjects/LiveEventDatabase.cs`
- `Assets/Scripts/Data/ScriptableObjects/EventMissionData.cs`
- `Assets/Scripts/Data/ScriptableObjects/EventMissionGroup.cs`
- `Assets/Scripts/Data/Structs/UserData/LiveEventProgress.cs`
- `Assets/Scripts/Data/Structs/UserData/EventMissionProgress.cs`
- `Assets/Scripts/Data/Structs/UserData/UserSaveData.cs` (수정 - v3 마이그레이션)

**Phase C: Request/Response (6개)**
- `Assets/Scripts/Data/Requests/GetActiveEventsRequest.cs`
- `Assets/Scripts/Data/Responses/GetActiveEventsResponse.cs`
- `Assets/Scripts/Data/Requests/VisitEventRequest.cs`
- `Assets/Scripts/Data/Responses/VisitEventResponse.cs`
- `Assets/Scripts/Data/Requests/ClaimEventMissionRequest.cs`
- `Assets/Scripts/Data/Responses/ClaimEventMissionResponse.cs`

**Phase D: Events + Handler (3개)**
- `Assets/Scripts/Event/OutGame/LiveEventEvents.cs`
- `Assets/Scripts/LocalServer/Handlers/EventHandler.cs`
- `Assets/Scripts/LocalServer/LocalGameServer.cs` (수정)

**Phase E: UI Assembly + Screen (4개)**
- `Assets/Scripts/Contents/OutGame/Event/Sc.Contents.Event.asmdef`
- `Assets/Scripts/Contents/OutGame/Event/IsExternalInit.cs`
- `Assets/Scripts/Contents/OutGame/Event/LiveEventScreen.cs`
- `Assets/Scripts/Contents/OutGame/Event/EventBannerItem.cs`

**Phase F: EventDetailScreen + Tabs (4개)**
- `Assets/Scripts/Contents/OutGame/Event/EventDetailScreen.cs`
- `Assets/Scripts/Contents/OutGame/Event/EventMissionTab.cs`
- `Assets/Scripts/Contents/OutGame/Event/EventStageTab.cs`
- `Assets/Scripts/Contents/OutGame/Event/EventShopTab.cs`

**Phase G: 재화 전환 + 통합 (3개)**
- `Assets/Scripts/LocalServer/Services/EventCurrencyConverter.cs`
- `Assets/Scripts/Core/Handlers/EventResponseHandler.cs`
- `Assets/Scripts/Contents/OutGame/Lobby/LobbyScreen.cs` (수정)

**추가: TabWidget 범용 클래스 (2개)**
- `Assets/Scripts/Common/UI/Widgets/TabButton.cs`
- `Assets/Scripts/Common/UI/Widgets/TabGroupWidget.cs`

## 아키텍처 결정

- **클린 아키텍처** 선택 (TabWidget 추상화 포함)
- **UserSaveData v3** 마이그레이션 (EventProgresses 추가)
- **Mission 로직**: 플레이스홀더 (구조만)
- **Stage/Shop Tab**: 플레이스홀더 UI ("준비 중" 표시)
- **이벤트 재화 전환**: 전체 구현

## 참조

- 진행 상황: `Docs/PROGRESS.md`
- 스펙 문서: `Docs/Specs/LiveEvent.md`