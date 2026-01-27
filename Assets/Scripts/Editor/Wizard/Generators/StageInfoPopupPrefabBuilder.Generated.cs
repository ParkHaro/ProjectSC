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
    /// StageInfoPopup 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Popups/StageInfoPopup.prefab
    /// Generated at: 2026-01-27 14:43:10
    /// </summary>
    public static class StageInfoPopupPrefabBuilder_Generated
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
        private static readonly Color Color = new Color32(25, 25, 45, 217);

        #endregion

        #region Constants

        private const float BTN_CLOSE_HEIGHT = 40f;
        private const float BTN_CLOSE_WIDTH = 40f;
        private const float CONTAINER_HEIGHT = 700f;
        private const float CONTAINER_WIDTH = 600f;
        private const float HEADER_HEIGHT = 60f;
        private const float SCROLL_VIEW_HEIGHT = 60f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// StageInfoPopup 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("StageInfoPopup");

            var backdrop = CreateBackdrop(root);
            var container = CreateContainer(root);

            // Add main component
            root.AddComponent<StageInfoPopup>();

            return root;
        }

        #region Backdrop

        private static GameObject CreateBackdrop(GameObject parent)
        {
            var go = CreateChild(parent, "Backdrop");
            SetStretch(go);


            var image = go.AddComponent<Image>();
            image.color = new Color32(0, 0, 0, 178);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            return go;
        }

        #endregion

        #region Container

        private static GameObject CreateContainer(GameObject parent)
        {
            var go = CreateChild(parent, "Container");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(600f, 700f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = BgCard;
            image.raycastTarget = true;

            CreateHeader(go);
            CreateScrollView(go);

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

            CreateTxtTitle(go);
            CreateBtnClose(go);

            return go;
        }

        #endregion

        #region TxtTitle

        private static GameObject CreateTxtTitle(GameObject parent)
        {
            var go = CreateChild(parent, "TxtTitle");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(16f, 0f);
            rect.offsetMax = new Vector2(-16f, 0f);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Information";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region BtnClose

        private static GameObject CreateBtnClose(GameObject parent)
        {
            var go = CreateChild(parent, "BtnClose");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(40f, 40f);
            rect.anchoredPosition = new Vector2(-8f, -8f);


            var image = go.AddComponent<Image>();
            image.color = new Color32(255, 255, 255, 8);
            image.raycastTarget = true;

            var button = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            if (img != null) button.targetGraphic = img;

            CreateText(go);

            return go;
        }

        #endregion

        #region Text

        private static GameObject CreateText(GameObject parent)
        {
            var go = CreateChild(parent, "Text");
            SetStretch(go);


            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "X";
            tmp.fontSize = 18f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region ScrollView

        private static GameObject CreateScrollView(GameObject parent)
        {
            var go = CreateChild(parent, "ScrollView");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0f, 0f);
            rect.offsetMax = new Vector2(0f, -60f);

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

            var mask = go.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            CreateContent(go);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = new Vector2(0f, 0f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(16, 16, 16, 16);
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return go;
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
