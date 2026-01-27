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
    /// PartySelectScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/PartySelectScreen.prefab
    /// Generated at: 2026-01-27 14:42:50
    /// </summary>
    public static class PartySelectScreenPrefabBuilder_Generated
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
        private static readonly Color BgGlass = new Color32(255, 255, 255, 26);
        private static readonly Color Blue = new Color32(80, 140, 255, 255);
        private static readonly Color Color = new Color32(40, 45, 60, 180);
        private static readonly Color Green = new Color32(100, 220, 140, 255);
        private static readonly Color Red = new Color32(255, 160, 60, 255);

        #endregion

        #region Constants

        private const float ACTION_BAR_HEIGHT = 80f;
        private const float AUTO_FORM_BUTTON_HEIGHT = 100f;
        private const float AUTO_FORM_BUTTON_WIDTH = 100f;
        private const float AUTO_TOGGLE_BUTTON_HEIGHT = 100f;
        private const float AUTO_TOGGLE_BUTTON_WIDTH = 100f;
        private const float BACK_BUTTON_HEIGHT = 60f;
        private const float BACK_BUTTON_WIDTH = 60f;
        private const float BACK_LINE_CONTAINER_HEIGHT = 90f;
        private const float BACK_LINE_CONTAINER_WIDTH = 300f;
        private const float BATTLE_PREVIEW_AREA_HEIGHT = 400f;
        private const float BATTLE_PREVIEW_AREA_WIDTH = 600f;
        private const float BATTLE_PREVIEW_VISUAL_HEIGHT = 300f;
        private const float BATTLE_PREVIEW_VISUAL_WIDTH = 600f;
        private const float CHARACTER_COUNT_TEXT_HEIGHT = 20f;
        private const float CHARACTER_LIST_CONTAINER_HEIGHT = 600f;
        private const float CHARACTER_SCROLL_VIEW_HEIGHT = 130f;
        private const float CHARACTER_SELECT_WIDGET_HEIGHT = 100f;
        private const float CHARACTER_SELECT_WIDGET_WIDTH = 200f;
        private const float CHARACTER_SLOT0_HEIGHT = 130f;
        private const float CHARACTER_SLOT0_WIDTH = 100f;
        private const float CHARACTER_SLOT1_HEIGHT = 130f;
        private const float CHARACTER_SLOT1_WIDTH = 100f;
        private const float CHARACTER_SLOT2_HEIGHT = 130f;
        private const float CHARACTER_SLOT2_WIDTH = 100f;
        private const float CHARACTER_SLOT3_HEIGHT = 130f;
        private const float CHARACTER_SLOT3_WIDTH = 100f;
        private const float CHARACTER_SLOT4_HEIGHT = 130f;
        private const float CHARACTER_SLOT4_WIDTH = 100f;
        private const float CHARACTER_SLOT5_HEIGHT = 130f;
        private const float CHARACTER_SLOT5_WIDTH = 100f;
        private const float CHARACTER_SLOT6_HEIGHT = 130f;
        private const float CHARACTER_SLOT6_WIDTH = 100f;
        private const float CHARACTER_SLOT7_HEIGHT = 130f;
        private const float CHARACTER_SLOT7_WIDTH = 100f;
        private const float CHARACTER_SLOT8_HEIGHT = 130f;
        private const float CHARACTER_SLOT8_WIDTH = 100f;
        private const float CONTENT_HEIGHT = 80f;
        private const float CURRENCY_H_U_D_HEIGHT = 40f;
        private const float CURRENCY_H_U_D_WIDTH = 200f;
        private const float ENEMY_FORMATION_HEIGHT = 200f;
        private const float ENEMY_FORMATION_WIDTH = 200f;
        private const float FILTER_TAB_HEIGHT = 100f;
        private const float FILTER_TAB_WIDTH = 100f;
        private const float FORMATION_SETTING_BUTTON_HEIGHT = 100f;
        private const float FORMATION_SETTING_BUTTON_WIDTH = 100f;
        private const float FRONT_LINE_CONTAINER_HEIGHT = 90f;
        private const float FRONT_LINE_CONTAINER_WIDTH = 300f;
        private const float HEADER_HEIGHT = 80f;
        private const float HOME_BUTTON_HEIGHT = 60f;
        private const float HOME_BUTTON_WIDTH = 60f;
        private const float INFO_BAR_HEIGHT = 25f;
        private const float PARTY_FORMATION_HEIGHT = 200f;
        private const float PARTY_FORMATION_WIDTH = 250f;
        private const float PARTY_POWER_TEXT_HEIGHT = 20f;
        private const float PARTY_SLOT_CONTAINER_HEIGHT = 100f;
        private const float PARTY_SLOT_CONTAINER_WIDTH = 600f;
        private const float PORTRAIT_HEIGHT = 90f;
        private const float PORTRAIT_WIDTH = 90f;
        private const float POWER_TAB_HEIGHT = 100f;
        private const float POWER_TAB_WIDTH = 100f;
        private const float QUICK_ACTION_BAR_HEIGHT = 60f;
        private const float QUICK_BATTLE_BUTTON_HEIGHT = 100f;
        private const float QUICK_BATTLE_BUTTON_WIDTH = 100f;
        private const float RECOMMENDED_POWER_TEXT_HEIGHT = 20f;
        private const float RENTAL_TAB_HEIGHT = 100f;
        private const float RENTAL_TAB_WIDTH = 100f;
        private const float SLOT0_HEIGHT = 90f;
        private const float SLOT0_WIDTH = 90f;
        private const float SLOT1_HEIGHT = 90f;
        private const float SLOT1_WIDTH = 90f;
        private const float SLOT2_HEIGHT = 90f;
        private const float SLOT2_WIDTH = 90f;
        private const float STAGE_DIFFICULTY_TEXT_HEIGHT = 30f;
        private const float STAGE_DIFFICULTY_TEXT_WIDTH = 400f;
        private const float STAGE_INFO_BUTTON_HEIGHT = 100f;
        private const float STAGE_INFO_BUTTON_WIDTH = 100f;
        private const float STAGE_INFO_CONTAINER_HEIGHT = 60f;
        private const float STAGE_INFO_CONTAINER_WIDTH = 400f;
        private const float STAGE_INFO_PANEL_HEIGHT = 120f;
        private const float STAGE_INFO_PANEL_WIDTH = 300f;
        private const float STAGE_INFO_WIDGET_HEIGHT = 100f;
        private const float STAGE_INFO_WIDGET_WIDTH = 200f;
        private const float STAGE_NAME_TEXT_HEIGHT = 30f;
        private const float STAGE_NAME_TEXT_WIDTH = 400f;
        private const float STAMINA_COST_TEXT_HEIGHT = 20f;
        private const float START_BUTTON_HEIGHT = 100f;
        private const float START_BUTTON_WIDTH = 100f;
        private const float TAB_BAR_HEIGHT = 50f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// PartySelectScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("PartySelectScreen");

            var background = CreateBackground(root);
            var safeArea = CreateSafeArea(root);

            // Add main component
            root.AddComponent<PartySelectScreen>();

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
            image.color = new Color32(8, 10, 18, 255);
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
            CreateStageInfoPanel(go);
            CreateStageInfoWidget(go);

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
            rect.sizeDelta = new Vector2(0f, 80f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(15, 18, 28, 242);
            image.raycastTarget = true;

            CreateBackButton(go);
            CreateHomeButton(go);
            CreateStageInfoContainer(go);
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
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(20f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
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
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
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
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(-20f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
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
            tmp.text = "H";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageInfoContainer

        private static GameObject CreateStageInfoContainer(GameObject parent)
        {
            var go = CreateChild(parent, "StageInfoContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateStageNameText(go);
            CreateStageDifficultyText(go);

            return go;
        }

        #endregion

        #region StageNameText

        private static GameObject CreateStageNameText(GameObject parent)
        {
            var go = CreateChild(parent, "StageNameText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 30f);
            rect.anchoredPosition = new Vector2(0f, 5f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10-7. 깜빡이는 터널!";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageDifficultyText

        private static GameObject CreateStageDifficultyText(GameObject parent)
        {
            var go = CreateChild(parent, "StageDifficultyText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 30f);
            rect.anchoredPosition = new Vector2(0f, -15f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Hard";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 160, 60, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
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
            rect.sizeDelta = new Vector2(200f, 40f);
            rect.anchoredPosition = new Vector2(-80f, 0f);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -80f);

            CreateLeftArea(go);
            CreateRightPanel(go);

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
            rect.anchorMax = new Vector2(0.6f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateBattlePreviewArea(go);
            CreateBattlePreviewVisual(go);
            CreateQuickActionBar(go);
            CreatePartySlotContainer(go);

            return go;
        }

        #endregion

        #region BattlePreviewArea

        private static GameObject CreateBattlePreviewArea(GameObject parent)
        {
            var go = CreateChild(parent, "BattlePreviewArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 400f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateFrontLineContainer(go);
            CreateBackLineContainer(go);

            return go;
        }

        #endregion

        #region FrontLineContainer

        private static GameObject CreateFrontLineContainer(GameObject parent)
        {
            var go = CreateChild(parent, "FrontLineContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 90f);
            rect.anchoredPosition = new Vector2(0f, 50f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateSlot0_1(go);
            CreateSlot1_1(go);
            CreateSlot2_1(go);

            return go;
        }

        #endregion

        #region Slot0

        private static GameObject CreateSlot0_1(GameObject parent)
        {
            var go = CreateChild(parent, "Slot0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Slot1

        private static GameObject CreateSlot1_1(GameObject parent)
        {
            var go = CreateChild(parent, "Slot1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Slot2

        private static GameObject CreateSlot2_1(GameObject parent)
        {
            var go = CreateChild(parent, "Slot2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region BackLineContainer

        private static GameObject CreateBackLineContainer(GameObject parent)
        {
            var go = CreateChild(parent, "BackLineContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 90f);
            rect.anchoredPosition = new Vector2(0f, -50f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateSlot0_2(go);
            CreateSlot1_2(go);
            CreateSlot2_2(go);

            return go;
        }

        #endregion

        #region Slot0

        private static GameObject CreateSlot0_2(GameObject parent)
        {
            var go = CreateChild(parent, "Slot0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Slot1

        private static GameObject CreateSlot1_2(GameObject parent)
        {
            var go = CreateChild(parent, "Slot1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region Slot2

        private static GameObject CreateSlot2_2(GameObject parent)
        {
            var go = CreateChild(parent, "Slot2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region BattlePreviewVisual

        private static GameObject CreateBattlePreviewVisual(GameObject parent)
        {
            var go = CreateChild(parent, "BattlePreviewVisual");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.6f);
            rect.anchorMax = new Vector2(0.5f, 0.6f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 300f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreatePartyFormation(go);
            CreateEnemyFormation(go);

            return go;
        }

        #endregion

        #region PartyFormation

        private static GameObject CreatePartyFormation(GameObject parent)
        {
            var go = CreateChild(parent, "PartyFormation");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(250f, 200f);
            rect.anchoredPosition = new Vector2(50f, 0f);

            return go;
        }

        #endregion

        #region EnemyFormation

        private static GameObject CreateEnemyFormation(GameObject parent)
        {
            var go = CreateChild(parent, "EnemyFormation");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 200f);
            rect.anchoredPosition = new Vector2(-50f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 90, 90, 76);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region QuickActionBar

        private static GameObject CreateQuickActionBar(GameObject parent)
        {
            var go = CreateChild(parent, "QuickActionBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 20f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(20, 20, 0, 0);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateAutoFormButton(go);
            CreateStageInfoButton(go);
            CreateFormationSettingButton(go);

            return go;
        }

        #endregion

        #region AutoFormButton

        private static GameObject CreateAutoFormButton(GameObject parent)
        {
            var go = CreateChild(parent, "AutoFormButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 102, 128, 255);
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
            tmp.text = "일괄 해제";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageInfoButton

        private static GameObject CreateStageInfoButton(GameObject parent)
        {
            var go = CreateChild(parent, "StageInfoButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 102, 128, 255);
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
            tmp.text = "스테이지 정보";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region FormationSettingButton

        private static GameObject CreateFormationSettingButton(GameObject parent)
        {
            var go = CreateChild(parent, "FormationSettingButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 102, 128, 255);
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
            tmp.text = "덱 설정";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PartySlotContainer

        private static GameObject CreatePartySlotContainer(GameObject parent)
        {
            var go = CreateChild(parent, "PartySlotContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.4f);
            rect.anchorMax = new Vector2(0.5f, 0.4f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            return go;
        }

        #endregion

        #region RightPanel

        private static GameObject CreateRightPanel(GameObject parent)
        {
            var go = CreateChild(parent, "RightPanel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.6f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(15, 18, 28, 128);
            image.raycastTarget = true;

            CreateTabBar(go);
            CreateCharacterScrollView(go);
            CreateActionBar(go);
            CreateCharacterSelectWidget(go);

            return go;
        }

        #endregion

        #region TabBar

        private static GameObject CreateTabBar(GameObject parent)
        {
            var go = CreateChild(parent, "TabBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateRentalTab(go);
            CreateFilterTab(go);
            CreatePowerTab(go);

            return go;
        }

        #endregion

        #region RentalTab

        private static GameObject CreateRentalTab(GameObject parent)
        {
            var go = CreateChild(parent, "RentalTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
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
            tmp.text = "대여";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region FilterTab

        private static GameObject CreateFilterTab(GameObject parent)
        {
            var go = CreateChild(parent, "FilterTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
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
            tmp.text = "필터OFF";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PowerTab

        private static GameObject CreatePowerTab(GameObject parent)
        {
            var go = CreateChild(parent, "PowerTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_8(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_8(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "전투력";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterScrollView

        private static GameObject CreateCharacterScrollView(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterScrollView");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 80f);
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


            CreateCharacterListContainer(go);

            return go;
        }

        #endregion

        #region CharacterListContainer

        private static GameObject CreateCharacterListContainer(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterListContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 600f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var grid = go.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(100f, 130f);
            grid.spacing = new Vector2(10f, 10f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperLeft;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 3;
            grid.padding = new RectOffset(10, 10, 10, 10);

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            CreateCharacterSlot0(go);
            CreateCharacterSlot1(go);
            CreateCharacterSlot2(go);
            CreateCharacterSlot3(go);
            CreateCharacterSlot4(go);
            CreateCharacterSlot5(go);
            CreateCharacterSlot6(go);
            CreateCharacterSlot7(go);
            CreateCharacterSlot8(go);

            return go;
        }

        #endregion

        #region CharacterSlot0

        private static GameObject CreateCharacterSlot0(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_1(go);
            CreateInfoBar_1(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_1(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_1(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot1

        private static GameObject CreateCharacterSlot1(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_2(go);
            CreateInfoBar_2(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_2(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_2(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot2

        private static GameObject CreateCharacterSlot2(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_3(go);
            CreateInfoBar_3(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_3(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_3(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot3

        private static GameObject CreateCharacterSlot3(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_4(go);
            CreateInfoBar_4(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_4(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_4(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot4

        private static GameObject CreateCharacterSlot4(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot4");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_5(go);
            CreateInfoBar_5(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_5(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_5(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot5

        private static GameObject CreateCharacterSlot5(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot5");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_6(go);
            CreateInfoBar_6(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_6(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_6(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot6

        private static GameObject CreateCharacterSlot6(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot6");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_7(go);
            CreateInfoBar_7(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_7(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_7(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot7

        private static GameObject CreateCharacterSlot7(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot7");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_8(go);
            CreateInfoBar_8(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_8(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_8(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSlot8

        private static GameObject CreateCharacterSlot8(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSlot8");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 130f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(40, 45, 60, 180);
            image.raycastTarget = true;

            CreatePortrait_9(go);
            CreateInfoBar_9(go);

            return go;
        }

        #endregion

        #region Portrait

        private static GameObject CreatePortrait_9(GameObject parent)
        {
            var go = CreateChild(parent, "Portrait");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(90f, 90f);
            rect.anchoredPosition = new Vector2(0f, -5f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 76, 102, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoBar

        private static GameObject CreateInfoBar_9(GameObject parent)
        {
            var go = CreateChild(parent, "InfoBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 25f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv.1";
            tmp.fontSize = 10f;
            tmp.color = new Color32(255, 255, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ActionBar

        private static GameObject CreateActionBar(GameObject parent)
        {
            var go = CreateChild(parent, "ActionBar");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 80f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(15, 15, 15, 15);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateQuickBattleButton(go);
            CreateStartButton(go);
            CreateAutoToggleButton(go);

            return go;
        }

        #endregion

        #region QuickBattleButton

        private static GameObject CreateQuickBattleButton(GameObject parent)
        {
            var go = CreateChild(parent, "QuickBattleButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 140, 255, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_9(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_9(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "소탕";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StartButton

        private static GameObject CreateStartButton(GameObject parent)
        {
            var go = CreateChild(parent, "StartButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 220, 140, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_10(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_10(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "출발";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region AutoToggleButton

        private static GameObject CreateAutoToggleButton(GameObject parent)
        {
            var go = CreateChild(parent, "AutoToggleButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 160, 60, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText_11(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText_11(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "자동";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterSelectWidget

        private static GameObject CreateCharacterSelectWidget(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSelectWidget");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 255, 26);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region StageInfoPanel

        private static GameObject CreateStageInfoPanel(GameObject parent)
        {
            var go = CreateChild(parent, "StageInfoPanel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(300f, 120f);
            rect.anchoredPosition = new Vector2(0f, -100f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(22, 26, 38, 255);
            image.raycastTarget = true;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 5f;
            layout.padding = new RectOffset(15, 15, 15, 15);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateStaminaCostText(go);
            CreateRecommendedPowerText(go);
            CreateCharacterCountText(go);
            CreatePartyPowerText(go);

            return go;
        }

        #endregion

        #region StaminaCostText

        private static GameObject CreateStaminaCostText(GameObject parent)
        {
            var go = CreateChild(parent, "StaminaCostText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "입장 비용: 30";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

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
            rect.sizeDelta = new Vector2(0f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "권장 전투력: 123,188";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterCountText

        private static GameObject CreateCharacterCountText(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterCountText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "편성된 사도: 6/6";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PartyPowerText

        private static GameObject CreatePartyPowerText(GameObject parent)
        {
            var go = CreateChild(parent, "PartyPowerText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "카드: 24/24";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StageInfoWidget

        private static GameObject CreateStageInfoWidget(GameObject parent)
        {
            var go = CreateChild(parent, "StageInfoWidget");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 0, 255, 26);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<PartySelectScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _frontLineContainer
            so.FindProperty("_frontLineContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/BattlePreviewArea/FrontLineContainer")?.GetComponent<RectTransform>();

            // _backLineContainer
            so.FindProperty("_backLineContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/BattlePreviewArea/BackLineContainer")?.GetComponent<RectTransform>();

            // _battlePreviewArea
            so.FindProperty("_battlePreviewArea").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/BattlePreviewVisual")?.GetComponent<RectTransform>();

            // _partyFormation
            so.FindProperty("_partyFormation").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/BattlePreviewVisual/PartyFormation")?.GetComponent<RectTransform>();

            // _enemyFormation
            so.FindProperty("_enemyFormation").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/BattlePreviewVisual/EnemyFormation")?.GetComponent<RectTransform>();

            // _stageNameText
            so.FindProperty("_stageNameText").objectReferenceValue = FindChild(root, "SafeArea/Header/StageInfoContainer/StageNameText")?.GetComponent<TextMeshProUGUI>();

            // _stageDifficultyText
            so.FindProperty("_stageDifficultyText").objectReferenceValue = FindChild(root, "SafeArea/Header/StageInfoContainer/StageDifficultyText")?.GetComponent<TextMeshProUGUI>();

            // _recommendedPowerText
            so.FindProperty("_recommendedPowerText").objectReferenceValue = FindChild(root, "SafeArea/StageInfoPanel/RecommendedPowerText")?.GetComponent<TextMeshProUGUI>();

            // _partySlotContainer
            so.FindProperty("_partySlotContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/PartySlotContainer")?.GetComponent<RectTransform>();

            // _partyPowerText
            so.FindProperty("_partyPowerText").objectReferenceValue = FindChild(root, "SafeArea/StageInfoPanel/PartyPowerText")?.GetComponent<TextMeshProUGUI>();

            // _characterListContainer
            so.FindProperty("_characterListContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/RightPanel/CharacterScrollView/Viewport/CharacterListContainer")?.GetComponent<RectTransform>();

            // _characterCountText
            so.FindProperty("_characterCountText").objectReferenceValue = FindChild(root, "SafeArea/StageInfoPanel/CharacterCountText")?.GetComponent<TextMeshProUGUI>();

            // _autoFormButton
            so.FindProperty("_autoFormButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/QuickActionBar/AutoFormButton")?.GetComponent<Button>();

            // _stageInfoButton
            so.FindProperty("_stageInfoButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/QuickActionBar/StageInfoButton")?.GetComponent<Button>();

            // _formationSettingButton
            so.FindProperty("_formationSettingButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/QuickActionBar/FormationSettingButton")?.GetComponent<Button>();

            // _staminaCostText
            so.FindProperty("_staminaCostText").objectReferenceValue = FindChild(root, "SafeArea/StageInfoPanel/StaminaCostText")?.GetComponent<TextMeshProUGUI>();

            // _startButton
            so.FindProperty("_startButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightPanel/ActionBar/StartButton")?.GetComponent<Button>();

            // _quickBattleButton
            so.FindProperty("_quickBattleButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightPanel/ActionBar/QuickBattleButton")?.GetComponent<Button>();

            // _autoToggleButton
            so.FindProperty("_autoToggleButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightPanel/ActionBar/AutoToggleButton")?.GetComponent<Button>();

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
