using NUnit.Framework;
using Sc.Core;
using Sc.Data;
using UnityEngine;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// RewardHelper 단위 테스트
    /// </summary>
    [TestFixture]
    public class RewardHelperTests
    {
        #region FormatText Tests

        [Test]
        public void FormatText_FormatsCurrency_WithDisplayName()
        {
            var reward = RewardInfo.Currency(CostType.Gold, 1000);

            var result = RewardHelper.FormatText(reward);

            Assert.That(result, Is.EqualTo("골드 x1,000"));
        }

        [Test]
        public void FormatText_FormatsGem()
        {
            var reward = RewardInfo.Currency(CostType.Gem, 50);

            var result = RewardHelper.FormatText(reward);

            Assert.That(result, Is.EqualTo("젬 x50"));
        }

        [Test]
        public void FormatText_FormatsPlayerExp()
        {
            var reward = RewardInfo.PlayerExp(500);

            var result = RewardHelper.FormatText(reward);

            Assert.That(result, Is.EqualTo("플레이어 경험치 x500"));
        }

        [Test]
        public void FormatText_UsesItemId_ForUnknownItem()
        {
            var reward = RewardInfo.Item("item_potion_001", 5);

            var result = RewardHelper.FormatText(reward);

            Assert.That(result, Is.EqualTo("item_potion_001 x5"));
        }

        [Test]
        public void FormatText_UsesCharacterId_ForUnknownCharacter()
        {
            var reward = RewardInfo.Character("char_hero_001");

            var result = RewardHelper.FormatText(reward);

            Assert.That(result, Is.EqualTo("char_hero_001 x1"));
        }

        #endregion

        #region FormatListText Tests

        [Test]
        public void FormatListText_ReturnsEmpty_WhenNull()
        {
            var result = RewardHelper.FormatListText(null);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FormatListText_ReturnsEmpty_WhenEmpty()
        {
            var result = RewardHelper.FormatListText(new RewardInfo[0]);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FormatListText_FormatsSingleReward()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 100) };

            var result = RewardHelper.FormatListText(rewards);

            Assert.That(result, Is.EqualTo("골드 x100"));
        }

        [Test]
        public void FormatListText_JoinsMultipleRewards_WithNewline()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 100),
                RewardInfo.Currency(CostType.Gem, 10)
            };

            var result = RewardHelper.FormatListText(rewards);

            Assert.That(result, Contains.Substring("골드 x100"));
            Assert.That(result, Contains.Substring("젬 x10"));
            Assert.That(result, Contains.Substring("\n"));
        }

        [Test]
        public void FormatListText_UsesCustomSeparator()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 100),
                RewardInfo.Currency(CostType.Gem, 10)
            };

            var result = RewardHelper.FormatListText(rewards, ", ");

            Assert.That(result, Is.EqualTo("골드 x100, 젬 x10"));
        }

        #endregion

        #region GetIconPath Tests

        [Test]
        public void GetIconPath_ReturnsCorrectPath_ForGold()
        {
            var reward = RewardInfo.Currency(CostType.Gold, 100);

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/Currency/Gold"));
        }

        [Test]
        public void GetIconPath_ReturnsCorrectPath_ForGem()
        {
            var reward = RewardInfo.Currency(CostType.Gem, 50);

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/Currency/Gem"));
        }

        [Test]
        public void GetIconPath_ReturnsCharacterPath_ForCharacter()
        {
            var reward = RewardInfo.Character("char_001");

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/Character/char_001"));
        }

        [Test]
        public void GetIconPath_ReturnsItemPath_ForItem()
        {
            var reward = RewardInfo.Item("item_sword", 1);

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/Item/item_sword"));
        }

        [Test]
        public void GetIconPath_ReturnsPlayerExpPath()
        {
            var reward = RewardInfo.PlayerExp(500);

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/System/PlayerExp"));
        }

        [Test]
        public void GetIconPath_ReturnsUnknownPath_ForInvalidCurrency()
        {
            var reward = new RewardInfo(RewardType.Currency, "InvalidType", 100);

            var result = RewardHelper.GetIconPath(reward);

            Assert.That(result, Is.EqualTo("Icons/Currency/Unknown"));
        }

        #endregion

        #region GetRarityColor Tests

        [Test]
        public void GetRarityColor_ReturnsLegendary_ForGem()
        {
            var reward = RewardInfo.Currency(CostType.Gem, 100);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Legendary));
        }

        [Test]
        public void GetRarityColor_ReturnsEpic_ForPickupSummonTicket()
        {
            var reward = RewardInfo.Currency(CostType.PickupSummonTicket, 1);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Epic));
        }

        [Test]
        public void GetRarityColor_ReturnsRare_ForSummonTicket()
        {
            var reward = RewardInfo.Currency(CostType.SummonTicket, 1);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Rare));
        }

        [Test]
        public void GetRarityColor_ReturnsUncommon_ForGold()
        {
            var reward = RewardInfo.Currency(CostType.Gold, 1000);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Uncommon));
        }

        [Test]
        public void GetRarityColor_ReturnsEpic_ForCharacter()
        {
            var reward = RewardInfo.Character("char_001");

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Epic));
        }

        [Test]
        public void GetRarityColor_ReturnsCommon_ForItem()
        {
            var reward = RewardInfo.Item("item_001", 1);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Common));
        }

        [Test]
        public void GetRarityColor_ReturnsUncommon_ForPlayerExp()
        {
            var reward = RewardInfo.PlayerExp(500);

            var result = RewardHelper.GetRarityColor(reward);

            Assert.That(result, Is.EqualTo(RewardHelper.RarityColors.Uncommon));
        }

        #endregion

        #region GetFrameColor Tests

        [Test]
        public void GetFrameColor_ReturnsCorrectColor_ForEachRarity()
        {
            Assert.That(RewardHelper.GetFrameColor(Rarity.N), Is.EqualTo(RewardHelper.RarityColors.Common));
            Assert.That(RewardHelper.GetFrameColor(Rarity.R), Is.EqualTo(RewardHelper.RarityColors.Uncommon));
            Assert.That(RewardHelper.GetFrameColor(Rarity.SR), Is.EqualTo(RewardHelper.RarityColors.Rare));
            Assert.That(RewardHelper.GetFrameColor(Rarity.SSR), Is.EqualTo(RewardHelper.RarityColors.Legendary));
        }

        #endregion

        #region SortByRarity Tests

        [Test]
        public void SortByRarity_DoesNotThrow_WhenNull()
        {
            Assert.DoesNotThrow(() => RewardHelper.SortByRarity(null));
        }

        [Test]
        public void SortByRarity_DoesNotThrow_WhenSingleItem()
        {
            var rewards = new[] { RewardInfo.Currency(CostType.Gold, 100) };

            Assert.DoesNotThrow(() => RewardHelper.SortByRarity(rewards));
        }

        [Test]
        public void SortByRarity_SortsCharacterFirst()
        {
            var rewards = new[]
            {
                RewardInfo.Currency(CostType.Gold, 100),
                RewardInfo.Character("char_001"),
                RewardInfo.Item("item_001", 1)
            };

            RewardHelper.SortByRarity(rewards);

            Assert.That(rewards[0].Type, Is.EqualTo(RewardType.Character));
        }

        [Test]
        public void SortByRarity_SortsInCorrectOrder()
        {
            var rewards = new[]
            {
                RewardInfo.PlayerExp(100),
                RewardInfo.Currency(CostType.Gold, 100),
                RewardInfo.Item("item_001", 1),
                RewardInfo.Character("char_001")
            };

            RewardHelper.SortByRarity(rewards);

            Assert.That(rewards[0].Type, Is.EqualTo(RewardType.Character));
            Assert.That(rewards[1].Type, Is.EqualTo(RewardType.Item));
            Assert.That(rewards[2].Type, Is.EqualTo(RewardType.Currency));
            Assert.That(rewards[3].Type, Is.EqualTo(RewardType.PlayerExp));
        }

        #endregion

        #region RarityColors Tests

        [Test]
        public void RarityColors_AreDistinct()
        {
            var colors = new[]
            {
                RewardHelper.RarityColors.Common,
                RewardHelper.RarityColors.Uncommon,
                RewardHelper.RarityColors.Rare,
                RewardHelper.RarityColors.Epic,
                RewardHelper.RarityColors.Legendary
            };

            // 모든 색상이 서로 다른지 확인
            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = i + 1; j < colors.Length; j++)
                {
                    Assert.That(colors[i], Is.Not.EqualTo(colors[j]),
                        $"Color at index {i} should not equal color at index {j}");
                }
            }
        }

        [Test]
        public void RarityColors_HaveValidRGBValues()
        {
            var colors = new[]
            {
                RewardHelper.RarityColors.Common,
                RewardHelper.RarityColors.Uncommon,
                RewardHelper.RarityColors.Rare,
                RewardHelper.RarityColors.Epic,
                RewardHelper.RarityColors.Legendary
            };

            foreach (var color in colors)
            {
                Assert.That(color.r, Is.InRange(0f, 1f));
                Assert.That(color.g, Is.InRange(0f, 1f));
                Assert.That(color.b, Is.InRange(0f, 1f));
            }
        }

        #endregion
    }
}
