namespace ModPosh.Logger
{
    public class LoggerConfig
    {
        public bool LogToFile { get; set; } = false;    
        public bool LogToConsole { get; set; } = true;
        public string LogFilePath { get; set; } = string.Empty;
    }
}
