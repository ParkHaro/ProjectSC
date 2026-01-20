using Sc.Common.UI;
using TMPro;
using UnityEngine;

namespace Sc.Contents.Event
{
    /// <summary>
    /// 이벤트 스테이지 탭 (플레이스홀더)
    /// </summary>
    public class EventStageTab : Widget
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _placeholderText;
        [SerializeField] private GameObject _stageListContainer;

        private string _eventId;
        private string _stageGroupId;

        protected override void OnInitialize()
        {
            Debug.Log("[EventStageTab] OnInitialize");
        }

        /// <summary>
        /// 스테이지 탭 설정
        /// </summary>
        public void Setup(string eventId, string stageGroupId)
        {
            _eventId = eventId;
            _stageGroupId = stageGroupId;

            Debug.Log($"[EventStageTab] Setup - EventId: {eventId}, StageGroupId: {stageGroupId}");

            RefreshUI();
        }

        private void RefreshUI()
        {
            // 플레이스홀더 표시
            if (_placeholderText != null)
            {
                _placeholderText.text = "스테이지 시스템 준비 중\n\n" +
                                        "이벤트 스테이지 기능은\n" +
                                        "추후 업데이트될 예정입니다.";
                _placeholderText.gameObject.SetActive(true);
            }

            if (_stageListContainer != null)
            {
                _stageListContainer.SetActive(false);
            }

            // TODO: 실제 스테이지 목록 구현
            // 1. StageListScreen 재사용
            // 2. Event 타입 필터 적용
            // 3. PresetGroupId: event_{eventId}
        }

        protected override void OnShow()
        {
            Debug.Log("[EventStageTab] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[EventStageTab] OnHide");
        }
    }
}
