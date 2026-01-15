using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 아이템 데이터베이스 (마스터 데이터 컬렉션)
    /// </summary>
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "SC/Database/Item")]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items = new();

        private Dictionary<string, ItemData> _lookup;

        /// <summary>
        /// 전체 아이템 목록
        /// </summary>
        public IReadOnlyList<ItemData> Items => _items;

        /// <summary>
        /// 아이템 수
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// ID로 아이템 조회
        /// </summary>
        public ItemData GetById(string id)
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
        /// 타입별 아이템 조회
        /// </summary>
        public IEnumerable<ItemData> GetByType(ItemType type)
        {
            foreach (var item in _items)
            {
                if (item.Type == type)
                    yield return item;
            }
        }

        /// <summary>
        /// 희귀도별 아이템 조회
        /// </summary>
        public IEnumerable<ItemData> GetByRarity(Rarity rarity)
        {
            foreach (var item in _items)
            {
                if (item.Rarity == rarity)
                    yield return item;
            }
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, ItemData>(_items.Count);
            foreach (var item in _items)
            {
                if (item != null && !string.IsNullOrEmpty(item.Id))
                {
                    _lookup[item.Id] = item;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(ItemData data)
        {
            if (data != null && !_items.Contains(data))
            {
                _items.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _items.Clear();
            _lookup = null;
        }
#endif
    }
}
