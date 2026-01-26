using System;
using System.Reflection;
using Sc.Common.UI.Attributes;
using Sc.Editor.AI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// PopupTemplateAttribute를 읽어서 해당 템플릿 유형에 맞는 Popup 프리팹 구조를 생성하는 팩토리.
    /// </summary>
    public static class PopupTemplateFactory
    {
        #region Public API

        /// <summary>
        /// Popup 타입의 Attribute를 읽어서 적절한 템플릿으로 GameObject 생성.
        /// </summary>
        /// <param name="popupType">Popup 컴포넌트 타입</param>
        /// <returns>생성된 Popup GameObject</returns>
        public static GameObject Create(Type popupType)
        {
            var attr = popupType.GetCustomAttribute<PopupTemplateAttribute>();
            var templateType = attr?.TemplateType ?? PopupTemplateType.Confirm;
            var size = new Vector2(attr?.Width ?? 500, attr?.Height ?? 300);

            return templateType switch
            {
                PopupTemplateType.Confirm => CreateConfirm(popupType, size),
                PopupTemplateType.Reward => CreateReward(popupType, size),
                PopupTemplateType.Info => CreateInfo(popupType, size),
                PopupTemplateType.FullScreen => CreateFullScreen(popupType),
                _ => CreateConfirm(popupType, size)
            };
        }

        #endregion

        #region Template Creators

        /// <summary>
        /// Confirm 템플릿 생성.
        /// Root (Stretch + CanvasGroup + PopupComponent)
        /// ├─ Backdrop (Stretch, Dim + CloseButton)
        /// └─ Container (Center, size)
        ///     ├─ Header (Top, 50px) - Title
        ///     ├─ Body (Stretch, Top=50, Bottom=60) - Message
        ///     └─ Footer (Bottom, 60px) - Buttons (Cancel, Confirm)
        /// </summary>
        private static GameObject CreateConfirm(Type popupType, Vector2 size)
        {
            const float headerHeight = 50f;
            const float footerHeight = 60f;

            // Root
            var root = UIComponentBuilder.CreateRoot(popupType.Name);

            // Backdrop
            UIComponentBuilder.CreateBackdrop(root, closeOnClick: true);

            // Container
            var container = UIComponentBuilder.CreateContainer(root, size);

            // Header
            var header = CreatePopupHeader(container.gameObject, headerHeight);
            CreateTitleText(header.gameObject, "Title");

            // Body
            var body = CreateBody(container.gameObject, headerHeight, footerHeight);
            CreateMessageText(body.gameObject);

            // Footer
            var footer = CreatePopupFooter(container.gameObject, footerHeight);
            CreateConfirmFooterButtons(footer.gameObject);

            // Add popup component
            root.AddComponent(popupType);

            return root;
        }

        /// <summary>
        /// Reward 템플릿 생성.
        /// Root (Stretch + CanvasGroup + PopupComponent)
        /// ├─ Backdrop (Stretch, Dim + CloseButton)
        /// └─ Container (Center, size)
        ///     ├─ Header (Top, 60px) - Title
        ///     ├─ GridBody (Stretch, Top=60, Bottom=70) - Grid for rewards
        ///     └─ Footer (Bottom, 70px) - Confirm Button
        /// </summary>
        private static GameObject CreateReward(Type popupType, Vector2 size)
        {
            const float headerHeight = 60f;
            const float footerHeight = 70f;

            // Root
            var root = UIComponentBuilder.CreateRoot(popupType.Name);

            // Backdrop
            UIComponentBuilder.CreateBackdrop(root, closeOnClick: true);

            // Container
            var container = UIComponentBuilder.CreateContainer(root, size);

            // Header
            var header = CreatePopupHeader(container.gameObject, headerHeight);
            CreateTitleText(header.gameObject, "Rewards");

            // GridBody
            var gridBody = CreateBody(container.gameObject, headerHeight, footerHeight, "GridBody");
            CreateRewardGrid(gridBody.gameObject);

            // Footer
            var footer = CreatePopupFooter(container.gameObject, footerHeight);
            CreateSingleButton(footer.gameObject, "ConfirmButton", "Confirm");

            // Add popup component
            root.AddComponent(popupType);

            return root;
        }

        /// <summary>
        /// Info 템플릿 생성.
        /// Root (Stretch + CanvasGroup + PopupComponent)
        /// ├─ Backdrop (Stretch, Dim + CloseButton)
        /// └─ Container (Center, size)
        ///     ├─ Header (Top, 60px) - Title + CloseButton
        ///     └─ ScrollBody (Stretch, Top=60) - ScrollRect
        /// </summary>
        private static GameObject CreateInfo(Type popupType, Vector2 size)
        {
            const float headerHeight = 60f;

            // Root
            var root = UIComponentBuilder.CreateRoot(popupType.Name);

            // Backdrop
            UIComponentBuilder.CreateBackdrop(root, closeOnClick: true);

            // Container
            var container = UIComponentBuilder.CreateContainer(root, size);

            // Header with close button
            var header = CreatePopupHeader(container.gameObject, headerHeight);
            CreateTitleText(header.gameObject, "Information");
            CreateHeaderCloseButton(header.gameObject);

            // ScrollBody
            UIComponentBuilder.CreateScrollContent(container.gameObject, headerHeight, 0f);

            // Add popup component
            root.AddComponent(popupType);

            return root;
        }

        /// <summary>
        /// FullScreen 템플릿 생성.
        /// Root (Stretch + CanvasGroup + PopupComponent)
        /// ├─ Background (Stretch, BgDeep)
        /// ├─ Content (Stretch)
        /// └─ CloseButton (TopRight corner)
        /// </summary>
        private static GameObject CreateFullScreen(Type popupType)
        {
            // Root
            var root = UIComponentBuilder.CreateRoot(popupType.Name);

            // Background
            UIComponentBuilder.CreateBackground(root, UITheme.BgDeep);

            // Content
            UIComponentBuilder.CreateContent(root, 0f, 0f);

            // Close button (TopRight)
            CreateFullScreenCloseButton(root);

            // Add popup component
            root.AddComponent(popupType);

            return root;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Popup용 Header 생성 (Container 내부).
        /// </summary>
        private static RectTransform CreatePopupHeader(GameObject parent, float height)
        {
            var go = UIComponentBuilder.CreateChild(parent, "Header");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // Top anchored, stretch horizontal
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(0, height);

            return rect;
        }

        /// <summary>
        /// Popup용 Body 생성 (Container 내부).
        /// </summary>
        private static RectTransform CreateBody(GameObject parent, float topOffset, float bottomOffset,
            string name = "Body")
        {
            var go = UIComponentBuilder.CreateChild(parent, name);
            var rect = UIComponentBuilder.SetStretch(go);

            rect.offsetMin = new Vector2(UITheme.Padding, bottomOffset);
            rect.offsetMax = new Vector2(-UITheme.Padding, -topOffset);

            return rect;
        }

        /// <summary>
        /// Popup용 Footer 생성 (Container 내부).
        /// </summary>
        private static RectTransform CreatePopupFooter(GameObject parent, float height)
        {
            var go = UIComponentBuilder.CreateChild(parent, "Footer");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // Bottom anchored, stretch horizontal
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(0.5f, 0);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(0, height);

            // Add HorizontalLayoutGroup for button alignment
            var layoutGroup = go.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.spacing = UITheme.Spacing;
            layoutGroup.padding = new RectOffset(
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                (int)UITheme.PaddingSmall,
                (int)UITheme.PaddingSmall
            );
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;

            return rect;
        }

        /// <summary>
        /// Title 텍스트 생성.
        /// </summary>
        private static void CreateTitleText(GameObject parent, string defaultText)
        {
            var go = UIComponentBuilder.CreateChild(parent, "TxtTitle");
            var rect = UIComponentBuilder.SetStretch(go);

            // Add padding
            rect.offsetMin = new Vector2(UITheme.Padding, 0);
            rect.offsetMax = new Vector2(-UITheme.Padding, 0);

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = defaultText;
            tmp.fontSize = UITheme.FontSizeTitle;
            tmp.color = UITheme.TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;

            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        /// <summary>
        /// Message 텍스트 생성 (Body 내부).
        /// </summary>
        private static void CreateMessageText(GameObject parent)
        {
            var go = UIComponentBuilder.CreateChild(parent, "TxtMessage");
            var rect = UIComponentBuilder.SetStretch(go);

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = "Message placeholder";
            tmp.fontSize = UITheme.FontSizeBody;
            tmp.color = UITheme.TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.raycastTarget = false;

            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        /// <summary>
        /// Confirm 팝업용 Footer 버튼들 생성 (Cancel, Confirm).
        /// </summary>
        private static void CreateConfirmFooterButtons(GameObject parent)
        {
            // Cancel Button
            CreateButtonPlaceholder(parent, "BtnCancel", "Cancel", UITheme.BgGlass, UITheme.TextPrimary);

            // Confirm Button
            CreateButtonPlaceholder(parent, "BtnConfirm", "Confirm", UITheme.AccentPrimary, UITheme.TextOnButton);
        }

        /// <summary>
        /// 단일 버튼 생성.
        /// </summary>
        private static void CreateSingleButton(GameObject parent, string name, string text)
        {
            CreateButtonPlaceholder(parent, name, text, UITheme.AccentPrimary, UITheme.TextOnButton);
        }

        /// <summary>
        /// 버튼 Placeholder 생성.
        /// </summary>
        private static void CreateButtonPlaceholder(GameObject parent, string name, string text, Color bgColor,
            Color textColor)
        {
            var go = UIComponentBuilder.CreateChild(parent, name);
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // LayoutElement for sizing
            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = UITheme.ButtonHeightNormal;
            layoutElement.preferredHeight = UITheme.ButtonHeightNormal;

            // Background
            var image = go.AddComponent<Image>();
            image.color = bgColor;
            image.raycastTarget = true;

            // Button
            var button = go.AddComponent<Button>();
            button.targetGraphic = image;

            // Text
            var textGo = UIComponentBuilder.CreateChild(go, "Text");
            UIComponentBuilder.SetStretch(textGo);

            var tmp = textGo.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = UITheme.FontSizeBody;
            tmp.color = textColor;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;

            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        /// <summary>
        /// Reward Grid 생성.
        /// </summary>
        private static void CreateRewardGrid(GameObject parent)
        {
            var gridLayout = parent.AddComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(80, 100);
            gridLayout.spacing = new Vector2(UITheme.Spacing, UITheme.Spacing);
            gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
            gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
            gridLayout.childAlignment = TextAnchor.UpperCenter;
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = 4;
            gridLayout.padding = new RectOffset(
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                (int)UITheme.Padding
            );
        }

        /// <summary>
        /// Header Close 버튼 생성 (Info 팝업용).
        /// </summary>
        private static void CreateHeaderCloseButton(GameObject parent)
        {
            var go = UIComponentBuilder.CreateChild(parent, "BtnClose");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // TopRight anchored
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-UITheme.PaddingSmall, -UITheme.PaddingSmall);
            rect.sizeDelta = new Vector2(40, 40);

            // Background
            var image = go.AddComponent<Image>();
            image.color = UITheme.BgGlass;
            image.raycastTarget = true;

            // Button
            var button = go.AddComponent<Button>();
            button.targetGraphic = image;

            // X Text
            var textGo = UIComponentBuilder.CreateChild(go, "Text");
            UIComponentBuilder.SetStretch(textGo);

            var tmp = textGo.AddComponent<TextMeshProUGUI>();
            tmp.text = "X";
            tmp.fontSize = UITheme.FontSizeSubtitle;
            tmp.color = UITheme.TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;

            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        /// <summary>
        /// FullScreen Close 버튼 생성.
        /// </summary>
        private static void CreateFullScreenCloseButton(GameObject parent)
        {
            var go = UIComponentBuilder.CreateChild(parent, "BtnClose");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // TopRight anchored
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-UITheme.Padding, -UITheme.Padding);
            rect.sizeDelta = new Vector2(48, 48);

            // Background
            var image = go.AddComponent<Image>();
            image.color = UITheme.BgCard;
            image.raycastTarget = true;

            // Button
            var button = go.AddComponent<Button>();
            button.targetGraphic = image;

            // X Text
            var textGo = UIComponentBuilder.CreateChild(go, "Text");
            UIComponentBuilder.SetStretch(textGo);

            var tmp = textGo.AddComponent<TextMeshProUGUI>();
            tmp.text = "X";
            tmp.fontSize = UITheme.FontSizeTitle;
            tmp.color = UITheme.TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontStyle = FontStyles.Bold;
            tmp.raycastTarget = false;

            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion
    }
}