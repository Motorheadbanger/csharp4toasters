using LinqToDB.Mapping;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressBookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; } = "";

        [Column(Name = "group_footer")]
        public string Footer { get; set; } = "";

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public GroupData()
        {
        }

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

        public bool Equals(GroupData other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (other is null)
                return 1;

            return Name.CompareTo(other.Name);
        }

        public static List<GroupData> GetGroupsListFromDb()
        {
            using (AddressbookDB database = new AddressbookDB())
            {
                return (from g in database.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressbookDB database = new AddressbookDB())
            {
                return (from c in database.Contacts
                            from gcr in database.GroupContactRelations.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                            select c).Distinct().ToList();
            }
        }
    }
}
