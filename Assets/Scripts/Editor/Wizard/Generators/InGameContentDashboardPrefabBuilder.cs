using Sc.Contents.Stage;
using Sc.Contents.Stage.Widgets;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// InGameContentDashboard 프리팹 빌더.
    /// 스펙: Docs/Specs/Stage.md - InGameContentDashboard UI 레이아웃 구조
    /// 레퍼런스: Docs/Design/Reference/StageDashbaord.jpg
    /// </summary>
    public static class InGameContentDashboardPrefabBuilder
    {
        #region Theme Colors

        private static readonly Color BgRoom = new Color32(180, 200, 180, 255);
        private static readonly Color BgCard = new Color32(30, 45, 60, 230);
        private static readonly Color BgHeader = new Color32(20, 35, 50, 240);
        private static readonly Color TextPrimary = Color.white;
        private static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);
        private static readonly Color AccentGreen = new Color32(100, 180, 100, 255);
        private static readonly Color AccentYellow = new Color32(255, 215, 100, 255);
        private static readonly Color ButtonActive = new Color32(80, 120, 160, 220);
        private static readonly Color ButtonLocked = new Color32(60, 60, 60, 200);
        private static readonly Color ContentButtonBg = new Color32(240, 230, 210, 230);
        private static readonly Color ProgressPanelBg = new Color32(50, 80, 60, 230);

        #endregion

        #region Constants

        private const float HEADER_HEIGHT = 70f;
        private const float RIGHT_TOP_WIDTH = 320f;
        private const float RIGHT_TOP_HEIGHT = 50f;
        private const float CONTENT_BUTTON_SIZE = 140f;
        private const float SMALL_BUTTON_SIZE = 100f;
        private const float PROGRESS_PANEL_WIDTH = 320f;
        private const float PROGRESS_PANEL_HEIGHT = 120f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// InGameContentDashboard 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("InGameContentDashboard");

            CreateBackground(root);
            CreateSafeArea(root);
            CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<InGameContentDashboard>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Root

        private static GameObject CreateRoot(string name)
        {
            var go = new GameObject(name);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
            go.AddComponent<CanvasGroup>();
            return go;
        }

        #endregion

        #region Background

        private static GameObject CreateBackground(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);

            // Room Background Image (방 인테리어)
            var roomBg = CreateChild(go, "RoomBackground");
            SetStretch(roomBg);
            var roomImage = roomBg.AddComponent<Image>();
            roomImage.color = BgRoom;
            roomImage.raycastTarget = true;

            // Decoration Layer (가구, 장식)
            var decorations = CreateChild(go, "DecorationLayer");
            SetStretch(decorations);
            CreateDecorations(decorations);

            return go;
        }

        private static void CreateDecorations(GameObject parent)
        {
            // 플레이스홀더 장식 요소들
            // 실제 프로덕션에서는 디자인 리소스 적용

            // 칠판 (좌측 상단)
            CreateDecoration(parent, "Blackboard", new Vector2(-400, 150), new Vector2(120, 80), new Color32(40, 50, 40, 200));

            // 트로피 선반 (중앙 상단)
            CreateDecoration(parent, "TrophyShelf", new Vector2(0, 180), new Vector2(200, 60), new Color32(100, 80, 60, 200));

            // 액자들 (우측)
            CreateDecoration(parent, "Frame1", new Vector2(350, 120), new Vector2(60, 80), new Color32(80, 60, 40, 200));
            CreateDecoration(parent, "Frame2", new Vector2(420, 100), new Vector2(50, 70), new Color32(90, 70, 50, 200));
        }

        private static void CreateDecoration(GameObject parent, string name, Vector2 position, Vector2 size, Color color)
        {
            var go = CreateChild(parent, name);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = size;
            rect.anchoredPosition = position;

            var image = go.AddComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
        }

        #endregion

        #region SafeArea

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var go = CreateChild(parent, "SafeArea");
            SetStretch(go);

            CreateHeader(go);
            CreateRightTopArea(go);
            CreateContent(go);

            return go;
        }

        #endregion

        #region Header

        private static GameObject CreateHeader(GameObject parent)
        {
            var go = CreateChild(parent, "Header");
            SetAnchorTop(go, HEADER_HEIGHT);

            var bg = go.AddComponent<Image>();
            bg.color = BgHeader;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.padding = new RectOffset(20, 20, 0, 0);
            layout.spacing = 15;

            // Back Button
            CreateButton(go, "BackButton", "<", 50, 50, BgCard);

            // Title
            CreateLabel(go, "TitleText", "모험", 24, TextPrimary, TextAlignmentOptions.Left);

            // Spacer
            var spacer = CreateChild(go, "Spacer");
            var spacerLayout = spacer.AddComponent<LayoutElement>();
            spacerLayout.flexibleWidth = 1;

            // Currency HUD placeholder
            var currencyArea = CreateChild(go, "CurrencyHUD");
            var currencyLayout = currencyArea.AddComponent<HorizontalLayoutGroup>();
            currencyLayout.spacing = 20;
            currencyLayout.childAlignment = TextAnchor.MiddleRight;

            CreateCurrencyItem(currencyArea, "Stamina", "102/102");
            CreateCurrencyItem(currencyArea, "Ticket", "180/180");
            CreateCurrencyItem(currencyArea, "Gold", "549,061");
            CreateCurrencyItem(currencyArea, "Premium", "1,809");

            // Home Button
            CreateButton(go, "HomeButton", "H", 40, 40, AccentGreen);

            return go;
        }

        private static void CreateCurrencyItem(GameObject parent, string name, string value)
        {
            var item = CreateChild(parent, name);
            var layout = item.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5;
            layout.childAlignment = TextAnchor.MiddleCenter;

            // Icon placeholder
            var icon = CreateChild(item, "Icon");
            var iconLayout = icon.AddComponent<LayoutElement>();
            iconLayout.preferredWidth = 24;
            iconLayout.preferredHeight = 24;
            var iconImage = icon.AddComponent<Image>();
            iconImage.color = AccentYellow;

            // Value text
            CreateLabel(item, "Value", value, 14, TextPrimary, TextAlignmentOptions.Left);

            // Plus button
            var plusBtn = CreateChild(item, "PlusButton");
            var plusLayout = plusBtn.AddComponent<LayoutElement>();
            plusLayout.preferredWidth = 20;
            plusLayout.preferredHeight = 20;
            var plusImage = plusBtn.AddComponent<Image>();
            plusImage.color = AccentGreen;
            plusBtn.AddComponent<Button>();
        }

        #endregion

        #region Right Top Area

        private static GameObject CreateRightTopArea(GameObject parent)
        {
            var go = CreateChild(parent, "RightTopArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.sizeDelta = new Vector2(RIGHT_TOP_WIDTH, RIGHT_TOP_HEIGHT);
            rect.anchoredPosition = new Vector2(-20, -HEADER_HEIGHT - 10);

            var bg = go.AddComponent<Image>();
            bg.color = BgCard;

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(15, 15, 10, 10);
            layout.spacing = 10;
            layout.childAlignment = TextAnchor.MiddleLeft;

            // Stage Progress Text
            CreateLabel(go, "StageProgressText", "11-10 최후의 방어선! 알프트반선!", 14, TextPrimary, TextAlignmentOptions.Left);

            // Navigate Button
            CreateButton(go, "StageProgressNavigateButton", ">>", 30, 30, AccentGreen);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, -HEADER_HEIGHT);

            // Content Buttons Area
            var buttonsArea = CreateChild(go, "ContentButtons");
            SetStretch(buttonsArea);

            CreateLeftSideButtons(buttonsArea);
            CreateCenterAreaButtons(buttonsArea);
            CreateRightSideButtons(buttonsArea);

            // Widgets Area
            CreateWidgetsArea(go);

            return go;
        }

        private static void CreateLeftSideButtons(GameObject parent)
        {
            var leftSide = CreateChild(parent, "LeftSide");
            var rect = leftSide.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0.3f, 1);
            rect.offsetMin = new Vector2(20, 20);
            rect.offsetMax = new Vector2(0, -20);

            // Short Term Class Button (단기 속성반)
            var shortTermBtn = CreateContentButton(leftSide, "ShortTermClassButton", "단기 속성반", 
                new Vector2(0.5f, 0.7f), CONTENT_BUTTON_SIZE);
            CreateSubLabel(shortTermBtn, "SeasonInfoText", "02/19/11:00 시즌 시작");

            // Dimension Clash Button (차원 대충돌)
            var dimensionBtn = CreateContentButton(leftSide, "DimensionClashButton", "차원 대충돌", 
                new Vector2(0.5f, 0.25f), CONTENT_BUTTON_SIZE);
            CreateSubLabel(dimensionBtn, "DungeonInfoText", "딜: 리버리");
        }

        private static void CreateCenterAreaButtons(GameObject parent)
        {
            var center = CreateChild(parent, "CenterArea");
            var rect = center.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.3f, 0);
            rect.anchorMax = new Vector2(0.7f, 1);
            rect.offsetMin = new Vector2(0, 20);
            rect.offsetMax = new Vector2(0, -20);

            // Nuruling Busters Button (누루링 버스터즈)
            CreateContentButton(center, "NurulingBustersButton", "누루링 버스터즈", 
                new Vector2(0.5f, 0.8f), CONTENT_BUTTON_SIZE);

            // PVP Button
            var pvpBtn = CreateContentButton(center, "PVPButton", "PVP", 
                new Vector2(0.3f, 0.5f), SMALL_BUTTON_SIZE);
            
            // Add trophy icon placeholder
            var trophyIcon = CreateChild(pvpBtn, "TrophyIcon");
            var trophyRect = trophyIcon.GetComponent<RectTransform>();
            trophyRect.anchorMin = new Vector2(0.5f, 1);
            trophyRect.anchorMax = new Vector2(0.5f, 1);
            trophyRect.pivot = new Vector2(0.5f, 0);
            trophyRect.sizeDelta = new Vector2(40, 40);
            trophyRect.anchoredPosition = new Vector2(0, -10);
            var trophyImage = trophyIcon.AddComponent<Image>();
            trophyImage.color = AccentYellow;

            // Main Story Progress Panel (중앙 하단)
            CreateMainStoryProgressPanel(center);
        }

        private static void CreateMainStoryProgressPanel(GameObject parent)
        {
            var panel = CreateChild(parent, "MainStoryProgressPanel");
            var rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0);
            rect.anchorMax = new Vector2(0.5f, 0);
            rect.pivot = new Vector2(0.5f, 0);
            rect.sizeDelta = new Vector2(PROGRESS_PANEL_WIDTH, PROGRESS_PANEL_HEIGHT);
            rect.anchoredPosition = new Vector2(0, 80);

            var bg = panel.AddComponent<Image>();
            bg.color = ProgressPanelBg;

            var button = panel.AddComponent<Button>();
            button.targetGraphic = bg;

            var layout = panel.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(15, 15, 10, 10);
            layout.spacing = 5;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Progress Label
            CreateLabel(panel, "MainStoryProgressText", "제 1 엘리베이터 B7 도전중", 12, TextSecondary, TextAlignmentOptions.Center);

            // Stage Name
            CreateLabel(panel, "MainStoryStageNameText", "세계수 급착기지", 18, TextPrimary, TextAlignmentOptions.Center);

            // Time Remaining
            CreateLabel(panel, "MainStoryTimeRemainingText", "06일 17시간 07분", 12, AccentYellow, TextAlignmentOptions.Center);

            // Enter Button (MainStory button reference)
            var enterBtn = CreateChild(panel, "MainStoryButton");
            var enterRect = enterBtn.GetComponent<RectTransform>();
            var enterLayout = enterBtn.AddComponent<LayoutElement>();
            enterLayout.preferredHeight = 35;
            enterLayout.preferredWidth = 120;

            var enterBg = enterBtn.AddComponent<Image>();
            enterBg.color = AccentGreen;

            var enterButton = enterBtn.AddComponent<Button>();
            enterButton.targetGraphic = enterBg;

            var enterLabel = CreateChild(enterBtn, "Label");
            SetStretch(enterLabel);
            var enterTmp = enterLabel.AddComponent<TextMeshProUGUI>();
            enterTmp.text = "입장";
            enterTmp.fontSize = 16;
            enterTmp.fontStyle = FontStyles.Bold;
            enterTmp.color = TextPrimary;
            enterTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(enterTmp);
        }

        private static void CreateRightSideButtons(GameObject parent)
        {
            var rightSide = CreateChild(parent, "RightSide");
            var rect = rightSide.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.7f, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(0, 20);
            rect.offsetMax = new Vector2(-20, -20);

            // Dungeon Button (던전)
            CreateContentButton(rightSide, "DungeonButton", "던전", 
                new Vector2(0.5f, 0.75f), CONTENT_BUTTON_SIZE);

            // Invasion Button (침략)
            CreateContentButton(rightSide, "InvasionButton", "침략", 
                new Vector2(0.5f, 0.45f), CONTENT_BUTTON_SIZE);

            // Deck Formation Button (덱 편성)
            CreateContentButton(rightSide, "DeckFormationButton", "덱 편성", 
                new Vector2(0.5f, 0.15f), SMALL_BUTTON_SIZE);
        }

        private static GameObject CreateContentButton(GameObject parent, string name, string label, Vector2 anchorPos, float size)
        {
            var go = CreateChild(parent, name);
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = anchorPos;
            rect.anchorMax = anchorPos;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(size, size);
            rect.anchoredPosition = Vector2.zero;

            // Background
            var bg = go.AddComponent<Image>();
            bg.color = ContentButtonBg;

            var button = go.AddComponent<Button>();
            button.targetGraphic = bg;

            // Canvas Group for lock state
            go.AddComponent<CanvasGroup>();

            // Layout
            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.spacing = 5;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Icon placeholder
            var icon = CreateChild(go, "Icon");
            var iconLayout = icon.AddComponent<LayoutElement>();
            iconLayout.preferredWidth = size * 0.5f;
            iconLayout.preferredHeight = size * 0.5f;
            var iconImage = icon.AddComponent<Image>();
            iconImage.color = new Color32(100, 100, 100, 150);

            // Label
            CreateLabel(go, "ContentLabel", label, 14, new Color32(50, 50, 50, 255), TextAlignmentOptions.Center);

            return go;
        }

        private static void CreateSubLabel(GameObject parent, string name, string text)
        {
            var label = CreateChild(parent, name);
            var labelLayout = label.AddComponent<LayoutElement>();
            labelLayout.preferredHeight = 16;

            var tmp = label.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 10;
            tmp.color = new Color32(80, 80, 80, 255);
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);
        }

        #endregion

        #region Widgets Area

        private static void CreateWidgetsArea(GameObject parent)
        {
            var widgetsArea = CreateChild(parent, "WidgetsArea");
            var rect = widgetsArea.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(0.5f, 0);
            rect.sizeDelta = new Vector2(0, 80);
            rect.anchoredPosition = Vector2.zero;

            var bg = widgetsArea.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.3f);

            var layout = widgetsArea.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(20, 20, 10, 10);
            layout.spacing = 20;
            layout.childAlignment = TextAnchor.MiddleCenter;

            // Progress Widget
            CreateProgressWidget(widgetsArea);

            // Quick Action Widget
            CreateQuickActionWidget(widgetsArea);
        }

        private static void CreateProgressWidget(GameObject parent)
        {
            var widget = CreateChild(parent, "ProgressWidget");
            var layoutElem = widget.AddComponent<LayoutElement>();
            layoutElem.preferredWidth = 300;
            layoutElem.preferredHeight = 60;

            widget.AddComponent<ContentProgressWidget>();

            var bg = widget.AddComponent<Image>();
            bg.color = BgCard;

            var layout = widget.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.spacing = 10;
            layout.childAlignment = TextAnchor.MiddleLeft;

            // Progress info
            var infoArea = CreateChild(widget, "InfoArea");
            var infoLayout = infoArea.AddComponent<VerticalLayoutGroup>();
            infoLayout.spacing = 2;
            infoLayout.childAlignment = TextAnchor.MiddleLeft;

            CreateLabel(infoArea, "ProgressLabelText", "제 1 엘리베이터 B7", 10, TextSecondary, TextAlignmentOptions.Left);
            CreateLabel(infoArea, "StageNameText", "세계수 급착기지", 14, TextPrimary, TextAlignmentOptions.Left);
            CreateLabel(infoArea, "TimeRemainingText", "06일 17시간", 10, AccentYellow, TextAlignmentOptions.Left);

            // Navigate button
            var navBtn = CreateChild(widget, "NavigateButton");
            var navLayout = navBtn.AddComponent<LayoutElement>();
            navLayout.preferredWidth = 30;
            navLayout.preferredHeight = 30;

            var navBg = navBtn.AddComponent<Image>();
            navBg.color = AccentGreen;

            navBtn.AddComponent<Button>();

            var navLabel = CreateChild(navBtn, "Label");
            SetStretch(navLabel);
            var navTmp = navLabel.AddComponent<TextMeshProUGUI>();
            navTmp.text = ">>";
            navTmp.fontSize = 14;
            navTmp.color = TextPrimary;
            navTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(navTmp);
        }

        private static void CreateQuickActionWidget(GameObject parent)
        {
            var widget = CreateChild(parent, "QuickActionWidget");
            var layoutElem = widget.AddComponent<LayoutElement>();
            layoutElem.preferredWidth = 350;
            layoutElem.preferredHeight = 60;

            widget.AddComponent<QuickActionWidget>();

            var bg = widget.AddComponent<Image>();
            bg.color = BgCard;

            var layout = widget.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.spacing = 15;
            layout.childAlignment = TextAnchor.MiddleCenter;

            // Quick Entry Button
            CreateActionButton(widget, "QuickEntryButton", "QuickEntryText", "빠른전투불가", 100, 40);

            // Auto Repeat Toggle
            var autoBtn = CreateActionButton(widget, "AutoRepeatButton", "AutoRepeatText", "자동OFF", 70, 40);
            var autoIndicator = CreateChild(autoBtn, "AutoRepeatOnIndicator");
            var indicatorRect = autoIndicator.GetComponent<RectTransform>();
            indicatorRect.anchorMin = new Vector2(1, 1);
            indicatorRect.anchorMax = new Vector2(1, 1);
            indicatorRect.pivot = new Vector2(1, 1);
            indicatorRect.sizeDelta = new Vector2(10, 10);
            indicatorRect.anchoredPosition = new Vector2(-5, -5);
            var indicatorImage = autoIndicator.AddComponent<Image>();
            indicatorImage.color = AccentGreen;
            autoIndicator.SetActive(false);

            // Skip Ticket Button
            var skipBtn = CreateActionButton(widget, "SkipTicketButton", "SkipTicketCountText", "0", 60, 40);

            // Deck Formation Button (in widget)
            CreateActionButton(widget, "DeckFormationButton", "DeckFormationText", "덱 편성", 80, 40);
        }

        private static GameObject CreateActionButton(GameObject parent, string name, string textName, string text, float width, float height)
        {
            var btn = CreateChild(parent, name);
            var layoutElem = btn.AddComponent<LayoutElement>();
            layoutElem.preferredWidth = width;
            layoutElem.preferredHeight = height;

            var bg = btn.AddComponent<Image>();
            bg.color = ButtonActive;

            var button = btn.AddComponent<Button>();
            button.targetGraphic = bg;

            var label = CreateChild(btn, textName);
            SetStretch(label);
            var tmp = label.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 12;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return btn;
        }

        #endregion

        #region Overlay Layer

        private static GameObject CreateOverlayLayer(GameObject parent)
        {
            var go = CreateChild(parent, "OverlayLayer");
            SetStretch(go);
            return go;
        }

        #endregion

        #region Helpers

        private static GameObject CreateChild(GameObject parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            go.AddComponent<RectTransform>();
            return go;
        }

        private static void SetStretch(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
        }

        private static void SetAnchorTop(GameObject go, float height)
        {
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.sizeDelta = new Vector2(0, height);
            rect.anchoredPosition = Vector2.zero;
        }

        private static GameObject CreateButton(GameObject parent, string name, string text, float width, float height, Color bgColor)
        {
            var go = CreateChild(parent, name);

            var layout = go.AddComponent<LayoutElement>();
            layout.preferredWidth = width;
            layout.preferredHeight = height;

            var image = go.AddComponent<Image>();
            image.color = bgColor;

            var button = go.AddComponent<Button>();
            button.targetGraphic = image;

            var label = CreateChild(go, "Label");
            SetStretch(label);
            var tmp = label.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 16;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return go;
        }

        private static GameObject CreateLabel(GameObject parent, string name, string text, int fontSize, Color color, TextAlignmentOptions alignment)
        {
            var go = CreateChild(parent, name);

            var layout = go.AddComponent<LayoutElement>();
            layout.preferredHeight = fontSize + 10;

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;
            tmp.color = color;
            tmp.alignment = alignment;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Connect Serialized Fields

        private static void ConnectSerializedFields(GameObject root)
        {
            var screen = root.GetComponent<InGameContentDashboard>();
            if (screen == null) return;

            var so = new SerializedObject(screen);

            // Widgets
            ConnectField(so, "_progressWidget", root, "SafeArea/Content/WidgetsArea/ProgressWidget");
            ConnectField(so, "_quickActionWidget", root, "SafeArea/Content/WidgetsArea/QuickActionWidget");

            // Left Side
            ConnectField(so, "_shortTermClassButton", root, "SafeArea/Content/ContentButtons/LeftSide/ShortTermClassButton");
            ConnectField(so, "_shortTermClassSeasonText", root, "SafeArea/Content/ContentButtons/LeftSide/ShortTermClassButton/SeasonInfoText");
            ConnectField(so, "_dimensionClashButton", root, "SafeArea/Content/ContentButtons/LeftSide/DimensionClashButton");
            ConnectField(so, "_dimensionClashDungeonText", root, "SafeArea/Content/ContentButtons/LeftSide/DimensionClashButton/DungeonInfoText");

            // Center Area
            ConnectField(so, "_nurulingBustersButton", root, "SafeArea/Content/ContentButtons/CenterArea/NurulingBustersButton");
            ConnectField(so, "_pvpButton", root, "SafeArea/Content/ContentButtons/CenterArea/PVPButton");
            ConnectField(so, "_mainStoryButton", root, "SafeArea/Content/ContentButtons/CenterArea/MainStoryProgressPanel/MainStoryButton");
            ConnectField(so, "_mainStoryProgressText", root, "SafeArea/Content/ContentButtons/CenterArea/MainStoryProgressPanel/MainStoryProgressText");
            ConnectField(so, "_mainStoryStageNameText", root, "SafeArea/Content/ContentButtons/CenterArea/MainStoryProgressPanel/MainStoryStageNameText");
            ConnectField(so, "_mainStoryTimeRemainingText", root, "SafeArea/Content/ContentButtons/CenterArea/MainStoryProgressPanel/MainStoryTimeRemainingText");

            // Right Side
            ConnectField(so, "_dungeonButton", root, "SafeArea/Content/ContentButtons/RightSide/DungeonButton");
            ConnectField(so, "_invasionButton", root, "SafeArea/Content/ContentButtons/RightSide/InvasionButton");
            ConnectField(so, "_deckFormationButton", root, "SafeArea/Content/ContentButtons/RightSide/DeckFormationButton");

            // Right Top Area
            ConnectField(so, "_stageProgressText", root, "SafeArea/RightTopArea/StageProgressText");
            ConnectField(so, "_stageProgressNavigateButton", root, "SafeArea/RightTopArea/StageProgressNavigateButton");

            // Navigation
            ConnectField(so, "_backButton", root, "SafeArea/Header/BackButton");

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectField(SerializedObject so, string fieldName, GameObject root, string path)
        {
            var prop = so.FindProperty(fieldName);
            if (prop == null) return;

            var target = root.transform.Find(path);
            if (target == null) return;

            if (prop.propertyType == SerializedPropertyType.ObjectReference)
            {
                var fieldType = prop.type;

                if (fieldType.Contains("Button"))
                    prop.objectReferenceValue = target.GetComponent<Button>();
                else if (fieldType.Contains("TMP_Text") || fieldType.Contains("TextMeshProUGUI"))
                    prop.objectReferenceValue = target.GetComponent<TextMeshProUGUI>();
                else if (fieldType.Contains("ContentProgressWidget"))
                    prop.objectReferenceValue = target.GetComponent<ContentProgressWidget>();
                else if (fieldType.Contains("QuickActionWidget"))
                    prop.objectReferenceValue = target.GetComponent<QuickActionWidget>();
                else if (fieldType.Contains("Transform"))
                    prop.objectReferenceValue = target;
                else
                    prop.objectReferenceValue = target.gameObject;
            }
        }

        #endregion
    }
}
