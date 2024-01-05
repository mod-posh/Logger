#### [Logger](index.md 'index')
### [ModPosh.Logger.Implementations](ModPosh.Logger.Implementations.md 'ModPosh.Logger.Implementations').[ConfigurationReader](ModPosh.Logger.Implementations.ConfigurationReader.md 'ModPosh.Logger.Implementations.ConfigurationReader')

## ConfigurationReader.ReadConfiguration(string) Method

Reads the logger configuration from a specified JSON file.

```csharp
public ModPosh.Logger.Models.LoggerConfig ReadConfiguration(string path);
```
#### Parameters

<a name='ModPosh.Logger.Implementations.ConfigurationReader.ReadConfiguration(string).path'></a>

`path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The file path of the JSON configuration file.

Implements [ReadConfiguration(string)](ModPosh.Logger.Interfaces.IConfigurationReader.ReadConfiguration(string).md 'ModPosh.Logger.Interfaces.IConfigurationReader.ReadConfiguration(string)')

#### Returns
[LoggerConfig](ModPosh.Logger.Models.LoggerConfig.md 'ModPosh.Logger.Models.LoggerConfig')  
A [LoggerConfig](ModPosh.Logger.Models.LoggerConfig.md 'ModPosh.Logger.Models.LoggerConfig') object populated with settings from the file.

#### Exceptions

[System.IO.FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.IO.FileNotFoundException 'System.IO.FileNotFoundException')  
Thrown when the specified file is not found.

[System.Text.Json.JsonException](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonException 'System.Text.Json.JsonException')  
Thrown when the file content is not a valid JSON or cannot be deserialized into a LoggerConfig object.

[System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception')  
Throws a general exception with a message indicating the error encountered during the reading process.

### Remarks
If the file is not found or if the JSON content is invalid, this method returns a default LoggerConfig object with logging enabled to console only.