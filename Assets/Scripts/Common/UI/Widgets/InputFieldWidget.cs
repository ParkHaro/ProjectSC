using System;
using TMPro;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// TMP_InputField를 래핑하는 Widget.
    /// 텍스트 입력 및 검증 기능 제공.
    /// </summary>
    public class InputFieldWidget : Widget
    {
        [SerializeField] private TMP_InputField _inputField;

        /// <summary>
        /// 텍스트 변경 시 발생하는 이벤트.
        /// </summary>
        public event Action<string> OnValueChanged;

        /// <summary>
        /// 입력 완료 시 발생하는 이벤트 (Enter 또는 포커스 아웃).
        /// </summary>
        public event Action<string> OnEndEdit;

        /// <summary>
        /// 입력 제출 시 발생하는 이벤트 (Enter만).
        /// </summary>
        public event Action<string> OnSubmit;

        public string Text => _inputField != null ? _inputField.text : string.Empty;
        public bool IsFocused => _inputField != null && _inputField.isFocused;

        #region Lifecycle

        protected override void OnInitialize()
        {
            if (_inputField != null)
            {
                _inputField.onValueChanged.AddListener(HandleValueChanged);
                _inputField.onEndEdit.AddListener(HandleEndEdit);
                _inputField.onSubmit.AddListener(HandleSubmit);
            }
        }

        protected override void OnBind(object data)
        {
            if (data is string text)
            {
                SetText(text);
            }
        }

        protected override void OnRelease()
        {
            OnValueChanged = null;
            OnEndEdit = null;
            OnSubmit = null;
        }

        #endregion

        #region Public API

        /// <summary>
        /// 텍스트 설정.
        /// </summary>
        public void SetText(string text)
        {
            if (_inputField != null)
            {
                _inputField.SetTextWithoutNotify(text ?? string.Empty);
            }
        }

        /// <summary>
        /// 텍스트 설정 (이벤트 발생).
        /// </summary>
        public void SetTextWithNotify(string text)
        {
            if (_inputField != null)
            {
                _inputField.text = text ?? string.Empty;
            }
        }

        /// <summary>
        /// 플레이스홀더 텍스트 설정.
        /// </summary>
        public void SetPlaceholder(string text)
        {
            if (_inputField != null && _inputField.placeholder is TMP_Text placeholder)
            {
                placeholder.text = text ?? string.Empty;
            }
        }

        /// <summary>
        /// 입력 필드 활성화/비활성화.
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            if (_inputField != null)
            {
                _inputField.interactable = interactable;
            }
        }

        /// <summary>
        /// 글자 수 제한 설정.
        /// </summary>
        public void SetCharacterLimit(int limit)
        {
            if (_inputField != null)
            {
                _inputField.characterLimit = limit;
            }
        }

        /// <summary>
        /// 입력 타입 설정.
        /// </summary>
        public void SetContentType(TMP_InputField.ContentType contentType)
        {
            if (_inputField != null)
            {
                _inputField.contentType = contentType;
            }
        }

        /// <summary>
        /// 포커스 활성화.
        /// </summary>
        public void ActivateInputField()
        {
            if (_inputField != null)
            {
                _inputField.ActivateInputField();
            }
        }

        /// <summary>
        /// 포커스 비활성화.
        /// </summary>
        public void DeactivateInputField()
        {
            if (_inputField != null)
            {
                _inputField.DeactivateInputField();
            }
        }

        /// <summary>
        /// 현재 텍스트 반환.
        /// </summary>
        public string GetText()
        {
            return _inputField != null ? _inputField.text : string.Empty;
        }

        #endregion

        #region Private

        private void HandleValueChanged(string value)
        {
            OnValueChanged?.Invoke(value);
        }

        private void HandleEndEdit(string value)
        {
            OnEndEdit?.Invoke(value);
        }

        private void HandleSubmit(string value)
        {
            OnSubmit?.Invoke(value);
        }

        #endregion
    }
}
