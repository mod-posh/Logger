#### [Logger](index.md 'index')
### [ModPosh.Logger.Implementations](ModPosh.Logger.Implementations.md 'ModPosh.Logger.Implementations')

## Logger Class

Implements the ILogger interface for logging messages to console and/or file.

```csharp
public class Logger :
ModPosh.Logger.Interfaces.ILogger
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Logger

Implements [ILogger](ModPosh.Logger.Interfaces.ILogger.md 'ModPosh.Logger.Interfaces.ILogger')

| Constructors | |
| :--- | :--- |
| [Logger()](ModPosh.Logger.Implementations.Logger.Logger().md 'ModPosh.Logger.Implementations.Logger.Logger()') | Initializes a new instance of the Logger class using default LoggerConfig. |
| [Logger(IConfigurationReader, string)](ModPosh.Logger.Implementations.Logger.Logger(ModPosh.Logger.Interfaces.IConfigurationReader,string).md 'ModPosh.Logger.Implementations.Logger.Logger(ModPosh.Logger.Interfaces.IConfigurationReader, string)') | Initializes a new instance of the Logger class using an IConfigurationReader and a path to the configuration file.<br/>This constructor allows for flexible configuration of the logger by reading settings from the specified configuration file. |
| [Logger(string)](ModPosh.Logger.Implementations.Logger.Logger(string).md 'ModPosh.Logger.Implementations.Logger.Logger(string)') | Initializes a new instance of the Logger class with a specified log file path.<br/>This constructor sets the logger to log messages to a file at the specified path. |

| Methods | |
| :--- | :--- |
| [CheckAndRotateLogFile()](ModPosh.Logger.Implementations.Logger.CheckAndRotateLogFile().md 'ModPosh.Logger.Implementations.Logger.CheckAndRotateLogFile()') | Checks and rotates the log file if it exceeds the maximum size. |
| [Log(string, string)](ModPosh.Logger.Implementations.Logger.Log(string,string).md 'ModPosh.Logger.Implementations.Logger.Log(string, string)') | Logs a message with the specified message type. |
| [LogError(string)](ModPosh.Logger.Implementations.Logger.LogError(string).md 'ModPosh.Logger.Implementations.Logger.LogError(string)') | Logs an error message. |
| [LogInformation(string)](ModPosh.Logger.Implementations.Logger.LogInformation(string).md 'ModPosh.Logger.Implementations.Logger.LogInformation(string)') | Logs an informational message. |
| [LogWarning(string)](ModPosh.Logger.Implementations.Logger.LogWarning(string).md 'ModPosh.Logger.Implementations.Logger.LogWarning(string)') | Logs a warning message. |
