---
type: spec
assembly: Sc.Contents.Stage
category: System
status: draft
version: "3.1"
dependencies: [Sc.Common, Sc.Packet, Sc.Data, Sc.Event, Sc.Contents.Character]
created: 2026-01-17
updated: 2026-01-20
changelog:
  - "3.1: Dungeon → StageCategory 용어 변경, Content Module 구현 완료"
  - "3.0: 컨텐츠 모듈 패턴 설계"
---

# Sc.Contents.Stage

## 목적

인게임 전투(Stage) 선택, 파티 편성, 전투 시작까지의 아웃게임 → 인게임 브릿지 시스템

---

## 레퍼런스

- `Docs/Design/Reference/StageSelectScreen.jpg` - 스테이지 선택 화면
- `Docs/Design/Reference/PartySelect.jpg` - 파티 편성 화면
- `Docs/Design/Reference/StageDashbaord.jpg` - 인게임 컨텐츠 대시보드

---

## StageSelectScreen UI 레이아웃 구조

### 전체 구조

```
StageSelectScreen (FullScreen)
├─ ScreenHeader ─────────────────────────────────────────────────────────
│   ├─ [Left] BackButton + TitleText ("스테이지 리스트")
│   └─ [Right] CurrencyHUD (스태미나 102/102, 골드 549,061, 프리미엄 1,809)
│
├─ RightTopArea ────────────────────────────────────────────────────────
│   └─ StageProgressWidget ("11-10 최후의 방어선! 알프트반선!")
│
├─ StageMapArea (중앙 전체) ─────────────────────────────────────────────
│   ├─ MapBackground (도시 배경)
│   │
│   ├─ StageNodes (아이소메트릭 맵)
│   │   ├─ StageNode (10-3) ★★☆
│   │   ├─ StageNode (10-4) ★★☆
│   │   ├─ StageNode (10-5) ★★☆
│   │   ├─ StageNode (10-6) ★★★
│   │   ├─ StageNode (10-7) ★★★ ← 현재 선택
│   │   ├─ StageNode (10-8) ★★☆
│   │   ├─ StageNode (10-9) ★★★
│   │   └─ StageNode (10-10) ★★☆
│   │
│   ├─ ChapterNavigation
│   │   ├─ PrevChapter ("<" 이전 월드)
│   │   └─ NextChapter (">" 다음 월드)
│   │
│   └─ StageInfoBubble (선택 노드 상단)
│       ├─ RecommendedPower ("권장 전투력: 117,660")
│       ├─ StageName ("깜빡이는 터널!")
│       ├─ EnemyPreview (적 캐릭터 미리보기)
│       └─ PartyPreview (내 파티 캐릭터)
│
├─ StarProgressBar (좌하단) ──────────────────────────────────────────────
│   ├─ CurrentStars (★ 14/30)
│   └─ RewardMilestones (10→25, 20→50, 30→100)
│
└─ FooterArea ───────────────────────────────────────────────────────────
    ├─ DifficultyTabs
    │   ├─ NormalTab ("순한맛")
    │   ├─ HardTab ("매운맛")
    │   └─ HellTab ("핵불맛")
    └─ WorldMapButton ("세계지도")
```

### 영역별 상세

#### 1. ScreenHeader (상단 헤더)
| 요소 | 설명 |
|------|------|
| **BackButton** | 이전 화면으로 돌아가기 |
| **TitleText** | "스테이지 리스트" |
| **CurrencyHUD** | 스태미나(102/102), 골드(549,061), 프리미엄(1,809) |

#### 2. RightTopArea (우상단)
| 요소 | 설명 |
|------|------|
| **StageProgressWidget** | 현재/최고 진행 스테이지 표시 ("11-10 최후의 방어선! 알프트반선!") |
| - ProgressLabel | 스테이지 번호 + 이름 |
| - NavigateButton | 해당 스테이지로 바로 이동 (>>) |

#### 3. StageMapArea (중앙 맵 영역)
| 요소 | 설명 |
|------|------|
| **MapBackground** | 아이소메트릭 도시 배경 (챕터별 다름) |
| **StageNodes** | 스테이지 노드 (격자 배치) |
| - StageNode | 개별 스테이지 (번호 + 별점) |
| - SelectedNode | 선택된 노드 (하이라이트 + 캐릭터 표시) |
| - LockedNode | 잠긴 노드 (자물쇠 아이콘) |
| **ChapterNavigation** | 챕터 이동 화살표 |
| - PrevChapter | "< 이전 월드" |
| - NextChapter | "> 다음 월드" |

#### 4. StageInfoBubble (스테이지 정보 버블)
| 요소 | 설명 |
|------|------|
| **RecommendedPower** | "권장 전투력: 117,660" |
| **StageName** | 스테이지 이름 ("깜빡이는 터널!") |
| **EnemyPreview** | 적 캐릭터 미리보기 (보스 등) |
| **PartyPreview** | 현재 파티 캐릭터 미리보기 |

#### 5. StarProgressBar (별 진행도)
| 요소 | 설명 |
|------|------|
| **CurrentStars** | 현재 획득 별 수 (★ 14/30) |
| **RewardMilestones** | 보상 구간 (10→25, 20→50, 30→100) |
| - MilestoneIcon | 각 구간 아이콘 (잎사귀) |
| - MilestoneReward | 보상 수량 |

#### 6. FooterArea (하단)
| 요소 | 설명 |
|------|------|
| **DifficultyTabs** | 난이도 탭 |
| - NormalTab | "순한맛" (Normal) |
| - HardTab | "매운맛" (Hard) |
| - HellTab | "핵불맛" (Hell) |
| **WorldMapButton** | "세계지도" - 월드맵 화면으로 이동 |

---

### Prefab 계층 구조

```
StageSelectScreen (RectTransform: Stretch)
├─ Background
│   └─ MapBackground (Image, 챕터별 배경)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   └─ ScreenHeader [Prefab]
│   │       ├─ BackButton
│   │       ├─ TitleText
│   │       └─ CurrencyHUD
│   │
│   ├─ Content (Stretch, Top=80, Bottom=120)
│   │   ├─ RightTopArea (Anchor: TopRight, 300x60)
│   │   │   └─ StageProgressWidget
│   │   │       ├─ ProgressLabel
│   │   │       └─ NavigateButton
│   │   │
│   │   ├─ StageMapArea (Stretch)
│   │   │   ├─ StageNodeContainer
│   │   │   │   └─ StageNode [Prefab] x N
│   │   │   │       ├─ NodeBackground
│   │   │   │       ├─ StageNumberText
│   │   │   │       ├─ StarGroup (★★★)
│   │   │   │       ├─ CharacterPreview (선택 시)
│   │   │   │       └─ LockIcon (잠금 시)
│   │   │   │
│   │   │   ├─ StageInfoBubble (Dynamic Position)
│   │   │   │   ├─ BubbleBackground
│   │   │   │   ├─ RecommendedPowerText
│   │   │   │   ├─ StageNameText
│   │   │   │   ├─ EnemyPreviewContainer
│   │   │   │   └─ PartyPreviewContainer
│   │   │   │
│   │   │   └─ ChapterNavigation
│   │   │       ├─ PrevChapterButton (Anchor: Left)
│   │   │       └─ NextChapterButton (Anchor: Right)
│   │   │
│   │   └─ StarProgressBar (Anchor: BottomLeft, 400x80)
│   │       ├─ StarIcon
│   │       ├─ ProgressText (14/30)
│   │       ├─ ProgressSlider
│   │       └─ MilestoneContainer
│   │           └─ MilestoneItem x 3
│   │
│   └─ Footer (Bottom, 120px)
│       ├─ DifficultyTabGroup (HorizontalLayoutGroup)
│       │   ├─ NormalTab
│       │   ├─ HardTab
│       │   └─ HellTab
│       └─ WorldMapButton (Anchor: Right)
│
└─ OverlayLayer
```

---

### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| RightTop | StageProgressWidget | `_stageProgressWidget` |
| Map | StageMapArea | `_stageMapArea` |
| Map | StageNodeContainer | `_stageNodeContainer` |
| Map | StageInfoBubble | `_stageInfoBubble` |
| Map | PrevChapterButton | `_prevChapterButton` |
| Map | NextChapterButton | `_nextChapterButton` |
| Progress | StarProgressBar | `_starProgressBar` |
| Footer | DifficultyTabGroup | `_difficultyTabs` |
| Footer | WorldMapButton | `_worldMapButton` |
| ContentModule | ContentModuleContainer | `_contentModuleContainer` |

---

### 네비게이션 흐름

```
StageSelectScreen
├─ StageNode 클릭 → StageInfoBubble 표시
├─ StageInfoBubble 클릭 → PartySelectScreen (해당 스테이지)
├─ PrevChapter → 이전 챕터 스테이지 로드
├─ NextChapter → 다음 챕터 스테이지 로드
├─ DifficultyTab → 해당 난이도 스테이지 로드
├─ WorldMapButton → WorldMapScreen (TBD)
├─ StageProgressWidget → 최고 진행 스테이지로 이동
└─ BackButton → 이전 화면 (InGameContentDashboard / Lobby)
```

---

## PartySelectScreen UI 레이아웃 구조

### 전체 구조

```
PartySelectScreen (FullScreen)
├─ ScreenHeader ─────────────────────────────────────────────────────────
│   ├─ [Left] BackButton + StageInfo ("10-7. 깜빡이는 터널!")
│   └─ [Right] CurrencyHUD (스태미나, 골드, 프리미엄) + HomeButton
│
├─ LeftArea (전투 미리보기) ──────────────────────────────────────────────
│   ├─ ElementIndicator (속성 아이콘 - 불/물/풀/빛/어둠)
│   │
│   ├─ BattlePreviewArea
│   │   ├─ PartyFormation (좌측 - 아군)
│   │   │   ├─ FrontLine (캐릭터 x3)
│   │   │   └─ BackLine (캐릭터 x3)
│   │   │
│   │   └─ EnemyFormation (우측 - 적)
│   │       └─ EnemySpots (빨간 원형 영역)
│   │
│   ├─ QuickActionBar
│   │   ├─ AutoFormButton ("일괄 해제")
│   │   └─ StageInfoButton ("스테이지 정보")
│   │
│   └─ FormationSettingButton ("덱 설정")
│
├─ StageInfoPanel (상단 중앙) ───────────────────────────────────────────
│   ├─ EntryInfo (입장 조건)
│   │   ├─ EntryCost (30 스태미나)
│   │   └─ RecommendedPower ("권장 전투력: 123,188")
│   │
│   ├─ BorrowInfo ("사도 대여: 0/1")
│   │
│   └─ FormationStatus
│       ├─ PartyCount ("편성된 사도: 6/6, 집략군 1")
│       └─ CardCount ("편성된 카드: 24/24, 집략군 카드 1")
│
├─ RightPanel (캐릭터 선택) ─────────────────────────────────────────────
│   ├─ TabBar
│   │   ├─ RentalTab ("대여")
│   │   ├─ FilterTab ("필터 OFF")
│   │   └─ SortTab ("전투력") + SortButton
│   │
│   ├─ CharacterGrid (3열 스크롤)
│   │   └─ CharacterSlot x N
│   │       ├─ Portrait
│   │       ├─ Level ("Lv.52")
│   │       ├─ Stars (★★★★★)
│   │       ├─ CombatPower ("25,555")
│   │       └─ EquippedBadge (편성 시)
│   │
│   └─ ActionBar (하단)
│       ├─ QuickBattleButton ("빠른전투불가")
│       ├─ StartButton ("출발") + CostIcon (10)
│       └─ AutoButton ("자동OFF")
│
└─ FooterBar ────────────────────────────────────────────────────────────
    ├─ EntryCostDisplay (60 스태미나)
    └─ RecommendedPowerDisplay (117,660)
```

### 영역별 상세

#### 1. ScreenHeader (상단 헤더)
| 요소 | 설명 |
|------|------|
| **BackButton** | 이전 화면(StageSelectScreen)으로 |
| **StageInfo** | "10-7. 깜빡이는 터널!" (스테이지 번호 + 이름) |
| **CurrencyHUD** | 스태미나(102/102), 골드(549,061), 프리미엄(1,809) |
| **HomeButton** | 로비로 바로 이동 |

#### 2. LeftArea (전투 미리보기)
| 요소 | 설명 |
|------|------|
| **ElementIndicator** | 속성 아이콘 (불/물/풀/빛/어둠) - 유리 속성 표시 |
| **BattlePreviewArea** | 전투 시뮬레이션 미리보기 |
| - PartyFormation | 아군 배치 (앞줄 3, 뒷줄 3) |
| - EnemyFormation | 적 위치 표시 (빨간 원형 영역) |
| **AutoFormButton** | "일괄 해제" - 편성 전체 해제 |
| **StageInfoButton** | "스테이지 정보" → StageInfoPopup |
| **FormationSettingButton** | "덱 설정" - 프리셋 관리 |

#### 3. StageInfoPanel (스테이지 정보)
| 요소 | 설명 |
|------|------|
| **EntryCost** | 입장 비용 (30 스태미나) |
| **RecommendedPower** | 권장 전투력 (123,188) |
| **BorrowInfo** | 사도 대여 정보 ("사도 대여: 0/1") |
| **PartyCount** | 편성된 사도 수 ("편성된 사도: 6/6, 집략군 1") |
| **CardCount** | 편성된 카드 수 ("편성된 카드: 24/24") |

#### 4. RightPanel (캐릭터 선택 영역)
| 요소 | 설명 |
|------|------|
| **TabBar** | 필터/정렬 탭 |
| - RentalTab | "대여" - 친구 캐릭터 대여 |
| - FilterTab | "필터 OFF" - 필터링 옵션 |
| - SortTab | "전투력" - 정렬 기준 + 순서 토글 |
| **CharacterGrid** | 캐릭터 목록 (3열 그리드, 세로 스크롤) |

#### 5. CharacterSlot (캐릭터 슬롯)
| 요소 | 설명 |
|------|------|
| **Portrait** | 캐릭터 초상화 |
| **ElementIcon** | 속성 아이콘 (좌상단) |
| **Level** | 레벨 표시 ("Lv.52") |
| **Stars** | 성급 (★★★★★) |
| **CombatPower** | 전투력 ("25,555") |
| **EquippedBadge** | 편성 여부 표시 (선택 시 녹색 테두리) |
| **SearchIcon** | 상세 정보 버튼 (돋보기) |

#### 6. ActionBar (하단 액션 영역)
| 요소 | 설명 |
|------|------|
| **QuickBattleButton** | "빠른전투불가" (소탕 불가 표시) |
| **StartButton** | "출발" + 비용 표시 (10) → 전투 시작 |
| **AutoButton** | "자동OFF" - 자동 전투 토글 |

#### 7. FooterBar (하단 정보)
| 요소 | 설명 |
|------|------|
| **EntryCostDisplay** | 60 스태미나 (사각 코인 아이콘) |
| **RecommendedPowerDisplay** | 117,660 (권장 전투력) |

---

### Prefab 계층 구조

```
PartySelectScreen (RectTransform: Stretch)
├─ Background
│   └─ BattlePreviewBackground
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   └─ ScreenHeader [Prefab]
│   │       ├─ BackButton
│   │       ├─ StageInfoText
│   │       ├─ CurrencyHUD
│   │       └─ HomeButton
│   │
│   ├─ Content (Stretch, Top=80)
│   │   ├─ LeftArea (Anchor: Left, Width=60%)
│   │   │   ├─ ElementIndicator (Anchor: TopLeft)
│   │   │   │   └─ ElementIcon x 5
│   │   │   │
│   │   │   ├─ BattlePreviewArea (Center)
│   │   │   │   ├─ PartyFormation
│   │   │   │   │   ├─ FrontLineSlot x 3
│   │   │   │   │   └─ BackLineSlot x 3
│   │   │   │   │
│   │   │   │   └─ EnemyFormation
│   │   │   │       └─ EnemySpot x N
│   │   │   │
│   │   │   ├─ QuickActionBar (Anchor: BottomLeft)
│   │   │   │   ├─ AutoFormButton
│   │   │   │   └─ StageInfoButton
│   │   │   │
│   │   │   └─ FormationSettingButton (Anchor: BottomLeft)
│   │   │
│   │   ├─ StageInfoPanel (Anchor: TopCenter)
│   │   │   ├─ EntryInfoGroup
│   │   │   │   ├─ EntryCostText
│   │   │   │   └─ RecommendedPowerText
│   │   │   ├─ BorrowInfoText
│   │   │   └─ FormationStatusGroup
│   │   │       ├─ PartyCountText
│   │   │       └─ CardCountText
│   │   │
│   │   └─ RightPanel (Anchor: Right, Width=40%)
│   │       ├─ TabBar (Top, 50px)
│   │       │   ├─ RentalTab
│   │       │   ├─ FilterTab
│   │       │   └─ SortTab
│   │       │
│   │       ├─ CharacterGrid (Stretch, GridLayoutGroup 3열)
│   │       │   └─ CharacterSlot [Prefab] x N
│   │       │       ├─ Portrait
│   │       │       ├─ ElementIcon
│   │       │       ├─ LevelText
│   │       │       ├─ StarGroup
│   │       │       ├─ CombatPowerText
│   │       │       ├─ EquippedIndicator
│   │       │       └─ SearchButton
│   │       │
│   │       └─ ActionBar (Bottom, 80px)
│   │           ├─ QuickBattleButton
│   │           ├─ StartButton
│   │           └─ AutoToggle
│   │
│   └─ Footer (Bottom, 60px)
│       ├─ EntryCostDisplay
│       └─ RecommendedPowerDisplay
│
└─ OverlayLayer
```

---

### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| Header | StageInfoText | `_stageInfoText` |
| Left | ElementIndicator | `_elementIndicator` |
| Left | BattlePreviewArea | `_battlePreviewArea` |
| Left | PartyFormation | `_partyFormation` |
| Left | EnemyFormation | `_enemyFormation` |
| Left | AutoFormButton | `_autoFormButton` |
| Left | StageInfoButton | `_stageInfoButton` |
| Left | FormationSettingButton | `_formationSettingButton` |
| Center | StageInfoPanel | `_stageInfoPanel` |
| Center | EntryCostText | `_entryCostText` |
| Center | RecommendedPowerText | `_recommendedPowerText` |
| Center | PartyCountText | `_partyCountText` |
| Right | CharacterGrid | `_characterGrid` |
| Right | RentalTab | `_rentalTab` |
| Right | FilterTab | `_filterTab` |
| Right | SortTab | `_sortTab` |
| Bottom | QuickBattleButton | `_quickBattleButton` |
| Bottom | StartButton | `_startButton` |
| Bottom | AutoToggle | `_autoToggle` |

---

### 네비게이션 흐름

```
PartySelectScreen
├─ CharacterSlot 클릭 → 파티에 추가/제거
├─ CharacterSlot 길게 누르기 → CharacterDetailPopup
├─ RentalTab → 친구 캐릭터 목록 표시
├─ FilterTab → FilterPopup
├─ SortTab → 정렬 변경
├─ AutoFormButton → 파티 일괄 해제
├─ StageInfoButton → StageInfoPopup
├─ FormationSettingButton → PresetManagePopup
├─ StartButton → BattleScene (전투 시작)
├─ QuickBattleButton → 즉시 전투 결과 (조건 충족 시)
└─ BackButton → StageSelectScreen
```

---

## InGameContentDashboard UI 레이아웃 구조

### 전체 구조

```
InGameContentDashboard (FullScreen) - "모험"
├─ ScreenHeader ─────────────────────────────────────────────────────────
│   ├─ [Left] BackButton + TitleText ("모험")
│   └─ [Right] CurrencyHUD (스태미나, 골드, 프리미엄) + HomeButton
│
├─ RightTopArea ────────────────────────────────────────────────────────
│   └─ StageProgressWidget ("11-10 최후의 방어선! 알프트반선!")
│
├─ ContentArea (방 인테리어 배경) ───────────────────────────────────────
│   │
│   ├─ LeftSide
│   │   ├─ ShortTermClassButton ("단기 속성반")
│   │   │   └─ SeasonInfo ("02/19/11:00 시즌 시작")
│   │   │
│   │   └─ DimensionClashButton ("차원 대충돌")
│   │       └─ DungeonLabel ("딜: 리버리")
│   │
│   ├─ CenterArea
│   │   ├─ NurulingBustersButton ("누루링 버스터즈")
│   │   │   └─ CharacterSprite (캐릭터 장식)
│   │   │
│   │   ├─ PVPButton ("PVP")
│   │   │   └─ TrophyIcon
│   │   │
│   │   └─ MainStoryProgress (중앙 하단)
│   │       ├─ ProgressLabel ("제 1 엘리베이터 B7 도전중")
│   │       ├─ StageNameLabel ("세계수 급착기지")
│   │       └─ TimeRemaining ("06일 17시간 07분")
│   │
│   ├─ RightSide
│   │   ├─ DungeonButton ("던전")
│   │   │   └─ DungeonIcon
│   │   │
│   │   ├─ InvasionButton ("침략")
│   │   │   └─ CharacterSprite
│   │   │
│   │   └─ DeckFormationButton ("덱 편성")
│   │       └─ FormationIcon
│   │
│   └─ Decorations (배경 장식)
│       ├─ FurnitureItems (가구, 선반, 액자 등)
│       └─ CharacterMascots (마스코트 캐릭터들)
│
└─ (Footer 없음 - 전체 화면 활용)
```

### 영역별 상세

#### 1. ScreenHeader (상단 헤더)
| 요소 | 설명 |
|------|------|
| **BackButton** | 이전 화면(Lobby)으로 |
| **TitleText** | "모험" |
| **CurrencyHUD** | 스태미나(102/102), 티켓(180/180), 골드(549,061), 프리미엄(1,809) |
| **HomeButton** | 로비로 바로 이동 |

#### 2. RightTopArea (우상단)
| 요소 | 설명 |
|------|------|
| **StageProgressWidget** | 현재 스토리 진행 상황 ("11-10 최후의 방어선! 알프트반선!") |
| - ProgressLabel | 스테이지 번호 + 이름 |
| - NavigateButton | 해당 스테이지로 바로 이동 (>>) |

#### 3. ContentArea - LeftSide (좌측 컨텐츠)
| 요소 | 설명 |
|------|------|
| **ShortTermClassButton** | "단기 속성반" - 속성별 단기 이벤트 |
| - SeasonInfo | 시즌 정보 ("02/19/11:00 시즌 시작") |
| **DimensionClashButton** | "차원 대충돌" - 차원 레이드 컨텐츠 |
| - DungeonLabel | 현재 던전 정보 ("딜: 리버리") |

#### 4. ContentArea - CenterArea (중앙 컨텐츠)
| 요소 | 설명 |
|------|------|
| **NurulingBustersButton** | "누루링 버스터즈" - 미니게임/보스전 |
| **PVPButton** | "PVP" - 실시간 대전 |
| - TrophyIcon | 트로피 아이콘 |
| **MainStoryProgress** | 메인 스토리 진행 현황 |
| - ProgressLabel | "제 1 엘리베이터 B7 도전중" |
| - StageNameLabel | "세계수 급착기지" |
| - TimeRemaining | 남은 시간 ("06일 17시간 07분") |

#### 5. ContentArea - RightSide (우측 컨텐츠)
| 요소 | 설명 |
|------|------|
| **DungeonButton** | "던전" - 일일/주간 던전 (골드, 경험치 등) |
| **InvasionButton** | "침략" - 침략 컨텐츠 |
| **DeckFormationButton** | "덱 편성" - 파티 프리셋 관리 |

#### 6. Decorations (배경 장식)
| 요소 | 설명 |
|------|------|
| **FurnitureItems** | 방 가구들 (선반, 칠판, 액자, 트로피 등) |
| **CharacterMascots** | 마스코트 캐릭터들 (장식용) |

---

### Prefab 계층 구조

```
InGameContentDashboard (RectTransform: Stretch)
├─ Background
│   └─ RoomBackground (Image, 방 인테리어)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   └─ ScreenHeader [Prefab]
│   │       ├─ BackButton
│   │       ├─ TitleText
│   │       ├─ CurrencyHUD
│   │       └─ HomeButton
│   │
│   ├─ Content (Stretch, Top=80)
│   │   ├─ RightTopArea (Anchor: TopRight, 300x60)
│   │   │   └─ StageProgressWidget
│   │   │       ├─ ProgressLabel
│   │   │       └─ NavigateButton
│   │   │
│   │   ├─ ContentButtons (Stretch, Free Position)
│   │   │   │
│   │   │   ├─ LeftSide
│   │   │   │   ├─ ShortTermClassButton (Anchor: Left)
│   │   │   │   │   ├─ ButtonBackground
│   │   │   │   │   ├─ ContentLabel
│   │   │   │   │   ├─ SeasonInfoText
│   │   │   │   │   └─ CharacterSprite
│   │   │   │   │
│   │   │   │   └─ DimensionClashButton (Anchor: BottomLeft)
│   │   │   │       ├─ ButtonBackground
│   │   │   │       ├─ ContentLabel
│   │   │   │       ├─ DungeonInfoText
│   │   │   │       └─ CharacterSprite
│   │   │   │
│   │   │   ├─ CenterArea
│   │   │   │   ├─ NurulingBustersButton (Anchor: TopCenter)
│   │   │   │   │   ├─ ButtonBackground
│   │   │   │   │   ├─ ContentLabel
│   │   │   │   │   └─ CharacterSprite
│   │   │   │   │
│   │   │   │   ├─ PVPButton (Anchor: CenterLeft)
│   │   │   │   │   ├─ ButtonBackground
│   │   │   │   │   ├─ ContentLabel
│   │   │   │   │   └─ TrophyIcon
│   │   │   │   │
│   │   │   │   └─ MainStoryProgressPanel (Anchor: BottomCenter)
│   │   │   │       ├─ ProgressLabelText
│   │   │   │       ├─ StageNameText
│   │   │   │       ├─ TimeRemainingText
│   │   │   │       └─ EnterButton
│   │   │   │
│   │   │   └─ RightSide
│   │   │       ├─ DungeonButton (Anchor: TopRight)
│   │   │       │   ├─ ButtonBackground
│   │   │       │   ├─ ContentLabel
│   │   │       │   └─ DungeonIcon
│   │   │       │
│   │   │       ├─ InvasionButton (Anchor: Right)
│   │   │       │   ├─ ButtonBackground
│   │   │       │   ├─ ContentLabel
│   │   │       │   └─ CharacterSprite
│   │   │       │
│   │   │       └─ DeckFormationButton (Anchor: BottomRight)
│   │   │           ├─ ButtonBackground
│   │   │           ├─ ContentLabel
│   │   │           └─ FormationIcon
│   │   │
│   │   └─ DecorationLayer (Behind Buttons)
│   │       └─ DecorationSprite x N
│   │
│   └─ (No Footer)
│
└─ OverlayLayer
```

---

### 컴포넌트 매핑

| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| RightTop | StageProgressWidget | `_stageProgressWidget` |
| Left | ShortTermClassButton | `_shortTermClassButton` |
| Left | DimensionClashButton | `_dimensionClashButton` |
| Center | NurulingBustersButton | `_nurulingBustersButton` |
| Center | PVPButton | `_pvpButton` |
| Center | MainStoryProgressPanel | `_mainStoryProgressPanel` |
| Right | DungeonButton | `_dungeonButton` |
| Right | InvasionButton | `_invasionButton` |
| Right | DeckFormationButton | `_deckFormationButton` |
| Background | RoomBackground | `_roomBackground` |

---

### 네비게이션 흐름

```
InGameContentDashboard ("모험")
├─ MainStoryProgressPanel → StageSelectScreen (메인스토리)
├─ DungeonButton → StageDashboard (던전 카테고리 선택)
├─ InvasionButton → StageSelectScreen (침략 스테이지)
├─ ShortTermClassButton → EventStageScreen (속성반 이벤트)
├─ DimensionClashButton → DimensionRaidScreen (차원 레이드)
├─ NurulingBustersButton → MinigameScreen (미니게임)
├─ PVPButton → PVPLobbyScreen (PVP)
├─ DeckFormationButton → DeckManageScreen (덱 편성)
├─ StageProgressWidget → StageSelectScreen (현재 진행 스테이지)
└─ BackButton → LobbyScreen
```

---

### 컨텐츠 버튼 상태

| 버튼 | 상태 | 표시 |
|------|------|------|
| **활성** | 진입 가능 | 일반 표시 |
| **비활성** | 레벨/조건 미충족 | 어둡게 + 잠금 아이콘 |
| **이벤트** | 기간 한정 | 시간 표시 + 하이라이트 |
| **신규** | 새 컨텐츠 | NEW 배지 |
| **진행중** | 미완료 컨텐츠 | 진행도 표시 |

---

## 핵심 개념

| 용어 | 정의 | 예시 |
|------|------|------|
| **Stage** | 인게임 전투 **한 판** | 1-1, 1-2, 보스전, 일일던전 1층 |
| **InGameContent** | 전투 컨텐츠 **대분류** | 메인스토리, 골드던전, 경험치던전, 보스레이드 |
| **StageCategory** | 컨텐츠 내 **세부 분류** | 불속성, 물속성, 1장, 2장 |

---

## 의존성

### 참조
- `Sc.Common` - UI 시스템, Navigation, Widget
- `Sc.Packet` - NetworkManager, Request/Response
- `Sc.Data` - 마스터/유저 데이터
- `Sc.Event` - 이벤트 발행
- `Sc.Contents.Character` - 캐릭터 정보, 파티 편성

### 참조됨
- `Sc.Contents.Lobby` - InGameContentDashboard 진입
- `Sc.Contents.Battle` - 전투 시스템 (BattleReadyEvent 수신)
- `Sc.Contents.Event` - 이벤트 스테이지 (EventStageContentModule 사용)

---

## 화면 계층 구조

```
Lobby
  │
  └─> InGameContentDashboard (컨텐츠 종류 선택)
        │
        │  ┌─────────────────────────────────────────────────────┐
        │  │ 컨텐츠에 따라 StageDashboard 유무 결정               │
        │  │ - 메인스토리: StageDashboard 스킵                   │
        │  │ - 골드/경험치던전: StageDashboard 필요 (카테고리 선택)│
        │  └─────────────────────────────────────────────────────┘
        │
        ├─[메인스토리]────────────────────> StageSelectScreen
        │                                   + MainStoryContentModule
        │
        ├─[골드던전]──> StageDashboard ──> StageSelectScreen
        │               (카테고리 선택)      + ElementDungeonContentModule
        │
        ├─[경험치던전]─> StageDashboard ─> StageSelectScreen
        │               (카테고리 선택)      + ExpDungeonContentModule
        │
        ├─[보스레이드]─> StageDashboard ─> StageSelectScreen
        │               (카테고리 선택)      + BossRaidContentModule
        │
        └─[무한의탑]────────────────────> StageSelectScreen
                                          + TowerContentModule
```

### 이벤트 스테이지 연동

```
LiveEventScreen
  │
  └─> EventDetailScreen
        │
        └─[스테이지 탭]─────────────────> StageSelectScreen
                                          + EventStageContentModule
```

---

## UI 아키텍처 (컴포지션 패턴)

### StageSelectScreen 구조

```
┌─────────────────────────────────────────────────────────────────┐
│                     StageSelectScreen                            │
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                    Header (공통)                             ││
│  │  [←] 스테이지 선택                       남은 입장: 3/5      ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │              Custom Content Area (확장 영역)                 ││
│  │     ← IStageContentModule이 UI를 생성하는 영역 →            ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                 StageListPanel (공통)                        ││
│  │  ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐                           ││
│  │  │ 1-1 │ │ 1-2 │ │ 1-3 │ │ 1-4 │  ...                      ││
│  │  │ ★★★ │ │ ★★☆ │ │ ☆☆☆ │ │ 🔒  │                           ││
│  │  └─────┘ └─────┘ └─────┘ └─────┘                           ││
│  └─────────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────────┐│
│  │                    Footer (공통)                             ││
│  │  총 보상: 💰1000  💎10              [소탕] [입장]            ││
│  └─────────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────────┘
```

### 컨텐츠별 모듈 (IStageContentModule)

| 모듈 | Custom Content Area 내용 |
|------|--------------------------|
| **MainStoryContentModule** | 챕터 탭 `[1장][2장][3장🔒]`, 스토리 진행도 |
| **ElementDungeonContentModule** | 속성 아이콘 🔥, 권장 속성 안내 💧 |
| **ExpDungeonContentModule** | 난이도 표시, 획득 경험치 미리보기 |
| **BossRaidContentModule** | 보스 HP 게이지, 내 기여도, 랭킹 버튼 |
| **TowerContentModule** | 현재 층, 최고 층, 보상 미리보기 |
| **EventStageContentModule** | 이벤트 이름, 남은 기간, 이벤트 재화 |

---

## 클래스 역할 정의

### 화면 (Screen)

| 클래스 | 역할 | 책임 | 비책임 |
|--------|------|------|--------|
| `InGameContentDashboard` | 컨텐츠 종류 선택 화면 | 컨텐츠 목록 표시, 진입 처리 | 스테이지 표시 |
| `StageDashboard` | 세부 분류 선택 화면 | 속성/난이도/보스 선택 | 스테이지 표시 |
| `StageSelectScreen` | 스테이지 선택 화면 | 공통 UI + 모듈 조합, 스테이지 목록 | 컨텐츠별 특수 로직 |
| `PartySelectScreen` | 파티 편성 화면 | 캐릭터 선택, 프리셋 관리, 전투 진입 | 전투 로직 |

### 패널/위젯 (Panel/Widget)

| 클래스 | 역할 | 책임 |
|--------|------|------|
| `StageListPanel` | 스테이지 목록 패널 | 스테이지 아이템 생성/관리, 스크롤 |
| `StageItemWidget` | 개별 스테이지 위젯 | 스테이지 정보 표시, 클릭 이벤트 |
| `ContentCategoryItem` | 컨텐츠 카테고리 아이템 | 컨텐츠 정보 표시 (Dashboard용) |

### 모듈 (Module)

| 인터페이스 | 역할 |
|------------|------|
| `IStageContentModule` | 컨텐츠별 확장 UI 인터페이스 |
| `BaseStageContentModule` | 모듈 공통 로직 (Template Method Pattern) |
| `StageContentModuleFactory` | 컨텐츠 타입별 모듈 생성 팩토리 |

```csharp
public interface IStageContentModule
{
    event Action<string> OnCategoryChanged;  // 카테고리 변경 이벤트
    void Initialize(Transform container, InGameContentType contentType);
    void SetCategoryId(string categoryId);   // 외부에서 카테고리 설정
    void Refresh(string selectedStageId);
    void OnStageSelected(StageData stageData);
    void Release();
}
```

### 팝업 (Popup)

| 클래스 | 역할 |
|--------|------|
| `StageInfoPopup` | 스테이지 상세 정보, Star 조건, 보상 표시 |

---

## 마스터 데이터

### InGameContentType

**위치**: `Assets/Scripts/Data/Enums/InGameContentType.cs`

```csharp
public enum InGameContentType
{
    MainStory,      // 메인 스토리
    HardMode,       // 하드 모드
    GoldDungeon,    // 골드 던전
    ExpDungeon,     // 경험치 던전
    SkillDungeon,   // 스킬 재화 던전
    BossRaid,       // 보스 레이드
    Tower,          // 무한의 탑
    Event,          // 이벤트 스테이지
}
```

### StageType (기존 확장)

**위치**: `Assets/Scripts/Data/Enums/StageType.cs`

```csharp
public enum StageType
{
    Normal,         // 일반 스테이지
    Boss,           // 보스 스테이지
    Challenge,      // 챌린지 스테이지
    Hidden,         // 히든 스테이지
}
```

### StarConditionType

**위치**: `Assets/Scripts/Data/Enums/StarConditionType.cs`

```csharp
public enum StarConditionType
{
    Clear,              // 클리어
    TurnLimit,          // N턴 이내 클리어
    NoCharacterDeath,   // 사망자 없이 클리어
    FullHP,             // 아군 전원 HP 100%
    ElementAdvantage,   // 유리 속성으로 클리어
}
```

### StageData

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageData.cs`

```csharp
[CreateAssetMenu(fileName = "StageData", menuName = "SC/Data/Stage")]
public class StageData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public InGameContentType ContentType;
    public string CategoryId;           // 속하는 카테고리 ID (속성/챕터 등)
    public StageType StageType;
    public int Chapter;
    public int StageNumber;
    public Difficulty Difficulty;

    [Header("입장 조건")]
    public CostType EntryCostType;      // 입장 재화 타입
    public int EntryCost;               // 입장 비용
    public LimitType LimitType;         // 입장 제한 타입
    public int LimitCount;              // 제한 횟수
    public DayOfWeek[] AvailableDays;   // 요일 제한 (일일 던전용)

    [Header("해금 조건")]
    public string UnlockConditionStageId;  // 선행 스테이지
    public int UnlockConditionLevel;       // 필요 레벨

    [Header("전투 정보")]
    public int RecommendedPower;        // 추천 전투력
    public string[] EnemyIds;           // 적 캐릭터 ID 목록

    [Header("보상 (레거시)")]
    public int RewardGold;
    public int RewardExp;

    [Header("보상 (신규)")]
    public List<RewardInfo> FirstClearRewards;
    public List<RewardInfo> RepeatClearRewards;

    [Header("별점 조건")]
    public StarCondition Star1Condition;
    public StarCondition Star2Condition;
    public StarCondition Star3Condition;

    [Header("표시")]
    public int DisplayOrder;
    public bool IsEnabled;
}
```

### StageDatabase

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageDatabase.cs`

```csharp
[CreateAssetMenu(fileName = "StageDatabase", menuName = "SC/Database/Stage")]
public class StageDatabase : ScriptableObject
{
    [SerializeField] private List<StageData> _stages;

    public StageData GetById(string id);
    public IEnumerable<StageData> GetByContentType(InGameContentType contentType);
    public IEnumerable<StageData> GetByContentTypeAndCategory(InGameContentType contentType, string categoryId);
    public IEnumerable<StageData> GetByCategory(string categoryId);
    public IEnumerable<StageData> GetByEvent(string eventId);
}
```

### StageCategoryData

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageCategoryData.cs`

```csharp
[CreateAssetMenu(fileName = "StageCategoryData", menuName = "SC/Data/StageCategory")]
public class StageCategoryData : ScriptableObject
{
    [Header("기본 정보")]
    public string Id;
    public InGameContentType ContentType;
    public string NameKey;
    public string DescriptionKey;
    public Sprite IconSprite;

    [Header("컨텐츠별 특화 필드")]
    public Element Element;         // 속성 던전용
    public Difficulty Difficulty;   // 난이도 던전용
    public int ChapterNumber;       // 메인스토리 챕터용

    [Header("표시")]
    public int DisplayOrder;
    public bool IsEnabled;
}
```

### StageCategoryDatabase

**위치**: `Assets/Scripts/Data/ScriptableObjects/StageCategoryDatabase.cs`

```csharp
[CreateAssetMenu(fileName = "StageCategoryDatabase", menuName = "SC/Database/StageCategory")]
public class StageCategoryDatabase : ScriptableObject
{
    [SerializeField] private List<StageCategoryData> _categories;

    public StageCategoryData GetById(string id);
    public IEnumerable<StageCategoryData> GetByContentType(InGameContentType contentType);
    public List<StageCategoryData> GetSortedByContentType(InGameContentType contentType);
    public StageCategoryData GetByElement(InGameContentType contentType, Element element);
    public StageCategoryData GetByChapter(int chapterNumber);
}
```

---

## 유저 데이터

### StageClearInfo 확장

**위치**: `Assets/Scripts/Data/Structs/UserData/StageProgress.cs`

```csharp
[Serializable]
public struct StageClearInfo
{
    public string StageId;
    public bool IsCleared;
    public int Stars;               // 0~3
    public bool[] StarAchieved;     // [star1, star2, star3] 개별 달성 여부
    public int BestTurnCount;
    public int ClearCount;
    public long FirstClearedAt;
    public long LastClearedAt;
}
```

### StageEntryRecord (NEW)

**위치**: `Assets/Scripts/Data/Structs/UserData/StageEntryRecord.cs`

```csharp
[Serializable]
public struct StageEntryRecord
{
    public string StageId;
    public int EntryCount;          // 입장 횟수
    public long LastEntryTime;
    public long ResetTime;          // 다음 리셋 시각

    public bool NeedsReset(long currentTime) => currentTime >= ResetTime;
}
```

### PartyPreset

**위치**: `Assets/Scripts/Data/Structs/UserData/PartyPreset.cs`

```csharp
[Serializable]
public struct PartyPreset
{
    public string PresetId;
    public string PresetGroupId;        // "main_story", "gold_dungeon_fire" 등
    public string Name;                 // 유저 지정 이름
    public List<string> CharacterInstanceIds;  // 최대 4~5명
    public long LastModifiedTime;
}
```

### UserSaveData 확장

```csharp
// UserSaveData v5
public Dictionary<string, StageEntryRecord> StageEntryRecords;  // Key: StageId
public List<PartyPreset> PartyPresets;

// Helper 메서드
public StageEntryRecord? FindStageEntryRecord(string stageId);
public void UpdateStageEntryRecord(string stageId, StageEntryRecord record);
public List<PartyPreset> GetPresetsForGroup(string presetGroupId);
public void UpdatePartyPreset(PartyPreset preset);
```

---

## Request/Response

### EnterStageRequest

```csharp
[Serializable]
public struct EnterStageRequest : IRequest
{
    public long Timestamp { get; set; }
    public string StageId;
    public List<string> PartyCharacterIds;
}
```

### EnterStageResponse

```csharp
[Serializable]
public struct EnterStageResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }  // 입장료 차감

    public string BattleSessionId;            // 전투 세션 ID
    public StageEntryRecord EntryRecord;      // 갱신된 입장 기록
}
```

### ClearStageRequest

```csharp
[Serializable]
public struct ClearStageRequest : IRequest
{
    public long Timestamp { get; set; }
    public string BattleSessionId;
    public bool IsVictory;
    public int TurnCount;
    public bool NoCharacterDeath;
    public bool AllFullHP;
}
```

### ClearStageResponse

```csharp
[Serializable]
public struct ClearStageResponse : IGameActionResponse
{
    public bool IsSuccess { get; set; }
    public ErrorCode ErrorCode { get; set; }
    public long ServerTime { get; set; }
    public UserDataDelta Delta { get; set; }  // 보상 지급

    public StageClearInfo ClearInfo;
    public bool[] NewStarsAchieved;           // 새로 달성한 별
    public List<RewardInfo> TotalRewards;
}
```

---

## Events

### StageEvents.cs

```csharp
// 입장 성공
public readonly struct StageEnteredEvent
{
    public string StageId { get; init; }
    public string BattleSessionId { get; init; }
}

// 입장 실패
public readonly struct StageEntryFailedEvent
{
    public string StageId { get; init; }
    public ErrorCode ErrorCode { get; init; }
    public string ErrorMessage { get; init; }
}

// 클리어 성공
public readonly struct StageClearedEvent
{
    public string StageId { get; init; }
    public bool IsVictory { get; init; }
    public bool IsFirstClear { get; init; }
    public bool[] NewStarsAchieved { get; init; }
    public List<RewardInfo> Rewards { get; init; }
}

// 전투 준비 완료 (Battle 시스템으로 전달)
public readonly struct BattleReadyEvent
{
    public string BattleSessionId { get; init; }
    public StageData StageData { get; init; }
    public List<string> PartyCharacterIds { get; init; }
}
```

---

## LocalServer

### StageEntryValidator

```csharp
public class StageEntryValidator
{
    public bool CanEnter(StageData stage, StageEntryRecord? record, out int remainingCount);
    public StageEntryRecord UpdateEntryRecord(StageData stage, StageEntryRecord? existing);
    public long CalculateNextResetTime(LimitType limitType, long currentTime);
    public bool IsAvailableToday(StageData stage, DayOfWeek today);
}
```

### StageHandler

```csharp
public class StageHandler :
    IRequestHandler<EnterStageRequest, EnterStageResponse>,
    IRequestHandler<ClearStageRequest, ClearStageResponse>
{
    public EnterStageResponse Handle(EnterStageRequest request, ref UserSaveData userData);
    public ClearStageResponse Handle(ClearStageRequest request, ref UserSaveData userData);

    private bool[] EvaluateStarConditions(StageData stage, ClearStageRequest request);
}
```

---

## 에러 코드

| ErrorCode | 값 | 설명 |
|-----------|-----|------|
| `StageNotFound` | 5101 | 스테이지 없음 |
| `StageLocked` | 5102 | 스테이지 잠김 (해금 조건 미충족) |
| `StageInsufficientCost` | 5103 | 입장 재화 부족 |
| `StageEntryLimitExceeded` | 5104 | 입장 제한 초과 |
| `StageInvalidParty` | 5105 | 잘못된 파티 구성 |
| `StageNotAvailableToday` | 5106 | 오늘 이용 불가 (요일 제한) |
| `StageInvalidBattleSession` | 5107 | 잘못된 전투 세션 |

---

## 파일 구조

```
Assets/Scripts/Contents/OutGame/Stage/
├── Sc.Contents.Stage.asmdef
│
├── Screens/
│   ├── InGameContentDashboard.cs
│   ├── StageDashboard.cs        (StageCategoryDatabase 사용)
│   ├── StageSelectScreen.cs     (StageContentModuleFactory 사용)
│   └── PartySelectScreen.cs
│
├── Panels/
│   ├── StageListPanel.cs
│   └── StageItemWidget.cs
│
├── Modules/
│   ├── IStageContentModule.cs
│   ├── BaseStageContentModule.cs     (추상 베이스, Template Method)
│   ├── StageContentModuleFactory.cs  (팩토리, 모듈 생성)
│   ├── MainStoryContentModule.cs     (챕터 탭, 진행도)
│   ├── ElementDungeonContentModule.cs (속성 아이콘, 권장 속성)
│   ├── ExpDungeonContentModule.cs    (TODO)
│   ├── BossRaidContentModule.cs      (TODO)
│   ├── TowerContentModule.cs         (TODO)
│   └── EventStageContentModule.cs    (TODO)
│
├── Popups/
│   ├── StageInfoPopup.cs
│   └── StageInfoState.cs
│
└── (States - Screen 내부 클래스)

Assets/Scripts/Data/ScriptableObjects/
├── StageData.cs                      (ContentType, CategoryId 확장)
├── StageDatabase.cs                  (GetByContentType, GetByCategory 확장)
├── StageCategoryData.cs              (카테고리 마스터 데이터)
└── StageCategoryDatabase.cs          (카테고리 데이터베이스)

Assets/Scripts/Editor/Tests/Stage/
├── StageContentModuleFactoryTests.cs
├── StageDatabaseTests.cs
└── StageCategoryDatabaseTests.cs
```

---

## 구현 체크리스트

```
Phase A: Data Foundation
- [x] InGameContentType.cs
- [x] StageType.cs
- [x] StarConditionType.cs
- [x] StarCondition.cs
- [x] StageData.cs (ContentType, CategoryId, StarConditions 확장)
- [x] StageDatabase.cs (GetByContentType, GetByCategory 등 확장)
- [x] StageCategoryData.cs
- [x] StageCategoryDatabase.cs
- [x] StageClearInfo 확장 (StarAchieved[])
- [x] StageEntryRecord.cs
- [x] PartyPreset.cs
- [x] UserSaveData v6 마이그레이션 (PartyPresets 추가)
- [x] Stage.json 샘플 데이터 (v2.0 - 신규 필드 포함)

Phase B: Request/Response
- [x] EnterStageRequest.cs
- [x] EnterStageResponse.cs
- [x] ClearStageRequest.cs
- [x] ClearStageResponse.cs

Phase C: Events
- [x] StageEvents.cs

Phase D: LocalServer
- [x] StageEntryValidator.cs
- [x] StageHandler.cs
- [x] LocalGameServer.cs 연동

Phase E: UI Screens
- [x] InGameContentDashboard.cs
- [x] StageDashboard.cs (StageCategoryDatabase 연동)
- [x] StageSelectScreen.cs (StageContentModuleFactory 연동)
- [x] PartySelectScreen.cs (플레이스홀더)

Phase F: UI Panels/Widgets
- [x] StageListPanel.cs
- [x] StageItemWidget.cs
- [x] ContentCategoryItem.cs

Phase G: Content Modules
- [x] IStageContentModule.cs (OnCategoryChanged, SetCategoryId 추가)
- [x] BaseStageContentModule.cs (Template Method Pattern)
- [x] StageContentModuleFactory.cs (Factory Pattern)
- [x] MainStoryContentModule.cs (챕터 탭, 진행도)
- [x] ElementDungeonContentModule.cs (속성 아이콘, 권장 속성)
- [x] ExpDungeonContentModule.cs (난이도 표시, 경험치 미리보기)
- [x] BossRaidContentModule.cs (보스 HP, 기여도, 랭킹)
- [x] TowerContentModule.cs (현재/최고 층, 보상 미리보기)
- [x] EventStageContentModule.cs (이벤트 정보, 남은 기간, 이벤트 재화)

Phase H: Popups/States
- [x] StageInfoPopup.cs
- [x] StageInfoState.cs
- [x] StageSelectState.cs (CategoryId 포함)
- [x] StageDashboardState.cs (InitialCategoryId 포함)
- [x] PartySelectState.cs

Phase I: Integration
- [x] LobbyScreen에 [던전] 버튼 추가
- [x] EventDetailScreen Stage 탭 연동 (EventStageTab → StageSelectScreen 네비게이션)
- [x] DataManager StageCategoryDatabase 추가

Phase J: Testing
- [x] StageEntryValidatorTests.cs
- [x] StageHandlerTests.cs
- [x] StageContentModuleFactoryTests.cs
- [x] StageDatabaseTests.cs
- [x] StageCategoryDatabaseTests.cs
```

---

## 관련 문서

- [Data.md](Data.md) - 데이터 구조 개요
- [Packet.md](Packet.md) - 네트워크 패턴
- [Character.md](Character.md) - 캐릭터 시스템
- [LiveEvent.md](LiveEvent.md) - 이벤트 스테이지 연동
- [Common/Reward.md](Common/Reward.md) - 보상 시스템
