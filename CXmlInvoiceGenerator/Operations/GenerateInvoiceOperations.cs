using CXmlInvoiceGenerator.AppConfig;
using CXmlInvoiceGenerator.Builder;
using CXmlInvoiceGenerator.FilesManager;
using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;
using Spectre.Console;

namespace CXmlInvoiceGenerator.Operations;

public class GenerateInvoiceOperations : IGenerateInvoiceOperations
{
    private readonly IPrettyPrint _prettyPrint;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICXmlBuilder _xmlBuilder;
    private readonly IFilesOps _fileOps;
    private readonly IConfig _config;
    private static string Now => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

    public GenerateInvoiceOperations(
        IPrettyPrint prettyPrint,
        IInvoiceRepository invoiceRepository,
        ICXmlBuilder xmlBuilder,
        IFilesOps fileOps,
        IConfig config)
    {
        _prettyPrint = prettyPrint;
        _invoiceRepository = invoiceRepository;
        _xmlBuilder = xmlBuilder;
        _fileOps = fileOps;
        _config = config;
    }

    public void GenerateCXmlForNewInvoices()
    {
        AnsiConsole.Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Star)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Thinking...", ctx =>
            {
                _prettyPrint.PrintText("New invoice generation run started at " + Now, "Green");
                ctx.Status("Generating Invoices...");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                GenerateInvoices();
                _prettyPrint.PrintText("New invoice generation run ended at " + Now, "Green");
            });
    }

    private void GenerateInvoices()
    {
        _prettyPrint.PrintText("Generation in progress...", "Yellow");

        var newInvoices = _invoiceRepository.GetNewInvoices();
        foreach (var newInvoice in newInvoices) // Iterating over all invoices assuming they are all today's
        {
            // Get Items on an Invoice
            var itemsOnInvoice = _invoiceRepository.GetItemsOnInvoice(newInvoice.Id);

            // Get Billing Address
            var billingAddress = _invoiceRepository.GetBillingAddressForInvoice(newInvoice.Id);

            // Get Delivery Address
            var deliveryAddress = _invoiceRepository.GetDeliveryAddressForSalesOrder(newInvoice.SalesOrderId);

            _xmlBuilder
                .AddHeader(
                    _config.FromIdentity,
                    _config.ToIdentity, 
                    _config.SenderIdentity, 
                    _config.SharedSecret,
                    _config.UserAgent)
                .AddInvoiceDetailRequest(
                    newInvoice.Id, 
                    "standard", 
                    "new", 
                    DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"), 
                    newInvoice.PaymentTermsDays, 
                    billingAddress,
                    deliveryAddress)
                .AddInvoiceDetailOrder(newInvoice, itemsOnInvoice)
                .AddInvoiceDetailSummary(newInvoice);

            _fileOps.SaveXmlToFile(_xmlBuilder.Build(), "Output", newInvoice.Id);
        }
    }
}