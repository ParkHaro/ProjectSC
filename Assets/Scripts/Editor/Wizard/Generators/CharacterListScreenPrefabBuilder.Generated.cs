using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Character;
using Sc.Contents.Character.Widgets;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// CharacterListScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/CharacterListScreen.prefab
    /// Generated at: 2026-01-27 14:42:49
    /// </summary>
    public static class CharacterListScreenPrefabBuilder_Generated
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
        private static readonly Color BgGlass = new Color32(120, 255, 120, 60);
        private static readonly Color Color = new Color32(40, 40, 60, 200);
        private static readonly Color Red = new Color32(255, 200, 100, 255);

        #endregion

        #region Constants

        private const float ALL_CHARACTERS_TAB_HEIGHT = 100f;
        private const float ALL_CHARACTERS_TAB_WIDTH = 100f;
        private const float BACK_BUTTON_HEIGHT = 40f;
        private const float BACK_BUTTON_WIDTH = 40f;
        private const float CENTER_AREA_HEIGHT = 100f;
        private const float CENTER_AREA_WIDTH = 100f;
        private const float CHARACTER_CARD_TEMPLATE_HEIGHT = 200f;
        private const float CHARACTER_CARD_TEMPLATE_WIDTH = 150f;
        private const float CHARACTER_THUMBNAIL_HEIGHT = 5f;
        private const float CONTENT_HEIGHT = 100f;
        private const float CONTENT_WIDTH = 100f;
        private const float CURRENCY_H_U_D_HEIGHT = 40f;
        private const float CURRENCY_H_U_D_WIDTH = 80f;
        private const float ELEMENT_ICON_HEIGHT = 30f;
        private const float ELEMENT_ICON_WIDTH = 30f;
        private const float EXPRESSION_FILTER_HEIGHT = 100f;
        private const float EXPRESSION_FILTER_WIDTH = 100f;
        private const float FAVORITES_TAB_HEIGHT = 100f;
        private const float FAVORITES_TAB_WIDTH = 100f;
        private const float FILTER_AREA_HEIGHT = 40f;
        private const float FILTER_TOGGLE_HEIGHT = 100f;
        private const float FILTER_TOGGLE_WIDTH = 100f;
        private const float GRID_CONTENT_HEIGHT = 100f;
        private const float HEADER_HEIGHT = 60f;
        private const float HOME_BUTTON_HEIGHT = 40f;
        private const float HOME_BUTTON_WIDTH = 40f;
        private const float LEFT_AREA_WIDTH = 200f;
        private const float NAME_TEXT_HEIGHT = 25f;
        private const float RIGHT_AREA_WIDTH = 150f;
        private const float ROLE_ICON_HEIGHT = 30f;
        private const float ROLE_ICON_WIDTH = 30f;
        private const float SAFE_AREA_HEIGHT = 40f;
        private const float SORT_BUTTON_HEIGHT = 100f;
        private const float SORT_BUTTON_WIDTH = 100f;
        private const float SORT_ORDER_TOGGLE_HEIGHT = 100f;
        private const float SORT_ORDER_TOGGLE_WIDTH = 100f;
        private const float SPACER_HEIGHT = 100f;
        private const float SPACER_WIDTH = 100f;
        private const float STAR_0_HEIGHT = 20f;
        private const float STAR_0_WIDTH = 20f;
        private const float STAR_1_HEIGHT = 20f;
        private const float STAR_1_WIDTH = 20f;
        private const float STAR_2_HEIGHT = 20f;
        private const float STAR_2_WIDTH = 20f;
        private const float STAR_3_HEIGHT = 20f;
        private const float STAR_3_WIDTH = 20f;
        private const float STAR_4_HEIGHT = 20f;
        private const float STAR_4_WIDTH = 20f;
        private const float STAR_RATING_HEIGHT = 20f;
        private const float STAR_RATING_WIDTH = 120f;
        private const float TAB_AREA_HEIGHT = 50f;
        private const float TITLE_TEXT_HEIGHT = 40f;
        private const float TITLE_TEXT_WIDTH = 120f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// CharacterListScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("CharacterListScreen");

            var background = CreateBackground(root);
            var safeArea = CreateSafeArea(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<CharacterListScreen>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Background

        private static GameObject CreateBackground(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(5, 5, 12, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region SafeArea

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var go = CreateChild(parent, "SafeArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(20f, 20f);
            rect.offsetMax = new Vector2(-20f, -20f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateHeader(go);
            CreateTabArea(go);
            CreateFilterArea(go);
            CreateContent(go);

            return go;
        }

        #endregion

        #region Header

        private static GameObject CreateHeader(GameObject parent)
        {
            var go = CreateChild(parent, "Header");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 60f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = false;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateLeftArea(go);
            CreateCenterArea(go);
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
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 200f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
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
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 40f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_1(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "<";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
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
            rect.sizeDelta = new Vector2(120f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 120f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사도";
            tmp.fontSize = 28f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CenterArea

        private static GameObject CreateCenterArea(GameObject parent)
        {
            var go = CreateChild(parent, "CenterArea");
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

        #region RightArea

        private static GameObject CreateRightArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(150f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 150f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
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
            rect.sizeDelta = new Vector2(80f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 20);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 40f;
            layoutElement.preferredHeight = 40f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_2(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "H";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TabArea

        private static GameObject CreateTabArea(GameObject parent)
        {
            var go = CreateChild(parent, "TabArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;
            layoutElement.flexibleWidth = 1f;

            CreateTabGroup(go);

            return go;
        }

        #endregion

        #region TabGroup

        private static GameObject CreateTabGroup(GameObject parent)
        {
            var go = CreateChild(parent, "TabGroup");
            SetStretch(go);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateAllCharactersTab(go);
            CreateFavoritesTab(go);

            return go;
        }

        #endregion

        #region AllCharactersTab

        private static GameObject CreateAllCharactersTab(GameObject parent)
        {
            var go = CreateChild(parent, "AllCharactersTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(120, 255, 120, 60);
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
            tmp.text = "모여라 사도!";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region FavoritesTab

        private static GameObject CreateFavoritesTab(GameObject parent)
        {
            var go = CreateChild(parent, "FavoritesTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(50, 50, 50, 100);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

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
            tmp.text = "관심 사도 0/2";
            tmp.fontSize = 18f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region FilterArea

        private static GameObject CreateFilterArea(GameObject parent)
        {
            var go = CreateChild(parent, "FilterArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 40f;
            layoutElement.flexibleWidth = 1f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = false;

            CreateFilterGroup(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<CharacterFilterWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_expressionFilterButton").objectReferenceValue = go.transform.Find("FilterGroup/ExpressionFilter")?.GetComponent<Button>();
                widgetSo.FindProperty("_expressionFilterText").objectReferenceValue = go.transform.Find("FilterGroup/ExpressionFilter/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_filterToggleButton").objectReferenceValue = go.transform.Find("FilterGroup/FilterToggle")?.GetComponent<Button>();
                widgetSo.FindProperty("_filterToggleText").objectReferenceValue = go.transform.Find("FilterGroup/FilterToggle/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_sortButton").objectReferenceValue = go.transform.Find("FilterGroup/SortButton")?.GetComponent<Button>();
                widgetSo.FindProperty("_sortText").objectReferenceValue = go.transform.Find("FilterGroup/SortButton/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_sortOrderButton").objectReferenceValue = go.transform.Find("FilterGroup/SortOrderToggle")?.GetComponent<Button>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region FilterGroup

        private static GameObject CreateFilterGroup(GameObject parent)
        {
            var go = CreateChild(parent, "FilterGroup");
            SetStretch(go);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateExpressionFilter(go);
            CreateFilterToggle(go);
            CreateSpacer(go);
            CreateSortButton(go);
            CreateSortOrderToggle(go);

            return go;
        }

        #endregion

        #region ExpressionFilter

        private static GameObject CreateExpressionFilter(GameObject parent)
        {
            var go = CreateChild(parent, "ExpressionFilter");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
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
            tmp.text = "감정표현";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region FilterToggle

        private static GameObject CreateFilterToggle(GameObject parent)
        {
            var go = CreateChild(parent, "FilterToggle");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

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
            tmp.text = "필터 OFF";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

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

        #region SortButton

        private static GameObject CreateSortButton(GameObject parent)
        {
            var go = CreateChild(parent, "SortButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 60f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

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
            tmp.text = "정렬";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SortOrderToggle

        private static GameObject CreateSortOrderToggle(GameObject parent)
        {
            var go = CreateChild(parent, "SortOrderToggle");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 40f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 40, 60, 200);
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
            tmp.text = "↓";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
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

            CreateCharacterGrid(go);

            return go;
        }

        #endregion

        #region CharacterGrid

        private static GameObject CreateCharacterGrid(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterGrid");
            SetStretch(go);

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


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = true;

            var mask = go.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            CreateGridContent(go);

            return go;
        }

        #endregion

        #region GridContent

        private static GameObject CreateGridContent(GameObject parent)
        {
            var go = CreateChild(parent, "GridContent");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var grid = go.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(150f, 200f);
            grid.spacing = new Vector2(10f, 10f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperCenter;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 6;
            grid.padding = new RectOffset(10, 10, 10, 10);

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            CreateCharacterCardTemplate(go);

            return go;
        }

        #endregion

        #region CharacterCardTemplate

        private static GameObject CreateCharacterCardTemplate(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterCardTemplate");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(150f, 200f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<CharacterCard>();

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateCardBackground(go);
            CreateCharacterThumbnail(go);
            CreateElementIcon(go);
            CreateRoleIcon(go);
            CreateStarRating(go);
            CreateNameText(go);

            return go;
        }

        #endregion

        #region CardBackground

        private static GameObject CreateCardBackground(GameObject parent)
        {
            var go = CreateChild(parent, "CardBackground");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 30, 45, 230);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region CharacterThumbnail

        private static GameObject CreateCharacterThumbnail(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterThumbnail");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.25f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-10f, -5f);
            rect.anchoredPosition = new Vector2(0f, -2.5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 20);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ElementIcon

        private static GameObject CreateElementIcon(GameObject parent)
        {
            var go = CreateChild(parent, "ElementIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(30f, 30f);
            rect.anchoredPosition = new Vector2(5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = AccentPrimary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region RoleIcon

        private static GameObject CreateRoleIcon(GameObject parent)
        {
            var go = CreateChild(parent, "RoleIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(30f, 30f);
            rect.anchoredPosition = new Vector2(-5f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region StarRating

        private static GameObject CreateStarRating(GameObject parent)
        {
            var go = CreateChild(parent, "StarRating");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(120f, 20f);
            rect.anchoredPosition = new Vector2(0f, 25f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateStar_0(go);
            CreateStar_1(go);
            CreateStar_2(go);
            CreateStar_3(go);
            CreateStar_4(go);

            return go;
        }

        #endregion

        #region Star_0

        private static GameObject CreateStar_0(GameObject parent)
        {
            var go = CreateChild(parent, "Star_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Star_1

        private static GameObject CreateStar_1(GameObject parent)
        {
            var go = CreateChild(parent, "Star_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Star_2

        private static GameObject CreateStar_2(GameObject parent)
        {
            var go = CreateChild(parent, "Star_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Star_3

        private static GameObject CreateStar_3(GameObject parent)
        {
            var go = CreateChild(parent, "Star_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Star_4

        private static GameObject CreateStar_4(GameObject parent)
        {
            var go = CreateChild(parent, "Star_4");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region NameText

        private static GameObject CreateNameText(GameObject parent)
        {
            var go = CreateChild(parent, "NameText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "캐릭터 이름";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
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

            go.AddComponent<CanvasGroup>();

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<CharacterListScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _allCharactersTab
            so.FindProperty("_allCharactersTab").objectReferenceValue = FindChild(root, "SafeArea/TabArea/TabGroup/AllCharactersTab")?.GetComponent<Button>();

            // _allCharactersTabText
            so.FindProperty("_allCharactersTabText").objectReferenceValue = FindChild(root, "SafeArea/TabArea/TabGroup/AllCharactersTab/Text")?.GetComponent<TextMeshProUGUI>();

            // _favoritesTab
            so.FindProperty("_favoritesTab").objectReferenceValue = FindChild(root, "SafeArea/TabArea/TabGroup/FavoritesTab")?.GetComponent<Button>();

            // _favoritesTabText
            so.FindProperty("_favoritesTabText").objectReferenceValue = FindChild(root, "SafeArea/TabArea/TabGroup/FavoritesTab/Text")?.GetComponent<TextMeshProUGUI>();

            // _filterWidget
            so.FindProperty("_filterWidget").objectReferenceValue = FindChild(root, "SafeArea/FilterArea")?.GetComponent<CharacterFilterWidget>();

            // _characterGridContainer
            so.FindProperty("_characterGridContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/CharacterGrid/Viewport/GridContent")?.GetComponent<RectTransform>();

            // _scrollRect
            so.FindProperty("_scrollRect").objectReferenceValue = FindChild(root, "SafeArea/Content/CharacterGrid")?.GetComponent<ScrollRect>();

            // _characterCardPrefab
            so.FindProperty("_characterCardPrefab").objectReferenceValue = FindChild(root, "SafeArea/Content/CharacterGrid/Viewport/GridContent/CharacterCardTemplate");

            // _listContainer
            so.FindProperty("_listContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/CharacterGrid/Viewport/GridContent")?.GetComponent<RectTransform>();

            // _characterItemPrefab
            so.FindProperty("_characterItemPrefab").objectReferenceValue = FindChild(root, "SafeArea/Content/CharacterGrid/Viewport/GridContent/CharacterCardTemplate");

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/LeftArea/BackButton")?.GetComponent<Button>();

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
