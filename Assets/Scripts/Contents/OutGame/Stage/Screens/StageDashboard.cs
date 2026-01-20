using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Widgets;
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
            /// 초기 선택 던전 ID
            /// </summary>
            public string InitialDungeonId { get; set; }
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

        private void CreateCategoryItem(DungeonCategoryInfo category)
        {
            if (_categoryItemPrefab == null || _categoryContainer == null) return;

            var itemGo = Instantiate(_categoryItemPrefab, _categoryContainer);
            var item = itemGo.GetComponent<ContentCategoryItem>();

            if (item != null)
            {
                item.Initialize();

                // ContentCategoryItem을 재사용하므로 InGameContentType으로 캐스팅
                // 실제로는 DungeonId를 사용해야 하지만, 현재 구조상 임시 처리
                item.Setup(
                    _currentState.ContentType,
                    category.IsLocked,
                    false,
                    _ => OnCategoryClicked(category)
                );

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

        private List<DungeonCategoryInfo> GetCategoriesForContent(InGameContentType contentType)
        {
            // TODO: 실제 DungeonDatabase에서 조회
            // 현재는 하드코딩된 목록 반환

            return contentType switch
            {
                InGameContentType.GoldDungeon => new List<DungeonCategoryInfo>
                {
                    new() { Id = "gold_fire", Name = "불 속성", Description = "불 속성 몬스터 출현", IsLocked = false },
                    new() { Id = "gold_water", Name = "물 속성", Description = "물 속성 몬스터 출현", IsLocked = false },
                    new() { Id = "gold_earth", Name = "땅 속성", Description = "땅 속성 몬스터 출현", IsLocked = true },
                    new() { Id = "gold_wind", Name = "바람 속성", Description = "바람 속성 몬스터 출현", IsLocked = true },
                },
                InGameContentType.ExpDungeon => new List<DungeonCategoryInfo>
                {
                    new() { Id = "exp_easy", Name = "초급", Description = "쉬운 난이도", IsLocked = false },
                    new() { Id = "exp_normal", Name = "중급", Description = "보통 난이도", IsLocked = false },
                    new() { Id = "exp_hard", Name = "고급", Description = "어려운 난이도", IsLocked = true },
                },
                InGameContentType.SkillDungeon => new List<DungeonCategoryInfo>
                {
                    new() { Id = "skill_attack", Name = "공격", Description = "공격 스킬 재료", IsLocked = false },
                    new() { Id = "skill_defense", Name = "방어", Description = "방어 스킬 재료", IsLocked = true },
                    new() { Id = "skill_support", Name = "지원", Description = "지원 스킬 재료", IsLocked = true },
                },
                InGameContentType.BossRaid => new List<DungeonCategoryInfo>
                {
                    new() { Id = "boss_dragon", Name = "드래곤", Description = "드래곤 보스", IsLocked = false },
                    new() { Id = "boss_demon", Name = "악마", Description = "악마 보스", IsLocked = true },
                },
                _ => new List<DungeonCategoryInfo>()
            };
        }

        #endregion

        #region Category Selection

        private void OnCategoryClicked(DungeonCategoryInfo category)
        {
            if (category.IsLocked)
            {
                Debug.Log($"[StageDashboard] Category locked: {category.Id}");
                return;
            }

            Debug.Log($"[StageDashboard] Category clicked: {category.Id}");

            // StageSelectScreen으로 이동
            StageSelectScreen.Open(new StageSelectScreen.StageSelectState
            {
                ContentType = _currentState.ContentType,
                DungeonId = category.Id
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

    /// <summary>
    /// 던전 카테고리 정보 (임시 구조체).
    /// DungeonData 구현 전까지 사용합니다.
    /// </summary>
    internal struct DungeonCategoryInfo
    {
        public string Id;
        public string Name;
        public string Description;
        public bool IsLocked;
    }
}
