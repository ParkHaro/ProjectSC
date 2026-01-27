using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Gacha;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// GachaScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/GachaScreen.prefab
    /// Generated at: 2026-01-27 14:42:50
    /// </summary>
    public static class GachaScreenPrefabBuilder_Generated
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
        private static readonly Color AccentPurple = new Color32(168, 85, 247, 255);
        private static readonly Color AccentSecondary = new Color32(255, 107, 157, 255);
        private static readonly Color BgGlass = new Color32(0, 212, 255, 50);
        private static readonly Color Color = new Color32(25, 25, 45, 217);
        private static readonly Color Green = new Color32(76, 217, 100, 255);
        private static readonly Color Red = new Color32(255, 195, 0, 255);

        #endregion

        #region Constants

        private const float BACK_BUTTON_HEIGHT = 44f;
        private const float BACK_BUTTON_WIDTH = 44f;
        private const float BANNER_CAROUSEL_HEIGHT = 120f;
        private const float BANNER_DESCRIPTION_HEIGHT = 100f;
        private const float BANNER_INFO_AREA_HEIGHT = 180f;
        private const float BANNER_PERIOD_HEIGHT = 24f;
        private const float BANNER_SLOT_0_WIDTH = 180f;
        private const float BANNER_SLOT_1_WIDTH = 180f;
        private const float BANNER_SLOT_2_WIDTH = 180f;
        private const float BANNER_SLOT_3_WIDTH = 180f;
        private const float BANNER_TITLE_HEIGHT = 36f;
        private const float BUTTON_LABEL_HEIGHT = 24f;
        private const float CARD_BUTTON_HEIGHT = 70f;
        private const float CHARACTER_DISPLAY_HEIGHT = 280f;
        private const float CHARACTER_INFO_BUTTON_WIDTH = 80f;
        private const float CONTENT_HEIGHT = 160f;
        private const float COST_GROUP_HEIGHT = 24f;
        private const float COST_ICON_HEIGHT = 18f;
        private const float COST_ICON_WIDTH = 18f;
        private const float COST_VALUE_HEIGHT = 20f;
        private const float COST_VALUE_WIDTH = 40f;
        private const float CURRENCY_H_U_D_HEIGHT = 50f;
        private const float CURRENCY_H_U_D_WIDTH = 600f;
        private const float CURRENCY_골드_HEIGHT = 40f;
        private const float CURRENCY_골드_WIDTH = 100f;
        private const float CURRENCY_무료_HEIGHT = 40f;
        private const float CURRENCY_무료_WIDTH = 100f;
        private const float CURRENCY_신앙심_HEIGHT = 40f;
        private const float CURRENCY_신앙심_WIDTH = 100f;
        private const float CURRENCY_유료_HEIGHT = 40f;
        private const float CURRENCY_유료_WIDTH = 100f;
        private const float CURRENCY_프리미엄_HEIGHT = 40f;
        private const float CURRENCY_프리미엄_WIDTH = 100f;
        private const float DOT_0_HEIGHT = 8f;
        private const float DOT_0_WIDTH = 8f;
        private const float DOT_1_HEIGHT = 8f;
        private const float DOT_1_WIDTH = 8f;
        private const float DOT_2_HEIGHT = 8f;
        private const float DOT_2_WIDTH = 8f;
        private const float DOT_3_HEIGHT = 8f;
        private const float DOT_3_WIDTH = 8f;
        private const float EXCHANGE_BUTTON_HEIGHT = 36f;
        private const float EXCHANGE_BUTTON_WIDTH = 70f;
        private const float FOOTER_HEIGHT = 80f;
        private const float FREE_PULL_BUTTON_WIDTH = 200f;
        private const float GACHA_BUTTON_HEIGHT = 70f;
        private const float GUARANTEE_BADGE_HEIGHT = 24f;
        private const float GUARANTEE_BADGE_WIDTH = 70f;
        private const float HEADER_HEIGHT = 80f;
        private const float HISTORY_BUTTON_WIDTH = 80f;
        private const float ICON_HEIGHT = 24f;
        private const float ICON_WIDTH = 24f;
        private const float INDICATORS_HEIGHT = 8f;
        private const float INDICATORS_WIDTH = 64f;
        private const float INFO_BUTTON_GROUP_HEIGHT = 60f;
        private const float INFO_BUTTON_GROUP_WIDTH = 300f;
        private const float LABEL_HEIGHT = 16f;
        private const float MENU_BUTTON_GROUP_HEIGHT = 240f;
        private const float MULTI_PULL_BUTTON_WIDTH = 200f;
        private const float PITY_DESCRIPTION_HEIGHT = 30f;
        private const float PITY_DESCRIPTION_WIDTH = 300f;
        private const float PITY_ICON_HEIGHT = 24f;
        private const float PITY_ICON_WIDTH = 24f;
        private const float PITY_INFO_AREA_HEIGHT = 60f;
        private const float PITY_LABEL_HEIGHT = 30f;
        private const float PITY_LABEL_WIDTH = 80f;
        private const float PULL_BUTTON_GROUP_HEIGHT = 100f;
        private const float RATE_INFO_BUTTON_WIDTH = 80f;
        private const float REMAINING_LABEL_HEIGHT = 18f;
        private const float SINGLE_PULL_BUTTON_WIDTH = 200f;
        private const float SPECIAL_BUTTON_HEIGHT = 70f;
        private const float THUMBNAIL_HEIGHT = 60f;
        private const float THUMBNAIL_WIDTH = 60f;
        private const float TITLE_HEIGHT = 40f;
        private const float TITLE_WIDTH = 150f;
        private const float VALUE_HEIGHT = 30f;
        private const float VALUE_WIDTH = 70f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// GachaScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("GachaScreen");

            var background = CreateBackground(root);
            var safeArea = CreateSafeArea(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<GachaScreen>();

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
            rect.sizeDelta = new Vector2(0f, 80f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateHeaderBg(go);
            CreateBackButton(go);
            CreateTitle(go);
            CreateCurrencyHUD(go);

            return go;
        }

        #endregion

        #region HeaderBg

        private static GameObject CreateHeaderBg(GameObject parent)
        {
            var go = CreateChild(parent, "HeaderBg");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(44f, 44f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 15);
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
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Title

        private static GameObject CreateTitle(GameObject parent)
        {
            var go = CreateChild(parent, "Title");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(150f, 40f);
            rect.anchoredPosition = new Vector2(70f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사도 모집";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
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
            rect.sizeDelta = new Vector2(600f, 50f);
            rect.anchoredPosition = new Vector2(-16f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateCurrency_유료(go);
            CreateCurrency_골드(go);
            CreateCurrency_무료(go);
            CreateCurrency_프리미엄(go);
            CreateCurrency_신앙심(go);

            return go;
        }

        #endregion

        #region Currency_유료

        private static GameObject CreateCurrency_유료(GameObject parent)
        {
            var go = CreateChild(parent, "Currency_유료");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_2(go);
            CreateValue_1(go);

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
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentPurple;
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(70f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "52";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Currency_골드

        private static GameObject CreateCurrency_골드(GameObject parent)
        {
            var go = CreateChild(parent, "Currency_골드");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_3(go);
            CreateValue_2(go);

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
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentGold;
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(70f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "549,061";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Currency_무료

        private static GameObject CreateCurrency_무료(GameObject parent)
        {
            var go = CreateChild(parent, "Currency_무료");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_4(go);
            CreateValue_3(go);

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
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 217, 100, 255);
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(70f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "0";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Currency_프리미엄

        private static GameObject CreateCurrency_프리미엄(GameObject parent)
        {
            var go = CreateChild(parent, "Currency_프리미엄");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_5(go);
            CreateValue_4(go);

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
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentSecondary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_4(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(70f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1,809";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Currency_신앙심

        private static GameObject CreateCurrency_신앙심(GameObject parent)
        {
            var go = CreateChild(parent, "Currency_신앙심");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateIcon_6(go);
            CreateValue_5(go);

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
            rect.sizeDelta = new Vector2(20f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentPrimary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Value

        private static GameObject CreateValue_5(GameObject parent)
        {
            var go = CreateChild(parent, "Value");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(70f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "4";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
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
            rect.offsetMin = new Vector2(0f, 80f);
            rect.offsetMax = new Vector2(0f, -80f);

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
            rect.sizeDelta = new Vector2(350f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateMenuButtonGroup(go);
            CreateCharacterDisplay(go);

            return go;
        }

        #endregion

        #region MenuButtonGroup

        private static GameObject CreateMenuButtonGroup(GameObject parent)
        {
            var go = CreateChild(parent, "MenuButtonGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(-32f, 240f);
            rect.anchoredPosition = new Vector2(0f, -20f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(16, 16, 0, 0);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateGachaButton(go);
            CreateSpecialButton(go);
            CreateCardButton(go);

            return go;
        }

        #endregion

        #region GachaButton

        private static GameObject CreateGachaButton(GameObject parent)
        {
            var go = CreateChild(parent, "GachaButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 70f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 70f;
            layoutElement.preferredHeight = 70f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 50);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_7(go);
            CreateLabel_1(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_7(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentPrimary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(70f, 0f);
            rect.offsetMax = new Vector2(-16f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사도 모집";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SpecialButton

        private static GameObject CreateSpecialButton(GameObject parent)
        {
            var go = CreateChild(parent, "SpecialButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 70f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 70f;
            layoutElement.preferredHeight = 70f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_8(go);
            CreateLabel_2(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_8(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(70f, 0f);
            rect.offsetMax = new Vector2(-16f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "특별 모집";
            tmp.fontSize = 18f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CardButton

        private static GameObject CreateCardButton(GameObject parent)
        {
            var go = CreateChild(parent, "CardButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 70f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 70f;
            layoutElement.preferredHeight = 70f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon_9(go);
            CreateLabel_3(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon_9(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(16f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(70f, 0f);
            rect.offsetMax = new Vector2(-16f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "카드 뽑기";
            tmp.fontSize = 18f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CharacterDisplay

        private static GameObject CreateCharacterDisplay(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterDisplay");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -280f);

            CreateCharacterImage(go);

            return go;
        }

        #endregion

        #region CharacterImage

        private static GameObject CreateCharacterImage(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterImage");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region RightArea

        private static GameObject CreateRightArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightArea");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(350f, 0f);
            rect.offsetMax = new Vector2(0f, 0f);

            CreateBannerCarousel(go);
            CreateBannerInfoArea(go);
            CreatePityInfoArea(go);
            CreatePullButtonGroup(go);

            return go;
        }

        #endregion

        #region BannerCarousel

        private static GameObject CreateBannerCarousel(GameObject parent)
        {
            var go = CreateChild(parent, "BannerCarousel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(-32f, 120f);
            rect.anchoredPosition = new Vector2(0f, -20f);

            var scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = true;
            scrollRect.vertical = false;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;

            CreateViewport(go);
            CreateIndicators(go);

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

            CreateBannerContainer(go);

            return go;
        }

        #endregion

        #region BannerContainer

        private static GameObject CreateBannerContainer(GameObject parent)
        {
            var go = CreateChild(parent, "BannerContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(768f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(0, 0, 8, 8);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateBannerSlot_0(go);
            CreateBannerSlot_1(go);
            CreateBannerSlot_2(go);
            CreateBannerSlot_3(go);

            return go;
        }

        #endregion

        #region BannerSlot_0

        private static GameObject CreateBannerSlot_0(GameObject parent)
        {
            var go = CreateChild(parent, "BannerSlot_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(180f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 40);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateThumbnail_1(go);
            CreateLabel_4(go);

            return go;
        }

        #endregion

        #region Thumbnail

        private static GameObject CreateThumbnail_1(GameObject parent)
        {
            var go = CreateChild(parent, "Thumbnail");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(8f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 76);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_4(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(75f, 0f);
            rect.offsetMax = new Vector2(-8f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "픽업 사도 모집";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BannerSlot_1

        private static GameObject CreateBannerSlot_1(GameObject parent)
        {
            var go = CreateChild(parent, "BannerSlot_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(180f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = BgCard;
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateThumbnail_2(go);
            CreateLabel_5(go);

            return go;
        }

        #endregion

        #region Thumbnail

        private static GameObject CreateThumbnail_2(GameObject parent)
        {
            var go = CreateChild(parent, "Thumbnail");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(8f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 76);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_5(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(75f, 0f);
            rect.offsetMax = new Vector2(-8f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "픽업 사도 모집";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BannerSlot_2

        private static GameObject CreateBannerSlot_2(GameObject parent)
        {
            var go = CreateChild(parent, "BannerSlot_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(180f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = BgCard;
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateThumbnail_3(go);
            CreateLabel_6(go);

            return go;
        }

        #endregion

        #region Thumbnail

        private static GameObject CreateThumbnail_3(GameObject parent)
        {
            var go = CreateChild(parent, "Thumbnail");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(8f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 76);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_6(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(75f, 0f);
            rect.offsetMax = new Vector2(-8f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "픽업 사도 모집";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BannerSlot_3

        private static GameObject CreateBannerSlot_3(GameObject parent)
        {
            var go = CreateChild(parent, "BannerSlot_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(180f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = BgCard;
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateThumbnail_4(go);
            CreateLabel_7(go);

            return go;
        }

        #endregion

        #region Thumbnail

        private static GameObject CreateThumbnail_4(GameObject parent)
        {
            var go = CreateChild(parent, "Thumbnail");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(8f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 76);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Label

        private static GameObject CreateLabel_7(GameObject parent)
        {
            var go = CreateChild(parent, "Label");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(75f, 0f);
            rect.offsetMax = new Vector2(-8f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사도 모집";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Indicators

        private static GameObject CreateIndicators(GameObject parent)
        {
            var go = CreateChild(parent, "Indicators");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(64f, 8f);
            rect.anchoredPosition = new Vector2(0f, 4f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateDot_0(go);
            CreateDot_1(go);
            CreateDot_2(go);
            CreateDot_3(go);

            return go;
        }

        #endregion

        #region Dot_0

        private static GameObject CreateDot_0(GameObject parent)
        {
            var go = CreateChild(parent, "Dot_0");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentPrimary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Dot_1

        private static GameObject CreateDot_1(GameObject parent)
        {
            var go = CreateChild(parent, "Dot_1");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Dot_2

        private static GameObject CreateDot_2(GameObject parent)
        {
            var go = CreateChild(parent, "Dot_2");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Dot_3

        private static GameObject CreateDot_3(GameObject parent)
        {
            var go = CreateChild(parent, "Dot_3");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region BannerInfoArea

        private static GameObject CreateBannerInfoArea(GameObject parent)
        {
            var go = CreateChild(parent, "BannerInfoArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(-32f, 180f);
            rect.anchoredPosition = new Vector2(0f, -160f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(16, 16, 8, 8);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateBannerTitle(go);
            CreateBannerPeriod(go);
            CreateBannerDescription(go);

            return go;
        }

        #endregion

        #region BannerTitle

        private static GameObject CreateBannerTitle(GameObject parent)
        {
            var go = CreateChild(parent, "BannerTitle");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 36f;
            layoutElement.preferredHeight = 36f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "용족 성골 귀족 비비";
            tmp.fontSize = 28f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BannerPeriod

        private static GameObject CreateBannerPeriod(GameObject parent)
        {
            var go = CreateChild(parent, "BannerPeriod");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "2026-01-22 11:00 ~ 2026-01-29 10:59";
            tmp.fontSize = 14f;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BannerDescription

        private static GameObject CreateBannerDescription(GameObject parent)
        {
            var go = CreateChild(parent, "BannerDescription");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 100f;
            layoutElement.preferredHeight = 100f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "기간 중 픽업된 사도 등장 시 비비 3판 출현!\n10회 모집 시 ★2 이상 사도 1인 안정\n\n※모집 시 신앙심을 함께 획득할 수 있습니다.\n※일특한 신앙심으로 캔서드로 교환 수도 있습니다.\n※이미 얻고있는 사도는 해당 사도 돌파석으로 변환됩니다.";
            tmp.fontSize = 12f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.TopLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PityInfoArea

        private static GameObject CreatePityInfoArea(GameObject parent)
        {
            var go = CreateChild(parent, "PityInfoArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(-32f, 60f);
            rect.anchoredPosition = new Vector2(0f, -350f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.padding = new RectOffset(16, 16, 8, 8);
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreatePityIcon(go);
            CreatePityLabel(go);
            CreateExchangeButton(go);
            CreatePityDescription(go);

            return go;
        }

        #endregion

        #region PityIcon

        private static GameObject CreatePityIcon(GameObject parent)
        {
            var go = CreateChild(parent, "PityIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentSecondary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region PityLabel

        private static GameObject CreatePityLabel(GameObject parent)
        {
            var go = CreateChild(parent, "PityLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "신앙심 110";
            tmp.fontSize = 16f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ExchangeButton

        private static GameObject CreateExchangeButton(GameObject parent)
        {
            var go = CreateChild(parent, "ExchangeButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(70f, 36f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 80, 120, 255);
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
            tmp.text = "교환";
            tmp.fontSize = 14f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PityDescription

        private static GameObject CreatePityDescription(GameObject parent)
        {
            var go = CreateChild(parent, "PityDescription");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 30f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사용하지 않은 신앙심은 다음 픽업 모집에 이월됩니다.";
            tmp.fontSize = 11f;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PullButtonGroup

        private static GameObject CreatePullButtonGroup(GameObject parent)
        {
            var go = CreateChild(parent, "PullButtonGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(-32f, 100f);
            rect.anchoredPosition = new Vector2(0f, 20f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.padding = new RectOffset(16, 16, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateFreePullButton(go);
            CreateSinglePullButton(go);
            CreateMultiPullButton(go);

            return go;
        }

        #endregion

        #region FreePullButton

        private static GameObject CreateFreePullButton(GameObject parent)
        {
            var go = CreateChild(parent, "FreePullButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 217, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateRemainingLabel(go);
            CreateButtonLabel_1(go);
            CreateCostGroup_1(go);

            return go;
        }

        #endregion

        #region RemainingLabel

        private static GameObject CreateRemainingLabel(GameObject parent)
        {
            var go = CreateChild(parent, "RemainingLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 18f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 18f;
            layoutElement.preferredHeight = 18f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1회 남음";
            tmp.fontSize = 12f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ButtonLabel

        private static GameObject CreateButtonLabel_1(GameObject parent)
        {
            var go = CreateChild(parent, "ButtonLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1일 1회 모집";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CostGroup

        private static GameObject CreateCostGroup_1(GameObject parent)
        {
            var go = CreateChild(parent, "CostGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateCostIcon_1(go);
            CreateCostValue_1(go);

            return go;
        }

        #endregion

        #region CostIcon

        private static GameObject CreateCostIcon_1(GameObject parent)
        {
            var go = CreateChild(parent, "CostIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(18f, 18f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(76, 217, 100, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CostValue

        private static GameObject CreateCostValue_1(GameObject parent)
        {
            var go = CreateChild(parent, "CostValue");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "30";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SinglePullButton

        private static GameObject CreateSinglePullButton(GameObject parent)
        {
            var go = CreateChild(parent, "SinglePullButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentPrimary;
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateButtonLabel_2(go);
            CreateCostGroup_2(go);

            return go;
        }

        #endregion

        #region ButtonLabel

        private static GameObject CreateButtonLabel_2(GameObject parent)
        {
            var go = CreateChild(parent, "ButtonLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1회 모집";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CostGroup

        private static GameObject CreateCostGroup_2(GameObject parent)
        {
            var go = CreateChild(parent, "CostGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateCostIcon_2(go);
            CreateCostValue_2(go);

            return go;
        }

        #endregion

        #region CostIcon

        private static GameObject CreateCostIcon_2(GameObject parent)
        {
            var go = CreateChild(parent, "CostIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(18f, 18f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentSecondary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CostValue

        private static GameObject CreateCostValue_2(GameObject parent)
        {
            var go = CreateChild(parent, "CostValue");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "1";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region MultiPullButton

        private static GameObject CreateMultiPullButton(GameObject parent)
        {
            var go = CreateChild(parent, "MultiPullButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 195, 0, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateButtonLabel_3(go);
            CreateCostGroup_3(go);
            CreateGuaranteeBadge(go);

            return go;
        }

        #endregion

        #region ButtonLabel

        private static GameObject CreateButtonLabel_3(GameObject parent)
        {
            var go = CreateChild(parent, "ButtonLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10회 모집";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region CostGroup

        private static GameObject CreateCostGroup_3(GameObject parent)
        {
            var go = CreateChild(parent, "CostGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 24f;
            layoutElement.preferredHeight = 24f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateCostIcon_3(go);
            CreateCostValue_3(go);

            return go;
        }

        #endregion

        #region CostIcon

        private static GameObject CreateCostIcon_3(GameObject parent)
        {
            var go = CreateChild(parent, "CostIcon");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(18f, 18f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = AccentSecondary;
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CostValue

        private static GameObject CreateCostValue_3(GameObject parent)
        {
            var go = CreateChild(parent, "CostValue");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(40f, 20f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "10";
            tmp.fontSize = 16f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region GuaranteeBadge

        private static GameObject CreateGuaranteeBadge(GameObject parent)
        {
            var go = CreateChild(parent, "GuaranteeBadge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(70f, 24f);
            rect.anchoredPosition = new Vector2(8f, 8f);


            var image = go.AddComponent<Image>();
            image.color = AccentSecondary;
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
            tmp.text = "★2 확정";
            tmp.fontSize = 11f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
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
            rect.sizeDelta = new Vector2(0f, 80f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateFooterBg(go);
            CreateInfoButtonGroup(go);

            return go;
        }

        #endregion

        #region FooterBg

        private static GameObject CreateFooterBg(GameObject parent)
        {
            var go = CreateChild(parent, "FooterBg");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region InfoButtonGroup

        private static GameObject CreateInfoButtonGroup(GameObject parent)
        {
            var go = CreateChild(parent, "InfoButtonGroup");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 60f);
            rect.anchoredPosition = new Vector2(16f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateCharacterInfoButton(go);
            CreateRateInfoButton(go);
            CreateHistoryButton(go);

            return go;
        }

        #endregion

        #region CharacterInfoButton

        private static GameObject CreateCharacterInfoButton(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterInfoButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 15);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(4, 4, 8, 4);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateIcon_10(go);
            CreateLabel_8(go);

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minWidth = 24f;
            layoutElement.minHeight = 24f;
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(0f, 16f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 16f;
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "사도 정보";
            tmp.fontSize = 11f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RateInfoButton

        private static GameObject CreateRateInfoButton(GameObject parent)
        {
            var go = CreateChild(parent, "RateInfoButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 15);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(4, 4, 8, 4);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateIcon_11(go);
            CreateLabel_9(go);

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minWidth = 24f;
            layoutElement.minHeight = 24f;
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(0f, 16f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 16f;
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "확률 정보";
            tmp.fontSize = 11f;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region HistoryButton

        private static GameObject CreateHistoryButton(GameObject parent)
        {
            var go = CreateChild(parent, "HistoryButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 15);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(4, 4, 8, 4);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateIcon_12(go);
            CreateLabel_10(go);

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
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minWidth = 24f;
            layoutElement.minHeight = 24f;
            layoutElement.preferredWidth = 24f;
            layoutElement.preferredHeight = 24f;


            var image = go.AddComponent<Image>();
            image.color = TextMuted;
            image.raycastTarget = false;

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
            rect.sizeDelta = new Vector2(0f, 16f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 16f;
            layoutElement.preferredHeight = 16f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "기록";
            tmp.fontSize = 11f;
            tmp.color = TextSecondary;
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
            var component = root.GetComponent<GachaScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _bannerContainer
            so.FindProperty("_bannerContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/BannerCarousel/Viewport/BannerContainer")?.GetComponent<RectTransform>();

            // _bannerScrollRect
            so.FindProperty("_bannerScrollRect").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/BannerCarousel")?.GetComponent<ScrollRect>();

            // _singlePullButton
            so.FindProperty("_singlePullButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/PullButtonGroup/SinglePullButton")?.GetComponent<Button>();

            // _multiPullButton
            so.FindProperty("_multiPullButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/PullButtonGroup/MultiPullButton")?.GetComponent<Button>();

            // _backButton
            so.FindProperty("_backButton").objectReferenceValue = FindChild(root, "SafeArea/Header/BackButton")?.GetComponent<Button>();

            // _bannerTitleText
            so.FindProperty("_bannerTitleText").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/BannerInfoArea/BannerTitle")?.GetComponent<TextMeshProUGUI>();

            // _bannerPeriodText
            so.FindProperty("_bannerPeriodText").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/BannerInfoArea/BannerPeriod")?.GetComponent<TextMeshProUGUI>();

            // _bannerDescriptionText
            so.FindProperty("_bannerDescriptionText").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/BannerInfoArea/BannerDescription")?.GetComponent<TextMeshProUGUI>();

            // _pityLabel
            so.FindProperty("_pityLabel").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/PityInfoArea/PityLabel")?.GetComponent<TextMeshProUGUI>();

            // _exchangeButton
            so.FindProperty("_exchangeButton").objectReferenceValue = FindChild(root, "SafeArea/Content/RightArea/PityInfoArea/ExchangeButton")?.GetComponent<Button>();

            // _rateDetailButton
            so.FindProperty("_rateDetailButton").objectReferenceValue = FindChild(root, "SafeArea/Footer/InfoButtonGroup/RateInfoButton")?.GetComponent<Button>();

            // _historyButton
            so.FindProperty("_historyButton").objectReferenceValue = FindChild(root, "SafeArea/Footer/InfoButtonGroup/HistoryButton")?.GetComponent<Button>();

            // _characterInfoButton
            so.FindProperty("_characterInfoButton").objectReferenceValue = FindChild(root, "SafeArea/Footer/InfoButtonGroup/CharacterInfoButton")?.GetComponent<Button>();

            // _gachaMenuButton
            so.FindProperty("_gachaMenuButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/MenuButtonGroup/GachaButton")?.GetComponent<Button>();

            // _specialMenuButton
            so.FindProperty("_specialMenuButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/MenuButtonGroup/SpecialButton")?.GetComponent<Button>();

            // _cardMenuButton
            so.FindProperty("_cardMenuButton").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/MenuButtonGroup/CardButton")?.GetComponent<Button>();

            // _characterDisplay
            so.FindProperty("_characterDisplay").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftArea/CharacterDisplay")?.GetComponent<RectTransform>();

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
