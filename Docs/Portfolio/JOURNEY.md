# 프로젝트 여정

프로젝트의 진행 과정을 커밋 기준으로 기록합니다.

---

## Phase 1: 프로젝트 초기화

### 2024-XX - Initial Setup

**커밋**: `b9266e2` Initial commit
- Unity 프로젝트 생성
- 기본 디렉토리 구조 설정

**커밋**: `92c3520` Add initial project configuration files and settings
- Unity 프로젝트 설정 파일 추가
- .gitignore, EditorConfig 등 설정

**커밋**: `968b75d` Add initial meta files for various game components and systems
- 게임 컴포넌트 기본 구조 설계
- Assembly Definition 기반 모듈화 시작

---

## Phase 2: 아키텍처 설계

### 이벤트/패킷 시스템 분리

**커밋**: `f5a5f11` Separate Event and Packet layers with interface abstraction
- **문제**: 이벤트와 네트워크 패킷의 책임 혼재
- **해결**: 인터페이스 추상화를 통한 계층 분리
- **결과**: 각 레이어가 독립적으로 변경 가능

### Editor AI 도구 도입

**커밋**: `d688bfd` Add Editor-only AI development tools assembly
- **목적**: AI와 협업 시 Unity Editor 작업 자동화
- **결정**: Editor 전용 Assembly로 분리하여 빌드 영향 제거

---

## Phase 3: UI 시스템 구축

### UI 아키텍처 진화

**커밋**: `0d89977` Update UIComponents: Unity 기본 컴포넌트 우선 사용 원칙 추가
- Unity 기본 컴포넌트 우선 사용 원칙 확립
- 커스텀 컴포넌트는 필요시에만 추가

**커밋**: `8160eef` Add UISystem, LiveEvent, Navigation spec documents
- UI 시스템 스펙 문서화
- Navigation 시스템 설계

**커밋**: `6792d60` Refactor UI architecture: MVP → Widget System
- **중요 결정**: MVP 패턴에서 Widget 기반 시스템으로 전환
- **이유**: Unity UI의 특성에 맞는 더 유연한 구조 필요
- **상세**: [DECISIONS.md](DECISIONS.md#ui-아키텍처-mvp--widget) 참조

### UI 구현

**커밋**: `5f13c68` Update Unity settings and add script meta files
- Unity 설정 최적화
- 스크립트 메타 파일 정리

**커밋**: `fcd51db` Add UniTask package dependency
- UniTask 패키지 추가
- 비동기 처리 기반 마련

**커밋**: `73ac0c7` Implement UI system core: Widget, Screen, Popup, Navigation
- Widget 시스템 핵심 구현
- Screen/Popup/Navigation 기본 동작

**커밋**: `28160c2` Add UI test environment: TestScreen, TestPopup, Prefabs
- 테스트 환경 구축
- 검증용 Screen/Popup 프리팹

---

## Phase 4: 개발 도구 강화

**커밋**: `c422391` Add Editor AI tools: NavigationDebugWindow, UITestSceneSetup
- Navigation 디버그 윈도우
- UI 테스트 씬 자동 셋업 도구

**커밋**: `3c19b09` Update specs: Navigation v2.0, UISystem v3.1
- 스펙 문서 버전 업데이트
- 구현 결과 반영

---

## Phase 5: 프로세스 개선

**커밋**: `30359c4` Update CLAUDE.md: Progress 추적 필수 지침 추가
- Progress 추적 필수화
- AI 협업 프로세스 정립

**커밋**: `a861c85` Add tmpclaude-* to .gitignore
- 임시 파일 관리 개선

---

## Phase 6: 데이터 아키텍처 v2.0

### 아키텍처 재설계

**커밋**: `8b6aae0` (리셋 기준점)
- **문제**: v1.0 로컬 중심 설계로는 라이브 서비스 대응 불가
- **결정**: 서버 중심(Server Authority) 아키텍처로 재설계
- **상세**: [DECISIONS.md](DECISIONS.md#데이터-아키텍처-로컬-중심--서버-중심) 참조

### 마스터 데이터 파이프라인

**커밋**: 여러 커밋 통합
- Excel/JSON → Python Export → JSON → AssetPostprocessor → ScriptableObject
- MasterDataImporter: JSON 변경 감지 시 자동 SO 생성
- MasterDataGeneratorWindow: 수동 생성 UI

### 서버 중심 데이터 흐름

**커밋**: 여러 커밋 통합
- IApiClient 인터페이스 정의
- LocalApiClient: 서버 응답 시뮬레이션 (로컬 JSON 저장)
- UserDataDelta: 부분 갱신 패턴
- DataManager: 읽기 전용 뷰, SetUserData/ApplyDelta

### 네트워크 아키텍처 개선

**커밋**: `0750591` Refactor PacketDispatcher to callback pattern
- PacketDispatcher 콜백 패턴으로 변경
- 순환 참조 해결을 위한 Sc.Foundation Assembly 분리

**커밋**: `8f7d28e` Optimize RequestQueue by removing reflection
- RequestQueue 리플렉션 제거 (성능 최적화)

### 통합 테스트 도구

**커밋**: `0da3d36` Add DataFlowTestWindow and update docs to v2.0
- DataFlowTestWindow 에디터 도구
  - Login/Gacha 흐름 통합 테스트
  - DataManager 자동 생성 및 Database 할당
- 문서 v2.0 업데이트 (Data.md, Packet.md, Core.md)

---

## Phase 7: MVP 기반 컴포넌트

### 기본 Widget 컴포넌트

**커밋**: `548caf3` Add base UI Widget components (8 types)
- Unity 기본 UI 컴포넌트를 Widget 시스템에 통합
- **목적**: MVP 화면 구현 전 일관된 UI 패턴 확립
- **결정**: Widget 래퍼로 라이프사이클 통합
- **상세**: [DECISIONS.md](DECISIONS.md#unity-기본-컴포넌트-widget화) 참조

구현된 Widget:
| Widget | 역할 |
|--------|------|
| TextWidget | 텍스트 표시/스타일링 |
| ButtonWidget | 클릭 이벤트, 라벨/아이콘 |
| ImageWidget | Sprite 표시 |
| SliderWidget | 값 조절 (볼륨 등) |
| ToggleWidget | ON/OFF 스위치 |
| InputFieldWidget | 텍스트 입력 |
| ProgressBarWidget | 진행률 표시 |
| ScrollViewWidget | 스크롤 컨테이너 |

---

## Phase 8: MVP 화면 구현

### MVP 화면 구현

**커밋**: `677b606` Add MVP screen implementations
- TitleScreen, LobbyScreen, GachaScreen, CharacterListScreen 구현
- CurrencyHUD, GachaResultPopup 컴포넌트 추가
- DataManager/NetworkManager 연동

**커밋**: `87b25d6` Add MVP Editor tools
- MVPSceneSetup: SC Tools/MVP 메뉴로 씬/프리팹 자동 생성
- ProjectEditorSettings: 에디터 공통 설정 (기본 폰트 등)
- **상세**: [DECISIONS.md](DECISIONS.md#에디터-설정-scriptableobject-도입) 참조

### 게임 초기화 흐름

**커밋**: `9d296ac` Document architecture decisions
- GameBootstrap → NetworkManager 초기화 → DataManager 로드 → Login
- GameInitializedEvent로 초기화 완료 알림
- GameFlowController가 이벤트 수신 후 TitleScreen Push

### 캐릭터 상세 화면 & API 간소화

**커밋**: `1eb467b` Add CharacterDetailScreen and simplify Navigation API
- **CharacterDetailScreen 추가**
  - 캐릭터 기본 정보, 스탯, 레벨/돌파, 설명 표시
  - CharacterListScreen에서 아이템 클릭 시 상세 화면 이동

- **Navigation API 간소화**
  - `Screen.Open(state)` / `Popup.Open(state)` 패턴 도입
  - 기존 장황한 형식에서 간결한 형식으로 개선
  - **상세**: [DECISIONS.md](DECISIONS.md#navigation-api-간소화-open-패턴) 참조

- **ScrollView 개선**
  - RectMask2D 사용 (Image+Mask보다 효율적)
  - LayoutElement 추가 (VerticalLayoutGroup 높이 인식)

---

## 진행 중

현재 진행 중인 작업은 [PROGRESS.md](../PROGRESS.md) 참조
