using NUnit.Framework;
using Sc.Core;
using Sc.Data;

namespace Sc.Editor.Tests.Core
{
    /// <summary>
    /// SaveMigrator 단위 테스트
    /// </summary>
    [TestFixture]
    public class SaveMigratorTests
    {
        private SaveMigrator _migrator;

        [SetUp]
        public void SetUp()
        {
            _migrator = new SaveMigrator();
        }

        #region NeedsMigration Tests

        [Test]
        public void NeedsMigration_ReturnsTrue_WhenVersionIsLower()
        {
            var data = CreateTestData(version: 1);

            var result = _migrator.NeedsMigration(data, targetVersion: 2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void NeedsMigration_ReturnsFalse_WhenVersionIsEqual()
        {
            var data = CreateTestData(version: 2);

            var result = _migrator.NeedsMigration(data, targetVersion: 2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void NeedsMigration_ReturnsFalse_WhenVersionIsHigher()
        {
            var data = CreateTestData(version: 3);

            var result = _migrator.NeedsMigration(data, targetVersion: 2);

            Assert.That(result, Is.False);
        }

        #endregion

        #region Migrate Tests

        [Test]
        public void Migrate_ReturnsOriginalData_WhenNoMigrationNeeded()
        {
            var data = CreateTestData(version: 2);

            var result = _migrator.Migrate(data, targetVersion: 2);

            Assert.That(result.Version, Is.EqualTo(2));
            Assert.That(result.Profile.Uid, Is.EqualTo(data.Profile.Uid));
        }

        [Test]
        public void Migrate_UpdatesVersion_WhenMigrationNeeded()
        {
            var data = CreateTestData(version: 1);

            var result = _migrator.Migrate(data, targetVersion: 2);

            Assert.That(result.Version, Is.EqualTo(UserSaveData.CurrentVersion));
        }

        [Test]
        public void Migrate_PreservesUserData_AfterMigration()
        {
            var data = CreateTestData(version: 1);
            data.Currency.Gold = 5000;
            data.Currency.Gem = 100;

            var result = _migrator.Migrate(data, targetVersion: 2);

            Assert.That(result.Currency.Gold, Is.EqualTo(5000));
            Assert.That(result.Currency.Gem, Is.EqualTo(100));
        }

        #endregion

        #region Custom Migration Tests

        [Test]
        public void Register_AddsMigration_ToChain()
        {
            var customMigration = new TestMigration(fromVersion: 1, toVersion: 2);
            _migrator.Register(customMigration);

            var data = CreateTestData(version: 1);
            var result = _migrator.Migrate(data, targetVersion: 2);

            Assert.That(customMigration.WasCalled, Is.True);
        }

        [Test]
        public void Register_SortsMigrations_ByFromVersion()
        {
            var migration2to3 = new TestMigration(fromVersion: 2, toVersion: 3);
            var migration1to2 = new TestMigration(fromVersion: 1, toVersion: 2);

            // 역순으로 등록
            _migrator.Register(migration2to3);
            _migrator.Register(migration1to2);

            var data = CreateTestData(version: 1);
            _migrator.Migrate(data, targetVersion: 3);

            // 1→2가 먼저 호출되어야 함
            Assert.That(migration1to2.CallOrder, Is.LessThan(migration2to3.CallOrder));
        }

        #endregion

        #region Helper Methods

        private UserSaveData CreateTestData(int version)
        {
            var data = UserSaveData.CreateNew("test_uid", "TestUser");
            // Version은 readonly가 아니므로 직접 설정
            data.Version = version;
            return data;
        }

        /// <summary>
        /// 테스트용 마이그레이션 구현
        /// </summary>
        private class TestMigration : ISaveMigration
        {
            private static int _globalCallCounter = 0;

            public int FromVersion { get; }
            public int ToVersion { get; }
            public bool WasCalled { get; private set; }
            public int CallOrder { get; private set; }

            public TestMigration(int fromVersion, int toVersion)
            {
                FromVersion = fromVersion;
                ToVersion = toVersion;
            }

            public UserSaveData Migrate(UserSaveData data)
            {
                WasCalled = true;
                CallOrder = ++_globalCallCounter;
                data.Version = ToVersion;
                return data;
            }
        }

        #endregion
    }
}
