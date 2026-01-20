using System.Collections.Generic;
using Sc.Data;

namespace Sc.Contents.Shop
{
    /// <summary>
    /// 상점 데이터 제공 인터페이스 (일반 상점 / 이벤트 상점 통합)
    /// </summary>
    public interface IShopProvider
    {
        /// <summary>
        /// 상점 ID
        /// </summary>
        string ShopId { get; }

        /// <summary>
        /// 사용 가능한 ProductType 목록
        /// </summary>
        List<ShopProductType> GetAvailableTypes();

        /// <summary>
        /// 특정 타입의 상품 목록 조회
        /// </summary>
        List<ShopProductData> GetProducts(ShopProductType type);

        /// <summary>
        /// 전체 상품 목록 조회
        /// </summary>
        List<ShopProductData> GetAllProducts();

        /// <summary>
        /// 구매 기록 조회
        /// </summary>
        ShopPurchaseRecord? GetPurchaseRecord(string productId);
    }
}
