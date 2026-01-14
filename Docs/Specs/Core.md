---
type: overview
assembly: Sc.Core
category: Infrastructure
status: draft
version: "1.0"
dependencies: [Sc.Data, Sc.Packet]
detail_docs: [Singleton, EventManager, ResourceManager, SceneLoader, AudioManager, SaveManager, StateMachine]
created: 2025-01-14
updated: 2025-01-14
---

# Sc.Core

## 목적
게임 전역에서 사용되는 핵심 인프라 시스템 제공. 컨텐츠 독립적인 기반 기능.

## 의존성
- **참조**: Sc.Data, Sc.Packet
- **참조됨**: Sc.Common, 모든 Contents

---

## 핵심 개념

| 개념 | 설명 |
|------|------|
| **Singleton** | 전역 단일 인스턴스. Manager 클래스의 베이스 |
| **Manager** | 특정 도메인 담당 전역 객체. Singleton 상속 |
| **System** | 재사용 가능한 범용 로직. 인스턴스화하여 사용 |

---

## 클래스 역할 정의

### Base

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| Singleton\<T\> | 전역 단일 인스턴스 보장 | 인스턴스 생성/파괴, 접근점 제공 | 비즈니스 로직, 데이터 저장 |

### Managers

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| GameManager | 게임 상태 총괄 | 초기화 순서, 게임 상태 전이, 앱 생명주기 | 개별 컨텐츠 로직, UI 제어 |
| EventManager | 이벤트 중계소 | Publish/Subscribe 관리, 구독자 호출 | 이벤트 내용 해석, 비즈니스 로직 |
| ResourceManager | 리소스 로드 담당 | Addressables 래핑, 로드/언로드, 캐싱 | 리소스 생성, 데이터 정의 |
| SceneLoader | 씬 전환 담당 | 비동기 씬 로드, 로딩 진행률, 전환 연출 | 씬 내부 로직, UI 표시 |
| AudioManager | 오디오 재생 담당 | BGM/SFX 재생, 볼륨 조절, 페이드 | 오디오 파일 관리, 믹싱 |
| SaveManager | 저장/로드 담당 | 직렬화, 파일 I/O, 슬롯 관리 | 저장 데이터 구조 정의, 암호화 |

### Systems

| 클래스 | 역할 | 책임 | 하지 않는 것 |
|--------|------|------|--------------|
| StateMachine\<T\> | 상태 전이 관리 | 상태 등록, 전이 실행, 현재 상태 추적 | 상태별 로직 구현, 조건 판단 |

---

## 클래스 관계도

```
                    ┌─────────────────┐
                    │  Singleton<T>   │
                    └────────┬────────┘
                             │ 상속
        ┌────────────────────┼────────────────────┐
        ↓                    ↓                    ↓
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│ GameManager  │    │ EventManager │    │ResourceManager│
└──────┬───────┘    └──────────────┘    └──────────────┘
       │ 소유                ↑                    ↑
       ↓                     │ 사용               │ 사용
┌──────────────┐             │                    │
│ SceneLoader  │─────────────┴────────────────────┘
│ AudioManager │
│ SaveManager  │
└──────────────┘

┌──────────────┐
│StateMachine<T>│ ← 독립 시스템, 필요 시 인스턴스화
└──────────────┘
```

---

## 초기화 순서

```
1. Singleton<T> 인스턴스 생성 (Awake)
2. GameManager.Initialize()
   ├─ EventManager 초기화
   ├─ ResourceManager 초기화
   ├─ SaveManager 초기화 → 저장 데이터 로드
   ├─ AudioManager 초기화
   └─ SceneLoader 초기화
3. 게임 시작 (첫 씬 로드)
```

---

## 사용 예시

```csharp
// 이벤트 발행/구독
EventManager.Instance.Subscribe<BattleEndEvent>(OnBattleEnd);
EventManager.Instance.Publish(new BattleEndEvent { ... });

// 리소스 로드
var data = await ResourceManager.Instance.LoadAsync<CharacterData>("char_001");

// 씬 전환
await SceneLoader.Instance.LoadSceneAsync(SceneType.Battle);

// 저장/로드
SaveManager.Instance.Save(saveData);
var loaded = SaveManager.Instance.Load<SaveData>();
```

---

## 설계 원칙

1. **컨텐츠 무관**: Core는 특정 컨텐츠를 알지 못함
2. **단일 책임**: 각 Manager는 한 가지 도메인만 담당
3. **의존성 주입 준비**: 테스트를 위해 인터페이스 분리 고려

---

## 상세 문서
- [Singleton.md](Core/Singleton.md) - 싱글톤 베이스 상세
- [EventManager.md](Core/EventManager.md) - 이벤트 시스템 상세
- [ResourceManager.md](Core/ResourceManager.md) - 리소스 관리 상세
- [SceneLoader.md](Core/SceneLoader.md) - 씬 전환 상세
- [AudioManager.md](Core/AudioManager.md) - 오디오 관리 상세
- [SaveManager.md](Core/SaveManager.md) - 저장 시스템 상세
- [StateMachine.md](Core/StateMachine.md) - 상태 머신 상세

---

## 상태

| 분류 | 파일 수 | 상태 |
|------|---------|------|
| Base | 1 | ⬜ |
| Managers | 6 | ⬜ |
| Systems | 1 | ⬜ |
