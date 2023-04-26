using DatabaseAccess;

namespace CXmlInvoiceGenerator.Factory
{
    public class InvoiceDbFactory : IInvoiceDbFactory
    {
        public Invoices InvoicesDb { get; }

        public InvoiceDbFactory()
        {
            InvoicesDb = new Invoices();
        }
    }
}
