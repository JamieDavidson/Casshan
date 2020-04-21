using System;

namespace Casshan.Logging
{
    internal sealed class LevelPrefixLogger : ILog
    {
        private readonly ILog m_UnderlyingLog;

        public LevelPrefixLogger(ILog underlyingLog)
        {
            m_UnderlyingLog = underlyingLog
                ?? throw new ArgumentNullException(nameof(underlyingLog));
        }

        public void Log(string message, LogLevel level)
        {
            m_UnderlyingLog.Log($"{GetPrefixForLogLevel(level)} {message}", level);
        }

        private static string GetPrefixForLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.debug:
                    return "[DEBUG]";
                case LogLevel.info:
                    return "[INFO]";
                case LogLevel.warning:
                    return "[WARN]";
                case LogLevel.success:
                    return "[SUCCESS]";
                case LogLevel.error:
                    return "[ERROR]";
                default:
                    return "[SHIET]";
            }
        }
    }
}
