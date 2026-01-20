using System;

namespace Sc.Data
{
    /// <summary>
    /// 이벤트 서브 컨텐츠 정보
    /// 이벤트 내 모듈형 서브 컨텐츠 (Mission/Stage/Shop/Minigame)
    /// </summary>
    [Serializable]
    public struct EventSubContent
    {
        /// <summary>
        /// 서브 컨텐츠 타입
        /// </summary>
        public EventSubContentType Type;

        /// <summary>
        /// 컨텐츠 ID (MissionGroupId, StageGroupId, ShopCategoryId 등)
        /// </summary>
        public string ContentId;

        /// <summary>
        /// UI 탭 순서 (0부터)
        /// </summary>
        public int TabOrder;

        /// <summary>
        /// StringData 키 (탭 이름)
        /// </summary>
        public string TabNameKey;

        /// <summary>
        /// 초기 잠금 여부
        /// </summary>
        public bool IsUnlocked;

        /// <summary>
        /// 해금 조건 설명 (선택)
        /// </summary>
        public string UnlockCondition;
    }
}
