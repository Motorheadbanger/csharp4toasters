using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            applicationManager.ContactsHelper.EnsureContactExists();

            List<ContactData> initialContactList = ContactData.GetContactsListFromDb();
            ContactData toBeModified = initialContactList[0];

            applicationManager.ContactsHelper.ModifyContact(toBeModified, new ContactData("Elake", "Laiset"));

            List<ContactData> modifiedContactList = ContactData.GetContactsListFromDb();

            initialContactList[0] = new ContactData("Elake", "Laiset");
            
            applicationManager.NavigationHelper.GoToHomePage();

            initialContactList.Sort();
            modifiedContactList.Sort();
            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
