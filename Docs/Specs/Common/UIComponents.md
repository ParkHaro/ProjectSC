---
type: spec
assembly: Sc.Common
class: UIButton, UIPopup, UIManager
category: UI
status: draft
version: "1.0"
dependencies: [Singleton, BaseView, ResourceManager, AudioManager]
created: 2025-01-14
updated: 2025-01-14
---

# UI Components

## 역할
재사용 가능한 UI 컴포넌트 및 팝업 시스템.

## 책임
- UIButton: 클릭 처리, 효과음, 쿨다운
- UIPopup: 팝업 열기/닫기 애니메이션
- UIManager: 팝업 스택 관리

## 비책임
- 개별 팝업 내부 로직
- 비팝업 UI 관리
- 화면 전환

---

## UIButton

### 인터페이스

| 멤버 | 타입 | 설명 |
|------|------|------|
| OnClick | event Action | 클릭 이벤트 |
| Interactable | bool | 활성화 상태 |
| SetText | void | 버튼 텍스트 설정 |

### 기능
- 클릭 시 효과음 재생 (AudioManager 연동)
- 쿨다운으로 연타 방지 (기본 0.2초)
- 자식 TMP_Text 자동 탐색

### 설정 (Inspector)

| 필드 | 기본값 | 설명 |
|------|--------|------|
| _clickSfxKey | "sfx_button_click" | 클릭 효과음 키 |
| _cooldown | 0.2f | 연타 방지 시간 |

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
       ├─ ConfirmPopup (UIPopup)
       │    ├─ Dim Background
       │    └─ Content
       │         ├─ UIButton (확인)
       │         └─ UIButton (취소)
       │
       └─ SettingsPopup (UIPopup)
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

// 버튼 사용
_button.OnClick += HandleClick;
_button.Interactable = false;
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
- [MVP.md](MVP.md)
