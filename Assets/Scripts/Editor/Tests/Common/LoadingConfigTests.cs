using NUnit.Framework;
using Sc.Common.UI;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// LoadingConfig 단위 테스트
    /// </summary>
    [TestFixture]
    public class LoadingConfigTests
    {
        #region CreateDefault Tests

        [Test]
        public void CreateDefault_ReturnsNonNullConfig()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config, Is.Not.Null);
        }

        [Test]
        public void CreateDefault_TimeoutSeconds_Is30()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config.TimeoutSeconds, Is.EqualTo(30f));
        }

        [Test]
        public void CreateDefault_FadeInDuration_Is02()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config.FadeInDuration, Is.EqualTo(0.2f));
        }

        [Test]
        public void CreateDefault_FadeOutDuration_Is015()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config.FadeOutDuration, Is.EqualTo(0.15f));
        }

        [Test]
        public void CreateDefault_SpinnerSpeed_Is360()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config.SpinnerSpeed, Is.EqualTo(360f));
        }

        [Test]
        public void CreateDefault_OverlayAlpha_Is08()
        {
            var config = LoadingConfig.CreateDefault();

            Assert.That(config.OverlayAlpha, Is.EqualTo(0.8f));
        }

        #endregion
    }
}
