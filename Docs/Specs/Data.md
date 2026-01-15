---
type: overview
assembly: Sc.Data
category: Data
status: approved
version: "2.0"
dependencies: []
detail_docs: [Enums, MasterData, UserData]
created: 2025-01-14
updated: 2025-01-15
---

# Sc.Data

## 목적
게임 전체에서 사용되는 순수 데이터 정의. 마스터 데이터(정적)와 유저 데이터(동적) 구조 제공.

## 의존성
- **참조**: 없음 (최하위 레이어)
- **참조됨**: Sc.Packet, Sc.Core, Sc.Common, 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **마스터 데이터** | 정적 게임 데이터. ScriptableObject로 관리 |
| **유저 데이터** | 동적 플레이어 데이터. 서버 응답으로만 갱신 |
| **Database** | 마스터 데이터 컬렉션. Lookup 기능 제공 |

---

## 데이터 파이프라인

### 마스터 데이터
```
Excel (기획)
   ↓ export_master_data.py
JSON (Assets/Data/MasterData/*.json)
   ↓ MasterDataImporter (AssetPostprocessor)
ScriptableObject (Assets/Data/Generated/*.asset)
   ↓
DataManager (런타임 캐시)
```

### 유저 데이터
```
서버 응답 (또는 LocalApiClient 시뮬레이션)
   ↓ LoginResponse.UserData
DataManager.SetUserData() (초기 로드)
   ↓ 이후 액션
*Response.Delta (UserDataDelta)
   ↓
DataManager.ApplyDelta() (부분 갱신)
```

---

## 클래스 역할 정의

### Enums

| 클래스 | 역할 | 정의 항목 |
|--------|------|-----------|
| Rarity | 희귀도 | N, R, SR, SSR |
| CharacterClass | 캐릭터 직업 | Warrior, Mage, Archer, Support, Tank |
| Element | 속성 | Fire, Water, Earth, Wind, Light, Dark |
| SkillType | 스킬 유형 | Active, Passive, Ultimate |
| TargetType | 타겟 유형 | Self, SingleEnemy, AllEnemies, SingleAlly, AllAllies |
| ItemType | 아이템 유형 | Weapon, Armor, Accessory, Consumable, Material |
| Difficulty | 난이도 | Normal, Hard, Hell |
| GachaType | 가챠 유형 | Standard, Limited, Pickup |
| CostType | 재화 유형 | Gold, Gem, FreeGem, Stamina |

### 마스터 데이터 (ScriptableObject)

| 클래스 | 역할 | 주요 필드 |
|--------|------|-----------|
| CharacterData | 캐릭터 정적 데이터 | Id, Name, Rarity, Class, Element, BaseStats |
| SkillData | 스킬 정적 데이터 | Id, Name, Type, TargetType, Power, Cooldown |
| ItemData | 아이템 정적 데이터 | Id, Name, Type, Rarity, Stats |
| StageData | 스테이지 정적 데이터 | Id, ChapterId, Difficulty, Enemies, Rewards |
| GachaPoolData | 가챠 풀 정적 데이터 | Id, GachaType, CharacterIds, Rates |

### Database (ScriptableObject 컬렉션)

| 클래스 | 역할 | 기능 |
|--------|------|------|
| CharacterDatabase | 캐릭터 마스터 컬렉션 | GetById(), GetAll(), Count |
| SkillDatabase | 스킬 마스터 컬렉션 | GetById(), GetAll(), Count |
| ItemDatabase | 아이템 마스터 컬렉션 | GetById(), GetAll(), Count |
| StageDatabase | 스테이지 마스터 컬렉션 | GetById(), GetByChapter(), Count |
| GachaPoolDatabase | 가챠 풀 마스터 컬렉션 | GetById(), GetByType(), Count |

### 유저 데이터 (Struct)

| 클래스 | 역할 | 주요 필드 |
|--------|------|-----------|
| UserSaveData | 통합 저장 구조체 | Version, Profile, Currency, Characters, Items, ... |
| UserProfile | 유저 프로필 | Uid, Nickname, Level, Exp, CreatedAt, LastLoginAt |
| UserCurrency | 유저 재화 | Gold, Gem, FreeGem, Stamina, MaxStamina |
| OwnedCharacter | 보유 캐릭터 | InstanceId, CharacterId, Level, Ascension, Equipment |
| OwnedItem | 보유 아이템 | InstanceId, ItemId, Count, EnhanceLevel |
| StageProgress | 스테이지 진행 | ClearedStages (StageClearInfo 리스트) |
| GachaPityData | 가챠 천장 | PityInfos (GachaPityInfo 리스트) |
| QuestProgress | 퀘스트 진행 | Daily, Weekly, Achievement 퀘스트 |

---

## 관계도

```
[Enums]
   ↑ 사용
[MasterData SO] ←── 참조 ── [Database SO]
                               ↑ 캐시
                          [DataManager]
                               ↑ 저장
                          [UserData Structs]
                               ↑ Delta
                          [UserDataDelta]
```

---

## 폴더 구조

```
Assets/Scripts/Data/
├── Sc.Data.asmdef
├── Enums/
│   └── *.cs (9개)
├── ScriptableObjects/
│   ├── CharacterData.cs, SkillData.cs, ...
│   └── *Database.cs (5개)
└── Structs/
    ├── MasterData/
    │   └── BaseStats.cs, GachaRates.cs, ...
    └── UserData/
        └── UserSaveData.cs, UserProfile.cs, ...

Assets/Data/
├── MasterData/
│   └── *.json (기획 데이터)
└── Generated/
    └── *.asset (SO 에셋)
```

---

## 설계 원칙

1. **순수 데이터**: 로직/메서드 구현 금지 (팩토리, 검색 헬퍼만 허용)
2. **서버 중심**: 유저 데이터는 서버 응답으로만 갱신 (클라이언트 직접 수정 금지)
3. **불변성**: UserData는 DataManager를 통해서만 접근 (읽기 전용 뷰)
4. **직렬화**: Unity/JSON 직렬화 호환

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Enums | 9 | ✅ |
| MasterData SO | 5 | ✅ |
| Database SO | 5 | ✅ |
| UserData Structs | 8 | ✅ |
