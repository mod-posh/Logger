using ModPosh.Logger.Models;

namespace ModPosh.Logger.Interfaces
{
    /// <summary>
    /// Defines a contract for a configuration reader that reads logger settings.
    /// </summary>
    public interface IConfigurationReader
    {
        /// <summary>
        /// Reads the logger configuration from the specified path and returns the configuration settings.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        /// <returns>A <see cref="LoggerConfig"/> object populated with settings from the configuration file.</returns>
        /// <remarks>
        /// Implementations of this method are responsible for handling file reading and deserialization 
        /// of the configuration file located at the specified path. The method should handle any exceptions 
        /// that occur during the reading process, such as file not found or JSON deserialization errors, 
        /// and should return a default configuration if necessary.
        /// </remarks>
        LoggerConfig ReadConfiguration(string path);
    }
}