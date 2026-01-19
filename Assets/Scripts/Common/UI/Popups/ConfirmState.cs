using System;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// ConfirmPopup 상태. 제목, 메시지, 버튼 텍스트, 콜백을 포함.
    /// </summary>
    public class ConfirmState : IPopupState
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
        /// 확인 버튼 텍스트
        /// </summary>
        public string ConfirmText { get; set; } = "확인";

        /// <summary>
        /// 취소 버튼 텍스트
        /// </summary>
        public string CancelText { get; set; } = "취소";

        /// <summary>
        /// 취소 버튼 표시 여부. false면 Alert 모드 (확인만)
        /// </summary>
        public bool ShowCancelButton { get; set; } = true;

        /// <summary>
        /// 확인 버튼 콜백
        /// </summary>
        public Action OnConfirm { get; set; }

        /// <summary>
        /// 취소 버튼 콜백 (배경 터치 시에도 호출)
        /// </summary>
        public Action OnCancel { get; set; }

        /// <summary>
        /// State 유효성 검증. Message가 비어있으면 경고 출력.
        /// </summary>
        /// <returns>항상 true (표시는 허용)</returns>
        public bool Validate()
        {
            if (string.IsNullOrEmpty(Message))
            {
                Debug.LogWarning("[ConfirmPopup] Message가 비어있음");
            }
            return true;
        }
    }
}
