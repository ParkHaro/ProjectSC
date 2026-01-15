using System;

namespace Sc.Data
{
    /// <summary>
    /// 유저 프로필 데이터
    /// </summary>
    [Serializable]
    public struct UserProfile
    {
        /// <summary>
        /// 유저 고유 식별자
        /// </summary>
        public string Uid;

        /// <summary>
        /// 닉네임
        /// </summary>
        public string Nickname;

        /// <summary>
        /// 계정 레벨
        /// </summary>
        public int Level;

        /// <summary>
        /// 현재 경험치
        /// </summary>
        public long Exp;

        /// <summary>
        /// 최대 경험치 (레벨업 필요)
        /// </summary>
        public long MaxExp;

        /// <summary>
        /// 계정 생성 시간 (Unix Timestamp)
        /// </summary>
        public long CreatedAt;

        /// <summary>
        /// 마지막 로그인 시간 (Unix Timestamp)
        /// </summary>
        public long LastLoginAt;

        /// <summary>
        /// 기본값으로 초기화된 프로필 생성
        /// </summary>
        public static UserProfile CreateDefault(string uid, string nickname)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return new UserProfile
            {
                Uid = uid,
                Nickname = nickname,
                Level = 1,
                Exp = 0,
                MaxExp = 100,
                CreatedAt = now,
                LastLoginAt = now
            };
        }
    }
}
