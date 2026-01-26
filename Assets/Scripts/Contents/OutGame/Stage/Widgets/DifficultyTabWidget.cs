using System;
using System.Collections.Generic;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 난이도 탭 위젯.
    /// 스테이지 난이도 선택 (순한맛/매운맛/핵불맛)을 제공합니다.
    /// </summary>
    public class DifficultyTabWidget : MonoBehaviour
    {
        [Header("Tabs")] [SerializeField] private DifficultyTab[] _tabs;

        [Header("World Map Button")] [SerializeField]
        private Button _worldMapButton;

        [SerializeField] private TMP_Text _worldMapButtonText;

        [Header("Colors")] [SerializeField] private Color _activeTabColor = new Color(0.4f, 0.7f, 0.4f, 1f);
        [SerializeField] private Color _inactiveTabColor = new Color(0.3f, 0.3f, 0.3f, 0.8f);
        [SerializeField] private Color _activeTextColor = Color.white;
        [SerializeField] private Color _inactiveTextColor = new Color(1f, 1f, 1f, 0.6f);

        private Difficulty _currentDifficulty;
        private bool _isInitialized;

        /// <summary>
        /// 난이도 변경 이벤트
        /// </summary>
        public event Action<Difficulty> OnDifficultyChanged;

        /// <summary>
        /// 월드맵 버튼 클릭 이벤트
        /// </summary>
        public event Action OnWorldMapClicked;

        /// <summary>
        /// 현재 선택된 난이도
        /// </summary>
        public Difficulty CurrentDifficulty => _currentDifficulty;

        private void Awake()
        {
            InitializeTabs();

            if (_worldMapButton != null)
            {
                _worldMapButton.onClick.AddListener(HandleWorldMapClick);
            }
        }

        private void OnDestroy()
        {
            if (_tabs != null)
            {
                foreach (var tab in _tabs)
                {
                    if (tab != null && tab.Button != null)
                    {
                        tab.Button.onClick.RemoveAllListeners();
                    }
                }
            }

            if (_worldMapButton != null)
            {
                _worldMapButton.onClick.RemoveListener(HandleWorldMapClick);
            }
        }

        private void InitializeTabs()
        {
            if (_tabs == null) return;

            foreach (var tab in _tabs)
            {
                if (tab?.Button != null)
                {
                    var difficulty = tab.Difficulty;
                    tab.Button.onClick.AddListener(() => SelectDifficulty(difficulty));
                }
            }

            _isInitialized = true;
        }

        /// <summary>
        /// 초기화 및 기본 난이도 설정
        /// </summary>
        public void Initialize(Difficulty defaultDifficulty = Difficulty.Normal)
        {
            _currentDifficulty = defaultDifficulty;
            UpdateTabVisuals();
        }

        /// <summary>
        /// 특정 난이도 선택
        /// </summary>
        public void SelectDifficulty(Difficulty difficulty)
        {
            if (_currentDifficulty == difficulty) return;

            _currentDifficulty = difficulty;
            UpdateTabVisuals();
            OnDifficultyChanged?.Invoke(difficulty);
        }

        /// <summary>
        /// 특정 난이도 탭 활성화/비활성화
        /// </summary>
        public void SetDifficultyEnabled(Difficulty difficulty, bool enabled)
        {
            var tab = FindTab(difficulty);
            if (tab?.Button != null)
            {
                tab.Button.interactable = enabled;
            }
        }

        /// <summary>
        /// 잠금된 난이도 표시
        /// </summary>
        public void SetDifficultyLocked(Difficulty difficulty, bool locked)
        {
            var tab = FindTab(difficulty);
            if (tab != null)
            {
                tab.SetLocked(locked);
            }
        }

        private void UpdateTabVisuals()
        {
            if (_tabs == null) return;

            foreach (var tab in _tabs)
            {
                if (tab == null) continue;

                bool isActive = tab.Difficulty == _currentDifficulty;

                // 배경색
                if (tab.Background != null)
                {
                    tab.Background.color = isActive ? _activeTabColor : _inactiveTabColor;
                }

                // 텍스트 색상
                if (tab.Label != null)
                {
                    tab.Label.color = isActive ? _activeTextColor : _inactiveTextColor;
                }

                // 선택 인디케이터
                if (tab.SelectionIndicator != null)
                {
                    tab.SelectionIndicator.SetActive(isActive);
                }
            }
        }

        private DifficultyTab FindTab(Difficulty difficulty)
        {
            if (_tabs == null) return null;

            foreach (var tab in _tabs)
            {
                if (tab != null && tab.Difficulty == difficulty)
                {
                    return tab;
                }
            }

            return null;
        }

        private void HandleWorldMapClick()
        {
            OnWorldMapClicked?.Invoke();
        }

        /// <summary>
        /// 난이도 탭 설정
        /// </summary>
        [Serializable]
        public class DifficultyTab
        {
            [SerializeField] private Difficulty _difficulty;
            [SerializeField] private Button _button;
            [SerializeField] private Image _background;
            [SerializeField] private TMP_Text _label;
            [SerializeField] private GameObject _selectionIndicator;
            [SerializeField] private GameObject _lockIcon;

            public Difficulty Difficulty => _difficulty;
            public Button Button => _button;
            public Image Background => _background;
            public TMP_Text Label => _label;
            public GameObject SelectionIndicator => _selectionIndicator;

            public void SetLocked(bool locked)
            {
                if (_lockIcon != null)
                {
                    _lockIcon.SetActive(locked);
                }

                if (_button != null)
                {
                    _button.interactable = !locked;
                }
            }
        }
    }
}