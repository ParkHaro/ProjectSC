using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sc.Core;
using Sc.Data;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 배너 캐러셀 위젯
    /// 배너 목록을 표시하고 선택/스와이프 기능 제공
    /// </summary>
    public class GachaBannerWidget : MonoBehaviour
    {
        [Header("배너 캐러셀")]
        [SerializeField] private Transform _bannerContainer;
        [SerializeField] private GameObject _bannerItemPrefab;
        [SerializeField] private ScrollRect _bannerScrollRect;
        [SerializeField] private Transform _indicatorContainer;
        [SerializeField] private GameObject _indicatorPrefab;

        [Header("배너 정보")]
        [SerializeField] private TMP_Text _bannerTitleText;
        [SerializeField] private TMP_Text _bannerPeriodText;
        [SerializeField] private TMP_Text _bannerDescriptionText;

        [Header("캐릭터 디스플레이")]
        [SerializeField] private Image _characterImage;
        [SerializeField] private Transform _characterDisplay;

        [Header("자동 스크롤")]
        [SerializeField] private float _autoScrollInterval = 5f;
        [SerializeField] private bool _enableAutoScroll = true;

        private readonly List<GachaBannerItem> _bannerItems = new();
        private readonly List<GameObject> _indicators = new();
        private int _currentIndex;
        private float _autoScrollTimer;
        private GachaPoolData _selectedPool;

        /// <summary>
        /// 배너 선택 이벤트
        /// </summary>
        public event Action<string> OnBannerSelected;

        /// <summary>
        /// 현재 선택된 풀 ID
        /// </summary>
        public string SelectedPoolId => _selectedPool?.Id;

        /// <summary>
        /// 현재 선택된 풀 데이터
        /// </summary>
        public GachaPoolData SelectedPool => _selectedPool;

        /// <summary>
        /// 배너 초기화
        /// </summary>
        /// <param name="pools">활성 가챠 풀 목록</param>
        /// <param name="selectedPoolId">초기 선택 풀 ID</param>
        public void Initialize(IReadOnlyList<GachaPoolData> pools, string selectedPoolId = null)
        {
            ClearBanners();

            foreach (var pool in pools)
            {
                var isSelected = !string.IsNullOrEmpty(selectedPoolId)
                    ? pool.Id == selectedPoolId
                    : _bannerItems.Count == 0;

                var bannerItem = CreateBannerItem(pool, isSelected);
                _bannerItems.Add(bannerItem);
                CreateIndicator();
            }

            // 초기 선택 설정
            if (_bannerItems.Count > 0)
            {
                var initialIndex = 0;
                if (!string.IsNullOrEmpty(selectedPoolId))
                {
                    for (int i = 0; i < _bannerItems.Count; i++)
                    {
                        if (_bannerItems[i].PoolId == selectedPoolId)
                        {
                            initialIndex = i;
                            break;
                        }
                    }
                }
                SelectBanner(initialIndex);
            }
        }

        /// <summary>
        /// 풀 ID로 배너 선택
        /// </summary>
        public void SelectByPoolId(string poolId)
        {
            for (int i = 0; i < _bannerItems.Count; i++)
            {
                if (_bannerItems[i].PoolId == poolId)
                {
                    SelectBanner(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 인덱스로 배너 선택
        /// </summary>
        public void SelectBanner(int index)
        {
            if (_bannerItems.Count == 0) return;

            _currentIndex = Mathf.Clamp(index, 0, _bannerItems.Count - 1);

            // 배너 아이템 선택 상태 업데이트
            for (int i = 0; i < _bannerItems.Count; i++)
            {
                _bannerItems[i].SetSelected(i == _currentIndex);
            }

            // 선택된 풀 설정
            var selectedItem = _bannerItems[_currentIndex];
            _selectedPool = selectedItem != null
                ? DataManager.Instance?.GachaPools?.GetById(selectedItem.PoolId)
                : null;

            // 인디케이터 업데이트
            UpdateIndicators();

            // 배너 정보 업데이트
            UpdateBannerInfo();

            // 스크롤 위치 업데이트
            ScrollToBanner(_currentIndex);

            // 타이머 리셋
            _autoScrollTimer = 0f;

            // 이벤트 발행
            if (selectedItem != null)
            {
                OnBannerSelected?.Invoke(selectedItem.PoolId);
            }
        }

        /// <summary>
        /// 다음 배너로 이동
        /// </summary>
        public void ShowNext()
        {
            if (_bannerItems.Count <= 1) return;
            SelectBanner((_currentIndex + 1) % _bannerItems.Count);
        }

        /// <summary>
        /// 이전 배너로 이동
        /// </summary>
        public void ShowPrevious()
        {
            if (_bannerItems.Count <= 1) return;
            SelectBanner((_currentIndex - 1 + _bannerItems.Count) % _bannerItems.Count);
        }

        /// <summary>
        /// 남은 시간 갱신 (외부 호출용)
        /// </summary>
        public void RefreshRemainingTime(DateTime serverTime)
        {
            foreach (var bannerItem in _bannerItems)
            {
                bannerItem?.RefreshRemainingTime(serverTime);
            }
        }

        private void Update()
        {
            if (!_enableAutoScroll || _bannerItems.Count <= 1) return;

            _autoScrollTimer += Time.deltaTime;
            if (_autoScrollTimer >= _autoScrollInterval)
            {
                ShowNext();
            }
        }

        private GachaBannerItem CreateBannerItem(GachaPoolData pool, bool isSelected)
        {
            if (_bannerContainer == null || _bannerItemPrefab == null)
            {
                Debug.LogWarning("[GachaBannerWidget] Banner container or prefab is null");
                return null;
            }

            var bannerGo = Instantiate(_bannerItemPrefab, _bannerContainer);
            var bannerItem = bannerGo.GetComponent<GachaBannerItem>();

            if (bannerItem != null)
            {
                bannerItem.Setup(pool, OnBannerItemSelected, isSelected);
            }

            return bannerItem;
        }

        private void CreateIndicator()
        {
            if (_indicatorContainer == null || _indicatorPrefab == null) return;

            var indicator = Instantiate(_indicatorPrefab, _indicatorContainer);
            _indicators.Add(indicator);
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

            foreach (var indicator in _indicators)
            {
                if (indicator != null)
                {
                    Destroy(indicator);
                }
            }
            _indicators.Clear();

            _selectedPool = null;
        }

        private void OnBannerItemSelected(string poolId)
        {
            SelectByPoolId(poolId);
        }

        private void UpdateIndicators()
        {
            for (int i = 0; i < _indicators.Count; i++)
            {
                var image = _indicators[i].GetComponent<Image>();
                if (image != null)
                {
                    image.color = i == _currentIndex
                        ? Color.white
                        : new Color(1f, 1f, 1f, 0.4f);
                }
            }
        }

        private void UpdateBannerInfo()
        {
            if (_selectedPool == null) return;

            // 배너 타이틀
            if (_bannerTitleText != null)
            {
                _bannerTitleText.text = _selectedPool.Name;
            }

            // 배너 기간
            if (_bannerPeriodText != null)
            {
                if (!string.IsNullOrEmpty(_selectedPool.StartDate) && !string.IsNullOrEmpty(_selectedPool.EndDate))
                {
                    _bannerPeriodText.text = $"{FormatDate(_selectedPool.StartDate)} ~ {FormatDate(_selectedPool.EndDate)}";
                    _bannerPeriodText.gameObject.SetActive(true);
                }
                else
                {
                    _bannerPeriodText.gameObject.SetActive(false);
                }
            }

            // 배너 설명
            if (_bannerDescriptionText != null)
            {
                _bannerDescriptionText.text = _selectedPool.Description ?? "";
            }

            // 캐릭터 이미지 (픽업인 경우)
            if (_characterDisplay != null)
            {
                var hasPickup = _selectedPool.Type == GachaType.Pickup &&
                                !string.IsNullOrEmpty(_selectedPool.RateUpCharacterId);
                _characterDisplay.gameObject.SetActive(hasPickup);

                // TODO: 캐릭터 이미지 로드
            }
        }

        private void ScrollToBanner(int index)
        {
            if (_bannerScrollRect == null || _bannerItems.Count <= 1) return;

            var normalizedPos = (float)index / (_bannerItems.Count - 1);
            _bannerScrollRect.horizontalNormalizedPosition = normalizedPos;
        }

        private string FormatDate(string isoDate)
        {
            if (DateTime.TryParse(isoDate, out var date))
            {
                return date.ToString("yyyy-MM-dd HH:mm");
            }
            return isoDate;
        }
    }
}
