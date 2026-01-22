using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby.Widgets
{
    /// <summary>
    /// 이벤트 배너 캐러셀.
    /// 좌우 스와이프로 배너 전환.
    /// </summary>
    public class EventBannerCarousel : MonoBehaviour
    {
        [SerializeField] private Transform _bannerContainer;
        [SerializeField] private Transform _indicatorContainer;
        [SerializeField] private GameObject _indicatorPrefab;
        [SerializeField] private float _autoScrollInterval = 5f;

        private readonly List<GameObject> _banners = new();
        private readonly List<GameObject> _indicators = new();
        private int _currentIndex;
        private float _autoScrollTimer;

        public event Action<int> OnBannerClicked;

        public void Initialize(IReadOnlyList<BannerData> banners)
        {
            ClearBanners();

            for (int i = 0; i < banners.Count; i++)
            {
                var banner = CreateBanner(banners[i], i);
                _banners.Add(banner);

                if (_indicatorPrefab != null && _indicatorContainer != null)
                {
                    var indicator = Instantiate(_indicatorPrefab, _indicatorContainer);
                    _indicators.Add(indicator);
                }
            }

            ShowBanner(0);
        }

        private GameObject CreateBanner(BannerData data, int index)
        {
            var bannerObj = new GameObject($"Banner_{index}");
            bannerObj.transform.SetParent(_bannerContainer, false);

            var rect = bannerObj.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = bannerObj.AddComponent<Image>();
            image.sprite = data.Sprite;
            image.preserveAspect = true;

            var button = bannerObj.AddComponent<Button>();
            int capturedIndex = index;
            button.onClick.AddListener(() => OnBannerClicked?.Invoke(capturedIndex));

            bannerObj.SetActive(false);
            return bannerObj;
        }

        private void ClearBanners()
        {
            foreach (var banner in _banners)
            {
                if (banner != null)
                    Destroy(banner);
            }
            _banners.Clear();

            foreach (var indicator in _indicators)
            {
                if (indicator != null)
                    Destroy(indicator);
            }
            _indicators.Clear();
        }

        public void ShowBanner(int index)
        {
            if (_banners.Count == 0) return;

            _currentIndex = Mathf.Clamp(index, 0, _banners.Count - 1);

            for (int i = 0; i < _banners.Count; i++)
            {
                _banners[i].SetActive(i == _currentIndex);
            }

            UpdateIndicators();
            _autoScrollTimer = 0f;
        }

        public void ShowNext() => ShowBanner((_currentIndex + 1) % _banners.Count);
        public void ShowPrevious() => ShowBanner((_currentIndex - 1 + _banners.Count) % _banners.Count);

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

        private void Update()
        {
            if (_banners.Count <= 1 || _autoScrollInterval <= 0) return;

            _autoScrollTimer += Time.deltaTime;
            if (_autoScrollTimer >= _autoScrollInterval)
            {
                ShowNext();
            }
        }
    }

    [Serializable]
    public class BannerData
    {
        public string Id;
        public Sprite Sprite;
        public string TargetScreen;
        public string TargetParam;
    }
}
