using NUnit.Framework;

namespace WebAddressBookTests
{
    public class AuthenticationTestBase : TestBase
    {
        [SetUp]
        public void SetupAuthentication()
        {
            applicationManager.LoginHelper.Login(new AccountData("admin", "secret"));
        }
    }
}
