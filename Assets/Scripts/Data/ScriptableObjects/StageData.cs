using System;
using System.Collections.Generic;
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
        [SerializeField] private InGameContentType _contentType;
        [SerializeField] private string _categoryId;
        [SerializeField] private StageType _stageType;
        [SerializeField] private int _chapter;
        [SerializeField] private int _stageNumber;
        [SerializeField] private Difficulty _difficulty;

        [Header("입장 조건")]
        [SerializeField] private CostType _entryCostType = CostType.Stamina;
        [SerializeField] private int _entryCost;
        [SerializeField] private LimitType _limitType = LimitType.None;
        [SerializeField] private int _limitCount;
        [SerializeField] private DayOfWeek[] _availableDays;

        [Header("해금 조건")]
        [SerializeField] private string _unlockConditionStageId;
        [SerializeField] private int _unlockConditionLevel;

        [Header("전투 정보")]
        [SerializeField] private int _recommendedPower;
        [SerializeField] private string[] _enemyIds;

        [Header("보상 (레거시)")]
        [SerializeField] private int _rewardGold;
        [SerializeField] private int _rewardExp;
        [SerializeField] private string[] _rewardItemIds;
        [SerializeField] private float[] _rewardItemRates;

        [Header("보상 (신규)")]
        [SerializeField] private List<RewardInfo> _firstClearRewards;
        [SerializeField] private List<RewardInfo> _repeatClearRewards;

        [Header("별점 조건")]
        [SerializeField] private StarCondition _star1Condition;
        [SerializeField] private StarCondition _star2Condition;
        [SerializeField] private StarCondition _star3Condition;

        [Header("표시")]
        [SerializeField] private int _displayOrder;
        [SerializeField] private bool _isEnabled = true;

        [Header("설명")]
        [TextArea(2, 4)]
        [SerializeField] private string _description;

        // Properties - 기본 정보
        public string Id => _id;
        public string Name => _name;
        public string NameEn => _nameEn;
        public InGameContentType ContentType => _contentType;
        public string CategoryId => _categoryId;
        public StageType StageType => _stageType;
        public int Chapter => _chapter;
        public int StageNumber => _stageNumber;
        public Difficulty Difficulty => _difficulty;
        public string Description => _description;

        // Properties - 입장 조건
        public CostType EntryCostType => _entryCostType;
        public int EntryCost => _entryCost;
        /// <summary>
        /// 레거시 호환용. EntryCost 사용 권장.
        /// </summary>
        public int StaminaCost => _entryCostType == CostType.Stamina ? _entryCost : 0;
        public LimitType LimitType => _limitType;
        public int LimitCount => _limitCount;
        public DayOfWeek[] AvailableDays => _availableDays;

        // Properties - 해금 조건
        public string UnlockConditionStageId => _unlockConditionStageId;
        public int UnlockConditionLevel => _unlockConditionLevel;

        // Properties - 전투 정보
        public int RecommendedPower => _recommendedPower;
        public string[] EnemyIds => _enemyIds;

        // Properties - 보상 (레거시)
        public int RewardGold => _rewardGold;
        public int RewardExp => _rewardExp;
        public string[] RewardItemIds => _rewardItemIds;
        public float[] RewardItemRates => _rewardItemRates;

        // Properties - 보상 (신규)
        public List<RewardInfo> FirstClearRewards => _firstClearRewards;
        public List<RewardInfo> RepeatClearRewards => _repeatClearRewards;

        // Properties - 별점 조건
        public StarCondition Star1Condition => _star1Condition;
        public StarCondition Star2Condition => _star2Condition;
        public StarCondition Star3Condition => _star3Condition;

        // Properties - 표시
        public int DisplayOrder => _displayOrder;
        public bool IsEnabled => _isEnabled;

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: JSON 데이터로 초기화 (레거시 호환)
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
            _entryCostType = CostType.Stamina;
            _entryCost = staminaCost;
            _recommendedPower = recommendedPower;
            _enemyIds = enemyIds;
            _rewardGold = rewardGold;
            _rewardExp = rewardExp;
            _rewardItemIds = rewardItemIds;
            _rewardItemRates = rewardItemRates;
            _description = description;
            _isEnabled = true;
        }

        /// <summary>
        /// Editor 전용: 전체 필드 초기화
        /// </summary>
        public void InitializeFull(
            string id,
            string name,
            string nameEn,
            InGameContentType contentType,
            string categoryId,
            StageType stageType,
            int chapter,
            int stageNumber,
            Difficulty difficulty,
            CostType entryCostType,
            int entryCost,
            LimitType limitType,
            int limitCount,
            DayOfWeek[] availableDays,
            string unlockConditionStageId,
            int unlockConditionLevel,
            int recommendedPower,
            string[] enemyIds,
            List<RewardInfo> firstClearRewards,
            List<RewardInfo> repeatClearRewards,
            StarCondition star1,
            StarCondition star2,
            StarCondition star3,
            int displayOrder,
            bool isEnabled,
            string description)
        {
            _id = id;
            _name = name;
            _nameEn = nameEn;
            _contentType = contentType;
            _categoryId = categoryId;
            _stageType = stageType;
            _chapter = chapter;
            _stageNumber = stageNumber;
            _difficulty = difficulty;
            _entryCostType = entryCostType;
            _entryCost = entryCost;
            _limitType = limitType;
            _limitCount = limitCount;
            _availableDays = availableDays;
            _unlockConditionStageId = unlockConditionStageId;
            _unlockConditionLevel = unlockConditionLevel;
            _recommendedPower = recommendedPower;
            _enemyIds = enemyIds;
            _firstClearRewards = firstClearRewards;
            _repeatClearRewards = repeatClearRewards;
            _star1Condition = star1;
            _star2Condition = star2;
            _star3Condition = star3;
            _displayOrder = displayOrder;
            _isEnabled = isEnabled;
            _description = description;
        }
#endif
    }
}
