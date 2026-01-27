# TASK 03: ShopScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | ShopScreen |
| Reference | `Docs/Design/Reference/Shop.jpg` |
| 스펙 문서 | `Docs/Specs/Shop.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs` |
| 난이도 | **중** |

---

## 사전 확인

```
1. Docs/Design/Reference/Shop.jpg (이미지 분석)
2. Docs/Specs/Shop.md → "UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Shop/ShopScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
ShopScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (상점 배경 - 나무 상자 테마)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   ├─ BackButtonGroup
│   │   │   ├─ BackButton
│   │   │   └─ TitleText ("상점")
│   │   └─ RightGroup
│   │       ├─ CurrencyHUD
│   │       └─ HomeButton
│   │
│   ├─ Content (Stretch, Top=60, Bottom=60)
│   │   ├─ LeftArea (Anchor: Left, 280px)
│   │   │   ├─ ShopkeeperDisplay
│   │   │   │   ├─ CharacterImage
│   │   │   │   └─ DialogueBox
│   │   │   │       └─ DialogueText
│   │   │   └─ TabList (VerticalLayoutGroup)
│   │   │       ├─ DailyShopTab (선택됨)
│   │   │       ├─ MiscShopTab
│   │   │       ├─ BattleGemShopTab
│   │   │       ├─ CertificateShopTab
│   │   │       ├─ RecommendShopTab
│   │   │       ├─ FrontierShopTab
│   │   │       └─ YeowooShopTab
│   │   │
│   │   └─ RightArea (Anchor: Stretch, Left=280)
│   │       ├─ ProductContainer
│   │       │   ├─ ProductGrid (GridLayoutGroup 3x2)
│   │       │   │   └─ ShopProductItem x 6 [Template]
│   │       │   └─ ProductGridFooter (HorizontalLayoutGroup)
│   │       │       └─ CategoryShortcut x 3
│   │       └─ ProductGridBackground
│   │
│   └─ Footer (Bottom, 50px)
│       ├─ RefreshTimerGroup
│       │   ├─ RefreshIcon
│       │   └─ RefreshTimerText
│       ├─ SelectAllToggle
│       └─ BulkPurchaseButton
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (60px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| TitleText | TMP_Text ("상점") |
| CurrencyHUD | 공통 프리팹 사용 |
| HomeButton | Button (홈 아이콘) |

### 2. LeftArea (280px 폭)

#### ShopkeeperDisplay
| 요소 | 구현 |
|------|------|
| CharacterImage | Image (상점 NPC) |
| DialogueBox | Image (말풍선 배경) |
| DialogueText | TMP_Text ("오늘은 뭘 보여드릴까요?") |

#### TabList (세로 탭)
| 탭 | 색상/상태 |
|------|------|
| DailyShopTab | 활성: 주황색 |
| MiscShopTab | 비활성: 회색 |
| BattleGemShopTab | 비활성 |
| CertificateShopTab | 비활성 |
| RecommendShopTab | 비활성 |
| FrontierShopTab | 비활성 |
| YeowooShopTab | 비활성 |

### 3. RightArea (상품 그리드)

#### ProductGrid (3열 x 2행)
| 요소 | 구현 |
|------|------|
| GridLayoutGroup | CellSize: 180x220, Spacing: 15 |
| ProductItem | 템플릿 (비활성 상태) |

#### ShopProductItem 구조
```
ShopProductItem (180x220)
├─ CardBackground (Image)
├─ ProductIcon (Image, 중앙)
├─ TagLabel (TMP_Text, 좌상단) - "일일갱신", "15K" 등
├─ ProductName (TMP_Text, 하단)
├─ PurchaseLimit (TMP_Text) - "구매 가능 1/1"
└─ PriceGroup
    ├─ CurrencyIcon
    └─ PriceText
```

#### ProductGridFooter
| 요소 | 구현 |
|------|------|
| CategoryShortcut | Button x 3 (바로가기) |

### 4. Footer (50px)
| 요소 | 구현 |
|------|------|
| RefreshIcon | Image |
| RefreshTimerText | TMP_Text ("갱신까지 10시간 12분") |
| SelectAllToggle | Toggle ("모두 선택 OFF") |
| BulkPurchaseButton | Button ("일괄 구매") |

---

## SerializeField 연결

```csharp
// ShopScreen.cs 예상 필드
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _titleText;
[SerializeField] private Button _homeButton;

// 좌측 영역
[SerializeField] private Image _shopkeeperImage;
[SerializeField] private TMP_Text _dialogueText;

// 탭 목록
[SerializeField] private Button _dailyShopTab;
[SerializeField] private Button _miscShopTab;
[SerializeField] private Button _battleGemShopTab;
[SerializeField] private Button _certificateShopTab;
[SerializeField] private Button _recommendShopTab;
[SerializeField] private Button _frontierShopTab;
[SerializeField] private Button _yeowooShopTab;

// 상품 그리드
[SerializeField] private Transform _productGridContainer;
[SerializeField] private GameObject _productItemTemplate;

// 푸터
[SerializeField] private TMP_Text _refreshTimerText;
[SerializeField] private Toggle _selectAllToggle;
[SerializeField] private Button _bulkPurchaseButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `ShopScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] LeftArea - ShopkeeperDisplay
- [ ] LeftArea - TabList (7개 탭)
- [ ] RightArea - ProductGrid (3x2)
- [ ] RightArea - ProductItem 템플릿
- [ ] RightArea - ProductGridFooter
- [ ] Footer 영역
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: ShopScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] LeftArea (상점 NPC + 7개 탭)
- [x] RightArea (상품 그리드 3x2)
- [x] ProductItem 템플릿
- [x] Footer (타이머, 일괄 구매)
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
