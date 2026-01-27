# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## ✅ 완료된 마일스톤: OUTGAME-V1

> **완료일**: 2026-01-21
> **상세 문서**: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)
> **작업 로그**: [Milestones/OUTGAME_V1_CHANGELOG.md](Milestones/OUTGAME_V1_CHANGELOG.md)

### 시스템 구현 현황

| Phase | 시스템 | 상태 |
|-------|--------|------|
| A | Logging, ErrorHandling | ✅ |
| B | SaveManager, LoadingIndicator | ✅ |
| C | Reward, TimeService | ✅ |
| D | SystemPopup, RewardPopup | ✅ |
| E | LocalServer 분리 | ✅ |
| F | LiveEvent, Shop, Stage | ✅ |
| F | GachaEnhancement, CharacterEnhancement, NavigationEnhancement | ✅ |

### 테스트 현황

| 영역 | 테스트 수 |
|------|----------|
| Foundation, Core, Common, Reward | 149개 |
| LocalServer | 40개 |
| LiveEvent | 115개 |
| Stage | 47개 |
| CharacterEnhancement | 26개 |
| GachaEnhancement | 28개 |
| **총계** | **405개** |

---

## ⚠️ 기술 부채

> **상세**: [SPEC_INDEX.md 간극 요약](Specs/SPEC_INDEX.md#문서-구현-간극-요약-2026-01-21)

### 미구현 (문서만 존재)

| 우선순위 | 항목 | 스펙 문서 |
|---------|------|----------|
| HIGH | Utility (CollectionExtensions, MathHelper) | Common/Utility.md |
| MEDIUM | AudioManager | Core/AudioManager.md |
| LOW | SceneLoader | Core/SceneLoader.md |
| LOW | DeepLink 시스템 | Common/NavigationEnhancement.md |
| LOW | Badge 시스템 | Common/NavigationEnhancement.md |

### 플레이스홀더 (부분 구현)

| 항목 | 시스템 | 현재 상태 |
|------|--------|----------|
| EventMissionTab | LiveEvent | UI만 존재, 기능 미구현 |
| EventShopTab | LiveEvent/Shop | UI만 존재, Provider 연동 안됨 |
| PartySelectScreen | Stage | 플레이스홀더 상태 |
| AttendanceCheckTask | Lobby | Stub 구현 |
| NewEventNotificationTask | Lobby | Stub 구현 |
| ClaimEventMission API | LiveEvent | 에러코드 6099 반환 |

---

## ✅ 완료: EDITOR-CLEANUP

> **완료일**: 2026-01-22
> **목표**: Editor 도구 정리 및 단순화
> **상세 문서**: [Specs/EditorToolsCleanup.md](Specs/EditorToolsCleanup.md)

### 완료 내용

| Phase | 작업 | 상태 |
|-------|------|------|
| 1 | 레거시 파일 삭제 (8개) | ✅ |
| 2 | 폴더 구조 정리 | ✅ |
| 3 | SetupTab 4버튼 단순화 | ✅ |
| 4 | PrefabGenerator 통합 확인 | ✅ |

### 삭제된 파일 (8개)

- MVPSceneSetup.cs, UITestSceneSetup.cs, LoadingSetup.cs
- PlayModeTestSetup.cs, SystemPopupSetup.cs
- NavigationDebugWindow.cs, DataFlowTestWindow.cs
- LobbyScreenSetup.cs

### 최종 Editor 구조

```
Editor/Wizard/
├── ProjectSetupWizard.cs    # 메인 윈도우
├── SetupTab.cs              # 4단계 설정 (Prefabs→Addressables→Scene→Debug)
├── DebugTab.cs              # 런타임 Navigation 디버그
├── DataTab.cs               # 마스터 데이터 관리
├── SettingsTab.cs           # 에디터 설정
├── AddressableSetupTool.cs  # Addressables 그룹 설정
├── MainSceneSetup.cs        # Main 씬 생성
├── Generators/PrefabGenerator.cs  # Screen/Popup 프리팹 생성
└── Setup/DebugPanelSetup.cs       # 디버그 패널 추가
```

---

## ✅ 완료: UI-PREFAB

> **기간**: 2026-01-22 ~ 2026-01-26
> **목표**: UI Prefab 규칙화 및 구조 재정립
> **계획 문서**: [.claude/plans/joyful-dancing-hennessy.md](../.claude/plans/joyful-dancing-hennessy.md)

### Phase 1: UI 문서화 (진행 중)

레퍼런스 이미지 기반 Screen UI 레이아웃 문서화

| Screen | 레퍼런스 | 스펙 문서 | 상태 |
|--------|----------|-----------|------|
| LobbyScreen | Lobby.jpg | Lobby.md | ✅ |
| CharacterListScreen | CharacterList.jpg | Character.md | ✅ |
| CharacterDetailScreen | CharacterDetail.jpg | Character.md | ✅ |
| ShopScreen | Shop.jpg | Shop.md | ✅ |
| GachaScreen | Gacha.jpg | Gacha.md | ✅ |
| LiveEventScreen | LiveEvent.jpg | LiveEvent.md | ✅ |
| StageSelectScreen | StageSelectScreen.jpg | Stage.md | ✅ |
| PartySelectScreen | PartySelect.jpg | Stage.md | ✅ |
| InGameContentDashboard | StageDashboard.jpg | Stage.md | ✅ |
| InventoryScreen | Inventory.jpg | Inventory.md | ✅ |

**작업 가이드**: [Design/UI_DOCUMENTATION_GUIDE.md](Design/UI_DOCUMENTATION_GUIDE.md)
**작업 정의서**: [Design/UI_DOCUMENTATION_TASKS.md](Design/UI_DOCUMENTATION_TASKS.md)

### ✅ LobbyScreen 프리팹 자동화 완료

> **완료일**: 2026-01-22

기존 Tab 기반 구조를 Lobby.md 스펙 기반 Navigation Button 구조로 재구현

| 작업 | 상태 |
|------|------|
| Widget 클래스 생성 (6개) | ✅ |
| LobbyScreen.cs 스펙 기반 재작성 | ✅ |
| LobbyScreenPrefabBuilder.cs 재구현 | ✅ |
| Tab 관련 파일 삭제 | ✅ |
| 빌드 테스트 | ✅ |

**생성된 Widget (Lobby/Widgets/)**:
- EventBannerCarousel.cs - 배너 슬라이드
- StageProgressWidget.cs - 스테이지 진행
- QuickMenuButton.cs - 퀵메뉴 (2x4 그리드)
- PassButton.cs - 패스 버튼 (4개)
- ContentNavButton.cs - 하단 네비게이션 (7개)
- CharacterDisplayWidget.cs - 캐릭터 디스플레이

**삭제된 파일 (Lobby/Tabs/)**:
- LobbyTabContent.cs, HomeTabContent.cs, CharacterTabContent.cs
- GachaTabContent.cs, SettingsTabContent.cs

### ✅ Phase 2: PrefabGenerator 확장 (완료)

> **완료일**: 2026-01-22

| 작업 | 파일 | 상태 |
|------|------|------|
| UITheme.cs | Editor/Wizard/Generators/ | ✅ |
| UIComponentBuilder.cs | Editor/Wizard/Generators/ | ✅ |
| ScreenTemplateFactory.cs | Editor/Wizard/Generators/ | ✅ |
| PopupTemplateFactory.cs | Editor/Wizard/Generators/ | ✅ |
| ScreenTemplateAttribute.cs | Common/UI/Attributes/ | ✅ |
| PopupTemplateAttribute.cs | Common/UI/Attributes/ | ✅ |
| PrefabGenerator 수정 | Editor/Wizard/Generators/ | ✅ |

**Screen Attribute 적용 (7개)**:
- TitleScreen (FullScreen), LobbyScreen (Tabbed)
- GachaScreen, GachaHistoryScreen, ShopScreen, LiveEventScreen (Standard)
- EventDetailScreen (Detail)

**Popup Attribute 적용 (7개)**:
- ConfirmPopup, RewardPopup, CostConfirmPopup, StageInfoPopup
- RateDetailPopup, GachaResultPopup, CharacterLevelUpPopup

### ✅ Phase 3: 테스트 프리팹 정리 (완료)

> **완료일**: 2026-01-22

| 작업 | 상태 |
|------|------|
| 테스트 프리팹 폴더 삭제 (Tests, UI/Tests) | ✅ |
| 테스트 Screen/Popup 프리팹 삭제 | ✅ |
| 테스트 스크립트 삭제 (Common/UI/Tests, Tests/TestWidgets) | ✅ |
| Navigation 테스트 러너/시나리오 삭제 | ✅ |

**삭제된 파일 (46개)**: 7,365줄 삭제

### ✅ Phase 4: PrefabSync 시스템 구축 (완료)

> **목표**: 프리팹 ↔ 코드 양방향 동기화 시스템
> **완료일**: 2026-01-26

#### 시스템 개요

```
[Prefab] → Analyzer → [JSON Spec] → Generator → [Builder Code]
                           ↑                          ↓
                     사용자 수정              프리팹 재생성
```

#### 구현 현황

| 파일 | 용도 | 상태 |
|------|------|------|
| `PrefabSync/PrefabStructureSpec.cs` | JSON 직렬화 모델 | ✅ |
| `PrefabSync/PrefabStructureAnalyzer.cs` | Prefab → JSON 변환 | ✅ |
| `PrefabSync/PrefabBuilderGenerator.cs` | JSON → C# Builder 생성 | ✅ |
| `PrefabSync/PrefabSyncWindow.cs` | 통합 에디터 윈도우 | ✅ |
| `PrefabSync/ManualBuilderExecutor.cs` | 수동 빌더 실행 및 파이프라인 | ✅ |

#### 기능 목록

| # | 기능 | 입력 | 출력 |
|---|------|------|------|
| 1 | Prefab → JSON Spec | 프리팹 | JSON |
| 2 | JSON Spec → Generated Code | JSON | .Generated.cs |
| 3 | **Build from Manual** | 수동 Builder | Prefab → JSON |
| 4 | Full Sync (1+2) | 프리팹 | JSON + Generated |
| 5 | **Full Pipeline (3+1+2)** | 수동 Builder | Prefab + JSON + Generated |

#### PrefabGenerator 동작 순서 (2026-01-27)

```
1. Generated 빌더 검색 ({TypeName}PrefabBuilder_Generated)
   ↓ 있으면 사용
2. Manual 빌더 검색 ({TypeName}PrefabBuilder)
   ↓ 있으면 사용
3. 템플릿 팩토리 사용 (ScreenTemplateFactory / PopupTemplateFactory)
   + 경고 로그 출력
```

- Generated 클래스 이름: `{Name}PrefabBuilder_Generated`
- Manual 클래스 이름: `{Name}PrefabBuilder`
- 빈 Manual 빌더 8개 삭제됨 (Generated로 대체)

#### 해결된 이슈

**CS0111 메서드 이름 충돌** → 인덱스 접미사 방식으로 해결
- 중복 노드명: `CreateLabel_1()`, `CreateLabel_2()` 등

#### LobbyScreen 테스트 완료

- ✅ PrefabSync로 LobbyScreen 분석 → JSON Spec 생성
- ✅ JSON Spec → LobbyScreenPrefabBuilder.Generated.cs 생성
- ✅ PrefabGenerator에 연결하여 프리팹 재생성 확인

---

### ✅ Phase 5: Popup 재생성 및 PrefabSync 확장 (완료)

> **완료일**: 2026-01-26

| 작업 | 상태 |
|------|------|
| CharacterAscensionPopup에 PopupTemplateAttribute 추가 | ✅ |
| UIComponentBuilder.CreateChild RectTransform 버그 수정 | ✅ |
| Popup 8개 재생성 | ✅ |
| LobbyScreen PrefabSync 적용 | ✅ (Phase 4) |
| TitleScreen - 기존 수동 빌더 유지 | ✅ |
| 다른 Screen - 템플릿 기본 구조 유지 | ✅ |

**재생성된 Popup (8개)**:
- ConfirmPopup, CostConfirmPopup, RewardPopup, StageInfoPopup
- RateDetailPopup, GachaResultPopup, CharacterLevelUpPopup, CharacterAscensionPopup

---

## ✅ 완료: SCREEN-PREFAB

> **기간**: 2026-01-26 ~ 2026-01-27
> **목표**: Reference 이미지 기반 Screen 프리팹 상세 구현

### 개요

```
[Reference Image] → [스펙 문서] → [ManualBuilder] → [Prefab]
                         ↓                ↓
                    UI 레이아웃      PrefabSync 적용
```

### ✅ Phase 1: 계획 수립 및 문서화 (완료)

| 작업 | 상태 |
|------|------|
| PROGRESS.md 업데이트 | ✅ |
| Screen별 작업 정의 | ✅ |
| 병렬 작업 계획 수립 | ✅ |

**계획 문서**: [SCREEN_PREFAB_PLAN.md](Design/SCREEN_PREFAB_PLAN.md)

### ✅ Phase 2: UI 레이아웃 스펙 문서화 (완료)

| Reference | Screen | 스펙 문서 | 문서화 |
|-----------|--------|-----------|--------|
| Lobby.jpg | LobbyScreen | Lobby.md | ✅ |
| CharacterList.jpg | CharacterListScreen | Character.md | ✅ |
| CharacterDetail.jpg | CharacterDetailScreen | Character.md | ✅ |
| Shop.jpg | ShopScreen | Shop.md | ✅ |
| Gacha.jpg | GachaScreen | Gacha.md | ✅ |
| LiveEvent.jpg | LiveEventScreen | LiveEvent.md | ✅ |
| StageSelectScreen.jpg | StageSelectScreen | Stage.md | ✅ |
| PartySelect.jpg | PartySelectScreen | Stage.md | ✅ |
| StageDashbaord.jpg | InGameContentDashboard | Stage.md | ✅ |
| Inventory.jpg | InventoryScreen | Inventory.md | ✅ |

### ✅ Phase 3: ManualBuilder 구현 (완료)

> **작업 지시서**: [Docs/Design/Tasks/MANUAL_BUILDER_OVERVIEW.md](Design/Tasks/MANUAL_BUILDER_OVERVIEW.md)

#### 이미 구현된 ManualBuilder

| Screen | Builder 파일 | 상태 |
|--------|-------------|------|
| TitleScreen | TitleScreenPrefabBuilder.cs | ✅ |
| InGameContentDashboard | InGameContentDashboardPrefabBuilder.cs | ✅ |
| LobbyScreen | LobbyScreenPrefabBuilder.Generated.cs | ✅ Generated |

#### 구현 대상 (8개)

| # | Screen | 작업 지시서 | 난이도 | 상태 |
|---|--------|-------------|--------|------|
| 1 | CharacterListScreen | [TASK_01](Design/Tasks/TASK_01_CharacterListScreen.md) | 중 | ✅ |
| 2 | CharacterDetailScreen | [TASK_02](Design/Tasks/TASK_02_CharacterDetailScreen.md) | 상 | ✅ |
| 3 | ShopScreen | [TASK_03](Design/Tasks/TASK_03_ShopScreen.md) | 중 | ✅ |
| 4 | GachaScreen | [TASK_04](Design/Tasks/TASK_04_GachaScreen.md) | 중 | ✅ |
| 5 | LiveEventScreen | [TASK_05](Design/Tasks/TASK_05_LiveEventScreen.md) | 중 | ✅ |
| 6 | StageSelectScreen | [TASK_06](Design/Tasks/TASK_06_StageSelectScreen.md) | 상 | ✅ |
| 7 | PartySelectScreen | [TASK_07](Design/Tasks/TASK_07_PartySelectScreen.md) | 상 | ✅ |
| 8 | InventoryScreen | [TASK_08](Design/Tasks/TASK_08_InventoryScreen.md) | 중 | ✅ |

#### 실행 방법

```bash
# 단일 작업
claude "Docs/Design/Tasks/TASK_01_CharacterListScreen.md 작업 진행해줘"

# 병렬 작업 (3개 터미널)
claude "Docs/Design/Tasks/TASK_01_CharacterListScreen.md 작업 진행해줘"  # Terminal 1
claude "Docs/Design/Tasks/TASK_03_ShopScreen.md 작업 진행해줘"          # Terminal 2
claude "Docs/Design/Tasks/TASK_06_StageSelectScreen.md 작업 진행해줘"   # Terminal 3
```

### ✅ Phase 4: PrefabSync 적용 (완료)

> **완료일**: 2026-01-27

| 작업 | 상태 |
|------|------|
| Screen JSON Spec 생성 (11개) | ✅ |
| Popup JSON Spec 생성 (11개) | ✅ |
| Generated Builder 생성 (20개) | ✅ |
| Prefab 생성 완료 (22개) | ✅ |

### 참조 문서

| 문서 | 용도 |
|------|------|
| [MANUAL_BUILDER_OVERVIEW.md](Design/Tasks/MANUAL_BUILDER_OVERVIEW.md) | **ManualBuilder 작업 개요** |
| [UI_DOCUMENTATION_GUIDE.md](Design/UI_DOCUMENTATION_GUIDE.md) | 문서화 가이드 |
| [UI_DOCUMENTATION_TASKS.md](Design/UI_DOCUMENTATION_TASKS.md) | 작업 정의서 |
| Specs/{Assembly}.md | 각 Screen UI 레이아웃 스펙 |

---

## 🚀 이후 단계

1. **인게임 전투 시스템 (BATTLE-V1)** - 핵심 게임플레이
2. **기술 부채 해소** - Utility, AudioManager, SceneLoader
3. **플레이스홀더 완성** - EventMission, EventShop

---

## 참조

| 문서 | 용도 |
|------|------|
| [OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md) | 마일스톤 상세 |
| [OUTGAME_V1_CHANGELOG.md](Milestones/OUTGAME_V1_CHANGELOG.md) | 상세 작업 로그 |
| [ARCHITECTURE.md](ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [SPEC_INDEX.md](Specs/SPEC_INDEX.md) | Assembly별 스펙 목록 |
| [DECISIONS.md](Portfolio/DECISIONS.md) | 의사결정 기록 |
| [JOURNEY.md](Portfolio/JOURNEY.md) | 프로젝트 여정 |
