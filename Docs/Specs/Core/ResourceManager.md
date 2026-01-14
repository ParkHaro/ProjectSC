---
type: spec
assembly: Sc.Core
class: ResourceManager
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton]
created: 2025-01-14
updated: 2025-01-14
---

# ResourceManager

## 역할
Addressables 래핑. 리소스 로드/언로드, 캐싱 담당.

## 책임
- 비동기 리소스 로드 (Addressables)
- 로드된 리소스 캐싱
- 리소스 해제 및 메모리 관리
- 로드 상태 추적

## 비책임
- 리소스 생성/정의
- Addressables 그룹 설정
- 번들 빌드/배포

---

## 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| LoadAsync | UniTask\<T\> LoadAsync\<T\>(string key) | 키로 리소스 로드 |
| LoadAsync | UniTask\<T\> LoadAsync\<T\>(AssetReference ref) | 참조로 리소스 로드 |
| Release | void Release(string key) | 리소스 해제 |
| ReleaseAll | void ReleaseAll() | 전체 캐시 해제 |
| PreloadAsync | UniTask PreloadAsync(IEnumerable\<string\>) | 미리 로드 |
| IsLoaded | bool IsLoaded(string key) | 캐시 존재 여부 |

---

## 동작 흐름

```
LoadAsync(key)
     ↓
  캐시 확인
   ├─ 있음 → 즉시 반환
   └─ 없음 → Addressables.LoadAssetAsync
                  ↓
             로드 완료
                  ↓
             캐시에 저장
                  ↓
                반환
```

---

## 사용 패턴

```csharp
// 단일 로드
var data = await ResourceManager.Instance.LoadAsync<CharacterData>("char_001");

// 미리 로드
await ResourceManager.Instance.PreloadAsync(new[] { "char_001", "char_002" });

// 해제
ResourceManager.Instance.Release("char_001");

// 씬 전환 시
ResourceManager.Instance.ReleaseAll();
```

---

## 캐시 전략

| 상황 | 동작 |
|------|------|
| 중복 로드 요청 | 캐시에서 반환 (중복 로드 안 함) |
| 해제 요청 | Addressables.Release + 캐시 제거 |
| 씬 전환 | ReleaseAll로 메모리 정리 |

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 해제 책임 | 사용 완료 후 Release 호출 필수 |
| 동시 로드 | 같은 키 동시 요청 시 하나만 로드 |
| 에러 | 로드 실패 시 null 반환 + 로그 |
| 메모리 | 장시간 미사용 리소스는 해제 권장 |

## 관련
- [Core.md](../Core.md)
- [Singleton.md](Singleton.md)
