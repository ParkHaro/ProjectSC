using System;
using Cysharp.Threading.Tasks;
using Sc.Core;
using UnityEngine;
using Object = UnityEngine.Object;

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
            Widget INavigationContext.View => View;

            public abstract UniTask Load();
            public abstract UniTask Enter();
            public abstract void Resume();
            public abstract void Pause();
            public abstract UniTask Exit();

            /// <summary>
            /// Screen은 기본적으로 ESC로 닫기 허용.
            /// </summary>
            public virtual bool HandleEscape() => true;

            /// <summary>
            /// Screen 전환 애니메이션 반환.
            /// </summary>
            public abstract Transition GetTransition();
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
        protected virtual void OnBind(TState state)
        {
        }

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
            private AssetScope _scope;
            private bool _isInstantiated;
            private bool _isLoading;

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
                // Race condition 방지
                if (_isLoading || _screen != null) return;
                _isLoading = true;

                try
                {
                    // 1. 씬에서 먼저 찾기 (기존 방식 하위 호환)
                    _screen = FindObjectOfType<TScreen>(true);

                    if (_screen != null)
                    {
                        _isInstantiated = false;
                        return;
                    }

                    // 2. Addressables에서 로드
                    if (!AssetManager.HasInstance)
                    {
                        Debug.LogError($"[ScreenWidget] AssetManager가 초기화되지 않음");
                        return;
                    }

                    // Canvas 체크
                    var parent = NavigationManager.Instance?.ScreenCanvas?.transform;
                    if (parent == null)
                    {
                        Debug.LogError($"[ScreenWidget] ScreenCanvas가 없어서 {typeof(TScreen).Name} 로드 불가");
                        return;
                    }

                    _scope = AssetManager.Instance.CreateScope($"Screen_{typeof(TScreen).Name}");
                    var result = await AssetManager.Instance.LoadScreenPrefabAsync<TScreen>(_scope);

                    if (!result.IsSuccess)
                    {
                        Debug.LogError($"[ScreenWidget] {typeof(TScreen).Name} 로드 실패: {result.Error}");
                        CleanupScope();
                        return;
                    }

                    // 3. 프리팹 인스턴스화
                    var prefab = result.Value;
                    var instance = Instantiate(prefab, parent);
                    instance.name = typeof(TScreen).Name;

                    _screen = instance.GetComponent<TScreen>();
                    _isInstantiated = true;

                    if (_screen == null)
                    {
                        Debug.LogError($"[ScreenWidget] 프리팹에 {typeof(TScreen).Name} 컴포넌트가 없음");
                        Destroy(instance);
                        _isInstantiated = false;
                        CleanupScope();
                    }
                }
                finally
                {
                    _isLoading = false;
                }
            }

            private void CleanupScope()
            {
                if (_scope != null && AssetManager.HasInstance)
                {
                    AssetManager.Instance.ReleaseScope(_scope);
                    _scope = null;
                }
            }

            public override async UniTask Enter()
            {
                _screen?.Initialize();
                _screen?.OnBind(_state);
                await UniTask.CompletedTask;
            }

            public override void Resume()
            {
                // 상호작용 다시 활성화 (화면은 이미 보이는 상태)
                _screen?.SetInteractable(true);
            }

            public override void Pause()
            {
                // 상호작용만 비활성화 (화면은 보이게 유지)
                _screen?.SetInteractable(false);
            }

            public override async UniTask Exit()
            {
                // 상태 저장
                var currentState = _screen?.GetState();
                if (currentState != null)
                    _state = currentState;

                // UI 숨김
                _screen?.Hide();

                // Addressables로 로드된 인스턴스 정리
                if (_isInstantiated && _screen != null)
                {
                    Object.Destroy(_screen.gameObject);
                    _screen = null;
                }

                // Scope 해제 (에셋 RefCount 감소)
                if (_scope != null)
                {
                    AssetManager.Instance?.ReleaseScope(_scope);
                    _scope = null;
                }

                await UniTask.CompletedTask;
            }

            public override Transition GetTransition() => _transition;

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
        /// Screen 열기 (Push).
        /// 사용법: LobbyScreen.Open(new LobbyState());
        /// </summary>
        public static void Open(TState state = default, Transition transition = null)
        {
            var builder = CreateContext(state);
            if (transition != null)
                builder.SetTransition(transition);

            NavigationManager.Instance?.Push(builder);
        }
        #endregion
    }
}