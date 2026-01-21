using Sc.Common.UI.Widgets;
using Sc.Contents.Lobby;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// LobbyScreen 전용 프리팹 빌더.
    /// 서브컬쳐 수집형 RPG 스타일 로비 UI 생성.
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
        private static readonly Color GlassBorder = new Color(1f, 1f, 1f, 0.1f);

        #endregion

        #region Constants

        private const float HEADER_HEIGHT = 80f;
        private const float BOTTOM_NAV_HEIGHT = 80f;
        private const float TAB_BUTTON_WIDTH = 120f;
        private const float TAB_BUTTON_HEIGHT = 60f;
        private const float QUICK_BUTTON_SIZE = 100f;

        #endregion

        /// <summary>
        /// LobbyScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot();

            // 1. Background
            CreateBackground(root);

            // 2. Main Content Area (Header 영역 제외)
            var mainContent = CreateMainContent(root);

            // 3. Character Display (Left)
            CreateCharacterDisplay(mainContent);

            // 4. Tab Content Area (Right)
            var tabContentArea = CreateTabContentArea(mainContent);

            // 5. Tab Contents
            var homeTab = CreateHomeTabContent(tabContentArea);
            var characterTab = CreateCharacterTabContent(tabContentArea);
            var gachaTab = CreateGachaTabContent(tabContentArea);
            var settingsTab = CreateSettingsTabContent(tabContentArea);

            // 6. Bottom Navigation
            var bottomNav = CreateBottomNav(root);

            // 7. Connect SerializedFields
            ConnectSerializedFields(root, bottomNav, new[]
            {
                homeTab, characterTab, gachaTab, settingsTab
            });

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

        #endregion

        #region Main Content

        private static GameObject CreateMainContent(GameObject parent)
        {
            var content = CreateChild(parent, "MainContent");
            var rect = SetStretch(content);

            // ScreenHeader 영역 확보 (상단 80px)
            rect.offsetMax = new Vector2(0, -HEADER_HEIGHT);
            // BottomNav 영역 확보 (하단 80px)
            rect.offsetMin = new Vector2(0, BOTTOM_NAV_HEIGHT);

            return content;
        }

        private static void CreateCharacterDisplay(GameObject parent)
        {
            var display = CreateChild(parent, "CharacterDisplay");
            var rect = display.AddComponent<RectTransform>();

            // Left 55%
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0.55f, 1);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

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

            // Character Image Placeholder
            var charImage = CreateChild(display, "CharacterImage");
            var charRect = charImage.AddComponent<RectTransform>();
            charRect.anchorMin = new Vector2(0.5f, 0);
            charRect.anchorMax = new Vector2(0.5f, 0);
            charRect.pivot = new Vector2(0.5f, 0);
            charRect.sizeDelta = new Vector2(350, 500);
            charRect.anchoredPosition = new Vector2(0, 20);

            var charImg = charImage.AddComponent<Image>();
            charImg.color = new Color(1, 1, 1, 0.1f);
            charImg.raycastTarget = false;

            // Placeholder Text
            var placeholderText = CreateChild(charImage, "PlaceholderText");
            var textRect = SetStretch(placeholderText);
            var tmp = placeholderText.AddComponent<TextMeshProUGUI>();
            tmp.text = "CHARACTER";
            tmp.fontSize = 24;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.Center;
        }

        private static GameObject CreateTabContentArea(GameObject parent)
        {
            var area = CreateChild(parent, "TabContentArea");
            var rect = area.AddComponent<RectTransform>();

            // Right 45%
            rect.anchorMin = new Vector2(0.55f, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(10, 10);
            rect.offsetMax = new Vector2(-20, -10);

            return area;
        }

        #endregion

        #region Tab Contents

        private static GameObject CreateHomeTabContent(GameObject parent)
        {
            var tab = CreateChild(parent, "HomeTabContent");
            SetStretch(tab);
            tab.AddComponent<HomeTabContent>();

            var layout = tab.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 16;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Section Title: Quick Menu
            CreateSectionTitle(tab, "QUICK MENU");

            // Quick Menu Grid
            var quickMenu = CreateChild(tab, "QuickMenu");
            var quickRect = quickMenu.AddComponent<RectTransform>();
            quickRect.sizeDelta = new Vector2(0, QUICK_BUTTON_SIZE + 20);

            var quickGrid = quickMenu.AddComponent<HorizontalLayoutGroup>();
            quickGrid.spacing = 12;
            quickGrid.childControlWidth = true;
            quickGrid.childControlHeight = true;
            quickGrid.childForceExpandWidth = true;
            quickGrid.childForceExpandHeight = true;

            var quickFitter = quickMenu.AddComponent<LayoutElement>();
            quickFitter.preferredHeight = QUICK_BUTTON_SIZE + 20;

            // Quick Buttons
            var stageBtn = CreateQuickButton(quickMenu, "StageButton", "Stage", AccentPrimary);
            var shopBtn = CreateQuickButton(quickMenu, "ShopButton", "Shop", AccentGold);
            var eventBtn = CreateQuickButton(quickMenu, "EventButton", "Event", AccentSecondary);

            // Connect buttons to HomeTabContent
            var homeContent = tab.GetComponent<HomeTabContent>();
            ConnectHomeTabButtons(homeContent, stageBtn, shopBtn, eventBtn);

            // Section Title: Banner
            CreateSectionTitle(tab, "NOTICE");

            // Banner Area
            CreateBannerArea(tab);

            return tab;
        }

        private static GameObject CreateCharacterTabContent(GameObject parent)
        {
            var tab = CreateChild(parent, "CharacterTabContent");
            SetStretch(tab);
            tab.SetActive(false);
            tab.AddComponent<CharacterTabContent>();

            var layout = tab.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 16;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Section Title
            CreateSectionTitle(tab, "MY CHARACTERS");

            // Character Grid Container
            var gridContainer = CreateChild(tab, "CharacterListContainer");
            var gridRect = gridContainer.AddComponent<RectTransform>();
            gridRect.sizeDelta = new Vector2(0, 200);

            var gridLayout = gridContainer.AddComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(80, 100);
            gridLayout.spacing = new Vector2(8, 8);
            gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
            gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
            gridLayout.childAlignment = TextAnchor.UpperLeft;
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = 4;

            var gridFitter = gridContainer.AddComponent<LayoutElement>();
            gridFitter.preferredHeight = 200;
            gridFitter.flexibleHeight = 1;

            // Navigate Button
            var navigateBtn = CreateNavigateButton(tab, "CharacterList");

            // Connect to CharacterTabContent
            var charContent = tab.GetComponent<CharacterTabContent>();
            ConnectCharacterTabFields(charContent, gridContainer.transform, navigateBtn);

            return tab;
        }

        private static GameObject CreateGachaTabContent(GameObject parent)
        {
            var tab = CreateChild(parent, "GachaTabContent");
            SetStretch(tab);
            tab.SetActive(false);
            tab.AddComponent<GachaTabContent>();

            var layout = tab.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 16;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Gacha Banner
            CreateGachaBanner(tab);

            // Navigate Button
            var navigateBtn = CreateNavigateButton(tab, "Gacha");
            var btnImage = navigateBtn.GetComponent<Image>();
            btnImage.color = AccentPurple;

            // Connect to GachaTabContent
            var gachaContent = tab.GetComponent<GachaTabContent>();
            ConnectGachaTabFields(gachaContent, null, navigateBtn);

            return tab;
        }

        private static GameObject CreateSettingsTabContent(GameObject parent)
        {
            var tab = CreateChild(parent, "SettingsTabContent");
            SetStretch(tab);
            tab.SetActive(false);
            tab.AddComponent<SettingsTabContent>();

            var layout = tab.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Section Title
            CreateSectionTitle(tab, "SETTINGS");

            // Settings Items
            CreateSettingsItem(tab, "Sound Settings");
            CreateSettingsItem(tab, "Game Settings");
            CreateSettingsItem(tab, "Account");
            CreateSettingsItem(tab, "Terms of Service");
            CreateSettingsItem(tab, "Version Info");

            return tab;
        }

        #endregion

        #region Bottom Navigation

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
            bgImage.color = new Color(0, 0, 0, 0.8f);

            // TabGroupWidget
            var tabGroup = nav.AddComponent<TabGroupWidget>();

            // Button Container
            var buttonContainer = CreateChild(nav, "TabButtonContainer");
            var containerRect = buttonContainer.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0.5f);
            containerRect.anchorMax = new Vector2(0.5f, 0.5f);
            containerRect.sizeDelta = new Vector2(500, TAB_BUTTON_HEIGHT);

            var containerLayout = buttonContainer.AddComponent<HorizontalLayoutGroup>();
            containerLayout.spacing = 8;
            containerLayout.childControlWidth = true;
            containerLayout.childControlHeight = true;
            containerLayout.childForceExpandWidth = true;
            containerLayout.childForceExpandHeight = true;

            // Create Tab Buttons
            var homeBtn = CreateTabButton(buttonContainer, "HomeTab", "HOME", 0);
            var charBtn = CreateTabButton(buttonContainer, "CharacterTab", "CHARACTER", 1);
            var gachaBtn = CreateTabButton(buttonContainer, "GachaTab", "GACHA", 2);
            var settingsBtn = CreateTabButton(buttonContainer, "SettingsTab", "SETTINGS", 3);

            // Tab Button Prefab (hidden template)
            var prefabTemplate = CreateTabButtonPrefab(nav);

            // Connect TabGroupWidget fields
            ConnectTabGroupFields(tabGroup, buttonContainer.transform, prefabTemplate);

            return nav;
        }

        private static GameObject CreateTabButton(GameObject parent, string name, string label, int index)
        {
            var btn = CreateChild(parent, name);
            var rect = btn.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(TAB_BUTTON_WIDTH, TAB_BUTTON_HEIGHT);

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button Component
            var button = btn.AddComponent<Button>();
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1, 1, 1, 0.9f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            button.colors = colors;

            // TabButton Component
            var tabButton = btn.AddComponent<TabButton>();

            // Layout
            var layout = btn.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4;
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Icon Placeholder
            var icon = CreateChild(btn, "Icon");
            var iconRect = icon.AddComponent<RectTransform>();
            iconRect.sizeDelta = new Vector2(24, 24);

            var iconFitter = icon.AddComponent<LayoutElement>();
            iconFitter.preferredWidth = 24;
            iconFitter.preferredHeight = 24;

            var iconImage = icon.AddComponent<Image>();
            iconImage.color = TextMuted;

            // Label
            var labelObj = CreateChild(btn, "Label");
            var labelRect = labelObj.AddComponent<RectTransform>();
            labelRect.sizeDelta = new Vector2(0, 16);

            var labelFitter = labelObj.AddComponent<LayoutElement>();
            labelFitter.preferredHeight = 16;

            var labelText = labelObj.AddComponent<TextMeshProUGUI>();
            labelText.text = label;
            labelText.fontSize = 11;
            labelText.color = TextMuted;
            labelText.alignment = TextAlignmentOptions.Center;

            // Selected Indicator
            var indicator = CreateChild(btn, "SelectedIndicator");
            var indRect = indicator.AddComponent<RectTransform>();
            indRect.anchorMin = new Vector2(0.2f, 1);
            indRect.anchorMax = new Vector2(0.8f, 1);
            indRect.pivot = new Vector2(0.5f, 1);
            indRect.sizeDelta = new Vector2(0, 3);
            indRect.anchoredPosition = Vector2.zero;

            var indImage = indicator.AddComponent<Image>();
            indImage.color = AccentPrimary;
            indicator.SetActive(false);

            // Badge
            var badge = CreateChild(btn, "Badge");
            var badgeRect = badge.AddComponent<RectTransform>();
            badgeRect.anchorMin = new Vector2(1, 1);
            badgeRect.anchorMax = new Vector2(1, 1);
            badgeRect.pivot = new Vector2(1, 1);
            badgeRect.sizeDelta = new Vector2(20, 20);
            badgeRect.anchoredPosition = new Vector2(-8, -8);

            var badgeBg = badge.AddComponent<Image>();
            badgeBg.color = AccentSecondary;

            var badgeText = CreateChild(badge, "Count");
            var badgeTextRect = SetStretch(badgeText);
            var badgeTmp = badgeText.AddComponent<TextMeshProUGUI>();
            badgeTmp.text = "";
            badgeTmp.fontSize = 10;
            badgeTmp.color = Color.white;
            badgeTmp.alignment = TextAlignmentOptions.Center;

            badge.SetActive(false);

            // Connect TabButton fields
            ConnectTabButtonFields(tabButton, button, labelText, bgImage, indicator, badge, badgeTmp);

            return btn;
        }

        private static GameObject CreateTabButtonPrefab(GameObject parent)
        {
            var prefab = CreateTabButton(parent, "TabButtonPrefab", "TAB", -1);
            prefab.SetActive(false);
            return prefab;
        }

        #endregion

        #region UI Components

        private static void CreateSectionTitle(GameObject parent, string title)
        {
            var titleObj = CreateChild(parent, $"Title_{title}");
            var rect = titleObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 24);

            var fitter = titleObj.AddComponent<LayoutElement>();
            fitter.preferredHeight = 24;

            var layout = titleObj.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;

            // Accent Bar
            var bar = CreateChild(titleObj, "AccentBar");
            var barRect = bar.AddComponent<RectTransform>();
            barRect.sizeDelta = new Vector2(4, 16);

            var barFitter = bar.AddComponent<LayoutElement>();
            barFitter.preferredWidth = 4;
            barFitter.preferredHeight = 16;

            var barImage = bar.AddComponent<Image>();
            barImage.color = AccentPrimary;

            // Text
            var text = CreateChild(titleObj, "Text");
            var textRect = text.AddComponent<RectTransform>();

            var textTmp = text.AddComponent<TextMeshProUGUI>();
            textTmp.text = title;
            textTmp.fontSize = 14;
            textTmp.fontStyle = FontStyles.Bold;
            textTmp.color = TextSecondary;
        }

        private static GameObject CreateQuickButton(GameObject parent, string name, string label, Color accentColor)
        {
            var btn = CreateChild(parent, name);
            var rect = btn.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(QUICK_BUTTON_SIZE, QUICK_BUTTON_SIZE);

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button
            var button = btn.AddComponent<Button>();
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1, 1, 1, 0.9f);
            button.colors = colors;

            // Layout
            var layout = btn.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8;
            layout.padding = new RectOffset(10, 10, 15, 10);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // Icon Placeholder
            var icon = CreateChild(btn, "Icon");
            var iconRect = icon.AddComponent<RectTransform>();
            iconRect.sizeDelta = new Vector2(32, 32);

            var iconFitter = icon.AddComponent<LayoutElement>();
            iconFitter.preferredWidth = 32;
            iconFitter.preferredHeight = 32;

            var iconImage = icon.AddComponent<Image>();
            iconImage.color = accentColor;

            // Label
            var labelObj = CreateChild(btn, "Label");
            var labelRect = labelObj.AddComponent<RectTransform>();
            labelRect.sizeDelta = new Vector2(0, 18);

            var labelFitter = labelObj.AddComponent<LayoutElement>();
            labelFitter.preferredHeight = 18;

            var labelText = labelObj.AddComponent<TextMeshProUGUI>();
            labelText.text = label;
            labelText.fontSize = 13;
            labelText.color = TextSecondary;
            labelText.alignment = TextAlignmentOptions.Center;

            return btn;
        }

        private static void CreateBannerArea(GameObject parent)
        {
            var banner = CreateChild(parent, "BannerArea");
            var rect = banner.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 100);

            var fitter = banner.AddComponent<LayoutElement>();
            fitter.preferredHeight = 100;
            fitter.flexibleHeight = 0;

            // Background
            var bgImage = banner.AddComponent<Image>();
            bgImage.color = BgCard;

            // Banner Text
            var textObj = CreateChild(banner, "BannerText");
            SetStretch(textObj);

            var layout = textObj.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            var title = CreateChild(textObj, "Title");
            var titleRect = title.AddComponent<RectTransform>();
            titleRect.sizeDelta = new Vector2(0, 30);

            var titleFitter = title.AddComponent<LayoutElement>();
            titleFitter.preferredHeight = 30;

            var titleTmp = title.AddComponent<TextMeshProUGUI>();
            titleTmp.text = "NEW CHARACTER PICKUP";
            titleTmp.fontSize = 18;
            titleTmp.fontStyle = FontStyles.Bold;
            titleTmp.color = AccentGold;
            titleTmp.alignment = TextAlignmentOptions.Center;

            var subtitle = CreateChild(textObj, "Subtitle");
            var subRect = subtitle.AddComponent<RectTransform>();
            subRect.sizeDelta = new Vector2(0, 20);

            var subFitter = subtitle.AddComponent<LayoutElement>();
            subFitter.preferredHeight = 20;

            var subTmp = subtitle.AddComponent<TextMeshProUGUI>();
            subTmp.text = "2x Rate UP!";
            subTmp.fontSize = 12;
            subTmp.color = TextSecondary;
            subTmp.alignment = TextAlignmentOptions.Center;
        }

        private static void CreateGachaBanner(GameObject parent)
        {
            var banner = CreateChild(parent, "GachaBanner");
            var rect = banner.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 150);

            var fitter = banner.AddComponent<LayoutElement>();
            fitter.preferredHeight = 150;

            // Background with gradient-like effect
            var bgImage = banner.AddComponent<Image>();
            bgImage.color = new Color(0.3f, 0.15f, 0.4f, 0.8f);

            // Content Layout
            var layout = banner.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8;
            layout.padding = new RectOffset(20, 20, 15, 15);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;

            // Title
            var title = CreateChild(banner, "Title");
            var titleRect = title.AddComponent<RectTransform>();

            var titleFitter = title.AddComponent<LayoutElement>();
            titleFitter.preferredHeight = 30;

            var titleTmp = title.AddComponent<TextMeshProUGUI>();
            titleTmp.text = "PREMIUM SUMMON";
            titleTmp.fontSize = 20;
            titleTmp.fontStyle = FontStyles.Bold;
            titleTmp.color = AccentGold;
            titleTmp.alignment = TextAlignmentOptions.Center;

            // Featured
            var featured = CreateChild(banner, "Featured");
            var featuredFitter = featured.AddComponent<LayoutElement>();
            featuredFitter.preferredHeight = 20;

            var featuredTmp = featured.AddComponent<TextMeshProUGUI>();
            featuredTmp.text = "Featured: SSR Aria";
            featuredTmp.fontSize = 14;
            featuredTmp.color = TextSecondary;
            featuredTmp.alignment = TextAlignmentOptions.Center;

            // Rates
            var rates = CreateChild(banner, "Rates");
            var ratesRect = rates.AddComponent<RectTransform>();
            rates.AddComponent<HorizontalLayoutGroup>().spacing = 30;
            rates.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;

            var ratesFitter = rates.AddComponent<LayoutElement>();
            ratesFitter.preferredHeight = 40;

            CreateRateItem(rates, "SSR", "3%", AccentGold);
            CreateRateItem(rates, "SR", "15%", AccentPurple);
            CreateRateItem(rates, "R", "82%", AccentPrimary);
        }

        private static void CreateRateItem(GameObject parent, string label, string value, Color color)
        {
            var item = CreateChild(parent, $"Rate_{label}");
            var layout = item.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;

            var valueObj = CreateChild(item, "Value");
            var valueTmp = valueObj.AddComponent<TextMeshProUGUI>();
            valueTmp.text = value;
            valueTmp.fontSize = 18;
            valueTmp.fontStyle = FontStyles.Bold;
            valueTmp.color = color;
            valueTmp.alignment = TextAlignmentOptions.Center;

            var labelObj = CreateChild(item, "Label");
            var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 10;
            labelTmp.color = TextMuted;
            labelTmp.alignment = TextAlignmentOptions.Center;
        }

        private static GameObject CreateNavigateButton(GameObject parent, string targetName)
        {
            var btn = CreateChild(parent, "NavigateButton");
            var rect = btn.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 50);

            var fitter = btn.AddComponent<LayoutElement>();
            fitter.preferredHeight = 50;

            // Background
            var bgImage = btn.AddComponent<Image>();
            bgImage.color = AccentPrimary;

            // Button
            var button = btn.AddComponent<Button>();

            // Label
            var label = CreateChild(btn, "Label");
            SetStretch(label);

            var labelTmp = label.AddComponent<TextMeshProUGUI>();
            labelTmp.text = $"Go to {targetName} →";
            labelTmp.fontSize = 14;
            labelTmp.fontStyle = FontStyles.Bold;
            labelTmp.color = BgDeep;
            labelTmp.alignment = TextAlignmentOptions.Center;

            return btn;
        }

        private static void CreateSettingsItem(GameObject parent, string label)
        {
            var item = CreateChild(parent, $"Settings_{label.Replace(" ", "")}");
            var rect = item.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 50);

            var fitter = item.AddComponent<LayoutElement>();
            fitter.preferredHeight = 50;

            // Background
            var bgImage = item.AddComponent<Image>();
            bgImage.color = BgCard;

            // Button
            item.AddComponent<Button>();

            // Layout
            var layout = item.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(16, 16, 0, 0);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;

            // Label
            var labelObj = CreateChild(item, "Label");
            var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 14;
            labelTmp.color = TextPrimary;

            // Arrow (right side)
            var arrow = CreateChild(item, "Arrow");
            var arrowRect = arrow.AddComponent<RectTransform>();

            var arrowFitter = arrow.AddComponent<LayoutElement>();
            arrowFitter.preferredWidth = 20;
            arrowFitter.flexibleWidth = 0;

            var arrowTmp = arrow.AddComponent<TextMeshProUGUI>();
            arrowTmp.text = "→";
            arrowTmp.fontSize = 14;
            arrowTmp.color = TextMuted;
            arrowTmp.alignment = TextAlignmentOptions.Right;

            // Spacer between label and arrow
            var spacer = CreateChild(item, "Spacer");
            var spacerFitter = spacer.AddComponent<LayoutElement>();
            spacerFitter.flexibleWidth = 1;

            // Reorder: Label, Spacer, Arrow
            spacer.transform.SetSiblingIndex(1);
        }

        #endregion

        #region SerializeField Connections

        private static void ConnectSerializedFields(GameObject root, GameObject bottomNav,
            GameObject[] tabContents)
        {
            var lobbyScreen = root.GetComponent<LobbyScreen>();
            var tabGroup = bottomNav.GetComponent<TabGroupWidget>();

            var so = new SerializedObject(lobbyScreen);

            // TabGroup
            so.FindProperty("_tabGroup").objectReferenceValue = tabGroup;

            // TabContents array
            var tabContentsProperty = so.FindProperty("_tabContents");
            tabContentsProperty.arraySize = tabContents.Length;
            for (int i = 0; i < tabContents.Length; i++)
            {
                var content = tabContents[i].GetComponent<LobbyTabContent>();
                tabContentsProperty.GetArrayElementAtIndex(i).objectReferenceValue = content;
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectTabGroupFields(TabGroupWidget tabGroup, Transform buttonContainer,
            GameObject prefab)
        {
            var so = new SerializedObject(tabGroup);
            so.FindProperty("_tabButtonContainer").objectReferenceValue = buttonContainer;
            so.FindProperty("_tabButtonPrefab").objectReferenceValue = prefab.GetComponent<TabButton>();
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectTabButtonFields(TabButton tabButton, Button button, TMP_Text label,
            Image background, GameObject indicator, GameObject badge, TMP_Text badgeCount)
        {
            var so = new SerializedObject(tabButton);
            so.FindProperty("_button").objectReferenceValue = button;
            so.FindProperty("_labelText").objectReferenceValue = label;
            so.FindProperty("_backgroundImage").objectReferenceValue = background;
            so.FindProperty("_selectedIndicator").objectReferenceValue = indicator;
            so.FindProperty("_badge").objectReferenceValue = badge;
            so.FindProperty("_badgeCountText").objectReferenceValue = badgeCount;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectHomeTabButtons(HomeTabContent homeContent, GameObject stageBtn,
            GameObject shopBtn, GameObject eventBtn)
        {
            var so = new SerializedObject(homeContent);
            so.FindProperty("_stageButton").objectReferenceValue = stageBtn.GetComponent<Button>();
            so.FindProperty("_shopButton").objectReferenceValue = shopBtn.GetComponent<Button>();
            so.FindProperty("_eventButton").objectReferenceValue = eventBtn.GetComponent<Button>();
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectCharacterTabFields(CharacterTabContent content, Transform container,
            GameObject navigateBtn)
        {
            var so = new SerializedObject(content);
            so.FindProperty("_characterListContainer").objectReferenceValue = container;
            so.FindProperty("_navigateButton").objectReferenceValue = navigateBtn.GetComponent<Button>();
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectGachaTabFields(GachaTabContent content, Transform container,
            GameObject navigateBtn)
        {
            var so = new SerializedObject(content);
            if (container != null)
                so.FindProperty("_gachaPoolContainer").objectReferenceValue = container;
            so.FindProperty("_navigateButton").objectReferenceValue = navigateBtn.GetComponent<Button>();
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
