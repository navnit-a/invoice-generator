using System.Xml.Linq;

namespace CXmlInvoiceGenerator.FilesManager;

public interface IFilesOps
{
    void SaveXmlToFile(XDocument xmlDocument, string outputDirectory, int fileId);
}