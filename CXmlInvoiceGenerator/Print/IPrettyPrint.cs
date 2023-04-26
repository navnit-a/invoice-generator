using Spectre.Console;

namespace CXmlInvoiceGenerator.Print;

public interface IPrettyPrint
{
    Table CreateTableWithColumns(IEnumerable<string> columnNames);
    void RenderTable(Table tableToRender, string title);
    void PrintText(string text, string colour);
}