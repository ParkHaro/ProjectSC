using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sc.Common.UI;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Core.Initialization
{
    /// <summary>
    /// 초기화 단계 순차 실행 서비스.
    /// 등록된 IInitStep들을 순서대로 실행하며 진행률을 LoadingService에 표시.
    /// </summary>
    public class InitializationSequence
    {
        private readonly List<IInitStep> _steps = new();
        private float _progress;
        private string _currentStepName;
        private bool _isRunning;

        /// <summary>현재 진행률 (0~1)</summary>
        public float Progress => _progress;

        /// <summary>현재 실행 중인 단계 이름</summary>
        public string CurrentStepName => _currentStepName;

        /// <summary>실행 중 여부</summary>
        public bool IsRunning => _isRunning;

        /// <summary>진행률 변경 이벤트</summary>
        public event Action<float, string> OnProgressChanged;

        /// <summary>
        /// 초기화 단계 등록
        /// </summary>
        public void RegisterStep(IInitStep step)
        {
            if (step == null)
            {
                Log.Warning("null step 등록 시도", LogCategory.System);
                return;
            }

            if (_isRunning)
            {
                Log.Warning("실행 중에는 단계를 등록할 수 없음", LogCategory.System);
                return;
            }

            _steps.Add(step);
        }

        /// <summary>
        /// 등록된 모든 단계 순차 실행
        /// </summary>
        /// <returns>성공 여부</returns>
        public async UniTask<Result<bool>> RunAsync()
        {
            if (_isRunning)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "이미 초기화 진행 중");
            }

            if (_steps.Count == 0)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "등록된 초기화 단계 없음");
            }

            _isRunning = true;
            _progress = 0f;

            // 총 가중치 계산
            float totalWeight = _steps.Sum(s => s.Weight);
            float completedWeight = 0f;

            // 로딩 UI 표시
            if (LoadingService.HasInstance)
            {
                LoadingService.Instance.ShowProgress(0f, "초기화 중...");
            }

            try
            {
                foreach (var step in _steps)
                {
                    _currentStepName = step.StepName;
                    _progress = totalWeight > 0 ? completedWeight / totalWeight : 0f;

                    // 진행률 업데이트
                    NotifyProgress(_progress, _currentStepName);

                    Log.Info($"[Init] {step.StepName} 시작...", LogCategory.System);

                    // 단계 실행
                    var result = await step.ExecuteAsync();

                    if (result.IsFailure)
                    {
                        Log.Error($"[Init] {step.StepName} 실패: {result.Message}", LogCategory.System);

                        // 실패 이벤트 발행
                        if (EventManager.HasInstance)
                        {
                            EventManager.Instance.Publish(new InitializationCompletedEvent
                            {
                                IsSuccess = false,
                                ErrorCode = result.Error,
                                ErrorMessage = result.Message
                            });
                        }

                        return Result<bool>.Failure(result.Error, result.Message);
                    }

                    Log.Info($"[Init] {step.StepName} 완료", LogCategory.System);
                    completedWeight += step.Weight;
                }

                // 완료
                _progress = 1f;
                _currentStepName = "완료";
                NotifyProgress(1f, "완료");

                Log.Info("[Init] 모든 초기화 단계 완료", LogCategory.System);

                // 성공 이벤트 발행
                if (EventManager.HasInstance)
                {
                    EventManager.Instance.Publish(new InitializationCompletedEvent
                    {
                        IsSuccess = true,
                        ErrorCode = ErrorCode.None
                    });
                }

                return Result<bool>.Success(true);
            }
            finally
            {
                _isRunning = false;

                // 로딩 UI 숨김
                if (LoadingService.HasInstance)
                {
                    LoadingService.Instance.Hide();
                }
            }
        }

        /// <summary>
        /// 등록된 단계 초기화
        /// </summary>
        public void Clear()
        {
            if (_isRunning)
            {
                Log.Warning("실행 중에는 단계를 초기화할 수 없음", LogCategory.System);
                return;
            }

            _steps.Clear();
            _progress = 0f;
            _currentStepName = null;
            OnProgressChanged = null;
        }

        private void NotifyProgress(float progress, string stepName)
        {
            // 이벤트 호출
            OnProgressChanged?.Invoke(progress, stepName);

            // LoadingService 업데이트
            if (LoadingService.HasInstance && LoadingService.Instance.IsLoading)
            {
                LoadingService.Instance.UpdateProgress(progress, stepName);
            }
        }
    }
}
