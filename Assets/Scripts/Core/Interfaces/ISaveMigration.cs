using Sc.Data;

namespace Sc.Core
{
    /// <summary>
    /// 세이브 데이터 마이그레이션 인터페이스.
    /// 버전별 마이그레이션 로직 구현.
    /// </summary>
    public interface ISaveMigration
    {
        /// <summary>
        /// 마이그레이션 시작 버전
        /// </summary>
        int FromVersion { get; }

        /// <summary>
        /// 마이그레이션 결과 버전
        /// </summary>
        int ToVersion { get; }

        /// <summary>
        /// 마이그레이션 실행
        /// </summary>
        /// <param name="data">원본 데이터</param>
        /// <returns>마이그레이션된 데이터</returns>
        UserSaveData Migrate(UserSaveData data);
    }
}
