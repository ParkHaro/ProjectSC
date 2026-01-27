# Round 2 - Agent C: PartySelectScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음
> **선행 조건**: Round 1 완료 (StageSelectScreen)

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | PartySelectScreen |
| Reference | `Docs/Design/Reference/PartySelect.jpg` |
| 스펙 문서 | `Docs/Specs/Stage.md` |
| 도메인 | Contents/Stage |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/PartySelect.jpg (이미지 분석)
2. Docs/Specs/Stage.md (PartySelectScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Stage/PartySelectScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Stage/Widgets/
├── PartySlotWidget.cs
├── CharacterSelectWidget.cs
└── StageInfoWidget.cs
```

#### PartySlotWidget.cs 요구사항
- 편성 슬롯 (4~6개)
- 캐릭터 할당/해제
- 빈 슬롯 / 할당된 슬롯 상태

#### CharacterSelectWidget.cs 요구사항
- 선택 가능한 캐릭터 그리드
- 이미 편성된 캐릭터 표시
- 속성 필터

#### StageInfoWidget.cs 요구사항
- 스테이지 정보 (이름, 난이도)
- 적 정보 미리보기
- 권장 전투력

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Stage/PartySelectScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private PartySlotWidget _partySlotWidget;
[SerializeField] private CharacterSelectWidget _characterSelectWidget;
[SerializeField] private StageInfoWidget _stageInfoWidget;
[SerializeField] private Button _startBattleButton;
[SerializeField] private Button _autoFormationButton;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/PartySelectScreenPrefabBuilder.cs
```

구조 참조:
```
PartySelectScreen
├── Background
├── SafeArea
│   ├── Header (BackButton + StageInfo)
│   ├── Content
│   │   ├── TopArea
│   │   │   └── PartySlotWidget (편성 슬롯)
│   │   ├── MiddleArea
│   │   │   └── CharacterSelectWidget (캐릭터 선택)
│   │   └── RightArea
│   │       └── StageInfoWidget (스테이지 정보)
│   └── Footer
│       ├── AutoFormationButton
│       └── StartBattleButton
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

---

## 완료 체크리스트

- [ ] PartySlotWidget.cs 생성
- [ ] CharacterSelectWidget.cs 생성
- [ ] StageInfoWidget.cs 생성
- [ ] PartySelectScreen.cs 수정
- [ ] PartySelectScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: PartySelectScreen 프리팹 구현

### 생성된 파일
- Contents/Stage/Widgets/PartySlotWidget.cs
- Contents/Stage/Widgets/CharacterSelectWidget.cs
- Contents/Stage/Widgets/StageInfoWidget.cs
- Editor/Wizard/Generators/PartySelectScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Stage/PartySelectScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
```
