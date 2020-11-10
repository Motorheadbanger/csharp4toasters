using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthenticationTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(15)));
            }

            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> initialContactList = applicationManager.ContactsHelper.GetContactsList();

            applicationManager.ContactsHelper.AddContact(contact);

            List<ContactData> modifiedContactList = applicationManager.ContactsHelper.GetContactsList();

            initialContactList.Add(contact);
            initialContactList.Sort();
            modifiedContactList.Sort();

            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
