---
type: spec
assembly: Sc.Data, Sc.Core
category: Utility
status: draft
version: "2.0"
dependencies: [UserDataDelta, CostType, DataManager]
created: 2026-01-17
updated: 2026-01-18
---

# 보상 시스템 (Reward System)

## 목적

게임 내 모든 보상 지급을 통합 처리하는 공통 모듈

## 필요성

| 기능 | 보상 발생 시점 |
|------|----------------|
| 가챠 | 소환 결과 지급 |
| 상점 | 구매 보상 지급 |
| 스테이지 | 클리어 보상, 초회 클리어 보상 |
| 이벤트 | 미션 완료 보상 |
| 퀘스트 | 퀘스트 완료 보상 |
| 출석 | 출석 보상 |

→ **동일한 보상 처리 로직이 반복됨** → 공통 모듈 필요

---

## 클래스 역할 정의

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `RewardType` | 보상 타입 열거형 | 보상 종류 분류 | - |
| `ItemCategory` | 아이템 세부 분류 | 아이템 종류 분류 | - |
| `RewardInfo` | 보상 정보 구조체 | 보상 데이터 전달 | 보상 적용 |
| `RewardProcessor` | 보상 처리 유틸리티 | Delta 생성, 검증 | UI 표시 |
| `RewardHelper` | UI 헬퍼 유틸리티 | 포맷팅, 아이콘, 색상 | 보상 적용 |

---

## 상세 정의

### RewardType

**위치**: `Assets/Scripts/Data/Enums/RewardType.cs`

```csharp
public enum RewardType
{
    Currency,       // 재화 (골드, 젬, 스태미나, 이벤트 코인)
    Item,           // 모든 인벤토리 아이템 (ItemCategory로 세분화)
    Character,      // 캐릭터 획득
    PlayerExp,      // 플레이어 경험치
}
```

### ItemCategory

**위치**: `Assets/Scripts/Data/Enums/ItemCategory.cs`

```csharp
public enum ItemCategory
{
    Equipment,      // 장비 (장착 전, 수량 기반)
    Consumable,     // 소모품 (경험치 아이템, 버프 등)
    Material,       // 재료 (강화, 진화, 돌파)
    CharacterShard, // 캐릭터 조각
    Furniture,      // 가구
    Ticket,         // 소환 티켓
}
```

### RewardInfo

**위치**: `Assets/Scripts/Data/Structs/Common/RewardInfo.cs`

```csharp
[Serializable]
public struct RewardInfo
{
    /// <summary>보상 타입</summary>
    public RewardType Type;

    /// <summary>
    /// 아이템/캐릭터 ID 또는 CostType 문자열
    /// - Currency: "Gold", "Gem", "Stamina" 등 (CostType.ToString())
    /// - Character: "char_001"
    /// - Item: "item_001"
    /// - PlayerExp: "" (빈 문자열, Amount만 사용)
    /// </summary>
    public string ItemId;

    /// <summary>수량</summary>
    public int Amount;
}
```

### RewardProcessor

**위치**: `Assets/Scripts/Core/Utility/RewardProcessor.cs`

**역할**: 보상 정보를 UserDataDelta로 변환하는 정적 유틸리티 (서버 로직)

### RewardHelper

**위치**: `Assets/Scripts/Core/Utility/RewardHelper.cs`

**역할**: UI 표시를 위한 클라이언트 헬퍼

---

## 인터페이스

### RewardProcessor (서버/Delta 생성)

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| `CreateDelta` | `UserDataDelta CreateDelta(RewardInfo[] rewards)` | 보상 → Delta 변환 |
| `ValidateRewards` | `bool ValidateRewards(RewardInfo[] rewards)` | 보상 유효성 검증 |
| `CanApplyRewards` | `bool CanApplyRewards(RewardInfo[] rewards)` | 지급 가능 여부 (인벤토리 등) |

### RewardHelper (클라이언트 UI)

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| `FormatText` | `string FormatText(RewardInfo reward)` | UI 표시용 텍스트 |
| `GetIconPath` | `string GetIconPath(RewardInfo reward)` | 아이콘 경로 |
| `GetRarityColor` | `Color GetRarityColor(RewardInfo reward)` | 희귀도 색상 |

---

## 동작 흐름

### 보상 지급 흐름

```
[서버/LocalApiClient]
        │
        │ 1. 보상 계산
        ▼
┌──────────────────┐
│ RewardProcessor  │
│ .CreateDelta()   │
└────────┬─────────┘
         │ 2. Delta 생성
         ▼
┌──────────────────┐
│  UserDataDelta   │
└────────┬─────────┘
         │ 3. Response에 포함
         ▼
┌──────────────────┐
│   DataManager    │
│  .ApplyDelta()   │
└────────┬─────────┘
         │ 4. 유저 데이터 갱신
         ▼
┌──────────────────┐
│ UserDataChanged  │
│     Event        │
└──────────────────┘
```

### CreateDelta 내부 로직

```
RewardInfo[] rewards
        │
        ▼
┌─────────────────────────────────┐
│ foreach (reward in rewards)     │
│   switch (reward.Type)          │
│     Currency → delta.Currency   │
│     Item → delta.AddedItems     │
│     Character → delta.AddedCharacters │
│     PlayerExp → delta.Profile.Exp │
└─────────────────────────────────┘
        │
        ▼
    UserDataDelta
```

---

## 사용 패턴

### LocalApiClient에서 사용
```csharp
// 가챠 보상 처리 예시
var rewards = CalculateGachaRewards(request);
var delta = RewardProcessor.CreateDelta(rewards);
response.Delta = delta;
response.Rewards = rewards;
```

### Response에서 Delta 적용
```csharp
// NetworkManager 응답 처리
DataManager.Instance.ApplyDelta(response.Delta);
EventManager.Instance.Publish(new GachaCompletedEvent { Rewards = response.Rewards });
```

### UI에서 보상 표시
```csharp
// RewardPopup에서 사용
foreach (var reward in rewards)
{
    var text = RewardHelper.FormatText(reward);
    var iconPath = RewardHelper.GetIconPath(reward);
    CreateRewardItem(reward, text, iconPath);
}
```

---

## Currency 매핑

| RewardInfo.ItemId | CostType | 설명 |
|-------------------|----------|------|
| `"Gold"` | CostType.Gold | 골드 |
| `"Gem"` | CostType.Gem | 젬 |
| `"Stamina"` | CostType.Stamina | 스태미나 |
| `"SummonTicket"` | CostType.SummonTicket | 소환권 |
| `"CharacterExp"` | CostType.CharacterExp | 캐릭터 경험치 재료 |
| `"ArenaCoin"` | CostType.ArenaCoin | 아레나 코인 |
| ... | ... | (CostType 전체 매핑) |

**변환 로직**:
```csharp
// ItemId → CostType
if (Enum.TryParse<CostType>(reward.ItemId, out var costType))
{
    // delta.Currency에 해당 재화 증가
}
```

---

## JSON 예시

### 마스터 데이터 내 보상 정의
```json
{
  "Rewards": [
    { "Type": "Currency", "ItemId": "Gold", "Amount": 1000 },
    { "Type": "Currency", "ItemId": "Gem", "Amount": 50 },
    { "Type": "Item", "ItemId": "item_exp_001", "Amount": 5 },
    { "Type": "Character", "ItemId": "char_001", "Amount": 1 },
    { "Type": "PlayerExp", "ItemId": "", "Amount": 100 }
  ]
}
```

---

## 주의사항

1. **RewardProcessor와 RewardHelper 분리**
   - RewardProcessor: 서버 로직 (Delta 생성, 검증)
   - RewardHelper: 클라이언트 UI (포맷팅, 아이콘)
   - 둘 다 Stateless (모든 메서드 static)

2. **Delta 병합 주의**
   - 여러 RewardInfo[]를 순차 처리 시 Delta 병합 필요
   - `UserDataDelta.Merge()` 사용

3. **Character 보상 특수 처리**
   - 캐릭터는 OwnedCharacter로 변환 시 InstanceId 생성 필요
   - LocalApiClient에서 처리

4. **PlayerExp 특수 처리**
   - ItemId는 빈 문자열 사용
   - Amount만으로 경험치 증가량 결정

5. **Amount 음수 불가**
   - 보상은 항상 양수
   - 차감은 별도 로직 (Cost 처리)

---

## 관련 문서

- [Data.md](../Data.md) - 데이터 구조 개요
- [UserDataDelta.md](../Packet/UserDataDelta.md) - Delta 패턴
- [CostType](../Data/Enums.md#costtype) - 재화 타입 열거형
