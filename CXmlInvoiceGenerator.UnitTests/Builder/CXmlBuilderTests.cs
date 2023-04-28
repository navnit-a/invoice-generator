using CXmlInvoiceGenerator.Builder;
using CXmlInvoiceGenerator.Models;
using System.Xml.Linq;

namespace CXmlInvoiceGenerator.UnitTests.Builder
{
    public class CXmlBuilderTests
    {
        [Fact]
        public void AddHeader_CreatesHeaderElement()
        {
            // Arrange
            var builder = new CXmlBuilder();

            // Act
            builder.AddHeader("FromIdentity", "ToIdentity", "SenderIdentity", "SharedSecret", "UserAgent");

            // Assert
            var header = builder.GetType().GetField("_header", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(builder) as XElement;
            Assert.NotNull(header);
            Assert.Equal("FromIdentity", header.Element("From").Element("Credential").Element("Identity").Value);
            Assert.Equal("ToIdentity", header.Element("To").Element("Credential").Element("Identity").Value);
            Assert.Equal("SenderIdentity", header.Element("Sender").Element("Credential").Element("Identity").Value);
            Assert.Equal("SharedSecret", header.Element("Sender").Element("Credential").Element("SharedSecret").Value);
            Assert.Equal("UserAgent", header.Element("Sender").Element("UserAgent").Value);
        }

        [Fact]
        public void AddInvoiceDetailRequest_CreatesInvoiceDetailRequestElement()
        {
            // Arrange
            var builder = new CXmlBuilder();
            var billingAddress = new BillingAddress
            {
                AddrLine1 = "BillingAddr1",
                AddrLine2 = "BillingAddr2",
                AddrLine3 = "BillingAddr3",
                AddrLine4 = "BillingAddr4",
                AddrPostCode = "BillingPostCode",
                Country = "BillingCountry",
                CountryCode = "BillingCountryCode"
            };
            var deliveryAddress = new DeliveryAddress
            {
                AddrLine1 = "DeliveryAddr1",
                AddrLine2 = "DeliveryAddr2",
                AddrLine3 = "DeliveryAddr3",
                AddrLine4 = "DeliveryAddr4",
                AddrPostCode = "DeliveryPostCode",
                Country = "DeliveryCountry",
                CountryCode = "DeliveryCountryCode"
            };

            // Act
            builder.AddInvoiceDetailRequest(1, "purpose", "operation", "2023-04-28", 30, billingAddress, deliveryAddress);

            // Assert
            var invoiceDetailRequest = builder.GetType().GetField("_invoiceDetailRequest", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(builder) as XElement;
            Assert.NotNull(invoiceDetailRequest);
            Assert.Equal("1", invoiceDetailRequest.Element("InvoiceDetailRequestHeader").Attribute("invoiceID").Value);
            Assert.Equal("purpose", invoiceDetailRequest.Element("InvoiceDetailRequestHeader").Attribute("purpose").Value);
            Assert.Equal("operation", invoiceDetailRequest.Element("InvoiceDetailRequestHeader").Attribute("operation").Value);
            Assert.Equal("2023-04-28", invoiceDetailRequest.Element("InvoiceDetailRequestHeader").Attribute("invoiceDate").Value);
        }
    }
}
