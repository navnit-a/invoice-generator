using System.Text;

namespace CXmlInvoiceGenerator.Models
{
    public class BillingAddress
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ContactName: {ContactName}");
            sb.AppendLine($"AddrLine1: {AddrLine1}");
            sb.AppendLine($"AddrLine2: {AddrLine2}");
            sb.AppendLine($"AddrLine3: {AddrLine3}");
            sb.AppendLine($"AddrLine4: {AddrLine4}");
            sb.AppendLine($"AddrLine5: {AddrLine5}");
            sb.AppendLine($"AddrPostCode: {AddrPostCode}");
            sb.AppendLine($"CountryCode: {CountryCode}");
            sb.AppendLine($"Country: {Country}");

            return sb.ToString();
        }
    }

}
