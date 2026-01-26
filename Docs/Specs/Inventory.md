# Sc.Contents.Inventory

## 개요
아이템 인벤토리 시스템 (Shared)

## 레퍼런스
- `Docs/Design/Reference/Inventory.jpg`

---

## UI 레이아웃 구조

### 전체 구조

```
InventoryScreen (FullScreen)
├─ Header ──────────────────────────────────────────────────────────────────
│   ├─ [Left] BackButton (<) + TitleText (배낭)
│   └─ [Right] CurrencyHUD (골드, 스태미나, 돈, 프리미엄) + HomeButton
│
├─ Content ─────────────────────────────────────────────────────────────────
│   ├─ LeftSideTab ─────────────┬─ MainArea ────────────────────────────────
│   │   ├─ 사용 (선택됨)        │   ├─ FilterBar
│   │   ├─ 성장                 │   │   ├─ CategoryDropdown (전체 ▼)
│   │   ├─ 장비                 │   │   ├─ SortDropdown (기본)
│   │   ├─ 교단                 │   │   └─ SettingsButton (⚙)
│   │   └─ 연성카드             │   │
│   │                           │   └─ ItemGrid (7열, 스크롤)
│   │                           │       └─ ItemSlot x N
│   │                           │           ├─ ItemIcon
│   │                           │           ├─ RarityBg (색상별)
│   │                           │           ├─ CountLabel
│   │                           │           └─ BadgeIcon (선택)
│   │                           │
│   └───────────────────────────┴─ ItemDetailPanel ─────────────────────────
│                                   ├─ EmptyState ("아이템을 선택해주세요")
│                                   └─ DetailView (선택 시)
│                                       ├─ ItemImage
│                                       ├─ ItemName
│                                       ├─ ItemDescription
│                                       ├─ ItemStats
│                                       └─ ActionButtons
│                                           ├─ UseButton (사용)
│                                           └─ SellButton (판매)
│
└───────────────────────────────────────────────────────────────────────────
```

### 영역별 상세

#### 1. Header (공통 ScreenHeader)
| 위치 | 요소 | 설명 |
|------|------|------|
| Left | BackButton | 뒤로가기 (<) |
| Left | TitleText | "배낭" |
| Right | CurrencyHUD | 골드(94,572), 스태미나(102/102), 돈(549,061), 프리미엄(1,809) |
| Right | HomeButton | 홈 버튼 (로비로 이동) |

#### 2. LeftSideTab (카테고리 탭)
| 요소 | 라벨 | 설명 |
|------|------|------|
| **UsageTab** | 사용 | 소비 아이템 (선택 시 초록색 하이라이트) |
| **GrowthTab** | 성장 | 성장 재료 |
| **EquipmentTab** | 장비 | 장비 아이템 |
| **GuildTab** | 교단 | 길드 관련 아이템 |
| **CardTab** | 연성카드 | 카드 관련 아이템 |

#### 3. FilterBar (필터 영역)
| 요소 | 설명 |
|------|------|
| **CategoryDropdown** | 세부 카테고리 필터 (전체 ▼) |
| **SortDropdown** | 정렬 기준 (기본, 이름순, 등급순, 최근순 등) |
| **SettingsButton** | 표시 설정 (그리드 크기, 필터 옵션 등) |

#### 4. ItemGrid (아이템 그리드)
| 요소 | 설명 |
|------|------|
| **ItemSlot** | 개별 아이템 슬롯 (7열 그리드) |
| - RarityBackground | 등급별 배경색 (파랑, 초록, 주황, 보라, 회색) |
| - ItemIcon | 아이템 아이콘 이미지 |
| - CountLabel | 보유 수량 (우하단) |
| - BadgeIcon | 특수 표시 (?, 잠금 등) |
| **ScrollView** | 세로 스크롤 (아이템 많을 시) |

#### 5. ItemDetailPanel (아이템 상세)
| 요소 | 설명 |
|------|------|
| **EmptyState** | 미선택 시 안내 ("아이템을 선택해주세요") |
| **DetailView** | 선택된 아이템 상세 정보 |
| - ItemImage | 아이템 큰 이미지 |
| - ItemName | 아이템 이름 |
| - ItemDescription | 아이템 설명 |
| - ItemStats | 아이템 스탯/효과 |
| **ActionButtons** | 액션 버튼 그룹 |
| - UseButton | 사용 버튼 (소비 아이템) |
| - SellButton | 판매 버튼 |

---

### Prefab 계층 구조

```
InventoryScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgLight, 연한 녹색 패턴)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   └─ ScreenHeader [Prefab]
│   │       ├─ BackButton
│   │       ├─ TitleText
│   │       ├─ CurrencyHUD
│   │       └─ HomeButton
│   │
│   └─ Content (Stretch, Top=60)
│       ├─ LeftSideTab (Anchor: Left, Width=120px)
│       │   └─ VerticalLayoutGroup
│       │       ├─ TabButton (사용) - Selected
│       │       ├─ TabButton (성장)
│       │       ├─ TabButton (장비)
│       │       ├─ TabButton (교단)
│       │       └─ TabButton (연성카드)
│       │
│       ├─ MainArea (Anchor: Stretch, Left=120)
│       │   ├─ FilterBar (Top, 50px)
│       │   │   ├─ CategoryDropdown (Left)
│       │   │   ├─ SortDropdown (Center-Right)
│       │   │   └─ SettingsButton (Right)
│       │   │
│       │   └─ ItemGridContainer (Stretch, Top=50)
│       │       └─ ScrollRect
│       │           └─ GridLayoutGroup (7 columns)
│       │               └─ ItemSlot [Prefab] x N
│       │                   ├─ RarityBackground (Image)
│       │                   ├─ ItemIcon (Image)
│       │                   ├─ CountLabel (TMP_Text)
│       │                   └─ BadgeIcon (Image, optional)
│       │
│       └─ ItemDetailPanel (Anchor: Right, Width=300px)
│           ├─ EmptyState
│           │   └─ GuideText ("아이템을 선택해주세요")
│           │
│           └─ DetailView (비활성 기본)
│               ├─ ItemImageContainer
│               │   └─ ItemImage
│               ├─ ItemInfoGroup
│               │   ├─ ItemName (TMP_Text)
│               │   ├─ ItemDescription (TMP_Text)
│               │   └─ ItemStats (TMP_Text)
│               └─ ActionButtonGroup (Bottom)
│                   ├─ UseButton
│                   └─ SellButton
│
└─ OverlayLayer
```

---

### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| LeftSideTab | TabButtonGroup | `_categoryTabs` |
| FilterBar | Dropdown (Category) | `_categoryDropdown` |
| FilterBar | Dropdown (Sort) | `_sortDropdown` |
| FilterBar | Button (Settings) | `_settingsButton` |
| MainArea | ScrollRect | `_itemScrollRect` |
| MainArea | GridLayoutGroup | `_itemGrid` |
| MainArea | ItemSlot (Prefab) | `_itemSlotPrefab` |
| DetailPanel | GameObject (Empty) | `_emptyState` |
| DetailPanel | GameObject (Detail) | `_detailView` |
| DetailPanel | Image | `_detailItemImage` |
| DetailPanel | TMP_Text | `_detailItemName` |
| DetailPanel | TMP_Text | `_detailItemDescription` |
| DetailPanel | TMP_Text | `_detailItemStats` |
| DetailPanel | Button | `_useButton` |
| DetailPanel | Button | `_sellButton` |

---

### 네비게이션 흐름

```
InventoryScreen
├─ BackButton → 이전 화면 (LobbyScreen 또는 호출 화면)
├─ HomeButton → LobbyScreen
├─ CategoryTab 선택 → 해당 카테고리 아이템 필터링
├─ ItemSlot 선택 → ItemDetailPanel 활성화
├─ UseButton → ItemUsePopup (수량 선택) → 사용 완료
├─ SellButton → ItemSellPopup (수량 선택) → 판매 완료
└─ SettingsButton → FilterSettingsPopup
```

---

### 상태별 UI 변화

| 상태 | UI 변화 |
|------|---------|
| 로딩 중 | ItemGrid에 로딩 스피너 표시 |
| 아이템 없음 | EmptyState에 "보유한 아이템이 없습니다" 표시 |
| 아이템 미선택 | ItemDetailPanel에 "아이템을 선택해주세요" 표시 |
| 아이템 선택 | ItemDetailPanel에 상세 정보 및 액션 버튼 표시 |
| 사용 불가 아이템 | UseButton 비활성화 (회색) |

---

## 참조
- Sc.Common

---

## 구조

```
Contents/OutGame/Inventory/
├── InventoryScreen.cs
├── Sc.Contents.Inventory.asmdef
└── Widgets/
    ├── ItemCard.cs
    ├── ItemDetailWidget.cs
    └── InventoryTabWidget.cs
```

---

## 클래스 목록

### Screen

| 클래스 | 설명 | 상태 |
|--------|------|------|
| InventoryScreen | 인벤토리 화면 (ScreenWidget) | ✅ |

### Widgets

| 클래스 | 설명 | 상태 |
|--------|------|------|
| ItemCard | 아이템 카드 (아이콘, 수량, 등급, 선택) | ✅ |
| ItemDetailWidget | 아이템 상세 정보 위젯 | ✅ |
| InventoryTabWidget | 카테고리 탭 위젯 | ✅ |
| InventoryTabButton | 개별 탭 버튼 컴포넌트 | ✅ |

### Enums

| Enum | 설명 |
|------|------|
| InventoryCategory | Usage, Growth, Equipment, Guild, Card |
| InventorySortType | Default, Name, Rarity, Recent |

---

## Editor

### PrefabBuilder

| 파일 | 위치 |
|------|------|
| InventoryScreenPrefabBuilder.cs | `Assets/Scripts/Editor/Wizard/Generators/` |

메뉴: `Tools/SC/Prefab Builders/Build InventoryScreen`

---

## Addressables 등록

프리팹 생성 후 Addressables 등록 필요:
- `Assets/Prefabs/UI/Screens/InventoryScreen.prefab`
- Address: `Screens/InventoryScreen`
- Group: `UI_Screens`
