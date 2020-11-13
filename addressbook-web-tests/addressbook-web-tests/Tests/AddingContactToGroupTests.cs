using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressBookTests
{
    class AddingContactToGroupTests : AuthenticationTestBase
    {
        [Test]
        public void AddingContactToGroup()
        {
            if (GroupData.GetGroupsListFromDb().Count == 0)
                applicationManager.GroupHelper.Create(new GroupData()
                {
                    Name = "emergency group"
                });

            GroupData group = GroupData.GetGroupsListFromDb()[0];

            List<ContactData> initialContacts = group.GetContacts();

            if (ContactData.GetContactsListFromDb().Except(initialContacts).FirstOrDefault() == null)
                applicationManager.ContactsHelper.AddContact(new ContactData
                {
                    FirstName = "emergency",
                    LastName = "contact"
                });

            ContactData contact = ContactData.GetContactsListFromDb().Except(initialContacts).First();

            applicationManager.ContactsHelper.AddContactToGroup(contact, group);

            List<ContactData> modifiedContacts = group.GetContacts();
            
            initialContacts.Add(contact);
            initialContacts.Sort();
            modifiedContacts.Sort();

            Assert.AreEqual(initialContacts, modifiedContacts);
        }
    }
}
