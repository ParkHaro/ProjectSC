# Round 1 - Agent C: StageSelectScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | StageSelectScreen |
| Reference | `Docs/Design/Reference/StageSelectScreen.jpg` |
| 스펙 문서 | `Docs/Specs/Stage.md` |
| 도메인 | Contents/Stage |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/StageSelectScreen.jpg (이미지 분석)
2. Docs/Specs/Stage.md (StageSelectScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Stage/StageSelectScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Stage/Widgets/
├── StageMapWidget.cs
├── StageNodeWidget.cs
└── ChapterSelectWidget.cs
```

#### StageMapWidget.cs 요구사항
- 맵 배경 표시
- StageNode들의 컨테이너
- 맵 스크롤/줌 기능

#### StageNodeWidget.cs 요구사항
- 스테이지 노드 표시 (번호, 별점, 상태)
- 클릭 이벤트 (스테이지 선택)
- 완료/미완료/잠금 상태 표시

#### ChapterSelectWidget.cs 요구사항
- 챕터 선택 드롭다운 또는 버튼
- 챕터 변경 이벤트

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Stage/StageSelectScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private ChapterSelectWidget _chapterSelectWidget;
[SerializeField] private StageMapWidget _stageMapWidget;
[SerializeField] private TMP_Text _stageProgressText;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/StageSelectScreenPrefabBuilder.cs
```

구조 참조:
```
StageSelectScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader + CurrencyHUD)
│   ├── RightTopArea
│   │   └── StageProgressWidget
│   ├── LeftArea
│   │   └── ChapterSelectWidget
│   └── Content
│       └── StageMapWidget
│           └── StageNodes (Container)
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
Assets/Scripts/Contents/Lobby/Widgets/StageProgressWidget.cs
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

- [ ] StageMapWidget.cs 생성
- [ ] StageNodeWidget.cs 생성
- [ ] ChapterSelectWidget.cs 생성
- [ ] StageSelectScreen.cs 수정
- [ ] StageSelectScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: StageSelectScreen 프리팹 구현

### 생성된 파일
- Contents/Stage/Widgets/StageMapWidget.cs
- Contents/Stage/Widgets/StageNodeWidget.cs
- Contents/Stage/Widgets/ChapterSelectWidget.cs
- Editor/Wizard/Generators/StageSelectScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Stage/StageSelectScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
- [ ] 프리팹 생성 테스트 (선택)
```
