# Architecture Decisions

프로젝트 아키텍처 관련 의사결정 기록.

---

## Assembly Definition 모듈화 전략

**일자**: 2024-XX-XX | **상태**: 결정됨

### 컨텍스트
프로젝트 규모 성장에 따른 컴파일 시간 증가 우려 및 의존성 관리 필요.

### 선택지
1. **Assembly 없이** - 단순하나 컴파일 시간 증가, 순환 참조 위험
2. **기능별 분리** - 컴파일 최적화, 의존성 명시 (선택)
3. **Layer별 분리** - 아키텍처 강제되나 수정 범위 큼

### 결정
**기능별 Assembly 분리** (`Sc.` 접두사)
- 독립적 컴파일/테스트, 순환 참조 컴파일 타임 방지

### 결과
Core, UI, Contents 등 명확한 모듈 경계. Editor 코드 완전 분리.

---

## 이벤트 기반 컨텐츠 통신

**일자**: 2024-XX-XX | **상태**: 결정됨

### 컨텍스트
컨텐츠(캐릭터, 인벤토리, 퀘스트 등) 간 통신 방식 필요. 직접 참조 시 강한 결합.

### 선택지
1. **직접 참조** - 단순하나 강한 결합, 순환 참조 위험
2. **이벤트 버스** - 느슨한 결합, 확장 용이 (선택)
3. **메시지 브로커** - 중앙 관리되나 복잡도 증가

### 결정
**타입 안전 이벤트 버스**
- 컨텐츠 간 의존성 제거, 제네릭 기반 타입 안전성

### 결과
Contents 간 Assembly 참조 없음. 기능 추가/제거 영향 최소화.

---

## LocalServer 서버 로직 분리

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
LocalApiClient 354줄 비대화 (저장/로드 + 로그인 + 가챠 + 상점 혼재). 서버 교체 시 영향 범위 불명확.

### 선택지
1. **현행 유지** - 파일 하나에 모든 것
2. **partial class** - 최소 변경이나 의존성 관리 불가
3. **Sc.LocalServer Assembly** - 명확한 책임 분리 (선택)

### 결정
**Sc.LocalServer Assembly 분리**
```
Sc.LocalServer/
├── LocalGameServer.cs      # 요청 라우팅
├── Handlers/               # Login, Gacha, Shop
├── Validators/             # ServerValidator
└── Services/               # Time, Gacha, Reward
```

### 결과
- LocalApiClient: 354줄 → 157줄 (56% 감소)
- 서버 교체 시 Assembly만 교체
- Handler 패턴으로 확장 용이

---

## AssetManager 아키텍처

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
개별 Addressables 사용 중복 → 중앙 집중 에셋 관리 필요.

### 선택지
1. **Monolithic** - 단일 클래스, 테스트 어려움
2. **Full Abstraction** - 완전 분리, 오버엔지니어링
3. **Pragmatic Balance** - 적절한 분리 (선택)

### 결정
**실용적 균형**
```
AssetManager (Singleton)
├── AssetHandle<T>       # 레퍼런스 카운팅
├── AssetScope           # 영역별 그룹
├── AssetCacheManager    # LRU 캐시
└── AssetLoader          # Addressables+Resources
```

### 결과
DataManager 구조와 유사하게 설계. IAssetHandle 인터페이스로 Reflection 제거.

---

## 에디터 도구 Bootstrap 레벨

**일자**: 2026-01-19 | **상태**: 결정됨

### 컨텍스트
여러 에디터 도구가 각각 다른 수준의 씬 오브젝트 생성. 역할 불명확, 중복 코드.

### 결정
**Bootstrap 레벨 체계화**

| 레벨 | 설명 | 도구 |
|------|------|------|
| None | 프리팹만 생성 | PlayModeTestSetup, SystemPopupSetup |
| Partial | EventSystem + 일부 매니저 | UITestSceneSetup, LoadingSetup |
| Full | 모든 매니저 | MVPSceneSetup |

### 결과
EditorUIHelpers.cs로 공용 코드 중앙화. 각 도구 역할 명확화.
