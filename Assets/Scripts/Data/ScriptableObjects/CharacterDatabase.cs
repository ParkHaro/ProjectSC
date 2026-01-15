using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 캐릭터 데이터베이스 (마스터 데이터 컬렉션)
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "SC/Database/Character")]
    public class CharacterDatabase : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characters = new();

        private Dictionary<string, CharacterData> _lookup;

        /// <summary>
        /// 전체 캐릭터 목록
        /// </summary>
        public IReadOnlyList<CharacterData> Characters => _characters;

        /// <summary>
        /// 캐릭터 수
        /// </summary>
        public int Count => _characters.Count;

        /// <summary>
        /// ID로 캐릭터 조회
        /// </summary>
        public CharacterData GetById(string id)
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
        /// 희귀도별 캐릭터 조회
        /// </summary>
        public IEnumerable<CharacterData> GetByRarity(Rarity rarity)
        {
            foreach (var character in _characters)
            {
                if (character.Rarity == rarity)
                    yield return character;
            }
        }

        /// <summary>
        /// 속성별 캐릭터 조회
        /// </summary>
        public IEnumerable<CharacterData> GetByElement(Element element)
        {
            foreach (var character in _characters)
            {
                if (character.Element == element)
                    yield return character;
            }
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, CharacterData>(_characters.Count);
            foreach (var character in _characters)
            {
                if (character != null && !string.IsNullOrEmpty(character.Id))
                {
                    _lookup[character.Id] = character;
                }
            }
        }

        private void OnEnable()
        {
            _lookup = null; // 재생성 강제
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: 캐릭터 추가
        /// </summary>
        public void Add(CharacterData data)
        {
            if (data != null && !_characters.Contains(data))
            {
                _characters.Add(data);
                _lookup = null;
            }
        }

        /// <summary>
        /// Editor 전용: 전체 클리어
        /// </summary>
        public void Clear()
        {
            _characters.Clear();
            _lookup = null;
        }
#endif
    }
}
