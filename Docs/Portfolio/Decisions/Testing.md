# Testing Decisions

테스트 인프라 관련 의사결정 기록.

---

## 테스트 아키텍처: 시스템 단위 테스트

**일자**: 2026-01-18 | **상태**: 결정됨

### 컨텍스트
OUTGAME-V1 마일스톤 구현 전 테스트 인프라 필요. 테스트가 마일스톤 Phase에 귀속되지 않아야 함.

### 선택지 (의존성 관리)
1. **Service Locator만** - 단순하나 숨겨진 의존성
2. **SO 기반 DI** - Unity 친화적이나 복잡한 서비스 주입 어려움
3. **SO + ServiceLocator 혼합** - 역할 분리 (선택)
4. **DI 프레임워크** - 강력하나 외부 의존성 추가

### 선택지 (테스트 구조)
1. **Phase 기반** - 마일스톤과 일치하나 변경 시 재구성 필요
2. **시스템 단위** - 마일스톤 독립, 재사용 가능 (선택)

### 결정
**SO + ServiceLocator 혼합 + 시스템 단위 테스트**
```csharp
public static class Services
{
    public static void Register<T>(T service);
    public static T Get<T>();
    public static void Clear();
}
```

### 결과
5계층 시스템 분류 (Foundation → Infrastructure → Data → UI → Content).

---

## SaveManager 저장소 추상화 (ISaveStorage)

**일자**: 2026-01-18 | **상태**: 결정됨

### 컨텍스트
LocalApiClient에서 직접 파일 I/O 수행 → 테스트 어려움.

### 선택지
1. **직접 파일 I/O** - 단순하나 테스트 시 실제 파일 필요
2. **ISaveStorage 추상화** - Mock 주입으로 테스트 용이 (선택)
3. **Static 헬퍼** - Mock 교체 불가

### 결정
**ISaveStorage 인터페이스 추상화**
```
Foundation/
├── ISaveStorage.cs
└── FileSaveStorage.cs

Tests/Mocks/
└── MockSaveStorage.cs
```

### 결과
NUnit 단위 테스트 37개 작성 가능. LocalApiClient는 ISaveStorage 생성자 주입.

---

## PlayMode 테스트 인프라

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
기존 수동 테스트 시나리오를 자동화 테스트로 전환 필요.

### 선택지
1. **최소 변경** - 기존 코드 직접 사용, PlayMode 특화 기능 부족
2. **클린 아키텍처** - 완전 새 인프라, 중복
3. **실용적 균형** - 기존 헬퍼 재사용 + PlayMode 인프라 (선택)

### 결정
**기존 헬퍼 + PlayMode 인프라**
```
Tests/PlayMode/
├── PlayModeTestBase.cs      # Addressables 초기화, SetUp/TearDown
├── PrefabTestHelper.cs      # 프리팹 로드 헬퍼
└── PlayModeAssert.cs        # Unity 오브젝트 어서션
```

### 결과
NavigationTestScenarios를 NUnit으로 래핑하여 자동화. 10개 테스트 작성.

---

## RewardPopup 동적 아이템 관리 (IItemSpawner)

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
RewardPopup에서 보상 아이템 동적 생성/해제. 향후 풀링 도입 고려.

### 선택지
1. **직접 Instantiate/Destroy** - 단순하나 풀링 시 수정 필요
2. **풀링 먼저 구현** - 필요성 미검증
3. **IItemSpawner 추상화** - 풀링 시 구현체만 교체 (선택)

### 결정
**IItemSpawner 추상화 + SimpleItemSpawner**
```csharp
public interface IItemSpawner<T> where T : Component
{
    T Spawn(Transform parent);
    void Despawn(T item);
    void DespawnAll();
}
```

### 결과
"구현은 단순하게, 인터페이스는 확장 가능하게" 원칙 적용. 테스트 12개 작성.
