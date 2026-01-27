using Sc.Contents.Stage;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// PartySelectScreen 전용 프리팹 빌더.
    /// 레퍼런스: Docs/Design/Reference/PartySelect.jpg
    /// </summary>
    public static class PartySelectScreenPrefabBuilder
    {
        #region Colors (Battle Preparation Theme)

        // Deep background
        private static readonly Color BgDeep = new Color32(8, 10, 18, 255);
        private static readonly Color BgDark = new Color32(15, 18, 28, 255);
        private static readonly Color BgPanel = new Color32(22, 26, 38, 255);

        // Accent colors - tactical interface
        private static readonly Color AccentBlue = new Color32(80, 140, 255, 255);
        private static readonly Color AccentGreen = new Color32(100, 220, 140, 255);
        private static readonly Color AccentRed = new Color32(255, 90, 90, 255);
        private static readonly Color AccentOrange = new Color32(255, 160, 60, 255);

        // Text colors
        private static readonly Color TextPrimary = Color.white;
        private static readonly Color TextSecondary = new Color(1f, 1f, 1f, 0.7f);
        private static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.5f);
        private static readonly Color TextDim = new Color(1f, 1f, 1f, 0.3f);

        // UI elements
        private static readonly Color BorderColor = new Color(1f, 1f, 1f, 0.15f);
        private static readonly Color SlotEmpty = new Color32(40, 45, 60, 180);
        private static readonly Color SlotFilled = new Color32(60, 70, 95, 200);

        #endregion

        #region Constants

        private const float HEADER_HEIGHT = 80f;
        private const float LEFT_AREA_WIDTH_RATIO = 0.6f;
        private const float STAGE_INFO_PANEL_WIDTH = 300f;
        private const float STAGE_INFO_PANEL_HEIGHT = 120f;
        private const float ACTION_BAR_HEIGHT = 80f;
        private const float PARTY_SLOT_SIZE = 90f;
        private const float PARTY_SLOT_SPACING = 10f;
        private const float CHARACTER_SLOT_SIZE = 100f;
        private const float GRID_SPACING = 10f;

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
            var root = CreateRoot();

            // 1. Background
            CreateBackground(root);

            // 2. SafeArea (전체 컨텐츠 영역)
            var safeArea = CreateSafeArea(root);

            // 3. Header (BackButton, StageInfoText, CurrencyHUD, HomeButton)
            var (backButton, homeButton, stageNameText, stageDifficultyText) = CreateHeader(safeArea);

            // 4. Content (Left + Right)
            var content = CreateContent(safeArea);

            // 5. LeftArea (60%)
            var leftArea = CreateLeftArea(content);
            var (frontLineContainer, backLineContainer) = CreateBattlePreviewArea(leftArea);
            var (partyFormation, enemyFormation, battlePreviewArea) = CreateFormationAreas(leftArea);
            var (autoFormButton, stageInfoButton, formationSettingButton) = CreateQuickActionBar(leftArea);

            // 6. StageInfoPanel (상단 중앙)
            var (staminaCostText, recommendedPowerText, characterCountText, partyPowerText) =
                CreateStageInfoPanel(safeArea);

            // 7. RightPanel (40%)
            var rightPanel = CreateRightPanel(content);
            CreateTabBar(rightPanel);
            var characterListContainer = CreateCharacterGrid(rightPanel);
            var (startButton, quickBattleButton, autoToggleButton) = CreateActionBar(rightPanel);

            // 8. Widget Placeholders
            var stageInfoWidget = CreateWidgetPlaceholder(safeArea, "StageInfoWidget");
            var characterSelectWidget = CreateWidgetPlaceholder(rightPanel, "CharacterSelectWidget");

            // 9. PartySlotContainer
            var partySlotContainer = CreatePartySlotContainer(leftArea);

            // 10. Connect SerializedFields
            ConnectSerializedFields(root,
                backButton, homeButton,
                stageNameText, stageDifficultyText,
                frontLineContainer, backLineContainer,
                battlePreviewArea, partyFormation, enemyFormation,
                autoFormButton, stageInfoButton, formationSettingButton,
                characterListContainer, partySlotContainer,
                staminaCostText, recommendedPowerText, characterCountText, partyPowerText,
                startButton, quickBattleButton, autoToggleButton,
                stageInfoWidget, characterSelectWidget);

            return root;
        }

        #region Root

        private static GameObject CreateRoot()
        {
            var root = new GameObject("PartySelectScreen");

            var rect = root.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            root.AddComponent<CanvasGroup>();
            root.AddComponent<PartySelectScreen>();

            return root;
        }

        #endregion

        #region Background

        private static void CreateBackground(GameObject parent)
        {
            var bg = CreateChild(parent, "Background");
            SetStretch(bg);

            var bgImage = bg.AddComponent<Image>();
            bgImage.color = BgDeep;
            bgImage.raycastTarget = false;
        }

        #endregion

        #region SafeArea

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var safeArea = CreateChild(parent, "SafeArea");
            SetStretch(safeArea);

            return safeArea;
        }

        #endregion

        #region Header

        private static (Button backButton, Button homeButton, TMP_Text stageNameText, TMP_Text stageDifficultyText)
            CreateHeader(GameObject parent)
        {
            var header = CreateChild(parent, "Header");
            var headerRect = header.AddComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0, 1);
            headerRect.anchorMax = Vector2.one;
            headerRect.pivot = new Vector2(0.5f, 1);
            headerRect.sizeDelta = new Vector2(0, HEADER_HEIGHT);
            headerRect.anchoredPosition = Vector2.zero;

            // Background
            var headerBg = header.AddComponent<Image>();
            headerBg.color = new Color(BgDark.r, BgDark.g, BgDark.b, 0.95f);

            // BackButton (Left)
            var backButton = CreateHeaderButton(header, "BackButton", new Vector2(20, 0), "<");

            // HomeButton (Right)
            var homeButton = CreateHeaderButton(header, "HomeButton", new Vector2(-20, 0), "H", false);

            // StageInfoText (Center)
            var stageInfoContainer = CreateChild(header, "StageInfoContainer");
            var stageInfoRect = stageInfoContainer.AddComponent<RectTransform>();
            stageInfoRect.anchorMin = new Vector2(0.5f, 0.5f);
            stageInfoRect.anchorMax = new Vector2(0.5f, 0.5f);
            stageInfoRect.sizeDelta = new Vector2(400, 60);

            var stageNameText = CreateText(stageInfoContainer, "StageNameText", "10-7. 깜빡이는 터널!", 20,
                TextAlignmentOptions.Center, new Vector2(0, 5));
            stageNameText.fontStyle = FontStyles.Bold;
            stageNameText.color = TextPrimary;

            var stageDifficultyText = CreateText(stageInfoContainer, "StageDifficultyText", "Hard", 14,
                TextAlignmentOptions.Center, new Vector2(0, -15));
            stageDifficultyText.color = AccentOrange;

            // CurrencyHUD Placeholder
            var currencyHUD = CreateChild(header, "CurrencyHUD");
            var currencyRect = currencyHUD.AddComponent<RectTransform>();
            currencyRect.anchorMin = new Vector2(1, 0.5f);
            currencyRect.anchorMax = new Vector2(1, 0.5f);
            currencyRect.pivot = new Vector2(1, 0.5f);
            currencyRect.sizeDelta = new Vector2(200, 40);
            currencyRect.anchoredPosition = new Vector2(-80, 0);

            return (backButton, homeButton, stageNameText, stageDifficultyText);
        }

        private static Button CreateHeaderButton(GameObject parent, string name, Vector2 position, string label,
            bool isLeft = true)
        {
            var btnObj = CreateChild(parent, name);
            var btnRect = btnObj.AddComponent<RectTransform>();

            btnRect.anchorMin = new Vector2(isLeft ? 0 : 1, 0.5f);
            btnRect.anchorMax = new Vector2(isLeft ? 0 : 1, 0.5f);
            btnRect.pivot = new Vector2(isLeft ? 0 : 1, 0.5f);
            btnRect.sizeDelta = new Vector2(60, 60);
            btnRect.anchoredPosition = position;

            var btnImage = btnObj.AddComponent<Image>();
            btnImage.color = new Color(1, 1, 1, 0.1f);

            var button = btnObj.AddComponent<Button>();

            var btnText = CreateChild(btnObj, "Text");
            SetStretch(btnText);
            var tmp = btnText.AddComponent<TextMeshProUGUI>();
            tmp.text = label;
            tmp.fontSize = 20;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = TextPrimary;
            ApplyFont(tmp);

            return button;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var content = CreateChild(parent, "Content");
            var contentRect = content.AddComponent<RectTransform>();
            contentRect.anchorMin = Vector2.zero;
            contentRect.anchorMax = Vector2.one;
            contentRect.offsetMin = new Vector2(0, 0);
            contentRect.offsetMax = new Vector2(0, -HEADER_HEIGHT);

            return content;
        }

        #endregion

        #region LeftArea

        private static GameObject CreateLeftArea(GameObject parent)
        {
            var leftArea = CreateChild(parent, "LeftArea");
            var leftRect = leftArea.AddComponent<RectTransform>();
            leftRect.anchorMin = Vector2.zero;
            leftRect.anchorMax = new Vector2(LEFT_AREA_WIDTH_RATIO, 1);
            leftRect.offsetMin = Vector2.zero;
            leftRect.offsetMax = Vector2.zero;

            return leftArea;
        }

        private static (Transform frontLine, Transform backLine) CreateBattlePreviewArea(GameObject parent)
        {
            var battlePreview = CreateChild(parent, "BattlePreviewArea");
            var previewRect = battlePreview.AddComponent<RectTransform>();
            previewRect.anchorMin = new Vector2(0.5f, 0.5f);
            previewRect.anchorMax = new Vector2(0.5f, 0.5f);
            previewRect.sizeDelta = new Vector2(600, 400);

            // FrontLineContainer (앞줄 3슬롯)
            var frontLine = CreateFormationLine(battlePreview, "FrontLineContainer", 3, new Vector2(0, 50));

            // BackLineContainer (뒷줄 3슬롯)
            var backLine = CreateFormationLine(battlePreview, "BackLineContainer", 3, new Vector2(0, -50));

            return (frontLine.transform, backLine.transform);
        }

        private static GameObject CreateFormationLine(GameObject parent, string name, int slotCount, Vector2 position)
        {
            var line = CreateChild(parent, name);
            var lineRect = line.AddComponent<RectTransform>();
            lineRect.anchorMin = new Vector2(0.5f, 0.5f);
            lineRect.anchorMax = new Vector2(0.5f, 0.5f);
            lineRect.sizeDelta = new Vector2(slotCount * (PARTY_SLOT_SIZE + PARTY_SLOT_SPACING), PARTY_SLOT_SIZE);
            lineRect.anchoredPosition = position;

            var layout = line.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = PARTY_SLOT_SPACING;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            // Create placeholder slots
            for (int i = 0; i < slotCount; i++)
            {
                CreatePartySlotPlaceholder(line, $"Slot{i}");
            }

            return line;
        }

        private static void CreatePartySlotPlaceholder(GameObject parent, string name)
        {
            var slot = CreateChild(parent, name);
            var slotRect = slot.AddComponent<RectTransform>();
            slotRect.sizeDelta = new Vector2(PARTY_SLOT_SIZE, PARTY_SLOT_SIZE);

            var slotImage = slot.AddComponent<Image>();
            slotImage.color = SlotEmpty;
        }

        private static (Transform partyFormation, Transform enemyFormation, Transform battlePreviewArea)
            CreateFormationAreas(GameObject parent)
        {
            var battlePreview = CreateChild(parent, "BattlePreviewVisual");
            var previewRect = battlePreview.AddComponent<RectTransform>();
            previewRect.anchorMin = new Vector2(0.5f, 0.6f);
            previewRect.anchorMax = new Vector2(0.5f, 0.6f);
            previewRect.sizeDelta = new Vector2(600, 300);

            // PartyFormation (좌측)
            var partyFormation = CreateChild(battlePreview, "PartyFormation");
            var partyRect = partyFormation.AddComponent<RectTransform>();
            partyRect.anchorMin = new Vector2(0, 0.5f);
            partyRect.anchorMax = new Vector2(0, 0.5f);
            partyRect.pivot = new Vector2(0, 0.5f);
            partyRect.sizeDelta = new Vector2(250, 200);
            partyRect.anchoredPosition = new Vector2(50, 0);

            // EnemyFormation (우측)
            var enemyFormation = CreateChild(battlePreview, "EnemyFormation");
            var enemyRect = enemyFormation.AddComponent<RectTransform>();
            enemyRect.anchorMin = new Vector2(1, 0.5f);
            enemyRect.anchorMax = new Vector2(1, 0.5f);
            enemyRect.pivot = new Vector2(1, 0.5f);
            enemyRect.sizeDelta = new Vector2(200, 200);
            enemyRect.anchoredPosition = new Vector2(-50, 0);

            var enemyImage = enemyFormation.AddComponent<Image>();
            enemyImage.color = new Color(AccentRed.r, AccentRed.g, AccentRed.b, 0.3f);

            return (partyFormation.transform, enemyFormation.transform, battlePreview.transform);
        }

        private static (Button autoForm, Button stageInfo, Button formationSetting) CreateQuickActionBar(
            GameObject parent)
        {
            var actionBar = CreateChild(parent, "QuickActionBar");
            var actionRect = actionBar.AddComponent<RectTransform>();
            actionRect.anchorMin = new Vector2(0, 0);
            actionRect.anchorMax = new Vector2(1, 0);
            actionRect.pivot = new Vector2(0.5f, 0);
            actionRect.sizeDelta = new Vector2(0, 60);
            actionRect.anchoredPosition = new Vector2(0, 20);

            var layout = actionBar.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(20, 20, 0, 0);
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var autoFormButton = CreateActionButton(actionBar, "AutoFormButton", "일괄 해제");
            var stageInfoButton = CreateActionButton(actionBar, "StageInfoButton", "스테이지 정보");
            var formationSettingButton = CreateActionButton(actionBar, "FormationSettingButton", "덱 설정");

            return (autoFormButton, stageInfoButton, formationSettingButton);
        }

        #endregion

        #region StageInfoPanel

        private static (TMP_Text stamina, TMP_Text power, TMP_Text charCount, TMP_Text partyPower)
            CreateStageInfoPanel(GameObject parent)
        {
            var panel = CreateChild(parent, "StageInfoPanel");
            var panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 1);
            panelRect.anchorMax = new Vector2(0.5f, 1);
            panelRect.pivot = new Vector2(0.5f, 1);
            panelRect.sizeDelta = new Vector2(STAGE_INFO_PANEL_WIDTH, STAGE_INFO_PANEL_HEIGHT);
            panelRect.anchoredPosition = new Vector2(0, -HEADER_HEIGHT - 20);

            var panelBg = panel.AddComponent<Image>();
            panelBg.color = BgPanel;

            // Layout
            var layout = panel.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(15, 15, 15, 15);
            layout.spacing = 5;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;

            var staminaCostText = CreateInfoText(panel, "StaminaCostText", "입장 비용: 30");
            var recommendedPowerText = CreateInfoText(panel, "RecommendedPowerText", "권장 전투력: 123,188");
            var characterCountText = CreateInfoText(panel, "CharacterCountText", "편성된 사도: 6/6");
            var partyPowerText = CreateInfoText(panel, "PartyPowerText", "카드: 24/24");

            return (staminaCostText, recommendedPowerText, characterCountText, partyPowerText);
        }

        private static TMP_Text CreateInfoText(GameObject parent, string name, string text)
        {
            var textObj = CreateChild(parent, name);
            var textRect = textObj.AddComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(0, 20);

            var tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 14;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Left;
            ApplyFont(tmp);

            return tmp;
        }

        #endregion

        #region RightPanel

        private static GameObject CreateRightPanel(GameObject parent)
        {
            var rightPanel = CreateChild(parent, "RightPanel");
            var rightRect = rightPanel.AddComponent<RectTransform>();
            rightRect.anchorMin = new Vector2(LEFT_AREA_WIDTH_RATIO, 0);
            rightRect.anchorMax = Vector2.one;
            rightRect.offsetMin = Vector2.zero;
            rightRect.offsetMax = Vector2.zero;

            var panelBg = rightPanel.AddComponent<Image>();
            panelBg.color = new Color(BgDark.r, BgDark.g, BgDark.b, 0.5f);

            return rightPanel;
        }

        private static void CreateTabBar(GameObject parent)
        {
            var tabBar = CreateChild(parent, "TabBar");
            var tabRect = tabBar.AddComponent<RectTransform>();
            tabRect.anchorMin = new Vector2(0, 1);
            tabRect.anchorMax = Vector2.one;
            tabRect.pivot = new Vector2(0.5f, 1);
            tabRect.sizeDelta = new Vector2(0, 50);
            tabRect.anchoredPosition = Vector2.zero;

            var layout = tabBar.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 5;
            layout.padding = new RectOffset(10, 10, 5, 5);
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            CreateTab(tabBar, "RentalTab", "대여");
            CreateTab(tabBar, "FilterTab", "필터OFF");
            CreateTab(tabBar, "PowerTab", "전투력");
        }

        private static void CreateTab(GameObject parent, string name, string label)
        {
            var tab = CreateChild(parent, name);

            var tabImage = tab.AddComponent<Image>();
            tabImage.color = new Color(1, 1, 1, 0.1f);

            var tabButton = tab.AddComponent<Button>();

            var tabText = CreateChild(tab, "Text");
            SetStretch(tabText);
            var tmp = tabText.AddComponent<TextMeshProUGUI>();
            tmp.text = label;
            tmp.fontSize = 14;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = TextMuted;
            ApplyFont(tmp);
        }

        private static Transform CreateCharacterGrid(GameObject parent)
        {
            var scrollContainer = CreateChild(parent, "CharacterScrollView");
            var scrollRect = scrollContainer.AddComponent<RectTransform>();
            scrollRect.anchorMin = new Vector2(0, 0);
            scrollRect.anchorMax = Vector2.one;
            scrollRect.offsetMin = new Vector2(0, ACTION_BAR_HEIGHT);
            scrollRect.offsetMax = new Vector2(0, -50);

            var scrollView = scrollContainer.AddComponent<ScrollRect>();
            scrollView.horizontal = false;
            scrollView.vertical = true;

            // Viewport
            var viewport = CreateChild(scrollContainer, "Viewport");
            SetStretch(viewport);
            viewport.AddComponent<RectMask2D>();

            scrollView.viewport = viewport.GetComponent<RectTransform>();

            // Content (CharacterListContainer)
            var characterListContainer = CreateChild(viewport, "CharacterListContainer");
            var contentRect = characterListContainer.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = Vector2.one;
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.sizeDelta = new Vector2(0, 600);

            scrollView.content = contentRect;

            // GridLayoutGroup (3열)
            var grid = characterListContainer.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(CHARACTER_SLOT_SIZE, CHARACTER_SLOT_SIZE + 30);
            grid.spacing = new Vector2(GRID_SPACING, GRID_SPACING);
            grid.padding = new RectOffset(10, 10, 10, 10);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 3;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;

            // ContentSizeFitter
            var fitter = characterListContainer.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            // Placeholder slots
            for (int i = 0; i < 9; i++)
            {
                CreateCharacterSlotPlaceholder(characterListContainer, $"CharacterSlot{i}");
            }

            return characterListContainer.transform;
        }

        private static void CreateCharacterSlotPlaceholder(GameObject parent, string name)
        {
            var slot = CreateChild(parent, name);
            var slotRect = slot.AddComponent<RectTransform>();
            slotRect.sizeDelta = new Vector2(CHARACTER_SLOT_SIZE, CHARACTER_SLOT_SIZE + 30);

            var slotImage = slot.AddComponent<Image>();
            slotImage.color = SlotEmpty;

            // Portrait area
            var portrait = CreateChild(slot, "Portrait");
            var portraitRect = portrait.AddComponent<RectTransform>();
            portraitRect.anchorMin = new Vector2(0.5f, 1);
            portraitRect.anchorMax = new Vector2(0.5f, 1);
            portraitRect.pivot = new Vector2(0.5f, 1);
            portraitRect.sizeDelta = new Vector2(CHARACTER_SLOT_SIZE - 10, CHARACTER_SLOT_SIZE - 10);
            portraitRect.anchoredPosition = new Vector2(0, -5);

            var portraitImage = portrait.AddComponent<Image>();
            portraitImage.color = new Color(0.3f, 0.3f, 0.4f, 0.5f);

            // Info bar
            var infoBar = CreateChild(slot, "InfoBar");
            var infoRect = infoBar.AddComponent<RectTransform>();
            infoRect.anchorMin = new Vector2(0, 0);
            infoRect.anchorMax = new Vector2(1, 0);
            infoRect.pivot = new Vector2(0.5f, 0);
            infoRect.sizeDelta = new Vector2(0, 25);

            var infoText = infoBar.AddComponent<TextMeshProUGUI>();
            infoText.text = "Lv.1";
            infoText.fontSize = 10;
            infoText.alignment = TextAlignmentOptions.Center;
            infoText.color = TextMuted;
            ApplyFont(infoText);
        }

        private static (Button start, Button quickBattle, Button autoToggle) CreateActionBar(GameObject parent)
        {
            var actionBar = CreateChild(parent, "ActionBar");
            var actionRect = actionBar.AddComponent<RectTransform>();
            actionRect.anchorMin = new Vector2(0, 0);
            actionRect.anchorMax = new Vector2(1, 0);
            actionRect.pivot = new Vector2(0.5f, 0);
            actionRect.sizeDelta = new Vector2(0, ACTION_BAR_HEIGHT);
            actionRect.anchoredPosition = Vector2.zero;

            var layout = actionBar.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(15, 15, 15, 15);
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var quickBattleButton = CreateActionButton(actionBar, "QuickBattleButton", "소탕", AccentBlue);
            var startButton = CreateActionButton(actionBar, "StartButton", "출발", AccentGreen);
            var autoToggleButton = CreateActionButton(actionBar, "AutoToggleButton", "자동", AccentOrange);

            return (startButton, quickBattleButton, autoToggleButton);
        }

        private static Button CreateActionButton(GameObject parent, string name, string label,
            Color? bgColor = null)
        {
            var btnObj = CreateChild(parent, name);

            var btnImage = btnObj.AddComponent<Image>();
            btnImage.color = bgColor ?? new Color(0.3f, 0.4f, 0.5f, 1f);

            var button = btnObj.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(1, 1, 1, 1.2f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            button.colors = colors;

            var textObj = CreateChild(btnObj, "Text");
            SetStretch(textObj);
            var tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = label;
            tmp.fontSize = 16;
            tmp.fontStyle = FontStyles.Bold;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;
            ApplyFont(tmp);

            return button;
        }

        #endregion

        #region Widgets

        private static GameObject CreateWidgetPlaceholder(GameObject parent, string name)
        {
            var widget = CreateChild(parent, name);
            var widgetRect = widget.AddComponent<RectTransform>();
            widgetRect.anchorMin = new Vector2(0.5f, 0.5f);
            widgetRect.anchorMax = new Vector2(0.5f, 0.5f);
            widgetRect.sizeDelta = new Vector2(200, 100);

            var image = widget.AddComponent<Image>();
            image.color = new Color(1, 0, 1, 0.1f); // Magenta tint for visibility

            return widget;
        }

        #endregion

        #region PartySlotContainer

        private static Transform CreatePartySlotContainer(GameObject parent)
        {
            var container = CreateChild(parent, "PartySlotContainer");
            var containerRect = container.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0.4f);
            containerRect.anchorMax = new Vector2(0.5f, 0.4f);
            containerRect.sizeDelta = new Vector2(600, 100);

            return container.transform;
        }

        #endregion

        #region SerializeField Connections

        private static void ConnectSerializedFields(
            GameObject root,
            Button backButton,
            Button homeButton,
            TMP_Text stageNameText,
            TMP_Text stageDifficultyText,
            Transform frontLineContainer,
            Transform backLineContainer,
            Transform battlePreviewArea,
            Transform partyFormation,
            Transform enemyFormation,
            Button autoFormButton,
            Button stageInfoButton,
            Button formationSettingButton,
            Transform characterListContainer,
            Transform partySlotContainer,
            TMP_Text staminaCostText,
            TMP_Text recommendedPowerText,
            TMP_Text characterCountText,
            TMP_Text partyPowerText,
            Button startButton,
            Button quickBattleButton,
            Button autoToggleButton,
            GameObject stageInfoWidget,
            GameObject characterSelectWidget)
        {
            var screen = root.GetComponent<PartySelectScreen>();
            var so = new SerializedObject(screen);

            // Navigation
            so.FindProperty("_backButton").objectReferenceValue = backButton;
            so.FindProperty("_homeButton").objectReferenceValue = homeButton;

            // Stage Info
            so.FindProperty("_stageNameText").objectReferenceValue = stageNameText;
            so.FindProperty("_stageDifficultyText").objectReferenceValue = stageDifficultyText;
            so.FindProperty("_recommendedPowerText").objectReferenceValue = recommendedPowerText;

            // Formation
            so.FindProperty("_frontLineContainer").objectReferenceValue = frontLineContainer;
            so.FindProperty("_backLineContainer").objectReferenceValue = backLineContainer;
            so.FindProperty("_battlePreviewArea").objectReferenceValue = battlePreviewArea;
            so.FindProperty("_partyFormation").objectReferenceValue = partyFormation;
            so.FindProperty("_enemyFormation").objectReferenceValue = enemyFormation;

            // Quick Actions
            so.FindProperty("_autoFormButton").objectReferenceValue = autoFormButton;
            so.FindProperty("_stageInfoButton").objectReferenceValue = stageInfoButton;
            so.FindProperty("_formationSettingButton").objectReferenceValue = formationSettingButton;

            // Character List
            so.FindProperty("_characterListContainer").objectReferenceValue = characterListContainer;
            so.FindProperty("_partySlotContainer").objectReferenceValue = partySlotContainer;
            so.FindProperty("_characterCountText").objectReferenceValue = characterCountText;
            so.FindProperty("_partyPowerText").objectReferenceValue = partyPowerText;

            // Footer
            so.FindProperty("_staminaCostText").objectReferenceValue = staminaCostText;
            so.FindProperty("_startButton").objectReferenceValue = startButton;
            so.FindProperty("_quickBattleButton").objectReferenceValue = quickBattleButton;
            so.FindProperty("_autoToggleButton").objectReferenceValue = autoToggleButton;

            // Widgets
            so.FindProperty("_stageInfoWidget").objectReferenceValue = stageInfoWidget;
            so.FindProperty("_characterSelectWidget").objectReferenceValue = characterSelectWidget;

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

        private static TMP_Text CreateText(GameObject parent, string name, string text, float fontSize,
            TextAlignmentOptions alignment, Vector2 position)
        {
            var textObj = CreateChild(parent, name);
            var textRect = textObj.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = new Vector2(400, 30);
            textRect.anchoredPosition = position;

            var tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;
            tmp.alignment = alignment;
            tmp.color = TextPrimary;
            ApplyFont(tmp);

            return tmp;
        }

        #endregion
    }
}
