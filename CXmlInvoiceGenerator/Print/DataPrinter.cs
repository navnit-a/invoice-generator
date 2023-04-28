using Spectre.Console.Rendering;
using Spectre.Console;

namespace CXmlInvoiceGenerator.Print
{
    public class DataPrinter : IDataPrinter
    {
        private readonly IPrettyPrint _prettyPrint;

        public DataPrinter(IPrettyPrint prettyPrint)
        {
            _prettyPrint = prettyPrint;
        }

        public void PrintTable<T>(List<T> objectList, string tableTitle)
        {
            if (objectList == null || objectList.Count == 0)
            {
                Console.WriteLine("No data to display.");
                return;
            }

            // Get the properties of the object
            var properties = typeof(T).GetProperties();

            // Create a table with columns based on the property names
            var columnNames = properties.Select(property => property.Name);
            var table = _prettyPrint.CreateTableWithColumns(columnNames);

            // Add table rows
            foreach (var obj in objectList)
            {
                var cellValues = new List<IRenderable>();
                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    cellValues.Add(new Text(value?.ToString() ?? string.Empty));
                }

                table.AddRow(cellValues);
            }

            // Render the table to the console
            _prettyPrint.RenderTable(table, tableTitle);
        }

    }
}
