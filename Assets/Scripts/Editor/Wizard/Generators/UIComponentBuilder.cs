using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// Screen/Popup 프리팹 생성 시 공통으로 사용하는 UI 컴포넌트 빌더.
    /// UITheme의 색상/레이아웃 상수를 사용하여 일관된 UI 구조를 생성.
    /// </summary>
    public static class UIComponentBuilder
    {
        #region Root & Background

        /// <summary>
        /// Root GameObject 생성 (RectTransform Stretch + CanvasGroup).
        /// </summary>
        /// <param name="name">GameObject 이름</param>
        /// <returns>생성된 Root GameObject</returns>
        public static GameObject CreateRoot(string name)
        {
            var go = new GameObject(name);
            SetStretch(go);
            go.AddComponent<CanvasGroup>();
            return go;
        }

        /// <summary>
        /// 배경 이미지 생성.
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="color">배경 색상 (기본값: UITheme.BgDeep)</param>
        /// <returns>생성된 Image 컴포넌트</returns>
        public static Image CreateBackground(GameObject parent, Color? color = null)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);

            var image = go.AddComponent<Image>();
            image.color = color ?? UITheme.BgDeep;
            image.raycastTarget = true;

            return image;
        }

        #endregion

        #region Layout Areas

        /// <summary>
        /// SafeArea 컨테이너 생성.
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <returns>SafeArea RectTransform</returns>
        public static RectTransform CreateSafeArea(GameObject parent)
        {
            var go = CreateChild(parent, "SafeArea");
            var rect = SetStretch(go);
            return rect;
        }

        /// <summary>
        /// Header 영역 생성 (Top anchored).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="height">헤더 높이 (기본값: UITheme.HeaderHeight)</param>
        /// <returns>Header RectTransform</returns>
        public static RectTransform CreateHeader(GameObject parent, float height = UITheme.HeaderHeight)
        {
            var go = CreateChild(parent, "Header");
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
        /// Content 영역 생성 (Stretch, topOffset/bottomOffset 적용).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="topOffset">상단 오프셋</param>
        /// <param name="bottomOffset">하단 오프셋</param>
        /// <returns>Content RectTransform</returns>
        public static RectTransform CreateContent(GameObject parent, float topOffset = 0f, float bottomOffset = 0f)
        {
            var go = CreateChild(parent, "Content");
            var rect = SetStretch(go);

            // Apply offsets
            rect.offsetMin = new Vector2(0, bottomOffset);
            rect.offsetMax = new Vector2(0, -topOffset);

            return rect;
        }

        /// <summary>
        /// ScrollContent 영역 생성 (ScrollRect + Viewport + Content).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="topOffset">상단 오프셋</param>
        /// <param name="bottomOffset">하단 오프셋</param>
        /// <returns>생성된 ScrollRect 컴포넌트</returns>
        public static ScrollRect CreateScrollContent(GameObject parent, float topOffset = 0f, float bottomOffset = 0f)
        {
            // ScrollView container
            var scrollViewGo = CreateChild(parent, "ScrollView");
            var scrollViewRect = SetStretch(scrollViewGo);
            scrollViewRect.offsetMin = new Vector2(0, bottomOffset);
            scrollViewRect.offsetMax = new Vector2(0, -topOffset);

            var scrollRect = scrollViewGo.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
            scrollRect.elasticity = 0.1f;
            scrollRect.inertia = true;
            scrollRect.decelerationRate = 0.135f;
            scrollRect.scrollSensitivity = 1f;

            // Viewport
            var viewportGo = CreateChild(scrollViewGo, "Viewport");
            var viewportRect = SetStretch(viewportGo);

            var viewportImage = viewportGo.AddComponent<Image>();
            viewportImage.color = Color.clear;
            viewportImage.raycastTarget = true;

            var mask = viewportGo.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            scrollRect.viewport = viewportRect;

            // Content
            var contentGo = CreateChild(viewportGo, "Content");
            var contentRect = contentGo.GetComponent<RectTransform>() ?? contentGo.AddComponent<RectTransform>();

            // Top-stretched, height will expand based on content
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.anchoredPosition = Vector2.zero;
            contentRect.sizeDelta = new Vector2(0, 0);

            // Add ContentSizeFitter for auto-sizing
            var sizeFitter = contentGo.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            // Add VerticalLayoutGroup for content arrangement
            var layoutGroup = contentGo.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.spacing = UITheme.Spacing;
            layoutGroup.padding = new RectOffset(
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                (int)UITheme.Padding,
                (int)UITheme.Padding
            );

            scrollRect.content = contentRect;

            return scrollRect;
        }

        /// <summary>
        /// Footer 영역 생성 (Bottom anchored).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="height">푸터 높이 (기본값: UITheme.FooterHeight)</param>
        /// <returns>Footer RectTransform</returns>
        public static RectTransform CreateFooter(GameObject parent, float height = UITheme.FooterHeight)
        {
            var go = CreateChild(parent, "Footer");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // Bottom anchored, stretch horizontal
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(0.5f, 0);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(0, height);

            return rect;
        }

        #endregion

        #region Popup Components

        /// <summary>
        /// Backdrop 생성 (Popup용 딤 처리, 클릭 시 닫기 옵션).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="closeOnClick">클릭 시 닫기 기능 활성화 여부</param>
        /// <returns>생성된 Backdrop Image와 CloseButton (closeOnClick이 true일 경우)</returns>
        public static (Image backdrop, Button closeButton) CreateBackdrop(GameObject parent, bool closeOnClick = true)
        {
            var go = CreateChild(parent, "Backdrop");
            SetStretch(go);

            var image = go.AddComponent<Image>();
            image.color = UITheme.Backdrop;
            image.raycastTarget = true;

            Button button = null;
            if (closeOnClick)
            {
                button = go.AddComponent<Button>();
                button.transition = Selectable.Transition.None;
            }

            return (image, button);
        }

        /// <summary>
        /// Container 생성 (Popup 중앙 패널).
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="size">컨테이너 크기</param>
        /// <returns>Container RectTransform</returns>
        public static RectTransform CreateContainer(GameObject parent, Vector2 size)
        {
            var go = CreateChild(parent, "Container");
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            // Center anchored
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = size;

            // Add background
            var image = go.AddComponent<Image>();
            image.color = UITheme.BgCard;
            image.raycastTarget = true;

            return rect;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Child GameObject 생성.
        /// </summary>
        /// <param name="parent">부모 GameObject</param>
        /// <param name="name">자식 GameObject 이름</param>
        /// <returns>생성된 자식 GameObject</returns>
        public static GameObject CreateChild(GameObject parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            return go;
        }

        /// <summary>
        /// RectTransform Stretch 설정.
        /// </summary>
        /// <param name="go">대상 GameObject</param>
        /// <returns>설정된 RectTransform</returns>
        public static RectTransform SetStretch(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>() ?? go.AddComponent<RectTransform>();

            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            return rect;
        }

        #endregion
    }
}