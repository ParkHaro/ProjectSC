# Round 2 - Agent B: GachaScreen

> **독립 실행 가능** - 다른 에이전트와 파일 충돌 없음

---

## 작업 개요

| 항목 | 값 |
|------|-----|
| Screen | GachaScreen |
| Reference | `Docs/Design/Reference/Gacha.jpg` |
| 스펙 문서 | `Docs/Specs/Gacha.md` |
| 도메인 | Contents/Gacha |

---

## 작업 순서

### Step 1: 컨텍스트 파악

```
읽기:
1. Docs/Design/Reference/Gacha.jpg (이미지 분석)
2. Docs/Specs/Gacha.md (GachaScreen UI 레이아웃 섹션)
3. Assets/Scripts/Contents/Gacha/GachaScreen.cs (현재 구현)
```

### Step 2: Widget 클래스 생성

생성할 파일:
```
Assets/Scripts/Contents/Gacha/Widgets/
├── GachaBannerWidget.cs
└── GachaPullButtonWidget.cs
```

#### GachaBannerWidget.cs 요구사항
- 배너 이미지 (픽업 캐릭터)
- 배너 정보 (이름, 기간)
- 배너 스와이프 (여러 배너)

#### GachaPullButtonWidget.cs 요구사항
- 1회 뽑기 버튼 (비용 표시)
- 10회 뽑기 버튼 (비용 표시, 보너스 표시)
- 재화 부족 시 비활성화

### Step 3: Screen 클래스 수정

파일: `Assets/Scripts/Contents/Gacha/GachaScreen.cs`

추가할 SerializeField:
```csharp
[SerializeField] private GachaBannerWidget _bannerWidget;
[SerializeField] private GachaPullButtonWidget _pullButtonWidget;
[SerializeField] private Button _rateInfoButton;
[SerializeField] private Button _historyButton;
```

### Step 4: PrefabBuilder 구현

생성할 파일:
```
Assets/Scripts/Editor/Wizard/Generators/GachaScreenPrefabBuilder.cs
```

구조 참조:
```
GachaScreen
├── Background
├── SafeArea
│   ├── Header (ScreenHeader + CurrencyHUD)
│   ├── Content
│   │   ├── BannerArea
│   │   │   └── GachaBannerWidget
│   │   ├── InfoArea
│   │   │   ├── RateInfoButton
│   │   │   └── HistoryButton
│   │   └── PullArea
│   │       └── GachaPullButtonWidget
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

- [ ] GachaBannerWidget.cs 생성
- [ ] GachaPullButtonWidget.cs 생성
- [ ] GachaScreen.cs 수정
- [ ] GachaScreenPrefabBuilder.cs 생성
- [ ] 빌드 에러 없음

---

## 완료 보고 형식

```markdown
## 완료: GachaScreen 프리팹 구현

### 생성된 파일
- Contents/Gacha/Widgets/GachaBannerWidget.cs
- Contents/Gacha/Widgets/GachaPullButtonWidget.cs
- Editor/Wizard/Generators/GachaScreenPrefabBuilder.cs

### 수정된 파일
- Contents/Gacha/GachaScreen.cs

### 빌드 결과
- [ ] 컴파일 성공/실패
```
