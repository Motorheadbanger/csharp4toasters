namespace WebAddressBookTests
{
    public class GroupData
    {
        public GroupData(string name)
        {
            Name = name;
        }

        public GroupData(string name, string header, string footer)
        {
            Name = name;
            Header = header;
            Footer = footer;
        }

        public string Name { get; set; }

        public string Header { get; set; } = "";

        public string Footer { get; set; } = "";
    }
}
