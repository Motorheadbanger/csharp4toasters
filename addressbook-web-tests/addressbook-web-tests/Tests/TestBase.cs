using NUnit.Framework;

namespace WebAddressBookTests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;

        [SetUp]
        public void SetupTest()
        {
            applicationManager = new ApplicationManager();
            applicationManager.NavigationHelper.GoToHomePage();
            applicationManager.LoginHelper.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            applicationManager.Stop();
        }
    }
}
