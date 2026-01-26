using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 빠른 입장 버튼, 자동 반복 설정, 스킵 티켓 사용 등
    /// 빠른 액션 버튼들을 담당하는 Widget.
    /// </summary>
    public class QuickActionWidget : MonoBehaviour
    {
        [Header("Quick Entry")]
        [SerializeField] private Button _quickEntryButton;
        [SerializeField] private TMP_Text _quickEntryText;
        [SerializeField] private Image _quickEntryIcon;

        [Header("Auto Repeat")]
        [SerializeField] private Button _autoRepeatButton;
        [SerializeField] private TMP_Text _autoRepeatText;
        [SerializeField] private Image _autoRepeatToggleIcon;
        [SerializeField] private GameObject _autoRepeatOnIndicator;

        [Header("Skip Ticket")]
        [SerializeField] private Button _skipTicketButton;
        [SerializeField] private TMP_Text _skipTicketCountText;
        [SerializeField] private Image _skipTicketIcon;

        [Header("Deck Formation")]
        [SerializeField] private Button _deckFormationButton;
        [SerializeField] private TMP_Text _deckFormationText;

        [Header("Colors")]
        [SerializeField] private Color _enabledColor = new Color32(100, 180, 100, 255);
        [SerializeField] private Color _disabledColor = new Color32(80, 80, 80, 200);
        [SerializeField] private Color _activeToggleColor = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _inactiveToggleColor = new Color32(150, 150, 150, 255);

        private bool _isAutoRepeatEnabled;
        private int _skipTicketCount;
        private bool _isQuickEntryAvailable;

        /// <summary>
        /// 빠른 입장 버튼 클릭 이벤트.
        /// </summary>
        public event Action OnQuickEntryClicked;

        /// <summary>
        /// 자동 반복 토글 이벤트.
        /// </summary>
        public event Action<bool> OnAutoRepeatToggled;

        /// <summary>
        /// 스킵 티켓 사용 버튼 클릭 이벤트.
        /// </summary>
        public event Action OnSkipTicketClicked;

        /// <summary>
        /// 덱 편성 버튼 클릭 이벤트.
        /// </summary>
        public event Action OnDeckFormationClicked;

        private void Awake()
        {
            SetupButtonListeners();
        }

        private void OnDestroy()
        {
            RemoveButtonListeners();
        }

        private void SetupButtonListeners()
        {
            if (_quickEntryButton != null)
            {
                _quickEntryButton.onClick.AddListener(HandleQuickEntryClicked);
            }

            if (_autoRepeatButton != null)
            {
                _autoRepeatButton.onClick.AddListener(HandleAutoRepeatClicked);
            }

            if (_skipTicketButton != null)
            {
                _skipTicketButton.onClick.AddListener(HandleSkipTicketClicked);
            }

            if (_deckFormationButton != null)
            {
                _deckFormationButton.onClick.AddListener(HandleDeckFormationClicked);
            }
        }

        private void RemoveButtonListeners()
        {
            if (_quickEntryButton != null)
            {
                _quickEntryButton.onClick.RemoveListener(HandleQuickEntryClicked);
            }

            if (_autoRepeatButton != null)
            {
                _autoRepeatButton.onClick.RemoveListener(HandleAutoRepeatClicked);
            }

            if (_skipTicketButton != null)
            {
                _skipTicketButton.onClick.RemoveListener(HandleSkipTicketClicked);
            }

            if (_deckFormationButton != null)
            {
                _deckFormationButton.onClick.RemoveListener(HandleDeckFormationClicked);
            }
        }

        /// <summary>
        /// 빠른 입장 버튼 설정.
        /// </summary>
        /// <param name="isAvailable">사용 가능 여부 (클리어한 스테이지만 가능)</param>
        /// <param name="buttonText">버튼 텍스트 (기본: "빠른전투")</param>
        public void SetQuickEntryState(bool isAvailable, string buttonText = null)
        {
            _isQuickEntryAvailable = isAvailable;

            if (_quickEntryButton != null)
            {
                _quickEntryButton.interactable = isAvailable;
            }

            if (_quickEntryText != null)
            {
                _quickEntryText.text = buttonText ?? (isAvailable ? "빠른전투" : "빠른전투불가");
                _quickEntryText.color = isAvailable ? Color.white : new Color(1f, 1f, 1f, 0.5f);
            }

            if (_quickEntryIcon != null)
            {
                _quickEntryIcon.color = isAvailable ? _enabledColor : _disabledColor;
            }
        }

        /// <summary>
        /// 자동 반복 상태 설정.
        /// </summary>
        /// <param name="isEnabled">자동 반복 활성화 여부</param>
        public void SetAutoRepeatState(bool isEnabled)
        {
            _isAutoRepeatEnabled = isEnabled;

            if (_autoRepeatText != null)
            {
                _autoRepeatText.text = isEnabled ? "자동ON" : "자동OFF";
            }

            if (_autoRepeatToggleIcon != null)
            {
                _autoRepeatToggleIcon.color = isEnabled ? _activeToggleColor : _inactiveToggleColor;
            }

            if (_autoRepeatOnIndicator != null)
            {
                _autoRepeatOnIndicator.SetActive(isEnabled);
            }
        }

        /// <summary>
        /// 스킵 티켓 정보 설정.
        /// </summary>
        /// <param name="ticketCount">보유 티켓 수</param>
        public void SetSkipTicketCount(int ticketCount)
        {
            _skipTicketCount = ticketCount;

            bool hasTickets = ticketCount > 0;

            if (_skipTicketButton != null)
            {
                _skipTicketButton.interactable = hasTickets;
            }

            if (_skipTicketCountText != null)
            {
                _skipTicketCountText.text = ticketCount.ToString();
                _skipTicketCountText.color = hasTickets ? Color.white : new Color(1f, 1f, 1f, 0.5f);
            }

            if (_skipTicketIcon != null)
            {
                _skipTicketIcon.color = hasTickets ? _enabledColor : _disabledColor;
            }
        }

        /// <summary>
        /// 덱 편성 버튼 텍스트 설정.
        /// </summary>
        /// <param name="buttonText">버튼 텍스트</param>
        public void SetDeckFormationText(string buttonText)
        {
            if (_deckFormationText != null)
            {
                _deckFormationText.text = buttonText ?? "덱 편성";
            }
        }

        /// <summary>
        /// 버튼 표시 여부 개별 설정.
        /// </summary>
        public void SetButtonVisibility(
            bool showQuickEntry = true,
            bool showAutoRepeat = true,
            bool showSkipTicket = true,
            bool showDeckFormation = true)
        {
            if (_quickEntryButton != null)
            {
                _quickEntryButton.gameObject.SetActive(showQuickEntry);
            }

            if (_autoRepeatButton != null)
            {
                _autoRepeatButton.gameObject.SetActive(showAutoRepeat);
            }

            if (_skipTicketButton != null)
            {
                _skipTicketButton.gameObject.SetActive(showSkipTicket);
            }

            if (_deckFormationButton != null)
            {
                _deckFormationButton.gameObject.SetActive(showDeckFormation);
            }
        }

        /// <summary>
        /// 위젯 전체 초기화.
        /// </summary>
        public void Clear()
        {
            SetQuickEntryState(false);
            SetAutoRepeatState(false);
            SetSkipTicketCount(0);
            SetDeckFormationText("덱 편성");
        }

        private void HandleQuickEntryClicked()
        {
            if (_isQuickEntryAvailable)
            {
                OnQuickEntryClicked?.Invoke();
            }
        }

        private void HandleAutoRepeatClicked()
        {
            _isAutoRepeatEnabled = !_isAutoRepeatEnabled;
            SetAutoRepeatState(_isAutoRepeatEnabled);
            OnAutoRepeatToggled?.Invoke(_isAutoRepeatEnabled);
        }

        private void HandleSkipTicketClicked()
        {
            if (_skipTicketCount > 0)
            {
                OnSkipTicketClicked?.Invoke();
            }
        }

        private void HandleDeckFormationClicked()
        {
            OnDeckFormationClicked?.Invoke();
        }
    }
}
