---
type: spec
assembly: Sc.Common
class: BaseView, BasePresenter, IView, IPresenter
category: Pattern
status: draft
version: "1.0"
dependencies: [EventManager]
created: 2025-01-14
updated: 2025-01-14
---

# MVP 패턴

## 역할
UI와 로직 분리. 테스트 용이성, 유지보수성 향상.

## 책임
### View
- UI 요소 표시
- 사용자 입력 이벤트 전달
- 애니메이션/효과 재생

### Presenter
- 비즈니스 로직 처리
- 데이터 가공
- EventManager 구독/해제
- View 갱신 지시

## 비책임
- Model은 별도 정의 (Sc.Data)
- View는 로직 판단하지 않음
- Presenter는 Unity 컴포넌트 직접 참조 안 함

---

## 구조

```
┌─────────────────────────────────────────────────┐
│                    MVP 패턴                      │
│                                                 │
│  ┌─────────┐      ┌───────────┐      ┌───────┐ │
│  │  View   │◄────►│ Presenter │◄────►│ Model │ │
│  │ (Unity) │      │   (C#)    │      │ (Data)│ │
│  └─────────┘      └───────────┘      └───────┘ │
│       │                 │                 │     │
│   UI 표시          로직 처리         데이터     │
│   사용자 입력      데이터 가공       상태 저장  │
└─────────────────────────────────────────────────┘
```

---

## 인터페이스

### IView

| 멤버 | 타입 | 설명 |
|------|------|------|
| Show() | void | UI 표시 |
| Hide() | void | UI 숨김 |
| IsVisible | bool | 표시 상태 |
| SetInteractable | void | 상호작용 가능 여부 |

### IPresenter

| 멤버 | 타입 | 설명 |
|------|------|------|
| Initialize() | void | 초기화 |
| Dispose() | void | 정리 |
| View | TView | 연결된 뷰 |
| SetView | void | 뷰 연결 |

### BaseView 생명주기

| 메서드 | 시점 | 동작 |
|--------|------|------|
| Awake | 생성 | CanvasGroup 캐싱 |
| OnShow | Show 호출 | 커스텀 표시 로직 |
| OnHide | Hide 호출 | 커스텀 숨김 로직 |

### BasePresenter 생명주기

| 메서드 | 시점 | 동작 |
|--------|------|------|
| OnInitialize | Initialize 호출 | 초기 데이터 로드 |
| SubscribeEvents | Initialize 호출 | 이벤트 구독 |
| UnsubscribeEvents | Dispose 호출 | 이벤트 해제 |
| OnDispose | Dispose 호출 | 리소스 정리 |

---

## 동작 흐름

### 초기화
```
SceneController.Start()
       ↓
  new Presenter()
       ↓
  SetView(view)
       ↓
  Initialize()
   ├─ OnInitialize (데이터 로드)
   └─ SubscribeEvents (이벤트 구독)
       ↓
  ShowView()
```

### 사용자 입력
```
User Click (View)
       ↓
  View.OnButtonClicked 이벤트
       ↓
  Presenter.HandleButton()
       ↓
  비즈니스 로직 처리
       ↓
  View.SetXXX() 호출
```

### 이벤트 수신
```
EventManager.Publish(GoldChangedEvent)
       ↓
  Presenter.OnGoldChanged()
       ↓
  View.SetGold(newAmount)
```

---

## 사용 패턴

```csharp
// Presenter 생성 및 초기화
_presenter = new LobbyPresenter(playerModel);
_presenter.SetView(_lobbyView);
_presenter.Initialize();

// 종료 시 정리
_presenter.Dispose();
```

---

## View 인터페이스 설계 규칙

| 항목 | 규칙 |
|------|------|
| Set 메서드 | 단일 데이터 표시용 (SetGold, SetLevel) |
| On 이벤트 | 버튼 클릭 등 사용자 입력 (OnConfirmClicked) |
| 반환값 | 가급적 void, 상태 조회는 프로퍼티 |

---

## 역할 분담 규칙

| 항목 | View | Presenter |
|------|------|-----------|
| Unity 참조 | O | X |
| 비즈니스 로직 | X | O |
| 이벤트 구독 | 버튼만 | EventManager |
| 데이터 가공 | X | O |
| 테스트 | 어려움 | 용이 |

## 관련
- [Common.md](../Common.md)
- [UIComponents.md](UIComponents.md)
