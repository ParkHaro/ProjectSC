using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Widgets;
using Sc.Data;
using Sc.Event.UI;
using Sc.Foundation;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 인게임 컨텐츠 대시보드.
    /// 컨텐츠 종류(메인스토리, 골드던전, 경험치던전 등)를 선택하는 화면입니다.
    /// </summary>
    public class InGameContentDashboard : ScreenWidget<InGameContentDashboard, InGameContentDashboard.DashboardState>
    {
        /// <summary>
        /// 대시보드 상태
        /// </summary>
        public class DashboardState : IScreenState
        {
            /// <summary>
            /// 초기 선택 컨텐츠 타입 (하이라이트용)
            /// </summary>
            public InGameContentType? InitialContentType { get; set; }
        }

        [Header("Content List")] [SerializeField]
        private Transform _contentContainer;

        [SerializeField] private ContentCategoryItem _categoryItemPrefab;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Navigation")] [SerializeField]
        private Button _backButton;

        private DashboardState _currentState;
        private readonly List<ContentCategoryItem> _categoryItems = new();

        // 표시할 컨텐츠 목록 (순서대로)
        private static readonly InGameContentType[] DisplayContents =
        {
            InGameContentType.MainStory,
            InGameContentType.HardMode,
            InGameContentType.GoldDungeon,
            InGameContentType.ExpDungeon,
            InGameContentType.SkillDungeon,
            InGameContentType.BossRaid,
            InGameContentType.Tower
        };

        protected override void OnInitialize()
        {
            Debug.Log("[InGameContentDashboard] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }
        }

        protected override void OnBind(DashboardState state)
        {
            _currentState = state ?? new DashboardState();

            Debug.Log("[InGameContentDashboard] OnBind");

            // Header 설정
            ScreenHeader.Instance?.Configure("stage_dashboard");

            RefreshContentList();
        }

        protected override void OnShow()
        {
            Debug.Log("[InGameContentDashboard] OnShow");

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        protected override void OnHide()
        {
            Debug.Log("[InGameContentDashboard] OnHide");

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override DashboardState GetState() => _currentState;

        #region Content List

        private void RefreshContentList()
        {
            ClearCategoryItems();

            foreach (var contentType in DisplayContents)
            {
                CreateCategoryItem(contentType);
            }

            // 스크롤 초기화
            if (_scrollRect != null)
            {
                _scrollRect.verticalNormalizedPosition = 1f;
            }
        }

        private void CreateCategoryItem(InGameContentType contentType)
        {
            if (_categoryItemPrefab == null || _contentContainer == null) return;

            var itemGo = Instantiate(_categoryItemPrefab, _contentContainer);
            var item = itemGo.GetComponent<ContentCategoryItem>();

            if (item != null)
            {
                item.Initialize();

                // TODO: 실제 잠금 상태는 UserData에서 확인
                bool isLocked = IsContentLocked(contentType);
                bool hasNew = false; // TODO: 새 컨텐츠 확인 로직

                item.Setup(contentType, isLocked, hasNew, OnContentClicked);
                _categoryItems.Add(item);
            }
        }

        private void ClearCategoryItems()
        {
            foreach (var item in _categoryItems)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }

            _categoryItems.Clear();
        }

        private bool IsContentLocked(InGameContentType contentType)
        {
            // TODO: 실제 해금 조건 확인
            // 현재는 메인스토리만 해금된 것으로 처리
            return contentType switch
            {
                InGameContentType.MainStory => false,
                InGameContentType.GoldDungeon => false,
                InGameContentType.ExpDungeon => false,
                _ => true
            };
        }

        #endregion

        #region Content Selection

        private void OnContentClicked(InGameContentType contentType)
        {
            Debug.Log($"[InGameContentDashboard] Content clicked: {contentType}");

            // 컨텐츠에 따라 다음 화면 결정
            switch (contentType)
            {
                // StageDashboard 스킵하고 바로 StageSelectScreen으로
                case InGameContentType.MainStory:
                case InGameContentType.Tower:
                    OpenStageSelectScreen(contentType);
                    break;

                // StageDashboard 거쳐서 세부 선택
                case InGameContentType.GoldDungeon:
                case InGameContentType.ExpDungeon:
                case InGameContentType.SkillDungeon:
                case InGameContentType.BossRaid:
                    OpenStageDashboard(contentType);
                    break;

                default:
                    Debug.LogWarning($"[InGameContentDashboard] Unhandled content type: {contentType}");
                    break;
            }
        }

        private void OpenStageSelectScreen(InGameContentType contentType)
        {
            StageSelectScreen.Open(new StageSelectScreen.StageSelectState
            {
                ContentType = contentType
            });
        }

        private void OpenStageDashboard(InGameContentType contentType)
        {
            StageDashboard.Open(new StageDashboard.StageDashboardState
            {
                ContentType = contentType
            });
        }

        #endregion

        #region Navigation

        private void OnBackClicked()
        {
            Debug.Log("[InGameContentDashboard] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion

        protected override void OnRelease()
        {
            if (_backButton != null)
            {
                _backButton.onClick.RemoveListener(OnBackClicked);
            }

            ClearCategoryItems();
        }
    }
}