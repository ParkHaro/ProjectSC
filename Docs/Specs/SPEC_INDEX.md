# 스펙 문서 인덱스

## 문서 참조 방법

1. **대분류 문서 우선 확인** - 역할, 책임, 관계 파악
2. **세부 문서는 구현 시에만** - 필드, 메서드, 코드 예시

---

## 기반 레이어

| Assembly | 설명 | 대분류 | 세부 문서 | 상태 |
|----------|------|--------|-----------|------|
| Sc.Data | 순수 데이터 정의 | [Data.md](Data.md) | [Enums](Data/Enums.md), [Structs](Data/Structs.md), [SO](Data/ScriptableObjects.md) | ⬜ |
| Sc.Event | 클라이언트 내부 이벤트 | [Event.md](Event.md) | [Common](Event/CommonEvents.md), [InGame](Event/InGameEvents.md), [OutGame](Event/OutGameEvents.md) | ⬜ |
| Sc.Packet | 서버 통신 인터페이스 | [Packet.md](Packet.md) | [IPacketService](Packet/IPacketService.md), [Requests](Packet/Requests.md), [Responses](Packet/Responses.md) | ⬜ |
| Sc.Core | 핵심 시스템 | [Core.md](Core.md) | [Singleton](Core/Singleton.md), [EventManager](Core/EventManager.md), [ResourceManager](Core/ResourceManager.md), [SceneLoader](Core/SceneLoader.md), [AudioManager](Core/AudioManager.md), [SaveManager](Core/SaveManager.md), [StateMachine](Core/StateMachine.md) | ⬜ |
| Sc.Common | 공통 모듈 | [Common.md](Common.md) | [MVP](Common/MVP.md), [UIComponents](Common/UIComponents.md), [Pool](Common/Pool.md), [Utility](Common/Utility.md) | ⬜ |

---

## Contents - Shared

| Assembly | 설명 | 패턴 | 대분류 | 상태 |
|----------|------|------|--------|------|
| Sc.Contents.Character | 캐릭터 시스템 | Factory, Flyweight | [Character.md](Character.md) | ⬜ |
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
| Sc.Contents.Lobby | 로비 시스템 | - | [Lobby.md](Lobby.md) | ⬜ |
| Sc.Contents.Gacha | 가챠 시스템 | Strategy | [Gacha.md](Gacha.md) | ⬜ |
| Sc.Contents.Shop | 상점 시스템 | - | [Shop.md](Shop.md) | ⬜ |
| Sc.Contents.Quest | 퀘스트 시스템 | Composite | [Quest.md](Quest.md) | ⬜ |

---

## 문서 구조

```
Docs/Specs/
├── SPEC_INDEX.md          # 이 파일
├── DOC_RULES.md           # 문서 작성 규칙
│
├── Data.md                # 대분류
├── Data/
│   ├── Enums.md
│   ├── Structs.md
│   └── ScriptableObjects.md
│
├── Event.md               # 대분류 (NEW)
├── Event/
│   ├── CommonEvents.md
│   ├── InGameEvents.md
│   └── OutGameEvents.md
│
├── Packet.md              # 대분류 (재설계)
├── Packet/
│   ├── IPacketService.md
│   ├── Requests.md
│   ├── Responses.md
│   └── LocalPacketService.md
│
├── Core.md
├── Core/
│   ├── Singleton.md
│   ├── EventManager.md
│   ├── ResourceManager.md
│   ├── SceneLoader.md
│   ├── AudioManager.md
│   ├── SaveManager.md
│   └── StateMachine.md
│
├── Common.md
├── Common/
│   ├── MVP.md
│   ├── UIComponents.md
│   ├── Pool.md
│   └── Utility.md
│
└── Contents (추후 작성)
    ├── Character.md
    ├── Inventory.md
    ├── Battle.md
    └── ...
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
