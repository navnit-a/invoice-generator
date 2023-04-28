using System.Text;

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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"InvoiceId: {InvoiceId}");
            sb.AppendLine($"StockItemId: {StockItemId}");
            sb.AppendLine($"Manufacturer: {Manufacturer}");
            sb.AppendLine($"PartNo: {PartNo}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Qty: {Qty}");
            sb.AppendLine($"UnitPrice: {UnitPrice}");
            sb.AppendLine($"LineTotal: {LineTotal}");

            return sb.ToString();
        }
    }

}
