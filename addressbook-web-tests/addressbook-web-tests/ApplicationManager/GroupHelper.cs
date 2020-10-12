using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper Modify(int index, GroupData groupData)
        {
            applicationManager.NavigationHelper.GoToGroupsPage();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(groupData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData groupData)
        {
            FillField(By.Name("group_name"), groupData.Name);
            FillField(By.Name("group_header"), groupData.Header);
            FillField(By.Name("group_footer"), groupData.Footer);
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            if (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]")))
                Create(new GroupData("emergency group"));

            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper Create(GroupData group)
        {
            applicationManager.NavigationHelper.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Remove(int index)
        {
            applicationManager.NavigationHelper.GoToGroupsPage();
            SelectGroup(index);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }
    }
}
