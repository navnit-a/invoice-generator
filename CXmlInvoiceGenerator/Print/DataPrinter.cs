using Spectre.Console.Rendering;
using Spectre.Console;
using System.Data;
using System.Text;

namespace CXmlInvoiceGenerator.Print
{
    public class DataPrinter : IDataPrinter
    {
        private readonly IPrettyPrint _prettyPrint;

        public DataPrinter(IPrettyPrint prettyPrint)
        {
            _prettyPrint = prettyPrint;
        }

        public void PrintTable(DataTable dataTable, string tableTitle)
        {
            // Create a table
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            var table = _prettyPrint.CreateTableWithColumns(columnNames);

            // Add table rows
            foreach (DataRow row in dataTable.Rows)
            {
                var cellValues = new List<IRenderable>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    cellValues.Add(new Text(row[column].ToString() ?? string.Empty));
                }

                table.AddRow(cellValues);
            }

            // Render the table to the console
            _prettyPrint.RenderTable(table, tableTitle);
        }

        public void PrintDataRow(DataRow dataRow, IList<string> columnNames, string title)
        {
            var dataTable = CreateDataTable(dataRow, columnNames);
            PrintTable(dataTable, title);
        }

        private DataTable CreateDataTable(DataRow dataRow, IList<string> columnNames)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("CombinedData");
            var newRow = dataTable.NewRow();

            var combinedData = new StringBuilder();
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i > 0)
                {
                    combinedData.Append(", ");
                }

                combinedData.Append(dataRow[columnNames[i]]);
            }

            newRow["CombinedData"] = combinedData.ToString();
            dataTable.Rows.Add(newRow);
            return dataTable;
        }
    }
}
