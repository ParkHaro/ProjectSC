using System;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Inventory.Widgets
{
    /// <summary>
    /// 아이템 상세 정보 위젯.
    /// 선택된 아이템의 상세 정보, 설명, 사용/판매 버튼 표시.
    /// </summary>
    public class ItemDetailWidget : MonoBehaviour
    {
        [Header("Empty State")]
        [SerializeField] private GameObject _emptyState;
        [SerializeField] private TMP_Text _emptyStateText;

        [Header("Detail View")]
        [SerializeField] private GameObject _detailView;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _rarityFrame;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemDescription;
        [SerializeField] private TMP_Text _itemStats;

        [Header("Action Buttons")]
        [SerializeField] private Button _useButton;
        [SerializeField] private TMP_Text _useButtonText;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _sellButtonText;

        [Header("Rarity Frame Colors")]
        [SerializeField] private Color _frameColorN = new Color32(180, 180, 180, 255);
        [SerializeField] private Color _frameColorR = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _frameColorSR = new Color32(100, 150, 255, 255);
        [SerializeField] private Color _frameColorSSR = new Color32(200, 100, 255, 255);

        private ItemData _currentItem;
        private int _currentCount;
        private Action<ItemData> _onUseCallback;
        private Action<ItemData> _onSellCallback;

        /// <summary>
        /// 현재 표시 중인 아이템
        /// </summary>
        public ItemData CurrentItem => _currentItem;

        private void Awake()
        {
            if (_useButton != null)
            {
                _useButton.onClick.AddListener(HandleUseClick);
            }

            if (_sellButton != null)
            {
                _sellButton.onClick.AddListener(HandleSellClick);
            }

            // 초기 상태: EmptyState 표시
            ShowEmptyState();
        }

        private void OnDestroy()
        {
            if (_useButton != null)
            {
                _useButton.onClick.RemoveListener(HandleUseClick);
            }

            if (_sellButton != null)
            {
                _sellButton.onClick.RemoveListener(HandleSellClick);
            }
        }

        private void HandleUseClick()
        {
            if (_currentItem != null)
            {
                _onUseCallback?.Invoke(_currentItem);
            }
        }

        private void HandleSellClick()
        {
            if (_currentItem != null)
            {
                _onSellCallback?.Invoke(_currentItem);
            }
        }

        /// <summary>
        /// 콜백 설정
        /// </summary>
        public void SetCallbacks(Action<ItemData> onUse, Action<ItemData> onSell)
        {
            _onUseCallback = onUse;
            _onSellCallback = onSell;
        }

        /// <summary>
        /// 아이템 상세 표시
        /// </summary>
        public void ShowItem(ItemData item, int count)
        {
            _currentItem = item;
            _currentCount = count;

            if (item == null)
            {
                ShowEmptyState();
                return;
            }

            ShowDetailView();
            RefreshUI();
        }

        /// <summary>
        /// 빈 상태 표시
        /// </summary>
        public void ShowEmptyState(string message = "아이템을 선택해주세요")
        {
            _currentItem = null;

            if (_emptyState != null)
            {
                _emptyState.SetActive(true);
            }

            if (_emptyStateText != null)
            {
                _emptyStateText.text = message;
            }

            if (_detailView != null)
            {
                _detailView.SetActive(false);
            }
        }

        /// <summary>
        /// 수량 업데이트
        /// </summary>
        public void UpdateCount(int count)
        {
            _currentCount = count;
            UpdateStatsText();
        }

        private void ShowDetailView()
        {
            if (_emptyState != null)
            {
                _emptyState.SetActive(false);
            }

            if (_detailView != null)
            {
                _detailView.SetActive(true);
            }
        }

        private void RefreshUI()
        {
            if (_currentItem == null) return;

            UpdateItemImage();
            UpdateRarityFrame();
            UpdateNameText();
            UpdateDescriptionText();
            UpdateStatsText();
            UpdateButtons();
        }

        private void UpdateItemImage()
        {
            // TODO: Addressables로 아이템 이미지 로드
            // LoadItemImageAsync(_currentItem.IconPath);
        }

        private void UpdateRarityFrame()
        {
            if (_rarityFrame == null || _currentItem == null) return;

            _rarityFrame.color = GetRarityFrameColor(_currentItem.Rarity);
        }

        private Color GetRarityFrameColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.N => _frameColorN,
                Rarity.R => _frameColorR,
                Rarity.SR => _frameColorSR,
                Rarity.SSR => _frameColorSSR,
                _ => _frameColorN
            };
        }

        private void UpdateNameText()
        {
            if (_itemName != null && _currentItem != null)
            {
                _itemName.text = _currentItem.Name;
            }
        }

        private void UpdateDescriptionText()
        {
            if (_itemDescription != null && _currentItem != null)
            {
                _itemDescription.text = _currentItem.Description ?? "";
            }
        }

        private void UpdateStatsText()
        {
            if (_itemStats == null || _currentItem == null) return;

            var statsBuilder = new System.Text.StringBuilder();

            // 보유 수량
            statsBuilder.AppendLine($"보유: {_currentCount}개");

            // 스탯 보너스 (있는 경우)
            if (_currentItem.AtkBonus > 0)
                statsBuilder.AppendLine($"공격력 +{_currentItem.AtkBonus}");
            if (_currentItem.DefBonus > 0)
                statsBuilder.AppendLine($"방어력 +{_currentItem.DefBonus}");
            if (_currentItem.HpBonus > 0)
                statsBuilder.AppendLine($"체력 +{_currentItem.HpBonus}");

            // 경험치 재료인 경우
            if (_currentItem.IsExpMaterial)
                statsBuilder.AppendLine($"경험치 +{_currentItem.ExpValue}");

            _itemStats.text = statsBuilder.ToString().TrimEnd();
        }

        private void UpdateButtons()
        {
            if (_currentItem == null) return;

            // 사용 버튼
            bool canUse = _currentItem.IsConsumable || _currentItem.IsExpMaterial;
            if (_useButton != null)
            {
                _useButton.gameObject.SetActive(canUse);
                _useButton.interactable = _currentCount > 0;
            }

            if (_useButtonText != null)
            {
                _useButtonText.text = _currentItem.IsExpMaterial ? "강화 재료" : "사용";
            }

            // 판매 버튼
            if (_sellButton != null)
            {
                _sellButton.interactable = _currentCount > 0;
            }

            if (_sellButtonText != null)
            {
                _sellButtonText.text = "판매";
            }
        }

        /// <summary>
        /// 상호작용 가능 여부 설정
        /// </summary>
        public void SetInteractable(bool interactable)
        {
            if (_useButton != null)
            {
                _useButton.interactable = interactable && _currentCount > 0;
            }

            if (_sellButton != null)
            {
                _sellButton.interactable = interactable && _currentCount > 0;
            }
        }

        /// <summary>
        /// 선택 해제
        /// </summary>
        public void ClearSelection()
        {
            _currentItem = null;
            _currentCount = 0;
            ShowEmptyState();
        }
    }
}
