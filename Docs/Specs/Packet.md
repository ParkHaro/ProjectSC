---
type: overview
assembly: Sc.Packet
category: Network
status: approved
version: "2.0"
dependencies: [Sc.Data, Sc.Foundation, UniTask]
detail_docs: [IApiClient, Requests, Responses, LocalApiClient, NetworkManager]
created: 2025-01-14
updated: 2025-01-15
---

# Sc.Packet

## 목적
서버 통신 인터페이스 계층. Request/Response + Delta 패턴으로 데이터 교환. 로컬/네트워크 구현 교체 가능.

## 의존성
- **참조**: Sc.Data (요청/응답 데이터), Sc.Foundation (EventManager), UniTask (비동기)
- **참조됨**: Sc.Core (DataManager), Contents (Gacha, Shop 등)

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **IApiClient** | API 통신 인터페이스 |
| **Request** | 서버 요청 클래스 (IRequest\<TResponse\>) |
| **Response** | 서버 응답 클래스 (IResponse, IGameActionResponse) |
| **UserDataDelta** | 유저 데이터 변경분 (부분 갱신) |
| **NetworkManager** | 네트워크 요청 오케스트레이션 |

---

## Packet vs Event 구분

| 구분 | Packet | Event |
|------|--------|-------|
| **목적** | 서버와 데이터 교환 | 클라이언트 내부 알림 |
| **방향** | Request → Response | 단방향 Publish |
| **응답** | 있음 (비동기, Delta 포함) | 없음 |
| **예시** | GachaRequest → GachaResponse | GachaResultEvent |
| **교체** | LocalApiClient ↔ ServerApiClient | 변경 없음 |

---

## 클래스 역할 정의

### Interfaces

| 클래스 | 역할 | 주요 메서드 |
|--------|------|-------------|
| IRequest | 요청 기본 인터페이스 | Timestamp |
| IRequest\<TResponse\> | 타입 안전 요청 | (IRequest 상속) |
| IResponse | 응답 기본 인터페이스 | IsSuccess, ErrorCode, ServerTime |
| IGameActionResponse | 게임 액션 응답 | Delta (UserDataDelta) |
| IApiClient | API 클라이언트 계약 | InitializeAsync, SendAsync |

### Services

| 클래스 | 역할 | 책임 |
|--------|------|------|
| LocalApiClient | 로컬 구현 | 서버 응답 시뮬레이션, JSON 저장 |
| NetworkManager | 요청 오케스트레이션 | RequestQueue, 콜백 핸들러 |
| RequestQueue | 요청 큐 관리 | 순차 처리, 동시성 제어 |
| PacketDispatcher | 응답 분배 | 타입별 핸들러 호출 |

### Requests

| 클래스 | 역할 | 주요 필드 |
|--------|------|-----------|
| LoginRequest | 로그인 요청 | DeviceId, UserId, Platform |
| GachaRequest | 가챠 요청 | GachaPoolId, PullType (Single/Multi) |
| ShopPurchaseRequest | 구매 요청 | ProductId, Amount |

### Responses

| 클래스 | 역할 | 주요 필드 |
|--------|------|-----------|
| LoginResponse | 로그인 응답 | UserData (전체), IsNewUser, SessionToken |
| GachaResponse | 가챠 응답 | Results[], Delta, CurrentPityCount |
| ShopPurchaseResponse | 구매 응답 | Rewards[], Delta |

### Delta

| 클래스 | 역할 | 주요 필드 |
|--------|------|-----------|
| UserDataDelta | 유저 데이터 변경분 | Profile?, Currency?, AddedCharacters, RemovedCharacterIds, AddedItems, ... |

---

## 아키텍처

```
┌─────────────────────────────────────────────────┐
│              Contents (사용처)                   │
│         GachaManager, ShopManager, etc.         │
└──────────────────┬──────────────────────────────┘
                   │ SendAsync<TReq, TRes>
                   ▼
┌─────────────────────────────────────────────────┐
│              NetworkManager                      │
│                                                 │
│   RequestQueue → PacketDispatcher → Handlers   │
└──────────────────┬──────────────────────────────┘
                   │
┌─────────────────────────────────────────────────┐
│            IApiClient (인터페이스)               │
│                                                 │
│   UniTask<TRes> SendAsync<TReq, TRes>(TReq)    │
└──────────────────┬──────────────────────────────┘
                   │ 구현
        ┌──────────┴──────────┐
        ▼                     ▼
┌───────────────┐    ┌────────────────────┐
│LocalApiClient │    │ServerApiClient     │
│ (시뮬레이션)   │    │ (실제 서버 통신)    │
│               │    │ (추후 구현)         │
└───────────────┘    └────────────────────┘
```

---

## Delta 패턴

### 개념
서버 응답에서 변경된 유저 데이터만 전달. 전체 동기화 대신 부분 갱신으로 효율화.

### 사용 흐름
```
1. Login: UserSaveData 전체 수신 → DataManager.SetUserData()
2. 이후 액션: UserDataDelta만 수신 → DataManager.ApplyDelta()
```

### Delta 구조
```csharp
public class UserDataDelta
{
    public UserProfile? Profile;         // 변경 시에만 값 있음
    public UserCurrency? Currency;
    public List<OwnedCharacter> AddedCharacters;
    public List<string> RemovedCharacterIds;
    public List<OwnedItem> AddedItems;
    public List<string> RemovedItemIds;
    // ...
}
```

---

## 사용 흐름

### 로그인
```
[앱 시작]
       ↓
LoginRequest.CreateGuest(deviceId, version, platform)
       ↓
IApiClient.SendAsync<LoginRequest, LoginResponse>()
       ↓
LoginResponse { UserData, SessionToken }
       ↓
DataManager.SetUserData(response.UserData)
       ↓
EventManager.Publish(new LoginCompleteEvent())
```

### 가챠
```
[가챠 버튼]
       ↓
GachaRequest.CreateSingle/Multi(poolId)
       ↓
IApiClient.SendAsync<GachaRequest, GachaResponse>()
       ↓
GachaResponse { Results[], Delta, PityCount }
       ↓
DataManager.ApplyDelta(response.Delta)
       ↓
EventManager.Publish(new GachaResultEvent { Results })
       ↓
UI 갱신
```

---

## 폴더 구조

```
Assets/Scripts/Packet/
├── Sc.Packet.asmdef
├── Services/
│   ├── IRequest.cs
│   ├── IResponse.cs
│   ├── IApiClient.cs
│   ├── UserDataDelta.cs
│   ├── NetworkManager.cs
│   ├── RequestQueue.cs
│   └── PacketDispatcher.cs
├── Requests/
│   ├── LoginRequest.cs
│   ├── GachaRequest.cs
│   └── ShopPurchaseRequest.cs
├── Responses/
│   ├── LoginResponse.cs
│   ├── GachaResponse.cs
│   └── ShopPurchaseResponse.cs
└── Local/
    └── LocalApiClient.cs
```

---

## 설계 원칙

1. **서버 중심**: 모든 유저 데이터 변경은 서버 응답(Delta)으로만
2. **인터페이스 분리**: IApiClient로 구현 교체 용이
3. **Delta 패턴**: 부분 갱신으로 네트워크/메모리 효율화
4. **비동기 처리**: UniTask 기반 async/await
5. **Handler 분리**: Response 타입별 Handler 클래스 분리

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Interfaces | 4 | ✅ |
| Services | 4 | ✅ |
| Requests | 3 | ✅ |
| Responses | 3 | ✅ |
| Local | 1 | ✅ |
