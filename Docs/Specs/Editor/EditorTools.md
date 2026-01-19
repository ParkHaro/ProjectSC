---
type: guide
assembly: Sc.Editor
category: Editor
status: active
version: "2.1"
created: 2025-01-15
updated: 2026-01-19
---

# 에디터 도구

## 목적

Unity Editor 메뉴를 통해 씬/프리팹 셋업을 자동화하는 도구 모음.

**메뉴 루트**: `SC Tools/`

---

## 메뉴 구조

```
SC Tools/
├── Setup/                          # 씬/프리팹 생성 도구
│   ├── Prefabs/
│   │   ├── Create All Test Prefabs
│   │   ├── Create MVP Prefabs
│   │   ├── Create Loading Prefab
│   │   ├── Delete All Test Prefabs
│   │   ├── Test/                   # 테스트 프리팹 개별 생성
│   │   ├── MVP/                    # MVP 프리팹 개별 생성
│   │   └── Dialog/                 # 다이얼로그 프리팹
│   │       ├── Create All Dialog Prefabs
│   │       ├── Create ConfirmPopup Prefab
│   │       └── Create CostConfirmPopup Prefab
│   │
│   ├── Scenes/
│   │   ├── Setup Test Scene
│   │   ├── Setup Loading Test Scene
│   │   ├── Setup MVP Scene
│   │   ├── MVP/                    # MVP 씬 서브메뉴
│   │   │   └── Rebuild All (Full Reset)
│   │   ├── Clear Test Objects
│   │   ├── Clear Loading Test Objects
│   │   └── Clear MVP Objects
│   │
│   └── Master Data/
│       └── Generate All
│
├── Debug/                          # 디버깅 도구
│   └── Navigation Debug Window
│
├── Data/                           # 데이터 도구
│   └── Master Data Generator
│
└── Settings/                       # 설정
    ├── Project Editor Settings
    └── Reset to Defaults
```

---

## Bootstrap 레벨

각 씬 셋업 도구는 다른 수준의 Bootstrap을 제공합니다:

| Bootstrap 레벨 | 설명 | 도구 |
|----------------|------|------|
| **None** | 프리팹 생성만 (씬 구성 없음) | PlayModeTestSetup, SystemPopupSetup |
| **Partial** | EventSystem + 일부 매니저 | UITestSceneSetup (NavigationManager), LoadingSetup (EventSystem만) |
| **Full** | 모든 매니저 생성 | MVPSceneSetup (NavigationManager, DataManager, NetworkManager, GameBootstrap, GameFlowController) |

---

## 도구 목록

| 도구 | 메뉴 경로 | 파일 | Bootstrap | 상태 |
|------|-----------|------|-----------|------|
| [테스트 프리팹 생성](#테스트-프리팹-생성) | Setup/Prefabs | PlayModeTestSetup.cs | None | ✅ |
| [다이얼로그 프리팹 생성](#다이얼로그-프리팹-생성) | Setup/Prefabs/Dialog | SystemPopupSetup.cs | None | ✅ |
| [MVP 프리팹 생성](#mvp-프리팹-생성) | Setup/Prefabs | MVPSceneSetup.cs | - | ✅ |
| [로딩 프리팹/씬](#로딩-프리팹씬) | Setup/Prefabs, Setup/Scenes | LoadingSetup.cs | Partial | ✅ |
| [테스트 씬 셋업](#테스트-씬-셋업) | Setup/Scenes | UITestSceneSetup.cs | Partial | ✅ |
| [MVP 씬 셋업](#mvp-씬-셋업) | Setup/Scenes | MVPSceneSetup.cs | Full | ✅ |
| [마스터 데이터 생성](#마스터-데이터-생성) | Setup/Master Data | MVPSceneSetup.cs | - | ✅ |
| [Navigation Debug](#navigation-debug-window) | Debug | NavigationDebugWindow.cs | - | ✅ |
| [Data Flow Test](#data-flow-test-window) | Data | DataFlowTestWindow.cs | - | ✅ |
| [Master Data Generator](#master-data-generator) | Data | MasterDataGeneratorWindow.cs | - | ✅ |
| [Project Settings](#project-settings) | Settings | ProjectEditorSettings.cs | - | ✅ |

---

## 공용 유틸리티

### EditorUIHelpers

**파일**: `Editor/AI/EditorUIHelpers.cs`

모든 Setup 도구에서 공통으로 사용하는 UI 생성 유틸리티.

**주요 메서드**:

| 메서드 | 용도 |
|--------|------|
| `GetProjectFont()` | 프로젝트 기본 폰트 로드 (캐싱) |
| `FindOrCreateCanvas()` | Canvas 찾거나 생성 |
| `CreateFullscreenPanel()` | 전체화면 패널 생성 |
| `CreateCenteredPanel()` | 중앙 정렬 패널 생성 |
| `CreateCenteredText()` | 중앙 정렬 TMP 텍스트 생성 |
| `CreateStretchedText()` | 스트레치 TMP 텍스트 생성 |
| `AddText()` | 기본 TMP 텍스트 컴포넌트 추가 |
| `CreateCenteredButton()` | 중앙 정렬 버튼 생성 |
| `CreateLayoutButton()` | 레이아웃 그룹용 버튼 생성 |
| `CreateUIObject()` | RectTransform 포함 기본 UI 오브젝트 |
| `CreateDimBackground()` | 딤 배경 생성 |
| `EnsureEventSystem()` | EventSystem 찾거나 생성 |
| `SaveAsPrefab()` | 프리팹 저장 (폴더 자동 생성) |
| `EnsureFolder()` | 폴더 경로 존재 확인 및 생성 |

---

## Setup 도구

### 테스트 프리팹 생성

**메뉴**: `SC Tools/Setup/Prefabs/`
**Bootstrap**: None (프리팹 생성 전용)

| 메뉴 | 기능 | 생성 위치 |
|------|------|----------|
| Create All Test Prefabs | 테스트용 프리팹 전체 생성 | `Assets/Prefabs/Tests/` |
| Test/Create Screen Prefab | SimpleTestScreen 생성 | `Assets/Prefabs/Tests/` |
| Test/Create Popup Prefab | SimpleTestPopup 생성 | `Assets/Prefabs/Tests/` |
| Test/Create SystemPopup Prefab | TestSystemPopup 생성 | `Assets/Prefabs/Tests/` |
| Delete All Test Prefabs | 테스트 프리팹 삭제 | - |

**생성 프리팹**:

| 프리팹 | 용도 | 구조 |
|--------|------|------|
| SimpleTestScreen | 기본 Screen 테스트 | Title, Info, Action/Back 버튼 |
| SimpleTestPopup | 기본 Popup 테스트 | Dim 배경, Title, Message, Confirm/Cancel 버튼 |
| TestSystemPopup | SystemPopup 테스트 | Header, Message, ButtonContainer |

**파일**: `Editor/AI/PlayModeTestSetup.cs`

---

### 다이얼로그 프리팹 생성

**메뉴**: `SC Tools/Setup/Prefabs/Dialog/`
**Bootstrap**: None (프리팹 생성 전용)

| 메뉴 | 기능 |
|------|------|
| Create All Dialog Prefabs | 모든 다이얼로그 프리팹 생성 |
| Create ConfirmPopup Prefab | 기본 확인 팝업 생성 |
| Create CostConfirmPopup Prefab | 재화 소모 확인 팝업 생성 |

**생성 프리팹**:

| 프리팹 | 용도 | 컴포넌트 |
|--------|------|----------|
| ConfirmPopup | 확인/취소 다이얼로그 | `ConfirmPopup` |
| CostConfirmPopup | 재화 소모 확인 | `CostConfirmPopup` |

**파일**: `Editor/AI/SystemPopupSetup.cs`

---

### MVP 프리팹 생성

**메뉴**: `SC Tools/Setup/Prefabs/`

| 메뉴 | 기능 |
|------|------|
| Create MVP Prefabs | MVP 화면 프리팹 생성 |
| MVP/Recreate Prefabs (Force) | 기존 삭제 후 재생성 |
| Delete MVP Prefabs | MVP 프리팹 삭제 |

**생성 프리팹**:
- TitleScreen, LobbyScreen, GachaScreen
- CharacterListScreen, CharacterDetailScreen
- GachaResultPopup, CurrencyHUD, ScreenHeader

**파일**: `Editor/AI/MVPSceneSetup.cs`

---

### 로딩 프리팹/씬

**메뉴**: `SC Tools/Setup/Prefabs/`, `SC Tools/Setup/Scenes/`
**Bootstrap**: Partial (EventSystem만 생성)

| 메뉴 | 기능 |
|------|------|
| Prefabs/Create Loading Prefab | LoadingWidget 프리팹 생성 |
| Scenes/Setup Loading Test Scene | 로딩 테스트 씬 구성 |
| Scenes/Clear Loading Test Objects | 로딩 테스트 오브젝트 정리 |

**생성 구조**:

```
LoadingWidget (Canvas)
├── FullScreenPanel      # 전체화면 로딩
├── IndicatorPanel       # 코너 인디케이터
└── ProgressPanel        # 진행률 표시
```

**파일**: `Editor/AI/LoadingSetup.cs`

---

### 테스트 씬 셋업

**메뉴**: `SC Tools/Setup/Scenes/`
**Bootstrap**: Partial (NavigationManager + EventSystem)

| 메뉴 | 기능 |
|------|------|
| Setup Test Scene | UI 테스트 씬 구성 |
| Clear Test Objects | 테스트 오브젝트 정리 |

**생성 구조**:

```
Hierarchy:
├── NavigationManager          # DontDestroyOnLoad
└── TestCanvas
    ├── ScreenContainer        # Sorting Order: 0
    ├── PopupContainer         # Sorting Order: 100
    └── TestControlPanel       # UITestSetup 컴포넌트
```

**파일**: `Editor/AI/UITestSceneSetup.cs`

---

### MVP 씬 셋업

**메뉴**: `SC Tools/Setup/Scenes/`
**Bootstrap**: Full (모든 매니저 생성)

| 메뉴 | 기능 |
|------|------|
| Setup MVP Scene | MVP 게임 씬 구성 |
| MVP/Rebuild All (Full Reset) | 전체 리셋 후 재구성 |
| Clear MVP Objects | MVP 오브젝트 정리 |

**생성 구조**:

```
Hierarchy:
├── NetworkManager             # DontDestroyOnLoad
├── DataManager                # DontDestroyOnLoad
├── GameBootstrap
├── GameFlowController
└── MVPCanvas
    ├── HeaderContainer        # Sorting Order: 50
    ├── ScreenContainer        # Sorting Order: 0
    └── PopupContainer         # Sorting Order: 100
```

**Bootstrap 매니저**:
- NavigationManager
- DataManager
- NetworkManager
- GameBootstrap
- GameFlowController

**파일**: `Editor/AI/MVPSceneSetup.cs`

---

### 마스터 데이터 생성

**메뉴**: `SC Tools/Setup/Master Data/`

| 메뉴 | 기능 |
|------|------|
| Generate All | JSON → ScriptableObject 변환 |

**생성 대상**:
- CharacterDatabase, SkillDatabase, ItemDatabase
- StageDatabase, GachaPoolDatabase
- ScreenHeaderConfigDatabase

**파일**: `Editor/AI/MVPSceneSetup.cs` (Generate 부분)

---

## Debug 도구

### Navigation Debug Window

**메뉴**: `SC Tools/Debug/Navigation Debug Window`

**역할**: Play 모드에서 UI Navigation 상태(Screen/Popup 스택)를 실시간으로 시각화.

**기능**:
- Screen Stack 표시 (역순, 최상위가 위)
- Popup Stack 표시
- 실시간 Auto Refresh
- Select 버튼으로 Hierarchy 선택
- Push/Pop 버튼으로 스택 직접 조작

**표시 정보**:

```
Screen Stack:
[1] TestScreen_2 (Current) [Visible]
[0] TestScreen_1           [Hidden]

Popup Stack:
[1] TestPopup_2 (Top)      [Visible]
[0] TestPopup_1            [Hidden]

Summary:
Screens: 2, Popups: 2
```

**파일**: `Editor/AI/NavigationDebugWindow.cs`

---

## Data 도구

### Data Flow Test Window

**메뉴**: `SC > Test > Data Flow Test Window`

**역할**: 데이터 아키텍처 v2.0의 핵심 흐름(Login/Gacha)을 통합 테스트.

**기능**:
- API Client 초기화 (LocalApiClient)
- DataManager 초기화 (마스터 데이터 검증)
- 로그인 테스트 (Login → SetUserData)
- 가챠 테스트 (Gacha → ApplyDelta)
- 현재 유저 데이터 실시간 표시
- 저장 데이터 삭제

**파일**: `Editor/AI/DataFlowTestWindow.cs`

---

### Master Data Generator

**메뉴**: `SC/Data/Master Data Generator`

**역할**: JSON 파일을 ScriptableObject로 수동 변환.

**기능**:
- JSON 파일 목록 표시
- 개별/전체 변환
- 생성된 Database 에셋 확인

**파일**: `Editor/Data/MasterDataGeneratorWindow.cs`

---

## Settings 도구

### Project Settings

**메뉴**: `SC Tools/Settings/`

| 메뉴 | 기능 |
|------|------|
| Project Editor Settings | 프로젝트 에디터 설정 창 |
| Reset to Defaults | 설정 초기화 |

**파일**: `Editor/AI/ProjectEditorSettings.cs`

---

## 설계 원칙

| 원칙 | 설명 |
|------|------|
| **메뉴 기반 접근** | 코드 수정 없이 메뉴 클릭으로 실행 |
| **기능별 그룹화** | Setup/Debug/Data/Settings로 분류 |
| **자동화 우선** | 수동 Inspector 설정 최소화 |
| **비파괴적** | 기존 씬 오브젝트 보존 (명시적 Clear 제외) |
| **프리팹 재사용** | 생성된 프리팹은 이후 수동 사용 가능 |
| **공용 헬퍼 사용** | EditorUIHelpers로 코드 중복 제거 |
| **Bootstrap 레벨 명시** | 각 도구의 씬 구성 범위 명확화 |

---

## 도구 추가 가이드

### 새 도구 추가 시

1. **Editor 스크립트 생성**: `Assets/Scripts/Editor/AI/{ToolName}.cs`
2. **EditorUIHelpers 활용**: 공용 UI 생성 메서드 사용
3. **메뉴 등록**: `[MenuItem("SC Tools/{Category}/{Action}")]`
4. **Bootstrap 레벨 명시**: 클래스 docstring에 Bootstrap 레벨 기재
5. **이 문서에 섹션 추가**: 도구 목록 테이블 + 상세 섹션

### 메뉴 경로 규칙

```csharp
// 프리팹 생성
[MenuItem("SC Tools/Setup/Prefabs/{Action}", priority = 100-199)]

// 씬 셋업
[MenuItem("SC Tools/Setup/Scenes/{Action}", priority = 200-299)]

// 데이터
[MenuItem("SC Tools/Setup/Master Data/{Action}", priority = 300-399)]

// 디버그
[MenuItem("SC Tools/Debug/{ToolName}", priority = 100)]

// 설정
[MenuItem("SC Tools/Settings/{Action}", priority = 1000)]
```

**Priority 규칙**:
- 100-119: 테스트 프리팹
- 120-129: 로딩 프리팹
- 130-139: MVP 프리팹
- 140-149: 다이얼로그 프리팹
- 150-189: 검증/기타
- 190-199: 삭제
- 200-299: 씬 셋업
- 300-399: 마스터 데이터

---

## 관련 문서

- [TestArchitecture.md](../Testing/TestArchitecture.md) - 테스트 아키텍처
- [EDITOR_REFACTORING.md](EDITOR_REFACTORING.md) - 리팩토링 계획
