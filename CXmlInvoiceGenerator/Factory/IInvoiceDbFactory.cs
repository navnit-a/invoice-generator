using DatabaseAccess;

namespace CXmlInvoiceGenerator.Factory;

public interface IInvoiceDbFactory
{
    Invoices InvoicesDb { get; } // Invoices is a class from DatabaseAccess and isn't an interface making it hard to test
}