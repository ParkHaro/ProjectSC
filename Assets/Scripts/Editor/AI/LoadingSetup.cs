using Sc.Common.UI;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sc.Editor.AI
{
    /// <summary>
    /// LoadingWidget 프리팹 및 테스트 씬 자동 생성 도구
    /// </summary>
    public static class LoadingSetup
    {
        private const string PrefabPath = "Assets/Prefabs/UI";
        private const string LoadingPrefabName = "LoadingWidget.prefab";

        #region Menu Items

        [MenuItem("SC Tools/Loading/Create LoadingWidget Prefab", priority = 100)]
        public static void CreateLoadingWidgetPrefab()
        {
            EnsureFolders();

            var prefab = CreateLoadingWidget();
            if (prefab != null)
            {
                Selection.activeObject = prefab;
                Debug.Log($"[LoadingSetup] LoadingWidget prefab created: {PrefabPath}/{LoadingPrefabName}");
            }
        }

        [MenuItem("SC Tools/Loading/Setup Loading Test Scene", priority = 101)]
        public static void SetupLoadingTestScene()
        {
            EnsureFolders();

            // 1. EventSystem
            CreateEventSystem();

            // 2. LoadingWidget 프리팹 생성 또는 로드
            var loadingPrefab = CreateLoadingWidget();

            // 3. 씬에 LoadingWidget 인스턴스 추가
            InstantiateLoadingWidget(loadingPrefab);

            // 4. 테스트 컨트롤 패널 생성
            CreateTestControlPanel();

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("[LoadingSetup] Loading test scene setup complete!");
        }

        [MenuItem("SC Tools/Loading/Clear Loading Test Objects", priority = 200)]
        public static void ClearLoadingTestObjects()
        {
            var loadingWidget = GameObject.Find("LoadingWidget");
            if (loadingWidget != null) Object.DestroyImmediate(loadingWidget);

            var testPanel = GameObject.Find("LoadingTestPanel");
            if (testPanel != null) Object.DestroyImmediate(testPanel);

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log("[LoadingSetup] Loading test objects cleared!");
        }

        #endregion

        #region LoadingWidget Prefab Creation

        private static GameObject CreateLoadingWidget()
        {
            var prefabPath = $"{PrefabPath}/{LoadingPrefabName}";

            // 이미 존재하면 로드
            var existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (existingPrefab != null)
            {
                Debug.Log($"[LoadingSetup] Using existing prefab: {prefabPath}");
                return existingPrefab;
            }

            // Root Canvas 생성
            var root = new GameObject("LoadingWidget");
            var canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            var scaler = root.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            root.AddComponent<GraphicRaycaster>();
            root.AddComponent<CanvasGroup>();

            // FullScreenPanel 생성
            var fullScreenPanel = CreateFullScreenPanel(root.transform);

            // IndicatorPanel 생성
            var indicatorPanel = CreateIndicatorPanel(root.transform);

            // ProgressPanel 생성
            var progressPanel = CreateProgressPanel(root.transform);

            // LoadingWidget 컴포넌트 추가 및 SerializedField 할당
            var widget = root.AddComponent<LoadingWidget>();
            AssignWidgetFields(widget, fullScreenPanel, indicatorPanel, progressPanel);

            // 프리팹 저장
            var prefab = PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);

            Debug.Log($"[LoadingSetup] Created: {prefabPath}");
            return prefab;
        }

        private static GameObject CreateFullScreenPanel(Transform parent)
        {
            var panel = new GameObject("FullScreenPanel");
            panel.transform.SetParent(parent, false);

            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;

            // Background (어두운 오버레이)
            var bg = new GameObject("Background");
            bg.transform.SetParent(panel.transform, false);
            var bgRect = bg.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            var bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.8f);
            bgImage.raycastTarget = true; // 입력 차단

            // Spinner (중앙)
            var spinner = CreateSpinner(panel.transform, "Spinner", 100);

            // Message (스피너 아래)
            var message = CreateTMPText(panel.transform, "Message", "Loading...",
                new Vector2(0, -80), new Vector2(400, 50), 24);

            panel.SetActive(false);
            return panel;
        }

        private static GameObject CreateIndicatorPanel(Transform parent)
        {
            var panel = new GameObject("IndicatorPanel");
            panel.transform.SetParent(parent, false);

            var rect = panel.AddComponent<RectTransform>();
            // 우측 상단에 배치
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-30, -30);
            rect.sizeDelta = new Vector2(60, 60);

            // Spinner (작은 크기)
            var spinner = CreateSpinner(panel.transform, "Spinner", 50);
            var spinnerRect = spinner.GetComponent<RectTransform>();
            spinnerRect.anchoredPosition = Vector2.zero;

            panel.SetActive(false);
            return panel;
        }

        private static GameObject CreateProgressPanel(Transform parent)
        {
            var panel = new GameObject("ProgressPanel");
            panel.transform.SetParent(parent, false);

            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;

            // Background
            var bg = new GameObject("Background");
            bg.transform.SetParent(panel.transform, false);
            var bgRect = bg.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            var bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.8f);
            bgImage.raycastTarget = true;

            // Spinner
            CreateSpinner(panel.transform, "Spinner", 80);

            // Message (스피너 아래)
            CreateTMPText(panel.transform, "Message", "Downloading...",
                new Vector2(0, -70), new Vector2(400, 40), 20);

            // ProgressBar (메시지 아래)
            var progressBar = CreateProgressBar(panel.transform);

            // PercentText (프로그레스 바 아래)
            CreateTMPText(panel.transform, "PercentText", "0%",
                new Vector2(0, -160), new Vector2(100, 30), 18);

            panel.SetActive(false);
            return panel;
        }

        private static GameObject CreateSpinner(Transform parent, string name, float size)
        {
            var spinner = new GameObject(name);
            spinner.transform.SetParent(parent, false);

            var rect = spinner.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(size, size);

            var image = spinner.AddComponent<Image>();
            // 플레이스홀더 스피너 색상
            image.color = new Color(0.3f, 0.6f, 1f, 1f);

            // 원형 스피너 효과를 위한 기본 설정
            // 실제 프로젝트에서는 스피너 스프라이트를 사용
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Radial360;
            image.fillAmount = 0.75f;

            return spinner;
        }

        private static TMP_Text CreateTMPText(Transform parent, string name, string text,
            Vector2 position, Vector2 size, int fontSize)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;

            return tmp;
        }

        private static Slider CreateProgressBar(Transform parent)
        {
            var sliderGo = new GameObject("ProgressBar");
            sliderGo.transform.SetParent(parent, false);

            var rect = sliderGo.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, -120);
            rect.sizeDelta = new Vector2(400, 20);

            // Background
            var bgGo = new GameObject("Background");
            bgGo.transform.SetParent(sliderGo.transform, false);
            var bgRect = bgGo.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            var bgImage = bgGo.AddComponent<Image>();
            bgImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);

            // Fill Area
            var fillArea = new GameObject("Fill Area");
            fillArea.transform.SetParent(sliderGo.transform, false);
            var fillAreaRect = fillArea.AddComponent<RectTransform>();
            fillAreaRect.anchorMin = Vector2.zero;
            fillAreaRect.anchorMax = Vector2.one;
            fillAreaRect.offsetMin = new Vector2(5, 5);
            fillAreaRect.offsetMax = new Vector2(-5, -5);

            // Fill
            var fill = new GameObject("Fill");
            fill.transform.SetParent(fillArea.transform, false);
            var fillRect = fill.AddComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.sizeDelta = Vector2.zero;
            var fillImage = fill.AddComponent<Image>();
            fillImage.color = new Color(0.3f, 0.6f, 1f, 1f);

            // Slider 컴포넌트
            var slider = sliderGo.AddComponent<Slider>();
            slider.fillRect = fillRect;
            slider.targetGraphic = bgImage;
            slider.direction = Slider.Direction.LeftToRight;
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 0f;
            slider.interactable = false; // 읽기 전용

            return slider;
        }

        private static void AssignWidgetFields(LoadingWidget widget,
            GameObject fullScreenPanel, GameObject indicatorPanel, GameObject progressPanel)
        {
            var so = new SerializedObject(widget);

            // FullScreen
            so.FindProperty("_fullScreenPanel").objectReferenceValue = fullScreenPanel;
            so.FindProperty("_fullScreenMessage").objectReferenceValue =
                fullScreenPanel.transform.Find("Message")?.GetComponent<TMP_Text>();
            so.FindProperty("_fullScreenSpinner").objectReferenceValue =
                fullScreenPanel.transform.Find("Spinner")?.GetComponent<Image>();

            // Indicator
            so.FindProperty("_indicatorPanel").objectReferenceValue = indicatorPanel;
            so.FindProperty("_indicatorSpinner").objectReferenceValue =
                indicatorPanel.transform.Find("Spinner")?.GetComponent<Image>();

            // Progress
            so.FindProperty("_progressPanel").objectReferenceValue = progressPanel;
            so.FindProperty("_progressMessage").objectReferenceValue =
                progressPanel.transform.Find("Message")?.GetComponent<TMP_Text>();
            so.FindProperty("_progressBar").objectReferenceValue =
                progressPanel.transform.Find("ProgressBar")?.GetComponent<Slider>();
            so.FindProperty("_progressPercent").objectReferenceValue =
                progressPanel.transform.Find("PercentText")?.GetComponent<TMP_Text>();

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        #endregion

        #region Test Scene Setup

        private static void CreateEventSystem()
        {
            var existing = Object.FindFirstObjectByType<EventSystem>();
            if (existing != null) return;

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private static void InstantiateLoadingWidget(GameObject prefab)
        {
            var existing = GameObject.Find("LoadingWidget");
            if (existing != null) return;

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.name = "LoadingWidget";
        }

        private static void CreateTestControlPanel()
        {
            var existing = GameObject.Find("LoadingTestPanel");
            if (existing != null) return;

            // Test Canvas 생성
            var canvasGo = new GameObject("LoadingTestPanel");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 50; // LoadingWidget(100) 아래

            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvasGo.AddComponent<GraphicRaycaster>();

            // Control Panel (좌하단)
            var panel = new GameObject("Panel");
            panel.transform.SetParent(canvasGo.transform, false);

            var panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(0, 0);
            panelRect.pivot = new Vector2(0, 0);
            panelRect.anchoredPosition = new Vector2(20, 20);
            panelRect.sizeDelta = new Vector2(220, 300);

            var panelBg = panel.AddComponent<Image>();
            panelBg.color = new Color(0, 0, 0, 0.7f);

            // Title
            CreateLegacyText(panel.transform, "Title", "Loading Test",
                new Vector2(10, -10), new Vector2(200, 30), 18);

            // Buttons
            float yPos = -50;
            float yStep = 45;

            CreateTestButton(panel.transform, "ShowIndicatorBtn", "Show Indicator",
                new Vector2(10, yPos), () => LoadingService.Instance.Show(LoadingType.Indicator));
            yPos -= yStep;

            CreateTestButton(panel.transform, "ShowFullScreenBtn", "Show FullScreen",
                new Vector2(10, yPos), () => LoadingService.Instance.Show(LoadingType.FullScreen, "Loading..."));
            yPos -= yStep;

            CreateTestButton(panel.transform, "ShowProgressBtn", "Show Progress",
                new Vector2(10, yPos), () => LoadingService.Instance.ShowProgress(0.5f, "Downloading..."));
            yPos -= yStep;

            CreateTestButton(panel.transform, "HideBtn", "Hide",
                new Vector2(10, yPos), () => LoadingService.Instance.Hide());
            yPos -= yStep;

            CreateTestButton(panel.transform, "ForceHideBtn", "Force Hide",
                new Vector2(10, yPos), () => LoadingService.Instance.ForceHide());

            // 테스트 컨트롤러 추가
            panel.AddComponent<Sc.Common.UI.Tests.LoadingTestController>();
        }

        private static void CreateLegacyText(Transform parent, string name, string content,
            Vector2 position, Vector2 size, int fontSize)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            var text = go.AddComponent<Text>();
            text.text = content;
            text.fontSize = fontSize;
            text.alignment = TextAnchor.MiddleLeft;
            text.color = Color.white;
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        private static Button CreateTestButton(Transform parent, string name, string label,
            Vector2 position, System.Action onClick)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(200, 40);

            var image = go.AddComponent<Image>();
            image.color = new Color(0.3f, 0.3f, 0.4f, 1f);

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.4f, 0.4f, 0.5f, 1f);
            colors.pressedColor = new Color(0.2f, 0.2f, 0.3f, 1f);
            button.colors = colors;

            // Label
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);
            var labelRect = labelGo.AddComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            var labelText = labelGo.AddComponent<Text>();
            labelText.text = label;
            labelText.fontSize = 14;
            labelText.alignment = TextAnchor.MiddleCenter;
            labelText.color = Color.white;
            labelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

            return button;
        }

        #endregion

        #region Utility

        private static void EnsureFolders()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/UI"))
                AssetDatabase.CreateFolder("Assets/Prefabs", "UI");
        }

        #endregion
    }
}
