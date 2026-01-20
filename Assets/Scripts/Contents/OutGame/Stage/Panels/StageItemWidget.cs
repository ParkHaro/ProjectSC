using System;
using Sc.Common.UI;
using Sc.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 개별 스테이지 아이템 위젯.
    /// StageListPanel에서 각 스테이지를 표시합니다.
    /// </summary>
    public class StageItemWidget : Widget
    {
        [Header("UI References")] [SerializeField]
        private TMP_Text _stageNumberText;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _difficultyText;
        [SerializeField] private TMP_Text _staminaCostText;
        [SerializeField] private Button _button;

        [Header("State Indicators")] [SerializeField]
        private GameObject _lockIcon;

        [SerializeField] private GameObject _clearIcon;
        [SerializeField] private GameObject[] _starObjects; // 별 3개

        [Header("Selection")] [SerializeField] private GameObject _selectedFrame;

        private StageData _stageData;
        private StageClearInfo? _clearInfo;
        private Action<StageData> _onClickCallback;
        private bool _isLocked;
        private bool _isSelected;

        protected override void OnInitialize()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(OnClicked);
            }
        }

        /// <summary>
        /// 스테이지 아이템 설정
        /// </summary>
        /// <param name="stageData">스테이지 데이터</param>
        /// <param name="clearInfo">클리어 정보 (없으면 null)</param>
        /// <param name="isLocked">잠금 여부</param>
        /// <param name="onClickCallback">클릭 콜백</param>
        public void Setup(
            StageData stageData,
            StageClearInfo? clearInfo,
            bool isLocked,
            Action<StageData> onClickCallback)
        {
            _stageData = stageData;
            _clearInfo = clearInfo;
            _isLocked = isLocked;
            _onClickCallback = onClickCallback;

            RefreshUI();
        }

        /// <summary>
        /// 선택 상태 설정
        /// </summary>
        public void SetSelected(bool selected)
        {
            _isSelected = selected;

            if (_selectedFrame != null)
            {
                _selectedFrame.SetActive(selected);
            }
        }

        private void RefreshUI()
        {
            if (_stageData == null) return;

            // 스테이지 번호
            if (_stageNumberText != null)
            {
                _stageNumberText.text = $"{_stageData.Chapter}-{_stageData.StageNumber}";
            }

            // 이름
            if (_nameText != null)
            {
                _nameText.text = _stageData.Name;
            }

            // 난이도
            if (_difficultyText != null)
            {
                _difficultyText.text = GetDifficultyText(_stageData.Difficulty);
            }

            // 스태미나
            if (_staminaCostText != null)
            {
                _staminaCostText.text = $"{_stageData.StaminaCost}";
            }

            // 잠금 상태
            if (_lockIcon != null)
            {
                _lockIcon.SetActive(_isLocked);
            }

            // 클리어 아이콘
            bool isCleared = _clearInfo?.IsCleared ?? false;
            if (_clearIcon != null)
            {
                _clearIcon.SetActive(isCleared && !_isLocked);
            }

            // 별 표시
            int stars = _clearInfo?.Stars ?? 0;
            if (_starObjects != null)
            {
                for (int i = 0; i < _starObjects.Length; i++)
                {
                    if (_starObjects[i] != null)
                    {
                        _starObjects[i].SetActive(i < stars && !_isLocked);
                    }
                }
            }

            // 버튼 상호작용
            if (_button != null)
            {
                _button.interactable = !_isLocked;
            }

            // 선택 프레임
            if (_selectedFrame != null)
            {
                _selectedFrame.SetActive(_isSelected);
            }
        }

        private void OnClicked()
        {
            if (_isLocked || _stageData == null) return;

            Debug.Log($"[StageItemWidget] Clicked: {_stageData.Id}");
            _onClickCallback?.Invoke(_stageData);
        }

        private string GetDifficultyText(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => "Easy",
                Difficulty.Normal => "Normal",
                Difficulty.Hard => "Hard",
                _ => difficulty.ToString()
            };
        }

        protected override void OnRelease()
        {
            _onClickCallback = null;
            _stageData = null;

            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClicked);
            }
        }
    }
}