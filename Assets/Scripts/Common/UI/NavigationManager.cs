using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// Screen/Popup 통합 스택 기반 화면 전환 및 히스토리 관리.
    /// Push 순서대로 단일 스택에 쌓이며, Pop 시 역순으로 제거됨.
    /// </summary>
    public class NavigationManager : MonoBehaviour
    {
        public static NavigationManager Instance { get; private set; }

        private readonly List<INavigationContext> _navigationStack = new();
        private bool _isTransitioning;

        #region Properties

        /// <summary>
        /// 현재 최상위 Screen (Popup 제외).
        /// </summary>
        public ScreenWidget CurrentScreen
        {
            get
            {
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i] is ScreenWidget.Context screenContext)
                        return screenContext.View;
                }
                return null;
            }
        }

        /// <summary>
        /// 현재 최상위 항목 (Screen 또는 Popup).
        /// </summary>
        public INavigationContext CurrentContext =>
            _navigationStack.Count > 0 ? _navigationStack[^1] : null;

        /// <summary>
        /// 전체 스택 개수.
        /// </summary>
        public int StackCount => _navigationStack.Count;

        /// <summary>
        /// Screen 개수.
        /// </summary>
        public int ScreenCount
        {
            get
            {
                int count = 0;
                foreach (var ctx in _navigationStack)
                {
                    if (ctx.ContextType == NavigationContextType.Screen)
                        count++;
                }
                return count;
            }
        }

        /// <summary>
        /// 최상위 항목이 Popup인지 여부.
        /// </summary>
        public bool HasPopupOnTop =>
            CurrentContext?.ContextType == NavigationContextType.Popup;

        /// <summary>
        /// Popup 개수.
        /// </summary>
        public int PopupCount => StackCount - ScreenCount;

        /// <summary>
        /// 전환 중 여부.
        /// </summary>
        public bool IsTransitioning => _isTransitioning;

        /// <summary>
        /// 네비게이션 스택 (읽기 전용, 디버그용).
        /// </summary>
        public IReadOnlyList<INavigationContext> NavigationStack => _navigationStack;

        #endregion

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }

        #endregion

        #region Push Operations

        /// <summary>
        /// Screen Push (중복 제거 적용).
        /// </summary>
        public void Push(ScreenWidget.Context context)
        {
            PushAsync(context).Forget();
        }

        /// <summary>
        /// 비동기 Screen Push.
        /// </summary>
        public async UniTask PushAsync(ScreenWidget.Context context)
        {
            if (_isTransitioning || context == null) return;

            _isTransitioning = true;

            try
            {
                // 중복 Screen 검사 및 제거
                await RemoveDuplicateScreen(context.ScreenType);

                // 이전 Screen 참조 (Transition용)
                var previousScreen = CurrentScreen;

                // 새 Screen 로드 및 진입 (Show 없이)
                await context.Load();
                await context.Enter();

                // Transition 설정 및 실행
                var transition = context.GetTransition();
                transition.OutScreen = previousScreen;
                transition.InScreen = context.View;
                await transition.CrossFade();

                // 스택에 추가
                _navigationStack.Add(context);
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        /// <summary>
        /// Popup Push.
        /// </summary>
        public void Push(PopupWidget.Context context)
        {
            PushAsync(context).Forget();
        }

        /// <summary>
        /// 비동기 Popup Push.
        /// </summary>
        public async UniTask PushAsync(PopupWidget.Context context)
        {
            if (_isTransitioning || context == null) return;

            // Screen이 없으면 Popup을 열 수 없음
            if (CurrentScreen == null) return;

            _isTransitioning = true;

            try
            {
                // 현재 최상위 항목 Pause (상호작용 차단)
                CurrentContext?.Pause();

                // 새 Popup 로드 및 진입 (Show 없이)
                await context.Load();
                await context.Enter();

                // Transition 설정 및 실행
                var transition = context.GetTransition();
                transition.Target = context.View;
                await transition.Show();

                // 스택에 추가
                _navigationStack.Add(context);
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        /// <summary>
        /// 기존 호환성을 위한 PushPopup 메서드.
        /// </summary>
        public void PushPopup(PopupWidget.Context context) => Push(context);

        /// <summary>
        /// 기존 호환성을 위한 PushPopupAsync 메서드.
        /// </summary>
        public UniTask PushPopupAsync(PopupWidget.Context context) => PushAsync(context);

        #endregion

        #region Pop Operations

        /// <summary>
        /// 최상위 항목 Pop.
        /// </summary>
        public void Pop()
        {
            PopAsync().Forget();
        }

        /// <summary>
        /// 비동기 Pop.
        /// </summary>
        public async UniTask PopAsync()
        {
            // Screen이 1개뿐이면 Pop 불가 (루트 Screen 보호)
            if (_isTransitioning || ScreenCount <= 1 && !HasPopupOnTop) return;

            _isTransitioning = true;

            try
            {
                var currentContext = CurrentContext;
                if (currentContext != null)
                {
                    // Popup: Hide Transition 실행
                    if (currentContext is PopupWidget.Context popupContext)
                    {
                        var transition = popupContext.GetTransition();
                        transition.Target = popupContext.View;
                        await transition.Hide();
                    }
                    // Screen: CrossFade Transition 실행
                    else if (currentContext is ScreenWidget.Context screenContext)
                    {
                        // 이전 Screen 찾기
                        ScreenWidget previousScreen = null;
                        for (int i = _navigationStack.Count - 2; i >= 0; i--)
                        {
                            if (_navigationStack[i] is ScreenWidget.Context prevCtx)
                            {
                                previousScreen = prevCtx.View;
                                break;
                            }
                        }

                        var transition = screenContext.GetTransition();
                        transition.OutScreen = screenContext.View;
                        transition.InScreen = previousScreen;
                        await transition.CrossFade();
                    }

                    await currentContext.Exit();
                    _navigationStack.RemoveAt(_navigationStack.Count - 1);
                }

                // Popup인 경우 이전 항목 Resume (Pause 해제)
                if (currentContext?.ContextType == NavigationContextType.Popup)
                {
                    CurrentContext?.Resume();
                }
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        /// <summary>
        /// 특정 Screen까지 Pop (해당 Screen 위의 모든 항목 제거).
        /// </summary>
        public void PopTo<TScreen>() where TScreen : ScreenWidget
        {
            PopToAsync<TScreen>().Forget();
        }

        /// <summary>
        /// 비동기로 특정 Screen까지 Pop.
        /// </summary>
        public async UniTask PopToAsync<TScreen>() where TScreen : ScreenWidget
        {
            var targetType = typeof(TScreen);

            while (_navigationStack.Count > 1)
            {
                var currentContext = CurrentContext;

                // 목표 Screen에 도달하면 중단
                if (currentContext.ContextType == NavigationContextType.Screen &&
                    currentContext.WidgetType == targetType)
                    break;

                await PopAsync();
            }
        }

        /// <summary>
        /// 특정 Popup 닫기.
        /// </summary>
        public void ClosePopup(PopupWidget popup)
        {
            ClosePopupAsync(popup).Forget();
        }

        /// <summary>
        /// 비동기로 특정 Popup 닫기.
        /// </summary>
        public async UniTask ClosePopupAsync(PopupWidget popup)
        {
            if (_isTransitioning || popup == null) return;

            _isTransitioning = true;

            try
            {
                // 해당 Popup 찾기
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i] is PopupWidget.Context popupContext &&
                        popupContext.View == popup)
                    {
                        // Transition으로 Hide
                        var transition = popupContext.GetTransition();
                        transition.Target = popupContext.View;
                        await transition.Hide();

                        // 최상위 항목인 경우
                        if (i == _navigationStack.Count - 1)
                        {
                            await popupContext.Exit();
                            _navigationStack.RemoveAt(i);
                            CurrentContext?.Resume();
                        }
                        else
                        {
                            // 중간에 있는 Popup 제거 (특수 케이스)
                            await popupContext.Exit();
                            _navigationStack.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        /// <summary>
        /// 모든 Popup 닫기 (Screen은 유지).
        /// </summary>
        public void CloseAllPopups()
        {
            CloseAllPopupsAsync().Forget();
        }

        /// <summary>
        /// 비동기로 모든 Popup 닫기.
        /// </summary>
        public async UniTask CloseAllPopupsAsync()
        {
            if (_isTransitioning) return;

            _isTransitioning = true;

            try
            {
                // 뒤에서부터 Popup만 제거
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i] is PopupWidget.Context popupContext)
                    {
                        // Transition으로 Hide
                        var transition = popupContext.GetTransition();
                        transition.Target = popupContext.View;
                        await transition.Hide();

                        await popupContext.Exit();
                        _navigationStack.RemoveAt(i);
                    }
                }

                // 최상위 Screen Resume
                CurrentContext?.Resume();
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        #endregion

        #region Back Navigation

        /// <summary>
        /// 뒤로가기. 최상위 항목의 HandleEscape() 확인 후 Pop.
        /// </summary>
        public bool Back()
        {
            var currentContext = CurrentContext;
            if (currentContext == null) return false;

            // HandleEscape()가 false면 닫기 거부
            if (!currentContext.HandleEscape()) return false;

            // Screen이 1개뿐이고 Popup이 없으면 더 이상 뒤로갈 곳 없음
            if (ScreenCount <= 1 && !HasPopupOnTop) return false;

            Pop();
            return true;
        }

        #endregion

        #region Private Helpers

        private async UniTask RemoveDuplicateScreen(Type screenType)
        {
            for (int i = _navigationStack.Count - 1; i >= 0; i--)
            {
                var context = _navigationStack[i];

                if (context.ContextType == NavigationContextType.Screen &&
                    context.WidgetType == screenType)
                {
                    // 해당 Screen 위의 모든 항목(Popup 포함) 제거
                    for (int j = _navigationStack.Count - 1; j >= i; j--)
                    {
                        await _navigationStack[j].Exit();
                        _navigationStack.RemoveAt(j);
                    }
                    break;
                }
            }
        }

        #endregion

        #region Debug

        /// <summary>
        /// 현재 스택 상태 문자열.
        /// </summary>
        public string GetStackDebugString()
        {
            if (_navigationStack.Count == 0) return "[Empty]";

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < _navigationStack.Count; i++)
            {
                var ctx = _navigationStack[i];
                var marker = ctx.ContextType == NavigationContextType.Screen ? "S" : "P";
                sb.Append($"[{marker}]{ctx.WidgetType.Name}");
                if (i < _navigationStack.Count - 1)
                    sb.Append(" → ");
            }
            return sb.ToString();
        }

        #endregion
    }
}
