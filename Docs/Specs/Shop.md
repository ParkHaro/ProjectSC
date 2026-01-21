---
type: spec
assembly: Sc.Contents.Shop
category: System
status: draft
version: "2.0"
dependencies: [Sc.Common, Sc.Packet, Sc.Data, Sc.Event]
created: 2026-01-17
updated: 2026-01-20
---

# Sc.Contents.Shop

## 목적

재화를 소비하여 상품을 구매하고 보상을 획득하는 상점 시스템

## 의존성

### 참조
- `Sc.Common` - UI 시스템, Navigation, TabGroupWidget
- `Sc.Packet` - NetworkManager, Request/Response
- `Sc.Data` - 마스터/유저 데이터
- `Sc.Event` - 이벤트 발행

### 참조됨
- `Sc.Contents.Lobby` - 상점 진입
- `Sc.Contents.Event` - 이벤트 상점 (IShopProvider 통합)

---

## 아키텍처 개요

### Clean Architecture (Provider 패턴)

```
┌─────────────────────────────────────────────────────────────┐
│                        UI Layer                              │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────┐  │
│  │ ShopScreen  │  │ShopProduct  │  │   CostConfirmPopup  │  │
│  │ (Tab구조)   │  │   Item      │  │     (재사용)        │  │
│  └──────┬──────┘  └─────────────┘  └─────────────────────┘  │
│         │                                                    │
│         ▼                                                    │
│  ┌─────────────────────────────────────────────────────┐    │
│  │              IShopProvider                           │    │
│  │   ├─ NormalShopProvider (일반 상점)                  │    │
│  │   └─ EventShopProvider (이벤트 상점)                 │    │
│  └─────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                       Data Layer                             │
│  ┌─────────────────┐  ┌─────────────────┐                   │
│  │ ShopProductData │  │ShopProduct      │                   │
│  │ (SO)            │  │Database (SO)    │                   │
│  └─────────────────┘  └─────────────────┘                   │
│  ┌─────────────────┐  ┌─────────────────┐                   │
│  │ShopPurchase     │  │ UserSaveData    │                   │
│  │Record           │  │ (v4 migration)  │                   │
│  └─────────────────┘  └─────────────────┘                   │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                      Server Layer                            │
│  ┌─────────────────────────────────────────────────────┐    │
│  │                   ShopHandler                        │    │
│  │  ├─ 상품 조회 (ShopProductDatabase)                  │    │
│  │  ├─ 구매 제한 검증 (LimitType 리셋 계산)             │    │
│  │  ├─ 재화 검증/차감 (ServerValidator, RewardService)  │    │
│  │  └─ Delta 생성                                       │    │
│  └─────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────┘
```

### 핵심 설계 결정

| 항목 | 결정 | 이유 |
|------|------|------|
| Tab 구조 | ProductType별 탭 | 상품 분류 명확 |
| 이벤트 상점 | IShopProvider 통합 | 코드 재사용, 확장성 |
| 확인 팝업 | CostConfirmPopup 재사용 | 기존 컴포넌트 활용 |
| 1차 범위 | 최소 기능 (탭 없이 단일 목록) | 빠른 구현 |

---

## 클래스 역할 정의

### 마스터 데이터

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ShopProductType` | 상품 타입 열거형 | 상품 분류 (Currency, Package, Item 등) | - |
| `ShopProductData` | 상품 SO | 개별 상품 정보 저장 | 구매 로직 |
| `ShopProductDatabase` | 상품 DB SO | 상품 목록 관리, 조회 | 구매 로직 |

### 유저 데이터

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ShopPurchaseRecord` | 구매 기록 | 구매 횟수, 리셋 시각 저장 | 리셋 로직 |

### Request/Response

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ShopPurchaseRequest` | 구매 요청 | 상품 ID, 수량 전달 | 검증 |
| `ShopPurchaseResponse` | 구매 응답 | 결과, 보상, Delta 전달 | 데이터 적용 |

### 이벤트

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ProductPurchasedEvent` | 구매 완료 이벤트 | 결과 알림 | 데이터 처리 |
| `ProductPurchaseFailedEvent` | 구매 실패 이벤트 | 실패 알림 | 에러 복구 |

### Provider 인터페이스

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `IShopProvider` | 상점 데이터 제공 인터페이스 | 상품 조회, 구매 가능 여부 | 구매 처리 |
| `NormalShopProvider` | 일반 상점 Provider | 일반 상품 필터링 | 이벤트 상품 |
| `EventShopProvider` | 이벤트 상점 Provider | 이벤트별 상품 필터링 | 일반 상품 |

### UI

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ShopScreen` | 상점 화면 | 탭 관리, 상품 목록 표시, 구매 요청 | 구매 로직 |
| `ShopProductItem` | 상품 아이템 | 개별 상품 표시 (일반/이벤트 공용) | 구매 처리 |
| `ShopState` | 화면 상태 | Provider, 초기 탭 저장 | - |

---

## 데이터 흐름

### 구매 플로우

```
[ShopScreen] User clicks product
    │
    ├─> CostConfirmPopup.Open(state)
    │       User confirms
    │
    └─> ShopPurchaseRequest → NetworkManager
            │
            ▼
        [LocalGameServer]
            ShopHandler.Handle()
            ├─ 1. 상품 조회
            ├─ 2. 구매 제한 검증
            ├─ 3. 재화 검증
            ├─ 4. 재화 차감
            ├─ 5. 보상 지급 (RewardService)
            ├─ 6. 구매 기록 업데이트
            └─ 7. Delta 생성
            │
            ▼
        ShopPurchaseResponse
            │
            ▼
        [DataManager]
            ApplyDelta(response.Delta)
            │
            ▼
        [EventManager]
            ProductPurchasedEvent
            │
            ├─> [ShopScreen] RefreshProductList()
            └─> [RewardPopup] Display rewards
```

### Provider 추상화 흐름

```
[ShopScreen]
    │
    ├─ Normal Shop: NormalShopProvider
    │   └─ GetProducts(type) → Filter EventShop 제외
    │
    └─ Event Shop: EventShopProvider(eventId)
        └─ GetProducts(EventShop) → Filter by eventId
```

---

## 설계 원칙

1. **서버 중심 구매**
   - 모든 구매는 서버(LocalGameServer) 검증 후 처리
   - 클라이언트는 요청만, 실제 처리는 서버

2. **Delta 패턴**
   - 구매 결과는 UserDataDelta로 전달
   - DataManager.ApplyDelta()로 일괄 적용

3. **제한 관리**
   - 일일/주간/월간 제한은 서버에서 리셋 계산
   - 클라이언트는 표시만 담당

4. **Provider 패턴**
   - IShopProvider로 일반/이벤트 상점 추상화
   - 동일한 ShopProductItem으로 모든 상점 표시

5. **탭 기반 분류**
   - ProductType별 탭 분리 (TabGroupWidget 재사용)
   - 확장 가능한 탭 구조

---

## 상태

| 분류 | 상태 | 비고 |
|------|------|------|
| 아키텍처 설계 | ✅ 완료 | |
| 마스터 데이터 | ✅ 완료 | ShopProductType, ShopProductData, ShopProductDatabase |
| 유저 데이터 | ✅ 완료 | ShopPurchaseRecord |
| Request/Response | ✅ 완료 | ShopPurchaseRequest/Response |
| 이벤트 | ✅ 완료 | ShopEvents.cs |
| ShopHandler | ✅ 완료 | LocalServer/Handlers/ |
| PurchaseLimitValidator | ✅ 완료 | LocalServer/Services/ |
| Provider | ✅ 완료 | IShopProvider, Normal/EventShopProvider |
| UI | 🔨 진행 중 | ShopState.cs 플레이스홀더 |
| 테스트 (Server) | ✅ 완료 | ShopHandlerTests, PurchaseLimitValidatorTests |
| 테스트 (Provider) | ⬜ 대기 | |

---

## 구현 체크리스트

```
Phase A: Data Foundation
- [x] ShopProductType.cs (Data/Enums/)
- [x] ShopProductData.cs (Data/ScriptableObjects/)
- [x] ShopProductDatabase.cs (Data/ScriptableObjects/)
- [x] ShopPurchaseRecord.cs (Data/Structs/UserData/)
- [x] UserSaveData.cs v4 마이그레이션 (ShopPurchaseHistory 필드)

Phase B: Events
- [x] ShopEvents.cs (Event/OutGame/)
  - [x] ProductPurchasedEvent
  - [x] ProductPurchaseFailedEvent

Phase C: Server Logic
- [x] PurchaseLimitValidator.cs (LocalServer/Services/)
- [x] ShopHandler.cs 확장 (LocalServer/Handlers/)
  - [x] 상품 조회
  - [x] 재화 검증
  - [x] 구매 제한 검증
  - [x] 재화 차감
  - [x] 보상 지급 (RewardService)
  - [x] 구매 기록 저장
  - [x] Delta 생성
- [x] LocalGameServer.cs에 ShopProductDatabase 주입

Phase D: UI Assembly (Sc.Contents.Shop)
- [x] Sc.Contents.Shop.asmdef
- [x] IShopProvider.cs (인터페이스)
- [x] NormalShopProvider.cs
- [x] EventShopProvider.cs
- [ ] ShopState.cs (플레이스홀더)
- [x] ShopScreen.cs
- [x] ShopProductItem.cs

Phase E: Integration
- [ ] LobbyScreen.cs에 [상점] 버튼 추가
- [ ] EventShopTab.cs 실제 구현 (Provider 연동)

Phase F: Testing
- [x] ShopHandlerTests.cs (24개)
- [x] PurchaseLimitValidatorTests.cs (16개)
- [ ] NormalShopProviderTests.cs
- [ ] EventShopProviderTests.cs
```

---

## 파일 구조

```
Assets/Scripts/
├── Data/
│   ├── Enums/
│   │   └── ShopProductType.cs          # NEW
│   ├── ScriptableObjects/
│   │   ├── ShopProductData.cs          # NEW
│   │   └── ShopProductDatabase.cs      # NEW
│   └── Structs/UserData/
│       └── ShopPurchaseRecord.cs       # NEW
│
├── Event/OutGame/
│   └── ShopEvents.cs                   # NEW
│
├── LocalServer/
│   ├── Handlers/
│   │   └── ShopHandler.cs              # MODIFY
│   ├── Services/
│   │   └── PurchaseLimitValidator.cs   # NEW
│   └── LocalGameServer.cs              # MODIFY
│
├── Contents/OutGame/
│   ├── Shop/                           # NEW FOLDER
│   │   ├── Sc.Contents.Shop.asmdef
│   │   ├── IShopProvider.cs
│   │   ├── NormalShopProvider.cs
│   │   ├── EventShopProvider.cs
│   │   ├── ShopState.cs
│   │   ├── ShopScreen.cs
│   │   └── ShopProductItem.cs
│   ├── Lobby/
│   │   └── LobbyScreen.cs              # MODIFY
│   └── Event/
│       └── EventShopTab.cs             # MODIFY
│
└── Editor/Tests/
    ├── LocalServer/
    │   ├── ShopHandlerTests.cs         # NEW
    │   └── PurchaseLimitValidatorTests.cs # NEW
    └── Data/
        └── ShopProviderTests.cs        # NEW
```

**총 16개 파일 생성, 5개 파일 수정**

---

## 에러 코드

| 코드 | 설명 |
|------|------|
| `SHOP_PRODUCT_NOT_FOUND` | 상품 없음 |
| `SHOP_INSUFFICIENT_CURRENCY` | 재화 부족 |
| `SHOP_LIMIT_EXCEEDED` | 구매 제한 초과 |
| `SHOP_PRODUCT_EXPIRED` | 상품 기간 만료 (Phase 2) |
| `SHOP_INVALID_AMOUNT` | 잘못된 구매 수량 (Phase 2) |

---

## 확장 포인트 (Phase 2)

### 기간 한정 상품
```csharp
// ShopProductData
public long StartAt;  // Unix timestamp
public long EndAt;    // Unix timestamp
```

### 다중 구매
```csharp
// ShopPurchaseRequest
public int Amount;  // 다중 구매 수량
```

### 할인 상품
```csharp
// ShopProductData
public int OriginalPrice;
public int DiscountPercent;
```

---

## 관련 문서

- [Data.md](Data.md) - 데이터 구조 개요
- [Packet.md](Packet.md) - 네트워크 패턴
- [Common/Reward.md](Common/Reward.md) - 보상 시스템
- [Common/Popups/ConfirmPopup.md](Common/Popups/ConfirmPopup.md) - 확인 팝업
- [Common/Popups/RewardPopup.md](Common/Popups/RewardPopup.md) - 보상 팝업
- [LiveEvent.md](LiveEvent.md) - 이벤트 시스템 (EventShopTab 참조)