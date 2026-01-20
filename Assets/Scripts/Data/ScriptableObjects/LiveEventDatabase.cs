using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 라이브 이벤트 데이터베이스
    /// </summary>
    [CreateAssetMenu(fileName = "LiveEventDatabase", menuName = "SC/Database/LiveEventDatabase")]
    public class LiveEventDatabase : ScriptableObject
    {
        [SerializeField] private List<LiveEventData> _events = new();

        private Dictionary<string, LiveEventData> _lookup;

        public IReadOnlyList<LiveEventData> Events => _events;
        public int Count => _events.Count;

        /// <summary>
        /// ID로 이벤트 조회
        /// </summary>
        public LiveEventData GetById(string id)
        {
            EnsureLookup();
            return _lookup.TryGetValue(id, out var data) ? data : null;
        }

        /// <summary>
        /// 활성 이벤트 목록 조회
        /// </summary>
        public IEnumerable<LiveEventData> GetActiveEvents(DateTime serverTime)
        {
            return _events
                .Where(e => e != null && e.IsActive(serverTime))
                .OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 유예 기간 이벤트 목록 조회
        /// </summary>
        public IEnumerable<LiveEventData> GetGracePeriodEvents(DateTime serverTime)
        {
            return _events
                .Where(e => e != null && e.IsInGracePeriod(serverTime))
                .OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 활성 + 유예 기간 이벤트 목록 조회
        /// </summary>
        public IEnumerable<LiveEventData> GetAvailableEvents(DateTime serverTime, bool includeGracePeriod = true)
        {
            var result = GetActiveEvents(serverTime);
            if (includeGracePeriod)
            {
                result = result.Concat(GetGracePeriodEvents(serverTime));
            }
            return result.OrderBy(e => e.DisplayOrder);
        }

        /// <summary>
        /// 타입별 이벤트 목록 조회
        /// </summary>
        public IEnumerable<LiveEventData> GetByType(LiveEventType eventType)
        {
            return _events.Where(e => e != null && e.EventType == eventType);
        }

        /// <summary>
        /// 유예 기간 만료된 이벤트 목록 (재화 전환 대상)
        /// </summary>
        public IEnumerable<LiveEventData> GetExpiredGracePeriodEvents(DateTime serverTime)
        {
            return _events.Where(e => e != null && e.HasEventCurrency && e.IsGracePeriodExpired(serverTime));
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, LiveEventData>(_events.Count);
            foreach (var evt in _events)
            {
                if (evt != null && !string.IsNullOrEmpty(evt.Id))
                {
                    _lookup[evt.Id] = evt;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(LiveEventData data)
        {
            if (data != null && !_events.Contains(data))
            {
                _events.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _events.Clear();
            _lookup = null;
        }
#endif
    }
}
