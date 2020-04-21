using System;

namespace Casshan.Logging
{
    internal sealed class TimestampLogger : ILog
    {
        private readonly ILog m_UnderlyingLog;
        private readonly DateTimeKind m_DateTimeKind;

        public TimestampLogger(ILog underlyingLog,
                               DateTimeKind dateTimeKind)
        {
            m_UnderlyingLog = underlyingLog;
            m_DateTimeKind = dateTimeKind;
            if (dateTimeKind == DateTimeKind.Unspecified)
                throw new ArgumentException("You must specify either UTC or local for timestamp logging");
        }

        public void Log(string message, LogLevel level)
        {
            var now = m_DateTimeKind == DateTimeKind.Utc
                ? DateTime.UtcNow
                : DateTime.Now;

            m_UnderlyingLog.Log($"[{now.ToLongTimeString()}] {message}", level);
        }
    }
}
