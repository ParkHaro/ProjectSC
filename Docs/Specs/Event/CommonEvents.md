---
type: spec
assembly: Sc.Event
class: SceneLoadedEvent, ErrorEvent
category: Event
status: draft
version: "1.0"
dependencies: [SceneType]
created: 2025-01-14
updated: 2025-01-14
---

# Event - Common Events

## 역할
게임 전역, 시스템 레벨 이벤트. 씬 관리, 에러 처리 등.

---

## SceneLoadedEvent

씬 로드 완료 알림.

### 페이로드

| 필드 | 타입 | 설명 |
|------|------|------|
| SceneType | SceneType | 로드된 씬 종류 |
| LoadTime | float | 로드 소요 시간 (초) |

### 흐름
```
발행: SceneLoader (씬 로드 완료 후)
  ↓
구독: GameManager (게임 상태 전이)
구독: UI (로딩 화면 닫기)
```

### 사용 패턴
```csharp
EventManager.Instance.Subscribe<SceneLoadedEvent>(OnSceneLoaded);
```

---

## ErrorEvent

에러 발생 알림.

### 페이로드

| 필드 | 타입 | 설명 |
|------|------|------|
| ErrorCode | int | 에러 코드 |
| Message | string | 에러 메시지 |
| IsFatal | bool | 치명적 에러 여부 |

### ErrorCode 범위

| 범위 | 분류 |
|------|------|
| 1000 ~ 1999 | 네트워크 |
| 2000 ~ 2999 | 리소스 |
| 3000 ~ 3999 | 데이터 |
| 4000 ~ 4999 | 시스템 |

### 흐름
```
발행: Any (에러 발생 시)
  ↓
구독: ErrorHandler (팝업 표시, 로그 기록)
```

### 처리 정책
- `IsFatal = true`: 게임 종료 또는 타이틀 복귀
- `IsFatal = false`: 토스트 메시지

---

## 설계 원칙

1. **readonly struct**: 불변 이벤트
2. **최소 페이로드**: 필요한 정보만
3. **단방향**: 알림만, 응답 없음

## 관련
- [Event.md](../Event.md)
- [Core/SceneLoader.md](../Core/SceneLoader.md)
