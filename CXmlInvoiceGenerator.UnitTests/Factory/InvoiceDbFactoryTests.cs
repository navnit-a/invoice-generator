using CXmlInvoiceGenerator.Factory;
using DatabaseAccess;

namespace CXmlInvoiceGenerator.UnitTests.Factory
{
    public class InvoiceDbFactoryTests
    {
        [Fact]
        public void Constructor_Initializes_InvoicesDb_Property()
        {
            // Act
            var invoiceDbFactory = new InvoiceDbFactory();

            // Assert
            Assert.NotNull(invoiceDbFactory.InvoicesDb);
            Assert.IsType<Invoices>(invoiceDbFactory.InvoicesDb);
        }
    }
}
