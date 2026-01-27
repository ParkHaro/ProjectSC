using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Stage;
using Sc.Contents.Stage.Widgets;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// StageSelectScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/StageSelectScreen.prefab
    /// Generated at: 2026-01-27 14:42:52
    /// </summary>
    public static class StageSelectScreenPrefabBuilder_Generated
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
        private static readonly Color Blue = new Color32(100, 180, 255, 255);
        private static readonly Color Color = new Color32(80, 120, 100, 255);
        private static readonly Color Green = new Color32(100, 180, 100, 255);
        private static readonly Color Red = new Color32(150, 80, 80, 255);

        #endregion

        #region Constants

        private const float BACK_BUTTON_HEIGHT = 100f;
        private const float BACK_BUTTON_WIDTH = 100f;
        private const float BUBBLE_STAGE_NAME_TEXT_HEIGHT = 50f;
        private const float BUBBLE_STAGE_NAME_TEXT_WIDTH = 200f;
        private const float CONTENT_HEIGHT = 170f;
        private const float CURRENCY_H_U_D_HEIGHT = 100f;
        private const float CURRENCY_H_U_D_WIDTH = 100f;
        private const float DIFFICULTY_TABS_HEIGHT = 100f;
        private const float DIFFICULTY_TABS_WIDTH = 100f;
        private const float ENEMY_ICON_HEIGHT = 100f;
        private const float ENEMY_ICON_WIDTH = 100f;
        private const float ENEMY_PREVIEW_CONTAINER_HEIGHT = 100f;
        private const float ENEMY_PREVIEW_CONTAINER_WIDTH = 100f;
        private const float ENTRY_LIMIT_TEXT_HEIGHT = 50f;
        private const float ENTRY_LIMIT_TEXT_WIDTH = 200f;
        private const float FOOTER_HEIGHT = 100f;
        private const float GOLD_HEIGHT = 100f;
        private const float GOLD_WIDTH = 100f;
        private const float HARD_TAB_HEIGHT = 100f;
        private const float HARD_TAB_WIDTH = 100f;
        private const float HEADER_HEIGHT = 70f;
        private const float HELL_TAB_HEIGHT = 100f;
        private const float HELL_TAB_WIDTH = 100f;
        private const float ICON_HEIGHT = 100f;
        private const float ICON_WIDTH = 100f;
        private const float MAP_CONTENT_HEIGHT = 800f;
        private const float MAP_CONTENT_WIDTH = 1200f;
        private const float MILESTONE_10_HEIGHT = 35f;
        private const float MILESTONE_10_WIDTH = 40f;
        private const float MILESTONE_20_HEIGHT = 35f;
        private const float MILESTONE_20_WIDTH = 40f;
        private const float MILESTONE_30_HEIGHT = 35f;
        private const float MILESTONE_30_WIDTH = 40f;
        private const float MODULE_CONTAINER_HEIGHT = 60f;
        private const float NEXT_ARROW_HEIGHT = 50f;
        private const float NEXT_ARROW_WIDTH = 200f;
        private const float NEXT_CHAPTER_BUTTON_HEIGHT = 120f;
        private const float NEXT_CHAPTER_BUTTON_WIDTH = 60f;
        private const float NEXT_CHAPTER_TEXT_HEIGHT = 50f;
        private const float NEXT_CHAPTER_TEXT_WIDTH = 200f;
        private const float NORMAL_TAB_HEIGHT = 100f;
        private const float NORMAL_TAB_WIDTH = 100f;
        private const float PARTY_ICON_0_HEIGHT = 100f;
        private const float PARTY_ICON_0_WIDTH = 100f;
        private const float PARTY_ICON_1_HEIGHT = 100f;
        private const float PARTY_ICON_1_WIDTH = 100f;
        private const float PARTY_ICON_2_HEIGHT = 100f;
        private const float PARTY_ICON_2_WIDTH = 100f;
        private const float PARTY_ICON_3_HEIGHT = 100f;
        private const float PARTY_ICON_3_WIDTH = 100f;
        private const float PARTY_PREVIEW_CONTAINER_HEIGHT = 100f;
        private const float PARTY_PREVIEW_CONTAINER_WIDTH = 100f;
        private const float PLUS_BUTTON_HEIGHT = 100f;
        private const float PLUS_BUTTON_WIDTH = 100f;
        private const float PREMIUM_HEIGHT = 100f;
        private const float PREMIUM_WIDTH = 100f;
        private const float PREV_ARROW_HEIGHT = 50f;
        private const float PREV_ARROW_WIDTH = 200f;
        private const float PREV_CHAPTER_BUTTON_HEIGHT = 120f;
        private const float PREV_CHAPTER_BUTTON_WIDTH = 60f;
        private const float PREV_CHAPTER_TEXT_HEIGHT = 50f;
        private const float PREV_CHAPTER_TEXT_WIDTH = 200f;
        private const float PROGRESS_SLIDER_HEIGHT = 20f;
        private const float PROGRESS_TEXT_HEIGHT = 50f;
        private const float PROGRESS_TEXT_WIDTH = 200f;
        private const float RECOMMENDED_POWER_TEXT_HEIGHT = 50f;
        private const float RECOMMENDED_POWER_TEXT_WIDTH = 200f;
        private const float REWARD_TEXT_HEIGHT = 50f;
        private const float REWARD_TEXT_WIDTH = 200f;
        private const float RIGHT_TOP_AREA_HEIGHT = 50f;
        private const float RIGHT_TOP_AREA_WIDTH = 320f;
        private const float SELECTION_INDICATOR_HEIGHT = 4f;
        private const float SLIDER_AREA_HEIGHT = 100f;
        private const float SLIDER_AREA_WIDTH = 100f;
        private const float SPACER_HEIGHT = 100f;
        private const float SPACER_WIDTH = 100f;
        private const float STAGE_INFO_BUBBLE_HEIGHT = 180f;
        private const float STAGE_INFO_BUBBLE_WIDTH = 280f;
        private const float STAGE_NODE_1010_HEIGHT = 100f;
        private const float STAGE_NODE_1010_WIDTH = 100f;
        private const float STAGE_NODE_103_HEIGHT = 100f;
        private const float STAGE_NODE_103_WIDTH = 100f;
        private const float STAGE_NODE_104_HEIGHT = 100f;
        private const float STAGE_NODE_104_WIDTH = 100f;
        private const float STAGE_NODE_105_HEIGHT = 100f;
        private const float STAGE_NODE_105_WIDTH = 100f;
        private const float STAGE_NODE_106_HEIGHT = 100f;
        private const float STAGE_NODE_106_WIDTH = 100f;
        private const float STAGE_NODE_107_HEIGHT = 100f;
        private const float STAGE_NODE_107_WIDTH = 100f;
        private const float STAGE_NODE_108_HEIGHT = 100f;
        private const float STAGE_NODE_108_WIDTH = 100f;
        private const float STAGE_NODE_109_HEIGHT = 100f;
        private const float STAGE_NODE_109_WIDTH = 100f;
        private const float STAGE_NUMBER_HEIGHT = 50f;
        private const float STAGE_NUMBER_WIDTH = 200f;
        private const float STAGE_PROGRESS_NAVIGATE_BUTTON_HEIGHT = 100f;
        private const float STAGE_PROGRESS_NAVIGATE_BUTTON_WIDTH = 100f;
        private const float STAGE_PROGRESS_TEXT_HEIGHT = 50f;
        private const float STAGE_PROGRESS_TEXT_WIDTH = 200f;
        private const float STAMINA_HEIGHT = 100f;
        private const float STAMINA_WIDTH = 100f;
        private const float STAR_ICON_HEIGHT = 100f;
        private const float STAR_ICON_WIDTH = 100f;
        private const float STAR_PROGRESS_BAR_HEIGHT = 70f;
        private const float STAR_PROGRESS_BAR_WIDTH = 350f;
        private const float STAR_TEXT_HEIGHT = 50f;
        private const float STAR_TEXT_WIDTH = 200f;
        private const float STARS_HEIGHT = 100f;
        private const float STARS_WIDTH = 100f;
        private const float TITLE_TEXT_HEIGHT = 50f;
        private const float TITLE_TEXT_WIDTH = 200f;
        private const float VALUE_HEIGHT = 50f;
        private const float VALUE_WIDTH = 200f;
        private const float WORLD_MAP_BUTTON_HEIGHT = 100f;
        private const float WORLD_MAP_BUTTON_WIDTH = 100f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// StageSelectScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("StageSelectScreen");

            var background = CreateBackground_1(root);
            var safeArea = CreateSafeArea(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<StageSelectScreen>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Background

        private static GameObject CreateBackground_1(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);

            CreateMapBackground(go);

            return go;
        }

        #endregion

        #region MapBackground

        private static GameObject CreateMapBackground(GameObject parent)
        {
            var go = CreateChild(parent, "MapBackground");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 120, 100, 255);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(0f, 70f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(20, 35, 50, 240);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(20, 20, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateBackButton(go);
            CreateTitleText(go);
            CreateEntryLimitText(go);
            CreateSpacer_1(go);
            CreateCurrencyHUD(go);

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
            image.color = new Color32(30, 45, 60, 230);
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
            tmp.text = "스테이지 리스트";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EntryLimitText

        private static GameObject CreateEntryLimitText(GameObject parent)
        {
            var go = CreateChild(parent, "EntryLimitText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
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
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateStamina(go);
            CreateGold(go);
            CreatePremium(go);

            return go;
        }

        #endregion

        #region Stamina

        private static GameObject CreateStamina(GameObject parent)
        {
            var go = CreateChild(parent, "Stamina");
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
            CreateValue_1(go);
            CreatePlusButton_1(go);

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
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_1(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "102/102";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PlusButton

        private static GameObject CreatePlusButton_1(GameObject parent)
        {
            var go = CreateChild(parent, "PlusButton");
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

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            return go;
        }

        #endregion

        #region Gold

        private static GameObject CreateGold(GameObject parent)
        {
            var go = CreateChild(parent, "Gold");
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
            CreateValue_2(go);
            CreatePlusButton_2(go);

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
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_2(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "549,061";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PlusButton

        private static GameObject CreatePlusButton_2(GameObject parent)
        {
            var go = CreateChild(parent, "PlusButton");
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

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            return go;
        }

        #endregion

        #region Premium

        private static GameObject CreatePremium(GameObject parent)
        {
            var go = CreateChild(parent, "Premium");
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

            CreateIcon_3(go);
            CreateValue_3(go);
            CreatePlusButton_3(go);

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
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_3(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1,809";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PlusButton

        private static GameObject CreatePlusButton_3(GameObject parent)
        {
            var go = CreateChild(parent, "PlusButton");
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

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 100f);
            rect.offsetMax = new Vector2(0f, -70f);

            CreateModuleContainer(go);
            CreateRightTopArea(go);
            CreateStageMapArea(go);
            CreateStarProgressBar(go);

            return go;
        }

        #endregion

        #region ModuleContainer

        private static GameObject CreateModuleContainer(GameObject parent)
        {
            var go = CreateChild(parent, "ModuleContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            return go;
        }

        #endregion

        #region RightTopArea

        private static GameObject CreateRightTopArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightTopArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(320f, 50f);
            rect.anchoredPosition = new Vector2(-20f, -10f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 45, 60, 230);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(15, 15, 10, 10);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateStageProgressText(go);
            CreateStageProgressNavigateButton(go);

            return go;
        }

        #endregion

        #region StageProgressText

        private static GameObject CreateStageProgressText(GameObject parent)
        {
            var go = CreateChild(parent, "StageProgressText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "11-10 최후의 방어선!";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageProgressNavigateButton

        private static GameObject CreateStageProgressNavigateButton(GameObject parent)
        {
            var go = CreateChild(parent, "StageProgressNavigateButton");
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
            tmp.text = ">>";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageMapArea

        private static GameObject CreateStageMapArea(GameObject parent)
        {
            var go = CreateChild(parent, "StageMapArea");
            SetStretch(go);

            CreateMapScrollView(go);
            CreateChapterNavigation(go);

            return go;
        }

        #endregion

        #region MapScrollView

        private static GameObject CreateMapScrollView(GameObject parent)
        {
            var go = CreateChild(parent, "MapScrollView");
            SetStretch(go);

            var scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = true;
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

            var mask = go.AddComponent<Mask>();
            mask.showMaskGraphic = false;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 3);
            image.raycastTarget = true;

            CreateMapContent(go);

            return go;
        }

        #endregion

        #region MapContent

        private static GameObject CreateMapContent(GameObject parent)
        {
            var go = CreateChild(parent, "MapContent");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(1200f, 800f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateStageNodeContainer(go);
            CreateStageInfoBubble(go);

            return go;
        }

        #endregion

        #region StageNodeContainer

        private static GameObject CreateStageNodeContainer(GameObject parent)
        {
            var go = CreateChild(parent, "StageNodeContainer");
            SetStretch(go);

            CreateStageNode_103(go);
            CreateStageNode_104(go);
            CreateStageNode_105(go);
            CreateStageNode_106(go);
            CreateStageNode_107(go);
            CreateStageNode_108(go);
            CreateStageNode_109(go);
            CreateStageNode_1010(go);

            return go;
        }

        #endregion

        #region StageNode_10-3

        private static GameObject CreateStageNode_103(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(100f, -150f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_1(go);
            CreateStars_1(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_1(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-3";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_1(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_1(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_1(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★☆";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-4

        private static GameObject CreateStageNode_104(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-4");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(300f, -100f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_2(go);
            CreateStars_2(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_2(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-4";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_2(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_2(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_2(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★☆";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-5

        private static GameObject CreateStageNode_105(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-5");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(500f, -150f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_3(go);
            CreateStars_3(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_3(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-5";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_3(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_3(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_3(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★☆";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-6

        private static GameObject CreateStageNode_106(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-6");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(400f, -300f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_4(go);
            CreateStars_4(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_4(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-6";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_4(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_4(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_4(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★★";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-7

        private static GameObject CreateStageNode_107(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-7");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(600f, -350f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(120, 200, 120, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_5(go);
            CreateStars_5(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_5(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-7";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_5(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_5(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_5(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★★";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-8

        private static GameObject CreateStageNode_108(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-8");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(800f, -300f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_6(go);
            CreateStars_6(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_6(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-8";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_6(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_6(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_6(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★☆";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-9

        private static GameObject CreateStageNode_109(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-9");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(700f, -150f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_7(go);
            CreateStars_7(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_7(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-9";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_7(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_7(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_7(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★★";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageNode_10-10

        private static GameObject CreateStageNode_1010(GameObject parent)
        {
            var go = CreateChild(parent, "StageNode_10-10");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(900f, -100f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 160, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStageNumber_8(go);
            CreateStars_8(go);

            return go;
        }

        #endregion

        #region StageNumber

        private static GameObject CreateStageNumber_8(GameObject parent)
        {
            var go = CreateChild(parent, "StageNumber");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-10";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Stars

        private static GameObject CreateStars_8(GameObject parent)
        {
            var go = CreateChild(parent, "Stars");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 20f;

            CreateStarText_8(go);

            return go;
        }

        #endregion

        #region StarText

        private static GameObject CreateStarText_8(GameObject parent)
        {
            var go = CreateChild(parent, "StarText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "★★☆";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 215, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageInfoBubble

        private static GameObject CreateStageInfoBubble(GameObject parent)
        {
            var go = CreateChild(parent, "StageInfoBubble");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(280f, 180f);
            rect.anchoredPosition = new Vector2(600f, -200f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(250, 250, 250, 240);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(15, 15, 15, 15);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateRecommendedPowerText(go);
            CreateBubbleStageNameText(go);
            CreateEnemyPreviewContainer(go);
            CreatePartyPreviewContainer(go);

            return go;
        }

        #endregion

        #region RecommendedPowerText

        private static GameObject CreateRecommendedPowerText(GameObject parent)
        {
            var go = CreateChild(parent, "RecommendedPowerText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "권장 전투력: 117,660";
            tmp.fontSize = 14f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BubbleStageNameText

        private static GameObject CreateBubbleStageNameText(GameObject parent)
        {
            var go = CreateChild(parent, "BubbleStageNameText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 28f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "깜빡이는 터널!";
            tmp.fontSize = 18f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EnemyPreviewContainer

        private static GameObject CreateEnemyPreviewContainer(GameObject parent)
        {
            var go = CreateChild(parent, "EnemyPreviewContainer");
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

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;

            CreateEnemyIcon(go);

            return go;
        }

        #endregion

        #region EnemyIcon

        private static GameObject CreateEnemyIcon(GameObject parent)
        {
            var go = CreateChild(parent, "EnemyIcon");
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
            image.color = new Color32(150, 80, 80, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PartyPreviewContainer

        private static GameObject CreatePartyPreviewContainer(GameObject parent)
        {
            var go = CreateChild(parent, "PartyPreviewContainer");
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

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 40f;

            CreatePartyIcon_0(go);
            CreatePartyIcon_1(go);
            CreatePartyIcon_2(go);
            CreatePartyIcon_3(go);

            return go;
        }

        #endregion

        #region PartyIcon_0

        private static GameObject CreatePartyIcon_0(GameObject parent)
        {
            var go = CreateChild(parent, "PartyIcon_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 35f;
            layoutElement.preferredHeight = 35f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 180, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PartyIcon_1

        private static GameObject CreatePartyIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "PartyIcon_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 35f;
            layoutElement.preferredHeight = 35f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 180, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PartyIcon_2

        private static GameObject CreatePartyIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "PartyIcon_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 35f;
            layoutElement.preferredHeight = 35f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 180, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region PartyIcon_3

        private static GameObject CreatePartyIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "PartyIcon_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 35f;
            layoutElement.preferredHeight = 35f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 180, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region ChapterNavigation

        private static GameObject CreateChapterNavigation(GameObject parent)
        {
            var go = CreateChild(parent, "ChapterNavigation");
            SetStretch(go);

            CreatePrevChapterButton(go);
            CreateNextChapterButton(go);

            return go;
        }

        #endregion

        #region PrevChapterButton

        private static GameObject CreatePrevChapterButton(GameObject parent)
        {
            var go = CreateChild(parent, "PrevChapterButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 120f);
            rect.anchoredPosition = new Vector2(10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 180);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 10);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreatePrevArrow(go);
            CreatePrevChapterText(go);

            return go;
        }

        #endregion

        #region PrevArrow

        private static GameObject CreatePrevArrow(GameObject parent)
        {
            var go = CreateChild(parent, "PrevArrow");
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
            tmp.text = "<";
            tmp.fontSize = 24f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PrevChapterText

        private static GameObject CreatePrevChapterText(GameObject parent)
        {
            var go = CreateChild(parent, "PrevChapterText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 22f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이전 월드";
            tmp.fontSize = 12f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region NextChapterButton

        private static GameObject CreateNextChapterButton(GameObject parent)
        {
            var go = CreateChild(parent, "NextChapterButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 120f);
            rect.anchoredPosition = new Vector2(-10f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 180);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(5, 5, 10, 10);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateNextArrow(go);
            CreateNextChapterText(go);

            return go;
        }

        #endregion

        #region NextArrow

        private static GameObject CreateNextArrow(GameObject parent)
        {
            var go = CreateChild(parent, "NextArrow");
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
            tmp.text = ">";
            tmp.fontSize = 24f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region NextChapterText

        private static GameObject CreateNextChapterText(GameObject parent)
        {
            var go = CreateChild(parent, "NextChapterText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 22f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "다음 월드";
            tmp.fontSize = 12f;
            tmp.color = new Color32(40, 40, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StarProgressBar

        private static GameObject CreateStarProgressBar(GameObject parent)
        {
            var go = CreateChild(parent, "StarProgressBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 0f);
            rect.pivot = new Vector2(0f, 0f);
            rect.sizeDelta = new Vector2(350f, 70f);
            rect.anchoredPosition = new Vector2(20f, 20f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(50, 60, 50, 220);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(15, 15, 10, 10);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateStarIcon(go);
            CreateProgressText(go);
            CreateSliderArea(go);

            return go;
        }

        #endregion

        #region StarIcon

        private static GameObject CreateStarIcon(GameObject parent)
        {
            var go = CreateChild(parent, "StarIcon");
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
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region ProgressText

        private static GameObject CreateProgressText(GameObject parent)
        {
            var go = CreateChild(parent, "ProgressText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 26f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "14/30";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SliderArea

        private static GameObject CreateSliderArea(GameObject parent)
        {
            var go = CreateChild(parent, "SliderArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 30f;
            layoutElement.flexibleWidth = 1f;

            CreateProgressSlider(go);
            CreateMilestoneContainer(go);

            return go;
        }

        #endregion

        #region ProgressSlider

        private static GameObject CreateProgressSlider(GameObject parent)
        {
            var go = CreateChild(parent, "ProgressSlider");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 10f);
            rect.offsetMax = new Vector2(0f, -10f);


            CreateBackground_2(go);
            CreateFillArea(go);

            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground_2(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 60, 60, 200);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Fill Area

        private static GameObject CreateFillArea(GameObject parent)
        {
            var go = CreateChild(parent, "Fill Area");
            SetStretch(go);

            CreateFill(go);

            return go;
        }

        #endregion

        #region Fill

        private static GameObject CreateFill(GameObject parent)
        {
            var go = CreateChild(parent, "Fill");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 0f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region MilestoneContainer

        private static GameObject CreateMilestoneContainer(GameObject parent)
        {
            var go = CreateChild(parent, "MilestoneContainer");
            SetStretch(go);

            CreateMilestone_10(go);
            CreateMilestone_20(go);
            CreateMilestone_30(go);

            return go;
        }

        #endregion

        #region Milestone_10

        private static GameObject CreateMilestone_10(GameObject parent)
        {
            var go = CreateChild(parent, "Milestone_10");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.3333333f, 0.5f);
            rect.anchorMax = new Vector2(0.3333333f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 35f);
            rect.anchoredPosition = new Vector2(0f, 15f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_4(go);
            CreateRewardText_1(go);

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
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region RewardText

        private static GameObject CreateRewardText_1(GameObject parent)
        {
            var go = CreateChild(parent, "RewardText");
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
            tmp.text = "25";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Milestone_20

        private static GameObject CreateMilestone_20(GameObject parent)
        {
            var go = CreateChild(parent, "Milestone_20");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.6666667f, 0.5f);
            rect.anchorMax = new Vector2(0.6666667f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 35f);
            rect.anchoredPosition = new Vector2(0f, 15f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_5(go);
            CreateRewardText_2(go);

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
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region RewardText

        private static GameObject CreateRewardText_2(GameObject parent)
        {
            var go = CreateChild(parent, "RewardText");
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
            tmp.text = "50";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Milestone_30

        private static GameObject CreateMilestone_30(GameObject parent)
        {
            var go = CreateChild(parent, "Milestone_30");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 35f);
            rect.anchoredPosition = new Vector2(0f, 15f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateIcon_6(go);
            CreateRewardText_3(go);

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
            layoutElement.preferredWidth = 20f;
            layoutElement.preferredHeight = 20f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region RewardText

        private static GameObject CreateRewardText_3(GameObject parent)
        {
            var go = CreateChild(parent, "RewardText");
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
            tmp.text = "100";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
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
            rect.sizeDelta = new Vector2(0f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 45, 60, 230);
            image.raycastTarget = true;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 15f;
            layout.padding = new RectOffset(20, 20, 15, 15);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateDifficultyTabs(go);
            CreateSpacer_2(go);
            CreateWorldMapButton(go);

            return go;
        }

        #endregion

        #region DifficultyTabs

        private static GameObject CreateDifficultyTabs(GameObject parent)
        {
            var go = CreateChild(parent, "DifficultyTabs");
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
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateNormalTab(go);
            CreateHardTab(go);
            CreateHellTab(go);

            return go;
        }

        #endregion

        #region NormalTab

        private static GameObject CreateNormalTab(GameObject parent)
        {
            var go = CreateChild(parent, "NormalTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_3(go);
            CreateSelectionIndicator_1(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "순한맛";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectionIndicator

        private static GameObject CreateSelectionIndicator_1(GameObject parent)
        {
            var go = CreateChild(parent, "SelectionIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 4f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region HardTab

        private static GameObject CreateHardTab(GameObject parent)
        {
            var go = CreateChild(parent, "HardTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 80, 60, 220);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_4(go);
            CreateSelectionIndicator_2(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "매운맛";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectionIndicator

        private static GameObject CreateSelectionIndicator_2(GameObject parent)
        {
            var go = CreateChild(parent, "SelectionIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 4f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region HellTab

        private static GameObject CreateHellTab(GameObject parent)
        {
            var go = CreateChild(parent, "HellTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 80f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(60, 80, 60, 220);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_5(go);
            CreateSelectionIndicator_3(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "핵불맛";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectionIndicator

        private static GameObject CreateSelectionIndicator_3(GameObject parent)
        {
            var go = CreateChild(parent, "SelectionIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 4f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 215, 100, 255);
            image.raycastTarget = true;
            go.SetActive(false);

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

        #region WorldMapButton

        private static GameObject CreateWorldMapButton(GameObject parent)
        {
            var go = CreateChild(parent, "WorldMapButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 180, 255, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_6(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "세계지도";
            tmp.fontSize = 16f;
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

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<StageSelectScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _titleText
            so.FindProperty("_titleText").objectReferenceValue = FindChild(root, "SafeArea/Header/TitleText")?.GetComponent<TextMeshProUGUI>();

            // _entryLimitText
            so.FindProperty("_entryLimitText").objectReferenceValue = FindChild(root, "SafeArea/Header/EntryLimitText")?.GetComponent<TextMeshProUGUI>();

            // _moduleContainer
            so.FindProperty("_moduleContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/ModuleContainer")?.GetComponent<RectTransform>();

            // _chapterSelectWidget
            so.FindProperty("_chapterSelectWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/StageMapArea/ChapterNavigation")?.GetComponent<ChapterSelectWidget>();

            // _stageMapWidget
            so.FindProperty("_stageMapWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/StageMapArea")?.GetComponent<StageMapWidget>();

            // _stageProgressText
            so.FindProperty("_stageProgressText").objectReferenceValue = FindChild(root, "SafeArea/Content/RightTopArea/StageProgressText")?.GetComponent<TextMeshProUGUI>();

            // _starProgressBar
            so.FindProperty("_starProgressBar").objectReferenceValue = FindChild(root, "SafeArea/Content/StarProgressBar")?.GetComponent<StarProgressBarWidget>();

            // _difficultyTabWidget
            so.FindProperty("_difficultyTabWidget").objectReferenceValue = FindChild(root, "SafeArea/Footer/DifficultyTabs")?.GetComponent<DifficultyTabWidget>();

            // _worldMapButton
            so.FindProperty("_worldMapButton").objectReferenceValue = FindChild(root, "SafeArea/Footer/WorldMapButton")?.GetComponent<Button>();

            // _detailPanel
            so.FindProperty("_detailPanel").objectReferenceValue = FindChild(root, "SafeArea/Content/StageMapArea/MapScrollView/Viewport/MapContent/StageInfoBubble");

            // _stageNameText
            so.FindProperty("_stageNameText").objectReferenceValue = FindChild(root, "SafeArea/Content/StageMapArea/MapScrollView/Viewport/MapContent/StageInfoBubble/BubbleStageNameText")?.GetComponent<TextMeshProUGUI>();

            // _recommendedPowerText
            so.FindProperty("_recommendedPowerText").objectReferenceValue = FindChild(root, "SafeArea/Content/StageMapArea/MapScrollView/Viewport/MapContent/StageInfoBubble/RecommendedPowerText")?.GetComponent<TextMeshProUGUI>();

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/BackButton")?.GetComponent<Button>();

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
