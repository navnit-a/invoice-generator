using Newtonsoft.Json.Linq;

namespace CXmlInvoiceGenerator.AppConfig
{
    public class Config : IConfig
    {
        public string FromIdentity { get; }
        public string ToIdentity { get; }
        public string SenderIdentity { get; }
        public string SharedSecret { get; }
        public string UserAgent { get; }

        public Config()
        {
            var configJson = JObject.Parse(File.ReadAllText("config.json"));

            FromIdentity = configJson["FromIdentity"]!.ToString();
            ToIdentity = configJson["ToIdentity"]!.ToString();
            SenderIdentity = configJson["SenderIdentity"]!.ToString();
            SharedSecret = configJson["SharedSecret"]!.ToString();
            UserAgent = configJson["UserAgent"]!.ToString();
        }
    }
}
