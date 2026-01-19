# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## 🎯 현재 마일스톤: 아웃게임 아키텍처 1차 (OUTGAME-V1)

> **상세 문서**: [Milestones/OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md)

### 시스템 구현 상태

| Phase | 시스템 | 상태 | 스펙 문서 |
|-------|--------|------|-----------|
| A | Logging, ErrorHandling | ✅ | Foundation/*.md |
| B | SaveManager, LoadingIndicator | ✅ | 마일스톤 내 |
| C | Reward, TimeService | ✅ | Common/Reward.md, Core/TimeService.md |
| D | SystemPopup, RewardPopup | ✅ | Common/Popups/*.md |
| E | LocalServer 분리 | ✅ | 마일스톤 내 |
| F | **LiveEvent** | ✅ | LiveEvent.md |
| F | Shop | ⬜ | Shop.md |
| F | Stage | ⬜ | Stage.md |
| F | GachaEnhancement | ⬜ | Gacha/Enhancement.md |
| F | CharacterEnhancement | ⬜ | Character/Enhancement.md |
| F | NavigationEnhancement | ⬜ | Common/NavigationEnhancement.md |

---

## 🚀 다음 작업

**지시**: "[시스템명] 구현하자" (예: "Shop 구현하자", "Stage 구현하자")

### 우선순위
1. **로비 진입 후처리 시스템** - [Lobby.md 참조](Specs/Lobby.md#로비-진입-후처리-시스템)
2. **Shop** 또는 **Stage** 시스템

---

## 🔨 진행 중인 작업

### 로비 진입 후처리 시스템 ⬜

> **스펙 문서**: [Lobby.md](Specs/Lobby.md#로비-진입-후처리-시스템)

```
- [ ] ILobbyEntryTask.cs (Priority, CheckRequired, Execute)
- [ ] LobbyEntryTaskRunner.cs
- [ ] AttendanceCheckTask.cs (Priority 10)
- [ ] EventCurrencyConversionTask.cs (Priority 20)
- [ ] NewEventNotificationTask.cs (Priority 30)
- [ ] LobbyScreen.OnShow()에서 TaskRunner 호출
```

---

## 🧪 테스트 인프라

> **상세 문서**: [Specs/Testing/TestArchitecture.md](Specs/Testing/TestArchitecture.md)

| 단계 | 항목 | 상태 | 테스트 수 |
|------|------|------|----------|
| 1~3차 | Foundation, Core, Common, Reward | ✅ | 149개 |
| 3.5차 | LocalServer | ✅ | 40개 |
| 4~4.5차 | PlayMode 인프라, 에디터 도구 | ✅ | - |
| 5차 | 시스템 확장 | ⬜ | - |

**총 테스트**: 189개

---

## ✅ 완료된 시스템 요약

<details>
<summary>클릭하여 펼치기</summary>

### 기반 인프라 (Phase A~E)
- **Logging**: LogLevel, LogCategory, Log.cs 정적 API
- **ErrorHandling**: ErrorCode, Result<T>, ErrorMessages
- **SaveManager**: ISaveStorage, FileSaveStorage, SaveMigrator
- **LoadingIndicator**: LoadingService, LoadingWidget, 레퍼런스 카운팅
- **Reward**: RewardInfo, RewardProcessor, RewardHelper
- **TimeService**: ITimeService, TimeHelper, LimitType
- **SystemPopup**: ConfirmPopup, CostConfirmPopup, State 패턴
- **RewardPopup**: RewardItem, IItemSpawner, 레이아웃 자동조정
- **LocalServer**: Sc.LocalServer Assembly 분리, Handler 패턴

### 컨텐츠 (Phase F)
- **LiveEvent**: 배너/상세/미션탭, TabWidget, 마스터데이터 SO

### MVP 완료
- Title, Lobby, Gacha, CharacterList, CharacterDetail Screen
- CurrencyHUD, GachaResultPopup, ScreenHeader
- Navigation 통합 스택, Transition 애니메이션
- DataManager, NetworkManager 이벤트 기반

</details>

---

## 작업 로그 (최근)

### 2026-01-20
- [x] LiveEvent 시스템 구현 완료
  - Enums, Data 구조체, UserSaveData v3 마이그레이션
  - Request/Response, LocalServer 핸들러
  - UI (LiveEventScreen, EventDetailScreen, TabWidget)
  - 마스터 데이터 SO, ResponseHandler, 이벤트
- [x] LocalServer 단위 테스트 (40개)
- [x] Request/Response 타입 Sc.Data로 이동

### 2026-01-19
- [x] Sc.LocalServer Assembly 분리
- [x] AssetManager 통합, RewardIconCache 대체
- [x] PlayMode 테스트 인프라 구축
- [x] 에디터 도구 리팩토링 (SC Tools 메뉴 재구성)

<details>
<summary>이전 작업 로그</summary>

### 2026-01-18~19
- SaveManager, LoadingIndicator, Reward, TimeService 구현
- SystemPopup, RewardPopup 구현
- NUnit 단위 테스트 149개

### 2026-01-16~17
- 아웃게임 아키텍처 V1 마일스톤 설계
- Screen/Popup Transition 애니메이션
- ScreenHeader, CharacterDetailScreen
- 재화 시스템 확장 (16개 CostType)

### 2026-01-15
- MVP 화면 구현 (Title, Lobby, Gacha, CharacterList)
- 네트워크 이벤트 큐 아키텍처
- 데이터 아키텍처 v2.0 (서버 중심)

### 2026-01-14
- 프로젝트 초기 설정
- Assembly 기반 아키텍처 설계
- 스펙 문서 작성

</details>

---

## 참조

| 문서 | 용도 |
|------|------|
| [OUTGAME_ARCHITECTURE_V1.md](Milestones/OUTGAME_ARCHITECTURE_V1.md) | 마일스톤 상세 |
| [ARCHITECTURE.md](ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [SPEC_INDEX.md](Specs/SPEC_INDEX.md) | Assembly별 스펙 목록 |
| [DECISIONS.md](Portfolio/DECISIONS.md) | 의사결정 기록 |
