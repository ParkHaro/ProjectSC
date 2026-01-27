using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Inventory;
using Sc.Contents.Inventory.Widgets;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// InventoryScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/InventoryScreen.prefab
    /// Generated at: 2026-01-27 11:55:23
    /// </summary>
    public static class InventoryScreenPrefabBuilder_Generated
    {
        #region Theme Colors

        private static readonly Color BgDeep = new Color32(10, 10, 18, 255);
        private static readonly Color BgCard = new Color32(25, 25, 45, 217);
        private static readonly Color BgOverlay = new Color32(0, 0, 0, 200);
        private static readonly Color TextPrimary = Color.white;
        private static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);
        private static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.5f);
        private static readonly Color AccentPrimary = new Color32(100, 200, 255, 255);
        private static readonly Color AccentGold = new Color32(255, 215, 100, 255);
        private static readonly Color Transparent = Color.clear;

        // Extracted from prefab
        private static readonly Color Blue = new Color32(150, 200, 255, 255);
        private static readonly Color Color = new Color32(200, 230, 200, 255);
        private static readonly Color Green = new Color32(100, 180, 100, 255);

        #endregion

        #region Constants

        private const float ACTION_BUTTONS_HEIGHT = 100f;
        private const float ACTION_BUTTONS_WIDTH = 100f;
        private const float ARROW_HEIGHT = 16f;
        private const float ARROW_WIDTH = 16f;
        private const float BACK_BUTTON_HEIGHT = 40f;
        private const float BACK_BUTTON_WIDTH = 40f;
        private const float CATEGORY_DROPDOWN_HEIGHT = 40f;
        private const float CATEGORY_DROPDOWN_WIDTH = 150f;
        private const float CONTENT_HEIGHT = 500f;
        private const float CURRENCY_H_U_D_HEIGHT = 40f;
        private const float CURRENCY_H_U_D_WIDTH = 400f;
        private const float EMPTY_STATE_HEIGHT = 50f;
        private const float FILTER_BAR_HEIGHT = 50f;
        private const float HEADER_HEIGHT = 60f;
        private const float HOME_BUTTON_HEIGHT = 40f;
        private const float HOME_BUTTON_WIDTH = 40f;
        private const float ITEM_DESCRIPTION_HEIGHT = 100f;
        private const float ITEM_DESCRIPTION_WIDTH = 100f;
        private const float ITEM_GRID_AREA_HEIGHT = 50f;
        private const float ITEM_IMAGE_HEIGHT = 120f;
        private const float ITEM_IMAGE_WIDTH = 120f;
        private const float ITEM_NAME_HEIGHT = 100f;
        private const float ITEM_NAME_WIDTH = 100f;
        private const float LABEL_HEIGHT = 4f;
        private const float SELECTED_INDICATOR_HEIGHT = 40f;
        private const float SELECTED_INDICATOR_WIDTH = 4f;
        private const float SELL_BUTTON_HEIGHT = 100f;
        private const float SELL_BUTTON_WIDTH = 100f;
        private const float SETTINGS_BUTTON_HEIGHT = 40f;
        private const float SETTINGS_BUTTON_WIDTH = 40f;
        private const float SORT_DROPDOWN_HEIGHT = 40f;
        private const float SORT_DROPDOWN_WIDTH = 120f;
        private const float SPACER_HEIGHT = 100f;
        private const float SPACER_WIDTH = 100f;
        private const float TAB_교단_HEIGHT = 60f;
        private const float TAB_사용_HEIGHT = 60f;
        private const float TAB_성장_HEIGHT = 60f;
        private const float TAB_연성카드_HEIGHT = 60f;
        private const float TAB_장비_HEIGHT = 60f;
        private const float TITLE_TEXT_HEIGHT = 40f;
        private const float TITLE_TEXT_WIDTH = 200f;
        private const float USE_BUTTON_HEIGHT = 100f;
        private const float USE_BUTTON_WIDTH = 100f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// InventoryScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("InventoryScreen");

            var background = CreateBackground_1(root);
            var safeArea = CreateSafeArea(root);

            // Add main component
            root.AddComponent<InventoryScreen>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Background

        private static GameObject CreateBackground_1(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(200, 230, 200, 255);
            image.raycastTarget = false;

            CreatePattern(go);

            return go;
        }

        #endregion

        #region Pattern

        private static GameObject CreatePattern(GameObject parent)
        {
            var go = CreateChild(parent, "Pattern");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(220, 240, 220, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region SafeArea

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var go = CreateChild(parent, "SafeArea");
            SetStretch(go);

            CreateHeader(go);
            CreateContent_1(go);

            return go;
        }

        #endregion

        #region Header

        private static GameObject CreateHeader(GameObject parent)
        {
            var go = CreateChild(parent, "Header");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = false;

            CreateBackButton(go);
            CreateTitleText(go);
            CreateCurrencyHUD(go);
            CreateHomeButton(go);

            return go;
        }

        #endregion

        #region BackButton

        private static GameObject CreateBackButton(GameObject parent)
        {
            var go = CreateChild(parent, "BackButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_1(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_1(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "<";
            tmp.fontSize = 18f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TitleText

        private static GameObject CreateTitleText(GameObject parent)
        {
            var go = CreateChild(parent, "TitleText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "배낭";
            tmp.fontSize = 24f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CurrencyHUD

        private static GameObject CreateCurrencyHUD(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyHUD");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 40f);
            rect.anchoredPosition = new Vector2(-60f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(245, 245, 245, 255);
            image.raycastTarget = false;

            CreateText_2(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_2(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "G: 94,572 | S: 102/102 | P: 1,809";
            tmp.fontSize = 12f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region HomeButton

        private static GameObject CreateHomeButton(GameObject parent)
        {
            var go = CreateChild(parent, "HomeButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(-10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_3(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_3(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "H";
            tmp.fontSize = 18f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent_1(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -60f);

            CreateLeftSideTab(go);
            CreateMainArea(go);
            CreateItemDetailPanel(go);

            return go;
        }

        #endregion

        #region LeftSideTab

        private static GameObject CreateLeftSideTab(GameObject parent)
        {
            var go = CreateChild(parent, "LeftSideTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(120f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = false;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateTab_사용(go);
            CreateTab_성장(go);
            CreateTab_장비(go);
            CreateTab_교단(go);
            CreateTab_연성카드(go);

            return go;
        }

        #endregion

        #region Tab_사용

        private static GameObject CreateTab_사용(GameObject parent)
        {
            var go = CreateChild(parent, "Tab_사용");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateBackground_2(go);
            CreateLabel_1(go);
            CreateSelectedIndicator_1(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_2(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사용";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_1(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Tab_성장

        private static GameObject CreateTab_성장(GameObject parent)
        {
            var go = CreateChild(parent, "Tab_성장");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateBackground_3(go);
            CreateLabel_2(go);
            CreateSelectedIndicator_2(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_3(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "성장";
            tmp.fontSize = 14f;
            tmp.color = new Color32(80, 80, 80, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_2(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = false;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Tab_장비

        private static GameObject CreateTab_장비(GameObject parent)
        {
            var go = CreateChild(parent, "Tab_장비");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateBackground_4(go);
            CreateLabel_3(go);
            CreateSelectedIndicator_3(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_4(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "장비";
            tmp.fontSize = 14f;
            tmp.color = new Color32(80, 80, 80, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_3(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = false;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Tab_교단

        private static GameObject CreateTab_교단(GameObject parent)
        {
            var go = CreateChild(parent, "Tab_교단");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateBackground_5(go);
            CreateLabel_4(go);
            CreateSelectedIndicator_4(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_5(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "교단";
            tmp.fontSize = 14f;
            tmp.color = new Color32(80, 80, 80, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_4(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = false;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Tab_연성카드

        private static GameObject CreateTab_연성카드(GameObject parent)
        {
            var go = CreateChild(parent, "Tab_연성카드");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateBackground_6(go);
            CreateLabel_5(go);
            CreateSelectedIndicator_5(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_6(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "연성카드";
            tmp.fontSize = 14f;
            tmp.color = new Color32(80, 80, 80, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_5(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = false;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region MainArea

        private static GameObject CreateMainArea(GameObject parent)
        {
            var go = CreateChild(parent, "MainArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(120f, 0f);
            rect.offsetMax = new Vector2(-300f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = false;

            CreateFilterBar(go);
            CreateItemGridArea(go);
            CreateEmptyState_1(go);

            return go;
        }

        #endregion

        #region FilterBar

        private static GameObject CreateFilterBar(GameObject parent)
        {
            var go = CreateChild(parent, "FilterBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(245, 245, 245, 255);
            image.raycastTarget = false;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(16, 16, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateCategoryDropdown(go);
            CreateSortDropdown(go);
            CreateSpacer(go);
            CreateSettingsButton(go);

            return go;
        }

        #endregion

        #region CategoryDropdown

        private static GameObject CreateCategoryDropdown(GameObject parent)
        {
            var go = CreateChild(parent, "CategoryDropdown");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(150f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 150f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = true;


            CreateLabel_6(go);
            CreateArrow_1(go);
            CreateTemplate_1(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(10f, 2f);
            rect.offsetMax = new Vector2(-30f, -2f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "";
            tmp.fontSize = 14f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Arrow

        private static GameObject CreateArrow_1(GameObject parent)
        {
            var go = CreateChild(parent, "Arrow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(16f, 16f);
            rect.anchoredPosition = new Vector2(-10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 100, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Template

        private static GameObject CreateTemplate_1(GameObject parent)
        {
            var go = CreateChild(parent, "Template");
            go.SetActive(false);

            return go;
        }

        #endregion

        #region SortDropdown

        private static GameObject CreateSortDropdown(GameObject parent)
        {
            var go = CreateChild(parent, "SortDropdown");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(120f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 120f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = true;


            CreateLabel_7(go);
            CreateArrow_2(go);
            CreateTemplate_2(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_7(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(10f, 2f);
            rect.offsetMax = new Vector2(-30f, -2f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "";
            tmp.fontSize = 14f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Arrow

        private static GameObject CreateArrow_2(GameObject parent)
        {
            var go = CreateChild(parent, "Arrow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(16f, 16f);
            rect.anchoredPosition = new Vector2(-10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 100, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Template

        private static GameObject CreateTemplate_2(GameObject parent)
        {
            var go = CreateChild(parent, "Template");
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Spacer

        private static GameObject CreateSpacer(GameObject parent)
        {
            var go = CreateChild(parent, "Spacer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;

            return go;
        }

        #endregion

        #region SettingsButton

        private static GameObject CreateSettingsButton(GameObject parent)
        {
            var go = CreateChild(parent, "SettingsButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 40f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "⚙";
            tmp.fontSize = 18f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ItemGridArea

        private static GameObject CreateItemGridArea(GameObject parent)
        {
            var go = CreateChild(parent, "ItemGridArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -50f);

            var scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;

            CreateViewport(go);

            return go;
        }

        #endregion

        #region Viewport

        private static GameObject CreateViewport(GameObject parent)
        {
            var go = CreateChild(parent, "Viewport");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = true;

            var mask = go.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            CreateContent_2(go);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent_2(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 500f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var grid = go.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(80f, 80f);
            grid.spacing = new Vector2(8f, 8f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperLeft;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 7;
            grid.padding = new RectOffset(16, 16, 16, 16);

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            return go;
        }

        #endregion

        #region EmptyState

        private static GameObject CreateEmptyState_1(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyState");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -50f);
            go.SetActive(false);

            CreateText_4(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_4(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "보유한 아이템이 없습니다.";
            tmp.fontSize = 16f;
            tmp.color = new Color32(150, 150, 150, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ItemDetailPanel

        private static GameObject CreateItemDetailPanel(GameObject parent)
        {
            var go = CreateChild(parent, "ItemDetailPanel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = false;

            CreateEmptyState_2(go);
            CreateDetailView(go);

            return go;
        }

        #endregion

        #region EmptyState

        private static GameObject CreateEmptyState_2(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyState");
            SetStretch(go);

            CreateText_5(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_5(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "아이템을 선택해주세요";
            tmp.fontSize = 14f;
            tmp.color = new Color32(150, 150, 150, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region DetailView

        private static GameObject CreateDetailView(GameObject parent)
        {
            var go = CreateChild(parent, "DetailView");
            SetStretch(go);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(16, 16, 16, 16);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateItemImage(go);
            CreateItemName(go);
            CreateItemDescription(go);
            CreateActionButtons(go);

            return go;
        }

        #endregion

        #region ItemImage

        private static GameObject CreateItemImage(GameObject parent)
        {
            var go = CreateChild(parent, "ItemImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(120f, 120f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 120f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 200, 255, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ItemName

        private static GameObject CreateItemName(GameObject parent)
        {
            var go = CreateChild(parent, "ItemName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 30f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Item Name";
            tmp.fontSize = 16f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ItemDescription

        private static GameObject CreateItemDescription(GameObject parent)
        {
            var go = CreateChild(parent, "ItemDescription");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 100f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Item description...";
            tmp.fontSize = 12f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.TopLeft;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ActionButtons

        private static GameObject CreateActionButtons(GameObject parent)
        {
            var go = CreateChild(parent, "ActionButtons");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 80f;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateUseButton(go);
            CreateSellButton(go);

            return go;
        }

        #endregion

        #region UseButton

        private static GameObject CreateUseButton(GameObject parent)
        {
            var go = CreateChild(parent, "UseButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_6(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_6(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사용";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SellButton

        private static GameObject CreateSellButton(GameObject parent)
        {
            var go = CreateChild(parent, "SellButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_7(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_7(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "판매";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<InventoryScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _tabWidget
            so.FindProperty("_tabWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftSideTab")?.GetComponent<InventoryTabWidget>();

            // _categoryDropdown
            so.FindProperty("_categoryDropdown").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/FilterBar/CategoryDropdown")?.GetComponent<TMP_Dropdown>();

            // _sortDropdown
            so.FindProperty("_sortDropdown").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/FilterBar/SortDropdown")?.GetComponent<TMP_Dropdown>();

            // _settingsButton
            so.FindProperty("_settingsButton").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/FilterBar/SettingsButton")?.GetComponent<Button>();

            // _itemGridContainer
            so.FindProperty("_itemGridContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/ItemGridArea/Viewport/Content")?.GetComponent<RectTransform>();

            // _itemScrollRect
            so.FindProperty("_itemScrollRect").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/ItemGridArea")?.GetComponent<ScrollRect>();

            // _itemDetailWidget
            so.FindProperty("_itemDetailWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/ItemDetailPanel")?.GetComponent<ItemDetailWidget>();

            // _emptyStateObject
            so.FindProperty("_emptyStateObject").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/EmptyState");

            // _emptyStateText
            so.FindProperty("_emptyStateText").objectReferenceValue = FindChild(root, "SafeArea/Content/MainArea/EmptyState/Text")?.GetComponent<TextMeshProUGUI>();

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/BackButton")?.GetComponent<Button>();

            // _homeButton
            so.FindProperty("_homeButton").objectReferenceValue = FindChild(root, "SafeArea/Header/HomeButton")?.GetComponent<Button>();

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static GameObject FindChild(GameObject root, string path)
        {
            if (string.IsNullOrEmpty(path)) return root;
            var t = root.transform.Find(path);
            return t?.gameObject;
        }

        #endregion

        #region Helpers

        private static GameObject CreateRoot(string name)
        {
            var root = new GameObject(name);
            var rect = root.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            root.AddComponent<CanvasGroup>();
            return root;
        }

        private static GameObject CreateChild(GameObject parent, string name)
        {
            var child = new GameObject(name);
            child.transform.SetParent(parent.transform, false);
            return child;
        }

        private static RectTransform SetStretch(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return rect;
        }

        #endregion
    }
}
