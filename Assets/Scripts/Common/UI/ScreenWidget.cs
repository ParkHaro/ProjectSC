using System;
using Cysharp.Threading.Tasks;

namespace Sc.Common.UI
{
    /// <summary>
    /// Screen 베이스 클래스 (비제네릭).
    /// 구현 클래스: LobbyScreen, BattleScreen 등 (Widget 접미사 없음)
    /// </summary>
    public abstract class ScreenWidget : Widget
    {
        /// <summary>
        /// Screen 상태 캡슐화 (State + Transition).
        /// </summary>
        public abstract class Context : INavigationContext
        {
            public abstract ScreenWidget View { get; }
            public abstract Type ScreenType { get; }

            // INavigationContext 구현
            public NavigationContextType ContextType => NavigationContextType.Screen;
            public Type WidgetType => ScreenType;

            public abstract UniTask Load();
            public abstract UniTask Enter();
            public abstract void Resume();
            public abstract void Pause();
            public abstract UniTask Exit();

            /// <summary>
            /// Screen은 기본적으로 ESC로 닫기 허용.
            /// </summary>
            public virtual bool HandleEscape() => true;
        }
    }

    /// <summary>
    /// Screen 제네릭 베이스 클래스.
    /// 구현 예시: public class LobbyScreen : ScreenWidget&lt;LobbyScreen, LobbyState&gt;
    /// </summary>
    /// <typeparam name="TScreen">Screen 타입 (LobbyScreen 등)</typeparam>
    /// <typeparam name="TState">State 타입</typeparam>
    public abstract class ScreenWidget<TScreen, TState> : ScreenWidget
        where TScreen : ScreenWidget<TScreen, TState>
        where TState : class, IScreenState
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
        /// Screen Context 구현.
        /// </summary>
        public new class Context : ScreenWidget.Context
        {
            private TScreen _screen;
            private TState _state;
            private readonly Transition _transition;

            public override ScreenWidget View => _screen;
            public override Type ScreenType => typeof(TScreen);
            public TState State => _state;

            internal Context(TState state, Transition transition)
            {
                _state = state;
                _transition = transition ?? Transition.Default;
            }

            public override async UniTask Load()
            {
                // TODO: Provider에서 Screen 인스턴스 로드
                // _screen = await ScreenProvider.Instance.Get<TScreen>();
                await UniTask.CompletedTask;
            }

            public override async UniTask Enter()
            {
                _screen?.OnBind(_state);
                await UniTask.CompletedTask;
            }

            public override void Resume()
            {
                _screen?.Show();
            }

            public override void Pause()
            {
                _screen?.Hide();
            }

            public override async UniTask Exit()
            {
                var currentState = _screen?.GetState();
                if (currentState != null)
                    _state = currentState;

                await UniTask.CompletedTask;
            }

            public Transition GetTransition() => _transition;

            #region Builder

            public struct Builder
            {
                private readonly TState _state;
                private Transition _transition;

                public Builder(TState state)
                {
                    _state = state;
                    _transition = null;
                }

                public Builder SetTransition(Transition transition)
                {
                    _transition = transition;
                    return this;
                }

                public Context Build() => new(_state, _transition ?? Transition.Default);

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
        /// Screen 열기.
        /// </summary>
        public static void Open(TState state = default, Transition transition = null)
        {
            var builder = CreateContext(state);
            if (transition != null)
                builder.SetTransition(transition);

            // NavigationManager.Instance.Push(builder);
        }

        #endregion
    }
}
