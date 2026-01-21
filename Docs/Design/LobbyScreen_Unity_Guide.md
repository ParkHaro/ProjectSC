# LobbyScreen Unity UGUI 구현 가이드

> **디자인 컨셉**: Luminous Dark Fantasy
> **참조 HTML**: `LobbyScreen_Design.html`

---

## 0. ScreenHeader 연동

**중요**: Header(재화, 프로필, 버튼)는 `ScreenHeader` 싱글톤 사용.
LobbyScreen은 ScreenHeader 영역을 침범하지 않도록 **Top Padding** 설정 필요.

```csharp
// LobbyScreen.OnBind()에서 호출
ScreenHeader.Instance?.Configure("lobby_default");
```

**ScreenHeader 높이**: 약 80px (SafeArea 포함 시 변동)
**LobbyScreen 컨텐츠**: Top offset = 80px 이상

---

## 1. 계층 구조

```
LobbyScreen (RectTransform: Stretch All, Top Padding: 80px)
├── Background
│   ├── GradientBG (Image)
│   └── ParticleEffect (ParticleSystem - optional)
│
├── MainContent (아래 ScreenHeader 영역 제외)
│   ├── CharacterDisplay (Left 55%)
│   │   ├── CharacterGlow (Image, radial gradient)
│   │   └── CharacterImage (Image)
│   │
│   └── TabContentArea (Right 45%)
│       ├── HomeTabContent
│       │   ├── QuickMenuSection
│       │   │   ├── StageButton
│       │   │   ├── ShopButton
│       │   │   └── EventButton
│       │   └── BannerArea
│       │
│       ├── CharacterTabContent
│       │   ├── CharacterGrid (GridLayoutGroup)
│       │   └── NavigateButton
│       │
│       ├── GachaTabContent
│       │   ├── GachaBanner
│       │   ├── GachaButtons
│       │   └── NavigateButton
│       │
│       └── SettingsTabContent
│           └── SettingsList (VerticalLayoutGroup)
│
└── BottomNav (Bottom: 80px)
    ├── HomeTab (TabButtonWidget)
    ├── CharacterTab (TabButtonWidget)
    ├── GachaTab (TabButtonWidget)
    └── SettingsTab (TabButtonWidget)

[ScreenHeader - 별도 Canvas에 존재]
├── Title
├── BackButton, ProfileButton, MenuButton, MailButton, NoticeButton
└── CurrencyHUD (Gold, Gem, Stamina)
```

---

## 2. 레이아웃 규칙

### RectTransform 설정

```
LobbyScreen Root:
- Anchor: Stretch All (0,0) - (1,1)
- Offset: Top = 80 (ScreenHeader 영역 확보)
- Offset: Bottom = 0 (BottomNav는 내부에 배치)

MainContent:
- Anchor: Stretch All
- Offset: Bottom = 80 (BottomNav 영역)

BottomNav:
- Anchor: Bottom Stretch
- Height: 80px
- Pivot: (0.5, 0)
```

---

## 3. 색상 팔레트 (Unity Color)

```csharp
// Background Colors
public static readonly Color BgDeep = new Color32(10, 10, 18, 255);        // #0a0a12
public static readonly Color BgSurface = new Color32(18, 18, 31, 255);     // #12121f
public static readonly Color BgCard = new Color32(25, 25, 45, 217);        // rgba(25,25,45,0.85)
public static readonly Color BgGlass = new Color32(255, 255, 255, 8);      // rgba(255,255,255,0.03)

// Accent Colors
public static readonly Color AccentPrimary = new Color32(0, 212, 255, 255);    // #00d4ff (Cyan)
public static readonly Color AccentSecondary = new Color32(255, 107, 157, 255); // #ff6b9d (Pink)
public static readonly Color AccentGold = new Color32(255, 215, 0, 255);        // #ffd700
public static readonly Color AccentPurple = new Color32(168, 85, 247, 255);     // #a855f7
public static readonly Color AccentGreen = new Color32(34, 197, 94, 255);       // #22c55e

// Text Colors
public static readonly Color TextPrimary = Color.white;
public static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);
public static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.4f);

// Border
public static readonly Color GlassBorder = new Color(1f, 1f, 1f, 0.1f);
```

---

## 4. 컴포넌트별 설정

### 4.1 ScreenHeader 연동

**ScreenHeader**는 별도 Canvas에서 관리됨. LobbyScreen은 `Configure()` 호출만 담당.

```csharp
// LobbyScreen.OnBind()
protected override void OnBind(LobbyState state)
{
    ScreenHeader.Instance?.Configure("lobby_default");
    // ...
}
```

**lobby_default 설정 (ScreenHeaderConfigData)**:
- ShowBackButton: false (로비는 최상위)
- ShowProfileButton: true
- ShowMenuButton: true
- ShowCurrency: true
- Title: "" (로비는 타이틀 없음)

### 4.2 Quick Menu Buttons

```
QuickButton:
- Size: Flexible (GridLayoutGroup cell)
- Aspect Ratio: 1:1
- Background: BgCard + Border(GlassBorder)
- Corner Radius: 16px
- Layout: Vertical, Center aligned

Icon:
- Size: 32x32
- Use Sprite (not emoji)

Label:
- Font: Noto Sans KR
- Size: 13
- Color: TextSecondary

Badge (Optional):
- Position: Top-Right (8, -8)
- Size: min 20x20
- Background: AccentSecondary
- Corner Radius: 10px
- Font Size: 11
- Animation: Scale pulse (1.0 → 1.1)
```

### 4.3 Bottom Navigation

```
NavItem:
- Size: Flexible (max 120px width)
- Height: 60px
- Background: BgCard + Border(GlassBorder)
- Corner Radius: 16px
- Layout: Vertical, Center aligned, gap 4px

Active State:
- Background: Gradient(AccentPrimary 20% opacity → transparent)
- Border: AccentPrimary
- Top Indicator: 3px height bar, AccentPrimary, glow effect
- Transform: TranslateY(-4px)

Icon:
- Size: 24x24
- Normal: TextMuted
- Active: AccentPrimary + DropShadow

Label:
- Font Size: 11
- Normal: TextMuted
- Active: AccentPrimary
```

### 4.4 TabGroupWidget 연동

```csharp
// TabGroupWidget에서 탭 버튼 연결
[SerializeField] private TabButtonWidget[] _tabButtons;

// 각 TabButtonWidget 설정
- Normal Sprite: card_bg_normal
- Selected Sprite: card_bg_selected
- Icon Normal Color: TextMuted
- Icon Selected Color: AccentPrimary
- Label Normal Color: TextMuted
- Label Selected Color: AccentPrimary
```

---

## 5. PrefabGenerator 수정 사항

### 5.1 LobbyScreen 전용 생성 메서드

```csharp
private static GameObject CreateLobbyScreenGameObject()
{
    var root = new GameObject("LobbyScreen");
    SetupFullScreenRect(root, topOffset: 80f); // ScreenHeader 영역 확보
    root.AddComponent<CanvasGroup>();
    root.AddComponent<LobbyScreen>();

    // 1. Background
    var bg = CreateChild(root, "Background");
    var bgImage = bg.AddComponent<Image>();
    bgImage.color = BgDeep;
    bgImage.raycastTarget = true;

    // 2. MainContent (ScreenHeader 아래, BottomNav 위)
    var mainContent = CreateMainContent(root);

    // 3. TabContents
    CreateTabContents(mainContent);

    // 4. BottomNav
    var bottomNav = CreateBottomNav(root);

    // SerializedField 연결
    ConnectSerializedFields(root);

    return root;
}
```

**참고**: Header/Currency는 ScreenHeader에서 관리하므로 LobbyScreen에서 생성하지 않음.

### 5.2 필요한 헬퍼 메서드

```csharp
// 레이아웃
private static void SetupFullScreenRect(GameObject go, float topOffset = 0f);
private static GameObject CreateMainContent(GameObject parent);

// 탭 컨텐츠
private static GameObject CreateTabContents(GameObject mainContent);
private static GameObject CreateHomeTabContent(Transform parent);
private static GameObject CreateCharacterTabContent(Transform parent);
private static GameObject CreateGachaTabContent(Transform parent);
private static GameObject CreateSettingsTabContent(Transform parent);

// UI 요소
private static GameObject CreateQuickMenuButton(Transform parent, string name, string label);
private static GameObject CreateBottomNav(GameObject parent);
private static GameObject CreateNavItem(Transform parent, string name, string label);
```

---

## 6. SerializeField 연결

```csharp
// LobbyScreen.cs에 필요한 참조
[Header("Tab System")]
[SerializeField] private TabGroupWidget _tabGroup;
[SerializeField] private LobbyTabContent[] _tabContents;

[Header("UI References")]
[SerializeField] private TMP_Text _welcomeText;

// 각 TabContent에 필요한 참조
// HomeTabContent
[SerializeField] private Button _stageButton;
[SerializeField] private Button _shopButton;
[SerializeField] private Button _eventButton;

// CharacterTabContent
[SerializeField] private Transform _characterListContainer;
[SerializeField] private Button _navigateButton;

// GachaTabContent
[SerializeField] private Transform _gachaPoolContainer;
[SerializeField] private Button _navigateButton;
```

---

## 7. 애니메이션 설정

### 7.1 탭 전환

```
Duration: 0.4s
Easing: EaseOutCubic

Outgoing Tab:
- Opacity: 1 → 0
- TranslateX: 0 → -20px

Incoming Tab:
- Opacity: 0 → 1
- TranslateX: 20px → 0
```

### 7.2 버튼 호버/프레스

```
Hover:
- Scale: 1.0 → 1.02
- Border Color: → AccentPrimary
- Duration: 0.3s

Press:
- Scale: 1.0 → 0.98
- Duration: 0.1s
```

### 7.3 캐릭터 Float 애니메이션

```
Animation: characterFloat
- TranslateY: 0 → -10px → 0
- Duration: 4s
- Easing: EaseInOutSine
- Loop: Infinite
```

---

## 8. 구현 우선순위

| 순서 | 항목 | 설명 |
|------|------|------|
| 1 | 기본 레이아웃 | Header, MainContent, BottomNav 구조 |
| 2 | TabGroup 연동 | 4개 탭 버튼 + 컨텐츠 전환 |
| 3 | HomeTab 퀵메뉴 | Stage, Shop, Event 버튼 |
| 4 | Currency 표시 | Gold, Gem, Stamina |
| 5 | 다른 탭 컨텐츠 | Character, Gacha, Settings |
| 6 | 애니메이션 | 탭 전환, 버튼 효과 |
| 7 | 배지 시스템 | 알림 배지 표시 |

---

## 9. 필요한 리소스

### Sprites
- `bg_card_normal.png` - 카드 배경 (9-sliced, rounded)
- `bg_card_selected.png` - 선택된 카드 배경
- `icon_home.png`, `icon_character.png`, `icon_gacha.png`, `icon_settings.png`
- `icon_stage.png`, `icon_shop.png`, `icon_event.png`
- `icon_gold.png`, `icon_gem.png`, `icon_stamina.png`
- `btn_add.png` - + 버튼

### Fonts
- Orbitron (또는 유사 SF 폰트) - 제목, 숫자
- Noto Sans KR - 본문

### Materials
- `GlowMaterial.mat` - 아이콘 글로우 효과용
