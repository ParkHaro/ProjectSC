# 문서 작성 규칙

## 핵심 원칙

**문서와 코드의 느슨한 결합**
- 문서: "무엇을, 왜" (개념, 계약, 의도)
- 코드: "어떻게" (구현, 로직)

---

## 문서에 포함 / 미포함

| 포함 O | 미포함 X |
|--------|----------|
| 역할, 책임 정의 | 전체 구현 코드 |
| 인터페이스 시그니처 (계약) | 내부 로직 상세 |
| 동작 흐름 (텍스트/다이어그램) | private 멤버 |
| 사용 패턴 (1-5줄 예시) | 에러 처리 상세 |
| 주의사항, 제약조건 | 최적화 코드 |
| 관련 문서 링크 | 테스트 코드 |

---

## 메타데이터 스키마

모든 스펙 문서는 YAML frontmatter 포함:

```yaml
---
type: spec | guide | index
assembly: Sc.{Name}
class: ClassName              # 세부 문서만
category: Enum | Struct | SO | Manager | System | UI | Pool | Utility | Event
status: draft | review | approved
version: "1.0"
dependencies: []              # 의존 클래스명 배열
created: YYYY-MM-DD
updated: YYYY-MM-DD
---
```

### 필드 설명

| 필드 | 필수 | 설명 |
|------|------|------|
| type | O | spec(스펙), guide(가이드), index(목록) |
| assembly | O | 소속 Assembly |
| class | 세부만 | 대상 클래스명 |
| category | O | 분류 |
| status | O | draft→review→approved |
| version | O | 스펙 버전 (SemVer) |
| dependencies | O | 의존 클래스 목록 |
| created | O | 최초 작성일 |
| updated | O | 최종 수정일 |

---

## 대분류 문서 템플릿

```markdown
---
type: spec
assembly: Sc.{Name}
category: {Category}
status: draft
version: "1.0"
created: YYYY-MM-DD
updated: YYYY-MM-DD
---

# Sc.{Name}

## 목적
(한 문장)

## 의존성
- 참조: ...
- 참조됨: ...

## 클래스 역할 정의

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|

## 관계도
(다이어그램)

## 설계 원칙
1. ...

## 상세 문서
- [Class.md](Assembly/Class.md)

## 상태
| 분류 | 상태 |
```

---

## 세부 문서 템플릿

```markdown
---
type: spec
assembly: Sc.{Name}
class: ClassName
category: {Category}
status: draft
version: "1.0"
dependencies: []
created: YYYY-MM-DD
updated: YYYY-MM-DD
---

# ClassName

## 역할
(한 문장)

## 책임
- ...

## 비책임
- ...

## 인터페이스

| 멤버 | 타입/시그니처 | 설명 |
|------|---------------|------|

## 동작 흐름
(텍스트 또는 다이어그램)

## 사용 패턴
(1-5줄 핵심 예시만)

## 주의사항
- ...

## 관련
- [Related.md](Related.md)
```

---

## 코드 예시 작성 규칙

### 허용
```csharp
// 호출 패턴 (1-3줄)
EventManager.Instance.Subscribe<BattleEndEvent>(OnBattleEnd);
EventManager.Instance.Publish(new BattleEndEvent { Result = result });
```

### 금지
```csharp
// 전체 클래스 구현 (XX)
public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<Type, Delegate> _events = new();

    public void Subscribe<T>(Action<T> callback) where T : struct
    {
        // ... 전체 구현
    }
    // ... 나머지 메서드들
}
```

---

## 문서 상태 흐름

```
draft → review → approved
  ↑        │
  └────────┘ (수정 필요 시)
```

| 상태 | 의미 | 다음 단계 |
|------|------|-----------|
| draft | 초안 작성 중 | 검토 요청 |
| review | 검토 중 | 승인 또는 수정 |
| approved | 승인됨 | 구현 가능 |

---

## 버전 관리

- Major: 구조 변경, 인터페이스 변경
- Minor: 기능 추가, 필드 추가
- Patch: 오타 수정, 설명 보완

예: `1.0` → `1.1` (필드 추가) → `2.0` (인터페이스 변경)
