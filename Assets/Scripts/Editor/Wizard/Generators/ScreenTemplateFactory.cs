using System;
using System.Reflection;
using Sc.Common.UI.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// Screen 타입의 ScreenTemplateAttribute를 읽어서 해당 템플릿 유형에 맞는
    /// Screen 프리팹 구조를 생성하는 팩토리 클래스.
    /// </summary>
    public static class ScreenTemplateFactory
    {
        /// <summary>
        /// Screen 타입의 Attribute를 읽어서 적절한 템플릿으로 GameObject 생성.
        /// </summary>
        /// <param name="screenType">Screen 컴포넌트 타입</param>
        /// <returns>생성된 Screen GameObject</returns>
        public static GameObject Create(Type screenType)
        {
            var attr = screenType.GetCustomAttribute<ScreenTemplateAttribute>();
            var templateType = attr?.TemplateType ?? ScreenTemplateType.Standard;

            return templateType switch
            {
                ScreenTemplateType.FullScreen => CreateFullScreen(screenType),
                ScreenTemplateType.Standard => CreateStandard(screenType),
                ScreenTemplateType.Tabbed => CreateTabbed(screenType),
                ScreenTemplateType.Detail => CreateDetail(screenType),
                _ => CreateStandard(screenType)
            };
        }

        /// <summary>
        /// FullScreen 템플릿 생성.
        /// Root (Stretch + CanvasGroup + ScreenComponent)
        /// ├─ Background (Image: BgDeep)
        /// └─ Content (Stretch)
        /// </summary>
        private static GameObject CreateFullScreen(Type screenType)
        {
            // Root
            var root = UIComponentBuilder.CreateRoot(screenType.Name);

            // Background
            UIComponentBuilder.CreateBackground(root);

            // Content
            UIComponentBuilder.CreateContent(root);

            // Add Screen component
            root.AddComponent(screenType);

            return root;
        }

        /// <summary>
        /// Standard 템플릿 생성.
        /// Root (Stretch + CanvasGroup + ScreenComponent)
        /// ├─ Background (Image: BgDeep)
        /// └─ SafeArea (Stretch)
        ///     ├─ Header (Top, 80px) - Placeholder
        ///     └─ Content (Stretch, Top=80)
        /// </summary>
        private static GameObject CreateStandard(Type screenType)
        {
            // Root
            var root = UIComponentBuilder.CreateRoot(screenType.Name);

            // Background
            UIComponentBuilder.CreateBackground(root);

            // SafeArea
            var safeArea = UIComponentBuilder.CreateSafeArea(root);

            // Header (Placeholder)
            UIComponentBuilder.CreateHeader(safeArea.gameObject, UITheme.HeaderHeight);

            // Content
            UIComponentBuilder.CreateContent(safeArea.gameObject, topOffset: UITheme.HeaderHeight);

            // Add Screen component
            root.AddComponent(screenType);

            return root;
        }

        /// <summary>
        /// Tabbed 템플릿 생성.
        /// Root (Stretch + CanvasGroup + ScreenComponent)
        /// ├─ Background (Image: BgDeep)
        /// └─ SafeArea (Stretch)
        ///     ├─ Header (Top, 80px) - Placeholder
        ///     ├─ Content (Stretch, Top=80, Bottom=80)
        ///     └─ Footer (Bottom, 80px) - Placeholder
        /// </summary>
        private static GameObject CreateTabbed(Type screenType)
        {
            // Root
            var root = UIComponentBuilder.CreateRoot(screenType.Name);

            // Background
            UIComponentBuilder.CreateBackground(root);

            // SafeArea
            var safeArea = UIComponentBuilder.CreateSafeArea(root);

            // Header (Placeholder)
            UIComponentBuilder.CreateHeader(safeArea.gameObject, UITheme.HeaderHeight);

            // Content
            UIComponentBuilder.CreateContent(
                safeArea.gameObject,
                topOffset: UITheme.HeaderHeight,
                bottomOffset: UITheme.FooterHeight
            );

            // Footer (Placeholder)
            UIComponentBuilder.CreateFooter(safeArea.gameObject, UITheme.FooterHeight);

            // Add Screen component
            root.AddComponent(screenType);

            return root;
        }

        /// <summary>
        /// Detail 템플릿 생성.
        /// Root (Stretch + CanvasGroup + ScreenComponent)
        /// ├─ Background (Image: BgDeep)
        /// └─ SafeArea (Stretch)
        ///     ├─ BackHeader (Top, 60px) - BackButton + Title
        ///     └─ ScrollContent (Stretch, Top=60) - ScrollRect
        /// </summary>
        private static GameObject CreateDetail(Type screenType)
        {
            // Root
            var root = UIComponentBuilder.CreateRoot(screenType.Name);

            // Background
            UIComponentBuilder.CreateBackground(root);

            // SafeArea
            var safeArea = UIComponentBuilder.CreateSafeArea(root);

            // BackHeader (60px) - Contains BackButton and Title placeholder
            var backHeader = UIComponentBuilder.CreateHeader(safeArea.gameObject, UITheme.BackHeaderHeight);
            backHeader.gameObject.name = "BackHeader";
            CreateBackHeaderContent(backHeader.gameObject);

            // ScrollContent
            UIComponentBuilder.CreateScrollContent(safeArea.gameObject, topOffset: UITheme.BackHeaderHeight);

            // Add Screen component
            root.AddComponent(screenType);

            return root;
        }

        /// <summary>
        /// BackHeader 내부 컨텐츠 생성 (BackButton + Title).
        /// </summary>
        private static void CreateBackHeaderContent(GameObject backHeader)
        {
            // Add HorizontalLayoutGroup for layout
            var layoutGroup = backHeader.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlWidth = false;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = true;
            layoutGroup.spacing = UITheme.SpacingSmall;
            layoutGroup.padding = new RectOffset(
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                0,
                0
            );
            layoutGroup.childAlignment = TextAnchor.MiddleLeft;

            // BackButton placeholder
            var backButton = UIComponentBuilder.CreateChild(backHeader, "BackButton");
            var backButtonRect = backButton.AddComponent<RectTransform>();
            backButtonRect.sizeDelta = new Vector2(44, 44);

            var backButtonImage = backButton.AddComponent<Image>();
            backButtonImage.color = UITheme.BgGlass;
            backButtonImage.raycastTarget = true;

            var backButtonComponent = backButton.AddComponent<Button>();
            backButtonComponent.targetGraphic = backButtonImage;

            var backButtonLayout = backButton.AddComponent<LayoutElement>();
            backButtonLayout.minWidth = 44;
            backButtonLayout.preferredWidth = 44;

            // Title placeholder
            var title = UIComponentBuilder.CreateChild(backHeader, "Title");
            title.AddComponent<RectTransform>();

            var titleLayout = title.AddComponent<LayoutElement>();
            titleLayout.flexibleWidth = 1;
        }
    }
}