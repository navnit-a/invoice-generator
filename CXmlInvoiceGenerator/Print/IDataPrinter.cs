using System.Data;

namespace CXmlInvoiceGenerator.Print;

public interface IDataPrinter
{
    void PrintTable(DataTable dataTable, string tableTitle);
    void PrintDataRow(DataRow dataRow, IList<string> columnNames, string title);
}