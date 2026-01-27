# Round 3 - Agent B: InGameContentDashboard

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음
> **선행 조건**: Round 1, 2 완료 (Stage 도메인)

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | InGameContentDashboard |
| Reference | `Docs/Design/Reference/StageDashbaord.jpg` |
| 스펙 문서 | `Docs/Specs/Stage.md` |
| 도메인 | Contents/Stage |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/StageDashbaord.jpg (이미지 분석)
2. Docs/Specs/Stage.md (InGameContentDashboard UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Stage/InGameContentDashboard.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Stage/Widgets/
├── ContentProgressWidget.cs
└── QuickActionWidget.cs
```

#### ContentProgressWidget.cs 요구사항
- 현재 진행 중인 스테이지/컨텐츠 표시
- 진행률 바
- 다음 보상 미리보기

#### QuickActionWidget.cs 요구사항
- 빠른 입장 버튼
- 자동 반복 설정
- 스킵 티켓 사용

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Stage/InGameContentDashboard.cs`

추가할 SerializeField:
```csharp
[SerializeField] private ContentProgressWidget _progressWidget;
[SerializeField] private QuickActionWidget _quickActionWidget;
[SerializeField] private Button _enterButton;
[SerializeField] private Button _stageSelectButton;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/InGameContentDashboardPrefabBuilder.cs
```

구조 참조:
```
InGameContentDashboard
├── Background
├── SafeArea
│   ├── Header (ScreenHeader)
│   ├── Content
│   │   ├── ProgressArea
│   │   │   └── ContentProgressWidget
│   │   └── ActionArea
│   │       └── QuickActionWidget
│   └── Footer
│       ├── StageSelectButton
│       └── EnterButton
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

---

## 완료 체크리스트

- [ ] ContentProgressWidget.cs 생성
- [ ] QuickActionWidget.cs 생성
- [ ] InGameContentDashboard.cs 수정
- [ ] InGameContentDashboardPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: InGameContentDashboard 프리팹 구현

### 생성된 파일
- Contents/Stage/Widgets/ContentProgressWidget.cs
- Contents/Stage/Widgets/QuickActionWidget.cs
- Editor/Wizard/Generators/InGameContentDashboardPrefabBuilder.cs

### 수정된 파일
- Contents/Stage/InGameContentDashboard.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
```
