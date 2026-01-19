using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Common.UI
{
    /// <summary>
    /// Button을 래핑하는 Widget.
    /// 클릭 이벤트 및 라벨/아이콘 관리 기능 제공.
    /// </summary>
    public class ButtonWidget : Widget
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _icon;

        /// <summary>
        /// 버튼 클릭 시 발생하는 이벤트.
        /// </summary>
        public event Action OnClick;

        public bool Interactable => _button != null && _button.interactable;

        #region Lifecycle

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        protected override void OnBind(object data)
        {
            if (data is string label)
            {
                SetLabel(label);
            }
        }

        protected override void OnRelease()
        {
            OnClick = null;
        }

        #endregion

        #region Public API

        /// <summary>
        /// 버튼 라벨 텍스트 설정.
        /// </summary>
        public void SetLabel(string text)
        {
            if (_label != null)
            {
                _label.text = text ?? string.Empty;
            }
        }

        /// <summary>
        /// 버튼 아이콘 설정.
        /// </summary>
        public void SetIcon(Sprite sprite)
        {
            if (_icon != null)
            {
                _icon.sprite = sprite;
                _icon.enabled = sprite != null;
            }
        }

        /// <summary>
        /// 버튼 활성화/비활성화 설정.
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            if (_button != null)
            {
                _button.interactable = interactable;
            }
        }

        /// <summary>
        /// 프로그래밍 방식으로 클릭 실행.
        /// </summary>
        public void PerformClick()
        {
            if (_button != null && _button.interactable)
            {
                HandleClick();
            }
        }

        #endregion

        #region Private

        private void HandleClick()
        {
            OnClick?.Invoke();
        }

        #endregion
    }
}
