---
type: spec
assembly: Sc.Common
class: ObjectPool, PoolManager, IPoolable
category: System
status: draft
version: "1.0"
dependencies: [Singleton]
created: 2025-01-14
updated: 2025-01-14
---

# Pool 시스템

## 역할
객체 재사용으로 GC 부하 감소. 전투 중 이펙트, 데미지 텍스트 등에 활용.

## 책임
- 오브젝트 풀링 (생성/반환)
- 풀 중앙 관리
- 초기 예열 (Prewarm)
- 최대 크기 제한

## 비책임
- 풀링 대상 로직
- 풀 생성 타이밍 결정
- 리소스 로딩

---

## 인터페이스

### IPoolable

| 멤버 | 타입 | 설명 |
|------|------|------|
| OnSpawn() | void | 풀에서 꺼낼 때 호출 |
| OnDespawn() | void | 풀에 반환할 때 호출 |

### ObjectPool\<T\>

| 멤버 | 타입 | 설명 |
|------|------|------|
| Spawn() | T | 풀에서 꺼내기 |
| Spawn(position) | T | 위치 지정 스폰 |
| Despawn(instance) | void | 풀에 반환 |
| DespawnAll() | void | 전체 반환 |
| Clear() | void | 풀 정리 (Destroy) |
| PooledCount | int | 대기 중인 객체 수 |
| ActiveCount | int | 사용 중인 객체 수 |

### PoolManager

| 멤버 | 타입 | 설명 |
|------|------|------|
| CreatePool\<T\> | void | 풀 생성 |
| Spawn\<T\> | T | 타입별 스폰 |
| Despawn\<T\> | void | 타입별 반환 |
| DespawnAll\<T\> | void | 타입별 전체 반환 |
| ClearPool\<T\> | void | 특정 풀 정리 |
| ClearAllPools | void | 전체 풀 정리 |

---

## 동작 흐름

### Spawn
```
PoolManager.Spawn<DamageText>()
       ↓
  ObjectPool<DamageText>.Spawn()
       ↓
  Stack에 대기 객체 있음?
   ├─ Yes → Pop
   └─ No → TotalCount < maxSize?
             ├─ Yes → Instantiate
             └─ No → null (경고)
       ↓
  SetActive(true)
       ↓
  OnSpawn() 호출
       ↓
  Active 목록에 추가
```

### Despawn
```
PoolManager.Despawn(instance)
       ↓
  Active 목록에서 제거
       ↓
  OnDespawn() 호출
       ↓
  SetActive(false)
       ↓
  Stack.Push
```

---

## 풀 계층 구조

```
PoolManager (Singleton)
   └─ [PoolContainer]
        ├─ [Pool_DamageText]
        │    ├─ DamageText (inactive)
        │    ├─ DamageText (inactive)
        │    └─ ...
        │
        └─ [Pool_HitEffect]
             ├─ HitEffect (inactive)
             └─ ...
```

---

## 사용 패턴

```csharp
// 풀 초기화 (씬 시작 시)
PoolManager.Instance.CreatePool(damageTextPrefab, 20, 50);

// 스폰
var text = PoolManager.Instance.Spawn<DamageText>(position);
text.SetDamage(100, isCritical: true);

// 자동 반환 (객체 내부에서)
PoolManager.Instance.Despawn(this);

// 전투 종료 시
PoolManager.Instance.DespawnAll<DamageText>();
```

---

## IPoolable 구현 규칙

### OnSpawn에서 해야 할 것
- 타이머/상태 초기화
- 시각 효과 초기화 (alpha, scale 등)
- 필요한 컴포넌트 활성화

### OnDespawn에서 해야 할 것
- 진행 중인 작업 취소 (Tween 등)
- 콜백/이벤트 정리
- 참조 해제

---

## 풀링 권장 대상

| 대상 | 이유 | 권장 크기 |
|------|------|-----------|
| 데미지 텍스트 | 전투 중 빈번 생성 | 20~50 |
| 히트 이펙트 | 공격마다 생성 | 10~30 |
| 투사체 | 스킬마다 생성 | 10~20 |
| 버프 아이콘 | UI 요소 | 10~20 |
| 파티클 | 이펙트 시스템 | 20~50 |

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 초기화 | 씬 로드 시 CreatePool 필수 |
| 정리 | 씬 전환 시 ClearAllPools 호출 |
| 크기 | maxSize 적절히 설정 (메모리 vs GC) |
| OnDespawn | 상태 초기화 철저히 (버그 원인) |
| 제약 | T는 Component + IPoolable |

## 관련
- [Common.md](../Common.md)
- [Core/Singleton.md](../Core/Singleton.md)
