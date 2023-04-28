using System.Data;

namespace CXmlInvoiceGenerator.Print;

public interface IDataPrinter
{
    void PrintTable<T>(List<T> objectList, string tableTitle);
}