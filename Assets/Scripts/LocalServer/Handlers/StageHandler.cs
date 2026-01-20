using System;
using System.Collections.Generic;
using Sc.Data;

namespace Sc.LocalServer
{
    /// <summary>
    /// 스테이지 관련 요청 처리 핸들러
    /// </summary>
    public class StageHandler
    {
        private readonly ServerValidator _validator;
        private readonly RewardService _rewardService;
        private readonly ServerTimeService _timeService;
        private readonly StageEntryValidator _entryValidator;

        // 에러 코드 (Stage.md 스펙 기준)
        private const int ERROR_STAGE_NOT_FOUND = 5101;
        private const int ERROR_STAGE_LOCKED = 5102;
        private const int ERROR_INSUFFICIENT_COST = 5103;
        private const int ERROR_ENTRY_LIMIT_EXCEEDED = 5104;
        private const int ERROR_INVALID_PARTY = 5105;
        private const int ERROR_NOT_AVAILABLE_TODAY = 5106;
        private const int ERROR_INVALID_BATTLE_SESSION = 5107;
        private const int ERROR_SERVER = 9999;

        // 임시: StageData 조회용 (실제로는 Database 주입 필요)
        private Func<string, StageDataInfo> _stageDataProvider;

        public StageHandler(
            ServerValidator validator,
            RewardService rewardService,
            ServerTimeService timeService)
        {
            _validator = validator;
            _rewardService = rewardService;
            _timeService = timeService;
            _entryValidator = new StageEntryValidator(timeService);
        }

        /// <summary>
        /// StageData 조회 함수 설정 (외부에서 주입)
        /// </summary>
        public void SetStageDataProvider(Func<string, StageDataInfo> provider)
        {
            _stageDataProvider = provider;
        }

        /// <summary>
        /// 스테이지 입장 요청 처리
        /// </summary>
        public EnterStageResponse HandleEnterStage(EnterStageRequest request, ref UserSaveData userData)
        {
            // 0. StageData 조회 함수 확인
            if (_stageDataProvider == null)
            {
                return EnterStageResponse.Fail(ERROR_SERVER, "StageDataProvider가 초기화되지 않았습니다.");
            }

            // 1. 스테이지 조회
            var stageInfo = _stageDataProvider(request.StageId);
            if (stageInfo == null)
            {
                return EnterStageResponse.Fail(ERROR_STAGE_NOT_FOUND, "존재하지 않는 스테이지입니다.");
            }

            // 2. 스테이지 활성화 여부 확인
            if (!stageInfo.IsEnabled)
            {
                return EnterStageResponse.Fail(ERROR_STAGE_LOCKED, "비활성화된 스테이지입니다.");
            }

            // 3. 해금 조건 확인
            if (!string.IsNullOrEmpty(stageInfo.UnlockConditionStageId))
            {
                var prevClearInfo = userData.StageProgress.FindClearInfo(stageInfo.UnlockConditionStageId);
                if (prevClearInfo == null || !prevClearInfo.Value.IsCleared)
                {
                    return EnterStageResponse.Fail(ERROR_STAGE_LOCKED, "선행 스테이지를 클리어해야 합니다.");
                }
            }

            // 4. 요일 제한 확인
            if (stageInfo.AvailableDays != null && stageInfo.AvailableDays.Length > 0)
            {
                if (!_entryValidator.IsAvailableToday(stageInfo.AvailableDays))
                {
                    return EnterStageResponse.Fail(ERROR_NOT_AVAILABLE_TODAY, "오늘은 입장할 수 없는 스테이지입니다.");
                }
            }

            // 5. 입장 제한 검증
            var existingRecord = userData.FindStageEntryRecord(request.StageId);
            if (!_entryValidator.CanEnter(stageInfo.LimitType, stageInfo.LimitCount, existingRecord,
                    out var remainingCount))
            {
                return EnterStageResponse.Fail(ERROR_ENTRY_LIMIT_EXCEEDED, $"입장 제한에 도달했습니다. (남은 횟수: {remainingCount})");
            }

            // 6. 재화 검증
            if (!HasEnoughCurrency(ref userData, stageInfo.EntryCostType, stageInfo.EntryCost))
            {
                return EnterStageResponse.Fail(ERROR_INSUFFICIENT_COST, "입장 재화가 부족합니다.");
            }

            // 7. 파티 검증 (최소 1명)
            if (request.PartyCharacterIds == null || request.PartyCharacterIds.Count == 0)
            {
                return EnterStageResponse.Fail(ERROR_INVALID_PARTY, "파티에 최소 1명의 캐릭터가 필요합니다.");
            }

            // 8. 재화 차감
            DeductCurrency(ref userData, stageInfo.EntryCostType, stageInfo.EntryCost);

            // 9. 입장 기록 업데이트
            var updatedRecord = _entryValidator.UpdateEntryRecord(
                request.StageId,
                stageInfo.LimitType,
                existingRecord);
            userData.UpdateStageEntryRecord(request.StageId, updatedRecord);

            // 10. 전투 세션 생성
            var battleSession = BattleSessionData.Create(
                request.StageId,
                request.PartyCharacterIds,
                _timeService.ServerTimeUtc);
            userData.CreateBattleSession(battleSession);

            // 11. Delta 생성 (재화 변경)
            var delta = UserDataDelta.WithCurrency(userData.Currency);

            return EnterStageResponse.Success(battleSession.SessionId, updatedRecord, delta);
        }

        /// <summary>
        /// 스테이지 클리어 요청 처리
        /// </summary>
        public ClearStageResponse HandleClearStage(ClearStageRequest request, ref UserSaveData userData)
        {
            // 0. StageData 조회 함수 확인
            if (_stageDataProvider == null)
            {
                return ClearStageResponse.Fail(ERROR_SERVER, "StageDataProvider가 초기화되지 않았습니다.");
            }

            // 1. 전투 세션 조회
            var session = userData.FindBattleSession(request.BattleSessionId);
            if (session == null)
            {
                return ClearStageResponse.Fail(ERROR_INVALID_BATTLE_SESSION, "존재하지 않는 전투 세션입니다.");
            }

            if (!session.Value.IsActive)
            {
                return ClearStageResponse.Fail(ERROR_INVALID_BATTLE_SESSION, "이미 종료된 전투 세션입니다.");
            }

            if (session.Value.IsExpired(_timeService.ServerTimeUtc))
            {
                return ClearStageResponse.Fail(ERROR_INVALID_BATTLE_SESSION, "만료된 전투 세션입니다.");
            }

            var stageId = session.Value.StageId;

            // 2. 스테이지 조회
            var stageInfo = _stageDataProvider(stageId);
            if (stageInfo == null)
            {
                return ClearStageResponse.Fail(ERROR_STAGE_NOT_FOUND, "스테이지 정보를 찾을 수 없습니다.");
            }

            // 3. 세션 비활성화
            userData.DeactivateBattleSession(request.BattleSessionId);

            // 4. 패배 시 빈 응답
            if (!request.IsVictory)
            {
                var emptyClearInfo = StageClearInfo.CreateDefault(stageId);
                return ClearStageResponse.Success(
                    emptyClearInfo,
                    new bool[3],
                    false,
                    new List<RewardInfo>(),
                    UserDataDelta.Empty());
            }

            // 5. 별 조건 평가
            var newStarsAchieved = EvaluateStarConditions(stageInfo, request);

            // 6. 기존 클리어 정보 조회
            var existingClearInfo = userData.StageProgress.FindClearInfo(stageId);
            var isFirstClear = existingClearInfo == null || !existingClearInfo.Value.IsCleared;

            // 7. 클리어 정보 업데이트
            var baseClearInfo = existingClearInfo ?? StageClearInfo.CreateDefault(stageId);
            var updatedClearInfo = baseClearInfo.UpdateWithClear(
                newStarsAchieved,
                request.TurnCount,
                _timeService.ServerTimeUtc);
            userData.StageProgress.UpdateClearInfo(stageId, updatedClearInfo);

            // 8. 보상 지급
            var rewardsToGive = new List<RewardInfo>();
            if (isFirstClear && stageInfo.FirstClearRewards != null)
            {
                rewardsToGive.AddRange(stageInfo.FirstClearRewards);
            }

            if (stageInfo.RepeatClearRewards != null)
            {
                rewardsToGive.AddRange(stageInfo.RepeatClearRewards);
            }

            var delta = _rewardService.CreateRewardDelta(rewardsToGive.ToArray(), ref userData);

            return ClearStageResponse.Success(
                updatedClearInfo,
                newStarsAchieved,
                isFirstClear,
                rewardsToGive,
                delta);
        }

        /// <summary>
        /// 별 조건 평가
        /// </summary>
        private bool[] EvaluateStarConditions(StageDataInfo stageInfo, ClearStageRequest request)
        {
            var stars = new bool[3];

            if (stageInfo.StarConditions == null || stageInfo.StarConditions.Length == 0)
            {
                // 기본: 클리어 시 1스타
                stars[0] = true;
                return stars;
            }

            for (int i = 0; i < Math.Min(3, stageInfo.StarConditions.Length); i++)
            {
                var condition = stageInfo.StarConditions[i];
                stars[i] = EvaluateSingleCondition(condition, request);
            }

            return stars;
        }

        /// <summary>
        /// 개별 별 조건 평가
        /// </summary>
        private bool EvaluateSingleCondition(StarConditionInfo condition, ClearStageRequest request)
        {
            return condition.Type switch
            {
                StarConditionType.Clear => request.IsVictory,
                StarConditionType.TurnLimit => request.IsVictory && request.TurnCount <= condition.Value,
                StarConditionType.NoCharacterDeath => request.IsVictory && request.NoCharacterDeath,
                StarConditionType.FullHP => request.IsVictory && request.AllFullHP,
                StarConditionType.ElementAdvantage => request.IsVictory, // 별도 검증 필요
                _ => false
            };
        }

        /// <summary>
        /// 재화 충분 여부 확인
        /// </summary>
        private bool HasEnoughCurrency(ref UserSaveData userData, CostType costType, int amount)
        {
            if (amount <= 0) return true;

            return costType switch
            {
                CostType.None => true,
                CostType.Gold => _validator.HasEnoughGold(userData.Currency, amount),
                CostType.Gem => _validator.HasEnoughGem(userData.Currency, amount),
                CostType.Stamina => _validator.HasEnoughStamina(userData.Currency, amount),
                _ => false
            };
        }

        /// <summary>
        /// 재화 차감
        /// </summary>
        private void DeductCurrency(ref UserSaveData userData, CostType costType, int amount)
        {
            if (amount <= 0) return;

            switch (costType)
            {
                case CostType.None:
                    break;
                case CostType.Gold:
                    _rewardService.DeductGold(ref userData.Currency, amount);
                    break;
                case CostType.Gem:
                    _rewardService.DeductGem(ref userData.Currency, amount);
                    break;
                case CostType.Stamina:
                    userData.Currency.Stamina -= amount;
                    break;
            }
        }
    }

    /// <summary>
    /// StageData 정보 전달용 구조체 (ScriptableObject 직접 참조 방지)
    /// </summary>
    public class StageDataInfo
    {
        public string Id;
        public bool IsEnabled;
        public string UnlockConditionStageId;
        public DayOfWeek[] AvailableDays;
        public LimitType LimitType;
        public int LimitCount;
        public CostType EntryCostType;
        public int EntryCost;
        public StarConditionInfo[] StarConditions;
        public List<RewardInfo> FirstClearRewards;
        public List<RewardInfo> RepeatClearRewards;
    }

    /// <summary>
    /// StarCondition 정보 전달용 구조체
    /// </summary>
    public class StarConditionInfo
    {
        public StarConditionType Type;
        public int Value;
    }
}