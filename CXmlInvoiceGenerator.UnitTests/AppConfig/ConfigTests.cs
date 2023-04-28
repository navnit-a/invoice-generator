using CXmlInvoiceGenerator.AppConfig;

namespace CXmlInvoiceGenerator.UnitTests.AppConfig
{
    public class ConfigTests
    {
        [Fact]
        public void Config_LoadsValuesFromJsonFile()
        {
            // Arrange
            var expectedFromIdentity = "123456789";
            var expectedToIdentity = "987654321";
            var expectedSenderIdentity = "123987654";
            var expectedSharedSecret = "sharedSecret";
            var expectedUserAgent = "UserAgent";

            // Create a temporary config.json file for testing
            File.WriteAllText("config.json", $@"{{
                ""FromIdentity"": ""{expectedFromIdentity}"",
                ""ToIdentity"": ""{expectedToIdentity}"",
                ""SenderIdentity"": ""{expectedSenderIdentity}"",
                ""SharedSecret"": ""{expectedSharedSecret}"",
                ""UserAgent"": ""{expectedUserAgent}""
            }}");

            // Act
            var config = new Config();

            // Assert
            Assert.Equal(expectedFromIdentity, config.FromIdentity);
            Assert.Equal(expectedToIdentity, config.ToIdentity);
            Assert.Equal(expectedSenderIdentity, config.SenderIdentity);
            Assert.Equal(expectedSharedSecret, config.SharedSecret);
            Assert.Equal(expectedUserAgent, config.UserAgent);

            // Clean up the temporary config.json file
            File.Delete("config.json");
        }
    }
}