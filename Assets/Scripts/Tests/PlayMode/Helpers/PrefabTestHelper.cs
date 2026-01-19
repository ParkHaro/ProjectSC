using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sc.Tests.PlayMode
{
    /// <summary>
    /// PlayMode 테스트용 프리팹 로드/인스턴스 관리 헬퍼.
    /// Addressables 비동기 로드, 인스턴스 생성, 자동 정리 제공.
    /// </summary>
    public class PrefabTestHelper
    {
        private readonly List<AsyncOperationHandle> _handles = new();
        private readonly List<GameObject> _instances = new();

        /// <summary>
        /// 생성된 인스턴스 수
        /// </summary>
        public int InstanceCount => _instances.Count;

        /// <summary>
        /// 로드된 핸들 수
        /// </summary>
        public int HandleCount => _handles.Count;

        /// <summary>
        /// Addressables로 프리팹 로드 (인스턴스화 하지 않음)
        /// </summary>
        public AsyncOperationHandle<GameObject> LoadPrefabAsync(string addressableKey)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(addressableKey);
            _handles.Add(handle);
            return handle;
        }

        /// <summary>
        /// Addressables로 프리팹 로드 후 인스턴스 생성
        /// </summary>
        public AsyncOperationHandle<GameObject> InstantiateAsync(string addressableKey, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(addressableKey, parent);
            handle.Completed += OnInstantiateCompleted;
            _handles.Add(handle);
            return handle;
        }

        /// <summary>
        /// 이미 로드된 프리팹에서 인스턴스 생성 (동기)
        /// </summary>
        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            var instance = UnityEngine.Object.Instantiate(prefab, parent);
            _instances.Add(instance);
            return instance;
        }

        /// <summary>
        /// 이미 로드된 프리팹에서 인스턴스 생성 (위치 지정)
        /// </summary>
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var instance = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
            _instances.Add(instance);
            return instance;
        }

        /// <summary>
        /// 특정 컴포넌트가 있는 인스턴스 생성
        /// </summary>
        public T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component
        {
            var instance = Instantiate(prefab, parent);
            return instance.GetComponent<T>();
        }

        /// <summary>
        /// 특정 인스턴스만 제거
        /// </summary>
        public void DestroyInstance(GameObject instance)
        {
            if (instance == null) return;

            if (_instances.Remove(instance))
            {
                UnityEngine.Object.Destroy(instance);
            }
        }

        /// <summary>
        /// 모든 인스턴스 제거
        /// </summary>
        public void DestroyAllInstances()
        {
            foreach (var instance in _instances)
            {
                if (instance != null)
                {
                    UnityEngine.Object.Destroy(instance);
                }
            }
            _instances.Clear();
        }

        /// <summary>
        /// 모든 Addressables 핸들 해제
        /// </summary>
        public void ReleaseAllHandles()
        {
            foreach (var handle in _handles)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
            _handles.Clear();
        }

        /// <summary>
        /// 모든 리소스 정리 (인스턴스 + 핸들)
        /// </summary>
        public void CleanupAll()
        {
            DestroyAllInstances();
            ReleaseAllHandles();
        }

        /// <summary>
        /// 인스턴스에서 컴포넌트 찾기
        /// </summary>
        public T FindComponent<T>(int instanceIndex = 0) where T : Component
        {
            if (instanceIndex < 0 || instanceIndex >= _instances.Count)
            {
                return null;
            }

            return _instances[instanceIndex]?.GetComponent<T>();
        }

        /// <summary>
        /// 모든 인스턴스에서 특정 컴포넌트 찾기
        /// </summary>
        public List<T> FindAllComponents<T>() where T : Component
        {
            var result = new List<T>();
            foreach (var instance in _instances)
            {
                if (instance == null) continue;

                var component = instance.GetComponent<T>();
                if (component != null)
                {
                    result.Add(component);
                }
            }
            return result;
        }

        /// <summary>
        /// 인스턴스 목록 조회
        /// </summary>
        public IReadOnlyList<GameObject> GetInstances() => _instances.AsReadOnly();

        private void OnInstantiateCompleted(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                _instances.Add(handle.Result);
            }
        }
    }
}
