using DatabaseAccess;
using System.Data;

namespace CXmlInvoiceGenerator
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("New invoice generation run starting at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            GenerateCXMLForNewInvoices();
            Console.WriteLine("New invoice generation run finishing at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }


        private static void GenerateCXMLForNewInvoices()
        {

            // == Please complete this function ==

            // 1) Using the DatabaseAccess dll provided and referenced (in the refs folder), load each invoice from the database
            //
            // 2) Create a cXml invoice document using the information from each invoice

            // The following is a very helpful resource for cXml:

            // https://compass.coupa.com/en-us/products/product-documentation/supplier-resources/supplier-integration-resources/standard-invoice-examples/sample-cxml-invoice-with-payment-terms

            // Assume the invoice is raised on the same day you find it, so PaymentTerms is from Today

            // VAT mode is header (overall total) only, not at item level

            // 3) Save the created invoices into a specified output file with the .xml file extension

            // The "purpose" for each invoice is "standard"
            // The "operation" for each invoice is "new"
            // The output folder is entirely up to you, based on your file system
            // You can use "fake" credentials (Domain/Identity/SharedSecret etc. etc.) of your own choosing for the From/To/Sender section for this test
            //
            // It would likely be a good idea for all of these to be configurable in some way, in a .Net options/settings file or an external ini file

            // Ideally, you will write reasonable progress steps to the console window

            // You may add references to anything you want from the standard Nuget URL

            // You may modify the signature to this function if you want to pass values into it

            // You may move this code into another class (or indeed classes) of your choosing

            Invoices invoiceDB = new();
            DataTable newInvoices = invoiceDB.GetNewInvoices();

            // invoiceDB contains other functions you will need...

        }




    }
}