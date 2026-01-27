# TASK 08: InventoryScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | InventoryScreen |
| Reference | `Docs/Design/Reference/Inventory.jpg` |
| 스펙 문서 | `Docs/Specs/Inventory.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/InventoryScreenPrefabBuilder.cs` |
| 난이도 | **중** |

---

## 사전 확인

```
1. Docs/Design/Reference/Inventory.jpg (이미지 분석)
2. Docs/Specs/Inventory.md → "UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Inventory/InventoryScreen.cs (신규 생성 필요할 수 있음)
```

---

## Prefab 계층 구조 (구현 목표)

```
InventoryScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgLight, 연한 녹색 패턴)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   ├─ BackButton
│   │   ├─ TitleText ("배낭")
│   │   ├─ CurrencyHUD
│   │   └─ HomeButton
│   │
│   └─ Content (Stretch, Top=60)
│       ├─ LeftSideTab (Anchor: Left, Width=120px)
│       │   └─ VerticalLayoutGroup
│       │       ├─ UsageTab ("사용") - Selected
│       │       ├─ GrowthTab ("성장")
│       │       ├─ EquipmentTab ("장비")
│       │       ├─ GuildTab ("교단")
│       │       └─ CardTab ("연성카드")
│       │
│       ├─ MainArea (Anchor: Stretch, Left=120, Right=300)
│       │   ├─ FilterBar (Top, 50px)
│       │   │   ├─ CategoryDropdown ("전체")
│       │   │   ├─ SortDropdown ("기본")
│       │   │   └─ SettingsButton
│       │   │
│       │   └─ ItemGridContainer (Stretch, Top=50)
│       │       └─ ScrollRect
│       │           └─ Content (GridLayoutGroup, 7열)
│       │               └─ ItemSlot [Template] x N
│       │
│       └─ ItemDetailPanel (Anchor: Right, Width=300px)
│           ├─ EmptyState
│           │   └─ GuideText ("아이템을 선택해주세요")
│           │
│           └─ DetailView (비활성 기본)
│               ├─ ItemImageContainer
│               │   └─ ItemImage
│               ├─ ItemInfoGroup
│               │   ├─ ItemName
│               │   ├─ ItemDescription
│               │   └─ ItemStats
│               └─ ActionButtonGroup
│                   ├─ UseButton ("사용")
│                   └─ SellButton ("판매")
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (60px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| TitleText | TMP_Text ("배낭") |
| CurrencyHUD | 골드, 스태미나, 돈, 프리미엄 |
| HomeButton | Button (홈 아이콘) |

### 2. LeftSideTab (120px 폭)

| 탭 | 상태 |
|------|------|
| UsageTab | "사용" - 선택됨 (초록색) |
| GrowthTab | "성장" - 비선택 |
| EquipmentTab | "장비" - 비선택 |
| GuildTab | "교단" - 비선택 |
| CardTab | "연성카드" - 비선택 |

### 3. FilterBar (50px)

| 요소 | 구현 |
|------|------|
| CategoryDropdown | TMP_Dropdown ("전체 ▼") |
| SortDropdown | TMP_Dropdown ("기본") |
| SettingsButton | Button (⚙ 아이콘) |

### 4. ItemGridContainer

| 요소 | 구현 |
|------|------|
| ScrollRect | Vertical 스크롤 |
| GridLayoutGroup | 7열, CellSize: 90x90, Spacing: 5 |
| ItemSlot | 템플릿 (비활성) |

#### ItemSlot 구조
```
ItemSlot (90x90)
├─ RarityBackground (Image, 등급별 색상)
│   - 파랑: 일반
│   - 초록: 고급
│   - 주황: 희귀
│   - 보라: 에픽
│   - 회색: 잠금/미획득
├─ ItemIcon (Image, 중앙)
├─ CountLabel (TMP_Text, 우하단) - "x99"
└─ BadgeIcon (Image, 선택적) - "?", 잠금 등
```

### 5. ItemDetailPanel (300px 폭)

#### EmptyState
| 요소 | 구현 |
|------|------|
| GuideText | TMP_Text ("아이템을 선택해주세요") |

#### DetailView
| 요소 | 구현 |
|------|------|
| ItemImage | Image (큰 아이콘) |
| ItemName | TMP_Text (아이템 이름) |
| ItemDescription | TMP_Text (설명, 여러 줄) |
| ItemStats | TMP_Text (효과/스탯) |
| UseButton | Button ("사용") |
| SellButton | Button ("판매") |

---

## SerializeField 연결

```csharp
// InventoryScreen.cs 예상 필드
// Header
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _titleText;
[SerializeField] private Button _homeButton;

// LeftSideTab
[SerializeField] private Button _usageTab;
[SerializeField] private Button _growthTab;
[SerializeField] private Button _equipmentTab;
[SerializeField] private Button _guildTab;
[SerializeField] private Button _cardTab;

// FilterBar
[SerializeField] private TMP_Dropdown _categoryDropdown;
[SerializeField] private TMP_Dropdown _sortDropdown;
[SerializeField] private Button _settingsButton;

// ItemGrid
[SerializeField] private ScrollRect _itemScrollRect;
[SerializeField] private Transform _itemGridContent;
[SerializeField] private GameObject _itemSlotTemplate;

// ItemDetailPanel
[SerializeField] private GameObject _emptyState;
[SerializeField] private GameObject _detailView;
[SerializeField] private Image _detailItemImage;
[SerializeField] private TMP_Text _detailItemName;
[SerializeField] private TMP_Text _detailItemDescription;
[SerializeField] private TMP_Text _detailItemStats;
[SerializeField] private Button _useButton;
[SerializeField] private Button _sellButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `InventoryScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의 (등급별 색상 포함)
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] LeftSideTab (5개 탭)
- [ ] FilterBar (Dropdown x 2 + Button)
- [ ] ItemGridContainer (ScrollRect + GridLayoutGroup)
- [ ] ItemSlot 템플릿
- [ ] ItemDetailPanel - EmptyState
- [ ] ItemDetailPanel - DetailView
- [ ] ActionButtonGroup (사용, 판매)
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: InventoryScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/InventoryScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] LeftSideTab (5개 카테고리 탭)
- [x] FilterBar (Dropdown, 정렬, 설정)
- [x] ItemGridContainer (7열 그리드)
- [x] ItemSlot 템플릿
- [x] ItemDetailPanel - EmptyState
- [x] ItemDetailPanel - DetailView
- [x] ActionButtonGroup
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- InventoryScreen.cs 클래스가 없다면 신규 생성 필요
- (기타 있다면 기록)
```
