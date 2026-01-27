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
    /// CharacterDetailScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/CharacterDetailScreen.prefab
    /// Generated at: 2026-01-27 14:42:49
    /// </summary>
    public static class CharacterDetailScreenPrefabBuilder_Generated
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
        private static readonly Color Color = new Color32(25, 25, 45, 230);
        private static readonly Color Green = new Color32(34, 197, 94, 255);
        private static readonly Color Red = new Color32(255, 200, 100, 255);

        #endregion

        #region Constants

        private const float ACTION_BUTTONS_HEIGHT = 32f;
        private const float ASIDE_MENU_BUTTON_HEIGHT = 50f;
        private const float ATTACK_TYPE_TAG_HEIGHT = 24f;
        private const float ATTACK_TYPE_TAG_WIDTH = 60f;
        private const float BACK_BUTTON_HEIGHT = 40f;
        private const float BACK_BUTTON_WIDTH = 50f;
        private const float BOARD_MENU_BUTTON_HEIGHT = 50f;
        private const float BOTTOM_INFO_AREA_HEIGHT = 100f;
        private const float CENTER_AREA_HEIGHT = 160f;
        private const float CHARACTER_SWITCH_BUTTON_HEIGHT = 80f;
        private const float CHARACTER_SWITCH_BUTTON_WIDTH = 40f;
        private const float COMBAT_POWER_WIDGET_HEIGHT = 48f;
        private const float COMPANION_IMAGE_HEIGHT = 80f;
        private const float COMPANION_IMAGE_WIDTH = 80f;
        private const float COSTUME_ICON_HEIGHT = 48f;
        private const float COSTUME_ICON_WIDTH = 48f;
        private const float DETAIL_BUTTON_HEIGHT = 36f;
        private const float DOGAM_BUTTON_HEIGHT = 32f;
        private const float DOGAM_BUTTON_WIDTH = 80f;
        private const float EQUIPMENT_MENU_BUTTON_HEIGHT = 50f;
        private const float FAVORITE_BUTTON_HEIGHT = 32f;
        private const float FAVORITE_BUTTON_WIDTH = 32f;
        private const float HEADER_HEIGHT = 60f;
        private const float HOME_BUTTON_HEIGHT = 40f;
        private const float HOME_BUTTON_WIDTH = 50f;
        private const float INFO_BUTTON_HEIGHT = 32f;
        private const float INFO_BUTTON_WIDTH = 32f;
        private const float INFO_MENU_BUTTON_HEIGHT = 50f;
        private const float LABEL_TEXT_HEIGHT = 24f;
        private const float LABEL_TEXT_WIDTH = 60f;
        private const float LEFT_MENU_AREA_HEIGHT = 60f;
        private const float LEVEL_TEXT_HEIGHT = 32f;
        private const float LEVEL_TEXT_WIDTH = 100f;
        private const float LEVEL_UP_MENU_BUTTON_HEIGHT = 50f;
        private const float MENU_LIST_HEIGHT = 398f;
        private const float NAME_TEXT_HEIGHT = 32f;
        private const float NAME_TEXT_WIDTH = 200f;
        private const float PERSONALITY_TAG_HEIGHT = 24f;
        private const float PERSONALITY_TAG_WIDTH = 60f;
        private const float POSITION_TAG_HEIGHT = 24f;
        private const float POSITION_TAG_WIDTH = 60f;
        private const float PROMOTION_MENU_BUTTON_HEIGHT = 50f;
        private const float RARITY_BADGE_HEIGHT = 32f;
        private const float RARITY_BADGE_WIDTH = 32f;
        private const float RIGHT_BOTTOM_AREA_HEIGHT = 80f;
        private const float RIGHT_BOTTOM_AREA_WIDTH = 320f;
        private const float RIGHT_CENTER_AREA_HEIGHT = 260f;
        private const float RIGHT_TOP_AREA_HEIGHT = 120f;
        private const float RIGHT_TOP_AREA_WIDTH = 320f;
        private const float ROLE_TAG_HEIGHT = 24f;
        private const float ROLE_TAG_WIDTH = 60f;
        private const float SKILL_MENU_BUTTON_HEIGHT = 50f;
        private const float STAR_0_HEIGHT = 24f;
        private const float STAR_0_WIDTH = 24f;
        private const float STAR_1_HEIGHT = 24f;
        private const float STAR_1_WIDTH = 24f;
        private const float STAR_2_HEIGHT = 24f;
        private const float STAR_2_WIDTH = 24f;
        private const float STAR_3_HEIGHT = 24f;
        private const float STAR_3_WIDTH = 24f;
        private const float STAR_4_HEIGHT = 24f;
        private const float STAR_4_WIDTH = 24f;
        private const float STAR_RATING_CONTAINER_HEIGHT = 24f;
        private const float STAR_RATING_CONTAINER_WIDTH = 136f;
        private const float STAT_LIST_HEIGHT = 136f;
        private const float STAT_ROW_H_P_HEIGHT = 36f;
        private const float STAT_ROW_S_P_HEIGHT = 36f;
        private const float STAT_ROW_마법_공격력_HEIGHT = 36f;
        private const float STAT_ROW_마법_방어력_HEIGHT = 36f;
        private const float STAT_ROW_물리_공격력_HEIGHT = 36f;
        private const float STAT_ROW_물리_방어력_HEIGHT = 36f;
        private const float STAT_TAB_GROUP_HEIGHT = 40f;
        private const float STATUS_TAB_HEIGHT = 100f;
        private const float STATUS_TAB_WIDTH = 100f;
        private const float TAG_GROUP_HEIGHT = 28f;
        private const float TITLE_TEXT_HEIGHT = 32f;
        private const float TITLE_TEXT_WIDTH = 300f;
        private const float TRAIT_TAB_HEIGHT = 100f;
        private const float TRAIT_TAB_WIDTH = 100f;
        private const float VALUE_TEXT_HEIGHT = 32f;
        private const float VALUE_TEXT_WIDTH = 120f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// CharacterDetailScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("CharacterDetailScreen");

            var background = CreateBackground(root);
            var safeArea = CreateSafeArea(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<CharacterDetailScreen>();

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
            image.color = BgDeep;
            image.raycastTarget = false;

            CreateGradientOverlay(go);

            return go;
        }

        #endregion

        #region GradientOverlay

        private static GameObject CreateGradientOverlay(GameObject parent)
        {
            var go = CreateChild(parent, "GradientOverlay");
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
            CreateLeftMenuArea(go);
            CreateCenterArea(go);
            CreateBottomInfoArea(go);
            CreateRightTopArea(go);
            CreateRightCenterArea(go);
            CreateRightBottomArea(go);

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
            image.color = new Color32(25, 25, 45, 230);
            image.raycastTarget = true;

            CreateBackButton(go);
            CreateTitleText_1(go);
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
            rect.sizeDelta = new Vector2(50f, 40f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
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
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TitleText

        private static GameObject CreateTitleText_1(GameObject parent)
        {
            var go = CreateChild(parent, "TitleText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Character Name";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
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
            rect.sizeDelta = new Vector2(50f, 40f);
            rect.anchoredPosition = new Vector2(-16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
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
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region LeftMenuArea

        private static GameObject CreateLeftMenuArea(GameObject parent)
        {
            var go = CreateChild(parent, "LeftMenuArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(100f, -60f);
            rect.anchoredPosition = new Vector2(0f, -30f);

            CreateMenuList(go);

            return go;
        }

        #endregion

        #region MenuList

        private static GameObject CreateMenuList(GameObject parent)
        {
            var go = CreateChild(parent, "MenuList");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 398f);
            rect.anchoredPosition = new Vector2(0f, -16f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(8, 8, 0, 0);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateInfoMenuButton(go);
            CreateLevelUpMenuButton(go);
            CreateEquipmentMenuButton(go);
            CreateSkillMenuButton(go);
            CreatePromotionMenuButton(go);
            CreateBoardMenuButton(go);
            CreateAsideMenuButton(go);

            return go;
        }

        #endregion

        #region InfoMenuButton

        private static GameObject CreateInfoMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "InfoMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_1(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "정보";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region LevelUpMenuButton

        private static GameObject CreateLevelUpMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "LevelUpMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_2(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "레벨업";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EquipmentMenuButton

        private static GameObject CreateEquipmentMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "EquipmentMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_3(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

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
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SkillMenuButton

        private static GameObject CreateSkillMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "SkillMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_4(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "스킬";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PromotionMenuButton

        private static GameObject CreatePromotionMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "PromotionMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_5(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "승급";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BoardMenuButton

        private static GameObject CreateBoardMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "BoardMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_6(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "보드";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region AsideMenuButton

        private static GameObject CreateAsideMenuButton(GameObject parent)
        {
            var go = CreateChild(parent, "AsideMenuButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 80, 200);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateLabel_7(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<MenuButtonWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_7(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "어사이드";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CenterArea

        private static GameObject CreateCenterArea(GameObject parent)
        {
            var go = CreateChild(parent, "CenterArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(100f, 100f);
            rect.offsetMax = new Vector2(-320f, -60f);

            CreateCharacterDisplay(go);
            CreateCharacterSwitchButton(go);
            CreateDogamButton(go);

            return go;
        }

        #endregion

        #region CharacterDisplay

        private static GameObject CreateCharacterDisplay(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterDisplay");
            SetStretch(go);

            CreateCharacterImage(go);
            CreateCompanionImage(go);

            return go;
        }

        #endregion

        #region CharacterImage

        private static GameObject CreateCharacterImage(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CompanionImage

        private static GameObject CreateCompanionImage(GameObject parent)
        {
            var go = CreateChild(parent, "CompanionImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(80f, 80f);
            rect.anchoredPosition = new Vector2(-16f, -16f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CharacterSwitchButton

        private static GameObject CreateCharacterSwitchButton(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterSwitchButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 80f);
            rect.anchoredPosition = new Vector2(-8f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_3(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = ">";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region DogamButton

        private static GameObject CreateDogamButton(GameObject parent)
        {
            var go = CreateChild(parent, "DogamButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 0f);
            rect.pivot = new Vector2(0f, 0f);
            rect.sizeDelta = new Vector2(80f, 32f);
            rect.anchoredPosition = new Vector2(16f, 16f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 30, 50, 200);
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
            tmp.text = "도감";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BottomInfoArea

        private static GameObject CreateBottomInfoArea(GameObject parent)
        {
            var go = CreateChild(parent, "BottomInfoArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0f, 0f);
            rect.sizeDelta = new Vector2(-420f, 100f);
            rect.anchoredPosition = new Vector2(100f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 30, 50, 200);
            image.raycastTarget = false;

            CreateRarityBadge(go);
            CreateNameText(go);
            CreateTagGroup(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<CharacterInfoWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_rarityBadge").objectReferenceValue = go.transform.Find("RarityBadge")?.GetComponent<Image>();
                widgetSo.FindProperty("_rarityText").objectReferenceValue = go.transform.Find("RarityBadge/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_nameText").objectReferenceValue = go.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_tagContainer").objectReferenceValue = go.transform.Find("TagGroup") as RectTransform;
                widgetSo.FindProperty("_personalityTag").objectReferenceValue = go.transform.Find("TagGroup/PersonalityTag")?.GetComponent<Image>();
                widgetSo.FindProperty("_personalityText").objectReferenceValue = go.transform.Find("TagGroup/PersonalityTag/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_roleTag").objectReferenceValue = go.transform.Find("TagGroup/RoleTag")?.GetComponent<Image>();
                widgetSo.FindProperty("_roleText").objectReferenceValue = go.transform.Find("TagGroup/RoleTag/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_attackTypeTag").objectReferenceValue = go.transform.Find("TagGroup/AttackTypeTag")?.GetComponent<Image>();
                widgetSo.FindProperty("_attackTypeText").objectReferenceValue = go.transform.Find("TagGroup/AttackTypeTag/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_positionTag").objectReferenceValue = go.transform.Find("TagGroup/PositionTag")?.GetComponent<Image>();
                widgetSo.FindProperty("_positionText").objectReferenceValue = go.transform.Find("TagGroup/PositionTag/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region RarityBadge

        private static GameObject CreateRarityBadge(GameObject parent)
        {
            var go = CreateChild(parent, "RarityBadge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(32f, 32f);
            rect.anchoredPosition = new Vector2(16f, -16f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
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
            tmp.text = "5";
            tmp.fontSize = 18f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region NameText

        private static GameObject CreateNameText(GameObject parent)
        {
            var go = CreateChild(parent, "NameText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(200f, 32f);
            rect.anchoredPosition = new Vector2(56f, -16f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Character Name";
            tmp.fontSize = 22f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TagGroup

        private static GameObject CreateTagGroup(GameObject parent)
        {
            var go = CreateChild(parent, "TagGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0f, 0f);
            rect.sizeDelta = new Vector2(0f, 28f);
            rect.anchoredPosition = new Vector2(16f, 16f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreatePersonalityTag(go);
            CreateRoleTag(go);
            CreateAttackTypeTag(go);
            CreatePositionTag(go);

            return go;
        }

        #endregion

        #region PersonalityTag

        private static GameObject CreatePersonalityTag(GameObject parent)
        {
            var go = CreateChild(parent, "PersonalityTag");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 165, 80, 255);
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
            tmp.text = "활발";
            tmp.fontSize = 12f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RoleTag

        private static GameObject CreateRoleTag(GameObject parent)
        {
            var go = CreateChild(parent, "RoleTag");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 150, 200, 255);
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
            tmp.text = "서포터";
            tmp.fontSize = 12f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region AttackTypeTag

        private static GameObject CreateAttackTypeTag(GameObject parent)
        {
            var go = CreateChild(parent, "AttackTypeTag");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 220, 100, 255);
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
            tmp.text = "물리";
            tmp.fontSize = 12f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PositionTag

        private static GameObject CreatePositionTag(GameObject parent)
        {
            var go = CreateChild(parent, "PositionTag");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 100, 100, 255);
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
            tmp.text = "후열";
            tmp.fontSize = 12f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

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
            rect.sizeDelta = new Vector2(320f, 120f);
            rect.anchoredPosition = new Vector2(0f, -60f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 30, 50, 200);
            image.raycastTarget = false;

            CreateLevelText(go);
            CreateStarRatingContainer(go);
            CreateCombatPowerWidget(go);

            return go;
        }

        #endregion

        #region LevelText

        private static GameObject CreateLevelText(GameObject parent)
        {
            var go = CreateChild(parent, "LevelText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(100f, 32f);
            rect.anchoredPosition = new Vector2(16f, -12f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Lv. 52";
            tmp.fontSize = 20f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StarRatingContainer

        private static GameObject CreateStarRatingContainer(GameObject parent)
        {
            var go = CreateChild(parent, "StarRatingContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(136f, 24f);
            rect.anchoredPosition = new Vector2(120f, -16f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 128);
            image.raycastTarget = true;

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region CombatPowerWidget

        private static GameObject CreateCombatPowerWidget(GameObject parent)
        {
            var go = CreateChild(parent, "CombatPowerWidget");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(-32f, 48f);
            rect.anchoredPosition = new Vector2(0f, 12f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(34, 197, 94, 255);
            image.raycastTarget = true;

            CreateLabelText(go);
            CreateValueText(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<CombatPowerWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_labelText").objectReferenceValue = go.transform.Find("LabelText")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_valueText").objectReferenceValue = go.transform.Find("ValueText")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region LabelText

        private static GameObject CreateLabelText(GameObject parent)
        {
            var go = CreateChild(parent, "LabelText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 24f);
            rect.anchoredPosition = new Vector2(12f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "전투력";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ValueText

        private static GameObject CreateValueText(GameObject parent)
        {
            var go = CreateChild(parent, "ValueText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(120f, 32f);
            rect.anchoredPosition = new Vector2(-12f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "25,555";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RightCenterArea

        private static GameObject CreateRightCenterArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightCenterArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(320f, -260f);
            rect.anchoredPosition = new Vector2(0f, -50f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(25, 25, 45, 230);
            image.raycastTarget = false;

            CreateStatTabGroup(go);
            CreateStatList(go);
            CreateActionButtons(go);
            CreateDetailButton(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<CharacterStatWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_statusTab").objectReferenceValue = go.transform.Find("StatTabGroup/StatusTab")?.GetComponent<Button>();
                widgetSo.FindProperty("_statusTabText").objectReferenceValue = go.transform.Find("StatTabGroup/StatusTab/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_statusTabBg").objectReferenceValue = go.transform.Find("StatTabGroup/StatusTab")?.GetComponent<Image>();
                widgetSo.FindProperty("_traitTab").objectReferenceValue = go.transform.Find("StatTabGroup/TraitTab")?.GetComponent<Button>();
                widgetSo.FindProperty("_traitTabText").objectReferenceValue = go.transform.Find("StatTabGroup/TraitTab/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_traitTabBg").objectReferenceValue = go.transform.Find("StatTabGroup/TraitTab")?.GetComponent<Image>();
                widgetSo.FindProperty("_statListContainer").objectReferenceValue = go.transform.Find("StatList") as RectTransform;
                widgetSo.FindProperty("_hpValue").objectReferenceValue = go.transform.Find("StatList/StatRow_HP/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_spValue").objectReferenceValue = go.transform.Find("StatList/StatRow_SP/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_physicalAttackValue").objectReferenceValue = go.transform.Find("StatList/StatRow_물리 공격력/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_magicAttackValue").objectReferenceValue = go.transform.Find("StatList/StatRow_마법 공격력/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_physicalDefenseValue").objectReferenceValue = go.transform.Find("StatList/StatRow_물리 방어력/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_magicDefenseValue").objectReferenceValue = go.transform.Find("StatList/StatRow_마법 방어력/Value")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_favoriteButton").objectReferenceValue = go.transform.Find("ActionButtons/FavoriteButton")?.GetComponent<Button>();
                widgetSo.FindProperty("_favoriteIcon").objectReferenceValue = go.transform.Find("ActionButtons/FavoriteButton")?.GetComponent<Image>();
                widgetSo.FindProperty("_infoButton").objectReferenceValue = go.transform.Find("ActionButtons/InfoButton")?.GetComponent<Button>();
                widgetSo.FindProperty("_detailButton").objectReferenceValue = go.transform.Find("DetailButton")?.GetComponent<Button>();
                widgetSo.FindProperty("_detailButtonText").objectReferenceValue = go.transform.Find("DetailButton/Text")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region StatTabGroup

        private static GameObject CreateStatTabGroup(GameObject parent)
        {
            var go = CreateChild(parent, "StatTabGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(-16f, 40f);
            rect.anchoredPosition = new Vector2(0f, -8f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateStatusTab(go);
            CreateTraitTab(go);

            return go;
        }

        #endregion

        #region StatusTab

        private static GameObject CreateStatusTab(GameObject parent)
        {
            var go = CreateChild(parent, "StatusTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(240, 240, 240, 255);
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
            tmp.text = "스테이터스";
            tmp.fontSize = 14f;
            tmp.color = new Color32(30, 30, 30, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region TraitTab

        private static GameObject CreateTraitTab(GameObject parent)
        {
            var go = CreateChild(parent, "TraitTab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 100, 100, 200);
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
            tmp.text = "특성";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatList

        private static GameObject CreateStatList(GameObject parent)
        {
            var go = CreateChild(parent, "StatList");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(8f, 80f);
            rect.offsetMax = new Vector2(-8f, -56f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStatRow_HP(go);
            CreateStatRow_SP(go);
            CreateStatRow_물리공격력(go);
            CreateStatRow_마법공격력(go);
            CreateStatRow_물리방어력(go);
            CreateStatRow_마법방어력(go);

            return go;
        }

        #endregion

        #region StatRow_HP

        private static GameObject CreateStatRow_HP(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_HP");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_8(go);
            CreateValue_1(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_8(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "HP";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_1(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "7,321";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatRow_SP

        private static GameObject CreateStatRow_SP(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_SP");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_9(go);
            CreateValue_2(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_9(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "SP";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_2(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "400";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatRow_물리 공격력

        private static GameObject CreateStatRow_물리공격력(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_물리 공격력");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_10(go);
            CreateValue_3(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_10(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "물리 공격력";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_3(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1,234";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatRow_마법 공격력

        private static GameObject CreateStatRow_마법공격력(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_마법 공격력");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_11(go);
            CreateValue_4(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_11(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "마법 공격력";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_4(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "567";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatRow_물리 방어력

        private static GameObject CreateStatRow_물리방어력(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_물리 방어력");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_12(go);
            CreateValue_5(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_12(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "물리 방어력";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_5(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "890";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region StatRow_마법 방어력

        private static GameObject CreateStatRow_마법방어력(GameObject parent)
        {
            var go = CreateChild(parent, "StatRow_마법 방어력");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 36f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 51);
            image.raycastTarget = true;

            CreateLabel_13(go);
            CreateValue_6(go);

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_13(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "마법 방어력";
            tmp.fontSize = 14f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_6(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-8f, 0f);
            rect.anchoredPosition = new Vector2(-4f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "678";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineRight;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
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
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(-16f, 32f);
            rect.anchoredPosition = new Vector2(0f, 44f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateFavoriteButton(go);
            CreateInfoButton(go);

            return go;
        }

        #endregion

        #region FavoriteButton

        private static GameObject CreateFavoriteButton(GameObject parent)
        {
            var go = CreateChild(parent, "FavoriteButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(32f, 32f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_4(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_4(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
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

        #region InfoButton

        private static GameObject CreateInfoButton(GameObject parent)
        {
            var go = CreateChild(parent, "InfoButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(32f, 32f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_5(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_5(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "i";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region DetailButton

        private static GameObject CreateDetailButton(GameObject parent)
        {
            var go = CreateChild(parent, "DetailButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(-16f, 36f);
            rect.anchoredPosition = new Vector2(0f, 8f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(30, 30, 50, 200);
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
            tmp.text = "상세 보기";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RightBottomArea

        private static GameObject CreateRightBottomArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightBottomArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(1f, 0f);
            rect.sizeDelta = new Vector2(320f, 80f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(200, 220, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateCostumeIcon(go);
            CreateTitleText_2(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<CostumeWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_costumeIcon").objectReferenceValue = go.transform.Find("CostumeIcon")?.GetComponent<Image>();
                widgetSo.FindProperty("_titleText").objectReferenceValue = go.transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region CostumeIcon

        private static GameObject CreateCostumeIcon(GameObject parent)
        {
            var go = CreateChild(parent, "CostumeIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(48f, 48f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 128);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region TitleText

        private static GameObject CreateTitleText_2(GameObject parent)
        {
            var go = CreateChild(parent, "TitleText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(-80f, 32f);
            rect.anchoredPosition = new Vector2(72f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "캐릭터의 옷장";
            tmp.fontSize = 16f;
            tmp.color = new Color32(50, 80, 20, 255);
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
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
            var component = root.GetComponent<CharacterDetailScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/BackButton")?.GetComponent<Button>();

            // _titleText
            so.FindProperty("_titleText").objectReferenceValue = FindChild(root, "SafeArea/Header/TitleText")?.GetComponent<TextMeshProUGUI>();

            // _homeButton
            so.FindProperty("_homeButton").objectReferenceValue = FindChild(root, "SafeArea/Header/HomeButton")?.GetComponent<Button>();

            // _menuButtonContainer
            so.FindProperty("_menuButtonContainer").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList")?.GetComponent<RectTransform>();

            // _infoMenuButton
            so.FindProperty("_infoMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/InfoMenuButton")?.GetComponent<MenuButtonWidget>();

            // _levelUpMenuButton
            so.FindProperty("_levelUpMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/LevelUpMenuButton")?.GetComponent<MenuButtonWidget>();

            // _equipmentMenuButton
            so.FindProperty("_equipmentMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/EquipmentMenuButton")?.GetComponent<MenuButtonWidget>();

            // _skillMenuButton
            so.FindProperty("_skillMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/SkillMenuButton")?.GetComponent<MenuButtonWidget>();

            // _promotionMenuButton
            so.FindProperty("_promotionMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/PromotionMenuButton")?.GetComponent<MenuButtonWidget>();

            // _boardMenuButton
            so.FindProperty("_boardMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/BoardMenuButton")?.GetComponent<MenuButtonWidget>();

            // _asideMenuButton
            so.FindProperty("_asideMenuButton").objectReferenceValue = FindChild(root, "SafeArea/LeftMenuArea/MenuList/AsideMenuButton")?.GetComponent<MenuButtonWidget>();

            // _characterImage
            so.FindProperty("_characterImage").objectReferenceValue = FindChild(root, "SafeArea/CenterArea/CharacterDisplay/CharacterImage")?.GetComponent<Image>();

            // _companionImage
            so.FindProperty("_companionImage").objectReferenceValue = FindChild(root, "SafeArea/CenterArea/CharacterDisplay/CompanionImage")?.GetComponent<Image>();

            // _characterSwitchButton
            so.FindProperty("_characterSwitchButton").objectReferenceValue = FindChild(root, "SafeArea/CenterArea/CharacterSwitchButton")?.GetComponent<Button>();

            // _dogamButton
            so.FindProperty("_dogamButton").objectReferenceValue = FindChild(root, "SafeArea/CenterArea/DogamButton")?.GetComponent<Button>();

            // _characterInfoWidget
            so.FindProperty("_characterInfoWidget").objectReferenceValue = FindChild(root, "SafeArea/BottomInfoArea")?.GetComponent<CharacterInfoWidget>();

            // _rarityBadge
            so.FindProperty("_rarityBadge").objectReferenceValue = FindChild(root, "SafeArea/BottomInfoArea/RarityBadge")?.GetComponent<Image>();

            // _rarityText
            so.FindProperty("_rarityText").objectReferenceValue = FindChild(root, "SafeArea/BottomInfoArea/RarityBadge/Text")?.GetComponent<TextMeshProUGUI>();

            // _nameText
            so.FindProperty("_nameText").objectReferenceValue = FindChild(root, "SafeArea/BottomInfoArea/NameText")?.GetComponent<TextMeshProUGUI>();

            // _levelText
            so.FindProperty("_levelText").objectReferenceValue = FindChild(root, "SafeArea/RightTopArea/LevelText")?.GetComponent<TextMeshProUGUI>();

            // _starRatingContainer
            so.FindProperty("_starRatingContainer").objectReferenceValue = FindChild(root, "SafeArea/RightTopArea/StarRatingContainer")?.GetComponent<RectTransform>();

            // _combatPowerWidget
            so.FindProperty("_combatPowerWidget").objectReferenceValue = FindChild(root, "SafeArea/RightTopArea/CombatPowerWidget")?.GetComponent<CombatPowerWidget>();

            // _characterStatWidget
            so.FindProperty("_characterStatWidget").objectReferenceValue = FindChild(root, "SafeArea/RightCenterArea")?.GetComponent<CharacterStatWidget>();

            // _costumeWidget
            so.FindProperty("_costumeWidget").objectReferenceValue = FindChild(root, "SafeArea/RightBottomArea")?.GetComponent<CostumeWidget>();

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
