using System.Collections.Generic;
using Sc.Data;
using Sc.Packet;

namespace Sc.Event.OutGame
{
    #region Login Events

    /// <summary>
    /// 로그인 완료 이벤트
    /// </summary>
    public readonly struct LoginCompletedEvent
    {
        /// <summary>
        /// 신규 유저 여부
        /// </summary>
        public bool IsNewUser { get; init; }

        /// <summary>
        /// 유저 ID
        /// </summary>
        public string UserId { get; init; }

        /// <summary>
        /// 유저 닉네임
        /// </summary>
        public string Nickname { get; init; }
    }

    /// <summary>
    /// 로그인 실패 이벤트
    /// </summary>
    public readonly struct LoginFailedEvent
    {
        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region Gacha Events

    /// <summary>
    /// 가챠 완료 이벤트
    /// </summary>
    public readonly struct GachaCompletedEvent
    {
        /// <summary>
        /// 가챠 풀 ID
        /// </summary>
        public string GachaPoolId { get; init; }

        /// <summary>
        /// 가챠 결과 목록
        /// </summary>
        public List<GachaResultItem> Results { get; init; }

        /// <summary>
        /// 현재 천장 카운트
        /// </summary>
        public int PityCount { get; init; }

        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        public UserDataDelta Delta { get; init; }
    }

    /// <summary>
    /// 가챠 실패 이벤트
    /// </summary>
    public readonly struct GachaFailedEvent
    {
        /// <summary>
        /// 가챠 풀 ID
        /// </summary>
        public string GachaPoolId { get; init; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region Purchase Events

    /// <summary>
    /// 상점 구매 완료 이벤트
    /// </summary>
    public readonly struct PurchaseCompletedEvent
    {
        /// <summary>
        /// 상품 ID
        /// </summary>
        public string ProductId { get; init; }

        /// <summary>
        /// 획득한 보상 목록
        /// </summary>
        public List<RewardInfo> Rewards { get; init; }

        /// <summary>
        /// 유저 데이터 변경분
        /// </summary>
        public UserDataDelta Delta { get; init; }
    }

    /// <summary>
    /// 상점 구매 실패 이벤트
    /// </summary>
    public readonly struct PurchaseFailedEvent
    {
        /// <summary>
        /// 상품 ID
        /// </summary>
        public string ProductId { get; init; }

        /// <summary>
        /// 에러 코드
        /// </summary>
        public int ErrorCode { get; init; }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region Game Flow Events

    /// <summary>
    /// 게임 초기화 완료 이벤트
    /// </summary>
    public readonly struct GameInitializedEvent
    {
        /// <summary>
        /// 초기화 성공 여부
        /// </summary>
        public bool IsSuccess { get; init; }

        /// <summary>
        /// 실패 시 에러 메시지
        /// </summary>
        public string ErrorMessage { get; init; }
    }

    #endregion

    #region UserData Events

    /// <summary>
    /// 유저 데이터 동기화 완료 이벤트
    /// </summary>
    public readonly struct UserDataSyncedEvent
    {
        /// <summary>
        /// 변경된 데이터
        /// </summary>
        public UserDataDelta Delta { get; init; }
    }

    /// <summary>
    /// 유저 데이터 로드 완료 이벤트
    /// </summary>
    public readonly struct UserDataLoadedEvent
    {
        /// <summary>
        /// 전체 유저 데이터
        /// </summary>
        public UserSaveData UserData { get; init; }
    }

    #endregion
}
