using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            applicationManager.GroupHelper.EnsureGroupExists();

            GroupData groupData = new GroupData("Group name 2")
            {
                Header = "Header 2",
                Footer = "Footer 2"
            };

            List<GroupData> initialGroupsList = GroupData.GetGroupsListFromDb();
            GroupData toBeModified = initialGroupsList[0];

            applicationManager.GroupHelper.Modify(toBeModified, groupData);

            List<GroupData> modifiedGroupsList = GroupData.GetGroupsListFromDb();

            Assert.AreEqual(initialGroupsList.Count, applicationManager.GroupHelper.GetGroupsCount());

            initialGroupsList[0].Name = groupData.Name;
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);

            foreach (GroupData group in modifiedGroupsList)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(groupData.Name, group.Name);
                }
            }
        }
    }
}
