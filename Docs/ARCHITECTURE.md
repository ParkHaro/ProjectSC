# 아키텍처

## 설계 원칙

1. **Assembly 분리**: 컴파일 시간 단축 및 의존성 명확화
2. **InGame/OutGame 분리**: 게임 영역별 모듈 독립
3. **이벤트 기반 통신**: 컨텐츠 간 직접 참조 최소화
4. **MVP 패턴**: UI와 로직 분리

---

## 폴더 구조

```
Assets/Scripts/
│
├── Data/                           # [Sc.Data] 순수 데이터 정의
│   ├── Enums/                     # 공용 열거형
│   ├── Structs/                   # 공용 구조체
│   └── ScriptableObjects/         # SO 정의
│
├── Event/                          # [Sc.Event] 클라이언트 내부 이벤트
│   ├── Common/                    # 공통 이벤트
│   ├── InGame/                    # 인게임 이벤트
│   └── OutGame/                   # 아웃게임 이벤트
│
├── Packet/                         # [Sc.Packet] 서버 통신 인터페이스
│   ├── Services/                  # IPacketService 인터페이스
│   ├── Requests/                  # 요청 구조체
│   ├── Responses/                 # 응답 구조체
│   └── Local/                     # LocalPacketService (현재)
│
├── Core/                           # [Sc.Core] 핵심 시스템
│   ├── Base/                      # Singleton 등 베이스
│   ├── Managers/                  # 전역 매니저
│   └── Systems/                   # 범용 시스템
│
├── Common/                         # [Sc.Common] 공통 모듈
│   ├── UI/                        # 공통 UI (Base, Components)
│   ├── Pool/                      # 오브젝트 풀
│   └── Utility/                   # 유틸리티
│
└── Contents/
    ├── Shared/                    # 인게임/아웃게임 공통
    │   ├── Character/             # [Sc.Contents.Character]
    │   └── Inventory/             # [Sc.Contents.Inventory]
    │
    ├── InGame/                    # 인게임 전용
    │   ├── Battle/                # [Sc.Contents.Battle]
    │   └── Skill/                 # [Sc.Contents.Skill]
    │
    └── OutGame/                   # 아웃게임 전용
        ├── Lobby/                 # [Sc.Contents.Lobby]
        ├── Gacha/                 # [Sc.Contents.Gacha]
        ├── Shop/                  # [Sc.Contents.Shop]
        └── Quest/                 # [Sc.Contents.Quest]
```

---

## Assembly 의존성

```
                     ┌───────────┐
                     │  Sc.Data  │ ← 의존성 없음
                     └─────┬─────┘
                           │
              ┌────────────┴────────────┐
              ↓                         ↓
        ┌───────────┐            ┌───────────┐
        │ Sc.Event  │            │ Sc.Packet │
        │(클라이언트)│            │(서버통신) │
        └─────┬─────┘            └───────────┘
              ↓                         │
        ┌───────────┐                   │
        │  Sc.Core  │ → Sc.Data, Sc.Event
        └─────┬─────┘                   │
              ↓                         │
        ┌───────────┐                   │
        │ Sc.Common │ → Sc.Core, ...    │
        └─────┬─────┘                   │
              │                         │
        ┌─────┴─────────────────────────┘
        ↓
┌──────────────────────────────────────┐
│            Contents/*                │
│  → Sc.Common, Sc.Packet (필요시)     │
└──────────────────────────────────────┘
```

### Assembly 참조 테이블

| Assembly | 참조 | 역할 |
|----------|------|------|
| `Sc.Data` | (없음) | 순수 데이터 정의 |
| `Sc.Event` | Sc.Data | 클라이언트 내부 이벤트 |
| `Sc.Packet` | Sc.Data, UniTask | 서버 통신 인터페이스 |
| `Sc.Core` | Sc.Data, Sc.Event | 핵심 시스템 (EventManager 등) |
| `Sc.Common` | Sc.Core, Sc.Data, Sc.Event | 공통 모듈 (MVP, Pool 등) |
| `Sc.Contents.Character` | Sc.Common | 캐릭터 시스템 |
| `Sc.Contents.Inventory` | Sc.Common | 인벤토리 시스템 |
| `Sc.Contents.Battle` | Sc.Common, Sc.Contents.Character | 전투 시스템 |
| `Sc.Contents.Skill` | Sc.Common, Sc.Contents.Character | 스킬 시스템 |
| `Sc.Contents.Lobby` | Sc.Common | 로비 시스템 |
| `Sc.Contents.Gacha` | Sc.Common, Sc.Contents.Character, Sc.Packet | 가챠 시스템 |
| `Sc.Contents.Shop` | Sc.Common, Sc.Contents.Inventory, Sc.Packet | 상점 시스템 |
| `Sc.Contents.Quest` | Sc.Common, Sc.Packet | 퀘스트 시스템 |

---

## 컨텐츠 간 통신

### 원칙
- **상하관계 있음**: Assembly 직접 참조
- **상하관계 없음**: Event (클라이언트 내부 이벤트)
- **서버 통신 필요**: Packet (Request/Response 패턴)

### Event vs Packet

| 구분 | Event | Packet |
|------|-------|--------|
| 목적 | 클라이언트 내부 알림 | 서버와 데이터 교환 |
| 방향 | 단방향 Publish | Request → Response |
| 응답 | 없음 | 있음 (비동기) |
| 예시 | BattleEndEvent | GachaRequest/Response |

### 통신 다이어그램

```
┌─────────────┐  Event    ┌─────────────┐
│   InGame    │◄────────►│   OutGame   │
│  (Battle)   │ (알림)    │  (Lobby)    │
└──────┬──────┘           └──────┬──────┘
       │                         │
       │    ┌─────────────┐      │
       │    │  Sc.Packet  │      │
       │    │ (서버 통신)  │◄─────┘ GachaRequest 등
       │    └─────────────┘
       │
       └───────────┬─────────────
                   ↓
            ┌──────────────┐
            │    Shared    │
            │ (Character)  │
            └──────────────┘
```

### Event 예시

```csharp
// Event/InGame/BattleEndEvent.cs
public struct BattleEndEvent
{
    public bool IsVictory;
    public List<RewardData> Rewards;
}

// Battle에서 발행
EventManager.Instance.Publish(new BattleEndEvent { ... });

// Lobby에서 구독
EventManager.Instance.Subscribe<BattleEndEvent>(OnBattleEnd);
```

### Packet 예시

```csharp
// Gacha에서 서버 요청
var request = new GachaRequest { PoolId = "standard", Count = 10 };
var response = await _packetService.SendAsync<GachaRequest, GachaResponse>(request);

// 결과를 Event로 알림
if (response.Success)
{
    EventManager.Instance.Publish(new GachaResultEvent { Results = response.Results });
}
```

---

## 컨텐츠 영역 분류

| 영역 | 컨텐츠 | 설명 |
|------|--------|------|
| **Shared** | Character | 캐릭터 데이터, 스탯, 팩토리 |
| **Shared** | Inventory | 인벤토리, 아이템 관리 |
| **InGame** | Battle | 전투 흐름, 턴 관리 |
| **InGame** | Skill | 스킬 실행, 버프/디버프 |
| **OutGame** | Lobby | 메인 로비, 메뉴 |
| **OutGame** | Gacha | 캐릭터 뽑기 |
| **OutGame** | Shop | 상점 |
| **OutGame** | Quest | 퀘스트, 업적 |

---

## Namespace 규칙

```csharp
// 기반 레이어
namespace Sc.Data { }
namespace Sc.Data.Enums { }
namespace Sc.Data.Structs { }

namespace Sc.Event { }
namespace Sc.Event.Common { }
namespace Sc.Event.InGame { }
namespace Sc.Event.OutGame { }

namespace Sc.Packet { }
namespace Sc.Packet.Services { }
namespace Sc.Packet.Requests { }
namespace Sc.Packet.Responses { }

namespace Sc.Core { }
namespace Sc.Core.Managers { }

namespace Sc.Common { }
namespace Sc.Common.UI { }

// 컨텐츠
namespace Sc.Contents.Character { }
namespace Sc.Contents.Battle { }
namespace Sc.Contents.Gacha { }
```
