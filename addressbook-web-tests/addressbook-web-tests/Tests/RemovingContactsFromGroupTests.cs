using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressBookTests
{
    public class RemovingContactsFromGroupTests : AuthenticationTestBase
    {
        [Test]
        public void RemoveContactFromGroupTest()
        {
            GroupData group = GroupData.GetGroupsListFromDb()[0];
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
