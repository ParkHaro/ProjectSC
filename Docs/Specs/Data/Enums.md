---
type: spec
assembly: Sc.Data
class: CharacterEnums, BattleEnums, ItemEnums, CommonEnums
category: Enum
status: draft
version: "1.0"
dependencies: []
created: 2025-01-14
updated: 2025-01-14
---

# Data - Enums

## 역할
게임 전역에서 사용되는 상수 집합 정의. 타입 안전한 선택지 제공.

## 파일 구성

| 파일 | 정의 Enum | 사용처 |
|------|-----------|--------|
| CharacterEnums.cs | CharacterRarity, CharacterClass, ElementType | Character, Battle, Gacha |
| BattleEnums.cs | BattleState, ActionType, TargetType | Battle, Skill |
| ItemEnums.cs | ItemType, ItemRarity | Inventory, Shop |
| CommonEnums.cs | GameState, SceneType | Core, 전역 |

---

## CharacterEnums.cs

### CharacterRarity
캐릭터 등급. 가챠 확률, 스탯 배율에 영향.

| 값 | int | 설명 |
|----|-----|------|
| Normal | 1 | 1성 |
| Rare | 2 | 2성 |
| Epic | 3 | 3성 |
| Legendary | 4 | 4성 |
| Mythic | 5 | 5성 |

### CharacterClass
캐릭터 직업. 역할과 스킬 타입 결정.

| 값 | 역할 |
|----|------|
| Warrior | 근접 딜러/탱커 |
| Mage | 원거리 딜러 |
| Archer | 원거리 딜러 |
| Healer | 서포터 |
| Tank | 방어/도발 |

### ElementType
속성. 상성 시스템에 사용.

| 값 | 강함 → | 약함 → |
|----|--------|--------|
| Fire | Wind | Water |
| Water | Fire | Wind |
| Wind | Water | Fire |
| Light | Dark | Dark |
| Dark | Light | Light |

---

## BattleEnums.cs

### BattleState
전투 상태. StateMachine에서 사용.

| 값 | 설명 | 다음 상태 |
|----|------|-----------|
| Ready | 전투 준비 | PlayerTurn |
| PlayerTurn | 플레이어 턴 | Animating |
| EnemyTurn | 적 턴 | Animating |
| Animating | 연출 중 | PlayerTurn/EnemyTurn/Victory/Defeat |
| Victory | 승리 | (종료) |
| Defeat | 패배 | (종료) |

### ActionType
행동 유형.

| 값 | 설명 |
|----|------|
| Attack | 기본 공격 |
| Skill | 스킬 사용 |
| Defend | 방어 |
| Item | 아이템 사용 |

### TargetType
스킬 타겟 유형.

| 값 | 설명 |
|----|------|
| Self | 자신 |
| SingleEnemy | 적 1체 |
| AllEnemies | 적 전체 |
| SingleAlly | 아군 1체 |
| AllAllies | 아군 전체 |
| All | 모두 |

---

## ItemEnums.cs

### ItemType

| 값 | 설명 |
|----|------|
| Consumable | 소비 아이템 |
| Equipment | 장비 |
| Material | 재료 |
| Currency | 재화 |

### ItemRarity

| 값 | 설명 |
|----|------|
| Common | 일반 |
| Uncommon | 고급 |
| Rare | 희귀 |
| Epic | 영웅 |
| Legendary | 전설 |

---

## CommonEnums.cs

### GameState

| 값 | 설명 |
|----|------|
| Loading | 로딩 중 |
| Title | 타이틀 |
| Lobby | 로비 |
| Battle | 전투 중 |
| Paused | 일시정지 |

### SceneType

| 값 | 씬 이름 |
|----|---------|
| Title | TitleScene |
| Lobby | LobbyScene |
| Battle | BattleScene |
| Gacha | GachaScene |

---

## 설계 원칙

1. **명시적 값 할당**: 직렬화/DB 저장 시 안정성 (예: `Normal = 1`)
2. **확장 고려**: 중간값 여유 두기
3. **네이밍**: PascalCase, 단수형

## 관련
- [Data.md](../Data.md)
- [Structs.md](Structs.md)
