using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// Toggle을 래핑하는 Widget.
    /// ON/OFF 스위치 기능 제공.
    /// </summary>
    public class ToggleWidget : Widget
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TMP_Text _label;

        /// <summary>
        /// 토글 값 변경 시 발생하는 이벤트.
        /// </summary>
        public event Action<bool> OnValueChanged;

        public bool IsOn => _toggle != null && _toggle.isOn;

        #region Lifecycle

        protected override void OnInitialize()
        {
            if (_toggle != null)
            {
                _toggle.onValueChanged.AddListener(HandleValueChanged);
            }
        }

        protected override void OnBind(object data)
        {
            if (data is bool value)
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
        /// 토글 값 설정.
        /// </summary>
        public void SetValue(bool isOn)
        {
            if (_toggle != null)
            {
                _toggle.SetIsOnWithoutNotify(isOn);
            }
        }

        /// <summary>
        /// 토글 값 설정 (이벤트 발생).
        /// </summary>
        public void SetValueWithNotify(bool isOn)
        {
            if (_toggle != null)
            {
                _toggle.isOn = isOn;
            }
        }

        /// <summary>
        /// 라벨 텍스트 설정.
        /// </summary>
        public void SetLabel(string text)
        {
            if (_label != null)
            {
                _label.text = text ?? string.Empty;
            }
        }

        /// <summary>
        /// 토글 활성화/비활성화.
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            if (_toggle != null)
            {
                _toggle.interactable = interactable;
            }
        }

        /// <summary>
        /// 토글 그룹 설정.
        /// </summary>
        public void SetGroup(ToggleGroup group)
        {
            if (_toggle != null)
            {
                _toggle.group = group;
            }
        }

        /// <summary>
        /// 현재 값 반환.
        /// </summary>
        public bool GetValue()
        {
            return _toggle != null && _toggle.isOn;
        }

        #endregion

        #region Private

        private void HandleValueChanged(bool isOn)
        {
            OnValueChanged?.Invoke(isOn);
        }

        #endregion
    }
}
