using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sc.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sc.Editor.Tests.Stage
{
    /// <summary>
    /// StageDatabase 단위 테스트.
    /// 스테이지 필터링 및 조회 로직 검증.
    /// </summary>
    [TestFixture]
    public class StageDatabaseTests
    {
        private StageDatabase _database;
        private StageData _mainStory1;
        private StageData _mainStory2;
        private StageData _goldDungeon1;
        private StageData _goldDungeon2;
        private StageData _expDungeon1;
        private StageData _eventStage1;

        [SetUp]
        public void SetUp()
        {
            _database = ScriptableObject.CreateInstance<StageDatabase>();

            // MainStory 스테이지
            _mainStory1 = CreateStage("stage_main_1_1", InGameContentType.MainStory, "chapter_1");
            _mainStory2 = CreateStage("stage_main_1_2", InGameContentType.MainStory, "chapter_1");

            // GoldDungeon 스테이지
            _goldDungeon1 = CreateStage("stage_gold_fire_1", InGameContentType.GoldDungeon, "gold_fire");
            _goldDungeon2 = CreateStage("stage_gold_water_1", InGameContentType.GoldDungeon, "gold_water");

            // ExpDungeon 스테이지
            _expDungeon1 = CreateStage("stage_exp_easy_1", InGameContentType.ExpDungeon, "exp_easy");

            // Event 스테이지 (EventId는 CategoryId를 통해 필터링됨)
            _eventStage1 = CreateStage("stage_event_1", InGameContentType.Event, "test_event");

            // 데이터베이스에 추가
            AddStagesToDatabase(_mainStory1, _mainStory2, _goldDungeon1, _goldDungeon2, _expDungeon1, _eventStage1);
        }

        [TearDown]
        public void TearDown()
        {
            DestroyStages();
            if (_database != null)
                Object.DestroyImmediate(_database);
        }

        #region GetByContentType Tests

        [Test]
        public void GetByContentType_ReturnsMatchingStages()
        {
            var mainStoryStages = _database.GetByContentType(InGameContentType.MainStory).ToList();

            Assert.That(mainStoryStages.Count, Is.EqualTo(2));
            Assert.That(mainStoryStages, Contains.Item(_mainStory1));
            Assert.That(mainStoryStages, Contains.Item(_mainStory2));
        }

        [Test]
        public void GetByContentType_ReturnsEmpty_WhenNoMatches()
        {
            var bossRaidStages = _database.GetByContentType(InGameContentType.BossRaid).ToList();

            Assert.That(bossRaidStages, Is.Empty);
        }

        [Test]
        public void GetByContentType_ReturnsSingleStage_WhenOnlyOneMatches()
        {
            var expStages = _database.GetByContentType(InGameContentType.ExpDungeon).ToList();

            Assert.That(expStages.Count, Is.EqualTo(1));
            Assert.That(expStages[0], Is.EqualTo(_expDungeon1));
        }

        #endregion

        #region GetByContentTypeAndCategory Tests

        [Test]
        public void GetByContentTypeAndCategory_ReturnsMatchingStages()
        {
            var goldFireStages = _database.GetByContentTypeAndCategory(
                InGameContentType.GoldDungeon, "gold_fire").ToList();

            Assert.That(goldFireStages.Count, Is.EqualTo(1));
            Assert.That(goldFireStages[0], Is.EqualTo(_goldDungeon1));
        }

        [Test]
        public void GetByContentTypeAndCategory_ReturnsEmpty_WhenCategoryNotMatches()
        {
            var stages = _database.GetByContentTypeAndCategory(
                InGameContentType.GoldDungeon, "gold_wind").ToList();

            Assert.That(stages, Is.Empty);
        }

        [Test]
        public void GetByContentTypeAndCategory_ReturnsEmpty_WhenContentTypeNotMatches()
        {
            var stages = _database.GetByContentTypeAndCategory(
                InGameContentType.MainStory, "gold_fire").ToList();

            Assert.That(stages, Is.Empty);
        }

        #endregion

        #region GetByCategory Tests

        [Test]
        public void GetByCategory_ReturnsAllStagesInCategory()
        {
            var chapter1Stages = _database.GetByCategory("chapter_1").ToList();

            Assert.That(chapter1Stages.Count, Is.EqualTo(2));
            Assert.That(chapter1Stages, Contains.Item(_mainStory1));
            Assert.That(chapter1Stages, Contains.Item(_mainStory2));
        }

        [Test]
        public void GetByCategory_ReturnsEmpty_WhenCategoryNotFound()
        {
            var stages = _database.GetByCategory("non_existent").ToList();

            Assert.That(stages, Is.Empty);
        }

        #endregion

        #region GetByEvent Tests

        [Test]
        public void GetByEvent_ReturnsMatchingStages()
        {
            var eventStages = _database.GetByEvent("test_event").ToList();

            Assert.That(eventStages.Count, Is.EqualTo(1));
            Assert.That(eventStages[0], Is.EqualTo(_eventStage1));
        }

        [Test]
        public void GetByEvent_ReturnsEmpty_WhenEventNotFound()
        {
            var stages = _database.GetByEvent("non_existent_event").ToList();

            Assert.That(stages, Is.Empty);
        }

        #endregion

        #region Helper Methods

        private StageData CreateStage(
            string id,
            InGameContentType contentType,
            string categoryId)
        {
            var stage = ScriptableObject.CreateInstance<StageData>();
            stage.InitializeFull(
                id: id,
                name: $"Test Stage {id}",
                nameEn: $"Test Stage {id}",
                contentType: contentType,
                categoryId: categoryId,
                stageType: StageType.Normal,
                chapter: 1,
                stageNumber: 1,
                difficulty: Difficulty.Normal,
                entryCostType: CostType.Stamina,
                entryCost: 10,
                limitType: LimitType.None,
                limitCount: 0,
                availableDays: Array.Empty<DayOfWeek>(),
                unlockConditionStageId: null,
                unlockConditionLevel: 0,
                recommendedPower: 1000,
                enemyIds: Array.Empty<string>(),
                firstClearRewards: new List<RewardInfo>(),
                repeatClearRewards: new List<RewardInfo>(),
                star1: default,
                star2: default,
                star3: default,
                displayOrder: 0,
                isEnabled: true,
                description: $"Description for {id}");

            return stage;
        }

        private void AddStagesToDatabase(params StageData[] stages)
        {
            foreach (var stage in stages)
            {
                _database.Add(stage);
            }
        }

        private void DestroyStages()
        {
            if (_mainStory1 != null) Object.DestroyImmediate(_mainStory1);
            if (_mainStory2 != null) Object.DestroyImmediate(_mainStory2);
            if (_goldDungeon1 != null) Object.DestroyImmediate(_goldDungeon1);
            if (_goldDungeon2 != null) Object.DestroyImmediate(_goldDungeon2);
            if (_expDungeon1 != null) Object.DestroyImmediate(_expDungeon1);
            if (_eventStage1 != null) Object.DestroyImmediate(_eventStage1);
        }

        #endregion
    }
}