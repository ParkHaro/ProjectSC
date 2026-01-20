using System;
using System.Collections.Generic;
using System.Linq;
using Sc.Common.UI;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 스테이지 대시보드.
    /// 컨텐츠 내 세부 분류(속성, 난이도, 보스 등)를 선택하는 화면입니다.
    /// </summary>
    public class StageDashboard : ScreenWidget<StageDashboard, StageDashboard.StageDashboardState>
    {
        /// <summary>
        /// 대시보드 상태
        /// </summary>
        public class StageDashboardState : IScreenState
        {
            /// <summary>
            /// 컨텐츠 타입
            /// </summary>
            public InGameContentType ContentType { get; set; }

            /// <summary>
            /// 초기 선택 카테고리 ID
            /// </summary>
            public string InitialCategoryId { get; set; }
        }

        [Header("Header")]
        [SerializeField] private TMP_Text _titleText;

        [Header("Category List")]
        [SerializeField] private Transform _categoryContainer;
        [SerializeField] private ContentCategoryItem _categoryItemPrefab;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Navigation")]
        [SerializeField] private Button _backButton;

        private StageDashboardState _currentState;
        private readonly List<ContentCategoryItem> _categoryItems = new();

        protected override void OnInitialize()
        {
            Debug.Log("[StageDashboard] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }
        }

        protected override void OnBind(StageDashboardState state)
        {
            _currentState = state ?? new StageDashboardState();

            Debug.Log($"[StageDashboard] OnBind - ContentType: {_currentState.ContentType}");

            // Header 설정
            ScreenHeader.Instance?.Configure("stage_dashboard");

            // 타이틀 설정
            if (_titleText != null)
            {
                _titleText.text = GetContentTitle(_currentState.ContentType);
            }

            RefreshCategoryList();
        }

        protected override void OnShow()
        {
            Debug.Log("[StageDashboard] OnShow");

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        protected override void OnHide()
        {
            Debug.Log("[StageDashboard] OnHide");

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override StageDashboardState GetState() => _currentState;

        #region Category List

        private void RefreshCategoryList()
        {
            ClearCategoryItems();

            var categories = GetCategoriesForContent(_currentState.ContentType);

            foreach (var category in categories)
            {
                CreateCategoryItem(category);
            }

            // 스크롤 초기화
            if (_scrollRect != null)
            {
                _scrollRect.verticalNormalizedPosition = 1f;
            }
        }

        private void CreateCategoryItem(StageCategoryData category)
        {
            if (_categoryItemPrefab == null || _categoryContainer == null || category == null) return;

            var itemGo = Instantiate(_categoryItemPrefab, _categoryContainer);
            var item = itemGo.GetComponent<ContentCategoryItem>();

            if (item != null)
            {
                item.Initialize();

                // 잠금 여부 확인 (유저 진행도 기반)
                bool isLocked = IsCategoryLocked(category);

                item.Setup(
                    _currentState.ContentType,
                    isLocked,
                    false,
                    _ => OnCategoryClicked(category)
                );

                _categoryItems.Add(item);
            }
        }

        private bool IsCategoryLocked(StageCategoryData category)
        {
            if (category == null) return true;

            // UnlockCondition 체크 (TODO: 유저 진행도 기반 구현)
            // 현재는 ChapterNumber 기반 간단 체크
            if (DataManager.Instance == null) return false;

            var progress = DataManager.Instance.StageProgress;

            // MainStory: 이전 챕터 클리어 필요
            if (category.ContentType == InGameContentType.MainStory)
            {
                return category.ChapterNumber > progress.CurrentChapter + 1;
            }

            return false;
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

        private List<StageCategoryData> GetCategoriesForContent(InGameContentType contentType)
        {
            // StageCategoryDatabase에서 조회
            var database = DataManager.Instance?.GetDatabase<StageCategoryDatabase>();
            if (database == null)
            {
                Debug.LogWarning("[StageDashboard] StageCategoryDatabase not found");
                return new List<StageCategoryData>();
            }

            return database.GetSortedByContentType(contentType);
        }

        #endregion

        #region Category Selection

        private void OnCategoryClicked(StageCategoryData category)
        {
            if (category == null) return;

            if (IsCategoryLocked(category))
            {
                Debug.Log($"[StageDashboard] Category locked: {category.Id}");
                // TODO: 잠금 안내 팝업 표시
                return;
            }

            Debug.Log($"[StageDashboard] Category clicked: {category.Id}");

            // StageSelectScreen으로 이동
            StageSelectScreen.Open(new StageSelectScreen.StageSelectState
            {
                ContentType = _currentState.ContentType,
                CategoryId = category.Id
            });
        }

        #endregion

        #region Navigation

        private void OnBackClicked()
        {
            Debug.Log("[StageDashboard] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion

        #region Helpers

        private string GetContentTitle(InGameContentType contentType)
        {
            return contentType switch
            {
                InGameContentType.GoldDungeon => "골드 던전",
                InGameContentType.ExpDungeon => "경험치 던전",
                InGameContentType.SkillDungeon => "스킬 던전",
                InGameContentType.BossRaid => "보스 레이드",
                _ => "던전 선택"
            };
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
