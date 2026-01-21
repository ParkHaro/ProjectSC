# LocalServer

> Assembly: `Sc.LocalServer`
> 네임스페이스: `Sc.LocalServer`

로컬 서버 시뮬레이션 레이어. 실제 서버 없이 클라이언트 개발/테스트를 위한 모의 서버 구현.

---

## 역할

- 서버 API 요청 처리 시뮬레이션
- 유저 데이터 검증 및 변경
- 보상 지급 및 Delta 생성
- 시간 기반 리셋/제한 처리

---

## 아키텍처

```
┌─────────────────────────────────────────────────────────┐
│                    LocalGameServer                       │
│  (진입점, 요청 라우팅, 외부 데이터 주입)                   │
└─────────────────────┬───────────────────────────────────┘
                      │
        ┌─────────────┼─────────────┐
        ▼             ▼             ▼
┌───────────┐  ┌───────────┐  ┌───────────┐
│  Handler  │  │  Handler  │  │  Handler  │
│ (Login)   │  │ (Gacha)   │  │ (Shop)    │  ...
└─────┬─────┘  └─────┬─────┘  └─────┬─────┘
      │              │              │
      └──────────────┼──────────────┘
                     ▼
        ┌────────────────────────┐
        │       Services         │
        │ (Validator, Reward,    │
        │  Time, GachaService)   │
        └────────────────────────┘
```

---

## 폴더 구조

```
Assets/Scripts/LocalServer/
├── LocalGameServer.cs           # 진입점
├── Sc.LocalServer.asmdef
│
├── Handlers/
│   ├── IRequestHandler.cs       # 핸들러 인터페이스
│   ├── LoginHandler.cs
│   ├── GachaHandler.cs
│   ├── ShopHandler.cs
│   ├── StageHandler.cs
│   ├── EventHandler.cs
│   ├── CharacterLevelUpHandler.cs    # 캐릭터 레벨업
│   └── CharacterAscensionHandler.cs  # 캐릭터 돌파
│
├── Services/
│   ├── ServerTimeService.cs     # 서버 시간 관리
│   ├── GachaService.cs          # 가챠 확률/비용 계산
│   ├── RewardService.cs         # 보상 지급/Delta 생성
│   ├── PurchaseLimitValidator.cs # 구매 제한 검증
│   ├── StageEntryValidator.cs   # 스테이지 입장 검증
│   └── EventCurrencyConverter.cs # 이벤트 재화 전환
│
└── Validators/
    └── ServerValidator.cs       # 공통 검증 (재화, 권한)
```

---

## 핵심 클래스

### LocalGameServer

진입점. 요청을 적절한 Handler로 라우팅.

```csharp
public class LocalGameServer
{
    // 핸들러
    private LoginHandler _loginHandler;
    private GachaHandler _gachaHandler;
    private ShopHandler _shopHandler;
    private StageHandler _stageHandler;
    private EventHandler _eventHandler;

    // 서비스
    private ServerTimeService _timeService;

    // 요청 처리
    public TResponse HandleRequest<TRequest, TResponse>(TRequest request, UserSaveData userData);

    // 이벤트 요청 (오버로드)
    public GetActiveEventsResponse HandleEventRequest(GetActiveEventsRequest request, UserSaveData userData);
    public VisitEventResponse HandleEventRequest(VisitEventRequest request, UserSaveData userData);
    public ClaimEventMissionResponse HandleEventRequest(ClaimEventMissionRequest request, UserSaveData userData);

    // 외부 데이터 주입
    public void SetShopProductDatabase(ShopProductDatabase database);
    public void SetStageDataProvider(Func<string, StageDataInfo> provider);
    public void SetEventDatabase(LiveEventDatabase database);

    // 서버 시간
    public DateTime GetServerTime();
}
```

### IRequestHandler<TRequest, TResponse>

핸들러 표준 인터페이스.

```csharp
public interface IRequestHandler<TRequest, TResponse>
{
    TResponse Handle(TRequest request, UserSaveData userData);
}
```

---

## Handlers

### LoginHandler

로그인 요청 처리. 신규 유저 생성 또는 기존 데이터 반환.

| 메서드 | 설명 |
|--------|------|
| `Handle(LoginRequest, UserSaveData)` | 로그인 처리, LoginResponse 반환 |

### GachaHandler

가챠 요청 처리. 확률 계산, 캐릭터 지급, 천장 업데이트.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_gachaService` | GachaService | 확률/비용 계산 |
| `_rewardService` | RewardService | 보상 지급 |
| `_validator` | ServerValidator | 재화 검증 |
| `_timeService` | ServerTimeService | 시간 서비스 |

| 메서드 | 설명 |
|--------|------|
| `Handle(GachaRequest, UserSaveData)` | 가챠 실행, 결과 반환 |
| `UpdatePityInfo()` | 천장 정보 업데이트 |

### ShopHandler

상점 구매 요청 처리. 구매 제한 검증, 재화 차감, 상품 지급.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_productDatabase` | ShopProductDatabase | 상품 데이터 |
| `_limitValidator` | PurchaseLimitValidator | 구매 제한 검증 |
| `_rewardService` | RewardService | 보상 지급 |
| `_validator` | ServerValidator | 재화 검증 |
| `_timeService` | ServerTimeService | 시간 서비스 |

| 메서드 | 설명 |
|--------|------|
| `Handle(ShopPurchaseRequest, UserSaveData)` | 구매 처리 |
| `SetProductDatabase(database)` | 상품 DB 주입 |
| `HasEnoughCurrency()` | 재화 보유 확인 |
| `DeductCurrency()` | 재화 차감 |

### StageHandler

스테이지 입장/클리어 요청 처리.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_stageDataProvider` | Func<string, StageDataInfo> | 스테이지 데이터 제공자 |
| `_entryValidator` | StageEntryValidator | 입장 제한 검증 |
| `_rewardService` | RewardService | 보상 지급 |
| `_validator` | ServerValidator | 재화 검증 |
| `_timeService` | ServerTimeService | 시간 서비스 |

| 메서드 | 설명 |
|--------|------|
| `HandleEnterStage(EnterStageRequest, UserSaveData)` | 입장 처리 |
| `HandleClearStage(ClearStageRequest, UserSaveData)` | 클리어 처리 |
| `EvaluateStarConditions()` | 별 조건 평가 |
| `SetStageDataProvider(provider)` | 데이터 제공자 주입 |

**StageDataInfo** (전달용 구조체)
```csharp
public class StageDataInfo
{
    public string Id;
    public bool IsEnabled;
    public LimitType LimitType;
    public int LimitCount;
    public CostType EntryCostType;
    public int EntryCost;
    public string UnlockConditionStageId;
    public DayOfWeek[] AvailableDays;
    public List<StarConditionInfo> StarConditions;
    public List<RewardInfo> FirstClearRewards;
    public List<RewardInfo> RepeatClearRewards;
}
```

### EventHandler

이벤트 관련 요청 처리.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_eventDatabase` | LiveEventDatabase | 이벤트 데이터 |
| `_validator` | ServerValidator | 검증 |
| `_timeService` | ServerTimeService | 시간 서비스 |

| 메서드 | 설명 |
|--------|------|
| `HandleGetActiveEvents(request, userData)` | 활성 이벤트 목록 |
| `HandleVisitEvent(request, userData)` | 이벤트 방문 처리 |
| `HandleClaimMission(request, userData)` | 미션 보상 수령 (플레이스홀더) |
| `CreateLiveEventInfo()` | 이벤트 정보 DTO 생성 |

### CharacterLevelUpHandler

캐릭터 레벨업 요청 처리. 재료 소모, 경험치 적용, 레벨 계산.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_characterDb` | CharacterDatabase | 캐릭터 데이터 |
| `_levelDb` | CharacterLevelDatabase | 레벨 테이블 |
| `_ascensionDb` | CharacterAscensionDatabase | 돌파 테이블 (레벨캡 계산용) |
| `_itemDb` | ItemDatabase | 아이템 데이터 |
| `_rewardService` | RewardService | 재화 차감 |
| `_validator` | ServerValidator | 검증 |

| 메서드 | 설명 |
|--------|------|
| `Handle(CharacterLevelUpRequest, ref UserSaveData)` | 레벨업 처리 |
| `SetDatabases(...)` | 데이터베이스 주입 |
| `DeductItem(ref userData, itemId, count)` | 재료 차감 |
| `UpdateCharacter(ref userData, character)` | 캐릭터 정보 갱신 |

**처리 흐름:**
1. 캐릭터 조회 및 레벨캡 확인
2. 재료 검증 및 경험치/골드 비용 계산
3. 골드 검증
4. 재료/골드 차감
5. 경험치 적용 및 레벨 계산
6. 스탯/전투력 재계산
7. Delta 반환

### CharacterAscensionHandler

캐릭터 돌파(한계돌파) 요청 처리. 레벨캡 상향.

| 필드 | 타입 | 설명 |
|------|------|------|
| `_characterDb` | CharacterDatabase | 캐릭터 데이터 |
| `_levelDb` | CharacterLevelDatabase | 레벨 테이블 |
| `_ascensionDb` | CharacterAscensionDatabase | 돌파 요구사항 |
| `_itemDb` | ItemDatabase | 아이템 데이터 |
| `_rewardService` | RewardService | 재화 차감 |
| `_validator` | ServerValidator | 검증 |

| 메서드 | 설명 |
|--------|------|
| `Handle(CharacterAscensionRequest, ref UserSaveData)` | 돌파 처리 |
| `SetDatabases(...)` | 데이터베이스 주입 |
| `DeductItem(ref userData, itemId, count)` | 재료 차감 |
| `UpdateCharacter(ref userData, character)` | 캐릭터 정보 갱신 |

**처리 흐름:**
1. 캐릭터 조회 및 최대 돌파 확인
2. 돌파 요구사항 조회
3. 레벨/재료/골드 검증
4. 재료/골드 차감
5. 돌파 단계 증가
6. 스탯/전투력 재계산
7. Delta 반환

---

## Services

### ServerTimeService

서버 시간 관리 및 리셋 시간 계산.

| 프로퍼티 | 타입 | 설명 |
|----------|------|------|
| `ServerTimeUtc` | DateTime | UTC 서버 시간 |
| `ServerDateTime` | DateTime | 로컬 서버 시간 |

| 메서드 | 설명 |
|--------|------|
| `GetNextResetTime(LimitType)` | 다음 리셋 시간 |
| `GetNextDayReset()` | 다음 일간 리셋 |
| `GetNextWeekReset()` | 다음 주간 리셋 |
| `GetNextMonthReset()` | 다음 월간 리셋 |
| `GetResetTimeAfter(DateTime, LimitType)` | 특정 시점 이후 리셋 |
| `HasResetOccurred(DateTime, LimitType)` | 리셋 발생 여부 |
| `IsWithinPeriod(start, end)` | 기간 내 여부 |

### GachaService

가챠 확률 및 비용 계산.

| 메서드 | 설명 |
|--------|------|
| `GetPullCount(GachaPullType)` | 뽑기 횟수 반환 |
| `CalculateCost(pool, pullType)` | 비용 계산 |
| `CalculateRarity(pool, pityInfo)` | 등급 결정 (천장 포함) |
| `GetRandomCharacterByRarity(pool, rarity)` | 랜덤 캐릭터 선택 |

### RewardService

보상 지급 및 Delta 생성.

| 메서드 | 설명 |
|--------|------|
| `ApplyReward(userData, rewards)` | 보상 적용 |
| `ApplyCurrencyReward(userData, type, amount)` | 재화 보상 |
| `CreateRewardDelta(rewards)` | 보상 Delta 생성 |
| `CreateCurrencyDelta(type, amount)` | 재화 Delta 생성 |
| `CreateCharacterDelta(characterId)` | 캐릭터 Delta 생성 |
| `DeductGold(userData, amount)` | 골드 차감 |
| `DeductGem(userData, amount)` | 젬 차감 |

### ServerValidator

공통 검증 로직.

| 메서드 | 설명 |
|--------|------|
| `HasEnoughGold(userData, amount)` | 골드 보유 확인 |
| `HasEnoughGem(userData, amount)` | 젬 보유 확인 |
| `HasEnoughStamina(userData, amount)` | 스태미나 보유 확인 |
| `HasCharacter(userData, characterId)` | 캐릭터 보유 확인 |
| `IsEventActive(eventId)` | 이벤트 활성 여부 |
| `GetServerTime()` | 서버 시간 반환 |

### PurchaseLimitValidator

상점 구매 제한 검증.

| 메서드 | 설명 |
|--------|------|
| `CanPurchase(product, record, out remaining)` | 구매 가능 여부 |
| `UpdatePurchaseRecord(productId, limitType, existing)` | 구매 기록 업데이트 |
| `CalculateResetTime(LimitType)` | 리셋 시간 계산 |
| `GetNextDailyReset()` | 다음 일간 리셋 |
| `GetNextWeeklyReset()` | 다음 주간 리셋 |
| `GetNextMonthlyReset()` | 다음 월간 리셋 |

### StageEntryValidator

스테이지 입장 제한 검증.

| 메서드 | 설명 |
|--------|------|
| `CanEnter(limitType, limitCount, record, out remaining)` | 입장 가능 여부 |
| `UpdateEntryRecord(stageId, limitType, existing)` | 입장 기록 업데이트 |
| `IsAvailableToday(availableDays)` | 오늘 입장 가능 여부 |
| `CalculateResetTime(LimitType)` | 리셋 시간 계산 |

### EventCurrencyConverter

이벤트 재화 전환.

| 메서드 | 설명 |
|--------|------|
| `ConvertExpiredCurrencies(userData)` | 만료 재화 일괄 전환 |
| `ConvertEventCurrency(userData, eventId)` | 특정 이벤트 재화 전환 |
| `AddCurrency(userData, currencyId, amount)` | 재화 추가 |

**ConversionResult** (결과 구조체)
```csharp
public struct ConversionResult
{
    public string EventId;
    public string SourceCurrencyId;
    public int SourceAmount;
    public string TargetCurrencyId;
    public int TargetAmount;
}
```

---

## 의존성

```
Sc.LocalServer
├── Sc.Foundation (Result<T>, ErrorCode)
├── Sc.Data (UserSaveData, Request/Response, SO)
└── Sc.Core (LimitType, RewardInfo)
```

---

## 요청 처리 흐름

```
1. NetworkManager.SendRequest<T>()
          │
          ▼
2. LocalGameServer.HandleRequest()
          │
          ▼
3. Handler.Handle()
   ├─ Validator 검증 (재화, 권한, 제한)
   ├─ 비즈니스 로직 실행
   ├─ RewardService로 보상 지급
   └─ Delta 생성
          │
          ▼
4. Response 반환 (Success/Error + Delta)
          │
          ▼
5. NetworkManager → Event 발행
```

---

## 에러 코드

| 코드 | 상수 | 설명 |
|------|------|------|
| 1001 | SHOP_PRODUCT_NOT_FOUND | 상품 없음 |
| 1002 | SHOP_LIMIT_EXCEEDED | 구매 제한 초과 |
| 1003 | SHOP_INSUFFICIENT_CURRENCY | 재화 부족 |
| 1004 | SHOP_PURCHASE_FAILED | 구매 실패 |
| 2001 | STAGE_NOT_FOUND | 스테이지 없음 |
| 2002 | STAGE_LOCKED | 스테이지 잠김 |
| 2003 | STAGE_LIMIT_EXCEEDED | 입장 제한 초과 |
| 2004 | STAGE_NOT_AVAILABLE_TODAY | 오늘 불가 |
| 6001 | EVENT_NOT_FOUND | 이벤트 없음 |
| 6002 | EVENT_NOT_ACTIVE | 이벤트 비활성 |
| 6099 | EVENT_CLAIM_NOT_IMPLEMENTED | 미구현 (플레이스홀더) |
| 7001 | CHARACTER_NOT_FOUND | 캐릭터 없음 |
| 7002 | CHARACTER_MAX_LEVEL | 최대 레벨 도달 |
| 7003 | CHARACTER_INSUFFICIENT_MATERIAL | 재료 부족 |
| 7004 | CHARACTER_INSUFFICIENT_GOLD | 골드 부족 |
| 7005 | CHARACTER_LEVEL_REQUIREMENT_NOT_MET | 돌파 레벨 요구사항 미충족 |
| 7006 | CHARACTER_MAX_ASCENSION | 최대 돌파 도달 |
| 7007 | CHARACTER_LEVEL_CAP_REACHED | 레벨캡 도달 (돌파 필요) |
| 9999 | UNKNOWN_ERROR | 알 수 없는 오류 |

---

## 테스트

| 테스트 파일 | 테스트 수 | 설명 |
|------------|----------|------|
| ShopHandlerTests.cs | 24개 | 상점 구매 로직 |
| PurchaseLimitValidatorTests.cs | 16개 | 구매 제한 검증 |
| StageEntryValidatorTests.cs | 21개 | 스테이지 입장 검증 |
| StageHandlerTests.cs | 26개 | 스테이지 입장/클리어 |
| CharacterLevelUpHandlerTests.cs | 14개 | 캐릭터 레벨업 |
| CharacterAscensionHandlerTests.cs | 17개 | 캐릭터 돌파 |

**총 테스트: 118개**

---

## 확장 포인트

### 새 Handler 추가

1. `IRequestHandler<TRequest, TResponse>` 구현
2. `LocalGameServer`에 필드 추가
3. `HandleRequest` 또는 전용 메서드 추가
4. 필요시 Request/Response 타입 정의

### 새 Validator 추가

1. `Services/` 폴더에 Validator 클래스 생성
2. `ServerTimeService` 의존성 주입
3. Handler에서 사용

---

## 주의사항

- **ScriptableObject 직접 참조 금지**: Handler에서 SO 직접 참조 대신 `StageDataInfo` 같은 전달용 구조체 사용
- **외부 주입 필수**: Database는 `Set*Database()` 메서드로 외부에서 주입
- **Delta 필수 반환**: 모든 데이터 변경은 Response의 Delta에 포함
- **테스트 가능성**: 모든 서비스는 생성자 주입으로 Mock 가능
