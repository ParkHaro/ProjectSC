using System.Linq;
using NUnit.Framework;
using Sc.Data;
using Sc.LocalServer;
using UnityEngine;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// ShopHandler 단위 테스트.
    /// 상점 구매 로직 검증.
    /// </summary>
    [TestFixture]
    public class ShopHandlerTests
    {
        private ShopHandler _handler;
        private ShopProductDatabase _database;
        private UserSaveData _testUserData;
        private ShopProductData _testProduct;
        private ShopProductData _limitedProduct;
        private ShopProductData _eventProduct;

        [SetUp]
        public void SetUp()
        {
            var timeService = new ServerTimeService();
            var validator = new ServerValidator(timeService);
            var rewardService = new RewardService();

            _handler = new ShopHandler(validator, rewardService, timeService);

            // 테스트용 데이터베이스 생성
            _database = ScriptableObject.CreateInstance<ShopProductDatabase>();

            // 테스트용 상품 생성
            _testProduct = CreateProduct("product_gold_100", ShopProductType.Currency, CostType.Gold, 100,
                new[] { RewardInfo.Currency(CostType.Gold, 1000) });

            _limitedProduct = CreateProduct("product_limited", ShopProductType.Package, CostType.Gem, 300,
                new[] { RewardInfo.Currency(CostType.Gold, 5000) },
                LimitType.Daily, 3);

            _eventProduct = CreateProduct("product_event", ShopProductType.EventShop, CostType.EventCurrency, 100,
                new[] { RewardInfo.Currency(CostType.Gem, 10) },
                eventId: "test_event");

            _database.Add(_testProduct);
            _database.Add(_limitedProduct);
            _database.Add(_eventProduct);

            _handler.SetProductDatabase(_database);

            // 테스트 유저 데이터
            _testUserData = UserSaveData.CreateNew("test_uid", "TestUser");
            _testUserData.Currency = new UserCurrency { Gold = 10000, Gem = 1000, FreeGem = 500 };
            _testUserData.EventCurrency = EventCurrencyData.CreateDefault();
            _testUserData.EventCurrency.AddCurrency("test_event", 500);
        }

        [TearDown]
        public void TearDown()
        {
            if (_database != null)
                Object.DestroyImmediate(_database);
            if (_testProduct != null)
                Object.DestroyImmediate(_testProduct);
            if (_limitedProduct != null)
                Object.DestroyImmediate(_limitedProduct);
            if (_eventProduct != null)
                Object.DestroyImmediate(_eventProduct);
        }

        #region Basic Purchase Tests

        [Test]
        public void Handle_ReturnsSuccess_WhenPurchaseValid()
        {
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.ProductId, Is.EqualTo(_testProduct.Id));
            Assert.That(response.Rewards, Is.Not.Null);
            Assert.That(response.Rewards.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Handle_DeductsCurrency_WhenPurchaseValid()
        {
            var initialGold = _testUserData.Currency.Gold;
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            _handler.Handle(request, ref _testUserData);

            Assert.That(_testUserData.Currency.Gold, Is.EqualTo(initialGold - _testProduct.Price));
        }

        [Test]
        public void Handle_GivesReward_WhenPurchaseValid()
        {
            var initialGold = _testUserData.Currency.Gold;
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            _handler.Handle(request, ref _testUserData);

            // 골드 차감 후 보상 지급
            var expectedGold = initialGold - _testProduct.Price + _testProduct.Rewards.First().Amount;
            Assert.That(_testUserData.Currency.Gold, Is.EqualTo(expectedGold));
        }

        #endregion

        #region Error Handling Tests

        [Test]
        public void Handle_ReturnsError_WhenProductNotFound()
        {
            var request = ShopPurchaseRequest.Create("non_existent_product", 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(1001)); // 상품 없음
        }

        [Test]
        public void Handle_ReturnsError_WhenInsufficientGold()
        {
            _testUserData.Currency = new UserCurrency { Gold = 50 }; // 부족한 골드
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(1003)); // 재화 부족
        }

        [Test]
        public void Handle_ReturnsError_WhenInsufficientGem()
        {
            _testUserData.Currency = new UserCurrency { Gem = 100, FreeGem = 100 }; // 부족한 젬
            var request = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(1003)); // 재화 부족
        }

        [Test]
        public void Handle_ReturnsError_WhenDatabaseNotSet()
        {
            _handler.SetProductDatabase(null);
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(9999)); // 서버 오류
        }

        #endregion

        #region Purchase Limit Tests

        [Test]
        public void Handle_ReturnsSuccess_WhenUnderLimit()
        {
            var request = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.UpdatedRecord.HasValue, Is.True);
            Assert.That(response.UpdatedRecord.Value.PurchaseCount, Is.EqualTo(1));
        }

        [Test]
        public void Handle_TracksMultiplePurchases()
        {
            // 첫 번째 구매
            var request1 = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);
            _handler.Handle(request1, ref _testUserData);

            // 두 번째 구매
            var request2 = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);
            var response = _handler.Handle(request2, ref _testUserData);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.UpdatedRecord.Value.PurchaseCount, Is.EqualTo(2));
        }

        [Test]
        public void Handle_ReturnsError_WhenAtLimit()
        {
            // 3회 구매 (제한 도달)
            for (int i = 0; i < 3; i++)
            {
                var req = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);
                _handler.Handle(req, ref _testUserData);
            }

            // 4번째 구매 시도
            var request = ShopPurchaseRequest.Create(_limitedProduct.Id, 1);
            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(1002)); // 구매 제한
        }

        #endregion

        #region Event Currency Tests

        [Test]
        public void Handle_DeductsEventCurrency_WhenEventProduct()
        {
            var initialEventCurrency = _testUserData.EventCurrency.GetCurrency("test_event");
            var request = ShopPurchaseRequest.Create(_eventProduct.Id, 1);

            _handler.Handle(request, ref _testUserData);

            var currentEventCurrency = _testUserData.EventCurrency.GetCurrency("test_event");
            Assert.That(currentEventCurrency, Is.EqualTo(initialEventCurrency - _eventProduct.Price));
        }

        [Test]
        public void Handle_ReturnsError_WhenInsufficientEventCurrency()
        {
            _testUserData.EventCurrency.SetCurrency("test_event", 50); // 부족한 이벤트 재화
            var request = ShopPurchaseRequest.Create(_eventProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorCode, Is.EqualTo(1003)); // 재화 부족
        }

        #endregion

        #region Delta Tests

        [Test]
        public void Handle_ReturnsDelta_WhenPurchaseValid()
        {
            var request = ShopPurchaseRequest.Create(_testProduct.Id, 1);

            var response = _handler.Handle(request, ref _testUserData);

            Assert.That(response.Delta, Is.Not.Null);
            Assert.That(response.Delta.HasChanges, Is.True);
            Assert.That(response.Delta.Currency.HasValue, Is.True);
        }

        #endregion

        #region Helper Methods

        private ShopProductData CreateProduct(
            string id,
            ShopProductType productType,
            CostType costType,
            int price,
            RewardInfo[] rewards,
            LimitType limitType = LimitType.None,
            int limitCount = 0,
            string eventId = null)
        {
            var product = ScriptableObject.CreateInstance<ShopProductData>();
            product.Initialize(
                id: id,
                productType: productType,
                nameKey: $"name_{id}",
                descriptionKey: $"desc_{id}",
                costType: costType,
                price: price,
                rewards: rewards,
                limitType: limitType,
                limitCount: limitCount,
                eventId: eventId);
            return product;
        }

        #endregion
    }
}
