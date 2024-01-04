namespace ModPosh.Logger
{
    /// <summary>
    /// Provides utility functions for file operations.
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Checks if the provided path is a valid file path and not a directory, and if the directory part of the path exists.
        /// </summary>
        /// <param name="path">The file path to validate.</param>
        /// <returns>
        /// <c>true</c> if the path is a valid file path and its directory part exists; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method checks the existence of the directory part of the path and verifies that the path itself is not a directory.
        /// It returns <c>false</c> and logs an error message to the console if an exception occurs during the validation process.
        /// </remarks>
        public static bool IsValidFile(string path)
        {
            try
            {
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    throw new ArgumentException("The directory part of the path does not exist.");
                }
                if (Directory.Exists(path))
                {
                    throw new ArgumentException("The path is a directory, not a file.");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsValidFile: {ex.Message}");
                return false;
            }
        }
    }
}