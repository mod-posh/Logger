#### [Logger](index.md 'index')
### [ModPosh.Logger.Implementations](ModPosh.Logger.Implementations.md 'ModPosh.Logger.Implementations').[Logger](ModPosh.Logger.Implementations.Logger.md 'ModPosh.Logger.Implementations.Logger')

## Logger(IConfigurationReader, string) Constructor

Initializes a new instance of the Logger class using an IConfigurationReader and a path to the configuration file.  
This constructor allows for flexible configuration of the logger by reading settings from the specified configuration file.

```csharp
public Logger(ModPosh.Logger.Interfaces.IConfigurationReader configReader, string Path);
```
#### Parameters

<a name='ModPosh.Logger.Implementations.Logger.Logger(ModPosh.Logger.Interfaces.IConfigurationReader,string).configReader'></a>

`configReader` [IConfigurationReader](ModPosh.Logger.Interfaces.IConfigurationReader.md 'ModPosh.Logger.Interfaces.IConfigurationReader')

The IConfigurationReader to read the Logger configuration.

<a name='ModPosh.Logger.Implementations.Logger.Logger(ModPosh.Logger.Interfaces.IConfigurationReader,string).Path'></a>

`Path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')