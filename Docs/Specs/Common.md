---
type: overview
assembly: Sc.Common
category: Shared
status: draft
version: "1.0"
dependencies: [Sc.Core, Sc.Data, Sc.Packet]
detail_docs: [MVP, UIComponents, Pool, Utility]
created: 2025-01-14
updated: 2025-01-14
---

# Sc.Common

## 목적
모든 컨텐츠에서 공통으로 사용하는 UI, 풀링, 유틸리티 제공.

## 의존성
- **참조**: Sc.Core, Sc.Data, Sc.Packet
- **참조됨**: 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **MVP 패턴** | Model-View-Presenter. UI와 로직 분리 |
| **Object Pool** | 객체 재사용으로 GC 최소화 |
| **Extension** | 기존 타입에 메서드 추가 |

---

## 클래스 역할 정의

### UI - MVP Base

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| IView | 뷰 계약 정의 | 뷰가 제공해야 할 기능 명세 | 구현 |
| IPresenter | 프레젠터 계약 정의 | 프레젠터가 제공해야 할 기능 명세 | 구현 |
| BaseView | 뷰 공통 기능 | Show/Hide, 애니메이션, 이벤트 바인딩 | 비즈니스 로직, 데이터 가공 |
| BasePresenter\<T\> | 프레젠터 공통 기능 | 뷰 참조, 초기화, 정리 | Unity 컴포넌트 직접 조작 |

### UI - Components

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| UIButton | 공통 버튼 | 클릭 이벤트, 상태(활성/비활성), 효과음 | 버튼별 로직 |
| UIPopup | 팝업 베이스 | 팝업 열기/닫기, 배경 딤, 스택 | 팝업 내용 |
| UIManager | UI 스택 관리 | 팝업 스택, 최상위 UI 추적, 뒤로가기 | 개별 UI 로직 |

### Pool

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| IPoolable | 풀링 가능 계약 | OnSpawn/OnDespawn 명세 | 구현 |
| ObjectPool\<T\> | 단일 타입 풀 | 생성, 대여, 반납, 확장 | 여러 타입 관리 |
| PoolManager | 풀 중앙 관리 | 풀 등록/조회, 전역 접근점 | 개별 풀 로직 |

### Utility

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| CollectionExtensions | 컬렉션 확장 | Shuffle, Random Pick, Safe Get | 컬렉션 생성 |
| MathHelper | 수학 유틸리티 | 확률 계산, 범위 클램프, 보간 | 복잡한 수학 연산 |

---

## MVP 패턴 흐름

```
┌─────────┐      ┌─────────────┐      ┌─────────┐
│  View   │ ←──→ │  Presenter  │ ←──→ │  Model  │
└─────────┘      └─────────────┘      └─────────┘
     │                  │                   │
 UI 표시            로직 처리           데이터
 사용자 입력        데이터 가공         상태 저장
```

### 책임 분리

| 계층 | 알아야 하는 것 | 몰라야 하는 것 |
|------|----------------|----------------|
| **View** | UI 요소, 애니메이션 | 비즈니스 로직, 데이터 구조 |
| **Presenter** | View 인터페이스, Model | Unity 컴포넌트, UI 세부사항 |
| **Model** | 데이터, 규칙 | View, Presenter |

---

## 클래스 관계도

```
┌─ UI ──────────────────────────────────────────┐
│                                               │
│  ┌─────────┐    ┌──────────────┐              │
│  │ IView   │←───│  BaseView    │              │
│  └─────────┘    └──────────────┘              │
│        ↑               ↑                      │
│        │               │ 상속                 │
│  ┌─────────────┐  ┌─────────┐  ┌──────────┐  │
│  │IPresenter   │  │UIButton │  │ UIPopup  │  │
│  └─────────────┘  └─────────┘  └──────────┘  │
│        ↑                            ↑        │
│        │                            │ 관리    │
│  ┌─────────────────┐         ┌───────────┐   │
│  │BasePresenter<T> │         │ UIManager │   │
│  └─────────────────┘         └───────────┘   │
└───────────────────────────────────────────────┘

┌─ Pool ────────────────────────────────────────┐
│  ┌───────────┐    ┌───────────────┐           │
│  │ IPoolable │←───│ ObjectPool<T> │           │
│  └───────────┘    └───────────────┘           │
│                          ↑                    │
│                          │ 관리               │
│                   ┌─────────────┐             │
│                   │ PoolManager │             │
│                   └─────────────┘             │
└───────────────────────────────────────────────┘

┌─ Utility ─────────────────────────────────────┐
│  ┌─────────────────────┐  ┌────────────┐      │
│  │ CollectionExtensions│  │ MathHelper │      │
│  └─────────────────────┘  └────────────┘      │
└───────────────────────────────────────────────┘
```

---

## 사용 예시

```csharp
// MVP 패턴
public class LobbyView : BaseView, ILobbyView { }
public class LobbyPresenter : BasePresenter<ILobbyView> { }

// 오브젝트 풀
var bullet = PoolManager.Instance.Spawn<Bullet>();
PoolManager.Instance.Despawn(bullet);

// 확장 메서드
var randomItem = itemList.RandomPick();
var shuffled = cardList.Shuffle();
```

---

## 설계 원칙

1. **재사용성**: 특정 컨텐츠에 종속되지 않음
2. **확장성**: 상속/구현으로 기능 확장
3. **성능**: 풀링으로 GC 최소화

---

## 상세 문서
- [MVP.md](Common/MVP.md) - MVP 패턴 상세 (IView, IPresenter, Base 클래스)
- [UIComponents.md](Common/UIComponents.md) - UI 컴포넌트 상세 (Button, Popup, Manager)
- [Pool.md](Common/Pool.md) - 오브젝트 풀 상세
- [Utility.md](Common/Utility.md) - 유틸리티 상세

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| UI/Base | 4 | ⬜ |
| UI/Components | 3 | ⬜ |
| Pool | 3 | ⬜ |
| Utility | 2 | ⬜ |
