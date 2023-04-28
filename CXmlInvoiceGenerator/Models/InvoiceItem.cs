namespace CXmlInvoiceGenerator.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int StockItemId { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
