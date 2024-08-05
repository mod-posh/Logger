#### [Logger](index.md 'index')

### [ModPosh.Logger.Factory](ModPosh.Logger.Factory.md 'ModPosh.Logger.Factory').[LoggerFactory](ModPosh.Logger.Factory.LoggerFactory.md 'ModPosh.Logger.Factory.LoggerFactory')

## LoggerFactory.CreateFileLogger(string) Method

Creates a Logger instance that logs messages to a specified file.

```csharp
public static ModPosh.Logger.Implementations.Logger CreateFileLogger(string Path);
```

#### Parameters

<a name='ModPosh.Logger.Factory.LoggerFactory.CreateFileLogger(string).Path'></a>

`Path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The file path where log messages will be written.

#### Returns

[Logger](ModPosh.Logger.Implementations.Logger.md 'ModPosh.Logger.Implementations.Logger')
An instance of Logger configured to log messages to the specified file.

### Remarks

This method creates a Logger instance that writes log messages to the file located at the provided path.
