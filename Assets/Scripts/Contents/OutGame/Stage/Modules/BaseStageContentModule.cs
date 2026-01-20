using System;
using Cysharp.Threading.Tasks;
using Sc.Core;
using Sc.Data;
using Sc.Foundation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 컨텐츠 모듈 추상 베이스 클래스.
    /// 공통 로직(리소스 관리, 라이프사이클)을 처리합니다.
    /// </summary>
    public abstract class BaseStageContentModule : IStageContentModule
    {
        protected Transform _container;
        protected InGameContentType _contentType;
        protected string _categoryId;
        protected GameObject _rootInstance;
        protected AssetScope _assetScope;
        protected bool _isInitialized;
        protected bool _isLoading;

        /// <summary>
        /// 카테고리 변경 이벤트
        /// </summary>
        public event Action<string> OnCategoryChanged;

        /// <summary>
        /// 모듈 초기화 완료 여부
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 로딩 중 여부
        /// </summary>
        public bool IsLoading => _isLoading;

        /// <summary>
        /// 컨텐츠 타입
        /// </summary>
        public InGameContentType ContentType => _contentType;

        /// <summary>
        /// 현재 카테고리 ID
        /// </summary>
        public string CategoryId => _categoryId;

        /// <summary>
        /// UI 프리팹 주소 반환. null 반환 시 UI 로드 스킵.
        /// </summary>
        protected abstract string GetPrefabAddress();

        /// <summary>
        /// UI 생성 후 초기화 (서브클래스 구현)
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// 상태 갱신 (서브클래스 구현)
        /// </summary>
        protected abstract void OnRefreshInternal(string selectedStageId);

        /// <summary>
        /// 스테이지 선택 시 처리 (서브클래스 옵션)
        /// </summary>
        protected virtual void OnStageSelectedInternal(StageData stageData)
        {
        }

        /// <summary>
        /// 리소스 해제 시 처리 (서브클래스 옵션)
        /// </summary>
        protected virtual void OnReleaseInternal()
        {
        }

        #region IStageContentModule Implementation

        public void Initialize(Transform container, InGameContentType contentType)
        {
            if (_isInitialized)
            {
                Log.Warning($"[{GetType().Name}] 이미 초기화됨", LogCategory.UI);
                return;
            }

            _container = container;
            _contentType = contentType;
            _assetScope = AssetManager.Instance?.CreateScope($"StageModule_{GetType().Name}");

            LoadAndCreateUIAsync().Forget();
        }

        public void SetCategoryId(string categoryId)
        {
            if (_categoryId == categoryId) return;

            _categoryId = categoryId;
            OnCategoryIdChanged(categoryId);
        }

        /// <summary>
        /// 카테고리 ID 변경 시 호출 (서브클래스 오버라이드 가능)
        /// </summary>
        protected virtual void OnCategoryIdChanged(string categoryId)
        {
            // 기본 구현: 아무것도 안함
        }

        /// <summary>
        /// 카테고리 변경 이벤트 발행 (서브클래스에서 호출)
        /// </summary>
        protected void NotifyCategoryChanged(string categoryId)
        {
            _categoryId = categoryId;
            OnCategoryChanged?.Invoke(categoryId);
        }

        public void Refresh(string selectedStageId)
        {
            if (!_isInitialized || _isLoading)
            {
                return;
            }

            OnRefreshInternal(selectedStageId);
        }

        public void OnStageSelected(StageData stageData)
        {
            if (!_isInitialized || _isLoading)
            {
                return;
            }

            OnStageSelectedInternal(stageData);
        }

        public void Release()
        {
            if (!_isInitialized && !_isLoading)
            {
                return;
            }

            OnReleaseInternal();

            if (_rootInstance != null)
            {
                Object.Destroy(_rootInstance);
                _rootInstance = null;
            }

            _assetScope?.Release();
            _assetScope = null;

            OnCategoryChanged = null;
            _categoryId = null;
            _isInitialized = false;
            _isLoading = false;

            Log.Debug($"[{GetType().Name}] 해제 완료", LogCategory.UI);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// UI 프리팹 비동기 로드 및 생성
        /// </summary>
        private async UniTaskVoid LoadAndCreateUIAsync()
        {
            var prefabAddress = GetPrefabAddress();

            // 프리팹 주소가 없으면 UI 없이 초기화
            if (string.IsNullOrEmpty(prefabAddress))
            {
                _isInitialized = true;
                OnInitialize();
                return;
            }

            _isLoading = true;

            var result = await AssetManager.Instance.LoadAsync<GameObject>(prefabAddress, _assetScope);

            if (!result.IsSuccess)
            {
                Log.Warning($"[{GetType().Name}] 프리팹 로드 실패: {prefabAddress}", LogCategory.UI);
                _isLoading = false;
                _isInitialized = true;
                OnInitialize();
                return;
            }

            if (_container == null)
            {
                Log.Warning($"[{GetType().Name}] Container가 null, 프리팹 인스턴스화 스킵", LogCategory.UI);
                _isLoading = false;
                _isInitialized = true;
                OnInitialize();
                return;
            }

            _rootInstance = Object.Instantiate(result.Value.Asset, _container);
            _rootInstance.name = GetType().Name;

            _isLoading = false;
            _isInitialized = true;

            Log.Debug($"[{GetType().Name}] UI 생성 완료: {prefabAddress}", LogCategory.UI);

            OnInitialize();
        }

        /// <summary>
        /// 루트 인스턴스에서 컴포넌트 찾기
        /// </summary>
        protected T FindComponent<T>() where T : Component
        {
            if (_rootInstance == null) return null;
            return _rootInstance.GetComponentInChildren<T>(true);
        }

        /// <summary>
        /// 루트 인스턴스에서 이름으로 Transform 찾기
        /// </summary>
        protected Transform FindTransform(string name)
        {
            if (_rootInstance == null) return null;
            return _rootInstance.transform.Find(name);
        }

        #endregion
    }
}