using System.IO;
using Sc.Common.UI.Widgets;
using Sc.Data;
using Sc.Editor.Wizard.PrefabSync;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// Widget 프리팹 생성 도구 (ScreenHeader, CurrencyHUD 등).
    /// </summary>
    public static class WidgetPrefabGenerator
    {
        private const string MVP_PATH = "Assets/Prefabs/UI/MVP";
        private const float HEADER_HEIGHT = 100f;
        private const float BUTTON_SIZE = 60f;
        private const float CURRENCY_ITEM_WIDTH = 150f;

        #region Public API

        /// <summary>
        /// ScreenHeader 프리팹 생성 (CurrencyHUD 포함).
        /// </summary>
        public static bool GenerateScreenHeaderPrefab()
        {
            EnsureDirectoryExists(MVP_PATH);

            var prefabPath = $"{MVP_PATH}/ScreenHeader.prefab";

            // 이미 존재하면 삭제
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                AssetDatabase.DeleteAsset(prefabPath);
            }

            // GameObject 구조 생성
            var go = CreateScreenHeaderGameObject();

            // 프리팹 저장 및 SerializeField 바인딩 (Prefab-First Binding)
            var prefab = PrefabFieldBinder.SavePrefabWithBindings(go, prefabPath, BindScreenHeaderFields);

            if (prefab != null)
            {
                Debug.Log($"[WidgetPrefabGenerator] ScreenHeader 프리팹 생성: {prefabPath}");
                AssetDatabase.Refresh();
                return true;
            }

            Debug.LogError("[WidgetPrefabGenerator] ScreenHeader 프리팹 생성 실패");
            return false;
        }

        /// <summary>
        /// 모든 Widget 프리팹 생성.
        /// </summary>
        public static int GenerateAllWidgetPrefabs()
        {
            var count = 0;

            if (GenerateScreenHeaderPrefab())
                count++;

            return count;
        }

        #endregion

        #region ScreenHeader Creation

        private static GameObject CreateScreenHeaderGameObject()
        {
            // Root: ScreenHeader
            var root = new GameObject("ScreenHeader");
            var rootRect = root.AddComponent<RectTransform>();
            SetupRectTransform(rootRect, AnchorPreset.TopStretch, new Vector2(0, HEADER_HEIGHT));

            // Canvas for visibility control
            var canvas = root.AddComponent<Canvas>();
            canvas.overrideSorting = false;

            root.AddComponent<CanvasGroup>();
            root.AddComponent<GraphicRaycaster>();

            // ScreenHeader 컴포넌트
            root.AddComponent<ScreenHeader>();

            // Background
            var bg = CreateChild(root.transform, "Background");
            var bgRect = bg.AddComponent<RectTransform>();
            SetupRectTransform(bgRect, AnchorPreset.Stretch);
            var bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.15f, 0.95f);

            // Left Buttons Area
            var leftArea = CreateLayoutGroup(root.transform, "LeftButtons", true, TextAnchor.MiddleLeft);
            var leftRect = leftArea.GetComponent<RectTransform>();
            SetupRectTransform(leftRect, AnchorPreset.MiddleLeft, new Vector2(200, HEADER_HEIGHT));
            leftRect.anchoredPosition = new Vector2(20, 0);

            // Back Button
            CreateButton(leftArea.transform, "BackButton", "<");

            // Profile Button
            CreateButton(leftArea.transform, "ProfileButton", "P");

            // Center Title
            var titleGo = CreateChild(root.transform, "Title");
            var titleRect = titleGo.AddComponent<RectTransform>();
            SetupRectTransform(titleRect, AnchorPreset.MiddleCenter, new Vector2(400, 50));
            var titleText = titleGo.AddComponent<TextMeshProUGUI>();
            titleText.text = "Title";
            titleText.fontSize = 36;
            titleText.fontStyle = FontStyles.Bold;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;

            // Right Area
            var rightArea = CreateLayoutGroup(root.transform, "RightArea", true, TextAnchor.MiddleRight);
            var rightRect = rightArea.GetComponent<RectTransform>();
            SetupRectTransform(rightRect, AnchorPreset.MiddleRight, new Vector2(500, HEADER_HEIGHT));
            rightRect.anchoredPosition = new Vector2(-20, 0);

            // CurrencyHUD
            CreateCurrencyHUD(rightArea.transform);

            // Menu Button
            CreateButton(rightArea.transform, "MenuButton", "M");

            // Mail Button
            CreateButton(rightArea.transform, "MailButton", "@");

            // Notice Button
            CreateButton(rightArea.transform, "NoticeButton", "!");

            return root;
        }

        /// <summary>
        /// 프리팹에 ScreenHeader의 SerializeField 바인딩.
        /// </summary>
        private static void BindScreenHeaderFields(GameObject prefab)
        {
            var screenHeader = prefab.GetComponent<ScreenHeader>();
            if (screenHeader == null) return;

            // Database 연결 시도
            var configDb = AssetDatabase.LoadAssetAtPath<ScreenHeaderConfigDatabase>(
                "Assets/Data/Generated/ScreenHeaderConfigDatabase.asset");

            // ScreenHeader 필드 바인딩
            PrefabFieldBinder.BindFields(screenHeader,
                ("_backButton", prefab.transform.Find("LeftButtons/BackButton")?.GetComponent<Button>()),
                ("_profileButton", prefab.transform.Find("LeftButtons/ProfileButton")?.GetComponent<Button>()),
                ("_titleText", prefab.transform.Find("Title")?.GetComponent<TMP_Text>()),
                ("_currencyHUD", prefab.transform.Find("RightArea/CurrencyHUD")?.GetComponent<CurrencyHUD>()),
                ("_menuButton", prefab.transform.Find("RightArea/MenuButton")?.GetComponent<Button>()),
                ("_mailButton", prefab.transform.Find("RightArea/MailButton")?.GetComponent<Button>()),
                ("_noticeButton", prefab.transform.Find("RightArea/NoticeButton")?.GetComponent<Button>()),
                ("_configDatabase", configDb)
            );

            // CurrencyHUD 필드 바인딩
            BindCurrencyHUDFields(prefab.transform.Find("RightArea/CurrencyHUD")?.gameObject);
        }

        /// <summary>
        /// CurrencyHUD의 SerializeField 바인딩.
        /// </summary>
        private static void BindCurrencyHUDFields(GameObject currencyHudGo)
        {
            if (currencyHudGo == null) return;

            var currencyHud = currencyHudGo.GetComponent<CurrencyHUD>();
            if (currencyHud == null) return;

            var goldArea = currencyHudGo.transform.Find("GoldArea");
            var gemArea = currencyHudGo.transform.Find("GemArea");

            PrefabFieldBinder.BindFields(currencyHud,
                ("_goldText", goldArea?.Find("Text")?.GetComponent<TMP_Text>()),
                ("_goldAddButton", goldArea?.Find("AddButton")?.GetComponent<Button>()),
                ("_gemText", gemArea?.Find("Text")?.GetComponent<TMP_Text>()),
                ("_gemAddButton", gemArea?.Find("AddButton")?.GetComponent<Button>())
            );
        }

        #endregion

        #region CurrencyHUD Creation

        private static GameObject CreateCurrencyHUD(Transform parent)
        {
            var root = new GameObject("CurrencyHUD");
            root.transform.SetParent(parent, false);

            var rootRect = root.AddComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(CURRENCY_ITEM_WIDTH * 2 + 20, HEADER_HEIGHT - 20);

            var layout = root.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.childControlWidth = true;
            layout.childControlHeight = true;

            root.AddComponent<Canvas>();
            root.AddComponent<CanvasGroup>();

            root.AddComponent<CurrencyHUD>();

            // Gold Area (구조만 생성, 바인딩은 BindCurrencyHUDFields에서)
            CreateCurrencyItem(root.transform, "GoldArea", "G", "0");

            // Gem Area
            CreateCurrencyItem(root.transform, "GemArea", "D", "0");

            return root;
        }

        private static GameObject CreateCurrencyItem(Transform parent, string name, string iconText,
            string defaultValue)
        {
            var root = new GameObject(name);
            root.transform.SetParent(parent, false);

            var rootRect = root.AddComponent<RectTransform>();
            rootRect.sizeDelta = new Vector2(CURRENCY_ITEM_WIDTH, 50);

            var layout = root.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.padding = new RectOffset(5, 5, 5, 5);

            // Background
            var bg = root.AddComponent<Image>();
            bg.color = new Color(0.2f, 0.2f, 0.25f, 0.8f);

            // Icon
            var iconGo = CreateChild(root.transform, "Icon");
            var iconRect = iconGo.AddComponent<RectTransform>();
            iconRect.sizeDelta = new Vector2(30, 30);
            var iconLayout = iconGo.AddComponent<LayoutElement>();
            iconLayout.preferredWidth = 30;
            iconLayout.preferredHeight = 30;
            var iconImage = iconGo.AddComponent<Image>();
            iconImage.color = Color.yellow;
            var iconTmp = iconGo.AddComponent<TextMeshProUGUI>();
            iconTmp.text = iconText;
            iconTmp.fontSize = 20;
            iconTmp.fontStyle = FontStyles.Bold;
            iconTmp.alignment = TextAlignmentOptions.Center;
            iconTmp.color = Color.black;

            // Text
            var textGo = CreateChild(root.transform, "Text");
            var textRect = textGo.AddComponent<RectTransform>();
            var textLayout = textGo.AddComponent<LayoutElement>();
            textLayout.flexibleWidth = 1;
            textLayout.preferredHeight = 30;
            var text = textGo.AddComponent<TextMeshProUGUI>();
            text.text = defaultValue;
            text.fontSize = 24;
            text.alignment = TextAlignmentOptions.Left;
            text.color = Color.white;

            // Add Button
            var addBtn = CreateChild(root.transform, "AddButton");
            var addRect = addBtn.AddComponent<RectTransform>();
            addRect.sizeDelta = new Vector2(30, 30);
            var addLayout = addBtn.AddComponent<LayoutElement>();
            addLayout.preferredWidth = 30;
            addLayout.preferredHeight = 30;
            var addImage = addBtn.AddComponent<Image>();
            addImage.color = new Color(0.3f, 0.7f, 0.3f, 1f);
            var addButton = addBtn.AddComponent<Button>();
            addButton.targetGraphic = addImage;
            var addText = addBtn.AddComponent<TextMeshProUGUI>();
            addText.text = "+";
            addText.fontSize = 24;
            addText.fontStyle = FontStyles.Bold;
            addText.alignment = TextAlignmentOptions.Center;
            addText.color = Color.white;

            return root;
        }

        #endregion

        #region Helpers

        private static GameObject CreateChild(Transform parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go;
        }

        private static GameObject CreateButton(Transform parent, string name, string text)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);

            var rect = go.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(BUTTON_SIZE, BUTTON_SIZE);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = BUTTON_SIZE;
            layoutElement.preferredHeight = BUTTON_SIZE;

            var image = go.AddComponent<Image>();
            image.color = new Color(0.3f, 0.3f, 0.35f, 1f);

            var button = go.AddComponent<Button>();
            button.targetGraphic = image;

            // Button Text
            var textGo = CreateChild(go.transform, "Text");
            var textRect = textGo.AddComponent<RectTransform>();
            SetupRectTransform(textRect, AnchorPreset.Stretch);
            var tmp = textGo.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 28;
            tmp.fontStyle = FontStyles.Bold;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;

            return go;
        }

        private static GameObject CreateLayoutGroup(Transform parent, string name, bool horizontal,
            TextAnchor alignment)
        {
            var go = CreateChild(parent, name);

            if (horizontal)
            {
                var layout = go.AddComponent<HorizontalLayoutGroup>();
                layout.spacing = 10;
                layout.childAlignment = alignment;
                layout.childForceExpandWidth = false;
                layout.childForceExpandHeight = false;
                layout.childControlWidth = false;
                layout.childControlHeight = false;
            }
            else
            {
                var layout = go.AddComponent<VerticalLayoutGroup>();
                layout.spacing = 10;
                layout.childAlignment = alignment;
                layout.childForceExpandWidth = false;
                layout.childForceExpandHeight = false;
                layout.childControlWidth = false;
                layout.childControlHeight = false;
            }

            return go;
        }

        private enum AnchorPreset
        {
            Stretch,
            TopStretch,
            MiddleLeft,
            MiddleCenter,
            MiddleRight
        }

        private static void SetupRectTransform(RectTransform rect, AnchorPreset preset, Vector2? sizeDelta = null)
        {
            switch (preset)
            {
                case AnchorPreset.Stretch:
                    rect.anchorMin = Vector2.zero;
                    rect.anchorMax = Vector2.one;
                    rect.sizeDelta = Vector2.zero;
                    rect.anchoredPosition = Vector2.zero;
                    break;
                case AnchorPreset.TopStretch:
                    rect.anchorMin = new Vector2(0, 1);
                    rect.anchorMax = new Vector2(1, 1);
                    rect.pivot = new Vector2(0.5f, 1);
                    rect.sizeDelta = sizeDelta ?? new Vector2(0, 100);
                    rect.anchoredPosition = Vector2.zero;
                    break;
                case AnchorPreset.MiddleLeft:
                    rect.anchorMin = new Vector2(0, 0.5f);
                    rect.anchorMax = new Vector2(0, 0.5f);
                    rect.pivot = new Vector2(0, 0.5f);
                    rect.sizeDelta = sizeDelta ?? new Vector2(100, 100);
                    break;
                case AnchorPreset.MiddleCenter:
                    rect.anchorMin = new Vector2(0.5f, 0.5f);
                    rect.anchorMax = new Vector2(0.5f, 0.5f);
                    rect.pivot = new Vector2(0.5f, 0.5f);
                    rect.sizeDelta = sizeDelta ?? new Vector2(100, 100);
                    rect.anchoredPosition = Vector2.zero;
                    break;
                case AnchorPreset.MiddleRight:
                    rect.anchorMin = new Vector2(1, 0.5f);
                    rect.anchorMax = new Vector2(1, 0.5f);
                    rect.pivot = new Vector2(1, 0.5f);
                    rect.sizeDelta = sizeDelta ?? new Vector2(100, 100);
                    break;
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
        }

        #endregion
    }
}