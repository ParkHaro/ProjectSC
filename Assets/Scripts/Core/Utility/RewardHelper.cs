using System;
using System.Text;
using Sc.Data;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 보상 UI 헬퍼 (클라이언트)
    /// 포맷팅, 아이콘 경로, 희귀도 색상 등
    /// </summary>
    public static class RewardHelper
    {
        #region Text Formatting

        /// <summary>
        /// 보상 정보를 UI 표시용 텍스트로 변환
        /// </summary>
        public static string FormatText(RewardInfo reward)
        {
            string name = GetDisplayName(reward);
            return $"{name} x{reward.Amount:N0}";
        }

        /// <summary>
        /// 보상 목록을 UI 표시용 텍스트로 변환
        /// </summary>
        public static string FormatListText(RewardInfo[] rewards, string separator = "\n")
        {
            if (rewards == null || rewards.Length == 0)
                return string.Empty;

            var sb = new StringBuilder();
            for (int i = 0; i < rewards.Length; i++)
            {
                if (i > 0) sb.Append(separator);
                sb.Append(FormatText(rewards[i]));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 보상의 표시 이름 조회
        /// </summary>
        public static string GetDisplayName(RewardInfo reward)
        {
            switch (reward.Type)
            {
                case RewardType.Currency:
                    return GetCurrencyDisplayName(reward.ItemId);

                case RewardType.Character:
                    // TODO: CharacterDatabase에서 이름 조회
                    return reward.ItemId;

                case RewardType.Item:
                    // TODO: ItemDatabase에서 이름 조회
                    return reward.ItemId;

                case RewardType.PlayerExp:
                    return "플레이어 경험치";

                default:
                    return reward.ItemId;
            }
        }

        private static string GetCurrencyDisplayName(string itemId)
        {
            if (!Enum.TryParse<CostType>(itemId, out var costType))
                return itemId;

            return costType switch
            {
                CostType.Gold => "골드",
                CostType.Gem => "젬",
                CostType.Stamina => "스태미나",
                CostType.SummonTicket => "소환권",
                CostType.PickupSummonTicket => "픽업 소환권",
                CostType.CharacterExp => "캐릭터 경험치 재료",
                CostType.BreakthroughMaterial => "돌파 재료",
                CostType.SkillBook => "스킬북",
                CostType.EquipmentExp => "장비 경험치 재료",
                CostType.EnhanceStone => "강화석",
                CostType.ArenaTicket => "아레나 입장권",
                CostType.ArenaCoin => "아레나 코인",
                CostType.GuildCoin => "길드 코인",
                CostType.RaidCoin => "레이드 코인",
                CostType.SeasonCoin => "시즌 코인",
                CostType.EventCurrency => "이벤트 재화",
                _ => itemId
            };
        }

        #endregion

        #region Icon Path

        /// <summary>
        /// 보상 아이콘 경로 조회
        /// </summary>
        public static string GetIconPath(RewardInfo reward)
        {
            switch (reward.Type)
            {
                case RewardType.Currency:
                    return GetCurrencyIconPath(reward.ItemId);

                case RewardType.Character:
                    return $"Icons/Character/{reward.ItemId}";

                case RewardType.Item:
                    return $"Icons/Item/{reward.ItemId}";

                case RewardType.PlayerExp:
                    return "Icons/System/PlayerExp";

                default:
                    return "Icons/System/Unknown";
            }
        }

        private static string GetCurrencyIconPath(string itemId)
        {
            if (!Enum.TryParse<CostType>(itemId, out var costType))
                return "Icons/Currency/Unknown";

            return costType switch
            {
                CostType.Gold => "Icons/Currency/Gold",
                CostType.Gem => "Icons/Currency/Gem",
                CostType.Stamina => "Icons/Currency/Stamina",
                CostType.SummonTicket => "Icons/Currency/SummonTicket",
                CostType.PickupSummonTicket => "Icons/Currency/PickupSummonTicket",
                CostType.CharacterExp => "Icons/Currency/CharacterExp",
                CostType.BreakthroughMaterial => "Icons/Currency/BreakthroughMaterial",
                CostType.SkillBook => "Icons/Currency/SkillBook",
                CostType.EquipmentExp => "Icons/Currency/EquipmentExp",
                CostType.EnhanceStone => "Icons/Currency/EnhanceStone",
                CostType.ArenaTicket => "Icons/Currency/ArenaTicket",
                CostType.ArenaCoin => "Icons/Currency/ArenaCoin",
                CostType.GuildCoin => "Icons/Currency/GuildCoin",
                CostType.RaidCoin => "Icons/Currency/RaidCoin",
                CostType.SeasonCoin => "Icons/Currency/SeasonCoin",
                CostType.EventCurrency => "Icons/Currency/EventCurrency",
                _ => "Icons/Currency/Unknown"
            };
        }

        #endregion

        #region Rarity Color

        /// <summary>
        /// 보상 희귀도 색상 조회
        /// </summary>
        public static Color GetRarityColor(RewardInfo reward)
        {
            switch (reward.Type)
            {
                case RewardType.Currency:
                    return GetCurrencyRarityColor(reward.ItemId);

                case RewardType.Character:
                    // TODO: CharacterDatabase에서 희귀도 조회
                    return RarityColors.Epic; // 캐릭터는 기본 Epic

                case RewardType.Item:
                    // TODO: ItemDatabase에서 희귀도 조회
                    return RarityColors.Common;

                case RewardType.PlayerExp:
                    return RarityColors.Uncommon;

                default:
                    return RarityColors.Common;
            }
        }

        private static Color GetCurrencyRarityColor(string itemId)
        {
            if (!Enum.TryParse<CostType>(itemId, out var costType))
                return RarityColors.Common;

            return costType switch
            {
                CostType.Gem => RarityColors.Legendary,
                CostType.PickupSummonTicket => RarityColors.Epic,
                CostType.SummonTicket => RarityColors.Rare,
                CostType.Gold => RarityColors.Uncommon,
                _ => RarityColors.Common
            };
        }

        /// <summary>
        /// 희귀도에 따른 프레임 색상 조회
        /// </summary>
        public static Color GetFrameColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.N => RarityColors.Common,
                Rarity.R => RarityColors.Uncommon,
                Rarity.SR => RarityColors.Rare,
                Rarity.SSR => RarityColors.Legendary,
                _ => RarityColors.Common
            };
        }

        #endregion

        #region Rarity Colors

        /// <summary>
        /// 희귀도 색상 정의
        /// </summary>
        public static class RarityColors
        {
            public static readonly Color Common = new Color(0.7f, 0.7f, 0.7f);       // 회색
            public static readonly Color Uncommon = new Color(0.4f, 0.8f, 0.4f);     // 녹색
            public static readonly Color Rare = new Color(0.3f, 0.5f, 1.0f);         // 파랑
            public static readonly Color Epic = new Color(0.7f, 0.3f, 0.9f);         // 보라
            public static readonly Color Legendary = new Color(1.0f, 0.7f, 0.2f);    // 금색
        }

        #endregion

        #region Sorting

        /// <summary>
        /// 보상 정렬 (희귀도 순: 캐릭터 > 장비 > 재료 > 재화)
        /// </summary>
        public static void SortByRarity(RewardInfo[] rewards)
        {
            if (rewards == null || rewards.Length <= 1) return;

            Array.Sort(rewards, (a, b) =>
            {
                int priorityA = GetSortPriority(a);
                int priorityB = GetSortPriority(b);
                return priorityA.CompareTo(priorityB);
            });
        }

        private static int GetSortPriority(RewardInfo reward)
        {
            return reward.Type switch
            {
                RewardType.Character => 0,  // 최우선
                RewardType.Item => 1,
                RewardType.Currency => 2,
                RewardType.PlayerExp => 3,
                _ => 99
            };
        }

        #endregion
    }
}
