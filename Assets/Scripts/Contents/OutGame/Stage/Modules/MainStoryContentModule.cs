using System.Collections.Generic;
using System.Linq;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Foundation;
using TMPro;
using UnityEngine;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 메인 스토리 컨텐츠 모듈.
    /// 챕터 탭, 스토리 진행도 표시.
    /// </summary>
    public class MainStoryContentModule : BaseStageContentModule
    {
        private TabGroupWidget _chapterTabs;
        private TMP_Text _progressText;
        private List<StageCategoryData> _chapters;
        private int _currentChapterIndex;

        protected override string GetPrefabAddress()
        {
            // UI 프리팹이 준비되면 아래 주석 해제
            // return "UI/Stage/MainStoryModuleUI";
            return null;
        }

        protected override void OnInitialize()
        {
            Log.Debug("[MainStoryContentModule] OnInitialize", LogCategory.UI);

            // UI 컴포넌트 찾기 (프리팹 있을 경우)
            if (_rootInstance != null)
            {
                _chapterTabs = FindComponent<TabGroupWidget>();
                _progressText = FindComponent<TMP_Text>();
            }

            // 챕터 데이터 로드
            LoadChapterData();

            // 챕터 탭 설정
            SetupChapterTabs();

            // 현재 챕터 결정
            DetermineCurrentChapter();
        }

        protected override void OnRefreshInternal(string selectedStageId)
        {
            UpdateProgressText();
        }

        protected override void OnStageSelectedInternal(StageData stageData)
        {
            // 선택된 스테이지의 챕터로 탭 업데이트 (필요 시)
            if (stageData != null && _chapterTabs != null)
            {
                var chapterIndex = FindChapterIndex(stageData.Chapter);
                if (chapterIndex >= 0 && chapterIndex != _currentChapterIndex)
                {
                    _chapterTabs.SelectTab(chapterIndex);
                }
            }
        }

        protected override void OnCategoryIdChanged(string categoryId)
        {
            // 외부에서 카테고리(챕터) 변경 시
            if (!string.IsNullOrEmpty(categoryId))
            {
                var chapterIndex = FindChapterIndexById(categoryId);
                if (chapterIndex >= 0)
                {
                    SelectChapter(chapterIndex, notify: false);
                }
            }
        }

        protected override void OnReleaseInternal()
        {
            if (_chapterTabs != null)
            {
                _chapterTabs.OnTabChanged -= OnChapterTabChanged;
            }

            _chapters = null;
        }

        #region Private Methods

        private void LoadChapterData()
        {
            var categoryDb = DataManager.Instance?.GetDatabase<StageCategoryDatabase>();
            if (categoryDb == null)
            {
                Log.Warning("[MainStoryContentModule] StageCategoryDatabase not found", LogCategory.UI);
                _chapters = new List<StageCategoryData>();
                return;
            }

            _chapters = categoryDb.GetSortedByContentType(InGameContentType.MainStory);
            Log.Debug($"[MainStoryContentModule] Loaded {_chapters.Count} chapters", LogCategory.UI);
        }

        private void SetupChapterTabs()
        {
            if (_chapterTabs == null || _chapters == null || _chapters.Count == 0)
            {
                return;
            }

            var labels = _chapters.Select(c => GetChapterLabel(c)).ToList();
            _chapterTabs.SetupTabButtons(labels);
            _chapterTabs.OnTabChanged += OnChapterTabChanged;
        }

        private void DetermineCurrentChapter()
        {
            if (_chapters == null || _chapters.Count == 0)
            {
                _currentChapterIndex = 0;
                return;
            }

            // 초기 카테고리 ID가 설정되어 있으면 사용
            if (!string.IsNullOrEmpty(_categoryId))
            {
                var index = FindChapterIndexById(_categoryId);
                if (index >= 0)
                {
                    SelectChapter(index, notify: false);
                    return;
                }
            }

            // 유저 진행도 기반으로 현재 챕터 결정
            if (DataManager.Instance != null)
            {
                var progress = DataManager.Instance.StageProgress;
                var currentChapter = progress.CurrentChapter;
                var index = FindChapterIndex(currentChapter);
                if (index >= 0)
                {
                    SelectChapter(index, notify: true);
                    return;
                }
            }

            // 기본: 첫 번째 챕터
            SelectChapter(0, notify: true);
        }

        private void SelectChapter(int index, bool notify)
        {
            if (index < 0 || _chapters == null || index >= _chapters.Count)
            {
                return;
            }

            _currentChapterIndex = index;
            _chapterTabs?.SelectTab(index);

            if (notify)
            {
                var categoryId = _chapters[index].Id;
                NotifyCategoryChanged(categoryId);
            }
        }

        private void OnChapterTabChanged(int tabIndex)
        {
            if (tabIndex == _currentChapterIndex) return;

            _currentChapterIndex = tabIndex;

            if (_chapters != null && tabIndex < _chapters.Count)
            {
                var categoryId = _chapters[tabIndex].Id;
                NotifyCategoryChanged(categoryId);
                Log.Debug($"[MainStoryContentModule] Chapter changed: {categoryId}", LogCategory.UI);
            }
        }

        private void UpdateProgressText()
        {
            if (_progressText == null) return;

            if (DataManager.Instance == null || _chapters == null || _currentChapterIndex >= _chapters.Count)
            {
                _progressText.text = "";
                return;
            }

            var stageDb = DataManager.Instance.GetDatabase<StageDatabase>();
            if (stageDb == null)
            {
                _progressText.text = "";
                return;
            }

            var progress = DataManager.Instance.StageProgress;
            var currentChapter = _chapters[_currentChapterIndex];
            var stages = stageDb.GetByContentTypeAndCategory(
                InGameContentType.MainStory,
                currentChapter.Id).ToList();

            var clearedCount = stages.Count(s => progress.IsStageCleared(s.Id));
            var totalCount = stages.Count;

            _progressText.text = $"{clearedCount}/{totalCount}";
        }

        private string GetChapterLabel(StageCategoryData category)
        {
            if (category == null) return "";

            // NameKey가 있으면 사용, 없으면 챕터 번호
            if (!string.IsNullOrEmpty(category.NameKey))
            {
                return category.GetDisplayName();
            }

            return $"{category.ChapterNumber}장";
        }

        private int FindChapterIndex(int chapterNumber)
        {
            if (_chapters == null) return -1;

            for (int i = 0; i < _chapters.Count; i++)
            {
                if (_chapters[i].ChapterNumber == chapterNumber)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindChapterIndexById(string categoryId)
        {
            if (_chapters == null || string.IsNullOrEmpty(categoryId)) return -1;

            for (int i = 0; i < _chapters.Count; i++)
            {
                if (_chapters[i].Id == categoryId)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion
    }
}
