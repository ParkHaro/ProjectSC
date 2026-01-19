using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sc.Tests;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Sc.Tests.PlayMode
{
    /// <summary>
    /// PlayMode 테스트 베이스 클래스.
    /// Addressables 초기화, 테스트 씬 로드, 자동 정리 제공.
    /// </summary>
    public abstract class PlayModeTestBase
    {
        protected const string TEST_SCENE_PATH = "Assets/Scenes/Test/Main.unity";

        protected GameObject TestRoot { get; private set; }
        protected TestCanvas TestCanvas { get; private set; }
        protected PrefabTestHelper PrefabHelper { get; private set; }

        private List<AsyncOperationHandle> _handles = new();
        private bool _addressablesInitialized;

        [UnitySetUp]
        public IEnumerator BaseSetUp()
        {
            // 1. Addressables 초기화
            yield return InitializeAddressables();

            // 2. 테스트 루트 생성
            TestRoot = new GameObject($"[TEST] {GetType().Name}");

            // 3. 테스트 Canvas 생성 (기존 Helper 재사용)
            TestCanvas = TestCanvasFactory.Create(TestRoot.transform);

            // 4. PrefabHelper 초기화
            PrefabHelper = new PrefabTestHelper();

            // 5. 하위 클래스 Setup
            yield return OnSetUp();
        }

        [UnityTearDown]
        public IEnumerator BaseTearDown()
        {
            // 1. 하위 클래스 Teardown
            yield return OnTearDown();

            // 2. PrefabHelper 정리
            PrefabHelper?.CleanupAll();
            PrefabHelper = null;

            // 3. 테스트 Canvas 정리
            TestCanvas.Destroy();

            // 4. 테스트 루트 제거
            if (TestRoot != null)
            {
                Object.Destroy(TestRoot);
                TestRoot = null;
            }

            // 5. Addressables 핸들 해제
            ReleaseAllHandles();

            yield return null;
        }

        /// <summary>
        /// 하위 클래스 Setup (선택적 오버라이드)
        /// </summary>
        protected virtual IEnumerator OnSetUp()
        {
            yield return null;
        }

        /// <summary>
        /// 하위 클래스 Teardown (선택적 오버라이드)
        /// </summary>
        protected virtual IEnumerator OnTearDown()
        {
            yield return null;
        }

        /// <summary>
        /// Addressables 초기화 여부
        /// </summary>
        protected bool IsAddressablesReady => _addressablesInitialized;

        /// <summary>
        /// Addressables 초기화 (실패 시에도 테스트 계속 진행)
        /// </summary>
        protected IEnumerator InitializeAddressables()
        {
            if (_addressablesInitialized)
            {
                yield break;
            }

            // Addressables 설정 파일 존재 확인
            var settingsPath = "Library/com.unity.addressables/aa/Windows/settings.json";
            if (!System.IO.File.Exists(settingsPath))
            {
                Debug.LogWarning($"[PlayModeTest] Addressables 미빌드. 스킵: {settingsPath}");
                yield break;
            }

            // 초기화 상태 추적 변수
            bool completed = false;
            bool success = false;
            System.Exception initException = null;

            // 콜백 핸들러 - 초기화 완료 시 호출됨
            void OnInitComplete(AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> op)
            {
                try
                {
                    success = op.Status == AsyncOperationStatus.Succeeded;
                }
                catch (System.Exception e)
                {
                    initException = e;
                    success = false;
                }
                finally
                {
                    completed = true;
                }
            }

            // Addressables 초기화 시작
            try
            {
                var handle = Addressables.InitializeAsync();
                handle.Completed += OnInitComplete;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[PlayModeTest] Addressables 초기화 실패: {e.Message}");
                yield break;
            }

            // 완료 대기 (최대 10초)
            float timeout = 10f;
            float elapsed = 0f;
            while (!completed && elapsed < timeout)
            {
                yield return null;
                elapsed += Time.unscaledDeltaTime;
            }

            // 결과 처리
            if (initException != null)
            {
                Debug.LogWarning($"[PlayModeTest] Addressables 초기화 예외: {initException.Message}");
            }
            else if (success)
            {
                _addressablesInitialized = true;
                Debug.Log("[PlayModeTest] Addressables 초기화 완료");
            }
            else if (!completed)
            {
                Debug.LogWarning("[PlayModeTest] Addressables 초기화 타임아웃");
            }
            else
            {
                Debug.LogWarning("[PlayModeTest] Addressables 초기화 실패");
            }
        }

        /// <summary>
        /// Addressables로 에셋 로드 (핸들 자동 추적)
        /// </summary>
        protected IEnumerator LoadAssetAsync<T>(string key, System.Action<T> onLoaded)
        {
            if (!_addressablesInitialized)
            {
                Debug.LogWarning($"[PlayModeTest] Addressables 미초기화. 에셋 로드 스킵: {key}");
                Assert.Ignore("Addressables not initialized");
                yield break;
            }

            T result = default;
            bool completed = false;
            bool success = false;

            var handle = Addressables.LoadAssetAsync<T>(key);
            _handles.Add(handle);

            handle.Completed += op =>
            {
                success = op.Status == AsyncOperationStatus.Succeeded;
                if (success) result = op.Result;
                completed = true;
            };

            while (!completed)
            {
                yield return null;
            }

            if (success)
            {
                onLoaded?.Invoke(result);
            }
            else
            {
                Debug.LogError($"[PlayModeTest] 에셋 로드 실패: {key}");
                Assert.Fail($"Failed to load asset: {key}");
            }
        }

        /// <summary>
        /// Addressables로 프리팹 인스턴스화
        /// </summary>
        protected IEnumerator InstantiateAsync(string key, Transform parent, System.Action<GameObject> onInstantiated)
        {
            if (!_addressablesInitialized)
            {
                Debug.LogWarning($"[PlayModeTest] Addressables 미초기화. 인스턴스화 스킵: {key}");
                Assert.Ignore("Addressables not initialized");
                yield break;
            }

            GameObject result = null;
            bool completed = false;
            bool success = false;

            var handle = Addressables.InstantiateAsync(key, parent);
            _handles.Add(handle);

            handle.Completed += op =>
            {
                success = op.Status == AsyncOperationStatus.Succeeded;
                if (success) result = op.Result;
                completed = true;
            };

            while (!completed)
            {
                yield return null;
            }

            if (success)
            {
                onInstantiated?.Invoke(result);
            }
            else
            {
                Debug.LogError($"[PlayModeTest] 프리팹 인스턴스화 실패: {key}");
                Assert.Fail($"Failed to instantiate prefab: {key}");
            }
        }

        /// <summary>
        /// 1프레임 대기
        /// </summary>
        protected IEnumerator WaitFrame(int frames = 1)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 초 단위 대기
        /// </summary>
        protected IEnumerator WaitSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        /// <summary>
        /// 조건 충족까지 대기 (타임아웃 포함)
        /// </summary>
        protected IEnumerator WaitUntil(System.Func<bool> condition, float timeoutSeconds = 5f)
        {
            float elapsed = 0f;
            while (!condition() && elapsed < timeoutSeconds)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }

            if (!condition())
            {
                Assert.Fail($"조건 충족 실패 (타임아웃: {timeoutSeconds}초)");
            }
        }

        private void ReleaseAllHandles()
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
    }
}
