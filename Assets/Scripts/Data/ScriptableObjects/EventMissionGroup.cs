using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 미션 그룹 (구조만)
    /// </summary>
    [CreateAssetMenu(fileName = "EventMissionGroup", menuName = "SC/Data/EventMissionGroup")]
    public class EventMissionGroup : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _eventId;
        [SerializeField] private List<EventMissionData> _missions = new();

        private Dictionary<string, EventMissionData> _lookup;

        public string Id => _id;
        public string EventId => _eventId;
        public IReadOnlyList<EventMissionData> Missions => _missions;
        public int Count => _missions.Count;

        /// <summary>
        /// ID로 미션 조회
        /// </summary>
        public EventMissionData GetMission(string missionId)
        {
            EnsureLookup();
            return _lookup.TryGetValue(missionId, out var data) ? data : null;
        }

        /// <summary>
        /// 표시 가능한 미션 목록 (숨김 제외)
        /// </summary>
        public IEnumerable<EventMissionData> GetVisibleMissions()
        {
            return _missions
                .Where(m => m != null && !m.IsHidden)
                .OrderBy(m => m.DisplayOrder);
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, EventMissionData>(_missions.Count);
            foreach (var mission in _missions)
            {
                if (mission != null && !string.IsNullOrEmpty(mission.Id))
                {
                    _lookup[mission.Id] = mission;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Initialize(string id, string eventId)
        {
            _id = id;
            _eventId = eventId;
        }

        public void Add(EventMissionData data)
        {
            if (data != null && !_missions.Contains(data))
            {
                _missions.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _missions.Clear();
            _lookup = null;
        }
#endif
    }
}
