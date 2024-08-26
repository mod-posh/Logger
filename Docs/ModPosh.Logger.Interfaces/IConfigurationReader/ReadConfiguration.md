# IConfigurationReader.ReadConfiguration method

Reads the logger configuration from the specified path and returns the configuration settings.

```csharp
public LoggerConfig ReadConfiguration(string path)
```

| parameter | description |
| --- | --- |
| path | The path to the configuration file. |

## Return Value

A [`LoggerConfig`](../../ModPosh.Logger.Models/LoggerConfig.md) object populated with settings from the configuration file.

## Remarks

Implementations of this method are responsible for handling file reading and deserialization of the configuration file located at the specified path. The method should handle any exceptions that occur during the reading process, such as file not found or JSON deserialization errors, and should return a default configuration if necessary.

## See Also

* class [LoggerConfig](../../ModPosh.Logger.Models/LoggerConfig.md)
* interface [IConfigurationReader](../IConfigurationReader.md)
* namespace [ModPosh.Logger.Interfaces](../IConfigurationReader.md.md)
* assembly [Logger](../../Logger.md)

<!-- DO NOT EDIT: generated by xmldocmd for Logger.dll -->