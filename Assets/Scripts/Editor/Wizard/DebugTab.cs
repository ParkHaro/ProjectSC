using UnityEngine;
using UnityEditor;
using Sc.Common.UI;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Debug 탭: 런타임 Navigation 스택 디버깅.
    /// PlayMode에서 NavigationManager 상태를 실시간으로 확인.
    /// </summary>
    public class DebugTab
    {
        private bool _showNavigationSection = true;
        private Vector2 _stackScrollPosition;

        // Navigation Debug 스타일
        private bool _stylesInitialized;
        private GUIStyle _screenItemStyle;
        private GUIStyle _popupItemStyle;
        private GUIStyle _currentItemStyle;

        public void Draw(EditorWindow window)
        {
            if (!Application.isPlaying)
            {
                DrawEditorModeMessage();
                return;
            }

            InitStyles();

            // Navigation Section
            DrawNavigationSection(window);
        }

        private void DrawEditorModeMessage()
        {
            EditorGUILayout.Space(20);

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.HelpBox(
                "디버그 도구는 PlayMode에서만 사용 가능합니다.\n\n" +
                "▶ Play 버튼을 눌러 게임을 실행하세요.",
                MessageType.Info);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("▶ Enter Play Mode", GUILayout.Height(35)))
            {
                EditorApplication.isPlaying = true;
            }

            EditorGUILayout.EndVertical();
        }

        private void InitStyles()
        {
            if (_stylesInitialized) return;

            _screenItemStyle = new GUIStyle(EditorStyles.helpBox);
            _screenItemStyle.normal.background = MakeTexture(new Color(0.15f, 0.25f, 0.4f, 0.5f));

            _popupItemStyle = new GUIStyle(EditorStyles.helpBox);
            _popupItemStyle.normal.background = MakeTexture(new Color(0.4f, 0.25f, 0.15f, 0.5f));

            _currentItemStyle = new GUIStyle(EditorStyles.helpBox);
            _currentItemStyle.normal.background = MakeTexture(new Color(0.2f, 0.5f, 0.3f, 0.5f));

            _stylesInitialized = true;
        }

        private Texture2D MakeTexture(Color color)
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        private void DrawNavigationSection(EditorWindow window)
        {
            _showNavigationSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showNavigationSection, "Navigation Stack");

            if (_showNavigationSection)
            {
                EditorGUILayout.BeginVertical("box");

                var navManager = NavigationManager.Instance;

                if (navManager == null)
                {
                    EditorGUILayout.HelpBox("NavigationManager 인스턴스가 없습니다.", MessageType.Warning);
                }
                else
                {
                    // Stack 표시
                    DrawNavigationStack(navManager);

                    EditorGUILayout.Space(5);

                    // 컨트롤 버튼
                    DrawNavigationControls(navManager);

                    EditorGUILayout.Space(5);

                    // 요약 정보
                    DrawNavigationSummary(navManager);
                }

                EditorGUILayout.EndVertical();

                // 자동 새로고침
                if (navManager != null)
                {
                    window.Repaint();
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawNavigationStack(NavigationManager navManager)
        {
            EditorGUILayout.LabelField("Stack (Top → Bottom)", EditorStyles.boldLabel);

            _stackScrollPosition = EditorGUILayout.BeginScrollView(_stackScrollPosition, GUILayout.MaxHeight(200));

            var stack = navManager.NavigationStack;
            var count = stack.Count;

            if (count == 0)
            {
                EditorGUILayout.LabelField("(empty)", EditorStyles.centeredGreyMiniLabel);
            }
            else
            {
                // 역순으로 표시 (Top이 위에)
                for (int i = count - 1; i >= 0; i--)
                {
                    var context = stack[i];
                    var isTop = (i == count - 1);
                    var isScreen = context.ContextType == NavigationContextType.Screen;

                    var style = isTop ? _currentItemStyle : (isScreen ? _screenItemStyle : _popupItemStyle);
                    var typeTag = isScreen ? "[S]" : "[P]";
                    var name = context.WidgetType?.Name ?? "Unknown";
                    var visibleIcon = context.View?.IsVisible == true ? "O" : "-";

                    EditorGUILayout.BeginHorizontal(style);

                    EditorGUILayout.LabelField($"{visibleIcon} {typeTag} {name}", GUILayout.ExpandWidth(true));

                    if (GUILayout.Button("Select", GUILayout.Width(50)))
                    {
                        if (context.View != null)
                        {
                            Selection.activeGameObject = context.View.gameObject;
                        }
                    }

                    // Popup만 닫기 버튼 표시 (Top일 때만)
                    if (isTop && !isScreen)
                    {
                        if (GUILayout.Button("X", GUILayout.Width(25)))
                        {
                            navManager.Back();
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawNavigationControls(NavigationManager navManager)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("<- Back", GUILayout.Height(25)))
            {
                navManager.Back();
            }

            if (GUILayout.Button("Close All Popups", GUILayout.Height(25)))
            {
                navManager.CloseAllPopups();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawNavigationSummary(NavigationManager navManager)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);

            var stack = navManager.NavigationStack;
            var screenCount = 0;
            var popupCount = 0;
            string currentScreen = "None";

            foreach (var ctx in stack)
            {
                if (ctx.ContextType == NavigationContextType.Screen)
                {
                    screenCount++;
                    currentScreen = ctx.WidgetType?.Name ?? "Unknown";
                }
                else
                {
                    popupCount++;
                }
            }

            EditorGUILayout.LabelField($"Total: {stack.Count}  |  Screens: {screenCount}  |  Popups: {popupCount}");
            EditorGUILayout.LabelField($"Current Screen: {currentScreen}");

            EditorGUILayout.EndVertical();
        }
    }
}
