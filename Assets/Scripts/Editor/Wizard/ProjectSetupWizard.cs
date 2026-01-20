using UnityEngine;
using UnityEditor;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// 통합 프로젝트 설정 Wizard.
    /// 기존 분산된 에디터 도구를 하나의 EditorWindow로 통합.
    /// </summary>
    public class ProjectSetupWizard : EditorWindow
    {
        private enum WizardTab
        {
            Setup,
            Debug,
            Data,
            Settings
        }

        private WizardTab _currentTab;
        private Vector2 _scrollPosition;
        
        // Tab 인스턴스
        private SetupTab _setupTab;
        private DebugTab _debugTab;
        private DataTab _dataTab;
        private SettingsTab _settingsTab;
        
        // EditorPrefs 키
        private const string PREF_SELECTED_TAB = "ProjectSetupWizard.SelectedTab";

        [MenuItem("SC Tools/Project Setup Wizard", priority = 0)]
        public static void ShowWindow()
        {
            var window = GetWindow<ProjectSetupWizard>(false, "Project Setup", true);
            window.minSize = new Vector2(450, 600);
            window.Show();
        }

        private void OnEnable()
        {
            // 이전 탭 상태 복원
            _currentTab = (WizardTab)EditorPrefs.GetInt(PREF_SELECTED_TAB, 0);
            
            // Tab 인스턴스 생성
            _setupTab = new SetupTab();
            _debugTab = new DebugTab();
            _dataTab = new DataTab();
            _settingsTab = new SettingsTab();
            
            // PlayMode 변경 감지
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private void OnDisable()
        {
            // 탭 상태 저장
            EditorPrefs.SetInt(PREF_SELECTED_TAB, (int)_currentTab);
            
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            // Debug 탭에서 PlayMode 변경 시 새로고침
            if (_currentTab == WizardTab.Debug)
            {
                Repaint();
            }
        }

        private void OnGUI()
        {
            DrawToolbar();
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            DrawTabContent();
            EditorGUILayout.EndScrollView();
            
            DrawStatusBar();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            var tabs = new[] { "Setup", "Debug", "Data", "Settings" };
            for (int i = 0; i < tabs.Length; i++)
            {
                var isSelected = (int)_currentTab == i;
                var style = isSelected ? "ToolbarButtonFlat" : EditorStyles.toolbarButton;
                
                if (GUILayout.Toggle(isSelected, tabs[i], style, GUILayout.Width(80)))
                {
                    if (!isSelected)
                    {
                        _currentTab = (WizardTab)i;
                        EditorPrefs.SetInt(PREF_SELECTED_TAB, i);
                    }
                }
            }
            
            GUILayout.FlexibleSpace();
            
            // 새로고침 버튼
            if (GUILayout.Button("↻", EditorStyles.toolbarButton, GUILayout.Width(25)))
            {
                Repaint();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTabContent()
        {
            EditorGUILayout.Space(10);
            
            switch (_currentTab)
            {
                case WizardTab.Setup:
                    _setupTab?.Draw();
                    break;
                case WizardTab.Debug:
                    _debugTab?.Draw(this);
                    break;
                case WizardTab.Data:
                    _dataTab?.Draw();
                    break;
                case WizardTab.Settings:
                    _settingsTab?.Draw();
                    break;
            }
        }

        private void DrawStatusBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            // PlayMode 상태 표시
            var playModeStatus = Application.isPlaying ? "▶ Playing" : "⏹ Editor";
            var playModeColor = Application.isPlaying ? Color.green : Color.gray;
            
            var prevColor = GUI.color;
            GUI.color = playModeColor;
            GUILayout.Label(playModeStatus, EditorStyles.miniLabel, GUILayout.Width(80));
            GUI.color = prevColor;
            
            GUILayout.FlexibleSpace();
            
            // 현재 탭 표시
            GUILayout.Label($"Tab: {_currentTab}", EditorStyles.miniLabel);
            
            EditorGUILayout.EndHorizontal();
        }
    }
}
