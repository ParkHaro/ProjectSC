using System;
using Sc.Data;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// CostConfirmPopup 상태. 재화 소모 확인에 필요한 정보 포함.
    /// </summary>
    public class CostConfirmState : IPopupState
    {
        /// <summary>
        /// 팝업 제목
        /// </summary>
        public string Title { get; set; } = "확인";

        /// <summary>
        /// 본문 메시지
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 소모 재화 타입
        /// </summary>
        public CostType CostType { get; set; } = CostType.Gold;

        /// <summary>
        /// 소모량
        /// </summary>
        public int CostAmount { get; set; }

        /// <summary>
        /// 현재 보유량. null이면 표시 안 함.
        /// </summary>
        public int? CurrentAmount { get; set; }

        /// <summary>
        /// 확인 버튼 텍스트
        /// </summary>
        public string ConfirmText { get; set; } = "확인";

        /// <summary>
        /// 취소 버튼 텍스트
        /// </summary>
        public string CancelText { get; set; } = "취소";

        /// <summary>
        /// 확인 버튼 콜백
        /// </summary>
        public Action OnConfirm { get; set; }

        /// <summary>
        /// 취소 버튼 콜백 (배경 터치 시에도 호출)
        /// </summary>
        public Action OnCancel { get; set; }

        /// <summary>
        /// 재화 부족 여부
        /// </summary>
        public bool IsInsufficient => CurrentAmount.HasValue && CurrentAmount.Value < CostAmount;

        /// <summary>
        /// State 유효성 검증. CostAmount가 0 이하면 false.
        /// </summary>
        /// <returns>유효하면 true</returns>
        public bool Validate()
        {
            if (CostAmount <= 0)
            {
                Debug.LogWarning("[CostConfirmPopup] CostAmount가 0 이하");
                return false;
            }
            return true;
        }
    }
}
