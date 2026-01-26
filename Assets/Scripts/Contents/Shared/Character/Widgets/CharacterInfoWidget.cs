using System;
using Sc.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 캐릭터 정보 위젯 데이터
    /// </summary>
    public struct CharacterInfoData
    {
        public string CharacterId;
        public string Name;
        public int Rarity;
        public int Level;
        public int Ascension;
        public ElementType Element;
        public RoleType Role;
        public PersonalityType Personality;
        public AttackType Attack;
        public PositionType Position;
        public Sprite CharacterImage;
        public Sprite CompanionImage;
    }

    /// <summary>
    /// 성격 타입
    /// </summary>
    public enum PersonalityType
    {
        None = 0,
        Active,     // 활발
        Calm,       // 차분
        Shy,        // 수줍음
        Bold        // 대담
    }

    /// <summary>
    /// 공격 타입
    /// </summary>
    public enum AttackType
    {
        None = 0,
        Physical,   // 물리
        Magic       // 마법
    }

    /// <summary>
    /// 배치 타입
    /// </summary>
    public enum PositionType
    {
        None = 0,
        Front,      // 전열
        Back        // 후열
    }

    /// <summary>
    /// 캐릭터 정보 위젯.
    /// 캐릭터 상세 화면의 하단 영역에 표시되는 기본 정보.
    /// - 희귀도 뱃지, 이름, 태그 그룹 (성격, 역할, 공격타입, 배치)
    /// </summary>
    public class CharacterInfoWidget : Widget
    {
        [Header("Rarity")]
        [SerializeField] private Image _rarityBadge;
        [SerializeField] private TMP_Text _rarityText;

        [Header("Name")]
        [SerializeField] private TMP_Text _nameText;

        [Header("Tag Group")]
        [SerializeField] private Transform _tagContainer;
        [SerializeField] private Image _personalityTag;
        [SerializeField] private TMP_Text _personalityText;
        [SerializeField] private Image _roleTag;
        [SerializeField] private TMP_Text _roleText;
        [SerializeField] private Image _attackTypeTag;
        [SerializeField] private TMP_Text _attackTypeText;
        [SerializeField] private Image _positionTag;
        [SerializeField] private TMP_Text _positionText;

        private CharacterInfoData _data;

        #region Tag Colors

        private static readonly Color PersonalityColor = new Color32(255, 165, 80, 255);  // 주황색
        private static readonly Color RoleColor = new Color32(255, 150, 200, 255);        // 분홍색
        private static readonly Color AttackTypeColor = new Color32(255, 220, 100, 255);  // 노란색
        private static readonly Color PositionColor = new Color32(255, 100, 100, 255);    // 빨간색

        #endregion

        protected override void OnInitialize()
        {
            Debug.Log("[CharacterInfoWidget] OnInitialize");
        }

        /// <summary>
        /// 위젯 설정
        /// </summary>
        public void Configure(CharacterInfoData data)
        {
            _data = data;
            RefreshUI();
        }

        /// <summary>
        /// 이름만 업데이트
        /// </summary>
        public void SetName(string name)
        {
            _data.Name = name;
            if (_nameText != null)
            {
                _nameText.text = name;
            }
        }

        private void RefreshUI()
        {
            // 희귀도 뱃지
            if (_rarityText != null)
            {
                _rarityText.text = _data.Rarity.ToString();
            }
            if (_rarityBadge != null)
            {
                _rarityBadge.color = GetRarityColor(_data.Rarity);
            }

            // 이름
            if (_nameText != null)
            {
                _nameText.text = _data.Name;
            }

            // 태그들
            UpdateTag(_personalityTag, _personalityText, GetPersonalityText(_data.Personality), PersonalityColor);
            UpdateTag(_roleTag, _roleText, GetRoleText(_data.Role), RoleColor);
            UpdateTag(_attackTypeTag, _attackTypeText, GetAttackTypeText(_data.Attack), AttackTypeColor);
            UpdateTag(_positionTag, _positionText, GetPositionText(_data.Position), PositionColor);
        }

        private void UpdateTag(Image tagBg, TMP_Text tagText, string text, Color color)
        {
            if (tagBg != null)
            {
                tagBg.color = color;
            }
            if (tagText != null)
            {
                tagText.text = text;
            }
        }

        #region Helper Methods

        private Color GetRarityColor(int rarity)
        {
            return rarity switch
            {
                5 => new Color32(255, 215, 0, 255),   // 금색
                4 => new Color32(168, 85, 247, 255), // 보라색
                3 => new Color32(59, 130, 246, 255), // 파란색
                2 => new Color32(34, 197, 94, 255),  // 초록색
                _ => new Color32(156, 163, 175, 255) // 회색
            };
        }

        private string GetPersonalityText(PersonalityType personality)
        {
            return personality switch
            {
                PersonalityType.Active => "활발",
                PersonalityType.Calm => "차분",
                PersonalityType.Shy => "수줍음",
                PersonalityType.Bold => "대담",
                _ => ""
            };
        }

        private string GetRoleText(RoleType role)
        {
            return role switch
            {
                RoleType.Attacker => "어태커",
                RoleType.Defender => "디펜더",
                RoleType.Supporter => "서포터",
                RoleType.Healer => "힐러",
                _ => ""
            };
        }

        private string GetAttackTypeText(AttackType attackType)
        {
            return attackType switch
            {
                AttackType.Physical => "물리",
                AttackType.Magic => "마법",
                _ => ""
            };
        }

        private string GetPositionText(PositionType position)
        {
            return position switch
            {
                PositionType.Front => "전열",
                PositionType.Back => "후열",
                _ => ""
            };
        }

        #endregion

        protected override void OnRelease()
        {
            _data = default;
        }
    }
}
