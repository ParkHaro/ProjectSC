---
type: spec
assembly: Sc.Common
class: ConfirmPopup
category: UI
status: draft
version: "2.0"
dependencies: [PopupWidget, NavigationManager, IPopupState]
created: 2026-01-17
updated: 2026-01-19
---

# ConfirmPopup

## 역할

범용 확인/취소 팝업 UI. Alert 모드(단일 버튼)도 지원.

## 책임

- 제목, 메시지 표시
- 확인/취소 버튼 처리
- 콜백 실행
- 배경 터치 시 취소 처리
- State 유효성 검증

## 비책임

- 특정 비즈니스 로직 처리
- 재화 표시 (→ CostConfirmPopup)
- 네트워크 요청

---

## 설계 원칙 (v2.0)

1. **기존 패턴 호환**: `PopupWidget<TPopup, TState>` 상속 (GachaResultPopup과 동일)
2. **State 검증**: `ConfirmState.Validate()`로 표시 전 유효성 검증
3. **Alert 모드**: `ShowCancelButton = false`로 단일 버튼 팝업 지원
4. **배경 터치**: 취소 버튼과 동일하게 처리 (OnCancel 콜백 실행)

---

## 사용 시나리오

| 시나리오 | Title | Message | 모드 |
|----------|-------|---------|------|
| 상품 구매 | "구매 확인" | "100 Gem으로 구매하시겠습니까?" | 확인/취소 |
| 전투 시작 | "출전 확인" | "스태미나 10을 소모합니다" | 확인/취소 |
| 게임 종료 | "종료 확인" | "게임을 종료하시겠습니까?" | 확인/취소 |
| 알림 (Alert) | "알림" | "스태미나가 부족합니다" | 확인만 |

---

## 인터페이스

### ConfirmState 필드

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `Title` | string | "확인" | 팝업 제목 |
| `Message` | string | "" | 본문 메시지 |
| `ConfirmText` | string | "확인" | 확인 버튼 텍스트 |
| `CancelText` | string | "취소" | 취소 버튼 텍스트 |
| `ShowCancelButton` | bool | true | 취소 버튼 표시 여부 (false = Alert 모드) |
| `OnConfirm` | Action | null | 확인 콜백 |
| `OnCancel` | Action | null | 취소 콜백 |

### State 검증 (Validate)

```csharp
public bool Validate()
{
    // Message가 비어있으면 경고 (표시는 허용)
    if (string.IsNullOrEmpty(Message))
        Debug.LogWarning("[ConfirmPopup] Message가 비어있음");
    return true;
}
```

### 클래스 시그니처

```csharp
public class ConfirmState : IPopupState { ... }
public class ConfirmPopup : PopupWidget<ConfirmPopup, ConfirmState> { ... }
```

---

## UI 구조

```
[ConfirmPopup] (PopupWidget)
├── Background (Button - 터치 시 취소)
├── Panel
│   ├── TitleText (TMP_Text)
│   ├── MessageText (TMP_Text)
│   └── ButtonGroup
│       ├── ConfirmButton (Button + TMP_Text)
│       └── CancelButton (Button + TMP_Text, ShowCancelButton=false면 숨김)
```

---

## 동작 흐름

```
1. ConfirmPopup.Open(state) 호출
2. state.Validate() 검증
3. NavigationManager.Push(context)
4. OnBind(state) → UI 바인딩
   ├── TitleText = state.Title
   ├── MessageText = state.Message
   ├── ConfirmButton.text = state.ConfirmText
   ├── CancelButton.text = state.CancelText
   └── CancelButton.SetActive(state.ShowCancelButton)
5. 사용자 선택 대기
   ├── [확인] 클릭 → state.OnConfirm?.Invoke() → Pop()
   ├── [취소] 클릭 → state.OnCancel?.Invoke() → Pop()
   └── [배경] 터치 → state.OnCancel?.Invoke() → Pop()
6. 팝업 닫힘
```

---

## 사용 패턴

### 확인/취소 모드
```csharp
ConfirmPopup.Open(new ConfirmState
{
    Title = "구매 확인",
    Message = "100 Gem으로 구매하시겠습니까?",
    OnConfirm = () => ExecutePurchase(productId),
    OnCancel = () => Debug.Log("구매 취소")
});
```

### Alert 모드 (단일 버튼)
```csharp
ConfirmPopup.Open(new ConfirmState
{
    Title = "알림",
    Message = "스태미나가 부족합니다.",
    ShowCancelButton = false
});
```

### 커스텀 버튼 텍스트
```csharp
ConfirmPopup.Open(new ConfirmState
{
    Title = "출전 확인",
    Message = "스태미나 10을 소모합니다.",
    ConfirmText = "출전",
    CancelText = "돌아가기",
    OnConfirm = () => StartBattle(stageId)
});
```

---

## 주의사항

1. **배경 터치 = 취소**
   - 배경 터치 시 OnCancel 콜백 실행 후 닫힘
   - Alert 모드에서도 배경 터치로 닫기 가능

2. **콜백 null 체크**
   - OnConfirm, OnCancel은 nullable
   - `?.Invoke()` 사용으로 안전하게 호출

3. **ESC 키 처리**
   - OnEscape() → true 반환 (닫기 허용)
   - 내부적으로 취소와 동일하게 처리

4. **Transition**
   - 기본: PopupScaleTransition (스케일 + 페이드)
   - Open() 호출 시 커스텀 Transition 지정 가능

---

## 프리팹 구조

```
ConfirmPopup.prefab
├── Canvas (Popup Layer, Sort Order 100)
│   └── ConfirmPopup (ConfirmPopup.cs)
│       ├── Background (Button + Image, Raycast Target)
│       └── Panel (RectTransform)
│           ├── Header
│           │   └── TitleText (TMP_Text)
│           ├── Body
│           │   └── MessageText (TMP_Text)
│           └── Footer (HorizontalLayoutGroup)
│               ├── CancelButton (Button + TMP_Text)
│               └── ConfirmButton (Button + TMP_Text)
```

---

## 관련 문서

- [UISystem.md](../UISystem.md) - Widget/PopupWidget 베이스
- [Navigation.md](../../Navigation.md) - 팝업 전환
- [CostConfirmPopup.md](./CostConfirmPopup.md) - 재화 확인 팝업
- [RewardPopup.md](./RewardPopup.md) - 보상 팝업
