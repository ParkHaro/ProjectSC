using UnityEngine;
using UnityEditor;
using Sc.Editor.AI;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Setup 탭: 씬/프리팹 설정 통합.
    /// MVPSceneSetup, LoadingSetup, SystemPopupSetup, PlayModeTestSetup 기능 통합.
    /// </summary>
    public class SetupTab
    {
        private bool _showMVPSection = true;
        private bool _showTestSection = true;
        private bool _showDialogSection = true;
        private bool _showAddressablesSection = true;

        public void Draw()
        {
            // MVP Scene Section
            DrawMVPSection();
            
            EditorGUILayout.Space(10);
            
            // Test Scenes Section
            DrawTestSection();
            
            EditorGUILayout.Space(10);
            
            // Dialog Prefabs Section
            DrawDialogSection();
            
            EditorGUILayout.Space(10);
            
            // Addressables Section
            DrawAddressablesSection();
        }

        private void DrawMVPSection()
        {
            _showMVPSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showMVPSection, "MVP Scene Setup");
            
            if (_showMVPSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                EditorGUILayout.LabelField("씬 설정", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Setup MVP Scene", GUILayout.Height(30)))
                {
                    MVPSceneSetup.SetupMVPScene();
                }
                
                if (GUILayout.Button("Clear Objects", GUILayout.Height(30)))
                {
                    MVPSceneSetup.ClearMVPObjects();
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("프리팹", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Create All", GUILayout.Height(25)))
                {
                    MVPSceneSetup.CreateAllPrefabs();
                }
                
                if (GUILayout.Button("Recreate (Force)", GUILayout.Height(25)))
                {
                    MVPSceneSetup.RecreateAllPrefabs();
                }
                
                if (GUILayout.Button("Delete All", GUILayout.Height(25)))
                {
                    if (EditorUtility.DisplayDialog("프리팹 삭제", 
                        "모든 MVP 프리팹을 삭제하시겠습니까?", "삭제", "취소"))
                    {
                        MVPSceneSetup.DeleteAllPrefabs();
                    }
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("마스터 데이터", EditorStyles.boldLabel);
                
                if (GUILayout.Button("Generate All Master Data", GUILayout.Height(25)))
                {
                    MVPSceneSetup.GenerateMasterData();
                }
                
                EditorGUILayout.Space(5);
                
                // Full Reset
                GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
                if (GUILayout.Button("🔄 Rebuild All (Full Reset)", GUILayout.Height(35)))
                {
                    if (EditorUtility.DisplayDialog("전체 재빌드", 
                        "마스터 데이터 → 프리팹 → 씬을 전체 재빌드합니다.\n계속하시겠습니까?", 
                        "재빌드", "취소"))
                    {
                        MVPSceneSetup.RebuildAll();
                    }
                }
                GUI.backgroundColor = Color.white;
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawTestSection()
        {
            _showTestSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showTestSection, "Test Scenes");
            
            if (_showTestSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                // UI Test
                EditorGUILayout.LabelField("UI Navigation Test", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Setup Test Scene", GUILayout.Height(25)))
                {
                    UITestSceneSetup.SetupTestScene();
                }
                
                if (GUILayout.Button("Create Prefabs", GUILayout.Height(25)))
                {
                    PlayModeTestSetup.CreateAllTestPrefabs();
                }
                
                if (GUILayout.Button("Clear", GUILayout.Height(25)))
                {
                    UITestSceneSetup.ClearTestObjects();
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(5);
                
                // Loading Test
                EditorGUILayout.LabelField("Loading Test", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Setup Loading Scene", GUILayout.Height(25)))
                {
                    LoadingSetup.SetupLoadingTestScene();
                }
                
                if (GUILayout.Button("Create Prefab", GUILayout.Height(25)))
                {
                    LoadingSetup.CreateLoadingWidgetPrefab();
                }
                
                if (GUILayout.Button("Clear", GUILayout.Height(25)))
                {
                    LoadingSetup.ClearLoadingTestObjects();
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawDialogSection()
        {
            _showDialogSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogSection, "Dialog Prefabs");
            
            if (_showDialogSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Create All Dialogs", GUILayout.Height(25)))
                {
                    SystemPopupSetup.CreateAllDialogPrefabs();
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("ConfirmPopup", GUILayout.Height(25)))
                {
                    SystemPopupSetup.CreateConfirmPopupPrefab();
                }
                
                if (GUILayout.Button("CostConfirmPopup", GUILayout.Height(25)))
                {
                    SystemPopupSetup.CreateCostConfirmPopupPrefab();
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawAddressablesSection()
        {
            _showAddressablesSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showAddressablesSection, "Addressables");
            
            if (_showAddressablesSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                EditorGUILayout.HelpBox(
                    "UI 프리팹을 Addressables에 자동 등록합니다.\n" +
                    "Group: UI_Screens, UI_Popups, UI_Widgets", 
                    MessageType.Info);
                
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Setup UI Groups", GUILayout.Height(30)))
                {
                    AddressableSetupTool.SetupUIGroups();
                }
                
                if (GUILayout.Button("Clear UI Groups", GUILayout.Height(30)))
                {
                    if (EditorUtility.DisplayDialog("Addressables 정리", 
                        "모든 UI Group을 제거하시겠습니까?", "제거", "취소"))
                    {
                        AddressableSetupTool.ClearUIGroups();
                    }
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
