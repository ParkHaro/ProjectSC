using UnityEngine;
using UnityEditor;
using Sc.Editor.AI;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Settings 탭: 프로젝트 설정 관리.
    /// ProjectEditorSettings 기능 통합.
    /// </summary>
    public class SettingsTab
    {
        private bool _showProjectSettings = true;
        private bool _showWizardSettings = true;
        
        private SerializedObject _settingsSO;
        
        // Wizard 설정 (EditorPrefs)
        private const string PREF_AUTO_REFRESH = "ProjectSetupWizard.AutoRefresh";
        private const string PREF_REFRESH_INTERVAL = "ProjectSetupWizard.RefreshInterval";

        public void Draw()
        {
            // Project Settings Section
            DrawProjectSettingsSection();
            
            EditorGUILayout.Space(10);
            
            // Wizard Settings Section
            DrawWizardSettingsSection();
            
            EditorGUILayout.Space(10);
            
            // Actions Section
            DrawActionsSection();
        }

        private void DrawProjectSettingsSection()
        {
            _showProjectSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showProjectSettings, "Project Settings");
            
            if (_showProjectSettings)
            {
                EditorGUILayout.BeginVertical("box");
                
                var settings = ProjectEditorSettings.Instance;
                
                if (settings == null)
                {
                    EditorGUILayout.HelpBox("ProjectEditorSettings를 찾을 수 없습니다.", MessageType.Warning);
                    
                    if (GUILayout.Button("Create Settings Asset"))
                    {
                        ProjectEditorSettings.Instance.ToString(); // 강제 생성
                    }
                }
                else
                {
                    // SerializedObject 초기화
                    if (_settingsSO == null || _settingsSO.targetObject != settings)
                    {
                        _settingsSO = new SerializedObject(settings);
                    }
                    
                    _settingsSO.Update();
                    
                    // Font
                    var fontProp = _settingsSO.FindProperty("_defaultFont");
                    EditorGUILayout.PropertyField(fontProp, new GUIContent("Default Font"));
                    
                    // Font Size
                    var fontSizeProp = _settingsSO.FindProperty("_defaultFontSize");
                    EditorGUILayout.PropertyField(fontSizeProp, new GUIContent("Default Font Size"));
                    
                    // Button Color
                    var buttonColorProp = _settingsSO.FindProperty("_defaultButtonColor");
                    EditorGUILayout.PropertyField(buttonColorProp, new GUIContent("Button Color"));
                    
                    // Background Color
                    var bgColorProp = _settingsSO.FindProperty("_defaultBackgroundColor");
                    EditorGUILayout.PropertyField(bgColorProp, new GUIContent("Background Color"));
                    
                    _settingsSO.ApplyModifiedProperties();
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawWizardSettingsSection()
        {
            _showWizardSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showWizardSettings, "Wizard Preferences");
            
            if (_showWizardSettings)
            {
                EditorGUILayout.BeginVertical("box");
                
                // Auto Refresh
                var autoRefresh = EditorPrefs.GetBool(PREF_AUTO_REFRESH, true);
                var newAutoRefresh = EditorGUILayout.Toggle("Auto-refresh in Play Mode", autoRefresh);
                if (newAutoRefresh != autoRefresh)
                {
                    EditorPrefs.SetBool(PREF_AUTO_REFRESH, newAutoRefresh);
                }
                
                // Refresh Interval
                EditorGUI.BeginDisabledGroup(!newAutoRefresh);
                var refreshInterval = EditorPrefs.GetFloat(PREF_REFRESH_INTERVAL, 0.1f);
                var newInterval = EditorGUILayout.Slider("Refresh Interval (s)", refreshInterval, 0.05f, 1f);
                if (!Mathf.Approximately(newInterval, refreshInterval))
                {
                    EditorPrefs.SetFloat(PREF_REFRESH_INTERVAL, newInterval);
                }
                EditorGUI.EndDisabledGroup();
                
                EditorGUILayout.Space(5);
                
                // Remember Tab
                EditorGUILayout.HelpBox("탭 상태는 자동으로 저장됩니다.", MessageType.Info);
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawActionsSection()
        {
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Reset to Defaults", GUILayout.Height(25)))
            {
                if (EditorUtility.DisplayDialog("설정 초기화", 
                    "모든 설정을 기본값으로 초기화하시겠습니까?", "초기화", "취소"))
                {
                    ResetToDefaults();
                }
            }
            
            if (GUILayout.Button("Open Settings Asset", GUILayout.Height(25)))
            {
                var settings = ProjectEditorSettings.Instance;
                if (settings != null)
                {
                    Selection.activeObject = settings;
                    EditorGUIUtility.PingObject(settings);
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }

        private void ResetToDefaults()
        {
            // Wizard Prefs
            EditorPrefs.SetBool(PREF_AUTO_REFRESH, true);
            EditorPrefs.SetFloat(PREF_REFRESH_INTERVAL, 0.1f);
            
            // Project Settings (via menu)
            EditorApplication.ExecuteMenuItem("SC Tools/Settings/Reset to Defaults");
            
            Debug.Log("[SettingsTab] 설정이 초기화되었습니다.");
        }
    }
}
