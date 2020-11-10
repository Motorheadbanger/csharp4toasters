using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebAddressBookTests
{
    public class ContactsHelper : HelperBase
    {
        private List<ContactData> contactsCache = null;

        public ContactsHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public ContactData GetContactContentFromTable(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactContentFromEditForm(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            GoToContactEditForm(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string home = driver.FindElement(By.Name("phone2")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Home = home
            };
        }

        public ContactsHelper AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            WaitForDbAmendment();
            contactsCache = null;
            applicationManager.NavigationHelper.GoToHomePage();

            return this;
        }

        public List<ContactData> GetContactsList()
        {
            if (contactsCache == null)
            {
                contactsCache = new List<ContactData>();

                applicationManager.NavigationHelper.GoToHomePage();

                ICollection<IWebElement> elementsList = driver.FindElements(By.XPath("//tr[contains(@name,'entry')]"));

                foreach (var element in elementsList)
                {
                    var contactData = new ContactData("", "")
                    {
                        FirstName = element.FindElement(By.XPath("./td[3]")).Text,
                        LastName = element.FindElement(By.XPath("./td[2]")).Text
                    };

                    contactsCache.Add(contactData);
                }
            }

            return new List<ContactData>(contactsCache);
        }

        public ContactsHelper ModifyContact(int index, ContactData contactData)
        {
            if (!IsElementPresent(By.XPath("//img[@title='Edit'][" + index + 1 + "]")))
                AddContact(new ContactData("emergency", "contact"));

            GoToContactEditForm(index);
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            WaitForDbAmendment();
            contactsCache = null;

            return this;
        }
        public ContactsHelper ModifyContact(ContactData contact, ContactData contactData)
        {
            if (!IsElementPresent(By.XPath("//a[@href='edit.php?id=" + contact.Id + "']")))
                AddContact(new ContactData("emergency", "contact"));

            GoToContactEditForm(contact.Id);
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            WaitForDbAmendment();
            contactsCache = null;

            return this;
        }

        public ContactsHelper RemoveContact(int index)
        {
            driver.FindElement(By.XPath("//input[contains(@title,'Select')][" + index + 1 + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            WaitForDbAmendment();
            contactsCache = null;

            return this;
        }

        public ContactsHelper RemoveContact(ContactData contact)
        {
            driver.FindElement(By.Id(contact.Id)).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            WaitForDbAmendment();
            contactsCache = null;

            return this;
        }

        public ContactsHelper GoToContactEditForm(int index)
        {
            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + 1 + "]")).Click();

            return this;
        }

        public ContactsHelper GoToContactEditForm(string contactId)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + contactId + "']")).Click();

            return this;
        }

        public ContactsHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroup(group.Name);
            CommitAddingContactToGroup();
            WaitForDbAmendment();

            return this;
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public int GetSearchResultsCount()
        {
            applicationManager.NavigationHelper.GoToHomePage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match match = new Regex(@"\d+").Match(text);
            return int.Parse(match.Value);
        }

        private void FillContactInfo(ContactData contactData)
        {
            FillField(By.Name("firstname"), contactData.FirstName);
            FillField(By.Name("lastname"), contactData.LastName);
        }

        public int GetContactsCount()
        {
            applicationManager.NavigationHelper.GoToHomePage();
            return driver.FindElements(By.XPath("//tr[contains(@name,'entry')]")).Count;
        }

        public void EnsureContactExists()
        {
            if (GetContactsCount() == 0)
            {
                AddContact(new ContactData("emergency", "contact"));
            }

            Assert.IsTrue(GetContactsCount() > 0);
        }

        private void WaitForDbAmendment()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
    }
}
