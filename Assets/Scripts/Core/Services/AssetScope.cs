using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 영역별 에셋 그룹 관리.
    /// 화면/팝업 단위로 Scope를 생성하여 에셋 수명 관리.
    /// </summary>
    public class AssetScope
    {
        private readonly string _name;
        private readonly List<object> _handles = new();
        private readonly Action<AssetScope> _onRelease;
        private bool _isReleased;

        /// <summary>Scope 이름</summary>
        public string Name => _name;

        /// <summary>등록된 에셋 수</summary>
        public int AssetCount => _handles.Count;

        /// <summary>해제 여부</summary>
        public bool IsReleased => _isReleased;

        internal AssetScope(string name, Action<AssetScope> onRelease)
        {
            _name = name;
            _onRelease = onRelease;
            _isReleased = false;
        }

        /// <summary>
        /// 핸들 등록 (내부용)
        /// </summary>
        internal void RegisterHandle<T>(AssetHandle<T> handle) where T : UnityEngine.Object
        {
            if (_isReleased)
            {
                Debug.LogWarning($"[AssetScope] 이미 해제된 Scope에 핸들 등록 시도: {_name}");
                return;
            }

            if (!_handles.Contains(handle))
            {
                _handles.Add(handle);
            }
        }

        /// <summary>
        /// 핸들 해제 (내부용)
        /// </summary>
        internal void UnregisterHandle<T>(AssetHandle<T> handle) where T : UnityEngine.Object
        {
            _handles.Remove(handle);
        }

        /// <summary>
        /// Scope 내 모든 에셋 해제
        /// </summary>
        public void Release()
        {
            if (_isReleased)
            {
                Debug.LogWarning($"[AssetScope] 이미 해제된 Scope: {_name}");
                return;
            }

            _isReleased = true;

            // 모든 핸들의 레퍼런스 감소
            foreach (var handle in _handles.ToArray())
            {
                ReleaseHandle(handle);
            }

            _handles.Clear();
            _onRelease?.Invoke(this);

            Debug.Log($"[AssetScope] Scope 해제: {_name}");
        }

        private void ReleaseHandle(object handle)
        {
            // Reflection 없이 타입별 처리
            var releaseMethod = handle.GetType().GetMethod("Release");
            releaseMethod?.Invoke(handle, null);
        }
    }
}
