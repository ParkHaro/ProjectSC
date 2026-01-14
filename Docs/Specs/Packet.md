---
type: overview
assembly: Sc.Packet
category: Network
status: draft
version: "1.0"
dependencies: [Sc.Data, UniTask]
detail_docs: [IPacketService, Requests, Responses, LocalPacketService]
created: 2025-01-14
updated: 2025-01-14
---

# Sc.Packet

## 목적
서버 통신 인터페이스 계층. Request/Response 패턴으로 데이터 교환. 로컬/네트워크 구현 교체 가능.

## 의존성
- **참조**: Sc.Data (요청/응답 데이터 타입), UniTask (비동기)
- **참조됨**: Contents (Gacha, Shop, Battle 등)

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **IPacketService** | 패킷 송수신 인터페이스 |
| **Request** | 서버 요청 구조체 |
| **Response** | 서버 응답 구조체 |
| **LocalPacketService** | 로컬 구현 (현재) |
| **NetworkPacketService** | 네트워크 구현 (추후) |

---

## Packet vs Event 구분

| 구분 | Packet | Event |
|------|--------|-------|
| **목적** | 서버와 데이터 교환 | 클라이언트 내부 알림 |
| **방향** | Request → Response | 단방향 Publish |
| **응답** | 있음 (비동기) | 없음 |
| **예시** | GachaRequest/Response | GachaResultEvent |
| **교체** | Local ↔ Network | 변경 없음 |

---

## 클래스 역할 정의

### Services

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| IPacketService | 패킷 서비스 계약 | SendAsync 인터페이스 정의 | 구현 |
| LocalPacketService | 로컬 처리 구현 | 즉시 로직 처리, 결과 반환 | 네트워크 통신 |
| NetworkPacketService | 네트워크 구현 (추후) | 서버 통신, 직렬화 | 로컬 로직 |

### Requests

| 클래스 | 역할 | 주요 필드 | 사용처 |
|--------|------|-----------|--------|
| GachaRequest | 가챠 요청 | PoolId, Count | Gacha |
| ShopPurchaseRequest | 구매 요청 | ItemId, Amount, CurrencyType | Shop |
| BattleResultRequest | 전투 결과 저장 | StageId, Result, Rewards | Battle |
| SaveDataRequest | 데이터 저장 | SaveData | Save |

### Responses

| 클래스 | 역할 | 주요 필드 | 사용처 |
|--------|------|-----------|--------|
| GachaResponse | 가챠 결과 | Success, Results[], RemainingGem | Gacha |
| ShopPurchaseResponse | 구매 결과 | Success, NewItemCount, NewCurrency | Shop |
| BattleResultResponse | 저장 확인 | Success | Battle |
| SaveDataResponse | 저장 확인 | Success, Timestamp | Save |

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
│           IPacketService (인터페이스)            │
│                                                 │
│   UniTask<TRes> SendAsync<TReq, TRes>(TReq)    │
└──────────────────┬──────────────────────────────┘
                   │ 구현
        ┌──────────┴──────────┐
        ▼                     ▼
┌───────────────┐    ┌────────────────────┐
│LocalPacketSvc │    │NetworkPacketSvc    │
│ (즉시 처리)    │    │ (서버 통신)         │
│               │    │ (추후 구현)         │
└───────────────┘    └────────────────────┘
```

---

## 사용 흐름

### 가챠 예시
```
[사용자 가챠 버튼]
       ↓
GachaManager.RollGacha()
       ↓
new GachaRequest { PoolId, Count }
       ↓
IPacketService.SendAsync<GachaRequest, GachaResponse>()
       │
       ├─ [LocalPacketService]
       │    └─ 즉시 로직 처리 → Response 반환
       │
       └─ [NetworkPacketService] (추후)
            └─ 서버 전송 → 응답 대기 → Response 반환
       ↓
GachaResponse { Success, Results, RemainingGem }
       ↓
EventManager.Publish(new GachaResultEvent { Results })
       ↓
UI 갱신
```

---

## 사용 패턴

```csharp
// 1. 요청 생성
var request = new GachaRequest
{
    PoolId = "standard",
    Count = 10
};

// 2. 패킷 전송 (인터페이스 사용)
var response = await _packetService.SendAsync<GachaRequest, GachaResponse>(request);

// 3. 결과 처리
if (response.Success)
{
    // 내부 이벤트 발행 (UI 갱신용)
    EventManager.Instance.Publish(new GachaResultEvent { Results = response.Results });
}
```

---

## 서비스 교체

```csharp
// GameManager에서 초기화
public class GameManager : Singleton<GameManager>
{
    public IPacketService PacketService { get; private set; }

    protected override void OnInitialize()
    {
        // 로컬 모드 (현재)
        PacketService = new LocalPacketService();

        // 네트워크 모드 (추후)
        // PacketService = new NetworkPacketService("server-url");
    }
}
```

---

## 폴더 구조

```
Assets/Scripts/Packet/
├── Sc.Packet.asmdef
├── Services/
│   └── IPacketService.cs
├── Requests/
│   ├── GachaRequest.cs
│   ├── ShopPurchaseRequest.cs
│   └── ...
├── Responses/
│   ├── GachaResponse.cs
│   ├── ShopPurchaseResponse.cs
│   └── ...
└── Local/
    └── LocalPacketService.cs
```

---

## 설계 원칙

1. **인터페이스 분리**: IPacketService로 구현 교체 용이
2. **비동기 처리**: UniTask 기반 async/await
3. **불변 구조체**: Request/Response는 struct
4. **단일 책임**: Packet은 통신만, 로직은 Contents에서

---

## 상세 문서
- [IPacketService.md](Packet/IPacketService.md) - 서비스 인터페이스 상세
- [Requests.md](Packet/Requests.md) - 요청 구조체 상세
- [Responses.md](Packet/Responses.md) - 응답 구조체 상세
- [LocalPacketService.md](Packet/LocalPacketService.md) - 로컬 구현 상세

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Services | 1 | ⬜ |
| Requests | 4+ | ⬜ |
| Responses | 4+ | ⬜ |
| Local | 1 | ⬜ |
