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

        public string GetContactContentFromDetails(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            GoToDetailsPage(index);

            return driver.FindElement(By.Id("content")).Text;
        }

        private void GoToDetailsPage(int index)
        {
            driver.FindElement(By.XPath("(//img[@src='icons/status_online.png'])[" + (index + 1) + "]")).Click();
        }

        public ContactData GetContactContentFromEditForm(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            GoToContactEditForm(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string home = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string secondaryAddress = driver.FindElement(By.Name("address2")).Text;
            string notes = driver.FindElement(By.Name("notes")).Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                MiddleName = middleName,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Home = home,
                Nickname = nickname,
                Company = company,
                Title = title,
                Fax = fax,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,
                SecondaryAddress = secondaryAddress,
                Notes = notes
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

        public ContactsHelper DeleteContact(int index)
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
            driver.FindElement(By.XPath("(//img[@title='Edit'])[" + (index + 1) + "]")).Click();

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

        public ContactsHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            OpenGroupContactsList(group);
            SelectContact(contact.Id);
            CommitRemovingContact();
            WaitForDbAmendment();

            return this;
        }

        private void CommitRemovingContact()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void OpenGroupContactsList(GroupData group)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(group.Name);
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
