using System;
using Sc.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 코스튬 위젯 데이터
    /// </summary>
    public struct CostumeData
    {
        public string CharacterName;
        public Sprite CostumeIcon;
        public int OwnedCount;
        public int TotalCount;
    }

    /// <summary>
    /// 코스튬 위젯.
    /// 캐릭터 상세 화면의 우측 하단에 표시.
    /// 캐릭터 의상 시스템으로의 진입점 역할.
    /// - 코스튬 아이콘
    /// - "{캐릭터이름}의 옷장" 텍스트
    /// </summary>
    public class CostumeWidget : Widget
    {
        [Header("Background")] [SerializeField]
        private Image _background;

        [SerializeField] private Button _button;

        [Header("Icon")] [SerializeField] private Image _costumeIcon;

        [Header("Text")] [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _countText;

        private CostumeData _data;

        // 색상 정의
        private static readonly Color BackgroundColor = new Color32(200, 220, 100, 255); // 연두색
        private static readonly Color TextColor = new Color32(50, 80, 20, 255); // 어두운 녹색

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action OnClicked;

        protected override void OnInitialize()
        {
            Debug.Log("[CostumeWidget] OnInitialize");

            if (_background != null)
            {
                _background.color = BackgroundColor;
            }

            if (_titleText != null)
            {
                _titleText.color = TextColor;
            }

            if (_countText != null)
            {
                _countText.color = TextColor;
            }

            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        /// <summary>
        /// 위젯 설정
        /// </summary>
        public void Configure(CostumeData data)
        {
            _data = data;
            RefreshUI();
        }

        /// <summary>
        /// 캐릭터 이름만 설정
        /// </summary>
        public void SetCharacterName(string characterName)
        {
            _data.CharacterName = characterName;
            if (_titleText != null)
            {
                _titleText.text = $"{characterName}의 옷장";
            }
        }

        /// <summary>
        /// 코스튬 아이콘 설정
        /// </summary>
        public void SetCostumeIcon(Sprite icon)
        {
            _data.CostumeIcon = icon;
            if (_costumeIcon != null && icon != null)
            {
                _costumeIcon.sprite = icon;
                _costumeIcon.gameObject.SetActive(true);
            }
        }

        private void RefreshUI()
        {
            // 타이틀 설정
            if (_titleText != null)
            {
                _titleText.text = $"{_data.CharacterName}의 옷장";
            }

            // 아이콘 설정
            if (_costumeIcon != null)
            {
                if (_data.CostumeIcon != null)
                {
                    _costumeIcon.sprite = _data.CostumeIcon;
                    _costumeIcon.gameObject.SetActive(true);
                }
                else
                {
                    _costumeIcon.gameObject.SetActive(false);
                }
            }

            // 보유 개수 표시 (선택적)
            if (_countText != null)
            {
                if (_data.TotalCount > 0)
                {
                    _countText.text = $"{_data.OwnedCount}/{_data.TotalCount}";
                    _countText.gameObject.SetActive(true);
                }
                else
                {
                    _countText.gameObject.SetActive(false);
                }
            }
        }

        private void HandleClick()
        {
            OnClicked?.Invoke();
        }

        protected override void OnRelease()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }

            OnClicked = null;
            _data = default;
        }
    }
}