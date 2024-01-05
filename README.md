| Latest Version | Nuget.org | Issues | License | Discord |
|-----------------|----------------|----------------|----------------|----------------|
| [![Latest Version](https://img.shields.io/github/v/tag/mod-posh/Logger)](https://github.com/mod-posh/Logger/tags) | [![Nuget.org](https://img.shields.io/nuget/dt/ModPosh.Logger)](https://www.nuget.org/packages/ModPosh.Logger) | [![GitHub issues](https://img.shields.io/github/issues/mod-posh/Logger)](https://github.com/mod-posh/Logger/issues) | [![GitHub license](https://img.shields.io/github/license/mod-posh/Logger)](https://github.com/mod-posh/Logger/blob/master/LICENSE) | [![Discord Server](https://assets-global.website-files.com/6257adef93867e50d84d30e2/636e0b5493894cf60b300587_full_logo_white_RGB.svg)](https://discord.com/channels/1044305359021555793/1044305781627035811) |
# ModPosh.Logger

`ModPosh.Logger` is a versatile logging library for C# projects and PowerShell scripts/modules. It provides functionality for logging messages to the console or a file, supporting custom configuration and log rotation.

## Code Documentation

Detailed documentation can be found in the [docs](docs\index.md) folder.

## Features

- Log messages to the console or file.
- Configure logging through a JSON configuration file or programmatically.
- Automatic log file rotation based on file size.

## Installation

### For C# Projects

1. **Add the Library**: Include the `ModPosh.Logger` library in your C# project. If you use a solution, you can compile it from the source, reference the DLL, or include the project directly.

### For PowerShell Scripts/Modules

1. **Load the Assembly: Ensure the compiled `ModPosh.Logger.dll`` is accessible to your PowerShell script or module. You will need to load this assembly in your scripts.

## Usage

### In C# Projects

1. **Initialization**: Create an instance of the `Logger` class.

   This method creates a Logger instance using configuration settings from a specified file path.

   ```csharp
   using ModPosh.Logger;

   // Assume you have a configuration file at the specified path
   string configFilePath = "path/to/config.json";

   // Create a Logger instance using the configuration file
   Logger loggerFromConfig = Factory.LoggerFactory.CreateLogger(configFilePath);

   // Use the logger
   loggerFromConfig.LogInformation("This is an informational message from the configured logger.");

   ```

   This method creates a Logger instance with default settings that log to the console.

   ```csharp
   // Create a Logger instance that logs to the console
   Logger consoleLogger = Factory.LoggerFactory.CreateConsoleLogger();

   // Use the logger
   consoleLogger.LogInformation("This message will be logged to the console.");
   ```

   This method creates a Logger instance that logs messages to a specified file.

   ```csharp
   // Specify the path where log messages will be written
   string logFilePath = "path/to/logfile.log";

   // Create a Logger instance that logs to the specified file
   Logger fileLogger = Factory.LoggerFactory.CreateFileLogger(logFilePath);

   // Use the logger
   fileLogger.LogInformation("This message will be logged to the file.");
   ```

2. **Logging Messages**:

   ```csharp
   logger.LogInformation("This is an informational message.");
   logger.LogWarning("This is a warning message.");
   logger.LogError("This is an error message.");
   ```

### In PowerShell Scripts/Modules

1. **Load the Assembly**:

   ```powershell
   Add-Type -Path "path\to\ModPosh.Logger.dll"
   ```

2. **Create a Logger Instance**:

   For default configuration:

   ```powershell
   $logger = New-Object ModPosh.Logger.Implementations.Logger
   ```

   With a direct log file path:

   ```powershell
   $logger = New-Object ModPosh.Logger.Implementations.Logger("path\to\logfile.log")
   ```

3. **Logging Messages**:

   ```powershell
   $logger.LogInformation("This is an informational message.")
   $logger.LogWarning("This is a warning message.")
   $logger.LogError("This is an error message.")
   ```

## Configuration

To configure logging, use the `appsettings.json` file with the following format:

```json
{
  "Logger": {
    "LogToFile": true,
    "LogToConsole": true,
    "LogFilePath": "path/to/logfile.log"
  }
}
```

## Additional Notes

- Ensure that the paths provided for log files are accessible and writable by the application or script.
- In PowerShell, the path to the DLL and configuration files should use the full path or a path relative to the current PowerShell session's location.
