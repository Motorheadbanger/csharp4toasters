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

            applicationManager.GroupHelper.Remove(0);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            initialGroupsList.RemoveAt(0);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }
    }
}
