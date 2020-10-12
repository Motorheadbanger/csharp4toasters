using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthenticationTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            applicationManager.ContactsHelper.RemoveContact(1);
            applicationManager.NavigationHelper.ReturnToHomePage();
        }
    }
}
