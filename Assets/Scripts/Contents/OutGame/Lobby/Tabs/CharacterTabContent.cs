using UnityEngine;
using UnityEngine.UI;
using Sc.Contents.Character;
using Sc.Core;

namespace Sc.Contents.Lobby
{
    /// <summary>
    /// 캐릭터 탭 컨텐츠 - 캐릭터 목록으로 네비게이션
    /// </summary>
    public class CharacterTabContent : LobbyTabContent
    {
        [Header("UI References")] [SerializeField]
        private Transform _characterListContainer;

        [SerializeField] private Button _navigateButton;

        private void Awake()
        {
            if (_navigateButton != null)
            {
                _navigateButton.onClick.AddListener(NavigateToCharacterList);
            }
        }

        private void OnDestroy()
        {
            if (_navigateButton != null)
            {
                _navigateButton.onClick.RemoveListener(NavigateToCharacterList);
            }
        }

        public override void OnTabSelected()
        {
            Refresh();
        }

        public override void Refresh()
        {
            // 캐릭터 목록 미리보기 표시 (추후 확장)
        }

        /// <summary>
        /// 캐릭터 목록 화면으로 이동
        /// </summary>
        public void NavigateToCharacterList()
        {
            Debug.Log("[CharacterTabContent] Navigate to CharacterList");
            CharacterListScreen.Open(new CharacterListState());
        }
    }
}