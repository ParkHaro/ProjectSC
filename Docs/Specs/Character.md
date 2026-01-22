# Sc.Contents.Character

## 개요
캐릭터 데이터, 스탯, 생성 시스템 (Shared)

## 레퍼런스
- `Docs/Design/Reference/CharacterList.jpg`
- `Docs/Design/Reference/CharacterDetail.jpg`

---

## UI 레이아웃 구조

### CharacterListScreen

#### 전체 구조

```
CharacterListScreen (FullScreen)
├─ Header ──────────────────────────────────────────────────────────────────
│   ├─ [Left] BackButton + TitleText ("사도")
│   └─ [Right] CurrencyHUD (골드: 549,061 / 프리미엄: 1,809) + HomeButton
│
├─ TabArea ─────────────────────────────────────────────────────────────────
│   ├─ TabButton ("모여라 사도!" - 전체 목록)
│   └─ TabButton ("관심 사도 0/2" - 즐겨찾기)
│
├─ FilterArea ──────────────────────────────────────────────────────────────
│   ├─ FilterButton ("감정표현")
│   ├─ FilterToggle ("필터 OFF")
│   ├─ SortButton ("정렬")
│   └─ SortOrderToggle (↓)
│
├─ CharacterGrid ───────────────────────────────────────────────────────────
│   └─ ScrollView (GridLayoutGroup 6열)
│       └─ CharacterCard x N
│           ├─ CardBackground (속성별 색상: 노랑/보라/빨강/초록/파랑)
│           ├─ CharacterThumbnail
│           ├─ ElementIcon (좌상단 속성 아이콘)
│           ├─ RoleIcon (우측 역할 아이콘)
│           ├─ StarRating (★★★★★)
│           └─ NameText ("쉐이디(역전)", "요미", ...)
│
└─ (하단 영역 없음 - 그리드가 전체 사용)
```

#### 영역별 상세

##### 1. Header
| 요소 | 설명 |
|------|------|
| **BackButton** | 이전 화면(로비)으로 돌아가기 |
| **TitleText** | "사도" (캐릭터 목록 화면) |
| **CurrencyHUD** | 골드(549,061), 프리미엄(1,809) 표시 |
| **HomeButton** | 로비로 바로 이동 |

##### 2. TabArea
| 요소 | 설명 |
|------|------|
| **AllCharactersTab** | "모여라 사도!" - 전체 캐릭터 목록 (활성 상태: 연두색) |
| **FavoritesTab** | "관심 사도 0/2" - 즐겨찾기 캐릭터 (최대 2개 설정 가능) |

##### 3. FilterArea
| 요소 | 설명 |
|------|------|
| **ExpressionFilter** | "감정표현" - 캐릭터 감정표현 필터 |
| **FilterToggle** | "필터 OFF" - 필터 활성화/비활성화 |
| **SortButton** | "정렬" - 정렬 기준 선택 팝업 |
| **SortOrderToggle** | 오름차순/내림차순 전환 |

##### 4. CharacterGrid
| 요소 | 설명 |
|------|------|
| **ScrollView** | 스크롤 가능한 캐릭터 카드 그리드 |
| **CharacterCard** | 개별 캐릭터 카드 |
| - CardBackground | 속성별 색상 (노랑/보라/빨강/초록/파랑/분홍) |
| - CharacterThumbnail | 캐릭터 SD 이미지 |
| - ElementIcon | 좌상단 속성 아이콘 (불/물/풍/빛/암) |
| - RoleIcon | 우측 역할 아이콘 (공격/방어/지원 등) |
| - StarRating | 희귀도 표시 (★1~5) |
| - NameText | 캐릭터 이름 |

#### Prefab 계층 구조

```
CharacterListScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   ├─ LeftArea
│   │   │   ├─ BackButton
│   │   │   └─ TitleText ("사도")
│   │   └─ RightArea
│   │       ├─ CurrencyHUD [Prefab]
│   │       └─ HomeButton
│   │
│   ├─ TabArea (Below Header, 50px)
│   │   └─ TabGroup (HorizontalLayoutGroup)
│   │       ├─ AllCharactersTab
│   │       └─ FavoritesTab
│   │
│   ├─ FilterArea (Below TabArea, 45px)
│   │   └─ FilterGroup (HorizontalLayoutGroup)
│   │       ├─ ExpressionFilterButton
│   │       ├─ FilterToggleButton
│   │       ├─ SortButton
│   │       └─ SortOrderButton
│   │
│   └─ Content (Stretch, Top=155px)
│       └─ CharacterGrid
│           └─ ScrollRect
│               └─ GridLayoutGroup (6 columns)
│                   └─ CharacterCard [Prefab] x N
│
└─ OverlayLayer
```

#### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | BackButton | `_backButton` |
| Header | TitleText | `_titleText` |
| Header | CurrencyHUD | `_currencyHUD` |
| Header | HomeButton | `_homeButton` |
| TabArea | AllCharactersTab | `_allCharactersTab` |
| TabArea | FavoritesTab | `_favoritesTab` |
| FilterArea | ExpressionFilterButton | `_expressionFilterButton` |
| FilterArea | FilterToggleButton | `_filterToggleButton` |
| FilterArea | SortButton | `_sortButton` |
| FilterArea | SortOrderButton | `_sortOrderButton` |
| Content | CharacterGrid | `_characterGrid` |
| Content | ScrollRect | `_scrollRect` |

#### 네비게이션 흐름

```
CharacterListScreen
├─ BackButton → LobbyScreen
├─ HomeButton → LobbyScreen
├─ CharacterCard (터치) → CharacterDetailScreen
├─ AllCharactersTab → 전체 목록 표시
├─ FavoritesTab → 즐겨찾기 목록 표시
├─ FilterToggle → 필터 팝업
└─ SortButton → 정렬 옵션 팝업
```

---

### CharacterDetailScreen

#### 전체 구조

```
CharacterDetailScreen (FullScreen)
├─ Header ──────────────────────────────────────────────────────────────────
│   ├─ [Left] BackButton + TitleText ("쉐이디(역전)")
│   └─ [Right] CurrencyHUD (골드: 549,061 / 프리미엄: 1,809) + HomeButton
│
├─ LeftMenuArea ────────────────────────────────────────────────────────────
│   ├─ MenuButton ("정보") - 기본 정보
│   ├─ MenuButton ("레벨업") - 레벨업 화면
│   ├─ MenuButton ("장비") - 장비 관리
│   ├─ MenuButton ("스킬") - 스킬 정보
│   ├─ MenuButton ("승급") - 승급 화면
│   ├─ MenuButton ("보드") - 스킬 보드
│   └─ MenuButton ("어사이드") - 어사이드 스토리
│
├─ CenterArea ──────────────────────────────────────────────────────────────
│   ├─ CharacterDisplay
│   │   ├─ CharacterImage (풀 일러스트)
│   │   └─ CompanionImage (우측 상단 동행 캐릭터)
│   ├─ CharacterSwitch (> 화살표) - 다음 캐릭터
│   └─ DogamButton ("도감") - 캐릭터 도감
│
├─ BottomInfoArea ──────────────────────────────────────────────────────────
│   ├─ RarityBadge (⑤) - 5성
│   ├─ NameText ("쉐이디(역전)")
│   └─ TagGroup
│       ├─ TagBadge ("활발" - 성격)
│       ├─ TagBadge ("서포터" - 역할)
│       ├─ TagBadge ("물리" - 공격 타입)
│       └─ TagBadge ("후열" - 배치)
│
├─ RightTopArea ────────────────────────────────────────────────────────────
│   ├─ LevelInfo
│   │   ├─ LevelText ("Lv. 52")
│   │   └─ StarRating (★★★☆☆)
│   └─ CombatPowerWidget
│       └─ CombatPowerText ("전투력 25,555")
│
├─ RightCenterArea ─────────────────────────────────────────────────────────
│   ├─ StatTabGroup
│   │   ├─ StatTab ("스테이터스") - 활성
│   │   └─ TraitTab ("특성")
│   ├─ StatList
│   │   ├─ HP: 36,250
│   │   ├─ SP: 400
│   │   ├─ 물리 공격력: 1,586
│   │   ├─ 마법 공격력: 215
│   │   ├─ 물리 방어력: 4,391
│   │   └─ 마법 방어력: 4,305
│   ├─ ActionButtons
│   │   ├─ FavoriteButton (하트)
│   │   └─ InfoButton (ⓘ)
│   └─ DetailButton ("상세 보기")
│
└─ RightBottomArea ─────────────────────────────────────────────────────────
    └─ CostumeWidget
        ├─ CostumeIcon (캐릭터 의상 아이콘)
        └─ CostumeText ("쉐이디(역전)의 옷장")
```

#### 영역별 상세

##### 1. Header
| 요소 | 설명 |
|------|------|
| **BackButton** | CharacterListScreen으로 돌아가기 |
| **TitleText** | 캐릭터 이름 ("쉐이디(역전)") |
| **CurrencyHUD** | 골드, 프리미엄 표시 |
| **HomeButton** | 로비로 바로 이동 |

##### 2. LeftMenuArea (좌측 메뉴)
| 요소 | 설명 |
|------|------|
| **InfoButton** | "정보" - 캐릭터 기본 정보 (현재 선택) |
| **LevelUpButton** | "레벨업" - 레벨업 화면으로 이동 |
| **EquipmentButton** | "장비" - 장비 관리 화면 |
| **SkillButton** | "스킬" - 스킬 정보 화면 |
| **PromotionButton** | "승급" - 승급(돌파) 화면 |
| **BoardButton** | "보드" - 스킬 보드 화면 |
| **AsideButton** | "어사이드" - 어사이드 스토리 |

##### 3. CenterArea (중앙 캐릭터 영역)
| 요소 | 설명 |
|------|------|
| **CharacterImage** | 캐릭터 풀 일러스트 (터치 시 상호작용) |
| **CompanionImage** | 우측 상단 동행 캐릭터 (유령 - 호감도 시스템) |
| **CharacterSwitch** | > 화살표 - 다음/이전 캐릭터로 전환 |
| **DogamButton** | "도감" - 캐릭터 도감 화면 |

##### 4. BottomInfoArea (하단 캐릭터 정보)
| 요소 | 설명 |
|------|------|
| **RarityBadge** | 희귀도 표시 (⑤ = 5성) |
| **NameText** | 캐릭터 이름 ("쉐이디(역전)") |
| **TagGroup** | 캐릭터 태그 배지 그룹 |
| - PersonalityTag | 성격 ("활발" - 주황색) |
| - RoleTag | 역할 ("서포터" - 분홍색) |
| - AttackTypeTag | 공격 타입 ("물리" - 노란색) |
| - PositionTag | 배치 ("후열" - 빨간색) |

##### 5. RightTopArea (우측 상단)
| 요소 | 설명 |
|------|------|
| **LevelText** | 현재 레벨 ("Lv. 52") |
| **StarRating** | 돌파 단계 (★★★☆☆ = 3돌파) |
| **CombatPowerWidget** | 전투력 표시 ("전투력 25,555" - 초록 배지) |

##### 6. RightCenterArea (우측 중앙 - 스탯 영역)
| 요소 | 설명 |
|------|------|
| **StatTabGroup** | 스탯/특성 탭 전환 |
| - StatTab | "스테이터스" - 스탯 목록 |
| - TraitTab | "특성" - 특성 목록 |
| **StatList** | 스탯 목록 테이블 |
| - HP | 체력 (36,250) |
| - SP | 스킬 포인트 (400) |
| - PhysicalAttack | 물리 공격력 (1,586) |
| - MagicAttack | 마법 공격력 (215) |
| - PhysicalDefense | 물리 방어력 (4,391) |
| - MagicDefense | 마법 방어력 (4,305) |
| **FavoriteButton** | 하트 - 즐겨찾기 토글 |
| **InfoButton** | ⓘ - 스탯 상세 설명 |
| **DetailButton** | "상세 보기" - 전체 스탯 상세 팝업 |

##### 7. RightBottomArea (우측 하단)
| 요소 | 설명 |
|------|------|
| **CostumeWidget** | 의상 시스템 진입점 |
| - CostumeIcon | 캐릭터 의상 미리보기 아이콘 |
| - CostumeText | "쉐이디(역전)의 옷장" |

#### Prefab 계층 구조

```
CharacterDetailScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep)
│
├─ SafeArea
│   ├─ Header (Top, 60px)
│   │   ├─ LeftArea
│   │   │   ├─ BackButton
│   │   │   └─ TitleText
│   │   └─ RightArea
│   │       ├─ CurrencyHUD [Prefab]
│   │       └─ HomeButton
│   │
│   └─ Content (Stretch, Top=60px)
│       ├─ LeftMenuArea (Anchor: Left, 100px)
│       │   └─ MenuGroup (VerticalLayoutGroup)
│       │       ├─ InfoButton
│       │       ├─ LevelUpButton
│       │       ├─ EquipmentButton
│       │       ├─ SkillButton
│       │       ├─ PromotionButton
│       │       ├─ BoardButton
│       │       └─ AsideButton
│       │
│       ├─ CenterArea (Anchor: Center, Stretch)
│       │   ├─ CharacterDisplay
│       │   │   ├─ CharacterImage
│       │   │   └─ CompanionImage
│       │   ├─ CharacterSwitchButton (>)
│       │   └─ DogamButton
│       │
│       ├─ BottomInfoArea (Anchor: Bottom, 80px)
│       │   ├─ RarityBadge
│       │   ├─ NameText
│       │   └─ TagGroup (HorizontalLayoutGroup)
│       │       └─ TagBadge x4
│       │
│       └─ RightArea (Anchor: Right, 350px)
│           ├─ RightTopArea (Top, 100px)
│           │   ├─ LevelInfo
│           │   │   ├─ LevelText
│           │   │   └─ StarRating
│           │   └─ CombatPowerWidget
│           │
│           ├─ RightCenterArea (Middle, 250px)
│           │   ├─ StatTabGroup
│           │   │   ├─ StatTab
│           │   │   └─ TraitTab
│           │   ├─ StatList
│           │   │   └─ StatRow x6
│           │   ├─ ActionButtonGroup
│           │   │   ├─ FavoriteButton
│           │   │   └─ InfoButton
│           │   └─ DetailButton
│           │
│           └─ RightBottomArea (Bottom, 80px)
│               └─ CostumeWidget
│                   ├─ CostumeIcon
│                   └─ CostumeText
│
└─ OverlayLayer
```

#### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | BackButton | `_backButton` |
| Header | TitleText | `_titleText` |
| Header | CurrencyHUD | `_currencyHUD` |
| Header | HomeButton | `_homeButton` |
| LeftMenu | InfoButton | `_infoButton` |
| LeftMenu | LevelUpButton | `_levelUpButton` |
| LeftMenu | EquipmentButton | `_equipmentButton` |
| LeftMenu | SkillButton | `_skillButton` |
| LeftMenu | PromotionButton | `_promotionButton` |
| LeftMenu | BoardButton | `_boardButton` |
| LeftMenu | AsideButton | `_asideButton` |
| Center | CharacterImage | `_characterImage` |
| Center | CompanionImage | `_companionImage` |
| Center | CharacterSwitchButton | `_characterSwitchButton` |
| Center | DogamButton | `_dogamButton` |
| BottomInfo | RarityBadge | `_rarityBadge` |
| BottomInfo | NameText | `_nameText` |
| BottomInfo | TagGroup | `_tagGroup` |
| RightTop | LevelText | `_levelText` |
| RightTop | StarRating | `_starRating` |
| RightTop | CombatPowerWidget | `_combatPowerWidget` |
| RightCenter | StatTabGroup | `_statTabGroup` |
| RightCenter | StatList | `_statList` |
| RightCenter | FavoriteButton | `_favoriteButton` |
| RightCenter | InfoButton | `_infoButton` |
| RightCenter | DetailButton | `_detailButton` |
| RightBottom | CostumeWidget | `_costumeWidget` |

#### 네비게이션 흐름

```
CharacterDetailScreen
├─ BackButton → CharacterListScreen
├─ HomeButton → LobbyScreen
├─ CharacterSwitchButton → 다음/이전 캐릭터 (같은 화면)
├─ InfoButton → 정보 탭 (현재 화면)
├─ LevelUpButton → CharacterLevelUpScreen
├─ EquipmentButton → CharacterEquipmentScreen
├─ SkillButton → CharacterSkillScreen
├─ PromotionButton → CharacterPromotionScreen
├─ BoardButton → CharacterBoardScreen
├─ AsideButton → CharacterAsideScreen
├─ DogamButton → CharacterDogamScreen
├─ DetailButton → StatDetailPopup
└─ CostumeWidget → CostumeScreen
```

---

### 화면 간 네비게이션 흐름

```
LobbyScreen
    │
    └─ CharacterButton ("사도")
           │
           ▼
CharacterListScreen ◄──────────────────────────────────────────┐
    │                                                           │
    ├─ BackButton → LobbyScreen                                 │
    │                                                           │
    └─ CharacterCard (터치)                                     │
           │                                                    │
           ▼                                                    │
CharacterDetailScreen ──────────────────────────────────────────┤
    │                                                           │
    ├─ BackButton ─────────────────────────────────────────────►┘
    │
    ├─ LevelUpButton → CharacterLevelUpScreen
    ├─ EquipmentButton → CharacterEquipmentScreen
    ├─ SkillButton → CharacterSkillScreen
    ├─ PromotionButton → CharacterPromotionScreen
    ├─ BoardButton → CharacterBoardScreen
    └─ AsideButton → CharacterAsideScreen
```

---

## 참조
- Sc.Common

## 패턴
- **Factory**: 캐릭터 인스턴스 생성
- **Flyweight**: 공유 데이터 최적화

---

## 구조

```
Contents/Shared/Character/
├── Logic/
│   ├── CharacterManager.cs
│   ├── CharacterStats.cs
│   └── Factory/
│       ├── ICharacterFactory.cs
│       └── CharacterFactory.cs
└── UI/
    ├── CharacterListView.cs
    └── CharacterDetailView.cs
```

---

## 클래스 목록

### Logic

| 클래스 | 설명 | 상태 |
|--------|------|------|
| CharacterManager | 캐릭터 컬렉션 관리 | ⬜ |
| CharacterStats | 런타임 스탯 (Flyweight-extrinsic) | ⬜ |
| ICharacterFactory | 팩토리 인터페이스 | ⬜ |
| CharacterFactory | 캐릭터 생성 구현 | ⬜ |

### UI

| 클래스 | 설명 | 상태 |
|--------|------|------|
| CharacterListView | 캐릭터 목록 화면 | ⬜ |
| CharacterDetailView | 캐릭터 상세 화면 | ⬜ |

---

## 상세 문서

- [Enhancement.md](Character/Enhancement.md) - 캐릭터 강화 시스템 (레벨업, 돌파) ✅
