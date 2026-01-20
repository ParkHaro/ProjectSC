using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스테이지 카테고리 데이터베이스
    /// </summary>
    [CreateAssetMenu(fileName = "StageCategoryDatabase", menuName = "SC/Database/StageCategory")]
    public class StageCategoryDatabase : ScriptableObject
    {
        [SerializeField] private List<StageCategoryData> _categories = new();

        private Dictionary<string, StageCategoryData> _lookup;

        /// <summary>
        /// 전체 카테고리 목록
        /// </summary>
        public IReadOnlyList<StageCategoryData> Categories => _categories;

        /// <summary>
        /// 카테고리 수
        /// </summary>
        public int Count => _categories.Count;

        /// <summary>
        /// ID로 카테고리 조회
        /// </summary>
        public StageCategoryData GetById(string id)
        {
            EnsureLookup();
            return _lookup.TryGetValue(id, out var data) ? data : null;
        }

        /// <summary>
        /// ID 존재 여부 확인
        /// </summary>
        public bool Contains(string id)
        {
            EnsureLookup();
            return _lookup.ContainsKey(id);
        }

        /// <summary>
        /// 컨텐츠 타입별 카테고리 조회
        /// </summary>
        public IEnumerable<StageCategoryData> GetByContentType(InGameContentType contentType)
        {
            foreach (var category in _categories)
            {
                if (category != null && category.ContentType == contentType && category.IsEnabled)
                    yield return category;
            }
        }

        /// <summary>
        /// 컨텐츠 타입별 카테고리 목록 (정렬됨)
        /// </summary>
        public List<StageCategoryData> GetSortedByContentType(InGameContentType contentType)
        {
            var result = new List<StageCategoryData>();
            foreach (var category in _categories)
            {
                if (category != null && category.ContentType == contentType && category.IsEnabled)
                {
                    result.Add(category);
                }
            }

            result.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));
            return result;
        }

        /// <summary>
        /// 속성별 카테고리 조회 (속성 던전용)
        /// </summary>
        public StageCategoryData GetByElement(InGameContentType contentType, Element element)
        {
            foreach (var category in _categories)
            {
                if (category != null &&
                    category.ContentType == contentType &&
                    category.Element == element &&
                    category.IsEnabled)
                {
                    return category;
                }
            }

            return null;
        }

        /// <summary>
        /// 챕터별 카테고리 조회 (메인스토리용)
        /// </summary>
        public StageCategoryData GetByChapter(int chapterNumber)
        {
            foreach (var category in _categories)
            {
                if (category != null &&
                    category.ContentType == InGameContentType.MainStory &&
                    category.ChapterNumber == chapterNumber &&
                    category.IsEnabled)
                {
                    return category;
                }
            }

            return null;
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, StageCategoryData>(_categories.Count);
            foreach (var category in _categories)
            {
                if (category != null && !string.IsNullOrEmpty(category.Id))
                {
                    _lookup[category.Id] = category;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(StageCategoryData data)
        {
            if (data != null && !_categories.Contains(data))
            {
                _categories.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _categories.Clear();
            _lookup = null;
        }
#endif
    }
}