using Sc.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Character.Widgets
{
    /// <summary>
    /// 전투력 위젯.
    /// 캐릭터 상세 화면의 우측 상단에 표시되는 전투력 배지.
    /// 초록색 배경에 전투력 아이콘과 수치 표시.
    /// </summary>
    public class CombatPowerWidget : Widget
    {
        [Header("Background")]
        [SerializeField] private Image _background;

        [Header("Icon")]
        [SerializeField] private Image _powerIcon;

        [Header("Text")]
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private TMP_Text _valueText;

        private int _combatPower;

        // 색상 정의
        private static readonly Color BackgroundColor = new Color32(34, 197, 94, 255); // 초록색
        private static readonly Color TextColor = Color.white;

        protected override void OnInitialize()
        {
            Debug.Log("[CombatPowerWidget] OnInitialize");

            // 기본 배경색 설정
            if (_background != null)
            {
                _background.color = BackgroundColor;
            }

            // 라벨 텍스트 설정
            if (_labelText != null)
            {
                _labelText.text = "전투력";
                _labelText.color = TextColor;
            }

            // 값 텍스트 색상 설정
            if (_valueText != null)
            {
                _valueText.color = TextColor;
            }
        }

        /// <summary>
        /// 전투력 설정
        /// </summary>
        public void SetCombatPower(int power)
        {
            _combatPower = power;
            RefreshUI();
        }

        /// <summary>
        /// 현재 전투력
        /// </summary>
        public int CombatPower => _combatPower;

        private void RefreshUI()
        {
            if (_valueText != null)
            {
                _valueText.text = _combatPower.ToString("N0");
            }
        }

        /// <summary>
        /// 전투력 증가 애니메이션 (전투력 변화 시 사용)
        /// </summary>
        public void AnimatePowerChange(int fromPower, int toPower, float duration = 0.5f)
        {
            // TODO: DOTween을 사용한 애니메이션 구현
            // 현재는 단순 설정
            SetCombatPower(toPower);
        }

        protected override void OnRelease()
        {
            _combatPower = 0;
        }
    }
}
