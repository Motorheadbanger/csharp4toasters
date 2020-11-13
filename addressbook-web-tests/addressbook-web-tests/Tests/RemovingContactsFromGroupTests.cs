using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class RemovingContactsFromGroupTests : AuthenticationTestBase
    {
        [Test]
        public void RemoveContactFromGroupTest()
        {
            if (GroupData.GetGroupsListFromDb().Count == 0)
                applicationManager.GroupHelper.Create(new GroupData()
                {
                    Name = "emergency group"
                });

            GroupData group = GroupData.GetGroupsListFromDb()[0];

            if (group.GetContacts().Count == 0) 
            {
                if (ContactData.GetContactsListFromDb().Count == 0)
                {
                    applicationManager.ContactsHelper.AddContact(new ContactData
                    {
                        FirstName = "emergency",
                        LastName = "contact"
                    });
                }

                applicationManager.ContactsHelper.AddContactToGroup(ContactData.GetContactsListFromDb()[0], group);
            }

            List<ContactData> initialContactsList = group.GetContacts();

            ContactData toBeRemoved = initialContactsList[0];

            applicationManager.ContactsHelper.RemoveContactFromGroup(toBeRemoved, group);

            List<ContactData> modifiedContactsList = group.GetContacts();

            initialContactsList.Remove(toBeRemoved);
            initialContactsList.Sort();
            modifiedContactsList.Sort();

            Assert.AreEqual(initialContactsList, modifiedContactsList);
        }
    }
}
