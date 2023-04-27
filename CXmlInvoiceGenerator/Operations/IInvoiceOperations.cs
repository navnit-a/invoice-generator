namespace CXmlInvoiceGenerator.Operations;

public interface IInvoiceOperations
{
    void GenerateCXmlForNewInvoices();

    /// <summary>
    ///     This method was created to output the data to the console for debugging purposes.
    ///     It is equivalent to looking at the tables in the database via GUI.
    /// </summary>
    void PrintDbTables();
}