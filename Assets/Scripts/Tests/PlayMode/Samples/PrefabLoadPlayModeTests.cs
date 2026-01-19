using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Sc.Tests.PlayMode.Samples
{
    /// <summary>
    /// Addressables 프리팹 로드 테스트 샘플.
    /// 실제 프리팹 구조 검증 예시.
    ///
    /// 사용 전 설정:
    /// 1. Addressables Groups에 프리팹 추가
    /// 2. 아래 KEY 상수를 실제 Addressable 키로 변경
    /// 3. Window > Asset Management > Addressables > Build 실행
    /// </summary>
    [TestFixture]
    public class PrefabLoadPlayModeTests : PlayModeTestBase
    {
        // Addressables 키 상수 (프로젝트에 맞게 수정 필요)
        // 예: "Assets/Prefabs/UI/Popup/SystemPopup.prefab" 또는 커스텀 키
        private const string SYSTEM_POPUP_KEY = "Prefabs/UI/Popup/SystemPopup";
        private const string REWARD_POPUP_KEY = "Prefabs/UI/Popup/RewardPopup";

        /// <summary>
        /// Addressable 키 존재 확인 (테스트 스킵 여부 결정)
        /// </summary>
        private IEnumerator CheckKeyExists(string key, System.Action<bool> onResult)
        {
            if (!IsAddressablesReady)
            {
                onResult?.Invoke(false);
                yield break;
            }

            bool completed = false;
            bool exists = false;

            var locationsHandle = Addressables.LoadResourceLocationsAsync(key);
            locationsHandle.Completed += op =>
            {
                exists = op.Status == AsyncOperationStatus.Succeeded && op.Result.Count > 0;
                completed = true;
            };

            while (!completed)
            {
                yield return null;
            }

            Addressables.Release(locationsHandle);
            onResult?.Invoke(exists);
        }

        /// <summary>
        /// 테스트 전 Addressable 키 검증. 없으면 스킵.
        /// </summary>
        private IEnumerator SkipIfKeyNotExists(string key)
        {
            bool keyExists = false;
            yield return CheckKeyExists(key, exists => keyExists = exists);

            if (!keyExists)
            {
                Assert.Ignore($"Addressable 키 미등록으로 테스트 스킵: {key}");
            }
        }

        [UnityTest]
        public IEnumerator SystemPopup_LoadsSuccessfully()
        {
            yield return SkipIfKeyNotExists(SYSTEM_POPUP_KEY);

            GameObject loadedPrefab = null;

            yield return LoadAssetAsync<GameObject>(SYSTEM_POPUP_KEY, prefab =>
            {
                loadedPrefab = prefab;
            });

            // 프리팹 존재 확인
            Assert.That(loadedPrefab, Is.Not.Null, "SystemPopup 프리팹 로드 실패");

            // 프리팹 이름 확인
            Assert.That(loadedPrefab.name, Does.Contain("SystemPopup"));
        }

        [UnityTest]
        public IEnumerator SystemPopup_HasRequiredComponents()
        {
            yield return SkipIfKeyNotExists(SYSTEM_POPUP_KEY);

            GameObject instance = null;

            yield return InstantiateAsync(SYSTEM_POPUP_KEY, TestCanvas.PopupContainer, go =>
            {
                instance = go;
            });

            // 인스턴스 확인
            PlayModeAssert.IsValid(instance, "SystemPopup 인스턴스 생성 실패");
            PlayModeAssert.IsActive(instance);

            // RectTransform 확인
            var rectTransform = PlayModeAssert.HasComponent<RectTransform>(instance);
            Assert.That(rectTransform, Is.Not.Null);

            // CanvasGroup 확인 (UI 가시성 제어용)
            PlayModeAssert.HasComponent<CanvasGroup>(instance);
        }

        [UnityTest]
        public IEnumerator SystemPopup_HasCorrectHierarchy()
        {
            yield return SkipIfKeyNotExists(SYSTEM_POPUP_KEY);

            GameObject instance = null;

            yield return InstantiateAsync(SYSTEM_POPUP_KEY, TestCanvas.PopupContainer, go =>
            {
                instance = go;
            });

            PlayModeAssert.IsValid(instance);

            // 필수 자식 요소 확인 (예시)
            var transform = instance.transform;

            // Background 확인
            var background = transform.Find("Background");
            Assert.That(background, Is.Not.Null, "Background 요소 없음");

            // Content 영역 확인
            var content = transform.Find("Content");
            Assert.That(content, Is.Not.Null, "Content 영역 없음");
        }

        [UnityTest]
        public IEnumerator PrefabHelper_TracksInstances()
        {
            yield return SkipIfKeyNotExists(SYSTEM_POPUP_KEY);

            // 초기 상태 확인
            Assert.That(PrefabHelper.InstanceCount, Is.EqualTo(0));

            GameObject instance1 = null;

            // 첫 번째 인스턴스
            yield return InstantiateAsync(SYSTEM_POPUP_KEY, TestCanvas.PopupContainer, go =>
            {
                instance1 = go;
            });

            // PrefabHelper는 InstantiateAsync 완료 후 인스턴스 추적
            yield return WaitFrame(1);

            // 두 번째 인스턴스 (다른 방식으로 테스트)
            GameObject prefab = null;
            yield return LoadAssetAsync<GameObject>(SYSTEM_POPUP_KEY, p =>
            {
                prefab = p;
            });

            var instance2 = PrefabHelper.Instantiate(prefab, TestCanvas.PopupContainer);

            // 인스턴스 수 확인
            Assert.That(PrefabHelper.InstanceCount, Is.GreaterThanOrEqualTo(1));

            // 컴포넌트 찾기 테스트
            var canvasGroups = PrefabHelper.FindAllComponents<CanvasGroup>();
            Assert.That(canvasGroups.Count, Is.GreaterThanOrEqualTo(1));
        }

        [UnityTest]
        public IEnumerator MultiplePopups_LoadInParallel()
        {
            // 키 존재 확인
            bool systemKeyExists = false;
            bool rewardKeyExists = false;

            yield return CheckKeyExists(SYSTEM_POPUP_KEY, exists => systemKeyExists = exists);
            yield return CheckKeyExists(REWARD_POPUP_KEY, exists => rewardKeyExists = exists);

            if (!systemKeyExists && !rewardKeyExists)
            {
                Assert.Ignore("SystemPopup, RewardPopup 키 모두 미등록으로 테스트 스킵");
                yield break;
            }

            // 여러 프리팹 동시 로드 테스트
            GameObject systemPopup = null;
            GameObject rewardPopup = null;
            bool systemLoaded = false;
            bool rewardLoaded = false;

            // 두 프리팹 로드 시작 (존재하는 것만)
            AsyncOperationHandle<GameObject> handle1 = default;
            AsyncOperationHandle<GameObject> handle2 = default;

            if (systemKeyExists)
            {
                handle1 = PrefabHelper.InstantiateAsync(SYSTEM_POPUP_KEY, TestCanvas.PopupContainer);
            }
            if (rewardKeyExists)
            {
                handle2 = PrefabHelper.InstantiateAsync(REWARD_POPUP_KEY, TestCanvas.PopupContainer);
            }

            // 완료 대기
            if (systemKeyExists)
            {
                yield return handle1;
                if (handle1.Status == AsyncOperationStatus.Succeeded)
                {
                    systemPopup = handle1.Result;
                    systemLoaded = true;
                }
            }

            if (rewardKeyExists)
            {
                yield return handle2;
                if (handle2.Status == AsyncOperationStatus.Succeeded)
                {
                    rewardPopup = handle2.Result;
                    rewardLoaded = true;
                }
            }

            // 최소 하나는 성공해야 함
            Assert.That(systemLoaded || rewardLoaded, Is.True,
                "SystemPopup 또는 RewardPopup 중 하나는 로드되어야 함");
        }

        [UnityTest]
        public IEnumerator Cleanup_RemovesAllInstances()
        {
            yield return SkipIfKeyNotExists(SYSTEM_POPUP_KEY);

            // 인스턴스 생성
            yield return InstantiateAsync(SYSTEM_POPUP_KEY, TestCanvas.PopupContainer, _ => { });
            yield return WaitFrame(1);

            int countBefore = PrefabHelper.InstanceCount;

            // 정리
            PrefabHelper.CleanupAll();

            int countAfter = PrefabHelper.InstanceCount;

            // 모든 인스턴스가 정리되었는지 확인
            Assert.That(countAfter, Is.EqualTo(0));
            Assert.That(countBefore, Is.GreaterThan(countAfter));
        }
    }
}
