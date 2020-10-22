using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            applicationManager.ContactsHelper.EnsureContactExists();

            List<ContactData> initialContactList = applicationManager.ContactsHelper.GetContactsList();

            applicationManager.ContactsHelper.ModifyContact(0, new ContactData("Elake", "Laiset"));

            List<ContactData> modifiedContactList = applicationManager.ContactsHelper.GetContactsList();

            initialContactList[0] = new ContactData("Elake", "Laiset");
            
            applicationManager.NavigationHelper.GoToHomePage();

            initialContactList.Sort();
            modifiedContactList.Sort();
            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
