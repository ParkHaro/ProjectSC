using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sc.Foundation
{
    /// <summary>
    /// 로그 설정 ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "LogConfig", menuName = "SC/Config/Log Config")]
    public class LogConfig : ScriptableObject
    {
        [Header("기본 설정")]
        [Tooltip("기본 로그 레벨 (이 레벨 이상만 출력)")]
        [SerializeField] private LogLevel _defaultLevel = LogLevel.Debug;

        [Header("카테고리별 레벨 오버라이드")]
        [SerializeField] private List<CategoryLevelOverride> _categoryOverrides = new();

        /// <summary>
        /// 기본 로그 레벨
        /// </summary>
        public LogLevel DefaultLevel => _defaultLevel;

        /// <summary>
        /// 특정 카테고리의 최소 로그 레벨 반환
        /// </summary>
        public LogLevel GetMinLevel(LogCategory category)
        {
            foreach (var @override in _categoryOverrides)
            {
                if (@override.Category == category)
                {
                    return @override.MinLevel;
                }
            }

            return _defaultLevel;
        }

        /// <summary>
        /// 해당 레벨/카테고리 로그가 출력 가능한지 확인
        /// </summary>
        public bool IsEnabled(LogLevel level, LogCategory category)
        {
            var minLevel = GetMinLevel(category);
            return level >= minLevel && minLevel != LogLevel.None;
        }
    }

    /// <summary>
    /// 카테고리별 로그 레벨 오버라이드
    /// </summary>
    [Serializable]
    public struct CategoryLevelOverride
    {
        public LogCategory Category;
        public LogLevel MinLevel;
    }
}
