---
type: spec
assembly: Sc.Core
class: SaveManager
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton, EventManager]
created: 2025-01-14
updated: 2025-01-14
---

# SaveManager

## 역할
게임 데이터 저장/로드. JSON 기반 파일 시스템.

## 책임
- 데이터 JSON 직렬화/역직렬화
- 파일 저장/로드
- 다중 슬롯 관리
- 저장 실패 시 ErrorEvent 발행

## 비책임
- 저장 데이터 구조 정의
- 암호화/복호화
- 클라우드 동기화
- 자동 저장 타이밍 결정

---

## 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| Save | void Save\<T\>(T data, string slot = "default") | 데이터 저장 |
| Load | T Load\<T\>(string slot = "default") | 데이터 로드 |
| Exists | bool Exists(string slot) | 저장 파일 존재 여부 |
| Delete | void Delete(string slot) | 슬롯 삭제 |
| DeleteAll | void DeleteAll() | 전체 삭제 |
| GetAllSlots | string[] GetAllSlots() | 모든 슬롯 이름 |

### 제약
- T는 `[Serializable]` 클래스
- Load<T>의 T는 기본 생성자 필수

---

## 저장 경로

```
[persistentDataPath]/
    ├─ default.sav      (기본 슬롯)
    ├─ slot_1.sav
    ├─ slot_2.sav
    └─ settings.sav
```

**플랫폼별 경로**:
- Windows: `%USERPROFILE%/AppData/LocalLow/<CompanyName>/<ProductName>/`
- Android: `/data/data/<package>/files/`
- iOS: `/var/mobile/Applications/<guid>/Documents/`

---

## 동작 흐름

### 저장
```
Save<T>(data, slot)
       ↓
  JsonUtility.ToJson
       ↓
  File.WriteAllText
   ├─ 성공 → 완료
   └─ 실패 → ErrorEvent(3001) 발행
```

### 로드
```
Load<T>(slot)
       ↓
  파일 존재?
   ├─ Yes → File.ReadAllText → FromJson
   └─ No → new T() 반환
       ↓
  파싱 성공?
   ├─ Yes → 반환
   └─ No → ErrorEvent(3002) + new T()
```

---

## 사용 패턴

```csharp
// 저장
SaveManager.Instance.Save(gameData);

// 로드 (없으면 새 객체)
var data = SaveManager.Instance.Load<GameSaveData>();

// 슬롯 지정
SaveManager.Instance.Save(data, "slot_2");
```

---

## 주의사항

| 항목 | 설명 |
|------|------|
| Dictionary | JsonUtility 미지원, 별도 래퍼 필요 |
| 버전 관리 | 데이터 구조 변경 시 마이그레이션 고려 |
| 암호화 | 민감 데이터는 별도 암호화 레이어 추가 |
| 백업 | 저장 전 기존 파일 백업 권장 |

## 관련
- [Core.md](../Core.md)
- [Singleton.md](Singleton.md)
- [Data/Structs.md](../Data/Structs.md)
