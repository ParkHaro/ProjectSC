using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스킬 데이터베이스 (마스터 데이터 컬렉션)
    /// </summary>
    [CreateAssetMenu(fileName = "SkillDatabase", menuName = "SC/Database/Skill")]
    public class SkillDatabase : ScriptableObject
    {
        [SerializeField] private List<SkillData> _skills = new();

        private Dictionary<string, SkillData> _lookup;

        /// <summary>
        /// 전체 스킬 목록
        /// </summary>
        public IReadOnlyList<SkillData> Skills => _skills;

        /// <summary>
        /// 스킬 수
        /// </summary>
        public int Count => _skills.Count;

        /// <summary>
        /// ID로 스킬 조회
        /// </summary>
        public SkillData GetById(string id)
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
        /// 속성별 스킬 조회
        /// </summary>
        public IEnumerable<SkillData> GetByElement(Element element)
        {
            foreach (var skill in _skills)
            {
                if (skill.Element == element)
                    yield return skill;
            }
        }

        /// <summary>
        /// 타입별 스킬 조회
        /// </summary>
        public IEnumerable<SkillData> GetByType(SkillType type)
        {
            foreach (var skill in _skills)
            {
                if (skill.Type == type)
                    yield return skill;
            }
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, SkillData>(_skills.Count);
            foreach (var skill in _skills)
            {
                if (skill != null && !string.IsNullOrEmpty(skill.Id))
                {
                    _lookup[skill.Id] = skill;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null;
        }

#if UNITY_EDITOR
        public void Add(SkillData data)
        {
            if (data != null && !_skills.Contains(data))
            {
                _skills.Add(data);
                _lookup = null;
            }
        }

        public void Clear()
        {
            _skills.Clear();
            _lookup = null;
        }
#endif
    }
}
