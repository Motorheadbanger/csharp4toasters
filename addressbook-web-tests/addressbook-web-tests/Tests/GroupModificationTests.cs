using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData groupData = new GroupData("Group name 2")
            {
                Header = "Header 2",
                Footer = "Footer 2"
            };

            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();

            applicationManager.GroupHelper.Modify(0, groupData);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            initialGroupsList[0].Name = groupData.Name;
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }
    }
}
