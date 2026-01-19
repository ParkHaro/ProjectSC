using System;
using Cysharp.Threading.Tasks;
using Sc.Foundation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sc.Core
{
    /// <summary>
    /// Addressables + Resources 통합 로더.
    /// Addressables 실패 시 Resources 폴백.
    /// </summary>
    internal class AssetLoader
    {
        /// <summary>
        /// 에셋 비동기 로드 (Addressables 우선, Resources 폴백)
        /// </summary>
        public async UniTask<LoadResult<T>> LoadAsync<T>(string key, float timeout = 30f)
            where T : UnityEngine.Object
        {
            // 1. Addressables 시도
            try
            {
                var addressableResult = await LoadFromAddressablesAsync<T>(key, timeout);
                if (addressableResult.IsSuccess)
                {
                    return addressableResult;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[AssetLoader] Addressables 로드 실패: {key}, {e.Message}");
            }

            // 2. Resources 폴백
            try
            {
                var resourceResult = await LoadFromResourcesAsync<T>(key);
                if (resourceResult.IsSuccess)
                {
                    return resourceResult;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[AssetLoader] Resources 로드 실패: {key}, {e.Message}");
            }

            // 3. 둘 다 실패
            return new LoadResult<T>
            {
                IsSuccess = false,
                Asset = null,
                AddressableHandle = null,
                Error = ErrorCode.AssetNotFound
            };
        }

        private async UniTask<LoadResult<T>> LoadFromAddressablesAsync<T>(string key, float timeout)
            where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);

            var timeoutTask = UniTask.Delay(TimeSpan.FromSeconds(timeout));
            var loadTask = handle.ToUniTask();

            var (hasResult, _) = await UniTask.WhenAny(loadTask, timeoutTask);

            if (!hasResult) // Timeout (timeoutTask 먼저 완료)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
                return new LoadResult<T>
                {
                    IsSuccess = false,
                    Asset = null,
                    AddressableHandle = null,
                    Error = ErrorCode.AssetLoadTimeout
                };
            }

            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                return new LoadResult<T>
                {
                    IsSuccess = true,
                    Asset = handle.Result,
                    AddressableHandle = handle,
                    Error = ErrorCode.None
                };
            }

            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }

            return new LoadResult<T>
            {
                IsSuccess = false,
                Asset = null,
                AddressableHandle = null,
                Error = ErrorCode.AssetNotFound
            };
        }

        private async UniTask<LoadResult<T>> LoadFromResourcesAsync<T>(string key)
            where T : UnityEngine.Object
        {
            // Resources 경로 변환 (Addressables 키에서 Resources 경로로)
            var resourcePath = ConvertToResourcePath(key);
            var request = Resources.LoadAsync<T>(resourcePath);

            await request.ToUniTask();

            if (request.asset is T asset)
            {
                return new LoadResult<T>
                {
                    IsSuccess = true,
                    Asset = asset,
                    AddressableHandle = null, // Resources는 핸들 없음
                    Error = ErrorCode.None
                };
            }

            return new LoadResult<T>
            {
                IsSuccess = false,
                Asset = null,
                AddressableHandle = null,
                Error = ErrorCode.AssetNotFound
            };
        }

        private string ConvertToResourcePath(string addressableKey)
        {
            // 예: "Assets/Textures/Icons/icon_gold.png" → "Icons/icon_gold"
            // 또는 "icon_gold" → "icon_gold"
            var path = addressableKey;

            // Assets/ 접두사 제거
            if (path.StartsWith("Assets/"))
            {
                path = path.Substring(7);
            }

            // Resources/ 접두사 제거
            if (path.Contains("Resources/"))
            {
                var idx = path.IndexOf("Resources/");
                path = path.Substring(idx + 10);
            }

            // 확장자 제거
            var lastDot = path.LastIndexOf('.');
            if (lastDot > 0)
            {
                path = path.Substring(0, lastDot);
            }

            return path;
        }

        /// <summary>
        /// 로드 결과
        /// </summary>
        public struct LoadResult<T> where T : UnityEngine.Object
        {
            public bool IsSuccess;
            public T Asset;
            public AsyncOperationHandle<T>? AddressableHandle;
            public ErrorCode Error;
        }
    }
}
