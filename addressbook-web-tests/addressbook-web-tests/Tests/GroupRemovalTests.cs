using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            applicationManager.NavigationHelper.GoToHomePage();
            applicationManager.LoginHelper.Login(new AccountData("admin", "secret"));
            applicationManager.NavigationHelper.GoToGroupsPage();
            applicationManager.GroupHelper.SelectGroup(1);
            applicationManager.GroupHelper.RemoveGroup();
            applicationManager.GroupHelper.ReturnToGroupsPage();
        }
    }
}
