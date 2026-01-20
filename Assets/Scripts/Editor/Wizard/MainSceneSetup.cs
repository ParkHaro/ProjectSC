using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.SceneManagement;
using Sc.Editor.AI;
using Sc.Common.UI;
using Sc.Core;
using Sc.Data;
using Object = UnityEngine.Object;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Main 씬 생성 도구.
    /// MVP 씬과 달리 프로덕션 구조로 설계:
    /// - 동적 프리팹 로딩 (Addressables)
    /// - 명확한 Canvas 레이어링
    /// - 초기화 시퀀스 통합
    /// </summary>
    public static class MainSceneSetup
    {
        private const string ScenePath = "Assets/Scenes/Main.unity";
        private const string PrefabBasePath = "Assets/Prefabs/UI";

        #region Menu Items

        [MenuItem("SC Tools/Setup/Scenes/Main/Setup Main Scene", priority = 200)]
        public static void SetupMainScene()
        {
            // 새 씬 생성 (현재 씬 저장 여부 확인)
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }

            // 빈 씬으로 시작
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // 1. 기본 오브젝트 생성
            CreateEventSystem();
            CreateCamera();

            // 2. Manager 오브젝트 생성
            CreateManagers();

            // 3. UI Canvas 계층 생성
            CreateUIHierarchy();

            // 4. NavigationManager에 Canvas 참조 할당
            AssignCanvasToNavigationManager();

            // 5. 씬 저장
            EditorUIHelpers.EnsureFolder("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);

            Debug.Log($"[MainSceneSetup] Main 씬 생성 완료: {ScenePath}");
        }

        [MenuItem("SC Tools/Setup/Scenes/Main/Clear Main Scene Objects", priority = 201)]
        public static void ClearMainSceneObjects()
        {
            // Manager 오브젝트들 삭제
            DestroyIfExists("Managers");
            DestroyIfExists("UIRoot");
            DestroyIfExists("Main Camera");
            DestroyIfExists("EventSystem");

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("[MainSceneSetup] Main 씬 오브젝트 정리 완료");
        }

        #endregion

        #region Scene Setup

        private static void CreateEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null) return;

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private static void CreateCamera()
        {
            var existing = GameObject.Find("Main Camera");
            if (existing != null) return;

            var cameraGo = new GameObject("Main Camera");
            cameraGo.tag = "MainCamera";

            var camera = cameraGo.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.05f, 0.05f, 0.1f, 1f);
            camera.orthographic = true;
            camera.orthographicSize = 5f;
            camera.depth = -1;
            camera.nearClipPlane = 0.3f;
            camera.farClipPlane = 1000f;

            cameraGo.AddComponent<AudioListener>();
        }

        private static void CreateManagers()
        {
            var existing = GameObject.Find("Managers");
            if (existing != null) return;

            // Managers 부모 오브젝트
            var managersGo = new GameObject("Managers");

            // NavigationManager
            var navGo = new GameObject("NavigationManager");
            navGo.transform.SetParent(managersGo.transform);
            navGo.AddComponent<NavigationManager>();

            // DataManager
            var dataGo = new GameObject("DataManager");
            dataGo.transform.SetParent(managersGo.transform);
            var dataManager = dataGo.AddComponent<DataManager>();
            AssignDatabases(dataManager);

            // NetworkManager
            var netGo = new GameObject("NetworkManager");
            netGo.transform.SetParent(managersGo.transform);
            netGo.AddComponent<NetworkManager>();

            // AssetManager (선택적 - 런타임에 생성될 수도 있음)
            var assetGo = new GameObject("AssetManager");
            assetGo.transform.SetParent(managersGo.transform);
            assetGo.AddComponent<AssetManager>();

            // LoadingService
            var loadingServiceGo = new GameObject("LoadingService");
            loadingServiceGo.transform.SetParent(managersGo.transform);
            loadingServiceGo.AddComponent<LoadingService>();

            // GameBootstrap
            var bootstrapGo = new GameObject("GameBootstrap");
            bootstrapGo.transform.SetParent(managersGo.transform);
            bootstrapGo.AddComponent<GameBootstrap>();

            // GameFlowController (동적 타입 로딩 - 순환 참조 방지)
            var flowGo = new GameObject("GameFlowController");
            flowGo.transform.SetParent(managersGo.transform);
            var flowControllerType = Type.GetType("Sc.Contents.Title.GameFlowController, Sc.Contents.Title");
            if (flowControllerType != null)
            {
                flowGo.AddComponent(flowControllerType);
            }
            else
            {
                Debug.LogWarning("[MainSceneSetup] GameFlowController 타입을 찾을 수 없습니다.");
            }
        }

        private static void AssignDatabases(DataManager dataManager)
        {
            if (dataManager == null) return;

            const string basePath = "Assets/Data/Generated/";

            var characterDb =
                AssetDatabase.LoadAssetAtPath<CharacterDatabase>(basePath + "CharacterDatabase.asset");
            var skillDb = AssetDatabase.LoadAssetAtPath<SkillDatabase>(basePath + "SkillDatabase.asset");
            var itemDb = AssetDatabase.LoadAssetAtPath<ItemDatabase>(basePath + "ItemDatabase.asset");
            var stageDb = AssetDatabase.LoadAssetAtPath<StageDatabase>(basePath + "StageDatabase.asset");
            var gachaPoolDb =
                AssetDatabase.LoadAssetAtPath<GachaPoolDatabase>(basePath + "GachaPoolDatabase.asset");

            var so = new SerializedObject(dataManager);
            so.FindProperty("_characterDatabase").objectReferenceValue = characterDb;
            so.FindProperty("_skillDatabase").objectReferenceValue = skillDb;
            so.FindProperty("_itemDatabase").objectReferenceValue = itemDb;
            so.FindProperty("_stageDatabase").objectReferenceValue = stageDb;
            so.FindProperty("_gachaPoolDatabase").objectReferenceValue = gachaPoolDb;
            so.ApplyModifiedPropertiesWithoutUndo();

            if (characterDb == null || skillDb == null || itemDb == null || stageDb == null || gachaPoolDb == null)
            {
                Debug.LogWarning("[MainSceneSetup] 일부 Database 에셋이 없습니다. Data 탭에서 Master Data를 생성하세요.");
            }
        }

        private static void CreateUIHierarchy()
        {
            var existing = GameObject.Find("UIRoot");
            if (existing != null) return;

            // UIRoot Canvas (최상위)
            var uiRootGo = new GameObject("UIRoot");
            var uiRootCanvas = uiRootGo.AddComponent<Canvas>();
            uiRootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            uiRootCanvas.sortingOrder = 0;

            var scaler = uiRootGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            uiRootGo.AddComponent<GraphicRaycaster>();

            // Screen Container (Sort: 10)
            CreateUIContainer(uiRootGo.transform, "ScreenContainer", 10);

            // Popup Container (Sort: 50)
            CreateUIContainer(uiRootGo.transform, "PopupContainer", 50);

            // Header Container (Sort: 80)
            var headerContainer = CreateUIContainer(uiRootGo.transform, "HeaderContainer", 80);

            // ScreenHeader 프리팹 인스턴스화
            InstantiateScreenHeader(headerContainer);

            // Loading Container (Sort: 100)
            var loadingContainer = CreateUIContainer(uiRootGo.transform, "LoadingContainer", 100);

            // LoadingWidget 프리팹 인스턴스화
            InstantiateLoadingWidget(loadingContainer);
        }

        private static RectTransform CreateUIContainer(Transform parent, string name, int sortingOrder)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            // Override Sorting
            var canvas = go.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = sortingOrder;

            go.AddComponent<GraphicRaycaster>();

            return rect;
        }

        private static void InstantiateScreenHeader(RectTransform container)
        {
            var prefabPath = $"{PrefabBasePath}/MVP/ScreenHeader.prefab";
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogWarning($"[MainSceneSetup] ScreenHeader 프리팹이 없습니다: {prefabPath}");
                return;
            }

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, container);
            if (instance != null)
            {
                var rect = instance.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(0.5f, 1);
                rect.anchoredPosition = Vector2.zero;

                // 초기에는 숨김 (TitleScreen에서는 Header 미사용)
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = false;
                }

                instance.SetActive(true);
            }
        }

        private static void InstantiateLoadingWidget(RectTransform container)
        {
            var prefabPath = $"{PrefabBasePath}/LoadingWidget.prefab";
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogWarning($"[MainSceneSetup] LoadingWidget 프리팹이 없습니다: {prefabPath}");

                // 기본 LoadingWidget 생성
                CreateDefaultLoadingWidget(container);
                return;
            }

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, container);
            if (instance != null)
            {
                var rect = instance.GetComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                rect.anchoredPosition = Vector2.zero;

                // 초기에는 숨김
                var canvas = instance.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = false;
                }

                instance.SetActive(true);
            }
        }

        private static void CreateDefaultLoadingWidget(RectTransform container)
        {
            var go = new GameObject("LoadingWidget");
            go.transform.SetParent(container, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            // 배경
            var bg = go.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.7f);

            // Canvas (가시성 제어용)
            var canvas = go.AddComponent<Canvas>();
            canvas.enabled = false; // 초기 숨김

            go.AddComponent<GraphicRaycaster>();

            // LoadingWidget 컴포넌트 추가
            go.AddComponent<LoadingWidget>();

            Debug.Log("[MainSceneSetup] 기본 LoadingWidget 생성됨 (프리팹 없음)");
        }

        private static void AssignCanvasToNavigationManager()
        {
            var navManager = Object.FindObjectOfType<NavigationManager>();
            if (navManager == null)
            {
                Debug.LogWarning("[MainSceneSetup] NavigationManager를 찾을 수 없습니다.");
                return;
            }

            var screenContainer = GameObject.Find("ScreenContainer");
            var popupContainer = GameObject.Find("PopupContainer");

            if (screenContainer == null || popupContainer == null)
            {
                Debug.LogWarning("[MainSceneSetup] ScreenContainer 또는 PopupContainer를 찾을 수 없습니다.");
                return;
            }

            var screenCanvas = screenContainer.GetComponent<Canvas>();
            var popupCanvas = popupContainer.GetComponent<Canvas>();

            var so = new SerializedObject(navManager);
            so.FindProperty("_screenCanvas").objectReferenceValue = screenCanvas;
            so.FindProperty("_popupCanvas").objectReferenceValue = popupCanvas;
            so.ApplyModifiedPropertiesWithoutUndo();

            Debug.Log("[MainSceneSetup] NavigationManager Canvas 참조 할당 완료");
        }

        #endregion

        #region Utility

        private static void DestroyIfExists(string name)
        {
            var go = GameObject.Find(name);
            if (go != null)
            {
                Object.DestroyImmediate(go);
            }
        }

        #endregion
    }
}