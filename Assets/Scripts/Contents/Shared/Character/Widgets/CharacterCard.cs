using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 캐릭터 카드 위젯.
    /// 캐릭터 목록에서 개별 캐릭터를 표시.
    /// </summary>
    public class CharacterCard : MonoBehaviour
    {
        [Header("Card Background")] [SerializeField]
        private Image _cardBackground;

        [Header("Character Display")] [SerializeField]
        private Image _characterThumbnail;

        [SerializeField] private Image _elementIcon;
        [SerializeField] private Image _roleIcon;

        [Header("Info")] [SerializeField] private Transform _starContainer;
        [SerializeField] private TMP_Text _nameText;

        [Header("Interaction")] [SerializeField]
        private Button _button;

        private string _characterId;

        /// <summary>
        /// 카드 클릭 이벤트
        /// </summary>
        public event Action<string> OnClicked;

        /// <summary>
        /// 속성별 배경 색상
        /// </summary>
        private static readonly Color FireColor = new Color32(255, 150, 150, 255); // 빨강

        private static readonly Color WaterColor = new Color32(150, 150, 255, 255); // 파랑
        private static readonly Color WindColor = new Color32(150, 255, 150, 255); // 초록
        private static readonly Color LightColor = new Color32(255, 255, 150, 255); // 노랑
        private static readonly Color DarkColor = new Color32(200, 150, 255, 255); // 보라
        private static readonly Color DefaultColor = new Color32(200, 200, 200, 255); // 회색

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

        private void HandleClick()
        {
            OnClicked?.Invoke(_characterId);
        }

        /// <summary>
        /// 캐릭터 카드 설정
        /// </summary>
        public void Configure(string characterId, string name, int rarity, ElementType element, RoleType role,
            Sprite thumbnail = null)
        {
            _characterId = characterId;

            // 이름 설정
            if (_nameText != null)
            {
                _nameText.text = name;
            }

            // 속성별 배경색
            if (_cardBackground != null)
            {
                _cardBackground.color = GetElementColor(element);
            }

            // 썸네일
            if (_characterThumbnail != null && thumbnail != null)
            {
                _characterThumbnail.sprite = thumbnail;
            }

            // 별 표시
            UpdateStarRating(rarity);
        }

        /// <summary>
        /// 속성 아이콘 설정
        /// </summary>
        public void SetElementIcon(Sprite sprite)
        {
            if (_elementIcon != null && sprite != null)
            {
                _elementIcon.sprite = sprite;
                _elementIcon.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 역할 아이콘 설정
        /// </summary>
        public void SetRoleIcon(Sprite sprite)
        {
            if (_roleIcon != null && sprite != null)
            {
                _roleIcon.sprite = sprite;
                _roleIcon.gameObject.SetActive(true);
            }
        }

        private void UpdateStarRating(int rarity)
        {
            if (_starContainer == null) return;

            // 별 자식들 활성화/비활성화
            for (int i = 0; i < _starContainer.childCount; i++)
            {
                _starContainer.GetChild(i).gameObject.SetActive(i < rarity);
            }
        }

        private Color GetElementColor(ElementType element)
        {
            return element switch
            {
                ElementType.Fire => FireColor,
                ElementType.Water => WaterColor,
                ElementType.Wind => WindColor,
                ElementType.Light => LightColor,
                ElementType.Dark => DarkColor,
                _ => DefaultColor
            };
        }
    }

    /// <summary>
    /// 캐릭터 속성 타입
    /// </summary>
    public enum ElementType
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Wind = 3,
        Light = 4,
        Dark = 5
    }

    /// <summary>
    /// 캐릭터 역할 타입
    /// </summary>
    public enum RoleType
    {
        None = 0,
        Attacker = 1,
        Defender = 2,
        Supporter = 3,
        Healer = 4
    }
}