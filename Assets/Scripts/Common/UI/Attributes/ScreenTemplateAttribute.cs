using System;

namespace Sc.Common.UI.Attributes
{
    /// <summary>
    /// Screen 프리팹 생성 시 사용할 템플릿 유형
    /// </summary>
    public enum ScreenTemplateType
    {
        /// <summary>Background + Content only</summary>
        FullScreen,

        /// <summary>Background + Header(80px) + Content</summary>
        Standard,

        /// <summary>Background + Header + Content + Footer(80px)</summary>
        Tabbed,

        /// <summary>Background + BackHeader(60px) + ScrollContent</summary>
        Detail
    }

    /// <summary>
    /// Screen 클래스에 적용하여 프리팹 생성 시 사용할 템플릿을 지정합니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScreenTemplateAttribute : Attribute
    {
        public ScreenTemplateType TemplateType { get; }

        public ScreenTemplateAttribute(ScreenTemplateType templateType)
        {
            TemplateType = templateType;
        }
    }
}