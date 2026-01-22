using System;

namespace Sc.Common.UI.Attributes
{
    /// <summary>
    /// Popup 프리팹 생성 시 사용할 템플릿 유형
    /// </summary>
    public enum PopupTemplateType
    {
        /// <summary>Backdrop + Container(Header+Body+Footer) - 500x300</summary>
        Confirm,

        /// <summary>Backdrop + Container(Header+GridBody+Footer) - 600x500</summary>
        Reward,

        /// <summary>Backdrop + Container(Header+ScrollBody+CloseBtn) - 600x700</summary>
        Info,

        /// <summary>Background + Content + CloseBtn - 전체 화면</summary>
        FullScreen
    }

    /// <summary>
    /// Popup 클래스에 템플릿 유형을 지정하는 Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PopupTemplateAttribute : Attribute
    {
        public PopupTemplateType TemplateType { get; }
        public float Width { get; }
        public float Height { get; }

        public PopupTemplateAttribute(PopupTemplateType templateType)
        {
            TemplateType = templateType;
            // 기본 크기 설정
            (Width, Height) = templateType switch
            {
                PopupTemplateType.Confirm => (500f, 300f),
                PopupTemplateType.Reward => (600f, 500f),
                PopupTemplateType.Info => (600f, 700f),
                PopupTemplateType.FullScreen => (0f, 0f), // Stretch
                _ => (500f, 300f)
            };
        }

        public PopupTemplateAttribute(PopupTemplateType templateType, float width, float height)
        {
            TemplateType = templateType;
            Width = width;
            Height = height;
        }
    }
}