using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 히스토리 화면
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.Standard)]
    public class GachaHistoryScreen : ScreenWidget<GachaHistoryScreen, GachaHistoryState>
    {
        private const int MaxDisplayRecords = 100;

        [Header("UI References")] [SerializeField]
        private TMP_Text _titleText;

        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Dropdown _filterDropdown;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("History List")] [SerializeField]
        private Transform _historyContainer;

        [SerializeField] private GameObject _historyItemPrefab;
        [SerializeField] private GameObject _emptyStatePanel;
        [SerializeField] private TMP_Text _emptyStateText;

        [Header("Statistics")] [SerializeField]
        private TMP_Text _statisticsText;

        private GachaHistoryState _currentState;
        private readonly List<GachaHistoryItem> _historyItems = new();
        private readonly List<GachaPoolData> _availablePools = new();

        protected override void OnInitialize()
        {
            Debug.Log("[GachaHistoryScreen] OnInitialize");

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_filterDropdown != null)
            {
                _filterDropdown.onValueChanged.AddListener(OnFilterChanged);
            }
        }

        protected override void OnBind(GachaHistoryState state)
        {
            _currentState = state ?? new GachaHistoryState();
            Debug.Log($"[GachaHistoryScreen] OnBind - FilterPoolId: {_currentState.FilterPoolId ?? "All"}");

            // Header 설정
            ScreenHeader.Instance?.Configure("gacha_history");

            // 필터 초기화
            InitializeFilter();

            // 히스토리 로드
            RefreshHistory();
        }

        protected override void OnShow()
        {
            Debug.Log("[GachaHistoryScreen] OnShow");

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        protected override void OnHide()
        {
            Debug.Log("[GachaHistoryScreen] OnHide");

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override GachaHistoryState GetState() => _currentState;

        #region Filter

        private void InitializeFilter()
        {
            if (_filterDropdown == null) return;

            _filterDropdown.ClearOptions();
            _availablePools.Clear();

            var options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("전체")
            };

            // 모든 풀 추가
            var gachaPoolDatabase = DataManager.Instance?.GachaPools;
            if (gachaPoolDatabase != null)
            {
                foreach (var pool in gachaPoolDatabase.GachaPools)
                {
                    _availablePools.Add(pool);
                    options.Add(new TMP_Dropdown.OptionData(pool.Name));
                }
            }

            _filterDropdown.AddOptions(options);

            // 초기 선택 설정
            var selectedIndex = 0;
            if (!string.IsNullOrEmpty(_currentState.FilterPoolId))
            {
                for (int i = 0; i < _availablePools.Count; i++)
                {
                    if (_availablePools[i].Id == _currentState.FilterPoolId)
                    {
                        selectedIndex = i + 1; // +1 for "전체"
                        break;
                    }
                }
            }

            _filterDropdown.SetValueWithoutNotify(selectedIndex);
        }

        private void OnFilterChanged(int index)
        {
            if (index == 0)
            {
                _currentState.FilterPoolId = null;
            }
            else if (index - 1 < _availablePools.Count)
            {
                _currentState.FilterPoolId = _availablePools[index - 1].Id;
            }

            RefreshHistory();
        }

        #endregion

        #region History

        private void RefreshHistory()
        {
            ClearHistoryItems();

            if (DataManager.Instance == null)
            {
                ShowEmptyState("데이터를 불러올 수 없습니다.");
                return;
            }

            // 필터에 따라 히스토리 조회
            List<GachaHistoryRecord> records;
            if (string.IsNullOrEmpty(_currentState.FilterPoolId))
            {
                records = DataManager.Instance.GetRecentGachaHistory(MaxDisplayRecords);
            }
            else
            {
                records = DataManager.Instance.GetGachaHistoryByPool(_currentState.FilterPoolId, MaxDisplayRecords);
            }

            if (records == null || records.Count == 0)
            {
                ShowEmptyState("소환 기록이 없습니다.");
                return;
            }

            HideEmptyState();

            // 히스토리 아이템 생성
            foreach (var record in records)
            {
                CreateHistoryItem(record);
            }

            // 통계 갱신
            RefreshStatistics(records);

            // 스크롤 맨 위로
            if (_scrollRect != null)
            {
                _scrollRect.verticalNormalizedPosition = 1f;
            }
        }

        private void CreateHistoryItem(GachaHistoryRecord record)
        {
            if (_historyContainer == null || _historyItemPrefab == null) return;

            var itemGo = Instantiate(_historyItemPrefab, _historyContainer);
            var historyItem = itemGo.GetComponent<GachaHistoryItem>();

            if (historyItem != null)
            {
                historyItem.Setup(record);
                _historyItems.Add(historyItem);
            }
        }

        private void ClearHistoryItems()
        {
            foreach (var item in _historyItems)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }

            _historyItems.Clear();
        }

        private void ShowEmptyState(string message)
        {
            if (_emptyStatePanel != null)
            {
                _emptyStatePanel.SetActive(true);
            }

            if (_emptyStateText != null)
            {
                _emptyStateText.text = message;
            }

            if (_statisticsText != null)
            {
                _statisticsText.gameObject.SetActive(false);
            }
        }

        private void HideEmptyState()
        {
            if (_emptyStatePanel != null)
            {
                _emptyStatePanel.SetActive(false);
            }

            if (_statisticsText != null)
            {
                _statisticsText.gameObject.SetActive(true);
            }
        }

        #endregion

        #region Statistics

        private void RefreshStatistics(List<GachaHistoryRecord> records)
        {
            if (_statisticsText == null) return;

            var totalPulls = 0;
            var totalSSR = 0;
            var totalSR = 0;
            var totalR = 0;

            foreach (var record in records)
            {
                totalPulls += record.Results?.Count ?? 0;
                totalSSR += record.SSRCount;
                totalSR += record.SRCount;
                totalR += record.RCount;
            }

            var ssrRate = totalPulls > 0 ? (float)totalSSR / totalPulls * 100f : 0f;
            var srRate = totalPulls > 0 ? (float)totalSR / totalPulls * 100f : 0f;

            _statisticsText.text = $"총 {totalPulls}회 | " +
                                   $"<color=#FFD700>SSR {totalSSR} ({ssrRate:F1}%)</color> | " +
                                   $"<color=#9932CC>SR {totalSR} ({srRate:F1}%)</color> | " +
                                   $"<color=#4169E1>R {totalR}</color>";
        }

        #endregion

        #region Event Handlers

        private void OnBackClicked()
        {
            Debug.Log("[GachaHistoryScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion
    }
}