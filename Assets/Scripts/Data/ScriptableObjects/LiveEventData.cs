using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 라이브 이벤트 마스터 데이터
    /// </summary>
    [CreateAssetMenu(fileName = "LiveEventData", menuName = "SC/Data/LiveEventData")]
    public class LiveEventData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private LiveEventType _eventType;
        [SerializeField] private string _nameKey;
        [SerializeField] private string _descriptionKey;
        [SerializeField] private string _bannerImage;

        [Header("기간")]
        [SerializeField] private string _startTime;
        [SerializeField] private string _endTime;

        [Header("서브 컨텐츠")]
        [SerializeField] private List<EventSubContent> _subContents = new();

        [Header("이벤트 재화")]
        [SerializeField] private bool _hasEventCurrency;
        [SerializeField] private EventCurrencyPolicy _currencyPolicy;

        [Header("표시")]
        [SerializeField] private int _displayOrder;
        [SerializeField] private bool _showCountdown;
        [SerializeField] private string _previewStartTime;

        #region Properties

        public string Id => _id;
        public LiveEventType EventType => _eventType;
        public string NameKey => _nameKey;
        public string DescriptionKey => _descriptionKey;
        public string BannerImage => _bannerImage;
        public string StartTimeStr => _startTime;
        public string EndTimeStr => _endTime;
        public IReadOnlyList<EventSubContent> SubContents => _subContents;
        public bool HasEventCurrency => _hasEventCurrency;
        public EventCurrencyPolicy CurrencyPolicy => _currencyPolicy;
        public int DisplayOrder => _displayOrder;
        public bool ShowCountdown => _showCountdown;
        public string PreviewStartTime => _previewStartTime;

        #endregion

        #region Time Helpers

        /// <summary>
        /// 시작 시간 (UTC)
        /// </summary>
        public DateTime StartTime => ParseDateTime(_startTime);

        /// <summary>
        /// 종료 시간 (UTC)
        /// </summary>
        public DateTime EndTime => ParseDateTime(_endTime);

        /// <summary>
        /// 이벤트 활성 여부
        /// </summary>
        public bool IsActive(DateTime serverTime)
        {
            return serverTime >= StartTime && serverTime < EndTime;
        }

        /// <summary>
        /// 유예 기간 중 여부
        /// </summary>
        public bool IsInGracePeriod(DateTime serverTime)
        {
            if (!_hasEventCurrency) return false;
            return serverTime >= EndTime && serverTime < EndTime.AddDays(_currencyPolicy.GracePeriodDays);
        }

        /// <summary>
        /// 유예 기간 만료 여부
        /// </summary>
        public bool IsGracePeriodExpired(DateTime serverTime)
        {
            if (!_hasEventCurrency) return false;
            return serverTime >= EndTime.AddDays(_currencyPolicy.GracePeriodDays);
        }

        /// <summary>
        /// 남은 일수 (Ceiling 적용)
        /// </summary>
        public int GetRemainingDays(DateTime serverTime)
        {
            var timeSpan = EndTime - serverTime;
            return Math.Max(0, (int)Math.Ceiling(timeSpan.TotalDays));
        }

        /// <summary>
        /// 유예 기간 남은 일수 (Ceiling 적용)
        /// </summary>
        public int GetGracePeriodRemainingDays(DateTime serverTime)
        {
            if (!IsInGracePeriod(serverTime)) return 0;
            var gracePeriodEnd = EndTime.AddDays(_currencyPolicy.GracePeriodDays);
            var timeSpan = gracePeriodEnd - serverTime;
            return Math.Max(0, (int)Math.Ceiling(timeSpan.TotalDays));
        }

        private DateTime ParseDateTime(string dateTimeStr)
        {
            if (string.IsNullOrEmpty(dateTimeStr)) return DateTime.MinValue;
            if (DateTime.TryParse(dateTimeStr, out var result))
                return result;
            return DateTime.MinValue;
        }

        #endregion

        #region SubContent Helpers

        /// <summary>
        /// 타입별 서브 컨텐츠 조회
        /// </summary>
        public EventSubContent? GetSubContent(EventSubContentType type)
        {
            foreach (var subContent in _subContents)
            {
                if (subContent.Type == type)
                    return subContent;
            }
            return null;
        }

        /// <summary>
        /// 해금된 서브 컨텐츠 목록
        /// </summary>
        public IEnumerable<EventSubContent> GetUnlockedSubContents()
        {
            return _subContents.Where(s => s.IsUnlocked).OrderBy(s => s.TabOrder);
        }

        /// <summary>
        /// 서브 컨텐츠 포함 여부
        /// </summary>
        public bool HasSubContent(EventSubContentType type)
        {
            return GetSubContent(type).HasValue;
        }

        #endregion

#if UNITY_EDITOR
        public void Initialize(
            string id,
            LiveEventType eventType,
            string nameKey,
            string descriptionKey,
            string bannerImage,
            string startTime,
            string endTime,
            List<EventSubContent> subContents,
            bool hasEventCurrency,
            EventCurrencyPolicy currencyPolicy,
            int displayOrder,
            bool showCountdown)
        {
            _id = id;
            _eventType = eventType;
            _nameKey = nameKey;
            _descriptionKey = descriptionKey;
            _bannerImage = bannerImage;
            _startTime = startTime;
            _endTime = endTime;
            _subContents = subContents ?? new List<EventSubContent>();
            _hasEventCurrency = hasEventCurrency;
            _currencyPolicy = currencyPolicy;
            _displayOrder = displayOrder;
            _showCountdown = showCountdown;
        }
#endif
    }
}
