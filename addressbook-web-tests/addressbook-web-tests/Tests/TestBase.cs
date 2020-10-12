using NUnit.Framework;

namespace WebAddressBookTests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;

        [SetUp]
        public void SetupApplicationManager()
        {
            applicationManager = ApplicationManager.GetInstance();
        }
    }
}
