using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 마일스톤 아이템 컴포넌트.
    /// 별 진행도에서 보상 마일스톤을 표시합니다.
    /// </summary>
    public class MilestoneItem : MonoBehaviour
    {
        [Header("Display")] [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private TMP_Text _requiredStarsText;

        [Header("Interaction")] [SerializeField]
        private Button _button;

        [Header("Status Indicators")] [SerializeField]
        private GameObject _reachedIndicator;

        [SerializeField] private GameObject _claimedIndicator;
        [SerializeField] private GameObject _lockedIndicator;

        [Header("Colors")] [SerializeField] private Color _reachedColor = new Color(0.4f, 0.8f, 0.4f, 1f);
        [SerializeField] private Color _unReachedColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
        [SerializeField] private Color _claimedColor = new Color(0.3f, 0.3f, 0.3f, 0.6f);

        private int _requiredStars;
        private int _reward;
        private bool _isReached;
        private bool _isClaimed;

        /// <summary>
        /// 필요한 별 수
        /// </summary>
        public int RequiredStars => _requiredStars;

        /// <summary>
        /// 보상 수량
        /// </summary>
        public int Reward => _reward;

        /// <summary>
        /// 달성 여부
        /// </summary>
        public bool IsReached => _isReached;

        /// <summary>
        /// 수령 여부
        /// </summary>
        public bool IsClaimed => _isClaimed;

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action OnClicked;

        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }
        }

        /// <summary>
        /// 마일스톤 초기화
        /// </summary>
        /// <param name="requiredStars">달성에 필요한 별 수</param>
        /// <param name="reward">보상 수량</param>
        /// <param name="isReached">달성 여부</param>
        /// <param name="isClaimed">수령 여부</param>
        public void Initialize(int requiredStars, int reward, bool isReached, bool isClaimed = false)
        {
            _requiredStars = requiredStars;
            _reward = reward;
            _isReached = isReached;
            _isClaimed = isClaimed;

            UpdateVisual();
        }

        /// <summary>
        /// 달성 상태 설정
        /// </summary>
        public void SetReached(bool reached)
        {
            _isReached = reached;
            UpdateVisual();
        }

        /// <summary>
        /// 수령 상태 설정
        /// </summary>
        public void SetClaimed(bool claimed)
        {
            _isClaimed = claimed;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            // 보상 텍스트
            if (_rewardText != null)
            {
                _rewardText.text = _reward.ToString();
            }

            // 필요 별 수 텍스트
            if (_requiredStarsText != null)
            {
                _requiredStarsText.text = _requiredStars.ToString();
            }

            // 상태 인디케이터
            if (_reachedIndicator != null)
            {
                _reachedIndicator.SetActive(_isReached && !_isClaimed);
            }

            if (_claimedIndicator != null)
            {
                _claimedIndicator.SetActive(_isClaimed);
            }

            if (_lockedIndicator != null)
            {
                _lockedIndicator.SetActive(!_isReached && !_isClaimed);
            }

            // 아이콘 색상
            if (_icon != null)
            {
                if (_isClaimed)
                {
                    _icon.color = _claimedColor;
                }
                else if (_isReached)
                {
                    _icon.color = _reachedColor;
                }
                else
                {
                    _icon.color = _unReachedColor;
                }
            }

            // 버튼 상호작용
            if (_button != null)
            {
                _button.interactable = _isReached && !_isClaimed;
            }
        }

        private void HandleClick()
        {
            OnClicked?.Invoke();
        }
    }
}