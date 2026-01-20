using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 상점 상품 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "ShopProductData", menuName = "SC/Data/ShopProductData")]
    public class ShopProductData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private ShopProductType _productType;
        [SerializeField] private string _nameKey;
        [SerializeField] private string _descriptionKey;
        [SerializeField] private Sprite _iconSprite;

        [Header("가격")]
        [SerializeField] private CostType _costType;
        [SerializeField] private int _price;

        [Header("보상")]
        [SerializeField] private RewardInfo[] _rewards;

        [Header("구매 제한")]
        [SerializeField] private LimitType _limitType;
        [SerializeField] private int _limitCount;

        [Header("표시")]
        [SerializeField] private int _displayOrder;
        [SerializeField] private bool _isEnabled = true;
        [SerializeField] private bool _isHot;
        [SerializeField] private bool _isNew;

        [Header("이벤트 상점 전용")]
        [SerializeField] private string _eventId;

        #region Properties

        public string Id => _id;
        public ShopProductType ProductType => _productType;
        public string NameKey => _nameKey;
        public string DescriptionKey => _descriptionKey;
        public Sprite IconSprite => _iconSprite;
        public CostType CostType => _costType;
        public int Price => _price;
        public IReadOnlyList<RewardInfo> Rewards => _rewards;
        public LimitType LimitType => _limitType;
        public int LimitCount => _limitCount;
        public int DisplayOrder => _displayOrder;
        public bool IsEnabled => _isEnabled;
        public bool IsHot => _isHot;
        public bool IsNew => _isNew;
        public string EventId => _eventId;

        #endregion

        #region Helpers

        /// <summary>
        /// 구매 제한이 있는지 여부
        /// </summary>
        public bool HasLimit => _limitType != LimitType.None && _limitCount > 0;

        /// <summary>
        /// 이벤트 상점 전용 상품인지 여부
        /// </summary>
        public bool IsEventExclusive => _productType == ShopProductType.EventShop;

        /// <summary>
        /// 특정 이벤트에 속하는지 여부
        /// </summary>
        public bool BelongsToEvent(string eventId)
        {
            if (!IsEventExclusive) return false;
            return string.Equals(_eventId, eventId, StringComparison.Ordinal);
        }

        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// 에디터 전용 초기화
        /// </summary>
        public void Initialize(
            string id,
            ShopProductType productType,
            string nameKey,
            string descriptionKey,
            CostType costType,
            int price,
            RewardInfo[] rewards,
            LimitType limitType = LimitType.None,
            int limitCount = 0,
            int displayOrder = 0,
            string eventId = null)
        {
            _id = id;
            _productType = productType;
            _nameKey = nameKey;
            _descriptionKey = descriptionKey;
            _costType = costType;
            _price = price;
            _rewards = rewards ?? Array.Empty<RewardInfo>();
            _limitType = limitType;
            _limitCount = limitCount;
            _displayOrder = displayOrder;
            _isEnabled = true;
            _eventId = eventId;
        }
#endif
    }
}