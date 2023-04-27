namespace CXmlInvoiceGenerator.Operations;

public static class ColumnInfo
{
    public static IList<string> GetColumnAddressNames()
    {
        return new List<string>
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
    }
}