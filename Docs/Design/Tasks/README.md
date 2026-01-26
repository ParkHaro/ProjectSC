# SCREEN-PREFAB 작업 지시서

> 각 터미널에서 독립적으로 실행 가능한 작업 단위

---

## 실행 방법

각 터미널에서 Claude Code 실행 후 해당 작업 지시서 경로 전달:

```bash
# Terminal A
claude "Docs/Design/Tasks/ROUND1_AGENT_A.md 작업 진행해줘"

# Terminal B
claude "Docs/Design/Tasks/ROUND1_AGENT_B.md 작업 진행해줘"

# Terminal C
claude "Docs/Design/Tasks/ROUND1_AGENT_C.md 작업 진행해줘"
```

---

## Round 1 (동시 실행)

| Agent | Screen | 파일 | 난이도 |
|-------|--------|------|--------|
| A | CharacterListScreen | [ROUND1_AGENT_A.md](ROUND1_AGENT_A.md) | 중 |
| B | ShopScreen | [ROUND1_AGENT_B.md](ROUND1_AGENT_B.md) | 중 |
| C | StageSelectScreen | [ROUND1_AGENT_C.md](ROUND1_AGENT_C.md) | 상 |

**완료 조건**: 3개 모두 빌드 성공

---

## Round 2 (동시 실행)

| Agent | Screen | 파일 | 난이도 |
|-------|--------|------|--------|
| A | CharacterDetailScreen | [ROUND2_AGENT_A.md](ROUND2_AGENT_A.md) | 상 |
| B | GachaScreen | [ROUND2_AGENT_B.md](ROUND2_AGENT_B.md) | 중 |
| C | PartySelectScreen | [ROUND2_AGENT_C.md](ROUND2_AGENT_C.md) | 상 |

**선행 조건**: Round 1 완료
**완료 조건**: 3개 모두 빌드 성공

---

## Round 3 (동시 실행)

| Agent | Screen | 파일 | 난이도 |
|-------|--------|------|--------|
| A | LiveEventScreen | [ROUND3_AGENT_A.md](ROUND3_AGENT_A.md) | 중 |
| B | InGameContentDashboard | [ROUND3_AGENT_B.md](ROUND3_AGENT_B.md) | 하 |
| C | InventoryScreen (신규) | [ROUND3_AGENT_C.md](ROUND3_AGENT_C.md) | 중 |

**선행 조건**: Round 2 완료
**완료 조건**: 3개 모두 빌드 성공, InventoryScreen Addressables 등록

---

## 진행 상태

### Round 1
- [ ] Agent A: CharacterListScreen
- [ ] Agent B: ShopScreen
- [ ] Agent C: StageSelectScreen

### Round 2
- [ ] Agent A: CharacterDetailScreen
- [ ] Agent B: GachaScreen
- [ ] Agent C: PartySelectScreen

### Round 3
- [ ] Agent A: LiveEventScreen
- [ ] Agent B: InGameContentDashboard
- [ ] Agent C: InventoryScreen

---

## 파일 충돌 방지 규칙

각 Round 내 Agent들은 서로 다른 도메인/파일을 수정:

| Round | Agent A | Agent B | Agent C |
|-------|---------|---------|---------|
| 1 | Character/* | Shop/* | Stage/* |
| 2 | Character/* | Gacha/* | Stage/* |
| 3 | LiveEvent/* | Stage/* | Inventory/* |

**주의**: Round 2의 Agent A와 C, Round 3의 Agent B는 이전 Round와 같은 도메인 수정
→ 반드시 이전 Round 완료 후 진행

---

## 완료 보고 수집

각 Agent 완료 후 PROGRESS.md 업데이트:

```markdown
### Phase 2: Screen별 PrefabBuilder 구현

| Reference | Screen | 상태 |
|-----------|--------|------|
| CharacterList.jpg | CharacterListScreen | ✅ |
| ... | ... | ... |
```
