# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## 🎯 현재 마일스톤: 아웃게임 아키텍처 1차 (OUTGAME-V1)

> **상세 문서**: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)

### 시스템 구현 상태

| Phase | 시스템 | 상태 | 스펙 문서 |
|-------|--------|------|-----------|
| A | Logging, ErrorHandling | ✅ | Foundation/*.md |
| B | SaveManager, LoadingIndicator | ✅ | 마일스톤 내 |
| C | Reward, TimeService | ✅ | Common/Reward.md, Core/TimeService.md |
| D | SystemPopup, RewardPopup | ✅ | Common/Popups/*.md |
| E | LocalServer 분리 | ✅ | 마일스톤 내 |
| F | **LiveEvent** | ✅ | LiveEvent.md |
| F | **Shop** | ✅ | Shop.md |
| F | **LobbyEntryTask** | ✅ | Lobby.md |
| F | Stage | ✅ | Stage.md (v3.1, Phase A~J 전체 완료) |
| F | GachaEnhancement | ⬜ | Gacha/Enhancement.md |
| F | CharacterEnhancement | ⬜ | Character/Enhancement.md |
| F | NavigationEnhancement | ⬜ | Common/NavigationEnhancement.md |

---

## 🚀 다음 작업

**지시**: "[시스템명] 구현하자" (예: "Shop 구현하자", "Stage 구현하자")

### 우선순위
1. **Stage** 시스템 구현 (설계 완료, [Stage.md v3.0](Specs/Stage.md))
2. GachaEnhancement / CharacterEnhancement

---

## 🔨 진행 중인 작업

(현재 진행 중인 작업 없음)

---

## 🧪 테스트 인프라

> **상세 문서**: [Specs/Testing/TestArchitecture.md](Specs/Testing/TestArchitecture.md)

| 단계 | 항목 | 상태 | 테스트 수 |
|------|------|------|----------|
| 1~3차 | Foundation, Core, Common, Reward | ✅ | 149개 |
| 3.5차 | LocalServer | ✅ | 40개 |
| 4~4.5차 | PlayMode 인프라, 에디터 도구 | ✅ | - |
| 5차 | LiveEvent 테스트 | ✅ | 115개 |
| 6차 | Stage 테스트 | ✅ | 47개 |

**총 테스트**: 351개

---

## ✅ 완료된 시스템 요약

<details>
<summary>클릭하여 펼치기</summary>

### 기반 인프라 (Phase A~E)
- **Logging**: LogLevel, LogCategory, Log.cs 정적 API
- **ErrorHandling**: ErrorCode, Result<T>, ErrorMessages
- **SaveManager**: ISaveStorage, FileSaveStorage, SaveMigrator
- **LoadingIndicator**: LoadingService, LoadingWidget, 레퍼런스 카운팅
- **Reward**: RewardInfo, RewardProcessor, RewardHelper
- **TimeService**: ITimeService, TimeHelper, LimitType
- **SystemPopup**: ConfirmPopup, CostConfirmPopup, State 패턴
- **RewardPopup**: RewardItem, IItemSpawner, 레이아웃 자동조정
- **LocalServer**: Sc.LocalServer Assembly 분리, Handler 패턴

### 컨텐츠 (Phase F)
- **LiveEvent**: ✅ 완료 (30개 파일, Phase A~G 전체)
- **Shop**: ✅ 완료 (17개 파일, Phase A~F 전체)

### MVP 완료
- Title, Lobby, Gacha, CharacterList, CharacterDetail Screen
- CurrencyHUD, GachaResultPopup, ScreenHeader
- Navigation 통합 스택, Transition 애니메이션
- DataManager, NetworkManager 이벤트 기반

</details>

---

## 작업 로그 (최근)

### 2026-01-21
- [x] **Stage 시스템 Phase J 완료** (2개 파일)
  - StageEntryValidatorTests.cs - 입장 제한 검증 테스트 (21개)
  - StageHandlerTests.cs - 입장/클리어 핸들러 테스트 (26개)

### 2026-01-20
- [x] **Stage 시스템 Phase H~I 완료** (4개 파일)
  - Phase H: StageInfoPopup
    - StageInfoState.cs - 스테이지 정보 팝업 상태
    - StageInfoPopup.cs - 스테이지 상세 정보 팝업 (별 조건, 보상, 입장 제한)
  - Phase I: EventDetailScreen 연동
    - EventStageTab.cs 수정 - StageSelectScreen 네비게이션 추가
    - Sc.Contents.Event.asmdef - Sc.Contents.Stage 참조 추가
  - Phase A: Stage.json v2.0
    - ContentType, CategoryId, StarConditions, FirstClearRewards, RepeatClearRewards 추가
- [x] **Stage 시스템 Phase G 완료** (6개 파일)
  - Content Modules 추가:
    - ExpDungeonContentModule.cs - 난이도 표시, 경험치 미리보기
    - BossRaidContentModule.cs - 보스 HP, 기여도, 랭킹 버튼
    - TowerContentModule.cs - 현재/최고 층, 보상 미리보기
    - EventStageContentModule.cs - 이벤트 이름, 남은 기간, 이벤트 재화
  - StageContentModuleFactory 업데이트 - 모든 모듈 등록
  - DataManager에 StageCategoryDatabase 추가
- [x] **PartyPreset 시스템 구현** (2개 파일)
  - PartyPreset.cs - 파티 프리셋 데이터 구조
  - UserSaveData v6 마이그레이션 (PartyPresets 필드)
- [x] **로비 진입 후처리 시스템 구현** (11개 파일)
  - Phase A (Core 인터페이스):
    - ILobbyEntryTask.cs - Task 인터페이스
    - IPopupQueueService.cs - 팝업 큐 인터페이스
    - LobbyTaskResult.cs - Task 결과 DTO
    - LobbyEntryTaskRunner.cs - Task 순차 실행기
  - Phase A (Event):
    - LobbyEvents.cs - LobbyEntryTasksCompletedEvent, LobbyEntryTaskCompletedEvent
  - Phase B (Common):
    - PopupQueueService.cs - 팝업 큐잉 서비스
  - Phase C (Lobby/Tasks):
    - AttendanceCheckTask.cs - 출석체크 (Stub)
    - EventCurrencyConversionTask.cs - 이벤트 재화 전환 (Full)
    - NewEventNotificationTask.cs - 신규 이벤트 알림 (Stub)
  - Phase D (통합):
    - LobbyScreen.cs 수정 - TaskRunner 초기화 및 OnShow 연동
    - DataManager.cs 확장 - GetUserDataCopy(), UpdateUserData()
  - Assembly 참조 추가: Sc.Contents.Lobby → Sc.Event, Sc.LocalServer
- [x] **Stage 시스템 Phase E~F 구현** (9개 파일)
  - Assembly: Sc.Contents.Stage.asmdef
  - Phase E (Screens):
    - InGameContentDashboard.cs - 컨텐츠 종류 선택
    - StageDashboard.cs - 세부 분류 선택 (속성/난이도)
    - StageSelectScreen.cs - 스테이지 목록 + 상세
    - PartySelectScreen.cs - 파티 편성 (플레이스홀더)
  - Phase F (Panels/Widgets):
    - StageListPanel.cs - 스테이지 목록 패널
    - StageItemWidget.cs - 개별 스테이지 아이템
    - ContentCategoryItem.cs - 컨텐츠 카테고리 아이템
  - Module 인터페이스:
    - IStageContentModule.cs (플레이스홀더)
  - LobbyScreen 연동 (_stageButton 추가)
- [x] **Stage 시스템 설계 완료** (Stage.md v3.0)
  - 컴포지션 패턴 확정: StageSelectScreen + IStageContentModule
  - 화면 계층 구조 정립:
    - Lobby → InGameContentDashboard → StageDashboard (선택적) → StageSelectScreen
  - 7개 컨텐츠 모듈 설계: MainStory, ElementDungeon, ExpDungeon, GoldDungeon, BossRaid, Tower, EventStage
  - StageListPanel을 StageSelectScreen 내 Panel로 통합
  - 별 시스템: StarCondition enum + StarAchieved[] 배열
  - 진입 제한: LimitType 재사용 (Daily/Weekly/Monthly)
- [x] **Main Scene 프리팹 자동화 구현** (Session 3)
  - Track A: UI 런타임 로딩
    - ScreenWidget/PopupWidget.Context.Load() Addressables 전환
    - 하이브리드 방식: 씬에 있으면 기존, 없으면 Addressables 로드
    - AssetScope 기반 메모리 관리 (Exit 시 자동 해제)
    - NavigationManager에 ScreenCanvas/PopupCanvas 참조 추가
  - Track B: 프리팹 생성 시스템
    - PrefabGenerator.cs 구현 (SC Tools/Prefabs 메뉴)
    - Screen/Popup 타입 자동 스캔 + 프리팹 생성
    - Addressables 자동 등록 (UI/Screens/*, UI/Popups/*)
  - Critical 이슈 수정:
    - Memory Leak (컴포넌트 누락 시 정리)
    - Null Canvas Parent 체크
    - Load Race Condition 방지 (_isLoading 플래그)
    - Nested Canvas 제거 (부모 Canvas 활용)
- [x] **Shop 시스템 구현 완료** (17개 파일)
  - Phase A: ShopProductType, ShopProductData, ShopProductDatabase, ShopPurchaseRecord
  - Phase B: ShopEvents (ProductPurchasedEvent, ProductPurchaseFailedEvent)
  - Phase C: PurchaseLimitValidator, ShopHandler (구매 제한, 재화 검증)
  - Phase D: IShopProvider, NormalShopProvider, EventShopProvider, ShopState, ShopScreen, ShopProductItem
  - Phase E: LobbyScreen Shop 버튼, DataManager/NetworkManager Database 주입
  - Phase F: PurchaseLimitValidatorTests, ShopHandlerTests
  - UserSaveData v4 마이그레이션 (ShopPurchaseRecords 필드)
- [x] **Main Scene 초기화 시스템 구현** (Session 2)
  - IInitStep 인터페이스 + InitializationSequence 서비스
  - 4개 초기화 스텝: AssetManager, NetworkManager, DataManager, Login
  - GameBootstrap 리팩토링 (순차 초기화 + 재시도 로직)
  - MainSceneSetup 에디터 도구 (SetupTab 통합)
  - Canvas 계층: Screen(10), Popup(50), Header(80), Loading(100)
- [x] **LiveEvent 테스트 추가** (115개)
  - Data 테스트: LiveEventData, Database, Progress, EventCurrency (84개)
  - LocalServer 테스트: EventHandler, EventCurrencyConverter (31개)
  - Mock 클래스: TestServerTimeService, MockLiveEventDatabase, MockServerTimeService
- [x] EventType → LiveEventType 리팩토링
  - UnityEventType 충돌 방지
  - Limited, Collab 이넘 값 추가
- [x] **LiveEvent 시스템 구현 완료** (30개 파일)
  - Phase A: Enums + 구조체 (5개)
  - Phase B: SO + UserData + Migration v3 (7개)
  - Phase C: Request/Response (6개)
  - Phase D: Events + Handler (3개)
  - Phase E: UI Assembly + Screen (4개)
  - Phase F: EventDetailScreen + Tabs (4개)
  - Phase G: 재화 전환 + 통합 (3개)
  - 추가: TabWidget 범용 클래스 (2개)
- [x] LiveEvent 구현 계획 수립
  - 구현 범위 확정 (Mission 구조만, Stage/Shop Tab UI만, 재화 전환 전체)
  - 7개 Phase로 작업 단위 분리
  - [IMPLEMENTATION_PLAN.md](Specs/LiveEvent/IMPLEMENTATION_PLAN.md) 작성
- [x] LocalServer 단위 테스트 (40개)
- [x] Request/Response 타입 Sc.Data로 이동

### 2026-01-19
- [x] Sc.LocalServer Assembly 분리
- [x] AssetManager 통합, RewardIconCache 대체
- [x] PlayMode 테스트 인프라 구축
- [x] 에디터 도구 리팩토링 (SC Tools 메뉴 재구성)

<details>
<summary>이전 작업 로그</summary>

### 2026-01-18~19
- SaveManager, LoadingIndicator, Reward, TimeService 구현
- SystemPopup, RewardPopup 구현
- NUnit 단위 테스트 149개

### 2026-01-16~17
- 아웃게임 아키텍처 V1 마일스톤 설계
- Screen/Popup Transition 애니메이션
- ScreenHeader, CharacterDetailScreen
- 재화 시스템 확장 (16개 CostType)

### 2026-01-15
- MVP 화면 구현 (Title, Lobby, Gacha, CharacterList)
- 네트워크 이벤트 큐 아키텍처
- 데이터 아키텍처 v2.0 (서버 중심)

### 2026-01-14
- 프로젝트 초기 설정
- Assembly 기반 아키텍처 설계
- 스펙 문서 작성

</details>

---

## 참조

| 문서 | 용도 |
|------|------|
| [OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md) | 마일스톤 상세 |
| [ARCHITECTURE.md](ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [SPEC_INDEX.md](Specs/SPEC_INDEX.md) | Assembly별 스펙 목록 |
| [DECISIONS.md](Portfolio/DECISIONS.md) | 의사결정 기록 |
