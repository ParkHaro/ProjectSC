using UnityEngine;
using UnityEditor;
using Sc.Editor.AI;
using Sc.Editor.Wizard.Generators;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Setup 탭: 씬/프리팹 설정 통합.
    /// MVPSceneSetup, LoadingSetup, SystemPopupSetup, PlayModeTestSetup, PrefabGenerator, LobbyScreenSetup 기능 통합.
    /// </summary>
    public class SetupTab
    {
        private bool _showMainSection = true;
        private bool _showUIPrefabsSection = true;
        private bool _showLobbySection = true;
        private bool _showMVPSection = false; // 기본 접힘 (레거시)
        private bool _showTestSection = false; // 기본 접힘
        private bool _showDialogSection = false; // 기본 접힘
        private bool _showAddressablesSection = true;

        public void Draw()
        {
            // Main Scene Section (Production)
            DrawMainSection();

            EditorGUILayout.Space(10);

            // UI Prefabs Section (Production) - NEW
            DrawUIPrefabsSection();

            EditorGUILayout.Space(10);

            // Lobby Setup Section - NEW
            DrawLobbySection();

            EditorGUILayout.Space(10);

            // Addressables Section
            DrawAddressablesSection();

            EditorGUILayout.Space(10);

            // MVP Scene Section (Legacy)
            DrawMVPSection();

            EditorGUILayout.Space(10);

            // Test Scenes Section
            DrawTestSection();

            EditorGUILayout.Space(10);

            // Dialog Prefabs Section
            DrawDialogSection();
        }

        private void DrawUIPrefabsSection()
        {
            _showUIPrefabsSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showUIPrefabsSection, "UI Prefabs (Production)");

            if (_showUIPrefabsSection)
            {
                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.HelpBox(
                    "모든 Screen/Popup 클래스를 스캔하여 프리팹을 자동 생성합니다.\n" +
                    "생성된 프리팹은 Addressables에 자동 등록됩니다.\n" +
                    "경로: Assets/Prefabs/UI/Screens/, Assets/Prefabs/UI/Popups/",
                    MessageType.Info);

                EditorGUILayout.Space(5);

                // 전체 생성 버튼
                GUI.backgroundColor = new Color(0.6f, 0.9f, 0.6f);
                if (GUILayout.Button("Generate All UI Prefabs", GUILayout.Height(35)))
                {
                    var screenCount = PrefabGenerator.GenerateAllScreenPrefabs();
                    var popupCount = PrefabGenerator.GenerateAllPopupPrefabs();
                    EditorUtility.DisplayDialog("완료",
                        $"Screen 프리팹 {screenCount}개, Popup 프리팹 {popupCount}개 생성됨", "확인");
                }
                GUI.backgroundColor = Color.white;

                EditorGUILayout.Space(5);

                // 개별 생성 버튼
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Screens Only", GUILayout.Height(25)))
                {
                    var count = PrefabGenerator.GenerateAllScreenPrefabs();
                    EditorUtility.DisplayDialog("완료", $"Screen 프리팹 {count}개 생성됨", "확인");
                }

                if (GUILayout.Button("Popups Only", GUILayout.Height(25)))
                {
                    var count = PrefabGenerator.GenerateAllPopupPrefabs();
                    EditorUtility.DisplayDialog("완료", $"Popup 프리팹 {count}개 생성됨", "확인");
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(5);

                // 폴더 열기
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Open Screens Folder", GUILayout.Height(20)))
                {
                    var path = "Assets/Prefabs/UI/Screens";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    EditorUtility.RevealInFinder(path);
                }

                if (GUILayout.Button("Open Popups Folder", GUILayout.Height(20)))
                {
                    var path = "Assets/Prefabs/UI/Popups";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    EditorUtility.RevealInFinder(path);
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawLobbySection()
        {
            _showLobbySection = EditorGUILayout.BeginFoldoutHeaderGroup(_showLobbySection, "Lobby Setup");

            if (_showLobbySection)
            {
                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.HelpBox(
                    "LobbyScreen의 탭 시스템을 설정합니다.\n" +
                    "TabButton 프리팹과 탭 컨텐츠를 자동으로 구성합니다.",
                    MessageType.Info);

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Setup Tab System", GUILayout.Height(30)))
                {
                    LobbyScreenSetup.SetupTabSystemMenu();
                }

                if (GUILayout.Button("Create TabButton Prefab", GUILayout.Height(30)))
                {
                    LobbyScreenSetup.CreateTabButtonPrefab();
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawMVPSection()
        {
            _showMVPSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showMVPSection, "MVP Scene Setup (Legacy)");
            
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

        private void DrawMainSection()
        {
            _showMainSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showMainSection, "Main Scene (Production)");

            if (_showMainSection)
            {
                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.HelpBox(
                    "프로덕션용 Main 씬을 생성합니다.\n" +
                    "- 동적 프리팹 로딩 (Addressables)\n" +
                    "- 초기화 시퀀스 + 로딩 UI\n" +
                    "- 에러 처리 및 재시도",
                    MessageType.Info);

                EditorGUILayout.BeginHorizontal();

                GUI.backgroundColor = new Color(0.6f, 0.8f, 1f);
                if (GUILayout.Button("Setup Main Scene", GUILayout.Height(35)))
                {
                    MainSceneSetup.SetupMainScene();
                }
                GUI.backgroundColor = Color.white;

                if (GUILayout.Button("Clear", GUILayout.Height(35)))
                {
                    if (EditorUtility.DisplayDialog("Main 씬 정리",
                        "Main 씬의 모든 오브젝트를 삭제하시겠습니까?", "삭제", "취소"))
                    {
                        MainSceneSetup.ClearMainSceneObjects();
                    }
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
