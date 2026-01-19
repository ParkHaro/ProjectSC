using System;
using System.Collections.Generic;

namespace Sc.Data
{
    /// <summary>
    /// 유저 데이터 변경분 (서버 응답으로 전달)
    /// </summary>
    [Serializable]
    public class UserDataDelta
    {
        /// <summary>
        /// 프로필 변경 (null이면 변경 없음)
        /// </summary>
        public UserProfile? Profile;

        /// <summary>
        /// 재화 변경 (null이면 변경 없음)
        /// </summary>
        public UserCurrency? Currency;

        /// <summary>
        /// 추가/변경된 캐릭터
        /// </summary>
        public List<OwnedCharacter> AddedCharacters;

        /// <summary>
        /// 삭제된 캐릭터 인스턴스 ID
        /// </summary>
        public List<string> RemovedCharacterIds;

        /// <summary>
        /// 추가/변경된 아이템
        /// </summary>
        public List<OwnedItem> AddedItems;

        /// <summary>
        /// 삭제된 아이템 인스턴스 ID (장비) 또는 아이템 ID (소모품)
        /// </summary>
        public List<string> RemovedItemIds;

        /// <summary>
        /// 스테이지 진행 변경
        /// </summary>
        public StageProgress? StageProgress;

        /// <summary>
        /// 가챠 천장 변경
        /// </summary>
        public GachaPityData? GachaPity;

        /// <summary>
        /// 퀘스트 진행 변경
        /// </summary>
        public QuestProgress? QuestProgress;

        /// <summary>
        /// 이벤트 재화 변경 (null이면 변경 없음)
        /// </summary>
        public EventCurrencyData? EventCurrency;

        /// <summary>
        /// 변경사항 있는지 확인
        /// </summary>
        public bool HasChanges =>
            Profile.HasValue ||
            Currency.HasValue ||
            (AddedCharacters != null && AddedCharacters.Count > 0) ||
            (RemovedCharacterIds != null && RemovedCharacterIds.Count > 0) ||
            (AddedItems != null && AddedItems.Count > 0) ||
            (RemovedItemIds != null && RemovedItemIds.Count > 0) ||
            StageProgress.HasValue ||
            GachaPity.HasValue ||
            QuestProgress.HasValue ||
            EventCurrency.HasValue;

        /// <summary>
        /// 빈 Delta 생성
        /// </summary>
        public static UserDataDelta Empty() => new UserDataDelta();

        /// <summary>
        /// 재화 변경만 포함하는 Delta 생성
        /// </summary>
        public static UserDataDelta WithCurrency(UserCurrency currency)
        {
            return new UserDataDelta { Currency = currency };
        }

        /// <summary>
        /// 캐릭터 추가만 포함하는 Delta 생성
        /// </summary>
        public static UserDataDelta WithAddedCharacters(List<OwnedCharacter> characters)
        {
            return new UserDataDelta { AddedCharacters = characters };
        }

        /// <summary>
        /// 아이템 추가만 포함하는 Delta 생성
        /// </summary>
        public static UserDataDelta WithAddedItems(List<OwnedItem> items)
        {
            return new UserDataDelta { AddedItems = items };
        }

        /// <summary>
        /// 이벤트 재화 변경만 포함하는 Delta 생성
        /// </summary>
        public static UserDataDelta WithEventCurrency(EventCurrencyData eventCurrency)
        {
            return new UserDataDelta { EventCurrency = eventCurrency };
        }
    }
}
