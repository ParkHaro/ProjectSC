using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;
using Sc.LocalServer;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// StageHandler 단위 테스트.
    /// 스테이지 입장/클리어 로직 검증.
    /// </summary>
    [TestFixture]
    public class StageHandlerTests
    {
        private StageHandler _handler;
        private ServerTimeService _timeService;
        private UserSaveData _testUserData;

        // 테스트용 스테이지 데이터
        private StageDataInfo _normalStage;
        private StageDataInfo _limitedStage;
        private StageDataInfo _lockedStage;
        private StageDataInfo _dayRestrictedStage;

        // 에러 코드 (Stage.md 스펙 기준)
        private const int ERROR_STAGE_NOT_FOUND = 5101;
        private const int ERROR_STAGE_LOCKED = 5102;
        private const int ERROR_INSUFFICIENT_COST = 5103;
        private const int ERROR_ENTRY_LIMIT_EXCEEDED = 5104;
        private const int ERROR_INVALID_PARTY = 5105;
        private const int ERROR_NOT_AVAILABLE_TODAY = 5106;
        private const int ERROR_INVALID_BATTLE_SESSION = 5107;
        private const int ERROR_SERVER = 9999;

        [SetUp]
        public void SetUp()
        {
            _timeService = new ServerTimeService();
            var validator = new ServerValidator(_timeService);
            var rewardService = new RewardService();

            _handler = new StageHandler(validator, rewardService, _timeService);

            // 테스트용 스테이지 데이터 생성
            _normalStage = CreateStageInfo(
                id: "stage_1_1",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                limitType: LimitType.None,
                limitCount: 0);

            _limitedStage = CreateStageInfo(
                id: "stage_limited",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 20,
                limitType: LimitType.Daily,
                limitCount: 3);

            _lockedStage = CreateStageInfo(
                id: "stage_locked",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                unlockConditionStageId: "stage_1_1");

            _dayRestrictedStage = CreateStageInfo(
                id: "stage_day_restricted",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                availableDays: GetOtherDays(GetToday())); // 오늘이 아닌 요일만

            // StageDataProvider 설정
            _handler.SetStageDataProvider(GetStageData);

            // 테스트 유저 데이터
            _testUserData = UserSaveData.CreateNew("test_uid", "TestUser");
            _testUserData.Currency = new UserCurrency
            {
                Gold = 10000,
                Gem = 1000,
                FreeGem = 500,
                Stamina = 100
            };
        }

        #region EnterStage Success Tests

        [Test]
        public void HandleEnterStage_ReturnsSuccess_WhenValid()
        {
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.BattleSessionId, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void HandleEnterStage_DeductsStamina_WhenValid()
        {
            var initialStamina = _testUserData.Currency.Stamina;
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });

            _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(_testUserData.Currency.Stamina, Is.EqualTo(initialStamina - _normalStage.EntryCost));
        }

        [Test]
        public void HandleEnterStage_CreatesBattleSession_WhenValid()
        {
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1", "char_2" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            var session = _testUserData.FindBattleSession(response.BattleSessionId);
            Assert.That(session, Is.Not.Null);
            Assert.That(session.Value.IsActive, Is.True);
            Assert.That(session.Value.StageId, Is.EqualTo(_normalStage.Id));
        }

        [Test]
        public void HandleEnterStage_UpdatesEntryRecord_WhenLimited()
        {
            var request = EnterStageRequest.Create(_limitedStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.EntryRecord.StageId, Is.EqualTo(_limitedStage.Id));
            Assert.That(response.EntryRecord.EntryCount, Is.EqualTo(1));
        }

        [Test]
        public void HandleEnterStage_AllowsUnlockedStage_WhenPrerequisiteCleared()
        {
            // 선행 스테이지 클리어 처리
            _testUserData.StageProgress.UpdateClearInfo(
                _normalStage.Id,
                StageClearInfo.CreateDefault(_normalStage.Id).UpdateWithClear(
                    new[] { true, false, false },
                    10,
                    _timeService.ServerTimeUtc));

            var request = EnterStageRequest.Create(_lockedStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.True);
        }

        #endregion

        #region EnterStage Error Tests

        [Test]
        public void HandleEnterStage_ReturnsError_WhenStageNotFound()
        {
            var request = EnterStageRequest.Create("non_existent_stage", new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_STAGE_NOT_FOUND));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenStageDisabled()
        {
            var disabledStage = CreateStageInfo("stage_disabled", isEnabled: false);
            _handler.SetStageDataProvider(id => id == "stage_disabled" ? disabledStage : GetStageData(id));

            var request = EnterStageRequest.Create("stage_disabled", new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_STAGE_LOCKED));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenPrerequisiteNotCleared()
        {
            var request = EnterStageRequest.Create(_lockedStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_STAGE_LOCKED));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenInsufficientStamina()
        {
            _testUserData.Currency.Stamina = 5; // 부족한 스태미나
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_INSUFFICIENT_COST));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenEmptyParty()
        {
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string>());

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_INVALID_PARTY));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenNullParty()
        {
            var request = EnterStageRequest.Create(_normalStage.Id, null);

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_INVALID_PARTY));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenEntryLimitExceeded()
        {
            // 3회 입장 (제한 도달)
            for (int i = 0; i < 3; i++)
            {
                var req = EnterStageRequest.Create(_limitedStage.Id, new List<string> { "char_1" });
                _handler.HandleEnterStage(req, ref _testUserData);
            }

            // 4번째 입장 시도
            var request = EnterStageRequest.Create(_limitedStage.Id, new List<string> { "char_1" });
            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_ENTRY_LIMIT_EXCEEDED));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenNotAvailableToday()
        {
            var request = EnterStageRequest.Create(_dayRestrictedStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_NOT_AVAILABLE_TODAY));
        }

        [Test]
        public void HandleEnterStage_ReturnsError_WhenProviderNotSet()
        {
            _handler.SetStageDataProvider(null);
            var request = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });

            var response = _handler.HandleEnterStage(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(ERROR_SERVER));
        }

        #endregion

        #region ClearStage Success Tests

        [Test]
        public void HandleClearStage_ReturnsSuccess_WhenVictory()
        {
            // 먼저 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어 요청
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.IsSuccess, Is.True);
            Assert.That(clearResponse.ClearInfo.IsCleared, Is.True);
        }

        [Test]
        public void HandleClearStage_UpdatesClearInfo_WhenFirstClear()
        {
            // 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: false);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.IsFirstClear, Is.True);
            Assert.That(clearResponse.ClearInfo.ClearCount, Is.EqualTo(1));
        }

        [Test]
        public void HandleClearStage_EvaluatesStarConditions()
        {
            // 별 조건이 있는 스테이지 생성
            var stageWithStars = CreateStageInfo(
                id: "stage_stars",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                starConditions: new[]
                {
                    new StarConditionInfo { Type = StarConditionType.Clear, Value = 0 },
                    new StarConditionInfo { Type = StarConditionType.TurnLimit, Value = 10 },
                    new StarConditionInfo { Type = StarConditionType.NoCharacterDeath, Value = 0 }
                });

            _handler.SetStageDataProvider(id => id == "stage_stars" ? stageWithStars : GetStageData(id));

            // 입장
            var enterRequest = EnterStageRequest.Create("stage_stars", new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어 (5턴, 사망 없음)
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: false);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.NewStarsAchieved[0], Is.True); // Clear
            Assert.That(clearResponse.NewStarsAchieved[1], Is.True); // TurnLimit (5 <= 10)
            Assert.That(clearResponse.NewStarsAchieved[2], Is.True); // NoCharacterDeath
        }

        [Test]
        public void HandleClearStage_GivesFirstClearRewards()
        {
            var stageWithRewards = CreateStageInfo(
                id: "stage_rewards",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                firstClearRewards: new List<RewardInfo>
                {
                    RewardInfo.Currency(CostType.Gem, 100)
                },
                repeatClearRewards: new List<RewardInfo>
                {
                    RewardInfo.Currency(CostType.Gold, 500)
                });

            _handler.SetStageDataProvider(id => id == "stage_rewards" ? stageWithRewards : GetStageData(id));

            // 입장
            var enterRequest = EnterStageRequest.Create("stage_rewards", new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 첫 클리어
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.TotalRewards.Count, Is.EqualTo(2)); // 첫 클리어 + 반복 클리어 보상
        }

        [Test]
        public void HandleClearStage_DeactivatesSession_WhenVictory()
        {
            // 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);

            _handler.HandleClearStage(clearRequest, ref _testUserData);

            var session = _testUserData.FindBattleSession(enterResponse.BattleSessionId);
            Assert.That(session.Value.IsActive, Is.False);
        }

        [Test]
        public void HandleClearStage_ReturnsSuccess_WhenDefeat()
        {
            // 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 패배
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: false,
                turnCount: 10,
                noCharacterDeath: false,
                allFullHP: false);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.IsSuccess, Is.True);
            Assert.That(clearResponse.ClearInfo.IsCleared, Is.False);
            Assert.That(clearResponse.TotalRewards.Count, Is.EqualTo(0));
        }

        #endregion

        #region ClearStage Error Tests

        [Test]
        public void HandleClearStage_ReturnsError_WhenSessionNotFound()
        {
            var clearRequest = ClearStageRequest.Create(
                "non_existent_session",
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.IsSuccess, Is.False);
            Assert.That(clearResponse.ErrorCode, Is.EqualTo(ERROR_INVALID_BATTLE_SESSION));
        }

        [Test]
        public void HandleClearStage_ReturnsError_WhenSessionAlreadyEnded()
        {
            // 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 첫 번째 클리어
            var clearRequest1 = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);
            _handler.HandleClearStage(clearRequest1, ref _testUserData);

            // 두 번째 클리어 시도 (이미 종료된 세션)
            var clearRequest2 = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);
            var clearResponse = _handler.HandleClearStage(clearRequest2, ref _testUserData);

            Assert.That(clearResponse.IsSuccess, Is.False);
            Assert.That(clearResponse.ErrorCode, Is.EqualTo(ERROR_INVALID_BATTLE_SESSION));
        }

        [Test]
        public void HandleClearStage_ReturnsError_WhenProviderNotSet()
        {
            // 입장
            var enterRequest = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // Provider 제거
            _handler.SetStageDataProvider(null);

            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: true,
                allFullHP: true);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.IsSuccess, Is.False);
            Assert.That(clearResponse.ErrorCode, Is.EqualTo(ERROR_SERVER));
        }

        #endregion

        #region Star Conditions Tests

        [Test]
        public void HandleClearStage_FailsTurnLimitCondition_WhenExceedsTurns()
        {
            var stageWithStars = CreateStageInfo(
                id: "stage_turn_limit",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                starConditions: new[]
                {
                    new StarConditionInfo { Type = StarConditionType.Clear, Value = 0 },
                    new StarConditionInfo { Type = StarConditionType.TurnLimit, Value = 5 },
                    new StarConditionInfo { Type = StarConditionType.NoCharacterDeath, Value = 0 }
                });

            _handler.SetStageDataProvider(id => id == "stage_turn_limit" ? stageWithStars : GetStageData(id));

            // 입장
            var enterRequest = EnterStageRequest.Create("stage_turn_limit", new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어 (10턴 - 제한 초과)
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 10,
                noCharacterDeath: true,
                allFullHP: false);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.NewStarsAchieved[0], Is.True); // Clear
            Assert.That(clearResponse.NewStarsAchieved[1], Is.False); // TurnLimit (10 > 5)
            Assert.That(clearResponse.NewStarsAchieved[2], Is.True); // NoCharacterDeath
        }

        [Test]
        public void HandleClearStage_FailsNoDeathCondition_WhenCharacterDied()
        {
            var stageWithStars = CreateStageInfo(
                id: "stage_no_death",
                isEnabled: true,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                starConditions: new[]
                {
                    new StarConditionInfo { Type = StarConditionType.Clear, Value = 0 },
                    new StarConditionInfo { Type = StarConditionType.NoCharacterDeath, Value = 0 }
                });

            _handler.SetStageDataProvider(id => id == "stage_no_death" ? stageWithStars : GetStageData(id));

            // 입장
            var enterRequest = EnterStageRequest.Create("stage_no_death", new List<string> { "char_1" });
            var enterResponse = _handler.HandleEnterStage(enterRequest, ref _testUserData);

            // 클리어 (사망자 있음)
            var clearRequest = ClearStageRequest.Create(
                enterResponse.BattleSessionId,
                isVictory: true,
                turnCount: 5,
                noCharacterDeath: false,
                allFullHP: false);

            var clearResponse = _handler.HandleClearStage(clearRequest, ref _testUserData);

            Assert.That(clearResponse.NewStarsAchieved[0], Is.True); // Clear
            Assert.That(clearResponse.NewStarsAchieved[1], Is.False); // NoCharacterDeath
        }

        #endregion

        #region Multiple Entry Tests

        [Test]
        public void HandleEnterStage_TracksMultipleEntries()
        {
            // 첫 번째 입장
            var request1 = EnterStageRequest.Create(_limitedStage.Id, new List<string> { "char_1" });
            _handler.HandleEnterStage(request1, ref _testUserData);

            // 두 번째 입장
            var request2 = EnterStageRequest.Create(_limitedStage.Id, new List<string> { "char_1" });
            var response2 = _handler.HandleEnterStage(request2, ref _testUserData);

            Assert.That(response2.EntryRecord.EntryCount, Is.EqualTo(2));
        }

        [Test]
        public void HandleClearStage_IncrementsClearCount_OnMultipleClears()
        {
            // 첫 번째 클리어
            var enterRequest1 = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse1 = _handler.HandleEnterStage(enterRequest1, ref _testUserData);
            var clearRequest1 = ClearStageRequest.Create(
                enterResponse1.BattleSessionId, true, 5, true, true);
            _handler.HandleClearStage(clearRequest1, ref _testUserData);

            // 두 번째 클리어
            var enterRequest2 = EnterStageRequest.Create(_normalStage.Id, new List<string> { "char_1" });
            var enterResponse2 = _handler.HandleEnterStage(enterRequest2, ref _testUserData);
            var clearRequest2 = ClearStageRequest.Create(
                enterResponse2.BattleSessionId, true, 5, true, true);
            var clearResponse2 = _handler.HandleClearStage(clearRequest2, ref _testUserData);

            Assert.That(clearResponse2.IsFirstClear, Is.False);
            Assert.That(clearResponse2.ClearInfo.ClearCount, Is.EqualTo(2));
        }

        #endregion

        #region Helper Methods

        private StageDataInfo GetStageData(string id)
        {
            return id switch
            {
                "stage_1_1" => _normalStage,
                "stage_limited" => _limitedStage,
                "stage_locked" => _lockedStage,
                "stage_day_restricted" => _dayRestrictedStage,
                _ => null
            };
        }

        private StageDataInfo CreateStageInfo(
            string id,
            bool isEnabled = true,
            CostType entryCostType = CostType.None,
            int entryCost = 0,
            LimitType limitType = LimitType.None,
            int limitCount = 0,
            string unlockConditionStageId = null,
            DayOfWeek[] availableDays = null,
            StarConditionInfo[] starConditions = null,
            List<RewardInfo> firstClearRewards = null,
            List<RewardInfo> repeatClearRewards = null)
        {
            return new StageDataInfo
            {
                Id = id,
                IsEnabled = isEnabled,
                EntryCostType = entryCostType,
                EntryCost = entryCost,
                LimitType = limitType,
                LimitCount = limitCount,
                UnlockConditionStageId = unlockConditionStageId,
                AvailableDays = availableDays,
                StarConditions = starConditions,
                FirstClearRewards = firstClearRewards,
                RepeatClearRewards = repeatClearRewards
            };
        }

        private DayOfWeek GetToday()
        {
            return DateTimeOffset.FromUnixTimeSeconds(_timeService.ServerTimeUtc).UtcDateTime.DayOfWeek;
        }

        private DayOfWeek[] GetOtherDays(DayOfWeek excludeDay)
        {
            var days = new List<DayOfWeek>();
            for (int i = 0; i < 7; i++)
            {
                var day = (DayOfWeek)i;
                if (day != excludeDay)
                    days.Add(day);
            }

            return days.ToArray();
        }

        #endregion
    }
}