using System.Text;

namespace CXmlInvoiceGenerator.Models
{
    public class NewInvoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SalesOrderId { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public decimal NetAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public string VATCode { get; set; } = string.Empty;
        public int VATPercentage { get; set; }
        public int PaymentTermsDays { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"CustomerId: {CustomerId}");
            sb.AppendLine($"SalesOrderId: {SalesOrderId}");
            sb.AppendLine($"CurrencyCode: {CurrencyCode}");
            sb.AppendLine($"NetAmount: {NetAmount}");
            sb.AppendLine($"VATAmount: {VATAmount}");
            sb.AppendLine($"GrossAmount: {GrossAmount}");
            sb.AppendLine($"VATCode: {VATCode}");
            sb.AppendLine($"VATPercentage: {VATPercentage}");
            sb.AppendLine($"PaymentTermsDays: {PaymentTermsDays}");

            return sb.ToString();
        }

    }

}
