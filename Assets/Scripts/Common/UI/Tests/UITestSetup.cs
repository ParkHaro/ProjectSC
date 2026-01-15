using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Tests
{
    /// <summary>
    /// 통합 스택 항목 래퍼.
    /// </summary>
    public class TestNavigationEntry
    {
        public Widget Widget { get; }
        public bool IsScreen { get; }
        public bool IsPopup => !IsScreen;

        public TestScreen AsScreen => Widget as TestScreen;
        public TestPopup AsPopup => Widget as TestPopup;

        public TestNavigationEntry(Widget widget, bool isScreen)
        {
            Widget = widget;
            IsScreen = isScreen;
        }
    }

    /// <summary>
    /// UI 시스템 테스트용 셋업.
    /// Screen/Popup 통합 스택 기반 관리.
    /// </summary>
    public class UITestSetup : MonoBehaviour
    {
        public static UITestSetup Instance { get; private set; }

        [Header("Test Prefabs")]
        [SerializeField] private TestScreen _screenPrefab;
        [SerializeField] private TestPopup _popupPrefab;

        [Header("UI Containers")]
        [SerializeField] private Transform _screenContainer;
        [SerializeField] private Transform _popupContainer;

        [Header("Test Controls")]
        [SerializeField] private Button _createScreenButton;
        [SerializeField] private Button _showScreenButton;
        [SerializeField] private Button _hideScreenButton;
        [SerializeField] private Button _createPopupButton;
        [SerializeField] private Button _showPopupButton;
        [SerializeField] private Button _hidePopupButton;
        [SerializeField] private Button _testFullFlowButton;

        // 통합 스택
        private readonly List<TestNavigationEntry> _navigationStack = new();
        private int _screenCounter;
        private int _popupCounter;

        #region Properties

        /// <summary>
        /// 전체 통합 스택.
        /// </summary>
        public IReadOnlyList<TestNavigationEntry> NavigationStack => _navigationStack;

        /// <summary>
        /// Screen만 필터링한 리스트 (호환성용).
        /// </summary>
        public IReadOnlyList<TestScreen> ScreenStack
        {
            get
            {
                var list = new List<TestScreen>();
                foreach (var entry in _navigationStack)
                {
                    if (entry.IsScreen)
                        list.Add(entry.AsScreen);
                }
                return list;
            }
        }

        /// <summary>
        /// Popup만 필터링한 리스트 (호환성용).
        /// </summary>
        public IReadOnlyList<TestPopup> PopupStack
        {
            get
            {
                var list = new List<TestPopup>();
                foreach (var entry in _navigationStack)
                {
                    if (entry.IsPopup)
                        list.Add(entry.AsPopup);
                }
                return list;
            }
        }

        /// <summary>
        /// 현재 최상위 Screen.
        /// </summary>
        public TestScreen CurrentScreen
        {
            get
            {
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i].IsScreen)
                        return _navigationStack[i].AsScreen;
                }
                return null;
            }
        }

        /// <summary>
        /// 현재 최상위 Popup.
        /// </summary>
        public TestPopup CurrentPopup
        {
            get
            {
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i].IsPopup)
                        return _navigationStack[i].AsPopup;
                }
                return null;
            }
        }

        /// <summary>
        /// 최상위 항목 (Screen 또는 Popup).
        /// </summary>
        public TestNavigationEntry CurrentEntry =>
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
                foreach (var entry in _navigationStack)
                {
                    if (entry.IsScreen) count++;
                }
                return count;
            }
        }

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
        }

        private void Start()
        {
            SetupButtons();
            EnsureNavigationManager();
            Debug.Log("[UITestSetup] Ready. Use buttons to test UI system.");
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleEscape();
            }
        }

        #endregion

        #region Setup

        private void SetupButtons()
        {
            _createScreenButton?.onClick.AddListener(() => PushScreen());
            _showScreenButton?.onClick.AddListener(ShowCurrentScreen);
            _hideScreenButton?.onClick.AddListener(HideCurrentScreen);
            _createPopupButton?.onClick.AddListener(() => PushPopup());
            _showPopupButton?.onClick.AddListener(ShowCurrentPopup);
            _hidePopupButton?.onClick.AddListener(HideCurrentPopup);
            _testFullFlowButton?.onClick.AddListener(TestFullFlow);
        }

        private void EnsureNavigationManager()
        {
            if (NavigationManager.Instance == null)
            {
                var go = new GameObject("NavigationManager");
                go.AddComponent<NavigationManager>();
                Debug.Log("[UITestSetup] NavigationManager created");
            }
        }

        #endregion

        #region Public API - Screen

        /// <summary>
        /// 새 Screen 생성 및 스택에 추가.
        /// </summary>
        public TestScreen PushScreen(TestScreenState state = null)
        {
            if (_screenPrefab == null)
            {
                Debug.LogWarning("[UITestSetup] Screen prefab not assigned!");
                return null;
            }

            // 새 Screen 생성
            _screenCounter++;
            var parent = _screenContainer != null ? _screenContainer : transform;
            var screen = Instantiate(_screenPrefab, parent);
            screen.gameObject.name = $"TestScreen_{_screenCounter}";

            // 초기화
            screen.Initialize();
            var screenState = state ?? new TestScreenState
            {
                Title = $"Screen #{_screenCounter}",
                Counter = _screenCounter
            };
            screen.Bind(screenState);

            // 통합 스택에 추가
            _navigationStack.Add(new TestNavigationEntry(screen, isScreen: true));

            // 가시성 갱신
            RefreshVisibility();

            Debug.Log($"[UITestSetup] Screen pushed: {screen.gameObject.name} | Stack: {GetStackDebugString()}");

            return screen;
        }

        /// <summary>
        /// 현재 Screen 제거 및 이전 항목 표시.
        /// </summary>
        public void PopScreen()
        {
            // Screen이 1개뿐이면 Pop 불가
            if (ScreenCount <= 1)
            {
                Debug.LogWarning("[UITestSetup] Cannot pop last screen");
                return;
            }

            // 최상위가 Screen이 아니면 Pop 불가 (Popup이 있으면 먼저 닫아야 함)
            var current = CurrentEntry;
            if (current == null || !current.IsScreen)
            {
                Debug.LogWarning("[UITestSetup] Top item is not a screen. Pop popup first.");
                return;
            }

            // Screen 제거
            _navigationStack.RemoveAt(_navigationStack.Count - 1);
            Destroy(current.Widget.gameObject);

            // 가시성 갱신
            RefreshVisibility();

            Debug.Log($"[UITestSetup] Screen popped | Stack: {GetStackDebugString()}");
        }

        private void ShowCurrentScreen()
        {
            if (CurrentScreen == null)
            {
                Debug.LogWarning("[UITestSetup] No screen to show. Create one first.");
                return;
            }
            CurrentScreen.Show();
        }

        private void HideCurrentScreen()
        {
            if (CurrentScreen == null)
            {
                Debug.LogWarning("[UITestSetup] No screen to hide.");
                return;
            }
            CurrentScreen.Hide();
        }

        #endregion

        #region Public API - Popup

        /// <summary>
        /// 새 Popup 생성 및 스택에 추가.
        /// </summary>
        public TestPopup PushPopup(TestPopupState state = null)
        {
            if (_popupPrefab == null)
            {
                Debug.LogWarning("[UITestSetup] Popup prefab not assigned!");
                return null;
            }

            // Screen이 없으면 Popup 열기 불가
            if (CurrentScreen == null)
            {
                Debug.LogWarning("[UITestSetup] Cannot open popup without a screen!");
                return null;
            }

            _popupCounter++;
            var parent = _popupContainer != null ? _popupContainer : transform;
            var popup = Instantiate(_popupPrefab, parent);
            popup.gameObject.name = $"TestPopup_{_popupCounter}";

            popup.Initialize();
            var popupState = state ?? new TestPopupState
            {
                Message = $"Popup #{_popupCounter}"
            };
            popup.Bind(popupState);

            // 통합 스택에 추가
            _navigationStack.Add(new TestNavigationEntry(popup, isScreen: false));

            // 가시성 갱신
            RefreshVisibility();

            Debug.Log($"[UITestSetup] Popup pushed: {popup.gameObject.name} | Stack: {GetStackDebugString()}");

            return popup;
        }

        /// <summary>
        /// 특정 Popup 제거.
        /// </summary>
        public void PopPopup(TestPopup popup = null)
        {
            if (popup == null)
            {
                // 최상위 항목이 Popup이면 제거
                var current = CurrentEntry;
                if (current == null || !current.IsPopup)
                {
                    Debug.LogWarning("[UITestSetup] No popup to pop");
                    return;
                }

                _navigationStack.RemoveAt(_navigationStack.Count - 1);
                Destroy(current.Widget.gameObject);

                // 가시성 갱신
                RefreshVisibility();

                Debug.Log($"[UITestSetup] Popup popped | Stack: {GetStackDebugString()}");
            }
            else
            {
                // 특정 Popup 찾아서 제거
                for (int i = _navigationStack.Count - 1; i >= 0; i--)
                {
                    if (_navigationStack[i].IsPopup && _navigationStack[i].AsPopup == popup)
                    {
                        _navigationStack.RemoveAt(i);
                        Destroy(popup.gameObject);

                        // 가시성 갱신
                        RefreshVisibility();

                        Debug.Log($"[UITestSetup] Popup removed | Stack: {GetStackDebugString()}");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 모든 Popup 제거.
        /// </summary>
        public void ClearPopups()
        {
            for (int i = _navigationStack.Count - 1; i >= 0; i--)
            {
                if (_navigationStack[i].IsPopup)
                {
                    var popup = _navigationStack[i].AsPopup;
                    _navigationStack.RemoveAt(i);
                    Destroy(popup.gameObject);
                }
            }

            // 가시성 갱신
            RefreshVisibility();

            Debug.Log($"[UITestSetup] All popups cleared | Stack: {GetStackDebugString()}");
        }

        private void ShowCurrentPopup()
        {
            if (CurrentPopup == null)
            {
                Debug.LogWarning("[UITestSetup] No popup to show. Create one first.");
                return;
            }
            CurrentPopup.Show();
        }

        private void HideCurrentPopup()
        {
            if (CurrentPopup == null)
            {
                Debug.LogWarning("[UITestSetup] No popup to hide.");
                return;
            }
            CurrentPopup.Hide();
        }

        #endregion

        #region Navigation

        /// <summary>
        /// 통합 Pop - 최상위 항목 제거.
        /// </summary>
        public void Pop()
        {
            var current = CurrentEntry;
            if (current == null) return;

            if (current.IsPopup)
            {
                PopPopup();
            }
            else if (ScreenCount > 1)
            {
                PopScreen();
            }
            else
            {
                Debug.LogWarning("[UITestSetup] Cannot pop last screen");
            }
        }

        private void HandleEscape()
        {
            Debug.Log("[UITestSetup] ESC pressed");

            var current = CurrentEntry;
            if (current == null) return;

            // Popup이면 OnEscape() 확인 후 닫기
            if (current.IsPopup)
            {
                if (current.AsPopup.OnEscape())
                {
                    PopPopup();
                }
                return;
            }

            // Screen이면 Pop (마지막 Screen 보호)
            if (ScreenCount > 1)
            {
                PopScreen();
            }
        }

        #endregion

        #region Visibility

        /// <summary>
        /// 가시성 갱신. 마지막 Screen 인덱스 기준으로 Show/Hide 결정.
        /// 가시성 경계 = 마지막 Screen 인덱스
        /// index >= boundary → Show
        /// index < boundary → Hide
        /// </summary>
        private void RefreshVisibility()
        {
            if (_navigationStack.Count == 0) return;

            // 마지막 Screen 인덱스 찾기 (뒤에서부터 탐색)
            int lastScreenIndex = -1;
            for (int i = _navigationStack.Count - 1; i >= 0; i--)
            {
                if (_navigationStack[i].IsScreen)
                {
                    lastScreenIndex = i;
                    break;
                }
            }

            // Screen이 없으면 모두 숨김
            if (lastScreenIndex < 0)
            {
                foreach (var entry in _navigationStack)
                {
                    entry.Widget.Hide();
                }
                return;
            }

            // 전체 스택 순회하며 가시성 설정
            for (int i = 0; i < _navigationStack.Count; i++)
            {
                if (i >= lastScreenIndex)
                {
                    _navigationStack[i].Widget.Show();
                }
                else
                {
                    _navigationStack[i].Widget.Hide();
                }
            }
        }

        #endregion

        #region Debug

        /// <summary>
        /// 스택 상태 문자열.
        /// </summary>
        public string GetStackDebugString()
        {
            if (_navigationStack.Count == 0) return "[Empty]";

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < _navigationStack.Count; i++)
            {
                var entry = _navigationStack[i];
                var marker = entry.IsScreen ? "S" : "P";
                sb.Append($"[{marker}]{entry.Widget.gameObject.name}");
                if (i < _navigationStack.Count - 1)
                    sb.Append(" → ");
            }
            return sb.ToString();
        }

        #endregion

        #region Test Flow

        private void TestFullFlow()
        {
            Debug.Log("=== Starting Full Flow Test ===");

            // Screen 생성
            PushScreen(new TestScreenState { Title = "First Screen", Counter = 1 });

            // 1초 후 Popup 생성
            Invoke(nameof(DelayedPopupTest), 1f);
        }

        private void DelayedPopupTest()
        {
            PushPopup(new TestPopupState { Message = "Hello from Full Flow Test!" });

            // 1초 후 Screen 추가
            Invoke(nameof(DelayedScreenTest), 1f);
        }

        private void DelayedScreenTest()
        {
            // Popup 위에 Screen 추가 테스트
            PushScreen(new TestScreenState { Title = "Second Screen", Counter = 2 });

            // 1초 후 Pop 테스트
            Invoke(nameof(DelayedPopTest), 1f);
        }

        private void DelayedPopTest()
        {
            Debug.Log("[UITestSetup] Auto-popping...");
            Pop(); // Screen2 Pop → Popup1 표시
            Invoke(nameof(DelayedPopTest2), 1f);
        }

        private void DelayedPopTest2()
        {
            Pop(); // Popup1 Pop → Screen1 표시
            Debug.Log("=== Full Flow Test Complete ===");
        }

        #endregion
    }
}
