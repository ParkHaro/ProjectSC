using Sc.Core;
using Sc.Data;
using Sc.Foundation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 속성 던전 컨텐츠 모듈.
    /// 속성 아이콘, 권장 속성 안내 표시.
    /// </summary>
    public class ElementDungeonContentModule : BaseStageContentModule
    {
        private Image _elementIcon;
        private TMP_Text _elementNameText;
        private TMP_Text _recommendText;
        private TMP_Text _descriptionText;

        private StageCategoryData _categoryData;

        protected override string GetPrefabAddress()
        {
            // UI 프리팹이 준비되면 아래 주석 해제
            // return "UI/Stage/ElementDungeonModuleUI";
            return null;
        }

        protected override void OnInitialize()
        {
            Log.Debug("[ElementDungeonContentModule] OnInitialize", LogCategory.UI);

            // UI 컴포넌트 찾기 (프리팹 있을 경우)
            if (_rootInstance != null)
            {
                _elementIcon = FindComponent<Image>();
                _elementNameText = FindTransform("ElementName")?.GetComponent<TMP_Text>();
                _recommendText = FindTransform("RecommendText")?.GetComponent<TMP_Text>();
                _descriptionText = FindTransform("Description")?.GetComponent<TMP_Text>();
            }

            // 카테고리 데이터 로드
            LoadCategoryData();

            // UI 업데이트
            UpdateUI();
        }

        protected override void OnRefreshInternal(string selectedStageId)
        {
            // 필요 시 추가 갱신
        }

        protected override void OnStageSelectedInternal(StageData stageData)
        {
            // 선택된 스테이지 기반 권장 속성 표시
            UpdateRecommendText(stageData);
        }

        protected override void OnCategoryIdChanged(string categoryId)
        {
            // 외부에서 카테고리 변경 시 (StageDashboard에서 다른 속성 선택)
            LoadCategoryData();
            UpdateUI();
        }

        protected override void OnReleaseInternal()
        {
            _categoryData = null;
        }

        #region Private Methods

        private void LoadCategoryData()
        {
            if (string.IsNullOrEmpty(_categoryId))
            {
                Log.Debug("[ElementDungeonContentModule] No categoryId set", LogCategory.UI);
                _categoryData = null;
                return;
            }

            var categoryDb = DataManager.Instance?.GetDatabase<StageCategoryDatabase>();
            if (categoryDb == null)
            {
                Log.Warning("[ElementDungeonContentModule] StageCategoryDatabase not found", LogCategory.UI);
                _categoryData = null;
                return;
            }

            _categoryData = categoryDb.GetById(_categoryId);

            if (_categoryData == null)
            {
                Log.Warning($"[ElementDungeonContentModule] Category not found: {_categoryId}", LogCategory.UI);
            }
            else
            {
                Log.Debug($"[ElementDungeonContentModule] Loaded category: {_categoryData.Id}, Element: {_categoryData.Element}", LogCategory.UI);
            }
        }

        private void UpdateUI()
        {
            if (_categoryData == null)
            {
                ClearUI();
                return;
            }

            // 속성 아이콘
            if (_elementIcon != null && _categoryData.IconSprite != null)
            {
                _elementIcon.sprite = _categoryData.IconSprite;
                _elementIcon.gameObject.SetActive(true);
            }

            // 속성 이름
            if (_elementNameText != null)
            {
                _elementNameText.text = GetElementDisplayName(_categoryData.Element);
            }

            // 설명
            if (_descriptionText != null && !string.IsNullOrEmpty(_categoryData.DescriptionKey))
            {
                _descriptionText.text = _categoryData.GetDisplayName();
            }

            // 권장 속성 (초기값)
            UpdateRecommendText(null);
        }

        private void ClearUI()
        {
            if (_elementIcon != null)
            {
                _elementIcon.gameObject.SetActive(false);
            }

            if (_elementNameText != null)
            {
                _elementNameText.text = "";
            }

            if (_descriptionText != null)
            {
                _descriptionText.text = "";
            }

            if (_recommendText != null)
            {
                _recommendText.text = "";
            }
        }

        private void UpdateRecommendText(StageData stageData)
        {
            if (_recommendText == null) return;

            if (_categoryData == null)
            {
                _recommendText.text = "";
                return;
            }

            // 권장 속성 계산 (해당 속성에 유리한 속성)
            var dungeonElement = _categoryData.Element;
            var recommendedElement = GetAdvantagedElement(dungeonElement);

            _recommendText.text = $"권장: {GetElementDisplayName(recommendedElement)} 속성";
        }

        private string GetElementDisplayName(Element element)
        {
            return element switch
            {
                Element.Fire => "불",
                Element.Water => "물",
                Element.Wind => "바람",
                Element.Earth => "대지",
                Element.Light => "빛",
                Element.Dark => "암흑",
                _ => element.ToString()
            };
        }

        /// <summary>
        /// 해당 속성에 유리한 속성 반환 (상성 테이블)
        /// </summary>
        private Element GetAdvantagedElement(Element targetElement)
        {
            // 간단한 상성 테이블 (불 < 물 < 바람 < 대지 < 불, 빛 <-> 암흑)
            return targetElement switch
            {
                Element.Fire => Element.Water,
                Element.Water => Element.Wind,
                Element.Wind => Element.Earth,
                Element.Earth => Element.Fire,
                Element.Light => Element.Dark,
                Element.Dark => Element.Light,
                _ => targetElement
            };
        }

        #endregion
    }
}
