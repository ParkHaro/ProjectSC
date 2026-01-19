using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Sc.Common.UI;
using Sc.Tests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sc.Tests.PlayMode.Samples
{
    /// <summary>
    /// Navigation 시스템 PlayMode 테스트.
    /// 기존 NavigationTestScenarios를 NUnit 테스트로 래핑.
    /// </summary>
    [TestFixture]
    public class NavigationPlayModeTests : PlayModeTestBase
    {
        private NavigationManager _navigation;
        private NavigationTestScenarios _scenarios;

        protected override IEnumerator OnSetUp()
        {
            // NavigationManager 생성
            var navObj = new GameObject("NavigationManager");
            navObj.transform.SetParent(TestRoot.transform);
            _navigation = navObj.AddComponent<NavigationManager>();

            // 시나리오 헬퍼 초기화
            _scenarios = new NavigationTestScenarios(_navigation, TestCanvas.ScreenContainer);

            yield return null;
        }

        protected override IEnumerator OnTearDown()
        {
            // NavigationManager는 TestRoot와 함께 정리됨
            _navigation = null;
            _scenarios = null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator PushPop_ThreeScreens_CountsCorrectly()
        {
            // UniTask를 IEnumerator로 변환
            var task = _scenarios.RunPushPopAllScenario();

            while (!task.Status.IsCompleted())
            {
                yield return null;
            }

            var result = task.GetAwaiter().GetResult();
            Assert.That(result.Success, Is.True, result.Message);
        }

        [UnityTest]
        public IEnumerator Visibility_ScreenBPushed_ScreenAHidden()
        {
            var task = _scenarios.RunVisibilityScenario();

            while (!task.Status.IsCompleted())
            {
                yield return null;
            }

            var result = task.GetAwaiter().GetResult();
            Assert.That(result.Success, Is.True, result.Message);
        }

        [UnityTest]
        public IEnumerator PopupStack_PushPop_MaintainsScreenState()
        {
            var task = _scenarios.RunPopupStackScenario();

            while (!task.Status.IsCompleted())
            {
                yield return null;
            }

            var result = task.GetAwaiter().GetResult();
            Assert.That(result.Success, Is.True, result.Message);
        }

        [UnityTest]
        public IEnumerator BackNavigation_PopsProperly()
        {
            var task = _scenarios.RunBackNavigationScenario();

            while (!task.Status.IsCompleted())
            {
                yield return null;
            }

            var result = task.GetAwaiter().GetResult();
            Assert.That(result.Success, Is.True, result.Message);
        }

        [UnityTest]
        public IEnumerator DuplicateScreen_RemovesExisting()
        {
            var task = _scenarios.RunDuplicateScreenScenario();

            while (!task.Status.IsCompleted())
            {
                yield return null;
            }

            var result = task.GetAwaiter().GetResult();
            Assert.That(result.Success, Is.True, result.Message);
        }
    }
}
