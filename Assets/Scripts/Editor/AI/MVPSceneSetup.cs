using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

namespace Sc.Editor.AI
{
    /// <summary>
    /// MVP 화면 테스트용 씬/프리팹 자동 생성 도구.
    /// </summary>
    public static class MVPSceneSetup
    {
        private const string PrefabPath = "Assets/Prefabs/UI/MVP";
        private const string ScenePath = "Assets/Scenes";

        #region Menu Items

        [MenuItem("SC Tools/MVP/Rebuild All (Full Reset)", priority = 99)]
        public static void RebuildAll()
        {
            Debug.Log("[MVPSceneSetup] ========== Full Rebuild Start ==========");

            // 0. 마스터 데이터 확인 및 생성
            EnsureMasterDataGenerated();

            // 1. 프리팹 전체 삭제 및 재생성
            RecreateAllPrefabs();

            // 2. 씬 오브젝트 정리
            ClearMVPObjects();

            // 3. 씬 재설정
            SetupMVPScene();

            Debug.Log("[MVPSceneSetup] ========== Full Rebuild Complete ==========");
        }

        [MenuItem("SC Tools/MVP/Setup MVP Scene", priority = 100)]
        public static void SetupMVPScene()
        {
            EnsureFolders();

            // 0. 마스터 데이터 확인 및 생성
            EnsureMasterDataGenerated();

            // 1. Canvas 생성
            var canvas = CreateOrGetCanvas("MVPCanvas");

            // 2. EventSystem 생성
            CreateEventSystem();

            // 3. NavigationManager 생성
            CreateNavigationManager();

            // 4. DataManager 생성
            CreateDataManager();

            // 5. NetworkManager & GameBootstrap 생성
            CreateNetworkManager();
            CreateGameBootstrap();

            // 6. GameFlowController 생성
            CreateGameFlowController();

            // 7. Header/Screen/Popup Containers
            var headerContainer = CreateContainer(canvas.transform, "HeaderContainer", 50);
            var screenContainer = CreateContainer(canvas.transform, "ScreenContainer", 0);
            var popupContainer = CreateContainer(canvas.transform, "PopupContainer", 100);

            // 8. 프리팹 생성
            var screenHeaderPrefab = CreateScreenHeaderPrefab();
            var titlePrefab = CreateTitleScreenPrefab();
            var lobbyPrefab = CreateLobbyScreenPrefab();
            var gachaPrefab = CreateGachaScreenPrefab();
            var characterListPrefab = CreateCharacterListScreenPrefab();
            var characterDetailPrefab = CreateCharacterDetailScreenPrefab();
            var gachaResultPrefab = CreateGachaResultPopupPrefab();

            // 9. ScreenHeader 배치 (상단 고정)
            InstantiateHeaderPrefab(screenHeaderPrefab, headerContainer);

            // 10. 모든 Screen/Popup 프리팹을 씬에 배치 (비활성화 상태)
            InstantiateScreenPrefab(titlePrefab, screenContainer, true);  // TitleScreen만 활성화
            InstantiateScreenPrefab(lobbyPrefab, screenContainer, false);
            InstantiateScreenPrefab(gachaPrefab, screenContainer, false);
            InstantiateScreenPrefab(characterListPrefab, screenContainer, false);
            InstantiateScreenPrefab(characterDetailPrefab, screenContainer, false);
            InstantiatePopupPrefab(gachaResultPrefab, popupContainer, false);

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("[MVPSceneSetup] MVP Scene setup complete!");
        }

        [MenuItem("SC Tools/MVP/Create All Prefabs", priority = 101)]
        public static void CreateAllPrefabs()
        {
            EnsureFolders();

            CreateScreenHeaderPrefab();
            CreateTitleScreenPrefab();
            CreateLobbyScreenPrefab();
            CreateGachaScreenPrefab();
            CreateCharacterListScreenPrefab();
            CreateCharacterDetailScreenPrefab();
            CreateGachaResultItemPrefab();  // GachaResultPopup보다 먼저 생성
            CreateGachaResultPopupPrefab();
            CreateCurrencyHUDPrefab();
            CreateCharacterListItemPrefab();

            AssetDatabase.Refresh();
            Debug.Log("[MVPSceneSetup] All MVP prefabs created!");
        }

        [MenuItem("SC Tools/MVP/Recreate All Prefabs (Force)", priority = 102)]
        public static void RecreateAllPrefabs()
        {
            EnsureFolders();

            // 기존 프리팹 삭제
            DeleteAllPrefabs();

            // 새로 생성
            CreateScreenHeaderPrefab();
            CreateTitleScreenPrefab();
            CreateLobbyScreenPrefab();
            CreateGachaScreenPrefab();
            CreateCharacterListScreenPrefab();
            CreateCharacterDetailScreenPrefab();
            CreateGachaResultItemPrefab();  // GachaResultPopup보다 먼저 생성
            CreateGachaResultPopupPrefab();
            CreateCurrencyHUDPrefab();
            CreateCharacterListItemPrefab();

            AssetDatabase.Refresh();
            Debug.Log("[MVPSceneSetup] All MVP prefabs recreated with current settings!");
        }

        [MenuItem("SC Tools/MVP/Delete All Prefabs", priority = 103)]
        public static void DeleteAllPrefabs()
        {
            var prefabFiles = new[]
            {
                "ScreenHeader.prefab",
                "TitleScreen.prefab",
                "LobbyScreen.prefab",
                "GachaScreen.prefab",
                "CharacterListScreen.prefab",
                "CharacterDetailScreen.prefab",
                "GachaResultPopup.prefab",
                "CurrencyHUD.prefab",
                "CharacterListItem.prefab",
                "GachaResultItem.prefab"
            };

            foreach (var file in prefabFiles)
            {
                var path = $"{PrefabPath}/{file}";
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path) != null)
                {
                    AssetDatabase.DeleteAsset(path);
                    Debug.Log($"[MVPSceneSetup] Deleted: {path}");
                }
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("SC Tools/MVP/Generate Master Data", priority = 104)]
        public static void GenerateMasterData()
        {
            const string jsonPath = "Assets/Data/MasterData/";

            Debug.Log("[MVPSceneSetup] Force reimporting all Master Data...");
            AssetDatabase.ImportAsset(jsonPath, ImportAssetOptions.ImportRecursive | ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
            Debug.Log("[MVPSceneSetup] Master Data generation complete!");
        }

        [MenuItem("SC Tools/MVP/Clear MVP Objects", priority = 200)]
        public static void ClearMVPObjects()
        {
            var canvas = GameObject.Find("MVPCanvas");
            if (canvas != null) Object.DestroyImmediate(canvas);

            var navManager = GameObject.Find("NavigationManager");
            if (navManager != null) Object.DestroyImmediate(navManager);

            var dataManager = GameObject.Find("DataManager");
            if (dataManager != null) Object.DestroyImmediate(dataManager);

            var networkManager = GameObject.Find("NetworkManager");
            if (networkManager != null) Object.DestroyImmediate(networkManager);

            var gameBootstrap = GameObject.Find("GameBootstrap");
            if (gameBootstrap != null) Object.DestroyImmediate(gameBootstrap);

            var gameFlowController = GameObject.Find("GameFlowController");
            if (gameFlowController != null) Object.DestroyImmediate(gameFlowController);

            // ScreenHeader 싱글턴 인스턴스 정리
            if (Common.UI.Widgets.ScreenHeader.Instance != null)
            {
                Object.DestroyImmediate(Common.UI.Widgets.ScreenHeader.Instance.gameObject);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("[MVPSceneSetup] MVP objects cleared!");
        }

        #endregion

        #region Scene Setup

        private static Canvas CreateOrGetCanvas(string name)
        {
            var existing = GameObject.Find(name);
            if (existing != null)
            {
                return existing.GetComponent<Canvas>();
            }

            var canvasGo = new GameObject(name);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            canvasGo.AddComponent<GraphicRaycaster>();

            return canvas;
        }

        private static void CreateEventSystem()
        {
            var existing = Object.FindObjectOfType<EventSystem>();
            if (existing != null) return;

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private static void CreateNavigationManager()
        {
            var existing = GameObject.Find("NavigationManager");
            if (existing != null) return;

            var go = new GameObject("NavigationManager");
            go.AddComponent<Common.UI.NavigationManager>();
        }

        private static void CreateDataManager()
        {
            var existing = GameObject.Find("DataManager");
            if (existing != null)
            {
                // 기존 DataManager에도 Database 할당 확인
                AssignDatabases(existing.GetComponent<Core.DataManager>());
                return;
            }

            var go = new GameObject("DataManager");
            var dataManager = go.AddComponent<Core.DataManager>();
            AssignDatabases(dataManager);
        }

        private static void AssignDatabases(Core.DataManager dataManager)
        {
            if (dataManager == null) return;

            const string basePath = "Assets/Data/Generated/";

            var characterDb = AssetDatabase.LoadAssetAtPath<Data.CharacterDatabase>(basePath + "CharacterDatabase.asset");
            var skillDb = AssetDatabase.LoadAssetAtPath<Data.SkillDatabase>(basePath + "SkillDatabase.asset");
            var itemDb = AssetDatabase.LoadAssetAtPath<Data.ItemDatabase>(basePath + "ItemDatabase.asset");
            var stageDb = AssetDatabase.LoadAssetAtPath<Data.StageDatabase>(basePath + "StageDatabase.asset");
            var gachaPoolDb = AssetDatabase.LoadAssetAtPath<Data.GachaPoolDatabase>(basePath + "GachaPoolDatabase.asset");

            var so = new SerializedObject(dataManager);
            so.FindProperty("_characterDatabase").objectReferenceValue = characterDb;
            so.FindProperty("_skillDatabase").objectReferenceValue = skillDb;
            so.FindProperty("_itemDatabase").objectReferenceValue = itemDb;
            so.FindProperty("_stageDatabase").objectReferenceValue = stageDb;
            so.FindProperty("_gachaPoolDatabase").objectReferenceValue = gachaPoolDb;
            so.ApplyModifiedPropertiesWithoutUndo();

            if (characterDb == null || skillDb == null || itemDb == null || stageDb == null || gachaPoolDb == null)
            {
                Debug.LogWarning("[MVPSceneSetup] 일부 Database 에셋이 없습니다. SC/Data/Master Data Generator로 생성하세요.");
            }
            else
            {
                Debug.Log("[MVPSceneSetup] DataManager에 Database 에셋 할당 완료");
            }
        }

        private static void EnsureMasterDataGenerated()
        {
            const string generatedPath = "Assets/Data/Generated/";
            const string jsonPath = "Assets/Data/MasterData/";

            // 필요한 Database들 확인
            var databases = new[]
            {
                ("CharacterDatabase.asset", "Character.json"),
                ("SkillDatabase.asset", "Skill.json"),
                ("ItemDatabase.asset", "Item.json"),
                ("StageDatabase.asset", "Stage.json"),
                ("GachaPoolDatabase.asset", "GachaPool.json"),
                ("ScreenHeaderConfigDatabase.asset", "ScreenHeaderConfig.json")
            };

            bool needsReimport = false;

            foreach (var (dbFile, jsonFile) in databases)
            {
                var dbPath = generatedPath + dbFile;
                var srcPath = jsonPath + jsonFile;

                if (AssetDatabase.LoadAssetAtPath<ScriptableObject>(dbPath) == null)
                {
                    if (System.IO.File.Exists(srcPath))
                    {
                        needsReimport = true;
                        Debug.Log($"[MVPSceneSetup] Missing: {dbFile}, will reimport from {jsonFile}");
                    }
                }
            }

            if (needsReimport)
            {
                Debug.Log("[MVPSceneSetup] Reimporting Master Data...");
                AssetDatabase.ImportAsset(jsonPath, ImportAssetOptions.ImportRecursive);
                AssetDatabase.Refresh();
            }
        }

        private static void CreateNetworkManager()
        {
            var existing = GameObject.Find("NetworkManager");
            if (existing != null) return;

            var go = new GameObject("NetworkManager");
            go.AddComponent<Core.NetworkManager>();
        }

        private static void CreateGameBootstrap()
        {
            var existing = GameObject.Find("GameBootstrap");
            if (existing != null) return;

            var go = new GameObject("GameBootstrap");
            go.AddComponent<Core.GameBootstrap>();
        }

        private static void CreateGameFlowController()
        {
            var existing = GameObject.Find("GameFlowController");
            if (existing != null) return;

            var go = new GameObject("GameFlowController");
            go.AddComponent<Contents.Title.GameFlowController>();
        }

        private static RectTransform CreateContainer(Transform parent, string name, int sortingOrder)
        {
            var existing = parent.Find(name);
            if (existing != null)
            {
                return existing.GetComponent<RectTransform>();
            }

            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            var sortGroup = go.AddComponent<Canvas>();
            sortGroup.overrideSorting = true;
            sortGroup.sortingOrder = sortingOrder;

            go.AddComponent<GraphicRaycaster>();

            return rect;
        }

        #endregion

        #region TitleScreen Prefab

        private static GameObject CreateTitleScreenPrefab()
        {
            var prefabPath = $"{PrefabPath}/TitleScreen.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var panel = CreateFullScreenPanel("TitleScreen", new Color(0.05f, 0.05f, 0.15f, 1f));

            // Canvas 추가 (가시성 제어용)
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 타이틀 텍스트
            CreateTMPText(panel.transform, "TitleText", "Project SC",
                new Vector2(0, 100), new Vector2(600, 100), 72, TextAlignmentOptions.Center);

            // 터치 안내 텍스트
            var touchText = CreateTMPText(panel.transform, "TouchToStartText", "Touch to Start",
                new Vector2(0, -100), new Vector2(400, 50), 28, TextAlignmentOptions.Center);

            // 터치 버튼 (전체 화면)
            var touchBtn = CreateFullScreenButton(panel.transform, "TouchArea");

            // 계정 초기화 버튼 (우측 하단)
            var resetBtn = CreateTMPButton(panel.transform, "ResetAccountButton", "계정 초기화",
                new Vector2(0, -450), new Vector2(200, 50), new Color(0.5f, 0.2f, 0.2f, 1f));

            // TitleScreen 컴포넌트 추가
            var titleScreen = panel.AddComponent<Contents.Title.TitleScreen>();

            var so = new SerializedObject(titleScreen);
            so.FindProperty("_touchArea").objectReferenceValue = touchBtn;
            so.FindProperty("_touchToStartText").objectReferenceValue = touchText;
            so.FindProperty("_resetAccountButton").objectReferenceValue = resetBtn;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region LobbyScreen Prefab

        private static GameObject CreateLobbyScreenPrefab()
        {
            var prefabPath = $"{PrefabPath}/LobbyScreen.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var panel = CreateFullScreenPanel("LobbyScreen", new Color(0.1f, 0.1f, 0.2f, 1f));

            // Canvas 추가
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 환영 텍스트 (상단)
            var welcomeText = CreateTMPText(panel.transform, "WelcomeText", "환영합니다!",
                new Vector2(0, 400), new Vector2(600, 60), 36, TextAlignmentOptions.Center);

            // 가챠 버튼
            var gachaBtn = CreateTMPButton(panel.transform, "GachaButton", "가챠",
                new Vector2(-150, 0), new Vector2(250, 80), new Color(0.8f, 0.4f, 0.2f, 1f));

            // 캐릭터 버튼
            var characterBtn = CreateTMPButton(panel.transform, "CharacterButton", "캐릭터",
                new Vector2(150, 0), new Vector2(250, 80), new Color(0.2f, 0.5f, 0.8f, 1f));

            // LobbyScreen 컴포넌트 추가
            var lobbyScreen = panel.AddComponent<Contents.Lobby.LobbyScreen>();

            var so = new SerializedObject(lobbyScreen);
            so.FindProperty("_gachaButton").objectReferenceValue = gachaBtn;
            so.FindProperty("_characterButton").objectReferenceValue = characterBtn;
            so.FindProperty("_welcomeText").objectReferenceValue = welcomeText;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region GachaScreen Prefab

        private static GameObject CreateGachaScreenPrefab()
        {
            var prefabPath = $"{PrefabPath}/GachaScreen.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var panel = CreateFullScreenPanel("GachaScreen", new Color(0.15f, 0.1f, 0.2f, 1f));

            // Canvas 추가
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 제목
            CreateTMPText(panel.transform, "Title", "소환",
                new Vector2(0, 400), new Vector2(400, 80), 48, TextAlignmentOptions.Center);

            // 보유 젬 표시
            var gemText = CreateTMPText(panel.transform, "GemText", "0",
                new Vector2(0, 300), new Vector2(300, 50), 32, TextAlignmentOptions.Center);

            // 천장 카운트
            var pityText = CreateTMPText(panel.transform, "PityCountText", "천장: 0/90",
                new Vector2(0, 250), new Vector2(300, 40), 24, TextAlignmentOptions.Center);

            // 1회 소환 버튼
            var singleBtn = CreateTMPButton(panel.transform, "SinglePullButton", "1회 소환",
                new Vector2(-150, 50), new Vector2(250, 100), new Color(0.6f, 0.3f, 0.6f, 1f));

            // 1회 비용 텍스트
            var singleCostText = CreateTMPText(singleBtn.transform, "CostText", "300",
                new Vector2(0, -30), new Vector2(100, 30), 20, TextAlignmentOptions.Center);

            // 10회 소환 버튼
            var multiBtn = CreateTMPButton(panel.transform, "MultiPullButton", "10회 소환",
                new Vector2(150, 50), new Vector2(250, 100), new Color(0.8f, 0.5f, 0.2f, 1f));

            // 10회 비용 텍스트
            var multiCostText = CreateTMPText(multiBtn.transform, "CostText", "2700",
                new Vector2(0, -30), new Vector2(100, 30), 20, TextAlignmentOptions.Center);

            // 뒤로가기 버튼
            var backBtn = CreateTMPButton(panel.transform, "BackButton", "뒤로",
                new Vector2(0, -350), new Vector2(200, 60), new Color(0.4f, 0.4f, 0.4f, 1f));

            // GachaScreen 컴포넌트 추가
            var gachaScreen = panel.AddComponent<Contents.Gacha.GachaScreen>();

            var so = new SerializedObject(gachaScreen);
            so.FindProperty("_singlePullButton").objectReferenceValue = singleBtn;
            so.FindProperty("_multiPullButton").objectReferenceValue = multiBtn;
            so.FindProperty("_backButton").objectReferenceValue = backBtn;
            so.FindProperty("_singleCostText").objectReferenceValue = singleCostText;
            so.FindProperty("_multiCostText").objectReferenceValue = multiCostText;
            so.FindProperty("_pityCountText").objectReferenceValue = pityText;
            so.FindProperty("_gemText").objectReferenceValue = gemText;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region CharacterListScreen Prefab

        private static GameObject CreateCharacterListScreenPrefab()
        {
            var prefabPath = $"{PrefabPath}/CharacterListScreen.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var panel = CreateFullScreenPanel("CharacterListScreen", new Color(0.1f, 0.15f, 0.2f, 1f));

            // Canvas 추가
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 제목
            CreateTMPText(panel.transform, "Title", "캐릭터",
                new Vector2(0, 450), new Vector2(400, 80), 48, TextAlignmentOptions.Center);

            // 카운트 텍스트
            var countText = CreateTMPText(panel.transform, "CountText", "보유 캐릭터: 0",
                new Vector2(0, 380), new Vector2(400, 40), 24, TextAlignmentOptions.Center);

            // 스크롤 뷰
            var scrollView = CreateScrollView(panel.transform, "ScrollView",
                new Vector2(0, 0), new Vector2(800, 600));

            var listContainer = scrollView.transform.Find("Viewport/Content");

            // 뒤로가기 버튼
            var backBtn = CreateTMPButton(panel.transform, "BackButton", "뒤로",
                new Vector2(0, -400), new Vector2(200, 60), new Color(0.4f, 0.4f, 0.4f, 1f));

            // CharacterListItem 프리팹 로드 또는 생성
            var itemPrefab = CreateCharacterListItemPrefab();

            // CharacterListScreen 컴포넌트 추가
            var characterListScreen = panel.AddComponent<Contents.Character.CharacterListScreen>();

            var so = new SerializedObject(characterListScreen);
            so.FindProperty("_listContainer").objectReferenceValue = listContainer;
            so.FindProperty("_characterItemPrefab").objectReferenceValue = itemPrefab;
            so.FindProperty("_scrollRect").objectReferenceValue = scrollView.GetComponent<ScrollRect>();
            so.FindProperty("_backButton").objectReferenceValue = backBtn;
            so.FindProperty("_countText").objectReferenceValue = countText;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region CharacterDetailScreen Prefab

        private static GameObject CreateCharacterDetailScreenPrefab()
        {
            var prefabPath = $"{PrefabPath}/CharacterDetailScreen.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var panel = CreateFullScreenPanel("CharacterDetailScreen", new Color(0.08f, 0.1f, 0.18f, 1f));

            // Canvas 추가
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 타이틀
            var titleText = CreateTMPText(panel.transform, "TitleText", "캐릭터 상세",
                new Vector2(0, 480), new Vector2(400, 60), 36, TextAlignmentOptions.Center);

            // 캐릭터 이미지 영역 (배경)
            var charImageArea = new GameObject("CharacterImageArea");
            charImageArea.transform.SetParent(panel.transform, false);
            var charImageRect = charImageArea.AddComponent<RectTransform>();
            charImageRect.anchorMin = new Vector2(0.5f, 0.5f);
            charImageRect.anchorMax = new Vector2(0.5f, 0.5f);
            charImageRect.anchoredPosition = new Vector2(-250, 150);
            charImageRect.sizeDelta = new Vector2(300, 350);
            var charImage = charImageArea.AddComponent<Image>();
            charImage.color = new Color(0.2f, 0.2f, 0.3f, 0.5f);

            // 기본 정보 영역
            var nameText = CreateTMPText(panel.transform, "NameText", "캐릭터 이름",
                new Vector2(150, 350), new Vector2(400, 50), 32, TextAlignmentOptions.Left);

            var rarityText = CreateTMPText(panel.transform, "RarityText", "SSR",
                new Vector2(150, 300), new Vector2(100, 40), 28, TextAlignmentOptions.Left);

            var classText = CreateTMPText(panel.transform, "ClassText", "전사",
                new Vector2(260, 300), new Vector2(100, 40), 24, TextAlignmentOptions.Left);

            var elementText = CreateTMPText(panel.transform, "ElementText", "화",
                new Vector2(370, 300), new Vector2(80, 40), 24, TextAlignmentOptions.Left);

            var levelText = CreateTMPText(panel.transform, "LevelText", "Lv. 1",
                new Vector2(150, 250), new Vector2(150, 40), 28, TextAlignmentOptions.Left);

            var ascensionText = CreateTMPText(panel.transform, "AscensionText", "돌파 0단계",
                new Vector2(320, 250), new Vector2(150, 40), 20, TextAlignmentOptions.Left);

            // 스탯 영역
            CreateTMPText(panel.transform, "StatLabel", "스탯",
                new Vector2(150, 180), new Vector2(100, 30), 20, TextAlignmentOptions.Left);

            var hpText = CreateTMPText(panel.transform, "HpText", "HP: 1000",
                new Vector2(150, 140), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            var atkText = CreateTMPText(panel.transform, "AtkText", "ATK: 100",
                new Vector2(350, 140), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            var defText = CreateTMPText(panel.transform, "DefText", "DEF: 50",
                new Vector2(150, 100), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            var spdText = CreateTMPText(panel.transform, "SpdText", "SPD: 100",
                new Vector2(350, 100), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            var critRateText = CreateTMPText(panel.transform, "CritRateText", "치명률: 5%",
                new Vector2(150, 60), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            var critDamageText = CreateTMPText(panel.transform, "CritDamageText", "치명피해: 150%",
                new Vector2(350, 60), new Vector2(180, 30), 18, TextAlignmentOptions.Left);

            // 설명 영역
            CreateTMPText(panel.transform, "DescLabel", "설명",
                new Vector2(0, -100), new Vector2(700, 30), 20, TextAlignmentOptions.Left);

            var descriptionText = CreateTMPText(panel.transform, "DescriptionText", "캐릭터 설명이 여기에 표시됩니다.",
                new Vector2(0, -180), new Vector2(700, 100), 16, TextAlignmentOptions.TopLeft);

            // 뒤로가기 버튼
            var backBtn = CreateTMPButton(panel.transform, "BackButton", "뒤로",
                new Vector2(0, -420), new Vector2(200, 60), new Color(0.4f, 0.4f, 0.4f, 1f));

            // CharacterDetailScreen 컴포넌트 추가
            var detailScreen = panel.AddComponent<Contents.Character.CharacterDetailScreen>();

            var so = new SerializedObject(detailScreen);
            so.FindProperty("_backButton").objectReferenceValue = backBtn;
            so.FindProperty("_titleText").objectReferenceValue = titleText;
            so.FindProperty("_nameText").objectReferenceValue = nameText;
            so.FindProperty("_rarityText").objectReferenceValue = rarityText;
            so.FindProperty("_classText").objectReferenceValue = classText;
            so.FindProperty("_elementText").objectReferenceValue = elementText;
            so.FindProperty("_levelText").objectReferenceValue = levelText;
            so.FindProperty("_characterImage").objectReferenceValue = charImage;
            so.FindProperty("_hpText").objectReferenceValue = hpText;
            so.FindProperty("_atkText").objectReferenceValue = atkText;
            so.FindProperty("_defText").objectReferenceValue = defText;
            so.FindProperty("_spdText").objectReferenceValue = spdText;
            so.FindProperty("_critRateText").objectReferenceValue = critRateText;
            so.FindProperty("_critDamageText").objectReferenceValue = critDamageText;
            so.FindProperty("_descriptionText").objectReferenceValue = descriptionText;
            so.FindProperty("_ascensionText").objectReferenceValue = ascensionText;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region ScreenHeader Prefab

        private static GameObject CreateScreenHeaderPrefab()
        {
            var prefabPath = $"{PrefabPath}/ScreenHeader.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            // 상단 고정 패널 (높이 100)
            var panel = new GameObject("ScreenHeader");
            var panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.pivot = new Vector2(0.5f, 1);
            panelRect.anchoredPosition = Vector2.zero;
            panelRect.sizeDelta = new Vector2(0, 100);

            var panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0.08f, 0.08f, 0.12f, 0.95f);

            // Canvas 추가 (가시성 제어용)
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 뒤로가기 버튼 (좌측) - Y=0은 앵커(수직중앙) 기준
            var backBtn = CreateHeaderButton(panel.transform, "BackButton", "<",
                new Vector2(50, 0), new Vector2(70, 50), new Color(0.3f, 0.3f, 0.4f, 1f));

            // 타이틀 텍스트 (중앙)
            var titleText = CreateHeaderTMPText(panel.transform, "TitleText", "",
                new Vector2(0, 0), new Vector2(400, 50), 28, TextAlignmentOptions.Center);

            // 우측 버튼들 (공지, 메일, 메뉴, 프로필 순서 - 오른쪽에서 왼쪽으로)
            var profileBtn = CreateHeaderButton(panel.transform, "ProfileButton", "P",
                new Vector2(-40, 0), new Vector2(50, 50), new Color(0.3f, 0.5f, 0.6f, 1f), true);

            var menuBtn = CreateHeaderButton(panel.transform, "MenuButton", "≡",
                new Vector2(-100, 0), new Vector2(50, 50), new Color(0.4f, 0.4f, 0.5f, 1f), true);

            var mailBtn = CreateHeaderButton(panel.transform, "MailButton", "✉",
                new Vector2(-160, 0), new Vector2(50, 50), new Color(0.5f, 0.4f, 0.3f, 1f), true);

            var noticeBtn = CreateHeaderButton(panel.transform, "NoticeButton", "!",
                new Vector2(-220, 0), new Vector2(50, 50), new Color(0.6f, 0.3f, 0.3f, 1f), true);

            // CurrencyHUD 프리팹 로드 또는 생성
            var currencyHudPrefab = CreateCurrencyHUDPrefab();

            // CurrencyHUD 인스턴스 배치 (타이틀 우측)
            GameObject currencyHudInstance = null;
            if (currencyHudPrefab != null)
            {
                currencyHudInstance = Object.Instantiate(currencyHudPrefab);
                currencyHudInstance.name = "CurrencyHUD";
                currencyHudInstance.transform.SetParent(panel.transform, false);

                var hudRect = currencyHudInstance.GetComponent<RectTransform>();
                hudRect.anchorMin = new Vector2(0.5f, 0.5f);
                hudRect.anchorMax = new Vector2(0.5f, 0.5f);
                hudRect.anchoredPosition = new Vector2(250, 0);
                hudRect.localScale = new Vector3(0.8f, 0.8f, 1f);  // 약간 축소
            }

            // ScreenHeader 컴포넌트 추가
            var screenHeader = panel.AddComponent<Common.UI.Widgets.ScreenHeader>();

            var so = new SerializedObject(screenHeader);
            so.FindProperty("_titleText").objectReferenceValue = titleText;
            so.FindProperty("_backButton").objectReferenceValue = backBtn;
            so.FindProperty("_profileButton").objectReferenceValue = profileBtn;
            so.FindProperty("_menuButton").objectReferenceValue = menuBtn;
            so.FindProperty("_mailButton").objectReferenceValue = mailBtn;
            so.FindProperty("_noticeButton").objectReferenceValue = noticeBtn;

            if (currencyHudInstance != null)
            {
                so.FindProperty("_currencyHUD").objectReferenceValue =
                    currencyHudInstance.GetComponent<Common.UI.Widgets.CurrencyHUD>();
            }

            // Database 에셋 로드 및 할당
            var configDb = AssetDatabase.LoadAssetAtPath<Data.ScreenHeaderConfigDatabase>(
                "Assets/Data/Generated/ScreenHeaderConfigDatabase.asset");
            if (configDb != null)
            {
                so.FindProperty("_configDatabase").objectReferenceValue = configDb;
            }
            else
            {
                Debug.LogWarning("[MVPSceneSetup] ScreenHeaderConfigDatabase not found. Run SC/Data/Master Data Generator first.");
            }

            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        private static Button CreateHeaderButton(Transform parent, string name, string label,
            Vector2 position, Vector2 size, Color bgColor, bool rightAnchor = false)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();

            if (rightAnchor)
            {
                // 우측 앵커 (우측 가장자리, 수직 중앙)
                rect.anchorMin = new Vector2(1, 0.5f);
                rect.anchorMax = new Vector2(1, 0.5f);
            }
            else
            {
                // 좌측 앵커 (좌측 가장자리, 수직 중앙)
                rect.anchorMin = new Vector2(0, 0.5f);
                rect.anchorMax = new Vector2(0, 0.5f);
            }

            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var image = go.AddComponent<Image>();
            image.color = bgColor;

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = bgColor * 1.2f;
            colors.pressedColor = bgColor * 0.8f;
            button.colors = colors;

            // Label
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);

            var labelRect = labelGo.AddComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            var labelText = labelGo.AddComponent<TextMeshProUGUI>();
            labelText.text = label;
            labelText.fontSize = 24;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.color = Color.white;

            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                labelText.font = defaultFont;
            }

            return button;
        }

        /// <summary>
        /// Header 전용 텍스트 생성 (수직 중앙 앵커)
        /// </summary>
        private static TMP_Text CreateHeaderTMPText(Transform parent, string name, string content,
            Vector2 position, Vector2 size, int fontSize, TextAlignmentOptions alignment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            // 수평 중앙, 수직 중앙 앵커
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var text = go.AddComponent<TextMeshProUGUI>();
            text.text = content;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = Color.white;

            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                text.font = defaultFont;
            }

            return text;
        }

        #endregion

        #region GachaResultPopup Prefab

        private static GameObject CreateGachaResultPopupPrefab()
        {
            var prefabPath = $"{PrefabPath}/GachaResultPopup.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            // 배경 딤
            var panel = CreateCenteredPanel("GachaResultPopup", new Vector2(700, 800),
                new Color(0.1f, 0.1f, 0.15f, 0.98f));

            // Canvas 추가
            panel.AddComponent<Canvas>();
            panel.AddComponent<GraphicRaycaster>();

            // 타이틀
            var titleText = CreateTMPText(panel.transform, "TitleText", "소환 결과",
                new Vector2(0, 350), new Vector2(400, 60), 36, TextAlignmentOptions.Center);

            // 결과 스크롤 뷰
            var scrollView = CreateScrollView(panel.transform, "ResultScrollView",
                new Vector2(0, 50), new Vector2(650, 500));

            var resultContainer = scrollView.transform.Find("Viewport/Content");

            // 확인 버튼
            var confirmBtn = CreateTMPButton(panel.transform, "ConfirmButton", "확인",
                new Vector2(-100, -330), new Vector2(180, 60), new Color(0.3f, 0.6f, 0.3f, 1f));

            // 다시 뽑기 버튼
            var retryBtn = CreateTMPButton(panel.transform, "RetryButton", "다시 뽑기",
                new Vector2(100, -330), new Vector2(180, 60), new Color(0.6f, 0.4f, 0.2f, 1f));

            // GachaResultItem 프리팹 로드 또는 생성
            var resultItemPrefab = CreateGachaResultItemPrefab();

            // GachaResultPopup 컴포넌트 추가
            var popup = panel.AddComponent<Contents.Gacha.GachaResultPopup>();

            var so = new SerializedObject(popup);
            so.FindProperty("_resultContainer").objectReferenceValue = resultContainer;
            so.FindProperty("_resultItemPrefab").objectReferenceValue = resultItemPrefab;
            so.FindProperty("_confirmButton").objectReferenceValue = confirmBtn;
            so.FindProperty("_retryButton").objectReferenceValue = retryBtn;
            so.FindProperty("_titleText").objectReferenceValue = titleText;
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region GachaResultItem Prefab

        private static GameObject CreateGachaResultItemPrefab()
        {
            var prefabPath = $"{PrefabPath}/GachaResultItem.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var item = new GameObject("GachaResultItem");
            var rect = item.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(600, 60);

            // LayoutElement 추가 (VerticalLayoutGroup에서 높이 인식용)
            var layoutElement = item.AddComponent<LayoutElement>();
            layoutElement.minHeight = 60;
            layoutElement.preferredHeight = 60;

            var bg = item.AddComponent<Image>();
            bg.color = new Color(0.2f, 0.2f, 0.3f, 0.8f);

            // 이름 텍스트 (stretch anchor)
            var nameTextGo = new GameObject("NameText");
            nameTextGo.transform.SetParent(item.transform, false);

            var nameRect = nameTextGo.AddComponent<RectTransform>();
            nameRect.anchorMin = Vector2.zero;
            nameRect.anchorMax = Vector2.one;
            nameRect.offsetMin = new Vector2(15, 5);
            nameRect.offsetMax = new Vector2(-15, -5);

            var nameText = nameTextGo.AddComponent<TextMeshProUGUI>();
            nameText.text = "[SSR] 캐릭터 이름";
            nameText.fontSize = 22;
            nameText.alignment = TextAlignmentOptions.MidlineLeft;
            nameText.color = Color.white;
            nameText.richText = true;  // Rich Text 활성화 (색상 태그용)

            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                nameText.font = defaultFont;
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(item, prefabPath);
            Object.DestroyImmediate(item);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region CurrencyHUD Prefab

        private static GameObject CreateCurrencyHUDPrefab()
        {
            var prefabPath = $"{PrefabPath}/CurrencyHUD.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            // HUD 패널 (상단 우측)
            var panel = new GameObject("CurrencyHUD");
            var rect = panel.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(400, 80);

            var bg = panel.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.5f);

            // Gold 영역
            var goldArea = CreateCurrencyArea(panel.transform, "GoldArea", "Gold", new Vector2(-100, 0));

            // Gem 영역
            var gemArea = CreateCurrencyArea(panel.transform, "GemArea", "Gem", new Vector2(100, 0));

            // CurrencyHUD 컴포넌트 추가
            var hud = panel.AddComponent<Common.UI.Widgets.CurrencyHUD>();

            var so = new SerializedObject(hud);
            so.FindProperty("_goldText").objectReferenceValue = goldArea.transform.Find("ValueText").GetComponent<TMP_Text>();
            so.FindProperty("_goldAddButton").objectReferenceValue = goldArea.transform.Find("AddButton").GetComponent<Button>();
            so.FindProperty("_gemText").objectReferenceValue = gemArea.transform.Find("ValueText").GetComponent<TMP_Text>();
            so.FindProperty("_gemAddButton").objectReferenceValue = gemArea.transform.Find("AddButton").GetComponent<Button>();
            so.ApplyModifiedPropertiesWithoutUndo();

            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        private static GameObject CreateCurrencyArea(Transform parent, string name, string label, Vector2 position)
        {
            var area = new GameObject(name);
            area.transform.SetParent(parent, false);

            var rect = area.AddComponent<RectTransform>();
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(180, 60);

            // 라벨
            CreateTMPText(area.transform, "Label", label,
                new Vector2(-50, 0), new Vector2(60, 30), 16, TextAlignmentOptions.Left);

            // 값
            CreateTMPText(area.transform, "ValueText", "0",
                new Vector2(20, 0), new Vector2(80, 30), 24, TextAlignmentOptions.Right);

            // + 버튼
            var addBtn = CreateTMPButton(area.transform, "AddButton", "+",
                new Vector2(70, 0), new Vector2(40, 40), new Color(0.3f, 0.5f, 0.3f, 1f));

            return area;
        }

        #endregion

        #region CharacterListItem Prefab

        private static GameObject CreateCharacterListItemPrefab()
        {
            var prefabPath = $"{PrefabPath}/CharacterListItem.prefab";

            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null) return existingPrefab;

            var item = new GameObject("CharacterListItem");
            var rect = item.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(750, 80);

            // LayoutElement 추가 (VerticalLayoutGroup에서 높이 인식용)
            var layoutElement = item.AddComponent<LayoutElement>();
            layoutElement.minHeight = 80;
            layoutElement.preferredHeight = 80;

            var bg = item.AddComponent<Image>();
            bg.color = new Color(0.2f, 0.2f, 0.3f, 0.8f);

            var btn = item.AddComponent<Button>();
            var colors = btn.colors;
            colors.highlightedColor = new Color(0.3f, 0.3f, 0.4f, 1f);
            btn.colors = colors;

            // 이름 텍스트 (stretch anchor로 변경)
            var nameTextGo = new GameObject("NameText");
            nameTextGo.transform.SetParent(item.transform, false);

            var nameRect = nameTextGo.AddComponent<RectTransform>();
            nameRect.anchorMin = Vector2.zero;
            nameRect.anchorMax = Vector2.one;
            nameRect.offsetMin = new Vector2(20, 10);  // left, bottom padding
            nameRect.offsetMax = new Vector2(-20, -10); // right, top padding

            var nameText = nameTextGo.AddComponent<TextMeshProUGUI>();
            nameText.text = "[SSR] 캐릭터 이름 Lv.1";
            nameText.fontSize = 24;
            nameText.alignment = TextAlignmentOptions.MidlineLeft;
            nameText.color = Color.white;

            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                nameText.font = defaultFont;
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(item, prefabPath);
            Object.DestroyImmediate(item);

            Debug.Log($"[MVPSceneSetup] Created: {prefabPath}");
            return prefab;
        }

        #endregion

        #region UI Helpers

        private static GameObject CreateFullScreenPanel(string name, Color bgColor)
        {
            var panel = new GameObject(name);
            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            var image = panel.AddComponent<Image>();
            image.color = bgColor;

            return panel;
        }

        private static GameObject CreateCenteredPanel(string name, Vector2 size, Color bgColor)
        {
            var panel = new GameObject(name);
            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = size;

            var image = panel.AddComponent<Image>();
            image.color = bgColor;

            return panel;
        }

        private static TMP_Text CreateTMPText(Transform parent, string name, string content,
            Vector2 position, Vector2 size, int fontSize, TextAlignmentOptions alignment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var text = go.AddComponent<TextMeshProUGUI>();
            text.text = content;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = Color.white;

            // 기본 폰트 적용
            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                text.font = defaultFont;
            }

            return text;
        }

        private static Button CreateTMPButton(Transform parent, string name, string label,
            Vector2 position, Vector2 size, Color bgColor)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var image = go.AddComponent<Image>();
            image.color = bgColor;

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = bgColor * 1.2f;
            colors.pressedColor = bgColor * 0.8f;
            button.colors = colors;

            // Label
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);

            var labelRect = labelGo.AddComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            var labelText = labelGo.AddComponent<TextMeshProUGUI>();
            labelText.text = label;
            labelText.fontSize = 24;
            labelText.alignment = TextAlignmentOptions.Center;
            labelText.color = Color.white;

            // 기본 폰트 적용
            var defaultFont = ProjectEditorSettings.Instance.DefaultFont;
            if (defaultFont != null)
            {
                labelText.font = defaultFont;
            }

            return button;
        }

        private static Button CreateFullScreenButton(Transform parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;

            var image = go.AddComponent<Image>();
            image.color = new Color(0, 0, 0, 0); // 투명

            var button = go.AddComponent<Button>();

            return button;
        }

        private static GameObject CreateScrollView(Transform parent, string name, Vector2 position, Vector2 size)
        {
            var scrollView = new GameObject(name);
            scrollView.transform.SetParent(parent, false);

            var rect = scrollView.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var scrollRect = scrollView.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;

            var scrollImage = scrollView.AddComponent<Image>();
            scrollImage.color = new Color(0, 0, 0, 0.3f);

            // Viewport - RectMask2D 사용 (Image+Mask보다 효율적)
            var viewport = new GameObject("Viewport");
            viewport.transform.SetParent(scrollView.transform, false);

            var vpRect = viewport.AddComponent<RectTransform>();
            vpRect.anchorMin = Vector2.zero;
            vpRect.anchorMax = Vector2.one;
            vpRect.offsetMin = Vector2.zero;  // stretch anchor 필수 설정
            vpRect.offsetMax = Vector2.zero;
            vpRect.pivot = new Vector2(0.5f, 0.5f);

            viewport.AddComponent<RectMask2D>();  // Mask 대신 RectMask2D (Image 불필요)

            // Content
            var content = new GameObject("Content");
            content.transform.SetParent(viewport.transform, false);

            var contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.anchoredPosition = Vector2.zero;  // 위치 초기화
            contentRect.offsetMin = new Vector2(0, contentRect.offsetMin.y);  // left = 0
            contentRect.offsetMax = new Vector2(0, contentRect.offsetMax.y);  // right = 0
            contentRect.sizeDelta = new Vector2(0, 0);

            var layout = content.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var fitter = content.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.viewport = vpRect;
            scrollRect.content = contentRect;

            return scrollView;
        }

        #endregion

        #region Prefab Instantiation

        private static void InstantiateScreenPrefab(GameObject prefab, RectTransform container, bool active)
        {
            if (prefab == null) return;

            // 이미 씬에 있는지 확인
            var existingName = prefab.name;
            var existing = container.Find(existingName);
            if (existing != null)
            {
                existing.gameObject.SetActive(active);
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
            if (instance != null)
            {
                var rect = instance.GetComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                rect.anchoredPosition = Vector2.zero;

                // Canvas.enabled로 가시성 제어
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = active;
                }

                instance.SetActive(true);  // GameObject는 항상 활성화
            }
        }

        private static void InstantiatePopupPrefab(GameObject prefab, RectTransform container, bool active)
        {
            if (prefab == null) return;

            // 이미 씬에 있는지 확인
            var existingName = prefab.name;
            var existing = container.Find(existingName);
            if (existing != null)
            {
                existing.gameObject.SetActive(active);
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
            if (instance != null)
            {
                var rect = instance.GetComponent<RectTransform>();
                // Popup은 중앙 정렬 유지
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);

                // Canvas.enabled로 가시성 제어
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = active;
                }

                instance.SetActive(true);  // GameObject는 항상 활성화
            }
        }

        private static void InstantiateHeaderPrefab(GameObject prefab, RectTransform container)
        {
            if (prefab == null) return;

            // 이미 씬에 있는지 확인
            var existingName = prefab.name;
            var existing = container.Find(existingName);
            if (existing != null)
            {
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
            if (instance != null)
            {
                var rect = instance.GetComponent<RectTransform>();
                // Header는 상단 고정
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(0.5f, 1);
                rect.anchoredPosition = Vector2.zero;

                // Canvas.enabled로 가시성 제어 (기본 숨김 - TitleScreen에서는 Header 사용 안 함)
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = false;
                }

                instance.SetActive(true);
            }
        }

        #endregion

        #region Utility

        private static void EnsureFolders()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/UI"))
                AssetDatabase.CreateFolder("Assets/Prefabs", "UI");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/UI/MVP"))
                AssetDatabase.CreateFolder("Assets/Prefabs/UI", "MVP");
        }

        #endregion
    }
}
