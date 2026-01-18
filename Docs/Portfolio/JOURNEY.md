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

## Phase 9: Transition 애니메이션

### Screen/Popup Transition 구현

**커밋**: `45ae44e` 등 8개
- Screen/Popup 전환 시 애니메이션 지원
- DOTween 기반 구현
- **상세**: [DECISIONS.md](DECISIONS.md#screenpopup-transition-애니메이션-설계) 참조

**구현 내용**:
| 클래스 | 역할 |
|--------|------|
| Transition | 베이스 추상 클래스 (Enter/Exit) |
| FadeTransition | Screen용 페이드 인/아웃 |
| PopupTransition | Popup용 베이스 클래스 |
| PopupScaleTransition | Popup용 스케일+페이드 |

**Widget 개선**:
- CachedCanvasGroup 프로퍼티 추가 (성능 최적화)
- CanvasGroup 컴포넌트 자동 캐싱

**Context 통합**:
- ScreenWidget.Context.Enter/Exit에서 Transition 호출
- PopupWidget.Context.Enter/Exit에서 Transition 호출
- Builder 패턴으로 Transition 설정 가능

**Assembly Definition 수정**:
- Sc.Common.asmdef에 DOTween.Modules, UniTask.DOTween 참조 추가
- DOTween 확장 메서드 사용 가능

---

## Phase 10: OUTGAME-V1 설계

### Foundation & Stage 설계 (Phase 0, 3)

**커밋**: `1b1d122` Add spec documents and update CLAUDE.md
- Phase 0 Foundation 시스템 상세 설계
  - Logging: LogLevel/LogCategory, ILogOutput 확장성
  - Error: ErrorCode 체계, Result<T> 패턴
  - StringData: 자체 구현 문자열 관리 (경량화)
  - SaveManager: 버전 마이그레이션 시스템
  - LoadingService: 레퍼런스 카운팅 기반
- Phase 3 Stage 시스템 설계
  - StageDashboardScreen: 파티편성 + 스테이지 정보 통합
  - PresetGroupId: 컨텐츠별 파티 프리셋 그룹핑
  - 3-Star 시스템: 스테이지별 커스텀 조건
- **상세**: [DECISIONS.md](DECISIONS.md#presetgroupid-기반-파티-프리셋) 참조

### LiveEvent 설계 (Phase 4)

**커밋**: `e4cdc93` Add Phase 4 LiveEvent detailed spec
- **중요 결정**: 모듈형 서브컨텐츠 구조
  - EventSubContent 배열로 유연한 이벤트 구성
  - Mission, Stage, Shop, Minigame, Story 모듈
  - 탭 순서/해금 조건 데이터 제어
- **중요 결정**: 이벤트 재화 정책
  - 유예 기간 (GracePeriodDays) 후 자동 전환
  - 범용 재화로 ConversionRate 비율 전환
- LiveEventData/LiveEventProgress 구조
- EventMissionData/EventMissionProgress 구조
- 16가지 MissionConditionType 정의
- Request/Response 패턴 설계
- Error 코드 6001-6007 범위 할당
- **상세**: [DECISIONS.md](DECISIONS.md#모듈형-이벤트-서브컨텐츠-eventsubcontent) 참조

---

## Phase 11: 테스트 아키텍처 설계

### 테스트 인프라 설계

**문서**: `Docs/Specs/Testing/TestArchitecture.md`
- **문제**: Phase별 구현에 앞서 테스트 가능한 환경 필요
- **핵심 원칙**: 테스트는 마일스톤 Phase가 아닌 시스템 단위로 구성
- **상세**: [DECISIONS.md](DECISIONS.md#테스트-아키텍처-시스템-단위-테스트-설계) 참조

**주요 결정**:
| 항목 | 결정 |
|------|------|
| 의존성 관리 | SO + ServiceLocator 혼합 패턴 |
| 테스트 구조 | 시스템 단위 (5계층: Foundation → Content) |
| 자동화 시점 | 2차 구축 (수동 테스트 안정화 후) |
| 첫 대상 | Navigation 시스템 |

**설계된 구조**:
```
Assets/Scripts/
├── Core/
│   └── Services.cs              # ServiceLocator
├── Tests/
│   ├── Base/
│   │   └── SystemTestRunner.cs  # 테스트 러너 기반 클래스
│   ├── Mocks/
│   │   ├── MockTimeService.cs
│   │   └── MockSaveStorage.cs
│   └── Runners/
│       └── NavigationTestRunner.cs
└── Data/TestData/               # 테스트용 SO
```

**5계층 시스템 분류**:
- Foundation (Log, Error, Services) - 의존성 없음
- Infrastructure (Time, Save, Loading) - Foundation만 의존
- Data (Master, User, Network) - Infrastructure까지 의존
- UI (Widget, Navigation, Popup) - Data까지 의존
- Content (Gacha, Character, Stage) - 전체 의존 가능

**배운 점**:
- 테스트 구조는 마일스톤(What to build)이 아닌 아키텍처(How it's built)를 따라야 함
- 수동 테스트 시나리오를 먼저 검증하고 자동화하면 안정적
- ServiceLocator + SO 혼합 패턴이 Unity에서 실용적

---

## Phase 12: Foundation 시스템 구현

### 테스트 인프라 Phase 1

**커밋**: `16d33ee` Add test infrastructure phase 1
- 테스트 인프라 기본 구조 구현
- SystemTestRunner 베이스 클래스
- ITestInterfaces (ITimeService, ISaveStorage)

**커밋**: `65eb307` Refactor to system-based implementation approach
- 시스템 기반 구현 접근법으로 리팩터링
- Locator 패턴 설정 방식 개선

### Foundation 시스템 구현

**커밋**: `4815dee` Add Foundation systems with unit tests
- **Log 시스템**: Log, LogLevel, LogCategory (Unity Debug.Log 래퍼)
- **Error 시스템**: ErrorCode, Result<T> 패턴
- **SaveManager**: 저장 추상화, 버전 마이그레이션
- **상세**: [DECISIONS.md](DECISIONS.md#savemanager-저장소-추상화-isavestorage) 참조

**핵심 구현**:
| 컴포넌트 | 역할 | 위치 |
|----------|------|------|
| ISaveStorage | 저장소 추상화 인터페이스 | Foundation |
| FileSaveStorage | 파일 기반 저장소 구현체 | Foundation |
| MockSaveStorage | 테스트용 메모리 저장소 | Tests/Mocks |
| SaveMigrator | 버전 마이그레이션 체인 | Core/Services |
| SaveManager | Singleton, 자동 저장, 마이그레이션 | Core/Managers |

**테스트 인프라**:
| 테스트 | 대상 | 방식 |
|--------|------|------|
| SaveStorageTests | FileSaveStorage | NUnit (17개 테스트) |
| SaveMigratorTests | SaveMigrator | NUnit (5개 테스트) |
| MockSaveStorageTests | MockSaveStorage | NUnit (12개 테스트) |
| SaveManagerTestRunner | SaveManager 통합 | 런타임 시나리오 |

**설계 결정**:
- ISaveStorage를 Foundation에 배치 (순환 참조 방지)
- 테스트 가능성을 위한 생성자 DI 패턴 도입
- NUnit 기반 에디터 테스트와 런타임 시나리오 테스트 병행

**배운 점**:
- 순환 참조 문제는 인터페이스 위치 조정으로 해결 가능
- NUnit 테스트는 빠른 피드백에 효과적, 런타임 테스트는 실제 환경 검증에 효과적
- Mock 객체를 별도로 테스트하면 테스트 신뢰도 향상

---

## 진행 중

현재 진행 중인 작업은 [PROGRESS.md](../PROGRESS.md) 참조
