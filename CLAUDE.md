# ProjectSC

서브컬쳐 수집형 RPG 포트폴리오 프로젝트

## 문서 참조
- [스펙 개요](Docs/Specs/SPEC_INDEX.md) - Assembly 목록 및 상태
- [진행상황](Docs/PROGRESS.md) - 현재 작업 상태
- [아키텍처](Docs/ARCHITECTURE.md) - 폴더 구조, 의존성
- [컨벤션](Docs/CONVENTIONS.md) - 코딩 규칙
- [문서 작성 규칙](Docs/DOC_RULES.md) - 문서 형식 및 메타데이터
- [포트폴리오](Docs/Portfolio/README.md) - AI 협업 과정 기록

## 핵심 규칙
- Assembly 접두사: `Sc.`
- 네임스페이스: `Sc.{폴더명}` (예: `Sc.Core`, `Sc.Contents.Character`)
- 인터페이스: `I` 접두사
- private 필드: `_` 접두사
- 파일당 클래스 1개
- 컨텐츠 간 통신: 이벤트 기반 (직접 참조 최소화)

## 기술 스택
Unity 2022.x / C# / UniTask / Addressables

## MCP 플러그인 활용

> **원칙**: 작업 유형에 맞는 플러그인을 적극 활용할 것

| 작업 유형 | 플러그인 | 용도 |
|-----------|----------|------|
| Unity API/라이브러리 문서 | **Context7** | Unity API, DOTween, UniTask 등 공식 문서 조회 |
| 코드 탐색/분석 | **Serena** | 심볼 검색, 참조 추적, 코드베이스 탐색 |
| 기능 개발 | **feature-dev** | 아키텍처 분석, 구현 설계 |
| 코드 리뷰 | **code-review** | PR 코드 리뷰, 변경사항 분석 |
| UI 작업 | **frontend-design** | UI 컴포넌트 디자인, 레이아웃 구현 |
| Git 작업 | **commit-commands** | 커밋, 푸시, PR 생성, 브랜치 정리 |

### 사용 시점
- **Unity API 확인 필요**: Context7로 최신 문서 조회 (예: `UniTask`, `Addressables` 사용법)
- **기존 코드 파악 필요**: Serena로 심볼/참조 탐색 (예: 특정 클래스 사용처 찾기)
- **새 기능 구현**: feature-dev로 설계 → Serena로 기존 패턴 확인 → 구현
- **UI 구현**: frontend-design으로 컴포넌트 설계
- **커밋/PR**: commit-commands 스킬 활용 (`/commit`, `/commit-push-pr`)

### 조합 예시
```
새 시스템 구현:
1. feature-dev:code-explorer → 기존 유사 시스템 분석
2. Context7 → 사용할 라이브러리 API 확인
3. feature-dev:code-architect → 구현 설계
4. Serena → 코드 작성 및 심볼 관리
5. code-review → 코드 리뷰
6. commit-commands → 커밋
```

## 문서 규칙

> **필수**: 문서 작업 전 [문서 작성 규칙](Docs/DOC_RULES.md)을 반드시 먼저 읽을 것

### 문서-코드 관계
- **문서**: What, Why 정의 (역할, 책임, 인터페이스)
- **코드**: How 구현 (실제 로직)
- **느슨한 결합**: 문서는 코드 전체를 포함하지 않음
- **사용 패턴**: 1-5줄 예시만 (전체 구현 X)

### 문서 참조 순서
1. **대분류 문서 우선**: `Docs/Specs/{Assembly}.md`
   - 클래스 역할, 책임, 관계 파악
   - 대부분의 작업은 대분류만으로 충분
2. **세부 문서 참조**: `Docs/Specs/{Assembly}/{Class}.md`
   - 구현 착수 시 인터페이스, 동작 흐름 확인
3. **메타데이터 활용**: YAML frontmatter로 문서 필터링 가능

### 문서 수정 시 주의
- 코드 변경 → 문서 자동 업데이트 아님 (수동 동기화)
- 문서에 전체 코드 포함 금지
- 메타데이터 형식 준수 (DOC_RULES.md 참조)

## 필수 작업 절차

### Progress 추적 (필수)
1. **작업 시작 전**: [Docs/PROGRESS.md](Docs/PROGRESS.md) 확인
2. **항목 없으면**: 해당 작업을 작업 로그에 추가 (`- [ ] 작업내용`)
3. **항목 있으면**: 현재 상태 파악 후 이어서 진행
4. **단계 완료 시**: 즉시 Progress 업데이트 (`- [ ]` → `- [x]`)
5. **작업 완료 시**: 관련 테이블 상태도 업데이트 (⬜ → 🔨 → ✅)

### 문서/코드 작업 전
- **문서 작업**: [Docs/DOC_RULES.md](Docs/DOC_RULES.md) 먼저 읽을 것
- **코드 작업**: 관련 스펙 문서(대분류) 먼저 확인할 것

### AI 에디터 도구
- **씬/프리팹 셋업 필요 시**: Editor 스크립트 생성 우선 제안
- **도구 문서**: [Docs/Specs/Editor/AITools.md](Docs/Specs/Editor/AITools.md) 참조

## 포트폴리오 기록 (필수)

> **핵심**: 모든 의사결정과 고민 과정을 포트폴리오로 활용할 수 있도록 기록

### 기록 대상
- 아키텍처/설계 결정
- 기술 선택 및 트레이드오프
- 문제 해결 과정
- AI와의 토론에서 나온 인사이트

### 기록 절차
1. **의사결정 발생 시**: [DECISIONS.md](Docs/Portfolio/DECISIONS.md)에 기록
   - 컨텍스트, 선택지, 결정 이유, 결과
2. **주요 마일스톤 시**: [JOURNEY.md](Docs/Portfolio/JOURNEY.md) 업데이트
   - 해당 Phase에 커밋 내용 추가
3. **새로운 협업 패턴 발견 시**: [AI_COLLABORATION.md](Docs/Portfolio/AI_COLLABORATION.md) 보완

### 기록 원칙
- "왜?"에 대한 답이 있어야 함 (결과만 X)
- 실패한 시도도 기록 (배운 점 포함)
- 코드보다 맥락과 근거 중심

## 커밋 규칙
- **제목**: 영어로 작성 (동사 원형으로 시작)
- **본문**: 한국어로 작성
- AI 사용 관련 문구 추가 금지 (Co-Authored-By 등)

```
Add feature name       ← 영어 제목

- 기능 설명 한국어로   ← 한국어 본문
- 변경 사항 상세
```
