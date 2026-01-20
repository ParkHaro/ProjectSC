using System.Collections.Generic;
using System.Linq;
using Sc.Core;
using Sc.Data;

namespace Sc.Contents.Shop
{
    /// <summary>
    /// 일반 상점 Provider
    /// </summary>
    public class NormalShopProvider : IShopProvider
    {
        private readonly ShopProductDatabase _database;

        public string ShopId => "normal_shop";

        public NormalShopProvider(ShopProductDatabase database)
        {
            _database = database;
        }

        public List<ShopProductType> GetAvailableTypes()
        {
            if (_database == null) return new List<ShopProductType>();

            return _database.GetAvailableTypes().ToList();
        }

        public List<ShopProductData> GetProducts(ShopProductType type)
        {
            if (_database == null) return new List<ShopProductData>();

            return _database.GetByType(type)
                .Where(p => !p.IsEventExclusive)
                .ToList();
        }

        public List<ShopProductData> GetAllProducts()
        {
            if (_database == null) return new List<ShopProductData>();

            return _database.GetGeneralShopProducts().ToList();
        }

        public ShopPurchaseRecord? GetPurchaseRecord(string productId)
        {
            return DataManager.Instance?.FindShopPurchaseRecord(productId);
        }
    }
}