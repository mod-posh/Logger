#### [Logger](index.md 'index')
### [ModPosh.Logger.Factory](ModPosh.Logger.Factory.md 'ModPosh.Logger.Factory').[LoggerFactory](ModPosh.Logger.Factory.LoggerFactory.md 'ModPosh.Logger.Factory.LoggerFactory')

## LoggerFactory.CreateLogger(string) Method

Creates a Logger instance using configuration settings from a specified file path.

```csharp
public static ModPosh.Logger.Implementations.Logger CreateLogger(string configFilePath);
```
#### Parameters

<a name='ModPosh.Logger.Factory.LoggerFactory.CreateLogger(string).configFilePath'></a>

`configFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The file path to the configuration file.

#### Returns
[Logger](ModPosh.Logger.Implementations.Logger.md 'ModPosh.Logger.Implementations.Logger')  
An instance of Logger configured based on the specified configuration file.

### Remarks
This method reads configuration settings from the provided file path and initializes a new Logger instance with these settings.