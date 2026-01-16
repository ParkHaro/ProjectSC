---
type: spec
assembly: Sc.Contents.Shop
category: System
status: draft
version: "1.0"
dependencies: [Sc.Common, Sc.Packet, Sc.Data, Sc.Event]
created: 2026-01-17
updated: 2026-01-17
---

# Sc.Contents.Shop

## 목적

재화를 소비하여 상품을 구매하고 보상을 획득하는 상점 시스템

## 의존성

### 참조
- `Sc.Common` - UI 시스템, Navigation
- `Sc.Packet` - NetworkManager, Request/Response
- `Sc.Data` - 마스터/유저 데이터
- `Sc.Event` - 이벤트 발행

### 참조됨
- `Sc.Contents.Lobby` - 상점 진입
- `Sc.Contents.Event` - 이벤트 상점 (재사용)

---

## 클래스 역할 정의

### 마스터 데이터

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ProductType` | 상품 타입 열거형 | 상품 분류 | - |
| `LimitType` | 제한 타입 열거형 | 구매 제한 분류 | - |
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
| `PurchaseFailedEvent` | 구매 실패 이벤트 | 실패 알림 | 에러 복구 |

### UI

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ShopScreen` | 상점 화면 | 상품 목록 표시, 구매 요청 | 구매 로직 |
| `ShopProductItem` | 상품 아이템 | 개별 상품 표시 | 구매 처리 |
| `PurchaseConfirmPopup` | 구매 확인 팝업 | 최종 확인 | 데이터 처리 |

---

## 관계도

```
┌─────────────────────────────────────────────────────────┐
│                     ShopScreen                           │
│  ┌─────────────────────────────────────────────────┐    │
│  │              ShopProductItem[]                   │    │
│  │  ┌─────────┐ ┌─────────┐ ┌─────────┐            │    │
│  │  │ Product │ │ Product │ │ Product │ ...        │    │
│  │  └────┬────┘ └────┬────┘ └────┬────┘            │    │
│  └───────┼──────────┼──────────┼───────────────────┘    │
└──────────┼──────────┼──────────┼────────────────────────┘
           │          │          │
           └──────────┼──────────┘
                      │ Click
                      ▼
           ┌────────────────────┐
           │ PurchaseConfirmPopup│
           │  "구매하시겠습니까?" │
           └──────────┬─────────┘
                      │ Confirm
                      ▼
           ┌────────────────────┐
           │  NetworkManager    │
           │ ShopPurchaseRequest│
           └──────────┬─────────┘
                      │
                      ▼
           ┌────────────────────┐
           │  LocalApiClient    │
           │   PurchaseAsync    │
           └──────────┬─────────┘
                      │
                      ▼
           ┌────────────────────┐
           │  DataManager       │
           │   ApplyDelta       │
           └──────────┬─────────┘
                      │
                      ▼
           ┌────────────────────┐
           │   RewardPopup      │
           │  획득 보상 표시     │
           └────────────────────┘
```

---

## 설계 원칙

1. **서버 중심 구매**
   - 모든 구매는 서버(LocalApiClient) 검증 후 처리
   - 클라이언트는 요청만, 실제 처리는 서버

2. **Delta 패턴**
   - 구매 결과는 UserDataDelta로 전달
   - DataManager.ApplyDelta()로 일괄 적용

3. **제한 관리**
   - 일일/주간/월간 제한은 서버에서 리셋
   - 클라이언트는 표시만 담당

4. **탭 기반 분류**
   - ProductType별 탭 분리
   - 확장 가능한 탭 구조

---

## 상세 문서

### 마스터 데이터
- [ShopProductData.md](Shop/ShopProductData.md)

### UI
- [ShopScreen.md](Shop/ShopScreen.md)

---

## 상태

| 분류 | 상태 |
|------|------|
| 마스터 데이터 | ⬜ 대기 |
| 유저 데이터 | ⬜ 대기 |
| Request/Response | ⬜ 대기 |
| 이벤트 | ⬜ 대기 |
| LocalApiClient | ⬜ 대기 |
| UI | ⬜ 대기 |
| 테스트 | ⬜ 대기 |

---

## 구현 체크리스트

```
Phase 2: 상점 구현

마스터 데이터:
- [ ] ProductType.cs (Data/Enums/)
- [ ] LimitType.cs (Data/Enums/)
- [ ] ShopProductData.cs (Data/ScriptableObjects/)
- [ ] ShopProductDatabase.cs (Data/ScriptableObjects/)
- [ ] ShopProduct.json (Data/MasterData/)
- [ ] MasterDataImporter에 ShopProduct 추가

유저 데이터:
- [ ] ShopPurchaseRecord 구조체 (Data/Structs/UserData/)
- [ ] UserSaveData.ShopPurchaseHistory 필드 추가
- [ ] UserSaveData Version 업그레이드 (v3)

Request/Response:
- [ ] ShopPurchaseRequest.cs 확장 (Packet/Requests/)
- [ ] ShopPurchaseResponse.cs 확장 (Packet/Responses/)

이벤트:
- [ ] ShopEvents.cs (Event/OutGame/)
  - [ ] ProductPurchasedEvent
  - [ ] PurchaseFailedEvent

API:
- [ ] LocalApiClient.PurchaseAsync 구현
  - [ ] 상품 조회
  - [ ] 재화 검증
  - [ ] 구매 제한 검증
  - [ ] 재화 차감
  - [ ] 보상 지급 (RewardProcessor)
  - [ ] 구매 기록 저장
  - [ ] Delta 생성

UI:
- [ ] Sc.Contents.Shop Assembly 생성
- [ ] ShopScreen.cs
- [ ] ShopProductItem.cs
- [ ] PurchaseConfirmPopup.cs
- [ ] MVPSceneSetup에 Shop 프리팹 추가

연동:
- [ ] LobbyScreen에 [상점] 버튼 추가
- [ ] NetworkManager 연동 테스트
- [ ] DataManager.ApplyDelta 확인
- [ ] CurrencyHUD 갱신 확인

검증 시나리오:
- [ ] 일반 구매 성공
- [ ] 재화 부족 실패
- [ ] 일일 제한 초과 실패
- [ ] 구매 후 재화 차감 확인
- [ ] 구매 후 아이템 지급 확인
```

---

## 에러 코드

| 코드 | 설명 |
|------|------|
| `SHOP_PRODUCT_NOT_FOUND` | 상품 없음 |
| `SHOP_INSUFFICIENT_CURRENCY` | 재화 부족 |
| `SHOP_LIMIT_EXCEEDED` | 구매 제한 초과 |
| `SHOP_PRODUCT_EXPIRED` | 상품 기간 만료 |
| `SHOP_INVALID_AMOUNT` | 잘못된 구매 수량 |

---

## 관련 문서

- [Data.md](Data.md) - 데이터 구조 개요
- [Packet.md](Packet.md) - 네트워크 패턴
- [Common/Reward.md](Common/Reward.md) - 보상 시스템
- [Common/Popups/ConfirmPopup.md](Common/Popups/ConfirmPopup.md) - 확인 팝업
- [Common/Popups/RewardPopup.md](Common/Popups/RewardPopup.md) - 보상 팝업
