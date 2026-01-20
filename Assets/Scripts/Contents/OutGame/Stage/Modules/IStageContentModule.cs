using System;
using Sc.Data;
using UnityEngine;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 컨텐츠별 확장 UI 모듈 인터페이스.
    /// StageSelectScreen의 Custom Content Area에 컨텐츠별 UI를 제공합니다.
    /// </summary>
    public interface IStageContentModule
    {
        /// <summary>
        /// 카테고리 변경 이벤트 (챕터, 속성 등 변경 시)
        /// </summary>
        event Action<string> OnCategoryChanged;

        /// <summary>
        /// 모듈 초기화. Container 하위에 UI를 생성합니다.
        /// </summary>
        /// <param name="container">UI를 배치할 부모 Transform</param>
        /// <param name="contentType">컨텐츠 타입</param>
        void Initialize(Transform container, InGameContentType contentType);

        /// <summary>
        /// 카테고리 ID 설정 (StageDashboard에서 선택 시)
        /// </summary>
        void SetCategoryId(string categoryId);

        /// <summary>
        /// 상태 변경 시 UI 갱신
        /// </summary>
        /// <param name="selectedStageId">선택된 스테이지 ID (null 가능)</param>
        void Refresh(string selectedStageId);

        /// <summary>
        /// 스테이지 선택 시 호출
        /// </summary>
        /// <param name="stageData">선택된 스테이지 데이터</param>
        void OnStageSelected(StageData stageData);

        /// <summary>
        /// 리소스 해제
        /// </summary>
        void Release();
    }
}