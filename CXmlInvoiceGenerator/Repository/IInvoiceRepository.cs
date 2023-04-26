using System.Data;

namespace CXmlInvoiceGenerator.Repository;

public interface IInvoiceRepository
{
    DataTable GetNewInvoices();
    DataTable GetItemsOnInvoice(int invoiceId);
    DataRow GetBillingAddressForInvoice(int invoiceId);
    DataRow GetDeliveryAddressForSalesOrder(int invoiceId);
}