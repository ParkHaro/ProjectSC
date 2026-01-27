# Round 3 - Agent C: InventoryScreen (신규)

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음
> **특이사항**: 신규 Screen 생성 필요

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | InventoryScreen (신규) |
| Reference | `Docs/Design/Reference/Inventory.jpg` |
| 스펙 문서 | `Docs/Specs/Inventory.md` |
| 도메인 | Contents/Inventory (신규) |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/Inventory.jpg (이미지 분석)
2. Docs/Specs/Inventory.md (UI 레이아웃 섹션)
3. 기존 Screen 구현 참조 (LobbyScreen, ShopScreen 등)
```

### Step 2: 폴더 구조 생성

생성할 폴더:
```
Assets/Scripts/Contents/Inventory/
├── InventoryScreen.cs
└── Widgets/
    ├── ItemCard.cs
    ├── ItemDetailWidget.cs
    └── InventoryTabWidget.cs
```

### Step 3: Widget 클래스 생성

#### ItemCard.cs 요구사항
- 아이템 아이콘
- 수량 표시
- 등급 표시 (색상)
- 선택 상태

#### ItemDetailWidget.cs 요구사항
- 선택된 아이템 상세 정보
- 아이템 설명
- 사용/판매 버튼

#### InventoryTabWidget.cs 요구사항
- 아이템 종류별 탭 (장비, 소비, 재료, 기타)
- 탭 선택 이벤트

### Step 4: Screen 클래스 생성 (신규)

파일: `Assets/Scripts/Contents/Inventory/InventoryScreen.cs`

```csharp
namespace Sc.Contents.Inventory
{
    public class InventoryScreen : ScreenBase
    {
        [SerializeField] private InventoryTabWidget _tabWidget;
        [SerializeField] private Transform _itemGridContainer;
        [SerializeField] private ItemDetailWidget _itemDetailWidget;
        [SerializeField] private ScrollRect _itemScrollView;
    }
}
```

### Step 5: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/InventoryScreenPrefabBuilder.cs
```

구조 참조:
```
InventoryScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader + CurrencyHUD)
│   ├── TabArea
│   │   └── InventoryTabWidget
│   ├── Content
│   │   ├── LeftArea
│   │   │   └── ItemGrid (ScrollView + GridLayoutGroup)
│   │   └── RightArea
│   │       └── ItemDetailWidget
└── OverlayLayer
```

### Step 6: Attribute 추가

InventoryScreen.cs에 추가:
```csharp
[ScreenTemplate(ScreenTemplateType.Standard)]
public class InventoryScreen : ScreenBase
```

### Step 7: 빌드 테스트 및 Addressables 등록

```bash
# Unity Editor 컴파일 확인
# PrefabGenerator로 프리팹 생성
# Addressables에 InventoryScreen 등록
```

---

## 참조 예시

### Screen 구현 예시
```
Assets/Scripts/Contents/Shop/ShopScreen.cs
```

### Widget 구현 예시
```
Assets/Scripts/Contents/Lobby/Widgets/QuickMenuButton.cs
```

### PrefabBuilder 예시
```
Assets/Scripts/Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs
```

---

## 완료 체크리스트

- [ ] Contents/Inventory 폴더 생성
- [ ] InventoryScreen.cs 생성
- [ ] ItemCard.cs 생성
- [ ] ItemDetailWidget.cs 생성
- [ ] InventoryTabWidget.cs 생성
- [ ] InventoryScreenPrefabBuilder.cs 생성
- [ ] ScreenTemplateAttribute 추가
- [ ] 빌드 에러 없음
- [ ] Addressables 등록 (선택)

---

## 완료 보고 형식

```markdown
## 완료: InventoryScreen 신규 구현

### 생성된 파일
- Contents/Inventory/InventoryScreen.cs (신규)
- Contents/Inventory/Widgets/ItemCard.cs
- Contents/Inventory/Widgets/ItemDetailWidget.cs
- Contents/Inventory/Widgets/InventoryTabWidget.cs
- Editor/Wizard/Generators/InventoryScreenPrefabBuilder.cs

### 추가 작업 필요
- [ ] Addressables 등록
- [ ] Navigation 연결

### 빌드 결과
- [ ] 컴파일 성공/실패
```
