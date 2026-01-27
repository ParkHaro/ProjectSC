# TASK 05: LiveEventScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | LiveEventScreen |
| Reference | `Docs/Design/Reference/LiveEvent.jpg` |
| 스펙 문서 | `Docs/Specs/LiveEvent.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/LiveEventScreenPrefabBuilder.cs` |
| 난이도 | **중** |

---

## 사전 확인

```
1. Docs/Design/Reference/LiveEvent.jpg (이미지 분석)
2. Docs/Specs/LiveEvent.md → "UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/LiveEvent/LiveEventScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
LiveEventScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep, 연두색 그라데이션)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   └─ ScreenHeader [Prefab]
│   │       ├─ ProfileWidget
│   │       ├─ TitleText ("이벤트")
│   │       ├─ CurrencyHUD
│   │       ├─ CharacterIconGroup
│   │       ├─ SettingsButton
│   │       └─ MailButton
│   │
│   └─ Content (Stretch, Top=80)
│       ├─ LeftPanel (Anchor: Left, Width=350)
│       │   └─ EventBannerScrollView (VerticalLayoutGroup)
│       │       └─ EventBannerItem [Template] x N
│       │           ├─ BannerImage
│       │           ├─ EventNameLabel
│       │           ├─ NewBadge
│       │           └─ SelectionHighlight
│       │
│       └─ RightPanel (Anchor: Stretch, Left=350)
│           └─ EventDetailContainer
│               ├─ TitleBannerArea (Top)
│               │   ├─ TitleBannerImage
│               │   └─ DecoElements
│               │
│               ├─ CharacterArea (Center)
│               │   └─ CharacterIllust
│               │
│               ├─ PeriodArea (Bottom-Center)
│               │   ├─ StartTimeLabel
│               │   └─ EndTimeLabel
│               │
│               ├─ RewardPreviewArea (Bottom-Left)
│               │   ├─ PreviewTitleLabel
│               │   └─ RewardIconContainer
│               │       └─ RewardIcon x N
│               │
│               └─ ButtonArea (Bottom-Right)
│                   └─ EnterButton ("바로 가기")
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (80px)

| 요소 | 구현 |
|------|------|
| ProfileWidget | Lv, 닉네임, EXP 바 |
| TitleText | TMP_Text ("이벤트") |
| CurrencyHUD | 골드, 프리미엄 표시 |
| CharacterIconGroup | Image x 3 (미니 캐릭터) |
| SettingsButton | Button (설정 아이콘) |
| MailButton | Button (메일 아이콘 + 뱃지) |

### 2. LeftPanel - EventBannerScrollView (350px 폭)

| 요소 | 구현 |
|------|------|
| ScrollRect | Vertical 스크롤 |
| EventBannerItem | 템플릿 (비활성) |

#### EventBannerItem 구조
```
EventBannerItem (350x80)
├─ BannerImage (Image)
├─ EventNameLabel (TMP_Text)
├─ NewBadge (Image, 분홍 꽃 아이콘)
└─ SelectionHighlight (Image, 선택 시 표시)
```

### 3. RightPanel - EventDetailContainer

#### TitleBannerArea
| 요소 | 구현 |
|------|------|
| TitleBannerImage | Image (이벤트 타이틀) |
| DecoElements | Image (슬라임 등 데코) |

#### CharacterArea
| 요소 | 구현 |
|------|------|
| CharacterIllust | Image (이벤트 캐릭터) |

#### PeriodArea
| 요소 | 구현 |
|------|------|
| StartTimeLabel | TMP_Text ("이벤트 시작: 2026-01-15 11:00") |
| EndTimeLabel | TMP_Text ("이벤트 종료: 2026-01-29 10:59") |

#### RewardPreviewArea
| 요소 | 구현 |
|------|------|
| PreviewTitleLabel | TMP_Text ("참여 시 획득 가능한 보상") |
| RewardIconContainer | HorizontalLayoutGroup |
| RewardIcon | Image (보상 아이콘 템플릿) |

#### ButtonArea
| 요소 | 구현 |
|------|------|
| EnterButton | Button ("바로 가기") |

---

## SerializeField 연결

```csharp
// LiveEventScreen.cs 예상 필드
// Header
[SerializeField] private TMP_Text _levelText;
[SerializeField] private TMP_Text _nicknameText;
[SerializeField] private Slider _expBar;
[SerializeField] private TMP_Text _titleText;
[SerializeField] private Button _settingsButton;
[SerializeField] private Button _mailButton;
[SerializeField] private TMP_Text _mailBadgeText;

// 좌측 패널
[SerializeField] private ScrollRect _eventBannerScrollView;
[SerializeField] private Transform _eventBannerContainer;
[SerializeField] private GameObject _eventBannerItemTemplate;

// 우측 패널
[SerializeField] private Image _titleBannerImage;
[SerializeField] private Image _characterIllust;
[SerializeField] private TMP_Text _startTimeLabel;
[SerializeField] private TMP_Text _endTimeLabel;
[SerializeField] private TMP_Text _previewTitleLabel;
[SerializeField] private Transform _rewardIconContainer;
[SerializeField] private Button _enterButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `LiveEventScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역 (ProfileWidget, CurrencyHUD 등)
- [ ] LeftPanel - EventBannerScrollView
- [ ] LeftPanel - EventBannerItem 템플릿
- [ ] RightPanel - TitleBannerArea
- [ ] RightPanel - CharacterArea
- [ ] RightPanel - PeriodArea
- [ ] RightPanel - RewardPreviewArea
- [ ] RightPanel - ButtonArea
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: LiveEventScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/LiveEventScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header (ProfileWidget, CurrencyHUD)
- [x] LeftPanel (이벤트 배너 목록)
- [x] EventBannerItem 템플릿
- [x] RightPanel - TitleBannerArea
- [x] RightPanel - CharacterArea
- [x] RightPanel - PeriodArea
- [x] RightPanel - RewardPreviewArea
- [x] RightPanel - ButtonArea
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
