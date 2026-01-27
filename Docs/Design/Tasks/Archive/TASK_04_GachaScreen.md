# TASK 04: GachaScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | GachaScreen |
| Reference | `Docs/Design/Reference/Gacha.jpg` |
| 스펙 문서 | `Docs/Specs/Gacha.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/GachaScreenPrefabBuilder.cs` |
| 난이도 | **중** |

---

## 사전 확인

```
1. Docs/Design/Reference/Gacha.jpg (이미지 분석)
2. Docs/Specs/Gacha.md → "UI 레이아웃 구조" 섹션
3. Assets/Scripts/Contents/Gacha/GachaScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

```
GachaScreen (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep)
│
├─ SafeArea
│   ├─ Header (Top, 80px)
│   │   ├─ BackButton
│   │   ├─ TitleText ("사도 모집")
│   │   └─ CurrencyHUD (유료, 골드, 무료, 프리미엄, 신앙심)
│   │
│   ├─ Content (Stretch, Top=80, Bottom=80)
│   │   ├─ LeftArea (Anchor: Left, Width=350)
│   │   │   ├─ MenuButtonGroup (VerticalLayoutGroup)
│   │   │   │   ├─ GachaButton ("사도 모집")
│   │   │   │   ├─ SpecialButton ("특별 모집")
│   │   │   │   └─ CardButton ("카드 뽑기")
│   │   │   │
│   │   │   └─ CharacterDisplay (Anchor: Stretch)
│   │   │       └─ CharacterImage (픽업 캐릭터)
│   │   │
│   │   └─ RightArea (Anchor: Stretch, Left=350)
│   │       ├─ BannerCarousel (Top, Height=120)
│   │       │   ├─ BannerContainer (HorizontalScroll)
│   │       │   │   └─ BannerSlot x 4
│   │       │   └─ Indicators
│   │       │
│   │       ├─ BannerInfoArea (Center)
│   │       │   ├─ BannerTitle (픽업 캐릭터명)
│   │       │   ├─ BannerPeriod (기간)
│   │       │   └─ BannerDescription
│   │       │
│   │       ├─ PityInfoArea
│   │       │   ├─ PityLabel ("신앙심 110")
│   │       │   └─ ExchangeButton ("교환")
│   │       │
│   │       └─ PullButtonGroup (HorizontalLayoutGroup)
│   │           ├─ FreePullButton ("1일 1회 모집", 무료 30)
│   │           ├─ SinglePullButton ("1회 모집", 프리미엄 1)
│   │           └─ MultiPullButton ("10회 모집", 프리미엄 10)
│   │
│   └─ Footer (Bottom, 80px)
│       └─ InfoButtonGroup (HorizontalLayoutGroup)
│           ├─ CharacterInfoButton ("사도 정보")
│           ├─ RateInfoButton ("확률 정보")
│           └─ HistoryButton ("기록")
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (80px)
| 요소 | 구현 |
|------|------|
| BackButton | Button (< 아이콘) |
| TitleText | TMP_Text ("사도 모집") |
| CurrencyHUD | 5개 재화 표시 (유료, 골드, 무료, 프리미엄, 신앙심) |

### 2. LeftArea (350px 폭)

#### MenuButtonGroup
| 버튼 | 상태 |
|------|------|
| GachaButton | 활성 (선택됨) |
| SpecialButton | 비활성 |
| CardButton | 비활성 |

#### CharacterDisplay
| 요소 | 구현 |
|------|------|
| CharacterImage | Image (픽업 캐릭터 일러스트) |

### 3. RightArea - BannerCarousel

| 요소 | 구현 |
|------|------|
| BannerContainer | ScrollRect (Horizontal) |
| BannerSlot | Image + Button (4개 슬롯) |
| Indicators | HorizontalLayoutGroup (점 4개) |

### 4. RightArea - BannerInfoArea

| 요소 | 구현 |
|------|------|
| BannerTitle | TMP_Text (픽업 캐릭터명) |
| BannerPeriod | TMP_Text (기간) |
| BannerDescription | TMP_Text (픽업 정보) |

### 5. RightArea - PityInfoArea

| 요소 | 구현 |
|------|------|
| PityLabel | TMP_Text ("신앙심 110") |
| ExchangeButton | Button ("교환") |

### 6. RightArea - PullButtonGroup

| 버튼 | 구현 |
|------|------|
| FreePullButton | Button + TMP_Text ("1일 1회 모집", 무료 30) |
| SinglePullButton | Button + TMP_Text ("1회 모집", 프리미엄 1) |
| MultiPullButton | Button + TMP_Text ("10회 모집", 프리미엄 10) + "★2 확정" 배지 |

### 7. Footer (InfoButtonGroup)

| 버튼 | 구현 |
|------|------|
| CharacterInfoButton | Button ("사도 정보") |
| RateInfoButton | Button ("확률 정보") |
| HistoryButton | Button ("기록") |

---

## SerializeField 연결

```csharp
// GachaScreen.cs 예상 필드
[SerializeField] private Button _backButton;
[SerializeField] private TMP_Text _titleText;

// 좌측 메뉴
[SerializeField] private Button _gachaButton;
[SerializeField] private Button _specialButton;
[SerializeField] private Button _cardButton;
[SerializeField] private Image _characterImage;

// 배너 캐러셀
[SerializeField] private ScrollRect _bannerCarousel;
[SerializeField] private Transform _bannerContainer;
[SerializeField] private Transform _indicatorContainer;

// 배너 정보
[SerializeField] private TMP_Text _bannerTitle;
[SerializeField] private TMP_Text _bannerPeriod;
[SerializeField] private TMP_Text _bannerDescription;

// 천장 정보
[SerializeField] private TMP_Text _pityLabel;
[SerializeField] private Button _exchangeButton;

// 뽑기 버튼
[SerializeField] private Button _freePullButton;
[SerializeField] private Button _singlePullButton;
[SerializeField] private Button _multiPullButton;

// 하단 정보 버튼
[SerializeField] private Button _characterInfoButton;
[SerializeField] private Button _rateInfoButton;
[SerializeField] private Button _historyButton;
```

---

## 참조 예시

- `Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs`
- `Assets/Scripts/Editor/AI/EditorUIHelpers.cs`

---

## 체크리스트

- [ ] `GachaScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의
- [ ] `Build()` 메서드 구현
- [ ] Header 영역
- [ ] LeftArea - MenuButtonGroup (3개 버튼)
- [ ] LeftArea - CharacterDisplay
- [ ] RightArea - BannerCarousel
- [ ] RightArea - BannerInfoArea
- [ ] RightArea - PityInfoArea
- [ ] RightArea - PullButtonGroup (3개 버튼)
- [ ] Footer - InfoButtonGroup (3개 버튼)
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: GachaScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/GachaScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header
- [x] LeftArea (메뉴 버튼 + 캐릭터 표시)
- [x] RightArea - BannerCarousel
- [x] RightArea - BannerInfoArea
- [x] RightArea - PityInfoArea
- [x] RightArea - PullButtonGroup
- [x] Footer (정보 버튼)
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
