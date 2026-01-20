using System;
using Sc.Common.UI;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 인게임 컨텐츠 카테고리 아이템 위젯.
    /// InGameContentDashboard에서 컨텐츠 종류를 표시합니다.
    /// </summary>
    public class ContentCategoryItem : Widget
    {
        [Header("UI References")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _lockIcon;
        [SerializeField] private GameObject _newBadge;

        private InGameContentType _contentType;
        private Action<InGameContentType> _onClickCallback;
        private bool _isLocked;

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(OnClicked);
            }
        }

        /// <summary>
        /// 컨텐츠 카테고리 설정
        /// </summary>
        /// <param name="contentType">컨텐츠 타입</param>
        /// <param name="isLocked">잠금 여부</param>
        /// <param name="hasNew">새 컨텐츠 표시 여부</param>
        /// <param name="onClickCallback">클릭 콜백</param>
        public void Setup(
            InGameContentType contentType,
            bool isLocked,
            bool hasNew,
            Action<InGameContentType> onClickCallback)
        {
            _contentType = contentType;
            _isLocked = isLocked;
            _onClickCallback = onClickCallback;

            RefreshUI(hasNew);
        }

        private void RefreshUI(bool hasNew)
        {
            // 이름 설정
            if (_nameText != null)
            {
                _nameText.text = GetContentName(_contentType);
            }

            // 설명 설정
            if (_descriptionText != null)
            {
                _descriptionText.text = GetContentDescription(_contentType);
            }

            // 잠금 상태
            if (_lockIcon != null)
            {
                _lockIcon.SetActive(_isLocked);
            }

            // 새 컨텐츠 뱃지
            if (_newBadge != null)
            {
                _newBadge.SetActive(hasNew && !_isLocked);
            }

            // 버튼 상호작용
            if (_button != null)
            {
                _button.interactable = !_isLocked;
            }
        }

        private void OnClicked()
        {
            if (_isLocked) return;

            Debug.Log($"[ContentCategoryItem] Clicked: {_contentType}");
            _onClickCallback?.Invoke(_contentType);
        }

        private string GetContentName(InGameContentType type)
        {
            return type switch
            {
                InGameContentType.MainStory => "메인 스토리",
                InGameContentType.HardMode => "하드 모드",
                InGameContentType.GoldDungeon => "골드 던전",
                InGameContentType.ExpDungeon => "경험치 던전",
                InGameContentType.SkillDungeon => "스킬 던전",
                InGameContentType.BossRaid => "보스 레이드",
                InGameContentType.Tower => "무한의 탑",
                InGameContentType.Event => "이벤트",
                _ => type.ToString()
            };
        }

        private string GetContentDescription(InGameContentType type)
        {
            return type switch
            {
                InGameContentType.MainStory => "메인 스토리를 진행하세요",
                InGameContentType.HardMode => "더 어려운 난이도에 도전하세요",
                InGameContentType.GoldDungeon => "골드를 획득할 수 있습니다",
                InGameContentType.ExpDungeon => "경험치를 획득할 수 있습니다",
                InGameContentType.SkillDungeon => "스킬 재료를 획득할 수 있습니다",
                InGameContentType.BossRaid => "강력한 보스에 도전하세요",
                InGameContentType.Tower => "무한한 층에 도전하세요",
                InGameContentType.Event => "기간 한정 이벤트",
                _ => ""
            };
        }

        protected override void OnRelease()
        {
            _onClickCallback = null;

            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClicked);
            }
        }
    }
}
