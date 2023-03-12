using System;

namespace Casshan.Logging
{
    internal sealed class ConsoleLogger : ILog
    {
        public void Log(string message, LogLevel level)
        {
            Console.WriteLine(message);
        }
    }
}
