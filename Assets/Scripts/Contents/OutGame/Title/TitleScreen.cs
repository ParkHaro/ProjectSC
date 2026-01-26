using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Common.UI.Widgets;
using Sc.Contents.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Title
{
    /// <summary>
    /// 타이틀 화면 State
    /// </summary>
    public class TitleState : IScreenState
    {
        // 타이틀 화면은 상태 없음
    }

    /// <summary>
    /// 타이틀 화면 - 터치 시 로비로 전환
    /// NavigationManager가 루트 Screen 보호 (스택 1개일 때 Back 거부)
    /// </summary>
    [ScreenTemplate(ScreenTemplateType.FullScreen)]
    public class TitleScreen : ScreenWidget<TitleScreen, TitleState>
    {
        [Header("UI References")] [SerializeField]
        private Button _touchArea;

        [SerializeField] private TMP_Text _touchToStartText;
        [SerializeField] private Button _resetAccountButton;

        protected override void OnInitialize()
        {
            Debug.Log("[TitleScreen] OnInitialize");

            if (_touchArea != null)
            {
                _touchArea.onClick.AddListener(OnTouchAreaClicked);
            }

            if (_resetAccountButton != null)
            {
                _resetAccountButton.onClick.AddListener(OnResetAccountClicked);
            }
        }

        protected override void OnBind(TitleState state)
        {
            Debug.Log("[TitleScreen] OnBind");

            // Header 숨김
            ScreenHeader.Instance?.Hide();
        }

        protected override void OnShow()
        {
            Debug.Log("[TitleScreen] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[TitleScreen] OnHide");
        }

        public override TitleState GetState() => new TitleState();

        private void OnTouchAreaClicked()
        {
            Debug.Log("[TitleScreen] Touch to Start!");

            // LobbyScreen으로 전환
            LobbyScreen.Open(new());
        }

        private void OnResetAccountClicked()
        {
            Debug.Log("[TitleScreen] Reset Account!");

            // 저장 데이터 삭제
            var savePath = System.IO.Path.Combine(Application.persistentDataPath, "user_save_data.json");
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
                Debug.Log($"[TitleScreen] 저장 데이터 삭제됨: {savePath}");
            }

            // 앱 재시작 안내 (실제로는 씬 리로드)
            Debug.Log("[TitleScreen] 게임을 재시작하세요.");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}