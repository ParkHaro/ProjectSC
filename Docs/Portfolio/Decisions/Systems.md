# Systems Decisions

공통 시스템 관련 의사결정 기록.

---

## 보상 시스템 (RewardType) 설계

**일자**: 2026-01-17 | **상태**: 결정됨

### 컨텍스트
아웃게임 아키텍처에서 공통 보상 시스템 필요. 수집형 RPG 다양한 보상 타입 처리.

### 선택지
1. **세분화된 RewardType (8개+)** - 타입만으로 분기 가능하나 중복 로직
2. **Thing 기반 통합 (4개)** - 단순하나 세부 구분에 추가 조회 필요
3. **처리 방식 기준 분류** - RewardType 4개 + ItemCategory 6개 (선택)

### 결정
**처리 방식 기준 분류**
```csharp
public enum RewardType { Currency, Item, Character, PlayerExp }
public enum ItemCategory { Equipment, Consumable, Material, CharacterShard, Furniture, Ticket }
```

### 결과
- 보상 처리 로직이 동일한 것을 같은 RewardType으로 분류
- UI 표시용 세분화는 ItemCategory로
- 61개 테스트 작성

### 추가 결정
- **장비**: 인벤토리=수량 기반, 장착=인스턴스 기반
- **스킨**: 별도 캐릭터로 처리 (CharacterData.BaseCharacterId)

---

## TimeService 시간 처리 설계

**일자**: 2026-01-17 | **상태**: 결정됨

### 컨텍스트
상점 구매 제한 (Daily/Weekly/Monthly) 리셋 시간 처리 필요.

### 결정
- **구현**: TimeService 중앙 집중
- **UI 타임존**: 로컬 시간 변환
- **위치**: ITimeService 인터페이스 공유, 클라/서버 각각 구현
- **리셋 기준**: UTC 0시

### 결과
테스트 시 MockTimeService로 시간 조작 가능. 45개 테스트 작성.

---

## Foundation 시스템 범위

**일자**: 2026-01-17 | **상태**: 결정됨

### 컨텍스트
OUTGAME-V1 구현 전 기반 시스템 필요성 논의.

### 결정 (Phase 0에 포함)
1. **Logging** - 디버깅, 릴리즈 빌드 최적화
2. **Error** - ErrorCode 체계, Result<T> 패턴
3. **SaveManager** - 버전 마이그레이션
4. **LoadingIndicator** - 네트워크 중 화면 차단

### 이후로 미룸
오브젝트 풀링, 리소스 로딩, 오디오, 로컬라이제이션, 설정

### 결과
1~4번은 다른 모든 시스템에서 사용하는 기반 인프라.

---

## RewardPopup 좌우 스크롤 카드형

**일자**: 2026-01-17 | **상태**: 결정됨

### 컨텍스트
보상 획득 팝업 UI 방식. 수집형 RPG 5개 게임 비교 분석.

### 선택지
1. **리스트형** - 심플하나 특별한 보상도 평범하게 보임
2. **그리드형** - 카드 컬렉션 느낌, 많은 보상 시 스크롤 필요
3. **좌우 스크롤 카드형** - 각 보상 강조, 모바일 UX 자연스러움 (선택)

### 결정
**좌우 스크롤 카드형 (캐러셀)**
- 정렬: 희귀도 순 (캐릭터 > 장비 > 재료 > 재화)
- 전체 보기: 그리드 형태 서브 팝업 (4열)
- 신규 아이템: NEW 뱃지 + 금색 프레임

---

## RewardPopup 아이콘 로딩 전략

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
Addressables 아이콘 로드 시 UI "튀는" 현상 방지 필요.

### 선택지
1. **동기 로드 (Resources)** - 즉시 표시하나 빌드 크기 증가
2. **지연 로드** - 빌드 최적화되나 튀는 현상
3. **프리로드 캐시** - 둘 다 해결 (선택)

### 결정
**프리로드 캐시**
```
OnBind → PreloadAsync → SpawnItems (캐시에서 즉시 표시)
```

### 결과
AssetManager Scope 기반 로딩으로 대체. RewardIconCache 127줄 삭제.
