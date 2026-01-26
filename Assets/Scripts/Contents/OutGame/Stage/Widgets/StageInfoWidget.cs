using System;
using System.Collections.Generic;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage.Widgets
{
    /// <summary>
    /// 스테이지 정보 위젯.
    /// 스테이지 정보와 적 미리보기를 표시합니다.
    /// 레퍼런스: Docs/Design/Reference/PartySelect.jpg - 상단 중앙 정보 영역
    /// </summary>
    public class StageInfoWidget : MonoBehaviour
    {
        [Header("Stage Info")]
        [SerializeField] private TMP_Text _stageNameText;
        [SerializeField] private TMP_Text _stageNumberText;
        [SerializeField] private TMP_Text _difficultyText;

        [Header("Entry Info")]
        [SerializeField] private Image _staminaIcon;
        [SerializeField] private TMP_Text _staminaCostText;
        [SerializeField] private TMP_Text _recommendedPowerText;
        [SerializeField] private TMP_Text _borrowInfoText;

        [Header("Formation Status")]
        [SerializeField] private TMP_Text _partyCountText;
        [SerializeField] private TMP_Text _cardCountText;

        [Header("Enemy Preview")]
        [SerializeField] private Transform _enemyPreviewContainer;
        [SerializeField] private GameObject _enemySlotPrefab;

        [Header("Element Indicator")]
        [SerializeField] private Transform _elementIndicatorContainer;
        [SerializeField] private Image[] _elementIcons;

        [Header("Colors")]
        [SerializeField] private Color _normalDifficultyColor = new Color32(100, 180, 100, 255);
        [SerializeField] private Color _hardDifficultyColor = new Color32(255, 150, 80, 255);
        [SerializeField] private Color _extremeDifficultyColor = new Color32(255, 80, 80, 255);
        [SerializeField] private Color _powerSufficientColor = new Color32(100, 200, 100, 255);
        [SerializeField] private Color _powerInsufficientColor = new Color32(255, 100, 100, 255);

        private StageData _stageData;
        private int _currentPartyPower;
        private int _partyCount;
        private int _maxPartyCount = 6;

        /// <summary>
        /// 스테이지 정보 클릭 이벤트 (상세 팝업 열기)
        /// </summary>
        public event Action<StageData> OnStageInfoClicked;

        /// <summary>
        /// 바인딩된 스테이지 데이터
        /// </summary>
        public StageData StageData => _stageData;

        /// <summary>
        /// 스테이지 정보 설정
        /// </summary>
        public void Configure(StageData stageData)
        {
            _stageData = stageData;

            if (stageData == null)
            {
                ClearUI();
                return;
            }

            UpdateStageInfo();
            UpdateEntryInfo();
            UpdateEnemyPreview();
        }

        /// <summary>
        /// 파티 상태 업데이트
        /// </summary>
        public void UpdatePartyStatus(int partyCount, int totalPower, int maxPartyCount = 6)
        {
            _partyCount = partyCount;
            _maxPartyCount = maxPartyCount;
            _currentPartyPower = totalPower;

            UpdateFormationStatus();
            UpdatePowerComparison();
        }

        /// <summary>
        /// 대여 정보 업데이트
        /// </summary>
        public void UpdateBorrowInfo(int borrowedCount, int maxBorrow)
        {
            if (_borrowInfoText != null)
            {
                _borrowInfoText.text = $"사도 대여: {borrowedCount}/{maxBorrow}";
            }
        }

        /// <summary>
        /// 속성 유리/불리 표시
        /// </summary>
        public void SetElementAdvantage(int[] advantageElements)
        {
            if (_elementIcons == null) return;

            // 모든 아이콘 비활성화
            foreach (var icon in _elementIcons)
            {
                if (icon != null)
                {
                    icon.gameObject.SetActive(false);
                }
            }

            // 유리 속성 활성화
            if (advantageElements != null)
            {
                foreach (var elementIndex in advantageElements)
                {
                    if (elementIndex >= 0 && elementIndex < _elementIcons.Length)
                    {
                        _elementIcons[elementIndex].gameObject.SetActive(true);
                    }
                }
            }
        }

        private void UpdateStageInfo()
        {
            if (_stageData == null) return;

            // 스테이지 이름
            if (_stageNameText != null)
            {
                _stageNameText.text = _stageData.Name ?? string.Empty;
            }

            // 스테이지 번호
            if (_stageNumberText != null)
            {
                _stageNumberText.text = $"{_stageData.Chapter}-{_stageData.StageNumber}";
            }

            // 난이도
            if (_difficultyText != null)
            {
                _difficultyText.text = GetDifficultyText(_stageData.Difficulty);
                _difficultyText.color = GetDifficultyColor(_stageData.Difficulty);
            }
        }

        private void UpdateEntryInfo()
        {
            if (_stageData == null) return;

            // 스태미나 비용
            if (_staminaCostText != null)
            {
                _staminaCostText.text = _stageData.StaminaCost.ToString();
            }

            // 권장 전투력
            if (_recommendedPowerText != null)
            {
                _recommendedPowerText.text = $"권장 전투력: {_stageData.RecommendedPower:N0}";
            }

            // 초기 대여 정보
            if (_borrowInfoText != null)
            {
                _borrowInfoText.text = "사도 대여: 0/1";
            }
        }

        private void UpdateFormationStatus()
        {
            // 편성된 캐릭터 수
            if (_partyCountText != null)
            {
                _partyCountText.text = $"편성된 사도: {_partyCount}/{_maxPartyCount}";
            }

            // 편성된 카드 수 (플레이스홀더)
            if (_cardCountText != null)
            {
                int cardCount = _partyCount * 4; // 캐릭터당 4장 가정
                int maxCards = _maxPartyCount * 4;
                _cardCountText.text = $"편성된 카드: {cardCount}/{maxCards}";
            }
        }

        private void UpdatePowerComparison()
        {
            if (_recommendedPowerText == null || _stageData == null) return;

            bool isSufficient = _currentPartyPower >= _stageData.RecommendedPower;
            _recommendedPowerText.color = isSufficient ? _powerSufficientColor : _powerInsufficientColor;
        }

        private void UpdateEnemyPreview()
        {
            if (_enemyPreviewContainer == null) return;

            // 기존 적 슬롯 제거
            for (int i = _enemyPreviewContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(_enemyPreviewContainer.GetChild(i).gameObject);
            }

            if (_stageData?.EnemyIds == null) return;

            // 적 슬롯 생성 (최대 5개까지만 표시)
            int maxDisplay = Mathf.Min(_stageData.EnemyIds.Length, 5);
            for (int i = 0; i < maxDisplay; i++)
            {
                CreateEnemySlot(_stageData.EnemyIds[i], i);
            }

            // 추가 적이 있으면 표시
            if (_stageData.EnemyIds.Length > 5)
            {
                CreateMoreIndicator(_stageData.EnemyIds.Length - 5);
            }
        }

        private void CreateEnemySlot(string enemyId, int index)
        {
            GameObject slotObj;
            if (_enemySlotPrefab != null)
            {
                slotObj = Instantiate(_enemySlotPrefab, _enemyPreviewContainer);
            }
            else
            {
                slotObj = new GameObject($"EnemySlot_{index}");
                slotObj.transform.SetParent(_enemyPreviewContainer, false);

                var rect = slotObj.AddComponent<RectTransform>();
                rect.sizeDelta = new Vector2(40, 40);

                var image = slotObj.AddComponent<Image>();
                image.color = new Color32(100, 60, 60, 255); // 적 플레이스홀더 색상
            }

            // 실제 구현에서는 EnemyData를 로드하여 아이콘 설정
        }

        private void CreateMoreIndicator(int additionalCount)
        {
            var moreObj = new GameObject("MoreIndicator");
            moreObj.transform.SetParent(_enemyPreviewContainer, false);

            var rect = moreObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(40, 40);

            var text = moreObj.AddComponent<TextMeshProUGUI>();
            text.text = $"+{additionalCount}";
            text.fontSize = 14;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Center;
        }

        private void ClearUI()
        {
            if (_stageNameText != null) _stageNameText.text = string.Empty;
            if (_stageNumberText != null) _stageNumberText.text = string.Empty;
            if (_difficultyText != null) _difficultyText.text = string.Empty;
            if (_staminaCostText != null) _staminaCostText.text = "0";
            if (_recommendedPowerText != null) _recommendedPowerText.text = "권장 전투력: 0";
            if (_borrowInfoText != null) _borrowInfoText.text = string.Empty;
            if (_partyCountText != null) _partyCountText.text = string.Empty;
            if (_cardCountText != null) _cardCountText.text = string.Empty;

            // 적 미리보기 클리어
            if (_enemyPreviewContainer != null)
            {
                for (int i = _enemyPreviewContainer.childCount - 1; i >= 0; i--)
                {
                    Destroy(_enemyPreviewContainer.GetChild(i).gameObject);
                }
            }
        }

        private string GetDifficultyText(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => "순한맛",
                Difficulty.Normal => "보통맛",
                Difficulty.Hard => "매운맛",
                _ => difficulty.ToString()
            };
        }

        private Color GetDifficultyColor(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => _normalDifficultyColor,
                Difficulty.Normal => _normalDifficultyColor,
                Difficulty.Hard => _hardDifficultyColor,
                _ => _normalDifficultyColor
            };
        }
    }
}
