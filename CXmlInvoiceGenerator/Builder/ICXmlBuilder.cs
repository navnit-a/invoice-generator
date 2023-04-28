using CXmlInvoiceGenerator.Models;
using System.Xml.Linq;

namespace CXmlInvoiceGenerator.Builder;

public interface ICXmlBuilder
{
    ICXmlBuilder AddHeader(string fromIdentity, string toIdentity, string senderIdentity, string sharedSecret, string userAgent);
    ICXmlBuilder AddInvoiceDetailRequest(
        int invoiceId, 
        string purpose, 
        string operation, 
        string invoiceDate,
        int paymentTermsDays,
        BillingAddress billingAddress,
        DeliveryAddress deliveryAddress);
    ICXmlBuilder AddInvoiceDetailOrder(NewInvoice newInvoice, List<InvoiceItem> itemsOnInvoice);
    ICXmlBuilder AddInvoiceDetailSummary(NewInvoice newInvoice);
    XDocument Build();
}