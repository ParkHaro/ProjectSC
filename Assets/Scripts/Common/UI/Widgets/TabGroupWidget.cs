using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Common.UI.Widgets
{
    /// <summary>
    /// 탭 데이터
    /// </summary>
    public struct TabData
    {
        public int Index;
        public string Label;
        public object UserData;
    }

    /// <summary>
    /// 탭 그룹 위젯 - 탭 전환 관리
    /// </summary>
    public class TabGroupWidget : Widget
    {
        [Header("References")]
        [SerializeField] private Transform _tabButtonContainer;
        [SerializeField] private TabButton _tabButtonPrefab;
        [SerializeField] private Transform _contentContainer;

        private readonly List<TabButton> _tabButtons = new();
        private readonly List<GameObject> _tabContents = new();
        private int _currentTabIndex = -1;

        /// <summary>
        /// 현재 선택된 탭 인덱스
        /// </summary>
        public int CurrentTabIndex => _currentTabIndex;

        /// <summary>
        /// 탭 개수
        /// </summary>
        public int TabCount => _tabButtons.Count;

        /// <summary>
        /// 탭 변경 이벤트
        /// </summary>
        public event Action<int> OnTabChanged;

        protected override void OnInitialize()
        {
            Debug.Log("[TabGroupWidget] OnInitialize");
        }

        /// <summary>
        /// 탭 초기화 (탭 데이터와 컨텐츠 함께 설정)
        /// </summary>
        public void SetupTabs(List<TabData> tabDataList, List<GameObject> tabContents = null)
        {
            ClearTabs();

            if (tabDataList == null || tabDataList.Count == 0)
            {
                Debug.LogWarning("[TabGroupWidget] No tabs to setup");
                return;
            }

            // 탭 버튼 생성
            for (int i = 0; i < tabDataList.Count; i++)
            {
                var tabData = tabDataList[i];
                CreateTabButton(i, tabData.Label);
            }

            // 탭 컨텐츠 등록
            if (tabContents != null)
            {
                foreach (var content in tabContents)
                {
                    _tabContents.Add(content);
                }
            }

            Debug.Log($"[TabGroupWidget] Created {_tabButtons.Count} tabs");
        }

        /// <summary>
        /// 탭 버튼만 설정 (컨텐츠는 별도 관리)
        /// </summary>
        public void SetupTabButtons(List<string> labels)
        {
            ClearTabs();

            if (labels == null || labels.Count == 0)
            {
                Debug.LogWarning("[TabGroupWidget] No labels provided");
                return;
            }

            for (int i = 0; i < labels.Count; i++)
            {
                CreateTabButton(i, labels[i]);
            }
        }

        /// <summary>
        /// 탭 컨텐츠 등록
        /// </summary>
        public void RegisterContent(GameObject content)
        {
            if (content != null && !_tabContents.Contains(content))
            {
                _tabContents.Add(content);
            }
        }

        /// <summary>
        /// 탭 선택
        /// </summary>
        public void SelectTab(int index)
        {
            if (index < 0 || index >= _tabButtons.Count)
            {
                Debug.LogWarning($"[TabGroupWidget] Invalid tab index: {index}");
                return;
            }

            if (_currentTabIndex == index)
            {
                return;
            }

            var previousIndex = _currentTabIndex;
            _currentTabIndex = index;

            // 버튼 상태 업데이트
            for (int i = 0; i < _tabButtons.Count; i++)
            {
                _tabButtons[i].SetSelected(i == index);
            }

            // 컨텐츠 전환
            for (int i = 0; i < _tabContents.Count; i++)
            {
                if (_tabContents[i] != null)
                {
                    _tabContents[i].SetActive(i == index);
                }
            }

            Debug.Log($"[TabGroupWidget] Tab changed: {previousIndex} -> {index}");
            OnTabChanged?.Invoke(index);
        }

        /// <summary>
        /// 첫 번째 탭 선택
        /// </summary>
        public void SelectFirstTab()
        {
            if (_tabButtons.Count > 0)
            {
                SelectTab(0);
            }
        }

        /// <summary>
        /// 탭 뱃지 설정
        /// </summary>
        public void SetBadge(int tabIndex, bool show)
        {
            if (tabIndex >= 0 && tabIndex < _tabButtons.Count)
            {
                _tabButtons[tabIndex].SetBadge(show);
            }
        }

        /// <summary>
        /// 탭 뱃지 카운트 설정
        /// </summary>
        public void SetBadgeCount(int tabIndex, int count)
        {
            if (tabIndex >= 0 && tabIndex < _tabButtons.Count)
            {
                _tabButtons[tabIndex].SetBadgeCount(count);
            }
        }

        /// <summary>
        /// 탭 레이블 변경
        /// </summary>
        public void SetTabLabel(int tabIndex, string label)
        {
            if (tabIndex >= 0 && tabIndex < _tabButtons.Count)
            {
                _tabButtons[tabIndex].SetLabel(label);
            }
        }

        /// <summary>
        /// 탭 버튼 가져오기
        /// </summary>
        public TabButton GetTabButton(int index)
        {
            if (index >= 0 && index < _tabButtons.Count)
            {
                return _tabButtons[index];
            }
            return null;
        }

        private void CreateTabButton(int index, string label)
        {
            if (_tabButtonPrefab == null || _tabButtonContainer == null)
            {
                Debug.LogError("[TabGroupWidget] TabButton prefab or container is null");
                return;
            }

            var buttonGo = Instantiate(_tabButtonPrefab, _tabButtonContainer);
            var tabButton = buttonGo.GetComponent<TabButton>();
            if (tabButton != null)
            {
                tabButton.Initialize();
                tabButton.Setup(index, label);
                tabButton.OnClicked += OnTabButtonClicked;
                _tabButtons.Add(tabButton);
            }
        }

        private void OnTabButtonClicked(int index)
        {
            SelectTab(index);
        }

        private void ClearTabs()
        {
            foreach (var button in _tabButtons)
            {
                if (button != null)
                {
                    button.OnClicked -= OnTabButtonClicked;
                    Destroy(button.gameObject);
                }
            }
            _tabButtons.Clear();
            _tabContents.Clear();
            _currentTabIndex = -1;
        }

        protected override void OnRelease()
        {
            OnTabChanged = null;
            ClearTabs();
        }

        private void OnDestroy()
        {
            OnRelease();
        }
    }
}
