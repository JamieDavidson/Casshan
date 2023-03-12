namespace Casshan.Logging
{
    public interface ILog
    {
        void Log(string message, LogLevel level);
    }
}
