using System.Globalization;
using System.Xml.Linq;
using CXmlInvoiceGenerator.Models;

namespace CXmlInvoiceGenerator.Builder;

public class CXmlBuilder : ICXmlBuilder
{
    private XElement _header;
    private XElement _invoiceDetailRequest;

    public ICXmlBuilder AddHeader(
        string fromIdentity, 
        string toIdentity, 
        string senderIdentity, 
        string sharedSecret,
        string userAgent)
    {
        _header = new XElement("Header",
            new XElement("From",
                new XElement("Credential", new XAttribute("domain", "DUNS"),
                    new XElement("Identity", fromIdentity))),
            new XElement("To",
                new XElement("Credential", new XAttribute("domain", "NetworkID"),
                    new XElement("Identity", toIdentity))),
            new XElement("Sender",
                new XElement("Credential", new XAttribute("domain", "DUNS"),
                    new XElement("Identity", senderIdentity),
                    new XElement("SharedSecret", sharedSecret)),
                new XElement("UserAgent", userAgent)));

        return this;
    }

    public ICXmlBuilder AddInvoiceDetailRequest(
        int invoiceId,
        string purpose,
        string operation,
        string invoiceDate,
        int paymentTermsDays,
        BillingAddress billingAddress, 
        DeliveryAddress deliveryAddress)
    {
        _invoiceDetailRequest = new XElement("InvoiceDetailRequest",
            new XElement("InvoiceDetailRequestHeader", new XAttribute("invoiceID", invoiceId),
                new XAttribute("purpose", purpose), new XAttribute("operation", operation),
                new XAttribute("invoiceDate", invoiceDate),
                new XElement("InvoiceDetailHeaderIndicator"),
                new XElement("InvoiceDetailLineIndicator", new XAttribute("isAccountingInLine", "yes")),
                new XElement("InvoicePartner",
                    new XElement("Contact", new XAttribute("role", "soldTo"),
                        new XElement("Name", new XAttribute(XNamespace.Xml + "lang", "en-US"), deliveryAddress.AddrLine1),
                        new XElement("PostalAddress",
                            new XElement("Street", deliveryAddress.AddrLine2),
                            new XElement("City", deliveryAddress.AddrLine3),
                            new XElement("State", deliveryAddress.AddrLine4),
                            new XElement("PostalCode", deliveryAddress.AddrPostCode),
                            new XElement("Country", new XAttribute("isoCountryCode", deliveryAddress.CountryCode), deliveryAddress.Country)))),
                new XElement("InvoicePartner",
                    new XElement("Contact", new XAttribute("role", "billTo"), new XAttribute("addressID", "1057"),
                        new XElement("Name", new XAttribute(XNamespace.Xml + "lang", "en-US"), billingAddress.AddrLine1),
                        new XElement("PostalAddress",
                            new XElement("Street", billingAddress.AddrLine2),
                            new XElement("City", billingAddress.AddrLine3),
                            new XElement("State", billingAddress.AddrLine4),
                            new XElement("PostalCode", billingAddress.AddrPostCode),
                            new XElement("Country", new XAttribute("isoCountryCode", billingAddress.CountryCode), billingAddress.Country)))),
                new XElement("PaymentTerm", new XAttribute("payInNumberofDays", paymentTermsDays))));

        return this;
    }

    public ICXmlBuilder AddInvoiceDetailOrder(NewInvoice newInvoice, List<InvoiceItem> itemsOnInvoice)
    {
        XNamespace xmlNamespace = "http://www.w3.org/XML/1998/namespace";

        var count = 0;
        foreach (var invoiceItem in itemsOnInvoice)
        {
            _invoiceDetailRequest.Add(
                new XElement("InvoiceDetailOrder",
                    new XElement("InvoiceDetailOrderInfo",
                        new XElement("OrderReference",
                            new XElement("DocumentReference", new XAttribute("payloadID", newInvoice.Id)))),
                    new XElement("InvoiceDetailItem", new XAttribute("invoiceLineNumber", 1),
                        new XAttribute("quantity", invoiceItem.Qty),
                        new XElement("UnitOfMeasure", "EA"),
                        new XElement("UnitPrice",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), invoiceItem.UnitPrice)),
                        new XElement("InvoiceDetailItemReference", new XAttribute("lineNumber", count++),
                            new XElement("ItemID",
                                new XElement("SupplierPartID", invoiceItem.PartNo)),
                            new XElement("Description", new XAttribute(xmlNamespace + "lang", "en-US"), invoiceItem.Description),
                            new XElement("ManufacturerPartID", invoiceItem.PartNo),
                            new XElement("ManufacturerName", new XAttribute(xmlNamespace + "lang", "en-US"), invoiceItem.Manufacturer)),
                        new XElement("SubtotalAmount",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.GrossAmount)),
                        new XElement("GrossAmount",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.GrossAmount)),
                        new XElement("NetAmount",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.NetAmount)))));
        }

        return this;
    }

    public ICXmlBuilder AddInvoiceDetailSummary(NewInvoice newInvoice)
    {
        XNamespace xmlNamespace = "http://www.w3.org/XML/1998/namespace";

        _invoiceDetailRequest.Add(
            new XElement("InvoiceDetailSummary",
                new XElement("SubtotalAmount",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.NetAmount)),
                new XElement("Tax",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.VATPercentage),
                    new XElement("Description", new XAttribute(xmlNamespace + "lang", "en-US")),
                    new XElement("TaxDetail", new XAttribute("purpose", "tax"), new XAttribute("category", "sales"),
                        new XAttribute("percentageRate", "0"),
                        new XElement("TaxableAmount",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.NetAmount)),
                        new XElement("TaxAmount",
                            new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.VATAmount)),
                        new XElement("TaxLocation", new XAttribute(xmlNamespace + "lang", "en-US"), "UK"))),
                new XElement("ShippingAmount",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), "0.0")),
                new XElement("GrossAmount",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.GrossAmount)),
                new XElement("NetAmount",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.NetAmount)),
                new XElement("DueAmount",
                    new XElement("Money", new XAttribute("currency", newInvoice.CurrencyCode), newInvoice.GrossAmount))));

        return this;
    }

    public XDocument Build()
    {
        var cXml = new XElement("cXML", new XAttribute("version", "1.0"),
            new XAttribute("payloadID", "xxx.xxxx@example.coupahost.com"),
            new XAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")),
            _header,
            new XElement(
                "Request", 
                new XAttribute("deploymentMode", "production"),
                _invoiceDetailRequest));

        return new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XDocumentType("cXML", null, "http://xml.cxml.org/schemas/cXML/1.2/InvoiceDetail.dtd", null), cXml);
    }
}