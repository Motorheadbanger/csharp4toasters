using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData groupData = new GroupData("Group name 2");
            groupData.Header = "Header 2";
            groupData.Footer = "Footer 2";

            applicationManager.GroupHelper.Modify(1, groupData);
        }
    }
}
