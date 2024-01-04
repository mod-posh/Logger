using NUnit.Framework;
using ModPosh.Logger;
using System.IO;

namespace ModPosh.Logger.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        private string tempPath = Path.GetTempPath();
        private string logFileName = "testlog.log";
        private StringWriter consoleOutput = new StringWriter();

        [SetUp]
        public void Setup()
        {
            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }
        [TearDown]
        public void TearDown()
        {
            string logFilePath = Path.Combine(tempPath, logFileName);
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }
            consoleOutput.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }
        [Test]
        public void BasicTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }
        [Test]
        public void LogToFile_WritesToLogFile()
        {
            // Arrange
            string logFilePath = Path.Combine(tempPath, logFileName);
            var logger = new Logger(true, logFilePath);

            // Act
            logger.LogInformation("Test log message");

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True);
            string logContents = File.ReadAllText(logFilePath);
            Assert.That(logContents.Contains("Test log message"), Is.True);
        }
        [Test]
        public void LogToFile_WritesToConsole()
        {
            // Arrange
            var logger = new Logger();

            // Act
            string expectedMessage = "Test log message";
            logger.LogInformation(expectedMessage);

            // Assert
            string actualOutput = consoleOutput.ToString().Trim();
            Assert.That(actualOutput.Contains(expectedMessage), Is.True);
        }
    }
}
