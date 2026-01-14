---
type: spec
assembly: Sc.Common
class: UIPopup, UIManager
category: UI
status: draft
version: "1.1"
dependencies: [Singleton, ResourceManager, AudioManager]
created: 2025-01-14
updated: 2025-01-15
---

# UI Components

## 역할
Unity 기본 컴포넌트를 보완하는 커스텀 UI 컴포넌트 및 팝업 시스템.

## 핵심 원칙

> **Unity 기본 컴포넌트 우선 사용**
>
> 커스텀 UIComponent는 기본 컴포넌트로 해결할 수 없는 경우에만 생성한다.

### 컴포넌트 사용 기준

| 기능 | 사용할 컴포넌트 | 커스텀 필요 |
|------|----------------|-------------|
| 텍스트 표시 | TMP_Text | X |
| 이미지 표시 | Image | X |
| 버튼 클릭 | Button | X |
| 토글 | Toggle | X |
| 슬라이더 | Slider | X |
| 입력 필드 | TMP_InputField | X |
| 스크롤 | ScrollRect | X |
| 팝업 관리 | - | O (UIManager) |
| 팝업 애니메이션 | - | O (UIPopup) |
| 가상화 리스트 | - | O (필요시) |

### 커스텀 컴포넌트 생성 조건

1. **Unity 기본 컴포넌트에 해당 기능이 없음**
2. **여러 Widget에서 반복되는 복합 동작** (예: 팝업 열기/닫기 + 딤 + 스택)
3. **성능 최적화 필요** (예: 대량 아이템 가상화 리스트)

### 확장이 필요한 경우

기본 컴포넌트 기능 확장은 Widget 레이어에서 처리:

```csharp
// ❌ 잘못된 예: 커스텀 UIButton 생성
public class UIButton : MonoBehaviour { ... }

// ✅ 올바른 예: Widget에서 Button 활용
public class ButtonWidget : Widget
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _label;

    public override void OnInitialize()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        AudioManager.Instance.PlaySfx("sfx_button_click");
        OnClick?.Invoke();
    }
}
```

## 책임
- UIPopup: 팝업 열기/닫기 애니메이션, 딤 처리
- UIManager: 팝업 스택 관리, 뒤로가기 처리

## 비책임
- 개별 팝업 내부 로직
- 비팝업 UI 관리
- 화면 전환

---

## UIPopup

### 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| Show() | void | 열기 애니메이션 |
| Close() | void | 닫기 애니메이션 → Hide |
| CloseOnBackButton | bool | 뒤로가기로 닫기 여부 |

### 기능
- 열기/닫기 Scale 애니메이션 (DOTween)
- 배경 딤 처리 및 클릭으로 닫기
- UIManager 스택 자동 연동

### 설정 (Inspector)

| 필드 | 기본값 | 설명 |
|------|--------|------|
| _dimBackground | - | 배경 딤 Image |
| _animationDuration | 0.2f | 애니메이션 시간 |
| _closeOnBackButton | true | ESC/뒤로가기로 닫기 |
| _closeOnDimClick | true | 배경 클릭으로 닫기 |

### 애니메이션 흐름

```
Show()
   ├─ 딤: Alpha 0 → 0.5
   └─ 컨텐츠: Scale 0 → 1 (OutBack)

Close()
   ├─ 딤: Alpha 0.5 → 0
   └─ 컨텐츠: Scale 1 → 0 (InBack)
       ↓
   UIManager.ClosePopup()
       ↓
     Hide()
```

---

## UIManager

### 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| ShowPopup\<T\> | T | 팝업 표시 및 스택 푸시 |
| ClosePopup | void | 스택에서 제거 |
| CloseTopPopup | void | 최상위 팝업 닫기 |
| CloseAllPopups | void | 전체 팝업 닫기 |
| CurrentPopup | UIPopup | 최상위 팝업 |
| PopupCount | int | 열린 팝업 수 |

### 동작 흐름

```
ShowPopup<ConfirmPopup>()
       ↓
  캐시 확인
   ├─ 있음 → 재사용
   └─ 없음 → ResourceManager.LoadAsync
                   ↓
              Instantiate → 캐싱
       ↓
  Stack.Push(popup)
       ↓
  popup.Show()
```

### 뒤로가기 처리

```
Update (ESC 감지)
       ↓
  PopupStack.Count > 0?
   └─ Yes → CurrentPopup.CloseOnBackButton?
              └─ Yes → popup.Close()
```

---

## 컴포넌트 계층

```
UIManager (Singleton)
   └─ Popup Stack
       ├─ ConfirmPopup (UIPopup 상속)
       │    ├─ Dim Background (Image)
       │    └─ Content
       │         ├─ Button (확인) ← Unity 기본
       │         └─ Button (취소) ← Unity 기본
       │
       └─ SettingsPopup (UIPopup 상속)
            └─ ...
```

---

## 사용 패턴

```csharp
// 팝업 열기
var popup = UIManager.Instance.ShowPopup<ConfirmPopup>();
popup.SetMessage("삭제하시겠습니까?");
popup.OnConfirm += DeleteItem;

// 팝업 닫기
UIManager.Instance.CloseTopPopup();
```

---

## 팝업 네이밍 규칙

| 팝업 타입 | 프리팹 키 |
|-----------|----------|
| ConfirmPopup | popup_confirmpopup |
| SettingsPopup | popup_settingspopup |
| RewardPopup | popup_rewardpopup |

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 캐싱 | 한번 생성된 팝업은 Hide만, Destroy 안 함 |
| 스택 | LIFO, 뒤로가기는 최상위만 닫음 |
| DOTween | 의존성 필요 |
| 메모리 | 씬 전환 시 CloseAllPopups 권장 |

## 관련
- [Common.md](../Common.md)
- [UISystem.md](UISystem.md)
