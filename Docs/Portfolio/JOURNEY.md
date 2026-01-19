# 프로젝트 여정

프로젝트 진행 과정을 Phase별로 요약합니다. 상세 의사결정은 [DECISIONS.md](DECISIONS.md) 참조.

---

## Phase 1-3: 프로젝트 초기화 및 아키텍처 설계

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 1 | Unity 프로젝트 생성, 기본 구조 | .gitignore, EditorConfig |
| 2 | Assembly Definition 모듈화 | `Sc.` 접두사 기반 분리 |
| 3 | 이벤트/패킷 분리, Editor 도구 | 계층 분리, AI 협업 도구 |

---

## Phase 4-6: UI 시스템 구축

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 4 | UI 아키텍처 설계 | MVP → Widget 전환 |
| 5 | Widget 시스템 구현 | Screen/Popup/Navigation |
| 6 | 개발 도구 강화 | NavigationDebugWindow, UITestSceneSetup |

**핵심 결정**: [UI 아키텍처: MVP → Widget](Decisions/UI.md#ui-아키텍처-mvp--widget)

---

## Phase 7-8: 데이터 아키텍처 v2.0

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 7 | 아키텍처 재설계 | 로컬 중심 → 서버 중심 |
| 8 | 마스터/유저 데이터 파이프라인 | JSON → SO, Delta 패턴 |

**핵심 결정**: [데이터 아키텍처 전환](Decisions/Data.md#데이터-아키텍처-로컬-중심--서버-중심)

---

## Phase 9-10: MVP 화면 구현

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 9 | 기본 Widget 컴포넌트 | 8개 타입 (Text, Button, Image...) |
| 10 | MVP 화면 | Title, Lobby, Gacha, CharacterList, CharacterDetail |

**핵심 결정**: [Unity 기본 컴포넌트 Widget화](Decisions/UI.md#unity-기본-컴포넌트-widget화)

---

## Phase 11-12: Transition & OUTGAME-V1 설계

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 11 | Transition 애니메이션 | DOTween 기반 Fade, Scale |
| 12 | 마일스톤 설계 | Foundation, Stage, LiveEvent 스펙 |

**핵심 결정**: [Screen/Popup Transition 설계](Decisions/UI.md#screenpopup-transition-애니메이션)

---

## Phase 13-17: Foundation & Common 시스템

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 13 | 테스트 인프라 설계 | 시스템 단위 테스트, ServiceLocator |
| 14 | Foundation 구현 | Log, Error, SaveManager (37개 테스트) |
| 15 | LoadingIndicator | 레퍼런스 카운팅 기반 |
| 16 | Reward 시스템 | RewardType 4개 + ItemCategory 6개 (61개 테스트) |
| 17 | TimeService | 서버 전환 비용 최소화 설계 (45개 테스트) |

**핵심 결정**: [테스트 아키텍처](Decisions/Testing.md#테스트-아키텍처-시스템-단위-테스트)

---

## Phase 18-19: SystemPopup & RewardPopup

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 18 | SystemPopup | ConfirmPopup, CostConfirmPopup (34개 테스트) |
| 19 | RewardPopup | IItemSpawner 추상화 (33개 테스트) |

**핵심 결정**: [SystemPopup 하이브리드 구조](Decisions/UI.md#systempopup-하이브리드-구조)

---

## Phase 20-22: 테스트 & 리팩토링

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 20 | PlayMode 테스트 | PlayModeTestBase, 10개 테스트 |
| 21 | 에디터 도구 리팩토링 | Bootstrap 레벨 체계화 |
| 22 | AssetManager 통합 | IAssetHandle, RewardIconCache 대체 |

**핵심 결정**: [PlayMode 테스트 인프라](Decisions/Testing.md#playmode-테스트-인프라)

---

## Phase 23-24: LocalServer 분리 & 테스트

| Phase | 주요 작업 | 결과 |
|-------|----------|------|
| 23 | Sc.LocalServer Assembly | LocalApiClient 354줄 → 157줄 |
| 24 | LocalServer 테스트 | 40개 테스트 (RewardService, ServerValidator...) |

**핵심 결정**: [LocalServer 서버 로직 분리](Decisions/Architecture.md#localserver-서버-로직-분리)

---

## Phase 25: LiveEvent 구현 (진행 중)

| 항목 | 상태 |
|------|------|
| Enums, Data 구조체 | ✅ |
| Request/Response, LocalServer 핸들러 | ✅ |
| UI (LiveEventScreen, EventDetailScreen) | ✅ |
| 마스터 데이터 SO, ResponseHandler | ✅ |
| 에디터 도구 (LiveEventSetup) | ⬜ |

**핵심 결정**: [모듈형 이벤트 서브컨텐츠](Decisions/Content.md#모듈형-이벤트-서브컨텐츠-eventsubcontent)

---

## 진행 중

현재 작업 상태는 [PROGRESS.md](../PROGRESS.md) 참조.
