using System;
using System.Collections.Generic;

namespace Sc.Foundation
{
    /// <summary>
    /// ErrorCode → StringData 키 매핑
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// 다국어 변환 함수 (외부에서 주입)
        /// </summary>
        /// <remarks>
        /// Foundation은 Sc.Core를 참조할 수 없으므로 델리게이트로 연동
        /// GameBootstrap에서 StringManager.Get을 주입
        /// </remarks>
        public static Func<string, string> LocalizeFunc { get; set; }

        private static readonly Dictionary<ErrorCode, string> _keys = new()
        {
            { ErrorCode.None, "" },

            // System
            { ErrorCode.SystemInitFailed, "error.system.init_failed" },
            { ErrorCode.ConfigLoadFailed, "error.system.config_load_failed" },

            // Asset
            { ErrorCode.AssetNotFound, "error.asset.not_found" },
            { ErrorCode.AssetLoadTimeout, "error.asset.load_timeout" },
            { ErrorCode.AssetLoadPartialFail, "error.asset.load_partial_fail" },
            { ErrorCode.AddressablesInitFailed, "error.asset.addressables_init_failed" },

            // Network
            { ErrorCode.NetworkDisconnected, "error.network.disconnected" },
            { ErrorCode.NetworkTimeout, "error.network.timeout" },
            { ErrorCode.ServerError, "error.network.server_error" },
            { ErrorCode.InvalidResponse, "error.network.invalid_response" },

            // Data
            { ErrorCode.SaveFailed, "error.data.save_failed" },
            { ErrorCode.LoadFailed, "error.data.load_failed" },
            { ErrorCode.ParseFailed, "error.data.parse_failed" },
            { ErrorCode.MigrationFailed, "error.data.migration_failed" },

            // Auth
            { ErrorCode.LoginFailed, "error.auth.login_failed" },
            { ErrorCode.SessionExpired, "error.auth.session_expired" },
            { ErrorCode.InvalidToken, "error.auth.invalid_token" },

            // Game
            { ErrorCode.InsufficientGold, "error.game.insufficient_gold" },
            { ErrorCode.InsufficientGem, "error.game.insufficient_gem" },
            { ErrorCode.InsufficientStamina, "error.game.insufficient_stamina" },
            { ErrorCode.InventoryFull, "error.game.inventory_full" },
            { ErrorCode.LevelNotMet, "error.game.level_not_met" },
            { ErrorCode.AlreadyOwned, "error.game.already_owned" },
            { ErrorCode.PurchaseLimitReached, "error.game.purchase_limit_reached" },
            { ErrorCode.StageNotCleared, "error.game.stage_not_cleared" },

            // UI
            { ErrorCode.ScreenLoadFailed, "error.ui.screen_load_failed" },
            { ErrorCode.PopupLoadFailed, "error.ui.popup_load_failed" }
        };

        /// <summary>
        /// ErrorCode에 해당하는 StringData 키 반환
        /// </summary>
        public static string GetKey(ErrorCode code)
        {
            return _keys.TryGetValue(code, out var key) ? key : string.Empty;
        }

        /// <summary>
        /// ErrorCode에 해당하는 다국어 메시지 반환
        /// </summary>
        /// <remarks>
        /// LocalizeFunc가 설정되어 있으면 사용, 아니면 키 반환
        /// </remarks>
        public static string GetMessage(ErrorCode code)
        {
            var key = GetKey(code);
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            return LocalizeFunc?.Invoke(key) ?? key;
        }
    }
}
