---
type: guide
assembly: Sc.Editor.AI
category: Editor
status: approved
version: "1.0"
created: 2025-01-15
updated: 2025-01-15
---

# AI 개발 도구

## 목적

Unity Editor 메뉴를 통해 씬/프리팹 셋업을 자동화하는 도구 모음.

## 의존성

- 참조: Sc.Data, Sc.Core, Sc.Common
- 참조됨: (없음 - Editor 전용)

---

## 도구 목록

| 도구 | 메뉴 경로 | 용도 | 상태 |
|------|-----------|------|------|
| [UITestSceneSetup](#uitestscenesetup) | SC Tools > UI Test | UI 시스템 테스트 씬/프리팹 자동 생성 | ✅ |
| [NavigationDebugWindow](#navigationdebugwindow) | SC Tools > UI Test | Navigation 상태 시각화 윈도우 | ✅ |
| [DataFlowTestWindow](#dataflowtestwindow) | SC > Test | 데이터 흐름 통합 테스트 | ✅ |

---

## UITestSceneSetup

### 역할

UI 시스템(Widget, Screen, Popup, NavigationManager) 테스트를 위한 씬과 프리팹을 자동 생성.

### 메뉴

| 메뉴 | 단축키 | 기능 |
|------|--------|------|
| `SC Tools > UI Test > Setup Test Scene` | - | 테스트 씬 전체 구성 |
| `SC Tools > UI Test > Create Test Prefabs Only` | - | 프리팹만 생성 |
| `SC Tools > UI Test > Clear Test Objects` | - | 테스트 오브젝트 정리 |

### 생성 항목

**Setup Test Scene 실행 시:**

```
Hierarchy:
├── NavigationManager          # DontDestroyOnLoad
└── TestCanvas
    ├── ScreenContainer        # Sorting Order: 0
    ├── PopupContainer         # Sorting Order: 100
    └── TestControlPanel       # UITestSetup 컴포넌트 포함
        └── 테스트 버튼 7개

Assets/Prefabs/UI/Tests/
├── TestScreen.prefab          # TestScreen + UI 요소
└── TestPopup.prefab           # TestPopup + UI 요소
```

### 사용 패턴

```
1. 아무 씬 열기
2. SC Tools > UI Test > Setup Test Scene
3. Play 모드 진입
4. 화면 왼쪽 하단 버튼으로 테스트
5. Console에서 로그 확인
```

### 테스트 가능 항목

| 테스트 가능 | Provider 필요 (추후) |
|-------------|----------------------|
| Widget 라이프사이클 | NavigationManager.Push/Pop |
| State 바인딩 | Screen 스택 관리 |
| ESC 키 처리 | 중복 Screen 제거 |

### 관련 클래스

- `Sc.Common.UI.Widget` - 기본 위젯
- `Sc.Common.UI.ScreenWidget` - Screen 베이스
- `Sc.Common.UI.PopupWidget` - Popup 베이스
- `Sc.Common.UI.NavigationManager` - 화면 전환 관리
- `Sc.Common.UI.Tests.UITestSetup` - 테스트 러너

---

## NavigationDebugWindow

### 역할

Play 모드에서 UI Navigation 상태(Screen/Popup 스택)를 실시간으로 시각화.

### 메뉴

| 메뉴 | 단축키 | 기능 |
|------|--------|------|
| `SC Tools > UI Test > Navigation Debug Window` | - | 디버그 윈도우 열기 |

### 기능

- **Screen Stack 표시**: 현재 Screen 스택을 역순으로 표시 (최상위가 위)
- **Popup Stack 표시**: 현재 Popup 스택을 역순으로 표시
- **실시간 업데이트**: Auto Refresh로 자동 갱신
- **오브젝트 선택**: Select 버튼으로 Hierarchy에서 선택
- **직접 제어**: Push/Pop 버튼으로 스택 조작

### 표시 정보

```
Screen Stack:
[1] TestScreen_2 (Current) [Visible]
[0] TestScreen_1           [Hidden]

Popup Stack:
[1] TestPopup_2 (Top)      [Visible]
[0] TestPopup_1            [Hidden]

Summary:
Screens: 2
Popups: 2
Current Screen: TestScreen_2
Top Popup: TestPopup_2
```

### 사용 패턴

```
1. SC Tools > UI Test > Navigation Debug Window
2. Play 모드 진입
3. 테스트 버튼 또는 윈도우 버튼으로 Screen/Popup 조작
4. 스택 상태 실시간 확인
```

### 관련 클래스

- `Sc.Common.UI.Tests.UITestSetup` - 스택 데이터 소스

---

## DataFlowTestWindow

### 역할

데이터 아키텍처 v2.0의 핵심 흐름(Login/Gacha)을 통합 테스트.

### 메뉴

| 메뉴 | 단축키 | 기능 |
|------|--------|------|
| `SC > Test > Data Flow Test Window` | - | 테스트 윈도우 열기 |

### 기능

- **API Client 초기화**: LocalApiClient 연결
- **DataManager 초기화**: 마스터 데이터 검증
- **로그인 테스트**: Login → SetUserData 흐름
- **가챠 테스트**: Gacha → ApplyDelta 흐름
- **데이터 뷰**: 현재 유저 데이터 실시간 표시
- **저장 데이터 삭제**: 테스트 초기화

### 테스트 흐름

```
Step 1: 초기화
  1-1. API Client 초기화 (LocalApiClient)
  1-2. DataManager 초기화 (마스터 데이터 검증)

Step 2: 로그인
  2-1. LoginRequest → LoginResponse → SetUserData()

Step 3: 가챠
  3-1. GachaRequest → GachaResponse → ApplyDelta()
```

### 사용 패턴

```
1. SC > Test > Data Flow Test Window
2. Play 모드 진입 (DataManager 프리팹 필요)
3. 1-1 → 1-2 → 2-1 순서로 초기화
4. 3-1로 가챠 테스트 (Single/Multi)
5. 테스트 로그 및 데이터 상태 확인
```

### 관련 클래스

- `Sc.Packet.LocalApiClient` - 로컬 API 클라이언트
- `Sc.Core.DataManager` - 데이터 매니저
- `Sc.Packet.LoginRequest/Response` - 로그인 패킷
- `Sc.Packet.GachaRequest/Response` - 가챠 패킷
- `Sc.Packet.UserDataDelta` - 데이터 변경분

---

## 도구 추가 가이드

### 새 도구 추가 시

1. **Editor 스크립트 생성**: `Assets/Scripts/Editor/AI/{ToolName}.cs`
2. **메뉴 등록**: `[MenuItem("SC Tools/{Category}/{Action}")]`
3. **이 문서에 섹션 추가**: 도구 목록 테이블 + 상세 섹션
4. **SPEC_INDEX.md 확인**: Editor 섹션에 반영 여부

### 섹션 템플릿

```markdown
## {ToolName}

### 역할
(한 문장)

### 메뉴
| 메뉴 | 단축키 | 기능 |
|------|--------|------|

### 생성 항목
(생성되는 오브젝트/파일 목록)

### 사용 패턴
(1-5줄)

### 관련 클래스
- ...
```

### 도구 삭제 시

1. Editor 스크립트 삭제
2. 이 문서에서 해당 섹션 및 도구 목록 행 제거

---

## 설계 원칙

1. **메뉴 기반 접근**: 코드 수정 없이 메뉴 클릭으로 실행
2. **자동화 우선**: 수동 Inspector 설정 최소화
3. **비파괴적**: 기존 씬 오브젝트 보존 (명시적 Clear 제외)
4. **프리팹 재사용**: 생성된 프리팹은 이후 수동 사용 가능
