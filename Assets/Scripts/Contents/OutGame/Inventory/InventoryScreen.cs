using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Common.UI.Widgets;
using Sc.Contents.Inventory.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Inventory
{
    /// <summary>
    /// 인벤토리 화면
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.Standard)]
    public class InventoryScreen : ScreenWidget<InventoryScreen, InventoryScreen.InventoryState>
    {
        /// <summary>
        /// 인벤토리 화면 상태
        /// </summary>
        public class InventoryState : IScreenState
        {
            /// <summary>
            /// 초기 선택 카테고리
            /// </summary>
            public InventoryCategory InitialCategory { get; set; } = InventoryCategory.Usage;

            /// <summary>
            /// 초기 선택 아이템 ID
            /// </summary>
            public string InitialItemId { get; set; }
        }

        [Header("Tab")]
        [SerializeField] private InventoryTabWidget _tabWidget;

        [Header("Filter Bar")]
        [SerializeField] private TMP_Dropdown _categoryDropdown;
        [SerializeField] private TMP_Dropdown _sortDropdown;
        [SerializeField] private Button _settingsButton;

        [Header("Item Grid")]
        [SerializeField] private Transform _itemGridContainer;
        [SerializeField] private ItemCard _itemCardPrefab;
        [SerializeField] private ScrollRect _itemScrollRect;

        [Header("Detail Panel")]
        [SerializeField] private ItemDetailWidget _itemDetailWidget;

        [Header("Empty State")]
        [SerializeField] private GameObject _emptyStateObject;
        [SerializeField] private TMP_Text _emptyStateText;

        [Header("Navigation")]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _homeButton;

        private InventoryState _currentState;
        private InventoryCategory _currentCategory;
        private readonly List<ItemCard> _itemCards = new();
        private ItemCard _selectedCard;
        private List<ItemData> _allItems;
        private Dictionary<string, int> _itemCounts;

        // 정렬/필터 상태
        private string _currentSubCategory = "전체";
        private InventorySortType _currentSortType = InventorySortType.Default;

        protected override void OnInitialize()
        {
            Debug.Log("[InventoryScreen] OnInitialize");

            // 탭 위젯 설정
            if (_tabWidget != null)
            {
                _tabWidget.SetOnTabChanged(OnTabChanged);
            }

            // 네비게이션 버튼
            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_homeButton != null)
            {
                _homeButton.onClick.AddListener(OnHomeClicked);
            }

            // 필터 드롭다운
            if (_categoryDropdown != null)
            {
                _categoryDropdown.onValueChanged.AddListener(OnCategoryFilterChanged);
            }

            if (_sortDropdown != null)
            {
                _sortDropdown.onValueChanged.AddListener(OnSortChanged);
            }

            // 설정 버튼
            if (_settingsButton != null)
            {
                _settingsButton.onClick.AddListener(OnSettingsClicked);
            }

            // 아이템 상세 위젯 콜백
            if (_itemDetailWidget != null)
            {
                _itemDetailWidget.SetCallbacks(OnItemUse, OnItemSell);
            }

            InitializeDropdowns();
        }

        private void InitializeDropdowns()
        {
            // 카테고리 드롭다운 초기화
            if (_categoryDropdown != null)
            {
                _categoryDropdown.ClearOptions();
                _categoryDropdown.AddOptions(new List<string> { "전체", "소모품", "강화 재료", "기타" });
            }

            // 정렬 드롭다운 초기화
            if (_sortDropdown != null)
            {
                _sortDropdown.ClearOptions();
                _sortDropdown.AddOptions(new List<string> { "기본", "이름순", "등급순", "최근순" });
            }
        }

        protected override void OnBind(InventoryState state)
        {
            _currentState = state ?? new InventoryState();
            _currentCategory = _currentState.InitialCategory;

            Debug.Log($"[InventoryScreen] OnBind - Category: {_currentCategory}");

            // Header 설정
            ScreenHeader.Instance?.Configure("inventory");

            // 탭 선택
            _tabWidget?.SelectCategory(_currentCategory);
        }

        protected override void OnShow()
        {
            Debug.Log("[InventoryScreen] OnShow");

            // DataManager 이벤트 구독
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged += OnUserDataChanged;
            }

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            LoadInventoryData();
            RefreshItemGrid();
        }

        protected override void OnHide()
        {
            Debug.Log("[InventoryScreen] OnHide");

            // DataManager 이벤트 해제
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged -= OnUserDataChanged;
            }

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override InventoryState GetState() => _currentState;

        #region Data Loading

        private void LoadInventoryData()
        {
            // TODO: DataManager에서 인벤토리 데이터 로드
            // _allItems = DataManager.Instance?.GetAllItems();
            // _itemCounts = DataManager.Instance?.GetItemCounts();

            // 임시 더미 데이터
            _allItems = new List<ItemData>();
            _itemCounts = new Dictionary<string, int>();
        }

        private void OnUserDataChanged()
        {
            LoadInventoryData();
            RefreshItemGrid();
        }

        #endregion

        #region Tab Handling

        private void OnTabChanged(InventoryCategory category)
        {
            Debug.Log($"[InventoryScreen] Tab changed to: {category}");
            _currentCategory = category;

            // 선택 해제
            ClearSelection();

            // 그리드 새로고침
            RefreshItemGrid();
        }

        #endregion

        #region Item Grid

        private void RefreshItemGrid()
        {
            ClearItemCards();

            var filteredItems = GetFilteredItems();

            if (filteredItems == null || filteredItems.Count == 0)
            {
                ShowEmptyState("보유한 아이템이 없습니다.");
                return;
            }

            HideEmptyState();

            var sortedItems = SortItems(filteredItems);

            foreach (var item in sortedItems)
            {
                int count = _itemCounts.TryGetValue(item.Id, out int c) ? c : 0;
                CreateItemCard(item, count);
            }

            // 스크롤 초기화
            if (_itemScrollRect != null)
            {
                _itemScrollRect.verticalNormalizedPosition = 1f;
            }

            // 초기 아이템 선택
            if (!string.IsNullOrEmpty(_currentState?.InitialItemId))
            {
                SelectItemById(_currentState.InitialItemId);
                _currentState.InitialItemId = null; // 한 번만 적용
            }
        }

        private List<ItemData> GetFilteredItems()
        {
            if (_allItems == null) return new List<ItemData>();

            var filtered = new List<ItemData>();

            foreach (var item in _allItems)
            {
                // 카테고리 필터
                if (!MatchesCategory(item, _currentCategory))
                    continue;

                // 서브 카테고리 필터
                if (!MatchesSubCategory(item, _currentSubCategory))
                    continue;

                filtered.Add(item);
            }

            return filtered;
        }

        private bool MatchesCategory(ItemData item, InventoryCategory category)
        {
            return category switch
            {
                InventoryCategory.Usage => item.Type == ItemType.Consumable,
                InventoryCategory.Growth => item.Type == ItemType.Material || item.IsExpMaterial,
                InventoryCategory.Equipment => item.Type == ItemType.Weapon || item.Type == ItemType.Armor || item.Type == ItemType.Accessory,
                InventoryCategory.Guild => false, // TODO: 길드 아이템 타입 추가 필요
                InventoryCategory.Card => false,  // TODO: 카드 아이템 타입 추가 필요
                _ => true
            };
        }

        private bool MatchesSubCategory(ItemData item, string subCategory)
        {
            if (string.IsNullOrEmpty(subCategory) || subCategory == "전체")
                return true;

            // TODO: 서브 카테고리 필터 구현
            return true;
        }

        private List<ItemData> SortItems(List<ItemData> items)
        {
            var sorted = new List<ItemData>(items);

            switch (_currentSortType)
            {
                case InventorySortType.Name:
                    sorted.Sort((a, b) => string.Compare(a.Name, b.Name, System.StringComparison.Ordinal));
                    break;

                case InventorySortType.Rarity:
                    sorted.Sort((a, b) => b.Rarity.CompareTo(a.Rarity));
                    break;

                case InventorySortType.Recent:
                    // TODO: 획득 시간 기준 정렬
                    break;

                case InventorySortType.Default:
                default:
                    // 기본: 등급 > 이름
                    sorted.Sort((a, b) =>
                    {
                        int rarityCompare = b.Rarity.CompareTo(a.Rarity);
                        return rarityCompare != 0 ? rarityCompare : string.Compare(a.Name, b.Name, System.StringComparison.Ordinal);
                    });
                    break;
            }

            return sorted;
        }

        private void CreateItemCard(ItemData itemData, int count)
        {
            if (_itemCardPrefab == null || _itemGridContainer == null) return;

            var cardGo = Instantiate(_itemCardPrefab, _itemGridContainer);
            var card = cardGo.GetComponent<ItemCard>();

            if (card != null)
            {
                card.Setup(itemData, count, OnItemCardClicked);
                _itemCards.Add(card);
            }
        }

        private void ClearItemCards()
        {
            foreach (var card in _itemCards)
            {
                if (card != null)
                {
                    Destroy(card.gameObject);
                }
            }

            _itemCards.Clear();
            _selectedCard = null;
        }

        private void OnItemCardClicked(ItemCard card)
        {
            Debug.Log($"[InventoryScreen] Item clicked: {card.ItemData?.Name}");

            // 이전 선택 해제
            _selectedCard?.SetSelected(false);

            // 새 선택
            _selectedCard = card;
            card.SetSelected(true);

            // 상세 패널 업데이트
            _itemDetailWidget?.ShowItem(card.ItemData, card.Count);
        }

        private void ClearSelection()
        {
            _selectedCard?.SetSelected(false);
            _selectedCard = null;
            _itemDetailWidget?.ClearSelection();
        }

        private void SelectItemById(string itemId)
        {
            foreach (var card in _itemCards)
            {
                if (card.ItemData?.Id == itemId)
                {
                    OnItemCardClicked(card);
                    break;
                }
            }
        }

        #endregion

        #region Filter & Sort

        private void OnCategoryFilterChanged(int index)
        {
            _currentSubCategory = _categoryDropdown.options[index].text;
            RefreshItemGrid();
        }

        private void OnSortChanged(int index)
        {
            _currentSortType = (InventorySortType)index;
            RefreshItemGrid();
        }

        private void OnSettingsClicked()
        {
            Debug.Log("[InventoryScreen] Settings clicked");
            // TODO: FilterSettingsPopup 열기
        }

        #endregion

        #region Item Actions

        private void OnItemUse(ItemData item)
        {
            Debug.Log($"[InventoryScreen] Use item: {item.Name}");

            if (item.IsExpMaterial)
            {
                // TODO: 캐릭터 선택 화면으로 이동
                Debug.Log("[InventoryScreen] Opening character selection for exp material");
            }
            else if (item.IsConsumable)
            {
                // TODO: 아이템 사용 확인 팝업
                Debug.Log("[InventoryScreen] Opening item use confirm popup");
            }
        }

        private void OnItemSell(ItemData item)
        {
            Debug.Log($"[InventoryScreen] Sell item: {item.Name}");
            // TODO: 아이템 판매 확인 팝업
        }

        #endregion

        #region Empty State

        private void ShowEmptyState(string message)
        {
            if (_emptyStateObject != null)
            {
                _emptyStateObject.SetActive(true);
            }

            if (_emptyStateText != null)
            {
                _emptyStateText.text = message;
            }
        }

        private void HideEmptyState()
        {
            if (_emptyStateObject != null)
            {
                _emptyStateObject.SetActive(false);
            }
        }

        #endregion

        #region Navigation

        private void OnBackClicked()
        {
            Debug.Log("[InventoryScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHomeClicked()
        {
            Debug.Log("[InventoryScreen] Home clicked");
            // TODO: LobbyScreen으로 이동
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        #endregion

        protected override void OnRelease()
        {
            if (_tabWidget != null)
            {
                _tabWidget.SetOnTabChanged(null);
            }

            if (_categoryDropdown != null)
            {
                _categoryDropdown.onValueChanged.RemoveListener(OnCategoryFilterChanged);
            }

            if (_sortDropdown != null)
            {
                _sortDropdown.onValueChanged.RemoveListener(OnSortChanged);
            }

            if (_settingsButton != null)
            {
                _settingsButton.onClick.RemoveListener(OnSettingsClicked);
            }

            ClearItemCards();

            _allItems = null;
            _itemCounts = null;
        }
    }

    /// <summary>
    /// 인벤토리 정렬 타입
    /// </summary>
    public enum InventorySortType
    {
        Default = 0,
        Name = 1,
        Rarity = 2,
        Recent = 3
    }
}
