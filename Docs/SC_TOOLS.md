# SC Tools 에디터 도구 가이드

Unity Editor의 `SC Tools` 메뉴와 `Project Setup Wizard`를 통해 프로젝트 설정을 관리합니다.

---

## Project Setup Wizard (권장)

**접근**: `SC Tools` → `Project Setup Wizard` (또는 단축키 설정)

모든 설정 도구를 하나의 창에서 관리하는 통합 Wizard입니다.

### 탭 구성

| 탭 | 용도 |
|----|------|
| **Setup** | 씬 설정, 프리팹 생성, Addressables |
| **Debug** | Navigation Stack, Data Flow (PlayMode) |
| **Data** | JSON 파일, 생성된 에셋 관리 |
| **Settings** | 프로젝트/Wizard 설정 |

---

## Setup 탭 (핵심)

### 1. Main Scene (Production)
프로덕션용 Main 씬을 자동 구성합니다.

| 버튼 | 기능 |
|------|------|
| Setup Main Scene | Managers, UIRoot, Canvas 계층 생성 |
| Clear | Main 씬 오브젝트 삭제 |

### 2. UI Prefabs (Production) ⭐
**모든 Screen/Popup 프리팹을 자동 생성합니다.**

| 버튼 | 기능 |
|------|------|
| **Generate All UI Prefabs** | Screen + Popup 전체 생성 |
| Screens Only | ScreenWidget 상속 클래스만 |
| Popups Only | PopupWidget 상속 클래스만 |
| Open Screens/Popups Folder | 생성된 프리팹 폴더 열기 |

**생성 경로**:
- `Assets/Prefabs/UI/Screens/`
- `Assets/Prefabs/UI/Popups/`

**Addressables 자동 등록**:
- 주소: `UI/Screens/{ClassName}`, `UI/Popups/{ClassName}`
- 그룹: `UI`

### 3. Lobby Setup
LobbyScreen의 탭 시스템을 설정합니다.

| 버튼 | 기능 |
|------|------|
| Setup Tab System | TabGroup + TabContents 자동 구성 |
| Create TabButton Prefab | TabButton 프리팹 생성 |

### 4. Addressables
UI 프리팹의 Addressables 그룹을 관리합니다.

| 버튼 | 기능 |
|------|------|
| Setup UI Groups | UI_Screens, UI_Popups, UI_Widgets 그룹 생성 |
| Clear UI Groups | 모든 UI 그룹 제거 |

### 5. MVP Scene Setup (Legacy)
초기 개발용 MVP 씬 설정. 프로덕션에서는 Main Scene 사용 권장.

### 6. Test Scenes
UI Navigation 테스트, Loading 테스트 등 개발용 씬 설정.

### 7. Dialog Prefabs
ConfirmPopup, CostConfirmPopup 등 시스템 팝업 프리팹 생성.

---

## Debug 탭

**PlayMode에서만 사용 가능합니다.**

### Navigation Stack
현재 화면 스택을 실시간으로 모니터링합니다.

- 스택 시각화 (Screen/Popup 구분)
- Back, Close All Popups 버튼
- 현재 화면 Summary

### Data Flow Test
별도 창에서 Login → Gacha 전체 흐름 테스트.

---

## Data 탭

### JSON Files
`Assets/Data/MasterData/` 내 JSON 파일 목록.

| 버튼 | 기능 |
|------|------|
| Open | 파일 선택 |
| Import | 재임포트 (ScriptableObject 재생성) |

### Generated Assets
`Assets/Data/Generated/` 내 Database ScriptableObject 상태.

| 버튼 | 기능 |
|------|------|
| Regenerate All | 모든 JSON 재임포트 |
| Delete All | 생성된 에셋 전체 삭제 |

---

## Settings 탭

### Project Settings
기본 폰트, 색상 등 프로젝트 설정.

### Wizard Preferences
- Auto-refresh in Play Mode
- Refresh Interval

---

## 독립 메뉴 항목

Project Setup Wizard 외에도 개별 메뉴로 접근 가능합니다:

```
SC Tools/
├── Project Setup Wizard          ← 통합 창 (권장)
├── Prefabs/
│   ├── Generate All Screen Prefabs
│   ├── Generate All Popup Prefabs
│   └── Generate All UI Prefabs
├── Lobby/
│   ├── Setup Tab System
│   └── Create TabButton Prefab Only
├── Addressables/
│   ├── Setup UI Groups
│   ├── Clear UI Groups
│   └── Validate UI Groups
├── Debug/
│   └── Navigation Debug Window
└── Settings/
    ├── Project Editor Settings
    └── Reset to Defaults
```

---

## 일반적인 워크플로우

### 새 프로젝트 설정
1. `Project Setup Wizard` 열기
2. Setup 탭 → **Setup Main Scene**
3. Setup 탭 → **Generate All UI Prefabs**
4. Data 탭 → **Regenerate All** (마스터 데이터)
5. Play Mode 테스트

### 새 Screen/Popup 추가 후
1. 코드 작성 (ScreenWidget<T,S> 상속)
2. Project Setup Wizard → Setup 탭
3. **Generate All UI Prefabs** 클릭
4. 새 프리팹이 `Assets/Prefabs/UI/` 에 생성됨
5. Addressables에 자동 등록됨

### 마스터 데이터 수정 후
1. JSON 파일 수정 (`Assets/Data/MasterData/*.json`)
2. Project Setup Wizard → Data 탭
3. 해당 파일 **Import** 또는 **Regenerate All**
4. ScriptableObject 자동 업데이트

---

## 트러블슈팅

### 프리팹 생성이 안 될 때
- 클래스가 `ScreenWidget<T,S>` 또는 `PopupWidget<T,S>`를 상속하는지 확인
- `abstract` 클래스는 제외됨
- 컴파일 에러가 없는지 확인

### Addressables 로드 실패
- `SC Tools → Addressables → Validate UI Groups` 실행
- 주소가 `UI/Screens/{Name}` 형식인지 확인
- Build → Build Player Content 실행 (빌드 시)

### Navigation이 동작하지 않을 때
- NavigationManager가 Main Scene에 있는지 확인
- Setup Main Scene 다시 실행
- Debug 탭에서 Stack 상태 확인
