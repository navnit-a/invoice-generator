using CXmlInvoiceGenerator.AppConfig;
using CXmlInvoiceGenerator.Builder;
using CXmlInvoiceGenerator.Factory;
using CXmlInvoiceGenerator.FilesManager;
using CXmlInvoiceGenerator.Menu;
using CXmlInvoiceGenerator.Operations;
using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;

namespace CXmlInvoiceGenerator;

internal class Program
{
    // Info: These initialisations would be done via an IoC container in a real application
    private static readonly PrettyPrint PrettyPrint = new();
    private static readonly InvoiceDbFactory InvoiceDbFactory = new();
    private static readonly IMenuHandler MenuHandler = new MenuHandler(PrettyPrint);
    private static readonly IGenerateInvoiceOperations GenerateInvoiceOperations = new GenerateInvoiceOperations
    (
        PrettyPrint,
        new InvoiceRepository(InvoiceDbFactory),
        new CXmlBuilder(),
        new FilesOps(PrettyPrint), 
        new Config()
    );

    private static readonly IPrintInvoiceOperations PrintInvoiceOperations = new PrintInvoiceOperations
    (
        PrettyPrint,
        new InvoiceRepository(InvoiceDbFactory),
        new DataPrinter(PrettyPrint)
    );

    private static void Main(string[] args)
    {
        MenuHandler.ShowMenu();
        var choice = MenuHandler.GetMenuChoice();

        switch (choice)
        {
            case 1:
                PrintInvoiceOperations.PrintDbTables();
                break;
            case 2:
                GenerateInvoiceOperations.GenerateCXmlForNewInvoices();
                break;
            default:
                PrettyPrint.PrintText("Invalid option. Please try again.", "red");
                break;
        }
    }
}