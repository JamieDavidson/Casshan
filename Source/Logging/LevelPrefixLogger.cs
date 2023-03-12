namespace Casshan.Logging
{
    public sealed class LevelPrefixLogger : ILog
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
                case LogLevel.Debug:
                    return "[DEBUG]";
                case LogLevel.Info:
                    return "[INFO]";
                case LogLevel.Warning:
                    return "[WARN]";
                case LogLevel.Success:
                    return "[SUCCESS]";
                case LogLevel.Error:
                    return "[ERROR]";
                default:
                    return "[SHIET]";
            }
        }
    }
}
