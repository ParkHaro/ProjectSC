using NUnit.Framework;
using Sc.Common.UI;
using UnityEngine;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// LoadingService 단위 테스트
    /// </summary>
    [TestFixture]
    public class LoadingServiceTests
    {
        private LoadingService _service;
        private GameObject _serviceObject;

        [SetUp]
        public void SetUp()
        {
            _serviceObject = new GameObject("TestLoadingService");
            _service = _serviceObject.AddComponent<LoadingService>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceObject != null)
            {
                Object.DestroyImmediate(_serviceObject);
            }
        }

        #region Reference Counting Tests

        [Test]
        public void Show_IncreasesRefCount()
        {
            _service.Show();

            Assert.That(_service.RefCount, Is.EqualTo(1));
            Assert.That(_service.IsLoading, Is.True);
        }

        [Test]
        public void Show_MultipleCalls_IncreasesRefCountAccordingly()
        {
            _service.Show();
            _service.Show();
            _service.Show();

            Assert.That(_service.RefCount, Is.EqualTo(3));
            Assert.That(_service.IsLoading, Is.True);
        }

        [Test]
        public void Hide_DecreasesRefCount()
        {
            _service.Show();
            _service.Show();

            _service.Hide();

            Assert.That(_service.RefCount, Is.EqualTo(1));
            Assert.That(_service.IsLoading, Is.True);
        }

        [Test]
        public void Hide_WhenRefCountReachesZero_IsLoadingBecomesFalse()
        {
            _service.Show();
            _service.Hide();

            Assert.That(_service.RefCount, Is.EqualTo(0));
            Assert.That(_service.IsLoading, Is.False);
        }

        [Test]
        public void Hide_WhenAlreadyZero_DoesNotGoNegative()
        {
            _service.Hide();

            Assert.That(_service.RefCount, Is.EqualTo(0));
            Assert.That(_service.IsLoading, Is.False);
        }

        [Test]
        public void ForceHide_ResetsRefCountToZero()
        {
            _service.Show();
            _service.Show();
            _service.Show();

            _service.ForceHide();

            Assert.That(_service.RefCount, Is.EqualTo(0));
            Assert.That(_service.IsLoading, Is.False);
        }

        #endregion

        #region LoadingType Tests

        [Test]
        public void Show_SetsCurrentType()
        {
            _service.Show(LoadingType.FullScreen);

            Assert.That(_service.CurrentType, Is.EqualTo(LoadingType.FullScreen));
        }

        [Test]
        public void ShowProgress_SetsCurrentTypeToProgress()
        {
            _service.ShowProgress(0.5f, "테스트");

            Assert.That(_service.CurrentType, Is.EqualTo(LoadingType.Progress));
        }

        #endregion

        #region Initial State Tests

        [Test]
        public void InitialState_RefCountIsZero()
        {
            Assert.That(_service.RefCount, Is.EqualTo(0));
        }

        [Test]
        public void InitialState_IsLoadingIsFalse()
        {
            Assert.That(_service.IsLoading, Is.False);
        }

        #endregion

        #region Nested Calls Tests

        [Test]
        public void NestedCalls_ShowHidePattern_WorksCorrectly()
        {
            // 시스템 A 시작
            _service.Show();
            Assert.That(_service.RefCount, Is.EqualTo(1));

            // 시스템 B 시작 (중첩)
            _service.Show();
            Assert.That(_service.RefCount, Is.EqualTo(2));

            // 시스템 B 종료
            _service.Hide();
            Assert.That(_service.RefCount, Is.EqualTo(1));
            Assert.That(_service.IsLoading, Is.True);

            // 시스템 A 종료
            _service.Hide();
            Assert.That(_service.RefCount, Is.EqualTo(0));
            Assert.That(_service.IsLoading, Is.False);
        }

        #endregion
    }
}
