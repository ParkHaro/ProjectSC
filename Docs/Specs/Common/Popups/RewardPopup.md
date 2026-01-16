---
type: spec
assembly: Sc.Common
class: RewardPopup
category: UI
status: draft
version: "1.0"
dependencies: [PopupWidget, RewardInfo, NavigationManager]
created: 2026-01-17
updated: 2026-01-17
---

# RewardPopup

## 역할

획득 보상 목록을 표시하는 범용 팝업 UI

## 책임

- 보상 목록 시각적 표시
- 아이콘 + 수량 표시
- 확인 후 닫기 처리

## 비책임

- 보상 계산
- 보상 적용 (이미 적용된 후 표시만)
- 데이터 저장

---

## 사용 시나리오

| 시나리오 | Title | 보상 예시 |
|----------|-------|-----------|
| 상점 구매 완료 | "구매 완료" | Gold x1000, Item x5 |
| 스테이지 클리어 | "클리어 보상" | Gold, Exp, Item |
| 이벤트 미션 완료 | "미션 보상" | Gem x100, Character 조각 |
| 출석 보상 | "출석 보상" | 재화, 아이템 |

---

## 인터페이스

### Context 필드

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `Title` | string | "획득 보상" | 팝업 제목 |
| `Rewards` | RewardInfo[] | [] | 보상 목록 |
| `OnClose` | Action | null | 닫기 콜백 |

### 자식 위젯

| 클래스 | 역할 |
|--------|------|
| `RewardItem` | 개별 보상 아이템 표시 |

---

## UI 구조

```
[RewardPopup] (PopupWidget)
├── Background (딤 처리)
├── PopupPanel
│   ├── TitleText
│   ├── RewardScrollView
│   │   └── RewardContainer (Horizontal/Grid Layout)
│   │       └── RewardItem[] (동적 생성)
│   │           ├── IconImage
│   │           ├── AmountText
│   │           └── NameText (선택적)
│   └── ConfirmButton
└── CloseArea
```

### RewardItem 구조

```
[RewardItem]
├── IconBackground
│   └── IconImage
├── AmountText ("x100")
└── RarityFrame (선택적, 캐릭터/장비)
```

---

## 동작 흐름

```
1. RewardPopup.Open(context) 호출
2. NavigationManager.PushAsync() → 팝업 표시
3. RewardContainer 자식 초기화 (기존 제거)
4. foreach (reward in Rewards)
   └── RewardItem 생성 및 바인딩
5. 사용자 [확인] 클릭
6. OnClose?.Invoke()
7. Pop() → 팝업 닫힘
```

---

## 사용 패턴

### 기본 사용
```csharp
RewardPopup.Open(new RewardPopup.Context
{
    Title = "클리어 보상",
    Rewards = response.Rewards,
    OnClose = () => NavigationManager.Instance.Back()
});
```

### 커스텀 제목
```csharp
RewardPopup.Open(new RewardPopup.Context
{
    Title = "첫 클리어 보너스!",
    Rewards = firstClearRewards
});
```

### 콜백 체이닝
```csharp
RewardPopup.Open(new RewardPopup.Context
{
    Rewards = rewards,
    OnClose = () =>
    {
        // 보상 팝업 닫힌 후 다음 화면으로 이동
        LobbyScreen.Open();
    }
});
```

---

## RewardItem 아이콘 매핑

| RewardType | 아이콘 소스 |
|------------|-------------|
| Currency | `Icons/Currency/{ItemId}` |
| Character | `CharacterDatabase.Get(ItemId).IconPath` |
| Item | `ItemDatabase.Get(ItemId).IconPath` |
| Exp | `Icons/Exp` |
| CharacterExp | `Icons/CharacterExp` |

### 아이콘 로드 로직
```csharp
private Sprite GetRewardIcon(RewardInfo reward)
{
    return reward.Type switch
    {
        RewardType.Currency => LoadCurrencyIcon(reward.ItemId),
        RewardType.Character => DataManager.Instance.Characters.Get(reward.ItemId)?.Icon,
        RewardType.Item => DataManager.Instance.Items.Get(reward.ItemId)?.Icon,
        _ => _defaultIcon
    };
}
```

---

## 레이아웃 설정

### 보상 수에 따른 레이아웃

| 보상 수 | 레이아웃 | 아이템 크기 |
|---------|----------|-------------|
| 1~4개 | Horizontal | 대형 (120x120) |
| 5~8개 | Grid 2x4 | 중형 (80x80) |
| 9개+ | Grid + Scroll | 소형 (60x60) |

```csharp
private void ConfigureLayout(int rewardCount)
{
    if (rewardCount <= 4)
    {
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _layoutGroup.constraintCount = 1;
        _layoutGroup.cellSize = new Vector2(120, 140);
    }
    else
    {
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layoutGroup.constraintCount = 4;
        _layoutGroup.cellSize = new Vector2(80, 100);
    }
}
```

---

## 연출 (선택적 확장)

### 기본 연출
- 팝업 등장: PopupScaleTransition (0.2s)
- 보상 아이템: 순차 Fade In (0.05s 간격)

### 희귀도별 연출 (Phase 5)
- SSR 캐릭터: 특수 이펙트, 사운드
- 일반 아이템: 기본 등장

---

## 주의사항

1. **빈 보상 처리**
   - Rewards가 비어있으면 팝업 표시하지 않음
   - 또는 "보상 없음" 메시지 표시

2. **대량 보상 처리**
   - 20개 초과 시 스크롤 필수
   - 성능 고려하여 풀링 또는 가상화

3. **중복 보상 병합**
   - 동일 ItemId 보상은 Amount 합산하여 표시
   - `MergeRewards(RewardInfo[])` 유틸리티 사용

4. **팝업 스택**
   - 여러 보상 팝업이 연속 표시될 수 있음
   - NavigationManager 스택으로 순차 처리

---

## 프리팹 구조

```
RewardPopup.prefab
├── Canvas (Popup Layer)
│   └── RewardPopup (RewardPopup.cs)
│       ├── Background
│       └── Panel
│           ├── Header
│           │   └── TitleText
│           ├── Body
│           │   └── RewardScrollView
│           │       └── Viewport
│           │           └── Content (GridLayoutGroup)
│           │               └── RewardItem.prefab (Template)
│           └── Footer
│               └── ConfirmButton

RewardItem.prefab
├── RewardItem (RewardItem.cs)
│   ├── IconBg
│   │   └── Icon (Image)
│   ├── AmountText (TMP_Text)
│   └── RarityFrame (Image, 조건부)
```

---

## 관련 문서

- [Reward.md](../Reward.md) - 보상 시스템
- [PopupWidget.md](../../Common/UISystem.md#popupwidget) - 팝업 베이스
- [ConfirmPopup.md](./ConfirmPopup.md) - 확인 팝업
- [GachaResultPopup.md](../../Gacha/GachaResultPopup.md) - 가챠 결과 (특화된 보상 팝업)
