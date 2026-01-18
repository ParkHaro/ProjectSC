using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// 로딩 UI 설정
    /// </summary>
    [CreateAssetMenu(fileName = "LoadingConfig", menuName = "Sc/UI/LoadingConfig")]
    public class LoadingConfig : ScriptableObject
    {
        [Header("Timeout")]
        [Tooltip("로딩 타임아웃 시간 (초)")]
        [SerializeField] private float _timeoutSeconds = 30f;

        [Header("Animation")]
        [Tooltip("페이드인 시간 (초)")]
        [SerializeField] private float _fadeInDuration = 0.2f;

        [Tooltip("페이드아웃 시간 (초)")]
        [SerializeField] private float _fadeOutDuration = 0.15f;

        [Tooltip("스피너 회전 속도 (도/초)")]
        [SerializeField] private float _spinnerSpeed = 360f;

        [Header("Visual")]
        [Tooltip("전체화면 오버레이 투명도")]
        [Range(0f, 1f)]
        [SerializeField] private float _overlayAlpha = 0.8f;

        public float TimeoutSeconds => _timeoutSeconds;
        public float FadeInDuration => _fadeInDuration;
        public float FadeOutDuration => _fadeOutDuration;
        public float SpinnerSpeed => _spinnerSpeed;
        public float OverlayAlpha => _overlayAlpha;

        /// <summary>
        /// 기본 설정 생성 (테스트용)
        /// </summary>
        public static LoadingConfig CreateDefault()
        {
            var config = CreateInstance<LoadingConfig>();
            config._timeoutSeconds = 30f;
            config._fadeInDuration = 0.2f;
            config._fadeOutDuration = 0.15f;
            config._spinnerSpeed = 360f;
            config._overlayAlpha = 0.8f;
            return config;
        }
    }
}
