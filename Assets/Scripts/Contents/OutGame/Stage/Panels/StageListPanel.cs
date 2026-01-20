using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Core;
using Sc.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 스테이지 목록 패널.
    /// 스테이지 아이템들을 관리하고 표시합니다.
    /// </summary>
    public class StageListPanel : Widget
    {
        [Header("List")] [SerializeField] private Transform _itemContainer;
        [SerializeField] private StageItemWidget _itemPrefab;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Empty State")] [SerializeField]
        private GameObject _emptyStateObject;

        private readonly List<StageItemWidget> _items = new();
        private StageData _selectedStage;
        private Action<StageData> _onStageSelected;

        /// <summary>
        /// 현재 선택된 스테이지
        /// </summary>
        public StageData SelectedStage => _selectedStage;

        /// <summary>
        /// 스테이지 선택 이벤트
        /// </summary>
        public event Action<StageData> OnStageSelected
        {
            add => _onStageSelected += value;
            remove => _onStageSelected -= value;
        }

        protected override void OnInitialize()
        {
            Debug.Log("[StageListPanel] OnInitialize");
        }

        /// <summary>
        /// 스테이지 목록 설정
        /// </summary>
        /// <param name="stages">표시할 스테이지 목록</param>
        /// <param name="initialSelectedId">초기 선택 스테이지 ID (없으면 null)</param>
        public void SetStages(IReadOnlyList<StageData> stages, string initialSelectedId = null)
        {
            ClearItems();

            if (stages == null || stages.Count == 0)
            {
                ShowEmptyState();
                return;
            }

            HideEmptyState();

            StageData initialSelected = null;

            foreach (var stage in stages)
            {
                var item = CreateItem(stage);

                if (initialSelectedId != null && stage.Id == initialSelectedId)
                {
                    initialSelected = stage;
                }
            }

            // 초기 선택
            if (initialSelected != null)
            {
                SelectStage(initialSelected, notifyEvent: false);
            }
            else if (stages.Count > 0)
            {
                // 첫 번째 스테이지 자동 선택
                SelectStage(stages[0], notifyEvent: false);
            }

            // 스크롤 초기화
            ResetScroll();
        }

        /// <summary>
        /// 스테이지 선택
        /// </summary>
        public void SelectStage(StageData stage, bool notifyEvent = true)
        {
            if (stage == null) return;

            _selectedStage = stage;

            // 모든 아이템 선택 상태 업데이트
            foreach (var item in _items)
            {
                // 현재 아이템이 선택된 스테이지인지 확인
                // StageItemWidget에서 StageData를 가져올 방법이 없으므로
                // 아이템 생성 시 저장한 매핑을 사용하거나, 여기서는 간단히 처리
                item.SetSelected(false);
            }

            // 선택된 아이템 찾아서 선택 상태 설정
            int index = FindItemIndex(stage);
            if (index >= 0 && index < _items.Count)
            {
                _items[index].SetSelected(true);
            }

            if (notifyEvent)
            {
                _onStageSelected?.Invoke(stage);
            }

            Debug.Log($"[StageListPanel] Stage selected: {stage.Id}");
        }

        /// <summary>
        /// 목록 갱신 (클리어 상태 등)
        /// </summary>
        public void RefreshItems()
        {
            // TODO: 클리어 상태 변경 시 아이템 갱신
            Debug.Log("[StageListPanel] RefreshItems");
        }

        private StageItemWidget CreateItem(StageData stageData)
        {
            if (_itemPrefab == null || _itemContainer == null)
            {
                Debug.LogError("[StageListPanel] Item prefab or container is null");
                return null;
            }

            var itemGo = Instantiate(_itemPrefab, _itemContainer);
            var item = itemGo.GetComponent<StageItemWidget>();

            if (item != null)
            {
                item.Initialize();

                // 클리어 정보 조회
                StageClearInfo? clearInfo = GetClearInfo(stageData.Id);

                // 잠금 상태 확인
                bool isLocked = IsLocked(stageData);

                item.Setup(stageData, clearInfo, isLocked, OnItemClicked);
                _items.Add(item);
            }

            return item;
        }

        private void OnItemClicked(StageData stageData)
        {
            SelectStage(stageData);
        }

        private int FindItemIndex(StageData stage)
        {
            // 현재 구조에서는 items 순서가 stages 순서와 동일하다고 가정
            // 실제로는 StageData → Index 매핑을 유지하는 것이 좋음
            for (int i = 0; i < _items.Count; i++)
            {
                // StageItemWidget에 StageData 참조가 있다면 비교 가능
                // 현재는 순서 기반으로 처리
            }

            return -1; // TODO: 실제 구현
        }

        private StageClearInfo? GetClearInfo(string stageId)
        {
            if (DataManager.Instance?.IsInitialized != true) return null;

            var progress = DataManager.Instance.StageProgress;
            return progress.FindClearInfo(stageId);
        }

        private bool IsLocked(StageData stageData)
        {
            // TODO: 실제 잠금 조건 확인
            // - 선행 스테이지 클리어 여부
            // - 레벨 조건
            // 현재는 간단히 처리
            return false;
        }

        private void ClearItems()
        {
            foreach (var item in _items)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }

            _items.Clear();
            _selectedStage = null;
        }

        private void ResetScroll()
        {
            if (_scrollRect != null)
            {
                _scrollRect.verticalNormalizedPosition = 1f;
            }
        }

        private void ShowEmptyState()
        {
            if (_emptyStateObject != null)
            {
                _emptyStateObject.SetActive(true);
            }
        }

        private void HideEmptyState()
        {
            if (_emptyStateObject != null)
            {
                _emptyStateObject.SetActive(false);
            }
        }

        protected override void OnRelease()
        {
            ClearItems();
            _onStageSelected = null;
        }
    }
}