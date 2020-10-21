using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthenticationTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();
            GroupData toBeRemoved = initialGroupsList[0];

            applicationManager.GroupHelper.Remove(0);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

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
