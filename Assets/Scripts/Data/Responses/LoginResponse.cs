using System;

namespace Sc.Data
{
    /// <summary>
    /// 로그인 응답
    /// </summary>
    [Serializable]
    public class LoginResponse : IResponse
    {
        /// <summary>
        /// 요청 성공 여부
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 서버 타임스탬프
        /// </summary>
        public long ServerTime { get; set; }

        /// <summary>
        /// 신규 유저 여부
        /// </summary>
        public bool IsNewUser;

        /// <summary>
        /// 유저 전체 데이터 (로그인 시 전체 동기화)
        /// </summary>
        public UserSaveData UserData;

        /// <summary>
        /// 세션 토큰 (추후 인증용)
        /// </summary>
        public string SessionToken;

        /// <summary>
        /// 성공 응답 생성
        /// </summary>
        public static LoginResponse Success(UserSaveData userData, bool isNewUser, string sessionToken = null)
        {
            return new LoginResponse
            {
                IsSuccess = true,
                ErrorCode = 0,
                ErrorMessage = null,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsNewUser = isNewUser,
                UserData = userData,
                SessionToken = sessionToken ?? Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 실패 응답 생성
        /// </summary>
        public static LoginResponse Fail(int errorCode, string errorMessage)
        {
            return new LoginResponse
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }
    }
}
