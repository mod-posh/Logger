using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModPosh.Logger
{
    public interface IConfigurationReader
    {
        LoggerConfig ReadConfiguration(string path);
    }
}