---
type: architecture
category: Testing
status: approved
version: "2.0"
created: 2026-01-18
updated: 2026-01-19
---

# 테스트 아키텍처

## 개요

시스템 단위 테스트 가능한 구조. 마일스톤/Phase와 독립적으로 각 시스템별 테스트 환경 제공.

**현재 상태**: NUnit 149개+, PlayMode 인프라 완료

---

## 테스트 레이어

| 레이어 | 위치 | 용도 | 실행 방식 |
|--------|------|------|-----------|
| **Edit Mode Tests** | `Editor/Tests/` | NUnit 단위 테스트 | Unity Test Runner (Edit Mode) |
| **PlayMode Tests** | `Tests/PlayMode/` | 자동화 Play Mode 테스트 | Unity Test Runner (Play Mode) |
| **Scenarios** | `Tests/Scenarios/` | 재사용 가능한 테스트 시나리오 | PlayMode 테스트에서 호출 |

```
┌─────────────────────────────────────────────────────────────┐
│                       테스트 계층                            │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  Level 3: E2E / Integration Test (PlayMode)                 │
│  └── 시스템 간 연동 (Navigation + Popup + Data)              │
│                                                             │
│  Level 2: System Test (PlayMode + Scenarios)                │
│  └── 시스템 단위 (Navigation만, RewardPopup만)               │
│                                                             │
│  Level 1: Unit Test (NUnit Edit Mode)                       │
│  └── 클래스/함수 단위 (Result<T>, TimeHelper)                │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## 핵심 원칙

| 원칙 | 설명 |
|------|------|
| **시스템 단위** | Phase가 아닌 시스템 기준 테스트 |
| **시나리오 재사용** | PlayMode와 수동 테스트가 같은 시나리오 공유 |
| **자동화 우선** | Unity Test Runner로 CI/CD 연동 가능 |
| **Mock 기반 격리** | ServiceLocator + Mock으로 의존성 격리 |

---

## 테스트 매트릭스

### 시스템별 구현 상태

| 시스템 | NUnit | PlayMode | Scenario |
|--------|-------|----------|----------|
| **Foundation** |
| Log | ✅ 11개 | - | - |
| Result<T> | ✅ 14개 | - | - |
| ErrorMessages | ✅ 11개 | - | - |
| **Core** |
| SaveStorage | ✅ 17개 | - | - |
| SaveMigrator | ✅ 완료 | - | - |
| TimeService | ✅ 25개 | - | - |
| TimeHelper | ✅ 20개 | - | - |
| **Common** |
| LoadingService | ✅ 완료 | - | - |
| RewardInfo | ✅ 16개 | - | - |
| RewardProcessor | ✅ 28개 | - | - |
| RewardHelper | ✅ 17개 | - | - |
| ConfirmState | ✅ 12개 | - | - |
| CostConfirmState | ✅ 22개 | - | - |
| RewardPopupState | ✅ 13개 | - | - |
| IPopupState | ✅ 8개 | - | - |
| SimpleItemSpawner | ✅ 12개 | - | - |
| **UI** |
| Navigation | - | ✅ 샘플 | ✅ 5개 시나리오 |
| Prefab Load | - | ✅ 샘플 | - |

**총계**: NUnit 149개+, PlayMode 샘플 2개, Scenario 5개

---

## 폴더 구조

```
Assets/Scripts/
├── Tests/                              # 런타임 테스트 (Sc.Tests)
│   ├── Sc.Tests.asmdef
│   │
│   ├── PlayMode/                       # PlayMode 테스트 인프라
│   │   ├── PlayModeTestBase.cs         # 베이스 클래스
│   │   ├── Helpers/
│   │   │   ├── PlayModeAssert.cs       # Unity 오브젝트 어서션
│   │   │   └── PrefabTestHelper.cs     # Addressables 프리팹 로드
│   │   └── Samples/
│   │       ├── NavigationPlayModeTests.cs
│   │       └── PrefabLoadPlayModeTests.cs
│   │
│   ├── Scenarios/                      # 재사용 시나리오
│   │   └── NavigationTestScenarios.cs
│   │
│   ├── Helpers/                        # 테스트 유틸리티
│   │   ├── TestCanvasFactory.cs
│   │   ├── TestResult.cs
│   │   └── TestUIBuilder.cs
│   │
│   ├── Mocks/                          # Mock 구현체
│   │   ├── MockTimeService.cs
│   │   ├── MockSaveStorage.cs
│   │   └── MockApiClient.cs
│   │
│   ├── Runners/                        # 시나리오 러너 (선택적)
│   │   ├── SystemTestRunner.cs         # 베이스 클래스
│   │   └── NavigationTestRunner.cs     # 수동 테스트용
│   │
│   └── TestWidgets/                    # 테스트용 위젯
│       ├── SimpleTestScreen.cs
│       └── SimpleTestPopup.cs
│
├── Editor/
│   └── Tests/                          # NUnit 단위 테스트 (Sc.Editor.Tests)
│       ├── Sc.Editor.Tests.asmdef
│       ├── Foundation/                 # 36개 테스트
│       ├── Core/                       # 36개+ 테스트
│       └── Common/                     # 77개+ 테스트
```

---

## NUnit 단위 테스트 (Edit Mode)

### Assembly 구성

**Sc.Editor.Tests.asmdef**
- 위치: `Assets/Scripts/Editor/Tests/`
- 참조: Sc.Foundation, Sc.Core, Sc.Common, Sc.Data, NUnit

### 테스트 작성 패턴

```csharp
[TestFixture]
public class ResultTests
{
    [Test]
    public void Success_WithValue_ReturnsIsSuccessTrue()
    {
        var result = Result<int>.Success(42);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(42));
    }

    [Test]
    public void Failure_WithError_ReturnsIsSuccessFalse()
    {
        var result = Result<int>.Failure(ErrorCode.InvalidOperation);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorCode.InvalidOperation));
    }
}
```

---

## PlayMode 테스트 인프라

### PlayModeTestBase

Addressables 초기화, TestCanvas 생성, 자동 정리를 담당하는 베이스 클래스.

```csharp
public abstract class PlayModeTestBase
{
    protected Canvas TestCanvas { get; }
    protected PrefabTestHelper PrefabHelper { get; }

    [UnitySetUp]
    public IEnumerator BaseSetUp()
    {
        // 1. Addressables 초기화
        // 2. TestCanvas 생성
        // 3. PrefabHelper 생성
        yield return OnSetUp();
    }

    [UnityTearDown]
    public IEnumerator BaseTearDown()
    {
        yield return OnTearDown();
        // 자동 정리: 핸들 해제, Canvas 파괴
    }

    protected virtual IEnumerator OnSetUp() { yield break; }
    protected virtual IEnumerator OnTearDown() { yield break; }
}
```

### PrefabTestHelper

Addressables 프리팹 로드 및 인스턴스 추적.

```csharp
// 프리팹 로드
GameObject prefab = null;
yield return LoadAssetAsync<GameObject>(address, p => prefab = p);

// 인스턴스 생성
yield return PrefabHelper.InstantiateAsync(address, TestCanvas.transform);
```

### PlayModeAssert

Unity 오브젝트 전용 어서션 헬퍼.

```csharp
PlayModeAssert.IsActive(gameObject);
PlayModeAssert.HasComponent<RectTransform>(gameObject);
PlayModeAssert.ChildCount(transform, 3);

// 비동기 대기
yield return PlayModeAssert.WaitUntilActive(gameObject, timeoutSeconds: 5f);
```

### 테스트 작성 예시

```csharp
[TestFixture]
public class NavigationPlayModeTests : PlayModeTestBase
{
    private NavigationTestScenarios _scenarios;

    protected override IEnumerator OnSetUp()
    {
        _scenarios = new NavigationTestScenarios(/*...*/);
        yield break;
    }

    [UnityTest]
    public IEnumerator PushPopAll_ThreeScreens_CountsCorrectly()
    {
        var task = _scenarios.RunPushPopAllScenario();
        while (!task.Status.IsCompleted()) yield return null;

        var result = task.GetAwaiter().GetResult();
        Assert.That(result.Success, Is.True, result.Message);
    }
}
```

---

## 시나리오 재사용

### 패턴: 시나리오 클래스 분리

시나리오 로직을 별도 클래스로 분리하여 PlayMode와 수동 테스트에서 공유.

```csharp
// Scenarios/NavigationTestScenarios.cs
public class NavigationTestScenarios
{
    public async UniTask<TestResult> RunPushPopAllScenario()
    {
        // 1. Screen 3개 Push
        // 2. Pop All
        // 3. 스택 카운트 검증
        return TestResult.Pass("Push/Pop All succeeded");
    }

    public async UniTask<TestResult> RunVisibilityScenario() { /*...*/ }
    public async UniTask<TestResult> RunBackNavigationScenario() { /*...*/ }
}
```

### 사용 방식

| 환경 | 호출 방식 |
|------|----------|
| **PlayMode Test** | `yield return scenarios.RunScenario().ToCoroutine()` |
| **수동 런타임** | `async void OnButtonClick() => await scenarios.RunScenario()` |

---

## Mock 시스템

### ServiceLocator 패턴

```csharp
// 서비스 등록
Services.Register<ITimeService>(new MockTimeService());
Services.Register<ISaveStorage>(new MockSaveStorage());

// 서비스 조회
var timeService = Services.Get<ITimeService>();

// 테스트 후 정리
Services.Clear();
```

### Mock 구현체

| Mock | 인터페이스 | 용도 |
|------|-----------|------|
| MockTimeService | ITimeService | 고정 시간 반환 |
| MockSaveStorage | ISaveStorage | 메모리 저장 |
| MockApiClient | IApiClient | 네트워크 시뮬레이션 |

---

## 테스트 프리팹 생성

**메뉴**: `SC Tools/Setup/Prefabs/`

| 메뉴 | 생성 항목 |
|------|----------|
| Create All Test Prefabs | SimpleTestScreen, SimpleTestPopup, TestSystemPopup |
| Delete All Test Prefabs | 위 프리팹 삭제 |

**생성 위치**: `Assets/Prefabs/Tests/`

---

## 확장 가이드

### 새 시스템 테스트 추가

1. **NUnit 테스트**: `Editor/Tests/{Layer}/{System}Tests.cs`
2. **시나리오 필요 시**: `Tests/Scenarios/{System}TestScenarios.cs`
3. **PlayMode 테스트**: `Tests/PlayMode/Samples/{System}PlayModeTests.cs`

### 새 Mock 추가

1. `Tests/Mocks/Mock{Service}.cs` 생성
2. 인터페이스 구현
3. `SystemTestRunner.RegisterMockServices()`에 등록

---

## 관련 문서

- [EditorTools.md](../Editor/EditorTools.md) - 에디터 도구
- [EDITOR_REFACTORING.md](../Editor/EDITOR_REFACTORING.md) - 리팩토링 계획
- [UISystem.md](../Common/UISystem.md) - UI 시스템
- [Navigation.md](../Navigation.md) - Navigation 시스템
