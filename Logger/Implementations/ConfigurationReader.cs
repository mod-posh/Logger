using System.Text.Json;
using ModPosh.Logger.Interfaces;
using ModPosh.Logger.Models;

namespace ModPosh.Logger.Implementations
{
    /// <summary>
    /// Provides functionality to read configuration settings for the Logger.
    /// </summary>
    public class ConfigurationReader : IConfigurationReader
    {
        /// <summary>
        /// Reads the logger configuration from a specified JSON file.
        /// </summary>
        /// <param name="path">The file path of the JSON configuration file.</param>
        /// <returns>A <see cref="LoggerConfig"/> object populated with settings from the file.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the specified file is not found.</exception>
        /// <exception cref="JsonException">Thrown when the file content is not a valid JSON or cannot be deserialized into a LoggerConfig object.</exception>
        /// <exception cref="Exception">Throws a general exception with a message indicating the error encountered during the reading process.</exception>
        /// <remarks>
        /// If the file is not found or if the JSON content is invalid, this method returns a default LoggerConfig object with logging enabled to console only.
        /// </remarks>
        public LoggerConfig ReadConfiguration(string path)
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
                // Optional: Handle specific actions when the file is not found, if necessary.
            }
            catch (JsonException)
            {
                // Optional: Handle specific actions when JSON deserialization fails, if necessary.
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading configuration: {ex.Message}");
            }
            // Returning default configuration if the file is not found or if deserialization fails
            return new LoggerConfig
            {
                LogToFile = false,
                LogToConsole = true,
                LogFilePath = string.Empty
            };
        }
    }
}