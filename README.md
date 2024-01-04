# ModPosh.Logger

`ModPosh.Logger` is a versatile logging library for C# projects and PowerShell scripts/modules. It provides functionality for logging messages to the console or a file, supporting custom configuration and log rotation.

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

   ```csharp
   using ModPosh.Logger;

   var logger = new Logger();
   ```

   To use a specific configuration file:

   ```csharp
   var logger = new Logger(true, "path/to/config.json");
   ```

   To directly specify a log file path:

   ```csharp
   var logger = new Logger("path/to/logfile.log");
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
   $logger = New-Object ModPosh.Logger.Logger
   ```

   With a configuration file:

   ```powershell
   $logger = New-Object ModPosh.Logger.Logger($true, "path\to\config.json")
   ```

   With a direct log file path:

   ```powershell
   $logger = New-Object ModPosh.Logger.Logger("path\to\logfile.log")
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
