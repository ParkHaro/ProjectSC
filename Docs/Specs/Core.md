---
type: overview
assembly: Sc.Core
category: Infrastructure
status: approved
version: "2.0"
dependencies: [Sc.Foundation, Sc.Data, Sc.Packet]
detail_docs: [DataManager, Handlers]
created: 2025-01-14
updated: 2025-01-15
---

# Sc.Core

## 목적
게임 전역에서 사용되는 핵심 인프라 시스템 제공. 데이터 관리 및 서버 응답 처리.

## 의존성
- **참조**: Sc.Foundation (Singleton, EventManager), Sc.Data, Sc.Packet
- **참조됨**: Sc.Common, 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **DataManager** | 마스터/유저 데이터 중앙 관리. 읽기 전용 뷰 제공 |
| **Handler** | 서버 응답 처리 담당. DataManager와 연동 |

---

## 클래스 역할 정의

### Managers

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| DataManager | 데이터 중앙 관리 | 마스터 캐시, 유저 뷰, Delta 적용 | 서버 통신, 직접 데이터 수정 |

### Handlers

| 클래스 | 역할 | 책임 |
|--------|------|------|
| LoginResponseHandler | 로그인 응답 처리 | SetUserData 호출, 이벤트 발행 |
| GachaResponseHandler | 가챠 응답 처리 | ApplyDelta 호출, 결과 이벤트 발행 |
| ShopResponseHandler | 구매 응답 처리 | ApplyDelta 호출, 구매 완료 이벤트 |

---

## DataManager 상세

### 역할
- **마스터 데이터 캐시**: 5개 Database SO 참조 (Characters, Skills, Items, Stages, GachaPools)
- **유저 데이터 뷰**: 읽기 전용 프로퍼티로 유저 데이터 접근
- **Delta 적용**: 서버 응답으로만 유저 데이터 갱신

### 주요 메서드

| 메서드 | 시그니처 | 용도 |
|--------|----------|------|
| Initialize | `bool` | 마스터 데이터 검증 및 초기화 |
| SetUserData | `void(UserSaveData)` | 로그인 시 전체 유저 데이터 설정 |
| ApplyDelta | `void(UserDataDelta)` | 서버 응답 Delta 적용 (부분 갱신) |

### 프로퍼티 (읽기 전용)

**마스터 데이터:**
- `Characters` → CharacterDatabase
- `Skills` → SkillDatabase
- `Items` → ItemDatabase
- `Stages` → StageDatabase
- `GachaPools` → GachaPoolDatabase

**유저 데이터 뷰:**
- `Profile` → UserProfile
- `Currency` → UserCurrency
- `OwnedCharacters` → IReadOnlyList\<OwnedCharacter\>
- `OwnedItems` → IReadOnlyList\<OwnedItem\>
- `StageProgress` → StageProgress
- `GachaPity` → GachaPityData
- `QuestProgress` → QuestProgress

### 이벤트
- `OnUserDataChanged`: 유저 데이터 변경 시 발생 (SetUserData, ApplyDelta 후)

---

## 데이터 흐름

### 초기화
```
앱 시작
   ↓
DataManager.Instance.Initialize()
   ↓ 마스터 데이터 검증
IApiClient.InitializeAsync()
   ↓
LoginRequest → LoginResponse
   ↓
DataManager.SetUserData(response.UserData)
   ↓
OnUserDataChanged 이벤트
```

### 게임 액션 (가챠 예시)
```
GachaRequest → GachaResponse
   ↓
DataManager.ApplyDelta(response.Delta)
   ↓
OnUserDataChanged 이벤트
   ↓
UI 갱신 (이벤트 구독)
```

---

## 서버 중심 아키텍처

### 설계 원칙
- **Server Authority**: 모든 유저 데이터 변경은 서버 응답으로만
- **클라이언트 읽기 전용**: DataManager는 읽기 전용 뷰만 제공
- **직접 수정 금지**: ModifyUserData 같은 메서드 없음

### Delta 적용 규칙
```csharp
// ApplyDelta 내부 로직
if (delta.Currency.HasValue)
    _userData.Currency = delta.Currency.Value;

if (delta.AddedCharacters != null)
    foreach (var c in delta.AddedCharacters)
        // 중복 체크 후 추가/갱신

if (delta.RemovedCharacterIds != null)
    foreach (var id in delta.RemovedCharacterIds)
        _userData.Characters.RemoveAll(c => c.InstanceId == id);
```

---

## 유틸리티 메서드

| 메서드 | 용도 |
|--------|------|
| HasCharacter(characterId) | 캐릭터 보유 여부 확인 |
| GetItemCount(itemId) | 아이템 보유 수량 확인 |
| GetCharacterMasterData(owned) | 보유 캐릭터의 마스터 데이터 조회 |
| GetItemMasterData(owned) | 보유 아이템의 마스터 데이터 조회 |

---

## 폴더 구조

```
Assets/Scripts/Core/
├── Sc.Core.asmdef
├── Managers/
│   └── DataManager.cs
└── Handlers/
    ├── LoginResponseHandler.cs
    ├── GachaResponseHandler.cs
    └── ShopResponseHandler.cs
```

---

## 설계 원칙

1. **서버 중심**: 유저 데이터 변경은 Delta로만
2. **읽기 전용 뷰**: 외부에서 직접 수정 불가
3. **이벤트 기반**: 데이터 변경 시 이벤트 발행
4. **마스터-유저 분리**: 정적 데이터와 동적 데이터 명확히 구분

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Managers | 1 | ✅ |
| Handlers | 3 | ⬜ (추후 구현) |
