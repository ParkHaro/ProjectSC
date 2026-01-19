using System;
using Cysharp.Threading.Tasks;
using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// 획득 보상 목록을 표시하는 범용 팝업.
    /// </summary>
    public class RewardPopup : PopupWidget<RewardPopup, RewardPopup.State>
    {
        #region Nested State

        /// <summary>
        /// RewardPopup 상태
        /// </summary>
        public class State : IPopupState
        {
            /// <summary>
            /// 팝업 제목
            /// </summary>
            public string Title { get; set; } = "획득 보상";

            /// <summary>
            /// 보상 목록
            /// </summary>
            public RewardInfo[] Rewards { get; set; } = Array.Empty<RewardInfo>();

            /// <summary>
            /// 닫기 콜백
            /// </summary>
            public Action OnClose { get; set; }

            /// <summary>
            /// 배경 터치로 닫기 허용 (기본: true)
            /// </summary>
            public bool AllowBackgroundDismiss => true;

            /// <summary>
            /// 유효성 검증
            /// </summary>
            public bool Validate()
            {
                if (Rewards == null || Rewards.Length == 0)
                {
                    Debug.LogWarning("[RewardPopup] Rewards가 비어있음");
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region SerializeFields

        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Transform _rewardContainer;
        [SerializeField] private RewardItem _rewardItemPrefab;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TMP_Text _confirmButtonText;
        [SerializeField] private Button _backgroundButton;

        [Header("Layout")]
        [SerializeField] private GridLayoutGroup _layoutGroup;

        [Header("Settings")]
        [SerializeField] private Vector2 _largeCellSize = new Vector2(120, 140);
        [SerializeField] private Vector2 _mediumCellSize = new Vector2(80, 100);
        [SerializeField] private Vector2 _smallCellSize = new Vector2(60, 80);

        #endregion

        #region Private Fields

        private State _currentState;
        private IItemSpawner<RewardItem> _itemSpawner;
        private RewardIconCache _iconCache;

        #endregion

        #region Lifecycle

        protected override void OnInitialize()
        {
            // 버튼 연결
            if (_confirmButton != null)
            {
                _confirmButton.onClick.AddListener(OnConfirmClicked);
            }

            if (_backgroundButton != null)
            {
                _backgroundButton.onClick.AddListener(OnBackgroundClicked);
            }

            // Spawner 초기화
            if (_rewardItemPrefab != null)
            {
                _itemSpawner = new SimpleItemSpawner<RewardItem>(_rewardItemPrefab);
            }

            // 아이콘 캐시 초기화
            _iconCache = new RewardIconCache();
        }

        protected override void OnBind(State state)
        {
            _currentState = state ?? new State();

            if (!_currentState.Validate())
            {
                // 유효하지 않은 상태 → 닫기
                NavigationManager.Instance?.Pop();
                return;
            }

            // 비동기 UI 갱신
            RefreshUIAsync().Forget();
        }

        public override State GetState() => _currentState;

        protected override void OnRelease()
        {
            // 아이템 정리
            _itemSpawner?.DespawnAll();

            // 아이콘 캐시 해제
            _iconCache?.Release();

            _currentState = null;
        }

        #endregion

        #region UI Refresh

        private async UniTaskVoid RefreshUIAsync()
        {
            // 1. 제목 설정
            if (_titleText != null)
            {
                _titleText.text = _currentState.Title;
            }

            // 2. 기존 아이템 정리
            _itemSpawner?.DespawnAll();

            // 3. 레이아웃 설정
            ConfigureLayout(_currentState.Rewards.Length);

            // 4. 아이콘 프리로드
            if (_iconCache != null)
            {
                await _iconCache.PreloadAsync(_currentState.Rewards);
            }

            // 5. 보상 아이템 생성
            SpawnRewardItems();
        }

        private void ConfigureLayout(int rewardCount)
        {
            if (_layoutGroup == null) return;

            if (rewardCount <= 4)
            {
                // 대형 (1줄)
                _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                _layoutGroup.constraintCount = 1;
                _layoutGroup.cellSize = _largeCellSize;
            }
            else if (rewardCount <= 8)
            {
                // 중형 (2줄)
                _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _layoutGroup.constraintCount = 4;
                _layoutGroup.cellSize = _mediumCellSize;
            }
            else
            {
                // 소형 (스크롤)
                _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _layoutGroup.constraintCount = 4;
                _layoutGroup.cellSize = _smallCellSize;
            }
        }

        private void SpawnRewardItems()
        {
            if (_itemSpawner == null || _rewardContainer == null) return;

            foreach (var reward in _currentState.Rewards)
            {
                var item = _itemSpawner.Spawn(_rewardContainer);
                if (item == null) continue;

                // 아이콘 조회 (캐시에서)
                var icon = _iconCache?.GetIcon(reward);

                // 바인딩
                item.Bind(reward, icon);
            }
        }

        #endregion

        #region Event Handlers

        private void OnConfirmClicked()
        {
            _currentState?.OnClose?.Invoke();
            NavigationManager.Instance?.Pop();
        }

        private void OnBackgroundClicked()
        {
            // State에서 배경 닫기 허용 여부 확인
            if (_currentState != null && !_currentState.AllowBackgroundDismiss)
            {
                return;
            }

            // 확인과 동일하게 처리
            OnConfirmClicked();
        }

        /// <summary>
        /// ESC 키 처리
        /// </summary>
        public override bool OnEscape()
        {
            OnConfirmClicked();
            return false;
        }

        #endregion
    }
}
