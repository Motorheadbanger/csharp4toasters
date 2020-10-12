using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("Group name 1");
            group.Header = "Header 1";
            group.Footer = "Footer 1";

            applicationManager.GroupHelper.Create(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            applicationManager.GroupHelper.Create(group);
        }
    }
}
