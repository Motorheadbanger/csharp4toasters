using OpenQA.Selenium;
using System;

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
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contactData.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contactData.LastName);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public ContactsHelper ModifyContact(int index, ContactData contactData)
        {
            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + "]")).Click();
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contactData.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contactData.LastName);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public ContactsHelper RemoveContact(int index)
        {
            driver.FindElement(By.XPath("//input[contains(@title,'Select')][" + index + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }
    }
}
