using Cysharp.Threading.Tasks;
using Sc.Foundation;

namespace Sc.Core.Initialization
{
    /// <summary>
    /// 초기화 단계 인터페이스.
    /// 게임 시작 시 순차적으로 실행되는 초기화 단계를 정의.
    /// </summary>
    public interface IInitStep
    {
        /// <summary>단계 이름 (UI 표시용)</summary>
        string StepName { get; }

        /// <summary>
        /// 예상 가중치 (진행률 계산용).
        /// 기본값 1.0f. 네트워크 작업 등 느린 단계는 더 높은 가중치 부여.
        /// </summary>
        float Weight { get; }

        /// <summary>
        /// 초기화 실행.
        /// </summary>
        /// <returns>성공 시 true, 실패 시 에러 정보 포함</returns>
        UniTask<Result<bool>> ExecuteAsync();
    }
}