using UnityEngine;

namespace Sc.Foundation
{
    /// <summary>
    /// Unity 콘솔에 로그를 출력하는 구현체
    /// </summary>
    public class UnityLogOutput : ILogOutput
    {
        /// <inheritdoc/>
        public void Write(LogLevel level, LogCategory category, string message)
        {
            var formattedMessage = $"[{category}] {message}";

            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    Debug.Log(formattedMessage);
                    break;

                case LogLevel.Warning:
                    Debug.LogWarning(formattedMessage);
                    break;

                case LogLevel.Error:
                    Debug.LogError(formattedMessage);
                    break;
            }
        }
    }
}
