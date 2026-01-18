using System.IO;
using NUnit.Framework;
using Sc.Foundation;

namespace Sc.Editor.Tests.Core
{
    /// <summary>
    /// ISaveStorage 구현체 단위 테스트
    /// </summary>
    [TestFixture]
    public class SaveStorageTests
    {
        private string _testDirectory;
        private FileSaveStorage _fileStorage;

        [SetUp]
        public void SetUp()
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), "SaveStorageTests_" + System.Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_testDirectory);
            _fileStorage = new FileSaveStorage(_testDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        #region FileSaveStorage Tests

        [Test]
        public void Save_ReturnsSuccess_WhenDataIsValid()
        {
            var result = _fileStorage.Save("test_key", "{\"data\":\"value\"}");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public void Save_CreatesFile_InCorrectLocation()
        {
            _fileStorage.Save("test_key", "{\"data\":\"value\"}");

            var expectedPath = Path.Combine(_testDirectory, "test_key.json");
            Assert.That(File.Exists(expectedPath), Is.True);
        }

        [Test]
        public void Save_ReturnsFailure_WhenKeyIsEmpty()
        {
            var result = _fileStorage.Save("", "data");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.SaveFailed));
        }

        [Test]
        public void Save_ReturnsFailure_WhenKeyIsNull()
        {
            var result = _fileStorage.Save(null, "data");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.SaveFailed));
        }

        [Test]
        public void Load_ReturnsSuccess_WhenFileExists()
        {
            var testData = "{\"name\":\"test\"}";
            _fileStorage.Save("load_test", testData);

            var result = _fileStorage.Load("load_test");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(testData));
        }

        [Test]
        public void Load_ReturnsFailure_WhenFileDoesNotExist()
        {
            var result = _fileStorage.Load("nonexistent_key");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.LoadFailed));
        }

        [Test]
        public void Load_ReturnsFailure_WhenKeyIsEmpty()
        {
            var result = _fileStorage.Load("");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.LoadFailed));
        }

        [Test]
        public void Exists_ReturnsTrue_WhenFileExists()
        {
            _fileStorage.Save("exists_test", "data");

            Assert.That(_fileStorage.Exists("exists_test"), Is.True);
        }

        [Test]
        public void Exists_ReturnsFalse_WhenFileDoesNotExist()
        {
            Assert.That(_fileStorage.Exists("nonexistent"), Is.False);
        }

        [Test]
        public void Exists_ReturnsFalse_WhenKeyIsEmpty()
        {
            Assert.That(_fileStorage.Exists(""), Is.False);
        }

        [Test]
        public void Exists_ReturnsFalse_WhenKeyIsNull()
        {
            Assert.That(_fileStorage.Exists(null), Is.False);
        }

        [Test]
        public void Delete_ReturnsSuccess_WhenFileExists()
        {
            _fileStorage.Save("delete_test", "data");

            var result = _fileStorage.Delete("delete_test");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(_fileStorage.Exists("delete_test"), Is.False);
        }

        [Test]
        public void Delete_ReturnsSuccess_WhenFileDoesNotExist()
        {
            var result = _fileStorage.Delete("nonexistent");

            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void Delete_ReturnsFailure_WhenKeyIsEmpty()
        {
            var result = _fileStorage.Delete("");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.SaveFailed));
        }

        [Test]
        public void SaveAndLoad_PreservesData_Correctly()
        {
            var originalData = "{\"user\":\"테스트\",\"level\":99,\"items\":[1,2,3]}";

            _fileStorage.Save("roundtrip_test", originalData);
            var result = _fileStorage.Load("roundtrip_test");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(originalData));
        }

        [Test]
        public void Save_OverwritesExistingFile()
        {
            _fileStorage.Save("overwrite_test", "original");
            _fileStorage.Save("overwrite_test", "updated");

            var result = _fileStorage.Load("overwrite_test");

            Assert.That(result.Value, Is.EqualTo("updated"));
        }

        #endregion
    }
}
