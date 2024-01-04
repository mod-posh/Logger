using ModPosh.Logger.Interfaces;
using Moq;
using NUnit.Framework;

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
        public void Logger_WritesToLogFile()
        {
            // Arrange
            string logFilePath = Path.Combine(tempPath, logFileName);
            var logger = new Implementations.Logger(logFilePath);

            // Act
            logger.LogInformation("Test log message");

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True);
            string logContents = File.ReadAllText(logFilePath);
            Assert.That(logContents.Contains("Test log message"), Is.True);
        }
        [Test]
        public void Logger_WritesToConsole()
        {
            // Arrange
            var logger = new Implementations.Logger();

            // Act
            string expectedMessage = "Test log message";
            logger.LogInformation(expectedMessage);

            // Assert
            string actualOutput = consoleOutput.ToString().Trim();
            Assert.That(actualOutput.Contains(expectedMessage), Is.True);
        }
        [Test]
        public void Logger_UsesConfiguration_WritesToFile()
        {
            // Arrange
            string logFilePath = Path.Combine(tempPath, logFileName);
            var mockConfigReader = new Mock<IConfigurationReader>();
            var mockConfig = new Models.LoggerConfig
            {
                LogToFile = true,
                LogToConsole = false,
                LogFilePath = logFilePath
            };
            mockConfigReader.Setup(m => m.ReadConfiguration(It.IsAny<string>())).Returns(mockConfig);
            var logger = new Implementations.Logger(mockConfigReader.Object, "appsettings.json");

            // Act
            string expectedMessage = "Test log message";
            logger.LogInformation(expectedMessage);

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True);
            string logContents = File.ReadAllText(logFilePath);
            Assert.That(logContents.Contains("Test log message"), Is.True);

        }
        [Test]
        public void Logger_UsesConfiguration_WritesToConsole()
        {
            // Arrange
            var mockConfigReader = new Mock<IConfigurationReader>();
            var mockConfig = new Models.LoggerConfig
            {
                LogToFile = false,
                LogToConsole = true,
                LogFilePath = string.Empty
            };
            mockConfigReader.Setup(m => m.ReadConfiguration(It.IsAny<string>())).Returns(mockConfig);
            var logger = new Implementations.Logger(mockConfigReader.Object, "appsettings.json");

            // Act
            string expectedMessage = "Test log message";
            logger.LogInformation(expectedMessage);

            // Assert
            string actualOutput = consoleOutput.ToString().Trim();
            Assert.That(actualOutput.Contains(expectedMessage), Is.True);
        }
    }
}