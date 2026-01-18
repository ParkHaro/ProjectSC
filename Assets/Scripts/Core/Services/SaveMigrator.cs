using System.Collections.Generic;
using System.Linq;
using Sc.Data;
using Sc.Foundation;

namespace Sc.Core
{
    /// <summary>
    /// 세이브 데이터 마이그레이션 실행기.
    /// 버전별 마이그레이션 체인을 순차적으로 실행.
    /// </summary>
    public class SaveMigrator
    {
        private readonly List<ISaveMigration> _migrations = new();

        /// <summary>
        /// 마이그레이션 등록
        /// </summary>
        public void Register(ISaveMigration migration)
        {
            _migrations.Add(migration);
            // FromVersion 오름차순 정렬
            _migrations.Sort((a, b) => a.FromVersion.CompareTo(b.FromVersion));
        }

        /// <summary>
        /// 마이그레이션 필요 여부 확인
        /// </summary>
        public bool NeedsMigration(UserSaveData data, int targetVersion)
        {
            return data.Version < targetVersion;
        }

        /// <summary>
        /// 마이그레이션 실행
        /// </summary>
        /// <param name="data">원본 데이터</param>
        /// <param name="targetVersion">목표 버전</param>
        /// <returns>마이그레이션된 데이터</returns>
        public UserSaveData Migrate(UserSaveData data, int targetVersion)
        {
            if (!NeedsMigration(data, targetVersion))
            {
                return data;
            }

            var currentVersion = data.Version;
            var result = data;

            Log.Info($"[SaveMigrator] 마이그레이션 시작: v{currentVersion} → v{targetVersion}", LogCategory.Data);

            // 등록된 마이그레이션 체인 실행
            foreach (var migration in _migrations.Where(m =>
                m.FromVersion >= currentVersion && m.ToVersion <= targetVersion))
            {
                if (result.Version == migration.FromVersion)
                {
                    Log.Debug($"[SaveMigrator] 적용: v{migration.FromVersion} → v{migration.ToVersion}", LogCategory.Data);
                    result = migration.Migrate(result);
                }
            }

            // 등록된 마이그레이션이 없거나 부족한 경우 기본 마이그레이션 사용
            if (result.Version < targetVersion)
            {
                Log.Debug($"[SaveMigrator] 기본 마이그레이션 사용: v{result.Version} → v{targetVersion}", LogCategory.Data);
                result = UserSaveData.Migrate(result);
            }

            Log.Info($"[SaveMigrator] 마이그레이션 완료: v{result.Version}", LogCategory.Data);
            return result;
        }
    }
}
