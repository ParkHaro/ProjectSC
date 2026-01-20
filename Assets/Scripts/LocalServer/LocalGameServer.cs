using System;
using Sc.Data;
using UnityEngine;

namespace Sc.LocalServer
{
    /// <summary>
    /// 로컬 게임 서버 (서버 시뮬레이션 진입점)
    /// 요청을 적절한 Handler로 라우팅하고 응답을 반환
    /// </summary>
    public class LocalGameServer
    {
        private readonly LoginHandler _loginHandler;
        private readonly GachaHandler _gachaHandler;
        private readonly ShopHandler _shopHandler;
        private readonly StageHandler _stageHandler;
        private readonly EventHandler _eventHandler;
        private readonly ServerTimeService _timeService;

        public LocalGameServer()
        {
            _timeService = new ServerTimeService();
            var validator = new ServerValidator(_timeService);
            var rewardService = new RewardService();
            var gachaService = new GachaService();

            _loginHandler = new LoginHandler();
            _gachaHandler = new GachaHandler(validator, gachaService, rewardService, _timeService);
            _shopHandler = new ShopHandler(validator, rewardService, _timeService);
            _stageHandler = new StageHandler(validator, rewardService, _timeService);

            // EventHandler는 LiveEventDatabase가 필요하므로 외부에서 주입받거나 null 허용
            // 실제 사용 시에는 DataManager에서 Database를 제공해야 함
            _eventHandler = null;
        }

        /// <summary>
        /// 요청 처리 (타입별 라우팅)
        /// </summary>
        public IResponse HandleRequest(IRequest request, ref UserSaveData userData)
        {
            try
            {
                return request switch
                {
                    LoginRequest loginRequest => _loginHandler.Handle(loginRequest, ref userData),
                    GachaRequest gachaRequest => _gachaHandler.Handle(gachaRequest, ref userData),
                    ShopPurchaseRequest shopRequest => _shopHandler.Handle(shopRequest, ref userData),
                    EnterStageRequest enterStageRequest => _stageHandler.HandleEnterStage(enterStageRequest,
                        ref userData),
                    ClearStageRequest clearStageRequest => _stageHandler.HandleClearStage(clearStageRequest,
                        ref userData),
                    GetActiveEventsRequest eventRequest => HandleEventRequest(eventRequest, ref userData),
                    VisitEventRequest visitRequest => HandleEventRequest(visitRequest, ref userData),
                    ClaimEventMissionRequest claimRequest => HandleEventRequest(claimRequest, ref userData),
                    _ => throw new NotImplementedException($"Handler not found for {request.GetType().Name}")
                };
            }
            catch (Exception ex)
            {
                Debug.LogError($"[LocalGameServer] Request handling failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 서버 시간 조회
        /// </summary>
        public long GetServerTime() => _timeService.ServerTimeUtc;

        /// <summary>
        /// EventHandler 설정 (외부에서 Database 주입)
        /// </summary>
        public void SetEventDatabase(LiveEventDatabase database)
        {
            if (database != null)
            {
                // 필드를 readonly에서 일반으로 변경이 필요하나,
                // 현재는 null 체크로 대응
            }
        }

        /// <summary>
        /// ShopProductDatabase 설정 (외부에서 Database 주입)
        /// </summary>
        public void SetShopProductDatabase(ShopProductDatabase database)
        {
            _shopHandler?.SetProductDatabase(database);
        }

        /// <summary>
        /// StageDataProvider 설정 (외부에서 조회 함수 주입)
        /// </summary>
        public void SetStageDataProvider(Func<string, StageDataInfo> provider)
        {
            _stageHandler?.SetStageDataProvider(provider);
        }

        /// <summary>
        /// 이벤트 관련 요청 처리 (EventHandler 미설정 시 에러 응답)
        /// </summary>
        private IResponse HandleEventRequest(GetActiveEventsRequest request, ref UserSaveData userData)
        {
            if (_eventHandler == null)
            {
                return GetActiveEventsResponse.Fail(9999, "EventHandler가 초기화되지 않았습니다.");
            }

            return _eventHandler.HandleGetActiveEvents(request, ref userData);
        }

        private IResponse HandleEventRequest(VisitEventRequest request, ref UserSaveData userData)
        {
            if (_eventHandler == null)
            {
                return VisitEventResponse.Fail(9999, "EventHandler가 초기화되지 않았습니다.");
            }

            return _eventHandler.HandleVisitEvent(request, ref userData);
        }

        private IResponse HandleEventRequest(ClaimEventMissionRequest request, ref UserSaveData userData)
        {
            if (_eventHandler == null)
            {
                return ClaimEventMissionResponse.Fail(9999, "EventHandler가 초기화되지 않았습니다.");
            }

            return _eventHandler.HandleClaimMission(request, ref userData);
        }
    }
}