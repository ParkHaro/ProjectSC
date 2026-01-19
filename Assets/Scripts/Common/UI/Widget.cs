using System.Collections.Generic;
using UnityEngine;

namespace Sc.Common.UI
{
    /// <summary>
    /// 모든 UI의 기본 단위. Composition 패턴 기반.
    /// </summary>
    public abstract class Widget : MonoBehaviour
    {
        [SerializeField] private Widget _parent;

        private readonly List<Widget> _children = new();
        private bool _isInitialized;
        private bool _isVisible;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        public Widget Parent => _parent;
        public IReadOnlyList<Widget> Children => _children;
        public bool IsInitialized => _isInitialized;
        public bool IsVisible => _isVisible;
        public CanvasGroup CanvasGroup => _canvasGroup;

        /// <summary>
        /// CanvasGroup 반환. 없으면 자동 추가.
        /// </summary>
        public CanvasGroup GetOrAddCanvasGroup()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
                if (_canvasGroup == null)
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            return _canvasGroup;
        }

        /// <summary>
        /// 상호작용 활성화/비활성화. 화면은 보이지만 터치 불가.
        /// </summary>
        public virtual void SetInteractable(bool interactable)
        {
            var cg = GetOrAddCanvasGroup();
            cg.interactable = interactable;
            cg.blocksRaycasts = interactable;
        }

        #region Lifecycle

        /// <summary>
        /// 최초 1회 초기화. 자식 Widget들도 초기화.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;

            OnInitialize();
            _isInitialized = true;

            foreach (var child in _children)
            {
                child.Initialize();
            }
        }

        /// <summary>
        /// 데이터 바인딩.
        /// </summary>
        public void Bind(object data)
        {
            OnBind(data);
        }

        /// <summary>
        /// 표시. Canvas.enabled 사용 (OnEnable/OnDisable 부작용 방지).
        /// </summary>
        public void Show()
        {
            if (_canvas != null)
            {
                _canvas.enabled = true;
            }
            else
            {
                gameObject.SetActive(true);
            }

            _isVisible = true;
            OnShow();

            foreach (var child in _children)
            {
                child.Show();
            }
        }

        /// <summary>
        /// 숨김. Canvas.enabled 사용 (OnEnable/OnDisable 부작용 방지).
        /// </summary>
        public void Hide()
        {
            foreach (var child in _children)
            {
                child.Hide();
            }

            OnHide();
            _isVisible = false;

            if (_canvas != null)
            {
                _canvas.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 갱신.
        /// </summary>
        public void Refresh()
        {
            OnRefresh();

            foreach (var child in _children)
            {
                child.Refresh();
            }
        }

        /// <summary>
        /// 해제.
        /// </summary>
        public void Release()
        {
            foreach (var child in _children)
            {
                child.Release();
            }

            OnRelease();
            _isInitialized = false;
        }

        #endregion

        #region Lifecycle Hooks

        protected virtual void OnInitialize() { }
        protected virtual void OnBind(object data) { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
        protected virtual void OnRefresh() { }
        protected virtual void OnRelease() { }

        #endregion

        #region Hierarchy

        public void AddChild(Widget child)
        {
            if (child == null || _children.Contains(child)) return;

            child._parent = this;
            _children.Add(child);

            if (_isInitialized && !child._isInitialized)
            {
                child.Initialize();
            }
        }

        public void RemoveChild(Widget child)
        {
            if (child == null || !_children.Contains(child)) return;

            child._parent = null;
            _children.Remove(child);
        }

        protected T FindChild<T>() where T : Widget
        {
            foreach (var child in _children)
            {
                if (child is T typed)
                    return typed;
            }
            return null;
        }

        protected T FindChildInHierarchy<T>() where T : Widget
        {
            foreach (var child in _children)
            {
                if (child is T typed)
                    return typed;

                var found = child.FindChildInHierarchy<T>();
                if (found != null)
                    return found;
            }
            return null;
        }

        #endregion

        #region Unity Lifecycle

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            CollectChildWidgets();
        }

        private void CollectChildWidgets()
        {
            _children.Clear();

            foreach (Transform child in transform)
            {
                var widget = child.GetComponent<Widget>();
                if (widget != null && widget._parent == null)
                {
                    widget._parent = this;
                    _children.Add(widget);
                }
            }
        }

        #endregion
    }
}
