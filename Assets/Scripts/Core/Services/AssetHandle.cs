using System;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 에셋 레퍼런스 카운팅 래퍼.
    /// Load 시 RefCount++, Release 시 RefCount--.
    /// </summary>
    public class AssetHandle<T> where T : UnityEngine.Object
    {
        private readonly string _key;
        private readonly T _asset;
        private readonly Action<AssetHandle<T>> _onRelease;
        private int _refCount;
        private bool _isReleased;

        /// <summary>에셋 키</summary>
        public string Key => _key;

        /// <summary>실제 에셋</summary>
        public T Asset => _isReleased ? null : _asset;

        /// <summary>현재 참조 수</summary>
        public int RefCount => _refCount;

        /// <summary>유효 여부</summary>
        public bool IsValid => !_isReleased && _asset != null;

        internal AssetHandle(string key, T asset, Action<AssetHandle<T>> onRelease)
        {
            _key = key;
            _asset = asset;
            _onRelease = onRelease;
            _refCount = 1;
            _isReleased = false;
        }

        /// <summary>
        /// 레퍼런스 증가
        /// </summary>
        internal void AddRef()
        {
            if (_isReleased)
            {
                Debug.LogWarning($"[AssetHandle] 이미 해제된 에셋에 AddRef 시도: {_key}");
                return;
            }
            _refCount++;
        }

        /// <summary>
        /// 레퍼런스 감소 및 해제 요청
        /// </summary>
        public void Release()
        {
            if (_isReleased)
            {
                Debug.LogWarning($"[AssetHandle] 이미 해제된 에셋에 Release 시도: {_key}");
                return;
            }

            _refCount--;

            if (_refCount <= 0)
            {
                _refCount = 0;
                _onRelease?.Invoke(this);
            }
        }

        /// <summary>
        /// 강제 해제 (내부용)
        /// </summary>
        internal void ForceRelease()
        {
            _isReleased = true;
            _refCount = 0;
        }

        /// <summary>
        /// 암시적 변환 (Asset 접근 편의)
        /// </summary>
        public static implicit operator T(AssetHandle<T> handle)
        {
            return handle?.Asset;
        }
    }
}
