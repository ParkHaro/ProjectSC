using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 가챠 풀 데이터베이스 (마스터 데이터 컬렉션)
    /// </summary>
    [CreateAssetMenu(fileName = "GachaPoolDatabase", menuName = "SC/Database/GachaPool")]
    public class GachaPoolDatabase : ScriptableObject
    {
        [SerializeField] private List<GachaPoolData> _gachaPools = new();

        private Dictionary<string, GachaPoolData> _lookup;

        /// <summary>
        /// 전체 가챠 풀 목록
        /// </summary>
        public IReadOnlyList<GachaPoolData> GachaPools => _gachaPools;

        /// <summary>
        /// 가챠 풀 수
        /// </summary>
        public int Count => _gachaPools.Count;

        /// <summary>
        /// ID로 가챠 풀 조회
        /// </summary>
        public GachaPoolData GetById(string id)
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
        /// 활성화된 가챠 풀 조회
        /// </summary>
        public IEnumerable<GachaPoolData> GetActivePools()
        {
            foreach (var pool in _gachaPools)
            {
                if (pool.IsActive)
                    yield return pool;
            }
        }

        /// <summary>
        /// 타입별 가챠 풀 조회
        /// </summary>
        public IEnumerable<GachaPoolData> GetByType(GachaType type)
        {
            foreach (var pool in _gachaPools)
            {
                if (pool.Type == type)
                    yield return pool;
            }
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, GachaPoolData>(_gachaPools.Count);
            foreach (var pool in _gachaPools)
            {
                if (pool != null && !string.IsNullOrEmpty(pool.Id))
                {
                    _lookup[pool.Id] = pool;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(GachaPoolData data)
        {
            if (data != null && !_gachaPools.Contains(data))
            {
                _gachaPools.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _gachaPools.Clear();
            _lookup = null;
        }
#endif
    }
}
