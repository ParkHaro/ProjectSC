using System;
using System.Collections.Generic;
using Sc.Contents.Character.Widgets;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 캐릭터 선택 위젯.
    /// 파티 편성용 캐릭터 그리드를 표시합니다.
    /// 레퍼런스: Docs/Design/Reference/PartySelect.jpg - 우측 캐릭터 선택 영역
    /// </summary>
    public class CharacterSelectWidget : MonoBehaviour
    {
        /// <summary>
        /// 정렬 타입
        /// </summary>
        public enum SortType
        {
            CombatPower,    // 전투력
            Level,          // 레벨
            Rarity,         // 희귀도
            Element,        // 속성
            Name            // 이름
        }

        [Header("Tab Bar")]
        [SerializeField] private Button _rentalTab;
        [SerializeField] private TMP_Text _rentalTabText;
        [SerializeField] private Button _filterButton;
        [SerializeField] private TMP_Text _filterButtonText;
        [SerializeField] private Button _sortButton;
        [SerializeField] private TMP_Text _sortButtonText;
        [SerializeField] private Button _sortOrderButton;
        [SerializeField] private Image _sortOrderIcon;

        [Header("Character Grid")]
        [SerializeField] private Transform _characterGridContainer;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _characterSlotPrefab;

        [Header("Empty State")]
        [SerializeField] private GameObject _emptyStateObject;
        [SerializeField] private TMP_Text _emptyStateText;

        [Header("Colors")]
        [SerializeField] private Color _tabActiveColor = new Color32(100, 180, 100, 255);
        [SerializeField] private Color _tabInactiveColor = new Color32(60, 60, 60, 200);
        [SerializeField] private Color _assignedBorderColor = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _normalBorderColor = new Color32(80, 80, 80, 255);

        private readonly List<CharacterSelectSlot> _characterSlots = new();
        private readonly HashSet<string> _assignedCharacterIds = new();
        private List<OwnedCharacter> _allCharacters = new();
        private List<OwnedCharacter> _filteredCharacters = new();

        private bool _isRentalMode;
        private bool _isFilterOn;
        private SortType _currentSortType = SortType.CombatPower;
        private bool _isAscending;
        private ElementType? _elementFilter;

        /// <summary>
        /// 캐릭터 선택 이벤트
        /// </summary>
        public event Action<OwnedCharacter> OnCharacterSelected;

        /// <summary>
        /// 캐릭터 상세보기 요청 이벤트 (길게 누르기)
        /// </summary>
        public event Action<OwnedCharacter> OnCharacterDetailRequested;

        /// <summary>
        /// 대여 탭 클릭 이벤트
        /// </summary>
        public event Action OnRentalTabClicked;

        /// <summary>
        /// 필터 버튼 클릭 이벤트
        /// </summary>
        public event Action OnFilterClicked;

        private void Awake()
        {
            SetupButtons();
        }

        private void OnDestroy()
        {
            CleanupButtons();
            ClearSlots();
        }

        private void SetupButtons()
        {
            if (_rentalTab != null)
                _rentalTab.onClick.AddListener(HandleRentalTabClick);

            if (_filterButton != null)
                _filterButton.onClick.AddListener(HandleFilterClick);

            if (_sortButton != null)
                _sortButton.onClick.AddListener(HandleSortClick);

            if (_sortOrderButton != null)
                _sortOrderButton.onClick.AddListener(HandleSortOrderClick);
        }

        private void CleanupButtons()
        {
            if (_rentalTab != null)
                _rentalTab.onClick.RemoveListener(HandleRentalTabClick);

            if (_filterButton != null)
                _filterButton.onClick.RemoveListener(HandleFilterClick);

            if (_sortButton != null)
                _sortButton.onClick.RemoveListener(HandleSortClick);

            if (_sortOrderButton != null)
                _sortOrderButton.onClick.RemoveListener(HandleSortOrderClick);
        }

        /// <summary>
        /// 위젯 초기화
        /// </summary>
        /// <param name="characters">표시할 캐릭터 목록</param>
        public void Initialize(List<OwnedCharacter> characters)
        {
            _allCharacters = characters ?? new List<OwnedCharacter>();
            _assignedCharacterIds.Clear();

            ApplyFilterAndSort();
            RefreshUI();
        }

        /// <summary>
        /// 캐릭터 목록 갱신
        /// </summary>
        public void RefreshCharacters(List<OwnedCharacter> characters)
        {
            _allCharacters = characters ?? new List<OwnedCharacter>();
            ApplyFilterAndSort();
            RefreshGrid();
        }

        /// <summary>
        /// 편성된 캐릭터 ID 설정
        /// </summary>
        public void SetAssignedCharacters(IEnumerable<string> characterIds)
        {
            _assignedCharacterIds.Clear();
            if (characterIds != null)
            {
                foreach (var id in characterIds)
                {
                    _assignedCharacterIds.Add(id);
                }
            }

            UpdateAssignedStates();
        }

        /// <summary>
        /// 편성 상태 추가
        /// </summary>
        public void AddAssignedCharacter(string characterId)
        {
            if (string.IsNullOrEmpty(characterId)) return;

            _assignedCharacterIds.Add(characterId);
            UpdateAssignedStates();
        }

        /// <summary>
        /// 편성 상태 제거
        /// </summary>
        public void RemoveAssignedCharacter(string characterId)
        {
            if (string.IsNullOrEmpty(characterId)) return;

            _assignedCharacterIds.Remove(characterId);
            UpdateAssignedStates();
        }

        /// <summary>
        /// 속성 필터 설정
        /// </summary>
        public void SetElementFilter(ElementType? element)
        {
            _elementFilter = element;
            _isFilterOn = element.HasValue;

            ApplyFilterAndSort();
            RefreshGrid();
            UpdateTabUI();
        }

        /// <summary>
        /// 정렬 타입 설정
        /// </summary>
        public void SetSortType(SortType sortType)
        {
            _currentSortType = sortType;

            ApplyFilterAndSort();
            RefreshGrid();
            UpdateTabUI();
        }

        /// <summary>
        /// 정렬 순서 토글
        /// </summary>
        public void ToggleSortOrder()
        {
            _isAscending = !_isAscending;

            ApplyFilterAndSort();
            RefreshGrid();
            UpdateTabUI();
        }

        private void ApplyFilterAndSort()
        {
            // 필터 적용
            _filteredCharacters = new List<OwnedCharacter>();
            foreach (var character in _allCharacters)
            {
                if (_elementFilter.HasValue)
                {
                    // 속성 필터 (실제 구현 시 CharacterData에서 Element 확인)
                    // 현재는 placeholder
                }

                _filteredCharacters.Add(character);
            }

            // 정렬 적용
            _filteredCharacters.Sort((a, b) =>
            {
                // TODO: CombatPower, Rarity는 마스터 데이터 연동 필요
                int comparison = _currentSortType switch
                {
                    SortType.CombatPower => (a.Level * 100).CompareTo(b.Level * 100), // 임시: Level 기반
                    SortType.Level => a.Level.CompareTo(b.Level),
                    SortType.Rarity => a.Level.CompareTo(b.Level), // 임시: Level로 대체
                    SortType.Name => string.Compare(a.InstanceId, b.InstanceId, StringComparison.Ordinal),
                    _ => a.Level.CompareTo(b.Level)
                };

                return _isAscending ? comparison : -comparison;
            });
        }

        private void RefreshUI()
        {
            RefreshGrid();
            UpdateTabUI();
        }

        private void RefreshGrid()
        {
            ClearSlots();

            if (_filteredCharacters.Count == 0)
            {
                ShowEmptyState(true);
                return;
            }

            ShowEmptyState(false);

            // 슬롯 생성
            foreach (var character in _filteredCharacters)
            {
                CreateCharacterSlot(character);
            }
        }

        private void CreateCharacterSlot(OwnedCharacter character)
        {
            if (_characterGridContainer == null) return;

            // Prefab 인스턴스화 또는 동적 생성
            GameObject slotObj;
            if (_characterSlotPrefab != null)
            {
                slotObj = Instantiate(_characterSlotPrefab, _characterGridContainer);
            }
            else
            {
                slotObj = new GameObject($"CharacterSlot_{character.InstanceId}");
                slotObj.transform.SetParent(_characterGridContainer, false);
                slotObj.AddComponent<RectTransform>();
            }

            var slot = slotObj.GetComponent<CharacterSelectSlot>();
            if (slot == null)
            {
                slot = slotObj.AddComponent<CharacterSelectSlot>();
            }

            bool isAssigned = _assignedCharacterIds.Contains(character.InstanceId);
            slot.Configure(character, isAssigned);
            slot.OnClicked += HandleCharacterSlotClicked;
            slot.OnLongPressed += HandleCharacterSlotLongPressed;

            _characterSlots.Add(slot);
        }

        private void ClearSlots()
        {
            foreach (var slot in _characterSlots)
            {
                if (slot != null)
                {
                    slot.OnClicked -= HandleCharacterSlotClicked;
                    slot.OnLongPressed -= HandleCharacterSlotLongPressed;
                    Destroy(slot.gameObject);
                }
            }

            _characterSlots.Clear();
        }

        private void UpdateAssignedStates()
        {
            foreach (var slot in _characterSlots)
            {
                if (slot != null && !string.IsNullOrEmpty(slot.Character.InstanceId))
                {
                    bool isAssigned = _assignedCharacterIds.Contains(slot.Character.InstanceId);
                    slot.SetAssigned(isAssigned);
                }
            }
        }

        private void UpdateTabUI()
        {
            // 대여 탭
            if (_rentalTab != null)
            {
                var rentalImage = _rentalTab.GetComponent<Image>();
                if (rentalImage != null)
                {
                    rentalImage.color = _isRentalMode ? _tabActiveColor : _tabInactiveColor;
                }
            }

            // 필터 버튼
            if (_filterButtonText != null)
            {
                _filterButtonText.text = _isFilterOn ? "필터 ON" : "필터 OFF";
            }

            // 정렬 버튼
            if (_sortButtonText != null)
            {
                _sortButtonText.text = GetSortTypeText(_currentSortType);
            }

            // 정렬 순서 아이콘
            if (_sortOrderIcon != null)
            {
                _sortOrderIcon.transform.localRotation = Quaternion.Euler(0, 0, _isAscending ? 180 : 0);
            }
        }

        private void ShowEmptyState(bool show)
        {
            if (_emptyStateObject != null)
            {
                _emptyStateObject.SetActive(show);
            }

            if (_emptyStateText != null && show)
            {
                _emptyStateText.text = _isFilterOn
                    ? "필터 조건에 맞는 캐릭터가 없습니다."
                    : "보유한 캐릭터가 없습니다.";
            }
        }

        private string GetSortTypeText(SortType sortType)
        {
            return sortType switch
            {
                SortType.CombatPower => "전투력",
                SortType.Level => "레벨",
                SortType.Rarity => "희귀도",
                SortType.Element => "속성",
                SortType.Name => "이름",
                _ => "정렬"
            };
        }

        #region Event Handlers

        private void HandleRentalTabClick()
        {
            _isRentalMode = !_isRentalMode;
            UpdateTabUI();
            OnRentalTabClicked?.Invoke();
        }

        private void HandleFilterClick()
        {
            OnFilterClicked?.Invoke();
        }

        private void HandleSortClick()
        {
            // 정렬 타입 순환
            _currentSortType = _currentSortType switch
            {
                SortType.CombatPower => SortType.Level,
                SortType.Level => SortType.Rarity,
                SortType.Rarity => SortType.Element,
                SortType.Element => SortType.Name,
                SortType.Name => SortType.CombatPower,
                _ => SortType.CombatPower
            };

            ApplyFilterAndSort();
            RefreshGrid();
            UpdateTabUI();
        }

        private void HandleSortOrderClick()
        {
            ToggleSortOrder();
        }

        private void HandleCharacterSlotClicked(OwnedCharacter character)
        {
            OnCharacterSelected?.Invoke(character);
        }

        private void HandleCharacterSlotLongPressed(OwnedCharacter character)
        {
            OnCharacterDetailRequested?.Invoke(character);
        }

        #endregion

        /// <summary>
        /// 현재 정렬 타입
        /// </summary>
        public SortType CurrentSortType => _currentSortType;

        /// <summary>
        /// 오름차순 여부
        /// </summary>
        public bool IsAscending => _isAscending;

        /// <summary>
        /// 대여 모드 여부
        /// </summary>
        public bool IsRentalMode => _isRentalMode;
    }

    /// <summary>
    /// 캐릭터 선택 슬롯.
    /// CharacterSelectWidget의 개별 아이템.
    /// </summary>
    public class CharacterSelectSlot : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField] private Image _cardBackground;
        [SerializeField] private Image _characterPortrait;
        [SerializeField] private Image _elementIcon;
        [SerializeField] private Image _assignedBorder;
        [SerializeField] private Image _assignedBadge;

        [Header("Info")]
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Transform _starContainer;
        [SerializeField] private TMP_Text _combatPowerText;

        [Header("Interaction")]
        [SerializeField] private Button _button;
        [SerializeField] private Button _detailButton;

        [Header("Colors")]
        [SerializeField] private Color _normalColor = new Color32(60, 60, 60, 255);
        [SerializeField] private Color _assignedColor = new Color32(60, 100, 60, 255);

        private OwnedCharacter _character;
        private bool _isAssigned;
        private float _pressStartTime;
        private const float LONG_PRESS_DURATION = 0.5f;

        /// <summary>
        /// 슬롯 클릭 이벤트
        /// </summary>
        public event Action<OwnedCharacter> OnClicked;

        /// <summary>
        /// 길게 누르기 이벤트
        /// </summary>
        public event Action<OwnedCharacter> OnLongPressed;

        /// <summary>
        /// 바인딩된 캐릭터
        /// </summary>
        public OwnedCharacter Character => _character;

        /// <summary>
        /// 편성 상태
        /// </summary>
        public bool IsAssigned => _isAssigned;

        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }

            if (_detailButton != null)
            {
                _detailButton.onClick.AddListener(HandleDetailClick);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }

            if (_detailButton != null)
            {
                _detailButton.onClick.RemoveListener(HandleDetailClick);
            }
        }

        /// <summary>
        /// 슬롯 구성
        /// </summary>
        public void Configure(OwnedCharacter character, bool isAssigned = false)
        {
            _character = character;
            _isAssigned = isAssigned;

            UpdateVisual();
        }

        /// <summary>
        /// 편성 상태 설정
        /// </summary>
        public void SetAssigned(bool assigned)
        {
            _isAssigned = assigned;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (string.IsNullOrEmpty(_character.InstanceId)) return;

            // 배경 색상
            if (_cardBackground != null)
            {
                _cardBackground.color = _isAssigned ? _assignedColor : _normalColor;
            }

            // 레벨
            if (_levelText != null)
            {
                _levelText.text = $"Lv.{_character.Level}";
            }

            // 전투력
            if (_combatPowerText != null)
            {
                // TODO: 실제 전투력 계산 로직 구현
                int combatPower = _character.Level * 100;
                _combatPowerText.text = $"{combatPower:N0}";
            }

            // 별 표시 (TODO: 마스터 데이터에서 Rarity 조회)
            UpdateStarRating(1);

            // 편성 표시
            if (_assignedBorder != null)
            {
                _assignedBorder.gameObject.SetActive(_isAssigned);
            }

            if (_assignedBadge != null)
            {
                _assignedBadge.gameObject.SetActive(_isAssigned);
            }
        }

        private void UpdateStarRating(int rarity)
        {
            if (_starContainer == null) return;

            for (int i = 0; i < _starContainer.childCount; i++)
            {
                _starContainer.GetChild(i).gameObject.SetActive(i < rarity);
            }
        }

        private void HandleClick()
        {
            if (!string.IsNullOrEmpty(_character.InstanceId))
            {
                OnClicked?.Invoke(_character);
            }
        }

        private void HandleDetailClick()
        {
            if (!string.IsNullOrEmpty(_character.InstanceId))
            {
                OnLongPressed?.Invoke(_character);
            }
        }

        /// <summary>
        /// 캐릭터 초상화 설정
        /// </summary>
        public void SetPortrait(Sprite sprite)
        {
            if (_characterPortrait != null && sprite != null)
            {
                _characterPortrait.sprite = sprite;
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
                _elementIcon.gameObject.SetActive(true);
            }
        }
    }
}
