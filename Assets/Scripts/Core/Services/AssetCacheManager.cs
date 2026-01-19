using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sc.Core
{
    /// <summary>
    /// LRU 캐시 관리.
    /// RefCount == 0인 에셋만 해제 대상.
    /// </summary>
    internal class AssetCacheManager
    {
        private readonly Dictionary<string, CacheEntry> _cache = new();
        private readonly LinkedList<string> _lruOrder = new();
        private readonly int _maxCacheSize;

        public AssetCacheManager(int maxCacheSize = 100)
        {
            _maxCacheSize = maxCacheSize;
        }

        /// <summary>
        /// 캐시된 에셋 수
        /// </summary>
        public int CacheCount => _cache.Count;

        /// <summary>
        /// 캐시에서 에셋 조회
        /// </summary>
        public bool TryGetCached<T>(string key, out AssetHandle<T> handle) where T : UnityEngine.Object
        {
            if (_cache.TryGetValue(key, out var entry) && entry.Handle is AssetHandle<T> typedHandle)
            {
                typedHandle.AddRef();
                UpdateLruOrder(key);
                handle = typedHandle;
                return true;
            }

            handle = null;
            return false;
        }

        /// <summary>
        /// 캐시에 에셋 등록
        /// </summary>
        public void AddToCache<T>(string key, AssetHandle<T> handle, AsyncOperationHandle<T>? addressableHandle)
            where T : UnityEngine.Object
        {
            var entry = new CacheEntry
            {
                Key = key,
                Handle = handle,
                AddressableHandle = addressableHandle.HasValue ? (AsyncOperationHandle?)addressableHandle.Value : null,
                LastAccessTime = Time.realtimeSinceStartup
            };

            _cache[key] = entry;
            _lruOrder.AddFirst(key);

            TrimCache();
        }

        /// <summary>
        /// 핸들 해제 콜백 (RefCount == 0일 때)
        /// </summary>
        public void OnHandleReleased<T>(AssetHandle<T> handle) where T : UnityEngine.Object
        {
            if (_cache.TryGetValue(handle.Key, out var entry))
            {
                // RefCount가 0이면 LRU 대상으로 표시
                entry.IsReleasable = true;
                entry.LastAccessTime = Time.realtimeSinceStartup;
            }
        }

        /// <summary>
        /// 캐시에서 즉시 제거
        /// </summary>
        public void RemoveFromCache(string key)
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                ReleaseEntry(entry);
                _cache.Remove(key);
                _lruOrder.Remove(key);
            }
        }

        /// <summary>
        /// 모든 캐시 정리
        /// </summary>
        public void ClearAll()
        {
            foreach (var entry in _cache.Values)
            {
                ReleaseEntry(entry);
            }
            _cache.Clear();
            _lruOrder.Clear();
        }

        private void UpdateLruOrder(string key)
        {
            _lruOrder.Remove(key);
            _lruOrder.AddFirst(key);

            if (_cache.TryGetValue(key, out var entry))
            {
                entry.LastAccessTime = Time.realtimeSinceStartup;
            }
        }

        private void TrimCache()
        {
            // 임계값 초과 시 해제 가능한 항목부터 제거
            while (_cache.Count > _maxCacheSize && _lruOrder.Count > 0)
            {
                var oldestKey = _lruOrder.Last?.Value;
                if (oldestKey == null) break;

                if (_cache.TryGetValue(oldestKey, out var entry) && entry.IsReleasable)
                {
                    ReleaseEntry(entry);
                    _cache.Remove(oldestKey);
                    _lruOrder.RemoveLast();
                    Debug.Log($"[AssetCacheManager] LRU 해제: {oldestKey}");
                }
                else
                {
                    // 가장 오래된 항목이 해제 불가능하면 중단
                    break;
                }
            }
        }

        private void ReleaseEntry(CacheEntry entry)
        {
            // AssetHandle 강제 해제
            var forceReleaseMethod = entry.Handle?.GetType().GetMethod("ForceRelease",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            forceReleaseMethod?.Invoke(entry.Handle, null);

            // Addressables 핸들 해제
            if (entry.AddressableHandle.HasValue && entry.AddressableHandle.Value.IsValid())
            {
                Addressables.Release(entry.AddressableHandle.Value);
            }
        }

        private class CacheEntry
        {
            public string Key { get; set; }
            public object Handle { get; set; }
            public AsyncOperationHandle? AddressableHandle { get; set; }
            public float LastAccessTime { get; set; }
            public bool IsReleasable { get; set; }
        }
    }
}
