using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthenticationTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }

            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();

            applicationManager.GroupHelper.Create(group);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            Assert.AreEqual(initialGroupsList.Count + 1, applicationManager.GroupHelper.GetGroupsCount());

            initialGroupsList.Add(group);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }
    }
}
