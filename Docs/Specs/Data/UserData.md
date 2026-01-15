---
type: spec
assembly: Sc.Data
class: UserSaveData, UserProfile, UserCurrency, OwnedCharacter, OwnedItem, StageProgress, GachaPityData, QuestProgress
category: Struct
status: approved
version: "1.0"
dependencies: [CurrencyType]
created: 2026-01-15
updated: 2026-01-15
---

# Data - UserData

## 역할
플레이어별 진행 데이터. 로컬/서버 저장용 직렬화 구조체.

## 마스터 데이터 vs 유저 데이터

| 구분 | 마스터 데이터 | 유저 데이터 |
|------|-------------|------------|
| 정의 | 기획 데이터 (게임 규칙) | 플레이어 진행 데이터 |
| 특징 | 모든 유저 동일, 읽기 전용 | 유저별 다름, 읽기/쓰기 |
| 저장 | ScriptableObject | JSON (로컬) / DB (서버) |
| 동기화 | 앱 업데이트 | 실시간 |
| 예시 | CharacterData, SkillData | UserProfile, OwnedCharacter |

---

## 파일 구성

| 파일 | 용도 | 사용처 |
|------|------|--------|
| UserSaveData.cs | 통합 저장 구조체 | DataManager |
| UserProfile.cs | 유저 프로필 | 로비, 설정 |
| UserCurrency.cs | 재화 정보 | Shop, Gacha |
| OwnedCharacter.cs | 보유 캐릭터 | Character, Battle |
| OwnedItem.cs | 보유 아이템 | Inventory |
| StageProgress.cs | 스테이지 진행 | Battle, Lobby |
| GachaPityData.cs | 천장 정보 | Gacha |
| QuestProgress.cs | 퀘스트 진행 | Quest |

---

## UserSaveData

통합 저장 데이터. 모든 유저 데이터를 포함.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| Version | int | 데이터 버전 (마이그레이션용) |
| Profile | UserProfile | 유저 프로필 |
| Currency | UserCurrency | 재화 정보 |
| Characters | OwnedCharacter[] | 보유 캐릭터 목록 |
| Items | OwnedItem[] | 보유 아이템 목록 |
| StageProgress | StageProgress | 스테이지 진행 |
| GachaPities | GachaPityData[] | 가챠 천장 목록 |
| QuestProgresses | QuestProgress[] | 퀘스트 진행 목록 |
| LastSyncAt | long | 마지막 동기화 시각 (Unix) |

### 상수

| 이름 | 값 | 설명 |
|------|-----|------|
| CurrentVersion | 1 | 현재 데이터 버전 |

### 특징
- `CreateDefault()` - 기본값 생성 (신규 유저)
- `UpdateSyncTime()` - 동기화 시각 갱신
- 버전 필드로 마이그레이션 지원

### 사용 패턴
```csharp
var data = UserSaveData.CreateDefault();
data.UpdateSyncTime();
```

---

## UserProfile

유저 프로필 정보.

### 필드

| 필드 | 타입 | 설명 | 기본값 |
|------|------|------|--------|
| Uid | string | 고유 ID (서버 발급) | GUID |
| Nickname | string | 닉네임 | "Player" |
| Level | int | 계정 레벨 | 1 |
| Exp | long | 누적 경험치 | 0 |
| TutorialFlags | int | 튜토리얼 비트플래그 | 0 |
| CreatedAt | long | 계정 생성 시각 (Unix) | 현재 |
| LastLoginAt | long | 마지막 로그인 시각 (Unix) | 현재 |

---

## UserCurrency

유저 재화 정보.

### 필드

| 필드 | 타입 | 설명 | 기본값 |
|------|------|------|--------|
| Gold | long | 골드 | 10,000 |
| FreeGem | int | 무료 보석 | 1,000 |
| PaidGem | int | 유료 보석 | 0 |
| Stamina | int | 현재 스태미나 | 120 |
| MaxStamina | int | 최대 스태미나 | 120 |
| StaminaRecoverAt | long | 다음 회복 시각 (Unix) | 0 |

### 프로퍼티

| 이름 | 타입 | 설명 |
|------|------|------|
| TotalGem | int | 총 보석 (무료 + 유료) |

### 헬퍼 메서드

| 메서드 | 반환 | 설명 |
|--------|------|------|
| TryConsumeGem(int) | bool | 보석 소비 (무료 우선) |

### 무료 보석 우선 정책
```csharp
// 소비 순서: FreeGem → PaidGem
if (currency.TryConsumeGem(100)) { ... }
```

---

## OwnedCharacter

보유 캐릭터 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| InstanceId | string | 인스턴스 고유 ID |
| CharacterId | string | 마스터 데이터 ID |
| Level | int | 캐릭터 레벨 |
| Exp | int | 현재 경험치 |
| Grade | int | 돌파 등급 (0~6) |
| IsLocked | bool | 잠금 여부 |
| EquippedItemIds | string[] | 장착 장비 ID 목록 |
| AcquiredAt | long | 획득 시각 (Unix) |

### 특징
- `InstanceId`: 동일 캐릭터 중복 보유 지원
- `CharacterId`: CharacterData.Id 참조

---

## OwnedItem

보유 아이템 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| ItemId | string | 마스터 데이터 ID |
| Amount | int | 보유 수량 |

### 특징
- 단순 수량 관리 (스택형 아이템)
- `ItemId`: ItemData.Id 참조

---

## StageProgress

스테이지 진행 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| ClearedStageIds | List\<string\> | 클리어한 스테이지 ID 목록 |
| StageStars | Dictionary\<string, int\> | 스테이지별 별점 |
| LastClearedChapter | int | 마지막 클리어 챕터 |

### 헬퍼 메서드

| 메서드 | 반환 | 설명 |
|--------|------|------|
| RecordClear(stageId, stars) | void | 클리어 기록 (최고 별점 갱신) |
| IsCleared(stageId) | bool | 클리어 여부 |
| GetStars(stageId) | int | 별점 조회 |

---

## GachaPityData

가챠 천장 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| PoolId | string | 가챠 풀 ID |
| PityCount | int | 천장 카운트 |
| LastPullAt | long | 마지막 뽑기 시각 (Unix) |

### 헬퍼 메서드

| 메서드 | 반환 | 설명 |
|--------|------|------|
| IncrementCount(count) | void | 카운트 증가 |
| ResetPity() | void | 천장 리셋 |

### 특징
- 풀별 독립 천장 관리
- 카운트 증가 시 시각 자동 갱신

---

## QuestProgress

퀘스트 진행 정보.

### 필드

| 필드 | 타입 | 설명 |
|------|------|------|
| QuestId | string | 퀘스트 ID |
| CurrentProgress | int | 현재 진행도 |
| IsCompleted | bool | 완료 여부 |
| IsRewardClaimed | bool | 보상 수령 여부 |

### 헬퍼 메서드

| 메서드 | 반환 | 설명 |
|--------|------|------|
| UpdateProgress(progress, target) | void | 진행도 업데이트 |
| TryClaimReward() | bool | 보상 수령 시도 |

### 상태 흐름
```
진행 중 → 완료 (IsCompleted) → 수령 (IsRewardClaimed)
```

---

## 관련 Enum

### CurrencyType

| 값 | 설명 |
|----|------|
| Gold (0) | 인게임 재화 |
| FreeGem (1) | 무료 보석 |
| PaidGem (2) | 유료 보석 |
| Stamina (3) | 스태미나 |
| EventToken (100) | 이벤트 전용 재화 |

---

## 설계 원칙

1. **직렬화 가능**: `[Serializable]` 어트리뷰트, JSON 호환
2. **기본값 제공**: `CreateDefault()` 팩토리 메서드
3. **헬퍼 메서드**: 자주 사용되는 연산 캡슐화
4. **마스터 데이터 참조**: ID 기반 연결 (CharacterId, ItemId)
5. **버전 관리**: 마이그레이션 지원

---

## 주의사항
- Struct는 값 복사됨 (배열/리스트 내부 수정 시 주의)
- 큰 배열 포함 시 `in` 파라미터로 전달 권장
- List/Dictionary 필드는 null 체크 필요

---

## 관련
- [Data.md](../Data.md)
- [Structs.md](Structs.md)
- [Enums.md](Enums.md)
- [DataManager](../../Core/DataManager.md) - 유저 데이터 관리
- [IDataService](../../Packet/IDataService.md) - 저장/로드 서비스
