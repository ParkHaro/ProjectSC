using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 통합 유저 저장 데이터
    /// </summary>
    [Serializable]
    public struct UserSaveData
    {
        /// <summary>
        /// 데이터 버전 (마이그레이션용)
        /// </summary>
        public int Version;

        /// <summary>
        /// 유저 프로필
        /// </summary>
        public UserProfile Profile;

        /// <summary>
        /// 유저 재화
        /// </summary>
        public UserCurrency Currency;

        /// <summary>
        /// 보유 캐릭터 목록
        /// </summary>
        public List<OwnedCharacter> Characters;

        /// <summary>
        /// 보유 아이템 목록
        /// </summary>
        public List<OwnedItem> Items;

        /// <summary>
        /// 스테이지 진행 데이터
        /// </summary>
        public StageProgress StageProgress;

        /// <summary>
        /// 가챠 천장 데이터
        /// </summary>
        public GachaPityData GachaPity;

        /// <summary>
        /// 퀘스트 진행 데이터
        /// </summary>
        public QuestProgress QuestProgress;

        /// <summary>
        /// 마지막 동기화 시간 (Unix Timestamp)
        /// </summary>
        public long LastSyncAt;

        /// <summary>
        /// 현재 데이터 버전
        /// </summary>
        public const int CurrentVersion = 1;

        /// <summary>
        /// 신규 유저 데이터 생성
        /// </summary>
        public static UserSaveData CreateNew(string uid, string nickname)
        {
            return new UserSaveData
            {
                Version = CurrentVersion,
                Profile = UserProfile.CreateDefault(uid, nickname),
                Currency = UserCurrency.CreateDefault(),
                Characters = new List<OwnedCharacter>(),
                Items = new List<OwnedItem>(),
                StageProgress = StageProgress.CreateDefault(),
                GachaPity = GachaPityData.CreateDefault(),
                QuestProgress = QuestProgress.CreateDefault(),
                LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 캐릭터 ID로 보유 캐릭터 조회
        /// </summary>
        public OwnedCharacter? FindCharacterById(string characterId)
        {
            if (Characters == null) return null;
            foreach (var character in Characters)
            {
                if (character.CharacterId == characterId)
                    return character;
            }
            return null;
        }

        /// <summary>
        /// 캐릭터 인스턴스 ID로 보유 캐릭터 조회
        /// </summary>
        public OwnedCharacter? FindCharacterByInstanceId(string instanceId)
        {
            if (Characters == null) return null;
            foreach (var character in Characters)
            {
                if (character.InstanceId == instanceId)
                    return character;
            }
            return null;
        }

        /// <summary>
        /// 아이템 ID로 보유 아이템 조회 (소모품용)
        /// </summary>
        public OwnedItem? FindItemById(string itemId)
        {
            if (Items == null) return null;
            foreach (var item in Items)
            {
                if (item.ItemId == itemId && !item.IsEquipment)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 아이템 인스턴스 ID로 보유 아이템 조회 (장비용)
        /// </summary>
        public OwnedItem? FindItemByInstanceId(string instanceId)
        {
            if (Items == null) return null;
            foreach (var item in Items)
            {
                if (item.InstanceId == instanceId)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 캐릭터 보유 여부 확인
        /// </summary>
        public bool HasCharacter(string characterId)
        {
            return FindCharacterById(characterId).HasValue;
        }

        /// <summary>
        /// 아이템 보유 수량 확인
        /// </summary>
        public int GetItemCount(string itemId)
        {
            var item = FindItemById(itemId);
            return item?.Count ?? 0;
        }
    }
}
