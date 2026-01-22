using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby.Widgets
{
    /// <summary>
    /// 패스 버튼 (레벨패스, 사록패스, 트라이얼패스, 스텝업패키지).
    /// </summary>
    public class PassButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _newBadge;
        [SerializeField] private string _passType;

        public event Action<string> OnClicked;

        public string PassType => _passType;

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
            OnClicked?.Invoke(_passType);
        }

        public void Configure(string passType, Sprite icon, string label)
        {
            _passType = passType;

            if (_icon != null)
                _icon.sprite = icon;

            if (_label != null)
                _label.text = label;
        }

        public void SetNewBadge(bool show)
        {
            if (_newBadge != null)
            {
                _newBadge.SetActive(show);
            }
        }
    }
}
