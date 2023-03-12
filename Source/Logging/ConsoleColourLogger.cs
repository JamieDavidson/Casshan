namespace Casshan.Logging
{
    public sealed class ConsoleColourLogger : ILog
    {
        private readonly ILog m_UnderlyingLog;

        public ConsoleColourLogger(ILog underlyingLog)
        {
            m_UnderlyingLog = underlyingLog
                ?? throw new ArgumentNullException(nameof(underlyingLog));
        }

        public void Log(string message, LogLevel level)
        {
            Console.ForegroundColor = GetColourForLogLevel(level);
            m_UnderlyingLog.Log(message, level);
            Console.ResetColor();
        }

        private static ConsoleColor GetColourForLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    return ConsoleColor.White;
                case LogLevel.Warning:
                    return ConsoleColor.Yellow;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Success:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }
}
