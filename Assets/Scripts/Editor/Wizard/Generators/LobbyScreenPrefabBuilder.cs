using Sc.Contents.Lobby;
using Sc.Contents.Lobby.Widgets;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// LobbyScreen 전용 프리팹 빌더.
    /// 스펙: Docs/Specs/Lobby.md
    /// </summary>
    public static class LobbyScreenPrefabBuilder
    {
        #region Colors (Luminous Dark Fantasy Theme)

        private static readonly Color BgDeep = new Color32(10, 10, 18, 255);
        private static readonly Color BgCard = new Color32(25, 25, 45, 217);
        private static readonly Color BgGlass = new Color32(255, 255, 255, 8);
        private static readonly Color AccentPrimary = new Color32(0, 212, 255, 255);
        private static readonly Color AccentSecondary = new Color32(255, 107, 157, 255);
        private static readonly Color AccentGold = new Color32(255, 215, 0, 255);
        private static readonly Color AccentPurple = new Color32(168, 85, 247, 255);
        private static readonly Color TextPrimary = Color.white;
        private static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);
        private static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.4f);

        #endregion

        #region Constants

        private const float HEADER_HEIGHT = 80f;
        private const float BOTTOM_NAV_HEIGHT = 100f;
        private const float LEFT_TOP_WIDTH = 400f;
        private const float LEFT_TOP_HEIGHT = 300f;
        private const float RIGHT_TOP_WIDTH = 350f;
        private const float RIGHT_TOP_HEIGHT = 250f;
        private const float RIGHT_BOTTOM_WIDTH = 200f;
        private const float RIGHT_BOTTOM_HEIGHT = 180f;
        private const float PASS_BUTTON_WIDTH = 90f;
        private const float PASS_BUTTON_HEIGHT = 100f;
        private const float QUICK_BUTTON_SIZE = 80f;
        private const float CONTENT_NAV_WIDTH = 100f;
        private const float CONTENT_NAV_HEIGHT = 80f;

        #endregion

        /// <summary>
        /// LobbyScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot();

            // 1. Background
            CreateBackground(root);

            // 2. SafeArea
            var safeArea = CreateSafeArea(root);

            // 3. Header Area (ScreenHeader는 별도 프리팹이므로 플레이스홀더만)
            CreateHeaderArea(safeArea);

            // 4. Content Area
            var content = CreateContentArea(safeArea);

            // 5. LeftTopArea
            var leftTop = CreateLeftTopArea(content);
            var bannerCarousel = CreateEventBannerCarousel(leftTop);
            var passButtons = CreatePassButtonGroup(leftTop);

            // 6. RightTopArea
            var rightTop = CreateRightTopArea(content);
            var stageProgress = CreateStageProgressWidget(rightTop);
            var quickMenuButtons = CreateQuickMenuGrid(rightTop);

            // 7. CenterArea (Character Display)
            var centerArea = CreateCenterArea(content);
            var characterDisplay = CreateCharacterDisplay(centerArea);

            // 8. RightBottomArea (InGameContentDashboard)
            var rightBottom = CreateRightBottomArea(content);
            var (stageShortcut, stageShortcutLabel, adventureBtn) = CreateInGameDashboard(rightBottom);

            // 9. BottomNav
            var bottomNav = CreateBottomNav(safeArea);
            var contentNavButtons = CreateContentNavButtons(bottomNav);

            // 10. OverlayLayer
            CreateOverlayLayer(root);

            // 11. Connect SerializedFields
            ConnectSerializedFields(
                root,
                bannerCarousel,
                passButtons,
                stageProgress,
                quickMenuButtons,
                characterDisplay,
                stageShortcut,
                stageShortcutLabel,
                adventureBtn,
                contentNavButtons,
                bottomNav.GetComponentInChildren<ScrollRect>()
            );

            return root;
        }

        #region Root & Background

        private static GameObject CreateRoot()
        {
            var root = new GameObject("LobbyScreen");

            var rect = root.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            root.AddComponent<CanvasGroup>();
            root.AddComponent<LobbyScreen>();

            return root;
        }

        private static void CreateBackground(GameObject parent)
        {
            var bg = CreateChild(parent, "Background");
            SetStretch(bg);

            var image = bg.AddComponent<Image>();
            image.color = BgDeep;
            image.raycastTarget = true;
        }

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var safeArea = CreateChild(parent, "SafeArea");
            SetStretch(safeArea);
            return safeArea;
        }

        private static void CreateHeaderArea(GameObject parent)
        {
            var header = CreateChild(parent, "Header");
            var rect = header.AddComponent<RectTransform>();

            // Top anchored, full width, 80px height
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.sizeDelta = new Vector2(0, HEADER_HEIGHT);
            rect.anchoredPosition = Vector2.zero;

            // Placeholder for ScreenHeader
            var placeholder = CreateChild(header, "ScreenHeaderPlaceholder");
            SetStretch(placeholder);
            var img = placeholder.AddComponent<Image>();
            img.color = new Color(0, 0, 0, 0.5f);

            var text = CreateChild(placeholder, "Text");
            SetStretch(text);
            var tmp = text.AddComponent<TextMeshProUGUI>();
            tmp.text = "[ScreenHeader]";
            tmp.fontSize = 14;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.Center;
        }

        private static GameObject CreateContentArea(GameObject parent)
        {
            var content = CreateChild(parent, "Content");
            var rect = content.AddComponent<RectTransform>();

            // Stretch, but leave space for header and bottom nav
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(0, BOTTOM_NAV_HEIGHT);
            rect.offsetMax = new Vector2(0, -HEADER_HEIGHT);

            return content;
        }

        private static void CreateOverlayLayer(GameObject parent)
        {
            var overlay = CreateChild(parent, "OverlayLayer");
            SetStretch(overlay);
            // 팝업 등을 위한 오버레이 레이어
        }

        #endregion

        #region LeftTopArea

        private static GameObject CreateLeftTopArea(GameObject parent)
        {
            var area = CreateChild(parent, "LeftTopArea");
            var rect = area.AddComponent<RectTransform>();

            // Top-Left anchored
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(LEFT_TOP_WIDTH, LEFT_TOP_HEIGHT);
            rect.anchoredPosition = new Vector2(20, -20);

            var layout = area.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 16;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return area;
        }

        private static EventBannerCarousel CreateEventBannerCarousel(GameObject parent)
        {
            var carousel = CreateChild(parent, "EventBannerCarousel");
            var rect = carousel.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 150);

            var fitter = carousel.AddComponent<LayoutElement>();
            fitter.preferredHeight = 150;

            // Background
            var bgImage = carousel.AddComponent<Image>();
            bgImage.color = BgCard;

            // Banner Container
            var bannerContainer = CreateChild(carousel, "BannerContainer");
            var bannerRect = SetStretch(bannerContainer);
            bannerRect.offsetMin = new Vector2(0, 20);
            bannerRect.offsetMax = Vector2.zero;

            // Indicators
            var indicators = CreateChild(carousel, "Indicators");
            var indRect = indicators.AddComponent<RectTransform>();
            indRect.anchorMin = new Vector2(0.5f, 0);
            indRect.anchorMax = new Vector2(0.5f, 0);
            indRect.pivot = new Vector2(0.5f, 0);
            indRect.sizeDelta = new Vector2(100, 16);
            indRect.anchoredPosition = new Vector2(0, 4);

            var indLayout = indicators.AddComponent<HorizontalLayoutGroup>();
            indLayout.spacing = 8;
            indLayout.childAlignment = TextAnchor.MiddleCenter;
            indLayout.childControlWidth = false;
            indLayout.childControlHeight = false;

            // Indicator Prefab (hidden)
            var indicatorPrefab = CreateChild(carousel, "IndicatorPrefab");
            var indPrefabRect = indicatorPrefab.AddComponent<RectTransform>();
            indPrefabRect.sizeDelta = new Vector2(8, 8);
            var indPrefabImg = indicatorPrefab.AddComponent<Image>();
            indPrefabImg.color = TextMuted;
            indicatorPrefab.SetActive(false);

            // Add component and connect
            var comp = carousel.AddComponent<EventBannerCarousel>();
            ConnectBannerCarouselFields(comp, bannerContainer.transform, indicators.transform, indicatorPrefab);

            return comp;
        }

        private static PassButton[] CreatePassButtonGroup(GameObject parent)
        {
            var group = CreateChild(parent, "PassButtonGroup");
            var rect = group.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, PASS_BUTTON_HEIGHT);

            var fitter = group.AddComponent<LayoutElement>();
            fitter.preferredHeight = PASS_BUTTON_HEIGHT;

            var layout = group.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            string[] passTypes = { "LevelPass", "StoryPass", "TrialPass", "StepUpPackage" };
            string[] passLabels = { "레벨패스", "사록패스", "트라이얼패스", "스텝업패키지" };

            var buttons = new PassButton[4];
            for (int i = 0; i < 4; i++)
            {
                buttons[i] = CreatePassButton(group, passTypes[i], passLabels[i]);
            }

            return buttons;
        }

        private static PassButton CreatePassButton(GameObject parent, string passType, string label)
        {
            var btn = CreateChild(parent, $"PassButton_{passType}");

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button
            var button = btn.AddComponent<Button>();

            // Layout
            var layout = btn.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4;
            layout.padding = new RectOffset(4, 4, 8, 4);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            // Icon
            var icon = CreateChild(btn, "Icon");
            var iconRect = icon.AddComponent<RectTransform>();
            var iconFitter = icon.AddComponent<LayoutElement>();
            iconFitter.preferredWidth = 40;
            iconFitter.preferredHeight = 40;
            var iconImg = icon.AddComponent<Image>();
            iconImg.color = AccentGold;

            // Label
            var labelObj = CreateChild(btn, "Label");
            var labelFitter = labelObj.AddComponent<LayoutElement>();
            labelFitter.preferredHeight = 20;
            var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 10;
            labelTmp.color = TextSecondary;
            labelTmp.alignment = TextAlignmentOptions.Center;

            // New Badge
            var newBadge = CreateChild(btn, "NewBadge");
            var badgeRect = newBadge.AddComponent<RectTransform>();
            badgeRect.anchorMin = new Vector2(1, 1);
            badgeRect.anchorMax = new Vector2(1, 1);
            badgeRect.pivot = new Vector2(1, 1);
            badgeRect.sizeDelta = new Vector2(24, 14);
            badgeRect.anchoredPosition = new Vector2(-2, -2);
            var badgeBg = newBadge.AddComponent<Image>();
            badgeBg.color = AccentSecondary;
            var badgeText = CreateChild(newBadge, "Text");
            SetStretch(badgeText);
            var badgeTmp = badgeText.AddComponent<TextMeshProUGUI>();
            badgeTmp.text = "NEW";
            badgeTmp.fontSize = 8;
            badgeTmp.color = Color.white;
            badgeTmp.alignment = TextAlignmentOptions.Center;
            newBadge.SetActive(false);

            // Add component and connect
            var comp = btn.AddComponent<PassButton>();
            ConnectPassButtonFields(comp, button, iconImg, labelTmp, newBadge, passType);

            return comp;
        }

        #endregion

        #region RightTopArea

        private static GameObject CreateRightTopArea(GameObject parent)
        {
            var area = CreateChild(parent, "RightTopArea");
            var rect = area.AddComponent<RectTransform>();

            // Top-Right anchored
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.sizeDelta = new Vector2(RIGHT_TOP_WIDTH, RIGHT_TOP_HEIGHT);
            rect.anchoredPosition = new Vector2(-20, -20);

            var layout = area.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return area;
        }

        private static StageProgressWidget CreateStageProgressWidget(GameObject parent)
        {
            var widget = CreateChild(parent, "StageProgressWidget");
            var rect = widget.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 50);

            var fitter = widget.AddComponent<LayoutElement>();
            fitter.preferredHeight = 50;

            // Background
            var bgImage = widget.AddComponent<Image>();
            bgImage.color = BgCard;

            // Layout
            var layout = widget.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4;
            layout.padding = new RectOffset(12, 12, 8, 8);
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            // Stage Label
            var stageLabel = CreateChild(widget, "StageLabel");
            var stageFitter = stageLabel.AddComponent<LayoutElement>();
            stageFitter.preferredHeight = 16;
            var stageTmp = stageLabel.AddComponent<TextMeshProUGUI>();
            stageTmp.text = "11-10";
            stageTmp.fontSize = 12;
            stageTmp.color = AccentPrimary;
            stageTmp.fontStyle = FontStyles.Bold;

            // Stage Name
            var stageName = CreateChild(widget, "StageName");
            var nameFitter = stageName.AddComponent<LayoutElement>();
            nameFitter.preferredHeight = 18;
            var nameTmp = stageName.AddComponent<TextMeshProUGUI>();
            nameTmp.text = "최후의 방어선!";
            nameTmp.fontSize = 14;
            nameTmp.color = TextPrimary;

            // Add component and connect
            var comp = widget.AddComponent<StageProgressWidget>();
            ConnectStageProgressFields(comp, stageTmp, nameTmp);

            return comp;
        }

        private static QuickMenuButton[] CreateQuickMenuGrid(GameObject parent)
        {
            var grid = CreateChild(parent, "QuickMenuGrid");
            var rect = grid.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 180);

            var fitter = grid.AddComponent<LayoutElement>();
            fitter.preferredHeight = 180;
            fitter.flexibleHeight = 1;

            var layout = grid.AddComponent<GridLayoutGroup>();
            layout.cellSize = new Vector2(QUICK_BUTTON_SIZE, QUICK_BUTTON_SIZE);
            layout.spacing = new Vector2(8, 8);
            layout.startCorner = GridLayoutGroup.Corner.UpperLeft;
            layout.startAxis = GridLayoutGroup.Axis.Horizontal;
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layout.constraintCount = 4;

            string[] screens = { "LiveEventScreen", "FarmScreen", "FriendScreen", "QuestScreen",
                                "PowerUpScreen", "MonthlyScreen", "ReturnScreen", "MissionScreen" };
            string[] labels = { "이벤트", "평일농장", "친구", "퀘스트",
                              "강해지기", "월간달", "복귀환영", "미션" };

            var buttons = new QuickMenuButton[8];
            for (int i = 0; i < 8; i++)
            {
                buttons[i] = CreateQuickMenuButton(grid, screens[i], labels[i]);
            }

            return buttons;
        }

        private static QuickMenuButton CreateQuickMenuButton(GameObject parent, string targetScreen, string label)
        {
            var btn = CreateChild(parent, $"QuickMenuButton_{targetScreen}");

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button
            var button = btn.AddComponent<Button>();

            // Layout
            var layout = btn.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4;
            layout.padding = new RectOffset(4, 4, 8, 4);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            // Icon
            var icon = CreateChild(btn, "Icon");
            var iconFitter = icon.AddComponent<LayoutElement>();
            iconFitter.preferredWidth = 32;
            iconFitter.preferredHeight = 32;
            var iconImg = icon.AddComponent<Image>();
            iconImg.color = AccentPrimary;

            // Label
            var labelObj = CreateChild(btn, "Label");
            var labelFitter = labelObj.AddComponent<LayoutElement>();
            labelFitter.preferredHeight = 14;
            var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 10;
            labelTmp.color = TextSecondary;
            labelTmp.alignment = TextAlignmentOptions.Center;

            // Badge
            var badge = CreateChild(btn, "Badge");
            var badgeRect = badge.AddComponent<RectTransform>();
            badgeRect.anchorMin = new Vector2(1, 1);
            badgeRect.anchorMax = new Vector2(1, 1);
            badgeRect.pivot = new Vector2(1, 1);
            badgeRect.sizeDelta = new Vector2(20, 20);
            badgeRect.anchoredPosition = new Vector2(-2, -2);
            var badgeBg = badge.AddComponent<Image>();
            badgeBg.color = AccentSecondary;
            var badgeCount = CreateChild(badge, "Count");
            SetStretch(badgeCount);
            var badgeTmp = badgeCount.AddComponent<TextMeshProUGUI>();
            badgeTmp.text = "";
            badgeTmp.fontSize = 10;
            badgeTmp.color = Color.white;
            badgeTmp.alignment = TextAlignmentOptions.Center;
            badge.SetActive(false);

            // Add component and connect
            var comp = btn.AddComponent<QuickMenuButton>();
            ConnectQuickMenuButtonFields(comp, button, iconImg, labelTmp, badge, badgeTmp, targetScreen);

            return comp;
        }

        #endregion

        #region CenterArea

        private static GameObject CreateCenterArea(GameObject parent)
        {
            var area = CreateChild(parent, "CenterArea");
            var rect = area.AddComponent<RectTransform>();

            // Left 55% of content area
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0.55f, 1);
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, -LEFT_TOP_HEIGHT - 20);

            return area;
        }

        private static CharacterDisplayWidget CreateCharacterDisplay(GameObject parent)
        {
            var display = CreateChild(parent, "CharacterDisplay");
            SetStretch(display);

            // Glow Effect
            var glow = CreateChild(display, "CharacterGlow");
            var glowRect = glow.AddComponent<RectTransform>();
            glowRect.anchorMin = new Vector2(0.5f, 0);
            glowRect.anchorMax = new Vector2(0.5f, 0);
            glowRect.sizeDelta = new Vector2(300, 100);
            glowRect.anchoredPosition = new Vector2(0, 50);
            var glowImage = glow.AddComponent<Image>();
            glowImage.color = new Color(0, 0.83f, 1f, 0.2f);
            glowImage.raycastTarget = false;

            // Character Image
            var charImage = CreateChild(display, "CharacterImage");
            var charRect = charImage.AddComponent<RectTransform>();
            charRect.anchorMin = new Vector2(0.5f, 0);
            charRect.anchorMax = new Vector2(0.5f, 0);
            charRect.pivot = new Vector2(0.5f, 0);
            charRect.sizeDelta = new Vector2(350, 500);
            charRect.anchoredPosition = new Vector2(0, 20);
            var charImg = charImage.AddComponent<Image>();
            charImg.color = new Color(1, 1, 1, 0.1f);
            var charBtn = charImage.AddComponent<Button>();

            // Placeholder Text
            var placeholder = CreateChild(charImage, "Placeholder");
            SetStretch(placeholder);
            var placeholderTmp = placeholder.AddComponent<TextMeshProUGUI>();
            placeholderTmp.text = "CHARACTER";
            placeholderTmp.fontSize = 24;
            placeholderTmp.color = TextMuted;
            placeholderTmp.alignment = TextAlignmentOptions.Center;

            // Dialogue Box
            var dialogueBox = CreateChild(display, "DialogueBox");
            var dialogueRect = dialogueBox.AddComponent<RectTransform>();
            dialogueRect.anchorMin = new Vector2(0.5f, 0);
            dialogueRect.anchorMax = new Vector2(0.5f, 0);
            dialogueRect.pivot = new Vector2(0.5f, 0);
            dialogueRect.sizeDelta = new Vector2(300, 60);
            dialogueRect.anchoredPosition = new Vector2(0, 520);
            var dialogueBg = dialogueBox.AddComponent<Image>();
            dialogueBg.color = BgCard;

            var dialogueText = CreateChild(dialogueBox, "DialogueText");
            SetStretch(dialogueText);
            var dialogueTmp = dialogueText.AddComponent<TextMeshProUGUI>();
            dialogueTmp.text = "내가 놀고 싶을 때 놀러 오고...";
            dialogueTmp.fontSize = 14;
            dialogueTmp.color = TextPrimary;
            dialogueTmp.alignment = TextAlignmentOptions.Center;
            dialogueTmp.margin = new Vector4(10, 10, 10, 10);

            // Left Arrow
            var leftArrow = CreateChild(display, "LeftArrow");
            var leftRect = leftArrow.AddComponent<RectTransform>();
            leftRect.anchorMin = new Vector2(0, 0.5f);
            leftRect.anchorMax = new Vector2(0, 0.5f);
            leftRect.pivot = new Vector2(0, 0.5f);
            leftRect.sizeDelta = new Vector2(40, 80);
            leftRect.anchoredPosition = new Vector2(10, 0);
            var leftBg = leftArrow.AddComponent<Image>();
            leftBg.color = BgCard;
            var leftBtn = leftArrow.AddComponent<Button>();
            var leftText = CreateChild(leftArrow, "Text");
            SetStretch(leftText);
            var leftTmp = leftText.AddComponent<TextMeshProUGUI>();
            leftTmp.text = "<";
            leftTmp.fontSize = 24;
            leftTmp.color = TextPrimary;
            leftTmp.alignment = TextAlignmentOptions.Center;

            // Right Arrow
            var rightArrow = CreateChild(display, "RightArrow");
            var rightRect = rightArrow.AddComponent<RectTransform>();
            rightRect.anchorMin = new Vector2(1, 0.5f);
            rightRect.anchorMax = new Vector2(1, 0.5f);
            rightRect.pivot = new Vector2(1, 0.5f);
            rightRect.sizeDelta = new Vector2(40, 80);
            rightRect.anchoredPosition = new Vector2(-10, 0);
            var rightBg = rightArrow.AddComponent<Image>();
            rightBg.color = BgCard;
            var rightBtn = rightArrow.AddComponent<Button>();
            var rightText = CreateChild(rightArrow, "Text");
            SetStretch(rightText);
            var rightTmp = rightText.AddComponent<TextMeshProUGUI>();
            rightTmp.text = ">";
            rightTmp.fontSize = 24;
            rightTmp.color = TextPrimary;
            rightTmp.alignment = TextAlignmentOptions.Center;

            // Add component and connect
            var comp = display.AddComponent<CharacterDisplayWidget>();
            ConnectCharacterDisplayFields(comp, charImg, dialogueTmp, charBtn, leftBtn, rightBtn, glowImage);

            return comp;
        }

        #endregion

        #region RightBottomArea

        private static GameObject CreateRightBottomArea(GameObject parent)
        {
            var area = CreateChild(parent, "RightBottomArea");
            var rect = area.AddComponent<RectTransform>();

            // Bottom-Right anchored
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            rect.sizeDelta = new Vector2(RIGHT_BOTTOM_WIDTH, RIGHT_BOTTOM_HEIGHT);
            rect.anchoredPosition = new Vector2(-20, 20);

            return area;
        }

        private static (Button, TMP_Text, Button) CreateInGameDashboard(GameObject parent)
        {
            var dashboard = CreateChild(parent, "InGameContentDashboard");
            SetStretch(dashboard);

            // Background
            var bgImage = dashboard.AddComponent<Image>();
            bgImage.color = BgCard;

            // Layout
            var layout = dashboard.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Stage Shortcut Button
            var shortcut = CreateChild(dashboard, "StageShortcutButton");
            var shortcutFitter = shortcut.AddComponent<LayoutElement>();
            shortcutFitter.preferredHeight = 50;
            var shortcutBg = shortcut.AddComponent<Image>();
            shortcutBg.color = AccentPrimary;
            var shortcutBtn = shortcut.AddComponent<Button>();

            var shortcutLabel = CreateChild(shortcut, "Label");
            SetStretch(shortcutLabel);
            var shortcutTmp = shortcutLabel.AddComponent<TextMeshProUGUI>();
            shortcutTmp.text = "11-1 바로 가자!";
            shortcutTmp.fontSize = 14;
            shortcutTmp.fontStyle = FontStyles.Bold;
            shortcutTmp.color = BgDeep;
            shortcutTmp.alignment = TextAlignmentOptions.Center;

            // Character Mini Group (placeholder)
            var miniGroup = CreateChild(dashboard, "CharacterMiniGroup");
            var miniFitter = miniGroup.AddComponent<LayoutElement>();
            miniFitter.preferredHeight = 50;
            var miniLayout = miniGroup.AddComponent<HorizontalLayoutGroup>();
            miniLayout.spacing = 4;
            miniLayout.childAlignment = TextAnchor.MiddleCenter;

            for (int i = 0; i < 4; i++)
            {
                var mini = CreateChild(miniGroup, $"CharMini_{i}");
                var miniRect = mini.AddComponent<RectTransform>();
                miniRect.sizeDelta = new Vector2(40, 40);
                var miniImg = mini.AddComponent<Image>();
                miniImg.color = new Color(1, 1, 1, 0.2f);
            }

            // Adventure Button
            var adventure = CreateChild(dashboard, "AdventureButton");
            var advFitter = adventure.AddComponent<LayoutElement>();
            advFitter.preferredHeight = 50;
            var advBg = adventure.AddComponent<Image>();
            advBg.color = AccentSecondary;
            var advBtn = adventure.AddComponent<Button>();

            var advLabel = CreateChild(adventure, "Label");
            SetStretch(advLabel);
            var advTmp = advLabel.AddComponent<TextMeshProUGUI>();
            advTmp.text = "모험";
            advTmp.fontSize = 16;
            advTmp.fontStyle = FontStyles.Bold;
            advTmp.color = Color.white;
            advTmp.alignment = TextAlignmentOptions.Center;

            return (shortcutBtn, shortcutTmp, advBtn);
        }

        #endregion

        #region BottomNav

        private static GameObject CreateBottomNav(GameObject parent)
        {
            var nav = CreateChild(parent, "BottomNav");
            var rect = nav.AddComponent<RectTransform>();

            // Bottom anchored, full width
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(0.5f, 0);
            rect.sizeDelta = new Vector2(0, BOTTOM_NAV_HEIGHT);
            rect.anchoredPosition = Vector2.zero;

            // Background
            var bgImage = nav.AddComponent<Image>();
            bgImage.color = new Color(0, 0, 0, 0.9f);

            // ScrollRect for horizontal scrolling
            var scrollRect = nav.AddComponent<ScrollRect>();
            scrollRect.horizontal = true;
            scrollRect.vertical = false;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;

            // Viewport
            var viewport = CreateChild(nav, "Viewport");
            var vpRect = SetStretch(viewport);
            vpRect.offsetMin = new Vector2(10, 10);
            vpRect.offsetMax = new Vector2(-10, -10);
            var vpMask = viewport.AddComponent<Mask>();
            vpMask.showMaskGraphic = false;
            var vpImage = viewport.AddComponent<Image>();

            // Content
            var content = CreateChild(viewport, "Content");
            var contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0, 0.5f);
            contentRect.anchorMax = new Vector2(0, 0.5f);
            contentRect.pivot = new Vector2(0, 0.5f);
            contentRect.sizeDelta = new Vector2(800, CONTENT_NAV_HEIGHT);

            var contentLayout = content.AddComponent<HorizontalLayoutGroup>();
            contentLayout.spacing = 8;
            contentLayout.childControlWidth = false;
            contentLayout.childControlHeight = true;
            contentLayout.childForceExpandWidth = false;
            contentLayout.childForceExpandHeight = true;

            var contentFitter = content.AddComponent<ContentSizeFitter>();
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.viewport = vpRect;
            scrollRect.content = contentRect;

            return nav;
        }

        private static ContentNavButton[] CreateContentNavButtons(GameObject parent)
        {
            var viewport = parent.transform.Find("Viewport");
            var content = viewport?.Find("Content");
            if (content == null) return new ContentNavButton[0];

            string[] screens = { "GachaScreen", "CashShopScreen", "ShopScreen", "CharacterListScreen",
                               "CardScreen", "TheaterScreen", "GuildScreen" };
            string[] labels = { "모집", "캐시상점", "상점", "사도", "카드", "극장", "고단" };
            Color[] colors = { AccentPurple, AccentGold, AccentPrimary, AccentSecondary,
                             AccentPrimary, AccentPurple, AccentGold };

            var buttons = new ContentNavButton[7];
            for (int i = 0; i < 7; i++)
            {
                buttons[i] = CreateContentNavButton(content.gameObject, screens[i], labels[i], colors[i]);
            }

            return buttons;
        }

        private static ContentNavButton CreateContentNavButton(GameObject parent, string targetScreen, string label, Color accentColor)
        {
            var btn = CreateChild(parent, $"ContentNavButton_{targetScreen}");
            var rect = btn.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(CONTENT_NAV_WIDTH, CONTENT_NAV_HEIGHT);

            var fitter = btn.AddComponent<LayoutElement>();
            fitter.preferredWidth = CONTENT_NAV_WIDTH;

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button
            var button = btn.AddComponent<Button>();

            // Layout
            var layout = btn.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4;
            layout.padding = new RectOffset(8, 8, 12, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            // Icon
            var icon = CreateChild(btn, "Icon");
            var iconFitter = icon.AddComponent<LayoutElement>();
            iconFitter.preferredWidth = 32;
            iconFitter.preferredHeight = 32;
            var iconImg = icon.AddComponent<Image>();
            iconImg.color = accentColor;

            // Label
            var labelObj = CreateChild(btn, "Label");
            var labelFitter = labelObj.AddComponent<LayoutElement>();
            labelFitter.preferredHeight = 18;
            var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 12;
            labelTmp.color = TextSecondary;
            labelTmp.alignment = TextAlignmentOptions.Center;

            // Badge
            var badge = CreateChild(btn, "Badge");
            var badgeRect = badge.AddComponent<RectTransform>();
            badgeRect.anchorMin = new Vector2(1, 1);
            badgeRect.anchorMax = new Vector2(1, 1);
            badgeRect.pivot = new Vector2(1, 1);
            badgeRect.sizeDelta = new Vector2(20, 20);
            badgeRect.anchoredPosition = new Vector2(-4, -4);
            var badgeBg = badge.AddComponent<Image>();
            badgeBg.color = AccentSecondary;
            var badgeCount = CreateChild(badge, "Count");
            SetStretch(badgeCount);
            var badgeTmp = badgeCount.AddComponent<TextMeshProUGUI>();
            badgeTmp.text = "";
            badgeTmp.fontSize = 10;
            badgeTmp.color = Color.white;
            badgeTmp.alignment = TextAlignmentOptions.Center;
            badge.SetActive(false);

            // Glow Effect (hidden by default)
            var glow = CreateChild(btn, "GlowEffect");
            var glowRect = glow.AddComponent<RectTransform>();
            glowRect.anchorMin = new Vector2(0.5f, 0);
            glowRect.anchorMax = new Vector2(0.5f, 0);
            glowRect.pivot = new Vector2(0.5f, 0);
            glowRect.sizeDelta = new Vector2(60, 4);
            glowRect.anchoredPosition = new Vector2(0, 2);
            var glowImg = glow.AddComponent<Image>();
            glowImg.color = accentColor;
            glow.SetActive(false);

            // Add component and connect
            var comp = btn.AddComponent<ContentNavButton>();
            ConnectContentNavButtonFields(comp, button, iconImg, labelTmp, badge, badgeTmp, glowImg, targetScreen);

            return comp;
        }

        #endregion

        #region SerializeField Connections

        private static void ConnectSerializedFields(
            GameObject root,
            EventBannerCarousel bannerCarousel,
            PassButton[] passButtons,
            StageProgressWidget stageProgress,
            QuickMenuButton[] quickMenuButtons,
            CharacterDisplayWidget characterDisplay,
            Button stageShortcut,
            TMP_Text stageShortcutLabel,
            Button adventureBtn,
            ContentNavButton[] contentNavButtons,
            ScrollRect bottomNavScroll)
        {
            var lobbyScreen = root.GetComponent<LobbyScreen>();
            var so = new SerializedObject(lobbyScreen);

            // Left Top Area
            so.FindProperty("_eventBannerCarousel").objectReferenceValue = bannerCarousel;

            var passButtonsProp = so.FindProperty("_passButtons");
            passButtonsProp.arraySize = passButtons.Length;
            for (int i = 0; i < passButtons.Length; i++)
            {
                passButtonsProp.GetArrayElementAtIndex(i).objectReferenceValue = passButtons[i];
            }

            // Right Top Area
            so.FindProperty("_stageProgressWidget").objectReferenceValue = stageProgress;

            var quickMenuProp = so.FindProperty("_quickMenuButtons");
            quickMenuProp.arraySize = quickMenuButtons.Length;
            for (int i = 0; i < quickMenuButtons.Length; i++)
            {
                quickMenuProp.GetArrayElementAtIndex(i).objectReferenceValue = quickMenuButtons[i];
            }

            // Center Area
            so.FindProperty("_characterDisplay").objectReferenceValue = characterDisplay;

            // Right Bottom Area
            var dashboardGO = stageShortcut?.transform.parent?.gameObject;
            so.FindProperty("_inGameDashboard").objectReferenceValue = dashboardGO;
            so.FindProperty("_stageShortcutButton").objectReferenceValue = stageShortcut;
            so.FindProperty("_stageShortcutLabel").objectReferenceValue = stageShortcutLabel;
            so.FindProperty("_adventureButton").objectReferenceValue = adventureBtn;

            // Bottom Nav
            var contentNavProp = so.FindProperty("_contentNavButtons");
            contentNavProp.arraySize = contentNavButtons.Length;
            for (int i = 0; i < contentNavButtons.Length; i++)
            {
                contentNavProp.GetArrayElementAtIndex(i).objectReferenceValue = contentNavButtons[i];
            }

            so.FindProperty("_bottomNavScroll").objectReferenceValue = bottomNavScroll;

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectBannerCarouselFields(EventBannerCarousel comp, Transform bannerContainer, Transform indicatorContainer, GameObject indicatorPrefab)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_bannerContainer").objectReferenceValue = bannerContainer;
            so.FindProperty("_indicatorContainer").objectReferenceValue = indicatorContainer;
            so.FindProperty("_indicatorPrefab").objectReferenceValue = indicatorPrefab;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectPassButtonFields(PassButton comp, Button button, Image icon, TMP_Text label, GameObject newBadge, string passType)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_button").objectReferenceValue = button;
            so.FindProperty("_icon").objectReferenceValue = icon;
            so.FindProperty("_label").objectReferenceValue = label;
            so.FindProperty("_newBadge").objectReferenceValue = newBadge;
            so.FindProperty("_passType").stringValue = passType;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectStageProgressFields(StageProgressWidget comp, TMP_Text stageLabel, TMP_Text stageName)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_stageLabel").objectReferenceValue = stageLabel;
            so.FindProperty("_stageName").objectReferenceValue = stageName;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectQuickMenuButtonFields(QuickMenuButton comp, Button button, Image icon, TMP_Text label, GameObject badge, TMP_Text badgeCount, string targetScreen)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_button").objectReferenceValue = button;
            so.FindProperty("_icon").objectReferenceValue = icon;
            so.FindProperty("_label").objectReferenceValue = label;
            so.FindProperty("_badge").objectReferenceValue = badge;
            so.FindProperty("_badgeCount").objectReferenceValue = badgeCount;
            so.FindProperty("_targetScreen").stringValue = targetScreen;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectCharacterDisplayFields(CharacterDisplayWidget comp, Image charImage, TMP_Text dialogueText, Button charButton, Button leftArrow, Button rightArrow, Image glow)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_characterImage").objectReferenceValue = charImage;
            so.FindProperty("_dialogueText").objectReferenceValue = dialogueText;
            so.FindProperty("_characterButton").objectReferenceValue = charButton;
            so.FindProperty("_leftArrow").objectReferenceValue = leftArrow;
            so.FindProperty("_rightArrow").objectReferenceValue = rightArrow;
            so.FindProperty("_glowEffect").objectReferenceValue = glow;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectContentNavButtonFields(ContentNavButton comp, Button button, Image icon, TMP_Text label, GameObject badge, TMP_Text badgeCount, Image glow, string targetScreen)
        {
            var so = new SerializedObject(comp);
            so.FindProperty("_button").objectReferenceValue = button;
            so.FindProperty("_icon").objectReferenceValue = icon;
            so.FindProperty("_label").objectReferenceValue = label;
            so.FindProperty("_badge").objectReferenceValue = badge;
            so.FindProperty("_badgeCount").objectReferenceValue = badgeCount;
            so.FindProperty("_glowEffect").objectReferenceValue = glow;
            so.FindProperty("_targetScreen").stringValue = targetScreen;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        #endregion

        #region Helpers

        private static GameObject CreateChild(GameObject parent, string name)
        {
            var child = new GameObject(name);
            child.transform.SetParent(parent.transform, false);
            return child;
        }

        private static RectTransform SetStretch(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>();
            if (rect == null)
                rect = go.AddComponent<RectTransform>();

            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            return rect;
        }

        #endregion
    }
}
