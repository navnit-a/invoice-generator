using CXmlInvoiceGenerator.Operations;

namespace CXmlInvoiceGenerator.UnitTests.Common
{
    public class ColumnInfoTests
    {
        [Fact]
        public void GetColumnAddressNames_Returns_Correct_Column_Names()
        {
            // Arrange
            var expectedColumnNames = new List<string>
            {
                "ContactName",
                "AddrLine1",
                "AddrLine2",
                "AddrLine3",
                "AddrLine4",
                "AddrLine5",
                "AddrPostCode",
                "CountryCode",
                "Country"
            };

            // Act
            var actualColumnNames = ColumnInfo.GetColumnAddressNames();

            // Assert
            Assert.Equal(expectedColumnNames, actualColumnNames);
        }
    }
}
