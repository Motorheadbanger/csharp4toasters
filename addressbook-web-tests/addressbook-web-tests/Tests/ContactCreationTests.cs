using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            applicationManager.NavigationHelper.GoToHomePage();
            applicationManager.LoginHelper.Login(new AccountData("admin", "secret"));
            applicationManager.ContactsHelper.AddContact(new ContactData("Psycho", "Mantis"));
            applicationManager.NavigationHelper.ReturnToHomePage();
        }
    }
}
