using System;

namespace WebAddressBookTests
{
    class ContactData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; }
        public string Nickname { get; set; } = "";
        public string Photo { get; set; } = "";
        public string Title { get; set; } = "";
        public string Company { get; set; } = "";
        public string Address { get; set; } = "";
        public string HomePhone { get; set; } = "";
        public string MobilePhone { get; set; } = "";
        public string WorkPhone { get; set; } = "";
        public string Fax { get; set; } = "";
        public string Email1 { get; set; } = "";
        public string Email2 { get; set; } = "";
        public string Email3 { get; set; } = "";
        public string Homepage { get; set; } = "";
        public DateTime Birthday { get; set; } = new DateTime();
        public DateTime Anniversary { get; set; } = new DateTime();
        public string Group { get; set; } = "";
        public string SecondaryAddress { get; set; } = "";
        public string Home { get; set; } = "";
        public string Notes { get; set; } = "";


        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
