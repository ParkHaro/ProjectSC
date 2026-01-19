# Data Decisions

데이터 아키텍처 관련 의사결정 기록.

---

## 데이터 아키텍처: 로컬 중심 → 서버 중심

**일자**: 2025-01-15 | **상태**: 결정됨

### 컨텍스트
초기 v1.0은 로컬 중심 설계. 서버 검증 없어 치팅 취약. 포트폴리오로서 실제 서비스 아키텍처 필요.

### 선택지
1. **로컬 중심 유지** - 단순하나 서버 검증 불가, 실무와 괴리
2. **서버 중심 + 로컬 시뮬레이션** - 라이브 아키텍처 (선택)
3. **실제 서버 구현** - 완전하나 범위 초과

### 결정
**서버 중심 (Server Authority) + LocalApiClient 시뮬레이션**
- 인터페이스(IApiClient)만 교체하면 실제 서버 연동 가능
- Delta 패턴으로 부분 갱신 효율화

### 결과
- DataManager: 읽기 전용 뷰만 제공
- SetUserData: 로그인 시 전체 데이터
- ApplyDelta: 이후 액션은 변경분만

### 회고
v1.0 구현 후 리셋 비용 발생. **초기 설계부터 라이브 서비스 기준으로 검토해야 함.**

---

## Delta 패턴 도입

**일자**: 2025-01-15 | **상태**: 결정됨

### 컨텍스트
서버 중심 아키텍처에서 유저 데이터 갱신 방식 결정 필요.

### 선택지
1. **전체 동기화** - 단순하나 네트워크 비효율
2. **Delta 패턴** - 변경분만 전달 (선택)
3. **이벤트 소싱** - 이력 추적 가능하나 오버엔지니어링

### 결정
**Delta 패턴**
- 실제 모바일 게임에서 보편적 사용
- Nullable 타입으로 변경 여부 표현

### 결과
```csharp
public class UserDataDelta
{
    public UserProfile? Profile;
    public UserCurrency? Currency;
    public List<OwnedCharacter> AddedCharacters;
    // ...
}
```

---

## 에디터 설정 ScriptableObject 도입

**일자**: 2025-01-16 | **상태**: 결정됨

### 컨텍스트
MVP 프리팹 생성 시 기본 폰트 적용 필요. 에디터 도구 공통 설정 관리.

### 선택지
1. **하드코딩** - 단순하나 변경 시 코드 수정 필요
2. **EditorPrefs** - 타입 제한, 프로젝트 간 공유 어려움
3. **ScriptableObject** - Inspector 편집, 버전 관리 가능 (선택)

### 결정
**ScriptableObject 기반 설정**
- ProjectEditorSettings SO 생성
- SC Tools > Settings 메뉴

### 결과
기본 폰트, 색상 등 시각적 설정을 Inspector에서 편집.

---

## TimeService 서버 전환 비용 최소화

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
TimeService 구현 시 로컬/서버 환경 차이 고려 필요.

### 결정
**서버 기준 인터페이스 + 로컬 구현**
```csharp
public interface ITimeService
{
    long ServerTimeUtc { get; }
    long TimeOffset { get; }           // 클라-서버 오프셋
    void SyncServerTime(long timestamp);
    // ...
}
```

### 결과
서버 전환 시 구현체만 교체. 인터페이스/호출부 변경 없음.
