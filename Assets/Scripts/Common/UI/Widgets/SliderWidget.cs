using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// Slider를 래핑하는 Widget.
    /// 값 조절 및 양방향 바인딩 기능 제공.
    /// </summary>
    public class SliderWidget : Widget
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private string _valueFormat = "{0:F1}";

        /// <summary>
        /// 슬라이더 값 변경 시 발생하는 이벤트.
        /// </summary>
        public event Action<float> OnValueChanged;

        public float Value => _slider != null ? _slider.value : 0f;
        public float MinValue => _slider != null ? _slider.minValue : 0f;
        public float MaxValue => _slider != null ? _slider.maxValue : 1f;

        #region Lifecycle

        protected override void OnInitialize()
        {
            if (_slider != null)
            {
                _slider.onValueChanged.AddListener(HandleValueChanged);
                UpdateValueText(_slider.value);
            }
        }

        protected override void OnBind(object data)
        {
            if (data is float value)
            {
                SetValue(value);
            }
        }

        protected override void OnRelease()
        {
            OnValueChanged = null;
        }

        #endregion

        #region Public API

        /// <summary>
        /// 슬라이더 값 설정.
        /// </summary>
        public void SetValue(float value)
        {
            if (_slider != null)
            {
                _slider.SetValueWithoutNotify(value);
                UpdateValueText(value);
            }
        }

        /// <summary>
        /// 슬라이더 값 설정 (이벤트 발생).
        /// </summary>
        public void SetValueWithNotify(float value)
        {
            if (_slider != null)
            {
                _slider.value = value;
            }
        }

        /// <summary>
        /// 슬라이더 범위 설정.
        /// </summary>
        public void SetRange(float min, float max)
        {
            if (_slider != null)
            {
                _slider.minValue = min;
                _slider.maxValue = max;
            }
        }

        /// <summary>
        /// 정수 단위로 제한.
        /// </summary>
        public void SetWholeNumbers(bool wholeNumbers)
        {
            if (_slider != null)
            {
                _slider.wholeNumbers = wholeNumbers;
            }
        }

        /// <summary>
        /// 슬라이더 활성화/비활성화.
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            if (_slider != null)
            {
                _slider.interactable = interactable;
            }
        }

        /// <summary>
        /// 값 텍스트 포맷 설정.
        /// </summary>
        public void SetValueFormat(string format)
        {
            _valueFormat = format;
            if (_slider != null)
            {
                UpdateValueText(_slider.value);
            }
        }

        /// <summary>
        /// 현재 값 반환.
        /// </summary>
        public float GetValue()
        {
            return _slider != null ? _slider.value : 0f;
        }

        /// <summary>
        /// 정규화된 값 반환 (0~1).
        /// </summary>
        public float GetNormalizedValue()
        {
            return _slider != null ? _slider.normalizedValue : 0f;
        }

        #endregion

        #region Private

        private void HandleValueChanged(float value)
        {
            UpdateValueText(value);
            OnValueChanged?.Invoke(value);
        }

        private void UpdateValueText(float value)
        {
            if (_valueText != null && !string.IsNullOrEmpty(_valueFormat))
            {
                _valueText.text = string.Format(_valueFormat, value);
            }
        }

        #endregion
    }
}
