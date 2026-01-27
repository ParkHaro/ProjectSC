using System.Collections.Generic;
using Sc.Common.UI.Widgets;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Shop;
using Sc.Contents.Shop.Widgets;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// ShopScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/ShopScreen.prefab
    /// Generated at: 2026-01-27 14:47:31
    /// </summary>
    public static class ShopScreenPrefabBuilder_Generated
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
        private static readonly Color BgGlass = new Color32(0, 0, 0, 76);
        private static readonly Color Blue = new Color32(100, 150, 255, 255);
        private static readonly Color Color = new Color32(80, 55, 40, 255);
        private static readonly Color Green = new Color32(100, 180, 100, 255);
        private static readonly Color Red = new Color32(255, 200, 100, 255);

        #endregion

        #region Constants

        private const float BACK_BUTTON_GROUP_HEIGHT = 100f;
        private const float BACK_BUTTON_GROUP_WIDTH = 100f;
        private const float BACK_BUTTON_HEIGHT = 100f;
        private const float BACK_BUTTON_WIDTH = 100f;
        private const float BACKGROUND_HEIGHT = 100f;
        private const float BACKGROUND_WIDTH = 100f;
        private const float BADGE_HEIGHT = 20f;
        private const float BADGE_WIDTH = 20f;
        private const float BULK_PURCHASE_BUTTON_HEIGHT = 100f;
        private const float BULK_PURCHASE_BUTTON_WIDTH = 100f;
        private const float CATEGORY_SHORTCUT_0_HEIGHT = 100f;
        private const float CATEGORY_SHORTCUT_0_WIDTH = 100f;
        private const float CATEGORY_SHORTCUT_1_HEIGHT = 100f;
        private const float CATEGORY_SHORTCUT_1_WIDTH = 100f;
        private const float CATEGORY_SHORTCUT_2_HEIGHT = 100f;
        private const float CATEGORY_SHORTCUT_2_WIDTH = 100f;
        private const float CHARACTER_IMAGE_HEIGHT = 100f;
        private const float CHARACTER_IMAGE_WIDTH = 100f;
        private const float CHECKMARK_HEIGHT = 8f;
        private const float CONTENT_HEIGHT = 110f;
        private const float CURRENCY_H_U_D_HEIGHT = 100f;
        private const float CURRENCY_H_U_D_WIDTH = 100f;
        private const float CURRENCY_ICON_HEIGHT = 100f;
        private const float CURRENCY_ICON_WIDTH = 100f;
        private const float DIALOGUE_BOX_HEIGHT = 100f;
        private const float DIALOGUE_BOX_WIDTH = 100f;
        private const float DIALOGUE_TEXT_HEIGHT = 50f;
        private const float DIALOGUE_TEXT_WIDTH = 200f;
        private const float EMPTY_STATE_TEXT_HEIGHT = 50f;
        private const float EMPTY_STATE_TEXT_WIDTH = 200f;
        private const float FOOTER_HEIGHT = 50f;
        private const float GEM_DISPLAY_HEIGHT = 100f;
        private const float GEM_DISPLAY_WIDTH = 100f;
        private const float GEM_TEXT_HEIGHT = 50f;
        private const float GEM_TEXT_WIDTH = 200f;
        private const float GOLD_DISPLAY_HEIGHT = 100f;
        private const float GOLD_DISPLAY_WIDTH = 100f;
        private const float GOLD_TEXT_HEIGHT = 50f;
        private const float GOLD_TEXT_WIDTH = 200f;
        private const float HEADER_HEIGHT = 60f;
        private const float HOME_BUTTON_HEIGHT = 100f;
        private const float HOME_BUTTON_WIDTH = 100f;
        private const float ICON_HEIGHT = 100f;
        private const float ICON_WIDTH = 100f;
        private const float LABEL_HEIGHT = 50f;
        private const float LABEL_WIDTH = 200f;
        private const float PRICE_GROUP_HEIGHT = 100f;
        private const float PRICE_GROUP_WIDTH = 100f;
        private const float PRICE_TEXT_HEIGHT = 50f;
        private const float PRICE_TEXT_WIDTH = 200f;
        private const float PRODUCT_CONTAINER_HEIGHT = 100f;
        private const float PRODUCT_CONTAINER_WIDTH = 100f;
        private const float PRODUCT_GRID_FOOTER_HEIGHT = 100f;
        private const float PRODUCT_GRID_FOOTER_WIDTH = 100f;
        private const float PRODUCT_GRID_HEIGHT = 475f;
        private const float PRODUCT_ICON_HEIGHT = 100f;
        private const float PRODUCT_ICON_WIDTH = 100f;
        private const float PRODUCT_ITEM_0_HEIGHT = 100f;
        private const float PRODUCT_ITEM_0_WIDTH = 100f;
        private const float PRODUCT_ITEM_1_HEIGHT = 100f;
        private const float PRODUCT_ITEM_1_WIDTH = 100f;
        private const float PRODUCT_ITEM_2_HEIGHT = 100f;
        private const float PRODUCT_ITEM_2_WIDTH = 100f;
        private const float PRODUCT_ITEM_3_HEIGHT = 100f;
        private const float PRODUCT_ITEM_3_WIDTH = 100f;
        private const float PRODUCT_ITEM_4_HEIGHT = 100f;
        private const float PRODUCT_ITEM_4_WIDTH = 100f;
        private const float PRODUCT_ITEM_5_HEIGHT = 100f;
        private const float PRODUCT_ITEM_5_WIDTH = 100f;
        private const float PRODUCT_NAME_HEIGHT = 50f;
        private const float PRODUCT_NAME_WIDTH = 200f;
        private const float PURCHASE_LIMIT_HEIGHT = 50f;
        private const float PURCHASE_LIMIT_WIDTH = 200f;
        private const float REFRESH_ICON_HEIGHT = 100f;
        private const float REFRESH_ICON_WIDTH = 100f;
        private const float REFRESH_TIMER_GROUP_HEIGHT = 100f;
        private const float REFRESH_TIMER_GROUP_WIDTH = 100f;
        private const float REFRESH_TIMER_TEXT_HEIGHT = 50f;
        private const float REFRESH_TIMER_TEXT_WIDTH = 200f;
        private const float RIGHT_GROUP_HEIGHT = 100f;
        private const float RIGHT_GROUP_WIDTH = 100f;
        private const float SCROLL_VIEW_HEIGHT = 100f;
        private const float SCROLL_VIEW_WIDTH = 100f;
        private const float SELECT_ALL_TEXT_HEIGHT = 50f;
        private const float SELECT_ALL_TEXT_WIDTH = 200f;
        private const float SELECT_ALL_TOGGLE_HEIGHT = 100f;
        private const float SELECT_ALL_TOGGLE_WIDTH = 100f;
        private const float SHOPKEEPER_DISPLAY_HEIGHT = 100f;
        private const float SHOPKEEPER_DISPLAY_WIDTH = 100f;
        private const float SPACER_HEIGHT = 100f;
        private const float SPACER_WIDTH = 100f;
        private const float TAB_BUTTON_0_HEIGHT = 100f;
        private const float TAB_BUTTON_0_WIDTH = 100f;
        private const float TAB_BUTTON_1_HEIGHT = 100f;
        private const float TAB_BUTTON_1_WIDTH = 100f;
        private const float TAB_BUTTON_2_HEIGHT = 100f;
        private const float TAB_BUTTON_2_WIDTH = 100f;
        private const float TAB_BUTTON_3_HEIGHT = 100f;
        private const float TAB_BUTTON_3_WIDTH = 100f;
        private const float TAB_BUTTON_4_HEIGHT = 100f;
        private const float TAB_BUTTON_4_WIDTH = 100f;
        private const float TAB_BUTTON_5_HEIGHT = 100f;
        private const float TAB_BUTTON_5_WIDTH = 100f;
        private const float TAB_BUTTON_6_HEIGHT = 100f;
        private const float TAB_BUTTON_6_WIDTH = 100f;
        private const float TAB_LIST_HEIGHT = 100f;
        private const float TAB_LIST_WIDTH = 100f;
        private const float TAG_LABEL_HEIGHT = 20f;
        private const float TAG_LABEL_WIDTH = 60f;
        private const float TITLE_TEXT_HEIGHT = 50f;
        private const float TITLE_TEXT_WIDTH = 200f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// ShopScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("ShopScreen");

            var background = CreateBackground_1(root);
            var safeArea = CreateSafeArea(root);
            var emptyState = CreateEmptyState(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<ShopScreen>();

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
            image.color = new Color32(80, 55, 40, 255);
            image.raycastTarget = true;

            CreateWoodPattern(go);
            CreateVignette(go);

            return go;
        }

        #endregion

        #region WoodPattern

        private static GameObject CreateWoodPattern(GameObject parent)
        {
            var go = CreateChild(parent, "WoodPattern");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(50, 35, 25, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Vignette

        private static GameObject CreateVignette(GameObject parent)
        {
            var go = CreateChild(parent, "Vignette");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 76);
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
            CreateContent(go);
            CreateFooter(go);

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
            image.color = new Color32(30, 45, 60, 240);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(20, 20, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateBackButtonGroup(go);
            CreateSpacer_1(go);
            CreateRightGroup(go);

            return go;
        }

        #endregion

        #region BackButtonGroup

        private static GameObject CreateBackButtonGroup(GameObject parent)
        {
            var go = CreateChild(parent, "BackButtonGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateBackButton(go);
            CreateTitleText(go);

            return go;
        }

        #endregion

        #region BackButton

        private static GameObject CreateBackButton(GameObject parent)
        {
            var go = CreateChild(parent, "BackButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 50f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 100, 120, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_1(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "<";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
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
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 34f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상점";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Spacer

        private static GameObject CreateSpacer_1(GameObject parent)
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

        #region RightGroup

        private static GameObject CreateRightGroup(GameObject parent)
        {
            var go = CreateChild(parent, "RightGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateCurrencyHUD(go);
            CreateHomeButton(go);

            return go;
        }

        #endregion

        #region CurrencyHUD

        private static GameObject CreateCurrencyHUD(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyHUD");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 20f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateGoldDisplay(go);
            CreateGemDisplay(go);

            return go;
        }

        #endregion

        #region GoldDisplay

        private static GameObject CreateGoldDisplay(GameObject parent)
        {
            var go = CreateChild(parent, "GoldDisplay");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_1(go);
            CreateGoldText(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region GoldText

        private static GameObject CreateGoldText(GameObject parent)
        {
            var go = CreateChild(parent, "GoldText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minWidth = 80f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "549,061";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region GemDisplay

        private static GameObject CreateGemDisplay(GameObject parent)
        {
            var go = CreateChild(parent, "GemDisplay");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_2(go);
            CreateGemText(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 255, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region GemText

        private static GameObject CreateGemText(GameObject parent)
        {
            var go = CreateChild(parent, "GemText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minWidth = 80f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1,809";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 40f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_2(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "H";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 50f);
            rect.offsetMax = new Vector2(0f, -60f);

            CreateLeftArea(go);
            CreateRightArea(go);

            return go;
        }

        #endregion

        #region LeftArea

        private static GameObject CreateLeftArea(GameObject parent)
        {
            var go = CreateChild(parent, "LeftArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(280f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateShopkeeperDisplay(go);
            CreateTabList(go);

            return go;
        }

        #endregion

        #region ShopkeeperDisplay

        private static GameObject CreateShopkeeperDisplay(GameObject parent)
        {
            var go = CreateChild(parent, "ShopkeeperDisplay");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 200f;
            layoutElement.flexibleWidth = 1f;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 5, 5);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateCharacterImage(go);
            CreateDialogueBox(go);

            return go;
        }

        #endregion

        #region CharacterImage

        private static GameObject CreateCharacterImage(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 120f;
            layoutElement.preferredHeight = 120f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(200, 150, 200, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region DialogueBox

        private static GameObject CreateDialogueBox(GameObject parent)
        {
            var go = CreateChild(parent, "DialogueBox");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 240, 240);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 0f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateDialogueText(go);

            return go;
        }

        #endregion

        #region DialogueText

        private static GameObject CreateDialogueText(GameObject parent)
        {
            var go = CreateChild(parent, "DialogueText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "오늘은 뭘 보여드릴까요?";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabList

        private static GameObject CreateTabList(GameObject parent)
        {
            var go = CreateChild(parent, "TabList");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;
            layoutElement.flexibleHeight = 1f;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 5, 5);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateTabButton_0(go);
            CreateTabButton_1(go);
            CreateTabButton_2(go);
            CreateTabButton_3(go);
            CreateTabButton_4(go);
            CreateTabButton_5(go);
            CreateTabButton_6(go);

            return go;
        }

        #endregion

        #region TabButton_0

        private static GameObject CreateTabButton_0(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_3(go);
            CreateLabel_3(go);
            CreateSelectedIndicator_1(go);
            CreateBadge_1(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "데일리";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_1(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_1(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_1(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_1

        private static GameObject CreateTabButton_1(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_4(go);
            CreateLabel_4(go);
            CreateSelectedIndicator_2(go);
            CreateBadge_2(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_4(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "잡화";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_2(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_2(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_2(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_2

        private static GameObject CreateTabButton_2(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_5(go);
            CreateLabel_5(go);
            CreateSelectedIndicator_3(go);
            CreateBadge_3(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_5(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "전투석";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_3(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_3(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_3(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_3

        private static GameObject CreateTabButton_3(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_6(go);
            CreateLabel_6(go);
            CreateSelectedIndicator_4(go);
            CreateBadge_4(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_6(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "인증서";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_4(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_4(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_4(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_4

        private static GameObject CreateTabButton_4(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_4");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_7(go);
            CreateLabel_7(go);
            CreateSelectedIndicator_5(go);
            CreateBadge_5(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_7(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_7(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "추천";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_5(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_5(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_5(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_5

        private static GameObject CreateTabButton_5(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_5");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_8(go);
            CreateLabel_8(go);
            CreateSelectedIndicator_6(go);
            CreateBadge_6(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_8(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_8(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "프론티어";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_6(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_6(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_6(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_6(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabButton_6

        private static GameObject CreateTabButton_6(GameObject parent)
        {
            var go = CreateChild(parent, "TabButton_6");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 55f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 80, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_9(go);
            CreateLabel_9(go);
            CreateSelectedIndicator_7(go);
            CreateBadge_7(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_9(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 153);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_9(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "여우";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectedIndicator

        private static GameObject CreateSelectedIndicator_7(GameObject parent)
        {
            var go = CreateChild(parent, "SelectedIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region Badge

        private static GameObject CreateBadge_7(GameObject parent)
        {
            var go = CreateChild(parent, "Badge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 0, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateBadgeCount_7(go);

            return go;
        }

        #endregion

        #region BadgeCount

        private static GameObject CreateBadgeCount_7(GameObject parent)
        {
            var go = CreateChild(parent, "BadgeCount");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RightArea

        private static GameObject CreateRightArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(280f, 0f);
            rect.offsetMax = new Vector2(0f, 0f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(15, 15, 10, 10);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateProductContainer(go);

            return go;
        }

        #endregion

        #region ProductContainer

        private static GameObject CreateProductContainer(GameObject parent)
        {
            var go = CreateChild(parent, "ProductContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;
            layoutElement.flexibleHeight = 1f;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateScrollView(go);
            CreateProductGridFooter(go);

            return go;
        }

        #endregion

        #region ScrollView

        private static GameObject CreateScrollView(GameObject parent)
        {
            var go = CreateChild(parent, "ScrollView");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;
            layoutElement.flexibleHeight = 1f;

            var scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 26);
            image.raycastTarget = true;

            CreateViewport(go);

            return go;
        }

        #endregion

        #region Viewport

        private static GameObject CreateViewport(GameObject parent)
        {
            var go = CreateChild(parent, "Viewport");
            SetStretch(go);

            var mask = go.AddComponent<Mask>();
            mask.showMaskGraphic = false;


            var image = go.AddComponent<Image>();
            image.color = TextPrimary;
            image.raycastTarget = true;

            CreateProductGrid(go);

            return go;
        }

        #endregion

        #region ProductGrid

        private static GameObject CreateProductGrid(GameObject parent)
        {
            var go = CreateChild(parent, "ProductGrid");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 475f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var grid = go.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(180f, 220f);
            grid.spacing = new Vector2(15f, 15f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperCenter;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 3;
            grid.padding = new RectOffset(10, 10, 10, 10);

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            CreateProductItem_0(go);
            CreateProductItem_1(go);
            CreateProductItem_2(go);
            CreateProductItem_3(go);
            CreateProductItem_4(go);
            CreateProductItem_5(go);

            return go;
        }

        #endregion

        #region ProductItem_0

        private static GameObject CreateProductItem_0(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_1(go);
            CreateProductIcon_1(go);
            CreateProductName_1(go);
            CreatePurchaseLimit_1(go);
            CreatePriceGroup_1(go);
            CreateSoldOutOverlay_1(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_1(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_1(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_1(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_1(go);
            CreatePriceText_1(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_1(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_1(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_1(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_1(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductItem_1

        private static GameObject CreateProductItem_1(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_2(go);
            CreateProductIcon_2(go);
            CreateProductName_2(go);
            CreatePurchaseLimit_2(go);
            CreatePriceGroup_2(go);
            CreateSoldOutOverlay_2(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_2(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_2(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_2(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_2(go);
            CreatePriceText_2(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_2(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_2(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_2(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_2(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductItem_2

        private static GameObject CreateProductItem_2(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_3(go);
            CreateProductIcon_3(go);
            CreateProductName_3(go);
            CreatePurchaseLimit_3(go);
            CreatePriceGroup_3(go);
            CreateSoldOutOverlay_3(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_3(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_3(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_3(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_3(go);
            CreatePriceText_3(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_3(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_3(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_3(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_3(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductItem_3

        private static GameObject CreateProductItem_3(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_4(go);
            CreateProductIcon_4(go);
            CreateProductName_4(go);
            CreatePurchaseLimit_4(go);
            CreatePriceGroup_4(go);
            CreateSoldOutOverlay_4(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_4(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_4(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_4(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_4(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_4(go);
            CreatePriceText_4(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_4(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_4(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_4(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_4(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_4(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductItem_4

        private static GameObject CreateProductItem_4(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_4");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_5(go);
            CreateProductIcon_5(go);
            CreateProductName_5(go);
            CreatePurchaseLimit_5(go);
            CreatePriceGroup_5(go);
            CreateSoldOutOverlay_5(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_5(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_5(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_5(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_5(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_5(go);
            CreatePriceText_5(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_5(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_5(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_5(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_5(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_5(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductItem_5

        private static GameObject CreateProductItem_5(GameObject parent)
        {
            var go = CreateChild(parent, "ProductItem_5");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<ShopProductItem>();

            go.AddComponent<CanvasGroup>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 245, 235, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateTagLabel_6(go);
            CreateProductIcon_6(go);
            CreateProductName_6(go);
            CreatePurchaseLimit_6(go);
            CreatePriceGroup_6(go);
            CreateSoldOutOverlay_6(go);

            return go;
        }

        #endregion

        #region TagLabel

        private static GameObject CreateTagLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "TagLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(60f, 20f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 80, 220);
            image.raycastTarget = true;

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
            tmp.text = "일일갱신";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductIcon

        private static GameObject CreateProductIcon_6(GameObject parent)
        {
            var go = CreateChild(parent, "ProductIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 100f;
            layoutElement.flexibleWidth = 0f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(150, 150, 150, 200);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ProductName

        private static GameObject CreateProductName_6(GameObject parent)
        {
            var go = CreateChild(parent, "ProductName");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품명";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PurchaseLimit

        private static GameObject CreatePurchaseLimit_6(GameObject parent)
        {
            var go = CreateChild(parent, "PurchaseLimit");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "구매 가능 1/1";
            tmp.fontSize = 11f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PriceGroup

        private static GameObject CreatePriceGroup_6(GameObject parent)
        {
            var go = CreateChild(parent, "PriceGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 25f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCurrencyIcon_6(go);
            CreatePriceText_6(go);

            return go;
        }

        #endregion

        #region CurrencyIcon

        private static GameObject CreateCurrencyIcon_6(GameObject parent)
        {
            var go = CreateChild(parent, "CurrencyIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 18f;
            layoutElement.preferredHeight = 18f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PriceText

        private static GameObject CreatePriceText_6(GameObject parent)
        {
            var go = CreateChild(parent, "PriceText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 14f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SoldOutOverlay

        private static GameObject CreateSoldOutOverlay_6(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSoldOutText_6(go);

            return go;
        }

        #endregion

        #region SoldOutText

        private static GameObject CreateSoldOutText_6(GameObject parent)
        {
            var go = CreateChild(parent, "SoldOutText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "품절";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ProductGridFooter

        private static GameObject CreateProductGridFooter(GameObject parent)
        {
            var go = CreateChild(parent, "ProductGridFooter");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 60f;
            layoutElement.flexibleWidth = 1f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateCategoryShortcut_0(go);
            CreateCategoryShortcut_1(go);
            CreateCategoryShortcut_2(go);

            return go;
        }

        #endregion

        #region CategoryShortcut_0

        private static GameObject CreateCategoryShortcut_0(GameObject parent)
        {
            var go = CreateChild(parent, "CategoryShortcut_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<CategoryShortcut>();

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(220, 210, 190, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_10(go);
            CreateLabel_10(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_10(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_10(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "고단 성장 재료 상자";
            tmp.fontSize = 12f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CategoryShortcut_1

        private static GameObject CreateCategoryShortcut_1(GameObject parent)
        {
            var go = CreateChild(parent, "CategoryShortcut_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<CategoryShortcut>();

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(220, 210, 190, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_11(go);
            CreateLabel_11(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_11(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_11(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "교주의방 꾸미기";
            tmp.fontSize = 12f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CategoryShortcut_2

        private static GameObject CreateCategoryShortcut_2(GameObject parent)
        {
            var go = CreateChild(parent, "CategoryShortcut_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<CategoryShortcut>();

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(220, 210, 190, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_12(go);
            CreateLabel_12(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_12(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_12(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "고단 요리 상자";
            tmp.fontSize = 12f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Footer

        private static GameObject CreateFooter(GameObject parent)
        {
            var go = CreateChild(parent, "Footer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 45, 60, 240);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 20f;
            layout.padding = new RectOffset(20, 20, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateRefreshTimerGroup(go);
            CreateSpacer_2(go);
            CreateSelectAllToggle(go);
            CreateBulkPurchaseButton(go);

            return go;
        }

        #endregion

        #region RefreshTimerGroup

        private static GameObject CreateRefreshTimerGroup(GameObject parent)
        {
            var go = CreateChild(parent, "RefreshTimerGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateRefreshIcon(go);
            CreateRefreshTimerText(go);

            return go;
        }

        #endregion

        #region RefreshIcon

        private static GameObject CreateRefreshIcon(GameObject parent)
        {
            var go = CreateChild(parent, "RefreshIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region RefreshTimerText

        private static GameObject CreateRefreshTimerText(GameObject parent)
        {
            var go = CreateChild(parent, "RefreshTimerText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "갱신까지 10시간 12분";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Spacer

        private static GameObject CreateSpacer_2(GameObject parent)
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

        #region SelectAllToggle

        private static GameObject CreateSelectAllToggle(GameObject parent)
        {
            var go = CreateChild(parent, "SelectAllToggle");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 140f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;


            CreateBackground_2(go);
            CreateSelectAllText(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_2(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 30f;
            layoutElement.preferredHeight = 30f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 100, 120, 255);
            image.raycastTarget = true;

            CreateCheckmark(go);

            return go;
        }

        #endregion

        #region Checkmark

        private static GameObject CreateCheckmark(GameObject parent)
        {
            var go = CreateChild(parent, "Checkmark");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(4f, 4f);
            rect.offsetMax = new Vector2(-4f, -4f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region SelectAllText

        private static GameObject CreateSelectAllText(GameObject parent)
        {
            var go = CreateChild(parent, "SelectAllText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "모두 선택 OFF";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BulkPurchaseButton

        private static GameObject CreateBulkPurchaseButton(GameObject parent)
        {
            var go = CreateChild(parent, "BulkPurchaseButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_13(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_13(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "일괄 구매";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EmptyState

        private static GameObject CreateEmptyState(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyState");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 0f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            go.SetActive(false);

            CreateEmptyStateText(go);

            return go;
        }

        #endregion

        #region EmptyStateText

        private static GameObject CreateEmptyStateText(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyStateText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "상품이 없습니다.";
            tmp.fontSize = 24f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region OverlayLayer

        private static GameObject CreateOverlayLayer(GameObject parent)
        {
            var go = CreateChild(parent, "OverlayLayer");
            SetStretch(go);

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<ShopScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _shopkeeperImage
            so.FindProperty("_shopkeeperImage").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/ShopkeeperDisplay/CharacterImage")?.GetComponent<Image>();

            // _dialogueText
            so.FindProperty("_dialogueText").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/ShopkeeperDisplay/DialogueBox/DialogueText")?.GetComponent<TextMeshProUGUI>();

            // _tabGroup
            so.FindProperty("_tabGroup").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList")?.GetComponent<TabGroupWidget>();

            // _tabButtons (array)
            var tabButtonsProp = so.FindProperty("_tabButtons");
            tabButtonsProp.arraySize = 7;
            tabButtonsProp.GetArrayElementAtIndex(0).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_0")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(1).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_1")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(2).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_2")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(3).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_3")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(4).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_4")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(5).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_5")?.GetComponent<ShopTabButton>();
            tabButtonsProp.GetArrayElementAtIndex(6).objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/TabList/TabButton_6")?.GetComponent<ShopTabButton>();

            // _productContainer
            so.FindProperty("_productContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ScrollView/Viewport/ProductGrid")?.GetComponent<RectTransform>();

            // _scrollRect
            so.FindProperty("_scrollRect").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ScrollView")?.GetComponent<ScrollRect>();

            // _categoryShortcutContainer
            so.FindProperty("_categoryShortcutContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ProductGridFooter")?.GetComponent<RectTransform>();

            // _categoryShortcuts (array)
            var categoryShortcutsProp = so.FindProperty("_categoryShortcuts");
            categoryShortcutsProp.arraySize = 3;
            categoryShortcutsProp.GetArrayElementAtIndex(0).objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ProductGridFooter/CategoryShortcut_0")?.GetComponent<CategoryShortcut>();
            categoryShortcutsProp.GetArrayElementAtIndex(1).objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ProductGridFooter/CategoryShortcut_1")?.GetComponent<CategoryShortcut>();
            categoryShortcutsProp.GetArrayElementAtIndex(2).objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/ProductContainer/ProductGridFooter/CategoryShortcut_2")?.GetComponent<CategoryShortcut>();

            // _goldText
            so.FindProperty("_goldText").objectReferenceValue = FindChild(root, "SafeArea/Header/RightGroup/CurrencyHUD/GoldDisplay/GoldText")?.GetComponent<TextMeshProUGUI>();

            // _gemText
            so.FindProperty("_gemText").objectReferenceValue = FindChild(root, "SafeArea/Header/RightGroup/CurrencyHUD/GemDisplay/GemText")?.GetComponent<TextMeshProUGUI>();

            // _refreshTimerText
            so.FindProperty("_refreshTimerText").objectReferenceValue = FindChild(root, "SafeArea/Footer/RefreshTimerGroup/RefreshTimerText")?.GetComponent<TextMeshProUGUI>();

            // _selectAllToggle
            so.FindProperty("_selectAllToggle").objectReferenceValue = FindChild(root, "SafeArea/Footer/SelectAllToggle")?.GetComponent<Toggle>();

            // _selectAllText
            so.FindProperty("_selectAllText").objectReferenceValue = FindChild(root, "SafeArea/Footer/SelectAllToggle/SelectAllText")?.GetComponent<TextMeshProUGUI>();

            // _bulkPurchaseButton
            so.FindProperty("_bulkPurchaseButton").objectReferenceValue = FindChild(root, "SafeArea/Footer/BulkPurchaseButton")?.GetComponent<Button>();

            // _emptyStateObject
            so.FindProperty("_emptyStateObject").objectReferenceValue = FindChild(root, "EmptyState");

            // _emptyStateText
            so.FindProperty("_emptyStateText").objectReferenceValue = FindChild(root, "EmptyState/EmptyStateText")?.GetComponent<TextMeshProUGUI>();

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/BackButtonGroup/BackButton")?.GetComponent<Button>();

            // _homeButton
            so.FindProperty("_homeButton").objectReferenceValue = FindChild(root, "SafeArea/Header/RightGroup/HomeButton")?.GetComponent<Button>();

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
