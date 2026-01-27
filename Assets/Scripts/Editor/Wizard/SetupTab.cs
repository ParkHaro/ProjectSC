using Sc.Editor.Wizard.Generators;
using UnityEditor;
using UnityEngine;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Setup 탭: 프로젝트 초기 설정을 위한 4단계 워크플로우.
    /// 1. Prefabs 생성 → 2. Addressables 설정 → 3. Main Scene 설정 → 4. Debug Panel (선택)
    /// </summary>
    public class SetupTab
    {
        public void Draw()
        {
            EditorGUILayout.Space(10);

            // 워크플로우 설명
            EditorGUILayout.HelpBox(
                "프로젝트 초기 설정 워크플로우:\n" +
                "1. UI 프리팹 생성 → 2. Addressables 등록 → 3. Main 씬 설정 → 4. 디버그 패널 (선택)",
                MessageType.Info);

            EditorGUILayout.Space(15);

            // Step 1: Prefabs
            DrawStep1_Prefabs();

            EditorGUILayout.Space(10);

            // Step 2: Addressables
            DrawStep2_Addressables();

            EditorGUILayout.Space(10);

            // Step 3: Main Scene
            DrawStep3_MainScene();

            EditorGUILayout.Space(10);

            // Step 4: Debug Panel (Optional)
            DrawStep4_DebugPanel();
        }

        private void DrawStep1_Prefabs()
        {
            EditorGUILayout.LabelField("Step 1: UI Prefabs", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("Screen 13개 + Popup 8개 프리팹을 자동 생성합니다.", EditorStyles.wordWrappedMiniLabel);

            EditorGUILayout.Space(5);

            GUI.backgroundColor = new Color(0.6f, 0.9f, 0.6f);
            if (GUILayout.Button("Generate All UI Prefabs", GUILayout.Height(35)))
            {
                var screenCount = PrefabGenerator.GenerateAllScreenPrefabs();
                var popupCount = PrefabGenerator.GenerateAllPopupPrefabs();
                EditorUtility.DisplayDialog("완료",
                    $"Screen 프리팹 {screenCount}개\nPopup 프리팹 {popupCount}개 생성됨", "확인");
            }

            GUI.backgroundColor = Color.white;

            // 재생성 버튼 (기존 프리팹 삭제 후 재생성)
            GUI.backgroundColor = new Color(1f, 0.8f, 0.5f);
            if (GUILayout.Button("🔄 Regenerate All UI Prefabs", GUILayout.Height(30)))
            {
                if (EditorUtility.DisplayDialog("프리팹 재생성",
                        "기존 Screen/Popup 프리팹을 모두 삭제하고 재생성합니다.\n계속하시겠습니까?", "재생성", "취소"))
                {
                    var (screens, popups) = PrefabGenerator.RegenerateAllPrefabs();
                    EditorUtility.DisplayDialog("완료",
                        $"Screen 프리팹 {screens}개\nPopup 프리팹 {popups}개 재생성됨", "확인");
                }
            }

            GUI.backgroundColor = Color.white;

            // 개별 생성 버튼
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Screens Only", GUILayout.Height(22)))
            {
                var count = PrefabGenerator.GenerateAllScreenPrefabs();
                EditorUtility.DisplayDialog("완료", $"Screen 프리팹 {count}개 생성됨", "확인");
            }

            if (GUILayout.Button("Popups Only", GUILayout.Height(22)))
            {
                var count = PrefabGenerator.GenerateAllPopupPrefabs();
                EditorUtility.DisplayDialog("완료", $"Popup 프리팹 {count}개 생성됨", "확인");
            }

            EditorGUILayout.EndHorizontal();

            // Widget 프리팹 생성
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Generate Widgets (ScreenHeader)", GUILayout.Height(22)))
            {
                var count = WidgetPrefabGenerator.GenerateAllWidgetPrefabs();
                EditorUtility.DisplayDialog("완료", $"Widget 프리팹 {count}개 생성됨", "확인");
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawStep2_Addressables()
        {
            EditorGUILayout.LabelField("Step 2: Addressables", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("UI 프리팹을 Addressables 그룹에 등록합니다.", EditorStyles.wordWrappedMiniLabel);

            EditorGUILayout.Space(5);

            GUI.backgroundColor = new Color(0.6f, 0.8f, 1f);
            if (GUILayout.Button("Setup Addressables", GUILayout.Height(35)))
            {
                AddressableSetupTool.SetupUIGroups();
                EditorUtility.DisplayDialog("완료", "Addressables UI Groups 설정 완료", "확인");
            }

            GUI.backgroundColor = Color.white;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Validate", GUILayout.Height(22)))
            {
                AddressableSetupTool.ValidateUIGroups();
            }

            if (GUILayout.Button("Clear", GUILayout.Height(22)))
            {
                if (EditorUtility.DisplayDialog("확인", "UI Groups를 제거하시겠습니까?", "제거", "취소"))
                {
                    AddressableSetupTool.ClearUIGroups();
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void DrawStep3_MainScene()
        {
            EditorGUILayout.LabelField("Step 3: Main Scene", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("프로덕션용 Main 씬을 생성합니다. (Managers + UIRoot)", EditorStyles.wordWrappedMiniLabel);

            EditorGUILayout.Space(5);

            GUI.backgroundColor = new Color(0.9f, 0.9f, 0.6f);
            if (GUILayout.Button("Setup Main Scene", GUILayout.Height(35)))
            {
                MainSceneSetup.SetupMainScene();
            }

            GUI.backgroundColor = Color.white;

            if (GUILayout.Button("Clear Scene Objects", GUILayout.Height(22)))
            {
                if (EditorUtility.DisplayDialog("확인", "씬의 모든 오브젝트를 삭제하시겠습니까?", "삭제", "취소"))
                {
                    MainSceneSetup.ClearMainSceneObjects();
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawStep4_DebugPanel()
        {
            EditorGUILayout.LabelField("Step 4: Debug Panel (Optional)", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("F12로 토글하는 디버그 네비게이션 패널을 추가합니다.", EditorStyles.wordWrappedMiniLabel);
            EditorGUILayout.LabelField("Development Build에서만 동작합니다.", EditorStyles.miniLabel);

            EditorGUILayout.Space(5);

            GUI.backgroundColor = new Color(0.9f, 0.8f, 0.6f);
            if (GUILayout.Button("Add Debug Navigation Panel", GUILayout.Height(30)))
            {
                DebugPanelSetup.AddDebugNavigationPanel();
            }

            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndVertical();
        }
    }
}