using CXmlInvoiceGenerator.Models;
using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;

namespace CXmlInvoiceGenerator.Operations;

public class PrintInvoiceOperations : IPrintInvoiceOperations
{
    private readonly IPrettyPrint _prettyPrint;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IDataPrinter _dataPrinter;
    private static string Now => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

    public PrintInvoiceOperations(
        IPrettyPrint prettyPrint,
        IInvoiceRepository invoiceRepository,
        IDataPrinter dataPrinter)
    {
        _prettyPrint = prettyPrint;
        _invoiceRepository = invoiceRepository;
        _dataPrinter = dataPrinter;
    }

    public void PrintDbTables()
    {
        _prettyPrint.PrintText("Printing db tables starting at " + Now, "green");
        PrintNewInvoices();
        PrintItemsOnInvoices();
        PrintBillingAddressForInvoice(768910);
        PrintDeliveryAddressForSalesOrder(891234);
        _prettyPrint.PrintText("Printing db tables finishing at " + Now, "green");
    }

    private void PrintNewInvoices()
    {
        var newInvoices = _invoiceRepository.GetNewInvoices();
        _dataPrinter.PrintTable(newInvoices, "New Invoices");
    }

    private void PrintItemsOnInvoices()
    {
        int[] invoiceIds = { 768910, 768911, 768912 };
        foreach (var invoiceId in invoiceIds)
        {
            var invoiceItems = _invoiceRepository.GetItemsOnInvoice(invoiceId);
            _dataPrinter.PrintTable(invoiceItems, $"Items on Invoice - {invoiceId}");
        }
    }

    private void PrintBillingAddressForInvoice(int invoiceId)
    {
        var billingAddressForInvoice = _invoiceRepository.GetBillingAddressForInvoice(invoiceId);
        _dataPrinter.PrintTable(new List<BillingAddress> { billingAddressForInvoice}, $"Billing Address for Invoice - {invoiceId}");
        
    }

    private void PrintDeliveryAddressForSalesOrder(int salesOrderId)
    {
        var deliveryAddress = _invoiceRepository.GetDeliveryAddressForSalesOrder(salesOrderId);
        _dataPrinter.PrintTable(new List<DeliveryAddress> { deliveryAddress }, $"Delivery Address for Sales Order - {salesOrderId}");
    }
}