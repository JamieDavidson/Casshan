namespace Casshan.Logging.Extensions
{
    public static class LoggingExtensions
    {
        public static ILog WithTimeStampLogging(this ILog underlyingLog, DateTimeKind kind)
        {
            return new TimestampLogger(underlyingLog, kind);
        }

        public static ILog WithConsoleColours(this ILog underlyingLog)
        {
            return new ConsoleColourLogger(underlyingLog);
        }

        public static ILog WithLogLevelPrefixes(this ILog underlyingLog)
        {
            return new LevelPrefixLogger(underlyingLog);
        }
    }
}
