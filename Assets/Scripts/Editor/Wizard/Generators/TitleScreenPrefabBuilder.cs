using Sc.Contents.Title;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// TitleScreen 전용 프리팹 빌더.
    /// Luminous Dark Fantasy 테마 - 드라마틱한 타이틀 화면 UI 생성.
    /// </summary>
    public static class TitleScreenPrefabBuilder
    {
        #region Colors (Luminous Dark Fantasy Theme)

        // Deep cosmic background
        private static readonly Color BgDeep = new Color32(5, 5, 12, 255);
        private static readonly Color BgVoid = new Color32(10, 8, 20, 255);

        // Accent colors - ethereal glow
        private static readonly Color AccentCyan = new Color32(0, 212, 255, 255);
        private static readonly Color AccentCyanGlow = new Color32(0, 180, 220, 80);
        private static readonly Color AccentGold = new Color32(255, 200, 100, 255);
        private static readonly Color AccentMagenta = new Color32(255, 80, 160, 255);

        // Text colors
        private static readonly Color TextPrimary = Color.white;
        private static readonly Color TextGlow = new Color(1f, 1f, 1f, 0.9f);
        private static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.4f);
        private static readonly Color TextDim = new Color(1f, 1f, 1f, 0.2f);

        // Glass effects
        private static readonly Color GlassOverlay = new Color(1f, 1f, 1f, 0.03f);
        private static readonly Color GlassBorder = new Color(1f, 1f, 1f, 0.08f);

        #endregion

        #region Constants

        private const float LOGO_TOP_OFFSET = 180f;
        private const float LOGO_WIDTH = 500f;
        private const float LOGO_HEIGHT = 120f;
        private const float TOUCH_TEXT_BOTTOM_OFFSET = 200f;
        private const float RESET_BUTTON_SIZE = 40f;
        private const float RESET_BUTTON_MARGIN = 20f;

        #endregion

        /// <summary>
        /// TitleScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot();

            // 1. Deep Background with atmosphere
            CreateBackground(root);

            // 2. Ambient Effects Layer
            CreateAmbientEffects(root);

            // 3. Logo Area
            CreateLogoArea(root);

            // 4. Touch Area (full screen invisible button)
            var touchArea = CreateTouchArea(root);

            // 5. Touch to Start Text with pulsing effect
            var touchText = CreateTouchToStartText(root);

            // 6. Reset Account Button (corner)
            var resetButton = CreateResetAccountButton(root);

            // 7. Version Info
            CreateVersionInfo(root);

            // 8. Connect SerializedFields
            ConnectSerializedFields(root, touchArea, touchText, resetButton);

            return root;
        }

        #region Root

        private static GameObject CreateRoot()
        {
            var root = new GameObject("TitleScreen");

            var rect = root.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            root.AddComponent<CanvasGroup>();
            root.AddComponent<TitleScreen>();

            return root;
        }

        #endregion

        #region Background & Atmosphere

        private static void CreateBackground(GameObject parent)
        {
            // Base deep background
            var bg = CreateChild(parent, "Background");
            SetStretch(bg);

            var bgImage = bg.AddComponent<Image>();
            bgImage.color = BgDeep;
            bgImage.raycastTarget = false;

            // Gradient overlay (top-to-bottom void effect)
            var gradientOverlay = CreateChild(bg, "GradientOverlay");
            SetStretch(gradientOverlay);

            var gradientImage = gradientOverlay.AddComponent<Image>();
            gradientImage.color = BgVoid;
            gradientImage.raycastTarget = false;

            // Vignette effect
            var vignette = CreateChild(bg, "Vignette");
            SetStretch(vignette);

            var vignetteImage = vignette.AddComponent<Image>();
            vignetteImage.color = new Color(0, 0, 0, 0.6f);
            vignetteImage.raycastTarget = false;
        }

        private static void CreateAmbientEffects(GameObject parent)
        {
            var effects = CreateChild(parent, "AmbientEffects");
            SetStretch(effects);

            // Top light source glow
            var topGlow = CreateChild(effects, "TopGlow");
            var topRect = topGlow.AddComponent<RectTransform>();
            topRect.anchorMin = new Vector2(0.5f, 1f);
            topRect.anchorMax = new Vector2(0.5f, 1f);
            topRect.pivot = new Vector2(0.5f, 1f);
            topRect.sizeDelta = new Vector2(800, 400);
            topRect.anchoredPosition = Vector2.zero;

            var topGlowImage = topGlow.AddComponent<Image>();
            topGlowImage.color = new Color(0, 0.5f, 0.7f, 0.15f);
            topGlowImage.raycastTarget = false;

            // Center ethereal glow
            var centerGlow = CreateChild(effects, "CenterGlow");
            var centerRect = centerGlow.AddComponent<RectTransform>();
            centerRect.anchorMin = new Vector2(0.5f, 0.6f);
            centerRect.anchorMax = new Vector2(0.5f, 0.6f);
            centerRect.sizeDelta = new Vector2(600, 300);

            var centerGlowImage = centerGlow.AddComponent<Image>();
            centerGlowImage.color = new Color(0.8f, 0.3f, 0.5f, 0.08f);
            centerGlowImage.raycastTarget = false;

            // Bottom glow (for touch text)
            var bottomGlow = CreateChild(effects, "BottomGlow");
            var bottomRect = bottomGlow.AddComponent<RectTransform>();
            bottomRect.anchorMin = new Vector2(0.5f, 0f);
            bottomRect.anchorMax = new Vector2(0.5f, 0f);
            bottomRect.pivot = new Vector2(0.5f, 0f);
            bottomRect.sizeDelta = new Vector2(500, 200);
            bottomRect.anchoredPosition = new Vector2(0, 100);

            var bottomGlowImage = bottomGlow.AddComponent<Image>();
            bottomGlowImage.color = AccentCyanGlow;
            bottomGlowImage.raycastTarget = false;

            // Particle hint spots (decorative)
            CreateParticleSpot(effects, new Vector2(0.2f, 0.7f), AccentCyan, 8f);
            CreateParticleSpot(effects, new Vector2(0.8f, 0.8f), AccentMagenta, 6f);
            CreateParticleSpot(effects, new Vector2(0.15f, 0.3f), AccentGold, 5f);
            CreateParticleSpot(effects, new Vector2(0.85f, 0.4f), AccentCyan, 7f);
            CreateParticleSpot(effects, new Vector2(0.3f, 0.5f), AccentMagenta, 4f);
            CreateParticleSpot(effects, new Vector2(0.7f, 0.6f), AccentGold, 5f);
        }

        private static void CreateParticleSpot(GameObject parent, Vector2 anchorPos, Color color, float size)
        {
            var spot = CreateChild(parent, "ParticleSpot");
            var rect = spot.AddComponent<RectTransform>();
            rect.anchorMin = anchorPos;
            rect.anchorMax = anchorPos;
            rect.sizeDelta = new Vector2(size, size);

            var image = spot.AddComponent<Image>();
            image.color = new Color(color.r, color.g, color.b, 0.6f);
            image.raycastTarget = false;
        }

        #endregion

        #region Logo Area

        private static void CreateLogoArea(GameObject parent)
        {
            var logoArea = CreateChild(parent, "LogoArea");
            var rect = logoArea.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(LOGO_WIDTH, LOGO_HEIGHT + 80);
            rect.anchoredPosition = new Vector2(0, -LOGO_TOP_OFFSET);

            // Logo glow effect behind
            var logoGlow = CreateChild(logoArea, "LogoGlow");
            var glowRect = logoGlow.AddComponent<RectTransform>();
            glowRect.anchorMin = new Vector2(0.5f, 0.5f);
            glowRect.anchorMax = new Vector2(0.5f, 0.5f);
            glowRect.sizeDelta = new Vector2(LOGO_WIDTH + 100, LOGO_HEIGHT + 60);

            var glowImage = logoGlow.AddComponent<Image>();
            glowImage.color = new Color(0, 0.7f, 1f, 0.1f);
            glowImage.raycastTarget = false;

            // Main logo placeholder
            var logoImage = CreateChild(logoArea, "LogoImage");
            var logoRect = logoImage.AddComponent<RectTransform>();
            logoRect.anchorMin = new Vector2(0.5f, 1f);
            logoRect.anchorMax = new Vector2(0.5f, 1f);
            logoRect.pivot = new Vector2(0.5f, 1f);
            logoRect.sizeDelta = new Vector2(LOGO_WIDTH, LOGO_HEIGHT);
            logoRect.anchoredPosition = Vector2.zero;

            var logoImg = logoImage.AddComponent<Image>();
            logoImg.color = GlassOverlay;
            logoImg.raycastTarget = false;

            // Game title text (placeholder for actual logo)
            var titleText = CreateChild(logoImage, "TitleText");
            SetStretch(titleText);

            var titleTmp = titleText.AddComponent<TextMeshProUGUI>();
            titleTmp.text = "PROJECT SC";
            titleTmp.fontSize = 72;
            titleTmp.fontStyle = FontStyles.Bold;
            titleTmp.color = TextPrimary;
            titleTmp.alignment = TextAlignmentOptions.Center;
            titleTmp.enableWordWrapping = false;

            // Subtitle
            var subtitle = CreateChild(logoArea, "Subtitle");
            var subRect = subtitle.AddComponent<RectTransform>();
            subRect.anchorMin = new Vector2(0.5f, 0f);
            subRect.anchorMax = new Vector2(0.5f, 0f);
            subRect.pivot = new Vector2(0.5f, 1f);
            subRect.sizeDelta = new Vector2(400, 30);
            subRect.anchoredPosition = new Vector2(0, 10);

            var subTmp = subtitle.AddComponent<TextMeshProUGUI>();
            subTmp.text = "SUBCULTURE COLLECTION RPG";
            subTmp.fontSize = 14;
            subTmp.characterSpacing = 8;
            subTmp.color = TextMuted;
            subTmp.alignment = TextAlignmentOptions.Center;

            // Decorative lines
            CreateDecorativeLine(logoArea, true);
            CreateDecorativeLine(logoArea, false);
        }

        private static void CreateDecorativeLine(GameObject parent, bool isLeft)
        {
            var line = CreateChild(parent, isLeft ? "LeftLine" : "RightLine");
            var rect = line.AddComponent<RectTransform>();

            float xAnchor = isLeft ? 0f : 1f;
            float xOffset = isLeft ? -30f : 30f;

            rect.anchorMin = new Vector2(xAnchor, 0.5f);
            rect.anchorMax = new Vector2(xAnchor, 0.5f);
            rect.pivot = new Vector2(isLeft ? 1f : 0f, 0.5f);
            rect.sizeDelta = new Vector2(80, 2);
            rect.anchoredPosition = new Vector2(xOffset, 0);

            var image = line.AddComponent<Image>();
            image.color = new Color(AccentCyan.r, AccentCyan.g, AccentCyan.b, 0.4f);
            image.raycastTarget = false;
        }

        #endregion

        #region Touch Area & Text

        private static Button CreateTouchArea(GameObject parent)
        {
            var touchArea = CreateChild(parent, "TouchArea");
            SetStretch(touchArea);

            // Invisible full-screen touch target
            var image = touchArea.AddComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
            image.raycastTarget = true;

            var button = touchArea.AddComponent<Button>();
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = new Color(1, 1, 1, 0.8f);
            button.colors = colors;

            return button;
        }

        private static TMP_Text CreateTouchToStartText(GameObject parent)
        {
            var container = CreateChild(parent, "TouchToStartContainer");
            var containerRect = container.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0f);
            containerRect.anchorMax = new Vector2(0.5f, 0f);
            containerRect.pivot = new Vector2(0.5f, 0f);
            containerRect.sizeDelta = new Vector2(400, 60);
            containerRect.anchoredPosition = new Vector2(0, TOUCH_TEXT_BOTTOM_OFFSET);

            // Glow behind text
            var textGlow = CreateChild(container, "TextGlow");
            var glowRect = textGlow.AddComponent<RectTransform>();
            glowRect.anchorMin = new Vector2(0.5f, 0.5f);
            glowRect.anchorMax = new Vector2(0.5f, 0.5f);
            glowRect.sizeDelta = new Vector2(300, 40);

            var glowImage = textGlow.AddComponent<Image>();
            glowImage.color = new Color(0, 0.8f, 1f, 0.15f);
            glowImage.raycastTarget = false;

            // Main text
            var textObj = CreateChild(container, "TouchText");
            SetStretch(textObj);

            var tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = "TOUCH TO START";
            tmp.fontSize = 24;
            tmp.characterSpacing = 6;
            tmp.color = AccentCyan;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;

            // Decorative brackets
            CreateTextBracket(container, true);
            CreateTextBracket(container, false);

            return tmp;
        }

        private static void CreateTextBracket(GameObject parent, bool isLeft)
        {
            var bracket = CreateChild(parent, isLeft ? "LeftBracket" : "RightBracket");
            var rect = bracket.AddComponent<RectTransform>();

            rect.anchorMin = new Vector2(isLeft ? 0f : 1f, 0.5f);
            rect.anchorMax = new Vector2(isLeft ? 0f : 1f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20, 30);
            rect.anchoredPosition = new Vector2(isLeft ? 20f : -20f, 0);

            var tmp = bracket.AddComponent<TextMeshProUGUI>();
            tmp.text = isLeft ? "[" : "]";
            tmp.fontSize = 28;
            tmp.color = new Color(AccentCyan.r, AccentCyan.g, AccentCyan.b, 0.5f);
            tmp.alignment = TextAlignmentOptions.Center;
        }

        #endregion

        #region Reset Account Button

        private static Button CreateResetAccountButton(GameObject parent)
        {
            var btnContainer = CreateChild(parent, "ResetAccountButton");
            var rect = btnContainer.AddComponent<RectTransform>();

            // Top-right corner
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(RESET_BUTTON_SIZE, RESET_BUTTON_SIZE);
            rect.anchoredPosition = new Vector2(-RESET_BUTTON_MARGIN, -RESET_BUTTON_MARGIN);

            // Background (subtle glass effect)
            var bgImage = btnContainer.AddComponent<Image>();
            bgImage.color = new Color(1f, 1f, 1f, 0.05f);
            bgImage.raycastTarget = true;

            var button = btnContainer.AddComponent<Button>();
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1, 1, 1, 1.5f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            button.colors = colors;

            // Icon (gear/settings symbol)
            var icon = CreateChild(btnContainer, "Icon");
            SetStretch(icon);

            var iconTmp = icon.AddComponent<TextMeshProUGUI>();
            iconTmp.text = "R";
            iconTmp.fontSize = 16;
            iconTmp.color = TextDim;
            iconTmp.alignment = TextAlignmentOptions.Center;
            iconTmp.fontStyle = FontStyles.Bold;

            return button;
        }

        #endregion

        #region Version Info

        private static void CreateVersionInfo(GameObject parent)
        {
            var versionContainer = CreateChild(parent, "VersionInfo");
            var rect = versionContainer.AddComponent<RectTransform>();

            // Bottom-right corner
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(1f, 0f);
            rect.sizeDelta = new Vector2(200, 40);
            rect.anchoredPosition = new Vector2(-20, 20);

            var tmp = versionContainer.AddComponent<TextMeshProUGUI>();
            tmp.text = "v0.1.0";
            tmp.fontSize = 12;
            tmp.color = TextDim;
            tmp.alignment = TextAlignmentOptions.BottomRight;
        }

        #endregion

        #region SerializeField Connections

        private static void ConnectSerializedFields(GameObject root, Button touchArea, TMP_Text touchText,
            Button resetButton)
        {
            var titleScreen = root.GetComponent<TitleScreen>();
            var so = new SerializedObject(titleScreen);

            so.FindProperty("_touchArea").objectReferenceValue = touchArea;
            so.FindProperty("_touchToStartText").objectReferenceValue = touchText;
            so.FindProperty("_resetAccountButton").objectReferenceValue = resetButton;

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