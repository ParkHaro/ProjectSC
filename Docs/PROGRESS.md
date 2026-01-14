# 진행 상황

## 상태 범례
- ⬜ 대기 | 🔨 진행 중 | ✅ 완료

---

## 현재 Phase: 1 - 기반 레이어 구현

### Phase 0 - 프로젝트 구조 설정 ✅

### 기반 레이어

| Assembly | 구조 | 구현 | 설명 |
|----------|------|------|------|
| Sc.Data | ✅ | ⬜ | 순수 데이터 정의 |
| Sc.Event | ✅ | ⬜ | 클라이언트 내부 이벤트 |
| Sc.Packet | ✅ | ⬜ | 서버 통신 인터페이스 |
| Sc.Core | ✅ | ⬜ | 핵심 시스템 |
| Sc.Common | ✅ | ⬜ | 공통 모듈 |

### Contents - Shared

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Character | ✅ | ⬜ |
| Sc.Contents.Inventory | ✅ | ⬜ |

### Contents - InGame

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Battle | ✅ | ⬜ |
| Sc.Contents.Skill | ✅ | ⬜ |

### Contents - OutGame

| Assembly | 구조 | 구현 |
|----------|------|------|
| Sc.Contents.Lobby | ✅ | ⬜ |
| Sc.Contents.Gacha | ✅ | ⬜ |
| Sc.Contents.Shop | ✅ | ⬜ |
| Sc.Contents.Quest | ✅ | ⬜ |

---

## 작업 로그

### 2025-01-14
- [x] 프로젝트 문서 구조 설정
- [x] CLAUDE.md 생성
- [x] Assembly 기반 아키텍처 설계
- [x] 스펙 문서 작성 완료
- [x] 폴더 구조 생성
- [x] Assembly Definition 파일 생성 (13개)
- [x] Event/Packet 분리 (클라이언트 이벤트 vs 서버 통신)
- [x] Sc.Event Assembly 추가
- [x] Sc.Packet 서버 통신 인터페이스 재설계
