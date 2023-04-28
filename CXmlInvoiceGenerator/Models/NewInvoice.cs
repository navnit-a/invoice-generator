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

    }
}
