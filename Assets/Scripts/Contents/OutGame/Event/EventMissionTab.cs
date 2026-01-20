using Sc.Common.UI;
using TMPro;
using UnityEngine;

namespace Sc.Contents.Event
{
    /// <summary>
    /// 이벤트 미션 탭 (플레이스홀더)
    /// </summary>
    public class EventMissionTab : Widget
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _placeholderText;
        [SerializeField] private GameObject _missionListContainer;

        private string _eventId;
        private string _missionGroupId;

        protected override void OnInitialize()
        {
            Debug.Log("[EventMissionTab] OnInitialize");
        }

        /// <summary>
        /// 미션 탭 설정
        /// </summary>
        public void Setup(string eventId, string missionGroupId)
        {
            _eventId = eventId;
            _missionGroupId = missionGroupId;

            Debug.Log($"[EventMissionTab] Setup - EventId: {eventId}, MissionGroupId: {missionGroupId}");

            RefreshUI();
        }

        private void RefreshUI()
        {
            // 플레이스홀더 표시
            if (_placeholderText != null)
            {
                _placeholderText.text = "미션 시스템 준비 중\n\n" +
                                        "미션 진행 및 보상 수령 기능은\n" +
                                        "추후 업데이트될 예정입니다.";
                _placeholderText.gameObject.SetActive(true);
            }

            if (_missionListContainer != null)
            {
                _missionListContainer.SetActive(false);
            }

            // TODO: 실제 미션 목록 구현
            // 1. EventMissionGroup 로드
            // 2. EventMissionItem[] 동적 생성
            // 3. 미션 진행도 표시
            // 4. 보상 수령 버튼 활성화
        }

        protected override void OnShow()
        {
            Debug.Log("[EventMissionTab] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[EventMissionTab] OnHide");
        }
    }
}
