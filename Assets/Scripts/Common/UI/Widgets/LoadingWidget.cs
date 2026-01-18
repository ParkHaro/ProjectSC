using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// 로딩 UI 위젯
    /// </summary>
    public class LoadingWidget : Widget
    {
        [Header("Full Screen")]
        [SerializeField] private GameObject _fullScreenPanel;
        [SerializeField] private TMP_Text _fullScreenMessage;
        [SerializeField] private Image _fullScreenSpinner;

        [Header("Indicator")]
        [SerializeField] private GameObject _indicatorPanel;
        [SerializeField] private Image _indicatorSpinner;

        [Header("Progress")]
        [SerializeField] private GameObject _progressPanel;
        [SerializeField] private TMP_Text _progressMessage;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _progressPercent;

        [Header("Animation")]
        [SerializeField] private float _fadeInDuration = 0.2f;
        [SerializeField] private float _fadeOutDuration = 0.15f;
        [SerializeField] private float _spinnerSpeed = 360f;

        private LoadingType _currentType;
        private Tweener _fadeTween;
        private bool _isShowing;

        protected override void Awake()
        {
            base.Awake();
            LoadingService.Instance.RegisterWidget(this);
            HideAllPanels();
        }

        private void Update()
        {
            if (!_isShowing) return;

            RotateSpinner();
        }

        /// <summary>
        /// 로딩 표시
        /// </summary>
        public void Show(LoadingType type, string message)
        {
            _currentType = type;
            _isShowing = true;

            HideAllPanels();
            ActivatePanel(type, message);
            FadeIn().Forget();
        }

        /// <summary>
        /// 진행률 로딩 표시
        /// </summary>
        public void ShowProgress(float progress, string message)
        {
            _currentType = LoadingType.Progress;
            _isShowing = true;

            HideAllPanels();
            ActivateProgressPanel(progress, message);
            FadeIn().Forget();
        }

        /// <summary>
        /// 진행률 업데이트
        /// </summary>
        public void UpdateProgress(float progress, string message = null)
        {
            if (_progressBar != null)
            {
                _progressBar.value = Mathf.Clamp01(progress);
            }

            if (_progressPercent != null)
            {
                _progressPercent.text = $"{Mathf.RoundToInt(progress * 100)}%";
            }

            if (!string.IsNullOrEmpty(message) && _progressMessage != null)
            {
                _progressMessage.text = message;
            }
        }

        /// <summary>
        /// 로딩 숨김
        /// </summary>
        public new void Hide()
        {
            FadeOut().Forget();
        }

        private void HideAllPanels()
        {
            if (_fullScreenPanel != null) _fullScreenPanel.SetActive(false);
            if (_indicatorPanel != null) _indicatorPanel.SetActive(false);
            if (_progressPanel != null) _progressPanel.SetActive(false);
        }

        private void ActivatePanel(LoadingType type, string message)
        {
            switch (type)
            {
                case LoadingType.FullScreen:
                    if (_fullScreenPanel != null)
                    {
                        _fullScreenPanel.SetActive(true);
                        if (_fullScreenMessage != null)
                        {
                            _fullScreenMessage.text = message ?? string.Empty;
                            _fullScreenMessage.gameObject.SetActive(!string.IsNullOrEmpty(message));
                        }
                    }
                    break;

                case LoadingType.Indicator:
                    if (_indicatorPanel != null)
                    {
                        _indicatorPanel.SetActive(true);
                    }
                    break;

                case LoadingType.Progress:
                    ActivateProgressPanel(0f, message);
                    break;
            }
        }

        private void ActivateProgressPanel(float progress, string message)
        {
            if (_progressPanel != null)
            {
                _progressPanel.SetActive(true);
            }

            if (_progressMessage != null)
            {
                _progressMessage.text = message ?? string.Empty;
                _progressMessage.gameObject.SetActive(!string.IsNullOrEmpty(message));
            }

            if (_progressBar != null)
            {
                _progressBar.value = Mathf.Clamp01(progress);
            }

            if (_progressPercent != null)
            {
                _progressPercent.text = $"{Mathf.RoundToInt(progress * 100)}%";
            }
        }

        private void RotateSpinner()
        {
            float rotation = _spinnerSpeed * Time.unscaledDeltaTime;

            switch (_currentType)
            {
                case LoadingType.FullScreen:
                    if (_fullScreenSpinner != null)
                    {
                        _fullScreenSpinner.transform.Rotate(0f, 0f, -rotation);
                    }
                    break;

                case LoadingType.Indicator:
                    if (_indicatorSpinner != null)
                    {
                        _indicatorSpinner.transform.Rotate(0f, 0f, -rotation);
                    }
                    break;

                case LoadingType.Progress:
                    if (_fullScreenSpinner != null)
                    {
                        _fullScreenSpinner.transform.Rotate(0f, 0f, -rotation);
                    }
                    break;
            }
        }

        private async UniTaskVoid FadeIn()
        {
            KillFadeTween();

            var canvasGroup = GetOrAddCanvasGroup();
            canvasGroup.alpha = 0f;
            base.Show();

            _fadeTween = canvasGroup
                .DOFade(1f, _fadeInDuration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);

            await _fadeTween.ToUniTask();
        }

        private async UniTaskVoid FadeOut()
        {
            KillFadeTween();

            var canvasGroup = GetOrAddCanvasGroup();

            _fadeTween = canvasGroup
                .DOFade(0f, _fadeOutDuration)
                .SetEase(Ease.InQuad)
                .SetUpdate(true);

            await _fadeTween.ToUniTask();

            _isShowing = false;
            HideAllPanels();
            base.Hide();
        }

        private void KillFadeTween()
        {
            if (_fadeTween != null && _fadeTween.IsActive())
            {
                _fadeTween.Kill();
                _fadeTween = null;
            }
        }

        protected override void OnRelease()
        {
            KillFadeTween();
            base.OnRelease();
        }
    }
}
