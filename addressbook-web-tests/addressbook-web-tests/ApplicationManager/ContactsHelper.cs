using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class ContactsHelper : HelperBase
    {
        private List<ContactData> contactsCache = null;

        public ContactsHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public ContactsHelper AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
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

            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + 1 + "]")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactsCache = null;

            return this;
        }

        public ContactsHelper RemoveContact(int index)
        {
            if (!IsElementPresent(By.XPath("//input[contains(@title,'Select')][" + index + 1 + "]")))
            {
                AddContact(new ContactData("emergency", "contact"));
            }

            driver.FindElement(By.XPath("//input[contains(@title,'Select')][" + index + 1 + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactsCache = null;

            return this;
        }

        private void FillContactInfo(ContactData contactData)
        {
            FillField(By.Name("firstname"), contactData.FirstName);
            FillField(By.Name("lastname"), contactData.LastName);
        }
    }
}
