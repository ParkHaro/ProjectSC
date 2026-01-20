using System;
using NUnit.Framework;
using Sc.Data;
using Sc.LocalServer;
using UnityEngine;

namespace Sc.Editor.Tests.LocalServer
{
    /// <summary>
    /// PurchaseLimitValidator 단위 테스트.
    /// 구매 제한 검증 로직 테스트.
    /// </summary>
    [TestFixture]
    public class PurchaseLimitValidatorTests
    {
        private PurchaseLimitValidator _validator;
        private ServerTimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = new ServerTimeService();
            _validator = new PurchaseLimitValidator(_timeService);
        }

        #region CanPurchase Tests

        [Test]
        public void CanPurchase_ReturnsTrue_WhenNoLimit()
        {
            var product = CreateProduct(LimitType.None, 0);

            var result = _validator.CanPurchase(product, null, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void CanPurchase_ReturnsTrue_WhenUnderLimit()
        {
            var product = CreateProduct(LimitType.Daily, 3);
            var record = CreateRecord(1, 0, 0); // 1회 구매함

            var result = _validator.CanPurchase(product, record, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(2));
        }

        [Test]
        public void CanPurchase_ReturnsFalse_WhenAtLimit()
        {
            var product = CreateProduct(LimitType.Daily, 3);
            var record = CreateRecord(3, 0, long.MaxValue); // 3회 구매, 리셋 안 됨

            var result = _validator.CanPurchase(product, record, out int remaining);

            Assert.That(result, Is.False);
            Assert.That(remaining, Is.EqualTo(0));
        }

        [Test]
        public void CanPurchase_ReturnsFalse_WhenOverLimit()
        {
            var product = CreateProduct(LimitType.Permanent, 1);
            var record = CreateRecord(1, 0, 0);

            var result = _validator.CanPurchase(product, record, out int remaining);

            Assert.That(result, Is.False);
            Assert.That(remaining, Is.EqualTo(0));
        }

        [Test]
        public void CanPurchase_ReturnsTrue_WhenNeedsReset()
        {
            var product = CreateProduct(LimitType.Daily, 1);
            // ResetTime이 과거 시간 (리셋 필요)
            var record = CreateRecord(1, 0, _timeService.ServerTimeUtc - 1000);

            var result = _validator.CanPurchase(product, record, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(1));
        }

        [Test]
        public void CanPurchase_ReturnsTrue_WhenNoRecordExists()
        {
            var product = CreateProduct(LimitType.Weekly, 5);

            var result = _validator.CanPurchase(product, null, out int remaining);

            Assert.That(result, Is.True);
            Assert.That(remaining, Is.EqualTo(5));
        }

        #endregion

        #region CalculateResetTime Tests

        [Test]
        public void CalculateResetTime_ReturnsZero_ForNoLimit()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.None);

            Assert.That(resetTime, Is.EqualTo(0));
        }

        [Test]
        public void CalculateResetTime_ReturnsZero_ForPermanent()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Permanent);

            Assert.That(resetTime, Is.EqualTo(0));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextMidnight_ForDaily()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Daily);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 24시간 이내여야 함
            Assert.That(resetTime, Is.LessThanOrEqualTo(_timeService.ServerTimeUtc + 86400));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextMonday_ForWeekly()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Weekly);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 7일 이내여야 함
            Assert.That(resetTime, Is.LessThanOrEqualTo(_timeService.ServerTimeUtc + 7 * 86400));
        }

        [Test]
        public void CalculateResetTime_ReturnsNextFirstOfMonth_ForMonthly()
        {
            var resetTime = _validator.CalculateResetTime(LimitType.Monthly);

            // 리셋 시간은 미래여야 함
            Assert.That(resetTime, Is.GreaterThan(_timeService.ServerTimeUtc));

            // 리셋 시간은 다음 달 1일이어야 함
            var resetDateTime = DateTimeOffset.FromUnixTimeSeconds(resetTime).UtcDateTime;
            Assert.That(resetDateTime.Day, Is.EqualTo(1));
        }

        #endregion

        #region UpdatePurchaseRecord Tests

        [Test]
        public void UpdatePurchaseRecord_CreatesNewRecord_WhenNoExisting()
        {
            var product = CreateProduct(LimitType.Daily, 3);

            var record = _validator.UpdatePurchaseRecord(product, null);

            Assert.That(record.ProductId, Is.EqualTo(product.Id));
            Assert.That(record.PurchaseCount, Is.EqualTo(1));
            Assert.That(record.LastPurchaseTime, Is.GreaterThan(0));
            Assert.That(record.ResetTime, Is.GreaterThan(_timeService.ServerTimeUtc));
        }

        [Test]
        public void UpdatePurchaseRecord_IncrementsPurchaseCount()
        {
            var product = CreateProduct(LimitType.Weekly, 5);
            var existingRecord =
                CreateRecord(2, _timeService.ServerTimeUtc - 1000, _timeService.ServerTimeUtc + 100000);

            var record = _validator.UpdatePurchaseRecord(product, existingRecord);

            Assert.That(record.PurchaseCount, Is.EqualTo(3));
        }

        [Test]
        public void UpdatePurchaseRecord_ResetsCount_WhenNeedsReset()
        {
            var product = CreateProduct(LimitType.Daily, 3);
            // 리셋 필요 (ResetTime이 과거)
            var existingRecord = CreateRecord(3, _timeService.ServerTimeUtc - 2000, _timeService.ServerTimeUtc - 1000);

            var record = _validator.UpdatePurchaseRecord(product, existingRecord);

            // 리셋 후 1회 구매로 시작
            Assert.That(record.PurchaseCount, Is.EqualTo(1));
            Assert.That(record.ResetTime, Is.GreaterThan(_timeService.ServerTimeUtc));
        }

        [Test]
        public void UpdatePurchaseRecord_KeepsZeroReset_ForPermanent()
        {
            var product = CreateProduct(LimitType.Permanent, 1);

            var record = _validator.UpdatePurchaseRecord(product, null);

            Assert.That(record.ResetTime, Is.EqualTo(0));
        }

        #endregion

        #region Helper Methods

        private ShopProductData CreateProduct(LimitType limitType, int limitCount)
        {
            var product = ScriptableObject.CreateInstance<ShopProductData>();
            product.Initialize(
                id: $"test_product_{limitType}",
                productType: ShopProductType.Item,
                nameKey: "Test Product",
                descriptionKey: "Test Description",
                costType: CostType.Gold,
                price: 100,
                rewards: new[] { RewardInfo.Currency(CostType.Gold, 100) },
                limitType: limitType,
                limitCount: limitCount);
            return product;
        }

        private ShopPurchaseRecord CreateRecord(int purchaseCount, long lastPurchaseTime, long resetTime)
        {
            return new ShopPurchaseRecord
            {
                ProductId = "test_product",
                PurchaseCount = purchaseCount,
                LastPurchaseTime = lastPurchaseTime,
                ResetTime = resetTime
            };
        }

        #endregion
    }
}