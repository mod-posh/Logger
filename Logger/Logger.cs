namespace ModPosh.Logger
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
    public class Logger : ILogger
    {
        private readonly string _logFilePath;
        private readonly bool _logToFile;
        private readonly bool _logToConsole;
        public Logger(bool logToConsole = true, bool logToFile = false, string logFilePath = "log.txt")
        {
            _logToConsole = logToConsole;
            _logToFile = logToFile;
            _logFilePath = logFilePath;
        }
        private void Log(string message, string messageType)
        {
            var formattedMessage = $"{DateTime.Now} [{messageType}]: {message}";

            if (_logToConsole)
            {
                Console.WriteLine(formattedMessage);
            }

            if (_logToFile)
            {
                File.AppendAllText(_logFilePath, formattedMessage + Environment.NewLine);
            }
        }
        public void LogInformation(string message)
        {
            Log(message, "INFO");
        }

        public void LogWarning(string message)
        {
            Log(message, "WARNING");
        }

        public void LogError(string message)
        {
            Log(message, "ERROR");
        }
    }
}