---
type: spec
assembly: Sc.Data
class: BaseStats, BattleResult, RewardData
category: Struct
status: draft
version: "1.0"
dependencies: [CharacterEnums, ItemEnums]
created: 2025-01-14
updated: 2025-01-14
---

# Data - Structs

## 역할
값 타입 데이터 묶음. 복사 전달, 불변성 권장.

## 파일 구성

| 파일 | 용도 | 사용처 |
|------|------|--------|
| BaseStats.cs | 캐릭터 스탯 | Character, Battle |
| BattleResult.cs | 전투 결과 | Battle → Lobby |
| RewardData.cs | 보상 정보 | Battle, Quest, Gacha |

---

## BaseStats

캐릭터 기본 스탯.

### 필드

| 필드 | 타입 | 설명 | 범위 |
|------|------|------|------|
| HP | int | 체력 | 1 ~ 999,999 |
| ATK | int | 공격력 | 0 ~ 99,999 |
| DEF | int | 방어력 | 0 ~ 99,999 |
| SPD | int | 속도 (행동 순서) | 1 ~ 9,999 |
| CritRate | float | 크리티컬 확률 | 0.0 ~ 1.0 |
| CritDamage | float | 크리티컬 배율 | 1.0 ~ 5.0 |

### 특징
- `readonly struct` 권장
- 연산자 오버로딩: `+` (스탯 합산)
- 생성자에서 범위 클램핑

### 사용 패턴
```csharp
var total = baseStats + buffStats;
```

---

## BattleResult

전투 결과 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| IsVictory | bool | 승리 여부 |
| TurnCount | int | 소요 턴 수 |
| TotalDamage | long | 총 누적 데미지 |
| Rewards | List\<RewardData\> | 획득 보상 |
| DeadCharacterIds | List\<string\> | 사망 캐릭터 ID |

### 사용 시점
- BattleEndEvent 페이로드로 전달
- 결과 화면 표시
- 퀘스트 진행도 체크

---

## RewardData

보상 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Type | RewardType | 보상 종류 |
| ItemId | string | 아이템 ID (Type이 Item일 때) |
| Amount | int | 수량 |

### RewardType (내부 Enum)

| 값 | 설명 |
|----|------|
| Gold | 골드 |
| Gem | 보석 (유료 재화) |
| Item | 아이템 |
| Character | 캐릭터 |
| Exp | 경험치 |

### 특징
- `readonly struct` 권장
- ItemId는 Type이 Item/Character일 때만 유효

---

## 설계 원칙

1. **불변성**: `readonly struct` 사용
2. **직렬화**: `[Serializable]` 어트리뷰트
3. **유효성**: 생성자에서 범위 검증
4. **기본값**: 의미 있는 기본값 설정

## 주의사항
- Struct는 값 복사됨 (참조 X)
- 큰 Struct는 `in` 파라미터로 전달 권장

## 관련
- [Data.md](../Data.md)
- [Enums.md](Enums.md)
