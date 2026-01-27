using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Title;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// TitleScreen 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Screens/TitleScreen.prefab
    /// Generated at: 2026-01-27 14:42:52
    /// </summary>
    public static class TitleScreenPrefabBuilder_Generated
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
        private static readonly Color BgGlass = new Color32(0, 128, 178, 38);
        private static readonly Color Red = new Color32(255, 80, 160, 153);

        #endregion

        #region Constants

        private const float BOTTOM_GLOW_HEIGHT = 200f;
        private const float BOTTOM_GLOW_WIDTH = 500f;
        private const float CENTER_GLOW_HEIGHT = 300f;
        private const float CENTER_GLOW_WIDTH = 600f;
        private const float LEFT_BRACKET_HEIGHT = 30f;
        private const float LEFT_BRACKET_WIDTH = 20f;
        private const float LEFT_LINE_HEIGHT = 2f;
        private const float LEFT_LINE_WIDTH = 80f;
        private const float LOGO_AREA_HEIGHT = 200f;
        private const float LOGO_AREA_WIDTH = 500f;
        private const float LOGO_GLOW_HEIGHT = 180f;
        private const float LOGO_GLOW_WIDTH = 600f;
        private const float LOGO_IMAGE_HEIGHT = 120f;
        private const float LOGO_IMAGE_WIDTH = 500f;
        private const float PARTICLE_SPOT_HEIGHT = 5f;
        private const float PARTICLE_SPOT_WIDTH = 5f;
        private const float RESET_ACCOUNT_BUTTON_HEIGHT = 40f;
        private const float RESET_ACCOUNT_BUTTON_WIDTH = 40f;
        private const float RIGHT_BRACKET_HEIGHT = 30f;
        private const float RIGHT_BRACKET_WIDTH = 20f;
        private const float RIGHT_LINE_HEIGHT = 2f;
        private const float RIGHT_LINE_WIDTH = 80f;
        private const float SUBTITLE_HEIGHT = 30f;
        private const float SUBTITLE_WIDTH = 400f;
        private const float TEXT_GLOW_HEIGHT = 40f;
        private const float TEXT_GLOW_WIDTH = 300f;
        private const float TOP_GLOW_HEIGHT = 400f;
        private const float TOP_GLOW_WIDTH = 800f;
        private const float TOUCH_TO_START_CONTAINER_HEIGHT = 60f;
        private const float TOUCH_TO_START_CONTAINER_WIDTH = 400f;
        private const float VERSION_INFO_HEIGHT = 40f;
        private const float VERSION_INFO_WIDTH = 200f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// TitleScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("TitleScreen");

            var background = CreateBackground(root);
            var ambientEffects = CreateAmbientEffects(root);
            var logoArea = CreateLogoArea(root);
            var touchArea = CreateTouchArea(root);
            var touchToStartContainer = CreateTouchToStartContainer(root);
            var resetAccountButton = CreateResetAccountButton(root);
            var versionInfo = CreateVersionInfo(root);

            // Add main component
            root.AddComponent<TitleScreen>();

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
            image.color = new Color32(5, 5, 12, 255);
            image.raycastTarget = false;

            CreateGradientOverlay(go);
            CreateVignette(go);

            return go;
        }

        #endregion

        #region GradientOverlay

        private static GameObject CreateGradientOverlay(GameObject parent)
        {
            var go = CreateChild(parent, "GradientOverlay");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(10, 8, 20, 255);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region Vignette

        private static GameObject CreateVignette(GameObject parent)
        {
            var go = CreateChild(parent, "Vignette");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region AmbientEffects

        private static GameObject CreateAmbientEffects(GameObject parent)
        {
            var go = CreateChild(parent, "AmbientEffects");
            SetStretch(go);

            CreateTopGlow(go);
            CreateCenterGlow(go);
            CreateBottomGlow(go);
            CreateParticleSpot_1(go);
            CreateParticleSpot_2(go);
            CreateParticleSpot_3(go);
            CreateParticleSpot_4(go);
            CreateParticleSpot_5(go);
            CreateParticleSpot_6(go);

            return go;
        }

        #endregion

        #region TopGlow

        private static GameObject CreateTopGlow(GameObject parent)
        {
            var go = CreateChild(parent, "TopGlow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(800f, 400f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 128, 178, 38);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region CenterGlow

        private static GameObject CreateCenterGlow(GameObject parent)
        {
            var go = CreateChild(parent, "CenterGlow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.6f);
            rect.anchorMax = new Vector2(0.5f, 0.6f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 300f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(204, 76, 128, 20);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region BottomGlow

        private static GameObject CreateBottomGlow(GameObject parent)
        {
            var go = CreateChild(parent, "BottomGlow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(500f, 200f);
            rect.anchoredPosition = new Vector2(0f, 100f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 180, 220, 80);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_1(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.2f, 0.7f);
            rect.anchorMax = new Vector2(0.2f, 0.7f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(8f, 8f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_2(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.8f, 0.8f);
            rect.anchorMax = new Vector2(0.8f, 0.8f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(6f, 6f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 80, 160, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_3(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.15f, 0.3f);
            rect.anchorMax = new Vector2(0.15f, 0.3f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(5f, 5f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_4(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.85f, 0.4f);
            rect.anchorMax = new Vector2(0.85f, 0.4f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(7f, 7f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_5(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.3f, 0.5f);
            rect.anchorMax = new Vector2(0.3f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(4f, 4f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 80, 160, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region ParticleSpot

        private static GameObject CreateParticleSpot_6(GameObject parent)
        {
            var go = CreateChild(parent, "ParticleSpot");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.7f, 0.6f);
            rect.anchorMax = new Vector2(0.7f, 0.6f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(5f, 5f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 200, 100, 153);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region LogoArea

        private static GameObject CreateLogoArea(GameObject parent)
        {
            var go = CreateChild(parent, "LogoArea");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(500f, 200f);
            rect.anchoredPosition = new Vector2(0f, -180f);

            CreateLogoGlow(go);
            CreateLogoImage(go);
            CreateSubtitle(go);
            CreateLeftLine(go);
            CreateRightLine(go);

            return go;
        }

        #endregion

        #region LogoGlow

        private static GameObject CreateLogoGlow(GameObject parent)
        {
            var go = CreateChild(parent, "LogoGlow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 180f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 178, 255, 26);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region LogoImage

        private static GameObject CreateLogoImage(GameObject parent)
        {
            var go = CreateChild(parent, "LogoImage");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(500f, 120f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = false;

            CreateTitleText(go);

            return go;
        }

        #endregion

        #region TitleText

        private static GameObject CreateTitleText(GameObject parent)
        {
            var go = CreateChild(parent, "TitleText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "PROJECT SC";
            tmp.fontSize = 72f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Subtitle

        private static GameObject CreateSubtitle(GameObject parent)
        {
            var go = CreateChild(parent, "Subtitle");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(400f, 30f);
            rect.anchoredPosition = new Vector2(0f, 10f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "SUBCULTURE COLLECTION RPG";
            tmp.fontSize = 14f;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region LeftLine

        private static GameObject CreateLeftLine(GameObject parent)
        {
            var go = CreateChild(parent, "LeftLine");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(1f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 2f);
            rect.anchoredPosition = new Vector2(-30f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 102);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region RightLine

        private static GameObject CreateRightLine(GameObject parent)
        {
            var go = CreateChild(parent, "RightLine");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(80f, 2f);
            rect.anchoredPosition = new Vector2(30f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 212, 255, 102);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region TouchArea

        private static GameObject CreateTouchArea(GameObject parent)
        {
            var go = CreateChild(parent, "TouchArea");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 0);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            return go;
        }

        #endregion

        #region TouchToStartContainer

        private static GameObject CreateTouchToStartContainer(GameObject parent)
        {
            var go = CreateChild(parent, "TouchToStartContainer");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.pivot = new Vector2(0.5f, 0f);
            rect.sizeDelta = new Vector2(400f, 60f);
            rect.anchoredPosition = new Vector2(0f, 200f);

            CreateTextGlow(go);
            CreateTouchText(go);
            CreateLeftBracket(go);
            CreateRightBracket(go);

            return go;
        }

        #endregion

        #region TextGlow

        private static GameObject CreateTextGlow(GameObject parent)
        {
            var go = CreateChild(parent, "TextGlow");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(300f, 40f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 204, 255, 38);
            image.raycastTarget = false;

            return go;
        }

        #endregion

        #region TouchText

        private static GameObject CreateTouchText(GameObject parent)
        {
            var go = CreateChild(parent, "TouchText");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "TOUCH TO START";
            tmp.fontSize = 24f;
            tmp.color = AccentPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region LeftBracket

        private static GameObject CreateLeftBracket(GameObject parent)
        {
            var go = CreateChild(parent, "LeftBracket");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 30f);
            rect.anchoredPosition = new Vector2(20f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "[";
            tmp.fontSize = 28f;
            tmp.color = new Color32(0, 212, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region RightBracket

        private static GameObject CreateRightBracket(GameObject parent)
        {
            var go = CreateChild(parent, "RightBracket");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0.5f);
            rect.anchorMax = new Vector2(1f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(20f, 30f);
            rect.anchoredPosition = new Vector2(-20f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "]";
            tmp.fontSize = 28f;
            tmp.color = new Color32(0, 212, 255, 128);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ResetAccountButton

        private static GameObject CreateResetAccountButton(GameObject parent)
        {
            var go = CreateChild(parent, "ResetAccountButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(-20f, -20f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 13);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateIcon(go);

            return go;
        }

        #endregion

        #region Icon

        private static GameObject CreateIcon(GameObject parent)
        {
            var go = CreateChild(parent, "Icon");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "R";
            tmp.fontSize = 16f;
            tmp.color = new Color32(255, 255, 255, 51);
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region VersionInfo

        private static GameObject CreateVersionInfo(GameObject parent)
        {
            var go = CreateChild(parent, "VersionInfo");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(1f, 0f);
            rect.sizeDelta = new Vector2(200f, 40f);
            rect.anchoredPosition = new Vector2(-20f, 20f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "v0.1.0";
            tmp.fontSize = 12f;
            tmp.color = new Color32(255, 255, 255, 51);
            tmp.alignment = TextAlignmentOptions.BottomRight;
            tmp.raycastTarget = true;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region SerializedField Connection

        private static void ConnectSerializedFields(GameObject root)
        {
            var component = root.GetComponent<TitleScreen>();
            if (component == null) return;

            var so = new SerializedObject(component);

            // _touchArea
            so.FindProperty("_touchArea").objectReferenceValue = FindChild(root, "TouchArea")?.GetComponent<Button>();

            // _touchToStartText
            so.FindProperty("_touchToStartText").objectReferenceValue = FindChild(root, "TouchToStartContainer/TouchText")?.GetComponent<TextMeshProUGUI>();

            // _resetAccountButton
            so.FindProperty("_resetAccountButton").objectReferenceValue = FindChild(root, "ResetAccountButton")?.GetComponent<Button>();

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
