using System;
using NUnit.Framework;
using Sc.Common.UI;
using Sc.Data;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// RewardPopup.State 단위 테스트
    /// </summary>
    [TestFixture]
    public class RewardPopupStateTests
    {
        #region Default Values Tests

        [Test]
        public void DefaultValues_AreSetCorrectly()
        {
            var state = new RewardPopup.State();

            Assert.That(state.Title, Is.EqualTo("획득 보상"));
            Assert.That(state.Rewards, Is.Not.Null);
            Assert.That(state.Rewards, Is.Empty);
            Assert.That(state.OnClose, Is.Null);
            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        #endregion

        #region Property Setting Tests

        [Test]
        public void Title_CanBeSet()
        {
            var state = new RewardPopup.State { Title = "스테이지 클리어 보상" };

            Assert.That(state.Title, Is.EqualTo("스테이지 클리어 보상"));
        }

        [Test]
        public void Rewards_CanBeSet()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 1000),
                RewardInfo.Currency(CostType.Gem, 50)
            };

            var state = new RewardPopup.State { Rewards = rewards };

            Assert.That(state.Rewards.Length, Is.EqualTo(2));
            Assert.That(state.Rewards[0].Type, Is.EqualTo(RewardType.Currency));
            Assert.That(state.Rewards[0].Amount, Is.EqualTo(1000));
        }

        #endregion

        #region Callback Tests

        [Test]
        public void OnClose_CanBeSet()
        {
            bool called = false;
            var state = new RewardPopup.State { OnClose = () => called = true };

            state.OnClose?.Invoke();

            Assert.That(called, Is.True);
        }

        #endregion

        #region Validate Tests

        [Test]
        public void Validate_WithValidRewards_ReturnsTrue()
        {
            var state = new RewardPopup.State
            {
                Rewards = new[] { RewardInfo.Currency(CostType.Gold, 100) }
            };

            var result = state.Validate();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Validate_WithNullRewards_ReturnsFalse()
        {
            var state = new RewardPopup.State { Rewards = null };

            var result = state.Validate();

            Assert.That(result, Is.False);
        }

        [Test]
        public void Validate_WithEmptyRewards_ReturnsFalse()
        {
            var state = new RewardPopup.State { Rewards = Array.Empty<RewardInfo>() };

            var result = state.Validate();

            Assert.That(result, Is.False);
        }

        [Test]
        public void Validate_WithMultipleRewards_ReturnsTrue()
        {
            var state = new RewardPopup.State
            {
                Rewards = new[]
                {
                    RewardInfo.Currency(CostType.Gold, 1000),
                    RewardInfo.Currency(CostType.Gem, 50),
                    RewardInfo.Item("item_001", 3),
                    RewardInfo.Character("char_001")
                }
            };

            var result = state.Validate();

            Assert.That(result, Is.True);
        }

        #endregion

        #region AllowBackgroundDismiss Tests

        [Test]
        public void AllowBackgroundDismiss_DefaultsToTrue()
        {
            var state = new RewardPopup.State();

            Assert.That(state.AllowBackgroundDismiss, Is.True);
        }

        [Test]
        public void State_ImplementsIPopupState()
        {
            var state = new RewardPopup.State();

            Assert.That(state, Is.InstanceOf<IPopupState>());
        }

        #endregion

        #region Usage Pattern Tests

        [Test]
        public void TypicalUsage_StageReward()
        {
            var state = new RewardPopup.State
            {
                Title = "스테이지 1-1 클리어",
                Rewards = new[]
                {
                    RewardInfo.Currency(CostType.Gold, 500),
                    RewardInfo.PlayerExp(100)
                }
            };

            Assert.That(state.Validate(), Is.True);
            Assert.That(state.Title, Is.EqualTo("스테이지 1-1 클리어"));
            Assert.That(state.Rewards.Length, Is.EqualTo(2));
        }

        [Test]
        public void TypicalUsage_GachaReward()
        {
            var state = new RewardPopup.State
            {
                Title = "소환 결과",
                Rewards = new[]
                {
                    RewardInfo.Character("char_hero_001"),
                    RewardInfo.Character("char_hero_002"),
                    RewardInfo.Item("item_piece_001", 30)
                }
            };

            Assert.That(state.Validate(), Is.True);
            Assert.That(state.Rewards.Length, Is.EqualTo(3));
        }

        #endregion
    }
}
