using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

namespace Sc.Tests.PlayMode
{
    /// <summary>
    /// PlayMode 테스트 전용 어설션 헬퍼.
    /// Unity 오브젝트, 컴포넌트, UI 관련 검증 유틸리티 제공.
    /// </summary>
    public static class PlayModeAssert
    {
        #region GameObject Assertions

        /// <summary>
        /// GameObject가 활성화 상태인지 검증
        /// </summary>
        public static void IsActive(GameObject obj, string message = null)
        {
            Assert.That(obj, Is.Not.Null, message ?? "GameObject is null");
            Assert.That(obj.activeInHierarchy, Is.True,
                message ?? $"GameObject '{obj.name}' is not active in hierarchy");
        }

        /// <summary>
        /// GameObject가 비활성화 상태인지 검증
        /// </summary>
        public static void IsInactive(GameObject obj, string message = null)
        {
            Assert.That(obj, Is.Not.Null, message ?? "GameObject is null");
            Assert.That(obj.activeInHierarchy, Is.False,
                message ?? $"GameObject '{obj.name}' is active in hierarchy");
        }

        /// <summary>
        /// GameObject가 유효(null이 아니고 파괴되지 않음)한지 검증
        /// </summary>
        public static void IsValid(GameObject obj, string message = null)
        {
            Assert.That(obj != null, Is.True, message ?? "GameObject is null or destroyed");
        }

        /// <summary>
        /// GameObject가 파괴되었는지 검증
        /// </summary>
        public static void IsDestroyed(GameObject obj, string message = null)
        {
            Assert.That(obj == null, Is.True, message ?? "GameObject is still valid");
        }

        #endregion

        #region Component Assertions

        /// <summary>
        /// 컴포넌트가 존재하는지 검증
        /// </summary>
        public static T HasComponent<T>(GameObject obj, string message = null) where T : Component
        {
            Assert.That(obj, Is.Not.Null, "GameObject is null");
            var component = obj.GetComponent<T>();
            Assert.That(component, Is.Not.Null,
                message ?? $"GameObject '{obj.name}' does not have component {typeof(T).Name}");
            return component;
        }

        /// <summary>
        /// 컴포넌트가 존재하지 않는지 검증
        /// </summary>
        public static void DoesNotHaveComponent<T>(GameObject obj, string message = null) where T : Component
        {
            Assert.That(obj, Is.Not.Null, "GameObject is null");
            var component = obj.GetComponent<T>();
            Assert.That(component, Is.Null,
                message ?? $"GameObject '{obj.name}' should not have component {typeof(T).Name}");
        }

        /// <summary>
        /// 컴포넌트가 활성화 상태인지 검증
        /// </summary>
        public static void IsEnabled(Behaviour component, string message = null)
        {
            Assert.That(component, Is.Not.Null, "Component is null");
            Assert.That(component.enabled, Is.True,
                message ?? $"Component '{component.GetType().Name}' is not enabled");
        }

        /// <summary>
        /// 컴포넌트가 비활성화 상태인지 검증
        /// </summary>
        public static void IsDisabled(Behaviour component, string message = null)
        {
            Assert.That(component, Is.Not.Null, "Component is null");
            Assert.That(component.enabled, Is.False,
                message ?? $"Component '{component.GetType().Name}' is enabled");
        }

        #endregion

        #region Child Assertions

        /// <summary>
        /// 자식 오브젝트 수 검증
        /// </summary>
        public static void HasChildCount(Transform parent, int expectedCount, string message = null)
        {
            Assert.That(parent, Is.Not.Null, "Parent transform is null");
            Assert.That(parent.childCount, Is.EqualTo(expectedCount),
                message ?? $"Expected {expectedCount} children but found {parent.childCount}");
        }

        /// <summary>
        /// 특정 이름의 자식이 존재하는지 검증
        /// </summary>
        public static Transform HasChild(Transform parent, string childName, string message = null)
        {
            Assert.That(parent, Is.Not.Null, "Parent transform is null");
            var child = parent.Find(childName);
            Assert.That(child, Is.Not.Null,
                message ?? $"Child '{childName}' not found under '{parent.name}'");
            return child;
        }

        /// <summary>
        /// 자식 오브젝트가 없는지 검증
        /// </summary>
        public static void HasNoChildren(Transform parent, string message = null)
        {
            Assert.That(parent, Is.Not.Null, "Parent transform is null");
            Assert.That(parent.childCount, Is.EqualTo(0),
                message ?? $"Expected no children but found {parent.childCount}");
        }

        #endregion

        #region UI Assertions

        /// <summary>
        /// RectTransform 크기 검증
        /// </summary>
        public static void HasSize(RectTransform rect, Vector2 expectedSize, float tolerance = 0.01f, string message = null)
        {
            Assert.That(rect, Is.Not.Null, "RectTransform is null");
            Assert.That(rect.rect.width, Is.EqualTo(expectedSize.x).Within(tolerance),
                message ?? $"Width mismatch: expected {expectedSize.x}, got {rect.rect.width}");
            Assert.That(rect.rect.height, Is.EqualTo(expectedSize.y).Within(tolerance),
                message ?? $"Height mismatch: expected {expectedSize.y}, got {rect.rect.height}");
        }

        /// <summary>
        /// RectTransform anchoredPosition 검증
        /// </summary>
        public static void HasAnchoredPosition(RectTransform rect, Vector2 expectedPos, float tolerance = 0.01f, string message = null)
        {
            Assert.That(rect, Is.Not.Null, "RectTransform is null");
            Assert.That(rect.anchoredPosition.x, Is.EqualTo(expectedPos.x).Within(tolerance),
                message ?? $"X position mismatch: expected {expectedPos.x}, got {rect.anchoredPosition.x}");
            Assert.That(rect.anchoredPosition.y, Is.EqualTo(expectedPos.y).Within(tolerance),
                message ?? $"Y position mismatch: expected {expectedPos.y}, got {rect.anchoredPosition.y}");
        }

        #endregion

        #region Transform Assertions

        /// <summary>
        /// 월드 위치 검증
        /// </summary>
        public static void HasPosition(Transform transform, Vector3 expectedPos, float tolerance = 0.01f, string message = null)
        {
            Assert.That(transform, Is.Not.Null, "Transform is null");
            var actual = transform.position;
            Assert.That(actual.x, Is.EqualTo(expectedPos.x).Within(tolerance),
                message ?? $"X position mismatch: expected {expectedPos.x}, got {actual.x}");
            Assert.That(actual.y, Is.EqualTo(expectedPos.y).Within(tolerance),
                message ?? $"Y position mismatch: expected {expectedPos.y}, got {actual.y}");
            Assert.That(actual.z, Is.EqualTo(expectedPos.z).Within(tolerance),
                message ?? $"Z position mismatch: expected {expectedPos.z}, got {actual.z}");
        }

        /// <summary>
        /// 로컬 위치 검증
        /// </summary>
        public static void HasLocalPosition(Transform transform, Vector3 expectedPos, float tolerance = 0.01f, string message = null)
        {
            Assert.That(transform, Is.Not.Null, "Transform is null");
            var actual = transform.localPosition;
            Assert.That(actual.x, Is.EqualTo(expectedPos.x).Within(tolerance),
                message ?? $"Local X mismatch: expected {expectedPos.x}, got {actual.x}");
            Assert.That(actual.y, Is.EqualTo(expectedPos.y).Within(tolerance),
                message ?? $"Local Y mismatch: expected {expectedPos.y}, got {actual.y}");
            Assert.That(actual.z, Is.EqualTo(expectedPos.z).Within(tolerance),
                message ?? $"Local Z mismatch: expected {expectedPos.z}, got {actual.z}");
        }

        #endregion

        #region Async/Wait Helpers (for use in IEnumerator tests)

        /// <summary>
        /// 조건이 참이 될 때까지 대기 (타임아웃 포함)
        /// </summary>
        public static IEnumerator WaitUntilTrue(Func<bool> condition, float timeoutSeconds = 5f, string failMessage = null)
        {
            float elapsed = 0f;
            while (!condition() && elapsed < timeoutSeconds)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }

            Assert.That(condition(), Is.True,
                failMessage ?? $"Condition not met within {timeoutSeconds} seconds");
        }

        /// <summary>
        /// 조건이 거짓이 될 때까지 대기 (타임아웃 포함)
        /// </summary>
        public static IEnumerator WaitUntilFalse(Func<bool> condition, float timeoutSeconds = 5f, string failMessage = null)
        {
            float elapsed = 0f;
            while (condition() && elapsed < timeoutSeconds)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }

            Assert.That(condition(), Is.False,
                failMessage ?? $"Condition still true after {timeoutSeconds} seconds");
        }

        /// <summary>
        /// GameObject가 활성화될 때까지 대기
        /// </summary>
        public static IEnumerator WaitUntilActive(GameObject obj, float timeoutSeconds = 5f)
        {
            yield return WaitUntilTrue(
                () => obj != null && obj.activeInHierarchy,
                timeoutSeconds,
                $"GameObject '{obj?.name ?? "null"}' did not become active within {timeoutSeconds} seconds"
            );
        }

        /// <summary>
        /// GameObject가 비활성화될 때까지 대기
        /// </summary>
        public static IEnumerator WaitUntilInactive(GameObject obj, float timeoutSeconds = 5f)
        {
            yield return WaitUntilTrue(
                () => obj == null || !obj.activeInHierarchy,
                timeoutSeconds,
                $"GameObject '{obj?.name ?? "null"}' did not become inactive within {timeoutSeconds} seconds"
            );
        }

        /// <summary>
        /// 컴포넌트가 존재할 때까지 대기
        /// </summary>
        public static IEnumerator WaitUntilComponentExists<T>(GameObject obj, float timeoutSeconds = 5f) where T : Component
        {
            T component = null;
            yield return WaitUntilTrue(
                () =>
                {
                    component = obj?.GetComponent<T>();
                    return component != null;
                },
                timeoutSeconds,
                $"Component {typeof(T).Name} not found on '{obj?.name ?? "null"}' within {timeoutSeconds} seconds"
            );
        }

        #endregion
    }
}
