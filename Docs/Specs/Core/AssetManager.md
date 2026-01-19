---
type: spec
assembly: Sc.Core
class: AssetManager
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton, Result, Log, ErrorCode, UniTask, Addressables]
created: 2026-01-19
updated: 2026-01-19
---

# AssetManager

## 역할
Addressables + Resources 통합 에셋 관리자. Scope 기반 + LRU 캐싱 혼합 전략으로 메모리를 효율적으로 관리한다.

## 책임
- Addressables / Resources 통합 로딩
- Scope(영역) 기반 에셋 수명 관리
- 레퍼런스 카운팅
- LRU 캐시 자동 해제
- 메모리 관리

## 비책임
- 에셋 생성/정의
- Addressables 그룹 설정
- 번들 빌드/배포
- 마스터 데이터 관리 (DataManager 담당)

---

## 클래스 구조

| 클래스 | 역할 | 위치 |
|--------|------|------|
| AssetManager | 메인 Singleton 매니저 | Core/Managers/ |
| AssetHandle\<T\> | 레퍼런스 카운팅 래퍼 | Core/Services/ |
| AssetScope | 영역별 에셋 그룹 관리 | Core/Services/ |
| AssetScopeManager | Scope 생성/삭제 헬퍼 | Core/Services/ |
| AssetCacheManager | LRU 캐시 관리 | Core/Services/ |
| AssetLoader | Addressables+Resources 로더 | Core/Services/ |

---

## 인터페이스

### AssetManager (Public)

| 멤버 | 시그니처 | 설명 |
|------|----------|------|
| Instance | `static AssetManager` | Singleton 인스턴스 |
| IsInitialized | `bool` | 초기화 완료 여부 |
| Initialize | `bool Initialize()` | 초기화 (GameBootstrap 최우선) |
| CreateScope | `AssetScope CreateScope(string name)` | Scope 생성 |
| GetScope | `AssetScope GetScope(string name)` | Scope 조회 |
| LoadAsync | `UniTask<Result<AssetHandle<T>>> LoadAsync<T>(string key, AssetScope scope = null)` | 에셋 로드 |
| Release | `void Release<T>(AssetHandle<T> handle)` | 에셋 해제 |
| ReleaseScope | `void ReleaseScope(AssetScope scope)` | Scope 전체 해제 |

### AssetHandle\<T\> (Public)

| 멤버 | 시그니처 | 설명 |
|------|----------|------|
| Key | `string` | 에셋 키 |
| Asset | `T` | 실제 에셋 |
| RefCount | `int` | 현재 참조 수 |
| IsValid | `bool` | 유효 여부 |
| Release | `void Release()` | 레퍼런스 감소 |
| implicit | `operator T` | 암시적 변환 |

### AssetScope (Public)

| 멤버 | 시그니처 | 설명 |
|------|----------|------|
| Name | `string` | Scope 이름 |
| AssetCount | `int` | 등록된 에셋 수 |
| IsReleased | `bool` | 해제 여부 |

---

## 동작 흐름

### 초기화 흐름

```
GameBootstrap.InitializeGameAsync()
    ↓
[최우선] AssetManager.Initialize()
    ↓
NetworkManager.InitializeAsync()
    ↓
DataManager.Initialize()
```

### 로드 흐름

```
LoadAsync<T>(key, scope)
    ↓
캐시 확인 ─── 있음 ──→ RefCount++, LRU 갱신, 반환
    │
   없음
    ↓
Addressables 로드 시도
    │
    ├─ 성공 → 캐싱, Scope 등록, 반환
    │
    └─ 실패
        ↓
    Resources 폴백 로드
        │
        ├─ 성공 → 캐싱, Scope 등록, 반환
        │
        └─ 실패 → Result.Failure(AssetNotFound)
```

### Scope 해제 흐름

```
ReleaseScope(scope)
    ↓
scope.Release()
    ↓
foreach (handle in handles)
    ↓
handle.Release() → RefCount--
    ↓
RefCount == 0? → LRU 대상으로 전환
    ↓
LRU 임계값 도달 시 → 자동 해제
```

---

## 캐싱 전략

### Scope 기반 (영역 관리)
- 화면/팝업 단위로 Scope 생성
- Scope 삭제 시 등록된 모든 에셋 자동 해제
- 명시적 수명 관리

### LRU 기반 (자동 해제)
- 캐시 크기 임계값 (기본 100개)
- RefCount == 0인 에셋만 해제 대상
- 가장 오래 미사용된 순서로 해제

### 레퍼런스 카운팅
- Load 시 RefCount++
- Release 시 RefCount--
- RefCount > 0이면 LRU 해제 보호

---

## 사용 패턴

### Scope 기반 (화면 단위)

```csharp
// 화면 진입
_scope = AssetManager.Instance.CreateScope("Gacha");
var banner = await AssetManager.Instance.LoadAsync<Sprite>("banner", _scope);
_image.sprite = banner.Value; // 암시적 변환

// 화면 종료
AssetManager.Instance.ReleaseScope(_scope);
```

### 전역 캐시 (공유 에셋)

```csharp
// Scope 없이 로드 (전역 LRU 캐시)
var icon = await AssetManager.Instance.LoadAsync<Sprite>("icon_gold");
_goldIcon.sprite = icon.Value;

// 명시적 해제
AssetManager.Instance.Release(icon.Value);
```

---

## 에러 코드

| 코드 | 이름 | 설명 |
|------|------|------|
| 1100 | AssetNotFound | 에셋을 찾을 수 없음 |
| 1101 | AssetLoadTimeout | 로드 타임아웃 |
| 1102 | AssetLoadPartialFail | 일부 에셋 로드 실패 |
| 1103 | AddressablesInitFailed | Addressables 초기화 실패 |

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 초기화 순서 | GameBootstrap에서 최우선 초기화 |
| RefCount 관리 | Load/Release 짝 맞추기 |
| Scope 수명 | 화면 종료 시 반드시 ReleaseScope |
| LRU 보호 | RefCount > 0이면 자동 해제 안 됨 |
| 메인 스레드 | Unity 메인 스레드에서만 호출 |

---

## 파일 구조

```
Assets/Scripts/Core/
├── Managers/
│   └── AssetManager.cs
└── Services/
    ├── AssetHandle.cs
    ├── AssetScope.cs
    ├── AssetScopeManager.cs
    ├── AssetCacheManager.cs
    └── AssetLoader.cs
```

---

## 관련
- [Singleton.md](../Foundation/Singleton.md)
- [Result.md](../Foundation/Error.md)
- [DataManager.md](DataManager.md)

---

## 상태

| 분류 | 상태 |
|------|------|
| 스펙 | ✅ draft |
| 구현 | ⬜ 대기 |
| 테스트 | ⬜ 대기 |
