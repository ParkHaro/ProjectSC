using System;
using System.Collections.Generic;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 챕터 선택 위젯.
    /// 이전/다음 월드(챕터) 이동 버튼과 현재 챕터 표시.
    /// </summary>
    public class ChapterSelectWidget : MonoBehaviour
    {
        [Header("Navigation Buttons")] [SerializeField]
        private Button _prevChapterButton;

        [SerializeField] private Button _nextChapterButton;
        [SerializeField] private TMP_Text _prevChapterText;
        [SerializeField] private TMP_Text _nextChapterText;

        [Header("Current Chapter Display")] [SerializeField]
        private TMP_Text _currentChapterText;

        [SerializeField] private TMP_Text _chapterNameText;

        [Header("Chapter Dropdown (Alternative)")] [SerializeField]
        private TMP_Dropdown _chapterDropdown;

        [Header("Settings")] [SerializeField] private string _prevButtonFormat = "이전 월드";
        [SerializeField] private string _nextButtonFormat = "다음 월드";

        private List<StageCategoryData> _chapters = new();
        private int _currentIndex;
        private bool _useDropdown;

        /// <summary>
        /// 챕터 변경 이벤트 (챕터 ID 전달)
        /// </summary>
        public event Action<string> OnChapterChanged;

        /// <summary>
        /// 현재 선택된 챕터 ID
        /// </summary>
        public string CurrentChapterId =>
            _chapters.Count > 0 && _currentIndex >= 0 && _currentIndex < _chapters.Count
                ? _chapters[_currentIndex].Id
                : null;

        /// <summary>
        /// 현재 선택된 챕터 인덱스
        /// </summary>
        public int CurrentChapterIndex => _currentIndex;

        private void Awake()
        {
            if (_prevChapterButton != null)
            {
                _prevChapterButton.onClick.AddListener(HandlePrevChapter);
            }

            if (_nextChapterButton != null)
            {
                _nextChapterButton.onClick.AddListener(HandleNextChapter);
            }

            if (_chapterDropdown != null)
            {
                _chapterDropdown.onValueChanged.AddListener(HandleDropdownChanged);
            }

            _useDropdown = _chapterDropdown != null;
        }

        private void OnDestroy()
        {
            if (_prevChapterButton != null)
            {
                _prevChapterButton.onClick.RemoveListener(HandlePrevChapter);
            }

            if (_nextChapterButton != null)
            {
                _nextChapterButton.onClick.RemoveListener(HandleNextChapter);
            }

            if (_chapterDropdown != null)
            {
                _chapterDropdown.onValueChanged.RemoveListener(HandleDropdownChanged);
            }
        }

        /// <summary>
        /// 챕터 목록으로 초기화
        /// </summary>
        public void Initialize(List<StageCategoryData> chapters, string initialChapterId = null)
        {
            _chapters = chapters ?? new List<StageCategoryData>();
            _currentIndex = 0;

            if (!string.IsNullOrEmpty(initialChapterId))
            {
                int index = _chapters.FindIndex(c => c.Id == initialChapterId);
                if (index >= 0)
                {
                    _currentIndex = index;
                }
            }

            if (_useDropdown)
            {
                InitializeDropdown();
            }

            UpdateDisplay();
        }

        /// <summary>
        /// 챕터 번호로 초기화 (단순 번호 기반)
        /// </summary>
        public void Initialize(int minChapter, int maxChapter, int currentChapter = 1)
        {
            _chapters.Clear();

            // 간단한 챕터 데이터 생성
            for (int i = minChapter; i <= maxChapter; i++)
            {
                var dummyCategory = ScriptableObject.CreateInstance<StageCategoryData>();
#if UNITY_EDITOR
                dummyCategory.Initialize(
                    id: $"chapter_{i}",
                    contentType: InGameContentType.MainStory,
                    nameKey: $"Chapter {i}",
                    descriptionKey: "",
                    element: default,
                    difficulty: Difficulty.Normal,
                    chapterNumber: i,
                    displayOrder: i,
                    isEnabled: true
                );
#endif
                _chapters.Add(dummyCategory);
            }

            _currentIndex = Mathf.Clamp(currentChapter - minChapter, 0, _chapters.Count - 1);

            if (_useDropdown)
            {
                InitializeDropdown();
            }

            UpdateDisplay();
        }

        /// <summary>
        /// 특정 챕터 선택
        /// </summary>
        public void SelectChapter(string chapterId)
        {
            int index = _chapters.FindIndex(c => c.Id == chapterId);
            if (index >= 0 && index != _currentIndex)
            {
                _currentIndex = index;
                UpdateDisplay();
                OnChapterChanged?.Invoke(CurrentChapterId);
            }
        }

        /// <summary>
        /// 특정 인덱스 선택
        /// </summary>
        public void SelectChapterByIndex(int index)
        {
            if (index >= 0 && index < _chapters.Count && index != _currentIndex)
            {
                _currentIndex = index;
                UpdateDisplay();
                OnChapterChanged?.Invoke(CurrentChapterId);
            }
        }

        #region Navigation

        private void HandlePrevChapter()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                UpdateDisplay();
                OnChapterChanged?.Invoke(CurrentChapterId);
            }
        }

        private void HandleNextChapter()
        {
            if (_currentIndex < _chapters.Count - 1)
            {
                _currentIndex++;
                UpdateDisplay();
                OnChapterChanged?.Invoke(CurrentChapterId);
            }
        }

        private void HandleDropdownChanged(int index)
        {
            if (index >= 0 && index < _chapters.Count && index != _currentIndex)
            {
                _currentIndex = index;
                UpdateDisplay();
                OnChapterChanged?.Invoke(CurrentChapterId);
            }
        }

        #endregion

        #region Display

        private void UpdateDisplay()
        {
            UpdateNavigationButtons();
            UpdateChapterText();
            UpdateDropdown();
        }

        private void UpdateNavigationButtons()
        {
            bool hasPrev = _currentIndex > 0;
            bool hasNext = _currentIndex < _chapters.Count - 1;

            if (_prevChapterButton != null)
            {
                _prevChapterButton.interactable = hasPrev;
                _prevChapterButton.gameObject.SetActive(true);
            }

            if (_nextChapterButton != null)
            {
                _nextChapterButton.interactable = hasNext;
                _nextChapterButton.gameObject.SetActive(true);
            }

            if (_prevChapterText != null)
            {
                _prevChapterText.text = _prevButtonFormat;
            }

            if (_nextChapterText != null)
            {
                _nextChapterText.text = _nextButtonFormat;
            }
        }

        private void UpdateChapterText()
        {
            if (_chapters.Count == 0) return;

            var current = _chapters[_currentIndex];

            if (_currentChapterText != null)
            {
                _currentChapterText.text = $"Chapter {current.ChapterNumber}";
            }

            if (_chapterNameText != null)
            {
                _chapterNameText.text = current.NameKey ?? $"챕터 {current.ChapterNumber}";
            }
        }

        private void InitializeDropdown()
        {
            if (_chapterDropdown == null) return;

            _chapterDropdown.ClearOptions();

            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var chapter in _chapters)
            {
                string displayName = !string.IsNullOrEmpty(chapter.NameKey)
                    ? chapter.NameKey
                    : $"Chapter {chapter.ChapterNumber}";
                options.Add(new TMP_Dropdown.OptionData(displayName));
            }

            _chapterDropdown.AddOptions(options);
            _chapterDropdown.SetValueWithoutNotify(_currentIndex);
        }

        private void UpdateDropdown()
        {
            if (_chapterDropdown != null && _chapterDropdown.value != _currentIndex)
            {
                _chapterDropdown.SetValueWithoutNotify(_currentIndex);
            }
        }

        #endregion

        /// <summary>
        /// 첫 번째 챕터인지 확인
        /// </summary>
        public bool IsFirstChapter => _currentIndex == 0;

        /// <summary>
        /// 마지막 챕터인지 확인
        /// </summary>
        public bool IsLastChapter => _currentIndex >= _chapters.Count - 1;

        /// <summary>
        /// 전체 챕터 수
        /// </summary>
        public int ChapterCount => _chapters.Count;
    }
}