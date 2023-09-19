namespace TiendaA01.CrossCuting.Logging
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        //void LogWarn(string message);
        //void LogDebug(string message);
        void LogError(string message);
    }
}
