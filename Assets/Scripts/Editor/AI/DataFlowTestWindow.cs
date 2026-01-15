using System;
using Cysharp.Threading.Tasks;
using Sc.Core;
using Sc.Data;
using Sc.Packet;
using UnityEditor;
using UnityEngine;

namespace Sc.Editor.AI
{
    /// <summary>
    /// 데이터 흐름 통합 테스트 에디터 윈도우
    /// Phase 5: Login/Gacha 흐름 검증용
    /// </summary>
    public class DataFlowTestWindow : EditorWindow
    {
        private LocalApiClient _apiClient;
        private Vector2 _scrollPosition;
        private string _testLog = "";

        // 테스트 상태
        private bool _isApiInitialized;
        private bool _isLoggedIn;
        private bool _isDataManagerInitialized;

        // 가챠 테스트 설정
        private string _gachaPoolId = "gacha_standard";
        private GachaPullType _pullType = GachaPullType.Single;

        [MenuItem("SC/Test/Data Flow Test Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<DataFlowTestWindow>("Data Flow Test");
            window.minSize = new Vector2(500, 600);
        }

        private void OnEnable()
        {
            _apiClient = new LocalApiClient(50); // 테스트용 짧은 지연
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("데이터 흐름 통합 테스트", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Phase 5: Login → SetUserData → Gacha → ApplyDelta 흐름 검증",
                MessageType.Info);

            EditorGUILayout.Space(10);
            DrawStatusSection();

            EditorGUILayout.Space(10);
            DrawTestSection();

            EditorGUILayout.Space(10);
            DrawDataViewSection();

            EditorGUILayout.Space(10);
            DrawLogSection();
        }

        private void DrawStatusSection()
        {
            EditorGUILayout.LabelField("시스템 상태", EditorStyles.boldLabel);

            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                DrawStatusIndicator("API Client", _isApiInitialized);
                DrawStatusIndicator("Login", _isLoggedIn);
                DrawStatusIndicator("DataManager", _isDataManagerInitialized);
            }
        }

        private void DrawStatusIndicator(string label, bool isActive)
        {
            var color = isActive ? Color.green : Color.gray;
            var status = isActive ? "●" : "○";

            using (new EditorGUILayout.VerticalScope(GUILayout.Width(100)))
            {
                var style = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = color }
                };
                EditorGUILayout.LabelField(status, style);
                EditorGUILayout.LabelField(label, EditorStyles.centeredGreyMiniLabel);
            }
        }

        private void DrawTestSection()
        {
            EditorGUILayout.LabelField("테스트 실행", EditorStyles.boldLabel);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // Step 1: Initialize
                EditorGUILayout.LabelField("Step 1: 초기화", EditorStyles.miniBoldLabel);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("1-1. API Client 초기화", GUILayout.Height(30)))
                    {
                        RunInitializeApiClient().Forget();
                    }

                    if (GUILayout.Button("1-2. DataManager 초기화", GUILayout.Height(30)))
                    {
                        RunInitializeDataManager();
                    }
                }

                EditorGUILayout.Space(5);

                // Step 2: Login
                EditorGUILayout.LabelField("Step 2: 로그인", EditorStyles.miniBoldLabel);
                using (new EditorGUI.DisabledGroupScope(!_isApiInitialized))
                {
                    if (GUILayout.Button("2-1. 로그인 요청 (Login → SetUserData)", GUILayout.Height(30)))
                    {
                        RunLoginTest().Forget();
                    }
                }

                EditorGUILayout.Space(5);

                // Step 3: Gacha
                EditorGUILayout.LabelField("Step 3: 가챠 테스트", EditorStyles.miniBoldLabel);
                using (new EditorGUI.DisabledGroupScope(!_isLoggedIn))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("가챠 풀:", GUILayout.Width(60));
                        _gachaPoolId = EditorGUILayout.TextField(_gachaPoolId);

                        EditorGUILayout.LabelField("타입:", GUILayout.Width(40));
                        _pullType = (GachaPullType)EditorGUILayout.EnumPopup(_pullType, GUILayout.Width(80));
                    }

                    if (GUILayout.Button("3-1. 가챠 요청 (Gacha → ApplyDelta)", GUILayout.Height(30)))
                    {
                        RunGachaTest().Forget();
                    }
                }

                EditorGUILayout.Space(5);

                // Reset
                EditorGUILayout.LabelField("유틸리티", EditorStyles.miniBoldLabel);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("저장 데이터 삭제", GUILayout.Height(25)))
                    {
                        DeleteSaveData();
                    }

                    if (GUILayout.Button("로그 클리어", GUILayout.Height(25)))
                    {
                        _testLog = "";
                    }
                }
            }
        }

        private void DrawDataViewSection()
        {
            EditorGUILayout.LabelField("현재 데이터 상태", EditorStyles.boldLabel);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (!_isLoggedIn)
                {
                    EditorGUILayout.HelpBox("로그인 후 데이터가 표시됩니다.", MessageType.None);
                    return;
                }

                var dm = DataManager.Instance;
                if (dm == null)
                {
                    EditorGUILayout.HelpBox("DataManager 인스턴스가 없습니다.", MessageType.Warning);
                    return;
                }

                // Profile
                EditorGUILayout.LabelField("프로필", EditorStyles.miniBoldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField($"UID: {dm.Profile.Uid}");
                EditorGUILayout.LabelField($"닉네임: {dm.Profile.Nickname}");
                EditorGUILayout.LabelField($"레벨: {dm.Profile.Level}");
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(3);

                // Currency
                EditorGUILayout.LabelField("재화", EditorStyles.miniBoldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField($"골드: {dm.Currency.Gold:N0}");
                EditorGUILayout.LabelField($"보석: {dm.Currency.Gem} (무료: {dm.Currency.FreeGem})");
                EditorGUILayout.LabelField($"스태미나: {dm.Currency.Stamina}/{dm.Currency.MaxStamina}");
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(3);

                // Characters
                EditorGUILayout.LabelField($"보유 캐릭터 ({dm.OwnedCharacters?.Count ?? 0})", EditorStyles.miniBoldLabel);
                EditorGUI.indentLevel++;
                if (dm.OwnedCharacters != null)
                {
                    foreach (var character in dm.OwnedCharacters)
                    {
                        EditorGUILayout.LabelField($"- {character.CharacterId} (Lv.{character.Level})");
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        private void DrawLogSection()
        {
            EditorGUILayout.LabelField("테스트 로그", EditorStyles.boldLabel);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,
                EditorStyles.helpBox, GUILayout.Height(150));

            var style = new GUIStyle(EditorStyles.label)
            {
                wordWrap = true,
                richText = true
            };
            EditorGUILayout.LabelField(_testLog, style);

            EditorGUILayout.EndScrollView();
        }

        #region Test Methods

        private async UniTaskVoid RunInitializeApiClient()
        {
            Log("=== API Client 초기화 시작 ===");

            try
            {
                var result = await _apiClient.InitializeAsync("local://");
                _isApiInitialized = result;

                Log($"API Client 초기화: {(result ? "<color=green>성공</color>" : "<color=red>실패</color>")}");
            }
            catch (Exception e)
            {
                Log($"<color=red>오류: {e.Message}</color>");
            }

            Repaint();
        }

        private void RunInitializeDataManager()
        {
            Log("=== DataManager 초기화 시작 ===");

            var dm = DataManager.Instance;
            if (dm == null)
            {
                Log("DataManager 인스턴스가 없습니다. 자동 생성합니다...");
                dm = CreateDataManagerInstance();

                if (dm == null)
                {
                    Log("<color=red>DataManager 생성 실패</color>");
                    return;
                }

                Log("<color=green>DataManager 자동 생성 완료</color>");
            }

            var result = dm.Initialize();
            _isDataManagerInitialized = result;

            Log($"DataManager 초기화: {(result ? "<color=green>성공</color>" : "<color=red>실패</color>")}");

            if (result)
            {
                Log($"  - Characters: {dm.Characters?.Count ?? 0}개");
                Log($"  - Skills: {dm.Skills?.Count ?? 0}개");
                Log($"  - Items: {dm.Items?.Count ?? 0}개");
                Log($"  - Stages: {dm.Stages?.Count ?? 0}개");
                Log($"  - GachaPools: {dm.GachaPools?.Count ?? 0}개");
            }

            Repaint();
        }

        private DataManager CreateDataManagerInstance()
        {
            // 기존 인스턴스 확인
            var existing = FindObjectOfType<DataManager>();
            if (existing != null)
                return existing;

            // 새 GameObject 생성
            var go = new GameObject("DataManager");

            // DataManager 컴포넌트 추가
            var dm = go.AddComponent<DataManager>();

            // SerializedObject를 통해 private 필드에 Database 할당
            var so = new SerializedObject(dm);

            AssignDatabase(so, "_characterDatabase", "CharacterDatabase");
            AssignDatabase(so, "_skillDatabase", "SkillDatabase");
            AssignDatabase(so, "_itemDatabase", "ItemDatabase");
            AssignDatabase(so, "_stageDatabase", "StageDatabase");
            AssignDatabase(so, "_gachaPoolDatabase", "GachaPoolDatabase");

            so.ApplyModifiedPropertiesWithoutUndo();

            Log("  - Database 에셋 자동 할당 완료");

            return dm;
        }

        private void AssignDatabase(SerializedObject so, string fieldName, string assetName)
        {
            var path = $"Assets/Data/Generated/{assetName}.asset";
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (asset != null)
            {
                var prop = so.FindProperty(fieldName);
                if (prop != null)
                {
                    prop.objectReferenceValue = asset;
                    Log($"  - {assetName}: 할당됨");
                }
            }
            else
            {
                Log($"  - <color=yellow>{assetName}: 에셋 없음 ({path})</color>");
            }
        }

        private async UniTaskVoid RunLoginTest()
        {
            Log("=== 로그인 테스트 시작 ===");

            try
            {
                var request = LoginRequest.CreateGuest(
                    SystemInfo.deviceUniqueIdentifier,
                    Application.version,
                    Application.platform.ToString()
                );

                Log($"Request: DeviceId={request.DeviceId?.Substring(0, 8)}...");

                var response = await _apiClient.SendAsync<LoginRequest, LoginResponse>(request);

                Log($"Response: IsSuccess={response.IsSuccess}, IsNewUser={response.IsNewUser}");

                if (response.IsSuccess)
                {
                    // DataManager에 유저 데이터 설정
                    var dm = DataManager.Instance;
                    if (dm != null && dm.IsInitialized)
                    {
                        dm.SetUserData(response.UserData);
                        _isLoggedIn = true;

                        Log("<color=green>SetUserData 완료</color>");
                        Log($"  - Profile: {response.UserData.Profile.Nickname}");
                        Log($"  - Characters: {response.UserData.Characters?.Count ?? 0}개");
                        Log($"  - Currency: Gold={response.UserData.Currency.Gold}, Gem={response.UserData.Currency.TotalGem}");
                    }
                    else
                    {
                        Log("<color=yellow>경고: DataManager가 초기화되지 않아 SetUserData 스킵</color>");
                        _isLoggedIn = true; // API 테스트는 성공
                    }
                }
                else
                {
                    Log($"<color=red>로그인 실패: {response.ErrorMessage}</color>");
                }
            }
            catch (Exception e)
            {
                Log($"<color=red>오류: {e.Message}</color>");
            }

            Repaint();
        }

        private async UniTaskVoid RunGachaTest()
        {
            Log("=== 가챠 테스트 시작 ===");

            try
            {
                var dm = DataManager.Instance;
                var beforeGem = dm?.Currency.TotalGem ?? 0;
                var beforeCharCount = dm?.OwnedCharacters?.Count ?? 0;

                Log($"Before: Gem={beforeGem}, Characters={beforeCharCount}");

                var request = _pullType == GachaPullType.Single
                    ? GachaRequest.CreateSingle(_gachaPoolId)
                    : GachaRequest.CreateMulti(_gachaPoolId);

                Log($"Request: Pool={request.GachaPoolId}, Type={request.PullType}");

                var response = await _apiClient.SendAsync<GachaRequest, GachaResponse>(request);

                Log($"Response: IsSuccess={response.IsSuccess}");

                if (response.IsSuccess)
                {
                    Log($"Results ({response.Results?.Count ?? 0}개):");
                    if (response.Results != null)
                    {
                        foreach (var result in response.Results)
                        {
                            var color = result.Rarity == Rarity.SSR ? "yellow" :
                                        result.Rarity == Rarity.SR ? "cyan" : "white";
                            Log($"  - <color={color}>[{result.Rarity}] {result.CharacterId}</color> {(result.IsNew ? "(NEW)" : "")}");
                        }
                    }

                    // Delta 적용
                    if (dm != null && dm.IsInitialized && response.Delta != null)
                    {
                        dm.ApplyDelta(response.Delta);

                        var afterGem = dm.Currency.TotalGem;
                        var afterCharCount = dm.OwnedCharacters?.Count ?? 0;

                        Log("<color=green>ApplyDelta 완료</color>");
                        Log($"  - Gem: {beforeGem} → {afterGem} ({afterGem - beforeGem})");
                        Log($"  - Characters: {beforeCharCount} → {afterCharCount} (+{afterCharCount - beforeCharCount})");
                        Log($"  - CurrentPity: {response.CurrentPityCount}");
                    }
                    else
                    {
                        Log("<color=yellow>경고: DataManager가 없거나 Delta가 null</color>");
                    }
                }
                else
                {
                    Log($"<color=red>가챠 실패: {response.ErrorMessage}</color>");
                }
            }
            catch (Exception e)
            {
                Log($"<color=red>오류: {e.Message}</color>");
            }

            Repaint();
        }

        private void DeleteSaveData()
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, "user_save_data.json");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                Log("<color=yellow>저장 데이터 삭제됨</color>");

                // 상태 리셋
                _isLoggedIn = false;
                _apiClient = new LocalApiClient(50);
                _isApiInitialized = false;
            }
            else
            {
                Log("삭제할 저장 데이터가 없습니다.");
            }

            Repaint();
        }

        private void Log(string message)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            _testLog += $"[{timestamp}] {message}\n";
        }

        #endregion
    }
}
