# TASK 06: StageSelectScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | StageSelectScreen |
| Reference | `Docs/Design/Reference/StageSelectScreen.jpg` |
| 스펙 문서 | `Docs/Specs/Stage.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/StageSelectScreenPrefabBuilder.cs` |
| 난이도 | **상** (아이소메트릭 맵 + 복합 UI) |

---

## 사전 확인

```
1. Docs/Design/Reference/StageSelectScreen.jpg (이미지 분석)
2. Docs/Specs/Stage.md → "StageSelectScreen UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Stage/StageSelectScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
StageSelectScreen (RectTransform: Stretch)
├─ Background
│   └─ MapBackground (Image, 챕터별 배경)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   ├─ BackButton
│   │   ├─ TitleText ("스테이지 리스트")
│   │   └─ CurrencyHUD (스태미나, 골드, 프리미엄)
│   │
│   ├─ Content (Stretch, Top=80, Bottom=120)
│   │   ├─ RightTopArea (Anchor: TopRight, 300x60)
│   │   │   └─ StageProgressWidget
│   │   │       ├─ ProgressLabel
│   │   │       └─ NavigateButton
│   │   │
│   │   ├─ StageMapArea (Stretch)
│   │   │   ├─ StageNodeContainer
│   │   │   │   └─ StageNode [Template] x N
│   │   │   │
│   │   │   ├─ StageInfoBubble (Dynamic Position)
│   │   │   │   ├─ BubbleBackground
│   │   │   │   ├─ RecommendedPowerText
│   │   │   │   ├─ StageNameText
│   │   │   │   ├─ EnemyPreviewContainer
│   │   │   │   └─ PartyPreviewContainer
│   │   │   │
│   │   │   └─ ChapterNavigation
│   │   │       ├─ PrevChapterButton
│   │   │       └─ NextChapterButton
│   │   │
│   │   └─ StarProgressBar (Anchor: BottomLeft, 400x80)
│   │       ├─ StarIcon
│   │       ├─ ProgressText
│   │       ├─ ProgressSlider
│   │       └─ MilestoneContainer
│   │
│   └─ Footer (Bottom, 120px)
│       ├─ DifficultyTabs
│       │   ├─ NormalTab ("순한맛")
│       │   ├─ HardTab ("매운맛")
│       │   └─ HellTab ("핵불맛")
│       └─ WorldMapButton ("세계지도")
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (80px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| TitleText | TMP_Text ("스테이지 리스트") |
| CurrencyHUD | 스태미나(102/102), 골드, 프리미엄 |

### 2. RightTopArea - StageProgressWidget
| 요소 | 구현 |
|------|------|
| ProgressLabel | TMP_Text ("11-10 최후의 방어선!") |
| NavigateButton | Button (>> 아이콘) |

### 3. StageMapArea

#### StageNodeContainer
| 요소 | 구현 |
|------|------|
| StageNode | 템플릿 (아이소메트릭 배치) |

#### StageNode 구조
```
StageNode (120x120)
├─ NodeBackground (Image, 속성별 색상)
├─ StageNumberText (TMP_Text, "10-7")
├─ StarGroup (HorizontalLayoutGroup)
│   └─ Star x 3
├─ CharacterPreview (Image, 선택 시)
└─ LockIcon (Image, 잠금 시)
```

#### StageInfoBubble
| 요소 | 구현 |
|------|------|
| BubbleBackground | Image (말풍선) |
| RecommendedPowerText | TMP_Text ("권장 전투력: 117,660") |
| StageNameText | TMP_Text ("깜빡이는 터널!") |
| EnemyPreviewContainer | Transform (적 미리보기) |
| PartyPreviewContainer | Transform (파티 미리보기) |

#### ChapterNavigation
| 요소 | 구현 |
|------|------|
| PrevChapterButton | Button ("< 이전 월드") |
| NextChapterButton | Button ("> 다음 월드") |

### 4. StarProgressBar
| 요소 | 구현 |
|------|------|
| StarIcon | Image (별 아이콘) |
| ProgressText | TMP_Text ("14/30") |
| ProgressSlider | Slider |
| MilestoneContainer | HorizontalLayoutGroup |
| MilestoneItem | Image + TMP_Text (10→25, 20→50, 30→100) |

### 5. Footer (120px)

#### DifficultyTabs
| 탭 | 상태 |
|------|------|
| NormalTab | "순한맛" (활성) |
| HardTab | "매운맛" (비활성) |
| HellTab | "핵불맛" (잠금) |

| 요소 | 구현 |
|------|------|
| WorldMapButton | Button ("세계지도") |

---

## SerializeField 연결

```csharp
// StageSelectScreen.cs 예상 필드
// Header
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _titleText;

// RightTopArea
[SerializeField] private TMP_Text _stageProgressLabel;
[SerializeField] private Button _navigateButton;

// StageMapArea
[SerializeField] private Transform _stageNodeContainer;
[SerializeField] private GameObject _stageNodeTemplate;
[SerializeField] private GameObject _stageInfoBubble;
[SerializeField] private TMP_Text _recommendedPowerText;
[SerializeField] private TMP_Text _stageNameText;
[SerializeField] private Transform _enemyPreviewContainer;
[SerializeField] private Transform _partyPreviewContainer;

// ChapterNavigation
[SerializeField] private Button _prevChapterButton;
[SerializeField] private Button _nextChapterButton;

// StarProgressBar
[SerializeField] private Image _starIcon;
[SerializeField] private TMP_Text _starProgressText;
[SerializeField] private Slider _starProgressSlider;
[SerializeField] private Transform _milestoneContainer;

// Footer
[SerializeField] private Button _normalTab;
[SerializeField] private Button _hardTab;
[SerializeField] private Button _hellTab;
[SerializeField] private Button _worldMapButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `StageSelectScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] RightTopArea - StageProgressWidget
- [ ] StageMapArea - StageNodeContainer
- [ ] StageMapArea - StageNode 템플릿
- [ ] StageMapArea - StageInfoBubble
- [ ] StageMapArea - ChapterNavigation
- [ ] StarProgressBar
- [ ] Footer - DifficultyTabs
- [ ] Footer - WorldMapButton
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: StageSelectScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/StageSelectScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] RightTopArea (StageProgressWidget)
- [x] StageMapArea (StageNodeContainer, StageInfoBubble, ChapterNavigation)
- [x] StageNode 템플릿
- [x] StarProgressBar
- [x] Footer (DifficultyTabs, WorldMapButton)
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
