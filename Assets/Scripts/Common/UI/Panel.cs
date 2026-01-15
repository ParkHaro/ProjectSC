namespace Sc.Common.UI
{
    /// <summary>
    /// Screen/Popup 내부 컨테이너.
    /// Navigation 대상이 아님. Screen의 State로 상태 관리.
    /// </summary>
    public abstract class Panel : Widget
    {
        /// <summary>
        /// Panel 활성화.
        /// </summary>
        public virtual void Activate()
        {
            Show();
        }

        /// <summary>
        /// Panel 비활성화.
        /// </summary>
        public virtual void Deactivate()
        {
            Hide();
        }
    }
}
