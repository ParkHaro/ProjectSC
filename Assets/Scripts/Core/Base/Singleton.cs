using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// MonoBehaviour 싱글톤 베이스 클래스
    /// </summary>
    /// <typeparam name="T">싱글톤 타입</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _isQuitting;

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isQuitting)
                {
                    Debug.LogWarning($"[Singleton] {typeof(T).Name} 인스턴스가 이미 파괴됨");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindFirstObjectByType<T>();

                        if (_instance == null)
                        {
                            var go = new GameObject($"[{typeof(T).Name}]");
                            _instance = go.AddComponent<T>();
                        }
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// 인스턴스 존재 여부
        /// </summary>
        public static bool HasInstance => _instance != null;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                OnSingletonAwake();
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[Singleton] {typeof(T).Name} 중복 인스턴스 파괴");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                OnSingletonDestroy();
                _instance = null;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
            OnSingletonDestroy();
        }

        /// <summary>
        /// 싱글톤 초기화 시 호출
        /// </summary>
        protected virtual void OnSingletonAwake() { }

        /// <summary>
        /// 싱글톤 파괴 시 호출
        /// </summary>
        protected virtual void OnSingletonDestroy() { }
    }
}
