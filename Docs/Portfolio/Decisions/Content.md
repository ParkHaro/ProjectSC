# Content Decisions

컨텐츠 시스템 관련 의사결정 기록.

---

## 모듈형 이벤트 서브컨텐츠 (EventSubContent)

**일자**: 2026-01-18 | **상태**: 결정됨

### 컨텍스트
라이브 이벤트 내부 컨텐츠 구성 방식. 이벤트마다 다른 조합 필요.

### 선택지
1. **고정형 구조** - 모든 이벤트가 Mission, Stage, Shop 포함
2. **모듈형 서브컨텐츠** - 필요한 모듈만 배열로 포함 (선택)
3. **상속 기반** - MissionEvent, StageEvent 등 타입별 클래스

### 결정
**모듈형 서브컨텐츠**
```csharp
public enum EventSubContentType { Mission, Stage, Shop, Minigame, Story }

[Serializable]
public struct EventSubContent
{
    public EventSubContentType Type;
    public string ContentId;      // MissionGroupId, StageGroupId 등
    public int TabOrder;
    public string TabNameKey;
}
```

### 결과
기획팀이 코드 변경 없이 이벤트 구성 가능. UI 탭 순서도 데이터로 제어.

---

## 이벤트 재화 정책: 유예 기간 + 자동 전환

**일자**: 2026-01-18 | **상태**: 결정됨

### 컨텍스트
이벤트 종료 후 남은 재화 처리 방식.

### 선택지
1. **즉시 삭제** - 단순하나 유저 불만, CS 이슈
2. **영구 보존** - 유저 친화적이나 재화 종류 무한 증가
3. **유예 기간 후 전환** - 사용 기회 + 정리 가능 (선택)

### 결정
**유예 기간 (7일) 후 범용 재화로 전환**
```csharp
public struct EventCurrencyPolicy
{
    public int GracePeriodDays;         // 유예 기간
    public string ConvertToCurrencyId;  // 전환 대상 (예: gold)
    public float ConversionRate;        // 전환 비율 (예: 0.5)
}
```

### 결과
- 유예 기간 중: 이벤트 상점 이용 가능, 재화 획득 불가
- 유예 종료 시: 남은 재화 × 비율 = 범용 재화, 알림 표시

---

## PresetGroupId 기반 파티 프리셋

**일자**: 2026-01-18 | **상태**: 결정됨

### 컨텍스트
스테이지 시스템에서 컨텐츠별 파티 프리셋 관리 방식.

### 선택지
1. **컨텐츠 타입 Enum** - 타입 안전하나 새 컨텐츠 시 Enum 수정 필요
2. **StageGroupId 직접 연결** - 단순하나 같은 카테고리 공유 불가
3. **PresetGroupId (문자열)** - 유연한 확장 (선택)

### 결정
**PresetGroupId 문자열 기반**
```
daily_{attribute}    # daily_fire, daily_water
boss_{bossId}        # boss_dragon
event_{eventId}      # event_summer2026
```

### 결과
코드 변경 없이 기획 추가 가능. 이벤트별 개별 프리셋 지원.

---

## 시즌패스 구현 범위

**일자**: 2026-01-17 | **상태**: 결정됨

### 컨텍스트
Phase 2 상점 설계 중 시즌패스(Pass) 포함 여부.

### 선택지 (유형)
1. **Type A: 단순 구독형** - Shop 확장으로 충분
2. **Type B: 레벨형 보상트랙** - 별도 시스템 필요
3. **Type C: 미션 기반형** - 미션+포인트+보상트랙 (선택)

### 선택지 (범위)
- 현재 마일스톤 포함 vs **별도 마일스톤으로 분리** (선택)

### 결정
- **Type C (미션 기반형)** - 유저 참여 유도 효과적
- **별도 마일스톤 (PASS-V1)** - 현재 범위 과다 방지

### 결과
ProductType에서 Pass 제외. 핵심 기능 우선 완성 후 확장.
