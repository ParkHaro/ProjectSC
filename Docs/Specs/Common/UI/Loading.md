---
type: spec
assembly: Sc.Common
category: UI
status: implemented
version: "1.0"
dependencies: [Widget, Singleton]
created: 2026-01-17
updated: 2026-01-18
---

# 로딩 UI (Loading UI)

## 목적

비동기 작업 중 유저에게 피드백을 제공하는 로딩 인디케이터 시스템

---

## 클래스 역할 정의

| 클래스 | 위치 | 역할 | 책임 | 비책임 |
|--------|------|------|------|--------|
| `LoadingType` | Sc.Common | 로딩 타입 열거형 | 로딩 UI 종류 분류 | - |
| `LoadingService` | Sc.Common | 로딩 관리 서비스 | Show/Hide 관리, 레퍼런스 카운팅 | UI 렌더링 |
| `LoadingWidget` | Sc.Common | 로딩 UI 위젯 | UI 표시/숨김, 애니메이션 | 로딩 상태 관리 |

---

## 상세 정의

### LoadingType

**위치**: `Assets/Scripts/Common/UI/Loading/LoadingType.cs`

```csharp
public enum LoadingType
{
    /// <summary>전체 화면 로딩 (씬 전환, 대용량 로드)</summary>
    FullScreen,

    /// <summary>작은 인디케이터 (API 호출, 짧은 대기)</summary>
    Indicator,

    /// <summary>진행률 표시 (다운로드, 리소스 로드)</summary>
    Progress
}
```

### LoadingService

**위치**: `Assets/Scripts/Common/UI/Loading/LoadingService.cs`

**역할**: 로딩 상태 관리, 레퍼런스 카운팅

```csharp
public class LoadingService : Singleton<LoadingService>
{
    private LoadingWidget _widget;
    private int _refCount;
    private float _timeoutSeconds = 30f;

    public bool IsLoading => _refCount > 0;

    /// <summary>
    /// 로딩 위젯 등록 (LoadingWidget.Awake에서 호출)
    /// </summary>
    public void RegisterWidget(LoadingWidget widget);

    /// <summary>
    /// 로딩 표시 (레퍼런스 카운트 증가)
    /// </summary>
    public void Show(LoadingType type = LoadingType.Indicator, string message = null);

    /// <summary>
    /// 진행률과 함께 로딩 표시
    /// </summary>
    public void ShowProgress(float progress, string message = null);

    /// <summary>
    /// 진행률 업데이트 (이미 표시 중일 때)
    /// </summary>
    public void UpdateProgress(float progress, string message = null);

    /// <summary>
    /// 로딩 숨김 (레퍼런스 카운트 감소)
    /// </summary>
    public void Hide();

    /// <summary>
    /// 강제 숨김 (카운트 무시)
    /// </summary>
    public void ForceHide();
}
```

### LoadingWidget

**위치**: `Assets/Scripts/Common/UI/Widgets/LoadingWidget.cs`

**역할**: 실제 UI 표시/숨김

```csharp
public class LoadingWidget : Widget
{
    [Header("Full Screen")]
    [SerializeField] private GameObject _fullScreenPanel;
    [SerializeField] private TMP_Text _fullScreenMessage;
    [SerializeField] private Image _fullScreenSpinner;

    [Header("Indicator")]
    [SerializeField] private GameObject _indicatorPanel;
    [SerializeField] private Image _indicatorSpinner;

    [Header("Progress")]
    [SerializeField] private GameObject _progressPanel;
    [SerializeField] private TMP_Text _progressMessage;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _progressPercent;

    [Header("Animation")]
    [SerializeField] private float _fadeInDuration = 0.2f;
    [SerializeField] private float _fadeOutDuration = 0.15f;
    [SerializeField] private float _spinnerSpeed = 360f;

    public void Show(LoadingType type, string message);
    public void ShowProgress(float progress, string message);
    public void UpdateProgress(float progress, string message);
    public void Hide();
}
```

---

## 인터페이스

### LoadingService

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| `Show` | `void Show(LoadingType type, string message = null)` | 로딩 표시 |
| `ShowProgress` | `void ShowProgress(float progress, string message = null)` | 진행률 로딩 표시 |
| `UpdateProgress` | `void UpdateProgress(float progress, string message = null)` | 진행률 업데이트 |
| `Hide` | `void Hide()` | 로딩 숨김 |
| `ForceHide` | `void ForceHide()` | 강제 숨김 |
| `IsLoading` | `bool` (property) | 로딩 중 여부 |

---

## 동작 흐름

### 레퍼런스 카운팅

```
Show() → _refCount = 1 → Widget.Show()
Show() → _refCount = 2 → (유지)
Hide() → _refCount = 1 → (유지)
Hide() → _refCount = 0 → Widget.Hide()
```

### Show 흐름

```
LoadingService.Show(FullScreen, "스테이지 로딩 중...")
              │
              ▼
┌─────────────────────────────┐
│ _refCount++                 │
│ _refCount == 1?             │
│   └─ Yes → 타임아웃 시작    │
└─────────────────────────────┘
              │
              ▼
┌─────────────────────────────┐
│ _widget.Show(type, message) │
│   ├─ 타입별 패널 활성화     │
│   ├─ 메시지 설정            │
│   └─ FadeIn 애니메이션      │
└─────────────────────────────┘
```

### Hide 흐름

```
LoadingService.Hide()
              │
              ▼
┌─────────────────────────────┐
│ _refCount--                 │
│ _refCount == 0?             │
│   ├─ Yes → 타임아웃 취소    │
│   │         Widget.Hide()   │
│   └─ No → (유지)            │
└─────────────────────────────┘
```

### 진행률 업데이트

```
ShowProgress(0f, "다운로드 중...")
              │
              ▼
        표시 (Progress 타입)
              │
              ▼ (반복)
UpdateProgress(0.3f)
UpdateProgress(0.6f)
UpdateProgress(1.0f)
              │
              ▼
           Hide()
```

### 타임아웃 처리

```
Show() 호출
    │
    ▼
타임아웃 코루틴 시작 (30초)
    │
    ├─ 정상 Hide() → 타임아웃 취소
    │
    └─ 타임아웃 발생
           │
           ▼
       ForceHide()
       Log.Warning("로딩 타임아웃", LogCategory.UI)
```

---

## UI 구조

### FullScreen (전체 화면)

```
┌─────────────────────────────────────┐
│█████████████████████████████████████│ ← 어두운 오버레이 (Alpha 0.8)
│█████████████████████████████████████│
│█████████████████████████████████████│
│████████████  ◎  ████████████████████│ ← 스피너 (회전)
│█████████████████████████████████████│
│████████ 스테이지 로딩 중... █████████│ ← 메시지 (선택)
│█████████████████████████████████████│
│█████████████████████████████████████│
└─────────────────────────────────────┘
```

### Indicator (작은 인디케이터)

```
                              ┌─────┐
                              │  ◎  │ ← 우측 상단 작은 스피너
                              └─────┘
```

### Progress (진행률)

```
┌─────────────────────────────────────┐
│█████████████████████████████████████│
│█████████████████████████████████████│
│████████████  ◎  ████████████████████│ ← 스피너
│█████████████████████████████████████│
│████████ 다운로드 중... ██████████████│ ← 메시지
│█████████████████████████████████████│
│████  ████████████░░░░░░░░░░░  ██████│ ← 프로그레스 바
│████           65%              ██████│ ← 퍼센트
│█████████████████████████████████████│
└─────────────────────────────────────┘
```

---

## 사용 패턴

### 기본 사용 (Indicator)

```csharp
LoadingService.Instance.Show();
await SomeAsyncOperation();
LoadingService.Instance.Hide();
```

### 전체 화면 로딩

```csharp
var msg = StringManager.Get("loading.stage_enter");
LoadingService.Instance.Show(LoadingType.FullScreen, msg);

await LoadStageResources();

LoadingService.Instance.Hide();
```

### 진행률 표시

```csharp
var msg = StringManager.Get("loading.download");
LoadingService.Instance.ShowProgress(0f, msg);

var handle = Addressables.DownloadDependenciesAsync(key);
while (!handle.IsDone)
{
    LoadingService.Instance.UpdateProgress(handle.PercentComplete);
    await UniTask.Yield();
}

LoadingService.Instance.Hide();
```

### 중첩 호출 (안전)

```csharp
// 시스템 A
LoadingService.Instance.Show();
await OperationA();
// 시스템 B도 동시에 Show() 호출 가능
LoadingService.Instance.Hide();

// 시스템 B
LoadingService.Instance.Show();
await OperationB();
LoadingService.Instance.Hide();
// 모든 Hide() 완료 시 실제 숨김
```

### try-finally 패턴 (권장)

```csharp
LoadingService.Instance.Show(LoadingType.FullScreen);
try
{
    await RiskyOperation();
}
finally
{
    LoadingService.Instance.Hide();
}
```

---

## 설정 (LoadingConfig)

**위치**: `Assets/Scripts/Common/UI/Loading/LoadingConfig.cs` (ScriptableObject)

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `TimeoutSeconds` | float | 30f | 타임아웃 시간 |
| `FadeInDuration` | float | 0.2f | 페이드인 시간 |
| `FadeOutDuration` | float | 0.15f | 페이드아웃 시간 |
| `SpinnerSpeed` | float | 360f | 스피너 회전 속도 (도/초) |
| `OverlayAlpha` | float | 0.8f | 전체화면 오버레이 투명도 |

---

## 프리팹 구조

**위치**: `Assets/Prefabs/UI/LoadingWidget.prefab`

```
LoadingWidget (Canvas - Overlay, SortOrder 100)
├── FullScreenPanel
│   ├── Background (Image, Black, Alpha 0.8)
│   ├── Spinner (Image, Rotating)
│   └── Message (TMP_Text)
├── IndicatorPanel
│   └── Spinner (Image, Small, Rotating)
└── ProgressPanel
    ├── Background (Image, Black, Alpha 0.8)
    ├── Spinner (Image, Rotating)
    ├── Message (TMP_Text)
    ├── ProgressBar (Slider)
    └── PercentText (TMP_Text)
```

---

## 초기화

### GameBootstrap에서

```csharp
// LoadingWidget 프리팹 인스턴스화 (DontDestroyOnLoad)
var loadingPrefab = Resources.Load<LoadingWidget>("UI/LoadingWidget");
var widget = Instantiate(loadingPrefab);
DontDestroyOnLoad(widget.gameObject);

// 또는 씬에 미리 배치하고 DontDestroyOnLoad 설정
```

### LoadingWidget.Awake에서

```csharp
private void Awake()
{
    LoadingService.Instance.RegisterWidget(this);
    HideAllPanels();
}
```

---

## 주의사항

1. **레퍼런스 카운팅 누수 방지**
   - try-finally 패턴 사용 권장
   - 타임아웃으로 최후 방어

2. **Canvas Sort Order**
   - 로딩 UI는 최상위 (100+)
   - 팝업보다 위에 표시

3. **입력 차단**
   - FullScreen 시 Raycast Target으로 입력 차단
   - Indicator는 입력 차단 안 함

4. **메시지 null 허용**
   - 메시지 없으면 스피너만 표시

5. **Progress 범위**
   - 0f ~ 1f (0% ~ 100%)
   - 범위 벗어나면 클램핑

---

## 폴더 구조

```
Assets/Scripts/Common/UI/
├── Loading/
│   ├── LoadingType.cs
│   ├── LoadingService.cs
│   └── LoadingConfig.cs
└── Widgets/
    └── LoadingWidget.cs
```

---

## 관련 문서

- [Common.md](../../Common.md) - 상위 문서
- [UISystem.md](../UISystem.md) - UI 시스템 개요
- [Widget.md](Widget.md) - 위젯 베이스 클래스
