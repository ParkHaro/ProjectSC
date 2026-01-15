# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## 현재 Phase: 1 - 기반 레이어 구현

### Phase 0 - 프로젝트 구조 설정 ✅

### 기반 레이어

| Assembly | 구조 | 구현 | 설명 |
|----------|------|------|------|
| Sc.Data | ✅ | ✅ | 마스터/유저 데이터 정의 |
| Sc.Event | ✅ | ⬜ | 클라이언트 내부 이벤트 |
| Sc.Packet | ✅ | ✅ | 서버 통신 인터페이스 (IApiService) |
| Sc.Core | ✅ | ✅ | 핵심 시스템 (Singleton, DataManager) |
| Sc.Common | ✅ | ✅ | 공통 모듈 (UI 시스템 완료) |

### Editor (빌드 제외)

| Assembly | 도구 | 상태 | 설명 |
|----------|------|------|------|
| Sc.Editor.AI | UITestSceneSetup | ✅ | UI 테스트 씬/프리팹 자동 생성 |
| Sc.Editor.AI | NavigationDebugWindow | ✅ | Navigation 상태 시각화 윈도우 |
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

### 데이터 아키텍처 v2.0 🔨

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

#### Phase 5: 통합 및 테스트 🔨

- [ ] 초기화 흐름 테스트 (Login → FetchUserData → 마스터 로드)
- [ ] 가챠 흐름 테스트 (Request → Response → Delta 적용)
- [ ] 문서 업데이트
  - Data.md (마스터 데이터 파이프라인)
  - Packet.md (IApiService, LocalApiService)
  - Core.md (DataManager 역할 변경)

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

## 작업 로그

### 2026-01-15
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
