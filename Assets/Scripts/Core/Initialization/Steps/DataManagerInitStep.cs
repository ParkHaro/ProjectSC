using Cysharp.Threading.Tasks;
using Sc.Foundation;

namespace Sc.Core.Initialization.Steps
{
    /// <summary>
    /// DataManager 초기화 단계 (마스터 데이터 검증)
    /// </summary>
    public class DataManagerInitStep : IInitStep
    {
        public string StepName => "게임 데이터";
        public float Weight => 1.5f;

        public async UniTask<Result<bool>> ExecuteAsync()
        {
            if (!DataManager.HasInstance)
            {
                return Result<bool>.Failure(ErrorCode.LoadFailed, "DataManager 인스턴스 없음");
            }

            var success = DataManager.Instance.Initialize();
            if (!success)
            {
                return Result<bool>.Failure(ErrorCode.LoadFailed, "마스터 데이터 로드 실패");
            }

            // 프레임 양보
            await UniTask.Yield();

            return Result<bool>.Success(true);
        }
    }
}
