---
type: spec
assembly: Sc.Common
class: ConfirmPopup
category: UI
status: draft
version: "1.0"
dependencies: [PopupWidget, NavigationManager]
created: 2026-01-17
updated: 2026-01-17
---

# ConfirmPopup

## 역할

범용 확인/취소 팝업 UI

## 책임

- 제목, 메시지 표시
- 확인/취소 버튼 처리
- 콜백 실행

## 비책임

- 특정 비즈니스 로직 처리
- 데이터 검증
- 네트워크 요청

---

## 사용 시나리오

| 시나리오 | Title | Message | 버튼 |
|----------|-------|---------|------|
| 상품 구매 | "구매 확인" | "100 Gem으로 구매하시겠습니까?" | 확인/취소 |
| 전투 시작 | "출전 확인" | "스태미나 10을 소모합니다" | 출전/취소 |
| 게임 종료 | "종료 확인" | "게임을 종료하시겠습니까?" | 종료/취소 |
| 알림 | "알림" | "스태미나가 부족합니다" | 확인 (단일) |

---

## 인터페이스

### Context 필드

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `Title` | string | "확인" | 팝업 제목 |
| `Message` | string | "" | 본문 메시지 |
| `ConfirmText` | string | "확인" | 확인 버튼 텍스트 |
| `CancelText` | string | "취소" | 취소 버튼 텍스트 |
| `OnConfirm` | Action | null | 확인 콜백 |
| `OnCancel` | Action | null | 취소 콜백 |
| `ShowCancelButton` | bool | true | 취소 버튼 표시 여부 |

### 메서드

| 메서드 | 설명 |
|--------|------|
| `OnConfirmClicked()` | 확인 버튼 클릭 처리 |
| `OnCancelClicked()` | 취소 버튼 클릭 처리 |

---

## UI 구조

```
[ConfirmPopup] (PopupWidget)
├── Background (딤 처리)
├── PopupPanel
│   ├── TitleText (TMP_Text)
│   ├── MessageText (TMP_Text)
│   └── ButtonGroup
│       ├── ConfirmButton (Button + TMP_Text)
│       └── CancelButton (Button + TMP_Text, 조건부 표시)
└── CloseArea (배경 터치 시 취소)
```

---

## 동작 흐름

```
1. ConfirmPopup.Open(context) 호출
2. NavigationManager.PushAsync() → 팝업 표시
3. UI 바인딩 (Title, Message, 버튼 텍스트)
4. 사용자 선택 대기
   ├── [확인] 클릭 → OnConfirm?.Invoke() → Pop()
   └── [취소] 클릭 → OnCancel?.Invoke() → Pop()
5. 팝업 닫힘
```

---

## 사용 패턴

### 기본 사용
```csharp
ConfirmPopup.Open(new ConfirmPopup.Context
{
    Title = "구매 확인",
    Message = $"{price} Gem으로 구매하시겠습니까?",
    OnConfirm = () => ExecutePurchase(productId),
    OnCancel = () => Debug.Log("구매 취소")
});
```

### 단일 버튼 (알림)
```csharp
ConfirmPopup.Open(new ConfirmPopup.Context
{
    Title = "알림",
    Message = "스태미나가 부족합니다.",
    ConfirmText = "확인",
    ShowCancelButton = false
});
```

### 커스텀 버튼 텍스트
```csharp
ConfirmPopup.Open(new ConfirmPopup.Context
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

1. **콜백 null 체크**
   - OnConfirm, OnCancel은 nullable
   - 호출 전 null 체크 또는 `?.Invoke()` 사용

2. **팝업 중복 방지**
   - NavigationManager가 동일 팝업 중복 처리
   - 필요 시 기존 팝업 닫고 새로 열기

3. **배경 터치 처리**
   - 기본: 배경 터치 = 취소
   - 중요 확인 시 배경 터치 비활성화 옵션 고려

4. **Transition**
   - PopupFadeTransition 사용 (기본)
   - Context에서 커스텀 Transition 지정 가능

---

## 프리팹 구조

```
ConfirmPopup.prefab
├── Canvas (Popup Layer)
│   └── ConfirmPopup (ConfirmPopup.cs)
│       ├── Background (Image, Raycast Target)
│       └── Panel
│           ├── Header
│           │   └── TitleText
│           ├── Body
│           │   └── MessageText
│           └── Footer
│               ├── ConfirmButton
│               └── CancelButton
```

---

## 관련 문서

- [PopupWidget.md](../../Common/UISystem.md#popupwidget) - 팝업 베이스
- [Navigation.md](../../Navigation.md) - 팝업 전환
- [RewardPopup.md](./RewardPopup.md) - 보상 팝업
