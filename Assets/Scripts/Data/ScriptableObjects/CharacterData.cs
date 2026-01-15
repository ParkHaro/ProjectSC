using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 캐릭터 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterData", menuName = "SC/Data/Character")]
    public class CharacterData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _nameEn;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private CharacterClass _characterClass;
        [SerializeField] private Element _element;

        [Header("기본 스탯")]
        [SerializeField] private int _baseHp;
        [SerializeField] private int _baseAtk;
        [SerializeField] private int _baseDef;
        [SerializeField] private int _baseSpd;

        [Header("치명타")]
        [SerializeField] private float _critRate;
        [SerializeField] private float _critDamage;

        [Header("스킬")]
        [SerializeField] private string[] _skillIds;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // Properties (읽기 전용)
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public Rarity Rarity => _rarity;
        public CharacterClass CharacterClass => _characterClass;
        public Element Element => _element;
        public int BaseHp => _baseHp;
        public int BaseAtk => _baseAtk;
        public int BaseDef => _baseDef;
        public int BaseSpd => _baseSpd;
        public float CritRate => _critRate;
        public float CritDamage => _critDamage;
        public string[] SkillIds => _skillIds;
        public string Description => _description;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화
        /// </summary>
        public void Initialize(string id, string name, string nameEn, Rarity rarity,
            CharacterClass characterClass, Element element, int baseHp, int baseAtk,
            int baseDef, int baseSpd, float critRate, float critDamage,
            string[] skillIds, string description)
        {
            _id = id;
            _name = name;
            _nameEn = nameEn;
            _rarity = rarity;
            _characterClass = characterClass;
            _element = element;
            _baseHp = baseHp;
            _baseAtk = baseAtk;
            _baseDef = baseDef;
            _baseSpd = baseSpd;
            _critRate = critRate;
            _critDamage = critDamage;
            _skillIds = skillIds;
            _description = description;
        }
#endif
    }
}
