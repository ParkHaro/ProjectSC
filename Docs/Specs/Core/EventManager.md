---
type: spec
assembly: Sc.Core
class: EventManager
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton]
created: 2025-01-14
updated: 2025-01-14
---

# EventManager

## 역할
이벤트 발행/구독 중계. 컨텐츠 간 디커플링 실현.

## 책임
- 이벤트 타입별 콜백 등록/해제 관리
- 이벤트 발행 시 등록된 콜백 호출
- 구독자 목록 관리

## 비책임
- 이벤트 내용 해석
- 비즈니스 로직 실행
- 이벤트 순서 보장

---

## 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| Subscribe | Subscribe\<T\>(Action\<T\>) | 이벤트 구독 |
| Unsubscribe | Unsubscribe\<T\>(Action\<T\>) | 구독 해제 |
| Publish | Publish\<T\>(T) | 이벤트 발행 |
| Clear | Clear\<T\>() | 특정 타입 구독 전체 해제 |
| ClearAll | ClearAll() | 모든 구독 해제 |

### 제약
- T는 `struct` 타입만 (이벤트 = 값 타입)

---

## 동작 흐름

```
Subscribe<T>(callback)
       ↓
  Dictionary<Type, Delegate>에 등록

Publish<T>(event)
       ↓
  해당 타입 Delegate 조회
       ↓
  등록된 모든 콜백 순차 호출
```

---

## 사용 패턴

```csharp
// 구독 (OnEnable)
EventManager.Instance.Subscribe<BattleEndEvent>(OnBattleEnd);

// 해제 (OnDisable)
EventManager.Instance.Unsubscribe<BattleEndEvent>(OnBattleEnd);

// 발행
EventManager.Instance.Publish(new BattleEndEvent { Result = result });
```

---

## 구독/해제 규칙

| 시점 | 동작 |
|------|------|
| OnEnable | Subscribe |
| OnDisable | Unsubscribe |
| OnDestroy | Unsubscribe (안전장치) |

**중요**: 반드시 Subscribe/Unsubscribe 쌍 유지

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 메모리 누수 | Unsubscribe 누락 시 발생 |
| 예외 전파 | 핸들러 예외 시 다음 핸들러 호출 안 됨 |
| 순서 | 구독 순서대로 호출 (보장하지 않음 권장) |
| 성능 | 프레임당 수백 개 이벤트는 피할 것 |

## 확장 고려

| 기능 | 설명 |
|------|------|
| 우선순위 | 구독 시 priority 파라미터 |
| 비동기 | UniTask 기반 async 발행 |
| 지연 발행 | 다음 프레임 발행 |

## 관련
- [Core.md](../Core.md)
- [Singleton.md](Singleton.md)
- [Packet.md](../Packet.md)
