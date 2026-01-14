---
type: spec
assembly: Sc.Event
class: MenuSelectedEvent, GachaResultEvent, QuestCompleteEvent, RewardClaimEvent, ItemPurchasedEvent
category: Event
status: draft
version: "1.0"
dependencies: [RewardData, CharacterRarity]
created: 2025-01-14
updated: 2025-01-14
---

# Event - OutGame Events

## 역할
전투 외 발생하는 이벤트. 메뉴, 가챠, 퀘스트, 상점 관련.

---

## MenuSelectedEvent

메뉴 선택 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| MenuType | MenuType | 선택된 메뉴 |

**MenuType**: Battle, Gacha, Character, Inventory, Shop, Quest, Settings

**발행**: UI → **구독**: LobbyManager

---

## GachaResultEvent

가챠 결과 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| PoolId | string | 가챠 풀 ID |
| Results | GachaResultItem[] | 결과 목록 |
| PityCount | int | 현재 천장 카운트 |

### GachaResultItem

| 필드 | 타입 | 설명 |
|------|------|------|
| CharacterId | string | 캐릭터 ID |
| Rarity | CharacterRarity | 등급 |
| IsNew | bool | 신규 여부 |
| IsPickup | bool | 픽업 여부 |

**발행**: GachaManager → **구독**: UI (연출), Inventory (캐릭터 추가)

### 흐름
```
[가챠 실행]
     ↓
GachaResultEvent
     ├→ UI: 연출 재생
     └→ Inventory: 캐릭터 추가
```

---

## QuestCompleteEvent

퀘스트 완료 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| QuestId | string | 퀘스트 ID |
| QuestType | QuestType | 종류 |
| Rewards | RewardData[] | 보상 목록 |

**QuestType**: Daily, Weekly, Achievement, Story, Event

**발행**: QuestManager → **구독**: UI (알림)

---

## RewardClaimEvent

보상 수령 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| Source | RewardSource | 보상 출처 |
| Rewards | RewardData[] | 보상 목록 |

**RewardSource**: Battle, Quest, Mail, Event, Shop

**발행**: UI → **구독**: Inventory (아이템 추가)

### 흐름
```
QuestCompleteEvent
      ↓
[사용자 수령 버튼]
      ↓
RewardClaimEvent
      └→ Inventory: 아이템/재화 추가
```

---

## ItemPurchasedEvent

아이템 구매 알림.

| 필드 | 타입 | 설명 |
|------|------|------|
| ItemId | string | 아이템 ID |
| Amount | int | 구매 수량 |
| CurrencyType | CurrencyType | 사용 재화 |
| Price | int | 지불 금액 |

**CurrencyType**: Gold, Gem, EventCurrency

**발행**: ShopManager → **구독**: Inventory (아이템 추가, 재화 차감)

---

## 설계 원칙

1. **결과 전달**: 처리 완료 후 결과만 알림
2. **출처 명시**: Source/Type으로 발생 맥락 전달
3. **UI 분리**: 이벤트 발행자는 UI 몰라야 함

## 관련
- [Event.md](../Event.md)
- [Data/Structs.md](../Data/Structs.md)
