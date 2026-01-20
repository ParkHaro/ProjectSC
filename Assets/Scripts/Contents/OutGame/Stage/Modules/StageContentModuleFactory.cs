using System;
using System.Collections.Generic;
using Sc.Data;
using Sc.Foundation;

namespace Sc.Contents.Stage
{
    /// <summary>
    /// 컨텐츠 타입별 모듈 생성 팩토리.
    /// 새 컨텐츠 추가 시 RegisterModule로 등록.
    /// </summary>
    public static class StageContentModuleFactory
    {
        private static readonly Dictionary<InGameContentType, Func<IStageContentModule>> _registry = new();
        private static bool _isInitialized;

        /// <summary>
        /// 팩토리 초기화 (기본 모듈 등록)
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized) return;

            // 기본 모듈 등록
            RegisterModule(InGameContentType.MainStory, () => new MainStoryContentModule());
            RegisterModule(InGameContentType.HardMode, () => new MainStoryContentModule()); // MainStory와 유사
            RegisterModule(InGameContentType.GoldDungeon, () => new ElementDungeonContentModule());
            RegisterModule(InGameContentType.ExpDungeon, () => new ElementDungeonContentModule()); // Element와 유사
            RegisterModule(InGameContentType.SkillDungeon, () => new ElementDungeonContentModule()); // Element와 유사

            // TODO: 추후 구현
            // RegisterModule(InGameContentType.BossRaid, () => new BossRaidContentModule());
            // RegisterModule(InGameContentType.Tower, () => new TowerContentModule());
            // RegisterModule(InGameContentType.Event, () => new EventStageContentModule());

            _isInitialized = true;
            Log.Info("[StageContentModuleFactory] 초기화 완료", LogCategory.UI);
        }

        /// <summary>
        /// 컨텐츠 타입에 맞는 모듈 생성
        /// </summary>
        public static IStageContentModule Create(InGameContentType contentType)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            if (_registry.TryGetValue(contentType, out var factory))
            {
                var module = factory();
                Log.Debug($"[StageContentModuleFactory] 모듈 생성: {contentType} -> {module.GetType().Name}",
                    LogCategory.UI);
                return module;
            }

            Log.Info($"[StageContentModuleFactory] {contentType}용 모듈 없음, null 반환", LogCategory.UI);
            return null;
        }

        /// <summary>
        /// 모듈 등록
        /// </summary>
        public static void RegisterModule(InGameContentType contentType, Func<IStageContentModule> factory)
        {
            if (factory == null)
            {
                Log.Warning($"[StageContentModuleFactory] null factory 등록 시도: {contentType}", LogCategory.UI);
                return;
            }

            _registry[contentType] = factory;
            Log.Debug($"[StageContentModuleFactory] 모듈 등록: {contentType}", LogCategory.UI);
        }

        /// <summary>
        /// 모듈 등록 해제
        /// </summary>
        public static void UnregisterModule(InGameContentType contentType)
        {
            if (_registry.Remove(contentType))
            {
                Log.Debug($"[StageContentModuleFactory] 모듈 해제: {contentType}", LogCategory.UI);
            }
        }

        /// <summary>
        /// 특정 컨텐츠 타입용 모듈이 등록되어 있는지 확인
        /// </summary>
        public static bool HasModule(InGameContentType contentType)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            return _registry.ContainsKey(contentType);
        }

        /// <summary>
        /// 레지스트리 초기화 (테스트용)
        /// </summary>
        public static void Reset()
        {
            _registry.Clear();
            _isInitialized = false;
        }
    }
}