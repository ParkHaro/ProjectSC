namespace Sc.Data
{
    /// <summary>
    /// 상점 상품 타입 (탭 분류용)
    /// </summary>
    public enum ShopProductType
    {
        /// <summary>
        /// 재화 상품 (Gem → Gold 등)
        /// </summary>
        Currency = 0,

        /// <summary>
        /// 패키지 (여러 아이템 묶음)
        /// </summary>
        Package = 1,

        /// <summary>
        /// 캐릭터 조각
        /// </summary>
        CharacterPiece = 2,

        /// <summary>
        /// 단일 아이템
        /// </summary>
        Item = 3,

        /// <summary>
        /// 스태미나 충전
        /// </summary>
        Stamina = 4,

        /// <summary>
        /// 이벤트 상점 전용
        /// </summary>
        EventShop = 100,
    }
}