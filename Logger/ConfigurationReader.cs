using System.Text.Json;

namespace ModPosh.Logger
{
    public static class ConfigurationReader
    {
        public static LoggerConfig ReadConfiguration(string path)
        {
            try
            {
                string configText = File.ReadAllText(path);
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
    }
}
