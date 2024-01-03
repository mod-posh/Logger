using System;
using System.IO;
using System.Text.Json;

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
        private readonly string _logFilePath = string.Empty;
        private readonly bool _logToFile;
        private readonly bool _logToConsole;
        public Logger()
        {
            _logToConsole = true;
        }
        private LoggerConfig ReadConfiguration()
        {
            try
            {
                string configText = File.ReadAllText("appsettings.json");
                var config = JsonSerializer.Deserialize<LoggerConfig>(configText);

                if (config != null)
                {
                    return config;
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (JsonException)
            {
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading configuration: {ex.Message}");
            }

            return new LoggerConfig
            {
                LogToFile = false,
                LogToConsole = true,
                LogFilePath = string.Empty
            };
        }
        public Logger(string logFilePath)
        {
            _logToFile = true;
            _logToConsole = false;
            _logFilePath = logFilePath;
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

                if (_logToFile && IsValidFile(_logFilePath))
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
        private bool IsValidFile(string path)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    throw new ArgumentException("The directory part of the path does not exist.");
                }
                if (Directory.Exists(path))
                {
                    throw new ArgumentException("The path is a directory, not a file.");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsValidFile: {ex.Message}");
                return false;
            }
        }
        private class LoggerConfig
        {
            public bool LogToFile { get; set; }
            public bool LogToConsole { get; set; }
            public string LogFilePath { get; set; } = string.Empty;
        }
    }
}