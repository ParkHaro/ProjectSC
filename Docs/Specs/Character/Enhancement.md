---
type: spec
assembly: Sc.Contents.Character
category: Enhancement
status: draft
version: "1.0"
dependencies: [Sc.Common, Sc.Packet, Phase1.CostConfirmPopup, Phase3.PowerFormula]
created: 2026-01-18
updated: 2026-01-21
---

# 캐릭터 강화 (Phase 5.2)

## 목적

Phase 0~4 시스템과 연동하여 캐릭터 성장 시스템 구축

## Phase 연계

| 연계 대상 | 적용 내용 |
|-----------|-----------|
| Phase 0 LoadingIndicator | 강화 요청 시 로딩 UI |
| Phase 0 Result<T> | API 반환 타입 통일 |
| Phase 1 RewardInfo | 재료 아이템 표현 |
| Phase 1 ItemCategory | 장비/재료 분류 |
| Phase 1 CostConfirmPopup | 재화 소비 확인 |
| Phase 1 RewardPopup | 강화 결과 표시 (스탯 변화) |
| Phase 3 전투력 공식 | 캐릭터 전투력 계산 |

---

## 마스터 데이터

### CharacterLevelData

**위치**: `Assets/Scripts/Data/ScriptableObjects/CharacterLevelData.cs`

```csharp
[CreateAssetMenu(fileName = "CharacterLevelData", menuName = "SC/Data/CharacterLevelData")]
public class CharacterLevelData : ScriptableObject
{
    public Rarity Rarity;
    public List<LevelRequirement> LevelRequirements;

    public LevelRequirement GetRequirement(int level)
        => LevelRequirements.FirstOrDefault(r => r.Level == level);

    public int GetMaxLevel() => LevelRequirements.Max(r => r.Level);
}

[Serializable]
public struct LevelRequirement
{
    public int Level;               // 목표 레벨
    public int RequiredExp;         // 필요 누적 경험치
    public int GoldCost;            // 레벨업 비용 (기본값, 재료 사용 시)
}
```

**예시 데이터**:
| Rarity | Level | RequiredExp | GoldCost |
|--------|-------|-------------|----------|
| Star5 | 2 | 100 | 100 |
| Star5 | 10 | 1,500 | 500 |
| Star5 | 50 | 50,000 | 5,000 |
| Star5 | 80 | 200,000 | 20,000 |

### CharacterAscensionData

**위치**: `Assets/Scripts/Data/ScriptableObjects/CharacterAscensionData.cs`

```csharp
[CreateAssetMenu(fileName = "CharacterAscensionData", menuName = "SC/Data/CharacterAscensionData")]
public class CharacterAscensionData : ScriptableObject
{
    public Rarity Rarity;
    public List<AscensionRequirement> AscensionRequirements;

    public AscensionRequirement GetRequirement(int ascensionLevel)
        => AscensionRequirements.FirstOrDefault(r => r.AscensionLevel == ascensionLevel);

    public int GetMaxAscension() => AscensionRequirements.Count;
}

[Serializable]
public struct AscensionRequirement
{
    public int AscensionLevel;              // 0→1, 1→2, ...
    public int RequiredCharacterLevel;      // 필요 캐릭터 레벨
    public List<RewardInfo> Materials;      // 필요 재료 (Phase 1 RewardInfo)
    public int GoldCost;
    public StatBonus StatBonus;             // 돌파 시 스탯 보너스
    public int LevelCapIncrease;            // 레벨 상한 증가
}

[Serializable]
public struct StatBonus
{
    public int HP;
    public int ATK;
    public int DEF;
    public int SPD;
    public float CritRate;
    public float CritDamage;

    public static StatBonus operator +(StatBonus a, StatBonus b)
        => new StatBonus
        {
            HP = a.HP + b.HP,
            ATK = a.ATK + b.ATK,
            DEF = a.DEF + b.DEF,
            SPD = a.SPD + b.SPD,
            CritRate = a.CritRate + b.CritRate,
            CritDamage = a.CritDamage + b.CritDamage
        };
}
```

**예시 데이터**:
| Rarity | Ascension | ReqLevel | Materials | GoldCost | LevelCap |
|--------|-----------|----------|-----------|----------|----------|
| Star5 | 0→1 | 20 | 속성정수 x5 | 10,000 | +10 (30) |
| Star5 | 1→2 | 40 | 속성정수 x10, 보스재료 x3 | 30,000 | +10 (40) |
| Star5 | 2→3 | 50 | 속성정수 x20, 보스재료 x6 | 50,000 | +10 (50) |

### ExpMaterialData

**위치**: `Assets/Scripts/Data/ScriptableObjects/ExpMaterialData.cs`

```csharp
[CreateAssetMenu(fileName = "ExpMaterialData", menuName = "SC/Data/ExpMaterialData")]
public class ExpMaterialData : ScriptableObject
{
    public string Id;
    public string NameKey;
    public string IconPath;
    public int ExpValue;            // 제공 경험치
    public Rarity Rarity;           // 재료 희귀도 (표시용)
}
```

---

## 유저 데이터 확장

### OwnedCharacter 확장

**위치**: `Assets/Scripts/Data/Structs/UserData/OwnedCharacter.cs`

```csharp
[Serializable]
public struct OwnedCharacter
{
    public string InstanceId;
    public string CharacterId;
    public int Level;
    public int CurrentExp;              // 신규: 현재 레벨 내 경험치
    public int Ascension;

    // 장비
    public string WeaponInstanceId;
    public string[] AccessoryInstanceIds;

    // 잠금
    public bool IsLocked;
    public bool IsFavorite;

    // 획득 정보
    public long AcquiredAt;

    // 헬퍼
    public int GetLevelCap(CharacterAscensionData ascensionData)
    {
        int baseCap = 20;  // 기본 레벨캡
        for (int i = 0; i < Ascension; i++)
        {
            var req = ascensionData.GetRequirement(i);
            baseCap += req.LevelCapIncrease;
        }
        return baseCap;
    }
}
```

---

## Request/Response

### CharacterLevelUpRequest

**위치**: `Assets/Scripts/Packet/Requests/CharacterLevelUpRequest.cs`

```csharp
[Serializable]
public struct CharacterLevelUpRequest : IRequest
{
    public long Timestamp { get; set; }
    public string CharacterInstanceId;
    public Dictionary<string, int> MaterialUsage;  // ItemId → 사용량
}
```

### CharacterLevelUpResponse

**위치**: `Assets/Scripts/Packet/Responses/CharacterLevelUpResponse.cs`

```csharp
[Serializable]
public struct CharacterLevelUpResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }

    public int PreviousLevel;
    public int NewLevel;
    public int PreviousExp;
    public int NewExp;
    public CharacterStats PreviousStats;
    public CharacterStats NewStats;
    public int PreviousPower;           // Phase 3 공식
    public int NewPower;
}

[Serializable]
public struct CharacterStats
{
    public int HP, ATK, DEF, SPD;
    public float CritRate, CritDamage;
}
```

### CharacterAscensionRequest

**위치**: `Assets/Scripts/Packet/Requests/CharacterAscensionRequest.cs`

```csharp
[Serializable]
public struct CharacterAscensionRequest : IRequest
{
    public long Timestamp { get; set; }
    public string CharacterInstanceId;
}
```

### CharacterAscensionResponse

**위치**: `Assets/Scripts/Packet/Responses/CharacterAscensionResponse.cs`

```csharp
[Serializable]
public struct CharacterAscensionResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }

    public int PreviousAscension;
    public int NewAscension;
    public int PreviousLevelCap;
    public int NewLevelCap;
    public CharacterStats PreviousStats;
    public CharacterStats NewStats;
    public int PreviousPower;
    public int NewPower;
}
```

---

## 이벤트

### CharacterEvents.cs

**위치**: `Assets/Scripts/Event/OutGame/CharacterEvents.cs`

```csharp
public struct CharacterLevelUpEvent
{
    public string CharacterInstanceId;
    public int PreviousLevel;
    public int NewLevel;
    public int PowerChange;
}

public struct CharacterAscensionEvent
{
    public string CharacterInstanceId;
    public int PreviousAscension;
    public int NewAscension;
    public int NewLevelCap;
}

public struct CharacterEquipChangedEvent
{
    public string CharacterInstanceId;
    public string SlotType;             // Weapon, Accessory1, ...
    public string PreviousItemId;
    public string NewItemId;
}
```

---

## 전투력 계산 (Phase 3 재사용)

### PowerCalculator

**위치**: `Assets/Scripts/Core/Utility/PowerCalculator.cs`

```csharp
public static class PowerCalculator
{
    /// <summary>
    /// Phase 3 정의 공식
    /// 캐릭터 전투력 = (HP/10) + (ATK*5) + (DEF*3) + (SPD*2) + (CritRate*100) + (CritDamage*50)
    /// </summary>
    public static int Calculate(CharacterStats stats)
    {
        return (int)(
            stats.HP / 10f +
            stats.ATK * 5 +
            stats.DEF * 3 +
            stats.SPD * 2 +
            stats.CritRate * 100 +
            stats.CritDamage * 50
        );
    }

    public static int Calculate(OwnedCharacter character, CharacterData data, CharacterAscensionData ascensionData)
    {
        var stats = CalculateStats(character, data, ascensionData);
        return Calculate(stats);
    }

    public static CharacterStats CalculateStats(OwnedCharacter character, CharacterData data, CharacterAscensionData ascensionData)
    {
        // 기본 스탯 + 레벨 보정 + 돌파 보너스
        var baseStats = data.BaseStats;
        var levelMultiplier = 1f + (character.Level - 1) * 0.05f;

        var stats = new CharacterStats
        {
            HP = (int)(baseStats.HP * levelMultiplier),
            ATK = (int)(baseStats.ATK * levelMultiplier),
            DEF = (int)(baseStats.DEF * levelMultiplier),
            SPD = baseStats.SPD,
            CritRate = baseStats.CritRate,
            CritDamage = baseStats.CritDamage
        };

        // 돌파 보너스 적용
        for (int i = 0; i < character.Ascension; i++)
        {
            var req = ascensionData.GetRequirement(i);
            stats.HP += req.StatBonus.HP;
            stats.ATK += req.StatBonus.ATK;
            stats.DEF += req.StatBonus.DEF;
            stats.SPD += req.StatBonus.SPD;
            stats.CritRate += req.StatBonus.CritRate;
            stats.CritDamage += req.StatBonus.CritDamage;
        }

        return stats;
    }
}
```

---

## UI 구조

### CharacterListScreen (리팩토링)

```
[CharacterListScreen]
├── ScreenHeader
├── FilterBar
│   ├── FilterButton (필터 팝업 열기)
│   ├── ActiveFilters (적용된 필터 태그)
│   └── ClearButton (필터 초기화)
├── SortBar
│   ├── SortDropdown
│   │   ├── 레벨순 ▼▲
│   │   ├── 희귀도순 ▼▲
│   │   ├── 전투력순 ▼▲
│   │   └── 획득순 ▼▲
│   └── SortDirection (오름/내림)
├── CharacterGrid (ScrollView)
│   └── CharacterCard[]
│       ├── CharacterIcon
│       ├── RarityStars (★★★★★)
│       ├── ElementBadge
│       ├── LevelText (Lv.50)
│       ├── PowerText (12,345)              ← Phase 3 공식
│       ├── AscensionIndicator (●●○)
│       ├── LockedIcon (잠금 표시)
│       └── FavoriteIcon (즐겨찾기)
├── TotalInfo
│   ├── 보유 캐릭터: n/m
│   └── 선택: n (다중 선택 시)
└── ActionBar (다중 선택 시)
    ├── [잠금/해제]
    └── [즐겨찾기]
```

### CharacterFilterPopup

```
[CharacterFilterPopup]
├── Title (필터)
├── RaritySection
│   └── ToggleGroup (★1~★5)
├── ClassSection
│   └── ToggleGroup (전사/마법사/궁수/...)
├── ElementSection
│   └── ToggleGroup (불/물/풀/번개/빛/어둠)
├── StatusSection
│   ├── 잠금 상태 (전체/잠금/해제)
│   └── 즐겨찾기 (전체/즐겨찾기)
└── Buttons
    ├── [초기화]
    └── [적용]
```

### CharacterDetailScreen (리팩토링)

```
[CharacterDetailScreen]
├── ScreenHeader
├── CharacterVisual
│   ├── FullArt
│   └── ElementBadge
├── BasicInfo
│   ├── Name
│   ├── RarityStars
│   ├── Class, Element
│   └── PowerDisplay (전투력: 12,345)       ← Phase 3 공식
├── TabGroup
│   ├── [스탯] 탭
│   │   ├── HP: 12,000
│   │   ├── ATK: 1,500
│   │   ├── DEF: 800
│   │   ├── SPD: 120
│   │   ├── 치명률: 25%
│   │   ├── 치명피해: 150%
│   │   └── 전투력 상세 (클릭 시 공식 표시)
│   ├── [스킬] 탭
│   │   └── SkillList
│   └── [장비] 탭
│       ├── WeaponSlot
│       └── AccessorySlots (4개)
├── LevelInfo
│   ├── Lv.50 / 60 (현재/상한)
│   ├── ExpBar
│   └── 돌파 단계: ●●●○○
├── ActionButtons
│   ├── [레벨업] → CharacterLevelUpPopup
│   └── [돌파] → CharacterAscensionPopup
└── SubButtons
    ├── [잠금]
    └── [즐겨찾기]
```

### CharacterLevelUpPopup

```
[CharacterLevelUpPopup]
├── Title (레벨업)
├── LevelChangeDisplay
│   ├── CurrentLevel (Lv.50)
│   ├── Arrow (→)
│   └── TargetLevel (Lv.55)
├── StatComparison
│   ├── BeforeStats
│   │   ├── HP: 12,000
│   │   ├── ATK: 1,500
│   │   └── ...
│   ├── Arrow
│   └── AfterStats (변화량 표시)
│       ├── HP: 12,500 (+500)
│       ├── ATK: 1,600 (+100)
│       └── ...
├── PowerChange
│   └── 전투력: 12,345 → 13,500 (+1,155)
├── MaterialSelector
│   ├── ExpMaterialItem[]
│   │   ├── Icon
│   │   ├── Name
│   │   ├── ExpValue (+100)
│   │   ├── OwnedCount (x50)
│   │   └── UseCount [-][5][+]
│   ├── TotalExp (획득 경험치: 1,500)
│   └── [자동 선택] 버튼
├── CostDisplay
│   └── 비용: 5,000 Gold
└── Buttons
    ├── [취소]
    └── [레벨업] → CostConfirmPopup         ← Phase 1 연계
```

### CharacterAscensionPopup

```
[CharacterAscensionPopup]
├── Title (돌파)
├── AscensionChangeDisplay
│   ├── CurrentAscension (●●○○○)
│   ├── Arrow (→)
│   └── NextAscension (●●●○○)
├── RequirementCheck
│   ├── 필요 레벨: Lv.40 ✓ (또는 ✗)
│   └── (미충족 시 잠금 표시)
├── MaterialList
│   └── MaterialSlot[]
│       ├── Icon
│       ├── Name
│       ├── Required (x10)
│       ├── Owned (x15)
│       └── Status (✓ 또는 부족)
├── StatBonusPreview
│   ├── HP +200
│   ├── ATK +50
│   └── ...
├── LevelCapChange
│   └── 레벨 상한: 40 → 50
├── PowerChange
│   └── 전투력: 12,345 → 13,800 (+1,455)
├── CostDisplay
│   └── 비용: 30,000 Gold
└── Buttons
    ├── [취소]
    └── [돌파] → CostConfirmPopup           ← Phase 1 연계
        (재료/레벨 미충족 시 비활성화)
```

---

## 흐름

### 레벨업 흐름

```
[CharacterDetailScreen]
        │
        ▼ [레벨업] 클릭
┌─────────────────────────┐
│ CharacterLevelUpPopup   │
│ - 재료 선택             │
│ - 스탯 변화 미리보기    │
└─────────┬───────────────┘
          │ [레벨업] 클릭
          ▼
┌─────────────────────────┐
│ CostConfirmPopup        │  ← Phase 1
│ "5,000 Gold 소비?"      │
└─────────┬───────────────┘
          │ 확인
          ▼
┌─────────────────────────┐
│ LoadingIndicator        │  ← Phase 0
└─────────┬───────────────┘
          │
          ▼
[NetworkManager.Send(CharacterLevelUpRequest)]
          │
          ▼
┌─────────────────────────┐
│ RewardPopup (변형)      │  ← Phase 1
│ - 스탯 변화 표시        │
│ - 전투력 변화 표시      │
└─────────┬───────────────┘
          │
          ▼
[CharacterDetailScreen 갱신]
```

### 돌파 흐름

```
[CharacterDetailScreen]
        │
        ▼ [돌파] 클릭
┌─────────────────────────┐
│ CharacterAscensionPopup │
│ - 재료 확인             │
│ - 레벨 조건 확인        │
└─────────┬───────────────┘
          │ [돌파] 클릭
          ▼
(레벨/재료 미충족 시 AlertPopup)
          │
          ▼ (충족 시)
┌─────────────────────────┐
│ CostConfirmPopup        │  ← Phase 1
│ "30,000 Gold 소비?"     │
└─────────┬───────────────┘
          │ 확인
          ▼
[... 동일한 흐름 ...]
```

---

## 에러 코드

| ErrorCode | 값 | 설명 |
|-----------|-----|------|
| `CharacterNotFound` | 7001 | 캐릭터 없음 |
| `CharacterMaxLevel` | 7002 | 최대 레벨 도달 |
| `CharacterInsufficientMaterial` | 7003 | 재료 부족 |
| `CharacterInsufficientGold` | 7004 | 골드 부족 |
| `CharacterLevelRequirementNotMet` | 7005 | 돌파 레벨 요구사항 미충족 |
| `CharacterMaxAscension` | 7006 | 최대 돌파 도달 |
| `CharacterLevelCapReached` | 7007 | 레벨 상한 도달 (돌파 필요) |

---

## 상태

| 분류 | 상태 | 비고 |
|------|------|------|
| 마스터 데이터 | ✅ 완료 | CharacterLevelDatabase, CharacterAscensionDatabase |
| 유저 데이터 | ✅ 완료 | OwnedCharacter.CurrentExp 확장 |
| Core | ✅ 완료 | PowerCalculator, CharacterStats |
| Request/Response | ✅ 완료 | LevelUp/Ascension Request/Response |
| 이벤트 | ✅ 완료 | CharacterEvents.cs |
| Handler | ✅ 완료 | CharacterLevelUpHandler, CharacterAscensionHandler |
| UI | ✅ 완료 | LevelUpPopup, AscensionPopup |
| 통합 | ✅ 완료 | CharacterDetailScreen 연동 |
| 테스트 | ✅ 완료 | 26개 (LevelUp 13, Ascension 13) |

---

## 구현 체크리스트

```
캐릭터 강화 (Phase 5.2):

Phase A: 마스터 데이터
- [x] CharacterStats.cs (Data/Structs/)
- [x] LevelRequirement.cs, AscensionRequirement.cs (Data/Structs/)
- [x] CharacterLevelDatabase.cs (Data/ScriptableObjects/)
- [x] CharacterAscensionDatabase.cs (Data/ScriptableObjects/)
- [x] PowerCalculator.cs (Data/Utility/)
- [x] CharacterLevel.json 샘플 데이터
- [x] CharacterAscension.json 샘플 데이터
- [x] ItemData.cs 확장 (ExpValue, GoldCostPerUse)

Phase B: 서버 레이어
- [x] CharacterLevelUpRequest.cs
- [x] CharacterLevelUpResponse.cs
- [x] CharacterAscensionRequest.cs
- [x] CharacterAscensionResponse.cs
- [x] CharacterEvents.cs (LevelUp/Ascension 이벤트)
- [x] CharacterLevelUpHandler.cs (LocalServer/Handlers/)
- [x] CharacterAscensionHandler.cs (LocalServer/Handlers/)

Phase C~D: UI
- [x] CharacterLevelUpPopup.cs - 재료 선택, 스탯 미리보기, 자동 선택
- [x] CharacterAscensionPopup.cs - 요구사항 확인, 스탯 보너스 미리보기

Phase E: 통합
- [x] CharacterDetailScreen.cs 수정 - 레벨업/돌파 버튼, 전투력 표시
- [x] DataManager.cs 수정 - LevelDatabase, AscensionDatabase 참조

Phase F: 테스트
- [x] CharacterLevelUpHandlerTests.cs (13개)
- [x] CharacterAscensionHandlerTests.cs (13개)

미구현 (Phase 2):
- [ ] CharacterListScreen 리팩토링 (필터/정렬)
- [ ] CharacterFilterPopup.cs
- [ ] CharacterCard.cs
- [ ] MaterialSlot.cs 위젯
- [ ] ExpMaterialItem.cs 위젯
```

---

## 관련 문서

- [Character.md](../Character.md) - 캐릭터 시스템 개요
- [Stage.md](../Stage.md) - Phase 3 전투력 공식 정의
- [Common/Popups/CostConfirmPopup.md](../Common/Popups/CostConfirmPopup.md) - Phase 1 CostConfirmPopup
