# Version 1.1.0

1. **Multi-Targeting Configuration**:
   - **What Changed**: The project file (`.csproj`) was updated to target both .NET 6.0 and .NET 7.0 by adding the `<TargetFrameworks>` element.
   - **Result**: The project now builds and runs on both .NET 6.0 and .NET 7.0, ensuring broader compatibility across different environments.

2. **Test Project Multi-Targeting**:
   - **What Changed**: The `Logger.Tests` project was similarly updated to target both .NET 6.0 and .NET 7.0.
   - **Result**: Tests are now executed against both frameworks, verifying that the library behaves correctly in both environments.

### **Resulting Benefits**

- **Increased Compatibility**: The project now supports both .NET 6.0 and .NET 7.0, making it accessible to a wider range of developers and applications.

## ENHANCEMENT

* issue-9: V1.1.0
* issue-8: Update Actions
* issue-7: Add support for .net6

