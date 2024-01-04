namespace ModPosh.Logger
{
    /// <summary>
    /// Provides factory methods for creating instances of the Logger class.
    /// </summary>
    public class LoggerFactory
    {
        /// <summary>
        /// Creates a Logger instance using configuration settings from a specified file path.
        /// </summary>
        /// <param name="configFilePath">The file path to the configuration file.</param>
        /// <returns>An instance of Logger configured based on the specified configuration file.</returns>
        /// <remarks>
        /// This method reads configuration settings from the provided file path and initializes a new Logger instance with these settings.
        /// </remarks>
        public static Logger CreateLogger(string configFilePath)
        {
            IConfigurationReader configReader = new ConfigurationReader();
            return new Logger(configReader, configFilePath);
        }
        /// <summary>
        /// Creates a Logger instance with default settings that logs to the console.
        /// </summary>
        /// <returns>An instance of Logger configured to log messages to the console.</returns>
        /// <remarks>
        /// This method creates a Logger instance with default settings where logging to the console is enabled.
        /// </remarks>
        public static Logger CreateConsoleLogger()
        {
            return new Logger();
        }
        /// <summary>
        /// Creates a Logger instance that logs messages to a specified file.
        /// </summary>
        /// <param name="path">The file path where log messages will be written.</param>
        /// <returns>An instance of Logger configured to log messages to the specified file.</returns>
        /// <remarks>
        /// This method creates a Logger instance that writes log messages to the file located at the provided path.
        /// </remarks>
        public static Logger CreateFileLogger(string Path)
        {
            return new Logger(Path);
        }
    }
}