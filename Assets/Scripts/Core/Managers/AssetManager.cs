using Cysharp.Threading.Tasks;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// Addressables + Resources 통합 에셋 관리자.
    /// Scope 기반 + LRU 캐싱 혼합 전략으로 메모리를 효율적으로 관리.
    /// </summary>
    public class AssetManager : Singleton<AssetManager>
    {
        [Header("캐시 설정")]
        [SerializeField] private int _maxCacheSize = 100;

        private AssetScopeManager _scopeManager;
        private AssetCacheManager _cacheManager;
        private AssetLoader _loader;
        private bool _isInitialized;

        /// <summary>
        /// 초기화 완료 여부
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 캐시된 에셋 수
        /// </summary>
        public int CacheCount => _cacheManager?.CacheCount ?? 0;

        /// <summary>
        /// 초기화 (GameBootstrap 최우선)
        /// </summary>
        public bool Initialize()
        {
            if (_isInitialized)
            {
                Debug.LogWarning("[AssetManager] 이미 초기화됨");
                return true;
            }

            _scopeManager = new AssetScopeManager();
            _cacheManager = new AssetCacheManager(_maxCacheSize);
            _loader = new AssetLoader();

            _isInitialized = true;
            Debug.Log("[AssetManager] 초기화 완료");
            return true;
        }

        #region Scope Management

        /// <summary>
        /// Scope 생성
        /// </summary>
        public AssetScope CreateScope(string name)
        {
            if (!_isInitialized)
            {
                Debug.LogError("[AssetManager] 초기화되지 않음");
                return null;
            }

            return _scopeManager.CreateScope(name);
        }

        /// <summary>
        /// Scope 조회
        /// </summary>
        public AssetScope GetScope(string name)
        {
            return _scopeManager?.GetScope(name);
        }

        /// <summary>
        /// Scope 전체 해제
        /// </summary>
        public void ReleaseScope(AssetScope scope)
        {
            if (scope == null) return;
            scope.Release();
        }

        /// <summary>
        /// Scope 이름으로 해제
        /// </summary>
        public void ReleaseScope(string scopeName)
        {
            _scopeManager?.ReleaseScope(scopeName);
        }

        #endregion

        #region Asset Loading

        /// <summary>
        /// 에셋 비동기 로드
        /// </summary>
        /// <param name="key">에셋 키 (Addressables 주소 또는 Resources 경로)</param>
        /// <param name="scope">Scope (null이면 전역 캐시)</param>
        /// <param name="timeout">타임아웃 (초)</param>
        public async UniTask<Result<AssetHandle<T>>> LoadAsync<T>(string key, AssetScope scope = null, float timeout = 30f)
            where T : UnityEngine.Object
        {
            if (!_isInitialized)
            {
                Debug.LogError("[AssetManager] 초기화되지 않음");
                return Result<AssetHandle<T>>.Failure(ErrorCode.SystemInitFailed);
            }

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[AssetManager] 에셋 키가 비어있음");
                return Result<AssetHandle<T>>.Failure(ErrorCode.AssetNotFound);
            }

            // 1. 캐시 확인
            if (_cacheManager.TryGetCached<T>(key, out var cachedHandle))
            {
                if (scope != null)
                {
                    scope.RegisterHandle(cachedHandle);
                }
                return Result<AssetHandle<T>>.Success(cachedHandle);
            }

            // 2. 새로 로드
            var loadResult = await _loader.LoadAsync<T>(key, timeout);

            if (!loadResult.IsSuccess)
            {
                Debug.LogWarning($"[AssetManager] 에셋 로드 실패: {key}");
                return Result<AssetHandle<T>>.Failure(loadResult.Error);
            }

            // 3. 핸들 생성 및 캐시 등록
            var handle = new AssetHandle<T>(key, loadResult.Asset, OnHandleReleased);
            _cacheManager.AddToCache(key, handle, loadResult.AddressableHandle);

            // 4. Scope에 등록
            if (scope != null)
            {
                scope.RegisterHandle(handle);
            }

            return Result<AssetHandle<T>>.Success(handle);
        }

        /// <summary>
        /// 에셋 해제
        /// </summary>
        public void Release<T>(AssetHandle<T> handle) where T : UnityEngine.Object
        {
            handle?.Release();
        }

        #endregion

        #region Cache Management

        /// <summary>
        /// 특정 에셋 캐시에서 제거
        /// </summary>
        public void Uncache(string key)
        {
            _cacheManager?.RemoveFromCache(key);
        }

        /// <summary>
        /// 전체 캐시 정리
        /// </summary>
        public void ClearCache()
        {
            _cacheManager?.ClearAll();
        }

        #endregion

        private void OnHandleReleased<T>(AssetHandle<T> handle) where T : UnityEngine.Object
        {
            _cacheManager?.OnHandleReleased(handle);
        }

        protected override void OnSingletonDestroy()
        {
            _scopeManager?.ReleaseAllScopes();
            _cacheManager?.ClearAll();
            _isInitialized = false;
        }
    }
}
