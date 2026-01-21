# 스펙 문서 인덱스

## 문서 참조 방법

1. **대분류 문서 우선 확인** - 역할, 책임, 관계 파악
2. **세부 문서는 구현 시에만** - 필드, 메서드, 코드 예시

---

## 기반 레이어

| Assembly | 설명 | 대분류 | 세부 문서 | 상태 |
|----------|------|--------|-----------|------|
| Sc.Foundation | 로깅, 에러처리, 서비스 | [Foundation.md](Foundation.md) | [Logging](Foundation/Logging.md), [Error](Foundation/Error.md) | ✅ |
| Sc.Data | 순수 데이터 정의 | [Data.md](Data.md) | [Enums](Data/Enums.md), [Structs](Data/Structs.md), [SO](Data/ScriptableObjects.md) | ⬜ |
| Sc.Event | 클라이언트 내부 이벤트 | [Event.md](Event.md) | [Common](Event/CommonEvents.md), [InGame](Event/InGameEvents.md), [OutGame](Event/OutGameEvents.md) | ⬜ |
| Sc.Packet | 서버 통신 인터페이스 | [Packet.md](Packet.md) | [IPacketService](Packet/IPacketService.md), [Requests](Packet/Requests.md), [Responses](Packet/Responses.md) | ⬜ |
| Sc.Core | 핵심 시스템 | [Core.md](Core.md) | [SaveManager](Core/SaveManager.md), [TimeService](Core/TimeService.md), [AssetManager](Core/AssetManager.md) | 🔨 |
| Sc.Common | 공통 모듈 | [Common.md](Common.md) | [UISystem](Common/UISystem.md), [UIComponents](Common/UIComponents.md), [Reward](Common/Reward.md) | 🔨 |
| Sc.LocalServer | 로컬 서버 시뮬레이션 | [LocalServer.md](LocalServer.md) | - | ✅ |

---

## Contents - Shared

| Assembly | 설명 | 패턴 | 대분류 | 상태 |
|----------|------|------|--------|------|
| Sc.Contents.Character | 캐릭터 시스템 | Factory, Flyweight | [Character.md](Character.md) | 🔨 (Enhancement ✅) |
| Sc.Contents.Inventory | 인벤토리 시스템 | - | [Inventory.md](Inventory.md) | ⬜ |

---

## Contents - InGame

| Assembly | 설명 | 패턴 | 대분류 | 상태 |
|----------|------|------|--------|------|
| Sc.Contents.Battle | 전투 시스템 | State, Command | [Battle.md](Battle.md) | ⬜ |
| Sc.Contents.Skill | 스킬/버프 시스템 | Decorator | [Skill.md](Skill.md) | ⬜ |

---

## Contents - OutGame

| Assembly | 설명 | 패턴 | 대분류 | 상태 |
|----------|------|------|--------|------|
| Sc.Contents.Lobby | 로비 시스템 | - | [Lobby.md](Lobby.md) | ✅ |
| Sc.Contents.Gacha | 가챠 시스템 | Strategy | [Gacha.md](Gacha.md) | ⬜ |
| Sc.Contents.Shop | 상점 시스템 | Provider | [Shop.md](Shop.md) | 🔨 |
| Sc.Contents.LiveEvent | 라이브 이벤트 | - | [LiveEvent.md](LiveEvent.md) | ✅ |
| Sc.Contents.Stage | 스테이지 시스템 | Composition | [Stage.md](Stage.md) | ✅ |
| Sc.Contents.Quest | 퀘스트 시스템 | Composite | [Quest.md](Quest.md) | ⬜ |

---

## Editor (빌드 제외)

| Assembly | 설명 | 대분류 | 상태 |
|----------|------|--------|------|
| Sc.Editor.AI | AI 기반 씬/프리팹 자동 생성 도구 | [AITools.md](Editor/AITools.md) | ✅ |

---

## 문서 구조

```
Docs/Specs/
├── SPEC_INDEX.md          # 이 파일
├── DOC_RULES.md           # 문서 작성 규칙
│
├── Foundation.md          # ✅ 완료
├── Foundation/
│   ├── Logging.md
│   └── Error.md
│
├── Data.md
├── Data/
│   ├── Enums.md
│   ├── Structs.md
│   └── ScriptableObjects.md
│
├── Event.md
├── Event/
│   ├── CommonEvents.md
│   ├── InGameEvents.md
│   └── OutGameEvents.md
│
├── Packet.md
├── Packet/
│   ├── IPacketService.md
│   ├── Requests.md
│   ├── Responses.md
│   └── LocalPacketService.md
│
├── Core.md                # 🔨 진행 중
├── Core/
│   ├── SaveManager.md
│   ├── TimeService.md
│   └── AssetManager.md
│
├── Common.md              # 🔨 진행 중
├── Common/
│   ├── UISystem.md
│   ├── UIComponents.md
│   ├── Reward.md
│   ├── Pool.md            # ⬜ 미구현
│   └── Utility.md         # ⬜ 미구현
│
├── LocalServer.md         # ✅ 완료
│
├── LiveEvent.md           # ✅ 완료
├── LiveEvent/
│   └── IMPLEMENTATION_PLAN.md
│
├── Shop.md                # 🔨 진행 중
│
├── Stage.md               # ✅ 완료
│
├── Lobby.md               # ✅ 완료
│
├── Editor/
│   └── AITools.md
│
└── (미구현 스펙)
    ├── Character.md
    ├── Inventory.md
    ├── Battle.md
    ├── Skill.md
    ├── Gacha.md
    └── Quest.md
```

---

## Event vs Packet

| 구분 | Event | Packet |
|------|-------|--------|
| 목적 | 클라이언트 내부 알림 | 서버와 데이터 교환 |
| 방향 | 단방향 Publish | Request → Response |
| 예시 | DamageEvent | GachaRequest/Response |

---

## 상태 범례

- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## 문서-구현 간극 요약 (2026-01-21)

### 긴급 조치 필요

| 항목 | 상태 |
|------|------|
| Pool 시스템 구현 | 문서만 존재 |
| Utility 구현 | 문서만 존재 |

### ~~미문서화 구현~~ → 문서화 완료 (2026-01-21)

| 시스템 | 항목 | 상태 |
|--------|------|------|
| Foundation | Services.cs, ISaveStorage, FileSaveStorage | ✅ |
| Core | NetworkManager, GameBootstrap, InitializationSequence 등 12개 | ✅ |
| Common | PopupQueueService, UIEventBridge | ✅ |

### 플레이스홀더 상태

| 항목 | 시스템 | 현재 상태 |
|------|--------|----------|
| EventMissionTab | LiveEvent | UI만 존재, 기능 미구현 |
| EventShopTab | LiveEvent/Shop | UI만 존재, Provider 연동 안됨 |
| PartySelectScreen | Stage | 플레이스홀더 상태 |
| ShopState | Shop | 비어있는 플레이스홀더 |
| AttendanceCheckTask | Lobby | Stub 구현 |
| NewEventNotificationTask | Lobby | Stub 구현 |
| ClaimEventMission API | LiveEvent | 에러코드 6099 반환 |
