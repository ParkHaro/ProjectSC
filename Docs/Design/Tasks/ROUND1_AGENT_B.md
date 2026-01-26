# Round 1 - Agent B: ShopScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | ShopScreen |
| Reference | `Docs/Design/Reference/Shop.jpg` |
| 스펙 문서 | `Docs/Specs/Shop.md` |
| 도메인 | Contents/Shop |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/Shop.jpg (이미지 분석)
2. Docs/Specs/Shop.md (ShopScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Shop/ShopScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Shop/Widgets/
├── ShopProductCard.cs
└── ShopTabWidget.cs
```

#### ShopProductCard.cs 요구사항
- 상품 이미지, 이름, 가격, 할인율 표시
- 구매 버튼
- 구매 가능/불가 상태 표시

#### ShopTabWidget.cs 요구사항
- 탭 버튼들 (일반, 패키지, 이벤트 등)
- 탭 선택 이벤트

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Shop/ShopScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private ShopTabWidget _tabWidget;
[SerializeField] private Transform _productGridContainer;
[SerializeField] private ScrollRect _productScrollView;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs
```

구조 참조:
```
ShopScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader + CurrencyHUD)
│   ├── TabArea
│   │   └── ShopTabWidget
│   └── Content
│       └── ProductGrid (ScrollView + GridLayoutGroup)
└── OverlayLayer
```

### Step 5: 빌드 테스트

```bash
# Unity Editor 컴파일 확인
# 에러 없이 빌드되는지 확인
```

---

## 참조 예시

### Widget 구현 예시
```
Assets/Scripts/Contents/Lobby/Widgets/QuickMenuButton.cs
```

### Screen 구현 예시
```
Assets/Scripts/Contents/Lobby/LobbyScreen.cs
```

### PrefabBuilder 예시
```
Assets/Scripts/Editor/Wizard/Generators/LobbyScreenPrefabBuilder.Generated.cs
```

---

## 완료 체크리스트

- [ ] ShopProductCard.cs 생성
- [ ] ShopTabWidget.cs 생성
- [ ] ShopScreen.cs 수정
- [ ] ShopScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: ShopScreen 프리팹 구현

### 생성된 파일
- Contents/Shop/Widgets/ShopProductCard.cs
- Contents/Shop/Widgets/ShopTabWidget.cs
- Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Shop/ShopScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
- [ ] 프리팹 생성 테스트 (선택)
```
