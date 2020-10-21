using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> initialContactList = applicationManager.ContactsHelper.GetContactsList();

            applicationManager.ContactsHelper.AddContact(new ContactData("Psycho", "Mantis"));

            List<ContactData> modifiedContactList = applicationManager.ContactsHelper.GetContactsList();

            initialContactList.Add(new ContactData("Psycho", "Mantis"));
            initialContactList.Sort();
            modifiedContactList.Sort();

            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
