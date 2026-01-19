using System;

namespace Sc.Data
{
    /// <summary>
    /// 로그인 요청
    /// </summary>
    [Serializable]
    public class LoginRequest : IRequest<LoginResponse>
    {
        /// <summary>
        /// 디바이스 ID (게스트 로그인용)
        /// </summary>
        public string DeviceId;

        /// <summary>
        /// 유저 ID (재로그인용)
        /// </summary>
        public string UserId;

        /// <summary>
        /// 클라이언트 버전
        /// </summary>
        public string ClientVersion;

        /// <summary>
        /// 플랫폼 (iOS, Android, Editor)
        /// </summary>
        public string Platform;

        /// <summary>
        /// 요청 타임스탬프
        /// </summary>
        public long Timestamp { get; private set; }

        public LoginRequest()
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 게스트 로그인 요청 생성
        /// </summary>
        public static LoginRequest CreateGuest(string deviceId, string clientVersion, string platform)
        {
            return new LoginRequest
            {
                DeviceId = deviceId,
                UserId = null,
                ClientVersion = clientVersion,
                Platform = platform
            };
        }

        /// <summary>
        /// 재로그인 요청 생성
        /// </summary>
        public static LoginRequest CreateRelogin(string userId, string clientVersion, string platform)
        {
            return new LoginRequest
            {
                DeviceId = null,
                UserId = userId,
                ClientVersion = clientVersion,
                Platform = platform
            };
        }
    }
}
