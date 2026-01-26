using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sc.Data;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 뽑기 버튼 위젯
    /// 1회/10회 뽑기 버튼과 비용 표시
    /// </summary>
    public class GachaPullButtonWidget : MonoBehaviour
    {
        [Header("무료 뽑기")]
        [SerializeField] private Button _freePullButton;
        [SerializeField] private TMP_Text _freePullLabel;
        [SerializeField] private TMP_Text _freePullCostText;
        [SerializeField] private Image _freePullCostIcon;
        [SerializeField] private TMP_Text _freePullRemainingText;
        [SerializeField] private GameObject _freePullContainer;

        [Header("1회 뽑기")]
        [SerializeField] private Button _singlePullButton;
        [SerializeField] private TMP_Text _singlePullLabel;
        [SerializeField] private TMP_Text _singlePullCostText;
        [SerializeField] private Image _singlePullCostIcon;

        [Header("10회 뽑기")]
        [SerializeField] private Button _multiPullButton;
        [SerializeField] private TMP_Text _multiPullLabel;
        [SerializeField] private TMP_Text _multiPullCostText;
        [SerializeField] private Image _multiPullCostIcon;
        [SerializeField] private GameObject _guaranteeBadge;
        [SerializeField] private TMP_Text _guaranteeBadgeText;

        [Header("비용 아이콘")]
        [SerializeField] private Sprite _gemIcon;
        [SerializeField] private Sprite _freeGemIcon;
        [SerializeField] private Sprite _ticketIcon;

        private GachaPoolData _poolData;
        private int _currentCurrency;
        private int _freePullRemaining;
        private bool _isPulling;

        /// <summary>
        /// 무료 뽑기 클릭 이벤트
        /// </summary>
        public event Action OnFreePullClicked;

        /// <summary>
        /// 1회 뽑기 클릭 이벤트
        /// </summary>
        public event Action OnSinglePullClicked;

        /// <summary>
        /// 10회 뽑기 클릭 이벤트
        /// </summary>
        public event Action OnMultiPullClicked;

        private void Awake()
        {
            if (_freePullButton != null)
            {
                _freePullButton.onClick.AddListener(() => OnFreePullClicked?.Invoke());
            }

            if (_singlePullButton != null)
            {
                _singlePullButton.onClick.AddListener(() => OnSinglePullClicked?.Invoke());
            }

            if (_multiPullButton != null)
            {
                _multiPullButton.onClick.AddListener(() => OnMultiPullClicked?.Invoke());
            }
        }

        private void OnDestroy()
        {
            if (_freePullButton != null)
            {
                _freePullButton.onClick.RemoveAllListeners();
            }

            if (_singlePullButton != null)
            {
                _singlePullButton.onClick.RemoveAllListeners();
            }

            if (_multiPullButton != null)
            {
                _multiPullButton.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        /// 풀 데이터로 초기화
        /// </summary>
        /// <param name="poolData">가챠 풀 데이터</param>
        /// <param name="currentCurrency">현재 보유 재화</param>
        /// <param name="freePullRemaining">무료 뽑기 남은 횟수</param>
        public void Initialize(GachaPoolData poolData, int currentCurrency, int freePullRemaining = 0)
        {
            _poolData = poolData;
            _currentCurrency = currentCurrency;
            _freePullRemaining = freePullRemaining;
            _isPulling = false;

            RefreshUI();
        }

        /// <summary>
        /// 재화 업데이트
        /// </summary>
        public void UpdateCurrency(int currentCurrency)
        {
            _currentCurrency = currentCurrency;
            UpdateButtonStates();
        }

        /// <summary>
        /// 무료 뽑기 횟수 업데이트
        /// </summary>
        public void UpdateFreePullRemaining(int remaining)
        {
            _freePullRemaining = remaining;
            UpdateFreePullButton();
            UpdateButtonStates();
        }

        /// <summary>
        /// 뽑기 진행 중 상태 설정
        /// </summary>
        public void SetPulling(bool isPulling)
        {
            _isPulling = isPulling;
            UpdateButtonStates();
        }

        private void RefreshUI()
        {
            if (_poolData == null) return;

            UpdateFreePullButton();
            UpdateSinglePullButton();
            UpdateMultiPullButton();
            UpdateButtonStates();
        }

        private void UpdateFreePullButton()
        {
            if (_freePullContainer == null) return;

            // 무료 뽑기가 있는 풀인지 확인
            var hasFreePull = _poolData?.Type == GachaType.Free || _freePullRemaining > 0;
            _freePullContainer.SetActive(hasFreePull);

            if (!hasFreePull) return;

            // 라벨
            if (_freePullLabel != null)
            {
                _freePullLabel.text = "1일 1회 모집";
            }

            // 비용 (무료)
            if (_freePullCostText != null)
            {
                _freePullCostText.text = "무료";
            }

            if (_freePullCostIcon != null && _freeGemIcon != null)
            {
                _freePullCostIcon.sprite = _freeGemIcon;
            }

            // 남은 횟수
            if (_freePullRemainingText != null)
            {
                _freePullRemainingText.text = $"{_freePullRemaining}회 남음";
                _freePullRemainingText.gameObject.SetActive(_freePullRemaining > 0);
            }
        }

        private void UpdateSinglePullButton()
        {
            if (_poolData == null) return;

            // 라벨
            if (_singlePullLabel != null)
            {
                _singlePullLabel.text = "1회 모집";
            }

            // 비용
            if (_singlePullCostText != null)
            {
                _singlePullCostText.text = _poolData.CostAmount > 0
                    ? $"{_poolData.CostAmount:N0}"
                    : "무료";
            }

            // 아이콘
            if (_singlePullCostIcon != null)
            {
                var icon = GetCostIcon(_poolData.CostType);
                if (icon != null)
                {
                    _singlePullCostIcon.sprite = icon;
                }
            }
        }

        private void UpdateMultiPullButton()
        {
            if (_poolData == null) return;

            // 라벨
            if (_multiPullLabel != null)
            {
                _multiPullLabel.text = "10회 모집";
            }

            // 비용
            if (_multiPullCostText != null)
            {
                _multiPullCostText.text = _poolData.CostAmount10 > 0
                    ? $"{_poolData.CostAmount10:N0}"
                    : "무료";
            }

            // 아이콘
            if (_multiPullCostIcon != null)
            {
                var icon = GetCostIcon(_poolData.CostType);
                if (icon != null)
                {
                    _multiPullCostIcon.sprite = icon;
                }
            }

            // 확정 배지 (10연차 SR 이상 확정)
            if (_guaranteeBadge != null)
            {
                // 일반적으로 10연차는 SR 이상 1개 확정
                _guaranteeBadge.SetActive(true);

                if (_guaranteeBadgeText != null)
                {
                    _guaranteeBadgeText.text = "★2 확정";
                }
            }
        }

        private void UpdateButtonStates()
        {
            if (_poolData == null) return;

            var canFreePull = !_isPulling && _freePullRemaining > 0;
            var canSinglePull = !_isPulling &&
                                (_poolData.CostAmount == 0 || _currentCurrency >= _poolData.CostAmount);
            var canMultiPull = !_isPulling &&
                               _poolData.CostAmount10 > 0 &&
                               _currentCurrency >= _poolData.CostAmount10;

            if (_freePullButton != null)
            {
                _freePullButton.interactable = canFreePull;
            }

            if (_singlePullButton != null)
            {
                _singlePullButton.interactable = canSinglePull;
            }

            if (_multiPullButton != null)
            {
                _multiPullButton.interactable = canMultiPull;
            }

            // 비용 텍스트 색상 업데이트 (부족하면 빨간색)
            UpdateCostTextColor(_singlePullCostText, _poolData.CostAmount, canSinglePull);
            UpdateCostTextColor(_multiPullCostText, _poolData.CostAmount10, canMultiPull);
        }

        private void UpdateCostTextColor(TMP_Text costText, int cost, bool canAfford)
        {
            if (costText == null || cost == 0) return;

            costText.color = canAfford
                ? Color.white
                : new Color(1f, 0.4f, 0.4f); // 부족하면 빨간색
        }

        private Sprite GetCostIcon(CostType costType)
        {
            return costType switch
            {
                CostType.Gem => _gemIcon,
                CostType.SummonTicket => _ticketIcon,
                CostType.PickupSummonTicket => _ticketIcon,
                _ => _gemIcon
            };
        }
    }
}
