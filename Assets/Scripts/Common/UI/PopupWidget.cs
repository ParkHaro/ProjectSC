using System;
using Cysharp.Threading.Tasks;

namespace Sc.Common.UI
{
    /// <summary>
    /// Popup 베이스 클래스 (비제네릭).
    /// 구현 클래스: ConfirmPopup, RewardPopup 등 (Widget 접미사 없음)
    /// </summary>
    public abstract class PopupWidget : Widget
    {
        /// <summary>
        /// ESC 키 처리. true 반환 시 닫기 허용.
        /// </summary>
        public virtual bool OnEscape() => true;

        /// <summary>
        /// Popup 상태 캡슐화 (State + Order).
        /// </summary>
        public abstract class Context : INavigationContext
        {
            public abstract PopupWidget View { get; }
            public abstract Type PopupType { get; }

            // INavigationContext 구현
            public NavigationContextType ContextType => NavigationContextType.Popup;
            public Type WidgetType => PopupType;

            public abstract UniTask Load();
            public abstract UniTask Enter();
            public abstract void Resume();
            public abstract void Pause();
            public abstract UniTask Exit();

            /// <summary>
            /// Popup의 OnEscape()에 위임.
            /// </summary>
            public virtual bool HandleEscape() => View?.OnEscape() ?? true;
        }
    }

    /// <summary>
    /// Popup 제네릭 베이스 클래스.
    /// 구현 예시: public class ConfirmPopup : PopupWidget&lt;ConfirmPopup, ConfirmState&gt;
    /// </summary>
    /// <typeparam name="TPopup">Popup 타입 (ConfirmPopup 등)</typeparam>
    /// <typeparam name="TState">State 타입</typeparam>
    public abstract class PopupWidget<TPopup, TState> : PopupWidget
        where TPopup : PopupWidget<TPopup, TState>
        where TState : class, IPopupState
    {
        #region Lifecycle Hooks

        /// <summary>
        /// State로 UI 초기화. 서브클래스에서 오버라이드.
        /// </summary>
        protected virtual void OnBind(TState state) { }

        /// <summary>
        /// 현재 상태 반환. 서브클래스에서 오버라이드.
        /// </summary>
        public virtual TState GetState() => default;

        #endregion

        #region Context

        /// <summary>
        /// Popup Context 구현.
        /// </summary>
        public new class Context : PopupWidget.Context
        {
            private TPopup _popup;
            private TState _state;
            private readonly int _sortingOrder;

            public override PopupWidget View => _popup;
            public override Type PopupType => typeof(TPopup);
            public TState State => _state;
            public int SortingOrder => _sortingOrder;

            internal Context(TState state, int sortingOrder)
            {
                _state = state;
                _sortingOrder = sortingOrder;
            }

            public override async UniTask Load()
            {
                // TODO: Provider에서 Popup 인스턴스 로드
                // _popup = await PopupProvider.Instance.Get<TPopup>();
                await UniTask.CompletedTask;
            }

            public override async UniTask Enter()
            {
                _popup?.OnBind(_state);
                await UniTask.CompletedTask;
            }

            public override void Resume()
            {
                _popup?.Show();
            }

            public override void Pause()
            {
                _popup?.Hide();
            }

            public override async UniTask Exit()
            {
                var currentState = _popup?.GetState();
                if (currentState != null)
                    _state = currentState;

                await UniTask.CompletedTask;
            }

            #region Builder

            public struct Builder
            {
                private readonly TState _state;
                private int _sortingOrder;

                public Builder(TState state)
                {
                    _state = state;
                    _sortingOrder = 0;
                }

                public Builder SetOrder(int order)
                {
                    _sortingOrder = order;
                    return this;
                }

                public Context Build() => new(_state, _sortingOrder);

                public static implicit operator Context(Builder builder) => builder.Build();
            }

            #endregion
        }

        #endregion

        #region Static Entry Points

        /// <summary>
        /// Context Builder 생성.
        /// </summary>
        public static Context.Builder CreateContext(TState state = default) => new(state);

        /// <summary>
        /// Popup 열기.
        /// </summary>
        public static void Open(TState state = default)
        {
            var context = CreateContext(state).Build();
            // NavigationManager.Instance.PushPopup(context);
        }

        #endregion
    }
}
