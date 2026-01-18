using System;
using System.Collections;
using Sc.Data;
using Sc.Foundation;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 유저 데이터 저장/로드 관리자.
    /// 마이그레이션, 자동 저장 지원.
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        private const string SaveKey = "user_save";

        private ISaveStorage _storage;
        private SaveMigrator _migrator;
        private Coroutine _autoSaveCoroutine;
        private UserSaveData _cachedData;
        private bool _isDirty;

        /// <summary>
        /// 현재 저장 데이터 버전
        /// </summary>
        public int CurrentVersion => UserSaveData.CurrentVersion;

        /// <summary>
        /// 저장된 데이터 존재 여부
        /// </summary>
        public bool HasSaveData => _storage?.Exists(SaveKey) ?? false;

        /// <summary>
        /// 자동 저장 활성화 여부
        /// </summary>
        public bool IsAutoSaveEnabled => _autoSaveCoroutine != null;

        /// <summary>
        /// 데이터 변경 여부 (저장 필요)
        /// </summary>
        public bool IsDirty => _isDirty;

        protected override void OnSingletonAwake()
        {
            Initialize(new FileSaveStorage(), new SaveMigrator());
        }

        /// <summary>
        /// 초기화 (테스트용 의존성 주입)
        /// </summary>
        public void Initialize(ISaveStorage storage, SaveMigrator migrator)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _migrator = migrator ?? throw new ArgumentNullException(nameof(migrator));
            _isDirty = false;

            Log.Info("[SaveManager] 초기화 완료", LogCategory.Data);
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        public Result<bool> Save(UserSaveData data)
        {
            if (_storage == null)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "SaveManager가 초기화되지 않았습니다.");
            }

            try
            {
                data.LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var json = JsonUtility.ToJson(data, true);

                var result = _storage.Save(SaveKey, json);
                if (result.IsSuccess)
                {
                    _cachedData = data;
                    _isDirty = false;
                    Log.Info($"[SaveManager] 저장 완료 (v{data.Version})", LogCategory.Data);
                }

                return result;
            }
            catch (Exception e)
            {
                Log.Error($"[SaveManager] 저장 중 예외 발생: {e.Message}", LogCategory.Data);
                return Result<bool>.Failure(ErrorCode.SaveFailed, e.Message);
            }
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        public Result<UserSaveData> Load()
        {
            if (_storage == null)
            {
                return Result<UserSaveData>.Failure(ErrorCode.SystemInitFailed, "SaveManager가 초기화되지 않았습니다.");
            }

            if (!HasSaveData)
            {
                return Result<UserSaveData>.Failure(ErrorCode.LoadFailed, "저장된 데이터가 없습니다.");
            }

            try
            {
                var jsonResult = _storage.Load(SaveKey);
                if (jsonResult.IsFailure)
                {
                    return Result<UserSaveData>.Failure(jsonResult.Error, jsonResult.Message);
                }

                var data = JsonUtility.FromJson<UserSaveData>(jsonResult.Value);

                // 마이그레이션 필요 시 실행
                if (NeedsMigration(data))
                {
                    data = Migrate(data);
                    // 마이그레이션 후 즉시 저장
                    Save(data);
                }

                // EventCurrency null 체크 (JSON 역직렬화 특성)
                if (data.EventCurrency.Currencies == null)
                {
                    data.EventCurrency = EventCurrencyData.CreateDefault();
                }

                _cachedData = data;
                _isDirty = false;
                Log.Info($"[SaveManager] 로드 완료 (v{data.Version})", LogCategory.Data);

                return Result<UserSaveData>.Success(data);
            }
            catch (Exception e)
            {
                Log.Error($"[SaveManager] 로드 중 예외 발생: {e.Message}", LogCategory.Data);
                return Result<UserSaveData>.Failure(ErrorCode.LoadFailed, e.Message);
            }
        }

        /// <summary>
        /// 마이그레이션 필요 여부 확인
        /// </summary>
        public bool NeedsMigration(UserSaveData data)
        {
            return _migrator?.NeedsMigration(data, CurrentVersion) ?? false;
        }

        /// <summary>
        /// 마이그레이션 실행
        /// </summary>
        public UserSaveData Migrate(UserSaveData data)
        {
            if (_migrator == null)
            {
                Log.Warning("[SaveManager] Migrator가 없습니다. 기본 마이그레이션 사용.", LogCategory.Data);
                return UserSaveData.Migrate(data);
            }

            return _migrator.Migrate(data, CurrentVersion);
        }

        /// <summary>
        /// 데이터 삭제
        /// </summary>
        public Result<bool> Delete()
        {
            if (_storage == null)
            {
                return Result<bool>.Failure(ErrorCode.SystemInitFailed, "SaveManager가 초기화되지 않았습니다.");
            }

            var result = _storage.Delete(SaveKey);
            if (result.IsSuccess)
            {
                _cachedData = default;
                _isDirty = false;
                Log.Info("[SaveManager] 데이터 삭제 완료", LogCategory.Data);
            }

            return result;
        }

        /// <summary>
        /// 데이터 변경 플래그 설정
        /// </summary>
        public void MarkDirty()
        {
            _isDirty = true;
        }

        /// <summary>
        /// 자동 저장 활성화
        /// </summary>
        /// <param name="intervalSeconds">저장 간격 (초)</param>
        public void EnableAutoSave(float intervalSeconds)
        {
            DisableAutoSave();
            _autoSaveCoroutine = StartCoroutine(AutoSaveCoroutine(intervalSeconds));
            Log.Info($"[SaveManager] 자동 저장 활성화 ({intervalSeconds}초 간격)", LogCategory.Data);
        }

        /// <summary>
        /// 자동 저장 비활성화
        /// </summary>
        public void DisableAutoSave()
        {
            if (_autoSaveCoroutine != null)
            {
                StopCoroutine(_autoSaveCoroutine);
                _autoSaveCoroutine = null;
                Log.Info("[SaveManager] 자동 저장 비활성화", LogCategory.Data);
            }
        }

        private IEnumerator AutoSaveCoroutine(float intervalSeconds)
        {
            var wait = new WaitForSeconds(intervalSeconds);

            while (true)
            {
                yield return wait;

                if (_isDirty && _cachedData.Version > 0)
                {
                    Save(_cachedData);
                }
            }
        }

        protected override void OnSingletonDestroy()
        {
            DisableAutoSave();

            // 종료 시 변경된 데이터 저장
            if (_isDirty && _cachedData.Version > 0)
            {
                Save(_cachedData);
            }
        }
    }
}
