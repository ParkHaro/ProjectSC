---
type: spec
assembly: Sc.Event
class: BattleStartEvent, BattleEndEvent, TurnStartEvent, TurnEndEvent, DamageEvent, HealEvent, SkillUsedEvent, BuffAppliedEvent, CharacterDeathEvent
category: Event
status: draft
version: "1.0"
dependencies: [BattleResult, ElementType]
created: 2025-01-14
updated: 2025-01-14
---

# Event - InGame Events

## 역할
전투 중 발생하는 이벤트. 전투 흐름, 액션, 상태 변화 알림.

---

## 전투 흐름 이벤트

### BattleStartEvent
전투 시작 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| StageId | string | 스테이지 ID |
| PartyCharacterIds | string[] | 파티 캐릭터 ID |

**발행**: BattleManager → **구독**: UI, AudioManager

### BattleEndEvent
전투 종료 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| Result | BattleResult | 전투 결과 |

**발행**: BattleManager → **구독**: Lobby, Quest, UI

### TurnStartEvent
턴 시작 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| TurnNumber | int | 현재 턴 |
| ActiveCharacterId | string | 행동 캐릭터 ID |
| IsPlayerTurn | bool | 플레이어 턴 여부 |

**발행**: BattleManager → **구독**: UI, AI

### TurnEndEvent
턴 종료 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| TurnNumber | int | 종료된 턴 |

**발행**: BattleManager → **구독**: UI, BuffSystem

---

## 액션 이벤트

### DamageEvent
데미지 발생 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| SourceId | string | 공격자 ID |
| TargetId | string | 피격자 ID |
| Amount | int | 데미지량 |
| IsCritical | bool | 크리티컬 여부 |
| Element | ElementType | 속성 |

**발행**: SkillSystem → **구독**: UI (데미지 텍스트), Character (HP 갱신)

### HealEvent
회복 발생 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| SourceId | string | 힐러 ID |
| TargetId | string | 대상 ID |
| Amount | int | 회복량 |

**발행**: SkillSystem → **구독**: UI, Character

### SkillUsedEvent
스킬 사용 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| CharacterId | string | 사용자 ID |
| SkillId | string | 스킬 ID |
| TargetIds | string[] | 타겟 ID 목록 |

**발행**: Character → **구독**: UI (연출), AudioManager (효과음)

---

## 상태 변화 이벤트

### BuffAppliedEvent
버프/디버프 적용 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| TargetId | string | 대상 ID |
| BuffType | BuffType | 버프 종류 |
| Value | float | 수치 |
| Duration | int | 지속 턴 |
| IsDebuff | bool | 디버프 여부 |

**BuffType**: AttackUp, DefenseUp, SpeedUp, AttackDown, DefenseDown, SpeedDown, Poison, Burn, Freeze, Stun

### CharacterDeathEvent
캐릭터 사망 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| CharacterId | string | 사망 캐릭터 ID |
| KillerId | string | 처치자 ID |
| IsPlayerCharacter | bool | 아군 여부 |

**발행**: Character → **구독**: BattleManager (승패 체크), UI (연출)

---

## 이벤트 흐름

```
BattleStartEvent
       ↓
   ┌───────────────┐
   │  Turn Loop    │
   │ TurnStartEvent│
   │      ↓        │
   │ SkillUsedEvent│
   │      ↓        │
   │ DamageEvent / │
   │ HealEvent /   │
   │ BuffAppliedEvent
   │      ↓        │
   │ CharacterDeathEvent?
   │      ↓        │
   │ TurnEndEvent  │
   └───────────────┘
       ↓
BattleEndEvent
```

---

## 관련
- [Event.md](../Event.md)
- [Data/Structs.md](../Data/Structs.md)
