using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class GroupHelper : HelperBase
    {
        private List<GroupData> groupsCache = null;

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

        public List<GroupData> GetGroupsList()
        {
            if (groupsCache == null)
            {
                groupsCache = new List<GroupData>();

                applicationManager.NavigationHelper.GoToGroupsPage();

                ICollection<IWebElement> elementsList = driver.FindElements(By.CssSelector("span.group"));

                foreach (var element in elementsList)
                {
                    groupsCache.Add(new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<GroupData>(groupsCache);
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

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupsCache = null;

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
            groupsCache = null;

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
            groupsCache = null;

            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            if (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index  + 1 + "]")))
                Create(new GroupData("emergency group"));

            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + 1 + "]")).Click();
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
