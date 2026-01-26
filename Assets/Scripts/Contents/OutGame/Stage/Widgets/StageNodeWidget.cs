using System;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 스테이지 노드 위젯.
    /// 맵에서 개별 스테이지를 표시하고 선택 가능하게 합니다.
    /// </summary>
    public class StageNodeWidget : MonoBehaviour
    {
        public enum NodeState
        {
            Locked,     // 잠금 (미해금)
            Available,  // 진입 가능
            Cleared,    // 클리어됨
            Selected    // 현재 선택됨
        }

        [Header("Visual")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _nodeIcon;
        [SerializeField] private Image _lockIcon;
        [SerializeField] private Image _selectionHighlight;
        [SerializeField] private Image _clearedCheckmark;

        [Header("Info")]
        [SerializeField] private TMP_Text _stageNumberText;
        [SerializeField] private Transform _starContainer;
        [SerializeField] private Image[] _starImages;

        [Header("Preview")]
        [SerializeField] private Transform _characterPreviewContainer;
        [SerializeField] private Image _bossPreviewImage;

        [Header("Interaction")]
        [SerializeField] private Button _nodeButton;

        [Header("Colors")]
        [SerializeField] private Color _lockedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        [SerializeField] private Color _availableColor = new Color(0.4f, 0.7f, 0.4f, 1f);
        [SerializeField] private Color _clearedColor = new Color(0.3f, 0.6f, 0.3f, 1f);
        [SerializeField] private Color _selectedColor = new Color(0.5f, 0.8f, 0.5f, 1f);
        [SerializeField] private Color _starFilledColor = new Color(1f, 0.85f, 0.2f, 1f);
        [SerializeField] private Color _starEmptyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        private StageData _stageData;
        private NodeState _currentState;
        private int _earnedStars;

        /// <summary>
        /// 노드 클릭 이벤트
        /// </summary>
        public event Action<StageNodeWidget, StageData> OnNodeClicked;

        /// <summary>
        /// 현재 바인딩된 스테이지 데이터
        /// </summary>
        public StageData StageData => _stageData;

        /// <summary>
        /// 현재 노드 상태
        /// </summary>
        public NodeState CurrentState => _currentState;

        private void Awake()
        {
            if (_nodeButton != null)
            {
                _nodeButton.onClick.AddListener(HandleNodeClick);
            }
        }

        private void OnDestroy()
        {
            if (_nodeButton != null)
            {
                _nodeButton.onClick.RemoveListener(HandleNodeClick);
            }
        }

        /// <summary>
        /// 노드 초기화
        /// </summary>
        public void Initialize(StageData stageData, NodeState state, int earnedStars = 0)
        {
            _stageData = stageData;
            _currentState = state;
            _earnedStars = Mathf.Clamp(earnedStars, 0, 3);

            UpdateVisual();
        }

        /// <summary>
        /// 노드 상태 설정
        /// </summary>
        public void SetState(NodeState state)
        {
            _currentState = state;
            UpdateVisual();
        }

        /// <summary>
        /// 획득 별 수 설정
        /// </summary>
        public void SetStars(int stars)
        {
            _earnedStars = Mathf.Clamp(stars, 0, 3);
            UpdateStars();
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            if (selected && _currentState != NodeState.Locked)
            {
                _currentState = NodeState.Selected;
            }
            else if (!selected && _currentState == NodeState.Selected)
            {
                _currentState = _earnedStars > 0 ? NodeState.Cleared : NodeState.Available;
            }

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            UpdateBackground();
            UpdateStageNumber();
            UpdateStars();
            UpdateIcons();
            UpdateInteractable();
        }

        private void UpdateBackground()
        {
            if (_background == null) return;

            _background.color = _currentState switch
            {
                NodeState.Locked => _lockedColor,
                NodeState.Available => _availableColor,
                NodeState.Cleared => _clearedColor,
                NodeState.Selected => _selectedColor,
                _ => _availableColor
            };
        }

        private void UpdateStageNumber()
        {
            if (_stageNumberText == null || _stageData == null) return;

            _stageNumberText.text = $"{_stageData.Chapter}-{_stageData.StageNumber}";
        }

        private void UpdateStars()
        {
            if (_starImages == null || _starImages.Length == 0) return;

            for (int i = 0; i < _starImages.Length; i++)
            {
                if (_starImages[i] != null)
                {
                    bool isFilled = i < _earnedStars;
                    _starImages[i].color = isFilled ? _starFilledColor : _starEmptyColor;
                }
            }

            // 잠금 상태에서는 별 숨김
            if (_starContainer != null)
            {
                _starContainer.gameObject.SetActive(_currentState != NodeState.Locked);
            }
        }

        private void UpdateIcons()
        {
            // 잠금 아이콘
            if (_lockIcon != null)
            {
                _lockIcon.gameObject.SetActive(_currentState == NodeState.Locked);
            }

            // 선택 하이라이트
            if (_selectionHighlight != null)
            {
                _selectionHighlight.gameObject.SetActive(_currentState == NodeState.Selected);
            }

            // 클리어 체크마크 (선택적)
            if (_clearedCheckmark != null)
            {
                _clearedCheckmark.gameObject.SetActive(_currentState == NodeState.Cleared);
            }

            // 캐릭터 미리보기 (선택 시에만)
            if (_characterPreviewContainer != null)
            {
                _characterPreviewContainer.gameObject.SetActive(_currentState == NodeState.Selected);
            }
        }

        private void UpdateInteractable()
        {
            if (_nodeButton == null) return;

            // 잠금 상태가 아닐 때만 상호작용 가능
            _nodeButton.interactable = _currentState != NodeState.Locked;
        }

        private void HandleNodeClick()
        {
            if (_currentState == NodeState.Locked) return;

            OnNodeClicked?.Invoke(this, _stageData);
        }

        /// <summary>
        /// 보스 미리보기 이미지 설정
        /// </summary>
        public void SetBossPreview(Sprite bossSprite)
        {
            if (_bossPreviewImage != null)
            {
                _bossPreviewImage.sprite = bossSprite;
                _bossPreviewImage.gameObject.SetActive(bossSprite != null);
            }
        }
    }
}
