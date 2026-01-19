using NUnit.Framework;
using Sc.Core;
using UnityEngine;

namespace Sc.Tests
{
    /// <summary>
    /// AssetCacheManager 단위 테스트
    /// </summary>
    [TestFixture]
    public class AssetCacheManagerTests
    {
        private AssetCacheManager _cacheManager;
        private Texture2D _testTexture;
        private bool _onReleaseCalled;

        [SetUp]
        public void SetUp()
        {
            _cacheManager = CreateCacheManager(5);
            _testTexture = new Texture2D(1, 1);
            _onReleaseCalled = false;
        }

        [TearDown]
        public void TearDown()
        {
            _cacheManager?.ClearAll();
            if (_testTexture != null)
            {
                Object.DestroyImmediate(_testTexture);
                _testTexture = null;
            }
        }

        private AssetCacheManager CreateCacheManager(int maxSize)
        {
            var constructor = typeof(AssetCacheManager).GetConstructor(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public,
                null,
                new[] { typeof(int) },
                null);

            return (AssetCacheManager)constructor.Invoke(new object[] { maxSize });
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
                new System.Action<AssetHandle<Texture2D>>(h => _onReleaseCalled = true)
            });
        }

        [Test]
        public void Constructor_CreatesEmptyCache()
        {
            Assert.AreEqual(0, _cacheManager.CacheCount);
        }

        [Test]
        public void AddToCache_IncreasesCacheCount()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            Assert.AreEqual(1, _cacheManager.CacheCount);
        }

        [Test]
        public void TryGetCached_ReturnsTrue_WhenAssetExists()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            var result = _cacheManager.TryGetCached<Texture2D>("test_key", out var cachedHandle);

            Assert.IsTrue(result);
            Assert.IsNotNull(cachedHandle);
            Assert.AreEqual(_testTexture, cachedHandle.Asset);
        }

        [Test]
        public void TryGetCached_ReturnsFalse_WhenAssetNotExists()
        {
            var result = _cacheManager.TryGetCached<Texture2D>("nonexistent", out var cachedHandle);

            Assert.IsFalse(result);
            Assert.IsNull(cachedHandle);
        }

        [Test]
        public void TryGetCached_IncrementsRefCount()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            // Initial RefCount is 1
            Assert.AreEqual(1, handle.RefCount);

            _cacheManager.TryGetCached<Texture2D>("test_key", out var cachedHandle);

            // RefCount should be incremented
            Assert.AreEqual(2, cachedHandle.RefCount);
        }

        [Test]
        public void RemoveFromCache_DecreasesCacheCount()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            _cacheManager.RemoveFromCache("test_key");

            Assert.AreEqual(0, _cacheManager.CacheCount);
        }

        [Test]
        public void RemoveFromCache_ForceReleasesHandle()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            _cacheManager.RemoveFromCache("test_key");

            Assert.IsFalse(handle.IsValid);
        }

        [Test]
        public void ClearAll_RemovesAllEntries()
        {
            var texture1 = new Texture2D(1, 1);
            var texture2 = new Texture2D(1, 1);

            var handle1 = CreateHandle("key1", texture1);
            var handle2 = CreateHandle("key2", texture2);

            _cacheManager.AddToCache("key1", handle1, null);
            _cacheManager.AddToCache("key2", handle2, null);

            _cacheManager.ClearAll();

            Assert.AreEqual(0, _cacheManager.CacheCount);
            Assert.IsFalse(handle1.IsValid);
            Assert.IsFalse(handle2.IsValid);

            Object.DestroyImmediate(texture1);
            Object.DestroyImmediate(texture2);
        }

        [Test]
        public void OnHandleReleased_MarksAsReleasable()
        {
            var handle = CreateHandle("test_key", _testTexture);
            _cacheManager.AddToCache("test_key", handle, null);

            _cacheManager.OnHandleReleased(handle);

            // After OnHandleReleased, the entry should be marked as releasable
            // This is verified indirectly through LRU trimming behavior
            Assert.AreEqual(1, _cacheManager.CacheCount);
        }

        [Test]
        public void TrimCache_RemovesReleasableEntries_WhenOverLimit()
        {
            // Create cache with max size 2
            var smallCache = CreateCacheManager(2);

            var textures = new Texture2D[3];
            var handles = new AssetHandle<Texture2D>[3];

            for (int i = 0; i < 3; i++)
            {
                textures[i] = new Texture2D(1, 1);
                handles[i] = CreateHandle($"key{i}", textures[i]);
            }

            // Add first two items
            smallCache.AddToCache("key0", handles[0], null);
            smallCache.AddToCache("key1", handles[1], null);

            // Mark first item as releasable
            smallCache.OnHandleReleased(handles[0]);

            // Add third item - should trigger trim
            smallCache.AddToCache("key2", handles[2], null);

            // First item should be removed (was releasable)
            Assert.AreEqual(2, smallCache.CacheCount);
            Assert.IsFalse(smallCache.TryGetCached<Texture2D>("key0", out _));

            smallCache.ClearAll();
            foreach (var tex in textures)
            {
                Object.DestroyImmediate(tex);
            }
        }

        [Test]
        public void TryGetCached_ResetsIsReleasable()
        {
            var smallCache = CreateCacheManager(2);

            var texture1 = new Texture2D(1, 1);
            var texture2 = new Texture2D(1, 1);
            var texture3 = new Texture2D(1, 1);

            var handle1 = CreateHandle("key1", texture1);
            var handle2 = CreateHandle("key2", texture2);
            var handle3 = CreateHandle("key3", texture3);

            // Add two items
            smallCache.AddToCache("key1", handle1, null);
            smallCache.AddToCache("key2", handle2, null);

            // Mark first as releasable
            smallCache.OnHandleReleased(handle1);

            // Access first item again - should reset IsReleasable
            smallCache.TryGetCached<Texture2D>("key1", out _);

            // Add third item
            smallCache.AddToCache("key3", handle3, null);

            // key1 should NOT be removed because it was accessed (IsReleasable reset)
            // key2 is not releasable, so nothing gets trimmed
            Assert.AreEqual(3, smallCache.CacheCount);

            smallCache.ClearAll();
            Object.DestroyImmediate(texture1);
            Object.DestroyImmediate(texture2);
            Object.DestroyImmediate(texture3);
        }

        [Test]
        public void TryGetCached_UpdatesLruOrder()
        {
            var smallCache = CreateCacheManager(2);

            var texture1 = new Texture2D(1, 1);
            var texture2 = new Texture2D(1, 1);
            var texture3 = new Texture2D(1, 1);

            var handle1 = CreateHandle("key1", texture1);
            var handle2 = CreateHandle("key2", texture2);
            var handle3 = CreateHandle("key3", texture3);

            // Add two items
            smallCache.AddToCache("key1", handle1, null);
            smallCache.AddToCache("key2", handle2, null);

            // Mark both as releasable
            smallCache.OnHandleReleased(handle1);
            smallCache.OnHandleReleased(handle2);

            // Access key1 - moves it to front of LRU
            smallCache.TryGetCached<Texture2D>("key1", out _);

            // Add third item
            smallCache.AddToCache("key3", handle3, null);

            // key2 should be removed (oldest releasable after key1 was accessed)
            // But key1 is no longer releasable after TryGetCached
            Assert.IsFalse(smallCache.TryGetCached<Texture2D>("key2", out _));

            smallCache.ClearAll();
            Object.DestroyImmediate(texture1);
            Object.DestroyImmediate(texture2);
            Object.DestroyImmediate(texture3);
        }
    }
}
