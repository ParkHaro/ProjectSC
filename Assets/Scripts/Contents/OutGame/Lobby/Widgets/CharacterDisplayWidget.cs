using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby.Widgets
{
    /// <summary>
    /// 캐릭터 표시 위젯.
    /// 메인 캐릭터 이미지 + 대사 + 좌우 전환.
    /// </summary>
    public class CharacterDisplayWidget : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Button _characterButton;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Image _glowEffect;

        private string[] _characterIds;
        private int _currentIndex;

        public event Action<string> OnCharacterClicked;
        public event Action<int> OnCharacterChanged;

        public string CurrentCharacterId =>
            _characterIds != null && _characterIds.Length > 0
                ? _characterIds[_currentIndex]
                : null;

        private void Awake()
        {
            if (_characterButton != null)
                _characterButton.onClick.AddListener(HandleCharacterClick);

            if (_leftArrow != null)
                _leftArrow.onClick.AddListener(ShowPrevious);

            if (_rightArrow != null)
                _rightArrow.onClick.AddListener(ShowNext);
        }

        private void OnDestroy()
        {
            if (_characterButton != null)
                _characterButton.onClick.RemoveListener(HandleCharacterClick);

            if (_leftArrow != null)
                _leftArrow.onClick.RemoveListener(ShowPrevious);

            if (_rightArrow != null)
                _rightArrow.onClick.RemoveListener(ShowNext);
        }

        public void Initialize(string[] characterIds)
        {
            _characterIds = characterIds;
            _currentIndex = 0;

            UpdateArrowVisibility();
            if (_characterIds != null && _characterIds.Length > 0)
            {
                LoadCharacter(_characterIds[0]);
            }
        }

        public void SetCharacter(Sprite sprite, string dialogue)
        {
            if (_characterImage != null)
                _characterImage.sprite = sprite;

            if (_dialogueText != null)
                _dialogueText.text = dialogue;
        }

        private void LoadCharacter(string characterId)
        {
            // TODO: CharacterId로 스프라이트와 대사 로드
            // DataManager에서 캐릭터 데이터 조회
            SetCharacter(null, $"캐릭터 {characterId}의 대사");
        }

        private void HandleCharacterClick()
        {
            OnCharacterClicked?.Invoke(CurrentCharacterId);
        }

        public void ShowNext()
        {
            if (_characterIds == null || _characterIds.Length <= 1) return;

            _currentIndex = (_currentIndex + 1) % _characterIds.Length;
            LoadCharacter(_characterIds[_currentIndex]);
            OnCharacterChanged?.Invoke(_currentIndex);
        }

        public void ShowPrevious()
        {
            if (_characterIds == null || _characterIds.Length <= 1) return;

            _currentIndex = (_currentIndex - 1 + _characterIds.Length) % _characterIds.Length;
            LoadCharacter(_characterIds[_currentIndex]);
            OnCharacterChanged?.Invoke(_currentIndex);
        }

        private void UpdateArrowVisibility()
        {
            bool showArrows = _characterIds != null && _characterIds.Length > 1;

            if (_leftArrow != null)
                _leftArrow.gameObject.SetActive(showArrows);

            if (_rightArrow != null)
                _rightArrow.gameObject.SetActive(showArrows);
        }
    }
}
