using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 아이템 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "SC/Data/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _nameEn;
        [SerializeField] private ItemType _type;
        [SerializeField] private Rarity _rarity;

        [Header("스탯 보너스")]
        [SerializeField] private int _atkBonus;
        [SerializeField] private int _defBonus;
        [SerializeField] private int _hpBonus;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // Properties (읽기 전용)
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public ItemType Type => _type;
        public Rarity Rarity => _rarity;
        public int AtkBonus => _atkBonus;
        public int DefBonus => _defBonus;
        public int HpBonus => _hpBonus;
        public string Description => _description;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화
        /// </summary>
        public void Initialize(string id, string name, string nameEn, ItemType type,
            Rarity rarity, int atkBonus, int defBonus, int hpBonus, string description)
        {
            _id = id;
            _name = name;
            _nameEn = nameEn;
            _type = type;
            _rarity = rarity;
            _atkBonus = atkBonus;
            _defBonus = defBonus;
            _hpBonus = hpBonus;
            _description = description;
        }
#endif
    }
}
