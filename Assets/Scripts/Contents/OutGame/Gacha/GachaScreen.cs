using Sc.Common.UI;
using Sc.Common.UI.Widgets;
using Sc.Core;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Event.UI;
using Sc.Foundation;
using Sc.Packet;
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
        public string SelectedPoolId = "pool_standard";
    }

    /// <summary>
    /// 가챠 화면 - 캐릭터 소환
    /// </summary>
    public class GachaScreen : ScreenWidget<GachaScreen, GachaState>
    {
        private const int SinglePullCost = 300;
        private const int MultiPullCost = 2700;

        [Header("UI References")]
        [SerializeField] private Button _singlePullButton;
        [SerializeField] private Button _multiPullButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _singleCostText;
        [SerializeField] private TMP_Text _multiCostText;
        [SerializeField] private TMP_Text _pityCountText;
        [SerializeField] private TMP_Text _gemText;

        private GachaState _currentState;
        private bool _isPulling;

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
        }

        protected override void OnBind(GachaState state)
        {
            _currentState = state ?? new GachaState();
            Debug.Log($"[GachaScreen] OnBind - Pool: {_currentState.SelectedPoolId}");

            // Header 설정
            ScreenHeader.Instance?.Configure("gacha_main");

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

        private void RefreshUI()
        {
            // 비용 텍스트
            if (_singleCostText != null)
            {
                _singleCostText.text = $"{SinglePullCost}";
            }

            if (_multiCostText != null)
            {
                _multiCostText.text = $"{MultiPullCost}";
            }

            // 재화 및 천장 정보
            if (DataManager.Instance?.IsInitialized == true)
            {
                var currency = DataManager.Instance.Currency;

                if (_gemText != null)
                {
                    _gemText.text = $"{currency.TotalGem:N0}";
                }

                // 천장 카운트
                var pityData = DataManager.Instance.GachaPity;
                var pityInfo = pityData.GetOrCreatePityInfo(_currentState.SelectedPoolId);
                if (_pityCountText != null)
                {
                    _pityCountText.text = $"천장: {pityInfo.PityCount}/90";
                }

                // 버튼 활성화 상태
                UpdateButtonStates(currency.TotalGem);
            }
        }

        private void UpdateButtonStates(int totalGem)
        {
            if (_singlePullButton != null)
            {
                _singlePullButton.interactable = !_isPulling && totalGem >= SinglePullCost;
            }

            if (_multiPullButton != null)
            {
                _multiPullButton.interactable = !_isPulling && totalGem >= MultiPullCost;
            }
        }

        private void OnUserDataChanged()
        {
            RefreshUI();
        }

        private void OnSinglePullClicked()
        {
            Debug.Log("[GachaScreen] Single pull clicked");
            ExecuteGacha(GachaPullType.Single);
        }

        private void OnMultiPullClicked()
        {
            Debug.Log("[GachaScreen] Multi pull clicked");
            ExecuteGacha(GachaPullType.Multi);
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
            UpdateButtonStates(DataManager.Instance?.Currency.TotalGem ?? 0);

            var request = pullType == GachaPullType.Single
                ? GachaRequest.CreateSingle(_currentState.SelectedPoolId)
                : GachaRequest.CreateMulti(_currentState.SelectedPoolId);

            Debug.Log($"[GachaScreen] Sending gacha request: {pullType}");
            NetworkManager.Instance.Send(request);
        }

        private void OnGachaCompleted(GachaCompletedEvent evt)
        {
            Debug.Log($"[GachaScreen] Gacha completed: {evt.Results?.Count ?? 0} results");

            _isPulling = false;
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
    }
}
