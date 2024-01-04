namespace ModPosh.Logger
{
    /// <summary>
    /// Represents configuration settings for the Logger.
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether logging to a file is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if logging to a file is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool LogToFile { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether logging to the console is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if logging to the console is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool LogToConsole { get; set; } = true;
        /// <summary>
        /// Gets or sets the file path for the log file.
        /// </summary>
        /// <value>
        /// The file path of the log file.
        /// </value>
        public string LogFilePath { get; set; } = string.Empty;
    }
}
