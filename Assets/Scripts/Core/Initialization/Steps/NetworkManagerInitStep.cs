using Cysharp.Threading.Tasks;
using Sc.Foundation;

namespace Sc.Core.Initialization.Steps
{
    /// <summary>
    /// NetworkManager 초기화 단계
    /// </summary>
    public class NetworkManagerInitStep : IInitStep
    {
        public string StepName => "네트워크";
        public float Weight => 1.0f;

        public async UniTask<Result<bool>> ExecuteAsync()
        {
            if (!NetworkManager.HasInstance)
            {
                return Result<bool>.Failure(ErrorCode.NetworkDisconnected, "NetworkManager 인스턴스 없음");
            }

            var success = await NetworkManager.Instance.InitializeAsync();
            if (!success)
            {
                return Result<bool>.Failure(ErrorCode.NetworkDisconnected, "네트워크 초기화 실패");
            }

            return Result<bool>.Success(true);
        }
    }
}