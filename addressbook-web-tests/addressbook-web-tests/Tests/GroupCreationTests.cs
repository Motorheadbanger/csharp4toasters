using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("Group name 1")
            {
                Header = "Header 1",
                Footer = "Footer 1"
            };

            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();

            applicationManager.GroupHelper.Create(group);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            initialGroupsList.Add(group);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("")
            {
                Header = "",
                Footer = ""
            };

            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();

            applicationManager.GroupHelper.Create(group);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            initialGroupsList.Add(group);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }
    }
}
