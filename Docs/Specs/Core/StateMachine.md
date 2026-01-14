---
type: spec
assembly: Sc.Core
class: StateMachine
category: System
status: draft
version: "1.0"
dependencies: []
created: 2025-01-14
updated: 2025-01-14
---

# StateMachine\<T\>

## 역할
범용 상태 머신. Battle, UI 등 다양한 컨텍스트에서 재사용.

## 책임
- 상태 등록/관리
- 상태 전이 처리 (Exit → Enter)
- 현재 상태 Update 호출 위임
- 상태 타입 추적

## 비책임
- 개별 상태 로직
- 상태 전이 조건 판단
- Update 호출 타이밍 관리 (호출자 책임)

---

## 인터페이스

### IState\<T\>

| 멤버 | 타입 | 설명 |
|------|------|------|
| StateType | T | 상태 식별 Enum 값 |
| OnEnter() | void | 상태 진입 시 |
| OnUpdate() | void | 매 프레임 |
| OnExit() | void | 상태 퇴장 시 |

### StateMachine\<T\>

| 멤버 | 타입 | 설명 |
|------|------|------|
| CurrentStateType | T | 현재 상태 타입 |
| CurrentState | IState\<T\> | 현재 상태 인스턴스 |
| AddState | void | 상태 등록 |
| ChangeState | void | 상태 전이 |
| Update | void | 현재 상태 Update |
| Clear | void | 전체 초기화 |

### 제약
- T는 `Enum` 타입만

---

## 동작 흐름

### 상태 전이
```
ChangeState(NewState)
       ↓
  상태 존재?
   ├─ No → 에러 로그, 종료
   └─ Yes
       ↓
  동일 상태?
   ├─ Yes → 경고, 종료
   └─ No
       ↓
  CurrentState.OnExit()
       ↓
  CurrentState = NewState
       ↓
  CurrentState.OnEnter()
```

### Update 루프
```
MonoBehaviour.Update()
       ↓
  StateMachine.Update()
       ↓
  CurrentState?.OnUpdate()
```

---

## 상태 전이 예시 (Battle)

```
    ┌─────────┐
    │  Ready  │
    └────┬────┘
         ↓ StartBattle
    ┌──────────┐
┌──→│PlayerTurn│
│   └────┬─────┘
│        ↓ Action
│   ┌──────────┐
│   │Animating │
│   └────┬─────┘
│   ┌────┴────┐
│   ↓         ↓
│ EnemyTurn Victory/Defeat
│   ↓ Action
│ Animating
│   │
└───┘
```

---

## 사용 패턴

```csharp
// 초기화
var sm = new StateMachine<BattleState>();
sm.AddState(new ReadyState());
sm.AddState(new PlayerTurnState());
sm.ChangeState(BattleState.Ready);

// Update에서 호출
sm.Update();
```

---

## 주의사항

| 항목 | 설명 |
|------|------|
| Update 호출 | MonoBehaviour에서 직접 호출 필요 |
| 상태 등록 | ChangeState 전 모든 상태 AddState 필수 |
| 중복 전이 | 같은 상태로 전이 무시 |
| 정리 | 사용 완료 시 Clear() 호출 권장 |

## 관련
- [Core.md](../Core.md)
- [Packet/InGameEvents.md](../Packet/InGameEvents.md)
