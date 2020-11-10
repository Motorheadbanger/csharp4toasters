using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
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

        public static IEnumerable<ContactData> GetContactsDataFromXml()
        {
            return (List<ContactData>) new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> GetContactsDataFromJson()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("GetContactsDataFromXml")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> initialContactList = ContactData.GetContactsListFromDb();

            applicationManager.ContactsHelper.AddContact(contact);

            List<ContactData> modifiedContactList = ContactData.GetContactsListFromDb();

            initialContactList.Add(contact);
            initialContactList.Sort();
            modifiedContactList.Sort();

            Assert.AreEqual(initialContactList, modifiedContactList);
        }
    }
}
