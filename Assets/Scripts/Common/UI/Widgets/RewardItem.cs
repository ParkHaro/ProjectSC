using Sc.Core;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// 개별 보상 아이템 표시 위젯.
    /// RewardPopup에서 동적으로 생성됨.
    /// </summary>
    public class RewardItem : Widget
    {
        [Header("UI References")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _backgroundImage;

        [Header("Fallback")]
        [SerializeField] private Sprite _fallbackIcon;
        [SerializeField] private Color _fallbackColor = Color.gray;

        private RewardInfo _reward;

        /// <summary>
        /// 보상 정보와 아이콘으로 UI 바인딩
        /// </summary>
        public void Bind(RewardInfo reward, Sprite icon = null)
        {
            _reward = reward;

            // 아이콘
            if (_iconImage != null)
            {
                _iconImage.sprite = icon ?? _fallbackIcon;
                _iconImage.enabled = _iconImage.sprite != null;
            }

            // 수량 텍스트
            if (_amountText != null)
            {
                _amountText.text = FormatAmount(reward.Amount);
            }

            // 이름 텍스트 (선택적)
            if (_nameText != null)
            {
                _nameText.text = RewardHelper.GetDisplayName(reward);
            }

            // 희귀도 색상
            var rarityColor = RewardHelper.GetRarityColor(reward);
            ApplyRarityColor(rarityColor);
        }

        private string FormatAmount(int amount)
        {
            if (amount >= 10000)
            {
                return $"x{amount / 10000f:0.#}만";
            }
            if (amount >= 1000)
            {
                return $"x{amount:N0}";
            }
            return $"x{amount}";
        }

        private void ApplyRarityColor(Color color)
        {
            if (_frameImage != null)
            {
                _frameImage.color = color;
            }

            if (_backgroundImage != null)
            {
                // 배경은 희귀도 색상의 투명 버전
                var bgColor = color;
                bgColor.a = 0.2f;
                _backgroundImage.color = bgColor;
            }
        }

        /// <summary>
        /// 현재 바인딩된 보상 정보
        /// </summary>
        public RewardInfo GetReward() => _reward;

        protected override void OnRelease()
        {
            _reward = default;
        }
    }
}
