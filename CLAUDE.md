# ProjectSC

서브컬쳐 수집형 RPG 포트폴리오 프로젝트

## 핵심 규칙

- Assembly 접두사: `Sc.`
- 네임스페이스: `Sc.{폴더명}` (예: `Sc.Core`, `Sc.Contents.Character`)
- 인터페이스: `I` 접두사, private 필드: `_` 접두사
- 파일당 클래스 1개
- 컨텐츠 간 통신: 이벤트 기반 (직접 참조 최소화)

**기술 스택**: Unity 2022.x / C# / UniTask / Addressables

## 자동 탐색 규칙

작업 시 관련 정보를 자동으로 탐색:

| 작업 유형 | 자동 탐색 대상 |
|-----------|----------------|
| 클래스 수정 | Serena `find_symbol` → 해당 클래스 구조 파악 |
| 새 기능 추가 | `Docs/Specs/{Assembly}.md` → 아키텍처 확인 |
| 참조 변경 | Serena `find_referencing_symbols` → 영향 범위 파악 |
| 패턴 확인 | 유사 기존 코드 검색 → 컨벤션 파악 |

### 문서 참조 우선순위

1. **대분류 먼저**: `Docs/Specs/{Assembly}.md` - 역할, 책임, 관계
2. **세부 문서**: `Docs/Specs/{Assembly}/{Class}.md` - 구현 시에만
3. **아키텍처**: `Docs/ARCHITECTURE.md` - 의존성, 폴더 구조

### Assembly별 스펙 문서

| Assembly | 스펙 문서 |
|----------|-----------|
| Sc.Core | `Docs/Specs/Core.md` |
| Sc.Common | `Docs/Specs/Common.md` |
| Sc.Data | `Docs/Specs/Data.md` |
| Sc.Event | `Docs/Specs/Event.md` |
| Sc.Contents.* | `Docs/Specs/{ContentName}.md` |

## MCP 도구 활용

> 글로벌 설정의 자동 선택 규칙 따름. 프로젝트 특화 사항만 기술.

- **Unity API 확인**: Context7 → `unity`, `unitask`, `dotween` 등
- **기존 코드 탐색**: Serena 우선 (심볼 기반이 더 정확)
- **새 기능 구현**: `/feature-dev` 스킬 활용

## 필수 작업 절차

### Progress 추적
1. 작업 시작 전: `Docs/PROGRESS.md` 확인
2. 작업 중: TodoWrite로 진행상황 추적
3. 완료 시: Progress 업데이트 (`- [ ]` → `- [x]`)

### 코드 작업 전
- 관련 스펙 문서(대분류) 먼저 확인
- 기존 패턴 파악 후 작업

## 커밋 규칙

- **제목**: 영어 (동사 원형 시작)
- **본문**: 한국어
- AI 관련 문구 추가 금지

```
Add feature name       ← 영어 제목

- 기능 설명 한국어로   ← 한국어 본문
```

## 포트폴리오 기록

의사결정 시 `Docs/Portfolio/DECISIONS.md`에 기록:
- 컨텍스트, 선택지, 결정 이유

## 참조 문서

| 문서 | 용도 |
|------|------|
| [PROGRESS.md](Docs/PROGRESS.md) | 현재 작업 상태 |
| [ARCHITECTURE.md](Docs/ARCHITECTURE.md) | 폴더 구조, 의존성 |
| [CONVENTIONS.md](Docs/CONVENTIONS.md) | 코딩 규칙 |
| [SPEC_INDEX.md](Docs/Specs/SPEC_INDEX.md) | Assembly 목록 |
