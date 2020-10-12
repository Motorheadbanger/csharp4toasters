using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthenticationTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            applicationManager.GroupHelper.Remove(1);
        }
    }
}
