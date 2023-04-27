using CXmlInvoiceGenerator.Factory;
using CXmlInvoiceGenerator.Menu;
using CXmlInvoiceGenerator.Operations;
using CXmlInvoiceGenerator.Print;
using CXmlInvoiceGenerator.Repository;

namespace CXmlInvoiceGenerator;

internal class Program
{
    // Info: These initialisations would be done via an IoC container in a real application
    private static readonly PrettyPrint PrettyPrint = new();
    private static readonly IMenuHandler MenuHandler = new MenuHandler(PrettyPrint);
    private static readonly InvoiceOperations InvoiceOperations = new
    (
        PrettyPrint,
        new InvoiceRepository(new InvoiceDbFactory()),
        new DataPrinter(PrettyPrint)
    ); 
    

    private static void Main(string[] args)
    {
        while (true)
        {
            MenuHandler.ShowMenu();
            var choice = MenuHandler.GetMenuChoice();

            switch (choice)
            {
                case 1:
                    InvoiceOperations.PrintDbTables();
                    break;
                case 2:
                    InvoiceOperations.GenerateCXmlForNewInvoices();
                    break;
                default:
                    PrettyPrint.PrintText("Invalid option. Please try again.", "red");
                    break;
            }
        }
    }
}