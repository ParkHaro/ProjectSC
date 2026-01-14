---
type: spec
assembly: Sc.Core
class: SceneLoader
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton, EventManager, SceneType]
created: 2025-01-14
updated: 2025-01-14
---

# SceneLoader

## 역할
비동기 씬 전환, 로딩 진행률 관리.

## 책임
- 비동기 씬 로드
- 로딩 진행률 콜백 제공
- 현재 씬 상태 추적
- SceneLoadedEvent 발행

## 비책임
- 씬 내부 로직
- 로딩 UI 표시
- 씬 설정/빌드세팅

---

## 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| CurrentScene | SceneType | 현재 씬 |
| IsLoading | bool | 로딩 중 여부 |
| LoadSceneAsync | UniTask | 씬 로드 |

### LoadSceneAsync 시그니처
```
UniTask LoadSceneAsync(SceneType sceneType, Action<float> onProgress = null)
```

---

## SceneType → 씬 이름 매핑

| SceneType | Scene Name |
|-----------|------------|
| Title | TitleScene |
| Lobby | LobbyScene |
| Battle | BattleScene |
| Gacha | GachaScene |

---

## 동작 흐름

```
LoadSceneAsync(SceneType.Battle)
           ↓
     IsLoading = true
           ↓
   SceneManager.LoadSceneAsync
           ↓
    ┌──────────────────┐
    │ 0% → 90% 로딩    │
    │ onProgress 콜백  │
    └──────────────────┘
           ↓
   allowSceneActivation = true
           ↓
      씬 활성화 대기
           ↓
     IsLoading = false
           ↓
   SceneLoadedEvent 발행
```

---

## 사용 패턴

```csharp
// 기본
await SceneLoader.Instance.LoadSceneAsync(SceneType.Battle);

// 진행률 표시
await SceneLoader.Instance.LoadSceneAsync(SceneType.Battle, progress =>
{
    loadingBar.value = progress;
});
```

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 중복 호출 | IsLoading 체크로 방지 |
| 진행률 90% | Unity 특성상 90%에서 잠시 정체 |
| 이벤트 | 완료 시 SceneLoadedEvent 자동 발행 |

## 관련
- [Core.md](../Core.md)
- [Packet/CommonEvents.md](../Packet/CommonEvents.md)
