using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sc.Common.UI;
using Sc.Contents.Lobby;
using Sc.Contents.Title;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Sc.Editor.Wizard.Generators
{
    /// <summary>
    /// Screen/Popup 프리팹 자동 생성 및 Addressables 등록.
    /// </summary>
    public static class PrefabGenerator
    {
        private const string PREFAB_ROOT = "Assets/Prefabs/UI";
        private const string SCREEN_PATH = PREFAB_ROOT + "/Screens";
        private const string POPUP_PATH = PREFAB_ROOT + "/Popups";
        private const string ADDRESSABLE_GROUP = "UI";

        #region Public API

        /// <summary>
        /// 모든 Screen 타입을 스캔하여 프리팹 생성.
        /// </summary>
        public static int GenerateAllScreenPrefabs()
        {
            var screenTypes = FindAllScreenTypes();
            var createdCount = 0;

            foreach (var type in screenTypes)
            {
                if (GenerateScreenPrefab(type))
                    createdCount++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[PrefabGenerator] Screen 프리팹 생성 완료: {createdCount}/{screenTypes.Length}개");
            return createdCount;
        }

        /// <summary>
        /// 모든 Popup 타입을 스캔하여 프리팹 생성.
        /// </summary>
        public static int GenerateAllPopupPrefabs()
        {
            var popupTypes = FindAllPopupTypes();
            var createdCount = 0;

            foreach (var type in popupTypes)
            {
                if (GeneratePopupPrefab(type))
                    createdCount++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[PrefabGenerator] Popup 프리팹 생성 완료: {createdCount}/{popupTypes.Length}개");
            return createdCount;
        }

        /// <summary>
        /// 특정 Screen 프리팹 생성.
        /// </summary>
        public static bool GenerateScreenPrefab(Type screenType)
        {
            if (!IsValidScreenType(screenType))
            {
                Debug.LogWarning($"[PrefabGenerator] {screenType.Name}는 유효한 Screen 타입이 아님");
                return false;
            }

            EnsureDirectoryExists(SCREEN_PATH);

            var prefabPath = $"{SCREEN_PATH}/{screenType.Name}.prefab";

            // 이미 존재하면 스킵
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                Debug.Log($"[PrefabGenerator] 이미 존재: {screenType.Name}");
                return false;
            }

            // GameObject 생성
            var go = CreateScreenGameObject(screenType);

            // 프리팹 저장
            var prefab = PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            UnityEngine.Object.DestroyImmediate(go);

            // Addressables 등록
            RegisterToAddressables(prefabPath, $"UI/Screens/{screenType.Name}");

            Debug.Log($"[PrefabGenerator] Screen 생성: {screenType.Name}");
            return true;
        }

        /// <summary>
        /// 특정 Popup 프리팹 생성.
        /// </summary>
        public static bool GeneratePopupPrefab(Type popupType)
        {
            if (!IsValidPopupType(popupType))
            {
                Debug.LogWarning($"[PrefabGenerator] {popupType.Name}는 유효한 Popup 타입이 아님");
                return false;
            }

            EnsureDirectoryExists(POPUP_PATH);

            var prefabPath = $"{POPUP_PATH}/{popupType.Name}.prefab";

            // 이미 존재하면 스킵
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                Debug.Log($"[PrefabGenerator] 이미 존재: {popupType.Name}");
                return false;
            }

            // GameObject 생성
            var go = CreatePopupGameObject(popupType);

            // 프리팹 저장
            var prefab = PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            UnityEngine.Object.DestroyImmediate(go);

            // Addressables 등록
            RegisterToAddressables(prefabPath, $"UI/Popups/{popupType.Name}");

            Debug.Log($"[PrefabGenerator] Popup 생성: {popupType.Name}");
            return true;
        }

        #endregion

        #region GameObject Creation

        private static GameObject CreateScreenGameObject(Type screenType)
        {
            // LobbyScreen 전용 빌더 사용
            if (screenType == typeof(LobbyScreen))
            {
                return LobbyScreenPrefabBuilder.Build();
            }

            // TitleScreen 전용 빌더 사용
            if (screenType == typeof(TitleScreen))
            {
                return TitleScreenPrefabBuilder.Build();
            }

            // 기본 Screen 생성
            var go = new GameObject(screenType.Name);

            // RectTransform 설정 (전체 화면)
            // Canvas는 부모(NavigationManager.ScreenCanvas)에서 제공하므로 추가하지 않음
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            // CanvasGroup 추가 (Transition용 alpha 제어)
            go.AddComponent<CanvasGroup>();

            // 배경 Image 추가
            var bg = go.AddComponent<Image>();
            bg.color = new Color(0.1f, 0.1f, 0.15f, 1f);
            bg.raycastTarget = true;

            // Screen 컴포넌트 추가
            go.AddComponent(screenType);

            return go;
        }

        private static GameObject CreatePopupGameObject(Type popupType)
        {
            var go = new GameObject(popupType.Name);

            // RectTransform 설정 (중앙 고정 크기)
            // Canvas는 부모(NavigationManager.PopupCanvas)에서 제공하므로 추가하지 않음
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(600, 400);
            rectTransform.anchoredPosition = Vector2.zero;

            // CanvasGroup 추가 (Transition용 alpha 제어)
            go.AddComponent<CanvasGroup>();

            // 배경 Image 추가
            var bg = go.AddComponent<Image>();
            bg.color = new Color(0.1f, 0.1f, 0.15f, 0.95f);
            bg.raycastTarget = true;

            // Popup 컴포넌트 추가
            go.AddComponent(popupType);

            return go;
        }

        #endregion

        #region Type Discovery

        private static Type[] FindAllScreenTypes()
        {
            var screenTypes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(IsValidScreenType)
                        .ToArray();
                    screenTypes.AddRange(types);
                }
                catch (Exception)
                {
                    // 일부 어셈블리는 타입 로드 실패할 수 있음
                }
            }

            return screenTypes.ToArray();
        }

        private static Type[] FindAllPopupTypes()
        {
            var popupTypes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(IsValidPopupType)
                        .ToArray();
                    popupTypes.AddRange(types);
                }
                catch (Exception)
                {
                    // 일부 어셈블리는 타입 로드 실패할 수 있음
                }
            }

            return popupTypes.ToArray();
        }

        private static bool IsValidScreenType(Type type)
        {
            return type != null
                   && !type.IsAbstract
                   && type.IsClass
                   && IsSubclassOfGeneric(type, typeof(ScreenWidget<,>));
        }

        private static bool IsValidPopupType(Type type)
        {
            return type != null
                   && !type.IsAbstract
                   && type.IsClass
                   && IsSubclassOfGeneric(type, typeof(PopupWidget<,>));
        }

        private static bool IsSubclassOfGeneric(Type type, Type genericType)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (genericType == cur)
                    return true;
                type = type.BaseType;
            }

            return false;
        }

        #endregion

        #region Addressables

        private static void RegisterToAddressables(string assetPath, string address)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogWarning("[PrefabGenerator] Addressables 설정을 찾을 수 없음. 수동 등록 필요.");
                return;
            }

            // 그룹 찾기 또는 생성
            var group = settings.FindGroup(ADDRESSABLE_GROUP);
            if (group == null)
            {
                group = settings.CreateGroup(ADDRESSABLE_GROUP, false, false, true, null,
                    typeof(UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema),
                    typeof(UnityEditor.AddressableAssets.Settings.GroupSchemas.ContentUpdateGroupSchema));
            }

            // 에셋 등록
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var entry = settings.CreateOrMoveEntry(guid, group, false, false);
            entry.address = address;

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
        }

        #endregion

        #region Helpers

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
        }

        #endregion
    }
}
