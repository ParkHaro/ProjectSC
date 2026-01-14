---
type: spec
assembly: Sc.Core
class: Singleton
category: System
status: draft
version: "1.0"
dependencies: []
created: 2025-01-14
updated: 2025-01-14
---

# Singleton\<T\>

## 역할
전역 단일 인스턴스 보장. 모든 Manager 클래스의 베이스.

## 책임
- 인스턴스 생성 및 생명주기 관리
- 전역 접근점 제공 (Instance 프로퍼티)
- 씬 전환 시 유지 (DontDestroyOnLoad)
- 중복 인스턴스 방지

## 비책임
- 비즈니스 로직
- 데이터 저장/관리
- 초기화 순서 결정 (GameManager 담당)

---

## 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| Instance | static T | 싱글톤 인스턴스 접근 |
| OnInitialize() | virtual void | 초기화 훅 (Awake에서 호출) |

### 생명주기 메서드

| 메서드 | 시점 | 동작 |
|--------|------|------|
| Awake | 인스턴스 생성 | 중복 체크, DontDestroyOnLoad, OnInitialize 호출 |
| OnDestroy | 파괴 | 인스턴스 참조 해제 |
| OnApplicationQuit | 앱 종료 | 종료 플래그 설정 |

---

## 동작 흐름

```
최초 Instance 접근
       ↓
  인스턴스 존재?
   ├─ Yes → 반환
   └─ No → FindObjectOfType
              ├─ 찾음 → 반환
              └─ 없음 → 새 GameObject 생성 + AddComponent
                           ↓
                     Awake 호출
                       ├─ 중복? → Destroy
                       └─ 최초 → DontDestroyOnLoad
                                      ↓
                                 OnInitialize
```

---

## 사용 패턴

```csharp
// 상속
public class GameManager : Singleton<GameManager>
{
    protected override void OnInitialize()
    {
        // 초기화 로직
    }
}

// 접근
GameManager.Instance.StartGame();
```

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 종료 시점 | OnApplicationQuit 이후 접근 시 null 반환 |
| 상속 필수 | base.Awake() 호출 필수 |
| 스레드 안전 | lock으로 보호됨 |
| 씬 전환 | DontDestroyOnLoad로 유지 |

## 관련
- [Core.md](../Core.md)
- [EventManager.md](EventManager.md)
