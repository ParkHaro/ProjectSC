# Round 1 - Agent A: CharacterListScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | CharacterListScreen |
| Reference | `Docs/Design/Reference/CharacterList.jpg` |
| 스펙 문서 | `Docs/Specs/Character.md` |
| 도메인 | Contents/Character |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/CharacterList.jpg (이미지 분석)
2. Docs/Specs/Character.md (CharacterListScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Character/CharacterListScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Character/Widgets/
├── CharacterCard.cs
└── CharacterFilterWidget.cs
```

#### CharacterCard.cs 요구사항
- 캐릭터 썸네일, 속성 아이콘, 역할 아이콘, 별점, 이름 표시
- 속성별 배경 색상 변경
- 클릭 이벤트 (CharacterDetailScreen 이동)

#### CharacterFilterWidget.cs 요구사항
- 필터 버튼, 필터 토글, 정렬 버튼, 정렬 순서 토글
- 필터/정렬 상태 변경 이벤트

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Character/CharacterListScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private Transform _characterGridContainer;
[SerializeField] private CharacterFilterWidget _filterWidget;
[SerializeField] private Button _allTabButton;
[SerializeField] private Button _favoriteTabButton;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/CharacterListScreenPrefabBuilder.cs
```

구조 참조:
```
CharacterListScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader)
│   ├── TabArea
│   │   ├── AllTabButton ("모여라 사도!")
│   │   └── FavoriteTabButton ("관심 사도 0/2")
│   ├── FilterArea
│   │   └── CharacterFilterWidget
│   └── Content
│       └── CharacterGrid (ScrollView + GridLayoutGroup)
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

- [ ] CharacterCard.cs 생성
- [ ] CharacterFilterWidget.cs 생성
- [ ] CharacterListScreen.cs 수정
- [ ] CharacterListScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: CharacterListScreen 프리팹 구현

### 생성된 파일
- Contents/Character/Widgets/CharacterCard.cs
- Contents/Character/Widgets/CharacterFilterWidget.cs
- Editor/Wizard/Generators/CharacterListScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Character/CharacterListScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
- [ ] 프리팹 생성 테스트 (선택)
```
