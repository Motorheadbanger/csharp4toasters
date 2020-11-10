using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            applicationManager.ContactsHelper.EnsureContactExists();

            List<ContactData> initialContactList = ContactData.GetContactsListFromDb();
            ContactData toBeRemoved = initialContactList[0];

            applicationManager.ContactsHelper.RemoveContact(toBeRemoved);

            List<ContactData> modifiedContactList = ContactData.GetContactsListFromDb();

            initialContactList.Remove(toBeRemoved);

            applicationManager.NavigationHelper.GoToHomePage();

            initialContactList.Sort();
            modifiedContactList.Sort();

            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
