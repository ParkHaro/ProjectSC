---
type: spec
assembly: Sc.Common
class: Widget, ScreenWidget, PopupWidget, Panel, Transition
category: UI
status: draft
version: "3.2"
dependencies: [NavigationManager, EventManager, DOTween]
created: 2025-01-14
updated: 2025-01-17
---

# UI 시스템

## 목적

Widget 기반 Composition 패턴과 State/Context 기반 화면 관리 시스템.

---

## 의존성

- **참조**: NavigationManager, EventManager
- **참조됨**: 모든 Contents UI

---

## 클래스 역할 정의

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| Widget | UI 기본 단위 | 생명주기, 계층 관리 | 비즈니스 로직 |
| ScreenWidget | 전체 화면 베이스 | State 기반 상태 관리, Context 생성 | Popup 관리 |
| PopupWidget | 모달 팝업 베이스 | State 기반 상태 관리, 닫기 처리 | Screen 전환 |
| Panel | 화면 내 컨테이너 | 섹션 구분, 표시/숨김 | Navigation, 히스토리 |

---

## 계층 구조

```
Widget (Base)
│
├── 일반 Widget (~Widget)
│   └── ButtonWidget, CurrencyWidget, ListWidget, TabWidget
│
├── Panel (~Panel) - 컨테이너
│   └── InventoryPanel, SettingsPanel
│
├── ScreenWidget<TScreen, TState> (베이스)
│   └── LobbyScreen, BattleScreen, ShopScreen (구현 시 Widget 생략)
│
└── PopupWidget<TPopup, TState> (베이스)
    └── ConfirmPopup, RewardPopup, SettingsPopup (구현 시 Widget 생략)
```

### 네이밍 규칙

| 분류 | 베이스 클래스 | 구현 클래스 예시 |
|------|--------------|------------------|
| 일반 Widget | Widget | ButtonWidget, CurrencyWidget |
| 컨테이너 | Panel | InventoryPanel, SettingsPanel |
| 전체 화면 | ScreenWidget | LobbyScreen, BattleScreen |
| 팝업 | PopupWidget | ConfirmPopup, RewardPopup |

### 역할 구분

| 타입 | Navigation 대상 | 히스토리 | State 관리 |
|------|----------------|----------|------------|
| Widget | X | X | X |
| Panel | X | X (Screen State로 복구) | X |
| Screen | O | O | O |
| Popup | O | O | O |

---

## Widget 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| Parent | Widget | 부모 Widget |
| Children | IReadOnlyList\<Widget\> | 자식 Widget 목록 |
| IsVisible | bool | 표시 여부 |
| IsInitialized | bool | 초기화 여부 |
| OnInitialize() | void | 최초 1회 초기화 |
| OnBind(object) | void | 데이터 바인딩 |
| OnShow() | void | 표시 |
| OnHide() | void | 숨김 |
| OnRefresh() | void | 갱신 |
| OnRelease() | void | 해제 |

---

## 가시성 제어

### 제어 방식

**Canvas.enabled 사용** (GameObject.SetActive 아님)

| 방식 | 렌더링 | OnEnable/OnDisable | 코루틴/애니메이션 | 권장 |
|------|--------|-------------------|------------------|------|
| SetActive(false) | 중단 | 호출됨 | 중단 | X |
| Canvas.enabled = false | 중단 | 호출 안됨 | 유지 | O |
| CanvasGroup.alpha = 0 | 부분 중단 | 호출 안됨 | 유지 | 보조용 |

### 구현 규칙

```csharp
// Widget.Show()
_canvas.enabled = true;
IsVisible = true;
OnShow();

// Widget.Hide()
_canvas.enabled = false;
IsVisible = false;
OnHide();
```

### 컴포넌트 요구사항

| 타입 | Canvas | CanvasGroup | 용도 |
|------|--------|-------------|------|
| ScreenWidget | 필수 | 선택 | 가시성 제어 |
| PopupWidget | 필수 | 선택 | 가시성 제어 |
| Panel | 선택 | 선택 | 개별 표시/숨김 |
| Widget (일반) | 선택 | 선택 | 필요 시 |

### 설계 근거

1. **OnEnable/OnDisable 부작용 방지**: 이벤트 구독/해제, 초기화 로직 반복 실행 방지
2. **코루틴/애니메이션 유지**: Hide 중에도 진행 중인 작업 유지
3. **렌더링 완전 중단**: Canvas.enabled = false 시 드로우콜 발생 안함
4. **상태 보존**: RectTransform, UI 컴포넌트 값 유지

---

## ScreenWidget 인터페이스

### ScreenWidget (비제네릭 베이스)

| 멤버 | 타입 | 설명 |
|------|------|------|
| Context | abstract class | 상태 캡슐화 (State + Transition) |

### ScreenWidget\<TScreen, TState\>

| 멤버 | 타입 | 설명 |
|------|------|------|
| OnBind(TState) | void | State로 UI 초기화 |
| GetState() | TState | 현재 상태 반환 |
| CreateContext(TState) | Context.Builder | Context 빌더 생성 |
| Open(TState, Transition) | void | 정적 진입점 |

### ScreenWidget.Context

| 멤버 | 타입 | 설명 |
|------|------|------|
| View | ScreenWidget | Screen 인스턴스 |
| ScreenType | Type | Screen 타입 |
| State | TState | 상태 데이터 |
| Load() | UniTask | 리소스 로딩 |
| Enter() | UniTask | 화면 진입 (OnBind 호출) |
| Resume() | void | 활성화 |
| Pause() | void | 비활성화 |
| Exit() | UniTask | 화면 퇴장 (GetState 호출) |

### Context.Builder

| 멤버 | 타입 | 설명 |
|------|------|------|
| SetTransition(Transition) | Builder | Transition 설정 |
| Build() | Context | Context 생성 |

---

## PopupWidget 인터페이스

### PopupWidget (비제네릭 베이스)

| 멤버 | 타입 | 설명 |
|------|------|------|
| Context | abstract class | 상태 캡슐화 (State + Order) |
| OnEscape() | bool | ESC 키 처리 (닫기 허용 여부) |

### PopupWidget\<TPopup, TState\>

| 멤버 | 타입 | 설명 |
|------|------|------|
| OnBind(TState) | void | State로 UI 초기화 |
| GetState() | TState | 현재 상태 반환 |
| CreateContext(TState) | Context.Builder | Context 빌더 생성 |
| Open(TState) | void | 정적 진입점 |

### PopupWidget.Context.Builder

| 멤버 | 타입 | 설명 |
|------|------|------|
| SetOrder(int) | Builder | 정렬 순서 설정 |
| Build() | Context | Context 생성 |

---

## State 설계

### IScreenState / IPopupState

마커 인터페이스. State는 class로 정의.

| 포함 | 미포함 |
|------|--------|
| UI 복구에 필요한 데이터 | 비즈니스 로직 |
| 탭 인덱스, 스크롤 위치 | 계산된 값 |
| 선택된 아이템 ID | 캐시 데이터 |

---

## 생명주기 흐름

### Screen 생명주기

```
[Push]
   │
   ▼
Load ──▶ Enter(OnBind) ──▶ Resume
                              │
                    ┌─────────┴─────────┐
                    │                   │
                 [활성]              Pause
                    │                   │
                    │               Resume
                    │                   │
                    └─────────┬─────────┘
                              │
                           [Pop]
                              │
                              ▼
                     Exit (GetState)
                              │
                              ▼
                           Unload
```

### 생명주기 메서드 용도

| 메서드 | 시점 | 용도 |
|--------|------|------|
| Load | 인스턴스 로드 | Provider에서 가져오기 |
| Enter | 화면 진입 | OnBind로 State 바인딩 |
| Resume | 활성화 | 이벤트 구독, 갱신 시작 |
| Pause | 비활성화 | 이벤트 해제, 갱신 중지 |
| Exit | 화면 퇴장 | GetState로 상태 저장 |
| Unload | 리소스 해제 | Provider로 반환 |

---

## 사용 패턴

```csharp
// Screen 열기
LobbyScreen.Open(new LobbyState { ActiveTabIndex = 2 });

// Popup 열기
ConfirmPopup.Open(new ConfirmState { Title = "확인", Message = "진행?" });

// Builder 방식
NavigationManager.Instance.Push(
    LobbyScreen.CreateContext(state).SetTransition(new FadeTransition())
);
```

---

## 설계 원칙

1. **Composition over Inheritance**: Widget 조합으로 UI 구성
2. **State 기반 복구**: Context에 State 보관, 복귀 시 OnBind로 복구
3. **Screen/Popup 분리**: Screen은 전체 화면, Popup은 모달
4. **Panel은 Navigation 대상 아님**: Screen의 State로 상태 관리

---

## 주의사항

- State는 class로 정의 (GC 영향 미미, null 체크 용이)
- OnBind에서 null 체크 필수
- GetState는 현재 UI 상태를 정확히 반환해야 함
- Popup의 OnEscape()가 false 반환 시 Back()으로 닫히지 않음
- **Screen/Popup에 Canvas 컴포넌트 필수** (가시성 제어용)
- **Show/Hide에 GameObject.SetActive 사용 금지** (OnEnable/OnDisable 부작용)

---

## Transition 시스템

### 개요

Screen/Popup 전환 시 애니메이션을 적용하는 시스템.
DOTween 기반 구현.

### Transition 클래스

| 클래스 | 설명 |
|--------|------|
| Transition | 베이스 추상 클래스 |
| FadeTransition | 페이드 인/아웃 (CanvasGroup.alpha) |
| PopupTransition | Popup 전용 베이스 |
| PopupScaleTransition | 스케일 + 페이드 애니메이션 |

### Transition 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| Enter(Widget) | UniTask | 진입 애니메이션 |
| Exit(Widget) | UniTask | 퇴장 애니메이션 |

### 사용 패턴

```csharp
// Screen 전환 시 Transition 적용
LobbyScreen.Open(new LobbyState(), new FadeTransition(0.3f));

// Popup 전환 시 Transition 적용
ConfirmPopup.Open(new ConfirmState(), new PopupScaleTransition());

// Builder 방식
NavigationManager.Instance.Push(
    LobbyScreen.CreateContext(state)
        .SetTransition(new FadeTransition())
        .Build()
);
```

### 구현 요구사항

- Widget에 CanvasGroup 필요 (alpha 제어용)
- Widget.CachedCanvasGroup 프로퍼티로 캐싱
- Transition은 Context.Enter()/Exit()에서 호출됨

---

## 관련

- [Common.md](../Common.md)
- [UIComponents.md](UIComponents.md)
- [Navigation.md](../Navigation.md)
