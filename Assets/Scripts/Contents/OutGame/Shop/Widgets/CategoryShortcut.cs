using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sc.Data;

namespace Sc.Contents.Shop.Widgets
{
    /// <summary>
    /// 카테고리 바로가기 버튼.
    /// 상품 그리드 하단에 표시되는 추가 카테고리 링크.
    /// </summary>
    public class CategoryShortcut : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _background;

        [Header("Target")] [SerializeField] private ShopProductType _targetCategory;
        [SerializeField] private string _customTargetId;

        /// <summary>
        /// 대상 카테고리
        /// </summary>
        public ShopProductType TargetCategory => _targetCategory;

        /// <summary>
        /// 커스텀 대상 ID (이벤트 상점 등)
        /// </summary>
        public string CustomTargetId => _customTargetId;

        /// <summary>
        /// 클릭 이벤트
        /// </summary>
        public event Action<CategoryShortcut> OnClicked;

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
            OnClicked?.Invoke(this);
        }

        /// <summary>
        /// 바로가기 설정
        /// </summary>
        public void Setup(ShopProductType category, Sprite icon, string label)
        {
            _targetCategory = category;

            if (_icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(icon != null);
            }

            if (_label != null)
            {
                _label.text = label;
            }
        }

        /// <summary>
        /// 커스텀 대상으로 설정 (이벤트 상점 등)
        /// </summary>
        public void SetupCustomTarget(string targetId, Sprite icon, string label)
        {
            _customTargetId = targetId;
            _targetCategory = ShopProductType.EventShop;

            if (_icon != null)
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(icon != null);
            }

            if (_label != null)
            {
                _label.text = label;
            }
        }

        /// <summary>
        /// 활성화 상태 설정
        /// </summary>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        /// <summary>
        /// 라벨 변경
        /// </summary>
        public void SetLabel(string label)
        {
            if (_label != null)
            {
                _label.text = label;
            }
        }
    }
}