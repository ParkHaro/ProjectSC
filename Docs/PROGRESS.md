# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## ✅ 완료된 마일스톤: OUTGAME-V1

> **완료일**: 2026-01-21
> **상세 문서**: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)
> **작업 로그**: [Milestones/OUTGAME_V1_CHANGELOG.md](Milestones/OUTGAME_V1_CHANGELOG.md)

### 시스템 구현 현황

| Phase | 시스템 | 상태 |
|-------|--------|------|
| A | Logging, ErrorHandling | ✅ |
| B | SaveManager, LoadingIndicator | ✅ |
| C | Reward, TimeService | ✅ |
| D | SystemPopup, RewardPopup | ✅ |
| E | LocalServer 분리 | ✅ |
| F | LiveEvent, Shop, Stage | ✅ |
| F | GachaEnhancement, CharacterEnhancement, NavigationEnhancement | ✅ |

### 테스트 현황

| 영역 | 테스트 수 |
|------|----------|
| Foundation, Core, Common, Reward | 149개 |
| LocalServer | 40개 |
| LiveEvent | 115개 |
| Stage | 47개 |
| CharacterEnhancement | 26개 |
| GachaEnhancement | 28개 |
| **총계** | **405개** |

---

## ⚠️ 기술 부채

> **상세**: [SPEC_INDEX.md 간극 요약](Specs/SPEC_INDEX.md#문서-구현-간극-요약-2026-01-21)

### 미구현 (문서만 존재)

| 우선순위 | 항목 | 스펙 문서 |
|---------|------|----------|
| HIGH | Utility (CollectionExtensions, MathHelper) | Common/Utility.md |
| MEDIUM | AudioManager | Core/AudioManager.md |
| LOW | SceneLoader | Core/SceneLoader.md |
| LOW | DeepLink 시스템 | Common/NavigationEnhancement.md |
| LOW | Badge 시스템 | Common/NavigationEnhancement.md |

### 플레이스홀더 (부분 구현)

| 항목 | 시스템 | 현재 상태 |
|------|--------|----------|
| EventMissionTab | LiveEvent | UI만 존재, 기능 미구현 |
| EventShopTab | LiveEvent/Shop | UI만 존재, Provider 연동 안됨 |
| PartySelectScreen | Stage | 플레이스홀더 상태 |
| AttendanceCheckTask | Lobby | Stub 구현 |
| NewEventNotificationTask | Lobby | Stub 구현 |
| ClaimEventMission API | LiveEvent | 에러코드 6099 반환 |

---

## 🚀 다음 단계

마일스톤 완료. 다음 마일스톤 설계 필요.

**가능한 방향**:
1. 인게임 전투 시스템 (BATTLE-V1)
2. 기술 부채 해소 (Utility, AudioManager)
3. 플레이스홀더 완성 (PartySelect, EventMission)

---

## 참조

| 문서 | 용도 |
|------|------|
| [OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md) | 마일스톤 상세 |
| [OUTGAME_V1_CHANGELOG.md](Milestones/OUTGAME_V1_CHANGELOG.md) | 상세 작업 로그 |
| [ARCHITECTURE.md](ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [SPEC_INDEX.md](Specs/SPEC_INDEX.md) | Assembly별 스펙 목록 |
| [DECISIONS.md](Portfolio/DECISIONS.md) | 의사결정 기록 |
| [JOURNEY.md](Portfolio/JOURNEY.md) | 프로젝트 여정 |
