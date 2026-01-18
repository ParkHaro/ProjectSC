# 진행 상황

## 상태 범례
- ⬜ 대기 | 📝 설계 검토 | 🔨 진행 중 | ✅ 완료

---

## 🚀 다음 작업 (clear 후 시작점)

**지시**: "[시스템명] 구현하자" (예: "Logging 구현하자", "SaveManager 구현하자")

**현재 마일스톤**: 🎯 아웃게임 아키텍처 1차 완성 (OUTGAME-V1)
- 상세 문서: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)

---

### 구현 원칙: 시스템 단위 개발

> **핵심**: Phase 단위가 아닌 **시스템 단위**로 구현 진행

**각 시스템 작업 흐름**:
```
1. 스펙 문서 확인/작성 (Docs/Specs/)
2. PROGRESS.md에 체크리스트 추가
3. 구현 착수
4. 완료 시 PROGRESS.md 업데이트
```

**원칙**:
- 의존성 기반 순서로 진행
- 스펙 문서화는 해당 시스템 구현 직전에 구체화
- 리팩토링 필요 시 영향 범위 명시

---

### 시스템 구현 순서 (의존성 기반)

#### Phase A: 기반 인프라 (독립)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 1 | Logging | ✅ | 없음 | ✅ Foundation/Logging.md |
| 2 | ErrorHandling | ✅ | 없음 | ✅ Foundation/Error.md |

#### Phase B: 기반 인프라 (Phase A 의존)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 3 | SaveManager | ⬜ | Result<T> | 마일스톤 내 |
| 4 | LoadingIndicator | ⬜ | Widget (있음) | 마일스톤 내 |

#### Phase C: 공통 데이터/서비스 (독립)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 5 | Reward | ⬜ | 없음 | ✅ Common/Reward.md |
| 6 | TimeService | ⬜ | 없음 | 마일스톤 내 |

#### Phase D: 공통 UI (Phase C 의존)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 7 | SystemPopup | ⬜ | Widget (있음) | ✅ Common/Popups/SystemPopup.md |
| 8 | RewardPopup | ⬜ | Reward | ✅ Common/Popups/RewardPopup.md |

#### Phase E: 서버 분리 (리팩토링)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 9 | LocalServer | ⬜ | Reward, TimeService, SaveManager | 작성 필요 |

#### Phase F: 컨텐츠 (순서 자유)
| # | 시스템 | 상태 | 의존성 | 스펙 문서 |
|---|--------|------|--------|-----------|
| 10 | Shop | ⬜ | LocalServer, SystemPopup | ✅ Shop.md |
| 11 | Stage | ⬜ | LocalServer, Reward | ✅ Stage.md |
| 12 | LiveEvent | ⬜ | LocalServer, TimeService | ✅ LiveEvent.md |
| 13 | GachaEnhancement | ⬜ | RewardPopup, LoadingIndicator | ✅ Gacha/Enhancement.md |
| 14 | CharacterEnhancement | ⬜ | Reward, SystemPopup | ✅ Character/Enhancement.md |
| 15 | NavigationEnhancement | ⬜ | 없음 | ✅ Common/NavigationEnhancement.md |

---

### 리팩토링 영향 범위

| 시스템 | 영향받는 기존 코드 | 조치 |
|--------|-------------------|------|
| SaveManager | LocalApiService 저장 로직 | SaveManager로 이관 |
| ErrorHandling | LocalApiService 에러 반환 | Result<T> 적용 |
| LocalServer | LocalApiService 전체 | Sc.LocalServer Assembly 분리 |

---

### 이전 작업 (MVP 완료) ✅

- MVP 화면 (Title, Lobby, Gacha, CharacterList, CharacterDetail)
- CurrencyHUD, GachaResultPopup, ScreenHeader
- DataManager 연동, NetworkManager 이벤트 기반
- Screen/Popup Transition 애니메이션

---

## 🧪 테스트 인프라 (마일스톤 독립)

> **원칙**: Phase/마일스톤과 별개로 시스템 단위 테스트 환경 구축
> **상세 문서**: [Specs/Testing/TestArchitecture.md](Specs/Testing/TestArchitecture.md)

### 구축 상태

| 단계 | 항목 | 상태 | 비고 |
|------|------|------|------|
| 1차 | 베이스 인프라 | ✅ 완료 | Services, SystemTestRunner |
| 1차 | Navigation 테스트 | ✅ 완료 | 첫 번째 시스템 |
| 2차 | Unity Test Framework | ✅ 완료 | NUnit 기반 단위 테스트 |
| 2차 | Foundation 테스트 | ✅ 완료 | Log, Result, ErrorMessages (36개) |
| 3차 | 시스템 확장 | ⬜ 대기 | Loading, Popup, ... |

### 1차 구축 체크리스트

```
베이스 인프라:
- [x] Services.cs (ServiceLocator)
- [x] SystemTestRunner.cs (베이스 클래스)
- [x] TestCanvasFactory.cs
- [x] TestUIBuilder.cs
- [x] TestResult.cs

Mock:
- [x] MockTimeService.cs
- [x] MockSaveStorage.cs
- [x] MockApiClient.cs

Navigation 테스트:
- [x] NavigationTestScenarios.cs
- [x] NavigationTestRunner.cs
- [x] SystemTestMenu.cs (에디터 메뉴)
```

### 시스템별 테스트 우선순위

| 우선순위 | 시스템 | 의존성 | Mock 필요 |
|----------|--------|--------|----------|
| 1 | Navigation | 없음 | 없음 |
| 2 | LoadingIndicator | 없음 | 없음 |
| 3 | Result<T> | 없음 | 없음 |
| 4 | SaveManager | ISaveStorage | MockSaveStorage |
| 5 | TimeService | 없음 | 없음 |
| 6 | RewardPopup | IRewardHelper | MockRewardHelper |
| 7 | SystemPopups | 없음 | 없음 |
| 8 | Gacha | IApiClient | MockApiClient |
| 9 | Shop | IApiClient, ITimeService | Mock들 |

---

---

## 🎯 마일스톤: 아웃게임 아키텍처 1차 (OUTGAME-V1)

> **상세 문서**: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)
> **구현 추적**: 상단 [시스템 구현 순서](#시스템-구현-순서-의존성-기반) 섹션 참조

### 목표
아웃게임 핵심 기능(가챠, 상점, 캐릭터리스트, 스테이지진입, 이벤트진입) 기초 토대 완성

### Phase → 시스템 매핑

> 기존 Phase 기반 설계를 시스템 단위 구현으로 전환

| Phase | 포함 시스템 | 상태 |
|-------|------------|------|
| 0 | Logging, ErrorHandling, SaveManager, LoadingIndicator | ⬜ |
| 1 | Reward, TimeService, SystemPopup, RewardPopup, LocalServer | ⬜ |
| 2 | Shop | ⬜ |
| 3 | Stage | ⬜ |
| 4 | LiveEvent | ⬜ |
| 5 | GachaEnhancement, CharacterEnhancement, NavigationEnhancement | ⬜ |

### 상세 체크리스트

각 시스템 구현 시 해당 시스템 섹션에 체크리스트 추가.
Phase별 상세 설계는 [마일스톤 문서](Milestones/OUTGAME_ARCHITECTURE_V1.md) 참조.

---

## 단기 목표: MVP 플레이 가능 버전 ✅

### 목표
게임 실행 → 타이틀 → 로비 → 가챠/캐릭터 확인까지 플레이 가능한 최소 버전

### 화면 흐름

```
[Title Screen]
    │ 터치
    ▼
[Lobby Screen]
    ├─ 상단 HUD: 재화 표시 + 임시 충전 버튼
    │
    ├─ [가챠] 버튼 ──────→ [Gacha Screen]
    │                         ├─ 1회 소환 (300 Gem)
    │                         └─ 10회 소환 (2700 Gem)
    │
    └─ [캐릭터] 버튼 ────→ [Character List Screen]
                              └─ 보유 캐릭터 목록 표시
```

### 구현 범위

| 화면 | 기능 | 우선순위 |
|------|------|----------|
| **Title** | 터치 시 로비 전환 | P0 |
| **Lobby** | 가챠/캐릭터 버튼, 재화 HUD | P0 |
| **Gacha** | 1회/10회 소환, 결과 표시 | P0 |
| **CharacterList** | 보유 캐릭터 리스트 | P0 |
| **재화 HUD** | Gold/Gem 표시, 충전 버튼 | P0 |

### 필요 컴포넌트

**Screens (NavigationManager)**
- [x] TitleScreen
- [x] LobbyScreen
- [x] GachaScreen
- [x] CharacterListScreen
- [x] CharacterDetailScreen

**UI Components**
- [x] CurrencyHUD (재화 표시 + 충전)
- [x] GachaResultPopup (소환 결과)

**데이터 연동**
- [x] DataManager ↔ CurrencyHUD (재화 바인딩)
- [x] DataManager ↔ CharacterList (캐릭터 목록)
- [x] NetworkManager ↔ Gacha (소환 요청/응답)

**게임 흐름**
- [x] GameBootstrap - 초기화 흐름 (NetworkManager → DataManager → Login)
- [x] GameFlowController - 초기화 완료 후 TitleScreen 전환
- [x] GameInitializedEvent - 초기화 완료 이벤트

### 제외 범위 (추후)
- 캐릭터 상세 화면
- 장비/강화 시스템
- 퀘스트/스테이지
- 연출/애니메이션
- 사운드

---

## 현재 Phase: 1 - 기반 레이어 구현

### Phase 0 - 프로젝트 구조 설정 ✅

### 기반 레이어

| Assembly | 구조 | 구현 | 설명 |
|----------|------|------|------|
| Sc.Foundation | ✅ | ✅ | 최하위 레이어 (Singleton, EventManager) |
| Sc.Data | ✅ | ✅ | 마스터/유저 데이터 정의 |
| Sc.Event | ✅ | ✅ | 클라이언트 내부 이벤트 |
| Sc.Packet | ✅ | ✅ | 서버 통신 (NetworkManager, RequestQueue) |
| Sc.Core | ✅ | ✅ | 핵심 시스템 (DataManager, 핸들러) |
| Sc.Common | ✅ | ✅ | 공통 모듈 (UI 시스템 완료) |

### Editor (빌드 제외)

| Assembly | 도구 | 상태 | 설명 |
|----------|------|------|------|
| Sc.Editor.AI | UITestSceneSetup | ✅ | UI 테스트 씬/프리팹 자동 생성 |
| Sc.Editor.AI | NavigationDebugWindow | ✅ | Navigation 상태 시각화 윈도우 |
| Sc.Editor.AI | MVPSceneSetup | ✅ | MVP 씬/프리팹 자동 생성 (SC Tools/MVP) |
| Sc.Editor.Data | MasterDataImporter | ✅ | JSON → SO 자동 변환 |

### Contents - Shared

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Character | ✅ | ⬜ |
| Sc.Contents.Inventory | ✅ | ⬜ |

### Contents - InGame

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Battle | ✅ | ⬜ |
| Sc.Contents.Skill | ✅ | ⬜ |

### Contents - OutGame

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Lobby | ✅ | ⬜ |
| Sc.Contents.Gacha | ✅ | ⬜ |
| Sc.Contents.Shop | ✅ | ⬜ |
| Sc.Contents.Quest | ✅ | ⬜ |

---

## 현재 진행 중인 작업

### 데이터 아키텍처 v2.0 ✅

**배경**: 라이브 서비스 기준 서버 중심 아키텍처로 재설계

**핵심 방향**:
- 설계: 서버 중심 (Server Authority)
- 구현: 로컬 더미 데이터 (서버 응답 시뮬레이션)
- 교체: 인터페이스만 서버 구현체로 교체하면 라이브 전환

**데이터 파이프라인**:
```
마스터 데이터: Excel → Python Export → JSON → AssetPostprocessor → ScriptableObject
유저 데이터:   서버 응답 (LocalApiService 시뮬레이션) → DataManager 캐시 → JSON 로컬 저장
```

---

#### Phase 0: 기존 코드 리셋 ✅

- [x] 8b6aae0 커밋으로 리셋 (Data 관련 커밋 제거)
- [x] 관련 문서 자동 원복 (git reset으로 처리됨)

---

#### Phase 1: 마스터 데이터 파이프라인 ✅

**1-1. Excel 템플릿 및 샘플 데이터** ✅
- [x] JSON 샘플 데이터 작성 (Excel 대체 - 파이프라인 검증용)
  - Character.json (6개 캐릭터)
  - Skill.json (14개 스킬)
  - Item.json (10개 아이템)
  - Stage.json (6개 스테이지)
  - GachaPool.json (4개 가챠풀)
- [x] Excel 시트 구조 문서화 (README.md)

**1-2. Python Export 스크립트** ✅
- [x] export_master_data.py 작성
  - Excel/JSON 파일 읽기 (pandas/openpyxl)
  - 데이터 검증 (필수 필드, 타입, Enum, 참조 무결성)
  - JSON 변환 및 출력
- [x] requirements.txt 의존성 파일
- [x] 출력 경로: Assets/Data/MasterData/*.json

**1-3. ScriptableObject 정의** ✅
- [x] Enums 정의 (9개)
  - Rarity, CharacterClass, Element
  - SkillType, TargetType, ItemType
  - Difficulty, GachaType, CostType
- [x] 개별 데이터 SO (5개)
  - CharacterData.cs
  - SkillData.cs
  - ItemData.cs
  - StageData.cs
  - GachaPoolData.cs (GachaRates 구조체 포함)
- [x] Database SO (5개, 컬렉션 + Lookup)
  - CharacterDatabase.cs
  - SkillDatabase.cs
  - ItemDatabase.cs
  - StageDatabase.cs
  - GachaPoolDatabase.cs

**1-4. AssetPostprocessor 구현** ✅
- [x] Sc.Editor.Data Assembly 생성
- [x] MasterDataImporter.cs (Editor)
  - JSON 파일 변경 감지 (OnPostprocessAllAssets)
  - JSON 파싱 → SO 생성/갱신
  - Assets/Data/Generated/ 에 저장
  - Character, Skill, Item, Stage, GachaPool 임포터
- [x] MasterDataGeneratorWindow.cs (수동 생성 UI)
  - SC/Data/Master Data Generator 메뉴
  - JSON 파일 목록 및 개별/전체 변환
  - 생성된 Database 에셋 확인

---

#### Phase 2: 유저 데이터 구조 ✅

**2-1. 유저 데이터 구조체** ✅
- [x] UserProfile.cs (Uid, Nickname, Level, Exp, CreatedAt, LastLoginAt)
- [x] UserCurrency.cs (Gold, Gem, FreeGem, Stamina, MaxStamina)
- [x] OwnedCharacter.cs (InstanceId, CharacterId, Level, Ascension, 장비)
- [x] OwnedItem.cs (장비/소모품 분리, EnhanceLevel)
- [x] StageProgress.cs (StageClearInfo 포함, 챕터/스테이지 진행)
- [x] GachaPityData.cs (GachaPityInfo, 풀별 천장 카운트)
- [x] QuestProgress.cs (Daily/Weekly/Achievement, QuestInfo, QuestStatus)
- [x] UserSaveData.cs (통합 저장 구조체, Version, 조회 헬퍼)

**2-2. Request/Response 구조체** ✅
- [x] IRequest 인터페이스 (Timestamp)
- [x] IResponse 인터페이스 (IsSuccess, ErrorCode, ServerTime)
- [x] IGameActionResponse 인터페이스 (UserDataDelta 포함)
- [x] UserDataDelta.cs (변경분 전달용)
- [x] LoginRequest/Response (게스트 로그인, 재로그인)
- [x] GachaRequest/Response (GachaPullType, GachaResultItem)
- [x] ShopPurchaseRequest/Response (PurchaseRewardItem)

---

#### Phase 3: 서비스 레이어 ✅

**3-1. IApiService 인터페이스** ✅
- [x] InitializeAsync()
- [x] LoginAsync(LoginRequest)
- [x] FetchUserDataAsync()
- [x] GachaAsync(GachaRequest)
- [x] PurchaseAsync(ShopPurchaseRequest)
- [x] SendAsync<TRequest, TResponse>() (확장용)

**3-2. LocalApiService 구현** ✅
- [x] 로컬 JSON 저장/로드 (Application.persistentDataPath)
- [x] 서버 응답 시뮬레이션 (100ms 지연)
- [x] 게임 로직 로컬 실행
  - LoginAsync: 신규/기존 유저 처리, 초기 캐릭터 지급
  - GachaAsync: 확률 계산, 천장 시스템, 재화 차감
  - PurchaseAsync: 상품 구매 처리

---

#### Phase 4: Core 레이어 ✅

**4-1. Singleton<T> 베이스** ✅
- [x] 전역 단일 인스턴스 보장 (thread-safe lock)
- [x] DontDestroyOnLoad
- [x] OnApplicationQuit 처리 (_isQuitting 플래그)
- [x] OnSingletonAwake/OnSingletonDestroy 가상 메서드

**4-2. DataManager 구현** ✅
- [x] 마스터 데이터 캐시 (5개 Database SO 참조)
- [x] 유저 데이터 캐시 (읽기 전용 뷰 - Properties)
- [x] InitializeAsync(IApiService) - API 서비스 연결
- [x] LoginAsync(LoginRequest) - 로그인 및 데이터 로드
- [x] ApplyDelta(UserDataDelta) - 서버 응답으로만 갱신
- [x] OnUserDataChanged 이벤트
- [x] 직접 수정 메서드 제거 (서버 중심)

---

#### Phase 5: 통합 및 테스트 ✅

- [x] DataFlowTestWindow 에디터 도구 생성
- [x] 초기화 흐름 테스트 (Login → SetUserData → 마스터 로드)
- [x] 가챠 흐름 테스트 (Request → Response → Delta 적용)
- [x] 문서 업데이트
  - Data.md (마스터 데이터 파이프라인, 유저 데이터 구조)
  - Packet.md (IApiClient, LocalApiClient, Delta 패턴)
  - Core.md (DataManager 역할 변경, 서버 중심 아키텍처)

---

### Navigation 가시성 시스템 구현 ✅

**배경**: Screen/Popup 통합 스택 구조에서 가시성 제어 필요

**완료**:
- [x] Navigation.md 문서화 (v2.0) - 통합 스택, 가시성 규칙
- [x] UISystem.md 문서화 (v3.1) - Canvas.enabled 기반 가시성
- [x] INavigationContext 인터페이스 생성
- [x] NavigationManager 통합 스택 구조 변경
- [x] UITestSetup 통합 스택 구조 변경
- [x] Widget.cs - Canvas.enabled 기반 Show/Hide 구현
- [x] NavigationManager.cs - RefreshVisibility() 추가
- [x] UITestSetup.cs - RefreshVisibility() 추가

**핵심 규칙**:
```
1. 가시성 경계 = 마지막 Screen 인덱스
2. index >= boundary → Show, else → Hide
3. Canvas.enabled 사용 (SetActive 금지)
4. Push/Pop 후 RefreshVisibility() 호출
```

**관련 문서**: [Navigation.md](Specs/Navigation.md), [UISystem.md](Specs/Common/UISystem.md)

---

### Screen/Popup Transition 애니메이션 ✅

**배경**: Screen 전환 및 Popup 열기/닫기 시 부드러운 시각적 전환 필요

**요구사항**:
- Screen: Transition 애니메이션 (CrossFade - Out/In 동시)
- Popup: PopupTransition 클래스로 Tween 지원 (즉시 전환하되 구조 확보)
- Transition 중 입력 차단
- 기본 Transition: FadeTransition (DOTween 사용)

**현재 상태 분석**:
- `Transition.cs`: 클래스 존재하나 TODO 상태 (미구현)
- `ScreenWidget.Context`: `_transition` 필드 있음, `GetTransition()` 있음
- `PopupWidget.Context`: Transition 지원 없음
- `NavigationManager`: Transition 호출하지 않음

**구현 계획**:

Phase 1: 기반 준비
- [ ] Widget.cs - CanvasGroup 캐싱 추가
- [ ] Transition.cs - DOTween 기반 FadeTransition 구현

Phase 2: Screen Transition
- [ ] NavigationManager - Screen Push에 CrossFade 통합
- [ ] NavigationManager - Screen Pop에 Transition 통합

Phase 3: Popup Transition
- [ ] PopupTransition.cs - Popup 전용 Tween 클래스 생성
- [ ] PopupWidget.cs - Context에 Transition 지원 추가
- [ ] NavigationManager - Popup Push/Pop에 Transition 통합

Phase 4: 검증
- [ ] 기존 Screen 전환 테스트
- [ ] Popup 전환 테스트
- [ ] 입력 차단 동작 확인

**파일 변경 목록**:
- `Widget.cs` (+3줄) - CanvasGroup 캐싱
- `Transition.cs` (+50줄) - DOTween 기반 구현
- `PopupTransition.cs` (신규, ~80줄) - Popup 전용
- `PopupWidget.cs` (+20줄) - Transition 지원
- `NavigationManager.cs` (+35줄) - Transition 호출 로직

---

## 작업 로그

### 2026-01-18 (계속)
- [x] Unity Test Framework 기반 단위 테스트 추가
  - [x] LogTests.cs (11개 테스트) - Log 레벨, 카테고리, Output 관리
  - [x] ResultTests.cs (14개 테스트) - Success/Failure, OnSuccess/OnFailure, Map
  - [x] ErrorMessagesTests.cs (11개 테스트) - GetKey, GetMessage, LocalizeFunc
  - [x] Sc.Editor.Tests/Foundation/ 폴더 구조 생성
- [x] ErrorHandling 시스템 구현 완료
  - [x] ErrorCode.cs (System/Network/Data/Auth/Game/UI 대역)
  - [x] ErrorMessages.cs (ErrorCode → StringData 키 매핑)
  - [x] Result.cs (성공/실패 명시적 래퍼, 체이닝 API)
- [x] Logging 시스템 구현 완료
  - [x] LogLevel.cs (Debug/Info/Warning/Error/None)
  - [x] LogCategory.cs (System/Network/Data/UI/Battle/Gacha)
  - [x] ILogOutput.cs (출력 인터페이스)
  - [x] UnityLogOutput.cs (Unity 콘솔 출력)
  - [x] LogConfig.cs (ScriptableObject, 카테고리별 레벨)
  - [x] Log.cs (정적 API, Conditional 어트리뷰트)
- [x] 시스템 단위 구현 방식으로 전환
  - [x] Phase 단위 → 시스템 단위 구현 순서 확정
  - [x] PROGRESS.md 시스템 구현 순서 섹션 추가
  - [x] 의존성 기반 구현 순서 정의 (Phase A~F)
  - [x] 리팩토링 영향 범위 명시 (SaveManager, ErrorHandling, LocalServer)
  - [x] 구현 원칙 문서화 (스펙 → 체크리스트 → 구현 → 업데이트)

### 2026-01-18
- [x] 테스트 기초 인프라 1차 구축 완료
  - [x] Services.cs (ServiceLocator 패턴)
  - [x] Sc.Tests Assembly 생성
  - [x] TestResult.cs, TestCanvasFactory.cs, TestUIBuilder.cs
  - [x] SystemTestRunner.cs (베이스 클래스)
  - [x] ITestInterfaces.cs (ITimeService, ISaveStorage 임시 정의)
  - [x] MockTimeService.cs, MockSaveStorage.cs, MockApiClient.cs
  - [x] SimpleTestScreen.cs, SimpleTestPopup.cs (테스트 위젯)
  - [x] NavigationTestScenarios.cs (테스트 시나리오)
  - [x] NavigationTestRunner.cs
  - [x] SystemTestMenu.cs (에디터 메뉴)
- [x] Phase 5 기존 강화 상세 설계 완료
  - [x] Phase 5.1 가챠 강화 스펙 작성 (Gacha/Enhancement.md)
    - [x] GachaPoolData 확장 (배너, 천장, 픽업 필드)
    - [x] GachaHistoryRecord 유저 데이터
    - [x] GachaScreen 리팩토링 (BannerScrollView, PityProgress)
    - [x] GachaBannerItem, GachaHistoryScreen UI 설계
    - [x] Phase 0~4 시스템 연동 정의
  - [x] Phase 5.2 캐릭터 강화 스펙 작성 (Character/Enhancement.md)
    - [x] CharacterLevelData, CharacterAscensionData 마스터 데이터
    - [x] ExpMaterialData 경험치 재료
    - [x] PowerCalculator (Phase 3 전투력 공식)
    - [x] LevelUpRequest/AscendRequest/Response
    - [x] CharacterFilterState 필터/정렬 시스템
    - [x] CharacterDetailScreen 탭 구조 (Info, LevelUp, Equipment)
  - [x] Phase 5.3 Navigation 강화 스펙 작성 (Common/NavigationEnhancement.md)
    - [x] Shortcut API (Screen.Open 래핑)
    - [x] DeepLink 시스템 (projectsc://screen/{name}?{params})
    - [x] TabGroupWidget (LobbyTabGroup)
    - [x] BadgeManager (NotificationBadge, 메뉴 알림)
  - [x] OUTGAME_ARCHITECTURE_V1.md Phase 5 섹션 업데이트
- [x] 테스트 아키텍처 설계 완료
  - [x] 시스템 단위 테스트 구조 설계 (Phase 독립)
  - [x] 의존성 관리 패턴 결정 (SO + ServiceLocator 혼합)
  - [x] TestArchitecture.md 문서 작성
  - [x] PROGRESS.md 테스트 인프라 섹션 추가
  - [x] OUTGAME_ARCHITECTURE_V1.md 개발 원칙 추가
- [x] Phase 4 라이브 이벤트 시스템 설계 완료
  - [x] LiveEvent.md v2.0 구조 설계
    - [x] 모듈형 서브컨텐츠 (EventSubContent)
    - [x] EventType: Story, Collection, Raid, Login, Celebration, Collaboration
    - [x] EventSubContentType: Mission, Stage, Shop, Minigame, Story
  - [x] 이벤트 재화 정책 설계
    - [x] EventCurrencyPolicy (유예 기간 + 범용 재화 전환)
  - [x] 미션 시스템 설계
    - [x] MissionConditionType (16종)
    - [x] EventMissionData, EventMissionGroup
    - [x] EventMissionProgress
  - [x] UI 설계
    - [x] LiveEventScreen (배너 목록)
    - [x] EventDetailScreen (서브컨텐츠 탭)
    - [x] EventMissionTab, EventStageTab, EventShopTab
  - [x] Request/Response 설계
    - [x] GetActiveEvents, ClaimEventMission, VisitEvent
  - [x] 에러 코드 (6001~6007)
- [x] Phase 3 스테이지 시스템 설계 완료
  - [x] Stage.md v2.0 구조 설계
    - [x] StageDashboardScreen (타입 선택 대시보드)
    - [x] StageListScreen, PartySelectScreen (재사용 컴포넌트)
    - [x] StageType: MainStory, HardMode, DailyDungeon, BossRaid, Tower, Event
  - [x] 해금 시스템 (UnlockConditionType: None, StageClear, ChapterClear, PlayerLevel)
  - [x] 3성 시스템 (StarConditionType: Clear, NoDeaths, TurnLimit, FullHP, NoBossSkill, ElementParty)
  - [x] PresetGroupId 기반 파티 프리셋 시스템
    - [x] 스테이지 타입 내 세부 분류 지원 (daily_fire, daily_water, boss_dragon 등)
    - [x] StageData.PresetGroupId 필드 추가
    - [x] UserSaveData.PartyPresets: Dictionary<string, List<PartyPreset>>
  - [x] StageBattleRequest/Response 설계
  - [x] 전투력 계산 공식 정의
  - [x] 에러 코드 정의 (5101~5107)

### 2026-01-17 (계속)
- [x] 아웃게임 아키텍처 V1 검토 - Phase 1 완료
  - [x] Phase 1.1 보상 시스템 논의 완료
    - [x] RewardType 범위 검토 (수집형 RPG 비교)
    - [x] ItemCategory 세분화 결정
    - [x] 장비 시스템 (인벤토리 수량 기반, 장착 시 인스턴스화)
    - [x] 스킨 시스템 (별도 캐릭터로 처리)
  - [x] Phase 1.2 서버/클라 분리 논의 완료
    - [x] Sc.LocalServer Assembly 분리 결정
    - [x] ResponseValidator 2차 검증 (요청-응답 일관성)
    - [x] RewardHelper (클라 UI 유틸리티) 분리
  - [x] Phase 1.3 범용 팝업 논의 완료
    - [x] SystemPopup 하이브리드 구조 (Base + 특화)
    - [x] ConfirmPopup, AlertPopup, InputPopup, CostConfirmPopup
    - [x] RewardPopup 좌우 스크롤 카드형 결정
    - [x] RewardFullViewPopup 그리드 전체보기
  - [x] DECISIONS.md 기록 완료
  - [ ] Phase 2~5 검토 (추후)

### 2026-01-17
- [x] 아웃게임 아키텍처 갭 분석
  - [x] 현재 구현 상태 파악 (가챠/캐릭터리스트 기초 완료)
  - [x] 필요 기능 분석 (상점, 스테이지, 이벤트)
  - [x] 공통 필요 모듈 식별 (RewardInfo, ConfirmPopup, RewardPopup)
- [x] 아웃게임 아키텍처 1차 마일스톤 문서화
  - [x] OUTGAME_ARCHITECTURE_V1.md 마일스톤 개요 문서
  - [x] Phase 1: Reward.md, ConfirmPopup.md, RewardPopup.md
  - [x] Phase 2: Shop.md, ShopProductData.md
  - [x] Phase 3: Stage.md
  - [x] Phase 4: LiveEvent.md (확장)
  - [x] PROGRESS.md 업데이트

### 2026-01-16 (저녁)
- [x] Screen/Popup Transition 애니메이션 구현
  - [x] Widget.cs - CanvasGroup 캐싱 (GetOrAddCanvasGroup)
  - [x] Transition.cs - DOTween FadeTransition, SlideTransition, CrossFade 구현
  - [x] PopupTransition.cs 생성 (PopupFadeTransition, PopupScaleTransition, PopupSlideTransition)
  - [x] PopupWidget.cs - abstract GetTransition, Context.Builder.SetTransition
  - [x] ScreenWidget.cs - abstract GetTransition, Context.Builder.SetTransition
  - [x] NavigationManager.cs - PushAsync/PopAsync에 Transition 호출 통합

### 2026-01-16 (오후)
- [x] ScreenHeader 뒤로가기 버튼 → Navigation 연동 버그 수정
  - [x] 원인 분석: ScreenHeader.Initialize()가 호출되지 않아 버튼 리스너 미등록
  - [x] Start()에서 Initialize() 자동 호출 추가
  - [x] Configure()에 방어적 초기화 추가 (Start 이전 호출 대비)

### 2026-01-16
- [x] 재화 시스템 확장 (Full 복잡도)
  - [x] CostType enum 확장 (16개 재화 타입)
  - [x] UserCurrency 필드 확장 (소환권, 육성재료, 컨텐츠재화, 시즌재화)
  - [x] EventCurrencyData 신규 구조체 (동적 이벤트 재화)
  - [x] UserSaveData v2 마이그레이션 (EventCurrency 추가)
  - [x] UserDataDelta EventCurrency 지원
  - [x] DataManager EventCurrency 프로퍼티 추가
  - [x] LocalApiClient 마이그레이션 로직 추가
  - [x] Data.md 문서 업데이트 (v2.1)
- [x] ScreenHeader 구현 (상단 공용 UI)
  - [x] 요구사항 정의 및 설계 토론
  - [x] ScreenHeader.md 스펙 문서 작성
  - [x] ScreenHeaderConfigData.cs (SO 정의)
  - [x] ScreenHeaderConfigDatabase.cs (Database SO)
  - [x] HeaderEvents.cs (이벤트 정의)
  - [x] ScreenHeader.cs (싱글턴 Widget)
  - [x] MasterDataImporter에 ScreenHeaderConfig 추가
  - [x] ScreenHeaderConfig.json (샘플 데이터)
  - [x] 기존 Screen에 Header 연동 (Title, Lobby, Gacha, CharacterList, CharacterDetail)
  - [x] MVPSceneSetup에 ScreenHeader 프리팹 자동생성 추가
  - [x] MVPSceneSetup에 마스터 데이터 자동생성 기능 추가
- [x] CharacterDetailScreen 구현
  - [x] CharacterDetailState (InstanceId, CharacterId)
  - [x] 캐릭터 기본 정보 표시 (이름, 희귀도, 클래스, 속성)
  - [x] 스탯 정보 표시 (HP, ATK, DEF, SPD, 치명률, 치명피해)
  - [x] 레벨, 돌파 단계, 설명 표시
  - [x] 뒤로가기 버튼 → NavigationManager.Back()
- [x] CharacterListScreen → CharacterDetailScreen 연동
  - [x] OnCharacterItemClicked에서 CharacterDetailScreen.Open() 호출
- [x] MVPSceneSetup에 CharacterDetailScreen 프리팹 생성 추가
- [x] Navigation API 간소화
  - [x] ScreenWidget.Open() / Push() 메서드 활성화
  - [x] PopupWidget.Open() / Push() 메서드 활성화
  - [x] 기존 코드 마이그레이션 (긴 형식 → 짧은 형식)
  - [x] Navigation.md 문서 업데이트 (v2.1)

### 2026-01-15 (오후 - 계속)
- [x] MVPSceneSetup Editor 도구 확장
  - [x] NetworkManager, GameBootstrap, GameFlowController 자동 생성
  - [x] Clear 기능에 새 오브젝트 포함
- [x] NetworkManager ↔ GachaScreen 연동
  - [x] GachaScreen에서 NetworkManager.Send() 호출
  - [x] GachaCompletedEvent/GachaFailedEvent 구독
  - [x] 결과 수신 시 GachaResultPopup 표시
- [x] 게임 초기화 흐름 구현
  - [x] GameInitializedEvent 이벤트 추가
  - [x] GameBootstrap에서 초기화 완료 시 이벤트 발행
  - [x] GameFlowController 생성 (이벤트 수신 → TitleScreen Push)
  - [x] Sc.Contents.Title Assembly에 Foundation, Event 참조 추가

### 2026-01-15 (오후)
- [x] MVP 화면 구현 (Phase 1)
  - [x] Sc.Contents.Title Assembly 생성
  - [x] TitleScreen 구현 (터치 시 로비 전환)
  - [x] LobbyScreen 구현 (가챠/캐릭터 버튼)
  - [x] GachaScreen 구현 (1회/10회 소환 UI)
  - [x] GachaResultPopup 구현 (소환 결과 표시)
  - [x] CharacterListScreen 구현 (보유 캐릭터 목록)
  - [x] CurrencyHUD 위젯 구현 (재화 표시 + 충전 이벤트)
- [x] Assembly 참조 설정
  - [x] Title → Lobby 참조
  - [x] Lobby → Gacha, Character 참조

### 2026-01-15
- [x] 기본 UI Widget 컴포넌트 구현 (8개)
  - [x] TextWidget (TMP_Text 래핑)
  - [x] ButtonWidget (Button + TMP_Text 래핑)
  - [x] ImageWidget (Image 래핑)
  - [x] SliderWidget (Slider 래핑, 양방향 바인딩)
  - [x] ToggleWidget (Toggle 래핑)
  - [x] InputFieldWidget (TMP_InputField 래핑)
  - [x] ProgressBarWidget (읽기 전용 진행률 표시)
  - [x] ScrollViewWidget (ScrollRect 래핑)
- [x] DataFlowTestWindow 에디터 도구 생성
  - [x] Login/Gacha 흐름 테스트 UI
  - [x] DataManager 자동 생성 및 Database 에셋 할당
  - [x] 실시간 데이터 상태 표시
- [x] 네트워크 이벤트 큐 아키텍처 구현
  - [x] Sc.Foundation Assembly 생성 (순환 참조 해결)
  - [x] Singleton, EventManager → Foundation으로 이동
  - [x] IApiClient 인터페이스 (non-generic SendAsync 추가)
  - [x] PacketDispatcher 콜백 패턴으로 변경 (이벤트 직접 발행 제거)
  - [x] NetworkManager 콜백 핸들러 구현 (RequestCompletedEvent, NetworkErrorEvent 발행)
  - [x] LocalApiClient non-generic SendAsync 구현
  - [x] RequestQueue 리플렉션 제거 (성능 최적화)
- [x] 데이터 아키텍처 v2.0 구현 완료
  - [x] 기존 Data 관련 커밋 리셋 (8b6aae0)
  - [x] 서버 중심 아키텍처로 재설계
  - [x] JSON → SO 파이프라인 구현
- [x] Phase 1: 마스터 데이터 파이프라인
  - [x] JSON 샘플 데이터 (Character, Skill, Item, Stage, GachaPool)
  - [x] Python Export 스크립트 (export_master_data.py)
  - [x] Enums 정의 (9개)
  - [x] Data SO 정의 (5개)
  - [x] Database SO 정의 (5개)
  - [x] AssetPostprocessor 구현 (MasterDataImporter, MasterDataGeneratorWindow)
- [x] Phase 2: 유저 데이터 구조
  - [x] UserData 구조체 (7개 + UserSaveData 통합)
  - [x] Request/Response 구조체 (Login, Gacha, ShopPurchase)
  - [x] UserDataDelta 구현
- [x] Phase 3: 서비스 레이어
  - [x] IApiService 인터페이스
  - [x] LocalApiService 구현 (서버 응답 시뮬레이션)
- [x] Phase 4: Core 레이어
  - [x] Singleton<T> 베이스 클래스
  - [x] DataManager 구현 (서버 중심, Delta 적용)
- [x] 데이터 아키텍처 v1.0 구현 후 리셋
  - 리셋 사유: 로컬 중심 → 서버 중심 아키텍처 변경
- [x] Navigation 가시성 시스템 구현 완료
- [x] UI 시스템 테스트 환경 구축
- [x] NavigationDebugWindow 에디터 윈도우 생성
- [x] Navigation 통합 스택 구조로 변경
- [x] UI 시스템 코어 구현 (Widget, Screen, Popup, Navigation)
- [x] Portfolio 문서 및 서비스 추상화 패턴 추가

### 2026-01-14
- [x] 프로젝트 문서 구조 설정
- [x] CLAUDE.md 생성
- [x] Assembly 기반 아키텍처 설계
- [x] 스펙 문서 작성 완료
- [x] 폴더 구조 생성
- [x] Assembly Definition 파일 생성 (13개)
- [x] Event/Packet 분리 (클라이언트 이벤트 vs 서버 통신)
- [x] Editor AI 도구 Assembly 추가

---

## 아키텍처 변경 이력

### v2.0 (현재 진행 중)
- 서버 중심 설계 (Server Authority)
- 마스터 데이터: Excel → JSON → ScriptableObject 파이프라인
- 유저 데이터: 서버 응답 캐시 (읽기 전용)
- IApiService: 통합 서비스 인터페이스
- LocalApiService: 서버 응답 시뮬레이션

### v1.0 (리셋됨 - 8b6aae0)
- 로컬 중심 설계
- IDataService: CRUD 인터페이스
- LocalDataService: JSON 저장/로드
- DataManager: 직접 수정 가능 (ModifyUserData)
