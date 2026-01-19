---
type: guide
assembly: Sc.Editor
category: Editor
status: draft
version: "1.0"
created: 2026-01-19
updated: 2026-01-19
---

# 에디터 도구 리팩토링 계획

## 개요

SC Tools 메뉴 구조 재구성 및 테스트 인프라 정리.

**목표**:
- 기능별 메뉴 통합
- 중복 기능 제거
- 미완성 코드 정리
- 문서 현행화

---

## 현재 구조 (AS-IS)

```
SC Tools/
├── Debug/
│   └── Navigation Debug Window
│
├── Loading/
│   ├── Create LoadingWidget Prefab
│   ├── Setup Loading Test Scene
│   └── Clear Loading Test Objects
│
├── MVP/
│   ├── Rebuild All (Full Reset)
│   ├── Setup MVP Scene
│   ├── Create All Prefabs
│   ├── Recreate All Prefabs (Force)
│   ├── Delete All Prefabs
│   ├── Generate Master Data
│   └── Clear MVP Objects
│
├── PlayMode Tests/
│   ├── Create All Test Prefabs
│   ├── Create Simple Screen/Popup Prefab
│   ├── Create System Popup Test Prefab
│   ├── Verify Test Scene
│   └── Delete All Test Prefabs
│
├── Settings/
│   ├── Project Editor Settings
│   └── Reset to Defaults
│
├── System Tests/
│   ├── UI/Test Navigation
│   └── Stop All Tests
│
└── UI Test/
    ├── Setup Test Scene
    ├── Create Test Prefabs Only
    └── Clear Test Objects
```

### 문제점

| 문제 | 설명 |
|------|------|
| **중복 기능** | UI Test, PlayMode Tests, Loading 모두 프리팹/씬 생성 |
| **명명 불일치** | 유사 기능이 다른 이름으로 분산 |
| **미완성 코드** | SaveManagerTestRunner, AssetManagerTestRunner 미구현 |
| **런타임 테스트 불필요** | System Tests → PlayMode 테스트로 대체 가능 |

---

## 목표 구조 (TO-BE)

```
SC Tools/
├── Setup/                          # 씬/프리팹 생성 도구
│   ├── Prefabs/
│   │   ├── Create All Test Prefabs
│   │   ├── Create MVP Prefabs
│   │   └── Delete All Test Prefabs
│   │
│   ├── Scenes/
│   │   ├── Setup Test Scene
│   │   ├── Setup MVP Scene
│   │   └── Clear Scene Objects
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

### 변경 원칙

| 원칙 | 설명 |
|------|------|
| **기능별 그룹화** | Setup/Debug/Data/Settings로 분류 |
| **프리팹 통합** | 모든 프리팹 생성을 Setup/Prefabs로 |
| **씬 통합** | 모든 씬 셋업을 Setup/Scenes로 |
| **런타임 테스트 제거** | PlayMode 테스트로 대체 |

---

## 파일 변경 목록

### 삭제 대상

| 파일 | 사유 |
|------|------|
| `Editor/Tests/SystemTestMenu.cs` | 런타임 테스트 메뉴 제거 |
| `Tests/Runners/SaveManagerTestRunner.cs` | 미완성 러너 |
| `Tests/Runners/AssetManagerTestRunner.cs` | 미완성 러너 |
| `Tests/Scenarios/SaveManagerTestScenarios.cs` | 미완성 시나리오 |
| `Tests/Scenarios/AssetManagerTestScenarios.cs` | 미완성 시나리오 |

### 수정 대상

| 파일 | 변경 내용 |
|------|----------|
| `Editor/AI/PlayModeTestSetup.cs` | 메뉴 경로: `SC Tools/Setup/Prefabs/` |
| `Editor/AI/UITestSceneSetup.cs` | 메뉴 경로: `SC Tools/Setup/Scenes/` |
| `Editor/AI/LoadingSetup.cs` | 메뉴 경로: `SC Tools/Setup/` 또는 통합 |
| `Editor/AI/MVPSceneSetup.cs` | 메뉴 경로: `SC Tools/Setup/` |

### 유지 대상

| 파일 | 비고 |
|------|------|
| `Editor/AI/NavigationDebugWindow.cs` | 경로 유지 (`SC Tools/Debug/`) |
| `Editor/AI/ProjectEditorSettings.cs` | 경로 유지 (`SC Tools/Settings/`) |
| `Editor/Data/MasterDataGeneratorWindow.cs` | 경로 변경 고려 |
| `Editor/Data/MasterDataImporter.cs` | 변경 없음 (자동 처리) |
| `Tests/Runners/SystemTestRunner.cs` | 베이스 클래스 유지 |
| `Tests/Runners/NavigationTestRunner.cs` | PlayMode와 시나리오 공유용 유지 |
| `Tests/Scenarios/NavigationTestScenarios.cs` | PlayMode 테스트에서 재사용 |

---

## 메뉴 경로 매핑

### 프리팹 생성 (Setup/Prefabs)

| 기존 경로 | 새 경로 | 파일 |
|----------|---------|------|
| `PlayMode Tests/Create All Test Prefabs` | `Setup/Prefabs/Create All Test Prefabs` | PlayModeTestSetup.cs |
| `PlayMode Tests/Create Simple Screen Prefab` | `Setup/Prefabs/Test/Create Screen` | PlayModeTestSetup.cs |
| `PlayMode Tests/Create Simple Popup Prefab` | `Setup/Prefabs/Test/Create Popup` | PlayModeTestSetup.cs |
| `PlayMode Tests/Create System Popup Test Prefab` | `Setup/Prefabs/Test/Create SystemPopup` | PlayModeTestSetup.cs |
| `PlayMode Tests/Delete All Test Prefabs` | `Setup/Prefabs/Delete All Test Prefabs` | PlayModeTestSetup.cs |
| `MVP/Create All Prefabs` | `Setup/Prefabs/Create MVP Prefabs` | MVPSceneSetup.cs |
| `MVP/Recreate All Prefabs (Force)` | `Setup/Prefabs/MVP/Recreate (Force)` | MVPSceneSetup.cs |
| `MVP/Delete All Prefabs` | `Setup/Prefabs/Delete MVP Prefabs` | MVPSceneSetup.cs |
| `Loading/Create LoadingWidget Prefab` | `Setup/Prefabs/Create Loading Prefab` | LoadingSetup.cs |

### 씬 생성 (Setup/Scenes)

| 기존 경로 | 새 경로 | 파일 |
|----------|---------|------|
| `UI Test/Setup Test Scene` | `Setup/Scenes/Setup Test Scene` | UITestSceneSetup.cs |
| `UI Test/Clear Test Objects` | `Setup/Scenes/Clear Test Objects` | UITestSceneSetup.cs |
| `MVP/Setup MVP Scene` | `Setup/Scenes/Setup MVP Scene` | MVPSceneSetup.cs |
| `MVP/Rebuild All (Full Reset)` | `Setup/Scenes/MVP/Rebuild All` | MVPSceneSetup.cs |
| `MVP/Clear MVP Objects` | `Setup/Scenes/Clear MVP Objects` | MVPSceneSetup.cs |
| `Loading/Setup Loading Test Scene` | 제거 (UI Test와 중복) | LoadingSetup.cs |
| `Loading/Clear Loading Test Objects` | 제거 (UI Test와 중복) | LoadingSetup.cs |

### 데이터 (Setup/Master Data 또는 Data)

| 기존 경로 | 새 경로 | 파일 |
|----------|---------|------|
| `MVP/Generate Master Data` | `Setup/Master Data/Generate All` | MVPSceneSetup.cs |

### 제거

| 기존 경로 | 사유 |
|----------|------|
| `System Tests/UI/Test Navigation` | PlayMode 테스트로 대체 |
| `System Tests/Stop All Tests` | 불필요 |
| `PlayMode Tests/Verify Test Scene` | 제거 또는 Setup으로 이동 |
| `UI Test/Create Test Prefabs Only` | Setup/Prefabs로 통합 |

---

## 테스트 인프라 정리

### 유지할 구조

```
Tests/
├── Helpers/                    # 테스트 유틸리티 (유지)
├── Mocks/                      # Mock 구현체 (유지)
├── Scenarios/
│   └── NavigationTestScenarios.cs  # PlayMode와 공유 (유지)
├── Runners/
│   ├── SystemTestRunner.cs         # 베이스 클래스 (유지)
│   └── NavigationTestRunner.cs     # 시나리오 테스트용 (유지)
├── TestWidgets/                # 테스트 위젯 (유지)
└── PlayMode/                   # PlayMode 테스트 (유지)
```

### 삭제할 구조

```
Tests/
├── Runners/
│   ├── SaveManagerTestRunner.cs    # 삭제
│   └── AssetManagerTestRunner.cs   # 삭제
└── Scenarios/
    ├── SaveManagerTestScenarios.cs # 삭제
    └── AssetManagerTestScenarios.cs # 삭제
```

---

## 문서 변경

| 문서 | 변경 내용 |
|------|----------|
| `Specs/Testing/TestArchitecture.md` | 새 구조 반영, 에디터 메뉴 섹션 업데이트 |
| `Specs/Editor/AITools.md` | EditorTools.md로 이름 변경, 새 메뉴 구조 반영 |
| `PROGRESS.md` | 작업 로그 추가 |

---

## 구현 순서

### Phase 1: 파일 삭제
1. 미완성 러너/시나리오 삭제 (5개 파일)
2. SystemTestMenu.cs 삭제

### Phase 2: 메뉴 경로 변경
1. PlayModeTestSetup.cs 수정
2. UITestSceneSetup.cs 수정
3. LoadingSetup.cs 수정 (또는 통합)
4. MVPSceneSetup.cs 수정

### Phase 3: 통합 정리
1. 중복 기능 제거/통합
2. 코드 정리

### Phase 4: 문서 업데이트
1. TestArchitecture.md 재작성
2. EditorTools.md 작성 (AITools.md 대체)
3. PROGRESS.md 업데이트

---

## 검증 체크리스트

- [ ] 모든 SC Tools 메뉴가 새 구조로 이동됨
- [ ] 삭제 대상 파일이 모두 제거됨
- [ ] PlayMode 테스트가 정상 동작
- [ ] NUnit 테스트가 정상 동작
- [ ] 문서가 현행화됨
- [ ] 컴파일 에러 없음

---

## 관련 문서

- [TestArchitecture.md](../Testing/TestArchitecture.md)
- [EditorTools.md](EditorTools.md) (신규)
