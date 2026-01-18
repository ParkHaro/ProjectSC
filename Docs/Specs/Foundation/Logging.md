---
type: spec
assembly: Sc.Foundation
category: System
status: approved
version: "1.0"
dependencies: []
created: 2026-01-17
updated: 2026-01-17
---

# 로깅 시스템 (Logging System)

## 목적

게임 전체에서 사용하는 통합 로깅 시스템. 카테고리별 필터링, 출력 대상 확장, 릴리즈 빌드 최적화 지원.

---

## 클래스 역할 정의

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `LogLevel` | 로그 심각도 열거형 | 로그 레벨 분류 | - |
| `LogCategory` | 로그 카테고리 열거형 | 도메인별 로그 분류 | - |
| `Log` | 로깅 진입점 | 레벨/카테고리 필터링, 출력 위임 | 출력 구현 |
| `LogConfig` | 로그 설정 SO | 카테고리별 레벨 설정, 출력 대상 관리 | 런타임 설정 변경 |
| `ILogOutput` | 출력 인터페이스 | 출력 대상 추상화 | 구체적 출력 |
| `UnityLogOutput` | Unity 콘솔 출력 | Debug.Log 호출 | 파일/원격 출력 |

---

## 상세 정의

### LogLevel

**위치**: `Assets/Scripts/Foundation/Logging/LogLevel.cs`

```csharp
public enum LogLevel
{
    Debug = 0,      // 개발 중 상세 정보 (릴리즈 strip)
    Info = 1,       // 일반 정보 (릴리즈 strip)
    Warning = 2,    // 주의 필요
    Error = 3,      // 오류 발생
    None = 99       // 로깅 비활성화
}
```

### LogCategory

**위치**: `Assets/Scripts/Foundation/Logging/LogCategory.cs`

```csharp
public enum LogCategory
{
    System,     // 초기화, 종료, 일반 시스템
    Network,    // API 요청/응답, 연결
    Data,       // 데이터 로드/저장, Delta 적용
    UI,         // 화면 전환, 팝업
    Battle,     // 전투 로직
    Gacha,      // 가챠 결과
}
```

### ILogOutput

**위치**: `Assets/Scripts/Foundation/Logging/ILogOutput.cs`

```csharp
public interface ILogOutput
{
    void Write(LogLevel level, LogCategory category, string message);
}
```

### UnityLogOutput

**위치**: `Assets/Scripts/Foundation/Logging/UnityLogOutput.cs`

**역할**: Unity 콘솔에 로그 출력

**동작**:
- `LogLevel.Debug/Info` → `Debug.Log`
- `LogLevel.Warning` → `Debug.LogWarning`
- `LogLevel.Error` → `Debug.LogError`

**포맷**: `[Category] message`

### LogConfig

**위치**: `Assets/Scripts/Foundation/Logging/LogConfig.cs`

**타입**: ScriptableObject

| 필드 | 타입 | 설명 |
|------|------|------|
| `DefaultLevel` | LogLevel | 기본 로그 레벨 |
| `CategoryLevels` | Dictionary | 카테고리별 레벨 오버라이드 |
| `Outputs` | List<ILogOutput> | 출력 대상 목록 |

**에셋 위치**: `Assets/Data/Config/LogConfig.asset`

### Log

**위치**: `Assets/Scripts/Foundation/Logging/Log.cs`

**역할**: 정적 로깅 유틸리티 (진입점)

---

## 인터페이스

### Log 정적 메서드

| 메서드 | 시그니처 | 빌드 포함 | 설명 |
|--------|----------|-----------|------|
| `Debug` | `void Debug(string msg, LogCategory cat = System)` | Editor/Dev | 상세 정보 |
| `Info` | `void Info(string msg, LogCategory cat = System)` | Editor/Dev | 일반 정보 |
| `Warning` | `void Warning(string msg, LogCategory cat = System)` | 항상 | 주의 |
| `Error` | `void Error(string msg, LogCategory cat = System)` | 항상 | 오류 |
| `Initialize` | `void Initialize(LogConfig config)` | 항상 | 설정 로드 |

### ILogOutput 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| `Write` | `void Write(LogLevel level, LogCategory category, string message)` | 로그 출력 |

---

## 동작 흐름

### 로그 출력 흐름

```
Log.Info("메시지", LogCategory.Network)
              │
              ▼
┌─────────────────────────────┐
│ [Conditional] 체크           │
│ - Editor/Dev → 통과         │
│ - Release → 호출 strip      │
└──────────────┬──────────────┘
               │
               ▼
┌─────────────────────────────┐
│ LogConfig.IsEnabled() 체크   │
│ - 카테고리 레벨 >= 설정 레벨? │
└──────────────┬──────────────┘
               │ Yes
               ▼
┌─────────────────────────────┐
│ foreach (output in Outputs) │
│   output.Write(level, cat,  │
│                message)     │
└─────────────────────────────┘
```

### 초기화 흐름

```
GameBootstrap.Awake()
        │
        ▼
Log.Initialize(logConfig)
        │
        ├─ LogConfig 에셋 참조
        └─ 기본 Output 등록 (UnityLogOutput)
```

---

## 빌드 최적화

### Conditional 어트리뷰트

```csharp
[Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
public static void Debug(string message, LogCategory category = LogCategory.System)
{
    WriteInternal(LogLevel.Debug, category, message);
}

[Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
public static void Info(string message, LogCategory category = LogCategory.System)
{
    WriteInternal(LogLevel.Info, category, message);
}

// Warning, Error는 Conditional 없음 (항상 포함)
```

### 빌드별 동작

| 빌드 타입 | Debug | Info | Warning | Error |
|-----------|-------|------|---------|-------|
| Editor | ✅ | ✅ | ✅ | ✅ |
| Development Build | ✅ | ✅ | ✅ | ✅ |
| Release Build | ❌ strip | ❌ strip | ✅ | ✅ |

---

## 사용 패턴

### 기본 사용

```csharp
Log.Info("게임 시작");
Log.Debug($"유저 ID: {userId}", LogCategory.System);
Log.Warning("스태미나 부족", LogCategory.UI);
Log.Error("네트워크 연결 실패", LogCategory.Network);
```

### 초기화 (GameBootstrap)

```csharp
[SerializeField] private LogConfig _logConfig;

private void Awake()
{
    Log.Initialize(_logConfig);
}
```

### 카테고리별 필터링 예시

```csharp
// LogConfig 설정:
// - DefaultLevel: Info
// - Network: Debug (상세 로깅)
// - Battle: Warning (중요한 것만)

Log.Debug("API 호출", LogCategory.Network);  // 출력됨
Log.Debug("데미지 계산", LogCategory.Battle); // 필터링됨
Log.Info("전투 시작", LogCategory.Battle);    // 필터링됨
Log.Warning("보스 등장", LogCategory.Battle); // 출력됨
```

---

## 확장 가능성

### 파일 출력 (향후)

```csharp
public class FileLogOutput : ILogOutput
{
    public void Write(LogLevel level, LogCategory category, string message)
    {
        // 파일에 기록
    }
}
```

### 원격 로깅 (향후)

```csharp
public class RemoteLogOutput : ILogOutput
{
    public void Write(LogLevel level, LogCategory category, string message)
    {
        // 서버로 전송 (Error 레벨만)
    }
}
```

---

## 주의사항

1. **Initialize 필수**: Log 사용 전 반드시 `Log.Initialize()` 호출
2. **문자열 보간 주의**: 릴리즈에서 strip되어도 문자열 생성은 발생
   ```csharp
   // 비효율적 (문자열 항상 생성)
   Log.Debug($"복잡한 계산: {ExpensiveCalculation()}");

   // 권장 (레벨 체크 후 생성)
   if (Log.IsEnabled(LogLevel.Debug, LogCategory.System))
       Log.Debug($"복잡한 계산: {ExpensiveCalculation()}");
   ```
3. **순환 참조 금지**: Foundation은 다른 Assembly 참조 불가
4. **스레드 안전성**: Unity 메인 스레드에서만 호출

---

## 관련 문서

- [Foundation.md](../Foundation.md) - 상위 문서
- [Error.md](Error.md) - 에러 처리 시스템
