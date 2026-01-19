using System.Collections.Generic;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// Scope 생성/삭제 헬퍼.
    /// </summary>
    internal class AssetScopeManager
    {
        private readonly Dictionary<string, AssetScope> _scopes = new();

        /// <summary>
        /// Scope 생성
        /// </summary>
        public AssetScope CreateScope(string name)
        {
            if (_scopes.TryGetValue(name, out var existing))
            {
                if (!existing.IsReleased)
                {
                    Debug.LogWarning($"[AssetScopeManager] 이미 존재하는 Scope: {name}");
                    return existing;
                }
                // 이미 해제된 Scope면 새로 생성
                _scopes.Remove(name);
            }

            var scope = new AssetScope(name, OnScopeReleased);
            _scopes[name] = scope;
            Debug.Log($"[AssetScopeManager] Scope 생성: {name}");
            return scope;
        }

        /// <summary>
        /// Scope 조회
        /// </summary>
        public AssetScope GetScope(string name)
        {
            if (_scopes.TryGetValue(name, out var scope) && !scope.IsReleased)
            {
                return scope;
            }
            return null;
        }

        /// <summary>
        /// Scope 존재 여부
        /// </summary>
        public bool HasScope(string name)
        {
            return _scopes.TryGetValue(name, out var scope) && !scope.IsReleased;
        }

        /// <summary>
        /// Scope 해제
        /// </summary>
        public void ReleaseScope(string name)
        {
            if (_scopes.TryGetValue(name, out var scope))
            {
                scope.Release();
            }
        }

        /// <summary>
        /// 모든 Scope 해제
        /// </summary>
        public void ReleaseAllScopes()
        {
            foreach (var scope in _scopes.Values)
            {
                if (!scope.IsReleased)
                {
                    scope.Release();
                }
            }
            _scopes.Clear();
        }

        private void OnScopeReleased(AssetScope scope)
        {
            _scopes.Remove(scope.Name);
        }
    }
}
