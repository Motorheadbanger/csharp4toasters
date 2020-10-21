using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthenticationTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> initialContactList = applicationManager.ContactsHelper.GetContactsList();

            applicationManager.ContactsHelper.RemoveContact(0);

            List<ContactData> modifiedContactList = applicationManager.ContactsHelper.GetContactsList();

            initialContactList.RemoveAt(0);

            applicationManager.NavigationHelper.GoToHomePage();

            initialContactList.Sort();
            modifiedContactList.Sort();

            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
