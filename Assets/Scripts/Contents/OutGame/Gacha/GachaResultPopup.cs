using System.Collections.Generic;
using Sc.Common.UI;
using Sc.Common.UI.Attributes;
using Sc.Core;
using Sc.Data;
using Sc.Packet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Gacha
{
    /// <summary>
    /// 가챠 결과 팝업 State
    /// </summary>
    public class GachaResultState : IPopupState
    {
        /// <summary>
        /// 가챠 결과 목록
        /// </summary>
        public List<GachaResultItem> Results;
    }

    /// <summary>
    /// 가챠 결과 팝업 - 소환 결과 표시
    /// </summary>
    [PopupTemplate(PopupTemplateType.FullScreen)]
    public class GachaResultPopup : PopupWidget<GachaResultPopup, GachaResultState>
    {
        [Header("UI References")] [SerializeField]
        private Transform _resultContainer;

        [SerializeField] private GameObject _resultItemPrefab;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TMP_Text _titleText;

        private GachaResultState _currentState;
        private readonly List<GameObject> _spawnedItems = new();

        protected override void OnInitialize()
        {
            Debug.Log("[GachaResultPopup] OnInitialize");

            if (_confirmButton != null)
            {
                _confirmButton.onClick.AddListener(OnConfirmClicked);
            }

            if (_retryButton != null)
            {
                _retryButton.onClick.AddListener(OnRetryClicked);
            }
        }

        protected override void OnBind(GachaResultState state)
        {
            _currentState = state ?? new GachaResultState { Results = new List<GachaResultItem>() };
            Debug.Log($"[GachaResultPopup] OnBind - Results: {_currentState.Results?.Count ?? 0}");

            RefreshUI();
        }

        protected override void OnShow()
        {
            Debug.Log("[GachaResultPopup] OnShow");
        }

        protected override void OnHide()
        {
            Debug.Log("[GachaResultPopup] OnHide");
        }

        protected override void OnRelease()
        {
            ClearResultItems();
        }

        public override GachaResultState GetState() => _currentState;

        private void RefreshUI()
        {
            // 타이틀 설정
            if (_titleText != null)
            {
                var count = _currentState.Results?.Count ?? 0;
                _titleText.text = count > 1 ? $"소환 결과 ({count})" : "소환 결과";
            }

            // 기존 아이템 정리
            ClearResultItems();

            // 결과 아이템 생성
            if (_currentState.Results != null && _resultContainer != null && _resultItemPrefab != null)
            {
                foreach (var result in _currentState.Results)
                {
                    var itemGo = Instantiate(_resultItemPrefab, _resultContainer);
                    SetupResultItem(itemGo, result);
                    _spawnedItems.Add(itemGo);
                }
            }
        }

        private void SetupResultItem(GameObject itemGo, GachaResultItem result)
        {
            // 캐릭터 마스터 데이터 조회
            var characterData = DataManager.Instance?.Characters?.GetById(result.CharacterId);

            // 이름 텍스트
            var nameText = itemGo.GetComponentInChildren<TMP_Text>();
            if (nameText != null)
            {
                var displayName = characterData?.Name ?? result.CharacterId;
                var newBadge = result.IsNew ? " <color=#FFD700>NEW!</color>" : "";
                var pityBadge = result.IsPity ? " <color=#FF4500>★천장★</color>" : "";
                nameText.text = $"[{GetRarityText(result.Rarity)}] {displayName}{newBadge}{pityBadge}";
            }

            // 배경색 (희귀도에 따라)
            var image = itemGo.GetComponent<Image>();
            if (image != null)
            {
                image.color = GetRarityColor(result.Rarity);
            }
        }

        private string GetRarityText(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.SSR => "SSR",
                Rarity.SR => "SR",
                Rarity.R => "R",
                _ => "N"
            };
        }

        private Color GetRarityColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.SSR => new Color(1f, 0.84f, 0f, 0.3f), // 금색
                Rarity.SR => new Color(0.5f, 0f, 0.5f, 0.3f), // 보라색
                Rarity.R => new Color(0f, 0.5f, 1f, 0.3f), // 파란색
                _ => new Color(0.5f, 0.5f, 0.5f, 0.3f) // 회색
            };
        }

        private void ClearResultItems()
        {
            foreach (var item in _spawnedItems)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }

            _spawnedItems.Clear();
        }

        private void OnConfirmClicked()
        {
            Debug.Log("[GachaResultPopup] Confirm clicked");
            NavigationManager.Instance?.Pop();
        }

        private void OnRetryClicked()
        {
            Debug.Log("[GachaResultPopup] Retry clicked");
            // 팝업 닫고 다시 뽑기 실행
            // 실제로는 GachaScreen에 이벤트로 알려야 함
            NavigationManager.Instance?.Pop();
        }

        /// <summary>
        /// 결과 확인 전까지 ESC로 닫기 불허
        /// </summary>
        public override bool OnEscape() => true;
    }
}