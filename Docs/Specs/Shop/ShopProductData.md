---
type: spec
assembly: Sc.Data
class: ShopProductData
category: SO
status: draft
version: "1.0"
dependencies: [ProductType, LimitType, CostType, RewardInfo]
created: 2026-01-17
updated: 2026-01-17
---

# ShopProductData

## 역할

상점 상품 정보를 저장하는 ScriptableObject

## 책임

- 상품 정보 저장 (이름, 가격, 보상 등)
- 구매 제한 정보 저장
- 표시 정보 저장 (아이콘, 정렬 순서)

## 비책임

- 구매 로직 처리
- 구매 제한 검증
- UI 표시

---

## 인터페이스

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| `Id` | string | 상품 고유 ID |
| `Name` | string | 상품 이름 |
| `Description` | string | 상품 설명 |
| `ProductType` | ProductType | 상품 타입 |
| `CostType` | CostType | 소비 재화 타입 |
| `Price` | int | 가격 |
| `Rewards` | RewardInfo[] | 지급 보상 목록 |
| `LimitType` | LimitType | 구매 제한 타입 |
| `LimitCount` | int | 제한 횟수 (0 = 무제한) |
| `DisplayOrder` | int | 표시 순서 |
| `IconPath` | string | 아이콘 경로 |
| `IsHot` | bool | HOT 표시 여부 |
| `IsNew` | bool | NEW 표시 여부 |
| `StartAt` | long | 판매 시작 시각 (0 = 항상) |
| `EndAt` | long | 판매 종료 시각 (0 = 항상) |

---

## Enum 정의

### ProductType

**위치**: `Assets/Scripts/Data/Enums/ProductType.cs`

```csharp
public enum ProductType
{
    Currency,       // 재화 상품 (Gem → Gold)
    Package,        // 패키지 (여러 아이템 묶음)
    Pass,           // 패스 (시즌패스 등)
    CharacterPiece, // 캐릭터 조각
    Item,           // 단일 아이템
    Stamina,        // 스태미나 충전
    EventShop,      // 이벤트 상점 전용
}
```

### LimitType

**위치**: `Assets/Scripts/Data/Enums/LimitType.cs`

```csharp
public enum LimitType
{
    None,           // 무제한
    Daily,          // 일일 제한 (00:00 UTC 리셋)
    Weekly,         // 주간 제한 (월요일 00:00 UTC 리셋)
    Monthly,        // 월간 제한 (1일 00:00 UTC 리셋)
    Permanent,      // 영구 제한 (1회만)
    Event,          // 이벤트 기간 동안 제한
}
```

---

## 클래스 정의

### ShopProductData.cs

**위치**: `Assets/Scripts/Data/ScriptableObjects/ShopProductData.cs`

```csharp
[CreateAssetMenu(fileName = "ShopProduct_", menuName = "SC/Data/Shop Product")]
public class ShopProductData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public string Name;
    [TextArea] public string Description;
    public ProductType ProductType;

    [Header("가격")]
    public CostType CostType;
    public int Price;

    [Header("보상")]
    public RewardInfo[] Rewards;

    [Header("구매 제한")]
    public LimitType LimitType;
    public int LimitCount;

    [Header("표시")]
    public int DisplayOrder;
    public string IconPath;
    public bool IsHot;
    public bool IsNew;

    [Header("판매 기간")]
    public long StartAt;
    public long EndAt;
}
```

### ShopProductDatabase.cs

**위치**: `Assets/Scripts/Data/ScriptableObjects/ShopProductDatabase.cs`

```csharp
[CreateAssetMenu(fileName = "ShopProductDatabase", menuName = "SC/Database/Shop Product")]
public class ShopProductDatabase : ScriptableObject
{
    [SerializeField] private List<ShopProductData> _products;

    private Dictionary<string, ShopProductData> _lookup;

    public void Initialize()
    {
        _lookup = _products.ToDictionary(p => p.Id);
    }

    public ShopProductData Get(string id)
    {
        if (_lookup == null) Initialize();
        return _lookup.TryGetValue(id, out var data) ? data : null;
    }

    public IReadOnlyList<ShopProductData> GetByType(ProductType type)
    {
        return _products.Where(p => p.ProductType == type)
                        .OrderBy(p => p.DisplayOrder)
                        .ToList();
    }

    public IReadOnlyList<ShopProductData> GetActive(long currentTime)
    {
        return _products.Where(p => IsActive(p, currentTime))
                        .OrderBy(p => p.DisplayOrder)
                        .ToList();
    }

    private bool IsActive(ShopProductData product, long currentTime)
    {
        if (product.StartAt > 0 && currentTime < product.StartAt) return false;
        if (product.EndAt > 0 && currentTime > product.EndAt) return false;
        return true;
    }
}
```

---

## JSON 샘플

**위치**: `Assets/Data/MasterData/ShopProduct.json`

```json
[
  {
    "Id": "shop_gold_1",
    "Name": "골드 주머니",
    "Description": "골드 1,000을 획득합니다.",
    "ProductType": "Currency",
    "CostType": "Gem",
    "Price": 50,
    "Rewards": [
      { "Type": "Currency", "ItemId": "Gold", "Amount": 1000 }
    ],
    "LimitType": "Daily",
    "LimitCount": 5,
    "DisplayOrder": 1,
    "IconPath": "Icons/Shop/gold_pack_1",
    "IsHot": false,
    "IsNew": false,
    "StartAt": 0,
    "EndAt": 0
  },
  {
    "Id": "shop_gold_2",
    "Name": "골드 상자",
    "Description": "골드 5,000을 획득합니다.",
    "ProductType": "Currency",
    "CostType": "Gem",
    "Price": 200,
    "Rewards": [
      { "Type": "Currency", "ItemId": "Gold", "Amount": 5000 }
    ],
    "LimitType": "Daily",
    "LimitCount": 3,
    "DisplayOrder": 2,
    "IconPath": "Icons/Shop/gold_pack_2",
    "IsHot": true,
    "IsNew": false,
    "StartAt": 0,
    "EndAt": 0
  },
  {
    "Id": "shop_stamina_1",
    "Name": "스태미나 충전",
    "Description": "스태미나 60을 회복합니다.",
    "ProductType": "Stamina",
    "CostType": "Gem",
    "Price": 30,
    "Rewards": [
      { "Type": "Currency", "ItemId": "Stamina", "Amount": 60 }
    ],
    "LimitType": "Daily",
    "LimitCount": 10,
    "DisplayOrder": 1,
    "IconPath": "Icons/Shop/stamina",
    "IsHot": false,
    "IsNew": false,
    "StartAt": 0,
    "EndAt": 0
  },
  {
    "Id": "shop_package_beginner",
    "Name": "초보자 패키지",
    "Description": "초보 모험가를 위한 특별 패키지!",
    "ProductType": "Package",
    "CostType": "Gem",
    "Price": 500,
    "Rewards": [
      { "Type": "Currency", "ItemId": "Gold", "Amount": 10000 },
      { "Type": "Currency", "ItemId": "Gem", "Amount": 100 },
      { "Type": "Item", "ItemId": "item_exp_001", "Amount": 10 },
      { "Type": "Item", "ItemId": "item_equip_sword_001", "Amount": 1 }
    ],
    "LimitType": "Permanent",
    "LimitCount": 1,
    "DisplayOrder": 1,
    "IconPath": "Icons/Shop/package_beginner",
    "IsHot": true,
    "IsNew": true,
    "StartAt": 0,
    "EndAt": 0
  },
  {
    "Id": "shop_char_piece_001",
    "Name": "아리아 조각 x10",
    "Description": "아리아 캐릭터 조각 10개",
    "ProductType": "CharacterPiece",
    "CostType": "Gold",
    "Price": 5000,
    "Rewards": [
      { "Type": "Item", "ItemId": "piece_char_001", "Amount": 10 }
    ],
    "LimitType": "Monthly",
    "LimitCount": 5,
    "DisplayOrder": 1,
    "IconPath": "Icons/Shop/piece_aria",
    "IsHot": false,
    "IsNew": false,
    "StartAt": 0,
    "EndAt": 0
  }
]
```

---

## 탭 구성 예시

| 탭 | ProductType | 설명 |
|----|-------------|------|
| 재화 | Currency, Stamina | 골드, 스태미나 구매 |
| 패키지 | Package | 복합 아이템 묶음 |
| 캐릭터 | CharacterPiece | 캐릭터 조각 |
| 아이템 | Item | 단일 아이템 |

---

## 주의사항

1. **ID 유니크성**
   - Id는 전체 상점에서 유일해야 함
   - 접두사 권장: `shop_{type}_{순번}`

2. **RewardInfo 타입 매칭**
   - Currency 보상: ItemId는 CostType.ToString() 값
   - Item 보상: ItemId는 실제 ItemData.Id 값
   - Character 보상: ItemId는 CharacterData.Id 값

3. **가격 정책**
   - Price는 항상 양수
   - 무료 상품도 Price = 0으로 설정

4. **제한 리셋 시각**
   - Daily: 매일 00:00 UTC
   - Weekly: 매주 월요일 00:00 UTC
   - Monthly: 매월 1일 00:00 UTC
   - 서버에서 리셋 시각 계산

---

## 관련 문서

- [Shop.md](../Shop.md) - 상점 시스템 개요
- [Reward.md](../Common/Reward.md) - 보상 시스템
- [Data.md](../Data.md) - 데이터 구조 개요
