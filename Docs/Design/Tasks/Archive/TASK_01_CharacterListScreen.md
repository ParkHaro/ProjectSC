# TASK 01: CharacterListScreen ManualBuilder

> **독립 실행 가능** - 다른 작업과 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | CharacterListScreen |
| Reference | `Docs/Design/Reference/CharacterList.jpg` |
| 스펙 문서 | `Docs/Specs/Character.md` |
| 출력 파일 | `Assets/Scripts/Editor/Wizard/Generators/CharacterListScreenPrefabBuilder.cs` |

---

## 사전 확인

### 1. 레퍼런스 이미지 분석
```
Docs/Design/Reference/CharacterList.jpg
```

### 2. 스펙 문서의 UI 레이아웃 확인
```
Docs/Specs/Character.md → "CharacterListScreen UI 레이아웃 구조" 섹션
```

### 3. Screen 클래스 확인
```
Assets/Scripts/Contents/Character/CharacterListScreen.cs
```

---

## Prefab 계층 구조 (구현 목표)

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
│   │       ├─ AllCharactersTab ("모여라 사도!")
│   │       └─ FavoritesTab ("관심 사도 0/2")
│   │
│   ├─ FilterArea (Below Tab, 40px)
│   │   └─ FilterGroup (HorizontalLayoutGroup)
│   │       ├─ ExpressionFilter
│   │       ├─ FilterToggle
│   │       ├─ Spacer (FlexibleWidth)
│   │       ├─ SortButton
│   │       └─ SortOrderToggle
│   │
│   └─ Content (Stretch, remaining)
│       └─ CharacterGrid (ScrollView)
│           └─ Viewport
│               └─ Content (GridLayoutGroup: 6열)
│                   └─ CharacterCard (Template) x N
│
└─ OverlayLayer
```

---

## 영역별 구현 상세

### 1. Header (60px)

| 요소 | 구현 |
|------|------|
| BackButton | Button + Image (뒤로가기 아이콘) |
| TitleText | TMP_Text ("사도") |
| CurrencyHUD | ScreenHeader 프리팹 사용 또는 직접 구성 |
| HomeButton | Button + Image (홈 아이콘) |

### 2. TabArea (50px)

| 요소 | 구현 |
|------|------|
| AllCharactersTab | Button + TMP_Text ("모여라 사도!") |
| FavoritesTab | Button + TMP_Text ("관심 사도 0/2") |
| 활성 상태 | 연두색 배경, 비활성은 투명 |

### 3. FilterArea (40px)

| 요소 | 구현 |
|------|------|
| ExpressionFilter | Button ("감정표현") |
| FilterToggle | Toggle ("필터 OFF") |
| SortButton | Button ("정렬") |
| SortOrderToggle | Toggle (↓/↑) |

### 4. CharacterGrid

| 요소 | 구현 |
|------|------|
| ScrollView | Scroll Rect + Mask |
| GridLayout | 6열, Cell: 150x200, Spacing: 10 |
| CharacterCard | 템플릿 (비활성 상태로 배치) |

### CharacterCard 구조

```
CharacterCard (150x200)
├─ CardBackground (속성별 색상)
├─ CharacterThumbnail (Image)
├─ ElementIcon (좌상단, 30x30)
├─ RoleIcon (우측, 30x30)
├─ StarRating (하단, HorizontalLayoutGroup)
│   └─ Star x 5
└─ NameText (하단, TMP_Text)
```

---

## SerializeField 연결

Screen 클래스의 SerializeField와 Prefab 연결:

```csharp
// CharacterListScreen.cs에 있어야 할 필드들
[SerializeField] private Button _backButton;
[SerializeField] private Button _homeButton;
[SerializeField] private Button _allCharactersTab;
[SerializeField] private Button _favoritesTab;
[SerializeField] private Button _expressionFilterButton;
[SerializeField] private Toggle _filterToggle;
[SerializeField] private Button _sortButton;
[SerializeField] private Toggle _sortOrderToggle;
[SerializeField] private ScrollRect _characterGrid;
[SerializeField] private Transform _gridContent;
[SerializeField] private GameObject _characterCardTemplate;
```

### ConnectSerializedFields 구현

```csharp
private static void ConnectSerializedFields(
    GameObject root,
    Button backButton,
    Button homeButton,
    Button allTab,
    Button favTab,
    Button expressionFilter,
    Toggle filterToggle,
    Button sortButton,
    Toggle sortOrderToggle,
    ScrollRect grid,
    Transform gridContent,
    GameObject cardTemplate)
{
    var screen = root.GetComponent<CharacterListScreen>();
    if (screen == null) return;

    var so = new SerializedObject(screen);
    so.FindProperty("_backButton").objectReferenceValue = backButton;
    so.FindProperty("_homeButton").objectReferenceValue = homeButton;
    // ... 나머지 필드들
    so.ApplyModifiedPropertiesWithoutUndo();
}
```

---

## 구현 순서

### Step 1: 기본 구조 생성
```csharp
public static GameObject Build()
{
    var root = CreateRoot();
    CreateBackground(root);
    var safeArea = CreateSafeArea(root);
    // ...
    return root;
}
```

### Step 2: Header 구현
- BackButton, TitleText, CurrencyHUD, HomeButton

### Step 3: TabArea 구현
- AllCharactersTab, FavoritesTab

### Step 4: FilterArea 구현
- ExpressionFilter, FilterToggle, SortButton, SortOrderToggle

### Step 5: CharacterGrid 구현
- ScrollView + GridLayoutGroup
- CharacterCard 템플릿

### Step 6: SerializeField 연결
- ConnectSerializedFields 구현

---

## 참조 예시

### ManualBuilder 예시
```
Assets/Scripts/Editor/Wizard/Generators/TitleScreenPrefabBuilder.cs
```

### UI 헬퍼
```csharp
// 폰트 적용
var font = EditorUIHelpers.GetProjectFont();
if (font != null) tmp.font = font;

// RectTransform Stretch 설정
private static void SetStretch(RectTransform rt)
{
    rt.anchorMin = Vector2.zero;
    rt.anchorMax = Vector2.one;
    rt.sizeDelta = Vector2.zero;
    rt.anchoredPosition = Vector2.zero;
}
```

---

## 체크리스트

- [ ] `CharacterListScreenPrefabBuilder.cs` 파일 생성
- [ ] 컬러 상수 정의 (테마 일관성)
- [ ] `Build()` 메서드 구현
- [ ] Background 생성
- [ ] SafeArea 생성
- [ ] Header 영역 (BackButton, TitleText, CurrencyHUD, HomeButton)
- [ ] TabArea (AllCharactersTab, FavoritesTab)
- [ ] FilterArea (ExpressionFilter, FilterToggle, SortButton, SortOrderToggle)
- [ ] CharacterGrid (ScrollView + GridLayoutGroup)
- [ ] CharacterCard 템플릿
- [ ] OverlayLayer
- [ ] `ConnectSerializedFields()` 구현
- [ ] 컴파일 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: CharacterListScreen ManualBuilder

### 생성된 파일
- `Assets/Scripts/Editor/Wizard/Generators/CharacterListScreenPrefabBuilder.cs`

### 구현된 영역
- [x] Header (BackButton, TitleText, CurrencyHUD, HomeButton)
- [x] TabArea (AllCharactersTab, FavoritesTab)
- [x] FilterArea (4개 요소)
- [x] CharacterGrid (ScrollView + GridLayoutGroup)
- [x] CharacterCard 템플릿
- [x] SerializeField 연결

### 빌드 결과
- [x] 컴파일 성공
- [ ] 프리팹 생성 테스트

### 특이사항
- (있다면 기록)
```
