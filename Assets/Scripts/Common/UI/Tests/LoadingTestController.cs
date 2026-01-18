using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI.Tests
{
    /// <summary>
    /// Loading 시스템 테스트용 런타임 컨트롤러
    /// </summary>
    public class LoadingTestController : MonoBehaviour
    {
        private void Start()
        {
            ConnectButton("ShowIndicatorBtn", () => LoadingService.Instance.Show(LoadingType.Indicator));
            ConnectButton("ShowFullScreenBtn", () => LoadingService.Instance.Show(LoadingType.FullScreen, "Loading..."));
            ConnectButton("ShowProgressBtn", () => LoadingService.Instance.ShowProgress(0.5f, "Downloading..."));
            ConnectButton("HideBtn", () => LoadingService.Instance.Hide());
            ConnectButton("ForceHideBtn", () => LoadingService.Instance.ForceHide());
        }

        private void ConnectButton(string name, UnityEngine.Events.UnityAction action)
        {
            var btn = transform.Find(name)?.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(action);
            }
        }
    }
}
