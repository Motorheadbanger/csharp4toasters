using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            applicationManager.ContactsHelper.RemoveContact(1);
            applicationManager.NavigationHelper.ReturnToHomePage();
        }
    }
}
