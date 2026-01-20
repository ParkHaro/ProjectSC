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
        /// 이벤트 재화 데이터
        /// </summary>
        public EventCurrencyData EventCurrency;

        /// <summary>
        /// 라이브 이벤트 진행 데이터 (Key: EventId)
        /// </summary>
        public Dictionary<string, LiveEventProgress> EventProgresses;

        /// <summary>
        /// 상점 구매 기록 (Key: ProductId)
        /// </summary>
        public Dictionary<string, ShopPurchaseRecord> ShopPurchaseRecords;

        /// <summary>
        /// 마지막 동기화 시간 (Unix Timestamp)
        /// </summary>
        public long LastSyncAt;

        /// <summary>
        /// 현재 데이터 버전
        /// </summary>
        public const int CurrentVersion = 4;

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
                EventCurrency = EventCurrencyData.CreateDefault(),
                EventProgresses = new Dictionary<string, LiveEventProgress>(),
                ShopPurchaseRecords = new Dictionary<string, ShopPurchaseRecord>(),
                LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 데이터 마이그레이션 (버전 업그레이드)
        /// </summary>
        public static UserSaveData Migrate(UserSaveData data)
        {
            // Version 1 → 2: EventCurrency 필드 추가
            if (data.Version < 2)
            {
                // EventCurrency가 null/비어있을 수 있음 (JSON 역직렬화 특성)
                if (data.EventCurrency.Currencies == null)
                {
                    data.EventCurrency = EventCurrencyData.CreateDefault();
                }

                data.Version = 2;
            }

            // Version 2 → 3: EventProgresses 필드 추가
            if (data.Version < 3)
            {
                if (data.EventProgresses == null)
                {
                    data.EventProgresses = new Dictionary<string, LiveEventProgress>();
                }

                data.Version = 3;
            }

            // Version 3 → 4: ShopPurchaseRecords 필드 추가
            if (data.Version < 4)
            {
                if (data.ShopPurchaseRecords == null)
                {
                    data.ShopPurchaseRecords = new Dictionary<string, ShopPurchaseRecord>();
                }

                data.Version = 4;
            }

            return data;
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

        #region Event Progress Helpers

        /// <summary>
        /// 이벤트 진행 상태 조회
        /// </summary>
        public LiveEventProgress? FindEventProgress(string eventId)
        {
            if (EventProgresses == null) return null;
            return EventProgresses.TryGetValue(eventId, out var progress) ? progress : null;
        }

        /// <summary>
        /// 이벤트 진행 상태 업데이트 또는 생성
        /// </summary>
        public void UpdateEventProgress(string eventId, LiveEventProgress progress)
        {
            EventProgresses ??= new Dictionary<string, LiveEventProgress>();
            EventProgresses[eventId] = progress;
        }

        /// <summary>
        /// 이벤트 진행 상태 생성 (없으면)
        /// </summary>
        public LiveEventProgress GetOrCreateEventProgress(string eventId)
        {
            EventProgresses ??= new Dictionary<string, LiveEventProgress>();
            if (!EventProgresses.TryGetValue(eventId, out var progress))
            {
                progress = LiveEventProgress.CreateDefault(eventId);
                EventProgresses[eventId] = progress;
            }

            return progress;
        }

        /// <summary>
        /// 이벤트 방문 처리
        /// </summary>
        public void VisitEvent(string eventId, long serverTime)
        {
            var progress = GetOrCreateEventProgress(eventId);
            if (!progress.HasVisited)
            {
                progress.HasVisited = true;
                progress.FirstVisitTime = serverTime;
                EventProgresses[eventId] = progress;
            }
        }

        /// <summary>
        /// 이벤트 방문 여부 확인
        /// </summary>
        public bool HasVisitedEvent(string eventId)
        {
            var progress = FindEventProgress(eventId);
            return progress?.HasVisited ?? false;
        }

        #endregion

        #region Shop Purchase Helpers

        /// <summary>
        /// 상품 구매 기록 조회
        /// </summary>
        public ShopPurchaseRecord? FindShopPurchaseRecord(string productId)
        {
            if (ShopPurchaseRecords == null) return null;
            return ShopPurchaseRecords.TryGetValue(productId, out var record) ? record : null;
        }

        /// <summary>
        /// 상품 구매 기록 업데이트
        /// </summary>
        public void UpdateShopPurchaseRecord(string productId, ShopPurchaseRecord record)
        {
            ShopPurchaseRecords ??= new Dictionary<string, ShopPurchaseRecord>();
            ShopPurchaseRecords[productId] = record;
        }

        /// <summary>
        /// 상품 구매 횟수 조회
        /// </summary>
        public int GetShopPurchaseCount(string productId)
        {
            var record = FindShopPurchaseRecord(productId);
            return record?.PurchaseCount ?? 0;
        }

        #endregion
    }
}