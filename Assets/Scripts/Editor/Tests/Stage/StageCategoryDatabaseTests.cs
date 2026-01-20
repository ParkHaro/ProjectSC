using System.Linq;
using NUnit.Framework;
using Sc.Data;
using UnityEngine;

namespace Sc.Editor.Tests.Stage
{
    /// <summary>
    /// StageCategoryDatabase 단위 테스트.
    /// 카테고리 조회 로직 검증.
    /// </summary>
    [TestFixture]
    public class StageCategoryDatabaseTests
    {
        private StageCategoryDatabase _database;
        private StageCategoryData _chapter1;
        private StageCategoryData _chapter2;
        private StageCategoryData _chapter3;
        private StageCategoryData _goldFire;
        private StageCategoryData _goldWater;
        private StageCategoryData _expEasy;

        [SetUp]
        public void SetUp()
        {
            _database = ScriptableObject.CreateInstance<StageCategoryDatabase>();

            // MainStory 챕터들
            _chapter1 = CreateCategory("chapter_1", InGameContentType.MainStory, chapterNumber: 1, displayOrder: 1);
            _chapter2 = CreateCategory("chapter_2", InGameContentType.MainStory, chapterNumber: 2, displayOrder: 2);
            _chapter3 = CreateCategory("chapter_3", InGameContentType.MainStory, chapterNumber: 3, displayOrder: 3);

            // GoldDungeon 속성들
            _goldFire = CreateCategory("gold_fire", InGameContentType.GoldDungeon, element: Element.Fire, displayOrder: 1);
            _goldWater = CreateCategory("gold_water", InGameContentType.GoldDungeon, element: Element.Water, displayOrder: 2);

            // ExpDungeon 난이도
            _expEasy = CreateCategory("exp_easy", InGameContentType.ExpDungeon, difficulty: Difficulty.Easy, displayOrder: 1);

            AddCategoriesToDatabase(_chapter1, _chapter2, _chapter3, _goldFire, _goldWater, _expEasy);
        }

        [TearDown]
        public void TearDown()
        {
            DestroyCategories();
            if (_database != null)
                Object.DestroyImmediate(_database);
        }

        #region GetById Tests

        [Test]
        public void GetById_ReturnsCategory_WhenIdExists()
        {
            var category = _database.GetById("chapter_1");

            Assert.That(category, Is.Not.Null);
            Assert.That(category.Id, Is.EqualTo("chapter_1"));
        }

        [Test]
        public void GetById_ReturnsNull_WhenIdNotExists()
        {
            var category = _database.GetById("non_existent");

            Assert.That(category, Is.Null);
        }

        [Test]
        public void GetById_ReturnsNull_WhenIdIsNull()
        {
            var category = _database.GetById(null);

            Assert.That(category, Is.Null);
        }

        #endregion

        #region GetByContentType Tests

        [Test]
        public void GetByContentType_ReturnsAllMatchingCategories()
        {
            var mainStoryCategories = _database.GetByContentType(InGameContentType.MainStory).ToList();

            Assert.That(mainStoryCategories.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetByContentType_ReturnsEmpty_WhenNoMatches()
        {
            var bossRaidCategories = _database.GetByContentType(InGameContentType.BossRaid).ToList();

            Assert.That(bossRaidCategories, Is.Empty);
        }

        #endregion

        #region GetSortedByContentType Tests

        [Test]
        public void GetSortedByContentType_ReturnsSortedBySortOrder()
        {
            var sortedCategories = _database.GetSortedByContentType(InGameContentType.MainStory);

            Assert.That(sortedCategories.Count, Is.EqualTo(3));
            Assert.That(sortedCategories[0].Id, Is.EqualTo("chapter_1"));
            Assert.That(sortedCategories[1].Id, Is.EqualTo("chapter_2"));
            Assert.That(sortedCategories[2].Id, Is.EqualTo("chapter_3"));
        }

        [Test]
        public void GetSortedByContentType_ReturnsEmptyList_WhenNoMatches()
        {
            var sorted = _database.GetSortedByContentType(InGameContentType.Tower);

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted, Is.Empty);
        }

        #endregion

        #region GetByElement Tests

        [Test]
        public void GetByElement_ReturnsMatchingCategory()
        {
            var fireCategory = _database.GetByElement(InGameContentType.GoldDungeon, Element.Fire);

            Assert.That(fireCategory, Is.Not.Null);
            Assert.That(fireCategory.Id, Is.EqualTo("gold_fire"));
        }

        [Test]
        public void GetByElement_ReturnsNull_WhenNoMatches()
        {
            var darkCategory = _database.GetByElement(InGameContentType.GoldDungeon, Element.Dark);

            Assert.That(darkCategory, Is.Null);
        }

        #endregion

        #region GetByChapter Tests

        [Test]
        public void GetByChapter_ReturnsMatchingCategory()
        {
            var chapter2Category = _database.GetByChapter(2);

            Assert.That(chapter2Category, Is.Not.Null);
            Assert.That(chapter2Category.Id, Is.EqualTo("chapter_2"));
        }

        [Test]
        public void GetByChapter_ReturnsNull_WhenChapterNotExists()
        {
            var category = _database.GetByChapter(99);

            Assert.That(category, Is.Null);
        }

        #endregion

        #region Helper Methods

        private StageCategoryData CreateCategory(
            string id,
            InGameContentType contentType,
            int chapterNumber = 0,
            Element element = Element.Fire,
            Difficulty difficulty = Difficulty.Normal,
            int displayOrder = 0)
        {
            var category = ScriptableObject.CreateInstance<StageCategoryData>();
            category.Initialize(
                id: id,
                contentType: contentType,
                nameKey: $"name_{id}",
                descriptionKey: $"desc_{id}",
                element: element,
                difficulty: difficulty,
                chapterNumber: chapterNumber,
                displayOrder: displayOrder,
                isEnabled: true);
            return category;
        }

        private void AddCategoriesToDatabase(params StageCategoryData[] categories)
        {
            foreach (var category in categories)
            {
                _database.Add(category);
            }
        }

        private void DestroyCategories()
        {
            if (_chapter1 != null) Object.DestroyImmediate(_chapter1);
            if (_chapter2 != null) Object.DestroyImmediate(_chapter2);
            if (_chapter3 != null) Object.DestroyImmediate(_chapter3);
            if (_goldFire != null) Object.DestroyImmediate(_goldFire);
            if (_goldWater != null) Object.DestroyImmediate(_goldWater);
            if (_expEasy != null) Object.DestroyImmediate(_expEasy);
        }

        #endregion
    }
}