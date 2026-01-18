using System;
using Sc.Core;
using Sc.Data;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Tests
{
    /// <summary>
    /// SaveManager 시스템 테스트 시나리오 모음.
    /// MockSaveStorage를 사용하여 격리된 테스트 환경 제공.
    /// </summary>
    public class SaveManagerTestScenarios
    {
        private readonly MockSaveStorage _mockStorage;
        private readonly SaveMigrator _migrator;
        private SaveManager _saveManager;

        public SaveManagerTestScenarios()
        {
            _mockStorage = new MockSaveStorage();
            _migrator = new SaveMigrator();
        }

        /// <summary>
        /// 테스트 환경 초기화
        /// </summary>
        public void Setup()
        {
            _mockStorage.Clear();

            // SaveManager가 Singleton이므로 Instance를 통해 접근
            // 테스트용으로 Initialize 호출하여 Mock 주입
            if (SaveManager.HasInstance)
            {
                SaveManager.Instance.Initialize(_mockStorage, _migrator);
            }
        }

        /// <summary>
        /// 테스트 환경 정리
        /// </summary>
        public void Teardown()
        {
            _mockStorage.Clear();
        }

        #region Scenario: Basic Save/Load

        /// <summary>
        /// 시나리오: 기본 저장/로드
        /// 새 데이터 저장 → 로드 → 데이터 일치 확인
        /// </summary>
        public TestResult RunBasicSaveLoadScenario()
        {
            Setup();

            try
            {
                // 테스트 데이터 생성
                var userData = UserSaveData.CreateNew("test_user_001", "TestPlayer");
                userData.Currency.Gold = 5000;
                userData.Currency.Gem = 100;

                // 저장
                var saveResult = SaveManager.Instance.Save(userData);
                if (saveResult.IsFailure)
                {
                    return TestResult.Fail($"저장 실패: {saveResult.Message}");
                }

                // 로드
                var loadResult = SaveManager.Instance.Load();
                if (loadResult.IsFailure)
                {
                    return TestResult.Fail($"로드 실패: {loadResult.Message}");
                }

                var loadedData = loadResult.Value;

                // 검증
                bool uidMatch = loadedData.Profile.Uid == "test_user_001";
                bool nicknameMatch = loadedData.Profile.Nickname == "TestPlayer";
                bool goldMatch = loadedData.Currency.Gold == 5000;
                bool gemMatch = loadedData.Currency.Gem == 100;

                bool success = uidMatch && nicknameMatch && goldMatch && gemMatch;

                return new TestResult
                {
                    Success = success,
                    Message = $"UID={uidMatch}, Nickname={nicknameMatch}, Gold={goldMatch}, Gem={gemMatch}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Scenario: Character Data Persistence

        /// <summary>
        /// 시나리오: 캐릭터 데이터 저장/로드
        /// 캐릭터 추가 → 저장 → 로드 → 캐릭터 데이터 확인
        /// </summary>
        public TestResult RunCharacterDataScenario()
        {
            Setup();

            try
            {
                var userData = UserSaveData.CreateNew("char_test_user", "CharTester");

                // 캐릭터 추가
                var char1 = OwnedCharacter.Create("char_001");
                char1.Level = 10;
                char1.Ascension = 3;
                userData.Characters.Add(char1);

                var char2 = OwnedCharacter.Create("char_002");
                char2.Level = 5;
                userData.Characters.Add(char2);

                // 저장
                var saveResult = SaveManager.Instance.Save(userData);
                if (saveResult.IsFailure)
                {
                    return TestResult.Fail($"저장 실패: {saveResult.Message}");
                }

                // 로드
                var loadResult = SaveManager.Instance.Load();
                if (loadResult.IsFailure)
                {
                    return TestResult.Fail($"로드 실패: {loadResult.Message}");
                }

                var loadedData = loadResult.Value;

                // 검증
                bool charCountMatch = loadedData.Characters.Count == 2;
                bool char1Exists = loadedData.HasCharacter("char_001");
                bool char2Exists = loadedData.HasCharacter("char_002");

                var loadedChar1 = loadedData.Characters.Find(c => c.CharacterId == "char_001");
                bool char1LevelMatch = loadedChar1.Level == 10;
                bool char1AscensionMatch = loadedChar1.Ascension == 3;

                bool success = charCountMatch && char1Exists && char2Exists && char1LevelMatch && char1AscensionMatch;

                return new TestResult
                {
                    Success = success,
                    Message = $"Count={charCountMatch}, Char1={char1Exists}(Lv{loadedChar1.Level}), Char2={char2Exists}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Scenario: Delete Data

        /// <summary>
        /// 시나리오: 데이터 삭제
        /// 저장 → 삭제 → HasSaveData false 확인
        /// </summary>
        public TestResult RunDeleteDataScenario()
        {
            Setup();

            try
            {
                var userData = UserSaveData.CreateNew("delete_test", "DeleteTester");

                // 저장
                SaveManager.Instance.Save(userData);
                bool existsAfterSave = SaveManager.Instance.HasSaveData;

                // 삭제
                var deleteResult = SaveManager.Instance.Delete();
                if (deleteResult.IsFailure)
                {
                    return TestResult.Fail($"삭제 실패: {deleteResult.Message}");
                }

                bool existsAfterDelete = SaveManager.Instance.HasSaveData;

                bool success = existsAfterSave && !existsAfterDelete;

                return new TestResult
                {
                    Success = success,
                    Message = $"저장후존재={existsAfterSave}, 삭제후존재={existsAfterDelete}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Scenario: Dirty Flag

        /// <summary>
        /// 시나리오: Dirty 플래그 관리
        /// MarkDirty → IsDirty true → Save → IsDirty false
        /// </summary>
        public TestResult RunDirtyFlagScenario()
        {
            Setup();

            try
            {
                var userData = UserSaveData.CreateNew("dirty_test", "DirtyTester");

                // 초기 저장
                SaveManager.Instance.Save(userData);
                bool isDirtyAfterSave = SaveManager.Instance.IsDirty;

                // Dirty 마킹
                SaveManager.Instance.MarkDirty();
                bool isDirtyAfterMark = SaveManager.Instance.IsDirty;

                // 다시 저장
                SaveManager.Instance.Save(userData);
                bool isDirtyAfterReSave = SaveManager.Instance.IsDirty;

                bool success = !isDirtyAfterSave && isDirtyAfterMark && !isDirtyAfterReSave;

                return new TestResult
                {
                    Success = success,
                    Message = $"저장후={isDirtyAfterSave}, 마킹후={isDirtyAfterMark}, 재저장후={isDirtyAfterReSave}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Scenario: Version Check

        /// <summary>
        /// 시나리오: 버전 정보 확인
        /// CurrentVersion이 UserSaveData.CurrentVersion과 일치
        /// </summary>
        public TestResult RunVersionCheckScenario()
        {
            Setup();

            try
            {
                int managerVersion = SaveManager.Instance.CurrentVersion;
                int dataVersion = UserSaveData.CurrentVersion;

                bool success = managerVersion == dataVersion;

                return new TestResult
                {
                    Success = success,
                    Message = $"Manager.Version={managerVersion}, Data.Version={dataVersion}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Scenario: Load Without Save

        /// <summary>
        /// 시나리오: 저장된 데이터 없이 로드 시도
        /// 저장 없이 Load → 실패 반환
        /// </summary>
        public TestResult RunLoadWithoutSaveScenario()
        {
            Setup();

            try
            {
                // 저장 없이 바로 로드 시도
                bool hasSaveData = SaveManager.Instance.HasSaveData;
                var loadResult = SaveManager.Instance.Load();

                bool success = !hasSaveData && loadResult.IsFailure;

                return new TestResult
                {
                    Success = success,
                    Message = $"HasSaveData={hasSaveData}, LoadFailed={loadResult.IsFailure}"
                };
            }
            catch (Exception e)
            {
                return TestResult.Fail($"예외 발생: {e.Message}");
            }
            finally
            {
                Teardown();
            }
        }

        #endregion

        #region Run All Scenarios

        /// <summary>
        /// 모든 시나리오 실행
        /// </summary>
        public void RunAllScenarios()
        {
            Debug.Log("[SaveManagerTest] ===== 테스트 시작 =====");

            LogResult("BasicSaveLoad", RunBasicSaveLoadScenario());
            LogResult("CharacterData", RunCharacterDataScenario());
            LogResult("DeleteData", RunDeleteDataScenario());
            LogResult("DirtyFlag", RunDirtyFlagScenario());
            LogResult("VersionCheck", RunVersionCheckScenario());
            LogResult("LoadWithoutSave", RunLoadWithoutSaveScenario());

            Debug.Log("[SaveManagerTest] ===== 테스트 완료 =====");
        }

        private void LogResult(string scenarioName, TestResult result)
        {
            var prefix = $"[SaveManagerTest:{scenarioName}]";
            if (result.Success)
            {
                Debug.Log($"{prefix} PASS - {result.Message}");
            }
            else
            {
                Debug.LogError($"{prefix} FAIL - {result.Message}");
            }
        }

        #endregion
    }
}
