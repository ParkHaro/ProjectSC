using System;
using Cysharp.Threading.Tasks;

namespace Sc.Common.UI
{
    /// <summary>
    /// Screen/Popup 공통 Navigation Context 인터페이스.
    /// 통합 스택에서 동일하게 처리하기 위한 추상화.
    /// </summary>
    public interface INavigationContext
    {
        /// <summary>
        /// Context 타입 (Screen 또는 Popup).
        /// </summary>
        NavigationContextType ContextType { get; }

        /// <summary>
        /// 실제 Widget 타입.
        /// </summary>
        Type WidgetType { get; }

        /// <summary>
        /// Widget 인스턴스 (런타임).
        /// </summary>
        Widget View { get; }

        /// <summary>
        /// 리소스 로드.
        /// </summary>
        UniTask Load();

        /// <summary>
        /// 화면 진입.
        /// </summary>
        UniTask Enter();

        /// <summary>
        /// 활성화 (표시).
        /// </summary>
        void Resume();

        /// <summary>
        /// 비활성화 (숨김).
        /// </summary>
        void Pause();

        /// <summary>
        /// 화면 퇴장.
        /// </summary>
        UniTask Exit();

        /// <summary>
        /// ESC 키 처리. true 반환 시 닫기 허용.
        /// Screen은 기본 true, Popup은 OnEscape() 위임.
        /// </summary>
        bool HandleEscape();
    }

    /// <summary>
    /// Navigation Context 타입.
    /// </summary>
    public enum NavigationContextType
    {
        Screen,
        Popup
    }
}
