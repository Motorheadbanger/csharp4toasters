﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class ContactsHelper : HelperBase
    {
        public ContactsHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public ContactsHelper AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            applicationManager.NavigationHelper.GoToHomePage();
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> contactsList = new List<ContactData>();

            applicationManager.NavigationHelper.GoToHomePage();

            ICollection<IWebElement> elementsList = driver.FindElements(By.XPath("//tr[contains(@name,'entry')]"));

            foreach (var element in elementsList)
            {
                var contactData = new ContactData("", "");
                contactData.FirstName = element.FindElement(By.XPath("./td[3]")).Text;
                contactData.LastName = element.FindElement(By.XPath("./td[2]")).Text;
                contactsList.Add(contactData);
            }

            return contactsList;
        }

        public ContactsHelper ModifyContact(int index, ContactData contactData)
        {
            if (!IsElementPresent(By.XPath("//img[@title='Edit'][" + index + 1 + "]")))
                AddContact(new ContactData("emergency", "contact"));

            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + 1 + "]")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
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
            return this;
        }

        private void FillContactInfo(ContactData contactData)
        {
            FillField(By.Name("firstname"), contactData.FirstName);
            FillField(By.Name("lastname"), contactData.LastName);
        }
    }
}
