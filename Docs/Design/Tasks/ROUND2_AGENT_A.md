# Round 2 - Agent A: CharacterDetailScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음
> **선행 조건**: Round 1 완료 (CharacterListScreen)

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | CharacterDetailScreen |
| Reference | `Docs/Design/Reference/CharacterDetail.jpg` |
| 스펙 문서 | `Docs/Specs/Character.md` |
| 도메인 | Contents/Character |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/CharacterDetail.jpg (이미지 분석)
2. Docs/Specs/Character.md (CharacterDetailScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Character/CharacterDetailScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Character/Widgets/
├── CharacterInfoWidget.cs
├── CharacterStatWidget.cs
└── SkillListWidget.cs
```

#### CharacterInfoWidget.cs 요구사항
- 캐릭터 전신 이미지
- 이름, 레벨, 속성, 역할 표시
- 별점 표시

#### CharacterStatWidget.cs 요구사항
- HP, 공격력, 방어력, 속도 등 스탯 표시
- 전투력 총합

#### SkillListWidget.cs 요구사항
- 스킬 아이콘 목록 (일반, 특수, 궁극기)
- 스킬 선택 시 상세 정보 표시

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Character/CharacterDetailScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private CharacterInfoWidget _characterInfoWidget;
[SerializeField] private CharacterStatWidget _statWidget;
[SerializeField] private SkillListWidget _skillListWidget;
[SerializeField] private Button _levelUpButton;
[SerializeField] private Button _ascensionButton;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/CharacterDetailScreenPrefabBuilder.cs
```

구조 참조:
```
CharacterDetailScreen
├── Background
├── SafeArea
│   ├── Header (BackButton + Title + CurrencyHUD)
│   ├── Content
│   │   ├── LeftArea
│   │   │   └── CharacterInfoWidget (전신 이미지)
│   │   └── RightArea
│   │       ├── CharacterStatWidget
│   │       └── SkillListWidget
│   └── Footer
│       ├── LevelUpButton
│       └── AscensionButton
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
Assets/Scripts/Contents/Lobby/Widgets/CharacterDisplayWidget.cs
```

### Screen 구현 예시
```
Assets/Scripts/Contents/Lobby/LobbyScreen.cs
```

---

## 완료 체크리스트

- [ ] CharacterInfoWidget.cs 생성
- [ ] CharacterStatWidget.cs 생성
- [ ] SkillListWidget.cs 생성
- [ ] CharacterDetailScreen.cs 수정
- [ ] CharacterDetailScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: CharacterDetailScreen 프리팹 구현

### 생성된 파일
- Contents/Character/Widgets/CharacterInfoWidget.cs
- Contents/Character/Widgets/CharacterStatWidget.cs
- Contents/Character/Widgets/SkillListWidget.cs
- Editor/Wizard/Generators/CharacterDetailScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Character/CharacterDetailScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
```
