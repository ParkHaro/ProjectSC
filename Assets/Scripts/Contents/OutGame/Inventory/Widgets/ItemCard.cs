using System;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Inventory.Widgets
{
    /// <summary>
    /// 인벤토리 아이템 카드 위젯.
    /// 아이템 아이콘, 수량, 등급 배경, 선택 상태를 표시.
    /// </summary>
    public class ItemCard : MonoBehaviour
    {
        [Header("Background")] [SerializeField]
        private Image _rarityBackground;

        [SerializeField] private Button _cardButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Item Display")] [SerializeField]
        private Image _itemIcon;

        [SerializeField] private TMP_Text _countLabel;
        [SerializeField] private Image _badgeIcon;
        [SerializeField] private GameObject _badgeContainer;

        [Header("Selection")] [SerializeField] private GameObject _selectedIndicator;

        [Header("Rarity Colors")] [SerializeField]
        private Color _colorN = new Color32(200, 200, 200, 255); // 회색

        [SerializeField] private Color _colorR = new Color32(100, 200, 100, 255); // 초록
        [SerializeField] private Color _colorSR = new Color32(100, 150, 255, 255); // 파랑
        [SerializeField] private Color _colorSSR = new Color32(200, 100, 255, 255); // 보라

        private ItemData _itemData;
        private int _count;
        private bool _isSelected;
        private Action<ItemCard> _onClickCallback;

        /// <summary>
        /// 아이템 데이터
        /// </summary>
        public ItemData ItemData => _itemData;

        /// <summary>
        /// 보유 수량
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// 선택 여부
        /// </summary>
        public bool IsSelected => _isSelected;

        private void Awake()
        {
            if (_cardButton != null)
            {
                _cardButton.onClick.AddListener(HandleClick);
            }
        }

        private void OnDestroy()
        {
            if (_cardButton != null)
            {
                _cardButton.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            _onClickCallback?.Invoke(this);
        }

        /// <summary>
        /// 아이템 설정
        /// </summary>
        public void Setup(ItemData itemData, int count, Action<ItemCard> onClick)
        {
            _itemData = itemData;
            _count = count;
            _onClickCallback = onClick;
            _isSelected = false;

            RefreshUI();
        }

        /// <summary>
        /// 수량 업데이트
        /// </summary>
        public void UpdateCount(int count)
        {
            _count = count;
            UpdateCountLabel();
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;
            UpdateSelectedIndicator();
        }

        /// <summary>
        /// 상호작용 가능 여부 설정
        /// </summary>
        public void SetInteractable(bool interactable)
        {
            if (_cardButton != null)
            {
                _cardButton.interactable = interactable;
            }

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = interactable ? 1f : 0.5f;
            }
        }

        /// <summary>
        /// 뱃지 표시
        /// </summary>
        public void SetBadge(Sprite badgeSprite)
        {
            if (_badgeContainer != null)
            {
                _badgeContainer.SetActive(badgeSprite != null);
            }

            if (_badgeIcon != null && badgeSprite != null)
            {
                _badgeIcon.sprite = badgeSprite;
            }
        }

        private void RefreshUI()
        {
            if (_itemData == null) return;

            UpdateRarityBackground();
            UpdateItemIcon();
            UpdateCountLabel();
            UpdateSelectedIndicator();
            HideBadge();
        }

        private void UpdateRarityBackground()
        {
            if (_rarityBackground == null || _itemData == null) return;

            _rarityBackground.color = GetRarityColor(_itemData.Rarity);
        }

        private Color GetRarityColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.N => _colorN,
                Rarity.R => _colorR,
                Rarity.SR => _colorSR,
                Rarity.SSR => _colorSSR,
                _ => _colorN
            };
        }

        private void UpdateItemIcon()
        {
            // TODO: Addressables로 아이템 아이콘 로드
            // LoadItemIconAsync(_itemData.IconPath);
        }

        private void UpdateCountLabel()
        {
            if (_countLabel != null)
            {
                _countLabel.text = _count > 1 ? $"{_count}" : "";
                _countLabel.gameObject.SetActive(_count > 1);
            }
        }

        private void UpdateSelectedIndicator()
        {
            if (_selectedIndicator != null)
            {
                _selectedIndicator.SetActive(_isSelected);
            }
        }

        private void HideBadge()
        {
            if (_badgeContainer != null)
            {
                _badgeContainer.SetActive(false);
            }
        }
    }
}