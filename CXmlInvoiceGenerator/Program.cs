using CXmlInvoiceGenerator.Common;
using CXmlInvoiceGenerator.Factory;
using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;

namespace CXmlInvoiceGenerator;

internal class Program
{
    private static readonly PrettyPrint PrettyPrint = new();
    private static string Now => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

    private static void Main(string[] args)
    {
        PrettyPrint.PrintText("New invoice generation run starting at " + Now, "green");
        PrintAll();
        //GenerateCxmlForNewInvoices();
        PrettyPrint.PrintText("New invoice generation run finishing at " + Now, "green");
    }

    private static void GenerateCxmlForNewInvoices()
    {
        // TODO: Implement this method
    }

    /// <summary>
    ///     This method has been created to output the data to the console for debugging purposes.
    /// </summary>
    private static void PrintAll()
    {
        IInvoiceRepository invoiceRepository = new InvoiceRepository(new InvoiceDbFactory());
        IDataPrinter dataPrinter = new DataPrinter(PrettyPrint);

        dataPrinter.PrintTable(invoiceRepository.GetNewInvoices(), "New Invoices");

        int[] invoiceIds = { 768910, 768911, 768912 };
        foreach (var invoiceId in invoiceIds)
            dataPrinter.PrintTable(invoiceRepository.GetItemsOnInvoice(invoiceId), $"Items on Invoice - {invoiceId}");

        dataPrinter.PrintDataRow(invoiceRepository.GetBillingAddressForInvoice(768910),
            ColumnInfo.GetColumnAddressNames(), "Billing Address for Invoice - 768910");
        dataPrinter.PrintDataRow(invoiceRepository.GetDeliveryAddressForSalesOrder(891234),
            ColumnInfo.GetColumnAddressNames(), "Delivery Address for Sales Order - 891234");
    }
}