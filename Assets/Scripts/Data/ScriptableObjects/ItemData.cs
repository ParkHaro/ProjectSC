using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 아이템 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "SC/Data/Item")]
    public class ItemData : ScriptableObject, IThing, IStackable, IConsumable
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

        [Header("스택 설정")]
        [Tooltip("0 = 스택 불가 (장비), -1 = 무제한")]
        [SerializeField] private int _maxStackCount = 0;

        [Header("소모 설정")]
        [SerializeField] private int _consumeCount = 1;
        [SerializeField] private float _cooldown = 0f;

        [Header("경험치 재료")]
        [Tooltip("캐릭터 경험치 제공량 (경험치 재료 아이템용)")]
        [SerializeField] private int _expValue = 0;
        [Tooltip("사용 시 골드 비용")]
        [SerializeField] private int _goldCostPerUse = 0;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // IThing
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public Rarity Rarity => _rarity;
        public string Description => _description;

        // IStackable
        public int MaxStackCount => _maxStackCount;

        // IConsumable
        public int ConsumeCount => _consumeCount;
        public float Cooldown => _cooldown;

        // 기존 속성
        public ItemType Type => _type;
        public int AtkBonus => _atkBonus;
        public int DefBonus => _defBonus;
        public int HpBonus => _hpBonus;

        // 경험치 재료
        public int ExpValue => _expValue;
        public int GoldCostPerUse => _goldCostPerUse;

        // 헬퍼 프로퍼티
        public bool IsStackable => _maxStackCount != 0;
        public bool IsConsumable => _type == ItemType.Consumable;
        public bool IsExpMaterial => _expValue > 0;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화
        /// </summary>
        public void Initialize(string id, string name, string nameEn, ItemType type,
            Rarity rarity, int atkBonus, int defBonus, int hpBonus, string description,
            int maxStackCount = 0, int consumeCount = 1, float cooldown = 0f,
            int expValue = 0, int goldCostPerUse = 0)
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
            _maxStackCount = maxStackCount;
            _consumeCount = consumeCount;
            _cooldown = cooldown;
            _expValue = expValue;
            _goldCostPerUse = goldCostPerUse;
        }
#endif
    }
}
