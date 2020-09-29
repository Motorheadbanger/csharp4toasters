using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            applicationManager.ContactsHelper.ModifyContact(1, new ContactData("Elake", "Laiset"));
            applicationManager.NavigationHelper.ReturnToHomePage();
        }
    }
}
