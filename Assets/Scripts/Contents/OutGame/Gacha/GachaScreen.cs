using System;
using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Event.UI;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 화면 State
    /// </summary>
    public class GachaState : IScreenState
    {
        /// <summary>
        /// 선택된 가챠 풀 ID
        /// </summary>
        public string SelectedPoolId = "gacha_standard";
    }

    /// <summary>
    /// 가챠 화면 - 캐릭터 소환
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.Standard)]
    public class GachaScreen : ScreenWidget<GachaScreen, GachaState>
    {
        [Header("배너 영역")]
        [SerializeField] private Transform _bannerContainer;
        [SerializeField] private GameObject _bannerItemPrefab;
        [SerializeField] private ScrollRect _bannerScrollRect;

        [Header("뽑기 버튼")]
        [SerializeField] private Button _singlePullButton;
        [SerializeField] private Button _multiPullButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _singleCostText;
        [SerializeField] private TMP_Text _multiCostText;

        [Header("정보 표시")]
        [SerializeField] private TMP_Text _poolNameText;
        [SerializeField] private TMP_Text _pityCountText;
        [SerializeField] private TMP_Text _gemText;
        [SerializeField] private Slider _pityProgressBar;
        [SerializeField] private TMP_Text _pityProgressText;

        [Header("추가 버튼")]
        [SerializeField] private Button _rateDetailButton;
        [SerializeField] private Button _historyButton;

        [Header("로딩")]
        [SerializeField] private GameObject _loadingIndicator;

        private GachaState _currentState;
        private bool _isPulling;
        private readonly List<GachaBannerItem> _bannerItems = new();
        private GachaPoolData _selectedPool;

        protected override void OnInitialize()
        {
            Debug.Log("[GachaScreen] OnInitialize");

            if (_singlePullButton != null)
            {
                _singlePullButton.onClick.AddListener(OnSinglePullClicked);
            }

            if (_multiPullButton != null)
            {
                _multiPullButton.onClick.AddListener(OnMultiPullClicked);
            }

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(OnBackClicked);
            }

            if (_rateDetailButton != null)
            {
                _rateDetailButton.onClick.AddListener(OnRateDetailClicked);
            }

            if (_historyButton != null)
            {
                _historyButton.onClick.AddListener(OnHistoryClicked);
            }
        }

        protected override void OnBind(GachaState state)
        {
            _currentState = state ?? new GachaState();
            Debug.Log($"[GachaScreen] OnBind - Pool: {_currentState.SelectedPoolId}");

            // Header 설정
            ScreenHeader.Instance?.Configure("gacha_main");

            // 배너 초기화
            InitializeBanners();

            // 선택된 풀로 UI 설정
            SelectPool(_currentState.SelectedPoolId);

            RefreshUI();
        }

        protected override void OnShow()
        {
            Debug.Log("[GachaScreen] OnShow");

            // DataManager 이벤트 구독
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged += OnUserDataChanged;
            }

            // 가챠 이벤트 구독
            EventManager.Instance?.Subscribe<GachaCompletedEvent>(OnGachaCompleted);
            EventManager.Instance?.Subscribe<GachaFailedEvent>(OnGachaFailed);

            // Header Back 이벤트 구독
            EventManager.Instance?.Subscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);

            HideLoading();
            RefreshUI();
        }

        protected override void OnHide()
        {
            Debug.Log("[GachaScreen] OnHide");

            // DataManager 이벤트 해제
            if (DataManager.Instance != null)
            {
                DataManager.Instance.OnUserDataChanged -= OnUserDataChanged;
            }

            // 가챠 이벤트 해제
            EventManager.Instance?.Unsubscribe<GachaCompletedEvent>(OnGachaCompleted);
            EventManager.Instance?.Unsubscribe<GachaFailedEvent>(OnGachaFailed);

            // Header Back 이벤트 해제
            EventManager.Instance?.Unsubscribe<HeaderBackClickedEvent>(OnHeaderBackClicked);
        }

        public override GachaState GetState() => _currentState;

        #region Banner Management

        private void InitializeBanners()
        {
            // 기존 배너 정리
            ClearBanners();

            if (_bannerContainer == null || _bannerItemPrefab == null) return;

            var gachaPoolDatabase = DataManager.Instance?.GachaPools;
            if (gachaPoolDatabase == null) return;

            // 활성 풀만 필터링하고 DisplayOrder로 정렬
            var activePools = new List<GachaPoolData>();
            var serverTime = DateTime.UtcNow;

            foreach (var pool in gachaPoolDatabase.GachaPools)
            {
                if (pool.IsActiveAt(serverTime))
                {
                    activePools.Add(pool);
                }
            }

            // DisplayOrder 오름차순 정렬
            activePools.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));

            // 배너 아이템 생성
            foreach (var pool in activePools)
            {
                var bannerGo = Instantiate(_bannerItemPrefab, _bannerContainer);
                var bannerItem = bannerGo.GetComponent<GachaBannerItem>();

                if (bannerItem != null)
                {
                    var isSelected = pool.Id == _currentState.SelectedPoolId;
                    bannerItem.Setup(pool, OnBannerSelected, isSelected);
                    _bannerItems.Add(bannerItem);
                }
            }
        }

        private void ClearBanners()
        {
            foreach (var item in _bannerItems)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }
            _bannerItems.Clear();
        }

        private void OnBannerSelected(string poolId)
        {
            if (poolId == _currentState.SelectedPoolId) return;

            SelectPool(poolId);
            RefreshUI();
        }

        private void SelectPool(string poolId)
        {
            _currentState.SelectedPoolId = poolId;
            _selectedPool = DataManager.Instance?.GachaPools?.GetById(poolId);

            // 배너 선택 상태 업데이트
            foreach (var bannerItem in _bannerItems)
            {
                bannerItem.SetSelected(bannerItem.PoolId == poolId);
            }

            Debug.Log($"[GachaScreen] Pool selected: {poolId}");
        }

        #endregion

        #region UI Refresh

        private void RefreshUI()
        {
            RefreshPoolInfo();
            RefreshCostButtons();
            RefreshPityProgress();
            RefreshCurrencyDisplay();
        }

        private void RefreshPoolInfo()
        {
            if (_selectedPool == null) return;

            if (_poolNameText != null)
            {
                _poolNameText.text = _selectedPool.Name;
            }
        }

        private void RefreshCostButtons()
        {
            if (_selectedPool == null) return;

            // 비용 텍스트
            if (_singleCostText != null)
            {
                _singleCostText.text = _selectedPool.CostAmount > 0
                    ? $"{_selectedPool.CostAmount:N0}"
                    : "무료";
            }

            if (_multiCostText != null)
            {
                _multiCostText.text = _selectedPool.CostAmount10 > 0
                    ? $"{_selectedPool.CostAmount10:N0}"
                    : "무료";
            }

            // 버튼 활성화 상태
            var totalGem = DataManager.Instance?.Currency.TotalGem ?? 0;
            UpdateButtonStates(totalGem);
        }

        private void RefreshPityProgress()
        {
            if (_selectedPool == null) return;

            var pityData = DataManager.Instance?.GachaPity;
            if (!pityData.HasValue) return;

            var pityInfo = pityData.Value.GetOrCreatePityInfo(_currentState.SelectedPoolId);
            var pityCount = pityInfo.PityCount;
            var pityMax = _selectedPool.PityCount;

            // 천장 카운트 텍스트
            if (_pityCountText != null)
            {
                _pityCountText.text = pityMax > 0
                    ? $"천장: {pityCount}/{pityMax}"
                    : "천장 없음";
            }

            // 프로그레스 바
            if (_pityProgressBar != null)
            {
                if (pityMax > 0)
                {
                    _pityProgressBar.gameObject.SetActive(true);
                    _pityProgressBar.value = (float)pityCount / pityMax;

                    // 소프트 천장 구간 하이라이트 (색상 변경)
                    if (_selectedPool.PitySoftStart > 0 && pityCount >= _selectedPool.PitySoftStart)
                    {
                        // 소프트 천장 구간 진입 시 색상 변경
                        var fillImage = _pityProgressBar.fillRect?.GetComponent<Image>();
                        if (fillImage != null)
                        {
                            fillImage.color = new Color(1f, 0.6f, 0f); // 주황색
                        }
                    }
                }
                else
                {
                    _pityProgressBar.gameObject.SetActive(false);
                }
            }

            // 프로그레스 텍스트 (소프트 천장 안내)
            if (_pityProgressText != null)
            {
                if (pityMax > 0 && _selectedPool.PitySoftStart > 0)
                {
                    var remainingToSoft = Math.Max(0, _selectedPool.PitySoftStart - pityCount);
                    if (remainingToSoft > 0)
                    {
                        _pityProgressText.text = $"확률 증가까지 {remainingToSoft}회";
                    }
                    else
                    {
                        var bonusPercent = _selectedPool.PitySoftRateBonus * 100f;
                        _pityProgressText.text = $"확률 증가 중! (+{bonusPercent:F1}%/회)";
                    }
                }
                else
                {
                    _pityProgressText.text = "";
                }
            }
        }

        private void RefreshCurrencyDisplay()
        {
            if (DataManager.Instance?.IsInitialized != true) return;

            var currency = DataManager.Instance.Currency;

            if (_gemText != null)
            {
                _gemText.text = $"{currency.TotalGem:N0}";
            }
        }

        private void UpdateButtonStates(int totalGem)
        {
            if (_selectedPool == null) return;

            var canSingle = !_isPulling &&
                            (_selectedPool.CostAmount == 0 || totalGem >= _selectedPool.CostAmount);
            var canMulti = !_isPulling &&
                           (_selectedPool.CostAmount10 == 0 || totalGem >= _selectedPool.CostAmount10);

            if (_singlePullButton != null)
            {
                _singlePullButton.interactable = canSingle;
            }

            if (_multiPullButton != null)
            {
                // 무료 소환은 10연차 비활성화
                _multiPullButton.interactable = canMulti && _selectedPool.CostAmount10 > 0;
            }
        }

        #endregion

        #region Pull Actions

        private void OnSinglePullClicked()
        {
            Debug.Log("[GachaScreen] Single pull clicked");
            TryExecuteGacha(GachaPullType.Single);
        }

        private void OnMultiPullClicked()
        {
            Debug.Log("[GachaScreen] Multi pull clicked");
            TryExecuteGacha(GachaPullType.Multi);
        }

        private void TryExecuteGacha(GachaPullType pullType)
        {
            if (_isPulling || _selectedPool == null) return;

            var cost = pullType == GachaPullType.Single
                ? _selectedPool.CostAmount
                : _selectedPool.CostAmount10;

            // 무료 소환이면 확인 없이 바로 실행
            if (cost == 0)
            {
                ExecuteGacha(pullType);
                return;
            }

            // 유료 소환이면 확인 팝업
            var currentGem = DataManager.Instance?.Currency.TotalGem ?? 0;
            var pullCountText = pullType == GachaPullType.Single ? "1회" : "10연차";

            CostConfirmPopup.Open(new CostConfirmState
            {
                Title = "소환 확인",
                Message = $"{_selectedPool.Name}\n{pullCountText} 소환을 진행하시겠습니까?",
                CostType = _selectedPool.CostType,
                CostAmount = cost,
                CurrentAmount = currentGem,
                ConfirmText = "소환",
                CancelText = "취소",
                OnConfirm = () => ExecuteGacha(pullType)
            });
        }

        private void ExecuteGacha(GachaPullType pullType)
        {
            if (_isPulling) return;

            if (NetworkManager.Instance == null || !NetworkManager.Instance.IsInitialized)
            {
                Debug.LogError("[GachaScreen] NetworkManager not initialized");
                return;
            }

            _isPulling = true;
            ShowLoading();
            UpdateButtonStates(DataManager.Instance?.Currency.TotalGem ?? 0);

            var request = pullType == GachaPullType.Single
                ? GachaRequest.CreateSingle(_currentState.SelectedPoolId)
                : GachaRequest.CreateMulti(_currentState.SelectedPoolId);

            Debug.Log($"[GachaScreen] Sending gacha request: {pullType}");
            NetworkManager.Instance.Send(request);
        }

        #endregion

        #region Event Handlers

        private void OnUserDataChanged()
        {
            RefreshUI();
        }

        private void OnGachaCompleted(GachaCompletedEvent evt)
        {
            Debug.Log($"[GachaScreen] Gacha completed: {evt.Results?.Count ?? 0} results");

            _isPulling = false;
            HideLoading();
            RefreshUI();

            // 결과 팝업 열기
            if (evt.Results != null && evt.Results.Count > 0)
            {
                GachaResultPopup.Open(new GachaResultState { Results = evt.Results });
            }
        }

        private void OnGachaFailed(GachaFailedEvent evt)
        {
            Debug.LogWarning($"[GachaScreen] Gacha failed: {evt.ErrorCode} - {evt.ErrorMessage}");

            _isPulling = false;
            HideLoading();
            RefreshUI();

            // TODO: 에러 팝업 표시
        }

        private void OnBackClicked()
        {
            Debug.Log("[GachaScreen] Back clicked");
            NavigationManager.Instance?.Back();
        }

        private void OnHeaderBackClicked(HeaderBackClickedEvent evt)
        {
            OnBackClicked();
        }

        private void OnRateDetailClicked()
        {
            Debug.Log("[GachaScreen] Rate detail clicked");

            if (_selectedPool == null) return;

            // RateDetailPopup 열기
            RateDetailPopup.Open(new RateDetailState
            {
                PoolData = _selectedPool
            });
        }

        private void OnHistoryClicked()
        {
            Debug.Log("[GachaScreen] History clicked");

            // GachaHistoryScreen 열기
            GachaHistoryScreen.Open(new GachaHistoryState
            {
                FilterPoolId = _currentState.SelectedPoolId
            });
        }

        #endregion

        #region Loading

        private void ShowLoading()
        {
            if (_loadingIndicator != null)
            {
                _loadingIndicator.SetActive(true);
            }
        }

        private void HideLoading()
        {
            if (_loadingIndicator != null)
            {
                _loadingIndicator.SetActive(false);
            }
        }

        #endregion
    }
}
