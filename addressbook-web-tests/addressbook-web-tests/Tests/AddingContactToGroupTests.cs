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
            GroupData group = GroupData.GetGroupsListFromDb()[0];
            List<ContactData> initialContacts = group.GetContacts();
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
