namespace CXmlInvoiceGenerator.AppConfig
{
    public interface IConfig
    {
        string FromIdentity { get; }
        string ToIdentity { get; }
        string SenderIdentity { get; }
        string SharedSecret { get; }
        string UserAgent { get; }
    }
}
