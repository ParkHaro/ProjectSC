---
type: spec
assembly: Sc.Common
class: CostConfirmPopup
category: UI
status: draft
version: "1.0"
dependencies: [PopupWidget, NavigationManager, IPopupState, CostType]
created: 2026-01-19
updated: 2026-01-19
---

# CostConfirmPopup

## 역할

재화 소모를 확인하는 확장 팝업 UI. 재화 아이콘과 수량을 시각적으로 표시.

## 책임

- 제목, 메시지 표시
- 재화 아이콘 + 소모량 표시
- 현재 보유량 표시 (선택적)
- 확인/취소 버튼 처리
- 콜백 실행
- 배경 터치 시 취소 처리
- State 유효성 검증 (재화 부족 여부 포함)

## 비책임

- 재화 차감 로직 (서버에서 처리)
- 재화 부족 시 자동 충전 유도
- 네트워크 요청

---

## 설계 원칙

1. **ConfirmPopup 확장**: 동일한 베이스 패턴에 재화 표시 기능 추가
2. **State 검증**: `CostConfirmState.Validate()`로 필수 필드 검증
3. **재화 타입 지원**: CostType enum으로 다양한 재화 지원
4. **부족 상태 표시**: 보유량 < 소모량일 때 시각적 경고

---

## 사용 시나리오

| 시나리오 | Title | CostType | Amount | 버튼 |
|----------|-------|----------|--------|------|
| 상품 구매 | "구매 확인" | Gem | 100 | 구매/취소 |
| 스테이지 입장 | "출전 확인" | Stamina | 10 | 출전/취소 |
| 장비 강화 | "강화 확인" | Gold | 5000 | 강화/취소 |
| 즉시 완료 | "즉시 완료" | Gem | 50 | 완료/취소 |

---

## 인터페이스

### CostConfirmState 필드

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `Title` | string | "확인" | 팝업 제목 |
| `Message` | string | "" | 본문 메시지 |
| `CostType` | CostType | Gold | 소모 재화 타입 |
| `CostAmount` | int | 0 | 소모량 |
| `CurrentAmount` | int? | null | 현재 보유량 (null이면 표시 안 함) |
| `ConfirmText` | string | "확인" | 확인 버튼 텍스트 |
| `CancelText` | string | "취소" | 취소 버튼 텍스트 |
| `OnConfirm` | Action | null | 확인 콜백 |
| `OnCancel` | Action | null | 취소 콜백 |

### State 검증 (Validate)

```csharp
public bool Validate()
{
    if (CostAmount <= 0)
    {
        Debug.LogWarning("[CostConfirmPopup] CostAmount가 0 이하");
        return false;
    }
    return true;
}
```

### 클래스 시그니처

```csharp
public class CostConfirmState : IPopupState { ... }
public class CostConfirmPopup : PopupWidget<CostConfirmPopup, CostConfirmState> { ... }
```

---

## UI 구조

```
[CostConfirmPopup] (PopupWidget)
├── Background (Button - 터치 시 취소)
├── Panel
│   ├── TitleText (TMP_Text)
│   ├── MessageText (TMP_Text)
│   ├── CostDisplay
│   │   ├── CostIcon (Image)
│   │   ├── CostAmountText (TMP_Text, "-100")
│   │   └── CurrentAmountText (TMP_Text, "(보유: 500)", 선택적)
│   └── ButtonGroup
│       ├── CancelButton (Button + TMP_Text)
│       └── ConfirmButton (Button + TMP_Text)
```

---

## 동작 흐름

```
1. CostConfirmPopup.Open(state) 호출
2. state.Validate() 검증
   └── CostAmount <= 0 → false 반환, 팝업 미표시
3. NavigationManager.Push(context)
4. OnBind(state) → UI 바인딩
   ├── TitleText = state.Title
   ├── MessageText = state.Message
   ├── CostIcon = GetCostIcon(state.CostType)
   ├── CostAmountText = $"-{state.CostAmount}"
   ├── CurrentAmountText 설정 (state.CurrentAmount != null일 때)
   ├── 부족 상태면 빨간색 표시
   └── 버튼 텍스트 설정
5. 사용자 선택 대기
   ├── [확인] 클릭 → state.OnConfirm?.Invoke() → Pop()
   ├── [취소] 클릭 → state.OnCancel?.Invoke() → Pop()
   └── [배경] 터치 → state.OnCancel?.Invoke() → Pop()
6. 팝업 닫힘
```

---

## 사용 패턴

### 기본 사용
```csharp
CostConfirmPopup.Open(new CostConfirmState
{
    Title = "구매 확인",
    Message = "이 아이템을 구매하시겠습니까?",
    CostType = CostType.Gem,
    CostAmount = 100,
    OnConfirm = () => ExecutePurchase(productId),
    OnCancel = () => Debug.Log("구매 취소")
});
```

### 보유량 표시
```csharp
CostConfirmPopup.Open(new CostConfirmState
{
    Title = "강화 확인",
    Message = "장비를 강화하시겠습니까?",
    CostType = CostType.Gold,
    CostAmount = 5000,
    CurrentAmount = DataManager.Instance.Currency.Gold,
    ConfirmText = "강화",
    OnConfirm = () => EnhanceEquipment(equipmentId)
});
```

### 스태미나 소모
```csharp
CostConfirmPopup.Open(new CostConfirmState
{
    Title = "출전 확인",
    Message = $"스테이지 {stageId}에 출전합니다.",
    CostType = CostType.Stamina,
    CostAmount = 10,
    CurrentAmount = DataManager.Instance.Currency.Stamina,
    ConfirmText = "출전",
    OnConfirm = () => StartBattle(stageId)
});
```

---

## 재화 타입별 아이콘

| CostType | 아이콘 경로 | 색상 |
|----------|-------------|------|
| Gold | Icons/Currency/Gold | #FFD700 |
| Gem | Icons/Currency/Gem | #00BFFF |
| FreeGem | Icons/Currency/FreeGem | #00FF7F |
| Stamina | Icons/Currency/Stamina | #32CD32 |
| (기타) | Icons/Currency/{CostType} | 기본 |

---

## 부족 상태 표시

```csharp
private void UpdateCostDisplay(CostConfirmState state)
{
    bool isInsufficient = state.CurrentAmount.HasValue
                       && state.CurrentAmount.Value < state.CostAmount;

    // 부족하면 빨간색으로 표시
    _costAmountText.color = isInsufficient ? Color.red : Color.white;

    // 확인 버튼 비활성화 (선택적)
    // _confirmButton.interactable = !isInsufficient;
}
```

> **Note**: 부족 상태에서도 확인 버튼은 활성 상태 유지 (서버에서 최종 검증)

---

## 주의사항

1. **재화 검증은 서버에서**
   - 클라이언트의 CurrentAmount는 표시용
   - 실제 차감 가능 여부는 서버에서 검증

2. **배경 터치 = 취소**
   - ConfirmPopup과 동일하게 처리

3. **CostAmount 검증**
   - 0 이하면 Validate() 실패
   - 팝업이 열리지 않음

4. **아이콘 로드**
   - CostType에 맞는 아이콘 Addressables로 로드
   - 로드 실패 시 기본 아이콘 표시

---

## 프리팹 구조

```
CostConfirmPopup.prefab
├── Canvas (Popup Layer, Sort Order 100)
│   └── CostConfirmPopup (CostConfirmPopup.cs)
│       ├── Background (Button + Image, Raycast Target)
│       └── Panel (RectTransform)
│           ├── Header
│           │   └── TitleText (TMP_Text)
│           ├── Body
│           │   ├── MessageText (TMP_Text)
│           │   └── CostDisplay (HorizontalLayoutGroup)
│           │       ├── CostIcon (Image, 48x48)
│           │       ├── CostAmountText (TMP_Text)
│           │       └── CurrentAmountText (TMP_Text, 작은 폰트)
│           └── Footer (HorizontalLayoutGroup)
│               ├── CancelButton (Button + TMP_Text)
│               └── ConfirmButton (Button + TMP_Text)
```

---

## 관련 문서

- [ConfirmPopup.md](./ConfirmPopup.md) - 기본 확인 팝업
- [UISystem.md](../UISystem.md) - Widget/PopupWidget 베이스
- [Navigation.md](../../Navigation.md) - 팝업 전환
- [Data.md](../../Data.md) - CostType enum 정의
