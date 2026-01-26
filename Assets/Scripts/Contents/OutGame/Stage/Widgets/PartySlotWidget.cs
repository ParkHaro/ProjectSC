using System;
using Sc.Contents.Character.Widgets;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 파티 슬롯 위젯.
    /// 편성 슬롯에 캐릭터를 할당하거나 해제합니다.
    /// 레퍼런스: Docs/Design/Reference/PartySelect.jpg - 좌측 전투 미리보기 영역
    /// </summary>
    public class PartySlotWidget : MonoBehaviour
    {
        /// <summary>
        /// 슬롯 상태
        /// </summary>
        public enum SlotState
        {
            Empty, // 빈 슬롯
            Assigned, // 캐릭터 할당됨
            Locked // 잠금 (해금 필요)
        }

        [Header("Visual")] [SerializeField] private Image _slotBackground;
        [SerializeField] private Image _characterThumbnail;
        [SerializeField] private Image _elementIcon;
        [SerializeField] private Image _emptyIcon;
        [SerializeField] private Image _lockIcon;
        [SerializeField] private Image _selectionHighlight;

        [Header("Character Info")] [SerializeField]
        private TMP_Text _levelText;

        [SerializeField] private Transform _starContainer;
        [SerializeField] private TMP_Text _combatPowerText;

        [Header("Position Indicator")] [SerializeField]
        private TMP_Text _positionText;

        [SerializeField] private Image _formationLineIndicator;

        [Header("Interaction")] [SerializeField]
        private Button _slotButton;

        [SerializeField] private Button _removeButton;

        [Header("Colors")] [SerializeField] private Color _emptyColor = new Color32(80, 80, 80, 200);
        [SerializeField] private Color _assignedColor = new Color32(60, 120, 60, 255);
        [SerializeField] private Color _lockedColor = new Color32(50, 50, 50, 180);
        [SerializeField] private Color _selectedColor = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _frontLineColor = new Color32(100, 150, 200, 255);
        [SerializeField] private Color _backLineColor = new Color32(150, 100, 200, 255);

        private SlotState _currentState = SlotState.Empty;
        private OwnedCharacter _assignedCharacter;
        private int _slotIndex;
        private bool _isFrontLine;
        private bool _isSelected;

        /// <summary>
        /// 슬롯 클릭 이벤트 (빈 슬롯 클릭 시 캐릭터 선택 요청)
        /// </summary>
        public event Action<PartySlotWidget, int> OnSlotClicked;

        /// <summary>
        /// 캐릭터 제거 요청 이벤트
        /// </summary>
        public event Action<PartySlotWidget, OwnedCharacter> OnRemoveRequested;

        /// <summary>
        /// 할당된 캐릭터
        /// </summary>
        public OwnedCharacter AssignedCharacter => _assignedCharacter;

        /// <summary>
        /// 슬롯 인덱스
        /// </summary>
        public int SlotIndex => _slotIndex;

        /// <summary>
        /// 현재 슬롯 상태
        /// </summary>
        public SlotState CurrentState => _currentState;

        /// <summary>
        /// 앞줄 여부
        /// </summary>
        public bool IsFrontLine => _isFrontLine;

        private void Awake()
        {
            if (_slotButton != null)
            {
                _slotButton.onClick.AddListener(HandleSlotClick);
            }

            if (_removeButton != null)
            {
                _removeButton.onClick.AddListener(HandleRemoveClick);
            }
        }

        private void OnDestroy()
        {
            if (_slotButton != null)
            {
                _slotButton.onClick.RemoveListener(HandleSlotClick);
            }

            if (_removeButton != null)
            {
                _removeButton.onClick.RemoveListener(HandleRemoveClick);
            }
        }

        /// <summary>
        /// 슬롯 초기화
        /// </summary>
        /// <param name="slotIndex">슬롯 인덱스</param>
        /// <param name="isFrontLine">앞줄 여부</param>
        /// <param name="isLocked">잠금 여부</param>
        public void Initialize(int slotIndex, bool isFrontLine, bool isLocked = false)
        {
            _slotIndex = slotIndex;
            _isFrontLine = isFrontLine;
            _currentState = isLocked ? SlotState.Locked : SlotState.Empty;
            _assignedCharacter = default;

            UpdateVisual();
        }

        /// <summary>
        /// 캐릭터 할당
        /// </summary>
        public void AssignCharacter(OwnedCharacter character)
        {
            if (_currentState == SlotState.Locked) return;

            _assignedCharacter = character;
            _currentState = !string.IsNullOrEmpty(character.InstanceId) ? SlotState.Assigned : SlotState.Empty;

            UpdateVisual();
        }

        /// <summary>
        /// 캐릭터 해제
        /// </summary>
        public void ClearCharacter()
        {
            if (_currentState == SlotState.Locked) return;

            _assignedCharacter = default;
            _currentState = SlotState.Empty;

            UpdateVisual();
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;
            UpdateVisual();
        }

        /// <summary>
        /// 잠금 상태 설정
        /// </summary>
        public void SetLocked(bool locked)
        {
            if (locked)
            {
                _currentState = SlotState.Locked;
                _assignedCharacter = default;
            }
            else if (_currentState == SlotState.Locked)
            {
                _currentState = SlotState.Empty;
            }

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            UpdateBackground();
            UpdateCharacterDisplay();
            UpdatePositionIndicator();
            UpdateIcons();
            UpdateInteractable();
        }

        private void UpdateBackground()
        {
            if (_slotBackground == null) return;

            Color bgColor = _currentState switch
            {
                SlotState.Empty => _emptyColor,
                SlotState.Assigned => _isSelected ? _selectedColor : _assignedColor,
                SlotState.Locked => _lockedColor,
                _ => _emptyColor
            };

            _slotBackground.color = bgColor;
        }

        private void UpdateCharacterDisplay()
        {
            bool hasCharacter = _currentState == SlotState.Assigned &&
                                !string.IsNullOrEmpty(_assignedCharacter.InstanceId);

            // 캐릭터 썸네일
            if (_characterThumbnail != null)
            {
                _characterThumbnail.gameObject.SetActive(hasCharacter);
                // 실제 썸네일은 외부에서 설정
            }

            // 레벨 텍스트
            if (_levelText != null)
            {
                _levelText.gameObject.SetActive(hasCharacter);
                if (hasCharacter)
                {
                    _levelText.text = $"Lv.{_assignedCharacter.Level}";
                }
            }

            // 별 표시
            if (_starContainer != null)
            {
                _starContainer.gameObject.SetActive(hasCharacter);
                if (hasCharacter)
                {
                    // TODO: Rarity는 마스터 데이터에서 조회 필요
                    UpdateStarRating(1);
                }
            }

            // 전투력
            if (_combatPowerText != null)
            {
                _combatPowerText.gameObject.SetActive(hasCharacter);
                if (hasCharacter)
                {
                    // TODO: 실제 전투력 계산 로직 구현
                    int combatPower = _assignedCharacter.Level * 100;
                    _combatPowerText.text = $"{combatPower:N0}";
                }
            }

            // 제거 버튼
            if (_removeButton != null)
            {
                _removeButton.gameObject.SetActive(hasCharacter);
            }
        }

        private void UpdatePositionIndicator()
        {
            if (_positionText != null)
            {
                _positionText.text = $"{(_isFrontLine ? "F" : "B")}{(_slotIndex % 3) + 1}";
            }

            if (_formationLineIndicator != null)
            {
                _formationLineIndicator.color = _isFrontLine ? _frontLineColor : _backLineColor;
            }
        }

        private void UpdateIcons()
        {
            // 빈 슬롯 아이콘
            if (_emptyIcon != null)
            {
                _emptyIcon.gameObject.SetActive(_currentState == SlotState.Empty);
            }

            // 잠금 아이콘
            if (_lockIcon != null)
            {
                _lockIcon.gameObject.SetActive(_currentState == SlotState.Locked);
            }

            // 선택 하이라이트
            if (_selectionHighlight != null)
            {
                _selectionHighlight.gameObject.SetActive(_isSelected);
            }

            // 속성 아이콘
            if (_elementIcon != null)
            {
                bool showElement = _currentState == SlotState.Assigned &&
                                   !string.IsNullOrEmpty(_assignedCharacter.InstanceId);
                _elementIcon.gameObject.SetActive(showElement);
            }
        }

        private void UpdateInteractable()
        {
            if (_slotButton == null) return;

            // 잠금 상태가 아닐 때만 상호작용 가능
            _slotButton.interactable = _currentState != SlotState.Locked;
        }

        private void UpdateStarRating(int rarity)
        {
            if (_starContainer == null) return;

            for (int i = 0; i < _starContainer.childCount; i++)
            {
                _starContainer.GetChild(i).gameObject.SetActive(i < rarity);
            }
        }

        private void HandleSlotClick()
        {
            if (_currentState == SlotState.Locked) return;

            OnSlotClicked?.Invoke(this, _slotIndex);
        }

        private void HandleRemoveClick()
        {
            if (_currentState != SlotState.Assigned || string.IsNullOrEmpty(_assignedCharacter.InstanceId)) return;

            OnRemoveRequested?.Invoke(this, _assignedCharacter);
        }

        /// <summary>
        /// 캐릭터 썸네일 설정
        /// </summary>
        public void SetThumbnail(Sprite sprite)
        {
            if (_characterThumbnail != null && sprite != null)
            {
                _characterThumbnail.sprite = sprite;
            }
        }

        /// <summary>
        /// 속성 아이콘 설정
        /// </summary>
        public void SetElementIcon(Sprite sprite)
        {
            if (_elementIcon != null && sprite != null)
            {
                _elementIcon.sprite = sprite;
            }
        }
    }
}