using System;
using System.Text.RegularExpressions;

namespace WebAddressBookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

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
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;

                return (PhoneFormat(HomePhone) + PhoneFormat(MobilePhone) + PhoneFormat(WorkPhone) + PhoneFormat(Home)).Trim();
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                    return allEmails;

                return (EMailFormat(Email1) + EMailFormat(Email2) + EMailFormat(Email3)).Trim();
            }
            set
            {
                allEmails = value;
            }
        }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        public int CompareTo(ContactData other)
        {
            if (other is null)
                return 1;

            if (LastName.CompareTo(other.LastName) == 0)
                return FirstName.CompareTo(other.FirstName);

            else return LastName.CompareTo(other.LastName);
        }

        private string PhoneFormat(string phone)
        {
            if (phone == null || phone == "")
                return "";

            return Regex.Replace(phone, "[ \\-()]", "") + "\r\n";
        }

        private string EMailFormat(string email)
        {
            if (email == null || email == "")
                return "";

            return email + "\r\n";
        }

        public override string ToString()
        {
            return "first name = " + FirstName + "\nlast name = " + LastName;
        }
    }
}
