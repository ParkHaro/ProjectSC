using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Addressables UI 자동 설정 도구.
    /// UI 프리팹을 Addressables Group에 자동 등록.
    /// </summary>
    public static class AddressableSetupTool
    {
        private const string UI_PREFAB_ROOT = "Assets/Prefabs/UI";

        public static void SetupUIGroups()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                UnityEngine.Debug.LogError(
                    "[AddressableSetupTool] Addressables Settings를 찾을 수 없습니다. Window > Asset Management > Addressables > Groups에서 생성하세요.");
                return;
            }

            Debug.Log("[AddressableSetupTool] UI Groups 설정 시작...");

            // 1. 기존 UI 그룹 제거
            RemoveExistingUIGroups(settings);

            // 2. 새 그룹 생성
            var screensGroup = CreateOrGetGroup(settings, "UI_Screens");
            var popupsGroup = CreateOrGetGroup(settings, "UI_Popups");
            var widgetsGroup = CreateOrGetGroup(settings, "UI_Widgets");

            // 3. Screen 프리팹 등록 (Screens 폴더)
            RegisterPrefabsByPattern(settings, screensGroup, $"{UI_PREFAB_ROOT}/Screens", "*Screen", "UI/Screens");

            // 4. Popup 프리팹 등록 (Popups 폴더)
            RegisterPrefabsByPattern(settings, popupsGroup, $"{UI_PREFAB_ROOT}/Popups", "*Popup", "UI/Popups");

            // 5. Widget 프리팹 등록 (MVP 폴더 - ScreenHeader)
            // CurrencyHUD는 ScreenHeader 내부에 포함되어 있으므로 별도 등록 불필요
            RegisterSpecificPrefabs(settings, widgetsGroup, $"{UI_PREFAB_ROOT}/MVP",
                new[] { "ScreenHeader" }, "UI/Widgets");
            RegisterSpecificPrefabs(settings, widgetsGroup, UI_PREFAB_ROOT,
                new[] { "LoadingWidget" }, "UI/Widgets");

            // 6. 저장
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupAdded, null, true);
            AssetDatabase.SaveAssets();

            Debug.Log("[AddressableSetupTool] UI Groups 설정 완료");
        }

        public static void ClearUIGroups()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                Debug.LogWarning("[AddressableSetupTool] Addressables Settings를 찾을 수 없습니다.");
                return;
            }

            RemoveExistingUIGroups(settings);

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupRemoved, null, true);
            AssetDatabase.SaveAssets();

            Debug.Log("[AddressableSetupTool] UI Groups 제거 완료");
        }

        public static void ValidateUIGroups()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings == null)
            {
                Debug.LogError("[AddressableSetupTool] Addressables Settings를 찾을 수 없습니다.");
                return;
            }

            var uiGroups = settings.groups.Where(g => g != null && g.Name.StartsWith("UI_")).ToList();

            Debug.Log($"[AddressableSetupTool] === UI Groups 검증 ===");

            foreach (var group in uiGroups)
            {
                var entries = group.entries.ToList();
                Debug.Log($"  {group.Name}: {entries.Count}개 에셋");

                foreach (var entry in entries)
                {
                    Debug.Log($"    - {entry.address} ({entry.AssetPath})");
                }
            }

            if (uiGroups.Count == 0)
            {
                Debug.LogWarning("[AddressableSetupTool] UI Group이 없습니다. 'Setup UI Groups'를 실행하세요.");
            }
        }

        private static void RemoveExistingUIGroups(AddressableAssetSettings settings)
        {
            var uiGroups = settings.groups
                .Where(g => g != null && g.Name.StartsWith("UI_"))
                .ToList();

            foreach (var group in uiGroups)
            {
                Debug.Log($"[AddressableSetupTool] 기존 그룹 제거: {group.Name}");
                settings.RemoveGroup(group);
            }
        }

        private static AddressableAssetGroup CreateOrGetGroup(AddressableAssetSettings settings, string groupName)
        {
            var group = settings.FindGroup(groupName);

            if (group != null)
            {
                return group;
            }

            // 새 그룹 생성
            group = settings.CreateGroup(groupName, false, false, true, null,
                typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));

            // 스키마 설정 (로컬 빌드)
            var bundleSchema = group.GetSchema<BundledAssetGroupSchema>();
            if (bundleSchema != null)
            {
                bundleSchema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);
                bundleSchema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
                bundleSchema.BundleMode = BundledAssetGroupSchema.BundlePackingMode.PackTogether;
            }

            Debug.Log($"[AddressableSetupTool] 그룹 생성: {groupName}");
            return group;
        }

        private static void RegisterPrefabsByPattern(
            AddressableAssetSettings settings,
            AddressableAssetGroup group,
            string folderPath,
            string pattern,
            string addressPrefix)
        {
            if (!Directory.Exists(folderPath))
            {
                Debug.LogWarning($"[AddressableSetupTool] 폴더가 없습니다: {folderPath}");
                return;
            }

            // 패턴으로 프리팹 검색
            var guids = AssetDatabase.FindAssets($"t:Prefab {pattern}", new[] { folderPath });

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var assetName = Path.GetFileNameWithoutExtension(assetPath);
                var address = $"{addressPrefix}/{assetName}";

                var entry = settings.CreateOrMoveEntry(guid, group, false, false);
                entry.address = address;

                Debug.Log($"[AddressableSetupTool] 등록: {address}");
            }
        }

        private static void RegisterSpecificPrefabs(
            AddressableAssetSettings settings,
            AddressableAssetGroup group,
            string folderPath,
            string[] prefabNames,
            string addressPrefix)
        {
            foreach (var prefabName in prefabNames)
            {
                var assetPath = $"{folderPath}/{prefabName}.prefab";

                if (!File.Exists(assetPath))
                {
                    Debug.LogWarning($"[AddressableSetupTool] 프리팹을 찾을 수 없습니다: {assetPath}");
                    continue;
                }

                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                var address = $"{addressPrefix}/{prefabName}";

                var entry = settings.CreateOrMoveEntry(guid, group, false, false);
                entry.address = address;

                Debug.Log($"[AddressableSetupTool] 등록: {address}");
            }
        }
    }
}