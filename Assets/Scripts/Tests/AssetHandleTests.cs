using NUnit.Framework;
using Sc.Core;
using UnityEngine;

namespace Sc.Tests
{
    /// <summary>
    /// AssetHandle 단위 테스트
    /// </summary>
    [TestFixture]
    public class AssetHandleTests
    {
        private Texture2D _testTexture;
        private AssetHandle<Texture2D> _handle;
        private bool _onReleaseCalled;

        [SetUp]
        public void SetUp()
        {
            _testTexture = new Texture2D(1, 1);
            _onReleaseCalled = false;
            _handle = CreateHandle("test_key", _testTexture);
        }

        [TearDown]
        public void TearDown()
        {
            if (_testTexture != null)
            {
                Object.DestroyImmediate(_testTexture);
                _testTexture = null;
            }
        }

        private AssetHandle<Texture2D> CreateHandle(string key, Texture2D asset)
        {
            // AssetHandle 생성자가 internal이므로 reflection 사용
            var constructor = typeof(AssetHandle<Texture2D>).GetConstructor(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new[] { typeof(string), typeof(Texture2D), typeof(System.Action<AssetHandle<Texture2D>>) },
                null);

            return (AssetHandle<Texture2D>)constructor.Invoke(new object[]
            {
                key,
                asset,
                new System.Action<AssetHandle<Texture2D>>(h => _onReleaseCalled = true)
            });
        }

        [Test]
        public void Constructor_SetsInitialValues()
        {
            Assert.AreEqual("test_key", _handle.Key);
            Assert.AreEqual(_testTexture, _handle.Asset);
            Assert.AreEqual(1, _handle.RefCount);
            Assert.IsTrue(_handle.IsValid);
        }

        [Test]
        public void ImplicitConversion_ReturnsAsset()
        {
            Texture2D texture = _handle;
            Assert.AreEqual(_testTexture, texture);
        }

        [Test]
        public void Release_DecrementsRefCount()
        {
            // AddRef로 RefCount 증가
            var addRefMethod = typeof(AssetHandle<Texture2D>).GetMethod("AddRef",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            addRefMethod.Invoke(_handle, null);

            Assert.AreEqual(2, _handle.RefCount);

            // Release
            _handle.Release();
            Assert.AreEqual(1, _handle.RefCount);
            Assert.IsFalse(_onReleaseCalled); // RefCount > 0이면 콜백 안 함
        }

        [Test]
        public void Release_WhenRefCountZero_CallsOnRelease()
        {
            _handle.Release();

            Assert.AreEqual(0, _handle.RefCount);
            Assert.IsTrue(_onReleaseCalled);
        }

        [Test]
        public void ForceRelease_ViaInterface_SetsIsReleasedTrue()
        {
            // IAssetHandle 인터페이스를 통한 ForceRelease 테스트
            IAssetHandle iHandle = _handle;
            iHandle.ForceRelease();

            Assert.IsFalse(_handle.IsValid);
            Assert.AreEqual(0, _handle.RefCount);
        }

        [Test]
        public void Asset_AfterForceRelease_ReturnsNull()
        {
            IAssetHandle iHandle = _handle;
            iHandle.ForceRelease();

            Assert.IsNull(_handle.Asset);
        }

        [Test]
        public void IAssetHandle_Key_ReturnsCorrectKey()
        {
            IAssetHandle iHandle = _handle;
            Assert.AreEqual("test_key", iHandle.Key);
        }

        [Test]
        public void IAssetHandle_RefCount_ReturnsCorrectValue()
        {
            IAssetHandle iHandle = _handle;
            Assert.AreEqual(1, iHandle.RefCount);
        }

        [Test]
        public void IAssetHandle_IsValid_ReturnsTrue_WhenNotReleased()
        {
            IAssetHandle iHandle = _handle;
            Assert.IsTrue(iHandle.IsValid);
        }

        [Test]
        public void IAssetHandle_IsValid_ReturnsFalse_AfterForceRelease()
        {
            IAssetHandle iHandle = _handle;
            iHandle.ForceRelease();
            Assert.IsFalse(iHandle.IsValid);
        }

        [Test]
        public void IAssetHandle_Release_DecrementsRefCount()
        {
            // AddRef로 RefCount 증가
            var addRefMethod = typeof(AssetHandle<Texture2D>).GetMethod("AddRef",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            addRefMethod.Invoke(_handle, null);

            IAssetHandle iHandle = _handle;
            iHandle.Release();

            Assert.AreEqual(1, iHandle.RefCount);
        }

        [Test]
        public void NullHandle_ImplicitConversion_ReturnsNull()
        {
            AssetHandle<Texture2D> nullHandle = null;
            Texture2D texture = nullHandle;
            Assert.IsNull(texture);
        }
    }
}
