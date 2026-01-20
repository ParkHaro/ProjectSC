using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 상점 상품 데이터베이스
    /// </summary>
    [CreateAssetMenu(fileName = "ShopProductDatabase", menuName = "SC/Database/ShopProductDatabase")]
    public class ShopProductDatabase : ScriptableObject
    {
        [SerializeField] private List<ShopProductData> _products = new();

        private Dictionary<string, ShopProductData> _lookup;

        public IReadOnlyList<ShopProductData> Products => _products;
        public int Count => _products.Count;

        /// <summary>
        /// ID로 상품 조회
        /// </summary>
        public ShopProductData GetById(string id)
        {
            EnsureLookup();
            return _lookup.TryGetValue(id, out var data) ? data : null;
        }

        /// <summary>
        /// 타입별 상품 목록 조회
        /// </summary>
        public IEnumerable<ShopProductData> GetByType(ShopProductType productType)
        {
            return _products
                .Where(p => p != null && p.IsEnabled && p.ProductType == productType)
                .OrderBy(p => p.DisplayOrder);
        }

        /// <summary>
        /// 일반 상점 상품 목록 조회 (이벤트 상점 제외)
        /// </summary>
        public IEnumerable<ShopProductData> GetGeneralShopProducts()
        {
            return _products
                .Where(p => p != null && p.IsEnabled && !p.IsEventExclusive)
                .OrderBy(p => p.DisplayOrder);
        }

        /// <summary>
        /// 특정 이벤트의 상품 목록 조회
        /// </summary>
        public IEnumerable<ShopProductData> GetEventProducts(string eventId)
        {
            return _products
                .Where(p => p != null && p.IsEnabled && p.BelongsToEvent(eventId))
                .OrderBy(p => p.DisplayOrder);
        }

        /// <summary>
        /// 사용 가능한 ProductType 목록 조회 (이벤트 상점 제외)
        /// </summary>
        public IEnumerable<ShopProductType> GetAvailableTypes()
        {
            return _products
                .Where(p => p != null && p.IsEnabled && !p.IsEventExclusive)
                .Select(p => p.ProductType)
                .Distinct()
                .OrderBy(t => (int)t);
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, ShopProductData>(_products.Count);
            foreach (var product in _products)
            {
                if (product != null && !string.IsNullOrEmpty(product.Id))
                {
                    _lookup[product.Id] = product;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(ShopProductData data)
        {
            if (data != null && !_products.Contains(data))
            {
                _products.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _products.Clear();
            _lookup = null;
        }
#endif
    }
}