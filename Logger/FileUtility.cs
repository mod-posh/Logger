using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModPosh.Logger
{
    public static class FileUtility
    {
        public static bool IsValidFile(string path)
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
    }
}
