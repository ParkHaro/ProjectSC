using System.Collections.Generic;
using System.Diagnostics;

namespace Sc.Foundation
{
    /// <summary>
    /// 정적 로깅 유틸리티 (진입점)
    /// </summary>
    public static class Log
    {
        private static LogConfig _config;
        private static readonly List<ILogOutput> _outputs = new();
        private static bool _initialized;

        /// <summary>
        /// 로깅 시스템 초기화
        /// </summary>
        /// <param name="config">로그 설정 (null이면 기본 설정 사용)</param>
        public static void Initialize(LogConfig config = null)
        {
            _config = config;
            _outputs.Clear();
            _outputs.Add(new UnityLogOutput());
            _initialized = true;
        }

        /// <summary>
        /// 출력 대상 추가
        /// </summary>
        public static void AddOutput(ILogOutput output)
        {
            if (output != null && !_outputs.Contains(output))
            {
                _outputs.Add(output);
            }
        }

        /// <summary>
        /// 출력 대상 제거
        /// </summary>
        public static void RemoveOutput(ILogOutput output)
        {
            _outputs.Remove(output);
        }

        /// <summary>
        /// 해당 레벨/카테고리 로그가 출력 가능한지 확인
        /// </summary>
        public static bool IsEnabled(LogLevel level, LogCategory category = LogCategory.System)
        {
            if (_config == null)
            {
                return level >= LogLevel.Debug;
            }

            return _config.IsEnabled(level, category);
        }

        /// <summary>
        /// Debug 레벨 로그 (Editor/Development 빌드에서만 포함)
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Debug(string message, LogCategory category = LogCategory.System)
        {
            WriteInternal(LogLevel.Debug, category, message);
        }

        /// <summary>
        /// Info 레벨 로그 (Editor/Development 빌드에서만 포함)
        /// </summary>
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Info(string message, LogCategory category = LogCategory.System)
        {
            WriteInternal(LogLevel.Info, category, message);
        }

        /// <summary>
        /// Warning 레벨 로그 (항상 포함)
        /// </summary>
        public static void Warning(string message, LogCategory category = LogCategory.System)
        {
            WriteInternal(LogLevel.Warning, category, message);
        }

        /// <summary>
        /// Error 레벨 로그 (항상 포함)
        /// </summary>
        public static void Error(string message, LogCategory category = LogCategory.System)
        {
            WriteInternal(LogLevel.Error, category, message);
        }

        private static void WriteInternal(LogLevel level, LogCategory category, string message)
        {
            // 미초기화 시 기본 출력 사용
            if (!_initialized)
            {
                Initialize();
            }

            // 레벨 체크
            if (!IsEnabled(level, category))
            {
                return;
            }

            // 모든 출력 대상에 전달
            foreach (var output in _outputs)
            {
                output.Write(level, category, message);
            }
        }
    }
}
