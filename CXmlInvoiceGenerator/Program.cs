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

// NOTES ON DESIGN DECISION
// ------------------------
/*
 * As I could not see the data from the DLL, I had to make a way to see it first visually - hence why I used Spectre Console
 * There are 2 types of Operations we can perform with this program - Print and Generate Invoice - hence, PrintInvoiceOperations and GenerateInvoiceOperations
 * Most classes have their respective interfaces to achieve testablilty and dependency injection
 * I have not implemented a DI Container but harded coded all new instances in Program.cs - everywhere else, the dependencies are injected
 * Every class tend to do only one thing - SRP
 * I have also used a Repository pattern to convert the data tables and data rows into concrete objects
 * I have used the builder pattern to build the cXML from the e.g link that was provided
 * As there's a lot to code and configure, I did not have time to break the builder pattern even into smaller classes - and I could use composition to lay each elements on top of each other
 * I've also demonstrated that these classes could be Unit tested (Not the Repository class as the Invoice class does not have an interface)
 * I've created a FilesOps class which reads the config from a config.json file
 * I've used a Factory class to generate the instance of the database provided
 * I've added logs to the console to show what's been done at execution time and where the paths of the invoices generated
 */