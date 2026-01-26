# Round 3 - Agent A: LiveEventScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | LiveEventScreen |
| Reference | `Docs/Design/Reference/LiveEvent.jpg` |
| 스펙 문서 | `Docs/Specs/LiveEvent.md` |
| 도메인 | Contents/LiveEvent |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/LiveEvent.jpg (이미지 분석)
2. Docs/Specs/LiveEvent.md (LiveEventScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/LiveEvent/LiveEventScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/LiveEvent/Widgets/
├── EventBannerWidget.cs
├── EventTabWidget.cs
└── EventTimerWidget.cs
```

#### EventBannerWidget.cs 요구사항
- 이벤트 메인 배너 이미지
- 이벤트 제목
- 이벤트 상세 버튼

#### EventTabWidget.cs 요구사항
- 스토리, 미션, 상점 탭
- 탭 선택 이벤트
- 탭별 알림 뱃지

#### EventTimerWidget.cs 요구사항
- 이벤트 남은 시간 표시
- 실시간 카운트다운

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/LiveEvent/LiveEventScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private EventBannerWidget _bannerWidget;
[SerializeField] private EventTabWidget _tabWidget;
[SerializeField] private EventTimerWidget _timerWidget;
[SerializeField] private Transform _contentContainer;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/LiveEventScreenPrefabBuilder.cs
```

구조 참조:
```
LiveEventScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader)
│   ├── BannerArea
│   │   ├── EventBannerWidget
│   │   └── EventTimerWidget
│   ├── TabArea
│   │   └── EventTabWidget
│   └── Content
│       └── ContentContainer (탭별 콘텐츠)
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
Assets/Scripts/Contents/Lobby/Widgets/EventBannerCarousel.cs
```

### Screen 구현 예시
```
Assets/Scripts/Contents/Lobby/LobbyScreen.cs
```

---

## 완료 체크리스트

- [ ] EventBannerWidget.cs 생성
- [ ] EventTabWidget.cs 생성
- [ ] EventTimerWidget.cs 생성
- [ ] LiveEventScreen.cs 수정
- [ ] LiveEventScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: LiveEventScreen 프리팹 구현

### 생성된 파일
- Contents/LiveEvent/Widgets/EventBannerWidget.cs
- Contents/LiveEvent/Widgets/EventTabWidget.cs
- Contents/LiveEvent/Widgets/EventTimerWidget.cs
- Editor/Wizard/Generators/LiveEventScreenPrefabBuilder.cs

### 수정된 파일
- Contents/LiveEvent/LiveEventScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
```
