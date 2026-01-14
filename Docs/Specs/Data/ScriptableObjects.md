---
type: spec
assembly: Sc.Data
class: CharacterData, SkillData, ItemData, StageData, GachaPoolData
category: SO
status: draft
version: "1.0"
dependencies: [CharacterEnums, BattleEnums, ItemEnums, BaseStats, RewardData]
created: 2025-01-14
updated: 2025-01-14
---

# Data - ScriptableObjects

## 역할
Unity 에셋 데이터. 에디터에서 편집, 런타임 읽기 전용.

## 파일 구성

| 파일 | 용도 | 사용처 |
|------|------|--------|
| CharacterData.cs | 캐릭터 정의 | Character, Gacha |
| SkillData.cs | 스킬 정의 | Skill, Battle |
| ItemData.cs | 아이템 정의 | Inventory, Shop |
| StageData.cs | 스테이지 정의 | Battle |
| GachaPoolData.cs | 가챠 풀 정의 | Gacha |

---

## CharacterData

캐릭터 정적 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Id | string | 고유 식별자 (예: "char_001") |
| Name | string | 표시 이름 |
| Rarity | CharacterRarity | 등급 |
| Class | CharacterClass | 직업 |
| Element | ElementType | 속성 |
| BaseStats | BaseStats | 기본 스탯 |
| SkillIds | string[] | 보유 스킬 ID |
| Portrait | AssetReferenceSprite | 초상화 |
| Prefab | AssetReference | 전투 프리팹 |

### CreateAssetMenu
`Sc/Data/Character`

---

## SkillData

스킬 정적 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Id | string | 고유 식별자 |
| Name | string | 표시 이름 |
| Description | string | 스킬 설명 |
| TargetType | TargetType | 타겟 유형 |
| Power | float | 위력 배율 |
| CoolDown | int | 쿨다운 턴 |
| ManaCost | int | 마나 소모 |
| Effects | SkillEffect[] | 부가 효과 |
| Icon | AssetReferenceSprite | 아이콘 |
| VFX | AssetReference | 이펙트 프리팹 |

### SkillEffect (내부 구조체)

| 필드 | 타입 | 설명 |
|------|------|------|
| Type | SkillEffectType | 효과 종류 (Damage, Heal, Buff 등) |
| Value | float | 효과 수치 |
| Duration | int | 지속 턴 |

---

## ItemData

아이템 정적 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Id | string | 고유 식별자 |
| Name | string | 표시 이름 |
| Description | string | 설명 |
| Type | ItemType | 종류 |
| Rarity | ItemRarity | 등급 |
| MaxStack | int | 최대 중첩 |
| SellPrice | int | 판매 가격 |
| Icon | AssetReferenceSprite | 아이콘 |

---

## StageData

스테이지 정적 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Id | string | 고유 식별자 |
| Name | string | 표시 이름 |
| Chapter | int | 챕터 번호 |
| Stage | int | 스테이지 번호 |
| EnemyWaves | EnemyWave[] | 적 웨이브 |
| Rewards | RewardData[] | 클리어 보상 |
| RequiredStamina | int | 필요 스태미나 |

### EnemyWave (내부 구조체)

| 필드 | 타입 | 설명 |
|------|------|------|
| EnemyIds | string[] | 적 ID 목록 |
| IsBossWave | bool | 보스 웨이브 여부 |

---

## GachaPoolData

가챠 풀 정적 데이터.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Id | string | 고유 식별자 |
| Name | string | 배너 이름 |
| BannerImage | AssetReferenceSprite | 배너 이미지 |
| StartDate | DateTime | 시작일 |
| EndDate | DateTime | 종료일 |
| PickupCharacterIds | string[] | 픽업 캐릭터 |
| RateTable | GachaRate[] | 확률 테이블 |
| PityCount | int | 천장 카운트 |

### GachaRate (내부 구조체)

| 필드 | 타입 | 설명 |
|------|------|------|
| Rarity | CharacterRarity | 등급 |
| Rate | float | 확률 (0.0 ~ 1.0) |
| IsPickup | bool | 픽업 여부 |

---

## 설계 원칙

1. **읽기 전용**: 런타임에 수정 금지
2. **Addressables**: 리소스는 AssetReference 사용
3. **ID 규칙**: `{타입}_{숫자}` (예: char_001, skill_001)
4. **Getter만**: private 필드 + public 프로퍼티

## 주의사항
- SO는 공유 인스턴스 (수정 시 모든 참조에 영향)
- 에디터 수정 시 버전 관리 주의

## 관련
- [Data.md](../Data.md)
- [Enums.md](Enums.md)
- [Structs.md](Structs.md)
