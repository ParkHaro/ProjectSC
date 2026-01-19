using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

namespace Sc.Editor.AI
{
    /// <summary>
    /// PlayMode 테스트용 프리팹 및 씬 자동 생성 도구.
    /// TestRunner PlayMode 테스트에서 사용할 테스트 프리팹 생성.
    /// </summary>
    public static class PlayModeTestSetup
    {
        private const string TestPrefabPath = "Assets/Prefabs/Tests";
        private const string TestAddressablePath = "Assets/AddressableResources/Tests";

        #region Menu Items

        [MenuItem("SC Tools/PlayMode Tests/Create All Test Prefabs", priority = 50)]
        public static void CreateAllTestPrefabs()
        {
            EnsureFolders();

            CreateSimpleTestScreenPrefab();
            CreateSimpleTestPopupPrefab();
            CreateTestSystemPopupPrefab();

            AssetDatabase.Refresh();
            Debug.Log("[PlayModeTestSetup] All test prefabs created!");
        }

        [MenuItem("SC Tools/PlayMode Tests/Create Simple Screen Prefab", priority = 100)]
        public static void CreateSimpleTestScreenPrefab()
        {
            EnsureFolders();

            var prefabPath = $"{TestPrefabPath}/SimpleTestScreen.prefab";

            // 이미 존재하면 스킵
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                Debug.Log($"[PlayModeTestSetup] Already exists: {prefabPath}");
                return;
            }

            // Root Panel
            var panel = CreateFullscreenPanel("SimpleTestScreen", new Color(0.15f, 0.15f, 0.25f, 0.95f));

            // CanvasGroup for visibility control
            panel.AddComponent<CanvasGroup>();

            // Title (TMP)
            CreateCenteredTMPText(panel.transform, "TitleText", "Test Screen",
                new Vector2(0, 200), new Vector2(600, 80), 48);

            // Info Text
            CreateCenteredTMPText(panel.transform, "InfoText", "Screen Info",
                new Vector2(0, 100), new Vector2(500, 50), 24);

            // Buttons
            CreateCenteredButton(panel.transform, "ActionButton", "Action",
                new Vector2(0, -50), new Vector2(200, 60));

            CreateCenteredButton(panel.transform, "BackButton", "Back",
                new Vector2(0, -130), new Vector2(200, 60));

            // Save prefab
            var prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            Object.DestroyImmediate(panel);

            Debug.Log($"[PlayModeTestSetup] Created: {prefabPath}");
        }

        [MenuItem("SC Tools/PlayMode Tests/Create Simple Popup Prefab", priority = 101)]
        public static void CreateSimpleTestPopupPrefab()
        {
            EnsureFolders();

            var prefabPath = $"{TestPrefabPath}/SimpleTestPopup.prefab";

            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                Debug.Log($"[PlayModeTestSetup] Already exists: {prefabPath}");
                return;
            }

            // Dimmed Background
            var root = new GameObject("SimpleTestPopup");
            var rootRect = root.AddComponent<RectTransform>();
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.sizeDelta = Vector2.zero;

            var dimBg = root.AddComponent<Image>();
            dimBg.color = new Color(0, 0, 0, 0.5f);

            root.AddComponent<CanvasGroup>();

            // Content Panel (centered)
            var content = new GameObject("Content");
            content.transform.SetParent(root.transform, false);

            var contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(500, 350);

            var contentBg = content.AddComponent<Image>();
            contentBg.color = new Color(0.2f, 0.2f, 0.3f, 1f);

            // Title
            CreateCenteredTMPText(content.transform, "TitleText", "Popup Title",
                new Vector2(0, 120), new Vector2(450, 50), 32);

            // Message
            CreateCenteredTMPText(content.transform, "MessageText", "Popup message content",
                new Vector2(0, 40), new Vector2(450, 80), 20);

            // Buttons
            CreateCenteredButton(content.transform, "ConfirmButton", "Confirm",
                new Vector2(-80, -100), new Vector2(140, 50));

            CreateCenteredButton(content.transform, "CancelButton", "Cancel",
                new Vector2(80, -100), new Vector2(140, 50));

            // Save prefab
            var prefab = PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);

            Debug.Log($"[PlayModeTestSetup] Created: {prefabPath}");
        }

        [MenuItem("SC Tools/PlayMode Tests/Create System Popup Test Prefab", priority = 102)]
        public static void CreateTestSystemPopupPrefab()
        {
            EnsureFolders();

            var prefabPath = $"{TestPrefabPath}/TestSystemPopup.prefab";

            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                Debug.Log($"[PlayModeTestSetup] Already exists: {prefabPath}");
                return;
            }

            // Root with dim background
            var root = new GameObject("TestSystemPopup");
            var rootRect = root.AddComponent<RectTransform>();
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.sizeDelta = Vector2.zero;

            // Background (for blocking)
            var background = new GameObject("Background");
            background.transform.SetParent(root.transform, false);
            var bgRect = background.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            var bgImage = background.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.6f);

            root.AddComponent<CanvasGroup>();

            // Content panel
            var content = new GameObject("Content");
            content.transform.SetParent(root.transform, false);

            var contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(600, 400);

            var contentBg = content.AddComponent<Image>();
            contentBg.color = new Color(0.18f, 0.18f, 0.28f, 1f);

            // Header
            var header = new GameObject("Header");
            header.transform.SetParent(content.transform, false);
            var headerRect = header.AddComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0, 1);
            headerRect.anchorMax = new Vector2(1, 1);
            headerRect.pivot = new Vector2(0.5f, 1);
            headerRect.anchoredPosition = Vector2.zero;
            headerRect.sizeDelta = new Vector2(0, 80);

            var headerBg = header.AddComponent<Image>();
            headerBg.color = new Color(0.25f, 0.25f, 0.4f, 1f);

            // Title in header
            CreateTMPText(header.transform, "TitleText", "System Message",
                new Vector2(0, 0), new Vector2(0, 80), 28, TextAnchor.MiddleCenter, true);

            // Body - Message
            CreateCenteredTMPText(content.transform, "MessageText", "This is a system popup message.",
                new Vector2(0, 20), new Vector2(550, 120), 22);

            // Button Container
            var buttonContainer = new GameObject("ButtonContainer");
            buttonContainer.transform.SetParent(content.transform, false);
            var bcRect = buttonContainer.AddComponent<RectTransform>();
            bcRect.anchorMin = new Vector2(0.5f, 0);
            bcRect.anchorMax = new Vector2(0.5f, 0);
            bcRect.pivot = new Vector2(0.5f, 0);
            bcRect.anchoredPosition = new Vector2(0, 30);
            bcRect.sizeDelta = new Vector2(400, 60);

            // HorizontalLayoutGroup
            var hlg = buttonContainer.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 20;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childForceExpandWidth = true;
            hlg.childForceExpandHeight = false;

            // Confirm Button
            CreateLayoutButton(buttonContainer.transform, "ConfirmButton", "Confirm", new Color(0.2f, 0.5f, 0.3f, 1f));

            // Cancel Button
            CreateLayoutButton(buttonContainer.transform, "CancelButton", "Cancel", new Color(0.5f, 0.3f, 0.3f, 1f));

            // Save prefab
            var prefab = PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            Object.DestroyImmediate(root);

            Debug.Log($"[PlayModeTestSetup] Created: {prefabPath}");
        }

        [MenuItem("SC Tools/PlayMode Tests/Verify Test Scene", priority = 200)]
        public static void VerifyTestScene()
        {
            var scenePath = "Assets/Scenes/Test/Main.unity";

            if (!System.IO.File.Exists(scenePath))
            {
                Debug.LogWarning($"[PlayModeTestSetup] Test scene not found: {scenePath}");
                return;
            }

            Debug.Log($"[PlayModeTestSetup] Test scene exists: {scenePath}");

            // 프리팹 존재 확인
            var prefabs = new[]
            {
                $"{TestPrefabPath}/SimpleTestScreen.prefab",
                $"{TestPrefabPath}/SimpleTestPopup.prefab",
                $"{TestPrefabPath}/TestSystemPopup.prefab"
            };

            foreach (var path in prefabs)
            {
                var exists = AssetDatabase.LoadAssetAtPath<GameObject>(path) != null;
                var status = exists ? "✓" : "✗";
                Debug.Log($"  {status} {path}");
            }
        }

        [MenuItem("SC Tools/PlayMode Tests/Delete All Test Prefabs", priority = 300)]
        public static void DeleteAllTestPrefabs()
        {
            if (!EditorUtility.DisplayDialog("Delete Test Prefabs",
                "Are you sure you want to delete all PlayMode test prefabs?",
                "Delete", "Cancel"))
            {
                return;
            }

            var prefabs = new[]
            {
                $"{TestPrefabPath}/SimpleTestScreen.prefab",
                $"{TestPrefabPath}/SimpleTestPopup.prefab",
                $"{TestPrefabPath}/TestSystemPopup.prefab"
            };

            foreach (var path in prefabs)
            {
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path) != null)
                {
                    AssetDatabase.DeleteAsset(path);
                    Debug.Log($"[PlayModeTestSetup] Deleted: {path}");
                }
            }

            AssetDatabase.Refresh();
        }

        #endregion

        #region UI Helpers

        private static GameObject CreateFullscreenPanel(string name, Color bgColor)
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

        private static TextMeshProUGUI CreateCenteredTMPText(Transform parent, string name, string content,
            Vector2 offset, Vector2 size, int fontSize)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = content;
            tmp.fontSize = fontSize;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;

            return tmp;
        }

        private static TextMeshProUGUI CreateTMPText(Transform parent, string name, string content,
            Vector2 offset, Vector2 size, int fontSize, TextAnchor alignment, bool stretch)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();

            if (stretch)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                rect.anchoredPosition = Vector2.zero;
            }
            else
            {
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = offset;
                rect.sizeDelta = size;
            }

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = content;
            tmp.fontSize = fontSize;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;

            return tmp;
        }

        private static Button CreateCenteredButton(Transform parent, string name, string label,
            Vector2 offset, Vector2 size)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;

            var image = go.AddComponent<Image>();
            image.color = new Color(0.3f, 0.3f, 0.45f, 1f);

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(0.4f, 0.4f, 0.55f, 1f);
            colors.pressedColor = new Color(0.2f, 0.2f, 0.35f, 1f);
            button.colors = colors;

            // Label (TMP)
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);
            var labelRect = labelGo.AddComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;
            labelRect.anchoredPosition = Vector2.zero;

            var labelTmp = labelGo.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 18;
            labelTmp.alignment = TextAlignmentOptions.Center;
            labelTmp.color = Color.white;

            return button;
        }

        private static Button CreateLayoutButton(Transform parent, string name, string label, Color bgColor)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            // Size will be controlled by layout group

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50;
            layoutElement.preferredHeight = 50;

            var image = go.AddComponent<Image>();
            image.color = bgColor;

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = bgColor * 1.2f;
            colors.pressedColor = bgColor * 0.8f;
            button.colors = colors;

            // Label (TMP)
            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);
            var labelRect = labelGo.AddComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;
            labelRect.anchoredPosition = Vector2.zero;

            var labelTmp = labelGo.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 20;
            labelTmp.alignment = TextAlignmentOptions.Center;
            labelTmp.color = Color.white;

            return button;
        }

        #endregion

        #region Utility

        private static void EnsureFolders()
        {
            // Ensure Prefabs/Tests folder
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/Tests"))
                AssetDatabase.CreateFolder("Assets/Prefabs", "Tests");
        }

        #endregion
    }
}
