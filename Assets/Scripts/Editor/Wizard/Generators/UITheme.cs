using UnityEngine;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// UI 테마 정의 (Luminous Dark Fantasy).
    /// 컬러, 레이아웃 상수를 중앙 관리.
    /// </summary>
    public static class UITheme
    {
        #region Colors

        /// <summary>배경 (깊은 어둠)</summary>
        public static readonly Color BgDeep = new Color32(10, 10, 18, 255);

        /// <summary>카드/패널 배경</summary>
        public static readonly Color BgCard = new Color32(25, 25, 45, 217);

        /// <summary>글래스 효과</summary>
        public static readonly Color BgGlass = new Color32(255, 255, 255, 8);

        /// <summary>팝업 백드롭 (딤 처리)</summary>
        public static readonly Color Backdrop = new Color(0, 0, 0, 0.7f);

        /// <summary>주요 강조색 (시안)</summary>
        public static readonly Color AccentPrimary = new Color32(0, 212, 255, 255);

        /// <summary>보조 강조색 (핑크)</summary>
        public static readonly Color AccentSecondary = new Color32(255, 107, 157, 255);

        /// <summary>골드 강조색</summary>
        public static readonly Color AccentGold = new Color32(255, 215, 0, 255);

        /// <summary>퍼플 강조색</summary>
        public static readonly Color AccentPurple = new Color32(168, 85, 247, 255);

        /// <summary>성공/긍정</summary>
        public static readonly Color AccentSuccess = new Color32(34, 197, 94, 255);

        /// <summary>경고</summary>
        public static readonly Color AccentWarning = new Color32(251, 191, 36, 255);

        /// <summary>위험/오류</summary>
        public static readonly Color AccentDanger = new Color32(239, 68, 68, 255);

        /// <summary>기본 텍스트 (흰색)</summary>
        public static readonly Color TextPrimary = Color.white;

        /// <summary>보조 텍스트</summary>
        public static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);

        /// <summary>비활성 텍스트</summary>
        public static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.4f);

        /// <summary>버튼 위 텍스트 (어두운)</summary>
        public static readonly Color TextOnButton = new Color32(10, 10, 18, 255);

        #endregion

        #region Layout

        /// <summary>헤더 높이</summary>
        public const float HeaderHeight = 80f;

        /// <summary>간소화된 헤더 높이 (Back 버튼만)</summary>
        public const float BackHeaderHeight = 60f;

        /// <summary>푸터 높이</summary>
        public const float FooterHeight = 80f;

        /// <summary>하단 네비게이션 높이</summary>
        public const float BottomNavHeight = 100f;

        /// <summary>기본 패딩</summary>
        public const float Padding = 16f;

        /// <summary>작은 패딩</summary>
        public const float PaddingSmall = 8f;

        /// <summary>큰 패딩</summary>
        public const float PaddingLarge = 24f;

        /// <summary>기본 간격</summary>
        public const float Spacing = 12f;

        /// <summary>작은 간격</summary>
        public const float SpacingSmall = 8f;

        /// <summary>큰 간격</summary>
        public const float SpacingLarge = 16f;

        /// <summary>모서리 둥글기</summary>
        public const float CornerRadius = 8f;

        #endregion

        #region Popup Sizes

        /// <summary>확인 팝업 크기</summary>
        public static readonly Vector2 PopupConfirmSize = new Vector2(500, 300);

        /// <summary>보상 팝업 크기</summary>
        public static readonly Vector2 PopupRewardSize = new Vector2(600, 500);

        /// <summary>정보 팝업 크기</summary>
        public static readonly Vector2 PopupInfoSize = new Vector2(600, 700);

        /// <summary>작은 팝업 크기</summary>
        public static readonly Vector2 PopupSmallSize = new Vector2(400, 250);

        #endregion

        #region Typography

        /// <summary>제목 폰트 크기</summary>
        public const float FontSizeTitle = 24f;

        /// <summary>부제목 폰트 크기</summary>
        public const float FontSizeSubtitle = 18f;

        /// <summary>본문 폰트 크기</summary>
        public const float FontSizeBody = 14f;

        /// <summary>캡션 폰트 크기</summary>
        public const float FontSizeCaption = 12f;

        /// <summary>작은 폰트 크기</summary>
        public const float FontSizeSmall = 10f;

        #endregion

        #region Button Heights

        /// <summary>큰 버튼 높이</summary>
        public const float ButtonHeightLarge = 56f;

        /// <summary>기본 버튼 높이</summary>
        public const float ButtonHeightNormal = 44f;

        /// <summary>작은 버튼 높이</summary>
        public const float ButtonHeightSmall = 32f;

        #endregion
    }
}