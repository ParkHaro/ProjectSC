using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Sc.Editor.Wizard
{
    /// <summary>
    /// Data 탭: 마스터 데이터 관리.
    /// MasterDataGeneratorWindow 기능 통합.
    /// </summary>
    public class DataTab
    {
        private bool _showJsonSection = true;
        private bool _showGeneratedSection = true;
        private Vector2 _jsonScrollPosition;
        
        private const string JSON_PATH = "Assets/Data/MasterData";
        private const string GENERATED_PATH = "Assets/Data/Generated";

        public void Draw()
        {
            // JSON Files Section
            DrawJsonSection();
            
            EditorGUILayout.Space(10);
            
            // Generated Assets Section
            DrawGeneratedSection();
            
            EditorGUILayout.Space(10);
            
            // Actions Section
            DrawActionsSection();
        }

        private void DrawJsonSection()
        {
            _showJsonSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showJsonSection, "JSON Files");
            
            if (_showJsonSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                if (!Directory.Exists(JSON_PATH))
                {
                    EditorGUILayout.HelpBox($"JSON 폴더가 없습니다: {JSON_PATH}", MessageType.Warning);
                }
                else
                {
                    var jsonFiles = Directory.GetFiles(JSON_PATH, "*.json")
                        .Where(f => !f.Contains("README"))
                        .ToArray();
                    
                    if (jsonFiles.Length == 0)
                    {
                        EditorGUILayout.LabelField("JSON 파일이 없습니다.", EditorStyles.centeredGreyMiniLabel);
                    }
                    else
                    {
                        _jsonScrollPosition = EditorGUILayout.BeginScrollView(_jsonScrollPosition, GUILayout.MaxHeight(150));
                        
                        foreach (var filePath in jsonFiles)
                        {
                            var fileName = Path.GetFileName(filePath);
                            var fileInfo = new FileInfo(filePath);
                            var sizeKB = fileInfo.Length / 1024f;
                            
                            EditorGUILayout.BeginHorizontal();
                            
                            EditorGUILayout.LabelField($"📄 {fileName}", GUILayout.ExpandWidth(true));
                            EditorGUILayout.LabelField($"{sizeKB:F1} KB", GUILayout.Width(60));
                            
                            if (GUILayout.Button("Open", GUILayout.Width(50)))
                            {
                                var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath.Replace("\\", "/"));
                                if (asset != null)
                                {
                                    EditorGUIUtility.PingObject(asset);
                                    Selection.activeObject = asset;
                                }
                            }
                            
                            if (GUILayout.Button("Import", GUILayout.Width(50)))
                            {
                                AssetDatabase.ImportAsset(filePath.Replace("\\", "/"), ImportAssetOptions.ForceUpdate);
                                Debug.Log($"[DataTab] {fileName} 재임포트 완료");
                            }
                            
                            EditorGUILayout.EndHorizontal();
                        }
                        
                        EditorGUILayout.EndScrollView();
                    }
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawGeneratedSection()
        {
            _showGeneratedSection = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneratedSection, "Generated Assets");
            
            if (_showGeneratedSection)
            {
                EditorGUILayout.BeginVertical("box");
                
                // Database 목록
                DrawDatabaseStatus("CharacterDatabase", "Sc.Data.CharacterDatabase");
                DrawDatabaseStatus("SkillDatabase", "Sc.Data.SkillDatabase");
                DrawDatabaseStatus("ItemDatabase", "Sc.Data.ItemDatabase");
                DrawDatabaseStatus("StageDatabase", "Sc.Data.StageDatabase");
                DrawDatabaseStatus("GachaPoolDatabase", "Sc.Data.GachaPoolDatabase");
                DrawDatabaseStatus("LiveEventDatabase", "Sc.Data.LiveEventDatabase");
                DrawDatabaseStatus("ScreenHeaderConfigDatabase", "Sc.Data.ScreenHeaderConfigDatabase");
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawDatabaseStatus(string name, string typeName)
        {
            var assetPath = $"{GENERATED_PATH}/{name}.asset";
            var exists = File.Exists(assetPath);
            var statusIcon = exists ? "✓" : "✗";
            var statusColor = exists ? Color.green : Color.red;
            
            EditorGUILayout.BeginHorizontal();
            
            var prevColor = GUI.color;
            GUI.color = statusColor;
            EditorGUILayout.LabelField(statusIcon, GUILayout.Width(20));
            GUI.color = prevColor;
            
            EditorGUILayout.LabelField(name, GUILayout.ExpandWidth(true));
            
            if (exists)
            {
                if (GUILayout.Button("Select", GUILayout.Width(50)))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
                    if (asset != null)
                    {
                        Selection.activeObject = asset;
                        EditorGUIUtility.PingObject(asset);
                    }
                }
                
                if (GUILayout.Button("Delete", GUILayout.Width(50)))
                {
                    if (EditorUtility.DisplayDialog("삭제 확인", 
                        $"{name}을(를) 삭제하시겠습니까?", "삭제", "취소"))
                    {
                        AssetDatabase.DeleteAsset(assetPath);
                        AssetDatabase.Refresh();
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Not found", EditorStyles.miniLabel, GUILayout.Width(100));
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawActionsSection()
        {
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("🔄 Regenerate All", GUILayout.Height(30)))
            {
                RegenerateAllMasterData();
            }
            
            if (GUILayout.Button("📂 Open Folder", GUILayout.Height(30)))
            {
                if (Directory.Exists(GENERATED_PATH))
                {
                    EditorUtility.RevealInFinder(GENERATED_PATH);
                }
                else
                {
                    Debug.LogWarning($"[DataTab] 폴더가 없습니다: {GENERATED_PATH}");
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("🗑 Delete All Generated Assets", GUILayout.Height(25)))
            {
                if (EditorUtility.DisplayDialog("전체 삭제", 
                    "생성된 모든 에셋을 삭제하시겠습니까?\n이 작업은 되돌릴 수 없습니다.", 
                    "삭제", "취소"))
                {
                    DeleteAllGeneratedAssets();
                }
            }
            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndVertical();
        }

        private void RegenerateAllMasterData()
        {
            if (!Directory.Exists(JSON_PATH))
            {
                Debug.LogError($"[DataTab] JSON 폴더가 없습니다: {JSON_PATH}");
                return;
            }
            
            var jsonFiles = Directory.GetFiles(JSON_PATH, "*.json")
                .Where(f => !f.Contains("README"))
                .ToArray();
            
            foreach (var filePath in jsonFiles)
            {
                AssetDatabase.ImportAsset(filePath.Replace("\\", "/"), ImportAssetOptions.ForceUpdate);
            }
            
            AssetDatabase.Refresh();
            Debug.Log($"[DataTab] {jsonFiles.Length}개 JSON 파일 재임포트 완료");
        }

        private void DeleteAllGeneratedAssets()
        {
            if (Directory.Exists(GENERATED_PATH))
            {
                AssetDatabase.DeleteAsset(GENERATED_PATH);
                AssetDatabase.Refresh();
                Debug.Log("[DataTab] 생성된 에셋 전체 삭제 완료");
            }
        }
    }
}
