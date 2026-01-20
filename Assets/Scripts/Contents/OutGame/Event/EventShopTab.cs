using Sc.Common.UI;
using TMPro;
using UnityEngine;

namespace Sc.Contents.Event
{
    /// <summary>
    /// 이벤트 상점 탭 (플레이스홀더)
    /// </summary>
    public class EventShopTab : Widget
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _placeholderText;
        [SerializeField] private GameObject _shopContainer;

        private string _eventId;
        private string _shopCategoryId;

        protected override void OnInitialize()
        {
            Debug.Log("[EventShopTab] OnInitialize");
        }

        /// <summary>
        /// 상점 탭 설정
        /// </summary>
        public void Setup(string eventId, string shopCategoryId)
        {
            _eventId = eventId;
            _shopCategoryId = shopCategoryId;

            Debug.Log($"[EventShopTab] Setup - EventId: {eventId}, ShopCategoryId: {shopCategoryId}");

            RefreshUI();
        }

        private void RefreshUI()
        {
            // 플레이스홀더 표시
            if (_placeholderText != null)
            {
                _placeholderText.text = "상점 시스템 준비 중\n\n" +
                                        "이벤트 상점 기능은\n" +
                                        "추후 업데이트될 예정입니다.";
                _placeholderText.gameObject.SetActive(true);
            }

            if (_shopContainer != null)
            {
                _shopContainer.SetActive(false);
            }

            // TODO: 실제 상점 구현
            // 1. ShopScreen 재사용
            // 2. EventShop 카테고리 필터
            // 3. 이벤트 재화 사용
        }

        protected override void OnShow()
        {
            Debug.Log("[EventShopTab] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[EventShopTab] OnHide");
        }
    }
}
