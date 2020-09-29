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
        }

        [TearDown]
        public void TeardownTest()
        {
            applicationManager.Stop();
        }
    }
}
