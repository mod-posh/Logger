using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModPosh.Logger
{
    public class LoggerConfig
    {
        public bool LogToFile { get; set; }
        public bool LogToConsole { get; set; }
        public string LogFilePath { get; set; } = string.Empty;
    }
}
