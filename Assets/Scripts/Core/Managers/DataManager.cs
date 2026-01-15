using System;
using Sc.Data;
using Sc.Foundation;
using Sc.Packet;
using UnityEngine;

namespace Sc.Core
{
    /// <summary>
    /// 데이터 매니저 (마스터 데이터 캐시 + 유저 데이터 읽기 전용 뷰)
    /// 서버 중심 아키텍처: 유저 데이터는 서버 응답(Delta)으로만 갱신
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        [Header("마스터 데이터")]
        [SerializeField] private CharacterDatabase _characterDatabase;
        [SerializeField] private SkillDatabase _skillDatabase;
        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private StageDatabase _stageDatabase;
        [SerializeField] private GachaPoolDatabase _gachaPoolDatabase;

        private UserSaveData _userData;
        private bool _isInitialized;

        /// <summary>
        /// 초기화 완료 여부
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 유저 데이터 변경 이벤트
        /// </summary>
        public event Action OnUserDataChanged;

        #region Master Data Access (읽기 전용)

        /// <summary>
        /// 캐릭터 마스터 데이터베이스
        /// </summary>
        public CharacterDatabase Characters => _characterDatabase;

        /// <summary>
        /// 스킬 마스터 데이터베이스
        /// </summary>
        public SkillDatabase Skills => _skillDatabase;

        /// <summary>
        /// 아이템 마스터 데이터베이스
        /// </summary>
        public ItemDatabase Items => _itemDatabase;

        /// <summary>
        /// 스테이지 마스터 데이터베이스
        /// </summary>
        public StageDatabase Stages => _stageDatabase;

        /// <summary>
        /// 가챠 풀 마스터 데이터베이스
        /// </summary>
        public GachaPoolDatabase GachaPools => _gachaPoolDatabase;

        #endregion

        #region User Data Access (읽기 전용 뷰)

        /// <summary>
        /// 유저 프로필 (읽기 전용)
        /// </summary>
        public UserProfile Profile => _userData.Profile;

        /// <summary>
        /// 유저 재화 (읽기 전용)
        /// </summary>
        public UserCurrency Currency => _userData.Currency;

        /// <summary>
        /// 보유 캐릭터 목록 (읽기 전용)
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<OwnedCharacter> OwnedCharacters => _userData.Characters;

        /// <summary>
        /// 보유 아이템 목록 (읽기 전용)
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<OwnedItem> OwnedItems => _userData.Items;

        /// <summary>
        /// 스테이지 진행 (읽기 전용)
        /// </summary>
        public StageProgress StageProgress => _userData.StageProgress;

        /// <summary>
        /// 가챠 천장 데이터 (읽기 전용)
        /// </summary>
        public GachaPityData GachaPity => _userData.GachaPity;

        /// <summary>
        /// 퀘스트 진행 (읽기 전용)
        /// </summary>
        public QuestProgress QuestProgress => _userData.QuestProgress;

        #endregion

        #region Initialization

        /// <summary>
        /// DataManager 초기화 (마스터 데이터 검증)
        /// </summary>
        public bool Initialize()
        {
            if (_isInitialized)
            {
                Debug.LogWarning("[DataManager] 이미 초기화됨");
                return true;
            }

            // 마스터 데이터 검증
            if (!ValidateMasterData())
            {
                Debug.LogError("[DataManager] 마스터 데이터 검증 실패");
                return false;
            }

            // 빈 유저 데이터로 초기화 (로그인 전까지)
            _userData = new UserSaveData();

            _isInitialized = true;
            Debug.Log("[DataManager] 초기화 완료");
            return true;
        }

        #endregion

        #region User Data Management

        /// <summary>
        /// 유저 데이터 직접 설정 (로그인 시 LoginResponseHandler에서 호출)
        /// </summary>
        public void SetUserData(UserSaveData userData)
        {
            _userData = userData;
            OnUserDataChanged?.Invoke();
            Debug.Log($"[DataManager] 유저 데이터 설정: {_userData.Profile.Nickname}");
        }

        /// <summary>
        /// 서버 응답 Delta 적용 (유저 데이터 갱신)
        /// Handler에서 호출
        /// </summary>
        public void ApplyDelta(UserDataDelta delta)
        {
            if (delta == null || !delta.HasChanges)
                return;

            // 프로필 갱신
            if (delta.Profile.HasValue)
            {
                _userData.Profile = delta.Profile.Value;
            }

            // 재화 갱신
            if (delta.Currency.HasValue)
            {
                _userData.Currency = delta.Currency.Value;
            }

            // 캐릭터 추가
            if (delta.AddedCharacters != null)
            {
                foreach (var character in delta.AddedCharacters)
                {
                    // 중복 체크 후 추가
                    var existing = _userData.Characters.FindIndex(c => c.InstanceId == character.InstanceId);
                    if (existing >= 0)
                    {
                        _userData.Characters[existing] = character;
                    }
                    else
                    {
                        _userData.Characters.Add(character);
                    }
                }
            }

            // 캐릭터 삭제
            if (delta.RemovedCharacterIds != null)
            {
                foreach (var id in delta.RemovedCharacterIds)
                {
                    _userData.Characters.RemoveAll(c => c.InstanceId == id);
                }
            }

            // 아이템 추가
            if (delta.AddedItems != null)
            {
                foreach (var item in delta.AddedItems)
                {
                    if (item.IsEquipment)
                    {
                        var existing = _userData.Items.FindIndex(i => i.InstanceId == item.InstanceId);
                        if (existing >= 0)
                        {
                            _userData.Items[existing] = item;
                        }
                        else
                        {
                            _userData.Items.Add(item);
                        }
                    }
                    else
                    {
                        // 소모품: 기존 아이템 수량 갱신
                        var existing = _userData.Items.FindIndex(i => i.ItemId == item.ItemId && !i.IsEquipment);
                        if (existing >= 0)
                        {
                            var existingItem = _userData.Items[existing];
                            existingItem.Count = item.Count;
                            _userData.Items[existing] = existingItem;
                        }
                        else
                        {
                            _userData.Items.Add(item);
                        }
                    }
                }
            }

            // 아이템 삭제
            if (delta.RemovedItemIds != null)
            {
                foreach (var id in delta.RemovedItemIds)
                {
                    _userData.Items.RemoveAll(i => i.InstanceId == id || i.ItemId == id);
                }
            }

            // 스테이지 진행 갱신
            if (delta.StageProgress.HasValue)
            {
                _userData.StageProgress = delta.StageProgress.Value;
            }

            // 가챠 천장 갱신
            if (delta.GachaPity.HasValue)
            {
                _userData.GachaPity = delta.GachaPity.Value;
            }

            // 퀘스트 진행 갱신
            if (delta.QuestProgress.HasValue)
            {
                _userData.QuestProgress = delta.QuestProgress.Value;
            }

            _userData.LastSyncAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            OnUserDataChanged?.Invoke();
            Debug.Log("[DataManager] Delta 적용 완료");
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// 캐릭터 보유 여부 확인
        /// </summary>
        public bool HasCharacter(string characterId)
        {
            return _userData.HasCharacter(characterId);
        }

        /// <summary>
        /// 아이템 보유 수량 확인
        /// </summary>
        public int GetItemCount(string itemId)
        {
            return _userData.GetItemCount(itemId);
        }

        /// <summary>
        /// 보유 캐릭터의 마스터 데이터 조회
        /// </summary>
        public CharacterData GetCharacterMasterData(OwnedCharacter owned)
        {
            return _characterDatabase?.GetById(owned.CharacterId);
        }

        /// <summary>
        /// 보유 아이템의 마스터 데이터 조회
        /// </summary>
        public ItemData GetItemMasterData(OwnedItem owned)
        {
            return _itemDatabase?.GetById(owned.ItemId);
        }

        private bool ValidateMasterData()
        {
            if (_characterDatabase == null)
            {
                Debug.LogWarning("[DataManager] CharacterDatabase가 설정되지 않음");
                return false;
            }
            if (_skillDatabase == null)
            {
                Debug.LogWarning("[DataManager] SkillDatabase가 설정되지 않음");
                return false;
            }
            if (_itemDatabase == null)
            {
                Debug.LogWarning("[DataManager] ItemDatabase가 설정되지 않음");
                return false;
            }
            if (_stageDatabase == null)
            {
                Debug.LogWarning("[DataManager] StageDatabase가 설정되지 않음");
                return false;
            }
            if (_gachaPoolDatabase == null)
            {
                Debug.LogWarning("[DataManager] GachaPoolDatabase가 설정되지 않음");
                return false;
            }
            return true;
        }

        #endregion

        protected override void OnSingletonDestroy()
        {
            _isInitialized = false;
        }
    }
}
