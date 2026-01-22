using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby.Widgets
{
    /// <summary>
    /// 퀵메뉴 버튼.
    /// 아이콘 + 라벨로 구성.
    /// </summary>
    public class QuickMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _badge;
        [SerializeField] private TMP_Text _badgeCount;
        [SerializeField] private string _targetScreen;

        public event Action<string> OnClicked;

        public string TargetScreen => _targetScreen;

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
            OnClicked?.Invoke(_targetScreen);
        }

        public void Configure(string targetScreen, Sprite icon, string label)
        {
            _targetScreen = targetScreen;

            if (_icon != null)
                _icon.sprite = icon;

            if (_label != null)
                _label.text = label;
        }

        public void SetBadge(int count)
        {
            if (_badge != null)
            {
                _badge.SetActive(count > 0);
            }

            if (_badgeCount != null)
            {
                _badgeCount.text = count > 99 ? "99+" : count.ToString();
            }
        }
    }
}
