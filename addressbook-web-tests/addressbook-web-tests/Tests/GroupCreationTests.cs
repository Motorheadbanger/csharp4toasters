using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            applicationManager.NavigationHelper.GoToHomePage();
            applicationManager.LoginHelper.Login(new AccountData("admin", "secret"));
            applicationManager.NavigationHelper.GoToGroupsPage();
            applicationManager.GroupHelper.InitGroupCreation();

            GroupData group = new GroupData("Group name 1");
            group.Header = "Header 1";
            group.Footer = "Footer 1";

            applicationManager.GroupHelper.FillGroupForm(group);
            applicationManager.GroupHelper.SubmitGroupCreation();
            applicationManager.GroupHelper.ReturnToGroupsPage();
        }
    }
}
