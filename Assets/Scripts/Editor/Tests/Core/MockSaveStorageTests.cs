using NUnit.Framework;
using Sc.Foundation;
using Sc.Tests;

namespace Sc.Editor.Tests.Core
{
    /// <summary>
    /// MockSaveStorage 단위 테스트
    /// Mock이 올바르게 동작하는지 검증
    /// </summary>
    [TestFixture]
    public class MockSaveStorageTests
    {
        private MockSaveStorage _storage;

        [SetUp]
        public void SetUp()
        {
            _storage = new MockSaveStorage();
        }

        #region Save Tests

        [Test]
        public void Save_ReturnsSuccess_Always()
        {
            var result = _storage.Save("key", "value");

            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void Save_StoresData_InMemory()
        {
            _storage.Save("test_key", "test_value");

            Assert.That(_storage.Exists("test_key"), Is.True);
            Assert.That(_storage.Count, Is.EqualTo(1));
        }

        [Test]
        public void Save_OverwritesExistingData()
        {
            _storage.Save("key", "original");
            _storage.Save("key", "updated");

            var result = _storage.Load("key");
            Assert.That(result.Value, Is.EqualTo("updated"));
            Assert.That(_storage.Count, Is.EqualTo(1));
        }

        #endregion

        #region Load Tests

        [Test]
        public void Load_ReturnsSuccess_WhenKeyExists()
        {
            _storage.Save("key", "value");

            var result = _storage.Load("key");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo("value"));
        }

        [Test]
        public void Load_ReturnsFailure_WhenKeyDoesNotExist()
        {
            var result = _storage.Load("nonexistent");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.LoadFailed));
        }

        #endregion

        #region Exists Tests

        [Test]
        public void Exists_ReturnsTrue_WhenKeyExists()
        {
            _storage.Save("key", "value");

            Assert.That(_storage.Exists("key"), Is.True);
        }

        [Test]
        public void Exists_ReturnsFalse_WhenKeyDoesNotExist()
        {
            Assert.That(_storage.Exists("nonexistent"), Is.False);
        }

        #endregion

        #region Delete Tests

        [Test]
        public void Delete_RemovesData()
        {
            _storage.Save("key", "value");

            _storage.Delete("key");

            Assert.That(_storage.Exists("key"), Is.False);
            Assert.That(_storage.Count, Is.EqualTo(0));
        }

        [Test]
        public void Delete_ReturnsSuccess_EvenWhenKeyDoesNotExist()
        {
            var result = _storage.Delete("nonexistent");

            Assert.That(result.IsSuccess, Is.True);
        }

        #endregion

        #region Clear Tests

        [Test]
        public void Clear_RemovesAllData()
        {
            _storage.Save("key1", "value1");
            _storage.Save("key2", "value2");
            _storage.Save("key3", "value3");

            _storage.Clear();

            Assert.That(_storage.Count, Is.EqualTo(0));
            Assert.That(_storage.Exists("key1"), Is.False);
            Assert.That(_storage.Exists("key2"), Is.False);
            Assert.That(_storage.Exists("key3"), Is.False);
        }

        #endregion

        #region GetAllKeys Tests

        [Test]
        public void GetAllKeys_ReturnsAllStoredKeys()
        {
            _storage.Save("key1", "value1");
            _storage.Save("key2", "value2");

            var keys = _storage.GetAllKeys();

            Assert.That(keys, Does.Contain("key1"));
            Assert.That(keys, Does.Contain("key2"));
        }

        [Test]
        public void GetAllKeys_ReturnsEmpty_WhenNoData()
        {
            var keys = _storage.GetAllKeys();

            Assert.That(keys, Is.Empty);
        }

        #endregion

        #region ISaveStorage Interface Compliance

        [Test]
        public void ImplementsISaveStorage_Correctly()
        {
            ISaveStorage storage = _storage;

            // 인터페이스를 통한 호출이 정상 동작하는지 확인
            var saveResult = storage.Save("interface_test", "data");
            Assert.That(saveResult.IsSuccess, Is.True);

            Assert.That(storage.Exists("interface_test"), Is.True);

            var loadResult = storage.Load("interface_test");
            Assert.That(loadResult.IsSuccess, Is.True);
            Assert.That(loadResult.Value, Is.EqualTo("data"));

            var deleteResult = storage.Delete("interface_test");
            Assert.That(deleteResult.IsSuccess, Is.True);
            Assert.That(storage.Exists("interface_test"), Is.False);
        }

        #endregion
    }
}
