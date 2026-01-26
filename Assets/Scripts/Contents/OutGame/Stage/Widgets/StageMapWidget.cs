using System;
using System.Collections.Generic;
using Sc.Core;
using Sc.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 스테이지 맵 위젯.
    /// 스테이지 노드들을 맵 형태로 표시하고 스크롤/줌 기능을 제공합니다.
    /// </summary>
    public class StageMapWidget : MonoBehaviour
    {
        [Header("Map")]
        [SerializeField] private Image _mapBackground;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _mapContent;
        [SerializeField] private Transform _nodeContainer;

        [Header("Info Bubble")]
        [SerializeField] private GameObject _infoBubble;
        [SerializeField] private TMPro.TMP_Text _recommendedPowerText;
        [SerializeField] private TMPro.TMP_Text _stageNameText;
        [SerializeField] private Transform _enemyPreviewContainer;
        [SerializeField] private Transform _partyPreviewContainer;
        [SerializeField] private Button _infoBubbleButton;

        [Header("Prefabs")]
        [SerializeField] private StageNodeWidget _nodePrefab;

        [Header("Settings")]
        [SerializeField] private float _minZoom = 0.5f;
        [SerializeField] private float _maxZoom = 2f;
        [SerializeField] private float _zoomSpeed = 0.1f;
        [SerializeField] private Vector2 _nodeSpacing = new Vector2(200f, 150f);
        [SerializeField] private int _nodesPerRow = 3;

        private List<StageNodeWidget> _nodeWidgets = new();
        private StageNodeWidget _selectedNode;
        private float _currentZoom = 1f;

        /// <summary>
        /// 스테이지 선택 이벤트
        /// </summary>
        public event Action<StageData> OnStageSelected;

        /// <summary>
        /// 스테이지 진입 요청 이벤트 (InfoBubble 클릭)
        /// </summary>
        public event Action<StageData> OnStageEnterRequested;

        /// <summary>
        /// 현재 선택된 스테이지
        /// </summary>
        public StageData SelectedStage => _selectedNode?.StageData;

        private void Awake()
        {
            if (_infoBubbleButton != null)
            {
                _infoBubbleButton.onClick.AddListener(HandleInfoBubbleClick);
            }

            HideInfoBubble();
        }

        private void OnDestroy()
        {
            if (_infoBubbleButton != null)
            {
                _infoBubbleButton.onClick.RemoveListener(HandleInfoBubbleClick);
            }

            ClearNodes();
        }

        /// <summary>
        /// 맵 초기화 및 스테이지 노드 생성
        /// </summary>
        public void Initialize(List<StageData> stages, string selectedStageId = null)
        {
            ClearNodes();

            if (stages == null || stages.Count == 0)
            {
                Debug.LogWarning("[StageMapWidget] No stages to display");
                return;
            }

            CreateNodes(stages);

            // 선택된 스테이지로 이동
            if (!string.IsNullOrEmpty(selectedStageId))
            {
                SelectStageById(selectedStageId);
            }
            else if (_nodeWidgets.Count > 0)
            {
                // 첫 번째 이용 가능한 스테이지 선택
                SelectFirstAvailableNode();
            }
        }

        /// <summary>
        /// 맵 배경 이미지 설정
        /// </summary>
        public void SetMapBackground(Sprite background)
        {
            if (_mapBackground != null)
            {
                _mapBackground.sprite = background;
            }
        }

        /// <summary>
        /// 스테이지 ID로 선택
        /// </summary>
        public void SelectStageById(string stageId)
        {
            var node = _nodeWidgets.Find(n => n.StageData?.Id == stageId);
            if (node != null)
            {
                SelectNode(node);
                ScrollToNode(node);
            }
        }

        /// <summary>
        /// 스테이지 데이터로 노드 상태 갱신
        /// </summary>
        public void RefreshNodeStates(Dictionary<string, int> stageStars, HashSet<string> unlockedStages)
        {
            foreach (var node in _nodeWidgets)
            {
                if (node.StageData == null) continue;

                string stageId = node.StageData.Id;
                bool isUnlocked = unlockedStages?.Contains(stageId) ?? true;
                int stars = stageStars?.GetValueOrDefault(stageId, 0) ?? 0;

                StageNodeWidget.NodeState state;
                if (!isUnlocked)
                {
                    state = StageNodeWidget.NodeState.Locked;
                }
                else if (stars > 0)
                {
                    state = StageNodeWidget.NodeState.Cleared;
                }
                else
                {
                    state = StageNodeWidget.NodeState.Available;
                }

                node.SetState(state);
                node.SetStars(stars);
            }

            // 현재 선택 유지
            if (_selectedNode != null)
            {
                _selectedNode.SetSelected(true);
            }
        }

        #region Node Management

        private void CreateNodes(List<StageData> stages)
        {
            if (_nodePrefab == null || _nodeContainer == null)
            {
                Debug.LogError("[StageMapWidget] Node prefab or container not assigned");
                return;
            }

            for (int i = 0; i < stages.Count; i++)
            {
                var stage = stages[i];
                var nodeGo = Instantiate(_nodePrefab.gameObject, _nodeContainer);
                var node = nodeGo.GetComponent<StageNodeWidget>();

                if (node != null)
                {
                    // 초기 상태 (나중에 RefreshNodeStates로 갱신됨)
                    var initialState = StageNodeWidget.NodeState.Available;
                    node.Initialize(stage, initialState, 0);
                    node.OnNodeClicked += HandleNodeClicked;

                    // 위치 설정 (그리드 배치)
                    PositionNode(nodeGo.GetComponent<RectTransform>(), i);

                    _nodeWidgets.Add(node);
                }
            }

            // 컨텐츠 크기 조정
            AdjustContentSize(stages.Count);
        }

        private void PositionNode(RectTransform nodeRect, int index)
        {
            if (nodeRect == null) return;

            int row = index / _nodesPerRow;
            int col = index % _nodesPerRow;

            // 아이소메트릭 배치 (지그재그)
            float xOffset = (row % 2 == 1) ? _nodeSpacing.x * 0.5f : 0f;
            float x = col * _nodeSpacing.x + xOffset;
            float y = -row * _nodeSpacing.y;

            nodeRect.anchoredPosition = new Vector2(x, y);
        }

        private void AdjustContentSize(int nodeCount)
        {
            if (_mapContent == null) return;

            int rows = Mathf.CeilToInt((float)nodeCount / _nodesPerRow);
            float width = _nodesPerRow * _nodeSpacing.x + _nodeSpacing.x;
            float height = rows * _nodeSpacing.y + _nodeSpacing.y;

            _mapContent.sizeDelta = new Vector2(width, height);
        }

        private void ClearNodes()
        {
            foreach (var node in _nodeWidgets)
            {
                if (node != null)
                {
                    node.OnNodeClicked -= HandleNodeClicked;
                    Destroy(node.gameObject);
                }
            }
            _nodeWidgets.Clear();
            _selectedNode = null;
        }

        #endregion

        #region Selection

        private void HandleNodeClicked(StageNodeWidget node, StageData stageData)
        {
            SelectNode(node);
        }

        private void SelectNode(StageNodeWidget node)
        {
            if (node == null || node.CurrentState == StageNodeWidget.NodeState.Locked) return;

            // 이전 선택 해제
            if (_selectedNode != null && _selectedNode != node)
            {
                _selectedNode.SetSelected(false);
            }

            // 새 선택
            _selectedNode = node;
            _selectedNode.SetSelected(true);

            // Info Bubble 표시
            ShowInfoBubble(_selectedNode);

            // 이벤트 발행
            OnStageSelected?.Invoke(_selectedNode.StageData);
        }

        private void SelectFirstAvailableNode()
        {
            foreach (var node in _nodeWidgets)
            {
                if (node.CurrentState != StageNodeWidget.NodeState.Locked)
                {
                    SelectNode(node);
                    return;
                }
            }
        }

        #endregion

        #region Info Bubble

        private void ShowInfoBubble(StageNodeWidget node)
        {
            if (_infoBubble == null || node?.StageData == null) return;

            _infoBubble.SetActive(true);

            // 정보 업데이트
            if (_recommendedPowerText != null)
            {
                _recommendedPowerText.text = $"권장 전투력: {node.StageData.RecommendedPower:N0}";
            }

            if (_stageNameText != null)
            {
                _stageNameText.text = node.StageData.Name;
            }

            // 위치 조정 (노드 위에)
            PositionInfoBubble(node);
        }

        private void HideInfoBubble()
        {
            if (_infoBubble != null)
            {
                _infoBubble.SetActive(false);
            }
        }

        private void PositionInfoBubble(StageNodeWidget node)
        {
            if (_infoBubble == null || node == null) return;

            var bubbleRect = _infoBubble.GetComponent<RectTransform>();
            var nodeRect = node.GetComponent<RectTransform>();

            if (bubbleRect != null && nodeRect != null)
            {
                // 노드 위에 배치
                Vector2 nodePos = nodeRect.anchoredPosition;
                bubbleRect.anchoredPosition = new Vector2(nodePos.x, nodePos.y + 100f);
            }
        }

        private void HandleInfoBubbleClick()
        {
            if (_selectedNode?.StageData != null)
            {
                OnStageEnterRequested?.Invoke(_selectedNode.StageData);
            }
        }

        #endregion

        #region Scroll & Zoom

        private void ScrollToNode(StageNodeWidget node)
        {
            if (_scrollRect == null || node == null) return;

            var nodeRect = node.GetComponent<RectTransform>();
            if (nodeRect == null) return;

            // 노드 위치로 스크롤
            Vector2 targetPos = nodeRect.anchoredPosition;
            Vector2 contentSize = _mapContent.sizeDelta;
            Vector2 viewportSize = _scrollRect.viewport.rect.size;

            float normalizedX = Mathf.Clamp01(targetPos.x / (contentSize.x - viewportSize.x));
            float normalizedY = Mathf.Clamp01(-targetPos.y / (contentSize.y - viewportSize.y));

            _scrollRect.normalizedPosition = new Vector2(normalizedX, 1f - normalizedY);
        }

        /// <summary>
        /// 줌 설정
        /// </summary>
        public void SetZoom(float zoom)
        {
            _currentZoom = Mathf.Clamp(zoom, _minZoom, _maxZoom);

            if (_mapContent != null)
            {
                _mapContent.localScale = Vector3.one * _currentZoom;
            }
        }

        /// <summary>
        /// 줌 인
        /// </summary>
        public void ZoomIn()
        {
            SetZoom(_currentZoom + _zoomSpeed);
        }

        /// <summary>
        /// 줌 아웃
        /// </summary>
        public void ZoomOut()
        {
            SetZoom(_currentZoom - _zoomSpeed);
        }

        #endregion
    }
}
