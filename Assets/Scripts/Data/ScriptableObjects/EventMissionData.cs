using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 미션 마스터 데이터 (구조만)
    /// </summary>
    [CreateAssetMenu(fileName = "EventMissionData", menuName = "SC/Data/EventMissionData")]
    public class EventMissionData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _missionGroupId;
        [SerializeField] private string _nameKey;
        [SerializeField] private string _descriptionKey;

        [Header("조건")]
        [SerializeField] private MissionConditionType _conditionType;
        [SerializeField] private string _conditionTargetId;
        [SerializeField] private int _requiredCount;

        [Header("보상")]
        [SerializeField] private List<RewardInfo> _rewards = new();

        [Header("표시")]
        [SerializeField] private int _displayOrder;
        [SerializeField] private bool _isHidden;

        #region Properties

        public string Id => _id;
        public string MissionGroupId => _missionGroupId;
        public string NameKey => _nameKey;
        public string DescriptionKey => _descriptionKey;
        public MissionConditionType ConditionType => _conditionType;
        public string ConditionTargetId => _conditionTargetId;
        public int RequiredCount => _requiredCount;
        public IReadOnlyList<RewardInfo> Rewards => _rewards;
        public int DisplayOrder => _displayOrder;
        public bool IsHidden => _isHidden;

        #endregion

#if UNITY_EDITOR
        public void Initialize(
            string id,
            string missionGroupId,
            string nameKey,
            string descriptionKey,
            MissionConditionType conditionType,
            string conditionTargetId,
            int requiredCount,
            List<RewardInfo> rewards,
            int displayOrder,
            bool isHidden)
        {
            _id = id;
            _missionGroupId = missionGroupId;
            _nameKey = nameKey;
            _descriptionKey = descriptionKey;
            _conditionType = conditionType;
            _conditionTargetId = conditionTargetId;
            _requiredCount = requiredCount;
            _rewards = rewards ?? new List<RewardInfo>();
            _displayOrder = displayOrder;
            _isHidden = isHidden;
        }
#endif
    }
}
