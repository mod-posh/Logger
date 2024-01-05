#### [Logger](index.md 'index')
### [ModPosh.Logger.Utilities](ModPosh.Logger.Utilities.md 'ModPosh.Logger.Utilities').[FileUtility](ModPosh.Logger.Utilities.FileUtility.md 'ModPosh.Logger.Utilities.FileUtility')

## FileUtility.IsValidFile(string) Method

Checks if the provided path is a valid file path and not a directory, and if the directory part of the path exists.

```csharp
public static bool IsValidFile(string path);
```
#### Parameters

<a name='ModPosh.Logger.Utilities.FileUtility.IsValidFile(string).path'></a>

`path` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The file path to validate.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
`true` if the path is a valid file path and its directory part exists; otherwise, `false`.

### Remarks
This method checks the existence of the directory part of the path and verifies that the path itself is not a directory.  
It returns `false` and logs an error message to the console if an exception occurs during the validation process.