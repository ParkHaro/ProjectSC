using System;
using System.Collections.Generic;
using Sc.Data;
using Sc.Packet;

namespace Sc.Core
{
    /// <summary>
    /// 보상 처리 유틸리티 (서버 로직)
    /// RewardInfo[]를 UserDataDelta로 변환
    /// </summary>
    public static class RewardProcessor
    {
        /// <summary>
        /// 보상 목록을 Delta로 변환
        /// </summary>
        /// <param name="rewards">지급할 보상 목록</param>
        /// <param name="currentData">현재 유저 데이터</param>
        /// <returns>적용할 Delta</returns>
        public static UserDataDelta CreateDelta(RewardInfo[] rewards, UserSaveData currentData)
        {
            if (rewards == null || rewards.Length == 0)
                return UserDataDelta.Empty();

            var delta = new UserDataDelta();
            var newCurrency = currentData.Currency;
            var newProfile = currentData.Profile;
            var addedCharacters = new List<OwnedCharacter>();
            var addedItems = new List<OwnedItem>();

            bool currencyChanged = false;
            bool profileChanged = false;

            foreach (var reward in rewards)
            {
                if (reward.Amount <= 0) continue;

                switch (reward.Type)
                {
                    case RewardType.Currency:
                        ApplyCurrencyReward(ref newCurrency, reward);
                        currencyChanged = true;
                        break;

                    case RewardType.Character:
                        var character = CreateCharacterReward(reward);
                        addedCharacters.Add(character);
                        break;

                    case RewardType.Item:
                        var item = CreateOrUpdateItemReward(reward, currentData, addedItems);
                        if (item.HasValue)
                            addedItems.Add(item.Value);
                        break;

                    case RewardType.PlayerExp:
                        newProfile.Exp += reward.Amount;
                        profileChanged = true;
                        // TODO: 레벨업 처리 로직 추가 가능
                        break;
                }
            }

            if (currencyChanged)
                delta.Currency = newCurrency;

            if (profileChanged)
                delta.Profile = newProfile;

            if (addedCharacters.Count > 0)
                delta.AddedCharacters = addedCharacters;

            if (addedItems.Count > 0)
                delta.AddedItems = addedItems;

            return delta;
        }

        /// <summary>
        /// 보상 유효성 검증
        /// </summary>
        public static bool ValidateRewards(RewardInfo[] rewards)
        {
            if (rewards == null) return false;

            foreach (var reward in rewards)
            {
                if (reward.Amount <= 0)
                    return false;

                switch (reward.Type)
                {
                    case RewardType.Currency:
                        if (!Enum.TryParse<CostType>(reward.ItemId, out _))
                            return false;
                        break;

                    case RewardType.Character:
                    case RewardType.Item:
                        if (string.IsNullOrEmpty(reward.ItemId))
                            return false;
                        break;

                    case RewardType.PlayerExp:
                        // ItemId는 비어있어도 됨
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 보상 지급 가능 여부 확인 (인벤토리 공간 등)
        /// </summary>
        public static bool CanApplyRewards(RewardInfo[] rewards, UserSaveData currentData, int maxInventorySlots = 500)
        {
            if (rewards == null) return true;

            int newItemCount = 0;
            foreach (var reward in rewards)
            {
                if (reward.Type == RewardType.Item)
                {
                    // 기존 아이템이 아닌 새 아이템인 경우 슬롯 필요
                    var existingItem = currentData.FindItemById(reward.ItemId);
                    if (!existingItem.HasValue)
                        newItemCount++;
                }
                else if (reward.Type == RewardType.Character)
                {
                    // 캐릭터는 항상 새 슬롯 필요 (중복 캐릭터도 별도 인스턴스)
                    newItemCount++;
                }
            }

            int currentItemCount = (currentData.Items?.Count ?? 0) + (currentData.Characters?.Count ?? 0);
            return (currentItemCount + newItemCount) <= maxInventorySlots;
        }

        #region Private Helpers

        private static void ApplyCurrencyReward(ref UserCurrency currency, RewardInfo reward)
        {
            if (!Enum.TryParse<CostType>(reward.ItemId, out var costType))
                return;

            switch (costType)
            {
                // 기본 재화
                case CostType.Gold:
                    currency.Gold += reward.Amount;
                    break;
                case CostType.Gem:
                    currency.Gem += reward.Amount;
                    break;
                case CostType.Stamina:
                    currency.Stamina = Math.Min(currency.Stamina + reward.Amount, currency.MaxStamina);
                    break;

                // 소환 재화
                case CostType.SummonTicket:
                    currency.SummonTicket += reward.Amount;
                    break;
                case CostType.PickupSummonTicket:
                    currency.PickupSummonTicket += reward.Amount;
                    break;

                // 캐릭터 육성 재화
                case CostType.CharacterExp:
                    currency.CharacterExpMaterial += reward.Amount;
                    break;
                case CostType.BreakthroughMaterial:
                    currency.BreakthroughMaterial += reward.Amount;
                    break;
                case CostType.SkillBook:
                    currency.SkillBook += reward.Amount;
                    break;

                // 장비 육성 재화
                case CostType.EquipmentExp:
                    currency.EquipmentExpMaterial += reward.Amount;
                    break;
                case CostType.EnhanceStone:
                    currency.EnhanceStone += reward.Amount;
                    break;

                // 컨텐츠 재화
                case CostType.ArenaTicket:
                    currency.ArenaTicket += reward.Amount;
                    break;
                case CostType.ArenaCoin:
                    currency.ArenaCoin += reward.Amount;
                    break;
                case CostType.GuildCoin:
                    currency.GuildCoin += reward.Amount;
                    break;
                case CostType.RaidCoin:
                    currency.RaidCoin += reward.Amount;
                    break;

                // 시즌 재화
                case CostType.SeasonCoin:
                    currency.SeasonCoin += reward.Amount;
                    break;

                // 이벤트 재화는 별도 처리 필요
                case CostType.EventCurrency:
                case CostType.None:
                default:
                    break;
            }
        }

        private static OwnedCharacter CreateCharacterReward(RewardInfo reward)
        {
            return OwnedCharacter.Create(reward.ItemId);
        }

        private static OwnedItem? CreateOrUpdateItemReward(
            RewardInfo reward,
            UserSaveData currentData,
            List<OwnedItem> pendingItems)
        {
            // 이미 pending 목록에 같은 아이템이 있는지 확인
            for (int i = 0; i < pendingItems.Count; i++)
            {
                if (pendingItems[i].ItemId == reward.ItemId && !pendingItems[i].IsEquipment)
                {
                    var existingItem = pendingItems[i];
                    existingItem.Count += reward.Amount;
                    pendingItems[i] = existingItem;
                    return null; // 이미 업데이트됨
                }
            }

            // 기존 인벤토리에 있는지 확인
            var currentItem = currentData.FindItemById(reward.ItemId);
            if (currentItem.HasValue)
            {
                // 기존 아이템 수량 증가
                var updated = currentItem.Value;
                updated.Count += reward.Amount;
                return updated;
            }

            // 새 아이템 생성 (소모품으로 가정)
            return OwnedItem.CreateConsumable(reward.ItemId, reward.Amount);
        }

        #endregion
    }
}
