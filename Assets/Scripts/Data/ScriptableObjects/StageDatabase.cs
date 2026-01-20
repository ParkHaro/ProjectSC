using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스테이지 데이터베이스 (마스터 데이터 컬렉션)
    /// </summary>
    [CreateAssetMenu(fileName = "StageDatabase", menuName = "SC/Database/Stage")]
    public class StageDatabase : ScriptableObject
    {
        [SerializeField] private List<StageData> _stages = new();

        private Dictionary<string, StageData> _lookup;

        /// <summary>
        /// 전체 스테이지 목록
        /// </summary>
        public IReadOnlyList<StageData> Stages => _stages;

        /// <summary>
        /// 스테이지 수
        /// </summary>
        public int Count => _stages.Count;

        /// <summary>
        /// ID로 스테이지 조회
        /// </summary>
        public StageData GetById(string id)
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
        /// 챕터별 스테이지 조회
        /// </summary>
        public IEnumerable<StageData> GetByChapter(int chapter)
        {
            foreach (var stage in _stages)
            {
                if (stage.Chapter == chapter)
                    yield return stage;
            }
        }

        /// <summary>
        /// 난이도별 스테이지 조회
        /// </summary>
        public IEnumerable<StageData> GetByDifficulty(Difficulty difficulty)
        {
            foreach (var stage in _stages)
            {
                if (stage.Difficulty == difficulty)
                    yield return stage;
            }
        }

        /// <summary>
        /// 챕터 및 스테이지 번호로 조회
        /// </summary>
        public StageData GetByChapterAndNumber(int chapter, int stageNumber)
        {
            foreach (var stage in _stages)
            {
                if (stage.Chapter == chapter && stage.StageNumber == stageNumber)
                    return stage;
            }

            return null;
        }

        /// <summary>
        /// 컨텐츠 타입별 스테이지 조회
        /// </summary>
        public IEnumerable<StageData> GetByContentType(InGameContentType contentType)
        {
            foreach (var stage in _stages)
            {
                if (stage != null && stage.ContentType == contentType && stage.IsEnabled)
                    yield return stage;
            }
        }

        /// <summary>
        /// 컨텐츠 타입 및 카테고리별 스테이지 조회
        /// </summary>
        public IEnumerable<StageData> GetByContentTypeAndCategory(InGameContentType contentType, string categoryId)
        {
            foreach (var stage in _stages)
            {
                if (stage != null &&
                    stage.ContentType == contentType &&
                    stage.CategoryId == categoryId &&
                    stage.IsEnabled)
                {
                    yield return stage;
                }
            }
        }

        /// <summary>
        /// 카테고리별 스테이지 조회
        /// </summary>
        public IEnumerable<StageData> GetByCategory(string categoryId)
        {
            foreach (var stage in _stages)
            {
                if (stage != null && stage.CategoryId == categoryId && stage.IsEnabled)
                    yield return stage;
            }
        }

        /// <summary>
        /// 이벤트 ID별 스테이지 조회 (이벤트 스테이지용)
        /// </summary>
        public IEnumerable<StageData> GetByEvent(string eventId)
        {
            foreach (var stage in _stages)
            {
                if (stage != null &&
                    stage.ContentType == InGameContentType.Event &&
                    stage.CategoryId == eventId &&
                    stage.IsEnabled)
                {
                    yield return stage;
                }
            }
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, StageData>(_stages.Count);
            foreach (var stage in _stages)
            {
                if (stage != null && !string.IsNullOrEmpty(stage.Id))
                {
                    _lookup[stage.Id] = stage;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(StageData data)
        {
            if (data != null && !_stages.Contains(data))
            {
                _stages.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _stages.Clear();
            _lookup = null;
        }
#endif
    }
}