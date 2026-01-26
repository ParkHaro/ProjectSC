using System;
using System.Collections.Generic;
using Sc.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 캐릭터 스탯 데이터
    /// </summary>
    public struct CharacterStatData
    {
        public int HP;
        public int SP;
        public int PhysicalAttack;
        public int MagicAttack;
        public int PhysicalDefense;
        public int MagicDefense;
        public float CritRate;
        public float CritDamage;
        public int Speed;
    }

    /// <summary>
    /// 스탯 행 데이터
    /// </summary>
    public struct StatRowData
    {
        public string Label;
        public string Value;
        public Sprite Icon;
    }

    /// <summary>
    /// 캐릭터 스탯 위젯.
    /// 캐릭터 상세 화면의 우측 중앙 영역에 표시.
    /// - 스테이터스/특성 탭 전환
    /// - HP, SP, 물리/마법 공격력, 물리/마법 방어력 등 스탯 목록
    /// - 즐겨찾기, 정보 버튼
    /// - 상세 보기 버튼
    /// </summary>
    public class CharacterStatWidget : Widget
    {
        [Header("Tab Group")] [SerializeField] private Button _statusTab;
        [SerializeField] private TMP_Text _statusTabText;
        [SerializeField] private Image _statusTabBg;
        [SerializeField] private Button _traitTab;
        [SerializeField] private TMP_Text _traitTabText;
        [SerializeField] private Image _traitTabBg;

        [Header("Stat List")] [SerializeField] private Transform _statListContainer;
        [SerializeField] private GameObject _statRowPrefab;

        [Header("Stat Rows (Direct References)")] [SerializeField]
        private TMP_Text _hpValue;

        [SerializeField] private TMP_Text _spValue;
        [SerializeField] private TMP_Text _physicalAttackValue;
        [SerializeField] private TMP_Text _magicAttackValue;
        [SerializeField] private TMP_Text _physicalDefenseValue;
        [SerializeField] private TMP_Text _magicDefenseValue;

        [Header("Action Buttons")] [SerializeField]
        private Button _favoriteButton;

        [SerializeField] private Image _favoriteIcon;
        [SerializeField] private Button _infoButton;

        [Header("Detail Button")] [SerializeField]
        private Button _detailButton;

        [SerializeField] private TMP_Text _detailButtonText;

        private CharacterStatData _statData;
        private bool _isFavorite;
        private int _currentTabIndex;
        private readonly List<GameObject> _dynamicStatRows = new();

        // Tab 색상
        private static readonly Color TabActiveColor = new Color32(240, 240, 240, 255);
        private static readonly Color TabInactiveColor = new Color32(100, 100, 100, 200);
        private static readonly Color TabActiveTextColor = new Color32(30, 30, 30, 255);
        private static readonly Color TabInactiveTextColor = new Color32(200, 200, 200, 255);

        /// <summary>
        /// 즐겨찾기 토글 이벤트
        /// </summary>
        public event Action<bool> OnFavoriteToggled;

        /// <summary>
        /// 정보 버튼 클릭 이벤트
        /// </summary>
        public event Action OnInfoClicked;

        /// <summary>
        /// 상세 보기 버튼 클릭 이벤트
        /// </summary>
        public event Action OnDetailClicked;

        /// <summary>
        /// 탭 변경 이벤트
        /// </summary>
        public event Action<int> OnTabChanged;

        protected override void OnInitialize()
        {
            Debug.Log("[CharacterStatWidget] OnInitialize");

            // Tab buttons
            if (_statusTab != null)
            {
                _statusTab.onClick.AddListener(() => SelectTab(0));
            }

            if (_traitTab != null)
            {
                _traitTab.onClick.AddListener(() => SelectTab(1));
            }

            // Action buttons
            if (_favoriteButton != null)
            {
                _favoriteButton.onClick.AddListener(HandleFavoriteClick);
            }

            if (_infoButton != null)
            {
                _infoButton.onClick.AddListener(HandleInfoClick);
            }

            // Detail button
            if (_detailButton != null)
            {
                _detailButton.onClick.AddListener(HandleDetailClick);
            }

            // 기본 탭 선택
            SelectTab(0);
        }

        /// <summary>
        /// 스탯 설정
        /// </summary>
        public void Configure(CharacterStatData data)
        {
            _statData = data;
            RefreshStatList();
        }

        /// <summary>
        /// 즐겨찾기 상태 설정
        /// </summary>
        public void SetFavorite(bool isFavorite)
        {
            _isFavorite = isFavorite;
            UpdateFavoriteUI();
        }

        /// <summary>
        /// 탭 선택
        /// </summary>
        public void SelectTab(int index)
        {
            if (_currentTabIndex == index) return;

            _currentTabIndex = index;
            UpdateTabUI();
            RefreshStatList();

            OnTabChanged?.Invoke(index);
        }

        private void UpdateTabUI()
        {
            // 스테이터스 탭
            if (_statusTabBg != null)
            {
                _statusTabBg.color = _currentTabIndex == 0 ? TabActiveColor : TabInactiveColor;
            }

            if (_statusTabText != null)
            {
                _statusTabText.color = _currentTabIndex == 0 ? TabActiveTextColor : TabInactiveTextColor;
            }

            // 특성 탭
            if (_traitTabBg != null)
            {
                _traitTabBg.color = _currentTabIndex == 1 ? TabActiveColor : TabInactiveColor;
            }

            if (_traitTabText != null)
            {
                _traitTabText.color = _currentTabIndex == 1 ? TabActiveTextColor : TabInactiveTextColor;
            }
        }

        private void RefreshStatList()
        {
            if (_currentTabIndex == 0)
            {
                RefreshStatusTab();
            }
            else
            {
                RefreshTraitTab();
            }
        }

        private void RefreshStatusTab()
        {
            // Direct references 사용
            if (_hpValue != null) _hpValue.text = _statData.HP.ToString("N0");
            if (_spValue != null) _spValue.text = _statData.SP.ToString("N0");
            if (_physicalAttackValue != null) _physicalAttackValue.text = _statData.PhysicalAttack.ToString("N0");
            if (_magicAttackValue != null) _magicAttackValue.text = _statData.MagicAttack.ToString("N0");
            if (_physicalDefenseValue != null) _physicalDefenseValue.text = _statData.PhysicalDefense.ToString("N0");
            if (_magicDefenseValue != null) _magicDefenseValue.text = _statData.MagicDefense.ToString("N0");

            // Show stat list, hide trait list
            if (_statListContainer != null)
            {
                _statListContainer.gameObject.SetActive(true);
            }
        }

        private void RefreshTraitTab()
        {
            // 특성 탭은 추후 구현 (스킬 관련 특성 등)
            // 현재는 빈 상태로 표시
            ClearDynamicRows();

            // TODO: 특성 데이터 표시
        }

        private void ClearDynamicRows()
        {
            foreach (var row in _dynamicStatRows)
            {
                if (row != null)
                {
                    Destroy(row);
                }
            }

            _dynamicStatRows.Clear();
        }

        private void UpdateFavoriteUI()
        {
            if (_favoriteIcon != null)
            {
                _favoriteIcon.color = _isFavorite
                    ? new Color32(255, 100, 100, 255) // 빨간색 (활성)
                    : new Color32(200, 200, 200, 255); // 회색 (비활성)
            }
        }

        #region Event Handlers

        private void HandleFavoriteClick()
        {
            _isFavorite = !_isFavorite;
            UpdateFavoriteUI();
            OnFavoriteToggled?.Invoke(_isFavorite);
        }

        private void HandleInfoClick()
        {
            OnInfoClicked?.Invoke();
        }

        private void HandleDetailClick()
        {
            OnDetailClicked?.Invoke();
        }

        #endregion

        protected override void OnRelease()
        {
            ClearDynamicRows();

            if (_statusTab != null) _statusTab.onClick.RemoveAllListeners();
            if (_traitTab != null) _traitTab.onClick.RemoveAllListeners();
            if (_favoriteButton != null) _favoriteButton.onClick.RemoveAllListeners();
            if (_infoButton != null) _infoButton.onClick.RemoveAllListeners();
            if (_detailButton != null) _detailButton.onClick.RemoveAllListeners();

            OnFavoriteToggled = null;
            OnInfoClicked = null;
            OnDetailClicked = null;
            OnTabChanged = null;
        }
    }
}