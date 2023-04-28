namespace CXmlInvoiceGenerator.Models
{
    public class DeliveryAddress
    {
        public string ContactName { get; set; } = string.Empty;
        public string AddrLine1 { get; set; } = string.Empty;
        public string AddrLine2 { get; set; } = string.Empty;
        public string AddrLine3 { get; set; } = string.Empty;
        public string AddrLine4 { get; set; } = string.Empty;
        public string AddrLine5 { get; set; } = string.Empty;
        public string AddrPostCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
