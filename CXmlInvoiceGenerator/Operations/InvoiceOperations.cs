using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;

namespace CXmlInvoiceGenerator.Operations;

public class InvoiceOperations : IInvoiceOperations
{
    private readonly IPrettyPrint _prettyPrint;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IDataPrinter _dataPrinter;
    private static string Now => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

    public InvoiceOperations(
        IPrettyPrint prettyPrint,
        IInvoiceRepository invoiceRepository,
        IDataPrinter dataPrinter)
    {
        _prettyPrint = prettyPrint;
        _invoiceRepository = invoiceRepository;
        _dataPrinter = dataPrinter;
    }

    public void GenerateCXmlForNewInvoices()
    {
        // TODO: Implement this method
        _prettyPrint.PrintText("New invoice generation run starting at " + Now, "cyan");
        _prettyPrint.PrintText("New invoice generation run ending at " + Now, "cyan");
    }

    /// <summary>
    ///     This method has been created to output the data to the console for debugging purposes.
    /// </summary>
    public void PrintDbTables()
    {
        _prettyPrint.PrintText("Printing db tables starting at " + Now, "green");

        _dataPrinter.PrintTable(_invoiceRepository.GetNewInvoices(), "New Invoices");

        int[] invoiceIds = { 768910, 768911, 768912 };
        foreach (var invoiceId in invoiceIds)
        {
            _dataPrinter.PrintTable(
                _invoiceRepository.GetItemsOnInvoice(invoiceId),
                $"Items on Invoice - {invoiceId}");
        }

        _dataPrinter.PrintDataRow(_invoiceRepository.GetBillingAddressForInvoice(768910),
            ColumnInfo.GetColumnAddressNames(), "Billing Address for Invoice - 768910");
        _dataPrinter.PrintDataRow(_invoiceRepository.GetDeliveryAddressForSalesOrder(891234),
            ColumnInfo.GetColumnAddressNames(), "Delivery Address for Sales Order - 891234");

        _prettyPrint.PrintText("Printing db tables finishing at " + Now, "green");
    }
}