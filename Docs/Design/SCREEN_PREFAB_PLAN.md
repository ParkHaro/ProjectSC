# SCREEN-PREFAB 병렬 작업 계획

> **목표**: Reference 이미지 기반 Screen 프리팹 상세 구현
> **전략**: 3개 에이전트 동시 병렬 작업

---

## 의존성 분석

### 스펙 문서별 Screen 그룹

| 스펙 문서 | Screen | 비고 |
|-----------|--------|------|
| Character.md | CharacterListScreen, CharacterDetailScreen | 동일 문서 |
| Shop.md | ShopScreen | 독립 |
| Gacha.md | GachaScreen | 독립 |
| LiveEvent.md | LiveEventScreen | 독립 |
| Stage.md | StageSelectScreen, PartySelectScreen, InGameContentDashboard | 동일 문서 |
| Inventory.md | InventoryScreen | 신규 생성 |

### 작업 단위별 의존성

```
[독립 작업 - 병렬 가능]
├─ ShopScreen (Shop.md)
├─ GachaScreen (Gacha.md)
├─ LiveEventScreen (LiveEvent.md)
└─ InventoryScreen (Inventory.md - 신규)

[순차 권장 - 동일 문서]
├─ Character.md: CharacterList → CharacterDetail
└─ Stage.md: StageSelect → PartySelect → InGameContentDashboard
```

---

## 병렬 작업 구조

### Round 1 (3개 동시)

| Agent | Screen | 스펙 문서 | 난이도 |
|-------|--------|-----------|--------|
| **A** | CharacterListScreen | Character.md | 중 |
| **B** | ShopScreen | Shop.md | 중 |
| **C** | StageSelectScreen | Stage.md | 상 |

### Round 2 (3개 동시)

| Agent | Screen | 스펙 문서 | 난이도 |
|-------|--------|-----------|--------|
| **A** | CharacterDetailScreen | Character.md | 상 |
| **B** | GachaScreen | Gacha.md | 중 |
| **C** | PartySelectScreen | Stage.md | 상 |

### Round 3 (3개 동시)

| Agent | Screen | 스펙 문서 | 난이도 |
|-------|--------|-----------|--------|
| **A** | LiveEventScreen | LiveEvent.md | 중 |
| **B** | InGameContentDashboard | Stage.md | 하 |
| **C** | InventoryScreen (신규) | Inventory.md | 중 |

---

## 각 에이전트 작업 흐름

### 단일 Screen 작업 (순차)

```
1. Reference 이미지 분석
   └─ Docs/Design/Reference/{Screen}.jpg

2. 스펙 문서 UI 레이아웃 확인
   └─ Docs/Specs/{Assembly}.md

3. Widget 클래스 설계/생성
   └─ Assets/Scripts/Contents/{Domain}/Widgets/

4. Screen 클래스 수정
   └─ Assets/Scripts/Contents/{Domain}/{Screen}.cs

5. PrefabBuilder 구현
   └─ Assets/Scripts/Editor/Wizard/Generators/{Screen}PrefabBuilder.cs

6. 프리팹 생성 테스트
   └─ Unity Editor에서 생성 확인
```

### 병렬 가능한 세부 작업

```
[동시 진행 가능]
├─ Widget 클래스 생성 (서로 다른 파일)
├─ PrefabBuilder 구현 (서로 다른 파일)
└─ 프리팹 생성 (서로 다른 프리팹)

[순차 진행 필요]
├─ Screen 클래스 수정 → PrefabBuilder 연동
└─ Widget 정의 → Screen에서 참조
```

---

## 파일 생성 계획

### Round 1 파일

#### Agent A: CharacterListScreen

```
생성:
├─ Contents/Character/Widgets/CharacterCard.cs
├─ Contents/Character/Widgets/CharacterFilterWidget.cs
└─ Editor/Wizard/Generators/CharacterListScreenPrefabBuilder.cs

수정:
└─ Contents/Character/CharacterListScreen.cs
```

#### Agent B: ShopScreen

```
생성:
├─ Contents/Shop/Widgets/ShopProductCard.cs
├─ Contents/Shop/Widgets/ShopTabWidget.cs
└─ Editor/Wizard/Generators/ShopScreenPrefabBuilder.cs

수정:
└─ Contents/Shop/ShopScreen.cs
```

#### Agent C: StageSelectScreen

```
생성:
├─ Contents/Stage/Widgets/StageMapWidget.cs
├─ Contents/Stage/Widgets/StageNodeWidget.cs
├─ Contents/Stage/Widgets/ChapterSelectWidget.cs
└─ Editor/Wizard/Generators/StageSelectScreenPrefabBuilder.cs

수정:
└─ Contents/Stage/StageSelectScreen.cs
```

### Round 2 파일

#### Agent A: CharacterDetailScreen

```
생성:
├─ Contents/Character/Widgets/CharacterInfoWidget.cs
├─ Contents/Character/Widgets/CharacterStatWidget.cs
├─ Contents/Character/Widgets/SkillListWidget.cs
└─ Editor/Wizard/Generators/CharacterDetailScreenPrefabBuilder.cs

수정:
└─ Contents/Character/CharacterDetailScreen.cs
```

#### Agent B: GachaScreen

```
생성:
├─ Contents/Gacha/Widgets/GachaBannerWidget.cs
├─ Contents/Gacha/Widgets/GachaPullButtonWidget.cs
└─ Editor/Wizard/Generators/GachaScreenPrefabBuilder.cs

수정:
└─ Contents/Gacha/GachaScreen.cs
```

#### Agent C: PartySelectScreen

```
생성:
├─ Contents/Stage/Widgets/PartySlotWidget.cs
├─ Contents/Stage/Widgets/CharacterSelectWidget.cs
├─ Contents/Stage/Widgets/StageInfoWidget.cs
└─ Editor/Wizard/Generators/PartySelectScreenPrefabBuilder.cs

수정:
└─ Contents/Stage/PartySelectScreen.cs
```

### Round 3 파일

#### Agent A: LiveEventScreen

```
생성:
├─ Contents/LiveEvent/Widgets/EventBannerWidget.cs
├─ Contents/LiveEvent/Widgets/EventTabWidget.cs
├─ Contents/LiveEvent/Widgets/EventTimerWidget.cs
└─ Editor/Wizard/Generators/LiveEventScreenPrefabBuilder.cs

수정:
└─ Contents/LiveEvent/LiveEventScreen.cs
```

#### Agent B: InGameContentDashboard

```
생성:
├─ Contents/Stage/Widgets/ContentProgressWidget.cs
├─ Contents/Stage/Widgets/QuickActionWidget.cs
└─ Editor/Wizard/Generators/InGameContentDashboardPrefabBuilder.cs

수정:
└─ Contents/Stage/InGameContentDashboard.cs
```

#### Agent C: InventoryScreen (신규)

```
생성:
├─ Contents/Inventory/InventoryScreen.cs (신규)
├─ Contents/Inventory/Widgets/ItemCard.cs
├─ Contents/Inventory/Widgets/ItemDetailWidget.cs
├─ Contents/Inventory/Widgets/InventoryTabWidget.cs
└─ Editor/Wizard/Generators/InventoryScreenPrefabBuilder.cs

설정:
└─ Addressables 등록 필요
```

---

## 에이전트 프롬프트 템플릿

```markdown
## 작업: {ScreenName} 프리팹 구현

### 입력
- Reference: `Docs/Design/Reference/{Screen}.jpg`
- 스펙 문서: `Docs/Specs/{Assembly}.md`
- 기존 Screen: `Assets/Scripts/Contents/{Domain}/{Screen}.cs`

### 작업 순서
1. Reference 이미지를 분석하여 UI 구조 파악
2. 스펙 문서의 UI 레이아웃 섹션 확인
3. 필요한 Widget 클래스 생성
4. Screen 클래스에 Widget 필드 추가
5. PrefabBuilder 구현
6. 빌드 테스트

### 출력
- 생성된 Widget 클래스 목록
- 수정된 Screen 클래스
- PrefabBuilder 구현
- 빌드 결과

### 참조
- `Docs/Specs/Lobby.md` - 완료 예시
- `Assets/Scripts/Contents/Lobby/LobbyScreen.cs` - Screen 구현 예시
- `Assets/Scripts/Editor/Wizard/Generators/LobbyScreenPrefabBuilder.Generated.cs` - Builder 예시
```

---

## 체크리스트

### Round 1

- [ ] **Agent A**: CharacterListScreen
  - [ ] CharacterCard Widget
  - [ ] CharacterFilterWidget
  - [ ] CharacterListScreenPrefabBuilder
  - [ ] 빌드 테스트

- [ ] **Agent B**: ShopScreen
  - [ ] ShopProductCard Widget
  - [ ] ShopTabWidget
  - [ ] ShopScreenPrefabBuilder
  - [ ] 빌드 테스트

- [ ] **Agent C**: StageSelectScreen
  - [ ] StageMapWidget
  - [ ] StageNodeWidget
  - [ ] ChapterSelectWidget
  - [ ] StageSelectScreenPrefabBuilder
  - [ ] 빌드 테스트

### Round 2

- [ ] **Agent A**: CharacterDetailScreen
- [ ] **Agent B**: GachaScreen
- [ ] **Agent C**: PartySelectScreen

### Round 3

- [ ] **Agent A**: LiveEventScreen
- [ ] **Agent B**: InGameContentDashboard
- [ ] **Agent C**: InventoryScreen (신규)

---

## 완료 기준

### 각 Screen 완료 조건

1. ✅ Widget 클래스 생성 (컴파일 성공)
2. ✅ Screen 클래스 수정 (SerializeField 추가)
3. ✅ PrefabBuilder 구현
4. ✅ 프리팹 생성 가능
5. ✅ 빌드 에러 없음

### 마일스톤 완료 조건

1. ✅ 9개 Screen 모두 구현
2. ✅ InventoryScreen 신규 생성 및 Addressables 등록
3. ✅ PrefabSync JSON Spec 생성 (선택)
