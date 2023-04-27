using CXmlInvoiceGenerator.Print;

namespace CXmlInvoiceGenerator.Menu;

public class MenuHandler : IMenuHandler
{
    private readonly IPrettyPrint _prettyPrint;

    public MenuHandler(IPrettyPrint prettyPrint)
    {
        _prettyPrint = prettyPrint;
    }
    public int GetMenuChoice()
    {
        if (int.TryParse(Console.ReadLine(), out var choice))
        {
            return choice;
        }
        return -1;
    }

    public void ShowMenu()
    {
        _prettyPrint.PrintText("Select an option:", "purple");
        _prettyPrint.PrintText("1. Print DB Tables", "blue");
        _prettyPrint.PrintText("2. Generate Cxml For New Invoices", "blue");
        Console.Write("Enter the number of your choice: ");
    }
}