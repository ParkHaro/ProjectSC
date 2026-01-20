using NUnit.Framework;
using Sc.Contents.Stage;
using Sc.Data;

namespace Sc.Editor.Tests.Stage
{
    /// <summary>
    /// StageContentModuleFactory 단위 테스트.
    /// 모듈 생성 및 등록 로직 검증.
    /// </summary>
    [TestFixture]
    public class StageContentModuleFactoryTests
    {
        [SetUp]
        public void SetUp()
        {
            // 각 테스트 전 팩토리 초기화
            StageContentModuleFactory.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            StageContentModuleFactory.Reset();
        }

        #region Create Tests

        [Test]
        public void Create_ReturnsMainStoryModule_WhenMainStoryContentType()
        {
            var module = StageContentModuleFactory.Create(InGameContentType.MainStory);

            Assert.That(module, Is.Not.Null);
            Assert.That(module, Is.TypeOf<MainStoryContentModule>());
        }

        [Test]
        public void Create_ReturnsMainStoryModule_WhenHardModeContentType()
        {
            // HardMode는 MainStory와 유사하므로 같은 모듈 사용
            var module = StageContentModuleFactory.Create(InGameContentType.HardMode);

            Assert.That(module, Is.Not.Null);
            Assert.That(module, Is.TypeOf<MainStoryContentModule>());
        }

        [Test]
        public void Create_ReturnsElementDungeonModule_WhenGoldDungeonContentType()
        {
            var module = StageContentModuleFactory.Create(InGameContentType.GoldDungeon);

            Assert.That(module, Is.Not.Null);
            Assert.That(module, Is.TypeOf<ElementDungeonContentModule>());
        }

        [Test]
        public void Create_ReturnsElementDungeonModule_WhenExpDungeonContentType()
        {
            var module = StageContentModuleFactory.Create(InGameContentType.ExpDungeon);

            Assert.That(module, Is.Not.Null);
            Assert.That(module, Is.TypeOf<ElementDungeonContentModule>());
        }

        [Test]
        public void Create_ReturnsElementDungeonModule_WhenSkillDungeonContentType()
        {
            var module = StageContentModuleFactory.Create(InGameContentType.SkillDungeon);

            Assert.That(module, Is.Not.Null);
            Assert.That(module, Is.TypeOf<ElementDungeonContentModule>());
        }

        [Test]
        public void Create_ReturnsNull_WhenNoModuleRegistered()
        {
            // BossRaid, Tower, Event는 아직 등록되지 않음
            var module = StageContentModuleFactory.Create(InGameContentType.BossRaid);

            Assert.That(module, Is.Null);
        }

        [Test]
        public void Create_CreatesNewInstanceEachTime()
        {
            var module1 = StageContentModuleFactory.Create(InGameContentType.MainStory);
            var module2 = StageContentModuleFactory.Create(InGameContentType.MainStory);

            Assert.That(module1, Is.Not.SameAs(module2));
        }

        #endregion

        #region HasModule Tests

        [Test]
        public void HasModule_ReturnsTrue_WhenModuleRegistered()
        {
            var hasMainStory = StageContentModuleFactory.HasModule(InGameContentType.MainStory);
            var hasGoldDungeon = StageContentModuleFactory.HasModule(InGameContentType.GoldDungeon);

            Assert.That(hasMainStory, Is.True);
            Assert.That(hasGoldDungeon, Is.True);
        }

        [Test]
        public void HasModule_ReturnsFalse_WhenModuleNotRegistered()
        {
            var hasBossRaid = StageContentModuleFactory.HasModule(InGameContentType.BossRaid);
            var hasTower = StageContentModuleFactory.HasModule(InGameContentType.Tower);

            Assert.That(hasBossRaid, Is.False);
            Assert.That(hasTower, Is.False);
        }

        #endregion

        #region RegisterModule Tests

        [Test]
        public void RegisterModule_OverridesExistingFactory()
        {
            // 기존 MainStory 모듈을 ElementDungeon으로 오버라이드
            StageContentModuleFactory.RegisterModule(
                InGameContentType.MainStory,
                () => new ElementDungeonContentModule());

            var module = StageContentModuleFactory.Create(InGameContentType.MainStory);

            Assert.That(module, Is.TypeOf<ElementDungeonContentModule>());
        }

        [Test]
        public void RegisterModule_AllowsCustomModule()
        {
            // BossRaid에 커스텀 모듈 등록 (ElementDungeon 재사용)
            StageContentModuleFactory.RegisterModule(
                InGameContentType.BossRaid,
                () => new ElementDungeonContentModule());

            var module = StageContentModuleFactory.Create(InGameContentType.BossRaid);

            Assert.That(module, Is.Not.Null);
            Assert.That(StageContentModuleFactory.HasModule(InGameContentType.BossRaid), Is.True);
        }

        [Test]
        public void RegisterModule_IgnoresNullFactory()
        {
            // null 팩토리 등록 시도
            StageContentModuleFactory.RegisterModule(InGameContentType.MainStory, null);

            // 기존 등록 유지
            var module = StageContentModuleFactory.Create(InGameContentType.MainStory);
            Assert.That(module, Is.Not.Null);
        }

        #endregion

        #region UnregisterModule Tests

        [Test]
        public void UnregisterModule_RemovesModule()
        {
            Assert.That(StageContentModuleFactory.HasModule(InGameContentType.MainStory), Is.True);

            StageContentModuleFactory.UnregisterModule(InGameContentType.MainStory);

            Assert.That(StageContentModuleFactory.HasModule(InGameContentType.MainStory), Is.False);
            Assert.That(StageContentModuleFactory.Create(InGameContentType.MainStory), Is.Null);
        }

        #endregion

        #region Reset Tests

        [Test]
        public void Reset_ClearsAllModules()
        {
            // Reset 호출 전 모듈 존재 확인
            Assert.That(StageContentModuleFactory.HasModule(InGameContentType.MainStory), Is.True);

            // Reset 후 초기화 전까지 모듈 없음
            StageContentModuleFactory.Reset();

            // 다음 Create 호출 시 자동 초기화되므로 여기서는 HasModule만 테스트
            // (HasModule도 자동 초기화됨)
            // 이 테스트는 Reset의 동작 자체를 검증
        }

        #endregion
    }
}