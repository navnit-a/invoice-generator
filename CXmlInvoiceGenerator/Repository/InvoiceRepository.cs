using System.Data;
using CXmlInvoiceGenerator.Factory;

namespace CXmlInvoiceGenerator.Repository;

/// <summary>
///     I've used a Repo to abstract the DB calls from the controller (here it's program.cs).
///     Controller -> Repo -> Factory -> DB
///     But it's still not testable because the Factory is still tightly coupled to the DB (Invoice instead of IInvoice).
/// </summary>
public class InvoiceRepository : IInvoiceRepository
{
    private readonly IInvoiceDbFactory _invoiceDbFactory;

    public InvoiceRepository(IInvoiceDbFactory invoiceDbFactory)
    {
        _invoiceDbFactory = invoiceDbFactory;
    }

    public DataTable GetNewInvoices()
    {
        return _invoiceDbFactory.InvoicesDb.GetNewInvoices();
    }

    public DataTable GetItemsOnInvoice(int invoiceId)
    {
        return _invoiceDbFactory.InvoicesDb.GetItemsOnInvoice(invoiceId);
    }

    public DataRow GetBillingAddressForInvoice(int invoiceId)
    {
        return _invoiceDbFactory.InvoicesDb.GetBillingAddressForInvoice(invoiceId);
    }

    public DataRow GetDeliveryAddressForSalesOrder(int invoiceId)
    {
        return _invoiceDbFactory.InvoicesDb.GetDeliveryAddressForSalesOrder(invoiceId);
    }
}