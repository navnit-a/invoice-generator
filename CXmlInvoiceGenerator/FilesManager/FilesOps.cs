using System.Xml.Linq;
using CXmlInvoiceGenerator.Print;

namespace CXmlInvoiceGenerator.FilesManager;

public class FilesOps : IFilesOps
{
    private readonly IPrettyPrint _prettyPrint;

    public FilesOps(IPrettyPrint prettyPrint)
    {
        _prettyPrint = prettyPrint;
    }

    public void SaveXmlToFile(XDocument xmlDocument, string outputDirectory, int fileId)
    {
        var outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputDirectory);
        Directory.CreateDirectory(outputPath);

        var fileName = $"cXmlOutput_{fileId}.xml";
        var outputFilePath = Path.Combine(outputPath, fileName);

        xmlDocument.Save(outputFilePath);
        _prettyPrint.PrintText($"cXML Invoice ID {fileId} saved at -> " + outputPath, "cyan");
    }
}