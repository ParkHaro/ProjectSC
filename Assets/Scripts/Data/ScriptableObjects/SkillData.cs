using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스킬 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "SkillData", menuName = "SC/Data/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _nameEn;
        [SerializeField] private SkillType _type;
        [SerializeField] private TargetType _targetType;
        [SerializeField] private Element _element;

        [Header("스킬 수치")]
        [SerializeField] private int _power;
        [SerializeField] private int _coolDown;
        [SerializeField] private int _manaCost;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // Properties (읽기 전용)
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public SkillType Type => _type;
        public TargetType TargetType => _targetType;
        public Element Element => _element;
        public int Power => _power;
        public int CoolDown => _coolDown;
        public int ManaCost => _manaCost;
        public string Description => _description;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화
        /// </summary>
        public void Initialize(string id, string name, string nameEn, SkillType type,
            TargetType targetType, Element element, int power, int coolDown,
            int manaCost, string description)
        {
            _id = id;
            _name = name;
            _nameEn = nameEn;
            _type = type;
            _targetType = targetType;
            _element = element;
            _power = power;
            _coolDown = coolDown;
            _manaCost = manaCost;
            _description = description;
        }
#endif
    }
}
