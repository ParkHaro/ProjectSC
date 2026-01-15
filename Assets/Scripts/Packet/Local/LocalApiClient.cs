using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Sc.Data;
using UnityEngine;

namespace Sc.Packet
{
    /// <summary>
    /// 로컬 API 클라이언트 (서버 응답 시뮬레이션)
    /// 개발/테스트용 더미 서버 구현
    /// </summary>
    public class LocalApiClient : IApiClient
    {
        private const string SaveFileName = "user_save_data.json";

        private readonly int _simulatedLatencyMs;
        private UserSaveData _userData;
        private string _sessionToken;
        private bool _isInitialized;

        public bool IsInitialized => _isInitialized;
        public string SessionToken => _sessionToken;

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);

        public LocalApiClient(int simulatedLatencyMs = 100)
        {
            _simulatedLatencyMs = simulatedLatencyMs;
        }

        #region IApiClient Implementation

        public async UniTask<bool> InitializeAsync(string baseUrl)
        {
            if (_isInitialized) return true;

            await UniTask.Delay(_simulatedLatencyMs);

            // 기존 저장 데이터 로드 시도
            if (File.Exists(SaveFilePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(SaveFilePath);
                    _userData = JsonUtility.FromJson<UserSaveData>(json);
                    Debug.Log($"[LocalApiClient] 저장 데이터 로드 완료: {_userData.Profile.Nickname}");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[LocalApiClient] 저장 데이터 로드 실패: {e.Message}");
                    _userData = default;
                }
            }

            _isInitialized = true;
            return true;
        }

        public async UniTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            await UniTask.Delay(_simulatedLatencyMs);

            // 요청 타입별 처리
            IResponse response = request switch
            {
                LoginRequest loginRequest => await HandleLoginAsync(loginRequest),
                GachaRequest gachaRequest => await HandleGachaAsync(gachaRequest),
                ShopPurchaseRequest purchaseRequest => await HandlePurchaseAsync(purchaseRequest),
                _ => throw new NotImplementedException($"Handler not found for {typeof(TRequest).Name}")
            };

            return (TResponse)response;
        }

        #endregion

        #region Request Handlers

        private async UniTask<LoginResponse> HandleLoginAsync(LoginRequest request)
        {
            bool isNewUser = false;

            // 기존 유저 데이터가 없으면 신규 생성
            if (string.IsNullOrEmpty(_userData.Profile.Uid))
            {
                var uid = request.UserId ?? Guid.NewGuid().ToString();
                var nickname = $"Player_{uid.Substring(0, 6)}";
                _userData = UserSaveData.CreateNew(uid, nickname);
                isNewUser = true;

                // 초기 캐릭터 지급 (튜토리얼 캐릭터)
                _userData.Characters.Add(OwnedCharacter.Create("char_005"));

                await SaveUserDataAsync();
                Debug.Log($"[LocalApiClient] 신규 유저 생성: {nickname}");
            }
            else
            {
                // 기존 유저 로그인
                _userData.Profile.LastLoginAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                await SaveUserDataAsync();
                Debug.Log($"[LocalApiClient] 기존 유저 로그인: {_userData.Profile.Nickname}");
            }

            _sessionToken = Guid.NewGuid().ToString();

            return LoginResponse.Success(_userData, isNewUser, _sessionToken);
        }

        private async UniTask<GachaResponse> HandleGachaAsync(GachaRequest request)
        {
            // 가챠 풀 정보 조회 (실제로는 마스터 데이터에서)
            var pullCount = request.PullType == GachaPullType.Multi ? 10 : 1;
            var costPerPull = 300;
            var totalCost = request.PullType == GachaPullType.Multi ? 2700 : costPerPull;

            // 재화 확인
            if (_userData.Currency.TotalGem < totalCost)
            {
                return GachaResponse.Fail(1001, "보석이 부족합니다.");
            }

            // 가챠 실행
            var results = new List<GachaResultItem>();
            var addedCharacters = new List<OwnedCharacter>();

            // 천장 정보
            var pityInfo = _userData.GachaPity.GetOrCreatePityInfo(request.GachaPoolId);
            var currentPity = pityInfo.PityCount;

            for (int i = 0; i < pullCount; i++)
            {
                currentPity++;

                var rarity = CalculateGachaRarity(currentPity);
                var characterId = GetRandomCharacterByRarity(rarity);

                var isNew = !_userData.HasCharacter(characterId);
                var isPity = currentPity >= 90 && rarity == Rarity.SSR;

                results.Add(new GachaResultItem
                {
                    CharacterId = characterId,
                    Rarity = rarity,
                    IsNew = isNew,
                    IsPity = isPity
                });

                var newCharacter = OwnedCharacter.Create(characterId);
                addedCharacters.Add(newCharacter);
                _userData.Characters.Add(newCharacter);

                // SSR 획득 시 천장 초기화
                if (rarity == Rarity.SSR)
                {
                    currentPity = 0;
                }
            }

            // 재화 차감
            if (_userData.Currency.FreeGem >= totalCost)
            {
                _userData.Currency.FreeGem -= totalCost;
            }
            else
            {
                var remaining = totalCost - _userData.Currency.FreeGem;
                _userData.Currency.FreeGem = 0;
                _userData.Currency.Gem -= remaining;
            }

            // 천장 정보 업데이트
            UpdatePityInfo(request.GachaPoolId, currentPity, pityInfo.TotalPullCount + pullCount);

            await SaveUserDataAsync();

            // Delta 생성
            var delta = new UserDataDelta
            {
                Currency = _userData.Currency,
                AddedCharacters = addedCharacters,
                GachaPity = _userData.GachaPity
            };

            return GachaResponse.Success(results, delta, currentPity);
        }

        private async UniTask<ShopPurchaseResponse> HandlePurchaseAsync(ShopPurchaseRequest request)
        {
            // 상품 정보 조회 (실제로는 마스터 데이터에서)
            if (request.ProductId == "gold_pack_small")
            {
                var gemCost = 100;
                var goldReward = 10000;

                if (_userData.Currency.TotalGem < gemCost)
                {
                    return ShopPurchaseResponse.Fail(1001, "보석이 부족합니다.");
                }

                // 재화 차감 및 지급
                if (_userData.Currency.FreeGem >= gemCost)
                {
                    _userData.Currency.FreeGem -= gemCost;
                }
                else
                {
                    var remaining = gemCost - _userData.Currency.FreeGem;
                    _userData.Currency.FreeGem = 0;
                    _userData.Currency.Gem -= remaining;
                }
                _userData.Currency.Gold += goldReward;

                await SaveUserDataAsync();

                var rewards = new List<PurchaseRewardItem>
                {
                    new PurchaseRewardItem
                    {
                        RewardType = "Currency",
                        RewardId = "Gold",
                        Amount = goldReward
                    }
                };

                var delta = UserDataDelta.WithCurrency(_userData.Currency);

                return ShopPurchaseResponse.Success(request.ProductId, rewards, delta);
            }

            return ShopPurchaseResponse.Fail(1002, "존재하지 않는 상품입니다.");
        }

        #endregion

        #region Private Methods

        private async UniTask SaveUserDataAsync()
        {
            try
            {
                _userData.LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var json = JsonUtility.ToJson(_userData, true);
                await File.WriteAllTextAsync(SaveFilePath, json);
                Debug.Log("[LocalApiClient] 데이터 저장 완료");
            }
            catch (Exception e)
            {
                Debug.LogError($"[LocalApiClient] 데이터 저장 실패: {e.Message}");
            }
        }

        private Rarity CalculateGachaRarity(int pityCount)
        {
            // 천장 시스템
            if (pityCount >= 90)
                return Rarity.SSR;

            var random = UnityEngine.Random.Range(0f, 1f);

            // 기본 확률: SSR 3%, SR 12%, R 85%
            if (random < 0.03f)
                return Rarity.SSR;
            if (random < 0.15f)
                return Rarity.SR;
            return Rarity.R;
        }

        private string GetRandomCharacterByRarity(Rarity rarity)
        {
            // 간단한 시뮬레이션 (실제로는 가챠 풀의 캐릭터 목록에서 선택)
            return rarity switch
            {
                Rarity.SSR => UnityEngine.Random.Range(0, 2) == 0 ? "char_001" : "char_002",
                Rarity.SR => UnityEngine.Random.Range(0, 2) == 0 ? "char_003" : "char_004",
                _ => UnityEngine.Random.Range(0, 2) == 0 ? "char_005" : "char_006"
            };
        }

        private void UpdatePityInfo(string gachaPoolId, int pityCount, int totalPullCount)
        {
            _userData.GachaPity.PityInfos ??= new List<GachaPityInfo>();

            for (int i = 0; i < _userData.GachaPity.PityInfos.Count; i++)
            {
                if (_userData.GachaPity.PityInfos[i].GachaPoolId == gachaPoolId)
                {
                    var info = _userData.GachaPity.PityInfos[i];
                    info.PityCount = pityCount;
                    info.TotalPullCount = totalPullCount;
                    info.LastPullAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    _userData.GachaPity.PityInfos[i] = info;
                    return;
                }
            }

            // 새로운 풀 정보 추가
            _userData.GachaPity.PityInfos.Add(new GachaPityInfo
            {
                GachaPoolId = gachaPoolId,
                PityCount = pityCount,
                TotalPullCount = totalPullCount,
                LastPullAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
        }

        #endregion
    }
}
