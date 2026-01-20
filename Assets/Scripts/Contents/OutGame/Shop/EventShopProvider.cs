using System.Collections.Generic;
using System.Linq;
using Sc.Core;
using Sc.Data;

namespace Sc.Contents.Shop
{
    /// <summary>
    /// 이벤트 상점 Provider
    /// </summary>
    public class EventShopProvider : IShopProvider
    {
        private readonly ShopProductDatabase _database;
        private readonly string _eventId;

        public string ShopId => $"event_shop_{_eventId}";

        public EventShopProvider(ShopProductDatabase database, string eventId)
        {
            _database = database;
            _eventId = eventId;
        }

        public List<ShopProductType> GetAvailableTypes()
        {
            // 이벤트 상점은 EventShop 타입만
            return new List<ShopProductType> { ShopProductType.EventShop };
        }

        public List<ShopProductData> GetProducts(ShopProductType type)
        {
            if (_database == null || type != ShopProductType.EventShop)
                return new List<ShopProductData>();

            return _database.GetEventProducts(_eventId).ToList();
        }

        public List<ShopProductData> GetAllProducts()
        {
            if (_database == null) return new List<ShopProductData>();

            return _database.GetEventProducts(_eventId).ToList();
        }

        public ShopPurchaseRecord? GetPurchaseRecord(string productId)
        {
            return DataManager.Instance?.FindShopPurchaseRecord(productId);
        }
    }
}