using NUnit.Framework;
using Sc.Data;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// RewardInfo 구조체 단위 테스트
    /// </summary>
    [TestFixture]
    public class RewardInfoTests
    {
        #region Constructor Tests

        [Test]
        public void Constructor_SetsAllFields_Correctly()
        {
            var reward = new RewardInfo(RewardType.Currency, "Gold", 100);

            Assert.That(reward.Type, Is.EqualTo(RewardType.Currency));
            Assert.That(reward.ItemId, Is.EqualTo("Gold"));
            Assert.That(reward.Amount, Is.EqualTo(100));
        }

        #endregion

        #region Factory Method Tests - Currency

        [Test]
        public void Currency_CreatesReward_WithCorrectType()
        {
            var reward = RewardInfo.Currency(CostType.Gold, 1000);

            Assert.That(reward.Type, Is.EqualTo(RewardType.Currency));
        }

        [Test]
        public void Currency_SetsItemId_ToCostTypeString()
        {
            var reward = RewardInfo.Currency(CostType.Gem, 50);

            Assert.That(reward.ItemId, Is.EqualTo("Gem"));
        }

        [Test]
        public void Currency_SetsAmount_Correctly()
        {
            var reward = RewardInfo.Currency(CostType.Stamina, 200);

            Assert.That(reward.Amount, Is.EqualTo(200));
        }

        [Test]
        public void Currency_WorksWithAllCostTypes()
        {
            var costTypes = new[]
            {
                CostType.Gold, CostType.Gem, CostType.Stamina,
                CostType.SummonTicket, CostType.ArenaCoin
            };

            foreach (var costType in costTypes)
            {
                var reward = RewardInfo.Currency(costType, 100);
                Assert.That(reward.ItemId, Is.EqualTo(costType.ToString()));
            }
        }

        #endregion

        #region Factory Method Tests - Item

        [Test]
        public void Item_CreatesReward_WithCorrectType()
        {
            var reward = RewardInfo.Item("item_potion_hp", 5);

            Assert.That(reward.Type, Is.EqualTo(RewardType.Item));
        }

        [Test]
        public void Item_SetsItemId_Correctly()
        {
            var reward = RewardInfo.Item("item_sword_001", 1);

            Assert.That(reward.ItemId, Is.EqualTo("item_sword_001"));
        }

        [Test]
        public void Item_SetsAmount_Correctly()
        {
            var reward = RewardInfo.Item("item_material", 99);

            Assert.That(reward.Amount, Is.EqualTo(99));
        }

        #endregion

        #region Factory Method Tests - Character

        [Test]
        public void Character_CreatesReward_WithCorrectType()
        {
            var reward = RewardInfo.Character("char_001");

            Assert.That(reward.Type, Is.EqualTo(RewardType.Character));
        }

        [Test]
        public void Character_SetsItemId_ToCharacterId()
        {
            var reward = RewardInfo.Character("char_hero_001");

            Assert.That(reward.ItemId, Is.EqualTo("char_hero_001"));
        }

        [Test]
        public void Character_SetsAmount_ToOne()
        {
            var reward = RewardInfo.Character("char_001");

            Assert.That(reward.Amount, Is.EqualTo(1));
        }

        #endregion

        #region Factory Method Tests - PlayerExp

        [Test]
        public void PlayerExp_CreatesReward_WithCorrectType()
        {
            var reward = RewardInfo.PlayerExp(500);

            Assert.That(reward.Type, Is.EqualTo(RewardType.PlayerExp));
        }

        [Test]
        public void PlayerExp_SetsItemId_ToEmpty()
        {
            var reward = RewardInfo.PlayerExp(100);

            Assert.That(reward.ItemId, Is.EqualTo(string.Empty));
        }

        [Test]
        public void PlayerExp_SetsAmount_Correctly()
        {
            var reward = RewardInfo.PlayerExp(9999);

            Assert.That(reward.Amount, Is.EqualTo(9999));
        }

        #endregion

        #region ToString Tests

        [Test]
        public void ToString_FormatsCorrectly_ForCurrency()
        {
            var reward = RewardInfo.Currency(CostType.Gold, 1000);

            var result = reward.ToString();

            Assert.That(result, Is.EqualTo("[Currency] Gold x1000"));
        }

        [Test]
        public void ToString_FormatsCorrectly_ForItem()
        {
            var reward = RewardInfo.Item("item_001", 5);

            var result = reward.ToString();

            Assert.That(result, Is.EqualTo("[Item] item_001 x5"));
        }

        [Test]
        public void ToString_FormatsCorrectly_ForCharacter()
        {
            var reward = RewardInfo.Character("char_001");

            var result = reward.ToString();

            Assert.That(result, Is.EqualTo("[Character] char_001 x1"));
        }

        [Test]
        public void ToString_FormatsCorrectly_ForPlayerExp()
        {
            var reward = RewardInfo.PlayerExp(500);

            var result = reward.ToString();

            Assert.That(result, Is.EqualTo("[PlayerExp]  x500"));
        }

        #endregion
    }
}
