# ProjectSC

**서브컬쳐 수집형 RPG 포트폴리오** | 100% AI 코드 생성 실험 프로젝트

[![Notion](https://img.shields.io/badge/Notion-프로젝트_문서-000000?style=flat&logo=notion)](https://www.notion.so/yongjepark/ProejctSC-2f6c3a3accb0814fad18ccb7687567ca)

> 프로그래머의 직접적인 코드 개입 없이 AI만으로 게임 프로젝트를 완성할 수 있을까?

---

## 프로젝트 개요

| 항목 | 내용                                              |
|------|-------------------------------------------------|
| **목표** | AI(Claude Code)만으로 서브컬쳐 수집형 RPG의 OutGame 시스템 구현 |
| **기간** | 2026.01.14 ~ 2026.01.28                         |
| **AI 도구** | Claude Code (Anthropic)                         |
| **Unity** | 2022.3 LTS                                      |

### 실험 목적

1. **AI 단독 개발 가능성 검증**: 프로그래머 개입 없이 AI만으로 프로젝트 진행
2. **추상화 우선 설계**: 프로토타이핑이 아닌 처음부터 확장 가능한 아키텍처 구축
3. **AI 협업 워크플로우 탐구**: 효과적인 AI 활용 방법론 발견

---

## 기술 스택

```
Unity 2022.3 LTS / C# / UniTask / Addressables
```

| 영역 | 기술 |
|------|------|
| **비동기** | UniTask |
| **리소스** | Addressables + LRU 캐싱 |
| **UI** | Widget-State 패턴 (MVP 대체) |
| **서버** | LocalGameServer (Request-Response) |
| **저장** | SaveManager + 마이그레이션 |
| **테스트** | Unity Test Framework (405+ 테스트) |

---

## 아키텍처

```
Assets/Scripts/
├── Foundation/          # 싱글톤, 부트스트랩
├── Core/               # AssetManager, SaveManager, Navigation
├── Common/             # UI 베이스, Pool, Utility
├── Data/               # ScriptableObject, UserSaveData
├── LocalServer/        # 서버 시뮬레이션 레이어
│   ├── Handlers/       # 요청 핸들러
│   ├── Services/       # 비즈니스 로직
│   └── Validators/     # 요청 검증
└── Contents/           # 실제 게임 컨텐츠
    ├── OutGame/        # 로비, 가챠, 상점, 캐릭터
    └── InGame/         # 전투 (미구현)
```

### 핵심 설계 결정

| 결정 | 이유 |
|------|------|
| **Widget-State > MVP** | Unity 컴포넌트 모델에 더 적합 |
| **LocalServer 레이어** | 실서버 전환 대비, 클라이언트 로직 분리 |
| **ScriptableObject > Factory** | Unity 에디터 친화적, 데이터 관리 용이 |

---

## 구현 현황

### OutGame 시스템 (85% 완료)

| 시스템 | 상태 | 설명 |
|--------|------|------|
| **Lobby** | ✅ | 메인 화면, 네비게이션 |
| **Gacha** | ✅ | 확률 시스템, 천장, 히스토리 |
| **Shop** | ✅ | 상품 목록, 구매 플로우 |
| **Character** | ✅ | 목록, 상세, 레벨업 |
| **Inventory** | ✅ | 아이템 관리 |
| **LiveEvent** | ⚠️ | 배너, 상세 (미션/상점 미완) |
| **Stage** | ⚠️ | 스테이지 선택, 파티 편성 |

### InGame 시스템 (미구현, FUTURE)

- Battle 시스템 (State + Command 패턴)
- Skill/Buff 시스템 (Decorator 패턴)

### 상세 구현 목록

[Notion 문서 참조](https://www.notion.so/2f6c3a3accb081cd92e5d58ff5906100)

---

## 프로젝트 회고

### 잘 된 점

- **초반 아키텍처 설계**: AI가 전체 구조를 빠르게 잡아줌
- **에디터 도구 생성**: 필요한 기능을 요청하면 바로 구현
- **Spec 문서 기반 작업**: 명확한 규칙이 있을 때 품질 향상
- **구현 속도**: 이미 충분히 빠름

### 어려웠던 점

- **검증 없는 진행**: 동작 확인 없이 진행 → 큰 기술부채
- **Context 한계**: 한도 초과 시 품질 급락
- **확신의 부재**: "이게 돌아가는지" 모르는 상태에서 진행 → 인지력 소모
- **Unity UI 자동화**: 아직 완벽하지 않음

---

## AI 개발 인사이트

### 효과적인 사용법

```
✅ 아키텍처 논의 및 설계 방향 결정
✅ 코드 최적화/리팩토링 아이디어
✅ 에디터 헬퍼 도구 제작
✅ 계획 수립 후 구현 (한번에 성공률 ↑)
✅ 작업 단위 분리 → 병렬 에이전트 실행
```

### 주의할 점

```
⚠️ Context 부족한 AI = 버그 생산기
⚠️ 매 구현마다 동작 검증 필수
⚠️ 너무 많은 기능을 완결 없이 진행 → 마무리 힘듦
⚠️ Context 한도 초과 시 고장 가능성
```

### 해결해야 할 과제

- 문서 ↔ 코드 자동 동기화 시스템
- Context 유지 전략
- 멀티 에이전트 작업 시 Unity 컴파일 충돌
- Unity 로그 분석 자동화

---

## 결론

### AI 100% 개발은 가능한가?

> **현재로서는 효과보다 비용이 크다.**

그러나:

| 관점 | 평가 |
|------|------|
| **설계/논의** | 매우 유의미 |
| **구현 속도** | 충분히 빠름 |
| **품질 보장** | 검증 환경 필수 |
| **작업 피로도** | 예상보다 높음 |

### 핵심 교훈

1. **AI 구현 → 즉시 테스트** 환경이 필수
2. **충분한 대화**로 방향 설정 후 구현
3. **딸깍 뚝딱이 아니다** — 생각보다 피곤함

---

## 문서

| 문서 | 설명 |
|------|------|
| [ARCHITECTURE.md](Docs/ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [SPEC.md](Docs/SPEC.md) | 시스템 스펙 |
| [STATUS_REPORT.md](Docs/STATUS_REPORT.md) | 구현 현황 보고서 |
| [PROGRESS.md](Docs/PROGRESS.md) | 진행 상황 |

---

## 라이선스

This project is for portfolio purposes.

---

<p align="center">
  <b>100% AI-Generated Code Experiment</b><br>
  Built with Claude Code
</p>
