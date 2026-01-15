# ProjectSC

서브컬쳐 수집형 RPG 포트폴리오 프로젝트

## 문서 참조
- [스펙 개요](Docs/Specs/SPEC_INDEX.md) - Assembly 목록 및 상태
- [진행상황](Docs/PROGRESS.md) - 현재 작업 상태
- [아키텍처](Docs/ARCHITECTURE.md) - 폴더 구조, 의존성
- [컨벤션](Docs/CONVENTIONS.md) - 코딩 규칙
- [문서 작성 규칙](Docs/DOC_RULES.md) - 문서 형식 및 메타데이터

## 핵심 규칙
- Assembly 접두사: `Sc.`
- 네임스페이스: `Sc.{폴더명}` (예: `Sc.Core`, `Sc.Contents.Character`)
- 인터페이스: `I` 접두사
- private 필드: `_` 접두사
- 파일당 클래스 1개
- 컨텐츠 간 통신: 이벤트 기반 (직접 참조 최소화)

## 기술 스택
Unity 2022.x / C# / UniTask / Addressables

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

## 커밋 규칙
- 커밋 메시지에 AI 사용 관련 문구 추가 금지 (Co-Authored-By 등)
