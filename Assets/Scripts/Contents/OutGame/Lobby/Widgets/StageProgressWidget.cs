using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Lobby.Widgets
{
    /// <summary>
    /// 스테이지 진행 표시 위젯.
    /// 현재 도달한 스테이지와 진행률 표시.
    /// </summary>
    public class StageProgressWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stageLabel;
        [SerializeField] private TMP_Text _stageName;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _progressText;

        private string _currentStageId;

        public void SetProgress(string stageId, string stageName, float progress)
        {
            _currentStageId = stageId;

            if (_stageLabel != null)
                _stageLabel.text = stageId;

            if (_stageName != null)
                _stageName.text = stageName;

            if (_progressBar != null)
                _progressBar.value = Mathf.Clamp01(progress);

            if (_progressText != null)
                _progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
        }

        public void SetProgress(int chapter, int stage, string stageName)
        {
            SetProgress($"{chapter}-{stage}", stageName, 0f);
        }

        public string CurrentStageId => _currentStageId;
    }
}
