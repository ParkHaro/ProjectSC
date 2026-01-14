---
type: spec
assembly: Sc.Common
class: CollectionExtensions, MathHelper
category: Utility
status: draft
version: "1.0"
dependencies: [ElementType]
created: 2025-01-14
updated: 2025-01-14
---

# Utility

## 역할
공통 유틸리티 함수 제공. 컬렉션 조작, 수학 계산, 전투 공식.

## 책임
- 컬렉션 확장 메서드
- 확률/랜덤 계산
- 전투 데미지 공식
- 값 변환 유틸리티

## 비책임
- Unity 전용 기능 (Mathf 등)
- 게임 로직
- 상태 관리

---

## CollectionExtensions

### 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| RandomPick | T RandomPick\<T\>(this IList\<T\>) | 랜덤 요소 |
| WeightedRandomPick | T WeightedRandomPick\<T\>(this IList\<T\>, Func\<T, float\>) | 가중치 랜덤 |
| Shuffle | IList\<T\> Shuffle\<T\>(this IList\<T\>) | Fisher-Yates 셔플 |
| SafeGet | T SafeGet\<T\>(this IList\<T\>, int, T) | 안전 인덱스 접근 |
| IsNullOrEmpty | bool IsNullOrEmpty\<T\>(this ICollection\<T\>) | null/빈 체크 |
| RandomPickMultiple | List\<T\> RandomPickMultiple\<T\>(this IList\<T\>, int) | 중복 없이 N개 |
| GetOrDefault | TValue GetOrDefault\<TKey, TValue\>(...) | Dictionary 안전 접근 |
| GetOrAdd | TValue GetOrAdd\<TKey, TValue\>(...) | 없으면 생성 후 반환 |

---

## MathHelper

### 확률/랜덤

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| CheckProbability | bool CheckProbability(float) | 확률 판정 (0.0~1.0) |
| RandomRange | int/float RandomRange(min, max) | 범위 내 랜덤 |

### 값 변환

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| Clamp | T Clamp\<T\>(T, T, T) | 범위 제한 |
| Lerp | float Lerp(a, b, t) | 선형 보간 |
| InverseLerp | float InverseLerp(a, b, value) | 역선형 보간 |
| Remap | float Remap(value, fromMin, fromMax, toMin, toMax) | 범위 재매핑 |
| PercentToDecimal | float PercentToDecimal(float) | 퍼센트 → 소수 |

### 전투 계산

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| CalculateDamage | int CalculateDamage(atk, def, multiplier) | 기본 데미지 |
| CalculateCriticalDamage | (int, bool) CalculateCriticalDamage(...) | 크리티컬 계산 |
| GetElementMultiplier | float GetElementMultiplier(attacker, defender) | 속성 상성 |

---

## 데미지 공식

### 기본 데미지
```
BaseDamage = Attack × SkillMultiplier
Reduction = Defense / (Defense + 100)
FinalDamage = BaseDamage × (1 - Reduction)
최소 데미지 = 1
```

### 크리티컬
```
IsCritical = Random < CritRate
CritDamage = BaseDamage × CritMultiplier (크리티컬 시)
```

### 속성 상성
```
유리: 1.5배
├─ Fire → Wind
├─ Wind → Water
├─ Water → Fire
├─ Light ↔ Dark

불리: 0.5배
├─ Fire → Water
├─ Wind → Fire
└─ Water → Wind

동일/중립: 1.0배
```

---

## 사용 패턴

```csharp
// 가챠 (가중치 랜덤)
var result = pool.WeightedRandomPick(item => item.Rate);

// 확률 판정 (30%)
if (MathHelper.CheckProbability(0.3f)) ApplyBuff();

// 데미지 계산
var dmg = MathHelper.CalculateDamage(atk, def, 1.5f);
var (final, isCrit) = MathHelper.CalculateCriticalDamage(dmg, critRate, critDmg);

// HP바 비율
var ratio = MathHelper.InverseLerp(0, maxHp, currentHp);
```

---

## 유틸리티 분류

| 분류 | 메서드 | 주요 용도 |
|------|--------|----------|
| 컬렉션 | RandomPick, Shuffle, SafeGet | 리스트 조작 |
| 확률 | CheckProbability, WeightedRandomPick | 가챠, 확률 판정 |
| 수학 | Clamp, Lerp, Remap | UI 값 변환 |
| 전투 | CalculateDamage, GetElementMultiplier | 전투 계산 |

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 스레드 안전 | System.Random은 스레드 안전하지 않음 |
| null 체크 | 컬렉션 메서드는 null 반환 가능 |
| 정밀도 | float 비교 시 Mathf.Approximately 사용 |
| Unity | Unity 전용 함수는 UnityEngine.Mathf 사용 |

## 관련
- [Common.md](../Common.md)
- [Data/Enums.md](../Data/Enums.md)
