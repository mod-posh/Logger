namespace ModPosh.Logger
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath = string.Empty;
        private readonly bool _logToFile;
        private readonly bool _logToConsole;
        public Logger()
        {
            var config = new LoggerConfig();
            _logToFile = config.LogToFile;
            _logToConsole = config.LogToConsole;
            _logFilePath = config.LogFilePath;
        }
        public Logger(bool FromConfig, string Path)
        {
            if (FromConfig == true)
            {
                var config = ConfigurationReader.ReadConfiguration(Path);
                _logToFile = config.LogToFile;
                _logToConsole = config.LogToConsole;
                _logFilePath = config.LogFilePath;
            }
            else
            {
                _logToFile = true;
                _logToConsole = false;
                _logFilePath = Path;
            }
        }
        private void Log(string message, string messageType)
        {
            var formattedMessage = $"{DateTime.Now} [{messageType}]: {message}";

            try
            {
                if (_logToConsole)
                {
                    Console.WriteLine(formattedMessage);
                }
                if (_logToFile && FileUtility.IsValidFile(_logFilePath))
                {
                    File.AppendAllText(_logFilePath, formattedMessage + Environment.NewLine);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error writing to log file: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access error writing to log file: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid argument error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error writing to log file: {ex.Message}");
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