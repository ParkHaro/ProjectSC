using System.Collections.Generic;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Sc.Common.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// RewardPopup 프리팹 빌더 (자동 생성됨).
    /// Generated from: Assets/Prefabs/UI/Popups/RewardPopup.prefab
    /// Generated at: 2026-01-27 14:43:10
    /// </summary>
    public static class RewardPopupPrefabBuilder_Generated
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
        private static readonly Color Blue = new Color32(100, 200, 255, 255);
        private static readonly Color Color = new Color32(25, 25, 45, 217);

        #endregion

        #region Constants

        private const float CONFIRM_BUTTON_HEIGHT = 100f;
        private const float CONFIRM_BUTTON_WIDTH = 100f;
        private const float CONTAINER_HEIGHT = 500f;
        private const float CONTAINER_WIDTH = 600f;
        private const float FOOTER_HEIGHT = 70f;
        private const float GRID_BODY_HEIGHT = 130f;
        private const float HEADER_HEIGHT = 60f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// RewardPopup 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("RewardPopup");

            var backdrop = CreateBackdrop(root);
            var container = CreateContainer(root);

            // Add main component
            root.AddComponent<RewardPopup>();

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
            rect.sizeDelta = new Vector2(600f, 500f);
            rect.anchoredPosition = new Vector2(0f, 0f);


            var image = go.AddComponent<Image>();
            image.color = BgCard;
            image.raycastTarget = true;

            CreateHeader(go);
            CreateGridBody(go);
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
            rect.sizeDelta = new Vector2(0f, 60f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            CreateTxtTitle(go);

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
            tmp.text = "Rewards";
            tmp.fontSize = 24f;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region GridBody

        private static GameObject CreateGridBody(GameObject parent)
        {
            var go = CreateChild(parent, "GridBody");
            SetStretch(go);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(16f, 70f);
            rect.offsetMax = new Vector2(-16f, -60f);

            var grid = go.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(80f, 100f);
            grid.spacing = new Vector2(12f, 12f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperCenter;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 4;
            grid.padding = new RectOffset(16, 16, 16, 16);

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
            rect.sizeDelta = new Vector2(0f, 70f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 12f;
            layout.padding = new RectOffset(16, 16, 8, 8);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateConfirmButton(go);

            return go;
        }

        #endregion

        #region ConfirmButton

        private static GameObject CreateConfirmButton(GameObject parent)
        {
            var go = CreateChild(parent, "ConfirmButton");
            var rect = go.GetComponent<RectTransform>();
            if (rect == null) rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(100f, 100f);
            rect.anchoredPosition = new Vector2(0f, 0f);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 44f;
            layoutElement.preferredHeight = 44f;


            var image = go.AddComponent<Image>();
            image.color = new Color32(100, 200, 255, 255);
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
            tmp.text = "Confirm";
            tmp.fontSize = 14f;
            tmp.color = BgDeep;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;
            ApplyFont(tmp);

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
