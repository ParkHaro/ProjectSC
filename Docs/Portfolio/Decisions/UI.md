# UI Decisions

UI 시스템 관련 의사결정 기록.

---

## UI 아키텍처: MVP → Widget

**일자**: 2024-XX-XX | **상태**: 결정됨

### 컨텍스트
초기 MVP(Model-View-Presenter) 패턴 검토. Unity GameObject 기반 UI와 괴리 발견.

### 선택지
1. **MVP 패턴** - 테스트 용이하나 Unity와 맞지 않음
2. **Widget 기반** - Unity 친화적, 계층 구조 자연스러움 (선택)
3. **MVVM** - 데이터 바인딩 강점이나 구현 복잡

### 결정
**Widget 기반 시스템**
- GameObject 계층과 자연스럽게 매핑
- Prefab 기반 워크플로우 호환

### 결과
Screen/Popup/Navigation 구조 직관적 구현. 테스트는 PlayMode로 대응.

---

## Unity 기본 컴포넌트 Widget화

**일자**: 2025-01-15 | **상태**: 결정됨

### 컨텍스트
Unity 기본 컴포넌트(Button, Text 등)를 Widget 라이프사이클과 통합 필요.

### 선택지
1. **직접 사용** - 추가 코드 없으나 라이프사이클 불일치
2. **Widget 래퍼** - 일관된 라이프사이클 (선택)
3. **확장 메서드** - 기존 컴포넌트 유지하나 통합 어려움

### 결정
**Widget 래퍼 클래스**
- OnInitialize에서 리스너 등록, OnRelease에서 정리
- 8개 구현: Text, Button, Image, Slider, Toggle, InputField, ProgressBar, ScrollView

### 결과
Screen/Popup 코드가 깔끔해짐. 메모리 누수 방지.

---

## Screen/Popup 인스턴스 로딩 방식

**일자**: 2025-01-16 | **상태**: 결정됨

### 컨텍스트
NavigationManager가 Screen/Popup을 어떻게 얻을지 결정 필요. MVP 테스트 우선.

### 선택지
1. **ScreenProvider (Addressables)** - 런타임 로딩, 복잡
2. **Resources.Load** - 단순하나 Resources 폴더 제약
3. **씬 배치 + FindObjectOfType** - 즉시 구현 가능 (선택)

### 결정
**씬 배치 + FindObjectOfType** (MVP 단계)
- 에디터에서 UI 구조 확인 용이
- 추후 ScreenProvider로 마이그레이션 가능

### 결과
MVPSceneSetup에서 모든 Screen/Popup 씬에 배치. Canvas.enabled로 가시성 제어.

---

## Navigation API 간소화: Open() 패턴

**일자**: 2026-01-16 | **상태**: 결정됨

### 컨텍스트
```csharp
// 기존 (장황)
NavigationManager.Instance?.Push(LobbyScreen.CreateContext(new LobbyState()));
```

### 결정
**static Open() 메서드**
```csharp
// 개선 (간결)
LobbyScreen.Open(new LobbyState());
```

### 결과
개발자 경험 향상. 기존 CreateContext 빌더도 유지 (고급 사용).

---

## Screen/Popup Transition 애니메이션

**일자**: 2025-01-17 | **상태**: 결정됨

### 컨텍스트
전환 시 애니메이션 없어 사용자 경험 부족. DOTween 이미 포함.

### 선택지
1. **Unity Animation** - 네이티브지만 설정 복잡
2. **DOTween 직접** - 유연하나 코드 분산
3. **Transition 추상화 + DOTween** - 일관된 인터페이스 (선택)

### 결정
**Transition 추상화**
```csharp
public abstract class Transition
{
    public abstract UniTask Enter(Widget widget);
    public abstract UniTask Exit(Widget widget);
}
```

### 결과
- FadeTransition (Screen용)
- PopupScaleTransition (Popup용)
- Widget.CachedCanvasGroup으로 성능 최적화

---

## SystemPopup 하이브리드 구조

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
확인/취소, 알림, 재화 소비 확인 팝업 필요. 기존 패턴과 일관성 유지.

### 결정
**기존 패턴 + State.Validate()**
```
PopupWidget<TPopup, TState>
    ↑
ConfirmPopup (ConfirmState)      ← ShowCancelButton=false로 Alert 모드
CostConfirmPopup (CostConfirmState)
```

### 결과
GachaResultPopup과 동일 패턴. 클린 아키텍처 핵심 개념만 흡수.
