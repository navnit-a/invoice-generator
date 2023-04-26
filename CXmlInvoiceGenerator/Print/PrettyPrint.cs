using Spectre.Console;

namespace CXmlInvoiceGenerator.Print;

public class PrettyPrint : IPrettyPrint
{
    public Table CreateTableWithColumns(IEnumerable<string> columnNames)
    {
        var table = new Table();
        foreach (var columnName in columnNames)
        {
            table.AddColumn(columnName);
        }
        return table;
    }

    public void RenderTable(Table tableToRender, string title)
    {
        AnsiConsole.MarkupLine($"[bold yellow]{title}[/]");
        AnsiConsole.Write(tableToRender);
    }

    public void PrintText(string text, string colour)
    {
        AnsiConsole.MarkupLine($"[bold {colour}]{text}[/]");
    }
}