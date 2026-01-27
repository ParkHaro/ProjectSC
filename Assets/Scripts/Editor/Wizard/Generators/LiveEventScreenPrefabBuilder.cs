using Sc.Contents.Event;
using Sc.Contents.Event.Widgets;
using Sc.Editor.AI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// LiveEventScreen 프리팹 빌더.
    /// 스펙: Docs/Specs/LiveEvent.md - UI 레이아웃 구조
    /// 레퍼런스: Docs/Design/Reference/LiveEvent.jpg
    ///
    /// 구조:
    /// - 좌측 패널: EventListWidget (이벤트 배너 목록)
    /// - 우측 패널: EventDetailWidget (선택된 이벤트 상세)
    /// </summary>
    public static class LiveEventScreenPrefabBuilder
    {
        #region Theme Colors (연두색 그라데이션 테마)

        // 연두색 배경 그라데이션
        private static readonly Color BgGreenLight = new Color32(200, 230, 180, 255);
        private static readonly Color BgGreenMid = new Color32(170, 210, 150, 255);
        private static readonly Color BgGreenDeep = new Color32(140, 190, 130, 255);

        // 카드/패널 배경
        private static readonly Color BgCard = new Color32(255, 255, 255, 240);
        private static readonly Color BgCardDark = new Color32(30, 45, 60, 230);
        private static readonly Color BgHeader = new Color32(100, 160, 100, 255);

        // 테두리 색상
        private static readonly Color BorderGreen = new Color32(80, 140, 80, 255);
        private static readonly Color BorderLight = new Color32(220, 240, 200, 255);

        // 텍스트 색상
        private static readonly Color TextPrimary = new Color32(50, 50, 50, 255);
        private static readonly Color TextSecondary = new Color32(100, 100, 100, 255);
        private static readonly Color TextWhite = Color.white;
        private static readonly Color TextMuted = new Color(1f, 1f, 1f, 0.6f);

        // 악센트 색상
        private static readonly Color AccentYellow = new Color32(255, 200, 80, 255);
        private static readonly Color AccentPink = new Color32(255, 150, 180, 255);
        private static readonly Color AccentBlue = new Color32(100, 180, 255, 255);

        // 버튼 색상
        private static readonly Color ButtonPrimary = new Color32(255, 200, 100, 255);
        private static readonly Color ButtonText = new Color32(80, 60, 40, 255);

        // 배너 관련
        private static readonly Color BannerSelected = new Color32(255, 255, 200, 255);
        private static readonly Color BannerNormal = new Color32(255, 255, 255, 230);

        // 뱃지 색상
        private static readonly Color BadgeNew = new Color32(255, 120, 160, 255);
        private static readonly Color BadgeReward = new Color32(255, 80, 80, 255);

        #endregion

        #region Constants

        private const float HEADER_HEIGHT = 80f;
        private const float LEFT_PANEL_WIDTH = 350f;
        private const float BANNER_ITEM_HEIGHT = 110f;
        private const float BANNER_SPACING = 12f;
        private const float CONTENT_PADDING = 20f;
        private const float PERIOD_AREA_HEIGHT = 40f;
        private const float REWARD_PREVIEW_HEIGHT = 80f;
        private const float BUTTON_HEIGHT = 50f;
        private const float REWARD_ICON_SIZE = 50f;

        #endregion

        #region Font Helper

        private static void ApplyFont(TextMeshProUGUI tmp)
        {
            var font = EditorUIHelpers.GetProjectFont();
            if (font != null) tmp.font = font;
        }

        #endregion

        /// <summary>
        /// LiveEventScreen 프리팹용 GameObject 생성.
        /// </summary>
        public static GameObject Build()
        {
            var root = CreateRoot("LiveEventScreen");

            CreateBackground(root);
            var safeArea = CreateSafeArea(root);
            CreateOverlayLayer(root);

            // Add main component
            root.AddComponent<LiveEventScreen>();

            // Connect serialized fields
            ConnectSerializedFields(root);

            return root;
        }

        #region Root

        private static GameObject CreateRoot(string name)
        {
            var go = new GameObject(name);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
            go.AddComponent<CanvasGroup>();
            return go;
        }

        #endregion

        #region Background (연두색 그라데이션)

        private static GameObject CreateBackground(GameObject parent)
        {
            var go = CreateChild(parent, "Background");
            SetStretch(go);

            // 베이스 배경 (연두색)
            var baseBg = CreateChild(go, "BaseBg");
            SetStretch(baseBg);
            var baseBgImage = baseBg.AddComponent<Image>();
            baseBgImage.color = BgGreenLight;
            baseBgImage.raycastTarget = true;

            // 상단 그라데이션 오버레이
            var topOverlay = CreateChild(go, "TopOverlay");
            var topRect = topOverlay.GetComponent<RectTransform>();
            topRect.anchorMin = new Vector2(0, 0.7f);
            topRect.anchorMax = new Vector2(1, 1);
            topRect.offsetMin = Vector2.zero;
            topRect.offsetMax = Vector2.zero;
            var topImage = topOverlay.AddComponent<Image>();
            topImage.color = new Color(BgGreenDeep.r, BgGreenDeep.g, BgGreenDeep.b, 0.3f);
            topImage.raycastTarget = false;

            // 하단 그라데이션 (구름 효과)
            var bottomOverlay = CreateChild(go, "BottomOverlay");
            var bottomRect = bottomOverlay.GetComponent<RectTransform>();
            bottomRect.anchorMin = new Vector2(0, 0);
            bottomRect.anchorMax = new Vector2(1, 0.15f);
            bottomRect.offsetMin = Vector2.zero;
            bottomRect.offsetMax = Vector2.zero;
            var bottomImage = bottomOverlay.AddComponent<Image>();
            bottomImage.color = new Color(1, 1, 1, 0.5f);
            bottomImage.raycastTarget = false;

            // 테두리 프레임
            CreateBorderFrame(go);

            return go;
        }

        private static void CreateBorderFrame(GameObject parent)
        {
            // 상단 테두리
            var topBorder = CreateChild(parent, "TopBorder");
            var topRect = topBorder.GetComponent<RectTransform>();
            topRect.anchorMin = new Vector2(0, 1);
            topRect.anchorMax = new Vector2(1, 1);
            topRect.pivot = new Vector2(0.5f, 1);
            topRect.sizeDelta = new Vector2(0, 8);
            var topImage = topBorder.AddComponent<Image>();
            topImage.color = BorderGreen;
            topImage.raycastTarget = false;

            // 하단 테두리
            var bottomBorder = CreateChild(parent, "BottomBorder");
            var bottomRect = bottomBorder.GetComponent<RectTransform>();
            bottomRect.anchorMin = new Vector2(0, 0);
            bottomRect.anchorMax = new Vector2(1, 0);
            bottomRect.pivot = new Vector2(0.5f, 0);
            bottomRect.sizeDelta = new Vector2(0, 8);
            var bottomImage = bottomBorder.AddComponent<Image>();
            bottomImage.color = BorderGreen;
            bottomImage.raycastTarget = false;

            // 좌측 테두리
            var leftBorder = CreateChild(parent, "LeftBorder");
            var leftRect = leftBorder.GetComponent<RectTransform>();
            leftRect.anchorMin = new Vector2(0, 0);
            leftRect.anchorMax = new Vector2(0, 1);
            leftRect.pivot = new Vector2(0, 0.5f);
            leftRect.sizeDelta = new Vector2(8, 0);
            var leftImage = leftBorder.AddComponent<Image>();
            leftImage.color = BorderGreen;
            leftImage.raycastTarget = false;

            // 우측 테두리
            var rightBorder = CreateChild(parent, "RightBorder");
            var rightRect = rightBorder.GetComponent<RectTransform>();
            rightRect.anchorMin = new Vector2(1, 0);
            rightRect.anchorMax = new Vector2(1, 1);
            rightRect.pivot = new Vector2(1, 0.5f);
            rightRect.sizeDelta = new Vector2(8, 0);
            var rightImage = rightBorder.AddComponent<Image>();
            rightImage.color = BorderGreen;
            rightImage.raycastTarget = false;
        }

        #endregion

        #region SafeArea

        private static GameObject CreateSafeArea(GameObject parent)
        {
            var go = CreateChild(parent, "SafeArea");
            SetStretch(go);

            CreateHeader(go);
            CreateContent(go);

            return go;
        }

        #endregion

        #region Header

        private static GameObject CreateHeader(GameObject parent)
        {
            var go = CreateChild(parent, "Header");
            SetAnchorTop(go, HEADER_HEIGHT);

            // Header placeholder (ScreenHeader가 런타임에 연결됨)
            var placeholder = CreateChild(go, "ScreenHeaderPlaceholder");
            SetStretch(placeholder);
            var placeholderImage = placeholder.AddComponent<Image>();
            placeholderImage.color = new Color(0, 0, 0, 0.3f);
            placeholderImage.raycastTarget = true;

            var placeholderText = CreateChild(placeholder, "Text");
            SetStretch(placeholderText);
            var tmp = placeholderText.AddComponent<TextMeshProUGUI>();
            tmp.text = "[ScreenHeader - 이벤트]";
            tmp.fontSize = 14;
            tmp.color = TextMuted;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return go;
        }

        #endregion

        #region Content

        private static GameObject CreateContent(GameObject parent)
        {
            var go = CreateChild(parent, "Content");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, -HEADER_HEIGHT);

            CreateLeftPanel(go);
            CreateRightPanel(go);

            return go;
        }

        #endregion

        #region LeftPanel (EventListWidget)

        private static GameObject CreateLeftPanel(GameObject parent)
        {
            var go = CreateChild(parent, "LeftPanel");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 0.5f);
            rect.sizeDelta = new Vector2(LEFT_PANEL_WIDTH, 0);
            rect.anchoredPosition = new Vector2(CONTENT_PADDING, 0);
            rect.offsetMin = new Vector2(CONTENT_PADDING, CONTENT_PADDING);
            rect.offsetMax = new Vector2(LEFT_PANEL_WIDTH + CONTENT_PADDING, -CONTENT_PADDING);

            // EventListWidget 추가
            go.AddComponent<EventListWidget>();

            // 배경
            var bg = go.AddComponent<Image>();
            bg.color = new Color(1, 1, 1, 0.1f);
            bg.raycastTarget = false;

            // EventBannerScrollView
            CreateEventBannerScrollView(go);

            // EmptyState
            CreateEmptyState(go);

            // LoadingIndicator
            CreateLoadingIndicator(go);

            // BannerItemPrefab (비활성화 상태로 생성)
            CreateBannerItemPrefab(go);

            return go;
        }

        private static GameObject CreateEventBannerScrollView(GameObject parent)
        {
            var scrollView = CreateChild(parent, "EventBannerScrollView");
            SetStretch(scrollView);

            var scrollRect = scrollView.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
            scrollRect.elasticity = 0.1f;

            // Viewport
            var viewport = CreateChild(scrollView, "Viewport");
            SetStretch(viewport);
            viewport.AddComponent<RectMask2D>();
            var viewportImage = viewport.AddComponent<Image>();
            viewportImage.color = Color.clear;
            viewportImage.raycastTarget = true;
            scrollRect.viewport = viewport.GetComponent<RectTransform>();

            // Content (BannerContainer)
            var content = CreateChild(viewport, "BannerContainer");
            var contentRect = content.GetComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.sizeDelta = new Vector2(0, 0);
            contentRect.anchoredPosition = Vector2.zero;

            var contentLayout = content.AddComponent<VerticalLayoutGroup>();
            contentLayout.spacing = BANNER_SPACING;
            contentLayout.padding = new RectOffset(10, 10, 10, 10);
            contentLayout.childAlignment = TextAnchor.UpperCenter;
            contentLayout.childControlWidth = true;
            contentLayout.childControlHeight = false;
            contentLayout.childForceExpandWidth = true;
            contentLayout.childForceExpandHeight = false;

            var contentFitter = content.AddComponent<ContentSizeFitter>();
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.content = contentRect;

            return scrollView;
        }

        private static GameObject CreateEmptyState(GameObject parent)
        {
            var go = CreateChild(parent, "EmptyState");
            SetStretch(go);
            go.SetActive(false);

            var bg = go.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.1f);
            bg.raycastTarget = false;

            var text = CreateChild(go, "EmptyText");
            SetStretch(text);
            var tmp = text.AddComponent<TextMeshProUGUI>();
            tmp.text = "진행 중인 이벤트가 없습니다.";
            tmp.fontSize = 16;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return go;
        }

        private static GameObject CreateLoadingIndicator(GameObject parent)
        {
            var go = CreateChild(parent, "LoadingIndicator");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(60, 60);
            go.SetActive(false);

            var bg = go.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.5f);

            var spinnerText = CreateChild(go, "SpinnerText");
            SetStretch(spinnerText);
            var tmp = spinnerText.AddComponent<TextMeshProUGUI>();
            tmp.text = "...";
            tmp.fontSize = 24;
            tmp.color = TextWhite;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return go;
        }

        private static GameObject CreateBannerItemPrefab(GameObject parent)
        {
            var go = CreateChild(parent, "EventBannerItemPrefab");
            var rect = go.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, BANNER_ITEM_HEIGHT);
            go.SetActive(false);

            // EventBannerItem 컴포넌트
            go.AddComponent<EventBannerItem>();

            // 배너 배경
            var bg = go.AddComponent<Image>();
            bg.color = BannerNormal;
            bg.raycastTarget = true;

            // 버튼
            var button = go.AddComponent<Button>();
            button.targetGraphic = bg;

            // 레이아웃
            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.padding = new RectOffset(8, 8, 8, 8);
            layout.spacing = 10;
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            // BannerImage
            var bannerImage = CreateChild(go, "BannerImage");
            var bannerImageLayout = bannerImage.AddComponent<LayoutElement>();
            bannerImageLayout.preferredWidth = 150;
            bannerImageLayout.preferredHeight = 90;
            var bannerImg = bannerImage.AddComponent<Image>();
            bannerImg.color = new Color32(100, 150, 200, 255);

            // Info Area
            var infoArea = CreateChild(go, "InfoArea");
            var infoLayout = infoArea.AddComponent<LayoutElement>();
            infoLayout.flexibleWidth = 1;
            var infoVertical = infoArea.AddComponent<VerticalLayoutGroup>();
            infoVertical.spacing = 4;
            infoVertical.childAlignment = TextAnchor.MiddleLeft;
            infoVertical.childControlWidth = true;
            infoVertical.childControlHeight = false;
            infoVertical.childForceExpandWidth = true;
            infoVertical.childForceExpandHeight = false;

            // EventNameLabel
            var nameLabel = CreateChild(infoArea, "NameText");
            var nameLayoutElem = nameLabel.AddComponent<LayoutElement>();
            nameLayoutElem.preferredHeight = 24;
            var nameTmp = nameLabel.AddComponent<TextMeshProUGUI>();
            nameTmp.text = "이벤트 이름";
            nameTmp.fontSize = 16;
            nameTmp.fontStyle = FontStyles.Bold;
            nameTmp.color = TextPrimary;
            nameTmp.alignment = TextAlignmentOptions.Left;
            ApplyFont(nameTmp);

            // RemainingDaysText
            var daysLabel = CreateChild(infoArea, "RemainingDaysText");
            var daysLayoutElem = daysLabel.AddComponent<LayoutElement>();
            daysLayoutElem.preferredHeight = 18;
            var daysTmp = daysLabel.AddComponent<TextMeshProUGUI>();
            daysTmp.text = "D-7";
            daysTmp.fontSize = 12;
            daysTmp.color = TextSecondary;
            daysTmp.alignment = TextAlignmentOptions.Left;
            ApplyFont(daysTmp);

            // GracePeriodIndicator
            var graceIndicator = CreateChild(infoArea, "GracePeriodIndicator");
            var graceLayout = graceIndicator.AddComponent<LayoutElement>();
            graceLayout.preferredHeight = 16;
            graceIndicator.SetActive(false);

            var graceHLayout = graceIndicator.AddComponent<HorizontalLayoutGroup>();
            graceHLayout.spacing = 4;
            graceHLayout.childAlignment = TextAnchor.MiddleLeft;

            var graceText = CreateChild(graceIndicator, "GracePeriodText");
            var graceTmp = graceText.AddComponent<TextMeshProUGUI>();
            graceTmp.text = "이벤트 종료";
            graceTmp.fontSize = 10;
            graceTmp.color = new Color32(200, 100, 100, 255);
            graceTmp.alignment = TextAlignmentOptions.Left;
            ApplyFont(graceTmp);

            // NewBadge
            var newBadge = CreateChild(go, "NewBadge");
            var newBadgeRect = newBadge.GetComponent<RectTransform>();
            newBadgeRect.anchorMin = new Vector2(0, 1);
            newBadgeRect.anchorMax = new Vector2(0, 1);
            newBadgeRect.pivot = new Vector2(0, 1);
            newBadgeRect.sizeDelta = new Vector2(24, 24);
            newBadgeRect.anchoredPosition = new Vector2(4, -4);
            var newBadgeImage = newBadge.AddComponent<Image>();
            newBadgeImage.color = BadgeNew;

            // RewardBadge
            var rewardBadge = CreateChild(go, "RewardBadge");
            var rewardBadgeRect = rewardBadge.GetComponent<RectTransform>();
            rewardBadgeRect.anchorMin = new Vector2(1, 1);
            rewardBadgeRect.anchorMax = new Vector2(1, 1);
            rewardBadgeRect.pivot = new Vector2(1, 1);
            rewardBadgeRect.sizeDelta = new Vector2(40, 20);
            rewardBadgeRect.anchoredPosition = new Vector2(-4, -4);
            var rewardBadgeImage = rewardBadge.AddComponent<Image>();
            rewardBadgeImage.color = BadgeReward;
            rewardBadge.SetActive(false);

            var rewardBadgeText = CreateChild(rewardBadge, "Text");
            SetStretch(rewardBadgeText);
            var rewardBadgeTmp = rewardBadgeText.AddComponent<TextMeshProUGUI>();
            rewardBadgeTmp.text = "보상!";
            rewardBadgeTmp.fontSize = 10;
            rewardBadgeTmp.color = TextWhite;
            rewardBadgeTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(rewardBadgeTmp);

            // Selection Highlight (테두리)
            var selectionHighlight = CreateChild(go, "SelectionHighlight");
            SetStretch(selectionHighlight);
            selectionHighlight.SetActive(false);
            var highlightImage = selectionHighlight.AddComponent<Image>();
            highlightImage.color = new Color(AccentYellow.r, AccentYellow.g, AccentYellow.b, 0.5f);
            highlightImage.raycastTarget = false;

            return go;
        }

        #endregion

        #region RightPanel (EventDetailWidget)

        private static GameObject CreateRightPanel(GameObject parent)
        {
            var go = CreateChild(parent, "RightPanel");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(LEFT_PANEL_WIDTH + CONTENT_PADDING * 2, CONTENT_PADDING);
            rect.offsetMax = new Vector2(-CONTENT_PADDING, -CONTENT_PADDING);

            // EventDetailWidget 추가
            go.AddComponent<EventDetailWidget>();

            // EventDetailContainer
            CreateEventDetailContainer(go);

            return go;
        }

        private static GameObject CreateEventDetailContainer(GameObject parent)
        {
            var container = CreateChild(parent, "EventDetailContainer");
            SetStretch(container);

            // 배경 (카드 스타일)
            var bg = container.AddComponent<Image>();
            bg.color = BgCard;
            bg.raycastTarget = false;

            // TitleBannerArea (상단)
            CreateTitleBannerArea(container);

            // CharacterArea (중앙)
            CreateCharacterArea(container);

            // PeriodArea (하단 중앙)
            CreatePeriodArea(container);

            // RewardPreviewArea (하단 좌측)
            CreateRewardPreviewArea(container);

            // ButtonArea (하단 우측)
            CreateButtonArea(container);

            return container;
        }

        private static GameObject CreateTitleBannerArea(GameObject parent)
        {
            var go = CreateChild(parent, "TitleBannerArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.75f);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(20, 0);
            rect.offsetMax = new Vector2(-20, -20);

            // TitleBannerImage
            var bannerImage = CreateChild(go, "TitleBannerImage");
            SetStretch(bannerImage);
            var img = bannerImage.AddComponent<Image>();
            img.color = new Color32(200, 220, 180, 255);
            img.raycastTarget = false;

            // 플레이스홀더 텍스트
            var placeholder = CreateChild(bannerImage, "Placeholder");
            SetStretch(placeholder);
            var tmp = placeholder.AddComponent<TextMeshProUGUI>();
            tmp.text = "이벤트 타이틀 배너";
            tmp.fontSize = 32;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = TextPrimary;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            // DecoElements
            var decoElements = CreateChild(go, "DecoElements");
            SetStretch(decoElements);

            return go;
        }

        private static GameObject CreateCharacterArea(GameObject parent)
        {
            var go = CreateChild(parent, "CharacterArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.25f);
            rect.anchorMax = new Vector2(1, 0.75f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = new Vector2(-20, 0);

            // CharacterIllust
            var characterIllust = CreateChild(go, "CharacterIllustImage");
            SetStretch(characterIllust);
            var img = characterIllust.AddComponent<Image>();
            img.color = new Color32(180, 200, 160, 255);
            img.raycastTarget = false;
            img.preserveAspect = true;

            // 플레이스홀더 텍스트
            var placeholder = CreateChild(characterIllust, "Placeholder");
            SetStretch(placeholder);
            var tmp = placeholder.AddComponent<TextMeshProUGUI>();
            tmp.text = "캐릭터\n일러스트";
            tmp.fontSize = 24;
            tmp.color = TextSecondary;
            tmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(tmp);

            return go;
        }

        private static GameObject CreatePeriodArea(GameObject parent)
        {
            var go = CreateChild(parent, "PeriodArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.15f);
            rect.anchorMax = new Vector2(1, 0.25f);
            rect.offsetMin = new Vector2(20, 0);
            rect.offsetMax = new Vector2(-20, 0);

            var bg = go.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.3f);
            bg.raycastTarget = false;

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(20, 20, 5, 5);
            layout.spacing = 2;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // StartTimeLabel
            var startLabel = CreateChild(go, "StartTimeLabel");
            var startLayoutElem = startLabel.AddComponent<LayoutElement>();
            startLayoutElem.preferredHeight = 18;
            var startTmp = startLabel.AddComponent<TextMeshProUGUI>();
            startTmp.text = "이벤트 시작: 2026-01-15 11:00(UTC +9)";
            startTmp.fontSize = 12;
            startTmp.color = TextWhite;
            startTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(startTmp);

            // EndTimeLabel
            var endLabel = CreateChild(go, "EndTimeLabel");
            var endLayoutElem = endLabel.AddComponent<LayoutElement>();
            endLayoutElem.preferredHeight = 18;
            var endTmp = endLabel.AddComponent<TextMeshProUGUI>();
            endTmp.text = "이벤트 종료: 2026-01-29 10:59(UTC +9)";
            endTmp.fontSize = 12;
            endTmp.color = TextWhite;
            endTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(endTmp);

            return go;
        }

        private static GameObject CreateRewardPreviewArea(GameObject parent)
        {
            var go = CreateChild(parent, "RewardPreviewArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0.6f, 0.15f);
            rect.offsetMin = new Vector2(20, 15);
            rect.offsetMax = new Vector2(0, 0);

            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 8;
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            // RewardPreviewTitle
            var titleLabel = CreateChild(go, "RewardPreviewTitle");
            var titleLayoutElem = titleLabel.AddComponent<LayoutElement>();
            titleLayoutElem.preferredHeight = 20;
            var titleTmp = titleLabel.AddComponent<TextMeshProUGUI>();
            titleTmp.text = "참여 시 획득 가능한 보상";
            titleTmp.fontSize = 12;
            titleTmp.color = TextSecondary;
            titleTmp.alignment = TextAlignmentOptions.Left;
            ApplyFont(titleTmp);

            // RewardIconContainer
            var iconContainer = CreateChild(go, "RewardIconContainer");
            var iconContainerLayout = iconContainer.AddComponent<LayoutElement>();
            iconContainerLayout.preferredHeight = REWARD_ICON_SIZE;

            var iconHLayout = iconContainer.AddComponent<HorizontalLayoutGroup>();
            iconHLayout.spacing = 8;
            iconHLayout.childAlignment = TextAnchor.MiddleLeft;
            iconHLayout.childControlWidth = false;
            iconHLayout.childControlHeight = false;
            iconHLayout.childForceExpandWidth = false;
            iconHLayout.childForceExpandHeight = false;

            // RewardIconPrefab (플레이스홀더 아이콘)
            var iconPrefab = CreateChild(iconContainer, "RewardIconPrefab");
            var iconPrefabLayout = iconPrefab.AddComponent<LayoutElement>();
            iconPrefabLayout.preferredWidth = REWARD_ICON_SIZE;
            iconPrefabLayout.preferredHeight = REWARD_ICON_SIZE;
            var iconPrefabImage = iconPrefab.AddComponent<Image>();
            iconPrefabImage.color = AccentBlue;
            iconPrefab.SetActive(false);

            // 샘플 보상 아이콘 1개 표시
            var sampleIcon = CreateChild(iconContainer, "SampleRewardIcon");
            var sampleIconLayout = sampleIcon.AddComponent<LayoutElement>();
            sampleIconLayout.preferredWidth = REWARD_ICON_SIZE;
            sampleIconLayout.preferredHeight = REWARD_ICON_SIZE;
            var sampleIconImage = sampleIcon.AddComponent<Image>();
            sampleIconImage.color = new Color32(150, 220, 150, 255);

            return go;
        }

        private static GameObject CreateButtonArea(GameObject parent)
        {
            var go = CreateChild(parent, "ButtonArea");
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.6f, 0);
            rect.anchorMax = new Vector2(1, 0.15f);
            rect.offsetMin = new Vector2(20, 15);
            rect.offsetMax = new Vector2(-20, 0);

            var layout = go.AddComponent<HorizontalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;

            // EnterButton (바로 가기)
            var enterButton = CreateChild(go, "EnterButton");
            var btnLayout = enterButton.AddComponent<LayoutElement>();
            btnLayout.preferredWidth = 120;
            btnLayout.preferredHeight = BUTTON_HEIGHT;

            var btnBg = enterButton.AddComponent<Image>();
            btnBg.color = ButtonPrimary;

            var btn = enterButton.AddComponent<Button>();
            btn.targetGraphic = btnBg;

            // 버튼 라벨
            var btnLabel = CreateChild(enterButton, "EnterButtonLabel");
            SetStretch(btnLabel);
            var btnTmp = btnLabel.AddComponent<TextMeshProUGUI>();
            btnTmp.text = "바로 가기";
            btnTmp.fontSize = 16;
            btnTmp.fontStyle = FontStyles.Bold;
            btnTmp.color = ButtonText;
            btnTmp.alignment = TextAlignmentOptions.Center;
            ApplyFont(btnTmp);

            return go;
        }

        #endregion

        #region Overlay Layer

        private static GameObject CreateOverlayLayer(GameObject parent)
        {
            var go = CreateChild(parent, "OverlayLayer");
            SetStretch(go);
            return go;
        }

        #endregion

        #region Helpers

        private static GameObject CreateChild(GameObject parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            go.AddComponent<RectTransform>();
            return go;
        }

        private static void SetStretch(GameObject go)
        {
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
        }

        private static void SetAnchorTop(GameObject go, float height)
        {
            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.sizeDelta = new Vector2(0, height);
            rect.anchoredPosition = Vector2.zero;
        }

        #endregion

        #region Connect Serialized Fields

        private static void ConnectSerializedFields(GameObject root)
        {
            var screen = root.GetComponent<LiveEventScreen>();
            if (screen == null) return;

            var so = new SerializedObject(screen);

            // Widgets
            ConnectField(so, "_eventListWidget", root, "SafeArea/Content/LeftPanel");
            ConnectField(so, "_eventDetailWidget", root, "SafeArea/Content/RightPanel");

            // Legacy UI References
            ConnectField(so, "_eventListContainer", root,
                "SafeArea/Content/LeftPanel/EventBannerScrollView/Viewport/BannerContainer");
            ConnectField(so, "_bannerItemPrefab", root, "SafeArea/Content/LeftPanel/EventBannerItemPrefab");
            ConnectField(so, "_emptyText", root, "SafeArea/Content/LeftPanel/EmptyState/EmptyText");
            ConnectField(so, "_loadingIndicator", root, "SafeArea/Content/LeftPanel/LoadingIndicator");

            so.ApplyModifiedPropertiesWithoutUndo();

            // EventListWidget 내부 필드 연결
            ConnectEventListWidgetFields(root);

            // EventDetailWidget 내부 필드 연결
            ConnectEventDetailWidgetFields(root);
        }

        private static void ConnectEventListWidgetFields(GameObject root)
        {
            var leftPanel = root.transform.Find("SafeArea/Content/LeftPanel");
            if (leftPanel == null) return;

            var listWidget = leftPanel.GetComponent<EventListWidget>();
            if (listWidget == null) return;

            var so = new SerializedObject(listWidget);

            ConnectFieldFromTransform(so, "_scrollRect", leftPanel, "EventBannerScrollView");
            ConnectFieldFromTransform(so, "_bannerContainer", leftPanel,
                "EventBannerScrollView/Viewport/BannerContainer");
            ConnectFieldFromTransform(so, "_bannerItemPrefab", leftPanel, "EventBannerItemPrefab");
            ConnectFieldFromTransform(so, "_emptyState", leftPanel, "EmptyState");
            ConnectFieldFromTransform(so, "_emptyText", leftPanel, "EmptyState/EmptyText");
            ConnectFieldFromTransform(so, "_loadingIndicator", leftPanel, "LoadingIndicator");

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectEventDetailWidgetFields(GameObject root)
        {
            var rightPanel = root.transform.Find("SafeArea/Content/RightPanel");
            if (rightPanel == null) return;

            var detailWidget = rightPanel.GetComponent<EventDetailWidget>();
            if (detailWidget == null) return;

            var so = new SerializedObject(detailWidget);

            ConnectFieldFromTransform(so, "_titleBannerImage", rightPanel,
                "EventDetailContainer/TitleBannerArea/TitleBannerImage");
            ConnectFieldFromTransform(so, "_characterIllustImage", rightPanel,
                "EventDetailContainer/CharacterArea/CharacterIllustImage");
            ConnectFieldFromTransform(so, "_startTimeLabel", rightPanel,
                "EventDetailContainer/PeriodArea/StartTimeLabel");
            ConnectFieldFromTransform(so, "_endTimeLabel", rightPanel, "EventDetailContainer/PeriodArea/EndTimeLabel");
            ConnectFieldFromTransform(so, "_rewardPreviewTitle", rightPanel,
                "EventDetailContainer/RewardPreviewArea/RewardPreviewTitle");
            ConnectFieldFromTransform(so, "_rewardIconContainer", rightPanel,
                "EventDetailContainer/RewardPreviewArea/RewardIconContainer");
            ConnectFieldFromTransform(so, "_rewardIconPrefab", rightPanel,
                "EventDetailContainer/RewardPreviewArea/RewardIconContainer/RewardIconPrefab");
            ConnectFieldFromTransform(so, "_enterButton", rightPanel, "EventDetailContainer/ButtonArea/EnterButton");
            ConnectFieldFromTransform(so, "_enterButtonLabel", rightPanel,
                "EventDetailContainer/ButtonArea/EnterButton/EnterButtonLabel");

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ConnectField(SerializedObject so, string fieldName, GameObject root, string path)
        {
            var prop = so.FindProperty(fieldName);
            if (prop == null) return;

            var target = root.transform.Find(path);
            if (target == null) return;

            SetPropertyValue(prop, target);
        }

        private static void ConnectFieldFromTransform(SerializedObject so, string fieldName, Transform parent,
            string path)
        {
            var prop = so.FindProperty(fieldName);
            if (prop == null) return;

            var target = parent.Find(path);
            if (target == null) return;

            SetPropertyValue(prop, target);
        }

        private static void SetPropertyValue(SerializedProperty prop, Transform target)
        {
            if (prop.propertyType != SerializedPropertyType.ObjectReference) return;

            var fieldType = prop.type;

            if (fieldType.Contains("Button"))
                prop.objectReferenceValue = target.GetComponent<Button>();
            else if (fieldType.Contains("TMP_Text") || fieldType.Contains("TextMeshProUGUI"))
                prop.objectReferenceValue = target.GetComponent<TextMeshProUGUI>();
            else if (fieldType.Contains("Image"))
                prop.objectReferenceValue = target.GetComponent<Image>();
            else if (fieldType.Contains("ScrollRect"))
                prop.objectReferenceValue = target.GetComponent<ScrollRect>();
            else if (fieldType.Contains("EventListWidget"))
                prop.objectReferenceValue = target.GetComponent<EventListWidget>();
            else if (fieldType.Contains("EventDetailWidget"))
                prop.objectReferenceValue = target.GetComponent<EventDetailWidget>();
            else if (fieldType.Contains("EventBannerItem"))
                prop.objectReferenceValue = target.GetComponent<EventBannerItem>();
            else if (fieldType.Contains("Transform"))
                prop.objectReferenceValue = target;
            else if (fieldType.Contains("GameObject"))
                prop.objectReferenceValue = target.gameObject;
            else
                prop.objectReferenceValue = target.gameObject;
        }

        #endregion
    }
}