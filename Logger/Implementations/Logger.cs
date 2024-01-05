using ModPosh.Logger.Interfaces;
using ModPosh.Logger.Utilities;

namespace ModPosh.Logger.Implementations
{
    /// <summary>
    /// Implements the ILogger interface for logging messages to console and/or file.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly string _logFilePath = string.Empty;
        private readonly bool _logToFile;
        private readonly bool _logToConsole;
        private readonly long _maxLogSize = 10 * 1024 * 1024;
        /// <summary>
        /// Initializes a new instance of the Logger class using default LoggerConfig.
        /// </summary>
        public Logger()
        {
            _logToFile = false;
            _logToConsole = true;
            _logFilePath = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the Logger class with a specified log file path.
        /// This constructor sets the logger to log messages to a file at the specified path.
        /// </summary>
        /// <param name="logPath">The file path where log messages will be written.</param>
        public Logger(string logPath)
        {
            _logToFile = true;
            _logToConsole = false;
            _logFilePath = logPath;
        }
        /// <summary>
        /// Initializes a new instance of the Logger class using an IConfigurationReader and a path to the configuration file.
        /// This constructor allows for flexible configuration of the logger by reading settings from the specified configuration file.
        /// </summary>
        /// <param name="configReader">The IConfigurationReader to read the Logger configuration.</param>
        /// <param name="Path">The path to the configuration file that contains settings for the logger.</param>
        public Logger(IConfigurationReader configReader, string Path)
        {
            var config = configReader.ReadConfiguration(Path);
            _logToFile = config.LogToFile;
            _logToConsole = config.LogToConsole;
            _logFilePath = config.LogFilePath;
        }
        /// <summary>
        /// Checks and rotates the log file if it exceeds the maximum size.
        /// </summary>
        private void CheckAndRotateLogFile()
        {
            if (File.Exists(_logFilePath) && new FileInfo(_logFilePath).Length > _maxLogSize)
            {
                string archivePath = $"{_logFilePath}.{DateTime.Now:yyyyMMddHHmmss}.bak";
                File.Move(_logFilePath, archivePath);
            }
        }
        /// <summary>
        /// Logs a message with the specified message type.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="messageType">The type of message (e.g., "INFO", "WARNING", "ERROR").</param>
        private void Log(string message, string messageType)
        {
            var formattedMessage = $"{DateTime.Now} [{messageType}]: {message}";

            try
            {
                CheckAndRotateLogFile();
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
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void LogInformation(string message)
        {
            Log(message, "INFO");
        }
        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message)
        {
            Log(message, "WARNING");
        }
        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            Log(message, "ERROR");
        }
    }
}