using NUnit.Framework;
using Sc.Common.UI;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// IPopupState 인터페이스 단위 테스트
    /// </summary>
    [TestFixture]
    public class IPopupStateTests
    {
        // 기본 구현체 (AllowBackgroundDismiss 기본값 사용)
        private class DefaultState : IPopupState { }

        // 커스텀 구현체 (AllowBackgroundDismiss 오버라이드)
        private class NoBackgroundDismissState : IPopupState
        {
            public bool AllowBackgroundDismiss => false;
        }

        // 조건부 구현체
        private class ConditionalState : IPopupState
        {
            public bool IsImportant { get; set; }
            public bool AllowBackgroundDismiss => !IsImportant;
        }

        #region Default Implementation Tests

        [Test]
        public void AllowBackgroundDismiss_DefaultValue_IsTrue()
        {
            IPopupState state = new DefaultState();

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        [Test]
        public void AllowBackgroundDismiss_CanBeOverridden_ToFalse()
        {
            IPopupState state = new NoBackgroundDismissState();

            Assert.That(state.AllowBackgroundDismiss, Is.False);
        }

        #endregion

        #region Conditional Override Tests

        [Test]
        public void AllowBackgroundDismiss_Conditional_WhenNotImportant_ReturnsTrue()
        {
            var state = new ConditionalState { IsImportant = false };

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        [Test]
        public void AllowBackgroundDismiss_Conditional_WhenImportant_ReturnsFalse()
        {
            var state = new ConditionalState { IsImportant = true };

            Assert.That(state.AllowBackgroundDismiss, Is.False);
        }

        #endregion

        #region Existing State Compatibility Tests

        [Test]
        public void ConfirmState_AllowBackgroundDismiss_DefaultsToTrue()
        {
            IPopupState state = new ConfirmState();

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        [Test]
        public void CostConfirmState_AllowBackgroundDismiss_DefaultsToTrue()
        {
            IPopupState state = new CostConfirmState();

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        [Test]
        public void RewardPopupState_AllowBackgroundDismiss_DefaultsToTrue()
        {
            IPopupState state = new RewardPopup.State();

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        #endregion
    }
}
