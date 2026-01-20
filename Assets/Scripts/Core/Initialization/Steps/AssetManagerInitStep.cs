using Cysharp.Threading.Tasks;
using Sc.Foundation;

namespace Sc.Core.Initialization.Steps
{
    /// <summary>
    /// AssetManager 초기화 단계
    /// </summary>
    public class AssetManagerInitStep : IInitStep
    {
        public string StepName => "리소스 시스템";
        public float Weight => 0.5f;

        public async UniTask<Result<bool>> ExecuteAsync()
        {
            if (!AssetManager.HasInstance)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "AssetManager 인스턴스 없음");
            }

            var success = AssetManager.Instance.Initialize();
            if (!success)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "AssetManager 초기화 실패");
            }

            // 프레임 양보 (UI 업데이트 허용)
            await UniTask.Yield();

            return Result<bool>.Success(true);
        }
    }
}
