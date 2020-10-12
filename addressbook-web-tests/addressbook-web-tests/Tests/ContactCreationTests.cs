using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            applicationManager.ContactsHelper.AddContact(new ContactData("Psycho", "Mantis"));
            applicationManager.NavigationHelper.ReturnToHomePage();
        }
    }
}
