using NUnit.Framework;
using Sc.Core;
using UnityEngine;

namespace Sc.Tests
{
    /// <summary>
    /// AssetScope 단위 테스트
    /// </summary>
    [TestFixture]
    public class AssetScopeTests
    {
        private AssetScope _scope;
        private bool _onReleaseCalled;

        [SetUp]
        public void SetUp()
        {
            _onReleaseCalled = false;
            _scope = CreateScope("TestScope");
        }

        [TearDown]
        public void TearDown()
        {
            if (_scope != null && !_scope.IsReleased)
            {
                _scope.Release();
            }
        }

        private AssetScope CreateScope(string name)
        {
            var constructor = typeof(AssetScope).GetConstructor(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new[] { typeof(string), typeof(System.Action<AssetScope>) },
                null);

            return (AssetScope)constructor.Invoke(new object[]
            {
                name,
                new System.Action<AssetScope>(s => _onReleaseCalled = true)
            });
        }

        [Test]
        public void Constructor_SetsInitialValues()
        {
            Assert.AreEqual("TestScope", _scope.Name);
            Assert.AreEqual(0, _scope.AssetCount);
            Assert.IsFalse(_scope.IsReleased);
        }

        [Test]
        public void Release_SetsIsReleasedTrue()
        {
            _scope.Release();

            Assert.IsTrue(_scope.IsReleased);
            Assert.IsTrue(_onReleaseCalled);
        }

        [Test]
        public void Release_WhenAlreadyReleased_DoesNotCallOnReleaseAgain()
        {
            _scope.Release();
            _onReleaseCalled = false;

            _scope.Release(); // 두 번째 호출

            Assert.IsFalse(_onReleaseCalled); // 콜백 다시 호출되지 않음
        }

        [Test]
        public void RegisterHandle_IncreasesAssetCount()
        {
            var texture = new Texture2D(1, 1);
            var handle = CreateHandle("test", texture);

            var registerMethod = typeof(AssetScope).GetMethod("RegisterHandle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            registerMethod = registerMethod.MakeGenericMethod(typeof(Texture2D));
            registerMethod.Invoke(_scope, new object[] { handle });

            Assert.AreEqual(1, _scope.AssetCount);

            Object.DestroyImmediate(texture);
        }

        [Test]
        public void UnregisterHandle_DecreasesAssetCount()
        {
            var texture = new Texture2D(1, 1);
            var handle = CreateHandle("test", texture);

            // Register
            var registerMethod = typeof(AssetScope).GetMethod("RegisterHandle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            registerMethod = registerMethod.MakeGenericMethod(typeof(Texture2D));
            registerMethod.Invoke(_scope, new object[] { handle });

            // Unregister
            var unregisterMethod = typeof(AssetScope).GetMethod("UnregisterHandle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            unregisterMethod = unregisterMethod.MakeGenericMethod(typeof(Texture2D));
            unregisterMethod.Invoke(_scope, new object[] { handle });

            Assert.AreEqual(0, _scope.AssetCount);

            Object.DestroyImmediate(texture);
        }

        [Test]
        public void RegisterHandle_WhenReleased_DoesNotRegister()
        {
            _scope.Release();

            var texture = new Texture2D(1, 1);
            var handle = CreateHandle("test", texture);

            var registerMethod = typeof(AssetScope).GetMethod("RegisterHandle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            registerMethod = registerMethod.MakeGenericMethod(typeof(Texture2D));
            registerMethod.Invoke(_scope, new object[] { handle });

            Assert.AreEqual(0, _scope.AssetCount);

            Object.DestroyImmediate(texture);
        }

        [Test]
        public void Release_ForceReleasesHandles_ViaIAssetHandle()
        {
            var texture = new Texture2D(1, 1);
            var handle = CreateHandle("test", texture);

            // Register
            var registerMethod = typeof(AssetScope).GetMethod("RegisterHandle",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            registerMethod = registerMethod.MakeGenericMethod(typeof(Texture2D));
            registerMethod.Invoke(_scope, new object[] { handle });

            // IAssetHandle 인터페이스로 확인
            IAssetHandle iHandle = handle;
            Assert.IsTrue(iHandle.IsValid);

            // Release scope
            _scope.Release();

            // Handle should be force released via IAssetHandle interface
            Assert.IsFalse(iHandle.IsValid);

            Object.DestroyImmediate(texture);
        }

        private AssetHandle<Texture2D> CreateHandle(string key, Texture2D asset)
        {
            var constructor = typeof(AssetHandle<Texture2D>).GetConstructor(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                new[] { typeof(string), typeof(Texture2D), typeof(System.Action<AssetHandle<Texture2D>>) },
                null);

            return (AssetHandle<Texture2D>)constructor.Invoke(new object[]
            {
                key,
                asset,
                null
            });
        }
    }
}
