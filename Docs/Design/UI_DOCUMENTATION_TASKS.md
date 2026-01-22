# UI 문서화 작업 정의서

각 에이전트가 독립적으로 작업할 수 있도록 구체적인 지시사항을 정의합니다.

---

## 공통 지시사항

### 작업 전 필수 확인
1. `Docs/Design/UI_DOCUMENTATION_GUIDE.md` 읽기
2. `Docs/Specs/Lobby.md`의 "UI 레이아웃 구조" 섹션 참조 (완료 예시)

### 작업 순서
1. 레퍼런스 이미지 분석 (`Read` 도구 사용)
2. 기존 스펙 문서 읽기
3. UI 레이아웃 섹션 추가 (`Edit` 도구 사용)
4. 완료 보고

### 출력 형식
```markdown
## 완료 보고: {ScreenName} UI 문서화

### 수행 내용
- 레퍼런스 이미지 분석 완료
- 스펙 문서 업데이트 완료

### 식별된 영역
- {영역1}: {요소 개수}개
- {영역2}: {요소 개수}개
- ...

### 네비게이션 흐름
- {버튼1} → {대상 Screen}
- ...

### 참고 파일
- 레퍼런스: `{경로}`
- 스펙 문서: `{경로}`
```

---

## Task 1: CharacterList

### 입력
- **레퍼런스**: `Docs/Design/Reference/CharacterList.jpg`
- **스펙 문서**: `Docs/Specs/Character.md`
- **Screen 클래스**: `CharacterListScreen`

### 작업 내용
1. `CharacterList.jpg` 이미지 분석
2. `Character.md`에 `CharacterListScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader)
   - FilterArea (필터/정렬 옵션)
   - CharacterGrid (캐릭터 카드 그리드)
   - BottomAction (선택 액션 버튼)

### 추가 위치
`## 개요` 섹션 바로 아래, `## 참조` 섹션 위

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (CharacterDetailScreen 연결)

---

## Task 2: CharacterDetail

### 입력
- **레퍼런스**: `Docs/Design/Reference/CharacterDetail.jpg`
- **스펙 문서**: `Docs/Specs/Character.md`
- **Screen 클래스**: `CharacterDetailScreen`

### 작업 내용
1. `CharacterDetail.jpg` 이미지 분석
2. `Character.md`에 `CharacterDetailScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (BackButton + Title)
   - CharacterInfo (캐릭터 이미지 + 기본 정보)
   - StatArea (스탯 정보)
   - SkillArea (스킬 목록)
   - ActionButtons (레벨업, 승격 등)

### 추가 위치
`CharacterListScreen` UI 레이아웃 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (LevelUpPopup, AscensionPopup 연결)

---

## Task 3: Shop

### 입력
- **레퍼런스**: `Docs/Design/Reference/Shop.jpg`
- **스펙 문서**: `Docs/Specs/Shop.md`
- **Screen 클래스**: `ShopScreen`

### 작업 내용
1. `Shop.jpg` 이미지 분석
2. `Shop.md`에 `ShopScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader + 재화 표시)
   - TabArea (상점 탭: 일반, 이벤트, 패키지 등)
   - ProductGrid (상품 목록)
   - ProductDetail (선택된 상품 상세)

### 추가 위치
`## 개요` 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블 (탭별 구분)
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (CostConfirmPopup 연결)

---

## Task 4: Gacha

### 입력
- **레퍼런스**: `Docs/Design/Reference/Gacha.jpg`
- **스펙 문서**: `Docs/Specs/Gacha.md`
- **Screen 클래스**: `GachaScreen`

### 작업 내용
1. `Gacha.jpg` 이미지 분석
2. `Gacha.md`에 `GachaScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader + 재화 표시)
   - BannerArea (가챠 배너)
   - RateInfo (확률 정보 버튼)
   - PullButtons (1회/10회 뽑기 버튼)
   - HistoryButton (기록 보기)

### 추가 위치
`## 개요` 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (GachaResultPopup, RateDetailPopup, GachaHistoryScreen 연결)

---

## Task 5: LiveEvent

### 입력
- **레퍼런스**: `Docs/Design/Reference/LiveEvent.jpg`
- **스펙 문서**: `Docs/Specs/LiveEvent.md`
- **Screen 클래스**: `LiveEventScreen`

### 작업 내용
1. `LiveEvent.jpg` 이미지 분석
2. `LiveEvent.md`에 `LiveEventScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader)
   - EventBanner (이벤트 메인 배너)
   - TabArea (스토리, 미션, 상점 탭)
   - ContentArea (탭별 콘텐츠)
   - EventTimer (남은 시간)

### 추가 위치
`## 개요` 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블 (탭별 구분)
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (EventDetailScreen 연결)

---

## Task 6: StageSelect

### 입력
- **레퍼런스**: `Docs/Design/Reference/StageSelectScreen.jpg`
- **스펙 문서**: `Docs/Specs/Stage.md`
- **Screen 클래스**: `StageSelectScreen`

### 작업 내용
1. `StageSelectScreen.jpg` 이미지 분석
2. `Stage.md`에 `StageSelectScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader)
   - ChapterArea (챕터 선택)
   - StageList (스테이지 목록/맵)
   - StageInfo (선택된 스테이지 정보)
   - EnterButton (입장 버튼)

### 추가 위치
`## 개요` 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (StageInfoPopup, PartySelectScreen 연결)

---

## Task 7: PartySelect

### 입력
- **레퍼런스**: `Docs/Design/Reference/PartySelect.jpg`
- **스펙 문서**: `Docs/Specs/Stage.md`
- **Screen 클래스**: `PartySelectScreen`

### 작업 내용
1. `PartySelect.jpg` 이미지 분석
2. `Stage.md`에 `PartySelectScreen` UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (BackButton + 스테이지 정보)
   - PartySlots (편성 슬롯)
   - CharacterList (선택 가능한 캐릭터 목록)
   - StageInfo (적 정보, 권장 전투력)
   - StartButton (전투 시작)

### 추가 위치
`StageSelectScreen` UI 레이아웃 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (전투 시작 → Battle)

---

## Task 8: StageDashboard

### 입력
- **레퍼런스**: `Docs/Design/Reference/StageDashbaord.jpg`
- **스펙 문서**: `Docs/Specs/Stage.md`
- **Widget 클래스**: `InGameContentDashboard`

### 작업 내용
1. `StageDashbaord.jpg` 이미지 분석
2. `Stage.md`에 `InGameContentDashboard` UI 레이아웃 섹션 추가
3. 예상 영역:
   - CurrentProgress (현재 진행 스테이지)
   - QuickActions (빠른 액션 버튼)
   - RewardPreview (보상 미리보기)
   - EnterButton (입장/계속하기)

### 추가 위치
`PartySelectScreen` UI 레이아웃 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름 (StageSelectScreen 연결)

---

## Task 9: Inventory

### 입력
- **레퍼런스**: `Docs/Design/Reference/Inventory.jpg`
- **스펙 문서**: `Docs/Specs/Inventory.md`
- **Screen 클래스**: (TBD - 신규 또는 기존)

### 작업 내용
1. `Inventory.jpg` 이미지 분석
2. `Inventory.md`에 UI 레이아웃 섹션 추가
3. 예상 영역:
   - Header (ScreenHeader)
   - TabArea (아이템 종류별 탭)
   - ItemGrid (아이템 목록)
   - ItemDetail (선택된 아이템 상세)
   - ActionButtons (사용, 판매 등)

### 추가 위치
`## 개요` 섹션 바로 아래

### 완료 기준
- [ ] 전체 구조 ASCII 다이어그램
- [ ] 영역별 상세 테이블
- [ ] Prefab 계층 구조
- [ ] 컴포넌트 매핑 테이블
- [ ] 네비게이션 흐름

---

## 작업 할당 요약

| Task | Screen | 스펙 문서 | 우선순위 |
|------|--------|-----------|----------|
| 1 | CharacterListScreen | Character.md | HIGH |
| 2 | CharacterDetailScreen | Character.md | HIGH |
| 3 | ShopScreen | Shop.md | HIGH |
| 4 | GachaScreen | Gacha.md | HIGH |
| 5 | LiveEventScreen | LiveEvent.md | MEDIUM |
| 6 | StageSelectScreen | Stage.md | MEDIUM |
| 7 | PartySelectScreen | Stage.md | MEDIUM |
| 8 | InGameContentDashboard | Stage.md | LOW |
| 9 | Inventory | Inventory.md | LOW |

---

## 에이전트 프롬프트 템플릿

```
## 작업: {ScreenName} UI 문서화

### 지시사항
1. 먼저 `Docs/Design/UI_DOCUMENTATION_GUIDE.md`를 읽어 작업 가이드를 확인하세요.
2. `Docs/Specs/Lobby.md`의 "UI 레이아웃 구조" 섹션을 참조하여 완료 예시를 확인하세요.
3. `Docs/Design/Reference/{ScreenName}.jpg` 이미지를 분석하세요.
4. `Docs/Specs/{Assembly}.md` 문서를 읽고 적절한 위치에 UI 레이아웃 섹션을 추가하세요.

### 필수 섹션
- 레퍼런스 경로
- 전체 구조 (ASCII 다이어그램)
- 영역별 상세 (테이블)
- Prefab 계층 구조
- 컴포넌트 매핑 (SerializeField)
- 네비게이션 흐름

### 완료 후
작업 완료 보고를 다음 형식으로 작성하세요:
- 수행 내용
- 식별된 영역 및 요소 수
- 네비게이션 흐름
- 참고 파일 경로
```
