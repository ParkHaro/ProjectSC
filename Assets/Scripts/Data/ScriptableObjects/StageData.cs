using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스테이지 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "StageData", menuName = "SC/Data/Stage")]
    public class StageData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _nameEn;
        [SerializeField] private int _chapter;
        [SerializeField] private int _stageNumber;
        [SerializeField] private Difficulty _difficulty;

        [Header("요구 사항")]
        [SerializeField] private int _staminaCost;
        [SerializeField] private int _recommendedPower;

        [Header("적 구성")]
        [SerializeField] private string[] _enemyIds;

        [Header("보상")]
        [SerializeField] private int _rewardGold;
        [SerializeField] private int _rewardExp;
        [SerializeField] private string[] _rewardItemIds;
        [SerializeField] private float[] _rewardItemRates;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // Properties (읽기 전용)
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public int Chapter => _chapter;
        public int StageNumber => _stageNumber;
        public Difficulty Difficulty => _difficulty;
        public int StaminaCost => _staminaCost;
        public int RecommendedPower => _recommendedPower;
        public string[] EnemyIds => _enemyIds;
        public int RewardGold => _rewardGold;
        public int RewardExp => _rewardExp;
        public string[] RewardItemIds => _rewardItemIds;
        public float[] RewardItemRates => _rewardItemRates;
        public string Description => _description;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화
        /// </summary>
        public void Initialize(string id, string name, string nameEn, int chapter,
            int stageNumber, Difficulty difficulty, int staminaCost, int recommendedPower,
            string[] enemyIds, int rewardGold, int rewardExp, string[] rewardItemIds,
            float[] rewardItemRates, string description)
        {
            _id = id;
            _name = name;
            _nameEn = nameEn;
            _chapter = chapter;
            _stageNumber = stageNumber;
            _difficulty = difficulty;
            _staminaCost = staminaCost;
            _recommendedPower = recommendedPower;
            _enemyIds = enemyIds;
            _rewardGold = rewardGold;
            _rewardExp = rewardExp;
            _rewardItemIds = rewardItemIds;
            _rewardItemRates = rewardItemRates;
            _description = description;
        }
#endif
    }
}
