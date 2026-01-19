# ProjectSC 프로젝트 개요

## 목적
서브컬쳐 수집형 RPG 포트폴리오 프로젝트

## 기술 스택
- **엔진**: Unity 2022.x
- **언어**: C#
- **비동기**: UniTask
- **리소스 관리**: Addressables
- **트윈**: DOTween

## 아키텍처 설계 원칙
1. **Assembly 분리**: 컴파일 시간 단축 및 의존성 명확화
2. **InGame/OutGame 분리**: 게임 영역별 모듈 독립
3. **이벤트 기반 통신**: 컨텐츠 간 직접 참조 최소화
4. **MVP 패턴**: UI와 로직 분리
5. **서비스 추상화**: 외부 데이터 소스는 인터페이스로 추상화

## 핵심 Assembly 구조
- `Sc.Foundation` - 최하위 레이어 (Singleton, EventManager)
- `Sc.Data` - 순수 데이터 정의 (의존성 없음)
- `Sc.Event` - 클라이언트 내부 이벤트
- `Sc.Packet` - 서버 통신 인터페이스
- `Sc.Core` - 핵심 시스템 (DataManager)
- `Sc.Common` - 공통 모듈 (UI 시스템)
- `Sc.Contents.*` - 컨텐츠 모듈들

## 데이터 아키텍처 (v2.0)
- **설계**: 서버 중심 (Server Authority)
- **구현**: 로컬 더미 데이터 (서버 응답 시뮬레이션)
- **마스터 데이터**: Excel → JSON → ScriptableObject 파이프라인
- **유저 데이터**: 서버 응답 캐시 (읽기 전용)

## 현재 상태
- MVP 화면 구현 완료 (Title, Lobby, Gacha, CharacterList, CharacterDetail)
- 기반 인프라 구현 완료 (Logging, ErrorHandling, SaveManager, LoadingIndicator)
- 아웃게임 아키텍처 V1 마일스톤 진행 중
