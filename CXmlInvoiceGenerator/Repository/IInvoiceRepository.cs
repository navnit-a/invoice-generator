using CXmlInvoiceGenerator.Models;

namespace CXmlInvoiceGenerator.Repository;

public interface IInvoiceRepository
{
    List<NewInvoice> GetNewInvoices();
    List<InvoiceItem> GetItemsOnInvoice(int invoiceId);
    BillingAddress GetBillingAddressForInvoice(int invoiceId);
    DeliveryAddress GetDeliveryAddressForSalesOrder(int salesOrderId);
}