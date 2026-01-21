# OUTGAME-V1 Changelog

마일스톤 작업 로그 아카이브.

---

## 2026-01-21

### GachaEnhancement 시스템 (Phase A~F)

**파일 13개 생성/수정**

| Phase | 항목 | 파일 |
|-------|------|------|
| A | 마스터 데이터 확장 | GachaPoolData.cs, GachaPool.json, MasterDataImporter.cs |
| B | 유저 데이터 확장 | GachaHistoryRecord.cs, UserSaveData.cs (v8) |
| C | GachaScreen 리팩토링 | GachaBannerItem.cs, GachaScreen.cs |
| D | RateDetailPopup | RateDetailPopup.cs, RateDetailState.cs |
| E | GachaHistoryScreen | GachaHistoryScreen.cs, GachaHistoryState.cs, GachaHistoryItem.cs |
| F | Server 로직 | GachaService.cs, GachaHandler.cs, GachaResponse.cs |

**테스트**: GachaServiceTests.cs (12개), GachaHistoryRecordTests.cs (16개)

---

### NavigationEnhancement 시스템 (Phase A~E)

**파일 13개 생성/수정**

| Phase | 항목 | 파일 |
|-------|------|------|
| A | Core 배지 시스템 | BadgeType.cs, IBadgeProvider.cs, BadgeManager.cs |
| B | Lobby Tabs | LobbyTabContent.cs, HomeTabContent.cs, CharacterTabContent.cs, GachaTabContent.cs, SettingsTabContent.cs |
| C | Badge Providers | EventBadgeProvider.cs, ShopBadgeProvider.cs, GachaBadgeProvider.cs |
| D | LobbyScreen 리팩토링 | LobbyScreen.cs |
| E | 프리팹 재구성 도구 | LobbyScreenSetup.cs |

---

### CharacterEnhancement 시스템 (Phase A~F)

**파일 21개 생성, 3개 수정**

| Phase | 항목 | 파일 |
|-------|------|------|
| A | 데이터 레이어 | CharacterStats.cs, LevelRequirement.cs, AscensionRequirement.cs, CharacterLevelDatabase.cs, CharacterAscensionDatabase.cs, PowerCalculator.cs, CharacterLevel.json, CharacterAscension.json |
| B | 서버 레이어 | CharacterLevelUpRequest/Response, CharacterAscensionRequest/Response, CharacterEvents.cs, CharacterLevelUpHandler.cs, CharacterAscensionHandler.cs |
| C~D | UI | CharacterLevelUpPopup.cs, CharacterAscensionPopup.cs |
| E | 통합 | ItemData.cs, CharacterDetailScreen.cs, DataManager.cs |
| F | 테스트 | CharacterLevelUpHandlerTests.cs (13개), CharacterAscensionHandlerTests.cs (13개) |

---

### Stage 시스템 (Phase J)

**테스트 47개**
- StageEntryValidatorTests.cs (21개)
- StageHandlerTests.cs (26개)

---

## 2026-01-20

### Stage 시스템 (Phase A~I)

**Phase A**: Stage.json v2.0
- ContentType, CategoryId, StarConditions, FirstClearRewards, RepeatClearRewards

**Phase E~F**: Screens & Widgets
- InGameContentDashboard.cs
- StageDashboard.cs
- StageSelectScreen.cs
- PartySelectScreen.cs (플레이스홀더)
- StageListPanel.cs
- StageItemWidget.cs
- ContentCategoryItem.cs

**Phase G**: Content Modules
- ExpDungeonContentModule.cs
- BossRaidContentModule.cs
- TowerContentModule.cs
- EventStageContentModule.cs

**Phase H~I**: Popup & 연동
- StageInfoPopup.cs, StageInfoState.cs
- EventStageTab.cs 수정

---

### PartyPreset 시스템

- PartyPreset.cs
- UserSaveData v6 마이그레이션

---

### LobbyEntryTask 시스템 (11개 파일)

| Phase | 파일 |
|-------|------|
| A (Core) | ILobbyEntryTask.cs, IPopupQueueService.cs, LobbyTaskResult.cs, LobbyEntryTaskRunner.cs, LobbyEvents.cs |
| B (Common) | PopupQueueService.cs |
| C (Tasks) | AttendanceCheckTask.cs, EventCurrencyConversionTask.cs, NewEventNotificationTask.cs |
| D (통합) | LobbyScreen.cs, DataManager.cs |

---

### Shop 시스템 (17개 파일)

| Phase | 파일 |
|-------|------|
| A | ShopProductType.cs, ShopProductData.cs, ShopProductDatabase.cs, ShopPurchaseRecord.cs |
| B | ShopEvents.cs |
| C | PurchaseLimitValidator.cs, ShopHandler.cs |
| D | IShopProvider.cs, NormalShopProvider.cs, EventShopProvider.cs, ShopState.cs, ShopScreen.cs, ShopProductItem.cs |
| E | LobbyScreen, DataManager, NetworkManager 연동 |
| F | PurchaseLimitValidatorTests.cs, ShopHandlerTests.cs |

---

### Main Scene 초기화 시스템 (Session 2)

- IInitStep 인터페이스 + InitializationSequence
- 4개 초기화 스텝: AssetManager, NetworkManager, DataManager, Login
- GameBootstrap 리팩토링
- MainSceneSetup 에디터 도구
- Canvas 계층: Screen(10), Popup(50), Header(80), Loading(100)

---

### Main Scene 프리팹 자동화 (Session 3)

**Track A**: UI 런타임 로딩
- ScreenWidget/PopupWidget Addressables 전환
- 하이브리드 방식, AssetScope 기반 메모리 관리

**Track B**: 프리팹 생성 시스템
- PrefabGenerator.cs
- Addressables 자동 등록

---

### LiveEvent 시스템 (30개 파일)

| Phase | 항목 |
|-------|------|
| A | Enums + 구조체 (5개) |
| B | SO + UserData + Migration v3 (7개) |
| C | Request/Response (6개) |
| D | Events + Handler (3개) |
| E | UI Assembly + Screen (4개) |
| F | EventDetailScreen + Tabs (4개) |
| G | 재화 전환 + 통합 (3개) |

**테스트 115개**: Data(84개), LocalServer(31개)

---

### 기타

- EventType → LiveEventType 리팩토링
- LocalServer 단위 테스트 40개
- Request/Response 타입 Sc.Data 이동

---

## 2026-01-19

### LocalServer Assembly 분리

- Sc.LocalServer Assembly 생성
- LocalApiClient 354줄 → 157줄

### AssetManager 통합

- IAssetHandle 인터페이스
- RewardIconCache 대체

### PlayMode 테스트 인프라

- PlayModeTestBase
- 에디터 도구 리팩토링 (SC Tools 메뉴 재구성)

---

## 2026-01-18~19

### Foundation & Common 시스템

- SaveManager, LoadingIndicator, Reward, TimeService
- SystemPopup, RewardPopup
- NUnit 단위 테스트 149개

---

## 2026-01-16~17

### 아웃게임 아키텍처 V1 마일스톤 설계

- Screen/Popup Transition 애니메이션
- ScreenHeader, CharacterDetailScreen
- 재화 시스템 확장 (16개 CostType)

---

## 2026-01-15

### MVP 화면 구현

- Title, Lobby, Gacha, CharacterList Screen
- 네트워크 이벤트 큐 아키텍처
- 데이터 아키텍처 v2.0 (서버 중심)

---

## 2026-01-14

### 프로젝트 초기 설정

- Assembly 기반 아키텍처 설계
- 스펙 문서 작성
