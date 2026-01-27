using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sc.Editor.Wizard.PrefabSync
{
    /// <summary>
    /// 프리팹 에셋의 SerializeField 바인딩 유틸리티.
    /// 프리팹 저장 후 에셋에 직접 바인딩하여 참조 손실을 방지.
    /// </summary>
    public static class PrefabFieldBinder
    {
        /// <summary>
        /// 프리팹 생성 및 SerializeField 바인딩을 원자적으로 수행.
        /// </summary>
        /// <param name="gameObject">임시 GameObject (저장 후 파괴됨)</param>
        /// <param name="prefabPath">저장할 프리팹 경로</param>
        /// <param name="bindFields">SerializeField 바인딩 액션 (프리팹 루트 전달)</param>
        /// <returns>저장된 프리팹 에셋</returns>
        public static GameObject SavePrefabWithBindings(
            GameObject gameObject,
            string prefabPath,
            Action<GameObject> bindFields)
        {
            // 1. 프리팹 저장 (구조만)
            var prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
            Object.DestroyImmediate(gameObject);

            if (prefab == null)
            {
                Debug.LogError($"[PrefabFieldBinder] 프리팹 저장 실패: {prefabPath}");
                return null;
            }

            // 2. 프리팹 에셋에 직접 바인딩
            bindFields?.Invoke(prefab);

            // 3. 변경사항 저장
            EditorUtility.SetDirty(prefab);
            AssetDatabase.SaveAssets();

            return prefab;
        }

        /// <summary>
        /// 컴포넌트의 SerializeField에 값 할당 (단일 호출로 배치 처리).
        /// </summary>
        public static void BindFields(
            Component component,
            params (string fieldName, Object value)[] bindings)
        {
            if (component == null)
            {
                Debug.LogWarning("[PrefabFieldBinder] BindFields: component is null");
                return;
            }

            var so = new SerializedObject(component);

            foreach (var (fieldName, value) in bindings)
            {
                var prop = so.FindProperty(fieldName);
                if (prop != null)
                {
                    prop.objectReferenceValue = value;
                }
                else
                {
                    Debug.LogWarning($"[PrefabFieldBinder] 필드를 찾을 수 없음: {component.GetType().Name}.{fieldName}");
                }
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// 배열 타입 SerializeField 바인딩.
        /// </summary>
        public static void BindArrayField(
            Component component,
            string fieldName,
            Object[] values)
        {
            if (component == null)
            {
                Debug.LogWarning("[PrefabFieldBinder] BindArrayField: component is null");
                return;
            }

            var so = new SerializedObject(component);
            var prop = so.FindProperty(fieldName);

            if (prop == null)
            {
                Debug.LogWarning($"[PrefabFieldBinder] 배열 필드를 찾을 수 없음: {component.GetType().Name}.{fieldName}");
                return;
            }

            prop.arraySize = values.Length;
            for (var i = 0; i < values.Length; i++)
            {
                prop.GetArrayElementAtIndex(i).objectReferenceValue = values[i];
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// 기존 프리팹에 SerializeField 바인딩 (재바인딩용).
        /// </summary>
        public static void RebindPrefab(string prefabPath, Action<GameObject> bindFields)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"[PrefabFieldBinder] 프리팹을 찾을 수 없음: {prefabPath}");
                return;
            }

            bindFields?.Invoke(prefab);

            EditorUtility.SetDirty(prefab);
            AssetDatabase.SaveAssets();
        }
    }
}