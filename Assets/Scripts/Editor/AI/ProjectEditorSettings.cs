using TMPro;
using UnityEditor;
using UnityEngine;

namespace Sc.Editor.AI
{
    /// <summary>
    /// 프로젝트 에디터 설정 (전역 기본값)
    /// SC Tools > Settings > Project Editor Settings 메뉴로 접근
    /// </summary>
    [CreateAssetMenu(fileName = "ProjectEditorSettings", menuName = "SC/Settings/Project Editor Settings")]
    public class ProjectEditorSettings : ScriptableObject
    {
        private const string SettingsPath = "Assets/Settings/ProjectEditorSettings.asset";

        [Header("TextMeshPro 설정")]
        [Tooltip("TMP 텍스트 생성 시 기본 폰트")]
        [SerializeField] private TMP_FontAsset _defaultFont;

        [Tooltip("TMP 텍스트 기본 폰트 크기")]
        [SerializeField] private float _defaultFontSize = 24f;

        [Header("UI 설정")]
        [Tooltip("기본 버튼 색상")]
        [SerializeField] private Color _defaultButtonColor = new Color(0.3f, 0.3f, 0.4f, 1f);

        [Tooltip("기본 배경 색상")]
        [SerializeField] private Color _defaultBackgroundColor = new Color(0.1f, 0.1f, 0.15f, 1f);

        #region Properties

        public TMP_FontAsset DefaultFont => _defaultFont;
        public float DefaultFontSize => _defaultFontSize;
        public Color DefaultButtonColor => _defaultButtonColor;
        public Color DefaultBackgroundColor => _defaultBackgroundColor;

        #endregion

        #region Singleton Access

        private static ProjectEditorSettings _instance;

        public static ProjectEditorSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = AssetDatabase.LoadAssetAtPath<ProjectEditorSettings>(SettingsPath);

                    if (_instance == null)
                    {
                        // Settings 폴더 확인
                        if (!AssetDatabase.IsValidFolder("Assets/Settings"))
                        {
                            AssetDatabase.CreateFolder("Assets", "Settings");
                        }

                        // 새 에셋 생성
                        _instance = CreateInstance<ProjectEditorSettings>();
                        AssetDatabase.CreateAsset(_instance, SettingsPath);
                        AssetDatabase.SaveAssets();

                        Debug.Log($"[ProjectEditorSettings] 새 설정 파일 생성: {SettingsPath}");
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Menu Items

        [MenuItem("SC Tools/Settings/Project Editor Settings", priority = 1000)]
        public static void SelectSettings()
        {
            var settings = Instance;
            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
        }

        [MenuItem("SC Tools/Settings/Reset to Defaults", priority = 1001)]
        public static void ResetToDefaults()
        {
            var settings = Instance;

            // 기본 폰트 찾기
            var defaultFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(
                "Assets/TextMesh Pro/Resources/Fonts & Materials/PretendardVariable SDF.asset");

            if (defaultFont != null)
            {
                var so = new SerializedObject(settings);
                so.FindProperty("_defaultFont").objectReferenceValue = defaultFont;
                so.FindProperty("_defaultFontSize").floatValue = 24f;
                so.FindProperty("_defaultButtonColor").colorValue = new Color(0.3f, 0.3f, 0.4f, 1f);
                so.FindProperty("_defaultBackgroundColor").colorValue = new Color(0.1f, 0.1f, 0.15f, 1f);
                so.ApplyModifiedProperties();

                Debug.Log("[ProjectEditorSettings] 기본값으로 초기화됨");
            }
            else
            {
                Debug.LogWarning("[ProjectEditorSettings] PretendardVariable SDF 폰트를 찾을 수 없음");
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// TMP_Text에 기본 폰트 적용
        /// </summary>
        public void ApplyDefaultFont(TMP_Text text)
        {
            if (text == null) return;

            if (_defaultFont != null)
            {
                text.font = _defaultFont;
            }
        }

        /// <summary>
        /// 기본 폰트 반환 (null이면 TMP 기본 폰트 사용)
        /// </summary>
        public TMP_FontAsset GetDefaultFontOrFallback()
        {
            if (_defaultFont != null)
                return _defaultFont;

            // Fallback: TMP 기본 폰트
            return TMP_Settings.defaultFontAsset;
        }

        #endregion
    }
}
