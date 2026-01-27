# TASK 07: PartySelectScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | PartySelectScreen |
| Reference | `Docs/Design/Reference/PartySelect.jpg` |
| 스펙 문서 | `Docs/Specs/Stage.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/PartySelectScreenPrefabBuilder.cs` |
| 난이도 | **상** (전투 미리보기 + 캐릭터 그리드) |

---

## 사전 확인

```
1. Docs/Design/Reference/PartySelect.jpg (이미지 분석)
2. Docs/Specs/Stage.md → "PartySelectScreen UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Stage/PartySelectScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
PartySelectScreen (RectTransform: Stretch)
├─ Background
│   └─ BattlePreviewBackground
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   ├─ BackButton
│   │   ├─ StageInfoText ("10-7. 깜빡이는 터널!")
│   │   ├─ CurrencyHUD
│   │   └─ HomeButton
│   │
│   └─ Content (Stretch, Top=80)
│       ├─ LeftArea (Anchor: Left, Width=60%)
│       │   ├─ ElementIndicator (TopLeft)
│       │   │   └─ ElementIcon x 5
│       │   │
│       │   ├─ BattlePreviewArea (Center)
│       │   │   ├─ PartyFormation
│       │   │   │   ├─ FrontLineSlot x 3
│       │   │   │   └─ BackLineSlot x 3
│       │   │   │
│       │   │   └─ EnemyFormation
│       │   │       └─ EnemySpot x N
│       │   │
│       │   ├─ QuickActionBar (BottomLeft)
│       │   │   ├─ AutoFormButton ("일괄 해제")
│       │   │   └─ StageInfoButton ("스테이지 정보")
│       │   │
│       │   └─ FormationSettingButton ("덱 설정")
│       │
│       ├─ StageInfoPanel (TopCenter)
│       │   ├─ EntryInfoGroup
│       │   │   ├─ EntryCostText
│       │   │   └─ RecommendedPowerText
│       │   ├─ BorrowInfoText
│       │   └─ FormationStatusGroup
│       │       ├─ PartyCountText
│       │       └─ CardCountText
│       │
│       └─ RightPanel (Anchor: Right, Width=40%)
│           ├─ TabBar (Top, 50px)
│           │   ├─ RentalTab ("대여")
│           │   ├─ FilterTab ("필터 OFF")
│           │   └─ SortTab ("전투력")
│           │
│           ├─ CharacterGrid (Stretch)
│           │   └─ ScrollView + GridLayoutGroup (3열)
│           │       └─ CharacterSlot [Template] x N
│           │
│           └─ ActionBar (Bottom, 80px)
│               ├─ QuickBattleButton ("빠른전투불가")
│               ├─ StartButton ("출발")
│               └─ AutoButton ("자동OFF")
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (80px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| StageInfoText | TMP_Text ("10-7. 깜빡이는 터널!") |
| CurrencyHUD | 스태미나, 골드, 프리미엄 |
| HomeButton | Button (홈 아이콘) |

### 2. LeftArea (60% 폭)

#### ElementIndicator
| 요소 | 구현 |
|------|------|
| ElementIcon | Image x 5 (불/물/풀/빛/어둠) |

#### BattlePreviewArea
| 요소 | 구현 |
|------|------|
| PartyFormation | 6개 슬롯 (앞줄 3 + 뒷줄 3) |
| FrontLineSlot | Image + 캐릭터 표시 |
| BackLineSlot | Image + 캐릭터 표시 |
| EnemyFormation | 적 위치 표시 (빨간 원형) |

#### QuickActionBar
| 요소 | 구현 |
|------|------|
| AutoFormButton | Button ("일괄 해제") |
| StageInfoButton | Button ("스테이지 정보") |

#### FormationSettingButton
| 요소 | 구현 |
|------|------|
| Button | Button ("덱 설정") |

### 3. StageInfoPanel (TopCenter)

| 요소 | 구현 |
|------|------|
| EntryCostText | TMP_Text ("30 스태미나") |
| RecommendedPowerText | TMP_Text ("권장 전투력: 123,188") |
| BorrowInfoText | TMP_Text ("사도 대여: 0/1") |
| PartyCountText | TMP_Text ("편성된 사도: 6/6") |
| CardCountText | TMP_Text ("편성된 카드: 24/24") |

### 4. RightPanel (40% 폭)

#### TabBar
| 탭 | 구현 |
|------|------|
| RentalTab | Button ("대여") |
| FilterTab | Button ("필터 OFF") |
| SortTab | Button ("전투력") + Toggle (정렬 순서) |

#### CharacterGrid
| 요소 | 구현 |
|------|------|
| ScrollRect | Vertical 스크롤 |
| GridLayoutGroup | 3열, CellSize: 150x200 |
| CharacterSlot | 템플릿 (비활성) |

#### CharacterSlot 구조
```
CharacterSlot (150x200)
├─ Portrait (Image)
├─ ElementIcon (Image, 좌상단)
├─ LevelText (TMP_Text, "Lv.52")
├─ StarGroup (HorizontalLayoutGroup)
│   └─ Star x 5
├─ CombatPowerText (TMP_Text, "25,555")
├─ EquippedBadge (Image, 편성 시 녹색 테두리)
└─ SearchButton (Button, 돋보기)
```

#### ActionBar
| 요소 | 구현 |
|------|------|
| QuickBattleButton | Button ("빠른전투불가") |
| StartButton | Button ("출발") + 비용 표시 |
| AutoButton | Toggle ("자동OFF") |

---

## SerializeField 연결

```csharp
// PartySelectScreen.cs 예상 필드
// Header
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _stageInfoText;
[SerializeField] private Button _homeButton;

// LeftArea
[SerializeField] private Transform _elementIndicator;
[SerializeField] private Transform _frontLineContainer;
[SerializeField] private Transform _backLineContainer;
[SerializeField] private Transform _enemyFormation;
[SerializeField] private Button _autoFormButton;
[SerializeField] private Button _stageInfoButton;
[SerializeField] private Button _formationSettingButton;

// StageInfoPanel
[SerializeField] private TMP_Text _entryCostText;
[SerializeField] private TMP_Text _recommendedPowerText;
[SerializeField] private TMP_Text _borrowInfoText;
[SerializeField] private TMP_Text _partyCountText;
[SerializeField] private TMP_Text _cardCountText;

// RightPanel
[SerializeField] private Button _rentalTab;
[SerializeField] private Button _filterTab;
[SerializeField] private Button _sortTab;
[SerializeField] private Toggle _sortOrderToggle;
[SerializeField] private ScrollRect _characterGrid;
[SerializeField] private Transform _characterGridContent;
[SerializeField] private GameObject _characterSlotTemplate;

// ActionBar
[SerializeField] private Button _quickBattleButton;
[SerializeField] private Button _startButton;
[SerializeField] private Toggle _autoButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `PartySelectScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] LeftArea - ElementIndicator
- [ ] LeftArea - BattlePreviewArea (파티 + 적)
- [ ] LeftArea - QuickActionBar
- [ ] StageInfoPanel
- [ ] RightPanel - TabBar
- [ ] RightPanel - CharacterGrid
- [ ] RightPanel - CharacterSlot 템플릿
- [ ] RightPanel - ActionBar
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: PartySelectScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/PartySelectScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] LeftArea (ElementIndicator, BattlePreviewArea, QuickActionBar)
- [x] StageInfoPanel
- [x] RightPanel - TabBar
- [x] RightPanel - CharacterGrid
- [x] CharacterSlot 템플릿
- [x] RightPanel - ActionBar
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
