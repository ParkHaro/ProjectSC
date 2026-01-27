# TASK 02: CharacterDetailScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | CharacterDetailScreen |
| Reference | `Docs/Design/Reference/CharacterDetail.jpg` |
| 스펙 문서 | `Docs/Specs/Character.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/CharacterDetailScreenPrefabBuilder.cs` |
| 난이도 | **상** (복잡한 다중 영역 레이아웃) |

---

## 사전 확인

```
1. Docs/Design/Reference/CharacterDetail.jpg (이미지 분석)
2. Docs/Specs/Character.md → "CharacterDetailScreen UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Character/CharacterDetailScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
CharacterDetailScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (캐릭터 테마 배경)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   ├─ BackButton
│   │   ├─ TitleText (캐릭터 이름)
│   │   ├─ CurrencyHUD
│   │   └─ HomeButton
│   │
│   ├─ LeftMenuArea (Left, 100px)
│   │   └─ MenuList (VerticalLayoutGroup)
│   │       ├─ InfoButton ("정보")
│   │       ├─ LevelUpButton ("레벨업")
│   │       ├─ EquipmentButton ("장비")
│   │       ├─ SkillButton ("스킬")
│   │       ├─ PromotionButton ("승급")
│   │       ├─ BoardButton ("보드")
│   │       └─ AsideButton ("어사이드")
│   │
│   ├─ CenterArea (Center, Flexible)
│   │   ├─ CharacterDisplay
│   │   │   ├─ CharacterImage (풀 일러스트)
│   │   │   └─ CompanionImage (동행 캐릭터)
│   │   ├─ CharacterSwitch (> 화살표)
│   │   └─ DogamButton ("도감")
│   │
│   ├─ BottomInfoArea (Bottom-Left)
│   │   ├─ RarityBadge (⑤)
│   │   ├─ NameText
│   │   └─ TagGroup (HorizontalLayoutGroup)
│   │       └─ TagBadge x 4
│   │
│   ├─ RightTopArea (Right-Top)
│   │   ├─ LevelInfo (Lv. 52, ★★★☆☆)
│   │   └─ CombatPowerWidget (전투력 25,555)
│   │
│   ├─ RightCenterArea (Right-Center)
│   │   ├─ StatTabGroup
│   │   │   ├─ StatTab ("스테이터스")
│   │   │   └─ TraitTab ("특성")
│   │   ├─ StatList (VerticalLayoutGroup)
│   │   │   └─ StatRow x 6
│   │   ├─ ActionButtons
│   │   │   ├─ FavoriteButton
│   │   │   └─ InfoButton
│   │   └─ DetailButton ("상세 보기")
│   │
│   └─ RightBottomArea (Right-Bottom)
│       └─ CostumeWidget
│           ├─ CostumeIcon
│           └─ CostumeText
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (60px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| TitleText | TMP_Text (캐릭터 이름) |
| CurrencyHUD | 공통 프리팹 사용 |
| HomeButton | Button (홈 아이콘) |

### 2. LeftMenuArea (100px 폭)
| 요소 | 구현 |
|------|------|
| MenuList | VerticalLayoutGroup, Spacing: 8 |
| MenuButton | Button + TMP_Text, 선택 시 강조 |

메뉴 버튼 7개: 정보, 레벨업, 장비, 스킬, 승급, 보드, 어사이드

### 3. CenterArea (캐릭터 표시)
| 요소 | 구현 |
|------|------|
| CharacterImage | Image (RawImage for Live2D) |
| CompanionImage | Image (우측 상단, 작은 크기) |
| CharacterSwitch | Button (> 화살표) |
| DogamButton | Button ("도감") |

### 4. BottomInfoArea
| 요소 | 구현 |
|------|------|
| RarityBadge | Image + TMP_Text (⑤) |
| NameText | TMP_Text (캐릭터 이름) |
| TagGroup | HorizontalLayoutGroup |
| TagBadge | Image + TMP_Text (색상별 태그) |

태그 색상:
- 성격(활발): 주황색
- 역할(서포터): 파란색
- 타입(물리): 빨간색
- 배치(후열): 보라색

### 5. RightTopArea
| 요소 | 구현 |
|------|------|
| LevelText | TMP_Text ("Lv. 52") |
| StarRating | HorizontalLayoutGroup + Image x 5 |
| CombatPowerText | TMP_Text ("전투력 25,555") |

### 6. RightCenterArea
| 요소 | 구현 |
|------|------|
| StatTabGroup | HorizontalLayoutGroup |
| StatTab | Toggle (스테이터스/특성) |
| StatList | VerticalLayoutGroup |
| StatRow | HP, SP, 물공, 마공, 물방, 마방 |
| FavoriteButton | Button (하트) |
| InfoButton | Button (ⓘ) |
| DetailButton | Button ("상세 보기") |

### 7. RightBottomArea
| 요소 | 구현 |
|------|------|
| CostumeIcon | Image (의상 아이콘) |
| CostumeText | TMP_Text ("~의 옷장") |

---

## SerializeField 연결

```csharp
// CharacterDetailScreen.cs 예상 필드
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _titleText;
[SerializeField] private Button _homeButton;

// 좌측 메뉴
[SerializeField] private Button _infoButton;
[SerializeField] private Button _levelUpButton;
[SerializeField] private Button _equipmentButton;
[SerializeField] private Button _skillButton;
[SerializeField] private Button _promotionButton;
[SerializeField] private Button _boardButton;
[SerializeField] private Button _asideButton;

// 중앙 영역
[SerializeField] private Image _characterImage;
[SerializeField] private Image _companionImage;
[SerializeField] private Button _characterSwitchButton;
[SerializeField] private Button _dogamButton;

// 하단 정보
[SerializeField] private TMP_Text _rarityText;
[SerializeField] private TMP_Text _characterNameText;
[SerializeField] private Transform _tagContainer;

// 우측 상단
[SerializeField] private TMP_Text _levelText;
[SerializeField] private Transform _starRatingContainer;
[SerializeField] private TMP_Text _combatPowerText;

// 우측 중앙
[SerializeField] private Toggle _statTab;
[SerializeField] private Toggle _traitTab;
[SerializeField] private Transform _statListContainer;
[SerializeField] private Button _favoriteButton;
[SerializeField] private Button _statInfoButton;
[SerializeField] private Button _detailButton;

// 우측 하단
[SerializeField] private Image _costumeIcon;
[SerializeField] private TMP_Text _costumeText;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `CharacterDetailScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] LeftMenuArea (7개 버튼)
- [ ] CenterArea (캐릭터 표시)
- [ ] BottomInfoArea (희귀도, 이름, 태그)
- [ ] RightTopArea (레벨, 전투력)
- [ ] RightCenterArea (스탯, 버튼)
- [ ] RightBottomArea (의상)
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: CharacterDetailScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/CharacterDetailScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] LeftMenuArea (7개 메뉴 버튼)
- [x] CenterArea (캐릭터 표시)
- [x] BottomInfoArea (희귀도, 이름, 태그)
- [x] RightTopArea (레벨, 전투력)
- [x] RightCenterArea (스탯, 버튼)
- [x] RightBottomArea (의상)
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
