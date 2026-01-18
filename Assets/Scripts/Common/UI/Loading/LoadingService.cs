using System.Collections;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// 로딩 상태 관리 서비스
    /// 레퍼런스 카운팅으로 중첩 호출 지원
    /// </summary>
    public class LoadingService : Singleton<LoadingService>
    {
        private LoadingWidget _widget;
        private LoadingConfig _config;
        private int _refCount;
        private Coroutine _timeoutCoroutine;
        private LoadingType _currentType;

        /// <summary>
        /// 로딩 중 여부
        /// </summary>
        public bool IsLoading => _refCount > 0;

        /// <summary>
        /// 현재 레퍼런스 카운트 (테스트용)
        /// </summary>
        public int RefCount => _refCount;

        /// <summary>
        /// 현재 로딩 타입
        /// </summary>
        public LoadingType CurrentType => _currentType;

        protected override void OnSingletonAwake()
        {
            _config = LoadingConfig.CreateDefault();
        }

        protected override void OnSingletonDestroy()
        {
            StopTimeoutCoroutine();
            _widget = null;
        }

        /// <summary>
        /// 설정 주입 (테스트용)
        /// </summary>
        public void SetConfig(LoadingConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// 로딩 위젯 등록 (LoadingWidget.Awake에서 호출)
        /// </summary>
        public void RegisterWidget(LoadingWidget widget)
        {
            _widget = widget;
        }

        /// <summary>
        /// 로딩 표시 (레퍼런스 카운트 증가)
        /// </summary>
        public void Show(LoadingType type = LoadingType.Indicator, string message = null)
        {
            _refCount++;
            _currentType = type;

            if (_refCount == 1)
            {
                StartTimeoutCoroutine();
            }

            if (_widget != null)
            {
                _widget.Show(type, message);
            }
            else
            {
                Log.Warning("LoadingWidget이 등록되지 않음", LogCategory.UI);
            }
        }

        /// <summary>
        /// 진행률과 함께 로딩 표시
        /// </summary>
        public void ShowProgress(float progress, string message = null)
        {
            _refCount++;
            _currentType = LoadingType.Progress;

            if (_refCount == 1)
            {
                StartTimeoutCoroutine();
            }

            if (_widget != null)
            {
                _widget.ShowProgress(progress, message);
            }
            else
            {
                Log.Warning("LoadingWidget이 등록되지 않음", LogCategory.UI);
            }
        }

        /// <summary>
        /// 진행률 업데이트 (이미 표시 중일 때)
        /// </summary>
        public void UpdateProgress(float progress, string message = null)
        {
            if (!IsLoading)
            {
                Log.Warning("로딩 중이 아닐 때 UpdateProgress 호출", LogCategory.UI);
                return;
            }

            if (_widget != null)
            {
                _widget.UpdateProgress(progress, message);
            }
        }

        /// <summary>
        /// 로딩 숨김 (레퍼런스 카운트 감소)
        /// </summary>
        public void Hide()
        {
            if (_refCount <= 0)
            {
                Log.Warning("레퍼런스 카운트가 이미 0", LogCategory.UI);
                return;
            }

            _refCount--;

            if (_refCount == 0)
            {
                StopTimeoutCoroutine();

                if (_widget != null)
                {
                    _widget.Hide();
                }
            }
        }

        /// <summary>
        /// 강제 숨김 (카운트 무시)
        /// </summary>
        public void ForceHide()
        {
            _refCount = 0;
            StopTimeoutCoroutine();

            if (_widget != null)
            {
                _widget.Hide();
            }
        }

        private void StartTimeoutCoroutine()
        {
            StopTimeoutCoroutine();

            if (_config != null && _config.TimeoutSeconds > 0)
            {
                _timeoutCoroutine = StartCoroutine(TimeoutRoutine());
            }
        }

        private void StopTimeoutCoroutine()
        {
            if (_timeoutCoroutine != null)
            {
                StopCoroutine(_timeoutCoroutine);
                _timeoutCoroutine = null;
            }
        }

        private IEnumerator TimeoutRoutine()
        {
            yield return new WaitForSecondsRealtime(_config.TimeoutSeconds);

            if (_refCount > 0)
            {
                Log.Warning($"로딩 타임아웃 ({_config.TimeoutSeconds}초)", LogCategory.UI);
                ForceHide();
            }
        }
    }
}
