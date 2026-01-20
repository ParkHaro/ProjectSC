using System;
using Cysharp.Threading.Tasks;
using Sc.Data;
using Sc.Event.OutGame;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Core.Initialization.Steps
{
    /// <summary>
    /// 로그인 단계.
    /// 게스트 로그인 요청 후 이벤트 기반 완료 대기.
    /// </summary>
    public class LoginStep : IInitStep
    {
        private const float TimeoutSeconds = 10f;

        public string StepName => "로그인";
        public float Weight => 2.0f;

        private UniTaskCompletionSource<Result<bool>> _loginTcs;
        private bool _isSubscribed;
        private bool _isCompleted;

        public async UniTask<Result<bool>> ExecuteAsync()
        {
            if (!NetworkManager.HasInstance)
            {
                return Result<bool>.Failure(ErrorCode.NetworkDisconnected, "NetworkManager 인스턴스 없음");
            }

            _loginTcs = new UniTaskCompletionSource<Result<bool>>();
            _isCompleted = false;

            // 이벤트 구독
            Subscribe();

            try
            {
                // 로그인 요청 생성
                var request = LoginRequest.CreateGuest(
                    SystemInfo.deviceUniqueIdentifier,
                    Application.version,
                    Application.platform.ToString()
                );

                // 요청 전송
                NetworkManager.Instance.Send(request);

                // 타임아웃 대기
                var loginTask = _loginTcs.Task;
                var timeoutCts = new System.Threading.CancellationTokenSource();

                try
                {
                    var result = await loginTask.Timeout(TimeSpan.FromSeconds(TimeoutSeconds));
                    _isCompleted = true;
                    return result;
                }
                catch (TimeoutException)
                {
                    _isCompleted = true;
                    return Result<bool>.Failure(ErrorCode.NetworkTimeout, "로그인 타임아웃");
                }
            }
            finally
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            if (_isSubscribed || !EventManager.HasInstance) return;

            EventManager.Instance.Subscribe<LoginCompletedEvent>(OnLoginCompleted);
            EventManager.Instance.Subscribe<LoginFailedEvent>(OnLoginFailed);
            _isSubscribed = true;
        }

        private void Unsubscribe()
        {
            if (!_isSubscribed || !EventManager.HasInstance) return;

            EventManager.Instance.Unsubscribe<LoginCompletedEvent>(OnLoginCompleted);
            EventManager.Instance.Unsubscribe<LoginFailedEvent>(OnLoginFailed);
            _isSubscribed = false;
        }

        private void OnLoginCompleted(LoginCompletedEvent evt)
        {
            if (_isCompleted) return; // 타임아웃 후 늦은 이벤트 무시

            Log.Info($"[LoginStep] 로그인 성공: {evt.Nickname}", LogCategory.Network);
            _loginTcs?.TrySetResult(Result<bool>.Success(true));
        }

        private void OnLoginFailed(LoginFailedEvent evt)
        {
            if (_isCompleted) return; // 타임아웃 후 늦은 이벤트 무시

            Log.Error($"[LoginStep] 로그인 실패: {evt.ErrorCode} - {evt.ErrorMessage}", LogCategory.Network);
            _loginTcs?.TrySetResult(Result<bool>.Failure(ErrorCode.LoginFailed, evt.ErrorMessage));
        }
    }
}
