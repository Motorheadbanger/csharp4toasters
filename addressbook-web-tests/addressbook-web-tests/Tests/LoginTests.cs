using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            applicationManager.LoginHelper.Logout();

            var accountData = new AccountData("admin", "secret");

            applicationManager.LoginHelper.Login(accountData);

            Assert.IsTrue(applicationManager.LoginHelper.IsLoggedIn(accountData));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            applicationManager.LoginHelper.Logout();

            var accountData = new AccountData("admin", "123456");

            applicationManager.LoginHelper.Login(accountData);

            Assert.IsFalse(applicationManager.LoginHelper.IsLoggedIn(accountData));
        }
    }
}
