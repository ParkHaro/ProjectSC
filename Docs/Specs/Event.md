---
type: overview
assembly: Sc.Event
category: Event
status: draft
version: "1.0"
dependencies: [Sc.Data]
detail_docs: [CommonEvents, InGameEvents, OutGameEvents]
created: 2025-01-14
updated: 2025-01-14
---

# Sc.Event

## 목적
클라이언트 내부 이벤트 정의. 컨텐츠 간 느슨한 결합을 위한 Publish/Subscribe 패턴 지원.

## 의존성
- **참조**: Sc.Data (이벤트 페이로드에 데이터 타입 사용)
- **참조됨**: Sc.Core(EventManager), 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **Event Struct** | 이벤트 데이터 운반체. 불변 struct로 정의 |
| **Publish** | 이벤트 발행. EventManager.Publish<T>(event) |
| **Subscribe** | 이벤트 구독. EventManager.Subscribe<T>(callback) |

---

## Event vs Packet 구분

| 구분 | Event | Packet |
|------|-------|--------|
| **목적** | 클라이언트 내부 알림 | 서버와 데이터 교환 |
| **방향** | 단방향 Publish | Request → Response |
| **사용처** | UI 갱신, 상태 알림 | 가챠, 구매, 저장 |
| **예시** | DamageEvent, GachaResultEvent | GachaRequest/Response |

---

## 이벤트 분류 기준

| 분류 | 기준 | 예시 |
|------|------|------|
| **Common** | 게임 전역, 시스템 레벨 | 씬 로드, 에러 |
| **InGame** | 전투 중 발생 | 데미지, 턴, 스킬 |
| **OutGame** | 전투 외 발생 | 메뉴, 가챠 결과, 퀘스트 |

---

## 클래스 역할 정의

### Common Events

| 이벤트 | 역할 | 페이로드 | 발행자 → 구독자 |
|--------|------|----------|-----------------|
| SceneLoadedEvent | 씬 로드 완료 알림 | SceneType | SceneLoader → GameManager, UI |
| ErrorEvent | 에러 발생 알림 | ErrorCode, Message | Any → ErrorHandler |

### InGame Events

| 이벤트 | 역할 | 페이로드 | 발행자 → 구독자 |
|--------|------|----------|-----------------|
| BattleStartEvent | 전투 시작 알림 | StageId, PartyIds | BattleManager → UI, Audio |
| BattleEndEvent | 전투 종료 알림 | BattleResult | BattleManager → Lobby, Quest |
| TurnStartEvent | 턴 시작 알림 | TurnNumber, ActiveCharacterId | BattleManager → UI, AI |
| DamageEvent | 데미지 발생 알림 | SourceId, TargetId, Amount | Skill → UI, Character |
| SkillUsedEvent | 스킬 사용 알림 | CharacterId, SkillId | Character → UI, Audio |
| CharacterDeathEvent | 캐릭터 사망 알림 | CharacterId, KillerId | Character → BattleManager |

### OutGame Events

| 이벤트 | 역할 | 페이로드 | 발행자 → 구독자 |
|--------|------|----------|-----------------|
| MenuSelectedEvent | 메뉴 선택 알림 | MenuType | UI → Lobby |
| GachaResultEvent | 가챠 결과 알림 | Results[] | GachaManager → UI |
| QuestCompleteEvent | 퀘스트 완료 알림 | QuestId, Rewards | Quest → UI |
| RewardClaimEvent | 보상 수령 알림 | RewardData | UI → Inventory |

---

## 이벤트 흐름 예시

### 전투 흐름
```
BattleStartEvent
    ↓
┌─→ TurnStartEvent
│       ↓
│   SkillUsedEvent → DamageEvent → BuffAppliedEvent
│       ↓
│   CharacterDeathEvent (조건부)
│       ↓
└── TurnEndEvent
        ↓
BattleEndEvent
```

---

## 설계 원칙

1. **불변 Struct**: readonly struct 사용 권장
2. **최소 페이로드**: 필요한 데이터만 포함
3. **단방향 흐름**: 이벤트는 알림만, 응답 없음

---

## 상세 문서
- [CommonEvents.md](Event/CommonEvents.md) - 공통 이벤트 상세
- [InGameEvents.md](Event/InGameEvents.md) - 인게임 이벤트 상세
- [OutGameEvents.md](Event/OutGameEvents.md) - 아웃게임 이벤트 상세

---

## 상태

| 분류 | 이벤트 수 | 상태 |
|------|-----------|------|
| Common | 2 | ⬜ |
| InGame | 9 | ⬜ |
| OutGame | 5 | ⬜ |
