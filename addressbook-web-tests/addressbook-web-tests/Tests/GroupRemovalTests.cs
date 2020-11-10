using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            applicationManager.GroupHelper.EnsureGroupExists();

            List<GroupData> initialGroupsList = GroupData.GetGroupsListFromDb();
            GroupData toBeRemoved = initialGroupsList[0];

            applicationManager.GroupHelper.Remove(toBeRemoved);

            List<GroupData> modifiedGroupsList = GroupData.GetGroupsListFromDb();

            Assert.AreEqual(initialGroupsList.Count - 1, applicationManager.GroupHelper.GetGroupsCount());
            
            initialGroupsList.RemoveAt(0);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);

            foreach (GroupData group in modifiedGroupsList)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
