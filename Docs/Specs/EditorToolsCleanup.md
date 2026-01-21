# Editor Tools 정리 계획

## 현재 상태 분석

### 파일 목록 (총 20개)

#### Editor/Wizard/ (10개)
| 파일 | 용도 | 상태 |
|------|------|------|
| ProjectSetupWizard.cs | 메인 윈도우 | **유지** |
| SetupTab.cs | Setup 탭 UI | **유지** (단순화) |
| DebugTab.cs | Debug 탭 UI | **유지** |
| DataTab.cs | Data 탭 - JSON/Database 관리 | **유지** |
| SettingsTab.cs | Settings 탭 | **유지** |
| AddressableSetupTool.cs | Addressables 그룹 설정 | **유지** |
| MainSceneSetup.cs | Main 씬 생성 | **유지** |
| Generators/PrefabGenerator.cs | Screen/Popup 프리팹 생성 | **유지** (확장) |
| Generators/LobbyScreenSetup.cs | Lobby 탭 설정 | **통합** → PrefabGenerator |
| Setup/DebugPanelSetup.cs | 디버그 패널 추가 | **유지** |

#### Editor/AI/ (10개)
| 파일 | 용도 | 상태 |
|------|------|------|
| EditorUIHelpers.cs | UI 생성 유틸리티 | **유지** |
| ProjectEditorSettings.cs | 에디터 설정 | **유지** |
| IsExternalInit.cs | C# 폴리필 | **유지** |
| MVPSceneSetup.cs | MVP 씬 생성 | **삭제** (레거시) |
| UITestSceneSetup.cs | 테스트 씬 | **삭제** (불필요) |
| LoadingSetup.cs | 로딩 테스트 | **삭제** (불필요) |
| PlayModeTestSetup.cs | 테스트 프리팹 | **삭제** (불필요) |
| SystemPopupSetup.cs | 다이얼로그 프리팹 | **삭제** (PrefabGenerator에서 처리) |
| NavigationDebugWindow.cs | 네비게이션 디버그 | **삭제** (DebugTab에 통합됨) |
| DataFlowTestWindow.cs | 데이터 흐름 테스트 | **삭제** (필요시 재구현) |

---

## 정리 후 목표 구조

### 최종 파일 (12개)

```
Editor/
├── Wizard/
│   ├── ProjectSetupWizard.cs     # 메인 윈도우
│   ├── Tabs/
│   │   ├── SetupTab.cs           # 4버튼 단순화
│   │   ├── DebugTab.cs           # 런타임 디버그
│   │   ├── DataTab.cs            # 데이터 관리
│   │   └── SettingsTab.cs        # 설정
│   └── Tools/
│       ├── PrefabGeneratorTool.cs    # 통합 프리팹 생성
│       ├── SceneSetupTool.cs         # Main 씬 전용
│       ├── AddressableSetupTool.cs   # Addressables
│       └── DebugPanelSetup.cs        # 디버그 패널
└── Utils/
    ├── EditorUIHelpers.cs        # UI 유틸리티
    └── ProjectEditorSettings.cs  # 에디터 설정
```

---

## 핵심 워크플로우

### Setup 탭: 4단계 버튼

```
┌─────────────────────────────────────────┐
│  1. [Generate UI Prefabs]               │
│     - 모든 Screen 프리팹 생성 (13개)    │
│     - 모든 Popup 프리팹 생성 (8개)      │
│     - Lobby TabButton 포함              │
├─────────────────────────────────────────┤
│  2. [Setup Addressables]                │
│     - UI_Screens 그룹                   │
│     - UI_Popups 그룹                    │
│     - UI_Widgets 그룹                   │
├─────────────────────────────────────────┤
│  3. [Setup Main Scene]                  │
│     - Managers (Navigation, Data, etc.) │
│     - UIRoot + Canvas 계층              │
│     - GameBootstrap 초기화              │
├─────────────────────────────────────────┤
│  4. [Add Debug Panel] (Optional)        │
│     - F12 토글 디버그 UI                │
│     - Development Build 전용            │
└─────────────────────────────────────────┘
```

### Data 탭: 데이터 관리

```
┌─────────────────────────────────────────┐
│  JSON Files                             │
│  - CharacterData.json                   │
│  - SkillData.json                       │
│  - ItemData.json                        │
│  - StageData.json                       │
│  - GachaPoolData.json                   │
│  - LiveEventData.json                   │
├─────────────────────────────────────────┤
│  Generated Assets                       │
│  - CharacterDatabase.asset              │
│  - SkillDatabase.asset                  │
│  - ItemDatabase.asset                   │
│  - StageDatabase.asset                  │
│  - GachaPoolDatabase.asset              │
│  - LiveEventDatabase.asset              │
├─────────────────────────────────────────┤
│  [Regenerate All]  [Delete All]         │
└─────────────────────────────────────────┘
```

---

## 삭제 대상 MenuItem 목록

현재 Editor/AI에서 제공하는 불필요한 메뉴:

```
SC Tools/Setup/Scenes/MVP/Rebuild All (Full Reset)
SC Tools/Setup/Scenes/Setup MVP Scene
SC Tools/Setup/Prefabs/Create MVP Prefabs
SC Tools/Setup/Prefabs/MVP/Recreate Prefabs (Force)
SC Tools/Setup/Prefabs/Delete MVP Prefabs
SC Tools/Setup/Scenes/Clear MVP Objects
SC Tools/Setup/Scenes/Setup Loading Test Scene
SC Tools/Setup/Scenes/Clear Loading Test Objects
SC Tools/Setup/Prefabs/Create Loading Prefab
SC Tools/Setup/Scenes/Setup Test Scene
SC Tools/Setup/Scenes/Clear Test Objects
SC Tools/Setup/Prefabs/Test/* (모든 테스트 프리팹)
SC Tools/Setup/Prefabs/Dialog/* (PrefabGenerator로 이동)
SC Tools/Debug/Navigation Debug Window (DebugTab에 통합)
SC/Test/Data Flow Test Window
```

---

## 구현된 Screen/Popup 목록

### Screens (13개)
1. TitleScreen
2. LobbyScreen
3. GachaScreen
4. GachaHistoryScreen
5. CharacterListScreen
6. CharacterDetailScreen
7. ShopScreen
8. LiveEventScreen
9. EventDetailScreen
10. InGameContentDashboard
11. StageDashboard
12. StageSelectScreen
13. PartySelectScreen

### Popups (8개)
1. ConfirmPopup
2. CostConfirmPopup
3. RewardPopup
4. GachaResultPopup
5. RateDetailPopup
6. CharacterLevelUpPopup
7. CharacterAscensionPopup
8. StageInfoPopup

---

## 작업 순서 (✅ 2026-01-22 완료)

### Phase 1: 파일 삭제 ✅
- [x] Editor/AI/MVPSceneSetup.cs 삭제
- [x] Editor/AI/UITestSceneSetup.cs 삭제
- [x] Editor/AI/LoadingSetup.cs 삭제
- [x] Editor/AI/PlayModeTestSetup.cs 삭제
- [x] Editor/AI/SystemPopupSetup.cs 삭제
- [x] Editor/AI/NavigationDebugWindow.cs 삭제
- [x] Editor/AI/DataFlowTestWindow.cs 삭제
- [x] Editor/Wizard/Generators/LobbyScreenSetup.cs 삭제

### Phase 2: 폴더 구조 정리 ✅
- [x] 현재 구조 유지 (기존 Wizard/, Generators/, Setup/ 활용)
- 추가 폴더 생성 불필요 (현재 구조가 명확함)

### Phase 3: SetupTab 단순화 ✅
- [x] 4버튼 UI로 재설계 (Prefabs→Addressables→Scene→Debug)
- [x] 레거시 섹션 제거
- [x] DebugTab 단순화 (Navigation 디버그만 유지)

### Phase 4: PrefabGenerator 통합 ✅
- [x] PrefabGenerator에 이미 모든 기능 포함 확인
- LobbyScreenSetup/SystemPopupSetup 기능은 이미 PrefabGenerator에서 처리

---

## 참고: UI 디자인

프리팹 UI 디자인은 `frontend-design` 스킬 사용:
```
/frontend-design [컴포넌트명]
```

예시:
- `/frontend-design LobbyScreen 탭 시스템`
- `/frontend-design GachaResultPopup 연출`
