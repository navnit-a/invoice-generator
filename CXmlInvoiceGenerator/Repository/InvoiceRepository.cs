using System.Data;
using CXmlInvoiceGenerator.Factory;
using CXmlInvoiceGenerator.Models;

namespace CXmlInvoiceGenerator.Repository;

/// <summary>
///     I've used a Repo to abstract the DB calls from the controller (here it's program.cs).
///     Controller -> Repo -> Factory -> DB
/// </summary>
public class InvoiceRepository : IInvoiceRepository
{
    private readonly IInvoiceDbFactory _invoiceDbFactory;

    public InvoiceRepository(IInvoiceDbFactory invoiceDbFactory)
    {
        _invoiceDbFactory = invoiceDbFactory;
    }

    public List<NewInvoice> GetNewInvoices()
    {
        var invoicesDataTable = _invoiceDbFactory.InvoicesDb.GetNewInvoices();
        var invoicesList = new List<NewInvoice>();

        foreach (DataRow row in invoicesDataTable.Rows)
        {
            var invoice = new NewInvoice
            {
                Id = Convert.ToInt32(row["Id"]),
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                SalesOrderId = Convert.ToInt32(row["SalesOrderId"]),
                CurrencyCode = row["CurrencyCode"].ToString(),
                NetAmount = Convert.ToDecimal(row["NetAmount"]),
                VATAmount = Convert.ToDecimal(row["VATAmount"]),
                GrossAmount = Convert.ToDecimal(row["GrossAmount"]),
                VATCode = row["VATCode"].ToString(),
                VATPercentage = Convert.ToInt32(row["VATPercentage"]),
                PaymentTermsDays = Convert.ToInt32(row["PaymentTermsDays"])
            };

            invoicesList.Add(invoice);
        }

        return invoicesList;
    }


    public List<InvoiceItem> GetItemsOnInvoice(int invoiceId)
    {
        var itemsDataTable = _invoiceDbFactory.InvoicesDb.GetItemsOnInvoice(invoiceId);
        var invoiceItems = new List<InvoiceItem>();

        foreach (DataRow row in itemsDataTable.Rows)
        {
            var item = new InvoiceItem
            {
                Id = Convert.ToInt32(row["Id"]),
                InvoiceId = Convert.ToInt32(row["InvoiceId"]),
                StockItemId = Convert.ToInt32(row["StockItemId"]),
                Manufacturer = row["Manufacturer"].ToString(),
                PartNo = row["PartNo"].ToString(),
                Description = row["Description"].ToString(),
                Qty = Convert.ToInt32(row["Qty"]),
                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                LineTotal = Convert.ToDecimal(row["LineTotal"])
            };

            invoiceItems.Add(item);
        }

        return invoiceItems;
    }


    public BillingAddress GetBillingAddressForInvoice(int invoiceId)
    {
        var row = _invoiceDbFactory.InvoicesDb.GetBillingAddressForInvoice(invoiceId);

        var billingAddress = new BillingAddress
        {
            ContactName = row["ContactName"].ToString(),
            AddrLine1 = row["AddrLine1"].ToString(),
            AddrLine2 = row["AddrLine2"].ToString(),
            AddrLine3 = row["AddrLine3"].ToString(),
            AddrLine4 = row["AddrLine4"].ToString(),
            AddrLine5 = row["AddrLine5"].ToString(),
            AddrPostCode = row["AddrPostCode"].ToString(),
            CountryCode = row["CountryCode"].ToString(),
            Country = row["Country"].ToString()
        };

        return billingAddress;
    }

    public DeliveryAddress GetDeliveryAddressForSalesOrder(int salesOrderId)
    {
        var row = _invoiceDbFactory.InvoicesDb.GetDeliveryAddressForSalesOrder(salesOrderId);

        var deliveryAddress = new DeliveryAddress
        {
            ContactName = row["ContactName"].ToString(),
            AddrLine1 = row["AddrLine1"].ToString(),
            AddrLine2 = row["AddrLine2"].ToString(),
            AddrLine3 = row["AddrLine3"].ToString(),
            AddrLine4 = row["AddrLine4"].ToString(),
            AddrLine5 = row["AddrLine5"].ToString(),
            AddrPostCode = row["AddrPostCode"].ToString(),
            CountryCode = row["CountryCode"].ToString(),
            Country = row["Country"].ToString()
        };

        return deliveryAddress;
    }
}