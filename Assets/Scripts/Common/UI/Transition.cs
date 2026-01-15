using Cysharp.Threading.Tasks;

namespace Sc.Common.UI
{
    /// <summary>
    /// Screen 전환 애니메이션 베이스 클래스.
    /// </summary>
    public abstract class Transition
    {
        public ScreenWidget OutScreen { get; set; }
        public ScreenWidget InScreen { get; set; }

        public abstract UniTask Out();
        public abstract UniTask In();

        public static Transition Default => new FadeTransition();
    }

    /// <summary>
    /// 페이드 인/아웃 전환.
    /// </summary>
    public class FadeTransition : Transition
    {
        public float Duration { get; set; } = 0.3f;

        public override async UniTask Out()
        {
            // TODO: 페이드 아웃 구현
            await UniTask.Delay((int)(Duration * 1000));
        }

        public override async UniTask In()
        {
            // TODO: 페이드 인 구현
            await UniTask.Delay((int)(Duration * 1000));
        }
    }

    /// <summary>
    /// 슬라이드 전환.
    /// </summary>
    public class SlideTransition : Transition
    {
        public float Duration { get; set; } = 0.3f;

        public override async UniTask Out()
        {
            // TODO: 슬라이드 아웃 구현
            await UniTask.Delay((int)(Duration * 1000));
        }

        public override async UniTask In()
        {
            // TODO: 슬라이드 인 구현
            await UniTask.Delay((int)(Duration * 1000));
        }
    }

    /// <summary>
    /// 즉시 전환 (애니메이션 없음).
    /// </summary>
    public class EmptyTransition : Transition
    {
        public override UniTask Out() => UniTask.CompletedTask;
        public override UniTask In() => UniTask.CompletedTask;
    }
}
