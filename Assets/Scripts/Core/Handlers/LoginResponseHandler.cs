using Sc.Event.OutGame;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 로그인 응답 핸들러
    /// </summary>
    public class LoginResponseHandler : PacketHandlerBase<LoginResponse>
    {
        public override void Handle(LoginResponse response)
        {
            if (response.IsSuccess)
            {
                Debug.Log($"[LoginHandler] Login success: {response.UserData.Profile.Nickname} (New: {response.IsNewUser})");

                // DataManager에 유저 데이터 설정
                DataManager.Instance.SetUserData(response.UserData);

                // 성공 이벤트 발행
                EventManager.Instance.Publish(new LoginCompletedEvent
                {
                    IsNewUser = response.IsNewUser,
                    UserId = response.UserData.Profile.Uid,
                    Nickname = response.UserData.Profile.Nickname
                });

                // 유저 데이터 로드 이벤트
                EventManager.Instance.Publish(new UserDataLoadedEvent
                {
                    UserData = response.UserData
                });
            }
            else
            {
                Debug.LogWarning($"[LoginHandler] Login failed: {response.ErrorCode} - {response.ErrorMessage}");

                // 실패 이벤트 발행
                EventManager.Instance.Publish(new LoginFailedEvent
                {
                    ErrorCode = response.ErrorCode,
                    ErrorMessage = response.ErrorMessage
                });
            }
        }
    }
}
