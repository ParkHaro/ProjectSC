# UI 레이아웃 문서화 작업 가이드

## 목적
레퍼런스 이미지를 분석하여 각 Screen의 UI 레이아웃 구조를 스펙 문서에 추가합니다.

---

## 작업 절차

### 1. 레퍼런스 이미지 분석
```
Docs/Design/Reference/{ScreenName}.jpg
```
- 이미지를 Read 도구로 열어 분석
- 영역별 UI 요소 식별
- 버튼/텍스트/아이콘 등 요소 목록화

### 2. 기존 스펙 문서 확인
```
Docs/Specs/{Assembly}.md
```
- 해당 Screen의 기존 문서 확인
- 이미 UI 레이아웃 섹션이 있는지 확인

### 3. UI 레이아웃 섹션 추가

#### 추가 위치
- `## 개요` 섹션 바로 아래
- `## 참조` 또는 `## 구조` 섹션 위

#### 필수 섹션 구조
```markdown
## 레퍼런스
- `Docs/Design/Reference/{ScreenName}.jpg`

---

## UI 레이아웃 구조

### 전체 구조
(ASCII 다이어그램)

### 영역별 상세
(테이블 형식)

### Prefab 계층 구조
(Unity 계층 트리)

### 컴포넌트 매핑
(SerializeField 매핑 테이블)

### 네비게이션 흐름
(화면 이동 다이어그램)
```

---

## 템플릿

### 전체 구조 템플릿 (ASCII)
```
{ScreenName} (FullScreen/Popup)
├─ Header ───────────────────────────────────────────────────────────────
│   └─ (헤더 요소들)
│
├─ Content ──────────────────────────────────────────────────────────────
│   ├─ LeftArea
│   ├─ CenterArea
│   └─ RightArea
│
└─ Footer ───────────────────────────────────────────────────────────────
    └─ (푸터 요소들)
```

### 영역별 상세 템플릿 (테이블)
```markdown
#### {영역명}
| 요소 | 설명 |
|------|------|
| **ElementName** | 설명 |
| - SubElement | 하위 설명 |
```

### Prefab 계층 구조 템플릿
```
{ScreenName} (RectTransform: Stretch)
├─ Background
│   └─ Image (BgDeep)
│
├─ SafeArea
│   ├─ Header (Top, {height}px)
│   │   └─ ...
│   │
│   ├─ Content (Stretch, Top={headerHeight})
│   │   └─ ...
│   │
│   └─ Footer (Bottom, {height}px) - 선택
│       └─ ...
│
└─ OverlayLayer - 선택
```

### 컴포넌트 매핑 템플릿
```markdown
| 영역 | Widget/Component | SerializeField |
|------|------------------|----------------|
| Header | ScreenHeader | `_screenHeader` |
| Content | SomeWidget | `_someWidget` |
```

### 네비게이션 흐름 템플릿
```markdown
{ScreenName}
├─ Button1 → TargetScreen1
├─ Button2 → TargetScreen2
└─ BackButton → 이전 화면
```

---

## 명명 규칙

### 영역명
| 영역 | 명명 |
|------|------|
| 상단 | Header, TopArea |
| 좌상단 | LeftTopArea |
| 우상단 | RightTopArea |
| 중앙 | CenterArea, Content |
| 좌하단 | LeftBottomArea |
| 우하단 | RightBottomArea |
| 하단 | Footer, BottomNav |

### 컴포넌트명
| 유형 | 접미사 | 예시 |
|------|--------|------|
| 버튼 | Button | ConfirmButton, BackButton |
| 텍스트 | Text, Label | TitleText, DescLabel |
| 이미지 | Image, Icon | CharacterImage, ItemIcon |
| 컨테이너 | Container, Group | ItemContainer, ButtonGroup |
| 위젯 | Widget | TabWidget, SliderWidget |
| 리스트 | List, Grid | ItemList, CharacterGrid |

### SerializeField 명명
```csharp
[SerializeField] private Button _confirmButton;
[SerializeField] private TMP_Text _titleText;
[SerializeField] private Image _characterImage;
[SerializeField] private Transform _itemContainer;
```

---

## 완료 기준

### 필수 체크리스트
- [ ] 레퍼런스 이미지 경로 추가
- [ ] 전체 구조 ASCII 다이어그램 작성
- [ ] 영역별 상세 테이블 작성
- [ ] Prefab 계층 구조 작성
- [ ] 컴포넌트 매핑 테이블 작성
- [ ] 네비게이션 흐름 작성

### 선택 체크리스트
- [ ] 상태별 UI 변화 (로딩, 에러, 빈 상태)
- [ ] 애니메이션/전환 효과 명세
- [ ] 반응형 레이아웃 규칙

---

## 예시: Lobby.md

완료된 예시는 `Docs/Specs/Lobby.md`의 "UI 레이아웃 구조" 섹션을 참조하세요.

---

## 작업 할당

| 레퍼런스 | 스펙 문서 | Screen 클래스 | 상태 |
|----------|-----------|---------------|------|
| Lobby.jpg | Lobby.md | LobbyScreen | ✅ 완료 |
| CharacterList.jpg | Character.md | CharacterListScreen | ✅ 완료 |
| CharacterDetail.jpg | Character.md | CharacterDetailScreen | ✅ 완료 |
| Shop.jpg | Shop.md | ShopScreen | ✅ 완료 |
| Gacha.jpg | Gacha.md | GachaScreen | ✅ 완료 |
| LiveEvent.jpg | LiveEvent.md | LiveEventScreen | ✅ 완료 |
| PartySelect.jpg | Stage.md | PartySelectScreen | ✅ 완료 |
| StageSelectScreen.jpg | Stage.md | StageSelectScreen | ✅ 완료 |
| StageDashbaord.jpg | Stage.md | InGameContentDashboard | ✅ 완료 |
| Inventory.jpg | Inventory.md | InventoryScreen | ✅ 완료 |
