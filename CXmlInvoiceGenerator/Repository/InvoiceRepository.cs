using CXmlInvoiceGenerator.Factory;
using System.Data;

namespace CXmlInvoiceGenerator.Repository
{
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
}
