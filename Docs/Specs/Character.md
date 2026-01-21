# Sc.Contents.Character

## 개요
캐릭터 데이터, 스탯, 생성 시스템 (Shared)

## 참조
- Sc.Common

## 패턴
- **Factory**: 캐릭터 인스턴스 생성
- **Flyweight**: 공유 데이터 최적화

---

## 구조

```
Contents/Shared/Character/
├── Logic/
│   ├── CharacterManager.cs
│   ├── CharacterStats.cs
│   └── Factory/
│       ├── ICharacterFactory.cs
│       └── CharacterFactory.cs
└── UI/
    ├── CharacterListView.cs
    └── CharacterDetailView.cs
```

---

## 클래스 목록

### Logic

| 클래스 | 설명 | 상태 |
|--------|------|------|
| CharacterManager | 캐릭터 컬렉션 관리 | ⬜ |
| CharacterStats | 런타임 스탯 (Flyweight-extrinsic) | ⬜ |
| ICharacterFactory | 팩토리 인터페이스 | ⬜ |
| CharacterFactory | 캐릭터 생성 구현 | ⬜ |

### UI

| 클래스 | 설명 | 상태 |
|--------|------|------|
| CharacterListView | 캐릭터 목록 화면 | ⬜ |
| CharacterDetailView | 캐릭터 상세 화면 | ⬜ |

---

## 상세 문서

- [Enhancement.md](Character/Enhancement.md) - 캐릭터 강화 시스템 (레벨업, 돌파) ✅
