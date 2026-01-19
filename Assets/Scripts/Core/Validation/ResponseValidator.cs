using Sc.Data;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 클라이언트측 응답 검증기
    /// 서버 응답과 요청 간의 일관성 검증 (2차 검증)
    /// </summary>
    /// <remarks>
    /// 주요 검증:
    /// - 요청과 응답의 일치성
    /// - Delta 적용 전 기본 검증
    /// - 비정상 응답 탐지
    /// </remarks>
    public static class ResponseValidator
    {
        /// <summary>
        /// 가챠 응답 검증
        /// </summary>
        public static bool ValidateGachaResponse(GachaRequest request, GachaResponse response)
        {
            if (!response.IsSuccess)
            {
                Debug.LogWarning($"[ResponseValidator] Gacha failed: {response.ErrorCode}");
                return false;
            }

            // 결과 개수 검증
            var expectedCount = request.PullType == GachaPullType.Multi ? 10 : 1;
            if (response.Results == null || response.Results.Count != expectedCount)
            {
                Debug.LogError($"[ResponseValidator] Invalid gacha result count: expected {expectedCount}, got {response.Results?.Count ?? 0}");
                return false;
            }

            // Delta 검증
            if (response.Delta.Currency.HasValue && response.Delta.Currency.Value.TotalGem < 0)
            {
                Debug.LogError("[ResponseValidator] Invalid currency in gacha delta: negative gem");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 상점 구매 응답 검증
        /// </summary>
        public static bool ValidateShopPurchaseResponse(ShopPurchaseRequest request, ShopPurchaseResponse response)
        {
            if (!response.IsSuccess)
            {
                Debug.LogWarning($"[ResponseValidator] Shop purchase failed: {response.ErrorCode}");
                return false;
            }

            // ProductId 일치 검증
            if (response.ProductId != request.ProductId)
            {
                Debug.LogError($"[ResponseValidator] Product ID mismatch: requested {request.ProductId}, got {response.ProductId}");
                return false;
            }

            // 보상 검증
            if (response.Rewards == null || response.Rewards.Count == 0)
            {
                Debug.LogError("[ResponseValidator] Shop purchase returned no rewards");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 로그인 응답 검증
        /// </summary>
        public static bool ValidateLoginResponse(LoginRequest request, LoginResponse response)
        {
            if (!response.IsSuccess)
            {
                Debug.LogWarning($"[ResponseValidator] Login failed: {response.ErrorCode}");
                return false;
            }

            // UserData 검증
            if (string.IsNullOrEmpty(response.UserData.Profile.Uid))
            {
                Debug.LogError("[ResponseValidator] Login response missing user UID");
                return false;
            }

            // SessionToken 검증
            if (string.IsNullOrEmpty(response.SessionToken))
            {
                Debug.LogError("[ResponseValidator] Login response missing session token");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delta 유효성 검증
        /// </summary>
        public static bool ValidateDelta(UserDataDelta delta)
        {
            // 재화 음수 체크
            if (delta.Currency.HasValue)
            {
                var currency = delta.Currency.Value;
                if (currency.Gold < 0 || currency.Gem < 0 || currency.FreeGem < 0)
                {
                    Debug.LogError("[ResponseValidator] Invalid delta: negative currency");
                    return false;
                }
            }

            return true;
        }
    }
}
