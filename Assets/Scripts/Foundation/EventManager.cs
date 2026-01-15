using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Foundation
{
    /// <summary>
    /// 전역 이벤트 매니저
    /// Publish/Subscribe 패턴으로 컴포넌트 간 통신
    /// </summary>
    public class EventManager : Singleton<EventManager>
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        /// <summary>
        /// 이벤트 구독
        /// </summary>
        /// <typeparam name="T">이벤트 타입 (struct)</typeparam>
        /// <param name="handler">이벤트 핸들러</param>
        public void Subscribe<T>(Action<T> handler) where T : struct
        {
            if (handler == null)
            {
                Debug.LogWarning($"[EventManager] Null handler for {typeof(T).Name}");
                return;
            }

            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var existing))
            {
                _handlers[type] = Delegate.Combine(existing, handler);
            }
            else
            {
                _handlers[type] = handler;
            }
        }

        /// <summary>
        /// 이벤트 구독 해제
        /// </summary>
        /// <typeparam name="T">이벤트 타입 (struct)</typeparam>
        /// <param name="handler">이벤트 핸들러</param>
        public void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            if (handler == null) return;

            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var existing))
            {
                var remaining = Delegate.Remove(existing, handler);
                if (remaining == null)
                {
                    _handlers.Remove(type);
                }
                else
                {
                    _handlers[type] = remaining;
                }
            }
        }

        /// <summary>
        /// 이벤트 발행
        /// </summary>
        /// <typeparam name="T">이벤트 타입 (struct)</typeparam>
        /// <param name="evt">이벤트 데이터</param>
        public void Publish<T>(T evt) where T : struct
        {
            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var handler))
            {
                try
                {
                    ((Action<T>)handler)?.Invoke(evt);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventManager] Error publishing {type.Name}: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        /// <summary>
        /// 특정 이벤트 타입의 모든 구독 해제
        /// </summary>
        /// <typeparam name="T">이벤트 타입</typeparam>
        public void Clear<T>() where T : struct
        {
            _handlers.Remove(typeof(T));
        }

        /// <summary>
        /// 모든 구독 해제
        /// </summary>
        public void ClearAll()
        {
            _handlers.Clear();
        }

        /// <summary>
        /// 특정 이벤트 타입의 구독자 수
        /// </summary>
        public int GetSubscriberCount<T>() where T : struct
        {
            if (_handlers.TryGetValue(typeof(T), out var handler))
            {
                return handler?.GetInvocationList()?.Length ?? 0;
            }
            return 0;
        }

        protected override void OnSingletonDestroy()
        {
            ClearAll();
        }
    }
}
