using UnityEngine;

namespace Sc.Data
{
    /// <summary>
    /// 스테이지 카테고리 마스터 데이터.
    /// 컨텐츠 내 세부 분류 (예: 골드던전의 불속성, 메인스토리의 1장)
    /// </summary>
    [CreateAssetMenu(fileName = "StageCategoryData", menuName = "SC/Data/StageCategory")]
    public class StageCategoryData : ScriptableObject
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private InGameContentType _contentType;
        [SerializeField] private string _nameKey;
        [SerializeField] private string _descriptionKey;
        [SerializeField] private Sprite _iconSprite;

        [Header("컨텐츠별 특화 필드")]
        [Tooltip("속성 던전용")]
        [SerializeField] private Element _element;

        [Tooltip("난이도 던전용")]
        [SerializeField] private Difficulty _difficulty;

        [Tooltip("메인스토리 챕터용")]
        [SerializeField] private int _chapterNumber;

        [Header("표시")]
        [SerializeField] private int _displayOrder;
        [SerializeField] private bool _isEnabled = true;

        // Properties
        public string Id => _id;
        public InGameContentType ContentType => _contentType;
        public string NameKey => _nameKey;
        public string DescriptionKey => _descriptionKey;
        public Sprite IconSprite => _iconSprite;
        public Element Element => _element;
        public Difficulty Difficulty => _difficulty;
        public int ChapterNumber => _chapterNumber;
        public int DisplayOrder => _displayOrder;
        public bool IsEnabled => _isEnabled;

        /// <summary>
        /// 표시용 이름 (NameKey 기반, 로컬라이제이션 미구현 시 키 반환)
        /// </summary>
        public string GetDisplayName()
        {
            // TODO: LocalizationManager 연동
            return _nameKey;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor 전용: 초기화
        /// </summary>
        public void Initialize(
            string id,
            InGameContentType contentType,
            string nameKey,
            string descriptionKey,
            Element element,
            Difficulty difficulty,
            int chapterNumber,
            int displayOrder,
            bool isEnabled)
        {
            _id = id;
            _contentType = contentType;
            _nameKey = nameKey;
            _descriptionKey = descriptionKey;
            _element = element;
            _difficulty = difficulty;
            _chapterNumber = chapterNumber;
            _displayOrder = displayOrder;
            _isEnabled = isEnabled;
        }
#endif
    }
}
