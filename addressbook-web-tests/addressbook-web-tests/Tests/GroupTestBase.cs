using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class GroupTestBase : AuthenticationTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<GroupData> fromUI = applicationManager.GroupHelper.GetGroupsList();
                List<GroupData> fromDb = GroupData.GetGroupsListFromDb();

                fromUI.Sort();
                fromDb.Sort();

                Assert.AreEqual(fromUI, fromDb);
            }
        }
    }
}
