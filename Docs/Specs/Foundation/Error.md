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

# 에러 처리 시스템 (Error System)

## 목적

게임 전체에서 사용하는 통합 에러 처리 시스템. 에러 코드 분류, 다국어 메시지 연동, Result<T> 패턴 지원.

---

## 클래스 역할 정의

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `ErrorCode` | 에러 코드 열거형 | 에러 종류 분류 | 메시지 제공 |
| `ErrorMessages` | 에러 메시지 매핑 | ErrorCode → StringData 키 변환 | 다국어 처리 |
| `Result<T>` | 결과 래퍼 구조체 | 성공/실패 명시적 전달 | 에러 복구 |

---

## 상세 정의

### ErrorCode

**위치**: `Assets/Scripts/Foundation/Error/ErrorCode.cs`

**번호 대역**:

| 대역 | 카테고리 | 설명 |
|------|----------|------|
| 0 | None | 에러 없음 |
| 1000~1999 | System | 초기화, 설정 오류 |
| 2000~2999 | Network | 연결, 타임아웃, 서버 오류 |
| 3000~3999 | Data | 저장/로드, 파싱 오류 |
| 4000~4999 | Auth | 로그인, 인증 오류 |
| 5000~5999 | Game | 재화 부족, 조건 미충족 |
| 6000~6999 | UI | 화면 로드 실패 |

```csharp
public enum ErrorCode
{
    None = 0,

    // System (1000~)
    SystemInitFailed = 1001,
    ConfigLoadFailed = 1002,

    // Network (2000~)
    NetworkDisconnected = 2001,
    NetworkTimeout = 2002,
    ServerError = 2003,
    InvalidResponse = 2004,

    // Data (3000~)
    SaveFailed = 3001,
    LoadFailed = 3002,
    ParseFailed = 3003,
    MigrationFailed = 3004,

    // Auth (4000~)
    LoginFailed = 4001,
    SessionExpired = 4002,
    InvalidToken = 4003,

    // Game (5000~)
    InsufficientGold = 5001,
    InsufficientGem = 5002,
    InsufficientStamina = 5003,
    InventoryFull = 5004,
    LevelNotMet = 5005,
    AlreadyOwned = 5006,
    PurchaseLimitReached = 5007,
    StageNotCleared = 5008,

    // UI (6000~)
    ScreenLoadFailed = 6001,
    PopupLoadFailed = 6002,
}
```

### ErrorMessages

**위치**: `Assets/Scripts/Foundation/Error/ErrorMessages.cs`

**역할**: ErrorCode → StringData 키 매핑

```csharp
public static class ErrorMessages
{
    // ErrorCode → StringData 키 매핑
    private static readonly Dictionary<ErrorCode, string> _keys = new()
    {
        { ErrorCode.None, "" },

        // Network
        { ErrorCode.NetworkDisconnected, "error.network.disconnected" },
        { ErrorCode.NetworkTimeout, "error.network.timeout" },
        { ErrorCode.ServerError, "error.network.server_error" },

        // Data
        { ErrorCode.SaveFailed, "error.data.save_failed" },
        { ErrorCode.LoadFailed, "error.data.load_failed" },

        // Game
        { ErrorCode.InsufficientGold, "error.game.insufficient_gold" },
        { ErrorCode.InsufficientGem, "error.game.insufficient_gem" },
        { ErrorCode.InsufficientStamina, "error.game.insufficient_stamina" },
        // ...
    };

    /// <summary>
    /// ErrorCode에 해당하는 StringData 키 반환
    /// </summary>
    public static string GetKey(ErrorCode code);

    /// <summary>
    /// ErrorCode에 해당하는 다국어 메시지 반환
    /// StringManager 연동 (Sc.Core)
    /// </summary>
    public static string GetMessage(ErrorCode code);
}
```

**StringManager 연동**:
- Foundation은 Sc.Core를 참조할 수 없음
- `GetMessage()`는 델리게이트 주입 방식으로 구현

```csharp
public static class ErrorMessages
{
    // 외부에서 주입 (GameBootstrap에서 설정)
    public static Func<string, string> LocalizeFunc { get; set; }

    public static string GetMessage(ErrorCode code)
    {
        var key = GetKey(code);
        if (string.IsNullOrEmpty(key)) return string.Empty;

        // LocalizeFunc가 설정되어 있으면 사용, 아니면 키 반환
        return LocalizeFunc?.Invoke(key) ?? key;
    }
}
```

### Result<T>

**위치**: `Assets/Scripts/Foundation/Error/Result.cs`

```csharp
public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public ErrorCode Error { get; }
    public string Message { get; }

    private Result(bool success, T value, ErrorCode error, string message)
    {
        IsSuccess = success;
        Value = value;
        Error = error;
        Message = message;
    }

    // 팩토리 메서드
    public static Result<T> Success(T value)
        => new(true, value, ErrorCode.None, null);

    public static Result<T> Failure(ErrorCode error)
        => new(false, default, error, ErrorMessages.GetMessage(error));

    public static Result<T> Failure(ErrorCode error, string customMessage)
        => new(false, default, error, customMessage);

    // 암시적 변환 (성공 케이스 간소화)
    public static implicit operator Result<T>(T value)
        => Success(value);
}
```

---

## 인터페이스

### ErrorMessages

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| `GetKey` | `string GetKey(ErrorCode code)` | 에러 코드 → StringData 키 |
| `GetMessage` | `string GetMessage(ErrorCode code)` | 에러 코드 → 다국어 메시지 |

### Result<T>

| 멤버 | 타입 | 설명 |
|------|------|------|
| `IsSuccess` | bool | 성공 여부 |
| `IsFailure` | bool | 실패 여부 |
| `Value` | T | 성공 시 값 |
| `Error` | ErrorCode | 실패 시 에러 코드 |
| `Message` | string | 에러 메시지 |
| `Success()` | static | 성공 Result 생성 |
| `Failure()` | static | 실패 Result 생성 |

---

## 동작 흐름

### 에러 발생 및 전달

```
[Service Layer]
        │
        │ 에러 발생
        ▼
return Result<T>.Failure(ErrorCode.InsufficientGold)
        │
        ▼
┌─────────────────────────────────┐
│ ErrorMessages.GetMessage()      │
│   → GetKey(code)                │
│   → LocalizeFunc(key)           │
│   → "골드가 부족합니다" (Ko)     │
└─────────────────────────────────┘
        │
        ▼
Result<T> { IsSuccess=false, Error=5001, Message="골드가 부족합니다" }
        │
        ▼
[UI Layer - 메시지 표시]
```

### LocalizeFunc 주입 흐름

```
GameBootstrap.Awake()
        │
        ▼
StringManager.Initialize()
        │
        ▼
ErrorMessages.LocalizeFunc = StringManager.Get;
```

---

## 사용 패턴

### 서비스에서 Result 반환

```csharp
public Result<PurchaseResult> Purchase(string productId)
{
    if (userData.Gold < product.Price)
        return Result<PurchaseResult>.Failure(ErrorCode.InsufficientGold);

    // 구매 처리...
    return new PurchaseResult { ... }; // 암시적 변환으로 Success
}
```

### 호출부에서 Result 처리

```csharp
var result = shopService.Purchase(productId);

if (result.IsFailure)
{
    ShowErrorPopup(result.Message);
    return;
}

// 성공 처리
var purchaseResult = result.Value;
```

### 체이닝 패턴 (선택적 확장)

```csharp
public Result<T> OnSuccess(Action<T> action)
{
    if (IsSuccess) action(Value);
    return this;
}

public Result<T> OnFailure(Action<ErrorCode, string> action)
{
    if (IsFailure) action(Error, Message);
    return this;
}

// 사용
shopService.Purchase(productId)
    .OnSuccess(r => ShowRewardPopup(r.Rewards))
    .OnFailure((code, msg) => ShowErrorPopup(msg));
```

---

## StringData 연동

### StringData 테이블 예시

```json
[
  { "Id": "error.network.timeout", "Ko": "서버 응답이 없습니다", "En": "Server not responding" },
  { "Id": "error.network.disconnected", "Ko": "네트워크 연결이 끊어졌습니다", "En": "Network disconnected" },
  { "Id": "error.game.insufficient_gold", "Ko": "골드가 부족합니다", "En": "Not enough gold" },
  { "Id": "error.game.insufficient_gem", "Ko": "젬이 부족합니다", "En": "Not enough gems" },
  { "Id": "error.game.insufficient_stamina", "Ko": "스태미나가 부족합니다", "En": "Not enough stamina" }
]
```

### 키 네이밍 규칙

```
error.{category}.{specific_error}

예:
error.network.timeout
error.network.disconnected
error.data.save_failed
error.game.insufficient_gold
error.auth.login_failed
```

---

## 주의사항

1. **Foundation 무의존성 유지**
   - StringManager(Sc.Core)를 직접 참조하지 않음
   - 델리게이트 주입으로 연동

2. **Result<T>는 struct**
   - 힙 할당 없음, 성능 고려
   - readonly로 불변성 보장

3. **에러 코드 추가 시**
   - ErrorCode enum에 추가
   - ErrorMessages._keys에 키 매핑 추가
   - StringData에 다국어 메시지 추가

4. **기본 메시지 fallback**
   - LocalizeFunc 미설정 시 키 문자열 반환
   - StringData에 키 없을 시 빈 문자열

---

## 관련 문서

- [Foundation.md](../Foundation.md) - 상위 문서
- [Logging.md](Logging.md) - 로깅 시스템
- [Data/StringData.md](../Data/StringData.md) - 다국어 텍스트 테이블
