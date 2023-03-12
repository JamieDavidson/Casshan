namespace Casshan.Logging
{
    public sealed class ConsoleLogger : ILog
    {
        public void Log(string message, LogLevel level)
        {
            Console.WriteLine(message);
        }
    }
}
