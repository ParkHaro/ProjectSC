# Main 씬 및 에디터 도구 통합 작업 계획

## 개요

에디터 도구를 통합 Wizard로 재구성하고, Addressables 기반 프리팹 시스템으로 전체 플레이 가능한 Main 씬을 구축하는 대규모 작업.

## 확정 사항

| 항목 | 결정 |
|------|------|
| **Main 씬 범위** | 전체 플레이 가능 (모든 Screen/Popup 동작) |
| **에디터 도구** | 통합 Setup Wizard (EditorWindow) |
| **프리팹 관리** | 코드 생성 + Addressables 런타임 로딩 |
| **세션 분리** | 기능 단위, 병렬 에이전트 2트랙 |
| **레거시 처리** | 완전 리팩토링 (기존 코드 삭제 후 재작성) |

---

## 세션 구조 (총 5세션)

### 세션 1: 기반 인프라

| 트랙 A: 에디터 도구 리팩토링 | 트랙 B: Addressables 인프라 |
|------------------------------|----------------------------|
| 기존 에디터 도구 분석 및 제거 | Addressables Group 설계 |
| ProjectSetupWizard 기본 구조 | 프리팹 Address 명명 규칙 |
| EditorUIHelpers 정리 | AssetManager 확장 API |
| 설정/유틸리티 통합 | 빌드 스크립트 작성 |

**통합 지점**: Wizard에서 Addressables 설정을 제어할 수 있도록 연결

**산출물**:
- `Assets/Scripts/Editor/Wizard/ProjectSetupWizard.cs`
- `Assets/Scripts/Editor/Wizard/Tabs/*.cs`
- Addressables Groups 설정
- `AssetManager` 확장 API

---

### 세션 2: Main 씬 구조

| 트랙 A: 씬 계층 구조 | 트랙 B: 초기화 시스템 |
|---------------------|---------------------|
| Main.unity 씬 생성 | GameBootstrap 리팩토링 |
| Canvas 계층 (Screen/Popup/Header) | 초기화 시퀀스 설계 |
| Manager 배치 구조 | 의존성 주입 패턴 |
| Camera 설정 | 에러 핸들링 체계 |

**통합 지점**: Bootstrap이 씬 구조를 인식하고 초기화

**산출물**:
- `Assets/Scenes/Main.unity`
- `GameBootstrap.cs` 리팩토링
- `InitializationSequence.cs`

---

### 세션 3: 프리팹 자동화

| 트랙 A: 프리팹 생성 시스템 | 트랙 B: UI 런타임 로딩 |
|--------------------------|---------------------|
| PrefabGenerator 구현 | NavigationManager Addressables 연동 |
| Screen 프리팹 템플릿 | Screen 동적 로딩 |
| Popup 프리팹 템플릿 | Popup 동적 로딩 |
| Widget 프리팹 템플릿 | 프리로드/언로드 정책 |

**통합 지점**: 생성된 프리팹이 런타임에 정상 로딩되는지 검증

**산출물**:
- `Assets/Scripts/Editor/Wizard/Generators/PrefabGenerator.cs`
- Screen/Popup 프리팹들 (Addressables 등록)
- `NavigationManager` Addressables 통합

---

### 세션 4: 기능 통합

| 트랙 A: 핵심 흐름 | 트랙 B: 부가 흐름 |
|-----------------|-----------------|
| Title → Login → Lobby | LiveEvent 흐름 |
| Gacha 전체 흐름 | CharacterList/Detail 흐름 |
| 로딩 표시 연동 | Popup 시스템 연동 |
| 저장/로드 연동 | ScreenHeader 연동 |

**통합 지점**: 모든 화면 전환이 정상 동작

**산출물**:
- 전체 화면 흐름 동작
- 저장/로드 연동 완료

---

### 세션 5: 최종 통합 및 검증

| 트랙 A: Wizard 완성 | 트랙 B: 테스트/문서화 |
|-------------------|---------------------|
| Setup Wizard 전체 기능 통합 | 통합 테스트 시나리오 |
| One-Click 씬 생성 | PlayMode 테스트 추가 |
| 프리팹 재생성 기능 | 사용자 가이드 작성 |
| 설정 저장/로드 | PROGRESS.md 업데이트 |

**통합 지점**: Wizard로 Main 씬을 처음부터 생성하고 전체 플레이 검증

**산출물**:
- 완성된 ProjectSetupWizard
- 통합 테스트
- 사용자 가이드

---

## 파일 변경 계획

### 삭제 대상

```
Assets/Scripts/Editor/AI/
├── MVPSceneSetup.cs         → 삭제
├── PlayModeTestSetup.cs     → 삭제
├── LoadingSetup.cs          → 삭제
├── SystemPopupSetup.cs      → 삭제
├── UITestSceneSetup.cs      → 삭제
├── DataFlowTestWindow.cs    → Wizard로 통합
├── NavigationDebugWindow.cs → Wizard로 통합
└── ProjectEditorSettings.cs → Wizard로 통합
```

### 유지/리팩토링

```
Assets/Scripts/Editor/AI/
└── EditorUIHelpers.cs       → 리팩토링 후 유지
```

### 신규 생성

```
Assets/Scripts/Editor/Wizard/
├── ProjectSetupWizard.cs
├── Tabs/
│   ├── SetupTab.cs
│   ├── DebugTab.cs
│   ├── DataTab.cs
│   └── SettingsTab.cs
└── Generators/
    ├── SceneGenerator.cs
    └── PrefabGenerator.cs

Assets/Scenes/
└── Main.unity
```

---

## 세션별 작업 ID

| 세션 | 작업 ID | 상태 |
|------|---------|------|
| 1 | `main-scene-s1-infra` | 대기 |
| 2 | `main-scene-s2-structure` | 대기 |
| 3 | `main-scene-s3-prefab` | 대기 |
| 4 | `main-scene-s4-integration` | 대기 |
| 5 | `main-scene-s5-final` | 대기 |

---

## 참조

- [PROGRESS.md](../../PROGRESS.md) - 전체 진행 상황
- [ARCHITECTURE.md](../../ARCHITECTURE.md) - 프로젝트 구조
- [EditorTools.md](EditorTools.md) - 기존 에디터 도구 문서
