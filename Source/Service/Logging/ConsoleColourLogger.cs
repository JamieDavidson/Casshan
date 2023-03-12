using System;

namespace Casshan.Logging
{
    internal sealed class ConsoleColourLogger : ILog
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
                case LogLevel.debug:
                case LogLevel.info:
                    return ConsoleColor.White;
                case LogLevel.warning:
                    return ConsoleColor.Yellow;
                case LogLevel.error:
                    return ConsoleColor.Red;
                case LogLevel.success:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }
}
