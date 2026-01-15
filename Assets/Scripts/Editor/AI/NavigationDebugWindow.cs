using UnityEngine;
using UnityEditor;
using Sc.Common.UI;

namespace Sc.Editor.AI
{
    /// <summary>
    /// Navigation 상태를 시각적으로 표시하는 에디터 윈도우.
    /// NavigationManager 기반으로 동작.
    /// </summary>
    public class NavigationDebugWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private bool _autoRepaint = true;
        private float _lastRepaintTime;

        // 스타일
        private GUIStyle _headerStyle;
        private GUIStyle _stackItemStyle;
        private GUIStyle _screenItemStyle;
        private GUIStyle _popupItemStyle;
        private GUIStyle _currentItemStyle;
        private GUIStyle _emptyStyle;
        private GUIStyle _statusStyle;
        private bool _stylesInitialized;

        [MenuItem("SC Tools/Debug/Navigation Debug Window", priority = 100)]
        public static void ShowWindow()
        {
            var window = GetWindow<NavigationDebugWindow>("Navigation Debug");
            window.minSize = new Vector2(350, 500);
            window.Show();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            _stylesInitialized = false;
            Repaint();
        }

        private void InitStyles()
        {
            if (_stylesInitialized) return;

            _headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                margin = new RectOffset(5, 5, 10, 5)
            };

            _stackItemStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(10, 10, 8, 8),
                margin = new RectOffset(5, 5, 2, 2)
            };

            _screenItemStyle = new GUIStyle(_stackItemStyle);
            _screenItemStyle.normal.background = MakeTexture(2, 2, new Color(0.15f, 0.25f, 0.4f, 0.3f));

            _popupItemStyle = new GUIStyle(_stackItemStyle);
            _popupItemStyle.normal.background = MakeTexture(2, 2, new Color(0.4f, 0.25f, 0.15f, 0.3f));

            _currentItemStyle = new GUIStyle(_stackItemStyle);
            _currentItemStyle.normal.background = MakeTexture(2, 2, new Color(0.2f, 0.5f, 0.3f, 0.4f));

            _emptyStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                fontSize = 11,
                padding = new RectOffset(10, 10, 20, 20)
            };

            _statusStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(10, 10, 10, 10),
                margin = new RectOffset(5, 5, 5, 5)
            };

            _stylesInitialized = true;
        }

        private void OnGUI()
        {
            InitStyles();

            // 툴바
            DrawToolbar();

            // Play 모드 체크
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Play 모드에서만 Navigation 상태를 확인할 수 있습니다.", MessageType.Info);
                return;
            }

            // NavigationManager 체크
            var navManager = NavigationManager.Instance;
            if (navManager == null)
            {
                EditorGUILayout.HelpBox(
                    "NavigationManager가 씬에 없습니다.\n" +
                    "SC Tools > MVP > Setup MVP Scene을 실행하세요.",
                    MessageType.Warning);

                if (GUILayout.Button("Find NavigationManager in Scene"))
                {
                    var found = Object.FindObjectOfType<NavigationManager>();
                    if (found != null)
                    {
                        Selection.activeGameObject = found.gameObject;
                        EditorGUIUtility.PingObject(found);
                    }
                }
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // 스택 표시
            DrawNavigationStack(navManager);

            EditorGUILayout.Space(10);

            // 컨트롤
            DrawControls(navManager);

            EditorGUILayout.Space(10);

            // 상태 요약
            DrawSummary(navManager);

            EditorGUILayout.EndScrollView();

            // Auto repaint
            if (_autoRepaint && Application.isPlaying)
            {
                if (Time.realtimeSinceStartup - _lastRepaintTime > 0.1f)
                {
                    _lastRepaintTime = Time.realtimeSinceStartup;
                    Repaint();
                }
            }
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            _autoRepaint = GUILayout.Toggle(_autoRepaint, "Auto Refresh", EditorStyles.toolbarButton);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton, GUILayout.Width(60)))
            {
                Repaint();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawNavigationStack(NavigationManager navManager)
        {
            EditorGUILayout.LabelField("Navigation Stack", _headerStyle);

            // 범례
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUI.backgroundColor = new Color(0.15f, 0.25f, 0.4f, 0.8f);
            GUILayout.Box(" [S] Screen ", GUILayout.Width(85));
            GUI.backgroundColor = new Color(0.4f, 0.25f, 0.15f, 0.8f);
            GUILayout.Box(" [P] Popup ", GUILayout.Width(80));
            GUI.backgroundColor = new Color(0.2f, 0.5f, 0.3f, 0.8f);
            GUILayout.Box(" Current ", GUILayout.Width(70));
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            var stack = navManager.NavigationStack;
            if (stack.Count == 0)
            {
                EditorGUILayout.LabelField("(Empty Stack)", _emptyStyle);
                return;
            }

            // 역순으로 표시 (최상위가 위로)
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                var context = stack[i];
                if (context == null) continue;

                bool isCurrent = i == stack.Count - 1;
                bool isScreen = context.ContextType == NavigationContextType.Screen;

                // 스타일 선택
                GUIStyle style;
                if (isCurrent)
                    style = _currentItemStyle;
                else if (isScreen)
                    style = _screenItemStyle;
                else
                    style = _popupItemStyle;

                EditorGUILayout.BeginHorizontal(style);

                // 인덱스 + 타입
                var typeMarker = isScreen ? "[S]" : "[P]";
                EditorGUILayout.LabelField($"{i} {typeMarker}", GUILayout.Width(45));

                // 이름
                string displayName = context.WidgetType?.Name ?? "Unknown";
                if (isCurrent) displayName += " ◀ TOP";
                EditorGUILayout.LabelField(displayName);

                // View 가져오기
                Widget view = null;
                if (context is ScreenWidget.Context screenCtx)
                    view = screenCtx.View;
                else if (context is PopupWidget.Context popupCtx)
                    view = popupCtx.View;

                // Visible 상태
                if (view != null)
                {
                    var visibleIcon = view.IsVisible ? "d_scenevis_visible_hover" : "d_scenevis_hidden_hover";
                    GUILayout.Label(EditorGUIUtility.IconContent(visibleIcon), GUILayout.Width(20));

                    // Select 버튼
                    if (GUILayout.Button("Select", GUILayout.Width(50)))
                    {
                        Selection.activeGameObject = view.gameObject;
                        EditorGUIUtility.PingObject(view);
                    }
                }

                // Close 버튼 (Popup만, 최상위만)
                if (!isScreen && isCurrent)
                {
                    if (GUILayout.Button("X", GUILayout.Width(22)))
                    {
                        navManager.Pop();
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawControls(NavigationManager navManager)
        {
            EditorGUILayout.LabelField("Controls", _headerStyle);

            EditorGUILayout.BeginHorizontal();

            // Back 버튼
            GUI.enabled = navManager.StackCount > 1 || navManager.HasPopupOnTop;
            if (GUILayout.Button("Back (Pop)", GUILayout.Height(30)))
            {
                navManager.Back();
            }
            GUI.enabled = true;

            // Close All Popups
            GUI.enabled = navManager.PopupCount > 0;
            if (GUILayout.Button("Close All Popups", GUILayout.Height(30)))
            {
                navManager.CloseAllPopups();
            }
            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();

            // Transitioning 경고
            if (navManager.IsTransitioning)
            {
                EditorGUILayout.HelpBox("전환 중...", MessageType.Info);
            }
        }

        private void DrawSummary(NavigationManager navManager)
        {
            EditorGUILayout.LabelField("Summary", _headerStyle);

            EditorGUILayout.BeginVertical(_statusStyle);

            // 카운트
            EditorGUILayout.LabelField($"Total Stack: {navManager.StackCount}");
            EditorGUILayout.LabelField($"Screens: {navManager.ScreenCount}");
            EditorGUILayout.LabelField($"Popups: {navManager.PopupCount}");

            EditorGUILayout.Space(5);

            // 현재 Screen
            var currentScreen = navManager.CurrentScreen;
            if (currentScreen != null)
            {
                EditorGUILayout.LabelField($"Current Screen: {currentScreen.GetType().Name}");
            }
            else
            {
                EditorGUILayout.LabelField("Current Screen: None");
            }

            // 최상위 Context
            var currentContext = navManager.CurrentContext;
            if (currentContext != null)
            {
                var typeStr = currentContext.ContextType == NavigationContextType.Screen ? "Screen" : "Popup";
                EditorGUILayout.LabelField($"Top Context: [{typeStr}] {currentContext.WidgetType?.Name}");
            }

            EditorGUILayout.Space(5);

            // 스택 문자열
            EditorGUILayout.LabelField("Stack Path:", EditorStyles.miniLabel);
            EditorGUILayout.LabelField(navManager.GetStackDebugString(), EditorStyles.wordWrappedMiniLabel);

            EditorGUILayout.EndVertical();
        }

        private static Texture2D MakeTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            var texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
    }
}
