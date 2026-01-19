# 자동 탐색 및 MCP 활용 규칙

## 작업별 자동 탐색

| 작업 유형 | 도구 | 탐색 대상 |
|-----------|------|-----------|
| 클래스 수정 | Serena `find_symbol` | 클래스 구조, 메서드 목록 |
| 참조 변경 | Serena `find_referencing_symbols` | 영향 범위 파악 |
| 새 기능 추가 | Read | `Docs/Specs/{Assembly}.md` |
| Unity API | Context7 | `unity`, `unitask`, `dotween` |
| 패턴 확인 | Serena `search_for_pattern` | 유사 기존 코드 |

## 문서 참조 우선순위

1. **대분류**: `Docs/Specs/{Assembly}.md` - 역할, 책임
2. **세부**: `Docs/Specs/{Assembly}/{Class}.md` - 구현 시
3. **아키텍처**: `Docs/ARCHITECTURE.md` - 의존성

## Assembly별 스펙 경로

- Sc.Core → `Docs/Specs/Core.md`
- Sc.Common → `Docs/Specs/Common.md`
- Sc.Data → `Docs/Specs/Data.md`
- Sc.Event → `Docs/Specs/Event.md`
- Sc.Contents.Character → `Docs/Specs/Character.md`
- Sc.Contents.Gacha → `Docs/Specs/Gacha.md`
- Sc.Contents.Lobby → `Docs/Specs/Lobby.md`
- Sc.Contents.Shop → `Docs/Specs/Shop.md`

## MCP 자동 선택 우선순위

1. **심볼 기반 작업** → Serena (가장 정확)
2. **라이브러리 문서** → Context7
3. **파일 검색** → Glob/Grep
4. **복잡한 탐색** → Task(Explore)
5. **기능 설계** → feature-dev 스킬
