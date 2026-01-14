---
type: overview
assembly: Sc.Data
category: Data
status: draft
version: "1.0"
dependencies: []
detail_docs: [Enums, Structs, ScriptableObjects]
created: 2025-01-14
updated: 2025-01-14
---

# Sc.Data

## 목적
게임 전체에서 사용되는 순수 데이터 정의. 로직 없이 데이터 구조만 제공.

## 의존성
- **참조**: 없음 (최하위 레이어)
- **참조됨**: Sc.Packet, Sc.Core, Sc.Common, 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **Enum** | 상수 집합. 타입 안전한 선택지 제공 |
| **Struct** | 값 타입 데이터 묶음. 복사 전달, 불변성 권장 |
| **ScriptableObject** | Unity 에셋 데이터. 에디터에서 편집, 런타임 읽기 전용 |

---

## 클래스 역할 정의

### Enums

| 클래스 | 역할 | 정의 항목 | 사용처 |
|--------|------|-----------|--------|
| CharacterEnums | 캐릭터 분류 상수 | Rarity(등급), Class(직업), ElementType(속성) | Character, Battle, Gacha |
| BattleEnums | 전투 상태/행동 상수 | BattleState(전투상태), ActionType(행동유형), TargetType(타겟유형) | Battle, Skill |
| ItemEnums | 아이템 분류 상수 | ItemType(종류), ItemRarity(등급) | Inventory, Shop |
| CommonEnums | 게임 전역 상수 | GameState(게임상태), SceneType(씬종류) | Core, 모든 Contents |

### Structs

| 클래스 | 역할 | 주요 필드 | 사용처 |
|--------|------|-----------|--------|
| BaseStats | 캐릭터 기본 스탯 | HP, ATK, DEF, SPD, CritRate, CritDamage | Character, Battle |
| BattleResult | 전투 결과 데이터 | IsVictory, Turns, Rewards | Battle → Lobby |
| RewardData | 보상 정보 | Type, ItemId, Amount | Battle, Quest, Gacha |

### ScriptableObjects

| 클래스 | 역할 | 주요 필드 | 사용처 |
|--------|------|-----------|--------|
| CharacterData | 캐릭터 정적 데이터 | Id, Name, Rarity, Class, Element, BaseStats, SkillIds | Character, Gacha |
| SkillData | 스킬 정적 데이터 | Id, Name, Type, TargetType, Power, CoolDown | Skill, Battle |
| ItemData | 아이템 정적 데이터 | Id, Name, Type, Rarity, Effects | Inventory, Shop |
| StageData | 스테이지 정적 데이터 | Id, Name, EnemyIds, Rewards | Battle |
| GachaPoolData | 가챠 풀 정적 데이터 | Id, Name, CharacterIds, Rates | Gacha |

---

## 관계도

```
[Enums] ←── 참조 ──┬── [Structs]
                   │
                   └── [ScriptableObjects]

ScriptableObjects는 Enums와 Structs를 필드로 사용
```

---

## 설계 원칙

1. **순수 데이터**: 로직, 메서드 구현 금지 (프로퍼티 getter만 허용)
2. **불변성 권장**: Struct는 readonly 권장
3. **직렬화 가능**: Unity/JSON 직렬화 고려

---

## 상세 문서
- [Enums.md](Data/Enums.md) - Enum 상세 정의
- [Structs.md](Data/Structs.md) - Struct 상세 정의
- [ScriptableObjects.md](Data/ScriptableObjects.md) - SO 상세 정의

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Enums | 4 | ⬜ |
| Structs | 3 | ⬜ |
| ScriptableObjects | 5 | ⬜ |
