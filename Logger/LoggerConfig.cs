namespace ModPosh.Logger
{
    public class LoggerConfig
    {
        public bool LogToFile { get; set; }
        public bool LogToConsole { get; set; }
        public string LogFilePath { get; set; } = string.Empty;
    }
}
