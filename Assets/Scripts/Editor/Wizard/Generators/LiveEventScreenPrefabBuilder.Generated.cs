using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Event;
using Sc.Contents.Event.Widgets;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// LiveEventScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/LiveEventScreen.prefab
    /// Generated at: 2026-01-27 14:42:50
    /// </summary>
    public static class LiveEventScreenPrefabBuilder_Generated
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
        private static readonly Color BgGlass = new Color32(140, 190, 130, 76);
        private static readonly Color Blue = new Color32(100, 180, 255, 255);
        private static readonly Color Color = new Color32(200, 230, 180, 255);
        private static readonly Color Green = new Color32(80, 140, 80, 255);
        private static readonly Color Red = new Color32(200, 100, 100, 255);

        #endregion

        #region Constants

        private const float BANNER_IMAGE_HEIGHT = 100f;
        private const float BANNER_IMAGE_WIDTH = 100f;
        private const float BOTTOM_BORDER_HEIGHT = 8f;
        private const float CONTENT_HEIGHT = 80f;
        private const float END_TIME_LABEL_HEIGHT = 50f;
        private const float END_TIME_LABEL_WIDTH = 200f;
        private const float ENTER_BUTTON_HEIGHT = 100f;
        private const float ENTER_BUTTON_WIDTH = 100f;
        private const float EVENT_BANNER_ITEM_PREFAB_HEIGHT = 110f;
        private const float GRACE_PERIOD_INDICATOR_HEIGHT = 100f;
        private const float GRACE_PERIOD_INDICATOR_WIDTH = 100f;
        private const float GRACE_PERIOD_TEXT_HEIGHT = 100f;
        private const float GRACE_PERIOD_TEXT_WIDTH = 100f;
        private const float HEADER_HEIGHT = 80f;
        private const float INFO_AREA_HEIGHT = 100f;
        private const float INFO_AREA_WIDTH = 100f;
        private const float LEFT_PANEL_HEIGHT = 40f;
        private const float LOADING_INDICATOR_HEIGHT = 60f;
        private const float LOADING_INDICATOR_WIDTH = 60f;
        private const float NAME_TEXT_HEIGHT = 100f;
        private const float NAME_TEXT_WIDTH = 100f;
        private const float NEW_BADGE_HEIGHT = 24f;
        private const float NEW_BADGE_WIDTH = 24f;
        private const float REMAINING_DAYS_TEXT_HEIGHT = 100f;
        private const float REMAINING_DAYS_TEXT_WIDTH = 100f;
        private const float REWARD_BADGE_HEIGHT = 20f;
        private const float REWARD_BADGE_WIDTH = 40f;
        private const float REWARD_ICON_CONTAINER_HEIGHT = 100f;
        private const float REWARD_ICON_CONTAINER_WIDTH = 100f;
        private const float REWARD_ICON_PREFAB_HEIGHT = 100f;
        private const float REWARD_ICON_PREFAB_WIDTH = 100f;
        private const float REWARD_PREVIEW_TITLE_HEIGHT = 50f;
        private const float REWARD_PREVIEW_TITLE_WIDTH = 200f;
        private const float RIGHT_PANEL_HEIGHT = 40f;
        private const float SAMPLE_REWARD_ICON_HEIGHT = 100f;
        private const float SAMPLE_REWARD_ICON_WIDTH = 100f;
        private const float START_TIME_LABEL_HEIGHT = 50f;
        private const float START_TIME_LABEL_WIDTH = 200f;
        private const float TITLE_BANNER_AREA_HEIGHT = 20f;
        private const float TOP_BORDER_HEIGHT = 8f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// LiveEventScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("LiveEventScreen");

            var background = CreateBackground(root);
            var safeArea = CreateSafeArea(root);
            var overlayLayer = CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<LiveEventScreen>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Background

        private static GameObject CreateBackground(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);

            CreateBaseBg(go);
            CreateTopOverlay(go);
            CreateBottomOverlay(go);
            CreateTopBorder(go);
            CreateBottomBorder(go);
            CreateLeftBorder(go);
            CreateRightBorder(go);

            return go;
        }

        #endregion

        #region BaseBg

        private static GameObject CreateBaseBg(GameObject parent)
        {
            var go = CreateChild(parent, "BaseBg");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(200, 230, 180, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region TopOverlay

        private static GameObject CreateTopOverlay(GameObject parent)
        {
            var go = CreateChild(parent, "TopOverlay");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.7f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(140, 190, 130, 76);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region BottomOverlay

        private static GameObject CreateBottomOverlay(GameObject parent)
        {
            var go = CreateChild(parent, "BottomOverlay");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0.15f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 128);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region TopBorder

        private static GameObject CreateTopBorder(GameObject parent)
        {
            var go = CreateChild(parent, "TopBorder");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 140, 80, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region BottomBorder

        private static GameObject CreateBottomBorder(GameObject parent)
        {
            var go = CreateChild(parent, "BottomBorder");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(0f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 140, 80, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region LeftBorder

        private static GameObject CreateLeftBorder(GameObject parent)
        {
            var go = CreateChild(parent, "LeftBorder");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 140, 80, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region RightBorder

        private static GameObject CreateRightBorder(GameObject parent)
        {
            var go = CreateChild(parent, "RightBorder");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(80, 140, 80, 255);
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

            CreateScreenHeaderPlaceholder(go);

            return go;
        }

        #endregion

        #region ScreenHeaderPlaceholder

        private static GameObject CreateScreenHeaderPlaceholder(GameObject parent)
        {
            var go = CreateChild(parent, "ScreenHeaderPlaceholder");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 76);
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
            tmp.text = "[ScreenHeader - 이벤트]";
            tmp.fontSize = 14f;
            tmp.color = new Color32(255, 255, 255, 153);
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
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -80f);

            CreateLeftPanel(go);
            CreateRightPanel(go);

            return go;
        }

        #endregion

        #region LeftPanel

        private static GameObject CreateLeftPanel(GameObject parent)
        {
            var go = CreateChild(parent, "LeftPanel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(350f, -40f);
            rect.anchoredPosition = new Vector2(20f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 26);
            image.raycastTarget = false;

            CreateEventBannerScrollView(go);
            CreateEmptyState(go);
            CreateLoadingIndicator(go);
            CreateEventBannerItemPrefab(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<EventListWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_scrollRect").objectReferenceValue = go.transform.Find("EventBannerScrollView")?.GetComponent<ScrollRect>();
                widgetSo.FindProperty("_bannerContainer").objectReferenceValue = go.transform.Find("EventBannerScrollView/Viewport/BannerContainer") as RectTransform;
                widgetSo.FindProperty("_bannerItemPrefab").objectReferenceValue = go.transform.Find("EventBannerItemPrefab")?.GetComponent<EventBannerItem>();
                widgetSo.FindProperty("_emptyState").objectReferenceValue = go.transform.Find("EmptyState")?.gameObject;
                widgetSo.FindProperty("_emptyText").objectReferenceValue = go.transform.Find("EmptyState/EmptyText")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_loadingIndicator").objectReferenceValue = go.transform.Find("LoadingIndicator")?.gameObject;
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region EventBannerScrollView

        private static GameObject CreateEventBannerScrollView(GameObject parent)
        {
            var go = CreateChild(parent, "EventBannerScrollView");
            SetStretch(go);

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
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            return go;
        }

        #endregion

        #region EmptyState

        private static GameObject CreateEmptyState(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyState");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 26);
            image.raycastTarget = false;
            go.SetActive(false);

            CreateEmptyText(go);

            return go;
        }

        #endregion

        #region EmptyText

        private static GameObject CreateEmptyText(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "진행 중인 이벤트가 없습니다.";
            tmp.fontSize = 16f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region LoadingIndicator

        private static GameObject CreateLoadingIndicator(GameObject parent)
        {
            var go = CreateChild(parent, "LoadingIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 128);
            image.raycastTarget = true;
            go.SetActive(false);

            CreateSpinnerText(go);

            return go;
        }

        #endregion

        #region SpinnerText

        private static GameObject CreateSpinnerText(GameObject parent)
        {
            var go = CreateChild(parent, "SpinnerText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "...";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EventBannerItemPrefab

        private static GameObject CreateEventBannerItemPrefab(GameObject parent)
        {
            var go = CreateChild(parent, "EventBannerItemPrefab");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(0f, 110f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            go.AddComponent<EventBannerItem>();


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 230);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10f;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;
            go.SetActive(false);

            CreateBannerImage(go);
            CreateInfoArea(go);
            CreateNewBadge(go);
            CreateRewardBadge(go);
            CreateSelectionHighlight(go);

            return go;
        }

        #endregion

        #region BannerImage

        private static GameObject CreateBannerImage(GameObject parent)
        {
            var go = CreateChild(parent, "BannerImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 150f;
            layoutElement.preferredHeight = 90f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 150, 200, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region InfoArea

        private static GameObject CreateInfoArea(GameObject parent)
        {
            var go = CreateChild(parent, "InfoArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateNameText(go);
            CreateRemainingDaysText(go);
            CreateGracePeriodIndicator(go);

            return go;
        }

        #endregion

        #region NameText

        private static GameObject CreateNameText(GameObject parent)
        {
            var go = CreateChild(parent, "NameText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 24f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 이름";
            tmp.fontSize = 16f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RemainingDaysText

        private static GameObject CreateRemainingDaysText(GameObject parent)
        {
            var go = CreateChild(parent, "RemainingDaysText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 18f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "D-7";
            tmp.fontSize = 12f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region GracePeriodIndicator

        private static GameObject CreateGracePeriodIndicator(GameObject parent)
        {
            var go = CreateChild(parent, "GracePeriodIndicator");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 16f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 4f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            go.SetActive(false);

            CreateGracePeriodText(go);

            return go;
        }

        #endregion

        #region GracePeriodText

        private static GameObject CreateGracePeriodText(GameObject parent)
        {
            var go = CreateChild(parent, "GracePeriodText");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 종료";
            tmp.fontSize = 10f;
            tmp.color = new Color32(200, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region NewBadge

        private static GameObject CreateNewBadge(GameObject parent)
        {
            var go = CreateChild(parent, "NewBadge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(24f, 24f);
            rect.anchoredPosition = new Vector2(4f, -4f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 120, 160, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region RewardBadge

        private static GameObject CreateRewardBadge(GameObject parent)
        {
            var go = CreateChild(parent, "RewardBadge");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(40f, 20f);
            rect.anchoredPosition = new Vector2(-4f, -4f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 80, 80, 255);
            image.raycastTarget = true;
            go.SetActive(false);

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
            tmp.text = "보상!";
            tmp.fontSize = 10f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SelectionHighlight

        private static GameObject CreateSelectionHighlight(GameObject parent)
        {
            var go = CreateChild(parent, "SelectionHighlight");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 80, 128);
            image.raycastTarget = false;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region RightPanel

        private static GameObject CreateRightPanel(GameObject parent)
        {
            var go = CreateChild(parent, "RightPanel");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(390f, 20f);
            rect.offsetMax = new Vector2(-20f, -20f);

            CreateEventDetailContainer(go);

            // Connect widget SerializeFields
            var widgetComp = go.GetComponent<EventDetailWidget>();
            if (widgetComp != null)
            {
                var widgetSo = new SerializedObject(widgetComp);
                widgetSo.FindProperty("_titleBannerImage").objectReferenceValue = go.transform.Find("EventDetailContainer/TitleBannerArea/TitleBannerImage")?.GetComponent<Image>();
                widgetSo.FindProperty("_characterIllustImage").objectReferenceValue = go.transform.Find("EventDetailContainer/CharacterArea/CharacterIllustImage")?.GetComponent<Image>();
                widgetSo.FindProperty("_startTimeLabel").objectReferenceValue = go.transform.Find("EventDetailContainer/PeriodArea/StartTimeLabel")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_endTimeLabel").objectReferenceValue = go.transform.Find("EventDetailContainer/PeriodArea/EndTimeLabel")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_rewardPreviewTitle").objectReferenceValue = go.transform.Find("EventDetailContainer/RewardPreviewArea/RewardPreviewTitle")?.GetComponent<TextMeshProUGUI>();
                widgetSo.FindProperty("_rewardIconContainer").objectReferenceValue = go.transform.Find("EventDetailContainer/RewardPreviewArea/RewardIconContainer") as RectTransform;
                widgetSo.FindProperty("_rewardIconPrefab").objectReferenceValue = go.transform.Find("EventDetailContainer/RewardPreviewArea/RewardIconContainer/RewardIconPrefab")?.gameObject;
                widgetSo.FindProperty("_enterButton").objectReferenceValue = go.transform.Find("EventDetailContainer/ButtonArea/EnterButton")?.GetComponent<Button>();
                widgetSo.FindProperty("_enterButtonLabel").objectReferenceValue = go.transform.Find("EventDetailContainer/ButtonArea/EnterButton/EnterButtonLabel")?.GetComponent<TextMeshProUGUI>();
                widgetSo.ApplyModifiedPropertiesWithoutUndo();
            }

            return go;
        }

        #endregion

        #region EventDetailContainer

        private static GameObject CreateEventDetailContainer(GameObject parent)
        {
            var go = CreateChild(parent, "EventDetailContainer");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 240);
            image.raycastTarget = false;

            CreateTitleBannerArea(go);
            CreateCharacterArea(go);
            CreatePeriodArea(go);
            CreateRewardPreviewArea(go);
            CreateButtonArea(go);

            return go;
        }

        #endregion

        #region TitleBannerArea

        private static GameObject CreateTitleBannerArea(GameObject parent)
        {
            var go = CreateChild(parent, "TitleBannerArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.75f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-40f, -20f);
            rect.anchoredPosition = new Vector2(0f, -10f);

            CreateTitleBannerImage(go);
            CreateDecoElements(go);

            return go;
        }

        #endregion

        #region TitleBannerImage

        private static GameObject CreateTitleBannerImage(GameObject parent)
        {
            var go = CreateChild(parent, "TitleBannerImage");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(200, 220, 180, 255);
            image.raycastTarget = false;

            CreatePlaceholder_1(go);

            return go;
        }

        #endregion

        #region Placeholder

        private static GameObject CreatePlaceholder_1(GameObject parent)
        {
            var go = CreateChild(parent, "Placeholder");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 타이틀 배너";
            tmp.fontSize = 32f;
            tmp.color = new Color32(50, 50, 50, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region DecoElements

        private static GameObject CreateDecoElements(GameObject parent)
        {
            var go = CreateChild(parent, "DecoElements");
            SetStretch(go);

            return go;
        }

        #endregion

        #region CharacterArea

        private static GameObject CreateCharacterArea(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.25f);
            rect.anchorMax = new Vector2(1f, 0.75f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-20f, 0f);
            rect.anchoredPosition = new Vector2(-10f, 0f);

            CreateCharacterIllustImage(go);

            return go;
        }

        #endregion

        #region CharacterIllustImage

        private static GameObject CreateCharacterIllustImage(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterIllustImage");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(180, 200, 160, 255);
            image.raycastTarget = false;

            CreatePlaceholder_2(go);

            return go;
        }

        #endregion

        #region Placeholder

        private static GameObject CreatePlaceholder_2(GameObject parent)
        {
            var go = CreateChild(parent, "Placeholder");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "캐릭터\n일러스트";
            tmp.fontSize = 24f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region PeriodArea

        private static GameObject CreatePeriodArea(GameObject parent)
        {
            var go = CreateChild(parent, "PeriodArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.15f);
            rect.anchorMax = new Vector2(1f, 0.25f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-40f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 76);
            image.raycastTarget = false;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 2f;
            layout.padding = new RectOffset(20, 20, 5, 5);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateStartTimeLabel(go);
            CreateEndTimeLabel(go);

            return go;
        }

        #endregion

        #region StartTimeLabel

        private static GameObject CreateStartTimeLabel(GameObject parent)
        {
            var go = CreateChild(parent, "StartTimeLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 18f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 시작: 2026-01-15 11:00(UTC +9)";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region EndTimeLabel

        private static GameObject CreateEndTimeLabel(GameObject parent)
        {
            var go = CreateChild(parent, "EndTimeLabel");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(200f, 50f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 18f;


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 종료: 2026-01-29 10:59(UTC +9)";
            tmp.fontSize = 12f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RewardPreviewArea

        private static GameObject CreateRewardPreviewArea(GameObject parent)
        {
            var go = CreateChild(parent, "RewardPreviewArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(0.6f, 0.15f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-20f, -15f);
            rect.anchoredPosition = new Vector2(10f, 7.5f);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateRewardPreviewTitle(go);
            CreateRewardIconContainer(go);

            return go;
        }

        #endregion

        #region RewardPreviewTitle

        private static GameObject CreateRewardPreviewTitle(GameObject parent)
        {
            var go = CreateChild(parent, "RewardPreviewTitle");
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
            tmp.text = "참여 시 획득 가능한 보상";
            tmp.fontSize = 12f;
            tmp.color = new Color32(100, 100, 100, 255);
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RewardIconContainer

        private static GameObject CreateRewardIconContainer(GameObject parent)
        {
            var go = CreateChild(parent, "RewardIconContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50f;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            CreateRewardIconPrefab(go);
            CreateSampleRewardIcon(go);

            return go;
        }

        #endregion

        #region RewardIconPrefab

        private static GameObject CreateRewardIconPrefab(GameObject parent)
        {
            var go = CreateChild(parent, "RewardIconPrefab");
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
            image.color = new Color32(100, 180, 255, 255);
            image.raycastTarget = true;
            go.SetActive(false);

            return go;
        }

        #endregion

        #region SampleRewardIcon

        private static GameObject CreateSampleRewardIcon(GameObject parent)
        {
            var go = CreateChild(parent, "SampleRewardIcon");
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
            image.color = new Color32(150, 220, 150, 255);
            image.raycastTarget = true;

            return go;
        }

        #endregion

        #region ButtonArea

        private static GameObject CreateButtonArea(GameObject parent)
        {
            var go = CreateChild(parent, "ButtonArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.6f, 0f);
            rect.anchorMax = new Vector2(1f, 0.15f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(-40f, -15f);
            rect.anchoredPosition = new Vector2(0f, 7.5f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 0f;
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            CreateEnterButton(go);

            return go;
        }

        #endregion

        #region EnterButton

        private static GameObject CreateEnterButton(GameObject parent)
        {
            var go = CreateChild(parent, "EnterButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 120f;
            layoutElement.preferredHeight = 50f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 255);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateEnterButtonLabel(go);

            return go;
        }

        #endregion

        #region EnterButtonLabel

        private static GameObject CreateEnterButtonLabel(GameObject parent)
        {
            var go = CreateChild(parent, "EnterButtonLabel");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "바로 가기";
            tmp.fontSize = 16f;
            tmp.color = new Color32(80, 60, 40, 255);
            tmp.alignment = TextAlignmentOptions.Center;
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

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<LiveEventScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _eventListWidget
            so.FindProperty("_eventListWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftPanel")?.GetComponent<EventListWidget>();

            // _eventDetailWidget
            so.FindProperty("_eventDetailWidget").objectReferenceValue = FindChild(root, "SafeArea/Content/RightPanel")?.GetComponent<EventDetailWidget>();

            // _eventListContainer
            so.FindProperty("_eventListContainer").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftPanel/EventBannerScrollView/Viewport/BannerContainer")?.GetComponent<RectTransform>();

            // _bannerItemPrefab
            so.FindProperty("_bannerItemPrefab").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftPanel/EventBannerItemPrefab")?.GetComponent<EventBannerItem>();

            // _emptyText
            so.FindProperty("_emptyText").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftPanel/EmptyState/EmptyText")?.GetComponent<TextMeshProUGUI>();

            // _loadingIndicator
            so.FindProperty("_loadingIndicator").objectReferenceValue = FindChild(root, "SafeArea/Content/LeftPanel/LoadingIndicator");

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
