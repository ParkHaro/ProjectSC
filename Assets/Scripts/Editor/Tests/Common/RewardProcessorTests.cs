using System.Collections.Generic;
using NUnit.Framework;
using Sc.Core;
using Sc.Data;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// RewardProcessor 단위 테스트
    /// </summary>
    [TestFixture]
    public class RewardProcessorTests
    {
        private UserSaveData _testUserData;

        [SetUp]
        public void SetUp()
        {
            _testUserData = UserSaveData.CreateNew("test_uid", "TestUser");
        }

        #region CreateDelta - Empty/Null Tests

        [Test]
        public void CreateDelta_ReturnsEmptyDelta_WhenRewardsIsNull()
        {
            var delta = RewardProcessor.CreateDelta(null, _testUserData);

            Assert.That(delta.Currency, Is.Null);
            Assert.That(delta.AddedCharacters, Is.Null);
            Assert.That(delta.AddedItems, Is.Null);
        }

        [Test]
        public void CreateDelta_ReturnsEmptyDelta_WhenRewardsIsEmpty()
        {
            var delta = RewardProcessor.CreateDelta(new RewardInfo[0], _testUserData);

            Assert.That(delta.Currency, Is.Null);
            Assert.That(delta.AddedCharacters, Is.Null);
            Assert.That(delta.AddedItems, Is.Null);
        }

        #endregion

        #region CreateDelta - Currency Tests

        [Test]
        public void CreateDelta_AddsCurrency_ForGoldReward()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 1000) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Currency.HasValue, Is.True);
            Assert.That(delta.Currency.Value.Gold, Is.EqualTo(_testUserData.Currency.Gold + 1000));
        }

        [Test]
        public void CreateDelta_AddsCurrency_ForGemReward()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gem, 50) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Currency.HasValue, Is.True);
            Assert.That(delta.Currency.Value.Gem, Is.EqualTo(_testUserData.Currency.Gem + 50));
        }

        [Test]
        public void CreateDelta_AddsCurrency_ForMultipleCurrencyRewards()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 500),
                RewardInfo.Currency(CostType.Gem, 10),
                RewardInfo.Currency(CostType.SummonTicket, 3)
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Currency.HasValue, Is.True);
            Assert.That(delta.Currency.Value.Gold, Is.EqualTo(_testUserData.Currency.Gold + 500));
            Assert.That(delta.Currency.Value.Gem, Is.EqualTo(_testUserData.Currency.Gem + 10));
            Assert.That(delta.Currency.Value.SummonTicket, Is.EqualTo(_testUserData.Currency.SummonTicket + 3));
        }

        [Test]
        public void CreateDelta_ClampsStamina_ToMaxStamina()
        {
            // Stamina는 MaxStamina를 초과하지 않아야 함
            var rewards = new[] { RewardInfo.Currency(CostType.Stamina, 9999) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Currency.HasValue, Is.True);
            Assert.That(delta.Currency.Value.Stamina, Is.LessThanOrEqualTo(delta.Currency.Value.MaxStamina));
        }

        #endregion

        #region CreateDelta - Character Tests

        [Test]
        public void CreateDelta_AddsCharacter_ForCharacterReward()
        {
            var rewards = new[] { RewardInfo.Character("char_001") };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedCharacters, Is.Not.Null);
            Assert.That(delta.AddedCharacters.Count, Is.EqualTo(1));
            Assert.That(delta.AddedCharacters[0].CharacterId, Is.EqualTo("char_001"));
        }

        [Test]
        public void CreateDelta_AddsMultipleCharacters()
        {
            var rewards = new[]
            {
                RewardInfo.Character("char_001"),
                RewardInfo.Character("char_002")
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedCharacters.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateDelta_CreatesUniqueInstanceId_ForEachCharacter()
        {
            var rewards = new[]
            {
                RewardInfo.Character("char_001"),
                RewardInfo.Character("char_001") // 같은 캐릭터 2번 획득
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedCharacters.Count, Is.EqualTo(2));
            Assert.That(delta.AddedCharacters[0].InstanceId, Is.Not.EqualTo(delta.AddedCharacters[1].InstanceId));
        }

        #endregion

        #region CreateDelta - Item Tests

        [Test]
        public void CreateDelta_AddsItem_ForNewItem()
        {
            var rewards = new[] { RewardInfo.Item("item_potion", 5) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedItems, Is.Not.Null);
            Assert.That(delta.AddedItems.Count, Is.EqualTo(1));
            Assert.That(delta.AddedItems[0].ItemId, Is.EqualTo("item_potion"));
            Assert.That(delta.AddedItems[0].Count, Is.EqualTo(5));
        }

        [Test]
        public void CreateDelta_MergesItemCount_ForSameItem()
        {
            var rewards = new[]
            {
                RewardInfo.Item("item_material", 3),
                RewardInfo.Item("item_material", 2)
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedItems.Count, Is.EqualTo(1));
            Assert.That(delta.AddedItems[0].Count, Is.EqualTo(5));
        }

        [Test]
        public void CreateDelta_StacksWithExistingItem()
        {
            // 기존 아이템 추가
            _testUserData.Items = new List<OwnedItem>
            {
                OwnedItem.CreateConsumable("item_potion", 10)
            };

            var rewards = new[] { RewardInfo.Item("item_potion", 5) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.AddedItems.Count, Is.EqualTo(1));
            Assert.That(delta.AddedItems[0].Count, Is.EqualTo(15)); // 10 + 5
        }

        #endregion

        #region CreateDelta - PlayerExp Tests

        [Test]
        public void CreateDelta_AddsPlayerExp()
        {
            var rewards = new[] { RewardInfo.PlayerExp(500) };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Profile.HasValue, Is.True);
            Assert.That(delta.Profile.Value.Exp, Is.EqualTo(_testUserData.Profile.Exp + 500));
        }

        [Test]
        public void CreateDelta_AccumulatesPlayerExp()
        {
            var rewards = new[]
            {
                RewardInfo.PlayerExp(100),
                RewardInfo.PlayerExp(200)
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Profile.Value.Exp, Is.EqualTo(_testUserData.Profile.Exp + 300));
        }

        #endregion

        #region CreateDelta - Mixed Rewards Tests

        [Test]
        public void CreateDelta_HandlesAllRewardTypes_Together()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 1000),
                RewardInfo.Currency(CostType.Gem, 50),
                RewardInfo.Character("char_001"),
                RewardInfo.Item("item_001", 3),
                RewardInfo.PlayerExp(200)
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            Assert.That(delta.Currency.HasValue, Is.True);
            Assert.That(delta.AddedCharacters.Count, Is.EqualTo(1));
            Assert.That(delta.AddedItems.Count, Is.EqualTo(1));
            Assert.That(delta.Profile.HasValue, Is.True);
        }

        [Test]
        public void CreateDelta_IgnoresZeroAmountRewards()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 0),
                RewardInfo.Currency(CostType.Gem, 100)
            };

            var delta = RewardProcessor.CreateDelta(rewards, _testUserData);

            // Gold는 변하지 않아야 함 (0 보상은 무시)
            Assert.That(delta.Currency.Value.Gold, Is.EqualTo(_testUserData.Currency.Gold));
            Assert.That(delta.Currency.Value.Gem, Is.EqualTo(_testUserData.Currency.Gem + 100));
        }

        #endregion

        #region ValidateRewards Tests

        [Test]
        public void ValidateRewards_ReturnsFalse_WhenNull()
        {
            Assert.That(RewardProcessor.ValidateRewards(null), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsTrue_ForValidCurrencyReward()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 100) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.True);
        }

        [Test]
        public void ValidateRewards_ReturnsFalse_ForInvalidCurrencyId()
        {
            var rewards = new[] { new RewardInfo(RewardType.Currency, "InvalidCurrency", 100) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsFalse_ForZeroAmount()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 0) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsFalse_ForNegativeAmount()
        {
            var rewards = new[] { new RewardInfo(RewardType.Currency, "Gold", -100) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsFalse_ForEmptyItemId()
        {
            var rewards = new[] { new RewardInfo(RewardType.Item, "", 5) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsFalse_ForEmptyCharacterId()
        {
            var rewards = new[] { new RewardInfo(RewardType.Character, null, 1) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.False);
        }

        [Test]
        public void ValidateRewards_ReturnsTrue_ForPlayerExpWithEmptyItemId()
        {
            var rewards = new[] { RewardInfo.PlayerExp(500) };

            Assert.That(RewardProcessor.ValidateRewards(rewards), Is.True);
        }

        #endregion

        #region CanApplyRewards Tests

        [Test]
        public void CanApplyRewards_ReturnsTrue_WhenNull()
        {
            Assert.That(RewardProcessor.CanApplyRewards(null, _testUserData), Is.True);
        }

        [Test]
        public void CanApplyRewards_ReturnsTrue_ForCurrencyOnly()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 1000) };

            Assert.That(RewardProcessor.CanApplyRewards(rewards, _testUserData), Is.True);
        }

        [Test]
        public void CanApplyRewards_ReturnsTrue_WhenWithinLimit()
        {
            var rewards = new[] { RewardInfo.Character("char_001") };

            Assert.That(RewardProcessor.CanApplyRewards(rewards, _testUserData, maxInventorySlots: 100), Is.True);
        }

        [Test]
        public void CanApplyRewards_ReturnsFalse_WhenExceedsLimit()
        {
            // 인벤토리 꽉 채우기
            _testUserData.Characters = new List<OwnedCharacter>();
            for (int i = 0; i < 10; i++)
            {
                _testUserData.Characters.Add(OwnedCharacter.Create($"char_{i:D3}"));
            }

            var rewards = new[] { RewardInfo.Character("char_new") };

            Assert.That(RewardProcessor.CanApplyRewards(rewards, _testUserData, maxInventorySlots: 10), Is.False);
        }

        [Test]
        public void CanApplyRewards_DoesNotCountExistingItems()
        {
            // 기존 아이템이 있을 때 같은 아이템 보상은 슬롯을 차지하지 않음
            _testUserData.Items = new List<OwnedItem>
            {
                OwnedItem.CreateConsumable("item_potion", 10)
            };

            var rewards = new[] { RewardInfo.Item("item_potion", 5) };

            Assert.That(RewardProcessor.CanApplyRewards(rewards, _testUserData, maxInventorySlots: 1), Is.True);
        }

        #endregion
    }
}
